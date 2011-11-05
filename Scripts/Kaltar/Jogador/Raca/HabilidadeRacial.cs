using System;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.Mobiles;
using Kaltar.Habilidades;

namespace Kaltar.Raca {

    public abstract class HabilidadeRacial : Habilidade {

        //armazena todas as habilidades
        private static Dictionary<IdHabilidadeRacial, HabilidadeRacial> habilidades = new Dictionary<IdHabilidadeRacial, HabilidadeRacial>();

        //todos os talentos disponíveis
        public static void Initialize()
        {
            //humano
            habilidades.Add(IdHabilidadeRacial.skillCapTrabalho, SkillCapTrabalho.Instance);
            habilidades.Add(IdHabilidadeRacial.vidaFolegoMana, null);
            habilidades.Add(IdHabilidadeRacial.forcaDestrezaInteligencia, null);
            habilidades.Add(IdHabilidadeRacial.tatica, null);

            //elfo
            habilidades.Add(IdHabilidadeRacial.folego, null);
            habilidades.Add(IdHabilidadeRacial.esconder, null);
            habilidades.Add(IdHabilidadeRacial.longaDistancia, null);
            habilidades.Add(IdHabilidadeRacial.destreza, null);
            habilidades.Add(IdHabilidadeRacial.resistenciaVeneno, null);

            //Meio-orc
            habilidades.Add(IdHabilidadeRacial.capacidadeCarga, null);
            habilidades.Add(IdHabilidadeRacial.forca, null);
            habilidades.Add(IdHabilidadeRacial.resistenciaFisica, null);
            habilidades.Add(IdHabilidadeRacial.resistenciaFrio, null);
    
            //Elfo negro
            habilidades.Add(IdHabilidadeRacial.resistenciaRaio, null);
            habilidades.Add(IdHabilidadeRacial.mana, null);
            habilidades.Add(IdHabilidadeRacial.inteligencia, null);
            //esconder que esta no elfo
        }

        #region metodos

        /**
        * Retorna a classe do talento pelo seu ID
        */
        public static HabilidadeRacial getHabilidadeRacial(IdHabilidadeRacial id)
        {
            return (HabilidadeRacial)habilidades[id];
        }	

        #endregion

    }
}
