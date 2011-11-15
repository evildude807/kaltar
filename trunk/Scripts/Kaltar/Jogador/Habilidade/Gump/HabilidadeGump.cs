using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;
using System.Collections.Generic;

namespace Kaltar.Habilidades {

	public abstract class HabilidadeGump : Gump {

        private Jogador jogador;

		public HabilidadeGump(Jogador jogador): base( 0, 0 ) {
			
            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

            this.jogador = jogador;

			jogador.CloseGump( typeof( HabilidadeGump ) );
			
			//ttulo	
			this.AddPage(0);
			this.AddBackground(10, 15, 482, 430, 9380);
			this.AddLabel(83, 54, 193, @getTitulo());
			this.AddImage(33, 59, 52);
			
			//total de pontos de HabilidadeRacials
			this.AddImage(401, 53, 51);
            this.AddLabel(417, 67, 0, @totalPontosHabilidade(jogador));

            //cabecalho da tabela de habilidade
            AddLabel(338, 135, 0, @"Nível");
            AddLabel(384, 135, 0, @"Nível Máximo");
            AddLabel(48, 135, 0, @"Habilidade");

            //imagens de separacao do cabecalho e lista
            AddImage(43, 159, 50);
            AddImage(181, 159, 50);
            AddImage(321, 159, 50);
            
			//lista de HabilidadeRacials
			listarHabilidades(jogador);
		}

		private void listarHabilidades(Jogador jogador) {

            List<HabilidadeNode> habilidades = getHabilidades(jogador);
			int y = 181;	//posicao do primeito HabilidadeRacial
			
			if(habilidades.Count == 0) {
				this.AddLabel(65, y, 0, @"Você não possui habilidade.");
            }

            for (int i = 0; i < habilidades.Count; i++)
            {
                HabilidadeNode habilidadeNode = habilidades[i];
                Habilidade habilidade = getHabilidade(habilidadeNode.Id);

                if (habilidade != null)
                {
                    this.AddLabel(65, y + (i * 20), 0, habilidade.Nome);
                    this.AddLabel(347, y + (i * 20), 0, habilidadeNode.Nivel + "");
                    this.AddLabel(416, y + (i * 20), 0, habilidade.NivelMaximo + "");
                    this.AddButton(45, y + (i * 20), 1210, 1209, (habilidade.Id + 1), GumpButtonType.Reply, 1);
                }
                else
                {
                    this.AddLabel(65, y + (i * 20), 0, "Não foi encontrado habilidade com id: " + habilidades[i]);
                }
            }
		}

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {

            if (info.ButtonID != 0)
            {
                jogador.CloseGump(typeof(HabilidadeGump));
                jogador.SendGump(new HabilidadeDescricaoGump(jogador, (info.ButtonID - 1), this));
            }
        }

        #region metodos abstratos

        /**
         * Recupera a habilidade por seu id.
         */ 
        public abstract Habilidade getHabilidade(int idHabilidade);

        /**
         * Recupera todas as habilidades do jogador.
         */
        public abstract List<HabilidadeNode> getHabilidades(Jogador jogador);
		
        /**
         * Recupera o número de pontos de habilidade.
         */ 
		public abstract string totalPontosHabilidade(Jogador jogador);

        /**
         * Recupera o título do gump.
         */
        public virtual string getTitulo()
        {
            return "Habilidades";
        }

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
