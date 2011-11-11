using System;
using Server;
using Server.Mobiles;

namespace Kaltar.Talentos {

    public sealed class Alerta : HabilidadeTalento
    {
		
		private static Alerta instance = new Alerta();
		public static Alerta Instance {
			get {return instance;}
		}		
		
		private Alerta() {
			id = (int)IdHabilidadeTalento.alerta;
			nome = "Alerta";
			descricao = "O seu estado de alerta é aprimorado o que lhe permite seila oque.";
			preRequisito = null;
            nivelMaximo = 1;
		}
		
        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            return false;
		}
	}
}
