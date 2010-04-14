using System;
using Server;
using Server.Spells;
using Server.Network;

namespace Server.ACC.CSS.Systems.Aprendiz{
	
	public abstract class AprendizSpell : KaltarSpell {
		
		public AprendizSpell( Mobile caster, Item scroll, SpellInfo info ) : base( caster, scroll, info ) {}
        public override SkillName CastSkill { get { return SkillName.Magery; } }
        public override SkillName DamageSkill { get { return SkillName.Magery; } }
        
		public override void SayMantra() {
			Caster.PublicOverheadMessage( MessageType.Regular, 0x3B2, false, Info.Mantra );
			Caster.PlaySound( 0x24A );
		}             
	}
}
