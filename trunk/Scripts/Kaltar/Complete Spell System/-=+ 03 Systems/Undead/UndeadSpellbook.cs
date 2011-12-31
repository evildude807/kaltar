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
using Server.Items;
using Server.Spells;

namespace Server.ACC.CSS.Systems.Cleric {

    public class UndeadSpellbook : CSpellbook {
        public override School School { get { return School.Espiritualista; } }

        [Constructable]
        public UndeadSpellbook()
            : this((ulong)0, CSSettings.FullSpellbooks)
        {
        }

        [Constructable]
        public UndeadSpellbook(bool full)
            : this((ulong)0, full)
        {
        }

        [Constructable]
        public UndeadSpellbook(ulong content, bool full)
            : base(content, 0xEFA, full)
        {
            Hue = 0x1F0;
            Name = "Livro de Magia Morto-Vivo";
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.AccessLevel == AccessLevel.Player)
            {
                Container pack = from.Backpack;
                if (!(Parent == from || (pack != null && Parent == pack)))
                {
                    from.SendMessage("O livro deve estar na sua mochila para ser aberto. Não pode estar dentro de outro container.");
                    return;
                }
                else if (SpellRestrictions.UseRestrictions && !SpellRestrictions.CheckRestrictions(from, this.School))
                {
                    return;
                }
            }

            from.CloseGump(typeof(UndeadSpellbookGump));
            from.SendGump(new UndeadSpellbookGump(this));
        }
    }
}
	
