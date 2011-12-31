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
            return base.CheckFizzle();
            //return true;
		}

		public override void SayMantra() {
			Caster.PublicOverheadMessage( MessageType.Regular, 0x3B2, false, Info.Mantra );
			Caster.PlaySound( 0x24A );
		}

		public override void DoFizzle() {
            base.DoFizzle();
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

        //necessário por causa das magia de undead, E isso tem no magerySpell
        public virtual bool CheckResisted(Mobile target)
        {
            double n = GetResistPercent(target);

            n /= 100.0;

            if (n <= 0.0)
                return false;

            if (n >= 1.0)
                return true;

            int maxSkill = (1 + (int)Circle) * 10;
            maxSkill += (1 + ((int)Circle / 6)) * 25;

            if (target.Skills[SkillName.MagicResist].Value < maxSkill)
                target.CheckSkill(SkillName.MagicResist, 0.0, target.Skills[SkillName.MagicResist].Cap);

            return (n >= Utility.RandomDouble());
        }

        public virtual double GetResistPercentForCircle(Mobile target, SpellCircle circle)
        {
            double firstPercent = target.Skills[SkillName.MagicResist].Value / 5.0;
            double secondPercent = target.Skills[SkillName.MagicResist].Value - (((Caster.Skills[CastSkill].Value - 20.0) / 5.0) + (1 + (int)circle) * 5.0);

            return (firstPercent > secondPercent ? firstPercent : secondPercent) / 2.0; // Seems should be about half of what stratics says.
        }

        public virtual double GetResistPercent(Mobile target)
        {
            return GetResistPercentForCircle(target, Circle);
        }
	}
}
