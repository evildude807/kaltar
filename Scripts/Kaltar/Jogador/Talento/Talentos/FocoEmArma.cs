using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;

namespace Kaltar.Talentos {

    public abstract class FocoEmArma : HabilidadeTalento
    {
		protected FocoEmArma() {
			id = (int)ID;
			nome = "Foco em Arma (" + NomeGrupo + ")";
            descricao = "Você é treinado para combater com o grupo de arma " + NomeGrupo + ", conseguindo assim acertar mais facilmente seus adversários. <br/> Bônus de 5 ponto na perícia de combate para cada nível.";
            preRequisito = "Classe: Homem de arma, Espiritualista e Ladino.";
            nivelMaximo = 2;
		}

        public abstract IdHabilidadeTalento ID { get; }

        public abstract string NomeGrupo { get; }

        public abstract SkillName TipoGrupo { get; }

        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            if (jogador.getSistemaClasse().getClasse() is Escudeiro || jogador.getSistemaClasse().getClasse() is Seminarista || jogador.getSistemaClasse().getClasse() is Gatuno)
            {
                return true;
            }

            return false;
		}

        public override int acertarBonus(HabilidadeNode node, Jogador jogador, Mobile defensor)
        {
 	        BaseWeapon arma = jogador.Weapon as BaseWeapon;
            if(arma.DefSkill.Equals(TipoGrupo)) {
                return node.Nivel * 5;
            }

            return 0;
        }

    }

    /*
     *         focoArmaEspada,
        focoArmaMaca,
        focoArmaMachado,
        focoArmaArco,
        focoArmaPunho,
        focoArmaEscrima
     */

    public class FocoEmArmaEspada : FocoEmArma {

        private FocoEmArmaEspada() {}

        private static FocoEmArmaEspada instance = new FocoEmArmaEspada();
        public static FocoEmArmaEspada Instance  { get { return instance; } }		

        public override IdHabilidadeTalento ID {
            get { return IdHabilidadeTalento.focoArmaEspada; }
        }
        public override string NomeGrupo
        {
            get { return "Espada"; }
        }
        public override SkillName TipoGrupo 
        {
            get { return SkillName.Swords; } 
        }
    }

    public class FocoEmArmaMachado : FocoEmArma {

        private FocoEmArmaMachado() { }

        private static FocoEmArmaMachado instance = new FocoEmArmaMachado();
        public static FocoEmArmaMachado Instance { get { return instance; } }

        public override IdHabilidadeTalento ID
        {
            get { return IdHabilidadeTalento.focoArmaMachado; }
        }
        public override string NomeGrupo
        {
            get { return "Machado"; }
        }
        public override SkillName TipoGrupo
        {
            get { return SkillName.TasteID; }
        }
    }

    public class FocoEmArmaMaca : FocoEmArma {

        private FocoEmArmaMaca() { }

        private static FocoEmArmaMaca instance = new FocoEmArmaMaca();
        public static FocoEmArmaMaca Instance { get { return instance; } }

        public override IdHabilidadeTalento ID
        {
            get { return IdHabilidadeTalento.focoArmaMaca; }
        }
        public override string NomeGrupo
        {
            get { return "Maça"; }
        }
        public override SkillName TipoGrupo
        {
            get { return SkillName.Macing; }
        }
    }

    public class FocoEmArmaEsgrima : FocoEmArma {

        private FocoEmArmaEsgrima() { }

        private static FocoEmArmaEsgrima instance = new FocoEmArmaEsgrima();
        public static FocoEmArmaEsgrima Instance { get { return instance; } }

        public override IdHabilidadeTalento ID
        {
            get { return IdHabilidadeTalento.focoArmaEsgrima; }
        }
        public override string NomeGrupo
        {
            get { return "Esgrima"; }
        }
        public override SkillName TipoGrupo
        {
            get { return SkillName.Fencing; }
        }
    }

    public class FocoEmArmaArco : FocoEmArma {

        private FocoEmArmaArco() { }

        private static FocoEmArmaArco instance = new FocoEmArmaArco();
        public static FocoEmArmaArco Instance { get { return instance; } }

        public override IdHabilidadeTalento ID
        {
            get { return IdHabilidadeTalento.focoArmaArco; }
        }
        public override string NomeGrupo
        {
            get { return "Arquearia"; }
        }
        public override SkillName TipoGrupo
        {
            get { return SkillName.Archery; }
        }
    }

    public class FocoEmArmaPunho : FocoEmArma {

        private FocoEmArmaPunho() { }

        private static FocoEmArmaPunho instance = new FocoEmArmaPunho();
        public static FocoEmArmaPunho Instance { get { return instance; } }

        public override IdHabilidadeTalento ID
        {
            get { return IdHabilidadeTalento.focoArmaPunho; }
        }
        public override string NomeGrupo
        {
            get { return "Punho"; }
        }
        public override SkillName TipoGrupo
        {
            get { return SkillName.Wrestling; }
        }
    }
}
