using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;

namespace Kaltar.Talentos {

    public sealed class FolegoAprimorado : HabilidadeTalento
    {
		
		private static FolegoAprimorado instance = new FolegoAprimorado();
		public static FolegoAprimorado Instance {
			get {return instance;}
		}		
		
		private FolegoAprimorado() {
			id = (int)IdHabilidadeTalento.folegoAprimorado;
			nome = "Folego Aprimorado";
            descricao = "Você regenera o seu cansaço mais rapidamente do que os outros. <br/> Bônus no tempo para regeneração passiva de folego.";
            preRequisito = "Destreza maior que 50.";
            nivelMaximo = 1;
		}
		
        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            if (jogador.RawDex >= 50)
            {
                return true;
            }

            return false;
		}
	}
}
