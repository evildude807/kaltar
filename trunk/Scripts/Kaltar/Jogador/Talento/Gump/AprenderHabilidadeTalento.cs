using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;
using System.Collections;
using System.Collections.Generic;
using Kaltar.Habilidades;

namespace Kaltar.Talentos
{
    public class AprenderHabilidadeTalento : AprenderHabilidadeGump
	{
        public static void Initialize()
        {
            CommandSystem.Register("aTalento", AccessLevel.Player, new CommandEventHandler(AprenderHabilidadeTalentos));
            CommandSystem.Register("rTalento", AccessLevel.Player, new CommandEventHandler(RemoverHabilidadeTalentos));
        }

        private static void RemoverHabilidadeTalentos(CommandEventArgs e)
        {
            Jogador jogador = (Jogador)e.Mobile;

            jogador.getSistemaTalento().removerTotalHabilidades();

            jogador.UpdateResistances();
        }

        private static void AprenderHabilidadeTalentos(CommandEventArgs e)
        {
            Jogador jogador = (Jogador)e.Mobile;

            List<int> habilidades = new List<int>();
            habilidades.Add((int)IdHabilidadeTalento.bloquear);
            habilidades.Add((int)IdHabilidadeTalento.flanquear);
            habilidades.Add((int)IdHabilidadeTalento.poderDaFe);
            habilidades.Add((int)IdHabilidadeTalento.peleDeferro);

            jogador.SendGump(new AprenderHabilidadeTalento(jogador, habilidades));
        }

        public AprenderHabilidadeTalento(Jogador jogador, List<int> habilidades) : base(jogador, habilidades)
        {

        }

        /**
         * Recupera a habilidade apartir do seu Id.
         */
        public override Habilidade getHabilidade(int idHabilidade)
        {
            try
            {
                return HabilidadeTalento.getHabilidadeTalento((IdHabilidadeTalento)idHabilidade);
            }
            catch(Exception e) {
                Console.WriteLine(e.StackTrace);

                jogador.SendMessage("Não foi possível encontrar habilidade com o id informado. " + idHabilidade);
                return null;
            }
        }

        /**
         * Quando confirmado o aprendizado, esse método será invocado pelo gump de Confirmação.
         */
        public override bool aprenderHabilidade(Jogador jogador, int idHabilidade)
        {
            try
            {
                return jogador.getSistemaTalento().aprender((IdHabilidadeTalento)idHabilidade);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);

                jogador.SendMessage("Não foi possível encontrar habilidade com o id informado. " + idHabilidade);
                return false;
            }
        }

        /**
         * Recupera o número de pontos de habilidade.
         */
        public override string totalPontosHabilidade(Jogador jogador)
        {
            return jogador.getSistemaTalento().pontosDisponiveis() + "";
        }

        /**
         * Recupera o nome do tipo de habilidade. ex.: habilidade Talento, talento etc.
         */
        public override string getTipoHabilidade()
        {
            return "talento";
        }
	}
}
