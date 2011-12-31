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

namespace Server.ACC.CSS.Systems.Cleric
{
    public class UndeadInitializer : BaseInitializer
	{
        public static void Configure()
        {
            Register(typeof(UndeadPoisonMarkSpell), "Poison Mark", "The Necromancer uses this spell to mark runes for travel.", "Daemon Blood; Pig Iron", "Skill: 60; Mana: 30", 2295, 3500, School.Espiritualista);
            Register(typeof(UndeadTravelByPoisonSpell), "Travel By Poison", "The Necromancer uses this spell to travel with the use of a marked rune.", "Bat Wing; Nox Crystal", "Skill: 40; Mana: 11", 2295, 3500, School.Espiritualista);
            Register(typeof(UndeadCurePoisonSpell), "Cure Poison", "Cures most poisons on anyone targeted by this spell.", "Nox Crystal; Pig Iron", "Skill: 15; Mana: 20", 2295, 3500, School.Espiritualista);
            Register(typeof(UndeadPoisonFieldSpell), "Poison Field", "Produces a wall of poison that will poison just about anything that walks through it.", "Bat Wing; Nox Crystal; Pig Iron", "Skill: 45; Mana: 20", 2295, 3500, School.Espiritualista);
            Register(typeof(UndeadGraveyardGatewaySpell), "Graveyard Gateway", "Produces a moongate from the use of a marked rune used for travel.", "Daemon Blood; Bat Wing; Grave Dust", "Skill: 70; Mana: 45", 2295, 3500, School.Espiritualista);
            Register(typeof(UndeadRevivalBySeanceSpell), "Revival By Seance", "Resurrects person of choice by the use of a Ouija board.", "Nox Crystal; Daemon Blood; Grave Dust", "Skill: 90; Mana: 55", 2295, 3500, School.Espiritualista);
            Register(typeof(UndeadHordeMinionsEyesSpell), "Horde Minion's Eyes", "Grants the target the eyes of a horde minion for superior vision to see at night.", "Bat Wing; Nox Crystal", "Skill: 15; Mana: 5", 2295, 3500, School.Espiritualista);
            Register(typeof(UndeadWallOfSpikesSpell), "Wall Of Spikes", "Summons a wall of stone spikes as a shield.", "Daemon Blood; Pig Iron", "Skill: 24; Mana: 10", 2295, 3500, School.Espiritualista);
            Register(typeof(UndeadPoisonIvyPatchSpell), "Poison Ivy Patch", "Forms a circle of poison ivy that causes damage & the ability to poison when walked on.", "Grave Dust; Nox Crystal; Pig Iron", "Skill: 68; Mana: 28", 2295, 3500, School.Espiritualista);
            Register(typeof(UndeadBloodPactSpell), "Blood Pact", "Heals a person of choice draining health & blood from the Necromancer.", "Bat Wing; Pig Iron", "Skill: 33; Mana: 15", 2295, 3500, School.Espiritualista);
            Register(typeof(UndeadPoisonSpell), "Poison", "Poison's the slected target.", "Nox Crystal; Daemon Blood", "Skill: 30; Mana: 20", 2295, 3500, School.Espiritualista);
            Register(typeof(UndeadMassCurseSpell), "Mass Curse", "Lowers the nearby enemeies abilities in combat.", "Bat Wing; Grave Dust; Daemon Blood; Nox Crystal", "Skill: 65; Mana: 25", 2295, 3500, School.Espiritualista);
            Register(typeof(UndeadMisfitsOfMondainSpell), "Misfits Of Mondain", "Summons creatures of the undead to aid in battle.", "Bat Wing; Grave Dust; Nox Crystal", "Skill: 37; Mana: 30", 2295, 3500, School.Espiritualista);
        }
	}
}