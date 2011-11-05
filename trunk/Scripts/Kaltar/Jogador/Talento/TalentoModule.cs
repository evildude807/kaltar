using System;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.ACC.CM;
using Server.Commands;
using Server.Mobiles;

namespace Kaltar.Talentos {

    public class TalentoModule : Module {

        public static void Initialize()
        {
            CommandSystem.Register("testetalento", AccessLevel.Player, new CommandEventHandler(testeTalento_OnCommand));
        }

        private static void testeTalento_OnCommand(CommandEventArgs e)
        {
            TalentoModule tm = (TalentoModule) CentralMemory.GetModule(e.Mobile.Serial, typeof(TalentoModule));
            if (tm == null)
            {
                e.Mobile.SendMessage("Voce nao tem o Talento module.");
                
                CentralMemory.AddModule(new TalentoModule(e.Mobile.Serial));
                e.Mobile.SendMessage("Foi adicionado o talento module.");
            }
            else
            {
                e.Mobile.SendMessage("Voce ja tem o talento module.");
            }
        }

        #region atributos
        
        //armazena todos os talentos do jogadore
        private Dictionary<IDTalento, IDTalento> talentos = new Dictionary<IDTalento, IDTalento>();
        
        //pontos gastos de talentos
        private int pontosGastos = 0;

        #endregion

        #region propriedades

        public int PontosGastos { get { return pontosGastos; } set { pontosGastos = value; } }

        public Dictionary<IDTalento, IDTalento> Talentos { get { return talentos; } }

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

            //Console.WriteLine( "num talentos: {0}", talentos.Count);

            writer.Write((int)pontosGastos);

            //serializa os objetivos
            writer.Write(talentos.Count);	// nmero de objetivos
            foreach (IDTalento idTalento in talentos.Values)
            {
                writer.Write((int)idTalento);
            }
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int versao = reader.ReadInt();

            pontosGastos = reader.ReadInt();

            int numTalentos = reader.ReadInt();

            //Console.WriteLine( "num talentos: {0}", numTalentos);

            //recuperas os objectivos
            talentos = new Dictionary<IDTalento, IDTalento>();
            for (int i = 0; i < numTalentos; i++)
            {
                IDTalento idTalento = (IDTalento)reader.ReadInt();
                talentos.Add(idTalento, idTalento);
            }
        }
        #endregion
    }
        
}
