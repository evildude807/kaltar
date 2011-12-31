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
	public class MinarAguaSpell : NaturezaSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
                "Minar água", "Minar água",
				224,
				9011,
				Reagent.Garlic,
				Reagent.Ginseng,
				Reagent.MandrakeRoot
			);

        public MinarAguaSpell(Mobile caster, Item scroll) : base(caster, scroll, m_Info) { }
		public override SpellCircle Circle { get { return SpellCircle.First; } }
        public override double RequiredSkill { get { return 10.0; } }
        public override double CastDelay { get { return 3.0; } }
        public override int RequiredMana { get { return 15; } }
        
		public override void OnCast()
		{
			if ( CheckSequence() )
			{
        
			}

			FinishSequence();
		}
	}
}