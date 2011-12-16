using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;

namespace Kaltar.Talentos {

    public sealed class ManaDeCombate : HabilidadeTalento
    {
		
		private static ManaDeCombate instance = new ManaDeCombate();
		public static ManaDeCombate Instance {
			get {return instance;}
		}		
		
		private ManaDeCombate() {
			id = (int)IdHabilidadeTalento.manaDeCombate;
            nome = "Mana de Combate";
            descricao = "Você se sente tão bem quando acerta um ataque crítico que é capaz de recuperar parte da Mana. <br/> Você recupera 50%, para cada ponto de habilidade, do dano causado em sua Mana.";
            preRequisito = "Não tem.";
            nivelMaximo = 1;
		}
		
        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            return true;
		}

        /**
         * Ocorre quando um ataque e aparado.
         * dano = Dano recebido se nao fosse aparado.
         */
        public override void onAtaqueCritico(Jogador atacante, Mobile defensor, int dano)
        {
            int cura = (int) (dano * 0.5);

            if (atacante.Mana + cura > atacante.ManaMax)
            {
                cura = atacante.ManaMax;
            }

            atacante.Mana += cura;

            atacante.PublicOverheadMessage(Server.Network.MessageType.Regular, 0, false, "Mana de Combate!!!");
        }
	}
}
