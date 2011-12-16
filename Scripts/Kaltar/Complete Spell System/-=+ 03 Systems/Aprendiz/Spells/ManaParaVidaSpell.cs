using System;
using Server.Targeting;
using Server.Network;
using Server.Spells;

namespace Server.ACC.CSS.Systems.Aprendiz {
	
	public class ManaParaVidaSpell : AprendizSpell {
		
		private static SpellInfo m_Info = new SpellInfo(
				"Mana para Vida",
                "Mana para Vida",
				236,
				9041,
				Reagent.Ginseng
		);

		public override SpellCircle Circle { get { return SpellCircle.First; } }
		public override double RequiredSkill{ get{ return 20.0; } }
		public override double CastDelay{ get{ return 3.0; } }
		public override int RequiredMana   { get{ return 10; } }
		
		public ManaParaVidaSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info ) {
		}
		
		public override void OnCast() {

            if (CheckSequence())
            {
                //Tempo para remover
                Timer efeito = new InternalTimer(Caster);
                efeito.Start();
            }
            FinishSequence();
		}

        private class InternalTimer : Timer {

            private Mobile caster;

            public InternalTimer(Mobile caster)
                : base(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2), 5)
            {
                this.caster = caster;
            }

            protected override void OnTick()
            {
                int perderMana = 3;
                int ganharVida = 2;

                if (!caster.Alive)
                {
                    this.Stop();
                    return;
                }

                if (caster.Mana - perderMana > 0)
                {
                    caster.Mana = caster.Mana - perderMana;
                    caster.Hits = caster.Hits + ganharVida;

                    caster.PlaySound(0x1E9);
                    caster.FixedParticles(0x375A, 9, 20, 5016, EffectLayer.Waist);
                }
            }
        }
	}
}