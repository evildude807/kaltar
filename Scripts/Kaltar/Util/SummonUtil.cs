using System;
using System.Collections.Generic;
using System.Text;
using Kaltar.Habilidades;
using Kaltar.Raca;
using Kaltar.Talentos;
using Server.Mobiles;
using Server;

namespace Kaltar.Util {
    public sealed class SummonUtil {

        #region instance

        private static SummonUtil instance = new SummonUtil();
		
		public static SummonUtil Instance {
			get {return instance;}
		}

        private SummonUtil()
        {
        }

        #endregion

        /**
         * Evento invocado quando uma criatura é invocada por um jogador.
         */ 
        public void OnSummon(Jogador caster, BaseCreature creature, ref Point3D p, ref TimeSpan duration)
        {
            SistemaTalento st = caster.getSistemaTalento();

            //Aumenta o duração
            if (st.possuiHabilidadeTalento(IdHabilidadeTalento.conjuracaoExtendida))
            {
                caster.SendMessage("Duracao normal: {0}", duration);

                double totalDuracaoSegundos = duration.TotalSeconds * 1.5;
                duration = TimeSpan.FromSeconds(totalDuracaoSegundos);

                caster.SendMessage("Duracao alterada: {0}", duration);
            }

            //Aumenta a resistencia das conjurações
            if (st.possuiHabilidadeTalento(IdHabilidadeTalento.conjuracaoResistente))
            {
                creature.AddResistanceMod(new ResistanceMod(ResistanceType.Cold, 5));
                creature.AddResistanceMod(new ResistanceMod(ResistanceType.Energy, 5));
                creature.AddResistanceMod(new ResistanceMod(ResistanceType.Fire, 5));
                creature.AddResistanceMod(new ResistanceMod(ResistanceType.Physical, 5));
                creature.AddResistanceMod(new ResistanceMod(ResistanceType.Poison, 5));

                caster.SendMessage("Bonus resistencia");
            }

            //Aumenta os atributos da conjuração
            if (st.possuiHabilidadeTalento(IdHabilidadeTalento.conjuracaoResistente))
            {
                HabilidadeNode node = st.getHabilidades()[IdHabilidadeTalento.conjuracaoResistente];

                StatMod statusBonus = new StatMod(StatType.All, "TalentoConjuracaoForte", node.Nivel * 10, TimeSpan.FromDays(99));
                creature.AddStatMod(statusBonus);

                caster.SendMessage("Bonus Status");
            }
        }

        /**
         * Verifica se o observador esta no arco de visão do alvo.
         */ 
        public bool estaNoArcoDeVisao(Mobile observador, Mobile alvo)
        {
            Direction direcaoObservador = observador.Direction;
            Direction direcaoNecessaria = observador.GetDirectionTo(alvo.Location);

            int intDirObervador = (int)direcaoObservador;
            int intDirNecessaria = (int)direcaoNecessaria;

            if (intDirNecessaria == intDirObervador
                || intDirNecessaria == intDirObervador + 1
                || intDirNecessaria == intDirObervador - 1)
            {
                return true;
            }

            return false;
        }
    }
}
