using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;

namespace Kaltar.Raca {
	
	public sealed class StatusMana : HabilidadeRacial {
		
		private static StatusMana instance = new StatusMana();
		public static StatusMana Instance {
			get {return instance;}
		}

        private StatusMana()
        {
            id = (int)IdHabilidadeRacial.mana;
            nome = "Mana";
            descricao = "Você possui mais Mana.";
            preRequisito = "Raça Elfo Negro";
            nivelMaximo = 2;
		}

        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            return jogador.getSistemaRaca().getRaca() is ElfoNegro;
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
        public override int manaBonus(HabilidadeNode node)
        {
            return bonus(node.Nivel);
        }            

        public override void aplicar(Jogador jogador, HabilidadeNode node, bool primeiraVez)
        {
            int ponto = primeiraVez ? node.Nivel : node.Nivel - 1;

            jogador.Mana += bonus(ponto);

        }
	}
}
