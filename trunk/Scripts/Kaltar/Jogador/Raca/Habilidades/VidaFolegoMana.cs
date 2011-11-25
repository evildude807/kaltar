using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;

namespace Kaltar.Raca {
	
	public sealed class VidaFolegoMana : HabilidadeRacial {
		
		private static VidaFolegoMana instance = new VidaFolegoMana();
		public static VidaFolegoMana Instance {
			get {return instance;}
		}

        private VidaFolegoMana()
        {
            id = (int)IdHabilidadeRacial.vidaFolegoMana;
            nome = "Pontos de vida, folego e mana";
            descricao = "Você possui mais pontos de vida, folego e mana.";
            preRequisito = "Raça Humano";
            nivelMaximo = 3;
		}

        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            return jogador.getSistemaRaca().getRaca() is Humano;
		}

        /*
         * Bonus que a habilidade da para a vida.
         */
        public override int vidaBonus(HabilidadeNode node)
        {
            return node.Nivel * 2;
        }

        /*
         * Bonus que a habilidade da para a folego.
         */
        public override int folegoBonus(HabilidadeNode node)
        {
            return node.Nivel * 2;
        }

        /*
         * Bonus que a habilidade da para a mana.
         */
        public override int manaBonus(HabilidadeNode node)
        {
            return node.Nivel * 2;
        }

        public override void aplicar(Jogador jogador, HabilidadeNode node, bool primeiraVez)
        {
            jogador.Mana += node.Nivel;
            jogador.Hits += node.Nivel;
            jogador.Stam += node.Nivel;

            if (jogador.Mana > jogador.ManaMax)
            {
                jogador.Mana = jogador.ManaMax;
            }
            if (jogador.Hits > jogador.HitsMax)
            {
                jogador.Hits = jogador.HitsMax;
            }
            if (jogador.Stam > jogador.StamMax)
            {
                jogador.Stam = jogador.StamMax;
            }
        }
	}
}
