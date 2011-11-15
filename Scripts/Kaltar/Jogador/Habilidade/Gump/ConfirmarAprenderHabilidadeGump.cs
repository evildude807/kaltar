using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;
using System.Collections;
using System.Collections.Generic;

namespace Kaltar.Habilidades 
{

    public enum Buttons {
        AprenderHabilidade,
        cancelarAprenderHabilidade,
    }

    public class ConfirmarAprenderHabilidadeGump : Gump {

        private Jogador jogador = null;
        private int IdHabilidade;
        private AprenderHabilidadeGump AprenderHabilidadeGump = null;

        public ConfirmarAprenderHabilidadeGump(Jogador jogador, int IdHabilidade, AprenderHabilidadeGump gump)
            : base(10, 30)
        {

            this.jogador = jogador;
            this.IdHabilidade = IdHabilidade;
            this.AprenderHabilidadeGump = gump;

            this.Closable = false;
            this.Disposable = false;
            this.Dragable = true;
            this.Resizable = false;

            jogador.CloseGump(typeof(ConfirmarAprenderHabilidadeGump));

            this.AddPage(0);
            this.AddBackground(86, 89, 403, 328, 9380);
            this.AddImage(108, 132, 52);
            this.AddLabel(158, 127, 193, @"Confirmação de aprendizado de " + AprenderHabilidadeGump.getTipoHabilidade());

            string descricao = montarDescricao();

            this.AddHtml(146, 170, 304, 173, @descricao, (bool)true, (bool)true);
            
            //acoes
            this.AddButton(215, 353, 247, 249, (int)Buttons.AprenderHabilidade, GumpButtonType.Reply, 0);
            this.AddButton(292, 352, 241, 243, (int)Buttons.cancelarAprenderHabilidade, GumpButtonType.Reply, 0);

        }

        private string montarDescricao()
        {

            Habilidade Habilidade = AprenderHabilidadeGump.getHabilidade(IdHabilidade);

            string descricao = "<u>Habilidade:</u> <b>" + Habilidade.Nome + "</b><br/>" +
                "<u>Nível máximo:</u> <b>" + Habilidade.NivelMaximo + "</b><br/>" + 
                "<u>Descricao:</u> " + (Habilidade.Descricao != null ? Habilidade.Descricao : "Nenhuma descricao.") + "<br/>" +
                "<u>Pre-requisito:</u> " + (Habilidade.PreRequisito != null ? Habilidade.PreRequisito : "Nenhum pre-requisito.");
            return descricao;
        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {

            if (info.ButtonID == (int)Buttons.cancelarAprenderHabilidade)
            {
                jogador.CloseGump(typeof(ConfirmarAprenderHabilidadeGump));
                jogador.SendGump(AprenderHabilidadeGump);
            }
            else if (info.ButtonID == (int)Buttons.AprenderHabilidade)
            {
                bool aprendeu = AprenderHabilidadeGump.aprenderHabilidade(jogador, IdHabilidade);
                Habilidade habilidade = AprenderHabilidadeGump.getHabilidade(IdHabilidade);
                
                /*
                if (aprendeu)
                {
                    jogador.SendMessage("Você aprendeu a " + AprenderHabilidadeGump.getTipoHabilidade() + " \"{0}\"", habilidade.Nome);
                }
                else
                {
                    jogador.SendMessage("Você não pode aprender a " + AprenderHabilidadeGump.getTipoHabilidade() + " \"{0}\"", habilidade.Nome);
                }
                */
            }
        }
    }
}
