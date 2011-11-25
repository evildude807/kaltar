using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;

namespace Kaltar.Raca {
	
	public sealed class PericiaArquearia : HabilidadeRacial {
		
		private static PericiaArquearia instance = new PericiaArquearia();
		public static PericiaArquearia Instance {
			get {return instance;}
		}

        private PericiaArquearia()
        {
            id = (int)IdHabilidadeRacial.longaDistancia;
            nome = "Perícia arquearia";
            descricao = "Você possui habilidade natural para a perícia arquearia.";
            preRequisito = "Raça Elfo";
            nivelMaximo = 2;
		}

        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            return jogador.getSistemaRaca().getRaca() is Elfo;
		}

        /*
         * Bonus que a habilidade da na skill.
         */
        public override double skillBonus(HabilidadeNode node, SkillName skillName)
        {
            if (SkillName.Archery.Equals(skillName))
            {
                return node.Nivel * 5;
            }
            return 0;
        }

        /**
         * Bonus que a habilidade concede ao cap de skill. Normalmente toda habilidade que da bonus em um determinada skill, deve dar o mesmo bonus do Cap.
         * 
         */
        public override int skillsCapBonus(HabilidadeNode node)
        {
            return node.Nivel * 5;
        }

        public override void aplicar(Jogador jogador, HabilidadeNode node, bool primeiraVez)
        {
            int ponto = primeiraVez ? node.Nivel : node.Nivel - 1;

            jogador.Skills.Archery.Base += (ponto * 5);
        }
	}
}
