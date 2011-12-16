using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;

namespace Kaltar.Talentos {

    public sealed class AgilidadeComArma : HabilidadeTalento
    {
		
		private static AgilidadeComArma instance = new AgilidadeComArma();
		public static AgilidadeComArma Instance {
			get {return instance;}
		}		
		
		private AgilidadeComArma() {
			id = (int)IdHabilidadeTalento.agilidadeComArma;
			nome = "Agilidade com Arma";
            descricao = "Você é treinado em utilizar armas leves, utilizando sua agilidade e destreza no lugar da força bruta. <br/> Utiliza o atributo destreza para calcula o bônus de dano com armas leves.";
            preRequisito = "Classe: Homem de arma ou Ladino, Destreza maior que 60.";
            nivelMaximo = 1;
		}
		
        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            if ((jogador.getSistemaClasse().getClasse() is Escudeiro || jogador.getSistemaClasse().getClasse() is Gatuno) && jogador.RawDex >= 60)
            {
                return true;
            }

            return false;
		}        
	}
}
