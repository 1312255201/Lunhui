using System;
using Exiled.API.Features;
using MEC;

namespace Lunhui
{
	public class Plugin : Plugin<YYYlikeconfig>
	{
		//Instance variable for eventhandlers
		public EventHandlers EventHandlers;
		public static int harmonyCounter;
		public override void OnEnabled()
		{
			base.OnEnabled();
			RegisterEvents();
			Log.Info("一只活蹦乱跳的小鱼告诉你差价加载了");
		}
		private void RegisterEvents()
		{
			try
			{
				//Set instance varible to a new instance, this should be nulled again in OnDisable
				EventHandlers = new EventHandlers(this);
				//Hook the events you will be using in the plugin. You should hook all events you will be using here, all events should be unhooked in OnDisabled 
				Exiled.Events.Handlers.Server.RoundStarted += EventHandlers.OnRoundStart;
				Exiled.Events.Handlers.Server.RoundEnded += EventHandlers.OnRoundEnd;
				Exiled.Events.Handlers.Server.RestartingRound += EventHandlers.OnRoundRestart;
				Exiled.Events.Handlers.Player.InteractingDoor += EventHandlers.OnDoorInteract;
				Exiled.Events.Handlers.Player.Joined += EventHandlers.OnPlayerJoin;
				Exiled.Events.Handlers.Player.Hurting += EventHandlers.OnPlayerHurt;
				Exiled.Events.Handlers.Player.Spawning += EventHandlers.OnPlayerSpawn;
				Exiled.Events.Handlers.Server.SendingRemoteAdminCommand += EventHandlers.RAMessage;
				Exiled.Events.Handlers.Player.Died += EventHandlers.OnPlayerDeath;
				Exiled.Events.Handlers.Player.Left += EventHandlers.OnPlayerLeave;
				Exiled.Events.Handlers.Player.PickingUpItem += EventHandlers.OnPlayerPickupItem;
				Exiled.Events.Handlers.Player.MedicalItemUsed += EventHandlers.OnUseMedicalItem;
				Exiled.Events.Handlers.Player.DroppingItem += EventHandlers.OnPlayerDropItem;
				Exiled.Events.Handlers.Server.RespawningTeam += EventHandlers.OnTeamReSpawn;
				Exiled.Events.Handlers.Server.SendingConsoleCommand += EventHandlers.ConsoleCommand;
				Timing.RunCoroutine(EventHandlers.SCPHEALTH());
				EventHandlers.ReloadConfig();
				Log.Info($"Lunhui loaded. c:");
			}
			catch (Exception e)
			{
				//This try catch is redundant, as EXILED will throw an error before this block can, but is here as an example of how to handle exceptions/errors
				Log.Error($"There was an error loading the plugin: {e}");
			}
		}

		public override void OnDisabled()
		{
			base.OnDisabled();
			UnregisterEvents();
		}
		private void UnregisterEvents()
		{
			Exiled.Events.Handlers.Server.RoundStarted -= EventHandlers.OnRoundStart;
			Exiled.Events.Handlers.Server.RoundEnded -= EventHandlers.OnRoundEnd;
			Exiled.Events.Handlers.Server.RestartingRound -= EventHandlers.OnRoundRestart;
			Exiled.Events.Handlers.Player.InteractingDoor -= EventHandlers.OnDoorInteract;
			Exiled.Events.Handlers.Player.Joined -= EventHandlers.OnPlayerJoin;
			Exiled.Events.Handlers.Player.Hurting -= EventHandlers.OnPlayerHurt;
			Exiled.Events.Handlers.Player.Spawning -= EventHandlers.OnPlayerSpawn;
			Exiled.Events.Handlers.Server.SendingRemoteAdminCommand -= EventHandlers.RAMessage;
			Exiled.Events.Handlers.Player.Died -= EventHandlers.OnPlayerDeath;
			Exiled.Events.Handlers.Player.Left -= EventHandlers.OnPlayerLeave;
			Exiled.Events.Handlers.Player.PickingUpItem -= EventHandlers.OnPlayerPickupItem;
			Exiled.Events.Handlers.Player.MedicalItemUsed -= EventHandlers.OnUseMedicalItem;
			Exiled.Events.Handlers.Player.DroppingItem -= EventHandlers.OnPlayerDropItem;
			Exiled.Events.Handlers.Server.RespawningTeam -= EventHandlers.OnTeamReSpawn;
			Exiled.Events.Handlers.Server.SendingConsoleCommand -= EventHandlers.ConsoleCommand;

			EventHandlers = null;
		}

	}
}