using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Regions;
using Server.Commands;
using Server.Spells;

namespace Scripts.Invasion_System
{
    public class InvasionController : Item
    {
        public bool m_InvasionStarted;
        [CommandProperty(AccessLevel.Counselor)]
        public bool InvasionStarted
        {
            get { return m_InvasionStarted; }
            set { return; }
        }

        private InvasionSpawnType m_InvasionTypeSpawn;
        [CommandProperty(AccessLevel.Counselor)]
        public InvasionSpawnType InvasionTypeSpawn
        {
            get { return m_InvasionTypeSpawn; }
            set { m_InvasionTypeSpawn = value; }
        }

        private int m_RequiredKillCount;
        [CommandProperty(AccessLevel.Counselor)]
        public int RequiredKillCount
        {
            get { return m_RequiredKillCount; }
            set { m_RequiredKillCount = value; }
        }

        private int m_SpawnDensity;
        [CommandProperty(AccessLevel.Counselor)]
        public int SpawnDensity
        {
            get { return m_SpawnDensity; }
            set { m_SpawnDensity = value; }
        }

        private int m_Kills;
        [CommandProperty(AccessLevel.Counselor)]
        public int Kills
        {
            get { return m_Kills; }
            set { m_Kills = value; }
        }

        private int m_Reward;
        [CommandProperty(AccessLevel.Counselor)]
        public int Reward
        {
            get { return m_Reward; }
            set { m_Reward = value; }
        }

        private InvasionRegion m_RegisteredInvasionRegion;
        public InvasionRegion RegisteredInvasionRegion
        {
            get { return m_RegisteredInvasionRegion; }
            set { m_RegisteredInvasionRegion = value; }
        }

        private Point3D m_InvasionOriginatesFrom;
        [CommandProperty(AccessLevel.Counselor)]
        public Point3D InvasionOriginatesFrom
        {
            get { return m_InvasionOriginatesFrom; }
            set { m_InvasionOriginatesFrom = value; }
        }

        private Point3D m_InvasionLocationTarget;
        [CommandProperty(AccessLevel.Counselor)]
        public Point3D InvasionLocationTarget
        {
            get { return m_InvasionLocationTarget; }
            set { m_InvasionLocationTarget = value; }
        }

        private Rectangle2D m_InvasionRegionBounds;
        [CommandProperty(AccessLevel.Counselor)]
        public Rectangle2D InvasionRegionBounds
        {
            get { return m_InvasionRegionBounds; }
            set { m_InvasionRegionBounds = value; }
        }

        private Map m_InvasionMap;
        [CommandProperty(AccessLevel.Counselor)]
        public Map InvasionMap
        {
            get { return m_InvasionMap; }
            set { m_InvasionMap = value; }
        }

        private List<InvasionSpawn> m_SpawnList;
        public List<InvasionSpawn> SpawnList
        {
            get { return m_SpawnList; }
            set { m_SpawnList = value; }
        }

        private Dictionary<PlayerMobile, int> m_Players;
        public Dictionary<PlayerMobile, int> Players
        {
            get { return m_Players; }
            set { m_Players = value; }
        }

        public static void Initialize()
        {
            List<InvasionController> controllers = new List<InvasionController>();
            foreach (Item item in World.Items.Values)
                if (item is InvasionController)
                    controllers.Add((InvasionController)item);
            for (int i = 0; i < controllers.Count; ++i)
            {
                InvasionController controller = (InvasionController)controllers[i];
                controller.Invasion_Tick();
                ControllerCount += 1;
            }

            InvasionEventSink.InvasionSpawnMovedEvent += new InvasionEventSink.InvasionSpawnMoved(OnInvasionSpawnMoved);
            InvasionEventSink.InvasionSpawnDiedEvent += new InvasionEventSink.InvasionSpawnDied(OnInvasionSpawnDied);
            InvasionEventSink.InvasionSpawnKilledEvent += new InvasionEventSink.InvasionSpawnKilled(OnInvasionSpawnKilled);
            InvasionEventSink.InvasionSpawnDeletedEvent += new InvasionEventSink.InvasionSpawnDeleted(OnInvasionSpawnDeleted);
        }

