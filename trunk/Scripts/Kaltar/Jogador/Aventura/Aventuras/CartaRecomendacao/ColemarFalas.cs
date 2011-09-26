using System;
using Server;
using Server.Mobiles;
using Kaltar.aventura;
using Kaltar.propriedade;
using Server.Items;
using Arya.DialogEditor;
using Kaltar.Classes;

namespace Kaltar.aventura {

	/// <summary>
	/// Description of ColemarFalas.
	/// </summary>
	public class ColemarFalas {
		
		/**
		 * Iniciar conversa.
		 */
		public static void iniciarConversa(Mobile m, DialogNPC npc) {
			Jogador jogador = (Jogador)m;
			
			//jogaro já tem classe
			if(!jogador.getSistemaClasse().getClasse().idClasse().Equals(classe.Aldeao)) {
				npc.SayTo(jogador, "O seu caminho já foi escolhido. Não tenho nada para você no momento.");
			}
			else if(jogador.getSistemaAventura().completouAventura(IDAventura.cartaRecomendacao) &&
               jogador.getSistemaClasse().getClasse().idClasse().Equals(classe.Aldeao) &&
			   m.Backpack.FindItemByType(typeof( CartaRecomendacaoItem ), true) == null) {
	
				npc.SayTo(jogador, "Tome mais cuidado com suas coisas. Aqui esta outra carta de recomendação.");
				CartaRecomendacaoItem cartaRec = new CartaRecomendacaoItem(jogador);
				jogador.AddToBackpack(cartaRec);				
			}
			else if(jogador.getSistemaAventura().pegouAventura(IDAventura.cartaRecomendacao)) {
				//pedir o favor
				npc.RunSpeechGump( "7933cac2-3294-4cd9-98f6-7427a57b6ec2", jogador );			
			}
			else {
				//falar para andar pela cidade a procura de um treinamento
				npc.RunSpeechGump( "92066456-80f4-43f6-a5f0-2510d894894b", jogador );			
			}
		}
		
		/**
		 * Concluir quest da carta de recomendação 
		 */
		public static void concluirQuestCartaRecomendacao(Mobile m, DialogNPC npc) {
			Jogador jogador = (Jogador)m;

			if(jogador.getSistemaAventura().pegouAventura(IDAventura.cartaRecomendacao)) {
				
				jogador.getSistemaAventura().finalizarAventura(IDAventura.cartaRecomendacao);

				CartaRecomendacaoItem cartaRec = new CartaRecomendacaoItem(jogador);
				jogador.AddToBackpack(cartaRec);
			}
		}		
	}
}
