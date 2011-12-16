using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;

namespace Kaltar.Talentos {

    public sealed class ContraAtaque : HabilidadeTalento
    {
		
		private static ContraAtaque instance = new ContraAtaque();
		public static ContraAtaque Instance {
			get {return instance;}
		}		
		
		private ContraAtaque() {
			id = (int)IdHabilidadeTalento.contraAtaque;
			nome = "Contra Ataque";
            descricao = "Quando você apara um ataque é capaz de rapidamente reagir e atacar seu inimigo de surpresa. <br/> Existe uma chance de 50% de realizar um contra ataque quando um ataque é aparado.";
            preRequisito = "Classe: Homem de arma, Talento: Escudo Protetor";
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
        public override void onAparar(Mobile attacker, Jogador defensor, int dano)
        {

            BaseWeapon weapon = defensor.Weapon as BaseWeapon;

            if (weapon != null)
            {
                defensor.FixedParticles(0x3779, 1, 15, 0x158B, 0x0, 0x3, EffectLayer.Waist);
                weapon.OnSwing(defensor, attacker);
            }

            defensor.PublicOverheadMessage(Server.Network.MessageType.Regular, 0, false, "Contra Ataque!!!");
        }
	}
}
