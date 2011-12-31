using System;
using Server.Network;
using Server.Multis;
using Server.Items;
using Server.Targeting;
using Server.Misc;
using Server.Regions;
using System.Collections;
using Server.Mobiles;
using Server.Spells;

namespace Server.ACC.CSS.Systems.Cleric
{
	public class UndeadMisfitsOfMondainSpell : UndeadSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		                                                "Misfits Of Mondain", "En Sec Grav Mon",
		                                               // SpellCircle.Third,
		                                                266,
		                                                9040,
		                                                false,
		                                                Reagent.NoxCrystal,
		                                                Reagent.GraveDust,
		                                                Reagent.BatWing
		                                               );

        public override SpellCircle Circle { get { return SpellCircle.Third; } }
		public override double RequiredSkill{ get{ return 37.0; } }
		public override int RequiredMana{ get{ return 30; } }
		public override int RequiredHealth{ get{ return 0; } }
		
		public UndeadMisfitsOfMondainSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}
		
		private static Type[] m_Types = new Type[]
		{
			typeof( Ghoul ),
			typeof( Zombie ),
			typeof( Lich ),
			typeof( Wraith ),
			typeof( Mummy )
		};
		
		public override void OnCast()
		{
			if ( CheckSequence() )
			{
				try
				{					
					Type undeadtype = ( m_Types[Utility.Random( m_Types.Length )] );
					
					BaseCreature creaturea = (BaseCreature)Activator.CreateInstance( undeadtype );
					BaseCreature creatureb = (BaseCreature)Activator.CreateInstance( undeadtype );
					BaseCreature creaturec = (BaseCreature)Activator.CreateInstance( undeadtype );
					BaseCreature creatured = (BaseCreature)Activator.CreateInstance( undeadtype );
					
					SpellHelper.Summon( creaturea, Caster, 0x215, TimeSpan.FromSeconds( 4.0 * Caster.Skills[CastSkill].Value ), false, false );
					SpellHelper.Summon( creatureb, Caster, 0x215, TimeSpan.FromSeconds( 4.0 * Caster.Skills[CastSkill].Value ), false, false );
					
					Double moreundead = 0 ;
					
					moreundead = Utility.Random( 10 ) + ( Caster.Skills[CastSkill].Value * 0.1 );
					
					
					if ( moreundead > 11 )
					{
						SpellHelper.Summon( creaturec, Caster, 0x215, TimeSpan.FromSeconds( 4.0 * Caster.Skills[CastSkill].Value ), false, false );
					}
					
					if ( moreundead > 18 )
					{
						SpellHelper.Summon( creatured, Caster, 0x215, TimeSpan.FromSeconds( 4.0 * Caster.Skills[CastSkill].Value ), false, false );
					}					
				}
				catch
				{
				}
			}
			
			FinishSequence();
		}
		
		public override TimeSpan GetCastDelay()
		{
			return TimeSpan.FromSeconds( 7.5 );
		}
	}
}
