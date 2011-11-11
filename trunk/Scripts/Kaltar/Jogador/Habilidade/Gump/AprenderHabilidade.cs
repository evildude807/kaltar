using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;
using System.Collections;
using System.Collections.Generic;

namespace Kaltar.Habilidades
{
	public abstract class AprenderHabilidade : Gump
	{

        protected Jogador jogador = null;
        protected List<int> habilidades = null;
		
		public AprenderHabilidade(Jogador jogador, List<int> habilidades) : base( 10, 30 ) {
			
			this.jogador = jogador;
			this.habilidades = habilidades;
			
			this.Closable=true;
			this.Disposable=false;
			this.Dragable=true;
			this.Resizable=false;
			
			jogador.CloseGump( typeof( AprenderHabilidade ) );
			
			this.AddPage(0);
			this.AddBackground(10, 15, 482, 430, 9380);
            this.AddLabel(82, 54, 193, @"Aprender " + getTipoHabilidade());
			this.AddImage(32, 56, 52);
			this.AddImage(401, 53, 51);
            this.AddLabel(417, 67, 0, @totalPontosHabilidade(jogador));
            this.AddHtml(72, 93, 395, 38, @"Escolha a " + getTipoHabilidade() + " que deseja aprender.", (bool)false, (bool)false);
			
			listarHabilidades();
		}
		
		private void listarHabilidades() {

            Habilidade habilidade = null;
			int y = 140;

			for(int i = 0; i < habilidades.Count; i++) {
                habilidade = getHabilidade(habilidades[i]);

                if (habilidade != null)
                {

                    this.AddLabel(65, y + (i * 20), 0, habilidade.Nome);
                    this.AddButton(45, y + (i * 20), 1210, 1209, (habilidade.Id + 1), GumpButtonType.Reply, 1);
				}
                else
                {
                    this.AddLabel(65, y + (i * 20), 0, "Não foi encontrado " + getTipoHabilidade() + " com id: " + habilidades[i]);
                }
			}
		}
        
        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {

            if (info.ButtonID != 0)
            {
                jogador.CloseGump(typeof(AprenderHabilidade));
                jogador.SendGump(new ConfirmarAprenderHabilidade(jogador, (info.ButtonID - 1), this));
            }
        }

        #region metodos abstratos

        /**
         * Recupera a habilidade apartir do seu Id.
         */ 
        public abstract Habilidade getHabilidade(int idHabilidade);

        /**
         * Quando confirmado o aprendizado, esse método será invocado pelo gump de Confirmação.
         */ 
        public abstract bool aprenderHabilidade(Jogador jogador, int IdHabilidade);

        /**
         * Recupera o número de pontos de habilidade.
         */
        public abstract string totalPontosHabilidade(Jogador jogador);

        /**
         * Recupera o nome do tipo de habilidade. ex.: habilidade racial, talento etc.
         */ 
        public virtual string getTipoHabilidade()
        {
            return "habilidade";
        }


        #endregion
    }
}
