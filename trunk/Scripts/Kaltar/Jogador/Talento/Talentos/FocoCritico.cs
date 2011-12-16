using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;

namespace Kaltar.Talentos {

    public abstract class FocoCritico : HabilidadeTalento
    {

        protected FocoCritico()
        {
			id = (int)ID;
			nome = "Foco Crítico";
            descricao = "Você é treinado em combate com um grupo de armas " + NomeGrupo + " e seus ataques críticos são acertados com maior facilidade. <br/> Bônus de 5% na chance de acertar ataques críticos com o grupo de arma escolhido.";
            preRequisito = "Classe: Homem de arma ou Ladino, Foco em Arma.";
            nivelMaximo = 1;
		}
		
        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            if ((jogador.getSistemaClasse().getClasse() is Escudeiro || jogador.getSistemaClasse().getClasse() is Gatuno))
            {
                if (jogador.getSistemaTalento().possuiHabilidadeTalento(TalentoNecessario))
                {
                    return true;
                }
            }

            return false;
		}

        public abstract IdHabilidadeTalento ID { get; }

        public abstract string NomeGrupo { get; }

        public abstract SkillName TipoGrupo { get; }

        public abstract IdHabilidadeTalento TalentoNecessario { get; }

        public override int chanceAtaqueCriticoBonus(HabilidadeNode node, Jogador atacante, Mobile defensor)
        {

            BaseWeapon arma = atacante.Weapon as BaseWeapon;
            if (arma.DefSkill.Equals(TipoGrupo))
            {
                return 5;
            }

            return 0;
        }      
	}

    public class FocoCriticoEspada : FocoCritico {

        private FocoCriticoEspada() { }

        private static FocoCriticoEspada instance = new FocoCriticoEspada();
        public static FocoCriticoEspada Instance { get { return instance; } }

        public override IdHabilidadeTalento ID
        {
            get { return IdHabilidadeTalento.focoCriticoEspada; }
        }
        public override string NomeGrupo
        {
            get { return "Espada"; }
        }
        public override SkillName TipoGrupo
        {
            get { return SkillName.Swords; }
        }

        public override IdHabilidadeTalento TalentoNecessario { get { return IdHabilidadeTalento.focoArmaEspada; } }
    }

    public class FocoCriticoMachado : FocoCritico {

        private FocoCriticoMachado() { }

        private static FocoCriticoMachado instance = new FocoCriticoMachado();
        public static FocoCriticoMachado Instance { get { return instance; } }

        public override IdHabilidadeTalento ID
        {
            get { return IdHabilidadeTalento.focoCriticoMachado; }
        }
        public override string NomeGrupo
        {
            get { return "Machado"; }
        }
        public override SkillName TipoGrupo
        {
            get { return SkillName.TasteID; }
        }

        public override IdHabilidadeTalento TalentoNecessario { get { return IdHabilidadeTalento.focoArmaMachado; } }
    }

    public class FocoCriticoMaca : FocoCritico {

        private FocoCriticoMaca() { }

        private static FocoCriticoMaca instance = new FocoCriticoMaca();
        public static FocoCriticoMaca Instance { get { return instance; } }

        public override IdHabilidadeTalento ID
        {
            get { return IdHabilidadeTalento.focoCriticoMaca; }
        }
        public override string NomeGrupo
        {
            get { return "Maça"; }
        }
        public override SkillName TipoGrupo
        {
            get { return SkillName.Macing; }
        }

        public override IdHabilidadeTalento TalentoNecessario { get { return IdHabilidadeTalento.focoArmaMaca; } }
    }

    public class FocoCriticoEsgrima : FocoCritico {

        private FocoCriticoEsgrima() { }

        private static FocoCriticoEsgrima instance = new FocoCriticoEsgrima();
        public static FocoCriticoEsgrima Instance { get { return instance; } }

        public override IdHabilidadeTalento ID
        {
            get { return IdHabilidadeTalento.focoCriticoEsgrima; }
        }
        public override string NomeGrupo
        {
            get { return "Esgrima"; }
        }
        public override SkillName TipoGrupo
        {
            get { return SkillName.Fencing; }
        }

        public override IdHabilidadeTalento TalentoNecessario { get { return IdHabilidadeTalento.focoArmaEsgrima; } }
    }

    public class FocoCriticoArco : FocoCritico {

        private FocoCriticoArco() { }

        private static FocoCriticoArco instance = new FocoCriticoArco();
        public static FocoCriticoArco Instance { get { return instance; } }

        public override IdHabilidadeTalento ID
        {
            get { return IdHabilidadeTalento.focoCriticoArco; }
        }
        public override string NomeGrupo
        {
            get { return "Arquearia"; }
        }
        public override SkillName TipoGrupo
        {
            get { return SkillName.Archery; }
        }

        public override IdHabilidadeTalento TalentoNecessario { get { return IdHabilidadeTalento.focoArmaArco; } }
    }

    public class FocoCriticoPunho : FocoCritico {

        private FocoCriticoPunho() { }

        private static FocoCriticoPunho instance = new FocoCriticoPunho();
        public static FocoCriticoPunho Instance { get { return instance; } }

        public override IdHabilidadeTalento ID
        {
            get { return IdHabilidadeTalento.focoCriticoPunho; }
        }
        public override string NomeGrupo
        {
            get { return "Punho"; }
        }
        public override SkillName TipoGrupo
        {
            get { return SkillName.Wrestling; }
        }

        public override IdHabilidadeTalento TalentoNecessario { get { return IdHabilidadeTalento.focoArmaPunho; } }
    }
}
