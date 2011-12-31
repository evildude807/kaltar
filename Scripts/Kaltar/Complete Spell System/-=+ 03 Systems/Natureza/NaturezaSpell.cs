using System;
using Server;
using Server.Spells;
using Server.Network;

namespace Server.ACC.CSS.Systems.Natureza {
	
	public abstract class NaturezaSpell : KaltarSpell {
		
		public NaturezaSpell( Mobile caster, Item scroll, SpellInfo info ) : base( caster, scroll, info ) {}
        public override SkillName CastSkill { get { return SkillName.Spellweaving; } }
        public override SkillName DamageSkill { get { return SkillName.Spellweaving; } }

        public override void SayMantra()
        {
            Caster.PublicOverheadMessage(MessageType.Regular, 0x41, false, Info.Mantra);
            Caster.PlaySound(0x24A);
        }             
	}
}
