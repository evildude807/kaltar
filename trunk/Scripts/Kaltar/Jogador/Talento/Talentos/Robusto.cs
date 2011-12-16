using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;

namespace Kaltar.Talentos {

    public sealed class Robusto : HabilidadeTalento
    {
		
		private static Robusto instance = new Robusto();
		public static Robusto Instance {
			get {return instance;}
		}		
		
		private Robusto() {
			id = (int)IdHabilidadeTalento.robusto;
			nome = "Robusto";
            descricao = "Sim, você é forte. <br/> Bônus na força. 5 para cada ponto.";
            preRequisito = "Não tem.";
            nivelMaximo = 2;
		}
		
        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            return true;
		}

        public override int forcaBonus(HabilidadeNode node)
        {
            return node.Nivel * 5;
        }

        public override void aplicar(Jogador jogador, HabilidadeNode node, bool primeiraVez)
        {
            int ponto = primeiraVez ? node.Nivel : node.Nivel - 1;

            jogador.RawStr += ponto * 5;

        }                
	}
}
