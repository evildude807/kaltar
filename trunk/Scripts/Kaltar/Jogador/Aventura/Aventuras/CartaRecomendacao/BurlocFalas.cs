using System;
using Server;
using Server.Mobiles;
using Kaltar.aventura;
using Kaltar.propriedade;
using Server.Items;
using Arya.DialogEditor;

namespace Kaltar.aventura {
	
	/// <summary>
	/// Description of BurlocFalas.
	/// </summary>
	public class BurlocFalas {
		
		/**
		 * Jogar entrando na academia de guerreiros.
		 */ 
		public static void alistarAcademia(Mobile m, DialogNPC npc) {
			Jogador jogador = (Jogador)m;			
			
			CartaRecomendacaoItem cartaRec = m.Backpack.FindItemByType(typeof( CartaRecomendacaoItem ), true) as CartaRecomendacaoItem;
			
			//fala sobre a carta
			if(cartaRec == null) {
				//falando sobre a carta
				npc.RunSpeechGump( "a7a1b0d8-563a-435d-b834-f60ccb43a770", m );			
			}
			else if(cartaRec.Name.Equals("Carta de recomendação para " + m.Name)){
				
				cartaRec.Delete();
				
				if(jogador.classe.Equals(classe.Aldeao)) {
					jogador.setClasse = classe.Escudeiro;
					
					//boas vindas a academia
					npc.RunSpeechGump( "da033fe2-c8a4-4f84-aab0-509485dfd495", m );									
				}
				else {
					npc.SayTo(jogador, "Você já escolheu o seu caminho.");
				}
			}
			else {
				npc.Say("Esta carta de recomendação não é para você.");
			}
		}

		/**
		 * Jogar pedindo para se alistar na academia de guerreiros.
		 */ 
		public static void queroMeAlistar(Mobile m, DialogNPC npc) {
			Jogador jogador = (Jogador)m;	
			CartaRecomendacaoItem cartaRec = m.Backpack.FindItemByType(typeof( CartaRecomendacaoItem ), true) as CartaRecomendacaoItem;
			
			//fala sobre a carta
			if(cartaRec == null) {
				
				if(!jogador.getSistemaAventura().pegouAventura(IDAventura.cartaRecomendacao)) {
					jogador.getSistemaAventura().iniciarAventura(IDAventura.cartaRecomendacao);
				}
				
				//falando sobre a carta
				npc.RunSpeechGump( "a7a1b0d8-563a-435d-b834-f60ccb43a770", m );			
			}
			else {
				//confirmação de alistamento
				npc.RunSpeechGump( "8e544309-53ef-419e-811b-24ecfb426a37", m );							
			}
		}		
	}
}
