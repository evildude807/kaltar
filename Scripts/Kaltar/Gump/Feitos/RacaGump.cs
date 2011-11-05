using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using Server.Mobiles;
using Kaltar.Raca;

namespace Server.Kaltar.Gumps
{
    public class RacaGump : Gump
    {
        Jogador caller;

        public static void Initialize()
        {
            CommandSystem.Register("comandoRaca", AccessLevel.Administrator, new CommandEventHandler(comandoRaca_OnCommand));
        }

        [Usage("comandoRaca")]
        [Description("Makes a call to your custom gump.")]
        public static void comandoRaca_OnCommand(CommandEventArgs e)
        {
            Jogador from = (Jogador)e.Mobile;

            if (from.HasGump(typeof(RacaGump)))
                from.CloseGump(typeof(RacaGump));
            from.SendGump(new RacaGump(from));
        }

        public RacaGump(Jogador from) : this()
        {
            caller = (Jogador)from;
        }

        public RacaGump() : base( 0, 0 )
        {
            this.Closable=false;
			this.Disposable=false;
			this.Dragable=false;
			AddPage(0);

			AddPage(1);
			AddBackground(137, 84, 479, 352, 9380);
			AddLabel(224, 129, 0, @"Raça");
			AddImage(171, 133, 52);
			AddLabel(205, 214, 0, @"Humano");
			AddLabel(206, 245, 0, @"Elfo");
			AddLabel(206, 274, 0, @"Meio-Orc");
			AddLabel(206, 306, 0, @"Elfo Negro");
			AddHtml( 288, 216, 284, 174, @"", (bool)true, (bool)true);
			AddButton(178, 218, 1209, 1210, (int)Buttons.bEscolhaHumano, GumpButtonType.Page, 2);
			AddButton(178, 248, 1209, 1210, (int)Buttons.bEscolhaElfo, GumpButtonType.Page, 3);
			AddButton(178, 279, 1209, 1210, (int)Buttons.bEscolhaMeioOrc, GumpButtonType.Page, 4);
			AddButton(178, 310, 1209, 1210, (int)Buttons.bEscolhaElfoNegro, GumpButtonType.Page, 5);
			
            AddPage(2);
			AddBackground(137, 84, 479, 491, 9380);
			AddImage(171, 133, 52);
			AddLabel(224, 129, 0, @"Raça");
			AddHtml( 174, 205, 407, 297, @"Humano", (bool)true, (bool)true);
			AddLabel(339, 177, 0, @"Humano");
			AddButton(511, 514, 247, 248, (int)Buttons.bOkHumano, GumpButtonType.Reply, 0);
			AddButton(428, 515, 241, 242, (int)Buttons.pgInicial, GumpButtonType.Page, 1);
			
            AddPage(3);
			AddBackground(137, 84, 479, 491, 9380);
			AddImage(171, 133, 52);
			AddLabel(224, 129, 0, @"Raça");
			AddHtml( 174, 205, 407, 297, @"Elfo", (bool)true, (bool)true);
			AddLabel(339, 177, 0, @"Elfo");
			AddButton(511, 514, 247, 248, (int)Buttons.bOkElfo, GumpButtonType.Reply, 0);
            AddButton(428, 515, 241, 242, (int)Buttons.pgInicial, GumpButtonType.Page, 1);
			
            AddPage(4);
			AddBackground(137, 84, 479, 491, 9380);
			AddImage(171, 133, 52);
			AddLabel(224, 129, 0, @"Raça");
			AddHtml( 174, 205, 407, 297, @"Meio-Orc", (bool)true, (bool)true);
			AddLabel(339, 177, 0, @"Meio-Orc");
			AddButton(511, 514, 247, 248, (int)Buttons.bOkMeioOrc, GumpButtonType.Reply, 0);
            AddButton(428, 515, 241, 242, (int)Buttons.pgInicial, GumpButtonType.Page, 1);
			
            AddPage(5);
			AddBackground(137, 84, 479, 491, 9380);
			AddImage(171, 133, 52);
			AddLabel(224, 129, 0, @"Raça");
			AddHtml( 174, 205, 407, 297, @"Elfo Negro", (bool)true, (bool)true);
			AddLabel(339, 177, 0, @"Elfo Negro");
			AddButton(511, 514, 247, 248, (int)Buttons.bOkElfoNegro, GumpButtonType.Reply, 0);
            AddButton(428, 515, 241, 242, (int)Buttons.pgInicial, GumpButtonType.Page, 1);
			
        }

        public enum Buttons
		{
			bEscolhaHumano,
			bEscolhaElfo,
			bEscolhaMeioOrc,
			bEscolhaElfoNegro,
			bOkHumano,
			bOkElfo,
			bOkMeioOrc,
			bOkElfoNegro,
            pgInicial
		}


        public override void OnResponse(NetState sender, RelayInfo info)
        {

            Race raca = null;

            switch(info.ButtonID)
            {
				case (int)Buttons.bOkHumano:
				{
                    raca = Race.Races[32];
					break;
				}
				case (int)Buttons.bOkElfo:
				{
                    raca = Race.Races[33];
					break;
				}
				case (int)Buttons.bOkMeioOrc:
				{
                    raca = Race.Races[34];
					break;
				}
				case (int)Buttons.bOkElfoNegro:
				{
                    raca = Race.Races[35];
					break;
				}
		    }

            SistemaRaca sistemaRaca = caller.getSistemaRaca();
            sistemaRaca.aplicarRaca(raca);
        }
    }
}