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
	public class UndeadPoisonIvyPatchSpell : UndeadSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		                                                "Poison Ivy Patch", "Kal An Nox ",
		                                                //SpellCircle.Seventh,
		                                                233,
		                                                9012,
		                                                false,
		                                                Reagent.GraveDust,
		                                                Reagent.NoxCrystal,
		                                                Reagent.PigIron
		                                               );

        public override SpellCircle Circle { get { return SpellCircle.Seventh; } }
		public override double RequiredSkill{ get{ return 68.0; } }
		public override int RequiredMana{ get{ return 28; } }
		public override int RequiredHealth{ get{ return 0; } }
		
		public UndeadPoisonIvyPatchSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
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
				if(this.Scroll!=null)
					Scroll.Consume();
				SpellHelper.Turn( Caster, p );
				SpellHelper.GetSurfaceTop( ref p );
				Effects.PlaySound( p, Caster.Map, 0x474 );
				
				Point3D loc = new Point3D( p.X, p.Y, p.Z );
				int mushx;
				int mushy;
				int mushz;
				InternalItem firstFlamea = new InternalItem( Caster.Location, Caster.Map, Caster );
				mushx=loc.X-2;
				mushy=loc.Y-2;
				mushz=loc.Z;
				Point3D mushxyz = new Point3D(mushx,mushy,mushz);
				firstFlamea.MoveToWorld( mushxyz, Caster.Map );
				firstFlamea.ItemID = 3267;
				InternalItem firstFlamec = new InternalItem( Caster.Location, Caster.Map, Caster );
				mushx=loc.X;
				mushy=loc.Y-3;
				mushz=loc.Z;
				Point3D mushxyzb = new Point3D(mushx,mushy,mushz);
				firstFlamec.MoveToWorld( mushxyzb, Caster.Map );
				firstFlamec.ItemID = 3267;
				InternalItem firstFlamed = new InternalItem( Caster.Location, Caster.Map, Caster );
				firstFlamed.ItemID = 3267;
				mushx=loc.X+2;
				mushy=loc.Y-2;
				mushz=loc.Z;
				Point3D mushxyzc = new Point3D(mushx,mushy,mushz);
				firstFlamed.MoveToWorld( mushxyzc, Caster.Map );
				InternalItem firstFlamee = new InternalItem( Caster.Location, Caster.Map, Caster );
				mushx=loc.X+3;
				firstFlamee.ItemID = 3267;
				mushy=loc.Y;
				mushz=loc.Z;
				Point3D mushxyzd = new Point3D(mushx,mushy,mushz);
				firstFlamee.MoveToWorld( mushxyzd, Caster.Map );
				InternalItem firstFlamef = new InternalItem( Caster.Location, Caster.Map, Caster );
				firstFlamef.ItemID = 3267;
				mushx=loc.X+2;
				mushy=loc.Y+2;
				mushz=loc.Z;
				Point3D mushxyze = new Point3D(mushx,mushy,mushz);
				firstFlamef.MoveToWorld( mushxyze, Caster.Map );
				InternalItem firstFlameg = new InternalItem( Caster.Location, Caster.Map, Caster );
				mushx=loc.X;
				firstFlameg.ItemID = 3267;
				mushy=loc.Y+3;
				mushz=loc.Z;
				Point3D mushxyzf = new Point3D(mushx,mushy,mushz);
				firstFlameg.MoveToWorld( mushxyzf, Caster.Map );
				InternalItem firstFlameh = new InternalItem( Caster.Location, Caster.Map, Caster );
				mushx=loc.X-2;
				firstFlameh.ItemID = 3267;
				mushy=loc.Y+2;
				mushz=loc.Z;
				Point3D mushxyzg = new Point3D(mushx,mushy,mushz);
				firstFlameh.MoveToWorld( mushxyzg, Caster.Map );
				InternalItem firstFlamei = new InternalItem( Caster.Location, Caster.Map, Caster );
				mushx=loc.X-3;
				firstFlamei.ItemID = 3267;
				mushy=loc.Y;
				mushz=loc.Z;
				Point3D mushxyzh = new Point3D(mushx,mushy,mushz);
				firstFlamei.MoveToWorld( mushxyzh, Caster.Map );				
			}
			FinishSequence();
		}
		
		[DispellableField]
		private class InternalItem : Item
		{
			private Timer m_Timer;
			private Timer m_Burn;
			private DateTime m_End;
			private Mobile m_Caster;
			
			public override bool BlocksFit{ get{ return true; } }
			
			public InternalItem( Point3D loc, Map map, Mobile caster ) : base( 0x3709 )
			{
				Visible = false;
				Movable = false;
				Light=LightType.Circle150;
				MoveToWorld( loc, map );
				m_Caster=caster;
				
				if ( caster.InLOS( this ) )
					Visible = true;
				else
					Delete();
				if ( Deleted )
					return;
				
				m_Timer = new InternalTimer( this, TimeSpan.FromSeconds( 30.0 ) );
				m_Timer.Start();
				m_Burn = new BurnTimer( this, m_Caster );
				m_Burn.Start();
				m_End = DateTime.Now + TimeSpan.FromSeconds( 30.0 );
			}
			
			public InternalItem( Serial serial ) : base( serial )
			{
			}
			
			public override bool OnMoveOver( Mobile m )
			{
				if ( Visible && m_Caster != null && SpellHelper.ValidIndirectTarget( m_Caster, m ) && m_Caster.CanBeHarmful( m, false ) )
				{
					m_Caster.DoHarmful( m );
					m.Poison = Poison.Deadly;
					m.FixedParticles( 0x374A, 10, 15, 5021, EffectLayer.Waist );
					m.PlaySound( 0x474 );
					
					int damage = Utility.Random( 18, 24 );
					
					if ( !Core.AOS && m.CheckSkill( SkillName.MagicResist, 0.0, 30.0 ) )
					{
						damage = Utility.Random( 9, 12 );
						m.SendLocalizedMessage( 501783 ); // You feel yourself resisting magical energy.
					}
					AOS.Damage( m, m_Caster, damage, 0, 100, 0, 0, 0 );
					m.PlaySound( 0x474 );
				}
				return true;
			}
			
			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );
				writer.Write( (int) 1 ); // version
				writer.Write( m_End - DateTime.Now );
			}
			
			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );
				int version = reader.ReadInt();
				
				switch ( version )
				{
					case 1:
						{
							TimeSpan duration = reader.ReadTimeSpan();
							m_Timer = new InternalTimer( this, duration );
							m_Timer.Start();
							m_End = DateTime.Now + duration;
							break;
						}
					case 0:
						{
							TimeSpan duration = TimeSpan.FromSeconds( 10.0 );
							m_Timer = new InternalTimer( this, duration );
							m_Timer.Start();
							m_End = DateTime.Now + duration;
							break;
						}
				}
			}
			
			public override void OnAfterDelete()
			{
				base.OnAfterDelete();
				if ( m_Timer != null )
					m_Timer.Stop();
			}
			
			private class InternalTimer : Timer
			{
				private Timer m_Burn;
				private InternalItem m_Item;
				
				public InternalTimer( InternalItem item, TimeSpan duration ) : base( duration )
				{
					m_Item = item;
				}
				
				protected override void OnTick()
				{
					m_Item.Delete();
				}
			}
		}
		
		private class InternalTarget : Target
		{
			private UndeadPoisonIvyPatchSpell m_Owner;
			
			public InternalTarget( UndeadPoisonIvyPatchSpell owner ) : base( 12, true, TargetFlags.None )
			{
				m_Owner = owner;
			}
			
			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is IPoint3D )
					m_Owner.Target( (IPoint3D)o );
			}
			
			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
		private class BurnTimer : Timer
		{
			private Item m_FireRing;
			private Mobile m_Caster;
			private DateTime m_Duration;
			private static Queue m_Queue = new Queue();
			
			public BurnTimer( Item ap, Mobile ca ) : base( TimeSpan.FromSeconds( 0.25 ), TimeSpan.FromSeconds( 0.5 ) )
			{
				Priority = TimerPriority.FiftyMS;
				m_FireRing = ap;
				m_Caster=ca;
				m_Duration = DateTime.Now + TimeSpan.FromSeconds( 15.0 + ( Utility.RandomDouble() * 15.0 ) );
			}
			
			protected override void OnTick()
			{
				if ( m_FireRing.Deleted )
					return;
				
				if ( DateTime.Now > m_Duration )
				{
					Stop();
				}
				else
				{
					Map map = m_FireRing.Map;
					if ( map != null )
					{
						foreach ( Mobile m in m_FireRing.GetMobilesInRange( 1 ) )
						{
							if ( (m.Z + 16) > m_FireRing.Z && (m_FireRing.Z + 12) > m.Z )
								m_Queue.Enqueue( m );
						}
						while ( m_Queue.Count > 0 )
						{
							Mobile m = (Mobile) m_Queue.Dequeue();
							if ( m_FireRing.Visible && m_Caster != null && SpellHelper.ValidIndirectTarget( m_Caster, m ) && m_Caster.CanBeHarmful( m, false ) )
							{
								m_Caster.DoHarmful( m );
								int damage = Utility.Random( 5, 8 );
								if ( !Core.AOS && m.CheckSkill( SkillName.MagicResist, 0.0, 30.0 ) )
								{
									damage = Utility.Random( 1, 3 );
									m.SendLocalizedMessage( 501783 ); // You feel yourself resisting magical energy.
								}
								AOS.Damage( m, m_Caster, damage, 0, 100, 0, 0, 0 );
								m.PlaySound( 0x474 );
								m.SendMessage( "You feel the effects of the poisonous plants!!!" );
							}
							
						}
					}
				}
			}
		}
	}
}

