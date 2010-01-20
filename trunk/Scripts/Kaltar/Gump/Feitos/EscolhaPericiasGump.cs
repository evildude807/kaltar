using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;
using Server.Misc;
using Server.Items;
using Server.Network;
using Server.Accounting;

namespace Server.Kaltar.Gumps {
	public class EscolhaPericiasGump : Gump {
		
		private Jogador jogador;
		private int totalPontos = 120;	
		private double maxPericia = 30.0;	
		private double minimoPericia = 0.0;
		private int periciasPorpagina = 10;
					
		private int inicio;
		private SkillInfo[] pericias = SkillInfo.Table;
		private int[] valoresPericia;
		
		public EscolhaPericiasGump(Jogador jogador, int[] valores, int inicio) : base( 20, 20 ) {
			
			this.jogador = jogador;
			this.pericias = pericias;
			this.valoresPericia = valores;
			this.inicio = inicio;
		
			this.Closable=false;
			this.Disposable=false;
			this.Dragable=true;
			this.Resizable=false;
			
			this.AddPage(0);
			this.AddBackground(58, 14, 371, 570, 9380);
			this.AddImage(83, 52, 52);
			this.AddLabel(146, 53, 0, @"Perícias");
			
			//total disponivel
			this.AddImage(333, 53, 51);
								
			this.AddLabel(355, 66, 0, totalParaGastar().ToString());
			
			this.AddLabel(199, 127, 0, @"Valor");			
			this.AddLabel(336, 112, 0, @"Máximo");
			this.AddLabel(335, 156, 0, @"Mínimo");
			this.AddLabel(348, 130, 0, maxPericia.ToString());
			this.AddLabel(349, 176, 0, minimoPericia.ToString());
		
			criarPaginas();
		}
		
		private int totalParaGastar() {
			
			int totalJogador = 0;
			for(int i = 0; i < valoresPericia.Length; i++) {
				totalJogador += valoresPericia[i];
			}
			
			return (totalPontos - totalJogador);
		}
		
		public enum Buttons
		{
			bsubirPer,
			bdecerPer,
			bproxPagina,
			banterioPagina,
		}
		
		private void criarPaginas() {
			//jogador.SendMessage( "inicio {0}", inicio);
			int posYsomar = 30;
			int posY = 160;

			int count = 1;
			for(int i = inicio; i < inicio + periciasPorpagina && i < pericias.Length; i++ ) {
				SkillInfo pericia = pericias[i];
			
				int posYCorrente = posY + (posYsomar * (count - 1));
				
				this.AddLabel(97, posYCorrente, 0, pericia.Name);
				this.AddLabel(211, posYCorrente, 31, valoresPericia[i].ToString());
				this.AddButton(273, posYCorrente, 2435, 2436, (i+1000), GumpButtonType.Reply, (int)Buttons.bsubirPer);
				this.AddButton(273, posYCorrente + 15, 2437, 2438, (i+2000), GumpButtonType.Reply, (int)Buttons.bdecerPer);
				
				//controle dos botoes de paginaçao e ok
				if(count == periciasPorpagina) {
					
					if(totalParaGastar() == 0) {
						this.AddButton(206, 521, 247, 248, -1, GumpButtonType.Reply, 0);					
					}
					
					//proxima e anterior pagina					
					if(inicio != 0) {
						this.AddButton(88, 512, 4506, 4506, inicio - periciasPorpagina, GumpButtonType.Reply, inicio);
					}
					this.AddButton(353, 511, 4502, 4502, inicio + periciasPorpagina, GumpButtonType.Reply, i);

					count = 0;
				}
				count++;
			}
			
			if(count != 1) {
				
				if(totalParaGastar() == 0) {
					this.AddButton(206, 521, 247, 248, -1, GumpButtonType.Reply, 0);								
				}

				jogador.SendMessage( "botao voltar {0}", inicio - periciasPorpagina);
				this.AddButton(88, 512, 4506, 4506, inicio - periciasPorpagina, GumpButtonType.Reply, inicio);
			}
		}

		private enum Acao {
			subir, descer
		}		
		
		private void subirPericia(int indicePericia) {
			
			if(validarPericia(indicePericia, Acao.subir)) {
				valoresPericia[indicePericia] += 5;
			}
			else {
				jogador.SendMessage( "Você não pode subir esta pericia.");
			}
			
		}
		
		private void descerPericia(int indicePericia) {
			
			if(validarPericia(indicePericia, Acao.descer)) {
				valoresPericia[indicePericia] -= 5;
			}
			else {
				jogador.SendMessage( "Você não pode diminuir esta pericia.");
			}
			
		}
		
		private bool validarPericia(int indicePericia, Acao acao) {
			
			//valida o total de pontos
			if(acao.Equals(Acao.subir)) {
				if(totalParaGastar() == 0) {
					return false;
				}
			}
			
			if(Acao.subir.Equals(acao)) {
				if(valoresPericia[indicePericia] >= maxPericia) {
					return false;
				}
				else {
					return true;
				}
			}
			else {
				if(valoresPericia[indicePericia] <= minimoPericia) {
					return false;
				}
				else {
					return true;
				}				
			}
		}
		
		private void teleportarParaCidade() {

			CityInfo cidadeInicial = new CityInfo( "Ouro Branco", "Estrada real", 1605, 672, 0, Map.Malas );
			jogador.MoveToWorld( cidadeInicial.Location, cidadeInicial.Map );			
		}
		
		private void aplicarValores() {
			
			for(int i = 0; i < pericias.Length; i++) {
				jogador.Skills[pericias[i].SkillID].Base = valoresPericia[i];
			}			
		}
		
		public override void OnResponse(Server.Network.NetState sender, RelayInfo info) {
			
			int botaoID = info.ButtonID;
			
			if(botaoID == -1) {
				//jogador.SendMessage( "botao ok");

				if(totalParaGastar() == 0) {
					aplicarValores();
					teleportarParaCidade();
				}
				else {
					jogador.SendMessage("Nao foram distribuidos todos os pontos.");
					jogador.SendGump(new EscolhaPericiasGump(jogador, valoresPericia, inicio));
				}
				
			}
			//botao de paginacao
			else if(botaoID < 1000) {
				//jogador.SendMessage( "botao de paginacao");
				jogador.SendGump(new EscolhaPericiasGump(jogador, valoresPericia, botaoID));
			}
			//botao de subir skill
			else if(botaoID < 2000){
				int indiceSkill = botaoID - 1000;
				//jogador.SendMessage( "subir pericia {0}", pericias[indiceSkill].Name);
				
				subirPericia(indiceSkill);
				
				jogador.SendGump(new EscolhaPericiasGump(jogador, valoresPericia, inicio));
			}
			else if(botaoID >= 2000) {
				int indiceSkill = botaoID - 2000;
				//jogador.SendMessage( "descer pericia {0}", pericias[indiceSkill].Name);				
				
				descerPericia(indiceSkill);
				
				jogador.SendGump(new EscolhaPericiasGump(jogador, valoresPericia, inicio));
			}
		}
	}
}
