 /*
    _________________________________
 -=(_)_______________________________)=-
   /   .   .   . ____  . ___      _/
  /~ /    /   / /     / /   )2006 /
 (~ (____(___/ (____ / /___/     (
  \ ----------------------------- \
   \     lucidnagual@gmail.com     \
    \_     ===================      \
     \   -Admin of "The Conjuring"-  \
      \_     ===================     ~\
       )       Advanced Archery        )
      /~    Version [3].0 & Above    _/
    _/_______________________________/
 -=(_)_______________________________)=-

 */

using System;


namespace Server.Items
{
	public class LightningBolt : Bolt, ICommodity
	{
		string ICommodity.Description
		{
			get
			{
				return String.Format( Amount == 1 ? "{0} lightning bolt" : "{0} lightning bolts", Amount );
			}
		}

		[Constructable]
		public LightningBolt() : this( 1 )
		{
		}

		[Constructable]
		public LightningBolt( int amount ) : base( 0x1BFB )
		{
			Name = "Lightning Bolt";
			Hue = 1174;
			Stackable = true;
			Weight = 0.1;
			Amount = amount;
		}

		public LightningBolt( Serial serial ) : base( serial )
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
	}
}
