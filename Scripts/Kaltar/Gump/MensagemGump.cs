using System;
using Server;
using Server.Gumps;
using Server.Mobiles;

namespace Kaltar.Gumps
{
	public class MensagemGump : Gump
	{
		public MensagemGump(string titulo, string msg) : base(10,10) {
			
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=true;
			
			this.AddPage(0);
			this.AddBackground(22, 19, 477, 386, 9380);
			this.AddHtml( 49, 76, 419, 289, @msg,(bool)false, (bool)true);
			this.AddLabel(229, 52, 1208, @titulo);
		}
	}
	
	public class Pergaminho : Item {

		private string k_msg = "";
		private string k_titulo = "";
				
		[Constructable]
		public Pergaminho() {
			ItemID = 5360;
		}

		public Pergaminho( Serial serial ) : base( serial ){
		}
		
		public virtual string Mensagem {
			get{return k_msg;}
			set{k_msg = value;}
		}
		
		public virtual string Titulo {
			get{return k_titulo;}
			set{k_titulo = value;}
		}
		
		public override void OnDoubleClick( Mobile from ) {
			
			if(from.CanSee(this)) {
				MensagemGump mg = new MensagemGump(Titulo,Mensagem);
				from.CloseGump(typeof(MensagemGump));
				from.SendGump(mg);
			}
			else {
				from.SendMessage("Você não consegue ver o pergaminho.");
			}
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write((string)k_titulo);
			writer.Write((string)k_msg);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			k_titulo = reader.ReadString();
			k_msg = reader.ReadString();
		}
	}
}
