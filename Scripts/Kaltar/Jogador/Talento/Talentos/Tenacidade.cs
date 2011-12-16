using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;

namespace Kaltar.Talentos {

    public sealed class Tenacidade : HabilidadeTalento
    {
		
		private static Tenacidade instance = new Tenacidade();
		public static Tenacidade Instance {
			get {return instance;}
		}		
		
		private Tenacidade() {
			id = (int)IdHabilidadeTalento.tenacidade;
			nome = "Tenacidade";
            descricao = "Você consegue resistir por muito mais tempo qualquer tipo de dano. <br/> Bônus de 10 pontos de vida para cada nível.";
            preRequisito = "Não tem.";
            nivelMaximo = 3;
		}
		
        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            return true;
		}

        public override int vidaBonus(HabilidadeNode node)
        {
            return node.Nivel * 10;
        }

        public override void aplicar(Jogador jogador, HabilidadeNode node, bool primeiraVez)
        {
            jogador.Hits += (node.Nivel * 10);
        }
	}
}