        public static void OnInvasionSpawnKilled(InvasionEventSink.InvasionSpawnKilledEventArgs args)
        {
            if (args.Controller == null || args.Killer == null)
                return;
            if (!args.Controller.InvasionStarted)
                return;

            if (args.Killer is PlayerMobile)
            {
                if (args.Controller.Players.ContainsKey((PlayerMobile)args.Killer))
                    args.Controller.Players[(PlayerMobile)args.Killer] += 1;
                else
                    args.Controller.Players.Add((PlayerMobile)args.Killer, 1);
            }
        }

        public static void OnInvasionSpawnMoved(InvasionEventSink.InvasionSpawnMovedEventArgs args)
        {
            if (!args.Controller.InvasionStarted)
                return;

            if(!args.Controller.RegisteredInvasionRegion.InRegion((Mobile)args.Spawn))
                args.Spawn.Delete();

            if (args.Spawn.CheckReachedTarget())
                args.Spawn.RangeHome = InvasionConfig.InvasionSpawnWanderFromTargetRange;
        }

        public static void OnInvasionSpawnDied(InvasionEventSink.InvasionSpawnDiedEventArgs args)
        {
            if (!args.Controller.InvasionStarted)
                return;

            args.Controller.Kills += 1;
            args.Controller.SpawnList.Remove(args.Spawn);
        }

        public static void OnInvasionSpawnDeleted(InvasionEventSink.InvasionSpawnDeletedEventArgs args)
        {
            if (!args.Controller.InvasionStarted)
                return;

            args.Controller.SpawnList.Remove(args.Spawn);
        }

        public static int ControllerCount;
        [Constructable]
        public InvasionController()
            : base(0xEDD)
        {
            Name = String.Format("DefaultInvasion{0}", ControllerCount.ToString());
            Visible = false;
            Movable = false;

            ControllerCount += 1;
            InvasionEventSink.OnInvasionControllerCreated(new InvasionEventSink.InvasionControllerCreatedEventArgs(this));
        }

        public void Invasion_Tick()
        {
            if (m_InvasionStarted)
            {
                if (InvasionConfig.Enabled)
                {
                    if (m_SpawnList == null || m_RegisteredInvasionRegion == null || m_Players == null)
                        StartInvasion();

                    if (m_SpawnList.Count < m_SpawnDensity)
                        SpawnInvasion();

                    if (m_Kills >= m_RequiredKillCount)
                        EndInvasion(InvasionEventSink.InvasionEndedEventArgs.InvasionEndedReason.KillCountReached);
                }
            }

            InvasionEventSink.OnInvasionTicked(new InvasionEventSink.InvasionTickedEventArgs(this));
            Timer.DelayCall(InvasionConfig.InvasionTickRate, new TimerCallback(Invasion_Tick));
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!InvasionConfig.Enabled)
                return;

            if (from is PlayerMobile)
            {
                PlayerMobile player = (PlayerMobile)from;
                if (player.AccessLevel >= InvasionConfig.RequiredLevelToStart)
                {
                    if (!m_InvasionStarted)
                    {
                        if (CanStartInvasion(player))
                            StartInvasion();
                    }
                    else
                    {
                        if (CanEndInvasion(player))
                            EndInvasion(InvasionEventSink.InvasionEndedEventArgs.InvasionEndedReason.DoubleClickEnded);
                    }
                }
            }

            base.OnDoubleClick(from);
        }

