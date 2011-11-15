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
            habilidades.Add(IdHabilidadeTalento.alerta, Alerta.Instance);
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
