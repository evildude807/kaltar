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
using Server.Items;
using Server.Commands;
using Server.Network;

namespace Kaltar.Morte
{
	/// <summary>
	/// Description of SistemaMorte.
	/// </summary>
    public class SistemaMorte	{

        public static void Initialize()
        {
            //quando o jogador morrer
            EventSink.PlayerDeath += new PlayerDeathEventHandler(EventSink_PlayerDeath);

            //quando o jogador loga
            EventSink.Login += new LoginEventHandler(EventSink_PlayerLogingOn);

            CommandSystem.Register("morte", AccessLevel.Player, new CommandEventHandler(testeMorte_OnCommand));
        }

        private static void testeMorte_OnCommand(CommandEventArgs e)
        {
            Jogador jogador = (Jogador)e.Mobile;

            int desmaio = jogador.getSistemaMorte().getMorteModule().Desmaio;
            int morte = jogador.getSistemaMorte().getMorteModule().Morte;

            jogador.SendMessage("Voce ja desmaiou {0} vezes e morreu {1}", desmaio, morte);
        }

        private static void EventSink_PlayerDeath(PlayerDeathEventArgs args)
        {
            Jogador jogador = (Jogador)args.Mobile;
            MorteModule mm = jogador.getSistemaMorte().getMorteModule();

            //mensagem
            jogador.PublicOverheadMessage(MessageType.Regular, 1, true, jogador.Name + " acaba de desmaiar.", true);

            if (mm != null)
            {
                //marca início de desmaio
                mm.Desmaiado = true;
                mm.InicioDesmaio = DateTime.Now;

                //manda o jogador para a sala da morte
                jogador.getSistemaMorte().teleportarSalaDaMorte();

                //inicia o timer de morte
                int tempoDesmaiado = (mm.Desmaio * TempoDesmaio) + 1; // para cada ponto de desmaio, fica mais tempo desmaiado
                mm.TimerMorte = new TimerMorte(jogador, tempoDesmaiado);
                mm.TimerMorte.Start();

                jogador.SendMessage("voce acaba de desmaiar. Deve recobrar a conciencia em {0} minutos", tempoDesmaiado);
            }
            else
            {
                Console.WriteLine("{0} não possui o modulo de morte.", jogador.Name);
            }
        }

        private static void EventSink_PlayerLogingOn(LoginEventArgs args)
        {
            Jogador jogador = (Jogador)args.Mobile;
            MorteModule mm = jogador.getSistemaMorte().getMorteModule();

            DateTime dataUltimoPontoGanho = mm.InicioDesmaio > mm.DataPontoGanho ? mm.InicioDesmaio : mm.DataPontoGanho;

            //tenta recuperar ponto de morte
            if (!mm.Morto && mm.Morte > 0 && mm.Desmaio == 0 && DateTime.Now > dataUltimoPontoGanho + TimeSpan.FromDays(tempoRecuperarDesmaio))
            {
                //calcula quantos pontos de desmaio ele deve ganhar
                TimeSpan difTempoDesmaiado = DateTime.Now - dataUltimoPontoGanho;
                double diasDesmaiado = difTempoDesmaiado.TotalDays;

                int recuperar = (int)(diasDesmaiado / tempoRecuperarDesmaio);

                //subtrai o desmaio e marca a nova data de desmaio para hoje.
                mm.DataPontoGanho = DateTime.Now;
                mm.Morte -= recuperar;
                jogador.SendMessage("Voce acaba de recuperar {0} pontos de morte.", recuperar);
            }
            //se ja passou o tempo de recuperar o tempo de desmaio
            else if (mm.Desmaio > 0 && DateTime.Now > dataUltimoPontoGanho + TimeSpan.FromDays(tempoRecuperarDesmaio))
            {

                //calcula quantos pontos de desmaio ele deve ganhar
                TimeSpan difTempoDesmaiado = DateTime.Now - dataUltimoPontoGanho;
                double diasDesmaiado = difTempoDesmaiado.TotalDays;

                int recuperar = (int)(diasDesmaiado / tempoRecuperarDesmaio);

                //subtrai o desmaio e marca a nova data de desmaio para hoje.
                mm.DataPontoGanho = DateTime.Now;
                mm.Desmaio -= recuperar;
                jogador.SendMessage("Voce acaba de recuperar {0} pontos de desmaio.", recuperar);
            }
        }

        /**
         * Envia o jogador para a sala da morte.
         */
        private void teleportarSalaDaMorte()
        {
            jogador.MoveToWorld(localSalaDaMorte.Location, localSalaDaMorte.Map);
        }

        /**
        * Envia o jogador para o local onde deve ser revivido.
        */
        private void teleportarLocalDeVolta()
        {
            //se tiver corpo, vai para o lugar do corpo
            if (jogador.Corpse != null)
            {
                jogador.MoveToWorld(jogador.Corpse.Location, jogador.Corpse.Map);
            }
            else
            {
                //se nao tiver corpo, vai para o lugar marcado
                MorteModule mm = jogador.getSistemaMorte().getMorteModule();
                CityInfo resultado = getLocalizacao(mm.LocalMaracado);

                jogador.MoveToWorld(resultado.Location, resultado.Map);
            }
        }

