/*
 * Autor: Tiago Augusto Data: 26/2/2005 
 * Projeto: Kaltar
 */
 
using System;
using Server;
using Server.Mobiles;
using System.Collections;
using Server.ACC.CM;
using Server.Kaltar.Items;
using Server.Commands;

namespace Kaltar.Raca
{

	public class SistemaRaca	{

        public static void Initialize()
        {
            CommandSystem.Register("raca", AccessLevel.Player, new CommandEventHandler(testeRaca_OnCommand));
        }

        private static void testeRaca_OnCommand(CommandEventArgs e)
        {
            Jogador jogador = (Jogador)e.Mobile;

            Race raca = jogador.getSistemaRaca().getRacaModule().Raca;
            if (raca != null)
            {
                jogador.SendMessage("Você é da raça {0}", raca.Name);
            }
            else
            {
                jogador.SendMessage("Você não tem nenhuma raça, procura um GM.");
            }
        }

		#region atributos
		
        //jogador dono dos propriedades
		private Jogador jogador = null;

        #endregion
		
		#region construtores
       
        public SistemaRaca(Jogador jogador)
        {
			this.jogador = jogador;
		}	
	
		#endregion
		
		#region métodos

        /**
         * Recupera o modulo de talento
         */
        private RacaModule getRacaModule()
        {
            RacaModule tm = (RacaModule)CentralMemory.GetModule(jogador.Serial, typeof(RacaModule));
            return tm;
        }

        /**
         * Atribui os valores da raca ao jogador.
         */ 
        public void aplicarRaca(Race raca)
        {
            bool feminino = jogador.Female;

            int cabeloCor = raca.RandomHairHue();
            int PeleCor = raca.RandomSkinHue();

            int barba = raca.RandomFacialHair(feminino);
            int cabelo = raca.RandomHair(feminino);    

            //atribui o cabelo
			jogador.HairItemID = cabelo;
			jogador.HairHue = cabeloCor;

            //atribui a barba
            jogador.FacialHairItemID = barba;
            jogador.FacialHairHue = cabeloCor;

            //se tiver a barba de orc, remove e pode adicionar novamente abaixo.
            Item barbaItem = jogador.FindItemOnLayer(Layer.FacialHair);
            if (barbaItem is OrcMascaraBarba)
            {
                jogador.RemoveItem(barbaItem);
            }

            //adiciona a barba padrão para os orcs
            if (raca is MeioOrc)
            {
                jogador.AddItem(new OrcMascaraBarba(PeleCor));
            }

            //atribui o corpo
            jogador.BodyValue = raca.AliveBody(feminino);

            //atribui a cor da pelo
            jogador.Hue = PeleCor;

            //atribui no modulo de raca a raca escolhida
            RacaModule rm = getRacaModule();
            rm.Raca = raca;

            jogador.SendMessage("Você acaba de se tornar um {0}", raca.Name);
        }

        /**
         * Retorna o total de força.
         */
        public int MaxStr {
            get { return ((IKaltarRaca)getRacaModule().Raca).MaxStr; } 
        }

        /**
        * Retorna o total de destreza.
        */
        public int MaxDex { 
            get { return ((IKaltarRaca)getRacaModule().Raca).MaxDex; } 
        }

        /**
         * Retorna o total de inteligencia.
         */
        public int MaxInt { 
            get { return ((IKaltarRaca)getRacaModule().Raca).MaxInt; }
        }
        
        /**
         * Retorna o total de status cap.
         */ 
        public int StatusCap { get { return 250; } }

        #endregion
    }
}
