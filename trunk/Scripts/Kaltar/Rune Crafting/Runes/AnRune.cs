using System;
using System.Collections;
using System.Collections.Generic;
using Server.Multis;
using Server.Mobiles;
using Server.Network;
using Server.ContextMenus;
using Server.Spells;
using Server.Targeting;
using Server.Misc;

namespace Server.Items
{
	public class AnRune : Item
	{
		[Constructable]
		public AnRune() : base( 0x1F14 )
		{
			Weight = 0.2;  // ?
			Name = "An Rune";
			Hue = 1000;
		}

		public override void OnDoubleClick( Mobile from ) 
		{
			double minSkill = 70.0;
		 
			PlayerMobile pm = from as PlayerMobile;
		
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}

			else if ( pm == null || from.Skills[SkillName.Inscribe].Base < 70.0 )
			{
				from.SendMessage( "You are not skilled enough to attempt this enhancement." );
			}

		        else if( from.InRange( this.GetWorldLocation(), 1 ) ) 
		        {
				double maxSkill = minSkill + 40.0;

				if ( !from.CheckSkill( SkillName.Inscribe, minSkill, maxSkill ) )
				{
					from.SendMessage( "The rune shatters, releasing the magic energy." );
					from.PlaySound( 65 );
					from.PlaySound( 0x1F8 );
					Delete();
					return;
				}
				else
				{
					from.SendMessage( "Select the item to enhance." );
					from.Target = new InternalTarget( this );
				}
		        } 

		        else 
		        { 
		        	from.SendLocalizedMessage( 500446 ); // That is too far away. 
		        } 
		} 
		
		private class InternalTarget : Target 
		{
			private AnRune m_AnRune;

			public InternalTarget( AnRune runeaug ) : base( 1, false, TargetFlags.None )
			{
				m_AnRune = runeaug;
			}

		 	protected override void OnTarget( Mobile from, object targeted ) 
		 	{ 
				int scalar;
				double mageSkill = from.Skills[SkillName.Inscribe].Value;

				if ( mageSkill >= 100.0 )
					scalar = 3;
				else if ( mageSkill >= 90.0 )
					scalar = 2;
				else
					scalar = 1;

			    	if ( targeted is BaseWeapon ) 
				{ 
			       		BaseWeapon Weapon = targeted as BaseWeapon; 

					if ( !from.InRange( ((Item)targeted).GetWorldLocation(), 1 ) ) 
					{ 
			          		from.SendLocalizedMessage( 500446 ); // That is too far away. 
		       			}

					else if (( ((Item)targeted).Parent != null ) && ( ((Item)targeted).Parent is Mobile ) ) 
			       		{ 
			          		from.SendMessage( "You cannot enhance that in it's current location." ); 
		       			}

					else
		       			{
						int DestroyChance = Utility.Random( 5 );

						if ( DestroyChance > 0 ) // Success
						{
							int augmentper = ( ( Utility.Random( 5 ) ) * scalar ) + 5;

							Weapon.WeaponAttributes.HitDispel += augmentper; from.SendMessage( "The An Rune enhances your weapon." );

				                  	from.PlaySound( 0x1F5 );
			        	          	m_AnRune.Delete();
			          		}

						else // Fail
						{
					  		from.SendMessage( "You have failed to enhance the weapon!" );
							from.SendMessage( "The weapon is damaged beyond repair!" );
							from.PlaySound( 42 );
						  	Weapon.Delete();
							m_AnRune.Delete();
				  		}
					}
				}
				else 
				{
		       			from.SendMessage( "You cannot enhance that." );
		    		} 
		  	}
		
		}

		public override bool DisplayLootType{ get{ return false; } }  // ha ha!

		public AnRune( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
		public override void AddNameProperty( ObjectPropertyList list )
		{
			base.AddNameProperty(list);
			list.Add( "Hit Dispel" );
		}
	}
}