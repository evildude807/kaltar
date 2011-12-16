using System;
using Server.Targeting;
using Server.Network;
using Server.Spells;

namespace Server.ACC.CSS.Systems.Aprendiz {
	
	public class ArmaduraArcanaSpell : AprendizSpell {
		
		private static SpellInfo m_Info = new SpellInfo(
				"Armadura Arcana", 
				"Armadura Arcana",
				236,
				9041,
				Reagent.SulfurousAsh
		);

		public override SpellCircle Circle { get { return SpellCircle.First; } }
		public override double RequiredSkill{ get{ return 20.0; } }
		public override double CastDelay{ get{ return 5.0; } }
		public override int RequiredMana   { get{ return 20; } }
		
		public ArmaduraArcanaSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info ) {
		}
		
		public override void OnCast() {

            if (CheckSequence())
            {
                Caster.PlaySound(0x1E9);
                Caster.FixedParticles(0x375A, 9, 20, 5016, EffectLayer.Waist);

                //valor da proteção
                int protecao = (int) (Caster.Skills[CastSkill].Value / 5.0) + Utility.Random(2);

                ResistanceMod[] resistencias = new ResistanceMod[5];

                resistencias[0] = new ResistanceMod(ResistanceType.Physical, protecao);
                resistencias[1] = new ResistanceMod(ResistanceType.Fire, protecao);
                resistencias[2] = new ResistanceMod(ResistanceType.Cold, protecao);
                resistencias[3] = new ResistanceMod(ResistanceType.Energy, protecao);
                resistencias[4] = new ResistanceMod(ResistanceType.Poison, protecao);

                Caster.AddResistanceMod(resistencias[0]);
                Caster.AddResistanceMod(resistencias[1]);
                Caster.AddResistanceMod(resistencias[2]);
                Caster.AddResistanceMod(resistencias[3]);
                Caster.AddResistanceMod(resistencias[4]);

                string args = String.Format("{0}", protecao);

                //adiciona o buff de armadura arcana
                BuffInfo armaduraArcana = new BuffInfo(BuffIcon.Protection, 1075814, 1075815, args.ToString());
                BuffInfo.AddBuff(Caster, armaduraArcana);

                //calculo da duracao
                int intDuracao = (int) (Caster.Skills[CastSkill].Value / 10) + Utility.Random(2);
                TimeSpan duracao = TimeSpan.FromMinutes(intDuracao);

                //Tempo para remover
                Timer remover = new InternalTimer(Caster, resistencias, armaduraArcana, duracao);
                remover.Start();
            }
            FinishSequence();
		}

        private class InternalTimer : Timer {

            private Mobile caster;
            private ResistanceMod[] resistencias;
            private BuffInfo armaduraArcana;

            public InternalTimer(Mobile caster, ResistanceMod[] resistencias, BuffInfo armaduraArcana, TimeSpan duracao)
                : base(duracao)
            {
                this.caster = caster;
                this.resistencias = resistencias;
                this.armaduraArcana = armaduraArcana;
            }

            protected override void OnTick()
            {
                caster.RemoveResistanceMod(resistencias[0]);
                caster.RemoveResistanceMod(resistencias[1]);
                caster.RemoveResistanceMod(resistencias[2]);
                caster.RemoveResistanceMod(resistencias[3]);
                caster.RemoveResistanceMod(resistencias[4]);

                BuffInfo.RemoveBuff(caster, armaduraArcana);
            }
        }
	}
}
