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

namespace Kaltar.Morte
{
	/// <summary>
	/// Description of SistemaMorte.
	/// </summary>
    //public class SistemaMorte	{

    //    //local da sala da morte.
    //    private static Point3D localSalaDaMorte = new Point3D(10, 10, 10);

    //    public static void Initialize()
    //    {
    //        // Register our event handler
    //        EventSink.PlayerDeath += new PlayerDeathEventHandler(EventSink_PlayerDeath);
    //    }

    //    private static void EventSink_PlayerDeath(PlayerDeathEventArgs args)
    //    {
    //        Jogador jogador = (Jogador)args.Mobile;
            
    //        //manda o jogador para a sala da morte
    //        teleportarSalaDaMorte(jogador);
                       

    //    }

    //    private static void teleportarSalaDaMorte(Jogador jogador)
    //    {
    //        jogador.MoveToWorld(localSalaDaMorte, Map.Malas);
    //    }


    //    #region atributos

    //    //jogador dono dos talentos
    //    private Jogador jogador = null;

    //    #endregion
			
    //    public SistemaMorte(Jogador jogador){
    //        this.jogador = jogador;
    //    }

    //    /**
    //     * Recupera o modulo de talento
    //     */
    //    private static MorteModule getMorteModule()
    //    {
    //        MorteModule tm = (MorteModule)CentralMemory.GetModule(jogador.Serial, typeof(MorteModule));
    //        return tm;
    //    }
    //}

    //#region timer de morte

    //public class TimerMorte : Timer {

    //    Jogador jogador;

    //    public TimerMorte(Jogador jogador) : base(TimeSpan.FromMinutes(1))
    //    {
    //        this.jogador = jogador;
    //    }

    //    protected override void OnTick()
    //    { 
    //        //SistemaMorte.getMorte
    //    }
    //}

    //#endregion
}
