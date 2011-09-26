/*
 * Autor: Tiago Augusto Data: 26/2/2005 
 * Projeto: Kaltar
 */
 
using System;
using Server;
using Server.Mobiles;
using System.Collections.Generic;
using Server.ACC.CM;

namespace Kaltar.Talentos
{
	/// <summary>
	/// Description of SistemaTalento.
	/// </summary>
	public class SistemaTalento	{
		
		#region atributos

		//jogador dono dos talentos
		private Jogador jogador = null;

		#endregion
			
		public SistemaTalento(Jogador jogador){
			this.jogador = jogador;
		}

        /**
         * Recupera o modulo de talento
         */
        private TalentoModule getTalentoModule()
        {
            TalentoModule tm = (TalentoModule)CentralMemory.GetModule(jogador.Serial, typeof(TalentoModule));
            return tm;
        }

		/**
		 * Pontos disponíveis para aprender talentos.
		 */
		public int pontosDisponiveis() {

			int meses = (int)(DateTime.Now - jogador.CreationTime).TotalDays/30;
			int horasJogadas = jogador.GameTime.Hours;
			
			int minino = meses < (horasJogadas/30) ? meses : (horasJogadas/30);

            return minino - getTalentoModule().PontosGastos + 1;
		}
		
        public Dictionary<IDTalento, IDTalento> getTalentos() {
            return getTalentoModule().Talentos;
        }

		/*
		 * Adiciona o talento ao jogador.
		 * É verificado se o jogador possui os pré-requisitos
		 * e já não possua o talento.
		 * 
		 * @mudancaClasse true quando for mudança de classe,  para não verificar pontos de talento.
		 */ 
		public bool aprender (IDTalento idTalento, bool mudancaClasse) {
		 	Talento talento = Talento.getTalento(idTalento);
		 	
		 	if(talento == null) {
		 		jogador.SendMessage("Talento não encontrado, informe os administradores.");
		 		return false;	
		 	}
		 	
		 	if(!mudancaClasse && pontosDisponiveis() < 1) {
		 		jogador.SendMessage("Você não possui pontos de talento disponíveis.");
		 		return false;
		 	}
		 	
			if (!talento.possuiPreRequisitos (jogador)) {
		 		jogador.SendMessage("Você não possui os pré-requisitos para aprender o talento.");		 		
				return false;
			}
				
			if (possuiTalento(talento.ID)) {
				jogador.SendMessage("Você já possui o talento.");
				return false;
			}
		 	
			adicionarTalento(talento);
			
			if(!mudancaClasse) {
                getTalentoModule().PontosGastos++;
			}

			return true;		 	
		}
		 
		/*
		 * Adiciona o talento ao jogador.
		 * É verificado se o jogador possui os pré-requisitos
		 * e já não possua o talento.
		 */ 
		public bool aprender (IDTalento idTalento) {
		 	return aprender(idTalento, false);
		}
		
		private void adicionarTalento (Talento talento) {
			jogador.SendMessage("Você acaba de aprender o talento {0}", talento.Nome);
			getTalentoModule().Talentos.Add(talento.ID, talento.ID);
		}
		
		 /**
		  * Verifica se o jogador já possui o talento
		  */ 
		public bool possuiTalento (IDTalento idTalento) {
            return getTalentoModule().Talentos.ContainsKey(idTalento);
		}
	}
}
