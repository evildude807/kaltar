using System;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.ACC.CM;
using Server.Commands;
using Server.Mobiles;

namespace Kaltar.Morte {

    public class MorteModule : Module {

        #region atributos

        //marca quantas vezes ja desmaiou.
        private int desmaio = 0;

        //marca quantas vezes ja morreu
        private int morte = 0;

        //marca o inicio do desmaio
        private DateTime inicioDesmaio;

        //marca o inicio da morte
        private DateTime inicioMorte;

        //colocar o local marcado para voltar ao morrer.
                    
        #endregion

        #region atributos, não serializados
        TimerMorte tm;
        #endregion

        #region propriedades

        public int Desmaio { get { return desmaio; } set { desmaio = value; } }

        public int Morte { get { return morte; } set { morte = value; } }

        public TimerMorte TimerMorte { get { return tm; } set { tm = value; } }

        public DateTime InicioDesmaio { get { return inicioDesmaio; } set { inicioDesmaio = value; } }

        public DateTime InicioMorte { get { return inicioMorte; } set { inicioMorte = value; } }

        #endregion

        #region sobrecarga
        public override string Name() { return "Morte Module"; }

        public MorteModule(Serial serial)
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

            writer.Write((int)desmaio);
            writer.Write((int)morte);
            writer.Write((DateTime) inicioDesmaio);
            writer.Write((DateTime) inicioMorte);

        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int versao = reader.ReadInt();

            desmaio = reader.ReadInt();
            morte = reader.ReadInt();
            inicioDesmaio = reader.ReadDateTime();
            inicioMorte = reader.ReadDateTime();
            
        }
        #endregion
    }
        
}
