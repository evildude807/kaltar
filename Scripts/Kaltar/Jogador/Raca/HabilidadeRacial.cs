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
            habilidades.Add(IdHabilidadeRacial.vidaFolegoMana, VidaFolegoMana.Instance);
            habilidades.Add(IdHabilidadeRacial.forcaDestrezaInteligencia, ForcaDestrezaInteligencia.Instance);
            habilidades.Add(IdHabilidadeRacial.tatica, PericiaTatica.Instance);

            //elfo
            habilidades.Add(IdHabilidadeRacial.folego, StatusFolego.Instance);
            habilidades.Add(IdHabilidadeRacial.esconder, PericiaEsconder.Instance);
            habilidades.Add(IdHabilidadeRacial.longaDistancia, PericiaArquearia.Instance);
            habilidades.Add(IdHabilidadeRacial.destreza, StatusDestreza.Instance);
            habilidades.Add(IdHabilidadeRacial.resistenciaVeneno, ResistenciaVeneno.Instance);

            //Meio-orc
            habilidades.Add(IdHabilidadeRacial.capacidadeCarga, CapacidadeCarga.Instance);
            habilidades.Add(IdHabilidadeRacial.forca, StatusForca.Instance);
            habilidades.Add(IdHabilidadeRacial.resistenciaFisica, ResistenciaFisica.Instance);
            habilidades.Add(IdHabilidadeRacial.resistenciaFrio, ResistenciaFrio.Instance);
    
            //Elfo negro
            habilidades.Add(IdHabilidadeRacial.resistenciaRaio, ResistenciaRaio.Instance);
            habilidades.Add(IdHabilidadeRacial.mana, StatusMana.Instance);
            habilidades.Add(IdHabilidadeRacial.inteligencia, StatusInteligencia.Instance);
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
