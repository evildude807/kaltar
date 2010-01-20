using System;
using Server;
using Server.Gumps;
using Server.Mobiles; 
using Server.Network; 
using System.IO; 
using Server.Misc;

namespace Server.Gumps
{
	public class CadastrarNovidade : Gump
	{
		private string pathnews;

		public CadastrarNovidade(string pathnews): base( 50, 50 )
		{
			this.pathnews = pathnews;
			
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(18, 18, 423, 307, 9200);
			this.AddLabel(153, 25, 1627, @"Digite a Novidade.");
			this.AddButton(151, 291, 238, 239, (int)Buttons.Button1, GumpButtonType.Reply, 0);
			this.AddBackground(37, 129, 384, 156, 2620);
			this.AddButton(231, 291, 241, 242, (int)Buttons.Button2, GumpButtonType.Reply, 0);
			this.AddLabel(42, 52, 0, @"Título");
			this.AddLabel(42, 108, 0, @"Novidade");
			this.AddBackground(42, 71, 377, 37, 2620);
			this.AddTextEntry(48, 79, 364, 21, 2, (int)Buttons.TextEntry1, @"");
			this.AddTextEntry(44, 137, 370, 139, 2, (int)Buttons.NovidadeEntry, @"Novidade...");

		}
		
		public enum Buttons
		{
			NovidadeEntry,
			Button1,
			Button2,
			TextEntry1,
		}

		public override void OnResponse( NetState sender, RelayInfo info ) 
		{ 
		        Mobile from = sender.Mobile; 
		
		        switch ( info.ButtonID ) 
		        { 
		           	case (int)Buttons.Button1: 
		          	{ 
		          		//realiza as modificações no arquivo
		          		TextRelay corpo = info.GetTextEntry((int)Buttons.NovidadeEntry );
		          		TextRelay titulo = info.GetTextEntry((int)Buttons.TextEntry1 );
						aplicarNovidade(titulo.Text, corpo.Text);
		          		break;
					}
		        }
		}

		private void aplicarNovidade(string titulo, string corpo) {
	
	       if ( File.Exists( pathnews ) && titulo != null && corpo != null && !titulo.Equals("") && !corpo.Equals(""))
	       { 
					StreamReader oln = new StreamReader (pathnews); 
					string oldtitle = oln.ReadLine(); 
					string oldbody = oln.ReadToEnd(); 
					oln.Close(); 
					
					if (oldtitle == "") 
					  oldtitle = "Old news"; 
					if (oldbody == "") 
					  oldbody = "There is no old news."; 
					
					File.Delete( pathnews );
					StreamWriter update = File.CreateText(pathnews); 
					update.WriteLine(titulo); 
					update.Write(corpo); 
					update.WriteLine (); 
					update.WriteLine("------------------------------------------------------"); 
					update.WriteLine(oldtitle); 
					update.WriteLine("------------------------------------------------------"); 
					update.Write(oldbody); 
					update.Close(); 
	          } 
		   else 
	       { 
	               StreamWriter update = File.CreateText(pathnews); 
	               update.WriteLine(titulo); 
	               update.Write(corpo); 
	               update.Close(); 
	       }
		NEWSGump.loadNEWS();
	    } 

	}
}
