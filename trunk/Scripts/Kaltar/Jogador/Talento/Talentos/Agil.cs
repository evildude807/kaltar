using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;

namespace Kaltar.Talentos {

    public sealed class Agil : HabilidadeTalento
    {
		
		private static Agil instance = new Agil();
		public static Agil Instance {
			get {return instance;}
		}		
		
		private Agil() {
			id = (int)IdHabilidadeTalento.agil;
			nome = "Agil";
            descricao = "Sua agilidade é tremenda, poucos conseguem acompanhar seus movimentos. <br/> Bônus na destreza. 5 para cada ponto.";
            preRequisito = "Não tem.";
            nivelMaximo = 2;
		}
		
        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            return true;
		}

        public override int destrezaBonus(HabilidadeNode node)
        {
            return node.Nivel * 5;
        }

        public override void aplicar(Jogador jogador, HabilidadeNode node, bool primeiraVez)
        {
            int ponto = primeiraVez ? node.Nivel : node.Nivel - 1;

            jogador.RawDex += ponto * 5;

        }                
	}
}
