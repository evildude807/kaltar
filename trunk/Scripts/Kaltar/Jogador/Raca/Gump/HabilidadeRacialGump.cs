using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;
using System.Collections.Generic;
using Kaltar.Habilidades;

namespace Kaltar.Raca {

    public class HabilidadeRacialGump : HabilidadeGump {

        public static void Initialize()
        {
            CommandSystem.Register("habilidaderacial", AccessLevel.Player, new CommandEventHandler(HabilidadeRacialsJogador));
        }

        private static void HabilidadeRacialsJogador(CommandEventArgs e)
        {
            Jogador jogador = (Jogador)e.Mobile;
            jogador.SendGump(new HabilidadeRacialGump(jogador));
        }

		public HabilidadeRacialGump(Jogador jogador): base(jogador) {
		}

        /**
         * Recupera a habilidade por seu id.
         */
        public override Habilidade getHabilidade(int idHabilidade)
        {
            return HabilidadeRacial.getHabilidadeRacial((IdHabilidadeRacial)idHabilidade);
        }

        /**
         * Recupera todas as habilidades do jogador.
         */
        public override List<int> getHabilidades(Jogador jogador)
        {
            List<int> habilidades = new List<int>();
            foreach(HabilidadeNode node in jogador.getSistemaRaca().getHabilidades().Values) {
                habilidades.Add(node.Id);
            }

            return habilidades;
        }

        /**
         * Recupera o número de pontos de habilidade.
         */
        public override string totalPontosHabilidade(Jogador jogador)
        {
            return jogador.getSistemaRaca().pontosDisponiveis() + "";
        }

        /**
         * Recupera o título do gump.
         */
        public override string getTitulo()
        {
            return "Habilidades raciais";
        }	
	}
}
