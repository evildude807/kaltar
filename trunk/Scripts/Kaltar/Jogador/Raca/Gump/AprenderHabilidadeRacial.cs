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
    public class AprenderHabilidadeRacial : AprenderHabilidade
	{
        public static void Initialize()
        {
            CommandSystem.Register("aHabilidaderacial", AccessLevel.Player, new CommandEventHandler(AprenderHabilidadeRacials));
        }

        private static void AprenderHabilidadeRacials(CommandEventArgs e)
        {
            Jogador jogador = (Jogador)e.Mobile;

            List<int> habilidades = new List<int>();
            habilidades.Add((int)IdHabilidadeRacial.skillCapTrabalho);

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
            Console.WriteLine(idHabilidade);
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
         * Recupera o nome do tipo de habilidade. ex.: habilidade racial, talento etc.
         */
        public override string getTipoHabilidade()
        {
            return "habilidade racial";
        }
	}
}
