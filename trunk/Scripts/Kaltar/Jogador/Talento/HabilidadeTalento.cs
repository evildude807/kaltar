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
            habilidades.Add(Alerta.Instance.ID, Alerta.Instance);

            habilidades.Add(ArmaComumEspada.Instance.ID, ArmaComumEspada.Instance);
            habilidades.Add(ArmaComumPontiaguda.Instance.ID, ArmaComumPontiaguda.Instance);
            habilidades.Add(ArmaComumMachado.Instance.ID, ArmaComumMachado.Instance);
            habilidades.Add(ArmaComumDistancia.Instance.ID, ArmaComumDistancia.Instance);
            habilidades.Add(ArmaComumAmasso.Instance.ID, ArmaComumAmasso.Instance);

            habilidades.Add(ArmaduraLeve.Instance.ID, ArmaduraLeve.Instance);
            habilidades.Add(ArmaduraMedia.Instance.ID, ArmaduraMedia.Instance);
            habilidades.Add(ArmaduraPesada.Instance.ID, ArmaduraPesada.Instance);

            habilidades.Add(EscudoPequeno.Instance.ID, EscudoPequeno.Instance);
            habilidades.Add(EscudoMedio.Instance.ID, EscudoMedio.Instance);
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
