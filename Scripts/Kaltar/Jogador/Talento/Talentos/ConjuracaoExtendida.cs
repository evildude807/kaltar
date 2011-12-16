using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;

namespace Kaltar.Talentos {

    public sealed class ConjuracaoExtendida : HabilidadeTalento
    {
		
		private static ConjuracaoExtendida instance = new ConjuracaoExtendida();
		public static ConjuracaoExtendida Instance {
			get {return instance;}
		}		
		
		private ConjuracaoExtendida() {
			id = (int)IdHabilidadeTalento.conjuracaoExtendida;
			nome = "Conjuração Extendida";
            descricao = "Suas conjurações são tem o seu tempo de duração aumentado. <br/> Aumenta em 50% a duração das conjurações.";
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
