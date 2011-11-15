using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;
using System.Collections.Generic;
using Kaltar.Habilidades;

namespace Kaltar.Talentos {

    public class HabilidadeTalentoGump : HabilidadeGump {

        public static void Initialize()
        {
            CommandSystem.Register("habilidadeTalento", AccessLevel.Player, new CommandEventHandler(HabilidadeTalentosJogador));
        }

        private static void HabilidadeTalentosJogador(CommandEventArgs e)
        {
            Jogador jogador = (Jogador)e.Mobile;
            jogador.SendGump(new HabilidadeTalentoGump(jogador));
        }

		public HabilidadeTalentoGump(Jogador jogador): base(jogador) {
		}

        /**
         * Recupera a habilidade por seu id.
         */
        public override Habilidade getHabilidade(int idHabilidade)
        {
            return HabilidadeTalento.getHabilidadeTalento((IdHabilidadeTalento)idHabilidade);
        }

        /**
         * Recupera todas as habilidades do jogador.
         */
        public override List<HabilidadeNode> getHabilidades(Jogador jogador)
        {
            List<HabilidadeNode> habilidades = new List<HabilidadeNode>();
            foreach(HabilidadeNode node in jogador.getSistemaTalento().getHabilidades().Values) {
                habilidades.Add(node);
            }

            return habilidades;
        }

        /**
         * Recupera o número de pontos de habilidade.
         */
        public override string totalPontosHabilidade(Jogador jogador)
        {
            return jogador.getSistemaTalento().pontosDisponiveis() + "";
        }

        /**
         * Recupera o título do gump.
         */
        public override string getTitulo()
        {
            return "Talentos";
        }	
	}
}
