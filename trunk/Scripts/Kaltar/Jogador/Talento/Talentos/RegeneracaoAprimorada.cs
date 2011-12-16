using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;

namespace Kaltar.Talentos {

    public sealed class RegeneracaoAprimorada : HabilidadeTalento
    {
		
		private static RegeneracaoAprimorada instance = new RegeneracaoAprimorada();
		public static RegeneracaoAprimorada Instance {
			get {return instance;}
		}		
		
		private RegeneracaoAprimorada() {
			id = (int)IdHabilidadeTalento.regeneracaoAprimorada;
			nome = "Regeneração Aprimorada";
            descricao = "Você regenera dos ferimentos mais rapidamente do que os outros. <br/> Bônus no tempo para regeneração passiva de pontos de vida.";
            preRequisito = "Força maior que 50.";
            nivelMaximo = 1;
		}
		
        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            if (jogador.RawStr >= 50)
            {
                return true;
            }

            return false;
		}
	}
}
