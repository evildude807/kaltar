using System;
using Server.Network;

using Kaltar.Classes;
using Kaltar.Talentos;
using Kaltar.Morte;
using Server.ACC.CM;
using Server.Mobiles;
using Kaltar.Modulo;

namespace Server.Misc
{
	public class LoginStats
	{
		public static void Initialize()
		{
			// Register our event handler
			EventSink.Login += new LoginEventHandler( EventSink_Login );
		}

		private static void EventSink_Login( LoginEventArgs args )
		{
			int userCount = NetState.Instances.Count;
			int itemCount = World.Items.Count;
			int mobileCount = World.Mobiles.Count;

			Mobile m = args.Mobile;

			m.SendMessage( "Bem-vindo, {0}! Existem {1} jogadores online, {2} itens e {3} contas no mundo de Kaltar.",
				args.Mobile.Name,
				userCount,
				itemCount,
				mobileCount);

            //registra os modulos que todo jogador deve possuir
            RegistroModule.registrarModuleJogador((Jogador)m);
		}
	}
}