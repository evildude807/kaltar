using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class BagOfRunes : Backpack
	{
		public override string DefaultName
		{
			get { return "a bag of runes"; }
		}

		[Constructable]
		public BagOfRunes() : this( 1 )
		{
			Movable = true;
			Hue = 1001;
		}

		[Constructable]
		public BagOfRunes( int amount )
		//[Constructable]
		//public BagOfRunes() : base()
		{
			LootType = LootType.Blessed;








			PlaceItemIn( this, 50, 65, new AgloRune()	);
			PlaceItemIn( this, 60, 65, new AnRune()	);
			PlaceItemIn( this, 70, 65, new ArmaRune()	);
			PlaceItemIn( this, 80, 65, new BalRune()	);
			PlaceItemIn( this, 90, 65, new BetRune()	);
			PlaceItemIn( this, 100, 65, new CharRune()	);
			PlaceItemIn( this, 110, 65, new CorpRune()	);
			PlaceItemIn( this, 120, 65, new DelRune()	);
			PlaceItemIn( this, 130, 65, new DesRune()	);
			PlaceItemIn( this, 140, 65, new DiumRune()	);

			PlaceItemIn( this, 50, 80, new ExRune()	);
			PlaceItemIn( this, 60, 80, new FlamRune()	);
			PlaceItemIn( this, 70, 80, new FlamusRune()	);
			PlaceItemIn( this, 80, 80, new FrioRune()	);
			PlaceItemIn( this, 90, 80, new FurisRune()	);
			PlaceItemIn( this, 100, 80, new GravRune()	);
			PlaceItemIn( this, 110, 80, new HurRune()	);
			PlaceItemIn( this, 120, 80, new InRune()	);
			PlaceItemIn( this, 130, 80, new JuxRune()	);
			PlaceItemIn( this, 140, 80, new KalRune()	);

			PlaceItemIn( this, 50, 95, new LorRune()	);
			PlaceItemIn( this, 60, 95, new MalasRune()	);
			PlaceItemIn( this, 70, 95, new ManiRune()	);
			PlaceItemIn( this, 80, 95, new NoxRune()	);
			PlaceItemIn( this, 90, 95, new OrtRune()	);
			PlaceItemIn( this, 100, 95, new PasRune()	);
			PlaceItemIn( this, 110, 95, new PorRune()	);
			PlaceItemIn( this, 120, 95, new QuasRune()	);
			PlaceItemIn( this, 130, 95, new RelRune()	);
			PlaceItemIn( this, 140, 95, new SarRune()	);

			PlaceItemIn( this, 50, 110, new SanctRune()	);
			PlaceItemIn( this, 60, 110, new TymRune()	);
			PlaceItemIn( this, 70, 110, new UusRune()	);
			PlaceItemIn( this, 80, 110, new VasRune()	);
			PlaceItemIn( this, 90, 110, new WisRune()	);
			PlaceItemIn( this, 100, 110, new XenRune()	);
			PlaceItemIn( this, 110, 110, new YlemRune()	);
			PlaceItemIn( this, 120, 110, new ZuRune()	);
			PlaceItemIn( this, 130, 110, new LumRune()	);
			PlaceItemIn( this, 140, 110, new MuRune()	);

			PlaceItemIn( this, 50, 125, new AhmRune()	);
			PlaceItemIn( this, 60, 125, new BehRune()	);
			PlaceItemIn( this, 70, 125, new CahRune()	);
			PlaceItemIn( this, 80, 125, new OmRune()	);
			PlaceItemIn( this, 90, 125, new RaRune()	);
			PlaceItemIn( this, 110, 125, new SummRune()	);

		}

		private static void PlaceItemIn( Container parent, int x, int y, Item item )
		{
			parent.AddItem( item );
			item.Location = new Point3D( x, y, 0 );
		}

		public override bool DisplayLootType{ get{ return false; } }

		public BagOfRunes( Serial serial ) : base( serial )
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