using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;

namespace Kaltar.Talentos {

    public sealed class RevitalizacaoDeCombate : HabilidadeTalento
    {
		
		private static RevitalizacaoDeCombate instance = new RevitalizacaoDeCombate();
		public static RevitalizacaoDeCombate Instance {
			get {return instance;}
		}		
		
		private RevitalizacaoDeCombate() {
			id = (int)IdHabilidadeTalento.revitalizacaoDeCombate;
            nome = "Revitalização de Combate";
            descricao = "Você se sente tão bem quando acerta um ataque crítico que é capaz de recuperar seus ferimentos leves. <br/> Você recupera 50%, para cada ponto de habilidade, do dano causado em seus pontos de vida.";
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
            
            atacante.Heal(dano, atacante, true);

            atacante.PublicOverheadMessage(Server.Network.MessageType.Regular, 0, false, "Revitalização de Combate!!!");
        }
	}
}