        public bool CanStartInvasion(PlayerMobile from)
        {
            if (m_InvasionRegionBounds.Height == 0 || m_InvasionRegionBounds.Width == 0)
            {
                from.SendMessage("InvasionRegionBounds does not appear to be set.");
                return false;
            }

            if (m_InvasionMap == null)
            {
                from.SendMessage("InvasionMap does not appear to be set.");
                return false;
            }

            if (m_InvasionOriginatesFrom.X == 0 || m_InvasionOriginatesFrom.Y == 0)
            {
                from.SendMessage("InvasionOriginatesFrom does not appear to be set.");
                return false;
            }

            if (m_InvasionLocationTarget.X == 0 || m_InvasionLocationTarget.Y == 0)
            {
                from.SendMessage("InvasionLocationTarget does not appear to be set.");
                return false;
            }

            if (m_RegisteredInvasionRegion == null)
                m_RegisteredInvasionRegion = new InvasionRegion(this);

            m_RegisteredInvasionRegion.Register();
            if (Region.Find(m_InvasionOriginatesFrom, m_InvasionMap) != m_RegisteredInvasionRegion)
            {
                from.SendMessage("InvasionOriginatesFrom must be a point in the invasion region.");
                return false;
            }

            if (Region.Find(m_InvasionLocationTarget, m_InvasionMap) != m_RegisteredInvasionRegion)
            {
                from.SendMessage("InvasionLocationTarget must be a point in the invasion region.");
                return false;
            }
            m_RegisteredInvasionRegion.Unregister();

            if (m_RequiredKillCount == 0)
            {
                from.SendMessage("RequiredKillCount does not appear to be set.");
                return false;
            }

            if (m_SpawnDensity == 0)
            {
                from.SendMessage("SpawnDensity does not appear to be set.");
                return false;
            }

            from.SendMessage("Invasion Started!");
            return true;
        }

        public void StartInvasion()
        {
            if (m_SpawnList == null)
                m_SpawnList = new List<InvasionSpawn>();
            if (m_RegisteredInvasionRegion == null)
                m_RegisteredInvasionRegion = new InvasionRegion(this);
            if (m_Players == null)
                m_Players = new Dictionary<PlayerMobile, int>();

            m_InvasionStarted = true;
            InvasionEventSink.OnInvasionStarted(new InvasionEventSink.InvasionStartedEventArgs(this));
        }

        public bool CanEndInvasion(PlayerMobile from)
        {
            from.SendMessage("Invasion Ended!");
            return true;
        }

        public void EndInvasion(InvasionEventSink.InvasionEndedEventArgs.InvasionEndedReason Reason)
        {
            m_InvasionStarted = false;

            List<InvasionSpawn> ToDelete = new List<InvasionSpawn>();
            for (int i = 0; i < m_SpawnList.Count; ++i)
                ToDelete.Add((InvasionSpawn)m_SpawnList[i]);

            foreach(Mobile m in World.Mobiles.Values)
                if (m is InvasionSpawn)
                    if(((InvasionSpawn)m).Controller == this)
                        ToDelete.Add((InvasionSpawn)m);

            for(int i = 0; i < ToDelete.Count; ++i)
            {
                InvasionSpawn deleting = (InvasionSpawn)ToDelete[i];
                deleting.Delete();
            }

            if (Reason == InvasionEventSink.InvasionEndedEventArgs.InvasionEndedReason.KillCountReached)
            {
                Console.WriteLine("Player Count:" + m_Players.Count.ToString());
                IEnumerator key = m_Players.Keys.GetEnumerator();
                for (int i = 0; i < m_Players.Count; ++i)
                {
                    key.MoveNext();
                    int kills = (int)m_Players[(PlayerMobile)key.Current];
                    int percent = (kills / m_RequiredKillCount) * 100;
                    if (percent > 100)
                        percent = 100;

                    int reward = (percent * m_Reward) / 100;

                    if (reward < 1)
                        reward = 1;

                    if(reward > 60000)
                        reward = 60000;

                    Console.WriteLine("Kills:" + kills.ToString());
                    Console.WriteLine("Percent:" + percent.ToString());

                    Item givenreward;
                    if (reward >= 5000)
                        givenreward = new BankCheck(reward);
                    else
                        givenreward = new Gold(reward);

                    BankBox bank = (BankBox)((PlayerMobile)key.Current).BankBox;
                    bank.AddItem(givenreward);

                    ((PlayerMobile)key.Current).SendMessage(String.Format("The invasion has ended! You have been rewarded {0}gp!", reward.ToString()));
                }
            }

            m_SpawnList.Clear();
            m_SpawnList = null;

            m_Players.Clear();
            m_Players = null;
            m_Kills = 0;

            InvasionEventSink.OnInvasionEnded(new InvasionEventSink.InvasionEndedEventArgs(this, Reason));
        }

