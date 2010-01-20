using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Gumps.Kaltar;

namespace Server.Gumps.Kaltar
{
	public class EditarPropriedades : Gump
	{
		public EditarPropriedades(Jogador jogador, string chave) : base( 0, 0 )
		{
			this.Closable=false;
			this.Disposable=false;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(62, 76, 369, 253, 9380);
			this.AddImage(86, 117, 52);
			this.AddLabel(139, 112, 5, @"Editar propriedade");
			this.AddLabel(102, 192, 0, @"Chave:");
			this.AddLabel(155, 191, 43, @"<valor da chave>");
			this.AddLabel(105, 222, 0, @"Valor:");
			this.AddTextEntry(158, 222, 229, 20, 43, (int)Buttons.teValor, @"");
			this.AddButton(120, 266, 247, 248, (int)Buttons.bEditar, GumpButtonType.Reply, 0);
			this.AddButton(212, 266, 241, 242, (int)Buttons.bCancelar, GumpButtonType.Reply, 0);
			this.AddButton(295, 270, 2463, 2464, (int)Buttons.bDeletar, GumpButtonType.Reply, 0);

		}
		
		public enum Buttons
		{
			teValor,
			bEditar,
			bCancelar,
			bDeletar,
		}

	}
}