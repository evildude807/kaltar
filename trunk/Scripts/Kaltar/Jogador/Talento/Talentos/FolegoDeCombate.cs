using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;

namespace Kaltar.Talentos {

    public sealed class FolegoDeCombate : HabilidadeTalento
    {
		
		private static FolegoDeCombate instance = new FolegoDeCombate();
		public static FolegoDeCombate Instance {
			get {return instance;}
		}		
		
		private FolegoDeCombate() {
			id = (int)IdHabilidadeTalento.folegoDeCombate;
            nome = "Folego de Combate";
            descricao = "Você se sente tão bem quando acerta um ataque crítico que é capaz de recuperar seu parte do seu folego. <br/> Você recupera 50%, para cada ponto de habilidade, do dano causado em seu folego.";
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

            if (atacante.Stam + cura > atacante.StamMax)
            {
                cura = atacante.StamMax;
            }

            atacante.Stam += cura;

            atacante.PublicOverheadMessage(Server.Network.MessageType.Regular, 0, false, "Folego de Combate!!!");
        }
	}
}
