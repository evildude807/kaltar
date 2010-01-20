using System;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Regions;
using Server.Commands;

namespace Scripts.Invasion_System
{
    public class InvasionEventSink
    {
        public delegate void InvasionControllerCreated(InvasionControllerCreatedEventArgs args);
        public static event InvasionControllerCreated InvasionControllerCreatedEvent;

        public static void OnInvasionControllerCreated(InvasionControllerCreatedEventArgs args)
        {
            if (InvasionControllerCreatedEvent != null)
                InvasionControllerCreatedEvent(args);
        }

        public class InvasionControllerCreatedEventArgs : EventArgs
        {
            public readonly InvasionController Controller;
            public InvasionControllerCreatedEventArgs(InvasionController c)
            {
                Controller = c;
            }
        }

        public delegate void InvasionControllerDeleted(InvasionControllerDeletedEventArgs args);
        public static event InvasionControllerDeleted InvasionControllerDeletedEvent;

        public static void OnInvasionControllerDeleted(InvasionControllerDeletedEventArgs args)
        {
            if (InvasionControllerDeletedEvent != null)
                InvasionControllerDeletedEvent(args);
        }

        public class InvasionControllerDeletedEventArgs : EventArgs
        {
            public readonly InvasionController Controller;
            public InvasionControllerDeletedEventArgs(InvasionController c)
            {
                Controller = c;
            }
        }

        public delegate void InvasionTicked(InvasionTickedEventArgs args);
        public static event InvasionTicked InvasionTickedEvent;

        public static void OnInvasionTicked(InvasionTickedEventArgs args)
        {
            if (InvasionTickedEvent != null)
                InvasionTickedEvent(args);
        }

        public class InvasionTickedEventArgs : EventArgs
        {
            public readonly InvasionController Controller;
            public InvasionTickedEventArgs(InvasionController c)
            {
                Controller = c;
            }
        }

        public delegate void InvasionStarted(InvasionStartedEventArgs args);
        public static event InvasionStarted InvasionStartedEvent;

        public static void OnInvasionStarted(InvasionStartedEventArgs args)
        {
            if (InvasionStartedEvent != null)
                InvasionStartedEvent(args);
        }

        public class InvasionStartedEventArgs : EventArgs
        {
            public readonly InvasionController Controller;
            public InvasionStartedEventArgs(InvasionController c)
            {
                Controller = c;
            }
        }

        public delegate void InvasionEnded(InvasionEndedEventArgs args);
        public static event InvasionEnded InvasionEndedEvent;

        public static void OnInvasionEnded(InvasionEndedEventArgs args)
        {
            if (InvasionEndedEvent != null)
                InvasionEndedEvent(args);
        }

        public class InvasionEndedEventArgs : EventArgs
        {
            public readonly InvasionController Controller;
            public readonly InvasionEndedReason EndReason;
            public InvasionEndedEventArgs(InvasionController c, InvasionEndedReason endreason)
            {
                Controller = c;
                EndReason = endreason;
            }

            public enum InvasionEndedReason
            {
                EndAllCommand = 0,
                KillCountReached = 1,
                DoubleClickEnded = 2
            }
        }

        public delegate void InvasionSpawnMoved(InvasionSpawnMovedEventArgs args);
        public static event InvasionSpawnMoved InvasionSpawnMovedEvent;

        public static void OnInvasionSpawnMoved(InvasionSpawnMovedEventArgs args)
        {
            if (InvasionSpawnMovedEvent != null)
                InvasionSpawnMovedEvent(args);
        }

        public class InvasionSpawnMovedEventArgs : EventArgs
        {
            public readonly InvasionController Controller;
            public readonly InvasionSpawn Spawn;
            public InvasionSpawnMovedEventArgs(InvasionController c, InvasionSpawn s)
            {
                Controller = c;
                Spawn = s;
            }
        }

        public delegate void InvasionSpawnDied(InvasionSpawnDiedEventArgs args);
        public static event InvasionSpawnDied InvasionSpawnDiedEvent;

        public static void OnInvasionSpawnDied(InvasionSpawnDiedEventArgs args)
        {
            if (InvasionSpawnDiedEvent != null)
                InvasionSpawnDiedEvent(args);
        }

        public class InvasionSpawnDiedEventArgs : EventArgs
        {
            public readonly InvasionController Controller;
            public readonly InvasionSpawn Spawn;
            public readonly Container DeathContainer;
            public InvasionSpawnDiedEventArgs(InvasionController c, InvasionSpawn spawn, Container cont)
            {
                Controller = c;
                Spawn = spawn;
                DeathContainer = cont;
            }
        }

        public delegate void InvasionSpawnDeleted(InvasionSpawnDeletedEventArgs args);
        public static event InvasionSpawnDeleted InvasionSpawnDeletedEvent;

        public static void OnInvasionSpawnDeleted(InvasionSpawnDeletedEventArgs args)
        {
            if (InvasionSpawnDeletedEvent != null)
                InvasionSpawnDeletedEvent(args);
        }

        public class InvasionSpawnDeletedEventArgs : EventArgs
        {
            public readonly InvasionController Controller;
            public readonly InvasionSpawn Spawn;
            public InvasionSpawnDeletedEventArgs(InvasionController c, InvasionSpawn spawn)
            {
                Controller = c;
                Spawn = spawn;
            }
        }

        public delegate void InvasionSpawnKilled(InvasionSpawnKilledEventArgs args);
        public static event InvasionSpawnKilled InvasionSpawnKilledEvent;

        public static void OnInvasionSpawnKilled(InvasionSpawnKilledEventArgs args)
        {
            if (InvasionSpawnKilledEvent != null)
                InvasionSpawnKilledEvent(args);
        }

        public class InvasionSpawnKilledEventArgs : EventArgs
        {
            public readonly InvasionController Controller;
            public readonly Mobile Killer;
            public InvasionSpawnKilledEventArgs(InvasionController c, Mobile killer)
            {
                Controller = c;
                Killer = killer;
            }
        }
    }
}