        public void SpawnInvasion()
        {
            int NumberToSpawn = (m_SpawnDensity - m_SpawnList.Count);
            for(int i = 0; i < NumberToSpawn; ++i)
            {
                bool foundpointmap = false;
                int attempts = 0;
                Point3D loc = new Point3D();
                Map map = null;
                while (!foundpointmap)
                {
                    object[] found = SpawnPointMap(OffsetPointFound(), m_InvasionMap);
                    if (found != null)
                    {
                        loc = (Point3D)found[0];
                        map = (Map)found[1];
                        foundpointmap = true;
                    }

                    attempts += 1;
                    if (attempts >= InvasionConfig.InvasionSpawnAttempts)
                    {
                        if (NumberToSpawn > 0)
                            NumberToSpawn -= 1;
                    }
                }

                InvasionSpawn spawned = CreateNewInvasionSpawn();
                if (spawned != null)
                {
                    spawned.Home = m_InvasionLocationTarget;
                    spawned.RangeHome = 0;
                    spawned.Location = loc;
                    spawned.Map = map;
                    m_SpawnList.Add(spawned);
                }
            }
        }

        public Point3D OffsetPointFound()
        {
            int i = Utility.Random(1, 4);
            switch (i)
            {
                case 1:
                    {
                        int x = m_InvasionOriginatesFrom.X + Utility.Random(InvasionConfig.SpawnPointRange);
                        int y = m_InvasionOriginatesFrom.Y + Utility.Random(InvasionConfig.SpawnPointRange);
                        int z = Map.GetAverageZ(x, y);
                        return new Point3D(x, y, z);
                    }
                case 2:
                    {
                        int x = m_InvasionOriginatesFrom.X - Utility.Random(InvasionConfig.SpawnPointRange);
                        int y = m_InvasionOriginatesFrom.Y - Utility.Random(InvasionConfig.SpawnPointRange);
                        int z = Map.GetAverageZ(x, y);
                        return new Point3D(x, y, z);
                    }
                case 3:
                    {
                        int x = m_InvasionOriginatesFrom.X + Utility.Random(InvasionConfig.SpawnPointRange);
                        int y = m_InvasionOriginatesFrom.Y - Utility.Random(InvasionConfig.SpawnPointRange);
                        int z = Map.GetAverageZ(x, y);
                        return new Point3D(x, y, z);
                    }
                case 4:
                    {
                        int x = m_InvasionOriginatesFrom.X - Utility.Random(InvasionConfig.SpawnPointRange);
                        int y = m_InvasionOriginatesFrom.Y + Utility.Random(InvasionConfig.SpawnPointRange);
                        int z = Map.GetAverageZ(x, y);
                        return new Point3D(x, y, z);
                    }
                default: { return m_InvasionOriginatesFrom; }
            }
        }

        public object[] SpawnPointMap(Point3D loc, Map map)
        {
            if (ValidSpawnLocation(loc, map))
                return new object[] { loc, map };

            return null;
        }

