using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;
using System.Collections.Generic;

namespace Kaltar.Talentos {

	public class TalentosGump : Gump {

        public static void Initialize()
        {
            CommandSystem.Register("talento", AccessLevel.Player, new CommandEventHandler(talentosJogador));
        }

		public TalentosGump(Jogador jogador): base( 0, 0 ) {
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			
			jogador.CloseGump( typeof( TalentosGump ) );
			
			//ttulo	
			this.AddPage(0);
			this.AddBackground(10, 15, 482, 430, 9380);
			this.AddLabel(105, 54, 193, @"Talentos");
			this.AddImage(33, 52, 52);
			
			//total de pontos de talentos
			this.AddImage(401, 53, 51);
			this.AddLabel(417, 67, 0, @totalPontosTalento(jogador));			
			
			//lista de talentos
			listarTalentos(jogador);
		}

		private void listarTalentos(Jogador jogador) {

            Dictionary<IDTalento, IDTalento> talentos = jogador.getSistemaTalento().getTalentos();
			int y = 135;	//posicao do primeito talento
			
			if(talentos.Count == 0) {
				this.AddLabel(65, y, 0, @"Voce nao possui nenhum talento.");
			}
			
			Talento talento = null;
			foreach ( IDTalento idTalento in talentos.Values ){
				talento = Talento.getTalento(idTalento);
				if(talento != null) {
					this.AddLabel(65, y, 0, @talento.Nome);
					this.AddImage(33, y, 2087);
	
					y += 25;
				}
			}
		}
		
		private string totalPontosTalento(Jogador jogador) {
			return jogador.getSistemaTalento().pontosDisponiveis() + "";
		}

        private static void talentosJogador( CommandEventArgs e ) {
			Jogador jogador = (Jogador)e.Mobile;
			jogador.SendGump(new TalentosGump(jogador));
		}
	}
}
