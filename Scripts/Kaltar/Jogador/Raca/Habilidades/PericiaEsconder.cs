using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;

namespace Kaltar.Raca {
	
	public sealed class PericiaEsconder : HabilidadeRacial {
		
		private static PericiaEsconder instance = new PericiaEsconder();
		public static PericiaEsconder Instance {
			get {return instance;}
		}

        private PericiaEsconder()
        {
            id = (int)IdHabilidadeRacial.esconder;
            nome = "Perícia esconder";
            descricao = "Você possui habilidade natural para a perícia esconder.";
            preRequisito = "Raça Elfo ou Raça Elfo Negro";
            nivelMaximo = 2;
		}

        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            return jogador.getSistemaRaca().getRaca() is Elfo || jogador.getSistemaRaca().getRaca() is ElfoNegro;
		}

        /*
         * Bonus que a habilidade da na skill.
         */
        public override double skillBonus(HabilidadeNode node, SkillName skillName)
        {
            if (SkillName.Hiding.Equals(skillName))
            {
                return node.Nivel * 10;
            }
            return 0;
        }

        /**
         * Bonus que a habilidade concede ao cap de skill. Normalmente toda habilidade que da bonus em um determinada skill, deve dar o mesmo bonus do Cap.
         * 
         */
        public override int skillsCapBonus(HabilidadeNode node)
        {
            return node.Nivel * 10;
        }

        public override void aplicar(Jogador jogador, HabilidadeNode node, bool primeiraVez)
        {
            int ponto = primeiraVez ? node.Nivel : node.Nivel - 1;

            jogador.Skills.Hiding.Base += (ponto * 10);
        }
	}
}
