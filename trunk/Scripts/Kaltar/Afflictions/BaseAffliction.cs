//Sickness and Affliction System
//   by Bujinsho
//  www.New-Valoria.net

using System;
using Server;
using System.Reflection;
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Items
{
    public class Affliction : Item
    {
        public string m_Type;
        public Mobile m_Victim;
        public int m_Severity;
        public int m_Virulence;
        public bool m_PlayerOnly = true;
        public bool m_Expires = true;

        public Timer m_afflictTimer;
        public double m_Period;

        public DateTime m_Expiry;

        [CommandProperty(AccessLevel.GameMaster)]
        public string Type { get { return m_Type; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Victim { get { return m_Victim; } set { m_Victim = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Severity { get { return m_Severity; } set { m_Severity = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Virulence { get { return m_Virulence; } set { m_Virulence = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool PlayerOnly { get { return m_PlayerOnly; } set { m_PlayerOnly = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Expires { get { return m_Expires; } set { m_Expires = value; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime Expiry { get { return m_Expiry; } }

        public Affliction(string type, double period, int virulence, double duration)
            : base(0xF7D)
        {
            Visible = false;
            Movable = false;
            Weight = 0;

            m_Type = type;
            m_Virulence = virulence;
            m_Period = period;

            if (duration == 0)
                m_Expires = false;

            m_Expiry = DateTime.Now + TimeSpan.FromHours(duration);

            m_afflictTimer = new AfflictionTimer(this, period);
            m_afflictTimer.Start();
        }

        public virtual void DoCycle()
        {
            if ((DateTime.Now > m_Expiry && m_Expires) || (m_Virulence == 0 && m_Victim == null))
            {
                m_afflictTimer.Stop();
                Delete();
            }

            Infect();

            if (m_Victim != null && IsOnline(m_Victim))
                DoSymptoms(m_Victim, m_Severity);
        }

        public virtual void DoSymptoms(Mobile victim, int severity)
        {
        }

        public virtual void Infect()
        {
            if (m_Virulence == 0)
                return;

            if (m_Victim != null && IsOnline(m_Victim))
            {
                this.Map = m_Victim.Map;
                this.Location = m_Victim.Location;
            }

            int range = m_Virulence / 10;

            if (range < 1)
                range = 1;

            IPooledEnumerable eable = GetMobilesInRange(range);

            foreach (Mobile m in eable)
            {
                if (!(m.Alive) || m.Deleted || m.Blessed || (!(m is PlayerMobile) && m_PlayerOnly) || m == m_Victim || HasType(m) || m.AccessLevel > AccessLevel.Player)
                    continue;

                double health = (double)m.Hits / (double)m.HitsMax / 100; ;
                double resist = health - (double)m_Virulence;
                double infectChance = Utility.RandomDouble() * 100;

                if (resist < 5)
                    resist = 5;

                if(resist > 95)
                    resist = 95;

                if (resist > infectChance)
                    continue;

                if (m_Victim == null && m is PlayerMobile)
                {
                    ApplyAffliction(m, m_Severity);
                    m_Victim = m;
                    continue;
                }

                Replicate(m);
            }
        }

        public void Replicate(Mobile newVictim)
        {
            Type affliction = this.GetType();
            Object o = Activator.CreateInstance(affliction);
            Affliction newAffliction = (Affliction)o;

            ApplyAffliction(newVictim, m_Severity);
            newAffliction.Victim = newVictim;

            newAffliction.MoveToWorld(newVictim.Location, newVictim.Map);
        }

        public bool HasType(Mobile m)
        {
            foreach (Item found in World.Items.Values)
            {
                if (found is Affliction)
                {
                    Affliction existing = (Affliction)found;

                    if (existing.Victim == m && existing.Type == m_Type)
                        return true;
                }
            }
            return false;
        }

        public bool IsOnline(Mobile player)
        {
            NetState checkPlayer = player.NetState;

            if (checkPlayer != null)
                return true;

            return false;
        }

        public virtual void ApplyAffliction(Mobile affected, int severity)
        {
        }

        public override void OnDelete()
        {
            if (m_Victim != null)
                CancelEffects(m_Victim);

            m_afflictTimer.Stop();            
            base.OnDelete();
        }

        public virtual void CancelEffects(Mobile cured)
        {
        }

        public Affliction(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);

            writer.Write(m_Victim);
            writer.Write(m_Type);
            writer.Write(m_Virulence);
            writer.Write(m_Severity);
            writer.Write(m_Period);
            writer.Write(m_Expiry);
            writer.Write(m_PlayerOnly);
            writer.Write(m_Expires);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_Victim = reader.ReadMobile();
            m_Type = reader.ReadString();
            m_Virulence = reader.ReadInt();
            m_Severity = reader.ReadInt();
            m_Period = reader.ReadDouble();
            m_Expiry = reader.ReadDateTime();
            m_PlayerOnly = reader.ReadBool();
            m_Expires = reader.ReadBool();

            if (DateTime.Now > m_Expiry && m_Expires)
                Delete();

            m_afflictTimer = new AfflictionTimer(this, m_Period);
            m_afflictTimer.Start();
        }

        private class AfflictionTimer : Timer
        {
            private Affliction m_Affliction;

            public AfflictionTimer(Affliction affliction, double period)
                : base(TimeSpan.FromSeconds(period), TimeSpan.FromMinutes(period))
            {
                m_Affliction = affliction;
            }

            protected override void OnTick()
            {
                m_Affliction.DoCycle();
            }
        }
    }
}