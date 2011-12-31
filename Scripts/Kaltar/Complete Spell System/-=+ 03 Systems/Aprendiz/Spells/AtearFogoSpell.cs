using System;
using Server.Targeting;
using Server.Network;
using Server.Spells;
using Kaltar.Util;

namespace Server.ACC.CSS.Systems.Aprendiz {
	
	public class AtearFogoSpell : AprendizSpell {
		
		private static SpellInfo m_Info = new SpellInfo(
				"Atear fogo",
                "Atear fogo",
				212,
				9041,
				Reagent.SulfurousAsh
		);

		public override SpellCircle Circle { get { return SpellCircle.Second; } }
		public override double RequiredSkill{ get{ return 30.0; } }
		public override double CastDelay{ get{ return 1.0; } }
		public override int RequiredMana   { get{ return 20; } }
		
		public AtearFogoSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info ) {
		}
		
		public override void OnCast() {
			Caster.Target = new InternalTarget( this );
		}

        public void Target(IPoint3D ponto)
        {
            if (CheckSequence())
            {
                TimeSpan duracao = TimeSpan.FromSeconds(Caster.Skills[CastSkill].Value / 2.0);
                FogoItem fogo = new FogoItem(duracao);

                Point3D loc = new Point3D(ponto.X, ponto.Y, ponto.Z);
                fogo.MoveToWorld(loc, Caster.Map);
            }

            FinishSequence();
        }

		public void Target( Mobile m ) {

			if ( !Caster.CanSee( m ) ) {
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( CheckHSequence( m ) )	{
				Mobile source = Caster;

				SpellHelper.Turn( source, m );
				SpellHelper.CheckReflect( (int)this.Circle, ref source, ref m );

                //duracao
                TimeSpan duracao = TimeSpan.FromSeconds(Caster.Skills[CastSkill].Value / 2);

                m.PlaySound(0x1DD);

                //ateia o fogo
                SummonUtil.Instance.atearFogo(m, duracao);
			}

			FinishSequence();
		}

        private class FogoItem : Item {

            private TimeSpan duracao;
            private DateTime final;

            public FogoItem(TimeSpan duracao)
                : base(0x19AB)
            {
                Visible = true;
                Movable = false;
                Light = LightType.Circle150;

                //duracao
                final = DateTime.Now + duracao;
                InternalTimer t = new InternalTimer(this, duracao);
                t.Start();
            }

            public FogoItem(Serial serial) : base(serial)
            {
            }

            public override bool OnMoveOver(Mobile m)
            {
                TimeSpan duracao = TimeSpan.FromSeconds(Utility.Random(5, 20));

                if(!ResistenciaUtil.Instance.resistiu(m, 0, ResistanceType.Fire)) {
                    SummonUtil.Instance.atearFogo(m, duracao);
                }

                return true;
            }

            public override void Serialize(GenericWriter writer)
            {
                base.Serialize(writer);
                writer.Write(0); // version
                writer.Write(final - DateTime.Now);
            }

            public override void Deserialize(GenericReader reader)
            {
                base.Deserialize(reader);
                int version = reader.ReadInt();
                duracao = reader.ReadTimeSpan();

                final = DateTime.Now + duracao;

                InternalTimer t = new InternalTimer(this, duracao);
                t.Start();
            }

            public override void OnAfterDelete()
            {
                base.OnAfterDelete();
            }

            private class InternalTimer : Timer {
                private FogoItem fogoItem;

                public InternalTimer(FogoItem fogoItem, TimeSpan duracao)
                    : base(duracao)
                {
                    this.fogoItem = fogoItem;
                }

                protected override void OnTick()
                {
                    if (fogoItem != null)
                    {
                        fogoItem.Delete();
                    }
                }
            }
        }

        private class InternalTarget : Target {
			private AtearFogoSpell m_Owner;

			public InternalTarget( AtearFogoSpell owner ) : base(12, true, TargetFlags.Harmful ) {
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o ) {
				if ( o is Mobile ) {
					m_Owner.Target( (Mobile)o);
				}
                else if (o is IPoint3D)
                {
                    m_Owner.Target((IPoint3D)o);
                }
			}

			protected override void OnTargetFinish( Mobile from ) {
				m_Owner.FinishSequence();
			}
		}
	}
}
