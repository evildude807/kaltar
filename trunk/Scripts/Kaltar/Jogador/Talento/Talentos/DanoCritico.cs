using System;
using Server;
using Server.Mobiles;
using Kaltar.Classes;
using Kaltar.Habilidades;
using Server.Items;

namespace Kaltar.Talentos {

    public abstract class DanoCritico : HabilidadeTalento
    {

        protected DanoCritico()
        {
			id = (int)ID;
			nome = "Dano Crítico";
            descricao = "Seus ataques críticos são mais perigosos. Você consegue deferir golpes mortais com o seu grupo de arma " + NomeGrupo + ". <br/> Bônus de 50% nos danos dos ataques críticos do grupo de arma escolhido.";
            preRequisito = "Foco Crítico.";
            nivelMaximo = 1;
		}
		
        public override bool PossuiPreRequisitos(Jogador jogador)
        {
            if (jogador.getSistemaClasse().getClasse() is Escudeiro || jogador.getSistemaClasse().getClasse() is Gatuno)
            {
                if (jogador.getSistemaTalento().possuiHabilidadeTalento(TalentoNecessario))
                {
                    return true;
                }
            }

            return false;
		}

        public abstract IdHabilidadeTalento TalentoNecessario { get; }

        public abstract IdHabilidadeTalento ID { get; }

        public abstract string NomeGrupo { get; }

        public abstract SkillName TipoGrupo { get; }

        public override int danoAtaqueCriticoBonus(HabilidadeNode node, Jogador atacante, Mobile defensor)
        {
            BaseWeapon arma = atacante.Weapon as BaseWeapon;
            if (arma.DefSkill.Equals(TipoGrupo))
            {
                return 50;
            }

            return 0;
        }
	}

    public class DanoCriticoEspada : DanoCritico {

        private DanoCriticoEspada() { }

        private static DanoCriticoEspada instance = new DanoCriticoEspada();
        public static DanoCriticoEspada Instance { get { return instance; } }

        public override IdHabilidadeTalento ID
        {
            get { return IdHabilidadeTalento.danoCriticoEspada; }
        }
        public override string NomeGrupo
        {
            get { return "Espada"; }
        }
        public override SkillName TipoGrupo
        {
            get { return SkillName.Swords; }
        }

        public override IdHabilidadeTalento TalentoNecessario { get { return IdHabilidadeTalento.focoCriticoEspada; } }
    }

    public class DanoCriticoMachado : DanoCritico {

        private DanoCriticoMachado() { }

        private static DanoCriticoMachado instance = new DanoCriticoMachado();
        public static DanoCriticoMachado Instance { get { return instance; } }

        public override IdHabilidadeTalento ID
        {
            get { return IdHabilidadeTalento.danoCriticoMachado; }
        }
        public override string NomeGrupo
        {
            get { return "Machado"; }
        }
        public override SkillName TipoGrupo
        {
            get { return SkillName.TasteID; }
        }

        public override IdHabilidadeTalento TalentoNecessario { get { return IdHabilidadeTalento.focoCriticoMachado; } }
    }

    public class DanoCriticoMaca : DanoCritico {

        private DanoCriticoMaca() { }

        private static DanoCriticoMaca instance = new DanoCriticoMaca();
        public static DanoCriticoMaca Instance { get { return instance; } }

        public override IdHabilidadeTalento ID
        {
            get { return IdHabilidadeTalento.danoCriticoMaca; }
        }
        public override string NomeGrupo
        {
            get { return "Maça"; }
        }
        public override SkillName TipoGrupo
        {
            get { return SkillName.Macing; }
        }
        public override IdHabilidadeTalento TalentoNecessario { get { return IdHabilidadeTalento.focoCriticoMaca; } }
    }

    public class DanoCriticoEsgrima : DanoCritico {

        private DanoCriticoEsgrima() { }

        private static DanoCriticoEsgrima instance = new DanoCriticoEsgrima();
        public static DanoCriticoEsgrima Instance { get { return instance; } }

        public override IdHabilidadeTalento ID
        {
            get { return IdHabilidadeTalento.danoCriticoEsgrima; }
        }
        public override string NomeGrupo
        {
            get { return "Esgrima"; }
        }
        public override SkillName TipoGrupo
        {
            get { return SkillName.Fencing; }
        }
        public override IdHabilidadeTalento TalentoNecessario { get { return IdHabilidadeTalento.focoCriticoEsgrima; } }
    }

    public class DanoCriticoArco : DanoCritico {

        private DanoCriticoArco() { }

        private static DanoCriticoArco instance = new DanoCriticoArco();
        public static DanoCriticoArco Instance { get { return instance; } }

        public override IdHabilidadeTalento ID
        {
            get { return IdHabilidadeTalento.danoCriticoArco; }
        }
        public override string NomeGrupo
        {
            get { return "Arquearia"; }
        }
        public override SkillName TipoGrupo
        {
            get { return SkillName.Archery; }
        }
        public override IdHabilidadeTalento TalentoNecessario { get { return IdHabilidadeTalento.focoCriticoArco; } }
    }

    public class DanoCriticoPunho : DanoCritico {

        private DanoCriticoPunho() { }

        private static DanoCriticoPunho instance = new DanoCriticoPunho();
        public static DanoCriticoPunho Instance { get { return instance; } }

        public override IdHabilidadeTalento ID
        {
            get { return IdHabilidadeTalento.danoCriticoPunho; }
        }
        public override string NomeGrupo
        {
            get { return "Punho"; }
        }
        public override SkillName TipoGrupo
        {
            get { return SkillName.Wrestling; }
        }
        public override IdHabilidadeTalento TalentoNecessario { get { return IdHabilidadeTalento.focoCriticoPunho; } }
    }
}
