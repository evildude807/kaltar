/*
 * Autor: Tiago Augusto Data: 26/2/2005 
 * Projeto: Kaltar
 */
 
using System;
using Server;
using Server.Mobiles;
using System.Collections.Generic;
using Server.ACC.CM;
using Kaltar.Classes;
using Server.Commands;

namespace Kaltar.Morte
{
	/// <summary>
	/// Description of SistemaMorte.
	/// </summary>
    public class SistemaMorte	{

        public static void Initialize()
        {
               // Register our event handler
            EventSink.PlayerDeath += new PlayerDeathEventHandler(EventSink_PlayerDeath);

            CommandSystem.Register("testeMorte", AccessLevel.Player, new CommandEventHandler(testeMorte_OnCommand));
        }

        private static void testeMorte_OnCommand(CommandEventArgs e)
        {
            Jogador jogador = (Jogador)e.Mobile;

            jogador.SendMessage(jogador.getSistemaMorte() + "");
            jogador.SendMessage(jogador.getSistemaMorte() != null ? jogador.getSistemaMorte().getMorteModule() +"": null);

            //int d = jogador.getSistemaMorte().getMorteModule().Desmaio;
            int d = 1;

            jogador.SendMessage("Voce ja desmaiou {0} vezes", d);
        }

        //local da sala da morte.
        private static Point3D localSalaDaMorte = new Point3D(705, 818, -90);

        //número máximo de desmaio até ganhar um ponto de morte.
        private static int MaxDesmaio = 5;

        //número de minuto para ficar desmaiado
        private static int tempoDesmaio = 1;

        private static void EventSink_PlayerDeath(PlayerDeathEventArgs args)
        {
            Jogador jogador = (Jogador)args.Mobile;
            Console.WriteLine("{0} acaba de desmaiar as {1}.", jogador.Name, DateTime.Now);
            MorteModule mm = jogador.getSistemaMorte().getMorteModule();

            if (mm != null)
            {
                //marca início de desmaio
                mm.InicioDesmaio = DateTime.Now;

                //manda o jogador para a sala da morte
                jogador.getSistemaMorte().teleportarSalaDaMorte();

                //inicia o timer de morte
                mm.TimerMorte = new TimerMorte(jogador);
                mm.TimerMorte.Start();
            }
            else
            {
                Console.WriteLine("{0} não possui o modulo de morte.", jogador.Name);
            }
        }

        /**
         * Envia o jogador para a sala da morte.
         */
        private void teleportarSalaDaMorte()
        {
            jogador.MoveToWorld(localSalaDaMorte, Map.Malas);
        }

        /**
        * Envia o jogador para o local onde deve ser revivido.
        */
        private void teleportarLocalDeVolta()
        {
            if (jogador.Corpse != null)
            {
                jogador.MoveToWorld(jogador.Corpse.Location, jogador.Corpse.Map);
            }
            else
            {
                //teleportar para local padrao marcado.
            }
        }

        public void onTimerMorte()
        {
            MorteModule mm = jogador.getSistemaMorte().getMorteModule();

            //soma o desmaio
            mm.Desmaio++;

            if (mm.Desmaio > MaxDesmaio)
            {
                morreu();
            }
            else
            {
                voltarAVida();
            }
        }

        private void morreu()
        {
            MorteModule mm = jogador.getSistemaMorte().getMorteModule();

            mm.Morte++;
            mm.Desmaio--;
            mm.InicioMorte = DateTime.Now;

            jogador.SendMessage("Voce acaba de morrer. Seu ponto de morte é {0}", mm.Morte);
        }

        private void voltarAVida()
        {
            MorteModule mm = jogador.getSistemaMorte().getMorteModule();
    
            //mm.InicioDesmaio = null;
                
            teleportarLocalDeVolta();
                
            //reviver o jogador
            jogador.Resurrect();

            ajustarVidaManaStamina();

            jogador.SendMessage("Voce acaba de acordar. Seu ponto de desmaio é {0}", mm.Desmaio);
        }
           
        /**
         * Quando o jogador volta a vida, ajustar os pontos de vida, mana e stamina
         */ 
        private void ajustarVidaManaStamina()
        {
            jogador.Hits = 10;
            jogador.Stam = 10;
            jogador.Mana = 10;
        }

        #region atributos

        //jogador dono dos talentos
        private Jogador jogador = null;

        #endregion

        #region propriedade

        public static int TempoDesmaio { get { return tempoDesmaio; } }

        #endregion

        public SistemaMorte(Jogador jogador){
            this.jogador = jogador;
        }

        /**
         * Recupera o modulo de talento
         */
        private MorteModule getMorteModule()
        {
            MorteModule tm = (MorteModule)CentralMemory.GetModule(jogador.Serial, typeof(MorteModule));
            return tm;
        }
    }

    #region timer de morte

    public class TimerMorte : Timer {

        Jogador jogador;

        public TimerMorte(Jogador jogador)
            : base(TimeSpan.FromMinutes(SistemaMorte.TempoDesmaio))
        {
            this.jogador = jogador;
        }

        protected override void OnTick()
        { 
            Console.WriteLine("{0} não foi tratado a tempo.", jogador.Name);

            jogador.getSistemaMorte().onTimerMorte();
        }
    }

    #endregion
}
