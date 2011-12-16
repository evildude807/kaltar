using System;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.Mobiles;
using Kaltar.Habilidades;

namespace Kaltar.Talentos {

    public abstract class HabilidadeTalento : Habilidade {

        //armazena todas as habilidades
        private static Dictionary<IdHabilidadeTalento, HabilidadeTalento> habilidades = new Dictionary<IdHabilidadeTalento, HabilidadeTalento>();

        //todos os habilidades disponíveis
        public static void Initialize()
        {
            habilidades.Add(IdHabilidadeTalento.bloquear, Bloquear.Instance);

            habilidades.Add(IdHabilidadeTalento.focoArmaEspada, FocoEmArmaEspada.Instance);
            habilidades.Add(IdHabilidadeTalento.focoArmaMachado, FocoEmArmaMachado.Instance);
            habilidades.Add(IdHabilidadeTalento.focoArmaMaca, FocoEmArmaMaca.Instance);
            habilidades.Add(IdHabilidadeTalento.focoArmaEsgrima, FocoEmArmaEsgrima.Instance);
            habilidades.Add(IdHabilidadeTalento.focoArmaPunho, FocoEmArmaPunho.Instance);
            habilidades.Add(IdHabilidadeTalento.focoArmaArco, FocoEmArmaArco.Instance);

            habilidades.Add(IdHabilidadeTalento.especializacaoArmaEspada, EspecializacaoEmArmaEspada.Instance);
            habilidades.Add(IdHabilidadeTalento.especializacaoArmaMachado, EspecializacaoEmArmaMachado.Instance);
            habilidades.Add(IdHabilidadeTalento.especializacaoArmaMaca, EspecializacaoEmArmaMaca.Instance);
            habilidades.Add(IdHabilidadeTalento.especializacaoArmaEsgrima, EspecializacaoEmArmaEsgrima.Instance);
            habilidades.Add(IdHabilidadeTalento.especializacaoArmaPunho, EspecializacaoEmArmaPunho.Instance);
            habilidades.Add(IdHabilidadeTalento.especializacaoArmaArco, EspecializacaoEmArmaArco.Instance);

            habilidades.Add(IdHabilidadeTalento.tenacidade, Tenacidade.Instance);
            habilidades.Add(IdHabilidadeTalento.aptiddaoMagica, AptidaoMagica.Instance);

            habilidades.Add(IdHabilidadeTalento.focoCriticoEspada, FocoCriticoEspada.Instance);
            habilidades.Add(IdHabilidadeTalento.focoCriticoMachado, FocoCriticoMachado.Instance);
            habilidades.Add(IdHabilidadeTalento.focoCriticoMaca, FocoCriticoMaca.Instance);
            habilidades.Add(IdHabilidadeTalento.focoCriticoEsgrima, FocoCriticoEsgrima.Instance);
            habilidades.Add(IdHabilidadeTalento.focoCriticoPunho, FocoCriticoPunho.Instance);
            habilidades.Add(IdHabilidadeTalento.focoCriticoArco, FocoCriticoArco.Instance);

            habilidades.Add(IdHabilidadeTalento.danoCriticoEspada, DanoCriticoEspada.Instance);
            habilidades.Add(IdHabilidadeTalento.danoCriticoMachado, DanoCriticoMachado.Instance);
            habilidades.Add(IdHabilidadeTalento.danoCriticoMaca, DanoCriticoMaca.Instance);
            habilidades.Add(IdHabilidadeTalento.danoCriticoEsgrima, DanoCriticoEsgrima.Instance);
            habilidades.Add(IdHabilidadeTalento.danoCriticoPunho, DanoCriticoPunho.Instance);
            habilidades.Add(IdHabilidadeTalento.danoCriticoArco, DanoCriticoArco.Instance);

            habilidades.Add(IdHabilidadeTalento.agil, Agil.Instance);
            habilidades.Add(IdHabilidadeTalento.robusto, Robusto.Instance);
            habilidades.Add(IdHabilidadeTalento.sagaz, Sagaz.Instance);

            habilidades.Add(IdHabilidadeTalento.conjuracaoResistente, ConjuracaoResistente.Instance);
            habilidades.Add(IdHabilidadeTalento.conjuracaoForte, ConjuracaoForte.Instance);
            habilidades.Add(IdHabilidadeTalento.conjuracaoExtendida, ConjuracaoExtendida.Instance);

            habilidades.Add(IdHabilidadeTalento.olhosDeAguia, OlhosDeAguia.Instance);

            habilidades.Add(IdHabilidadeTalento.peleDeferro, PeleDeFerro.Instance);
            habilidades.Add(IdHabilidadeTalento.resistenciaElemental, ResistenciaElemental.Instance);

            habilidades.Add(IdHabilidadeTalento.flanquear, Flanquear.Instance);
            habilidades.Add(IdHabilidadeTalento.poderDaFe, PoderDaFe.Instance);

            habilidades.Add(IdHabilidadeTalento.revitalizacaoDeCombate, RevitalizacaoDeCombate.Instance);
            habilidades.Add(IdHabilidadeTalento.folegoDeCombate, FolegoDeCombate.Instance);
            habilidades.Add(IdHabilidadeTalento.manaDeCombate, ManaDeCombate.Instance);

            habilidades.Add(IdHabilidadeTalento.manaRevigorante, ManaRevigorante.Instance);
            habilidades.Add(IdHabilidadeTalento.folegoRevigorante, FolegoRevigorante.Instance);
            habilidades.Add(IdHabilidadeTalento.defesaRevigorante, DefesaRevigorante.Instance);
        }

        #region metodos

        /**
        * Retorna a classe do talento pelo seu ID
        */
        public static HabilidadeTalento getHabilidadeTalento(IdHabilidadeTalento id)
        {
            return (HabilidadeTalento)habilidades[id];
        }	

        #endregion

    }
}
