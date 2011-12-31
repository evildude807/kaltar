using System;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;
using Server.Spells;

namespace Server.ACC.CSS.Systems.Natureza {

    public class MatilhaSpell : NaturezaSpell {
        private static SpellInfo m_Info = new SpellInfo(
                                                        "Matilha", "Matilha",
            //SpellCircle.Third,
                                                        266,
                                                        9040,
                                                        false,
                                                        Reagent.BlackPearl,
                                                        Reagent.Bloodmoss,
                                                        CReagent.PetrafiedWood
                                                       );

        public override SpellCircle Circle { get { return SpellCircle.Second; } }
        public override double CastDelay { get { return 5.0; } }
        public override double RequiredSkill { get { return 30.0; } }
        public override int RequiredMana { get { return 20; } }

        public MatilhaSpell(Mobile caster, Item scroll)
            : base(caster, scroll, m_Info)
        {
        }

        public override void OnCast()
        {
            if (CheckSequence())
            {
                TimeSpan duracao = TimeSpan.FromSeconds(4.0 * Caster.Skills[CastSkill].Value);

                int quantidade = getQuantidade();
                for(int i =0; i < quantidade; i++) {
                    Dog cachorro = new Dog();
                    SpellHelper.Summon(cachorro, Caster, 0x215, duracao, false, false);
                }
            }

            FinishSequence();
        }

        private int getQuantidade()
        {
            int quantiadade = 0;
            int valor = (int) Caster.Skills[SkillName.AnimalTaming].Value;

            if (valor < 20)
            {
                quantiadade = 1;
            } else if(valor < 50) {
                quantiadade = 2;
            }
            else if(valor < 70) {
                quantiadade = 3;
            }
            else {
                quantiadade = 4;
            }

            return quantiadade;
        }
    }
}
