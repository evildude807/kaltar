using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;

namespace Kaltar.Talentos {

    public sealed class ConjuracaoResistente : HabilidadeTalento
    {
		
		private static ConjuracaoResistente instance = new ConjuracaoResistente();
		public static ConjuracaoResistente Instance {
			get {return instance;}
		}		
		
		private ConjuracaoResistente() {
			id = (int)IdHabilidadeTalento.conjuracaoResistente;
			nome = "Conjuração Resistênte";
            descricao = "Suas conjurações são mais resistentes do que as demais. Conferindo a elas maior resistência. <br/> Aumenta toda as resistência das conjurações em 5 pontos.";
            preRequisito = "Classe: Aprendiz ou Espiritualista";
            nivelMaximo = 1;
		}
		
        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            if (jogador.getSistemaClasse().getClasse() is Aprendiz || jogador.getSistemaClasse().getClasse() is Seminarista)
            {
                return true;
            }

            return false;
		}
	}
}