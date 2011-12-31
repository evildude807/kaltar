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
	public class UndeadGraveyardGatewaySpell : UndeadSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
		                                                "Graveyard Gateway", "Un Grav Ohm Sepa",
		                                                //SpellCircle.Seventh,
		                                                263,
		                                                9032,
		                                                Reagent.GraveDust,
		                                                Reagent.DaemonBlood,
		                                                Reagent.BatWing
		                                               );

        public override SpellCircle Circle { get { return SpellCircle.Seventh; } }
		public override double RequiredSkill{ get{ return 70; } }
		public override int RequiredMana{ get{ return 45; } }
		public override int RequiredHealth{ get{ return 0; } }
		
		private RunebookEntry m_Entry;
		
		public UndeadGraveyardGatewaySpell( Mobile caster, Item scroll ) : this( caster, scroll, null )
		{
		}
		
		public UndeadGraveyardGatewaySpell( Mobile caster, Item scroll, RunebookEntry entry ) : base( caster, scroll, m_Info )
		{
			m_Entry = entry;
		}
		
		public override void OnCast()
		{

			if ( m_Entry == null )
				Caster.Target = new InternalTarget( this );
			else
				Effect( m_Entry.Location, m_Entry.Map, true );
			Caster.BoltEffect( 0 );
		}
		
		public override bool CheckCast()
		{
			if ( Caster.Criminal )
			{
				Caster.SendLocalizedMessage( 1005561, "", 0x22 ); // Thou'rt a criminal and cannot escape so easily.
				return false;
			}
			else if ( SpellHelper.CheckCombat( Caster ) )
			{
				Caster.SendLocalizedMessage( 1005564, "", 0x22 ); // Wouldst thou flee during the heat of battle??
				return false;
			}
			
			return SpellHelper.CheckTravel( Caster, TravelCheckType.GateFrom );
		}
		
		public void Effect( Point3D loc, Map map, bool checkMulti )
		{
			if ( map == null || (!Core.AOS && Caster.Map != map) )
			{
				Caster.SendLocalizedMessage( 1005570 ); // You can not gate to another facet.
			}
			else if ( !SpellHelper.CheckTravel( Caster, TravelCheckType.GateFrom ) )
			{
			}
			else if ( !SpellHelper.CheckTravel( Caster,  map, loc, TravelCheckType.GateTo ) )
			{
			}
			else if ( Caster.Kills >= 5 && map != Map.Felucca )
			{
				Caster.SendLocalizedMessage( 1019004 ); // You are not allowed to travel there.
			}
			else if ( Caster.Criminal )
			{
				Caster.SendLocalizedMessage( 1005561, "", 0x22 ); // Thou'rt a criminal and cannot escape so easily.
			}
			else if ( SpellHelper.CheckCombat( Caster ) )
			{
				Caster.SendLocalizedMessage( 1005564, "", 0x22 ); // Wouldst thou flee during the heat of battle??
			}
			else if ( !map.CanSpawnMobile( loc.X, loc.Y, loc.Z ) )
			{
				Caster.SendLocalizedMessage( 501942 ); // That location is blocked.
			}
			else if ( (checkMulti && SpellHelper.CheckMulti( loc, map )) )
			{
				Caster.SendLocalizedMessage( 501942 ); // That location is blocked.
			}
			else if ( CheckSequence() )
			{
				Caster.SendMessage( "You open a spooky scarey portal in a graveyard circle" ); // You open a magical gate to another location
				
				Effects.PlaySound( Caster.Location, Caster.Map, 0x482 );
				int gravx;
				int gravy;
				int gravz;
				
				
				InternalItem firstGatea = new InternalItem( loc, map );
				gravx=Caster.X;
				gravy=Caster.Y;
				gravz=Caster.Z;
				firstGatea.ItemID=0x1A0C;
				Point3D gravxyz = new Point3D(gravx,gravy,gravz);
				firstGatea.MoveToWorld( gravxyz, Caster.Map );
				InternalItem firstGateb = new InternalItem( loc, map );
				gravx=Caster.X;
				gravy=Caster.Y;
				firstGateb.ItemID=0x373A; //Moongate
				gravz=Caster.Z+1;
				Point3D gravxyza = new Point3D(gravx,gravy,gravz);
				firstGateb.MoveToWorld( gravxyza, Caster.Map );
				InternalItem firstGatec = new InternalItem( loc, map );
				gravx=Caster.X-1;
				firstGatec.ItemID=0xEDD;
				gravy=Caster.Y+1;
				gravz=Caster.Z;
				Point3D gravxyzb = new Point3D(gravx,gravy,gravz);
				firstGatec.MoveToWorld( gravxyzb, Caster.Map );
				InternalItem firstGated = new InternalItem( loc, map);
				firstGated.ItemID=0x117B;
				gravx=Caster.X;
				gravy=Caster.Y+2;
				gravz=Caster.Z;
				Point3D gravxyzc = new Point3D(gravx,gravy,gravz);
				firstGated.MoveToWorld( gravxyzc, Caster.Map );
				InternalItem firstGatee = new InternalItem( loc, map );
				gravx=Caster.X+1;
				firstGatee.ItemID=0x2203;
				gravy=Caster.Y+1;
				gravz=Caster.Z;
				Point3D gravxyzd = new Point3D(gravx,gravy,gravz);
				firstGatee.MoveToWorld( gravxyzd, Caster.Map );
				InternalItem firstGatef = new InternalItem( loc, map );
				firstGatef.ItemID=0x1178;
				gravx=Caster.X+2;
				gravy=Caster.Y;
				gravz=Caster.Z;
				Point3D gravxyze = new Point3D(gravx,gravy,gravz);
				firstGatef.MoveToWorld( gravxyze, Caster.Map );
				InternalItem firstGateg = new InternalItem( loc, map );
				gravx=Caster.X+1;
				firstGateg.ItemID=0x1184;
				gravy=Caster.Y-1;
				gravz=Caster.Z;
				Point3D gravxyzf = new Point3D(gravx,gravy,gravz);
				firstGateg.MoveToWorld( gravxyzf, Caster.Map );
				
				Effects.PlaySound( loc, map, 0x482 );
				
				InternalItem secondGatea = new InternalItem( Caster.Location, Caster.Map );
				gravx=loc.X;
				gravy=loc.Y;
				gravz=loc.Z;
				secondGatea.ItemID=0x1A0C;
				Point3D gravaxyz = new Point3D(gravx,gravy,gravz);
				secondGatea.MoveToWorld( gravaxyz, map);
				InternalItem secondGateb = new InternalItem( Caster.Location, Caster.Map );
				gravx=loc.X;
				gravy=loc.Y;
				secondGateb.ItemID=0x373A; //Moongate
				gravz=loc.Z+1;
				Point3D gravaxyza = new Point3D(gravx,gravy,gravz);
				secondGateb.MoveToWorld( gravaxyza, map);
				InternalItem secondGatec = new InternalItem( Caster.Location, Caster.Map );
				gravx=loc.X-1;
				secondGatec.ItemID=0xEDD;
				gravy=loc.Y+1;
				gravz=loc.Z-1;
				Point3D gravaxyzb = new Point3D(gravx,gravy,gravz);
				secondGatec.MoveToWorld( gravaxyzb, map);
				InternalItem secondGated = new InternalItem( Caster.Location, Caster.Map);
				gravx=loc.X;
				gravy=loc.Y+2;
				secondGated.ItemID=0x117B;
				gravz=loc.Z;
				Point3D gravaxyzc = new Point3D(gravx,gravy,gravz);
				secondGated.MoveToWorld( gravaxyzc, map);
				InternalItem secondGatee = new InternalItem( Caster.Location, Caster.Map );
				gravx=loc.X+1;
				gravy=loc.Y+1;
				gravz=loc.Z;
				secondGatee.ItemID=0x2203;
				Point3D gravaxyzd = new Point3D(gravx,gravy,gravz);
				secondGatee.MoveToWorld( gravaxyzd, map);
				InternalItem secondGatef = new InternalItem( Caster.Location, Caster.Map );
				gravx=loc.X+2;
				gravy=loc.Y;
				gravz=loc.Z;
				secondGatef.ItemID=0x1178;
				Point3D gravaxyze = new Point3D(gravx,gravy,gravz);
				secondGatef.MoveToWorld( gravaxyze, map);
				InternalItem secondGateg = new InternalItem( Caster.Location, Caster.Map );
				gravx=loc.X+1;
				secondGateg.ItemID=0x1184;
				gravy=loc.Y-1;
				gravz=loc.Z;
				Point3D gravaxyzf = new Point3D(gravx,gravy,gravz);
				secondGateg.MoveToWorld( gravaxyzf, map);
			}
			
			FinishSequence();
		}
		
		[DispellableField]
		private class InternalItem : Moongate
		{
			public override bool ShowFeluccaWarning{ get{ return Core.AOS; } }
			
			public InternalItem( Point3D target, Map map ) : base( target, map )
			{
				Map = map;
				
				if ( ShowFeluccaWarning && map == Map.Felucca )
					ItemID = 0xDDA;
				
				Dispellable = true;
				
				InternalTimer t = new InternalTimer( this );
				t.Start();
			}
			
			public InternalItem( Serial serial ) : base( serial )
			{
			}
			
			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );
			}
			
			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );
				
				Delete();
			}
			
			private class InternalTimer : Timer
			{
				private Item m_Item;
				
				public InternalTimer( Item item ) : base( TimeSpan.FromSeconds( 30.0 ) )
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
			private UndeadGraveyardGatewaySpell m_Owner;
			
			public InternalTarget( UndeadGraveyardGatewaySpell owner ) : base( 12, false, TargetFlags.None )
			{
				m_Owner = owner;
				
				owner.Caster.LocalOverheadMessage( MessageType.Regular, 0x3B2, 501029 ); // Select Marked item.
			}
			
			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is RecallRune )
				{
					RecallRune rune = (RecallRune)o;
					
					if ( rune.Marked )
						m_Owner.Effect( rune.Target, rune.TargetMap, true );
					else
						from.SendLocalizedMessage( 501803 ); // That rune is not yet marked.
				}
				else if ( o is Runebook )
				{
					RunebookEntry e = ((Runebook)o).Default;
					
					if ( e != null )
						m_Owner.Effect( e.Location, e.Map, true );
					else
						from.SendLocalizedMessage( 502354 ); // Target is not marked.
				}
				else
				{
					from.Send( new MessageLocalized( from.Serial, from.Body, MessageType.Regular, 0x3B2, 3, 501030, from.Name, "" ) ); // I can not gate travel from that object.
				}
			}
			
			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}

