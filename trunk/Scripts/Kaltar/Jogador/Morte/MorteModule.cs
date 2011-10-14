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

        //marca se esta desmaiado
        private bool desmaiado;

        //marca se esta morto
        private bool morto;

        //marca o inicio do desmaio
        private DateTime inicioDesmaio;

        //marca o inicio da morte
        private DateTime inicioMorte;

        //local marcado para voltar ao morrer.
        private String localMarcado = "padrao";

        //marca a data do ultimo ponto ganho
        private DateTime dataPontoGanho;
            
        #endregion

        #region atributos, não serializados
        SistemaMorte.TimerMorte tm;
        #endregion

        #region propriedades

        public String LocalMaracado { get { return localMarcado; } set { localMarcado = value; } }

        public int Desmaio { get { return desmaio; } set { desmaio = value > 0 ? value : 0; } }

        public int Morte { get { return morte; } set { morte = value > 0 ? value : 0; } }

        public SistemaMorte.TimerMorte TimerMorte { get { return tm; } set { tm = value; } }

        public bool Desmaiado { get { return desmaiado; } set { desmaiado = value; } }

        public bool Morto { get { return morto; } set { morto = value; } }

        public DateTime InicioDesmaio { get { return inicioDesmaio; } set { inicioDesmaio = value; } }

        public DateTime InicioMorte { get { return inicioMorte; } set { inicioMorte = value; } }

        public DateTime DataPontoGanho { get { return dataPontoGanho; } set { dataPontoGanho = value; } }

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
            writer.Write(desmaiado);
            writer.Write(morto);
            writer.Write((DateTime) inicioDesmaio);
            writer.Write((DateTime) inicioMorte);
            writer.Write(localMarcado);
            writer.Write((DateTime)dataPontoGanho);

        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int versao = reader.ReadInt();

            desmaio = reader.ReadInt();
            morte = reader.ReadInt();
            desmaiado = reader.ReadBool();
            morto = reader.ReadBool();
            inicioDesmaio = reader.ReadDateTime();
            inicioMorte = reader.ReadDateTime();
            dataPontoGanho = reader.ReadDateTime();
            localMarcado = reader.ReadString();
            
        }
        #endregion
    }
        
}
