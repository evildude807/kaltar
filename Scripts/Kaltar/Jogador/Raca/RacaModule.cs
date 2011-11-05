using System;
using System.Collections.Generic;
using System.Text;
using Server.ACC.CM;
using Server.Mobiles;
using Server;
using Kaltar.Habilidades;

namespace Kaltar.Raca
{
    public class RacaModule : Module
    {
        #region atributos

        //raca do jogador
        private Race raca;

        //armazena todos os talentos do jogadore
        private Dictionary<IdHabilidadeRacial, HabilidadeNode> habilidades = new Dictionary<IdHabilidadeRacial, HabilidadeNode>();

        #endregion

        #region propriedades

        public Race Raca { get { return raca; } set { raca = value; } }

        public Dictionary<IdHabilidadeRacial, HabilidadeNode> Habilidades { get { return habilidades; } }

        #endregion

        #region sobrecarga
        public override string Name() { return "Raca Module"; }

        public RacaModule(Serial serial)
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

            writer.Write(raca);

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

            raca = reader.ReadRace();

            //recuperas as habilidades
            habilidades = new Dictionary<IdHabilidadeRacial, HabilidadeNode>();
            int numHabilidade = reader.ReadInt();
            for (int i = 0; i < numHabilidade; i++)
            {
                HabilidadeNode hn = new HabilidadeNode();
                hn.Deserialize(reader);

                habilidades.Add((IdHabilidadeRacial)hn.Id, hn);
            }
        }

        #endregion
    }
}
