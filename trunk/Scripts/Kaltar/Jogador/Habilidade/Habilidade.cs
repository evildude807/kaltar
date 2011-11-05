using System;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.Mobiles;

namespace Kaltar.Habilidades {
    
    public abstract class Habilidade {

        #region atributos

        protected int id;
        protected string nome;
        protected string descricao;
        protected string preRequisito;
        protected int nivelMaximo = 1;

        #endregion
        
        #region propriedade

        public int Id { get { return id; } }
        public int NivelMaximo { get { return nivelMaximo; } }
        public string Nome { get { return nome; } }
        public string Descricao { get { return descricao; } }
        public string PreRequisito { get { return preRequisito; } }

        #endregion

        #region metodos

        protected Habilidade() {
        }
        
        protected Habilidade(int id, int nivelMaximo, string nome, string descricao, string prerequisito)
        {
            this.id = id;
            this.nivelMaximo = nivelMaximo;
            this.nome = nome;
            this.preRequisito = prerequisito;
        }

        public abstract bool PossuiPreRequisitos(Jogador jogador);

        #endregion

    }

}
