using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;
using Kaltar.Util;

namespace Kaltar.Talentos {

    public sealed class PoderDaFe : HabilidadeTalento
    {
		
		private static PoderDaFe instance = new PoderDaFe();
		public static PoderDaFe Instance {
			get {return instance;}
		}		
		
		private PoderDaFe() {
			id = (int)IdHabilidadeTalento.poderDaFe;
			nome = "Poder Da Fé";
            descricao = "Sua fé é absoluta, concedendo ao mortos-vivos nenhuma chance contra seus ataques. <br/> Bônus no dano contra morto-vivos igual a valor da perícia Divino/10.";
            preRequisito = "Classe: Espiritualista";
            nivelMaximo = 1;
		}
		
        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            //Tiago, colocar o requisito do talento emboscada
            if (jogador.getSistemaClasse().getClasse() is Seminarista)
            {
                return true;
            }

            return false;
		}

        public override int danoBonus(HabilidadeNode node, Jogador atacante, Mobile defensor)
        {
            int bonus = 0;
            
            if(isMortoVivo(defensor)) {

                bonus = (int) (atacante.Skills.SpiritSpeak.Value / 10);

                atacante.SendMessage("Bonus dano Poder da Fé: {0}", bonus);

                //mensagem para atacante e defensor
                atacante.PublicOverheadMessage(Server.Network.MessageType.Regular, 0, false, "Poder da Fé!");
                defensor.PrivateOverheadMessage(Server.Network.MessageType.Regular, 0, false, "Você recebeu o Poder da Fé!", defensor.NetState);
            }

            return bonus;
        }

        /**
         * Completar com os monstros que são mort-vivo.
         */ 
        private bool isMortoVivo(Mobile defensor)
        {
            return defensor is Zombie || defensor is Lich || defensor is Skeleton;
        }

	}
}
