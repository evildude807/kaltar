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
    public class InvasionConfig
    {
        // User Configuration
        public static bool Enabled = true; // Is this system enabled?
        public static AccessLevel RequiredLevelToStart = AccessLevel.GameMaster; // The access level required to start an invasion
        public static bool AllowInvasionLooting = false; // Can player loot the killed invasion monsters?
        public static TimeSpan InvasionTickRate = TimeSpan.FromSeconds(20); // Directly effects the rate at which the invasion spawn occurs. This is the rate that the invasion is check by an individual internal timer, changing this value will change system performance and possibly server performance.
        public static int InvasionSpawnAttempts = 30;
        public static int InvasionSpawnWanderFromTargetRange = 200; // How far spawn will wander from target location
        public static int InvasionSpawnReachedTargetRange = 4; // How close to the target do they have to be before they wander?
        public static int DefaultRegionPriority = 150; // The default priority of invasion regions.
        public static int SpawnPointRange = 16; // The possible offset from the set spawn location on an invasion controller.
        // End User Configuration

        public static List<InvasionController> InvasionControllerRegistry =  new List<InvasionController>();

        public static void Initialize()
        {
            FindAllInvasionControllers();

            InvasionEventSink.InvasionControllerCreatedEvent += new InvasionEventSink.InvasionControllerCreated(OnInvasionControllerCreated);
            InvasionEventSink.InvasionControllerDeletedEvent += new InvasionEventSink.InvasionControllerDeleted(OnInvasionControllerDeleted);

            CommandSystem.Register("startallinvasions", AccessLevel.GameMaster, new CommandEventHandler(StartAllCommand));
            CommandSystem.Register("viewallinvasions", AccessLevel.GameMaster, new CommandEventHandler(ViewAllCommand));
            CommandSystem.Register("endallinvasions", AccessLevel.GameMaster, new CommandEventHandler(EndAllCommand));
        }

        public static void FindAllInvasionControllers()
        {
            foreach (Item item in World.Items.Values)
                if (item is InvasionController)
                    InvasionControllerRegistry.Add((InvasionController)item);
        }

        public static void OnInvasionControllerCreated(InvasionEventSink.InvasionControllerCreatedEventArgs args)
        {
            InvasionControllerRegistry.Add(args.Controller);
        }

        public static void OnInvasionControllerDeleted(InvasionEventSink.InvasionControllerDeletedEventArgs args)
        {
            InvasionControllerRegistry.Remove(args.Controller);
        }

        public static void StartAllCommand(CommandEventArgs args) // Starts all invasions
        {
            if (!Enabled)
                return;

            for (int i = 0; i < InvasionControllerRegistry.Count; ++i)
                if (!((InvasionController)InvasionControllerRegistry[i]).InvasionStarted)
                    ((InvasionController)InvasionControllerRegistry[i]).StartInvasion();

            args.Mobile.SendMessage("All invasion have been started!");
        }

        public static void ViewAllCommand(CommandEventArgs args) // View a gump displaying active/inactive invasion controllers
        {
            if (!Enabled)
                return;


        }

        public static void EndAllCommand(CommandEventArgs args) // Ends all invasions
        {
            if (!Enabled)
                return;

            for (int i = 0; i < InvasionControllerRegistry.Count; ++i)
                if (((InvasionController)InvasionControllerRegistry[i]).InvasionStarted)
                    ((InvasionController)InvasionControllerRegistry[i]).EndInvasion(InvasionEventSink.InvasionEndedEventArgs.InvasionEndedReason.EndAllCommand);

            args.Mobile.SendMessage("All invasion have been ended!");
        }
    }
}
