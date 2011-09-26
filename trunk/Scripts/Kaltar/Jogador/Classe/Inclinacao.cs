using System;
using System.Collections.Generic;
using System.Text;
using Server.Mobiles;
using Kaltar.Util;

namespace Kaltar.Classes {

    public class Inclinacao {
        
        public static void combate(Jogador jogador)
        {
            SkillUtil.zerarSkills(jogador);

            jogador.Skills.Swords.Base = 30;
            jogador.Skills.Macing.Base = 30;
            jogador.Skills.Fencing.Base = 30;
            jogador.Skills.Begging.Base = 30;
            jogador.Skills.Wrestling.Base = 30;
            jogador.Skills.Archery.Base = 10;
            jogador.Skills.Tactics.Base = 30;
            jogador.Skills.Parry.Base = 30;
        }

        public static void feiticaria(Jogador jogador)
        {
            SkillUtil.zerarSkills(jogador);

            jogador.Skills.Magery.Base = 30;
            jogador.Skills.Meditation.Base = 30;
            jogador.Skills.MagicResist.Base = 30;
            jogador.Skills.Inscribe.Base = 10;
            jogador.Skills.Macing.Base = 20;
        }

        public static void subterfugio(Jogador jogador)
        {
            SkillUtil.zerarSkills(jogador);

            jogador.Skills.Fencing.Base = 25;
            jogador.Skills.Archery.Base = 25;
            jogador.Skills.Wrestling.Base = 20;
            jogador.Skills.Tactics.Base = 20;
            jogador.Skills.DetectHidden.Base = 30;
            jogador.Skills.Hiding.Base = 30;
            jogador.Skills.RemoveTrap.Base = 30;
            jogador.Skills.Lockpicking.Base = 30;
            jogador.Skills.Stealing.Base = 30;
            jogador.Skills.Snooping.Base = 30;
            jogador.Skills.Stealth.Base = 30;
        }

        public static void clericato(Jogador jogador)
        {
            SkillUtil.zerarSkills(jogador);

            jogador.Skills.Meditation.Base = 30;
            jogador.Skills.Macing.Base = 20;
            jogador.Skills.Tactics.Base = 10;
            jogador.Skills.Healing.Base = 30;
            jogador.Skills.Veterinary.Base = 30;
            jogador.Skills.SpiritSpeak.Base = 30;
            jogador.Skills.Parry.Base = 10;
        }
    }
}
