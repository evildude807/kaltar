using System;
using System.Collections;
using Server.Items;

namespace Server.Mobiles
{
	public class SBHunter: SBInfo
	{
		private ArrayList m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBHunter()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override ArrayList BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : ArrayList
		{
			public InternalBuyInfo()
			{
				Add( new GenericBuyInfo("a Hunting Bounty Deed", typeof( WildAnimalsBountydeed ), 1, 20, 0x14F0, 0 ) );
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
				Add( typeof( WildBeardeed ), 2000 );
				Add( typeof( WildWolfdeed ), 3000 );
				Add( typeof( WildPantherdeed ), 1200 );
				Add( typeof( WildCougardeed ), 800 );
				Add( typeof( WildBoardeed ), 500 );
				Add( typeof( WildGiantSerpentdeed ), 2500 );
				Add( typeof( WildAlligatordeed ), 2800 );
				Add( typeof( WildRabbitdeed ), 2500 );
				Add( typeof( WildGiantRatdeed ), 3500 );
				Add( typeof( WildScorpiondeed ), 4000 );
				Add( typeof( WildCentaurdeed ), 5000 );
			}
		}
	}
}
