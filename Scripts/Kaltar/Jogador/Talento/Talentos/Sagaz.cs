using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;

namespace Kaltar.Talentos {

    public sealed class Sagaz : HabilidadeTalento
    {
		
		private static Sagaz instance = new Sagaz();
		public static Sagaz Instance {
			get {return instance;}
		}		
		
		private Sagaz() {
			id = (int)IdHabilidadeTalento.sagaz;
			nome = "Sagaz";
            descricao = "Sua aptidão para apreender ou compreender as coisas é surpreendente. <br/> Bônus na Inteligência. 5 para cada ponto.";
            preRequisito = "Não tem.";
            nivelMaximo = 2;
		}
		
        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            return true;
		}

        public override int inteligenciaBonus(HabilidadeNode node)
        {
            return node.Nivel * 5;
        }

        public override void aplicar(Jogador jogador, HabilidadeNode node, bool primeiraVez)
        {
            int ponto = primeiraVez ? node.Nivel : node.Nivel - 1;

            jogador.RawInt += ponto * 5;

        }                
	}
}
