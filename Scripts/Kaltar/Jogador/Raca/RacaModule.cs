using System;
using System.Collections.Generic;
using System.Text;
using Server.ACC.CM;
using Server.Mobiles;
using Server;

namespace Kaltar.Raca
{
    public class RacaModule : Module
    {
        #region atributos

        //raca do jogador
        private Race raca;
        
        #endregion

        #region propriedades

        public Race Raca { get { return raca; } set { raca = value; } }

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

        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int versao = reader.ReadInt();

            raca = reader.ReadRace();
        }
        #endregion
    }
}
