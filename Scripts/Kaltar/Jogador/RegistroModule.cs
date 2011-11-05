using System;
using System.Collections.Generic;
using System.Text;

using Kaltar.Classes;
using Kaltar.Talentos;
using Kaltar.Util;
using Kaltar.propriedade;
using Kaltar.aventura;
using Kaltar.Morte;
using Server.Mobiles;
using Server.ACC.CM;
using Kaltar.Raca;

namespace Kaltar.Modulo
{
    public class RegistroModule
    {
        public static void registrarModuleJogador(Jogador jogador)
        {
            //modulo de talento
            CentralMemory.AddModule(new TalentoModule(jogador.Serial));

            //modulo de classe
            CentralMemory.AddModule(new ClasseModule(jogador.Serial));

            //modulo de morte
            CentralMemory.AddModule(new MorteModule(jogador.Serial));

            //modulo de raca
            CentralMemory.AddModule(new RacaModule(jogador.Serial));
        }
    }
}
