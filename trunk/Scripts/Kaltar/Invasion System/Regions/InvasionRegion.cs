using System;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.Regions;
using Server.Items;
using Server.Mobiles;
using Server.Spells;

namespace Scripts.Invasion_System
{
    public class InvasionRegion : BaseRegion
    {
        public static int Count = 0;
        public InvasionController Controller;
        public Dictionary<Point3D, bool> RegionSpawnTable;

        public InvasionRegion(InvasionController c)
            : base(String.Format("InvasionRegion{0}", Count.ToString()), c.InvasionMap, InvasionConfig.DefaultRegionPriority, c.InvasionRegionBounds)
        {
            Controller = c;
            Count += 1;
            GenerateSpawnTable();

            InvasionEventSink.InvasionStartedEvent += new InvasionEventSink.InvasionStarted(OnInvasionStarted);
            InvasionEventSink.InvasionEndedEvent += new InvasionEventSink.InvasionEnded(OnInvasionEnded);
        }

        public void GenerateSpawnTable()
        {
            if (RegionSpawnTable == null)
                RegionSpawnTable = new Dictionary<Point3D, bool>();

            int x = Controller.InvasionRegionBounds.Start.X, y = Controller.InvasionRegionBounds.Start.Y, h = Controller.InvasionRegionBounds.Height, w = Controller.InvasionRegionBounds.Width;
            int tilecount = h * w;

            int atx = x, aty = y;
            for (int i = 0; i < tilecount; ++i)
            {
                Point3D tile = new Point3D(atx, aty, Controller.InvasionMap.GetAverageZ(atx, aty));
                RegionSpawnTable.Add(tile, SpellHelper.FindValidSpawnLocation(Controller.InvasionMap, ref tile, true));

                if ((atx - x) == w)
                {
                    atx = x;
                    aty += 1;
                }
                atx += 1;
            }
        }

        public bool InRegion(Mobile m)
        {
            if (m.Map != Map)
                return false;
            if(!Contains(m.Location))
                return false;

            return true;
        }

        public void OnInvasionStarted(InvasionEventSink.InvasionStartedEventArgs args)
        {
            if (args.Controller == Controller)
                Register();
        }

        public void OnInvasionEnded(InvasionEventSink.InvasionEndedEventArgs args)
        {
            if (args.Controller == Controller)
                Unregister();
        }

        public override bool OnDoubleClick(Mobile m, object o)
        {
            if (o is Corpse && Controller.InvasionStarted)
            {
                Corpse c = (Corpse)o;
                if (c.Owner is InvasionSpawn && m is PlayerMobile)
                {
                    m.SendMessage("You cannot loot the corpses of the invasion monsters.");
                    return false;
                }
            }

            return base.OnDoubleClick(m, o);
        }
    }
}