        private CityInfo getLocalizacao(string nomeLocal)
        {
            CityInfo local;
            if (nomeLocal == "padrao")
            {
                local = new CityInfo("Ouro Branco", "Barco", 1385, 579, 32, Map.Malas);
            }
            else if (nomeLocal == "ouroBranco")
            {
                local = new CityInfo("Ouro Branco", "Igreja", 1652, 591, 6, Map.Malas);
            }
            else if (nomeLocal == "loboLeite")
            {
                local = new CityInfo("Lobo leite", "Barco", 1385, 579, 32, Map.Malas);
            }
            else
            {
                local = new CityInfo("Ouro Branco", "Barco", 1385, 579, 32, Map.Malas);
            }

            return local;
        }

        void onTimerMorte()
        {
            MorteModule mm = jogador.getSistemaMorte().getMorteModule();

            //soma o desmaio
            mm.Desmaio++;

            if (mm.Desmaio > MaxDesmaio)
            {
                morreu();
                jogador.SendMessage("Voce acaba de morrer. Seu ponto de morte é {0}", mm.Morte);
            }
            else
            {
                voltarAVida();
                tratarCorpo();
                jogador.SendMessage("Voce acaba de acordar. Seu ponto de desmaio é {0}", mm.Desmaio);
            }
        }

        private void morreu()
        {
            MorteModule mm = jogador.getSistemaMorte().getMorteModule();

            mm.Morto = true;
            mm.Morte++;
            mm.InicioMorte = DateTime.Now;

            mm.Desmaio = MaxDesmaio;
        }

        private void voltarAVida()
        {
            MorteModule mm = jogador.getSistemaMorte().getMorteModule();

            mm.Morto = false;
            mm.InicioMorte = DateTime.MinValue;

            mm.Desmaiado = false;
            mm.InicioDesmaio = DateTime.MinValue;
                
            teleportarLocalDeVolta();
                
            //reviver o jogador
            jogador.Resurrect();

            ajustarVidaManaStamina();
        }

        /**
         * Pega as coisas do corpo e veste no jogador e apaga o corpo
         */ 
        private void tratarCorpo()
        {

            //pega os itens do corpo
            Container pack = jogador.Backpack;
            Container corpse = jogador.Corpse;

            if (pack != null && corpse != null)
            {
                List<Item> items = new List<Item>(corpse.Items);
                for (int i = 0; i < items.Count; ++i)
                {
                    Item item = items[i];

                    if (item.Layer != Layer.Hair && item.Layer != Layer.FacialHair && item.Movable)
                    {
                        pack.DropItem(item);

                        if (item.Layer != Layer.Backpack && item.CanEquip(jogador))
                        {
                            jogador.EquipItem(item);
                        }
                    }
                }
            }

            //deleta o corpo
            if (corpse is Corpse)
            {
                Corpse corpo = (Corpse)corpse;
                corpo.Delete();
            }
            else
            {
                jogador.SendMessage("Nao foi possivel remover o seu corpo, aviso o Staff.");
            }
        }
   
        /**
         * Quando o jogador volta a vida, ajustar os pontos de vida, mana e stamina
         */ 
        private void ajustarVidaManaStamina()
        {
            jogador.Hits = (int)(jogador.HitsMax * 0.10);
            jogador.Stam = (int)(jogador.StamMax * 0.10);
            jogador.Mana = (int)(jogador.ManaMax * 0.10);
        }

        /*
         * Quando o jogador e curado por alquem, quando esta desmaiado
         */ 
        public void levantarDesmaiado()
        {
            MorteModule mm = jogador.getSistemaMorte().getMorteModule();
         
            //para o tempo de morte
            if (mm.TimerMorte != null)
            {
                mm.TimerMorte.Stop();
                mm.TimerMorte = null;
            }

            //volta a vida
            voltarAVida();
            tratarCorpo();
        }

        /**
         * Verifica se o jogador pode ser revivido depois da morte.
         */ 
        public bool podeReviver()
        {
            MorteModule mm = jogador.getSistemaMorte().getMorteModule();

            int tempoRecuperarMorte = mm.Morte * tempoRecuperarDesmaio;

            if (mm.Morto && DateTime.Now > mm.InicioMorte + TimeSpan.FromDays(tempoRecuperarMorte))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void levantarMorte()
        {
            MorteModule mm = jogador.getSistemaMorte().getMorteModule();

            jogador.Corpse = null;

            //volta a vida
            voltarAVida();
        }

        #region atributos

        //jogador dono dos talentos
        private Jogador jogador = null;

        //local da sala da morte.
        CityInfo localSalaDaMorte = new CityInfo("Ceu", "Sala da morte", 1385, 579, 32, Map.Malas);

        //número máximo de desmaio até ganhar um ponto de morte.
        private static int MaxDesmaio = 5;

        //número de minuto para ficar desmaiado
        private static int tempoDesmaio = 1;

        //número de dias para recuperar ponto de desmaio
        private static int tempoRecuperarDesmaio = 3;

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

        #region timer de morte

        public class TimerMorte : Timer
        {

            Jogador jogador;

            public TimerMorte(Jogador jogador, int tempoDesmaiado)
                : base(TimeSpan.FromMinutes(tempoDesmaiado))
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
}
