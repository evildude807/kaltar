using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;

namespace Kaltar.Raca {
	
	public sealed class StatusFolego : HabilidadeRacial {
		
		private static StatusFolego instance = new StatusFolego();
		public static StatusFolego Instance {
			get {return instance;}
		}

        private StatusFolego()
        {
            id = (int)IdHabilidadeRacial.mana;
            nome = "Folego";
            descricao = "Você possui mais folego.";
            preRequisito = "Raça Elfo";
            nivelMaximo = 2;
		}

        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            return jogador.getSistemaRaca().getRaca() is Elfo;
		}

        private int bonus(int nivel)
        {
            if (nivel == 1)
            {
                return 10;
            }
            else if (nivel == 2)
            {
                return 25;
            }

            return 0;
        }

        /*
         * Bonus que a habilidade da para a mana.
         */
        public override int folegoBonus(HabilidadeNode node)
        {
            return bonus(node.Nivel);
        }            

        public override void aplicar(Jogador jogador, HabilidadeNode node, bool primeiraVez)
        {
            int ponto = primeiraVez ? node.Nivel : node.Nivel - 1;

            jogador.Stam += bonus(ponto);

        }
	}
}
