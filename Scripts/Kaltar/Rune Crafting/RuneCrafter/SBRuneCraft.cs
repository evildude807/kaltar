using System;
using System.Collections;
using Server.Items;

namespace Server.Mobiles
{
	public class SBRuneCraft: SBInfo
	{
		private ArrayList m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBRuneCraft()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override ArrayList BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : ArrayList
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo( typeof( ScribesPen ), 8,  20, 0xFBF, 0 ) );
				Add( new GenericBuyInfo( typeof( BlankScroll ), 5, 999, 0x0E34, 0 ) );
				Add( new GenericBuyInfo( typeof( RuneChisel ), 5000,  20, 0x10E7, 0xB92 ) );
				Add( new GenericBuyInfo( typeof( BlankRune ), 15, 999, 0x1726, 0x3e8 ) );
				//Add( new GenericBuyInfo( "1041267", typeof( Runebook ), 3500, 10, 0xEFA, 0x461 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( ScribesPen ), 4 );
				Add( typeof( BlankRune ), 3 );
				Add( typeof( RoughStone ), 8 );
				Add( typeof( RuneChisel ), 9 );
				Add( typeof( BlankScroll ), 3 );
			}
		}
	}
}