using System;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.ACC.CM;
using Server.Commands;
using Server.Mobiles;
using Kaltar.Habilidades;

namespace Kaltar.Talentos {

    public class TalentoModule : Module {

        #region atributos

        //armazena todos os talentos do jogadore
        private Dictionary<IdHabilidadeTalento, HabilidadeNode> habilidades = new Dictionary<IdHabilidadeTalento, HabilidadeNode>();

        #endregion

        #region propriedades

        public Dictionary<IdHabilidadeTalento, HabilidadeNode> Habilidades { get { return habilidades; } }

        #endregion

        #region sobrecarga
        public override string Name() { return "Talento Module"; }

        public TalentoModule(Serial serial)
            : base(serial)
        {
            
        }

        public override void Append(Module mod, bool negatively)
        {

        }

        #endregion

        #region serialização

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);			//verso		

            //serializa os objetivos
            writer.Write(habilidades.Count);	// numero de objetivos
            foreach (HabilidadeNode habilidade in habilidades.Values)
            {
                habilidade.Serialize(writer);
            }

        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int versao = reader.ReadInt();

            //recuperas as habilidades
            habilidades = new Dictionary<IdHabilidadeTalento, HabilidadeNode>();
            int numHabilidade = reader.ReadInt();
            for (int i = 0; i < numHabilidade; i++)
            {
                HabilidadeNode hn = new HabilidadeNode();
                hn.Deserialize(reader);

                habilidades.Add((IdHabilidadeTalento)hn.Id, hn);
            }
        }

        #endregion
    }
        
}
