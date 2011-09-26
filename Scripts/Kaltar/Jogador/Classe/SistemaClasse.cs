/*
 * Autor: Tiago Augusto Data: 26/2/2005 
 * Projeto: Kaltar
 */
 
using System;
using Server;
using Server.Mobiles;
using System.Collections.Generic;
using Server.ACC.CM;

namespace Kaltar.Classes
{
	/// <summary>
	/// Description of SistemaClasse.
	/// </summary>
    public class SistemaClasse {

        #region atributos

        //jogador dono dos talentos
        private Jogador jogador = null;

        #endregion

        public SistemaClasse(Jogador jogador)
        {
            this.jogador = jogador;
        }

        /**
         * Recupera o modulo de talento
         */
        private ClasseModule getClasseModule()
        {
            ClasseModule tm = (ClasseModule)CentralMemory.GetModule(jogador.Serial, typeof(ClasseModule));
            return tm;
        }

        public void adicionarClasse(classe idClasse)
        {
            Classe classe = getClasse(idClasse);
            
            getClasseModule().IdClasse = idClasse;
            classe.adicionarClasse(jogador);
        }
        
        private Classe getClasse(classe idClasse)
        {
            if (idClasse == classe.Aldeao)
            {
                return Aldeao.getInstacia();
            }
            else if (idClasse == classe.Escudeiro)
            {
                return Escudeiro.getInstacia();
            }
            else if (idClasse == classe.Aprendiz)
            {
                return Aprendiz.getInstacia();
            }
            else if (idClasse == classe.Seminarista)
            {
                return Seminarista.getInstacia();
            }
            else if (idClasse == classe.Gatuno)
            {
                return Gatuno.getInstacia();
            }
            else
            {
                Console.WriteLine("ERROR: personagem {0} sem classe definida", jogador.Name);
                return Aldeao.getInstacia();
            }
        }

        /**
         * Recupera a classe do jogador.
         */ 
        public Classe getClasse()
        {
            classe idClasse = getClasseModule().IdClasse;
            return getClasse(idClasse);
        }
    }

}
