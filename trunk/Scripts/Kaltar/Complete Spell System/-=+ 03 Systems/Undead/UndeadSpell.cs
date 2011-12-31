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
using Server;
using Server.Spells;
using Server.Network;

namespace Server.ACC.CSS.Systems.Cleric
{
    public abstract class UndeadSpell : KaltarSpell {

        public UndeadSpell(Mobile caster, Item scroll, SpellInfo info) : base(caster, scroll, info) { }
        public override SkillName CastSkill { get { return SkillName.Necromancy; } }
        public override SkillName DamageSkill { get { return SkillName.SpiritSpeak; } }
    }
}