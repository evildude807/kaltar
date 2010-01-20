using System;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Regions;

namespace Scripts.Invasion_System
{
    public static class InvasionSpawnLists
    {
        private static Type[] m_InvasionAntSpawn = new Type[] { typeof(InvasionAntLion), typeof(InvasionBlackSolenInfiltratorQueen), typeof(InvasionBlackSolenInfiltratorWarrior), typeof(InvasionBlackSolenQueen), typeof(InvasionBlackSolenWarrior), typeof(InvasionBlackSolenWorker), typeof(InvasionRedSolenInfiltratorQueen), typeof(InvasionRedSolenInfiltratorWarrior), typeof(InvasionRedSolenQueen), typeof(InvasionRedSolenWarrior), typeof(InvasionRedSolenWorker) };
        public static Type[] InvasionAntSpawn
        {
            get { return m_InvasionAntSpawn; }
        }

        private static Type[] m_InvasionAOSSpawn = new Type[] { typeof(InvasionCrystalElemental), typeof(InvasionFleshGolem), typeof(InvasionGibberling), typeof(InvasionGoreFiend), typeof(InvasionMoundOfMaggots), typeof(InvasionPatchworkSkeleton), typeof(InvasionRavager), typeof(InvasionVampireBat), typeof(InvasionWailingBanshee), typeof(InvasionWandererOfTheVoid) };
        public static Type[] InvasionAOSSpawn
        {
            get { return m_InvasionAOSSpawn; }
        }

        private static Type[] m_InvasionArachnidSpawn = new Type[] { typeof(InvasionDreadSpider), typeof(InvasionFrostSpider), typeof(InvasionGiantBlackWidow), typeof(InvasionGiantSpider), typeof(InvasionTerathanAvenger), typeof(InvasionTerathanDrone), typeof(InvasionTerathanMatriarch), typeof(InvasionTerathanWarrior) };
        public static Type[] InvasionArachnidSpawn
        {
            get { return m_InvasionArachnidSpawn; }
        }

        private static Type[] m_InvasionElementalSpawn = new Type[] { typeof(InvasionAirElemental), typeof(InvasionBloodElemental), typeof(InvasionEarthElemental), typeof(InvasionEfreet), typeof(InvasionFireElemental), typeof(InvasionIceElemental), typeof(InvasionPoisonElemental), typeof(InvasionSnowElemental), typeof(InvasionToxicElemental), typeof(InvasionWaterElemental) };
        public static Type[] InvasionElementalSpawn
        {
            get { return m_InvasionElementalSpawn; }
        }

        private static Type[] m_InvasionExodusSpawn = new Type[] { typeof(InvasionExodusMinion), typeof(InvasionExodusOverseer) };
        public static Type[] InvasionExodusSpawn
        {
            get { return m_InvasionExodusSpawn; }
        }

        private static Type[] m_InvasionJukaSpawn = new Type[] { typeof(InvasionJukaLord), typeof(InvasionJukaMage), typeof(InvasionJukaWarrior) };
        public static Type[] InvasionJukaSpawn
        {
            get { return m_InvasionJukaSpawn; }
        }

        private static Type[] m_InvasionMiscSpawn = new Type[] { typeof(InvasionCentaur), typeof(InvasionDarkWisp), typeof(InvasionPlagueBeast), typeof(InvasionSandVortex), typeof(InvasionShadowWisp), typeof(InvasionSlime), typeof(InvasionWisp) };
        public static Type[] InvasionMiscSpawn
        {
            get { return m_InvasionMiscSpawn; }
        }

        private static Type[] m_InvasionOrcSpawn = new Type[] { typeof(InvasionOrc), typeof(InvasionOrcBomber), typeof(InvasionOrcCaptain), typeof(InvasionOrcishLord), typeof(InvasionOrcishMage) };
        public static Type[] InvasionOrcSpawn
        {
            get { return m_InvasionOrcSpawn; }
        }

        private static Type[] m_InvasionPlantSpawn = new Type[] { typeof(InvasionBogling), typeof(InvasionBogThing), typeof(InvasionQuagmire), typeof(InvasionSwampTentacle), typeof(InvasionTreefellow), typeof(InvasionWhippingVine) };
        public static Type[] InvasionPlantSpawn
        {
            get { return m_InvasionPlantSpawn; }
        }

        private static Type[] m_InvasionRatmanSpawn = new Type[] { typeof(InvasionRatman), typeof(InvasionRatmanArcher), typeof(InvasionRatmanMage) };
        public static Type[] InvasionRatmanSpawn
        {
            get { return m_InvasionRatmanSpawn; }
        }

        private static Type[] m_InvasionReptileSpawn = new Type[] { typeof(InvasionDragon), typeof(InvasionDrake), typeof(InvasionLizardman), typeof(InvasionOphidianArchmage), typeof(InvasionOphidianKnight), typeof(InvasionOphidianMage), typeof(InvasionOphidianMatriarch), typeof(InvasionOphidianWarrior), typeof(InvasionWhiteWyrm), typeof(InvasionWyvern) };
        public static Type[] InvasionReptileSpawn
        {
            get { return m_InvasionReptileSpawn; }
        }
    }
}
