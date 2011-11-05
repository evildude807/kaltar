using System;
using System.Collections.Generic;
using System.Text;
using Server;

namespace Kaltar.Habilidades {

    public class HabilidadeNode {

        #region atributo

        private int id;
        private int nivel;

        #endregion

        #region propriedade

        public int Id { get { return id; } }
        public int Nivel { get { return nivel; } }

        #endregion

        #region metodo

        public HabilidadeNode()
        {
        }

        public HabilidadeNode(int id, int nivel)
        {
            this.id = id;
            this.nivel = nivel;
        }

        public void aumentarNivel()
        {
            nivel++;
        }

        #endregion

        #region serialização

        public virtual void Serialize(GenericWriter writer)
        {
            writer.Write(0);			//verso		
            writer.Write(id);
            writer.Write(nivel);
        }

        public virtual void Deserialize(GenericReader reader)
        {
            int versao = reader.ReadInt();
            id = reader.ReadInt();
            nivel = reader.ReadInt();
        }

        #endregion
    }
}

