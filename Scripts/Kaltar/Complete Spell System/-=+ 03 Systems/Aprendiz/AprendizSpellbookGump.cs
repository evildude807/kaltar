using System;

namespace Server.ACC.CSS.Systems.Aprendiz
{
	public class AprendizSpellbookGump : CSpellbookGump
	{
		public override string TextHue  { get{ return "336666"; } }
		public override int    BGImage  { get{ return 2203; } }
		public override int    SpellBtn { get{ return 2362; } }
		public override int    SpellBtnP{ get{ return 2361; } }
		public override string Label1   { get{ return "Aprendiz"; } }
		public override string Label2   { get{ return "Magias"; } }
		public override Type   GumpType { get{ return typeof( AprendizSpellbookGump ); } }

		public AprendizSpellbookGump( AprendizSpellbook book ) : base( book ) {
		}
	}
}
