using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;

namespace Kaltar.Talentos {

    public sealed class Bloquear : HabilidadeTalento
    {
		
		private static Bloquear instance = new Bloquear();
		public static Bloquear Instance {
			get {return instance;}
		}		
		
		private Bloquear() {
			id = (int)IdHabilidadeTalento.bloquear;
			nome = "Bloquear";
            descricao = "Você adiquiriu um nível de habilidade para se defender com o escudo impressionante, tornado quase impossível ser atingido. <br/> Bônus de 5% para cada nível no teste de aparar com escudo.";
            preRequisito = "Classe: Homem de arma, Utilizar algum escudo e perícia aparar maior que 40.";
            nivelMaximo = 2;
		}
		
        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            if (jogador.getSistemaClasse().getClasse() is Escudeiro && jogador.Skills[SkillName.Parry].Value >= 40)
            {
                return true;
            }

            return false;
		}

        //Bônus de 5% para cada nível no teste de aparar com escudo.
        public override double apararBonus(HabilidadeNode node, Item item)
        {
            if (item is BaseShield)
            {
                return node.Nivel * 0.05;
            }

            return 0;
        }

        
	}
}
