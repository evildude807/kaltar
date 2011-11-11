using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;
using System.Collections.Generic;

namespace Kaltar.Habilidades {

	public abstract class HabilidadeGump : Gump {

		public HabilidadeGump(Jogador jogador): base( 0, 0 ) {
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			
			jogador.CloseGump( typeof( HabilidadeGump ) );
			
			//ttulo	
			this.AddPage(0);
			this.AddBackground(10, 15, 482, 430, 9380);
			this.AddLabel(83, 54, 193, @getTitulo());
			this.AddImage(33, 59, 52);
			
			//total de pontos de HabilidadeRacials
			this.AddImage(401, 53, 51);
            this.AddLabel(417, 67, 0, @totalPontosHabilidade(jogador));			
			
			//lista de HabilidadeRacials
			listarHabilidades(jogador);
		}

		private void listarHabilidades(Jogador jogador) {

            List<int> habilidades = getHabilidades(jogador);
			int y = 135;	//posicao do primeito HabilidadeRacial
			
			if(habilidades.Count == 0) {
				this.AddLabel(65, y, 0, @"Você não possui habilidade.");
            }

            for (int i = 0; i < habilidades.Count; i++)
            {
                Habilidade habilidade = getHabilidade(habilidades[i]);

                if (habilidade != null)
                {
                    this.AddLabel(65, y + (i * 20), 0, habilidade.Nome);
                    this.AddButton(45, y + (i * 20), 1210, 1209, (habilidade.Id + 1), GumpButtonType.Reply, 1);
                }
                else
                {
                    this.AddLabel(65, y + (i * 20), 0, "Não foi encontrado habilidade com id: " + habilidades[i]);
                }
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
        public abstract List<int> getHabilidades(Jogador jogador);
		
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

        #endregion
    }
}
