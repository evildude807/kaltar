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
	public class NecroMassCurseSpell : UndeadSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		                                                "Mass Curse", "Vas Des Sanct",
		                                                //SpellCircle.Sixth,
		                                                218,
		                                                9031,
		                                                false,
		                                                Reagent.BatWing,
		                                                Reagent.GraveDust,
		                                                Reagent.DaemonBlood,
		                                                Reagent.NoxCrystal
		                                               );

        public override SpellCircle Circle { get { return SpellCircle.Sixth; } }
		public override double RequiredSkill{ get{ return 65; } }
		public override int RequiredMana{ get{ return 25; } }
		public override int RequiredHealth{ get{ return 0; } }
		
		public NecroMassCurseSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}
		
		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}
		
		public void Target( IPoint3D p )
		{
			if ( !Caster.CanSee( p ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( SpellHelper.CheckTown( p, Caster ) && CheckSequence() )
			{
				SpellHelper.Turn( Caster, p );
				
				SpellHelper.GetSurfaceTop( ref p );
				
				ArrayList targets = new ArrayList();
				
				Map map = Caster.Map;
				
				if ( map != null )
				{
					IPooledEnumerable eable = map.GetMobilesInRange( new Point3D( p ), 3 );
					
					foreach ( Mobile m in eable )
					{
						if ( Core.AOS && m == Caster )
							continue;
						
						if ( SpellHelper.ValidIndirectTarget( Caster, m ) && Caster.CanSee( m ) && Caster.CanBeHarmful( m, false ) )
							targets.Add( m );
					}
					
					eable.Free();
				}
				
				for ( int i = 0; i < targets.Count; ++i )
				{
					Mobile m = (Mobile)targets[i];
					
					Caster.DoHarmful( m );
					
					SpellHelper.AddStatCurse( Caster, m, StatType.Str ); SpellHelper.DisableSkillCheck = true;
					SpellHelper.AddStatCurse( Caster, m, StatType.Dex );
					SpellHelper.AddStatCurse( Caster, m, StatType.Int ); SpellHelper.DisableSkillCheck = false;
					
					m.FixedParticles( 0x374A, 10, 15, 5028, EffectLayer.Waist );
					m.PlaySound( 0x1FB );
				}
			}
			
			FinishSequence();
		}
		
		private class InternalTarget : Target
		{
			private NecroMassCurseSpell m_Owner;
			
			public InternalTarget( NecroMassCurseSpell owner ) : base( 12, true, TargetFlags.None )
			{
				m_Owner = owner;
			}
			
			protected override void OnTarget( Mobile from, object o )
			{
				IPoint3D p = o as IPoint3D;
				
				if ( p != null )
					m_Owner.Target( p );
			}
			
			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
