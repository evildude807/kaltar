using System; 
using System.IO; 
using Server; 
using Server.Network; 
using Server.Gumps; 
using Server.Commands;
//using Server.Accounting; 
using Server.Mobiles; 

namespace Server.Misc 
{ 
   public class NEWSGump : Gump 
   { 
      public static string pathload = "Scripts/Kaltar/Noticias/motd.txt"; 
      public static string pathnews = "Scripts/Kaltar/Noticias/news.txt"; 
      public static string title = "Novidades"; 
      public static string body = "Não existe nenhum novidade."; 
      public static string oldtitle = "Velhas notícias."; 
      public static string oldbody = "Não há notícias velhas."; 

      public static void Initialize() 
      { 
         CommandSystem.Register( "addNovidade", AccessLevel.Administrator, new CommandEventHandler( MOTD_OnCommand ) ); 
		 CommandSystem.Register( "novidade", AccessLevel.Player, new CommandEventHandler( verNovidade_OnCommand ) ); 
         loadNEWS(); 
      } 

      public static void loadNEWS() 
      { 
         if ( File.Exists( pathnews ) ) 
         { 
            StreamReader ip = new StreamReader ( pathnews ); 
            title = ip.ReadLine(); 
            if(title == "") 
               title = "Novidades"; 
            body = ip.ReadToEnd(); 
            if(body == "") 
               body = "Não existe novidades."; 
            ip.Close(); 
         } 
      } 

      public static void loadMOTD() 
      { 
         if ( File.Exists( pathload ) ) 
         { 
            StreamReader ip = new StreamReader ( pathload ); 
            title = ip.ReadLine(); 
            body = ip.ReadToEnd(); 
                                ip.Close(); 
                                File.Delete( pathload ); 
            if ((title == "") && (body == "")) 
            { 
            } 
            else 
            { 
               if (title == "") 
               { 
                  title = ( "Novidades em: " + Convert.ToString( DateTime.Now.Day ) + "." + Convert.ToString( DateTime.Now.Month ) + "." + Convert.ToString( DateTime.Now.Year ) ); 
               } 
               if ( File.Exists( pathnews ) ) 
               { 
                  StreamReader oln = new StreamReader (pathnews); 
                  oldtitle = oln.ReadLine(); 
                  oldbody = oln.ReadToEnd(); 
                  oln.Close(); 
                  if ((oldtitle == "") && (oldbody == "")) 
                  { 
                                         File.Delete( pathnews ); 
                     StreamWriter update = File.CreateText(pathnews); 
                     update.WriteLine(title); 
                     update.Write(body); 
                     update.Close(); 
                  } 
                  else 
                  { 
                     if (oldtitle == "") 
                        oldtitle = "Novidades antigas"; 
                     if (oldbody == "") 
                        oldbody = "Não existe novidades antigas"; 
                                                        File.Delete( pathnews ); 
                     StreamWriter update = File.CreateText(pathnews); 
                     update.WriteLine(title); 
                     update.Write(body); 
                          update.WriteLine (); 
                                                        update.WriteLine("------------------------------------------------------"); 
                     update.WriteLine(oldtitle); 
                     update.WriteLine("------------------------------------------------------"); 
                     update.Write(oldbody); 
                     update.Close(); 
                  } 
               } 
               else 
               { 
                                      StreamWriter update = File.CreateText(pathnews); 
                                      update.WriteLine(title); 
                                      update.Write(body); 
                                      update.Close(); 
               } 
            } 
         } 
      } 

		[Usage( "addNovidade" )] 
                [Description( "Mostra o menu para cadastrar Novidade." )] 
                public static void MOTD_OnCommand( CommandEventArgs args ) 
                { 
                      Mobile m = args.Mobile; 
                      m.SendGump( new NEWSGump( m, true ) ); 
                } 

		[Usage( "novidade" )] 
                [Description( "Mostra as novidades do shartd." )] 

                public static void verNovidade_OnCommand ( CommandEventArgs args ) 
                { 
                      Mobile m = args.Mobile; 
                      m.SendGump( new NEWSGump( m, false ) ); 
                } 

                public NEWSGump( Mobile m, bool admin ) : base( 140, 80 ) 
                { 
                   if ( admin ) 
                        { 
                        AddPage( 1 ); 
                        AddBackground( 0, 0, 160, 70, 5054 ); 
                        AddButton( 10, 10, 0xFB7, 0xFB9, 3, GumpButtonType.Reply, 0 ); 
                        AddLabel( 45, 10, 0x34, "Adicionar novidade" ); 
                        AddButton( 10, 35, 0xFB7, 0xFB9, 4, GumpButtonType.Reply, 0 ); 
                        AddLabel( 45, 35, 0x34, "Atualizar Novidades" ); 
                        } 
                        else 
                        { 
                        AddPage( 0 ); 
                        AddImageTiled( 0, 0, 400, 400, 0x52 ); 
                        AddAlphaRegion( 1, 1, 398, 398 ); 
                        AddImageTiled( 10, 10, 380, 20, 0xBBC ); 
                        AddImageTiled( 11, 11, 378, 18, 0x2426 ); 
                        AddHtml( 11, 11, 378, 18, "<div align=\"center\" color=\"2100\">"+ title +"</div>", false, false ); 
                        AddImageTiled( 10, 42, 380, 300, 0xBBC ); 
                        AddImageTiled( 11, 43, 378, 298, 0x2426 ); 
                        AddHtml( 13, 43, 376, 298, body, false, true ); 
                        } 
                } 

                public override void OnResponse( NetState sender, RelayInfo info ) 
                { 
                        Mobile from = sender.Mobile; 
//                        Account acct=(Account)from.Account; 
                        switch ( info.ButtonID ) 
                        { 
                           case 1: 
                           { 
//                                   acct.SetTag( "motd", "true" ); 
                                   from.SendGump( new NEWSGump( from, false ) ); 
                                   break; 
                           } 
                           case 2: 
                           { 
//                                   acct.SetTag( "motd", "false" ); 
                                   from.SendGump( new NEWSGump( from, false ) ); 
                                   break; 
                           } 
                           case 3: 
                           { 
								
				
					from.SendGump( new CadastrarNovidade(pathnews) ); 
//                                   loadMOTD(); 
//                                   from.SendGump( new NEWSGump( from, true ) ); 
                                   break; 
                           } 
                           case 4: 
                           { 
                                   loadNEWS(); 
//                                   foreach ( Mobile m in World.Mobiles.Values ) 
//                                   { 
//                                           if(m == null) 
//                                                   continue; 
//                                           PlayerMobile pm = m as PlayerMobile; 
//                                           if ( pm is PlayerMobile ) 
//                                           { 
//                                                   Account accoun = (Account)m.Account; 
//                                                   if(accoun == null) 
//                                                        continue; 
//                                                   accoun.SetTag( "motd", "false" ); 
//                                           } 
//                                   } 
                                   from.SendMessage( "Todos os jogadores verão as novidades ao logar." ); 
                                   from.SendGump( new NEWSGump( from, true ) ); 
                                   break; 
                           } 
                        } 
                } 
        } 
} 
