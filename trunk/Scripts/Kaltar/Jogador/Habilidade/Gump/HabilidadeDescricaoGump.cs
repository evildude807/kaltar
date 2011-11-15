using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;
using System.Collections;
using System.Collections.Generic;

namespace Kaltar.Habilidades 
{

    public class HabilidadeDescricaoGump : Gump {

        private Jogador jogador = null;
        private int IdHabilidade;
        private HabilidadeGump habilidadeGump = null;

        public HabilidadeDescricaoGump(Jogador jogador, int IdHabilidade, HabilidadeGump gump)
            : base(10, 30)
        {

            this.jogador = jogador;
            this.IdHabilidade = IdHabilidade;
            this.habilidadeGump = gump;

            this.Closable = false;
            this.Disposable = false;
            this.Dragable = true;
            this.Resizable = false;

            jogador.CloseGump(typeof(HabilidadeDescricaoGump));

            this.AddPage(0);
            this.AddBackground(86, 89, 403, 328, 9380);
            this.AddImage(108, 132, 52);
            this.AddLabel(158, 127, 193, @"Descrição de " + habilidadeGump.getTipoHabilidade());

            string descricao = montarDescricao();

            this.AddHtml(146, 170, 304, 173, @descricao, (bool)true, (bool)true);
            
            //acoes
            this.AddButton(292, 352, 241, 243, (int)Buttons.cancelarAprenderHabilidade, GumpButtonType.Reply, 0);

        }

        private string montarDescricao()
        {

            Habilidade Habilidade = habilidadeGump.getHabilidade(IdHabilidade);

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
                jogador.CloseGump(typeof(HabilidadeGump));
                jogador.SendGump(habilidadeGump);
            }

        }
    }
}
