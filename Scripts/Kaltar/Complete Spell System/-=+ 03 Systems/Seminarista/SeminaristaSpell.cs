using System;
using Server;
using Server.Spells;
using Server.Network;

namespace Server.ACC.CSS.Systems.Cleric{
	
	public abstract class SeminaristaSpell : KaltarSpell {
		
		public SeminaristaSpell( Mobile caster, Item scroll, SpellInfo info ) : base( caster, scroll, info ) {}
        public override SkillName CastSkill { get { return SkillName.SpiritSpeak; } }
        public override SkillName DamageSkill { get { return SkillName.SpiritSpeak; } }
	}
}
