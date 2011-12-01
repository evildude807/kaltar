using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;
using System.Collections;
using System.Collections.Generic;
using Kaltar.Habilidades;

namespace Kaltar.Raca
{
    public class AprenderHabilidadeRacial : AprenderHabilidadeGump
	{
        public static void Initialize()
        {
            CommandSystem.Register("aHabilidaderacial", AccessLevel.Player, new CommandEventHandler(AprenderHabilidadeRacials));
            CommandSystem.Register("rHabilidaderacial", AccessLevel.Player, new CommandEventHandler(RemoverHabilidadeRacials));
        }

        private static void RemoverHabilidadeRacials(CommandEventArgs e)
        {
            Jogador jogador = (Jogador)e.Mobile;

            jogador.getSistemaRaca().removerTotalHabilidades();

            jogador.UpdateResistances();
        }

        private static void AprenderHabilidadeRacials(CommandEventArgs e)
        {
            Jogador jogador = (Jogador)e.Mobile;

            List<int> habilidades = new List<int>();
            
            habilidades.Add((int)IdHabilidadeRacial.skillCapTrabalho);
            habilidades.Add((int)IdHabilidadeRacial.resistenciaFisica);
            habilidades.Add((int)IdHabilidadeRacial.resistenciaRaio);
            habilidades.Add((int)IdHabilidadeRacial.mana);
            habilidades.Add((int)IdHabilidadeRacial.capacidadeCarga);
            habilidades.Add((int)IdHabilidadeRacial.inteligencia);
            habilidades.Add((int)IdHabilidadeRacial.forcaDestrezaInteligencia);
            habilidades.Add((int)IdHabilidadeRacial.longaDistancia);

            jogador.SendGump(new AprenderHabilidadeRacial(jogador, habilidades));
        }

        public AprenderHabilidadeRacial(Jogador jogador, List<int> habilidades) : base(jogador, habilidades)
        {

        }

        /**
         * Recupera a habilidade apartir do seu Id.
         */
        public override Habilidade getHabilidade(int idHabilidade)
        {
            try
            {
                return HabilidadeRacial.getHabilidadeRacial((IdHabilidadeRacial)idHabilidade);
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
                return jogador.getSistemaRaca().aprender((IdHabilidadeRacial)idHabilidade);
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
            return jogador.getSistemaRaca().pontosDisponiveis() + "";
        }

        /**
         * Recupera o nome do tipo de habilidade. ex.: habilidade racial, talento etc.
         */
        public override string getTipoHabilidade()
        {
            return "habilidade racial";
        }
	}
}
