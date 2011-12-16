using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;

namespace Kaltar.Talentos {

    public sealed class ConjuracaoForte : HabilidadeTalento
    {
		
		private static ConjuracaoForte instance = new ConjuracaoForte();
		public static ConjuracaoForte Instance {
			get {return instance;}
		}		
		
		private ConjuracaoForte() {
			id = (int)IdHabilidadeTalento.conjuracaoForte;
			nome = "Conjuração Resistênte";
            descricao = "Suas conjurações são mais fortes do que as demais. Conferindo a elas maior força e destreza. <br/> Aumenta a força e destreza das conjurações em 5 para cada ponto.";
            preRequisito = "Classe: Aprendiz ou Espiritualista";
            nivelMaximo = 2;
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