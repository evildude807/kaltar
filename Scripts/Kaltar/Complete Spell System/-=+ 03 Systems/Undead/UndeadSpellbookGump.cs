/*
Special thanks to Ryan.
With RunUO we now have the ability to become our own Richard Garriott.
All Spells System created by x-SirSly-x, Admin of Land of Obsidian.
All Spells System 4.0 created & supported by Lucid Nagual, Admin of The Conjuring.
All Spells System 5.0 created by A_li_N.
All Spells Optional Restrictive System created by Alien, Daat99 and Lucid Nagual.
    _________________________________
 -=(_)_______________________________)=-
   /   .   .   . ____  . ___      _/
  /~ /    /   / /     / /   )2005 /
 (~ (____(___/ (____ / /___/     (
  \ ----------------------------- \
   \     lucidnagual@gmail.com     \
    \_     ===================      \
     \   -Admin of "The Conjuring"-  \
      \_     ===================     ~\
       )       All Spells System       )
      /~     Version [5].0 & Above   _/
    _/_______________________________/
 -=(_)_______________________________)=-
 */

using System;

namespace Server.ACC.CSS.Systems.Cleric
{
    public class UndeadSpellbookGump : CSpellbookGump {

        public override string TextHue { get { return "2307"; } }
        public override int BGImage { get { return 11008; } }
        public override int SpellBtn { get { return 2362; } }
        public override int SpellBtnP { get { return 2361; } }
        public override string Label1 { get { return "Morto-Vivo"; } }
        public override string Label2 { get { return "Resas"; } }
        public override Type GumpType { get { return typeof(UndeadSpellbookGump); } }

        public UndeadSpellbookGump(UndeadSpellbook book)
            : base(book)
        {
        }
	}
}
