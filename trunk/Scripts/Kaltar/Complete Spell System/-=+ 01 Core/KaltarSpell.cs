using System;
using Server;
using Server.Spells;
using Server.Network;

namespace Server.ACC.CSS
{
	public abstract class KaltarSpell : CSpell {
		
		
        public abstract SpellCircle Circle { get; }
        
		public KaltarSpell( Mobile caster, Item scroll, SpellInfo info ) : base( caster, scroll, info )	{}

        public override SkillName CastSkill { get { return SkillName.Magery; } }
        public override SkillName DamageSkill { get { return SkillName.Magery; } }
        public override bool ClearHandsOnCast { get { return false; } }
      	public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds(3 * CastDelaySecondsPerTick); } }
      	
        public override int GetMana() {
            return RequiredMana;
        }
        
        public override bool CheckCast() {
			if ( !base.CheckCast() )
				return false;

			if ( Caster.Skills[CastSkill].Value < RequiredSkill ) {
				Caster.SendMessage( "Você deve ter no mínimo " + RequiredSkill + " de " + CastSkill  + "para invocar a magia." );
				return false;
			}
			else if ( Caster.Mana < ScaleMana( GetMana() ) ) {
				Caster.SendMessage( "Você deve ter no mínimo " + GetMana() + " de Mana para invocar a magia." );
				return false;
			}

			return true;
		}

		public override bool CheckFizzle() {
			if ( !base.CheckFizzle() ) {
				return false;
			}
			
			return true;
		}

		public override void SayMantra() {
			Caster.PublicOverheadMessage( MessageType.Regular, 0x3B2, false, Info.Mantra );
			Caster.PlaySound( 0x24A );
		}

		public override void DoFizzle() {
			Caster.PlaySound( 0x1D6 );
			Caster.NextSpellTime = DateTime.Now;
		}

		public override void DoHurtFizzle() {
			Caster.PlaySound( 0x1D6 );
		}

		public override void OnDisturb( DisturbType type, bool message ) {
			base.OnDisturb( type, message );

			if ( message ) {
				Caster.PlaySound( 0x1D6 );
			}
		}

		public override void OnBeginCast() {
			base.OnBeginCast();

			Caster.FixedEffect( 0x37C4, 10, 42, 4, 3 );
		}

		public override void GetCastSkills( out double min, out double max ) {
			min = RequiredSkill;
			max = RequiredSkill + 40.0;
		}
	}
}