        public bool ValidSpawnLocation(Point3D loc, Map map)
        {
            if (m_RegisteredInvasionRegion == null)
                return false;

            if (!m_RegisteredInvasionRegion.Contains(loc))
                return false;

            if (!m_RegisteredInvasionRegion.RegionSpawnTable.ContainsKey(loc))
                return false;

            if (!(bool)m_RegisteredInvasionRegion.RegionSpawnTable[loc])
                return false;

            return true;
        }

        public InvasionSpawn CreateNewInvasionSpawn()
        {
            switch (m_InvasionTypeSpawn)
            {
                case InvasionSpawnType.Ants: { return CreateNewInvasionSpawnByTypeList(InvasionSpawnLists.InvasionAntSpawn); }
                case InvasionSpawnType.AOS: { return CreateNewInvasionSpawnByTypeList(InvasionSpawnLists.InvasionAOSSpawn); }
                case InvasionSpawnType.Arachnid: { return CreateNewInvasionSpawnByTypeList(InvasionSpawnLists.InvasionArachnidSpawn); }
                case InvasionSpawnType.Elemental: { return CreateNewInvasionSpawnByTypeList(InvasionSpawnLists.InvasionElementalSpawn); }
                case InvasionSpawnType.Exodus: { return CreateNewInvasionSpawnByTypeList(InvasionSpawnLists.InvasionExodusSpawn); }
                case InvasionSpawnType.Jukas: { return CreateNewInvasionSpawnByTypeList(InvasionSpawnLists.InvasionJukaSpawn); }
                case InvasionSpawnType.Misc: { return CreateNewInvasionSpawnByTypeList(InvasionSpawnLists.InvasionMiscSpawn); }
                case InvasionSpawnType.Orc: { return CreateNewInvasionSpawnByTypeList(InvasionSpawnLists.InvasionOrcSpawn); }
                case InvasionSpawnType.Plant: { return CreateNewInvasionSpawnByTypeList(InvasionSpawnLists.InvasionPlantSpawn); }
                case InvasionSpawnType.Ratman: { return CreateNewInvasionSpawnByTypeList(InvasionSpawnLists.InvasionRatmanSpawn); }
                case InvasionSpawnType.Reptile: { return CreateNewInvasionSpawnByTypeList(InvasionSpawnLists.InvasionReptileSpawn); }
            }

            return null;
        }

        public InvasionSpawn CreateNewInvasionSpawnByTypeList(Type[] list)
        {
            Type type = list[Utility.Random(0, list.Length)];
            try { return (InvasionSpawn)Activator.CreateInstance(type, new object[]{ this }); }
            catch (Exception e) { Console.WriteLine(String.Format("Invasion System: {0}", e.Message));  return null; }
        }

        public override void OnDelete()
        {
            InvasionEventSink.OnInvasionControllerDeleted(new InvasionEventSink.InvasionControllerDeletedEventArgs(this));
            base.OnDelete();
        }

        public InvasionController(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version

            writer.Write((bool)m_InvasionStarted);
            writer.Write((int)m_InvasionTypeSpawn);
            writer.Write((int)m_RequiredKillCount);
            writer.Write((int)m_SpawnDensity);
            writer.Write((int)m_Kills);
            writer.Write((int)m_Reward);
            writer.Write((Point3D)m_InvasionLocationTarget);
            writer.Write((Point3D)m_InvasionOriginatesFrom);
            writer.Write((Rectangle2D)m_InvasionRegionBounds);
            writer.Write((Map)m_InvasionMap);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

            m_InvasionStarted = reader.ReadBool();
            m_InvasionTypeSpawn = (InvasionSpawnType)reader.ReadInt();
            m_RequiredKillCount = reader.ReadInt();
            m_SpawnDensity = reader.ReadInt();
            m_Kills = reader.ReadInt();
            m_Reward = reader.ReadInt();
            m_InvasionLocationTarget = reader.ReadPoint3D();
            m_InvasionOriginatesFrom = reader.ReadPoint3D();
            m_InvasionRegionBounds = reader.ReadRect2D();
            m_InvasionMap = reader.ReadMap();
        }
    }
}
