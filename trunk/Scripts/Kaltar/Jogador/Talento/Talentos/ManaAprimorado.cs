using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;

namespace Kaltar.Talentos {

    public sealed class ManaAprimorado : HabilidadeTalento
    {
		
		private static ManaAprimorado instance = new ManaAprimorado();
		public static ManaAprimorado Instance {
			get {return instance;}
		}		
		
		private ManaAprimorado() {
			id = (int)IdHabilidadeTalento.manaAprimorado;
			nome = "Mana Aprimorado";
            descricao = "Você regenera a sua mana mais rapidamente do que os outros. <br/> Bônus no tempo para regeneração passiva de mana.";
            preRequisito = "Inteligencia maior que 50.";
            nivelMaximo = 1;
		}
		
        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            if (jogador.RawInt >= 50)
            {
                return true;
            }

            return false;
		}
	}
}
