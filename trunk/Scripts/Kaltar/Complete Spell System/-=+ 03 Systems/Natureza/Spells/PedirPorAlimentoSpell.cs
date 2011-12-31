using System;
using System.Collections;
using System.Collections.Generic;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;
using Server.Spells;
using Server.Items;

namespace Server.ACC.CSS.Systems.Natureza
{
	public class PedirPorAlimentoSpell : NaturezaSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
                "Pedir Por Alimento", "Pedir Por Alimento",
				224,
				9011,
				Reagent.Garlic,
				Reagent.Ginseng,
				Reagent.MandrakeRoot
			);

        public PedirPorAlimentoSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info) { }
		public override SpellCircle Circle { get { return SpellCircle.First; } }
        public override double RequiredSkill { get { return 10.0; } }
        public override double CastDelay { get { return 3.0; } }
        public override int RequiredMana { get { return 15; } }

        private static Type[] m_Food = new Type[]
			{
				typeof( Grapes ),
				typeof( Sausage ),
				 typeof( Apple ),
				 typeof( Peach ),
                 typeof( Pear ),
                 typeof( Watermelon )
			};

		public override void OnCast()
		{
			if ( CheckSequence() )
			{
                int quantidade = (int) (Caster.Skills[CastSkill].Value / 10.0) + 1;

                for(int i=0; i < quantidade; i++) {

                    Item food = (Item)Activator.CreateInstance(m_Food[Utility.Random(m_Food.Length)]);

				    if ( food != null )
				    {
					    Caster.AddToBackpack( food );

                        food.MoveToWorld(Caster.Location, Caster.Map);
				    }
                }

                Caster.PublicOverheadMessage(MessageType.Emote, 0, false, "Frutas brotam do chão.");
                
                Caster.FixedParticles(0, 10, 5, 2003, EffectLayer.RightHand);
                Caster.PlaySound(0x1E2);
			}

			FinishSequence();
		}
	}
}