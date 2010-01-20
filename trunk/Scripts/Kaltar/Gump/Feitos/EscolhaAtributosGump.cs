using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;

namespace Server.Kaltar.Gumps
{
	public class EscolhaAtributosGump : Gump {
		
		private Jogador jogador;
		private int totalPontos = 90;	
		private int maxAtributo = 40;	
		private int minimoAtributo = 10;
		
		private int forca;
		private int destresa;
		private int inteligencia;
		
		public EscolhaAtributosGump(Jogador jogador, int forca, int destresa, int inteligencia) : base( 20, 20 ) {
			
			this.jogador = jogador;
			this.forca = forca;
			this.destresa = destresa;
			this.inteligencia = inteligencia;
			
			this.Closable=false;
			this.Disposable=false;
			this.Dragable=true;
			this.Resizable=false;
			
			this.AddPage(0);
			this.AddBackground(77, 57, 299, 319, 9380);
			this.AddLabel(173, 102, 0, @"Atributos");
			this.AddImage(109, 99, 52);
			this.AddLabel(118, 184, 0, @"Força:");
			this.AddLabel(117, 226, 0, @"Destresa:");
			this.AddLabel(117, 269, 0, @"Inteligencia:");
			this.AddLabel(216, 185, 31, forca.ToString());
			this.AddLabel(216, 226, 31, destresa.ToString());
			this.AddLabel(216, 269, 31, inteligencia.ToString());
			this.AddLabel(202, 158, 0, @"Valor");
			this.AddButton(250, 181, 2435, 2436, (int)Buttons.bsubirFor, GumpButtonType.Reply, 0);
			this.AddButton(251, 198, 2437, 2438, (int)Buttons.bdescerFor, GumpButtonType.Reply, 0);
			this.AddButton(251, 223, 2435, 2436, (int)Buttons.bsubirDes, GumpButtonType.Reply, 0);
			this.AddButton(251, 240, 2437, 2438, (int)Buttons.bdescerDes, GumpButtonType.Reply, 0);
			this.AddButton(251, 265, 2435, 2436, (int)Buttons.bsubirInt, GumpButtonType.Reply, 0);
			this.AddButton(251, 282, 2437, 2438, (int)Buttons.bdescerInt, GumpButtonType.Reply, 0);
			this.AddImage(285, 97, 51);
			
			int totalParaGastar = (totalPontos - (forca + destresa + inteligencia));
			
			this.AddLabel(307, 110, 0, totalParaGastar.ToString());
			
			if(totalParaGastar == 0) {
				this.AddButton(196, 310, 247, 248, (int)Buttons.bOk, GumpButtonType.Reply, 0);
			}
			
			this.AddLabel(289, 157, 0, @"Máximo");
			this.AddLabel(288, 201, 0, @"Mínimo");
			this.AddLabel(301, 175, 0, maxAtributo.ToString());
			this.AddLabel(302, 221, 0, minimoAtributo.ToString());

		}
	
		public enum Buttons {
			bsubirFor,
			bdescerFor,
			bsubirDes,
			bdescerDes,
			bsubirInt,
			bdescerInt,
			bOk,
		}
		
		private enum Atributo {
			forca, destresa, inteligencia
		}
		private enum Acao {
			subir, descer
		}
		
		private void subirAtributo(Atributo atributo) {
			if(validarAtributo(atributo, Acao.subir)) {
				somarAtributo(atributo);
			}
			else {
				jogador.SendMessage( "Você não pode aumentar este atributo.");
			}
		}
		
		private void descerAtributo(Atributo atributo) {
			if(validarAtributo(atributo, Acao.descer)) {
				diminuirAtributo(atributo);
			}
			else {
				jogador.SendMessage( "Você não pode diminuir este atributo.");
			}			
		}
		
		private void somarAtributo(Atributo atributo) {
			if(atributo == Atributo.forca) {
				forca += 5;
			}
			else if(atributo == Atributo.destresa) {
				destresa += 5;
			}
			else {
				inteligencia += 5;
			}
		}
						
		private void diminuirAtributo(Atributo atributo) {
			if(atributo == Atributo.forca) {
				forca -= 5;
			}
			else if(atributo == Atributo.destresa) {
				destresa -= 5;
			}
			else {
				inteligencia -= 5;
			}
		}		
		
		private bool validarAtributo(Atributo atributo, Acao acao) {
			
			//valida o total de pontos
			if(acao.Equals(Acao.subir)) {
				int totalParaGastar = (totalPontos - (forca + destresa + inteligencia));
				if(totalParaGastar == 0) {
					return false;
				}
			}
			
			//força
			if(atributo == Atributo.forca) {
				
				if(acao == Acao.subir) {
					if(forca >= maxAtributo) {
						return false;
					}
					return true;
				}
				else {
					if(forca <= minimoAtributo) {
						return false;
					}
					return true;					
				}
			}
			
			//destresa
			else if(atributo == Atributo.destresa) {
				
				if(acao == Acao.subir) {
					if(destresa >= maxAtributo) {
						return false;
					}
					return true;
				}
				else {
					if(destresa <= minimoAtributo) {
						return false;
					}
					return true;					
				}
			}
			
			//inteligencia
			else {
				
				if(acao == Acao.subir) {
					if(inteligencia >= maxAtributo) {
						return false;
					}
					return true;
				}
				else {
					if(inteligencia <= minimoAtributo) {
						return false;
					}
					return true;					
				}
			}
		}
		
		public override void OnResponse(Server.Network.NetState sender, RelayInfo info) {

			if(info.ButtonID == (int)Buttons.bsubirFor) {
				subirAtributo(Atributo.forca);
			}
			else if(info.ButtonID == (int)Buttons.bdescerFor) {
				descerAtributo(Atributo.forca);
			}
			else if(info.ButtonID == (int)Buttons.bsubirDes) {
				subirAtributo(Atributo.destresa);
			}
			else if(info.ButtonID == (int)Buttons.bdescerDes) {
				descerAtributo(Atributo.destresa);
			}
			else if(info.ButtonID == (int)Buttons.bsubirInt) {
				subirAtributo(Atributo.inteligencia);
			}
			else if(info.ButtonID == (int)Buttons.bdescerInt) {
				descerAtributo(Atributo.inteligencia);
			}
			else if(info.ButtonID == (int)Buttons.bOk) {
				
				int totalParaGastar = (totalPontos - (forca + destresa + inteligencia));
				if(totalParaGastar == 0) {
					jogador.Str = forca;
					jogador.Dex = destresa;
					jogador.Int = inteligencia;

					jogador.SendGump(new EscolhaPericiasGump(jogador, new int[SkillInfo.Table.Length] ,0));
				}
				else {
					jogador.SendMessage("Nao foram distribuidos todos os pontos.");
				}
			}
				
			if(info.ButtonID != (int) Buttons.bOk) {
				jogador.SendGump(new EscolhaAtributosGump(jogador, forca, destresa, inteligencia));
			}
		}
	}
}
