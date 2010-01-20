// by Old School Oct 2008
#define RunUo2_0

using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;

namespace Server.Gumps
{
    public class WildAnimalsGump : Gump
    {
        Mobile caller;

        public static void Initialize()
        {
#if(RunUo2_0)
            CommandSystem.Register("WABounty", AccessLevel.Player, new CommandEventHandler(WABounty_OnCommand));
#else
            Register("WABounty", AccessLevel.Player, new CommandEventHandler(WABounty_OnCommand));
#endif
        }

        [Usage("WABounty")]
        [Description("Makes a call to your custom gump.")]
        public static void WABounty_OnCommand(CommandEventArgs e)
        {
            Mobile from = e.Mobile;

            if (from.HasGump(typeof(WildAnimalsGump)))
                from.CloseGump(typeof(WildAnimalsGump));
            from.SendGump(new WildAnimalsGump(from));
        }

        public WildAnimalsGump(Mobile from) : this()
        {
            caller = from;
        }

        public WildAnimalsGump() : base( 0, 0 )
        {
            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImageTiled(159, 122, 508, 390, 200);
			AddImage(151, 471, 204);
			AddImage(152, 116, 206);
			AddImage(630, 471, 205);
			AddImage(625, 114, 207);
			AddImage(152, 157, 202);
			AddImage(625, 162, 203);
			AddImageTiled(194, 472, 436, 43, 233);
			AddImageTiled(198, 118, 425, 41, 201);
			AddHtml( 192, 216, 441, 252, @"Introduction:
			Wild animals every where, killing crops, digging up our food, killing our cattle, destroying everything. It is up to you to track down these wild creatures and destroy them. These creatures our wild and dangerous. Return the deeds they drop in return for your reward.
			
			Wild Bears:
			Wild bears our killing our sheep and invadeing our country side. Please destroy them.
			Bounty:2,000 Gold
			
			Wild Wolfs:
			Wild wolfs our feeding our our chickens and hens. The farmers our going crazy. They can not sell any fresh chickens to the cooks to make money. Remove them from the area.
			Bounty:3,000 Gold
			
			Wild Panthers:
			Wild panthers our killing innocent people. End their killing spree quickly.
			Bounty:1,200 Gold
			
			Wild Cougars:
			Wild cougars our feeding on the horses in the stables. Remove them from the area.
			Bounty:800 Gold
			
			Wild Boars:
			Wild boars our eating up the crops. the farmers our complaining about them. Kill all the wild boars.
			Bounty:500 Gold
			
			Wild Giant Serpents:
			Wild giant serpents our feeding off the small animals in the area. They seem to only come into the farming areas at night. Kill them all, show no mercy.
			Bounty:2,500 Gold
			
			Wild Giant Rats:
			Wild giant rats in the sewers our spreading dieases at a rapid speed. Kill them before they infect us all. 
			Bounty:3.500 Gold
			
			Wild Scropions:
			Wild Scropions our making their way toward the city. The king has ordered they be destroyed. The king is paying a pretty nice bounty.
			Bounty:4,000 Gold
			
			Wild Centaurs:
			Wild centaurs our trying to gather togethier and revolt. Our queen ordered they all be killed. She is paying a mighty fee for their removal.
			Bounty:5,000 Gold
			
			Wild Alligators:
			Wild alligators seem to be attacking people. The waters have become unsafe. The locals our requesting someone remove them from the area.
			Bounty:2,800 Gold", (bool)true, (bool)true);
			AddLabel(264, 163, 1160, @"Ultima Online Knights Wild Animals Bounty List");
			

            
        }

        

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            switch(info.ButtonID)
            {
                				case 0:
				{

					break;
				}

            }
        }
    }
}