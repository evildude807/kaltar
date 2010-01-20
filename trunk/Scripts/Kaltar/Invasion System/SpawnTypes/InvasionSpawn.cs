using System;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Regions;

namespace Scripts.Invasion_System
{
    public class InvasionSpawn : BaseCreature
    {
        private InvasionController m_Controller;
        public InvasionController Controller
        {
            get { return m_Controller; }
            set { m_Controller = value; }
        }

        private InvasionSpawnType m_SpawnType;
        public InvasionSpawnType SpawnType
        {
            get { return m_SpawnType; }
            set { m_SpawnType = value; }
        }

        public static void Initialize()
        {
            List<InvasionSpawn> spawn = new List<InvasionSpawn>();
            foreach (Mobile m in World.Mobiles.Values)
                if (m is InvasionSpawn)
                    spawn.Add((InvasionSpawn)m);

            for (int i = 0; i < spawn.Count; ++i)
                spawn[i].Delete();
        }

        public InvasionSpawn(InvasionController c, InvasionSpawnType spawntype, AIType ai_type, FightMode fightmode, double[] ints)
            : base(ai_type, fightmode, (int)ints[0], (int)ints[1], ints[2], ints[3])
        {
            m_Controller = c;
            m_SpawnType = spawntype;
        }

        protected override bool OnMove(Direction d)
        {
            if (m_Controller == null)
                Delete();

            if (m_Controller != null)
                InvasionEventSink.OnInvasionSpawnMoved(new InvasionEventSink.InvasionSpawnMovedEventArgs(m_Controller, this));

            return base.OnMove(d);
        }

        public override void OnDeath(Container c)
        {
            if (m_Controller == null)
            {
                Delete();
                base.OnDeath(c);
                return;
            }

            InvasionEventSink.OnInvasionSpawnDied(new InvasionEventSink.InvasionSpawnDiedEventArgs(m_Controller, this, c));
            base.OnDeath(c);
        }

        public override void OnKilledBy(Mobile mob)
        {
            if (m_Controller != null && mob != null)
                InvasionEventSink.OnInvasionSpawnKilled(new InvasionEventSink.InvasionSpawnKilledEventArgs(Controller, mob));
            base.OnKilledBy(mob);
        }

        public override void OnDelete()
        {
            if (m_Controller == null)
            {
                base.OnDelete();
                return;
            }

            InvasionEventSink.OnInvasionSpawnDeleted(new InvasionEventSink.InvasionSpawnDeletedEventArgs(m_Controller, this));
            base.OnDelete();
        }

        public bool CheckReachedTarget()
        {
            if(Location.X == m_Controller.InvasionLocationTarget.X && Location.Y == m_Controller.InvasionLocationTarget.Y)
                return true;

            int x = Location.X;
            int x1 = m_Controller.InvasionLocationTarget.X;
            int y = Location.Y;
            int y1 = m_Controller.InvasionLocationTarget.Y;
            int trange = InvasionConfig.InvasionSpawnReachedTargetRange;

            bool inxrange = false;
            if(((x >= x1) && (x <= (x + trange))) || ((x <= x1) && (x >= (x1 - trange))) || ((x >= x1) && (x <= (x1 + trange))) || ((x <= x1) && (x >= (x1 - trange))))
                inxrange = true;

            bool inyrange = false;
            if((y >= y1) && (y <= (y + trange)) || ((y <= y1) && (y >= (y1 - trange))) || ((y <= y1) && (y >= (y1 - trange))) || ((y >= y1) && (y <= (y1 + trange))))
                inyrange = true;

            if (inxrange && inyrange)
            {
                Console.WriteLine("Check Sucessful");
                return true;
            }
            else
                return false;
        }

        public InvasionSpawn(Serial serial)
            : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
            writer.Write((Item)m_Controller);
            writer.Write((int)m_SpawnType);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            m_Controller = (InvasionController)reader.ReadItem();
            m_SpawnType = (InvasionSpawnType)reader.ReadInt();
            Delete();
		}
    }

    public enum InvasionSpawnType
    {
        Orc = 0,
        Ratman = 1,
        Arachnid = 2,
        Reptile = 3,
        Exodus = 4,
        Jukas = 5,
        Elemental = 6,
        Ants = 7,
        AOS = 8,
        Plant = 9,
        Misc = 10
    }
}
