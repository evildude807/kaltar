using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;

namespace Kaltar.Talentos {

    public abstract class EspecializacaoEmArma : HabilidadeTalento
    {
		protected EspecializacaoEmArma() {
			id = (int)ID;
            nome = "Especialização em Arma (" + NomeGrupo + ")";
            descricao = "Você é treinado com um grupo de arma " + NomeGrupo + ", conseguindo deferir golpes mais poderosos. <br/> Bônus de 5 pontos de dano para cada nível.";
            preRequisito = "Classe: Homem de arma.";
            nivelMaximo = 2;
		}

        public abstract IdHabilidadeTalento ID { get; }

        public abstract string NomeGrupo { get; }

        public abstract SkillName TipoGrupo { get; }

        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            if (jogador.getSistemaClasse().getClasse() is Escudeiro)
            {
                return true;
            }

            return false;
		}

        public override int danoBonus(HabilidadeNode node, Jogador atacante, Mobile defensor)
        {
            BaseWeapon arma = atacante.Weapon as BaseWeapon;
            if (arma.DefSkill.Equals(TipoGrupo))
            {
                return node.Nivel * 5;
            }

            return 0;
        }
        
    }

    public class EspecializacaoEmArmaEspada : EspecializacaoEmArma {

        private EspecializacaoEmArmaEspada() {}

        private static EspecializacaoEmArmaEspada instance = new EspecializacaoEmArmaEspada();
        public static EspecializacaoEmArmaEspada Instance  { get { return instance; } }		

        public override IdHabilidadeTalento ID {
            get { return IdHabilidadeTalento.especializacaoArmaEspada; }
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

    public class EspecializacaoEmArmaMachado : EspecializacaoEmArma {

        private EspecializacaoEmArmaMachado() { }

        private static EspecializacaoEmArmaMachado instance = new EspecializacaoEmArmaMachado();
        public static EspecializacaoEmArmaMachado Instance { get { return instance; } }

        public override IdHabilidadeTalento ID
        {
            get { return IdHabilidadeTalento.especializacaoArmaMachado; }
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

    public class EspecializacaoEmArmaMaca : EspecializacaoEmArma {

        private EspecializacaoEmArmaMaca() { }

        private static EspecializacaoEmArmaMaca instance = new EspecializacaoEmArmaMaca();
        public static EspecializacaoEmArmaMaca Instance { get { return instance; } }

        public override IdHabilidadeTalento ID
        {
            get { return IdHabilidadeTalento.especializacaoArmaMaca; }
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

    public class EspecializacaoEmArmaEsgrima : EspecializacaoEmArma {

        private EspecializacaoEmArmaEsgrima() { }

        private static EspecializacaoEmArmaEsgrima instance = new EspecializacaoEmArmaEsgrima();
        public static EspecializacaoEmArmaEsgrima Instance { get { return instance; } }

        public override IdHabilidadeTalento ID
        {
            get { return IdHabilidadeTalento.especializacaoArmaEsgrima; }
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

    public class EspecializacaoEmArmaArco : EspecializacaoEmArma {

        private EspecializacaoEmArmaArco() { }

        private static EspecializacaoEmArmaArco instance = new EspecializacaoEmArmaArco();
        public static EspecializacaoEmArmaArco Instance { get { return instance; } }

        public override IdHabilidadeTalento ID
        {
            get { return IdHabilidadeTalento.especializacaoArmaArco; }
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

    public class EspecializacaoEmArmaPunho : EspecializacaoEmArma {

        private EspecializacaoEmArmaPunho() { }

        private static EspecializacaoEmArmaPunho instance = new EspecializacaoEmArmaPunho();
        public static EspecializacaoEmArmaPunho Instance { get { return instance; } }

        public override IdHabilidadeTalento ID
        {
            get { return IdHabilidadeTalento.especializacaoArmaPunho; }
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
