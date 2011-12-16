using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;

namespace Kaltar.Talentos {

    public sealed class AptidaoMagica : HabilidadeTalento
    {
		
		private static AptidaoMagica instance = new AptidaoMagica();
		public static AptidaoMagica Instance {
			get {return instance;}
		}		
		
		private AptidaoMagica() {
			id = (int)IdHabilidadeTalento.aptiddaoMagica;
			nome = "Aptidão Mágica";
            descricao = "Você mantem uma sintonia com o Mana, conseguindo armazená-lo melhor do que as outras pessoas.. <br/> Bônus de 10 pontos de vida para cada nível.";
            preRequisito = "Não tem.";
            nivelMaximo = 3;
		}
		
        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            return true;
		}

        public override int manaBonus(HabilidadeNode node)
        {
            return node.Nivel * 10;
        }

        public override void aplicar(Jogador jogador, HabilidadeNode node, bool primeiraVez)
        {
            jogador.Mana += (node.Nivel * 10);
        }
	}
}
