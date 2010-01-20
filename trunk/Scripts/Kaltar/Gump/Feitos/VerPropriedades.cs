using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;
using Server.Misc;
using Server.Items;
using Server.Network;
using Server.Accounting;
using System.Collections;
using Server.Gumps.Kaltar;

namespace Server.Kaltar.Gumps
{
	public class VerPropriedades : Gump {

		private Jogador jogador;
		private static int itensPorPagina = 30;
		private ArrayList chaves;			
		private int inicial;
		
		public VerPropriedades(Jogador jogador, int inicio) : base( 0, 0 ) {
			
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			this.jogador = jogador;
			this.inicial = inicial;
			
			this.AddPage(0);
			this.AddBackground(31, 27, 394, 400, 9380);
			this.AddLabel(110, 60, 5, @"Propriedades");
			this.AddLabel(244, 75, 5, @"Jogador:");
			this.AddImage(56, 65, 52);
			this.AddButton(329, 102, 2460, 2461, (int)Buttons.bAdicionarProp, GumpButtonType.Reply, 0);

			//nome do jogaro
			this.AddLabel(304, 75, 0, @jogador.Name);
			
			this.AddLabel(92, 144, 5, @"Chave");
			this.AddLabel(239, 145, 5, @"Valor");
			
			criarListaChavePropriedades();
			
			listarPropriedades()	;
		}
		
		private void criarListaChavePropriedades() {
			Hashtable propriedades = jogador.getSistemaPropriedade().Propriedades;
			chaves = new ArrayList(propriedades.Keys.Count);
			foreach ( string chave in propriedades.Keys ){
				string valor = (string)propriedades[chave];
				chaves.Add(valor);
			}	
		}
		
		private void listarPropriedades() {
			
			Hashtable propriedades = jogador.getSistemaPropriedade().Propriedades;

			int posYsomar = 30;
			int posY = 168;
			int count = 1;
			for(int i = inicial; i < inicial + itensPorPagina && i < chaves.Count; i++ ) {
				
				string chave = (string)chaves[i];
				string valor = (string)propriedades[chave];
				
				int posYCorrente = posY + (posYsomar * (count - 1));
				
				this.AddLabel(92, posYCorrente, 43, @chave);
				this.AddLabel(240, posYCorrente, 43, @valor);
				this.AddButton(63, posYCorrente, 2117, 2118, (count-1) + 1000, GumpButtonType.Reply, 0);
				count++;
			}

		}

		public enum Buttons
		{
			bEditarProp,
			bAdicionarProp,
		}
		
		public override void OnResponse(Server.Network.NetState sender, RelayInfo info) {
			int botaoID = info.ButtonID;
			if(botaoID == -1) {
				
			}
			else if(botaoID >= 1000) {
				string chave = (string)chaves[botaoID - 1000];
				EditarPropriedades editarPropriedade = new EditarPropriedades(jogador, chave);
				jogador.SendGump(editarPropriedade);					
			}			
			else{
				jogador.SendGump(new VerPropriedades(jogador, 0));
			}
		}
	}
}
