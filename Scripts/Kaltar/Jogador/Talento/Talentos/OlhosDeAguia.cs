using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;

namespace Kaltar.Talentos {

    public sealed class OlhosDeAguia : HabilidadeTalento
    {
		
		private static OlhosDeAguia instance = new OlhosDeAguia();
		public static OlhosDeAguia Instance {
			get {return instance;}
		}		
		
		private OlhosDeAguia() {
			id = (int)IdHabilidadeTalento.olhosDeAguia;
			nome = "OlhosDeAguia";
            descricao = "Sua habilidade com armas de longa distância que o seu alcance é maior. <br/> Aumenta o alcance das armas de longa distância em 2 tiles.";
            preRequisito = "Classe: Homem de arma ou Ladino, Foco em Arma (Arquearia)";
            nivelMaximo = 1;
		}
		
        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            if (jogador.getSistemaClasse().getClasse() is Escudeiro || jogador.getSistemaClasse().getClasse() is Gatuno)
            {
                if (jogador.getSistemaTalento().possuiHabilidadeTalento(IdHabilidadeTalento.olhosDeAguia))
                {
                    return true;
                }
            }

            return false;
		}
        
	}
}
