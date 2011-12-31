using System;
using Server.Targeting;
using Server.Network;
using Server.Spells;
using System.Collections.Generic;

using Kaltar.Util;

namespace Server.ACC.CSS.Systems.Aprendiz {
	
	public class CampoDeEnergiaSpell : AprendizSpell {
		
		private static SpellInfo m_Info = new SpellInfo(
				"Campo de energia", 
				"Campo de energia",
				212,
				9041,
				Reagent.SulfurousAsh
		);

		public override SpellCircle Circle { get { return SpellCircle.Second; } }
		public override double RequiredSkill{ get{ return 20.0; } }
		public override double CastDelay{ get{ return 3.0; } }
		public override int RequiredMana   { get{ return 30; } }
		
		public CampoDeEnergiaSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info ) {
		}
		
		public override void OnCast() {

            if (CheckSequence())
            {
                TimeSpan duracao = TimeSpan.FromSeconds(Caster.Skills[CastSkill].Value / 2);

                Timer t = new InternalTimer(this, Caster, duracao);
                t.Start();
            }

            FinishSequence();
        }

        private class InternalTimer : Timer
		{
			private Mobile Caster;
            private CampoDeEnergiaSpell magia;
            private DateTime final;
            private DateTime efeitoSonoro;

			public InternalTimer(CampoDeEnergiaSpell magia, Mobile owner, TimeSpan duracao) : base(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1))
			{
                this.magia = magia;
				this.Caster = owner;
                this.final = DateTime.Now + duracao;
				this.Priority = TimerPriority.OneSecond;

                this.efeitoSonoro = DateTime.Now + TimeSpan.FromSeconds(4);

                //playsound
                Caster.PlaySound(0x1DD);
			}

			protected override void OnTick()
			{
                if (DateTime.Now > final)
                {
                    Stop();
                    return;
                }

                //efeito
                Caster.FixedEffect(0x3779, 5, 40);
                if (DateTime.Now > efeitoSonoro)
                {
                    Caster.PlaySound(0x1DD);
                }

                //lista dos mobiles que vao tomar dano
                List<Mobile> targets = new List<Mobile>();
                
                //pega os mobiles afetados
                IPooledEnumerable mobiles = Caster.GetMobilesInRange(2);
                foreach (Mobile m in mobiles)
                {
                    if (m == Caster)
                        continue;

                    if (SpellHelper.ValidIndirectTarget(Caster, m)
                        && Caster.CanBeHarmful(m, false)
                        && Caster.CanSee(m))
                    {
                        targets.Add(m);
                    }
                }
                mobiles.Free();

                if (targets.Count > 0)
                {
                    //para cada mobile, enviar um raio
                    foreach (Mobile mobile in targets)
                    {
                        Caster.DoHarmful(mobile);

                        double damage = magia.GetNewAosDamage(1, 1, 2, null);

                        SpellHelper.Damage(magia, mobile, damage, 0, 0, 0, 0, 100);

                        Caster.MovingParticles(mobile, 0x379F, 5, 0, false, false, 0, 0, 0);
                    }

                    Caster.PlaySound(0x20A);
                }
			}
		}
	}

}
