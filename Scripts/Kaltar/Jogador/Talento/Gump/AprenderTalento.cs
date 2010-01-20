using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;
using System.Collections;
using System.Collections.Generic;

namespace Kaltar.Talentos
{
	public class AprenderTalento : Gump
	{
		private Jogador jogador = null;
		private List<IDTalento> talentos = null;
		
		public AprenderTalento(Jogador jogador, List<IDTalento> talentos) : base( 10, 30 ) {
			
			this.jogador = jogador;
			this.talentos = talentos;
			
			this.Closable=true;
			this.Disposable=false;
			this.Dragable=true;
			this.Resizable=false;
			
			jogador.CloseGump( typeof( AprenderTalento ) );
			
			this.AddPage(0);
			this.AddBackground(10, 15, 482, 430, 9380);
			this.AddLabel(105, 54, 193, @"Aprender Talentos");
			this.AddImage(32, 56, 52);
			this.AddImage(401, 53, 51);
			this.AddLabel(417, 67, 0, @jogador.getSistemaTalento().pontosDisponiveis() + "");
			this.AddHtml( 72, 93, 395, 38, @"Escolha o talento que deseja aprender.<br>Clique no talento e uma tela irá exibir a descrião e confimação para o aprendizado.", (bool)false, (bool)false);
			
			listarTalentos();
		}
		
		private void listarTalentos() {
			
			Talento talento = null;
			int y = 140;
			for(int i = 0; i < talentos.Count; i++) {
				talento = Talento.getTalento(talentos[i]);
				if(talento != null) {
			
					this.AddLabel(65, y + (i*20), 0, @talento.Nome);
					this.AddButton(45, y + (i*20), 1210, 1209, (i+1), GumpButtonType.Reply, 1);
				}
			}
		}
		
		public override void OnResponse(Server.Network.NetState sender, RelayInfo info) {

			if(info.ButtonID != 0) {
				jogador.CloseGump( typeof( AprenderTalento ) );
				jogador.SendGump(new ConfirmarAprenderTalento(jogador, talentos[info.ButtonID-1], this));
			}
		}		
		
		public static void Initialize() {
			CommandSystem.Register( "atalento", AccessLevel.Player, new CommandEventHandler( aprenderTalentos ) );
		}		

		private static void aprenderTalentos( CommandEventArgs e ) {
			Jogador jogador = (Jogador)e.Mobile;
			
			List<IDTalento> talentos = new List<IDTalento>();
			talentos.Add(IDTalento.alerta);
			talentos.Add(IDTalento.escudoPequeno);
			
			jogador.SendGump(new AprenderTalento(jogador, talentos));
		}		
		
		public enum Buttons {
			aprenderTalento,
			cancelarAprenderTalento,
		}
		
		protected class ConfirmarAprenderTalento : Gump {
			
			private Jogador jogador = null;			
			private IDTalento idTalento;
			private AprenderTalento aprenderTalentoGump = null;
			
			public ConfirmarAprenderTalento(Jogador jogador, IDTalento idTalento, AprenderTalento gump) : base( 10, 30 ) {
				
				this.jogador = jogador;
				this.idTalento = idTalento;
				this.aprenderTalentoGump = gump;
					
				this.Closable=false;
				this.Disposable=false;
				this.Dragable=true;
				this.Resizable=false;

				jogador.CloseGump( typeof( ConfirmarAprenderTalento ) );				
				
				this.AddPage(0);
				this.AddBackground(86, 89, 403, 328, 9380);
				this.AddImage(108, 132, 52);
				this.AddLabel(208, 127, 193, @"Confirmação de talento");

				string descricao = montarDescricao();

				this.AddHtml( 146, 170, 304, 173, @descricao, (bool)true, (bool)true);
				this.AddButton(215, 353, 247, 249, (int)Buttons.aprenderTalento, GumpButtonType.Reply, 0);
				this.AddButton(292, 352, 241, 243, (int)Buttons.cancelarAprenderTalento, GumpButtonType.Reply, 0);

			}
			
			private string montarDescricao() {
				
				Talento talento = Talento.getTalento(idTalento);
				
				string descricao = "<u>Talento:</u> <b>" + talento.Nome + "</b><br>" +
					"<u>Descrição:</u> " + (talento.Descricao != null ? talento.Descricao : "Nenhuma descrição.") + "<br><u>" +
					"Pré-requisito:</u> " + (talento.PreRequisitos != null ? talento.PreRequisitos : "Nenhum pré-requisito.");
				return descricao;
			}
			
			public override void OnResponse(Server.Network.NetState sender, RelayInfo info) {
				
				if(info.ButtonID == (int)Buttons.cancelarAprenderTalento) {
					jogador.CloseGump( typeof( ConfirmarAprenderTalento ) );
					jogador.SendGump(aprenderTalentoGump);					
				}
				else if(info.ButtonID == (int)Buttons.aprenderTalento) {
					bool aprendeu = jogador.getSistemaTalento().aprender(idTalento);
					
					if(aprendeu) {
						jogador.SendMessage("Você acaba de aprender o talento {0}", Talento.getTalento(idTalento).Nome);
					}
					else {
						jogador.SendMessage("Você não pode aprender o talento {0}", Talento.getTalento(idTalento).Nome);
					}
				}
			}			
		}
	}
}
