using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using MEC;
using UnityEngine;

namespace Lunhui
{
	// Token: 0x02000002 RID: 2
	public class LunhuiConfig
	{
		public float SCPHealth;
		public float SCPSecondOnce;
		public float SCP106Health;
		public float SCP049MaxHealth;
		public float SCP0492MaxHealth;
		public float SCP096MaxHealth;
		public float SCP106MaxHealth;
		public float SCP173MaxHealth;
		public float SCP939MaxHealth;
		public float SecondAddHealth;
	}
	public class EventHandlers
	{
		public static LunhuiConfig Config;
		public EventHandlers(Plugin plugin) => this.plugin = plugin;
		public Plugin plugin;
		public static List<int> SCPadd = new List<int>();
		public static bool Break = false;
		public static int lzrspawncount = 0;
		public static int ysyspawncount = 0;
		public static List<int> scp181idq = new List<int>();
		public static List<int> scpspsjdidq = new List<int>();
		public static List<int> scpspsidq = new List<int>();
		public static List<int> scplbidq = new List<int>();
		public static List<int> scpysyidq = new List<int>();
		public static List<int> scplzridq = new List<int>();
		public static List<Pickup> Baba = new List<Pickup>();
		public static List<Pickup> Baba1 = new List<Pickup>();
		public static List<int> Eatid = new List<int>();
		public static List<int> ready = new List<int>();
		public static List<int> breakid = new List<int>();
		public static List<int> Canteatshit = new List<int>();
		public static List<int> useing2 = new List<int>();
		public static List<int> used2 = new List<int>();
		private int classd;

		public void ReloadConfig()
		{
			string[] pzx = { "SCPHealth", "SCPSecondOnce", "SCP106Health", "SCP049MaxHealth", "SCP0492MaxHealth", "SCP096MaxHealth", "SCP106MaxHealth", "SCP173MaxHealth", "SCP939MaxHealth", "SecondAddHealth" };
			string[] defaultpz = { "5", "1000", "1", "4935", "800", "4096", "750", "6173", "5393", "1000" };
			for (int i = 0; i < pzx.Length; i++)
			{
				string cat = pzx[i];
				if (MangConfig.GetConfig(cat) == "")
				{
					Log.Info("配置项[" + cat + "]不存在，创建默认配置中");
					MangConfig.SetConfig(cat, defaultpz[i]);
				}
			}
			Config = new LunhuiConfig
			{
				SCPHealth = float.Parse(MangConfig.GetConfig("SCPHealth")),
				SCPSecondOnce = float.Parse(MangConfig.GetConfig("SCPSecondOnce")),
				SCP106Health = float.Parse(MangConfig.GetConfig("SCP106Health")),
				SCP049MaxHealth = float.Parse(MangConfig.GetConfig("SCP049MaxHealth")),
				SCP0492MaxHealth = float.Parse(MangConfig.GetConfig("SCP0492MaxHealth")),
				SCP096MaxHealth = float.Parse(MangConfig.GetConfig("SCP096MaxHealth")),
				SCP106MaxHealth = float.Parse(MangConfig.GetConfig("SCP106MaxHealth")),
				SCP173MaxHealth = float.Parse(MangConfig.GetConfig("SCP173MaxHealth")),
				SCP939MaxHealth = float.Parse(MangConfig.GetConfig("SCP939MaxHealth")),
				SecondAddHealth = float.Parse(MangConfig.GetConfig("SecondAddHealth"))
			};
			Log.Info("\n" + "定制插件就别找芒辉V1.0[配置读取完毕]" + "\nSCP每次回血：" + Config.SCPHealth
				+ "\n几秒加一次：" + Config.SCPSecondOnce
				+ "\n106每次回血：" + Config.SCP106Health
				+ "\n049血量：" + Config.SCP049MaxHealth
				+ "\n0492血量：" + Config.SCP0492MaxHealth
				+ "\n096血量：" + Config.SCP096MaxHealth
				+ "\n106血量：" + Config.SCP106MaxHealth
				+ "\n173血量：" + Config.SCP173MaxHealth
				+ "\n939血量：" + Config.SCP939MaxHealth
				+ "\n几秒检测坐标一次：" + Config.SecondAddHealth + "\n");
		}
		public void OnPlayerSpawn(SpawningEventArgs ev)
		{
			Timing.RunCoroutine(delayspawn(ev));
		}
		public void OnRoundStart()
		{
			Timing.RunCoroutine(Fenpei());
		}
		public IEnumerator<float> Laobakouxue3(int id, Vector3 zb)
		{
			foreach (Player cat in Player.List)
			{
				if (cat.ReferenceHub.queryProcessor.PlayerId == id)
				{
					while (!(cat.ReferenceHub.playerStats.Health < 1f))
					{
						yield return Timing.WaitForSeconds(0.035f);
						cat.ReferenceHub.playerMovementSync.OverridePosition(zb, 0f, false);
						cat.ReferenceHub.playerStats.Health = cat.ReferenceHub.playerStats.Health - 1f;
					}
					cat.ReferenceHub.playerStats.HurtPlayer(new PlayerStats.HitInfo(0.5f, cat.ReferenceHub.nicknameSync.Network_myNickSync, DamageTypes.Scp207, cat.ReferenceHub.queryProcessor.PlayerId), cat.ReferenceHub.gameObject);
				}
			}
		}
		public IEnumerator<float> test(int id)
		{
			yield return Timing.WaitForSeconds(60f);
			ready.Remove(id);
		}
		public IEnumerator<float> Laobahuixue(int id, int time, int heal)
		{
			foreach (Player cat in Player.List)
			{
				if (cat.ReferenceHub.queryProcessor.PlayerId == id)
				{
					for (int test = 0; test <= heal; test++)
					{
						yield return Timing.WaitForSeconds(1f);
						cat.ReferenceHub.playerStats.Health = cat.ReferenceHub.playerStats.Health + (float)heal;
					}
				}
			}
		}
		public IEnumerator<float> Laobakouxue(int id, float hurt, Vector3 vec)
		{
			List<Player> test = new List<Player>(Player.List).ToList<Player>();
			for (int o = 0; o <= test.Count; o++)
			{
				Player cat = test[o];
				bool flag = cat.ReferenceHub.queryProcessor.PlayerId == id;
				if (flag)
				{
					yield return Timing.WaitForSeconds(10f);
					Vector3 tete = new PlayerPositionData(cat.ReferenceHub).position;
					for (int u = 0; u <= 60; u++)
					{
						int a = 0;
						if (breakid.Contains(cat.ReferenceHub.queryProcessor.PlayerId))
							a++;
						if (a == 0)
						{
							cat.ReferenceHub.playerStats.HurtPlayer(new PlayerStats.HitInfo(hurt / 60f, cat.ReferenceHub.nicknameSync.Network_myNickSync, DamageTypes.Scp207, cat.ReferenceHub.queryProcessor.PlayerId), cat.ReferenceHub.gameObject);
							cat.ReferenceHub.playerMovementSync.OverridePosition(tete, 0f, false);
							yield return Timing.WaitForSeconds(0.05f);
						}
					}
					yield return Timing.WaitForSeconds(10f);
					tete = new PlayerPositionData(cat.ReferenceHub).position;
					for (int u2 = 0; u2 <= 60; u2++)
					{
						int a2 = 0;
						bool flag4 = breakid.Contains(cat.ReferenceHub.queryProcessor.PlayerId);
						if (flag4)
							a2++;
						if (a2 == 0)
						{
							cat.ReferenceHub.playerStats.HurtPlayer(new PlayerStats.HitInfo(hurt / 60f, cat.ReferenceHub.nicknameSync.Network_myNickSync, DamageTypes.Scp207, cat.ReferenceHub.queryProcessor.PlayerId), cat.ReferenceHub.gameObject);
							cat.ReferenceHub.playerMovementSync.OverridePosition(tete, 0f, false);
							yield return Timing.WaitForSeconds(0.05f);
						}
					}
					yield return Timing.WaitForSeconds(10f);
					tete = new PlayerPositionData(cat.ReferenceHub).position;
					for (int u3 = 0; u3 <= 60; u3++)
					{
						int a3 = 0;
						if (breakid.Contains(cat.ReferenceHub.queryProcessor.PlayerId))
							a3++;
						bool flag7 = a3 == 0;
						if (flag7)
						{
							cat.ReferenceHub.playerStats.HurtPlayer(new PlayerStats.HitInfo(hurt / 60f, cat.ReferenceHub.nicknameSync.Network_myNickSync, DamageTypes.Scp207, cat.ReferenceHub.queryProcessor.PlayerId), cat.ReferenceHub.gameObject);
							cat.ReferenceHub.playerMovementSync.OverridePosition(tete, 0f, false);
							yield return Timing.WaitForSeconds(0.05f);
						}
					}
				}
			}
			yield break;
		}
		public IEnumerator<float> delayspawn(SpawningEventArgs ev)
		{
			yield return Timing.WaitForSeconds(0.1f);
			if (ev.Player.Role == RoleType.Scp049)
				ev.Player.ReferenceHub.playerStats.Health = Config.SCP049MaxHealth;
			if (ev.Player.Role == RoleType.Scp0492)
				ev.Player.ReferenceHub.playerStats.Health = Config.SCP0492MaxHealth;
			if (ev.Player.Role == RoleType.Scp106)
				ev.Player.ReferenceHub.playerStats.Health = Config.SCP106MaxHealth;
			if (ev.Player.Role == RoleType.Scp096)
				ev.Player.ReferenceHub.playerStats.Health = Config.SCP096MaxHealth;
			if (ev.Player.Role == RoleType.Scp173)
				ev.Player.ReferenceHub.playerStats.Health = Config.SCP173MaxHealth;
			if (ev.Player.Role == RoleType.Scp93953)
				ev.Player.ReferenceHub.playerStats.Health = Config.SCP939MaxHealth;
			if (ev.Player.Role == RoleType.Scp93989)
				ev.Player.ReferenceHub.playerStats.Health = Config.SCP939MaxHealth;
			bool flag5 = ev.Player.Role == RoleType.Scp93989 || ev.Player.Role == RoleType.Scp93953;
			if (scplbidq.Contains(ev.Player.ReferenceHub.queryProcessor.PlayerId))
			{
				ev.Player.ReferenceHub.playerStats.Health = 2500f;
			}
		}
		public IEnumerator<float> Fenpei()
		{
			yield return Timing.WaitForSeconds(1f);
			List<Player> playerList = new List<Player>(Player.List).ToList<Player>();
			int playerNumber = playerList.Count;
			foreach (Door door in UnityEngine.Object.FindObjectsOfType<Door>())
			{
				if (door.DoorName.Contains("096"))
				{
					for (int c = 0; c <= 30; c++)
					{
						Pickup test = PlayerManager.localPlayer.GetComponent<Inventory>().SetPickup(ItemType.Coin, 0f, new Vector3(door.transform.position.x, door.transform.position.y + 5f, door.transform.position.z), Quaternion.identity, 0, 0, 0);
						Baba.Add(test);
						if (c == 7)
							Baba1.Add(test);
						if (c == 18)
							Baba1.Add(test);
					}
				}
			}
			for (int c2 = 0; c2 <= 30; c2++)
			{
				Pickup test2 = PlayerManager.localPlayer.GetComponent<Inventory>().SetPickup(ItemType.Coin, 0f, new Vector3(-54f, 990f, -49f), Quaternion.identity, 0, 0, 0);
				Baba.Add(test2);
				if (c2 == 7)
					Baba1.Add(test2);
				if (c2 == 18)
					Baba1.Add(test2);
			}
			for (int c3 = 0; c3 <= 30; c3++)
			{
				Pickup test3 = PlayerManager.localPlayer.GetComponent<Inventory>().SetPickup(ItemType.Coin, 0f, new Vector3(Map.GetRandomSpawnPoint(RoleType.Scp173).x, Map.GetRandomSpawnPoint(RoleType.Scp173).y + 5f, Map.GetRandomSpawnPoint(RoleType.Scp173).z), Quaternion.identity, 0, 0, 0);
				Baba.Add(test3);
				if (c3 == 7)
				{
					Baba1.Add(test3);
				}
			}
			if (playerList.Count > 2)
			{
				List<Player> classD = new List<Player>();
				for (int c4 = 0; c4 < playerNumber; c4++)
				{
					Player currentPlayer = playerList[c4];
					Team playerType = currentPlayer.Team;
					if (playerType == Team.CDP)
					{
						classD.Add(currentPlayer);
					}
				}
				int classDNumber = classD.Count;
				if (classDNumber > 0)
				{
					int Random = EventHandlers.GetOnlyRandom(0, classDNumber - 1);
					scp181idq.Add(classD[Random].ReferenceHub.queryProcessor.PlayerId);
					Timing.RunCoroutine(makescp(classD[Random].ReferenceHub.queryProcessor.PlayerId, "181"));

				}
			}
			bool flag16 = playerList.Count > 5;
			if (flag16)
			{
				List<Player> TmpList = new List<Player>();
				for (int c5 = 0; c5 < playerNumber; c5++)
				{
					Player currentPlayer2 = playerList[c5];
					Team playerType2 = currentPlayer2.Team;
					if (playerType2 == Team.CDP || playerType2 == Team.RSC)
					{
						if (!scp181idq.Contains(currentPlayer2.ReferenceHub.queryProcessor.PlayerId))
						{
							TmpList.Add(currentPlayer2);
						}
					}
				}
				int classDNumber2 = TmpList.Count;
				if (classDNumber2 > 0)
				{
					int Random2 = EventHandlers.GetOnlyRandom(0, classDNumber2 - 1);
					scpspsjdidq.Add(TmpList[Random2].ReferenceHub.queryProcessor.PlayerId);
					Timing.RunCoroutine(makescp(TmpList[Random2].ReferenceHub.queryProcessor.PlayerId, "szsjd"));
				}
			}
		}
		public static IEnumerator<float> SCPHEALTH()
		{

			Dictionary<int, Vector3> SCPPostion = new Dictionary<int, Vector3>();
			long time1 = 0;
			long time2 = 0;
			long time3 = 0;
			long time4 = 0;
			while (true)
			{
				yield return Timing.WaitForSeconds(0.3f);
				if (new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds() - time3 > (long)Config.SCPSecondOnce)
				{
					time3 = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
					foreach (Player cat in Player.List)
					{
						if (cat.ReferenceHub.characterClassManager.IsAnyScp())
						{
							if (SCPPostion.ContainsKey(cat.ReferenceHub.queryProcessor.PlayerId))
							{
								if (SCPPostion[cat.ReferenceHub.queryProcessor.PlayerId] == cat.ReferenceHub.transform.position)
								{
									for (int i = 0; i < SCPadd.Count; i++)
										if (SCPadd[i] == cat.ReferenceHub.queryProcessor.PlayerId)
										{
											SCPadd.Remove(SCPadd[i]);
										}
									SCPadd.Add(cat.ReferenceHub.queryProcessor.PlayerId);
								}
								else
								{
									SCPPostion.Remove(cat.ReferenceHub.queryProcessor.PlayerId);
									SCPPostion.Add(cat.ReferenceHub.queryProcessor.PlayerId, cat.ReferenceHub.transform.position);
									for (int i = 0; i < SCPadd.Count; i++)
										if (SCPadd[i] == cat.ReferenceHub.queryProcessor.PlayerId)
										{
											SCPadd.Remove(SCPadd[i]);
										}
								}
							}
							if (!SCPPostion.ContainsKey(cat.ReferenceHub.queryProcessor.PlayerId))
								SCPPostion.Add(cat.ReferenceHub.queryProcessor.PlayerId, cat.ReferenceHub.transform.position);
						}
						else
						{
							if (SCPPostion.ContainsKey(cat.ReferenceHub.queryProcessor.PlayerId))
								SCPPostion.Remove(cat.ReferenceHub.queryProcessor.PlayerId);
						}
					}
				}
				if (new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds() - time4 > 1000)
				{
					time4 = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
					foreach (Player cat1 in Player.List)
					{
						if (scpysyidq.Contains(cat1.ReferenceHub.queryProcessor.PlayerId))
						{
							if (cat1.ReferenceHub.playerStats.Health < 80)
							{
								cat1.ReferenceHub.playerStats.Health = cat1.ReferenceHub.playerStats.Health + 1;
							}
						}
					}
				}
				if (new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds() - time1 > 10000)
				{
					time1 = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
					foreach (int cat in scpspsjdidq)
					{
						foreach (Player cat1 in Player.List)
						{
							if (cat1.ReferenceHub.queryProcessor.PlayerId == cat)
							{
								if (cat1.ReferenceHub.playerStats.Health < 100)
								{
									cat1.ReferenceHub.playerStats.Health = cat1.ReferenceHub.playerStats.Health + 3;
								}
							}
						}
					}
				}
				if (new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds() - time2 > (long)Config.SecondAddHealth)
				{
					time2 = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
					foreach (int cat in SCPadd)
					{
						Player player1 = Player.Get(cat);
						if (player1.ReferenceHub.characterClassManager.CurClass == RoleType.Scp049 && player1.ReferenceHub.playerStats.Health < Config.SCP049MaxHealth)
							player1.ReferenceHub.playerStats.Health = player1.ReferenceHub.playerStats.Health + Config.SCPHealth;
						if (player1.ReferenceHub.characterClassManager.CurClass == RoleType.Scp096 && player1.ReferenceHub.playerStats.Health < Config.SCP096MaxHealth)
							player1.ReferenceHub.playerStats.Health = player1.ReferenceHub.playerStats.Health + Config.SCPHealth;
						if (player1.ReferenceHub.characterClassManager.CurClass == RoleType.Scp173 && player1.ReferenceHub.playerStats.Health < Config.SCP173MaxHealth)
							player1.ReferenceHub.playerStats.Health = player1.ReferenceHub.playerStats.Health + Config.SCPHealth;
						if (player1.ReferenceHub.characterClassManager.CurClass == RoleType.Scp93953 && player1.ReferenceHub.playerStats.Health < Config.SCP939MaxHealth)
							player1.ReferenceHub.playerStats.Health = player1.ReferenceHub.playerStats.Health + Config.SCPHealth;
						if (player1.ReferenceHub.characterClassManager.CurClass == RoleType.Scp93989 && player1.ReferenceHub.playerStats.Health < Config.SCP939MaxHealth)
							player1.ReferenceHub.playerStats.Health = player1.ReferenceHub.playerStats.Health + Config.SCPHealth;
						if (player1.ReferenceHub.characterClassManager.CurClass == RoleType.Scp0492 && player1.ReferenceHub.playerStats.Health < Config.SCP0492MaxHealth)
							player1.ReferenceHub.playerStats.Health = player1.ReferenceHub.playerStats.Health + Config.SCPHealth;
						if (player1.ReferenceHub.characterClassManager.CurClass == RoleType.Scp106 && player1.ReferenceHub.playerStats.Health < Config.SCP106MaxHealth)
							player1.ReferenceHub.playerStats.Health = player1.ReferenceHub.playerStats.Health + Config.SCP106Health;
					}
				}
			}
		}
		public void CheckRoundEnd(EndingRoundEventArgs ev)
		{
			List<Team> list = new List<Team>();
			foreach (Player cat in Player.List)
				list.Add(cat.Team);
			foreach (Player cat in Player.List)
				if (scpspsidq.Contains(cat.ReferenceHub.queryProcessor.PlayerId) || scpspsjdidq.Contains(cat.ReferenceHub.queryProcessor.PlayerId))
				{
					if (cat.ReferenceHub.characterClassManager.IsHuman())
					{
						if (!list.Contains(Team.CDP) && !list.Contains(Team.MTF) && !list.Contains(Team.RSC) && list.Contains(Team.SCP))
						{
							ev.IsRoundEnded = true;
							return;
						}
						else if(list.Contains(Team.CDP) && !list.Contains(Team.MTF) && !list.Contains(Team.RSC) && list.Contains(Team.SCP))
						{
							classd = 0;
							foreach(Player player in Player.List)
							{
								if(player.Role == RoleType.ClassD && (!scpspsjdidq.Contains(player.Id)))
								{
									classd++;
								}
							}
							if(classd == 0)
							{
								ev.IsRoundEnded = true;
							}
						}
						if (!list.Contains(Team.SCP) && (list.Contains(Team.CDP) || list.Contains(Team.CHI) || list.Contains(Team.MTF) || list.Contains(Team.RSC)))
						{
							ev.IsRoundEnded = false;
						}
					}
				}
		}
		public void OnRoundEnd(RoundEndedEventArgs ev)
		{
		}
		public void OnPlayerDeath(DiedEventArgs ev)
		{
			for (int j = 0; j < scp181idq.Count; j++)
			{
				int num2 = scp181idq[j];
				bool flag2 = num2 == ev.Target.ReferenceHub.queryProcessor.PlayerId;
				if (flag2)
				{
					scp181idq.Remove(num2);
					ev.Target.ReferenceHub.serverRoles.SetColor("");
					ev.Target.ReferenceHub.serverRoles.SetText("");
				}
			}
			for (int k = 0; k < scpspsidq.Count; k++)
			{
				int num3 = scpspsidq[k];
				bool flag3 = num3 == ev.Target.ReferenceHub.queryProcessor.PlayerId;
				if (flag3)
				{
					scpspsidq.Remove(num3);
					ev.Target.ReferenceHub.serverRoles.SetColor("");
					ev.Target.ReferenceHub.serverRoles.SetText("");
				}
			}
			for (int l = 0; l < scpspsjdidq.Count; l++)
			{
				int num4 = scpspsjdidq[l];
				bool flag4 = num4 == ev.Target.ReferenceHub.queryProcessor.PlayerId;
				if (flag4)
				{
					scpspsjdidq.Remove(num4);
					ev.Target.ReferenceHub.serverRoles.SetColor("");
					ev.Target.ReferenceHub.serverRoles.SetText("");
				}
			}
			for (int m = 0; m < scplbidq.Count; m++)
			{
				int num5 = scplbidq[m];
				bool flag5 = num5 == ev.Target.ReferenceHub.queryProcessor.PlayerId;
				if (flag5)
				{
					scplbidq.Remove(num5);
					ev.Target.ReferenceHub.serverRoles.SetColor("");
					ev.Target.ReferenceHub.serverRoles.SetText("");
				}
			}
			for (int n = 0; n < scplzridq.Count; n++)
			{
				int num6 = scplzridq[n];
				bool flag6 = num6 == ev.Target.ReferenceHub.queryProcessor.PlayerId;
				if (flag6)
				{
					scplzridq.Remove(num6);
					ev.Target.ReferenceHub.serverRoles.SetColor("");
					ev.Target.ReferenceHub.serverRoles.SetText("");
				}
			}
			for (int n = 0; n < scpysyidq.Count; n++)
			{
				int num6 = scpysyidq[n];
				bool flag6 = num6 == ev.Target.ReferenceHub.queryProcessor.PlayerId;
				if (flag6)
				{
					scpysyidq.Remove(num6);
					ev.Target.ReferenceHub.serverRoles.SetColor("");
					ev.Target.ReferenceHub.serverRoles.SetText("");
				}
			}
		}
		public void OnPlayerLeave(LeftEventArgs ev)
		{
			breakid.Add(ev.Player.ReferenceHub.queryProcessor.PlayerId);
			for (int j = 0; j < scp181idq.Count; j++)
			{
				int num2 = scp181idq[j];
				bool flag2 = num2 == ev.Player.ReferenceHub.queryProcessor.PlayerId;
				if (flag2)
				{
					scp181idq.Remove(num2);
					ev.Player.ReferenceHub.serverRoles.SetColor("");
					ev.Player.ReferenceHub.serverRoles.SetText("");
				}
			}
			for (int k = 0; k < scpspsidq.Count; k++)
			{
				int num3 = scpspsidq[k];
				bool flag3 = num3 == ev.Player.ReferenceHub.queryProcessor.PlayerId;
				if (flag3)
				{
					scpspsidq.Remove(num3);
					ev.Player.ReferenceHub.serverRoles.SetColor("");
					ev.Player.ReferenceHub.serverRoles.SetText("");
				}
			}
			for (int l = 0; l < scpspsjdidq.Count; l++)
			{
				int num4 = scpspsjdidq[l];
				bool flag4 = num4 == ev.Player.ReferenceHub.queryProcessor.PlayerId;
				if (flag4)
				{
					scpspsjdidq.Remove(num4);
					ev.Player.ReferenceHub.serverRoles.SetColor("");
					ev.Player.ReferenceHub.serverRoles.SetText("");
				}
			}
			for (int m = 0; m < scplbidq.Count; m++)
			{
				int num5 = scplbidq[m];
				bool flag5 = num5 == ev.Player.ReferenceHub.queryProcessor.PlayerId;
				if (flag5)
				{
					scplbidq.Remove(num5);
					ev.Player.ReferenceHub.serverRoles.SetColor("");
					ev.Player.ReferenceHub.serverRoles.SetText("");
				}
			}
			for (int n = 0; n < scplzridq.Count; n++)
			{
				int num6 = scplzridq[n];
				bool flag6 = num6 == ev.Player.ReferenceHub.queryProcessor.PlayerId;
				if (flag6)
				{
					scplzridq.Remove(num6);
					ev.Player.ReferenceHub.serverRoles.SetColor("");
					ev.Player.ReferenceHub.serverRoles.SetText("");
				}
			}
			for (int n = 0; n < scpysyidq.Count; n++)
			{
				int num6 = scpysyidq[n];
				bool flag6 = num6 == ev.Player.ReferenceHub.queryProcessor.PlayerId;
				if (flag6)
				{
					scpysyidq.Remove(num6);
					ev.Player.ReferenceHub.serverRoles.SetColor("");
					ev.Player.ReferenceHub.serverRoles.SetText("");
				}
			}
		}
		public void OnDoorInteract(InteractingDoorEventArgs ev)
		{
			if (scplzridq.Contains(ev.Player.ReferenceHub.queryProcessor.PlayerId))
			{
				int onlyRandom = EventHandlers.GetOnlyRandom(1, 100);
				if (onlyRandom <= 9)
				{
					ev.IsAllowed = true;
					ev.Door.DestroyDoor(true);
				}
			}
			if (scp181idq.Contains(ev.Player.ReferenceHub.queryProcessor.PlayerId))
			{
				bool flag4 = false;
				if (ev.Player.ReferenceHub.inventory.curItem == ItemType.KeycardChaosInsurgency ||
					ev.Player.ReferenceHub.inventory.curItem == ItemType.KeycardContainmentEngineer ||
					ev.Player.ReferenceHub.inventory.curItem == ItemType.KeycardFacilityManager ||
					ev.Player.ReferenceHub.inventory.curItem == ItemType.KeycardGuard ||
					ev.Player.ReferenceHub.inventory.curItem == ItemType.KeycardJanitor ||
					ev.Player.ReferenceHub.inventory.curItem == ItemType.KeycardNTFCommander ||
					ev.Player.ReferenceHub.inventory.curItem == ItemType.KeycardNTFLieutenant ||
					ev.Player.ReferenceHub.inventory.curItem == ItemType.KeycardO5 ||
					ev.Player.ReferenceHub.inventory.curItem == ItemType.KeycardScientist ||
					ev.Player.ReferenceHub.inventory.curItem == ItemType.KeycardScientistMajor ||
					ev.Player.ReferenceHub.inventory.curItem == ItemType.KeycardSeniorGuard ||
					ev.Player.ReferenceHub.inventory.curItem == ItemType.KeycardZoneManager)
				{
					flag4 = true;
				}
				if (ev.Door.permissionLevel.Equals(""))
				{
					flag4 = true;
				}
				if (!flag4)
				{
					int onlyRandom2 = GetOnlyRandom(1, 100);
					int num = 18;
					if (onlyRandom2 <= num && !ev.Door.lockdown)
					{
						ev.IsAllowed = true;
						return;
					}
				}
			}
			if (ready.Contains(ev.Player.ReferenceHub.queryProcessor.PlayerId))
			{
				bool flag4 = false;
				if (ev.Player.ReferenceHub.inventory.curItem == ItemType.KeycardChaosInsurgency ||
					ev.Player.ReferenceHub.inventory.curItem == ItemType.KeycardContainmentEngineer ||
					ev.Player.ReferenceHub.inventory.curItem == ItemType.KeycardFacilityManager ||
					ev.Player.ReferenceHub.inventory.curItem == ItemType.KeycardGuard ||
					ev.Player.ReferenceHub.inventory.curItem == ItemType.KeycardJanitor ||
					ev.Player.ReferenceHub.inventory.curItem == ItemType.KeycardNTFCommander ||
					ev.Player.ReferenceHub.inventory.curItem == ItemType.KeycardNTFLieutenant ||
					ev.Player.ReferenceHub.inventory.curItem == ItemType.KeycardO5 ||
					ev.Player.ReferenceHub.inventory.curItem == ItemType.KeycardScientist ||
					ev.Player.ReferenceHub.inventory.curItem == ItemType.KeycardScientistMajor ||
					ev.Player.ReferenceHub.inventory.curItem == ItemType.KeycardSeniorGuard ||
					ev.Player.ReferenceHub.inventory.curItem == ItemType.KeycardZoneManager)
				{
					flag4 = true;
				}
				if (ev.Door.permissionLevel.Equals(""))
				{
					flag4 = true;
				}
				if (!flag4)
				{
					int onlyRandom2 = GetOnlyRandom(1, 100);
					int num = 50;
					if (onlyRandom2 <= num && !ev.Door.lockdown)
					{
						ev.IsAllowed = true;
						return;
					}
				}
			}
		}
		public void OnUseMedicalItem(UsedMedicalItemEventArgs ev)
		{
			if (ev.Item == ItemType.SCP500)
			{
				breakid.Add(ev.Player.ReferenceHub.queryProcessor.PlayerId);
			}
		}
		public void OnPlayerHurt(HurtingEventArgs ev)
		{
			float 最终伤害 = ev.Amount;
			float 对面反伤 = 0;
			bool 蛇之手间谍攻击 = scpspsjdidq.Contains(ev.Attacker.ReferenceHub.queryProcessor.PlayerId);
			bool 蛇之手攻击 = scpspsidq.Contains(ev.Attacker.ReferenceHub.queryProcessor.PlayerId);
			bool 蛇之手间谍受伤 = scpspsjdidq.Contains(ev.Target.ReferenceHub.queryProcessor.PlayerId);
			bool 幸运儿攻击 = scp181idq.Contains(ev.Attacker.ReferenceHub.queryProcessor.PlayerId);
			bool 老八攻击 = scplbidq.Contains(ev.Attacker.ReferenceHub.queryProcessor.PlayerId);
			bool 无赖战士攻击 = scplzridq.Contains(ev.Attacker.ReferenceHub.queryProcessor.PlayerId);
			bool 燕双鹰攻击 = scpysyidq.Contains(ev.Attacker.ReferenceHub.queryProcessor.PlayerId);
			bool 蛇之手受伤 = scpspsidq.Contains(ev.Target.ReferenceHub.queryProcessor.PlayerId);
			bool 燕双鹰受伤 = scpysyidq.Contains(ev.Target.ReferenceHub.queryProcessor.PlayerId);
			bool 无赖战士受伤 = scplzridq.Contains(ev.Target.ReferenceHub.queryProcessor.PlayerId);
			bool 老八受伤 = scplbidq.Contains(ev.Target.ReferenceHub.queryProcessor.PlayerId);
			bool 幸运儿受伤 = scp181idq.Contains(ev.Target.ReferenceHub.queryProcessor.PlayerId);
			bool 自残 = ev.Attacker == ev.Target;
			bool 是否免伤 = false;
			if (幸运儿受伤)
			{
				bool flag4 = ev.DamageType == DamageTypes.Nuke;
				if (flag4)
				{
					ev.Target.Broadcast(3, "<color=#FF0000>[SCP-181特殊能力]</color>\n<color=green>不能免疫核弹伤害,跑路了,告辞!</color>");
				}
				int onlyRandom = GetOnlyRandom(1, 100);
				if (onlyRandom <= 50 && ev.DamageType != DamageTypes.Nuke)
				{
					ev.Target.Broadcast(3, "<color=#FF0000>[SCP-181特殊能力]</color>\n<color=green>成功免疫该伤害</color>");
					if (ev.Attacker != null)
					{
						ev.Attacker.Broadcast(3, "<color=#FF0000>[SCP-181特殊能力]</color>\n<color=green>您的攻击已被免疫</color>");
					}
					最终伤害 = 0f;
					是否免伤 = true;

				}
			}
			if (自残)
			{
				if (无赖战士受伤)
				{
					if (ev.DamageType == DamageTypes.Scp207)
					{
						最终伤害 = 0f;
					}
					if (ev.DamageType == DamageTypes.Falldown)
					{
						最终伤害 = 0f;
					}
				}
				if (燕双鹰受伤)
				{
					if (ev.DamageType == DamageTypes.Scp207)
					{
						最终伤害 = 0f;
					}
					if (ev.DamageType == DamageTypes.Falldown)
					{
						最终伤害 = 0f;
					}
				}

			}
			else
			{
				if (蛇之手间谍受伤)
				{
					if (ev.Attacker.ReferenceHub.characterClassManager.IsHuman() && !ev.Attacker.ReferenceHub.characterClassManager.IsAnyScp())
					{
						最终伤害 = 3f;
					}
					if (ev.Attacker.ReferenceHub.characterClassManager.IsAnyScp())
					{
						最终伤害 = 0f;
					}
					if (ev.DamageType == DamageTypes.RagdollLess)
					{
						最终伤害 = 0f;
					}
				}
				if (蛇之手间谍攻击)
				{
					if (ev.Target.Team == Team.SCP)
					{
						最终伤害 = 0f;
					}
					if (scpspsidq.Contains(ev.Target.ReferenceHub.queryProcessor.PlayerId))
					{
						最终伤害 = 0f;
					}
				}
				if (蛇之手受伤)
				{
					if (ev.Attacker.ReferenceHub.characterClassManager.IsAnyScp())
					{
						最终伤害 = 0f;
					}
				}
				if (蛇之手攻击)
				{
					if (scpspsjdidq.Contains(ev.Target.ReferenceHub.queryProcessor.PlayerId))
					{
						最终伤害 = 0f;
					}
				}
				if (燕双鹰受伤)
				{
					if (!ev.Target.ReferenceHub.characterClassManager.IsAnyScp())
					{
						对面反伤 = 5f;
						if (蛇之手间谍攻击)
							对面反伤 = 1f;
						最终伤害 = 最终伤害 * 0.3f;
					}
					else
					{
						对面反伤 = 20f;
						if (ev.DamageType == DamageTypes.Scp096)
						{
							最终伤害 = 12f;
						}
						if (ev.DamageType == DamageTypes.Scp0492)
						{
							最终伤害 = 10f;
						}
						if (ev.DamageType == DamageTypes.Scp106)
						{
							最终伤害 = 15f;
						}
						if (ev.DamageType == DamageTypes.MicroHid)
						{
							最终伤害 = 1.3f;
						}
						if (ev.DamageType == DamageTypes.Pocket)
						{
							最终伤害 = 0f;
						}
						if (ev.DamageType == DamageTypes.Grenade)
						{
							最终伤害 = (最终伤害 * 1.5f) / 100;
						}
						if (ev.DamageType == DamageTypes.Scp173 || ev.DamageType == DamageTypes.Scp939 || ev.DamageType == DamageTypes.Scp049)
						{
							最终伤害 = 20f;
						}
					}
					int num = GetOnlyRandom(1, 100);
					if (num <= 20)
					{
						最终伤害 = 0f;
					}
				}
				if (燕双鹰攻击)
				{
					if (ev.DamageType != DamageTypes.Grenade || ev.DamageType != DamageTypes.MicroHid)
					{
						if (ev.Target.ReferenceHub.characterClassManager.IsAnyScp())
						{
							if (useing2.Contains(ev.Target.ReferenceHub.queryProcessor.PlayerId)) { 最终伤害 = 最终伤害 + 最终伤害 * 1f; } else { 最终伤害 = 最终伤害 + 最终伤害 * 0.5f; }

						}
						else
						{
							int num = GetOnlyRandom(1, 100);
							if (num > 50)
							{
								最终伤害 = 最终伤害 + 最终伤害 * 0.2f;
							}
							if (!老八受伤)
							{
								if (num > 99)
								{
									最终伤害 = 999999f;
								}
								if (蛇之手间谍受伤)
								{
									if (num > 94)
									{
										最终伤害 = 999999f;
									}
								}
								if (无赖战士受伤)
								{
									if (num > 89)
									{
										最终伤害 = 999999f;
									}
								}
							}
							else
							{
								最终伤害 = 最终伤害 + 最终伤害 * 2.0f;
							}
						}
					}
					if (ev.DamageType == DamageTypes.Usp && 最终伤害 != 999999f)
					{
						if (ev.Target.ReferenceHub.characterClassManager.IsAnyScp())
						{ 最终伤害 = 200f; }
						else { 最终伤害 = 99f; }
						if (老八受伤) { 最终伤害 = 200f; }
						if (无赖战士受伤) { 最终伤害 = 70f; }
					}
					if (蛇之手间谍受伤)
					{
						最终伤害 = 5f;
					}
				}
				if (无赖战士受伤)
				{
					if (ev.Target.ReferenceHub.characterClassManager.IsHuman() && !ev.Target.ReferenceHub.characterClassManager.IsAnyScp())
					{
						if (
						!老八攻击 &&
						!无赖战士攻击 &&
						!燕双鹰攻击)
						{
							最终伤害 = 70f;
							if (useing2.Contains(ev.Target.ReferenceHub.queryProcessor.PlayerId))
								最终伤害 = 2f;
						}
					}
					else if (ev.Target.ReferenceHub.characterClassManager.IsAnyScp())
					{
						if (ev.DamageType != DamageTypes.Scp106)
							最终伤害 = 13f;
						else
						{
							最终伤害 = 10f;
						}
						if (useing2.Contains(ev.Target.ReferenceHub.queryProcessor.PlayerId))
							最终伤害 = 5f;
					}
					if (燕双鹰攻击 && useing2.Contains(ev.Attacker.ReferenceHub.queryProcessor.PlayerId) && GetOnlyRandom(0, 100) < 10)
					{
						最终伤害 = 99999f;
					}
					if (燕双鹰攻击)
					{
						if (ev.DamageType == DamageTypes.Usp)
							最终伤害 = 25f;
						else
							最终伤害 = 最终伤害 * 0.2f;
					}
					if (蛇之手间谍攻击)
					{
						对面反伤 = 2f;
					}
				}
				if (无赖战士攻击)
				{
				}
				if (老八受伤)
				{
					if (ev.DamageType == DamageTypes.MicroHid)
					{
						对面反伤 = ev.Amount / 25f;
					}
					else if (ev.DamageType != DamageTypes.Grenade)
					{
						对面反伤 = 5f;
					}
					if (无赖战士攻击)
					{
						对面反伤 = 1f;
					}
				}
				if (老八攻击)
				{
					最终伤害 = 0f;
				}
				if (幸运儿受伤)
				{

				}
				if (幸运儿攻击)
				{
				}
				if (ev.Target.Team == Team.CDP && !蛇之手间谍受伤)
				{
					if (ev.Attacker.Team == Team.CHI && !蛇之手间谍攻击)
					{
						对面反伤 = 0f;
						最终伤害 = 0f;
					}
					if (ev.Attacker.Team == Team.CDP && !蛇之手间谍攻击)
					{
						对面反伤 = 0f;
						最终伤害 = 0f;
					}
				}
				if (ev.Target.Team == Team.RSC && !蛇之手间谍受伤)
				{
					if (ev.Attacker.Team == Team.MTF || ev.Attacker.Team == Team.RSC && !蛇之手间谍攻击)
					{
						对面反伤 = 0f;
						最终伤害 = 0f;
					}
					if (ev.Attacker.Team == Team.RSC || ev.Attacker.Team == Team.RSC && !蛇之手间谍攻击)
					{
						对面反伤 = 0f;
						最终伤害 = 0f;
					}
				}
				if (ev.Target.Team == Team.MTF && !蛇之手间谍受伤)
				{
					if (ev.Attacker.Team == Team.MTF || ev.Attacker.Team == Team.RSC && !蛇之手间谍攻击)
					{
						对面反伤 = 0f;
						最终伤害 = 0f;
					}
				}
				if (ev.Target.Team == Team.CHI && !蛇之手间谍受伤)
				{
					if (ev.Attacker.Team == Team.CDP && !蛇之手间谍攻击)
					{
						对面反伤 = 0f;
						最终伤害 = 0f;
					}
					if (ev.Attacker.Team == Team.CHI && !蛇之手间谍攻击)
					{
						对面反伤 = 0f;
						最终伤害 = 0f;
					}
				}
				if (ev.Target.Team == Team.SCP || 蛇之手间谍受伤)
				{
					if (ev.Attacker.Team == Team.SCP || 蛇之手间谍攻击)
					{
						对面反伤 = 0f;
						最终伤害 = 0f;
					}
				}
				ev.Amount = 最终伤害;
				ev.Attacker.Health -= 对面反伤;
			}

		}

		public void ConsoleCommand(SendingConsoleCommandEventArgs ev)
		{
			if(ev.Name == "bc")
			{
				if(ev.Arguments.Count >= 1)
				{
					string txt = "";
					for (int i = 0; i < ev.Arguments.Count; i++)
					{
						txt += " "+ ev.Arguments[i];
					}
					Map.Broadcast(5,ev.Player.Nickname + ":"+txt)
				}
			}
			if (ev.Name == "1")
			{
				bool flag5 = used2.Contains(ev.Player.ReferenceHub.queryProcessor.PlayerId);
				if (flag5)
				{
					ev.ReturnMessage = "已经使用过了。";
				}
				else
				{
					bool flag6 = scpysyidq.Contains(ev.Player.ReferenceHub.queryProcessor.PlayerId);
					if (flag6)
					{
						used2.Add(ev.Player.ReferenceHub.queryProcessor.PlayerId);
						Timing.RunCoroutine(test1(ev.Player.ReferenceHub.queryProcessor.PlayerId, 1));
						ev.ReturnMessage = "攻击人形（非对友）目标100%暴毙，攻击SCP目标伤害提高200%（3倍伤害）,持续时间10秒";

					}
				}
			}
		}
		public IEnumerator<float> test1(int id, int type)
		{
			yield return Timing.WaitForSeconds(3f);
			useing2.Add(id);
			yield return Timing.WaitForSeconds(12f);
			if (type == 1)
			{
				useing2.Remove(id);
			}
		}
		public void OnPlayerPickupItem(PickingUpItemEventArgs ev)
		{
			bool flag = scplzridq.Contains(ev.Player.ReferenceHub.queryProcessor.PlayerId);
			if (flag)
			{
				bool flag2 = ev.Pickup.info.itemId != ItemType.Ammo556;
				bool flag3 = ev.Pickup.info.itemId != ItemType.Ammo762;
				bool flag4 = ev.Pickup.info.itemId != ItemType.Ammo9mm;
				if (flag2 || flag3 || flag4)
				{
					ev.IsAllowed = false;
				}
			}
			else
			{
				bool flag5 = scpysyidq.Contains(ev.Player.ReferenceHub.queryProcessor.PlayerId);
				if (flag5)
				{
					bool flag6 = ev.Pickup.info.itemId == ItemType.Ammo9mm;
					if (flag6)
					{
						ev.IsAllowed = false;
						return;
					}
					bool flag7 = ev.Pickup.info.itemId == ItemType.Adrenaline;
					if (flag7)
					{
						ev.IsAllowed = false;
						return;
					}
					bool flag8 = ev.Pickup.info.itemId == ItemType.Painkillers;
					if (flag8)
					{
						ev.IsAllowed = false;
						return;
					}
					bool flag9 = ev.Pickup.info.itemId == ItemType.SCP500;
					if (flag9)
					{
						ev.IsAllowed = false;
						return;
					}
					bool flag10 = ev.Pickup.info.itemId == ItemType.GunUSP;
					if (flag10)
					{
						ev.IsAllowed = false;
						return;
					}
					bool flag11 = ev.Pickup.info.itemId == ItemType.Medkit;
					if (flag11)
					{
						ev.IsAllowed = false;
						return;
					}
					bool flag12 = ev.Pickup.info.itemId == ItemType.SCP268;
					if (flag12)
					{
						ev.IsAllowed = false;
						return;
					}
					bool flag14 = ev.Pickup.info.itemId == ItemType.Coin;
					if (flag14)
					{
						ev.IsAllowed = false;
						return;
					}
				}
				bool flag13 = Baba.Contains(ev.Pickup);
				if (flag13)
				{
					bool flag15 = Canteatshit.Contains(ev.Player.ReferenceHub.queryProcessor.PlayerId);
					if (flag15)
					{
						ev.Player.Broadcast(7, "[系统信息]你无法进食更香的屎");
						ev.IsAllowed = false;
					}
					else
					{
						bool flag16 = scpspsidq.Contains(ev.Player.ReferenceHub.queryProcessor.PlayerId);
						if (flag16)
						{
							ev.IsAllowed = false;
							ev.Player.Broadcast(1, "<color=#ff9900>你不是老八的继承人喔！</color>");
						}
						else
						{
							bool flag17 = scpspsjdidq.Contains(ev.Player.ReferenceHub.queryProcessor.PlayerId);
							if (flag17)
							{
								ev.IsAllowed = false;
								ev.Player.Broadcast(1, "<color=#ff9900>你不是老八的继承人喔！</color>");
							}
							else
							{
								int onlyRandom = EventHandlers.GetOnlyRandom(1, 100);
								int num = 1;
								foreach (int num2 in Eatid)
								{
									bool flag18 = num2 == ev.Player.ReferenceHub.queryProcessor.PlayerId;
									if (flag18)
									{
										num++;
									}
								}
								bool flag19 = num >= 4;
								if (flag19)
								{
									ev.IsAllowed = false;
									ev.Player.Broadcast(1, "<color=#ff9900>虽然你上辈子是老八。但是我绝不会吃如此肮脏的东西</color>");
								}
								else
								{
									bool flag20 = num >= 3;
									if (flag20)
									{
										ev.IsAllowed = false;
										ev.Pickup.Delete();
										Eatid.Add(ev.Player.ReferenceHub.queryProcessor.PlayerId);
										ev.Player.Broadcast(3, "<color=#ff9900>老铁们啊，还是那句话，只有你们想不到的，没有老八做不到的。你们不要笑我狼狈不堪，我也可以笑你离开你的父母比我吃屎都难。奥利给！干了！兄弟们！</color>");
										ev.Player.Broadcast(4, "<color=#ff9900>原地卡住不动，持续快速掉血--到死为止，无法使用500消除</color>");
										Timing.RunCoroutine(Laobakouxue3(ev.Player.ReferenceHub.queryProcessor.PlayerId, ev.Player.Position));
									}
									else
									{
										bool flag21 = num < 3;
										if (flag21)
										{
											Eatid.Add(ev.Player.ReferenceHub.queryProcessor.PlayerId);
											bool flag22 = ev.Player.ReferenceHub.inventory.items.Count >= 8;
											if (flag22)
											{
												ev.IsAllowed = false;
												ev.Player.Broadcast(1, "<color=#ff9900>我老八今天吃不动了</color>");
											}
											else
											{
												ev.IsAllowed = false;
												ev.Pickup.Delete();
											}
											bool flag23 = Baba1.Contains(ev.Pickup);
											if (flag23)
											{
												bool flag24 = scplbidq.Count < 1;
												if (flag24)
												{
													ev.Player.Broadcast(7, "<color=#ff9900>[系统信息]恭喜化身老八，吃更香的屎</color>");
													Timing.RunCoroutine(makescp(ev.Player.ReferenceHub.queryProcessor.PlayerId, "lb"));
													return;
												}
											}
											bool flag25 = onlyRandom <= 16;
											if (flag25)
											{
												bool flag26 = num == 1;
												if (flag26)
												{
													ev.Player.Broadcast(3, "<color=#ff9900>老铁们啊，只有你们想不到的，没有老八做不到的。还是那句话，今天我老八挑战一把吃粑粑。兄弟们，奥利给！干了！</color>");
													bool flag27 = EventHandlers.GetOnlyRandom(1, 100) <= 50;
													if (flag27)
													{
														ev.Player.Broadcast(4, "<color=#ff9900>直接掉血80</color>");
														ev.Player.ReferenceHub.playerStats.HurtPlayer(new PlayerStats.HitInfo(80f, ev.Player.ReferenceHub.nicknameSync.Network_myNickSync, DamageTypes.Scp207, ev.Player.ReferenceHub.queryProcessor.PlayerId), ev.Player.GameObject);
													}
													else
													{
														ev.Player.Broadcast(4, "<color=#ff9900>效果弹幕显示过后，每隔10秒钟卡住一次，卡一次3秒钟，同时掉血30，共卡住3次，第三次之后效果消失，食用500可消除</color>");
														Timing.RunCoroutine(Laobakouxue(ev.Player.ReferenceHub.queryProcessor.PlayerId, 30f, ev.Player.Position));
													}
												}
												else
												{
													bool flag28 = num == 2;
													if (flag28)
													{
														ev.Player.Broadcast(3, "<color=#ff9900>老铁们啊，虽然不是同一时间，但是同一个厕所。老八我再次挑战一把吃粑粑，奥利给！干了！兄弟们！</color>");
														bool flag29 = EventHandlers.GetOnlyRandom(1, 100) <= 50;
														if (flag29)
														{
															ev.Player.Broadcast(4, "<color=#ff9900>直接掉血99</color>");
															ev.Player.ReferenceHub.playerStats.HurtPlayer(new PlayerStats.HitInfo(99f, ev.Player.ReferenceHub.nicknameSync.Network_myNickSync, DamageTypes.Scp207, ev.Player.ReferenceHub.queryProcessor.PlayerId), ev.Player.GameObject);
														}
														else
														{
															ev.Player.Broadcast(4, "<color=#ff9900>效果弹幕显示过后，每隔10秒钟卡住一次，卡一次3秒钟，同时掉血33，共卡住3次，第三次之后效果消失，食用500可消除</color>");
															Timing.RunCoroutine(Laobakouxue(ev.Player.ReferenceHub.queryProcessor.PlayerId, 33f, ev.Player.Position));
														}
													}
												}
											}
											else
											{
												bool flag30 = onlyRandom <= 21;
												if (flag30)
												{
													ev.Player.Broadcast(1, "<color=#ff9900>[2]一日三餐没烦恼，今天就吃老八秘制小汉堡，既实惠还管饱</color>");
													ev.Player.Broadcast(1, "<color=#ff9900>[1]一日三餐没烦恼，今天就吃老八秘制小汉堡，既实惠还管饱</color>");
													ev.Player.Broadcast(4, "<color=#ff9900>回满血，摸门概率50，获得肾上腺素和SCP500各一个</color>");
													ev.Player.ReferenceHub.playerStats.Health = (float)ev.Player.ReferenceHub.playerStats.maxHP;
													ev.Player.ReferenceHub.inventory.AddNewItem(ItemType.Adrenaline, -4.65664672E+11f, 0, 0, 0);
													ev.Player.ReferenceHub.inventory.AddNewItem(ItemType.SCP500, -4.65664672E+11f, 0, 0, 0);
													ready.Add(ev.Player.ReferenceHub.queryProcessor.PlayerId);
													Timing.RunCoroutine(test(ev.Player.ReferenceHub.queryProcessor.PlayerId));
												}
												else
												{
													bool flag31 = onlyRandom <= 26;
													if (flag31)
													{
														ev.Player.Broadcast(1, "<color=#ff9900>[2]美食界里我老八，今天吃个哈密瓜，往里倒点臭卤虾，万人称我美食家</color>");
														ev.Player.Broadcast(1, "<color=#ff9900>[1]美食界里我老八，今天吃个哈密瓜，往里倒点臭卤虾，万人称我美食家</color>");
														ev.Player.Broadcast(4, "<color=#ff9900>获得电磁炮配大机枪");
														ev.Player.ReferenceHub.inventory.AddNewItem(ItemType.MicroHID, -4.65664672E+11f, 0, 0, 0);
														ev.Player.ReferenceHub.inventory.AddNewItem(ItemType.GunLogicer, -4.65664672E+11f, 0, 0, 0);
													}
													else
													{
														bool flag32 = onlyRandom <= 38;
														if (flag32)
														{
															ev.Player.Broadcast(1, "<color=#ff9900>[2]都说面条吃不饱，就怪配料没放好，臭豆腐腐乳黄瓜丝，迟到嘴里美汁er汁er</color>");
															ev.Player.Broadcast(1, "<color=#ff9900>[1]都说面条吃不饱，就怪配料没放好，臭豆腐腐乳黄瓜丝，迟到嘴里美汁er汁er</color>");
															ev.Player.Broadcast(4, "<color=#ff9900>即效生命值30，可乐一瓶，肾上腺素一个");
															ev.Player.ReferenceHub.inventory.AddNewItem(ItemType.SCP207, -4.65664672E+11f, 0, 0, 0);
															ev.Player.ReferenceHub.inventory.AddNewItem(ItemType.Adrenaline, -4.65664672E+11f, 0, 0, 0);
															ev.Player.ReferenceHub.playerStats.Health = ev.Player.ReferenceHub.playerStats.Health + 30f;
														}
														else
														{
															bool flag33 = onlyRandom <= 52;
															if (flag33)
															{
																ev.Player.Broadcast(1, "<color=#ff9900>[2]腐乳豆腐拌香蕉，简简单单的夜宵，不管是香还是臭，到我嘴</color>");
																ev.Player.Broadcast(1, "<color=#ff9900>[1]腐乳豆腐拌香蕉，简简单单的夜宵，不管是香还是臭，到我嘴</color>");
																ev.Player.Broadcast(4, "<color=#ff9900>即效生命值20，回复生命值每秒2血持续40s</color>");
																ev.Player.ReferenceHub.playerStats.Health = ev.Player.ReferenceHub.playerStats.Health + 20f;
																Timing.RunCoroutine(Laobahuixue(ev.Player.ReferenceHub.queryProcessor.PlayerId, 40, 2));
															}
															else
															{
																bool flag34 = onlyRandom <= 67;
																if (flag34)
																{
																	ev.Player.Broadcast(1, "<color=#ff9900>[2]美食界里我老八，今天吃个大扒鸭，要问扒鸭哪里好，扒鸭屁股是块宝</color>");
																	ev.Player.Broadcast(1, "<color=#ff9900>[1]美食界里我老八，今天吃个大扒鸭，要问扒鸭哪里好，扒鸭屁股是块宝</color>");
																	ev.Player.Broadcast(4, "<color=#ff9900>传送至173旁边（若无173则传送至任意非079的SCP旁边，若无SCP则传送至地面B电梯旁）</color>");
																	List<Player> list = new List<Player>();
																	foreach (Player referenceHub in Player.List)
																	{
																		bool flag35 = referenceHub.Team == Team.SCP;
																		if (flag35)
																		{
																			bool flag36 = referenceHub.ReferenceHub.characterClassManager.CurClass != RoleType.Scp079;
																			if (flag36)
																			{
																				list.Add(referenceHub);
																			}
																		}
																		bool flag37 = referenceHub.ReferenceHub.characterClassManager.CurClass == RoleType.Scp173;
																		if (flag37)
																		{
																			ev.Player.ReferenceHub.playerMovementSync.OverridePosition(referenceHub.Position, 0f, false);
																			return;
																		}
																	}
																	bool flag38 = list.Count < 1;
																	if (flag38)
																	{
																		ev.Player.ReferenceHub.playerMovementSync.OverridePosition(new Vector3(87f, 994f, -49f), 0f, false);
																	}
																	ev.Player.ReferenceHub.playerMovementSync.OverridePosition(list[0].Position, 0f, false);
																}
																else
																{
																	bool flag39 = onlyRandom <= 85;
																	if (flag39)
																	{
																		ev.Player.Broadcast(1, "<color=#ff9900>[2]老铁们啊，尝不到苦辣，永远不知道酸甜，今天老八挑战一把酸甜苦辣咸，奥利给，干了兄弟们！</color>");
																		ev.Player.Broadcast(1, "<color=#ff9900>[1]老铁们啊，尝不到苦辣，永远不知道酸甜，今天老八挑战一把酸甜苦辣咸，奥利给，干了兄弟们！</color>");
																		ev.Player.Broadcast(4, "<color=#ff9900>随机传送</color>");
																		int onlyRandom2 = EventHandlers.GetOnlyRandom(1, 5);
																		bool flag40 = onlyRandom2 == 1;
																		if (flag40)
																		{
																			foreach (Player referenceHub2 in Player.List)
																			{
																				bool flag41 = referenceHub2.ReferenceHub.characterClassManager.CurClass == 0;
																				if (flag41)
																				{
																					ev.Player.ReferenceHub.playerMovementSync.OverridePosition(referenceHub2.Position, 0f, false);
																					return;
																				}
																			}
																		}
																		bool flag42 = onlyRandom2 == 2;
																		if (flag42)
																		{
																			foreach (Door door in UnityEngine.Object.FindObjectsOfType<Door>())
																			{
																				bool flag43 = door.DoorName.Contains("ESCAPE_INNER");
																				if (flag43)
																				{
																					ev.Player.ReferenceHub.playerMovementSync.OverridePosition(new Vector3(door.transform.position.x, door.transform.position.y + 4f, door.transform.position.z), 0f, false);
																					return;
																				}
																			}
																		}
																		bool flag44 = onlyRandom2 == 3;
																		if (flag44)
																		{
																			ev.Player.ReferenceHub.playerMovementSync.OverridePosition(new Vector3(0f, -1997f, 0f), -1f, false);
																		}
																		bool flag45 = onlyRandom2 == 4;
																		if (flag45)
																		{
																			ev.Player.ReferenceHub.playerMovementSync.OverridePosition(Map.GetRandomSpawnPoint(RoleType.Scp049), -1f, false);
																		}
																		bool flag46 = onlyRandom2 == 4;
																		if (flag46)
																		{
																			bool flag47 = EventHandlers.GetOnlyRandom(1, 2) == 1;
																			if (flag47)
																			{
																				foreach (Door door2 in UnityEngine.Object.FindObjectsOfType<Door>())
																				{
																					bool flag48 = door2.DoorName.Contains("GATE_A");
																					if (flag48)
																					{
																						ev.Player.ReferenceHub.playerMovementSync.OverridePosition(new Vector3(door2.transform.position.x + 2f, door2.transform.position.y + 4f, door2.transform.position.z + 2f), 0f, false);
																						break;
																					}
																				}
																			}
																			else
																			{
																				foreach (Door door3 in UnityEngine.Object.FindObjectsOfType<Door>())
																				{
																					bool flag49 = door3.DoorName.Contains("GATE_B");
																					if (flag49)
																					{
																						ev.Player.ReferenceHub.playerMovementSync.OverridePosition(new Vector3(door3.transform.position.x + 2f, door3.transform.position.y + 4f, door3.transform.position.z + 2f), 0f, false);
																						break;
																					}
																				}
																			}
																		}
																	}
																	else
																	{
																		bool flag50 = onlyRandom <= 95;
																		if (flag50)
																		{
																			ev.Player.Broadcast(1, "<color=#ff9900>[2]俘虏</color>");
																			ev.Player.Broadcast(1, "<color=#ff9900>[1]俘虏</color>");
																			ev.Player.Broadcast(4, "<color=#ff9900>直接变为敌对阵营的武装人员</color>");
																			bool flag51 = ev.Player.Team == Team.MTF;
																			if (flag51)
																			{
																				ev.Player.ReferenceHub.characterClassManager.SetPlayersClass(RoleType.ClassD, ev.Player.GameObject, false, false);
																			}
																			else
																			{
																				bool flag52 = ev.Player.ReferenceHub.characterClassManager.CurClass == RoleType.ClassD;
																				if (flag52)
																				{
																					ev.Player.ReferenceHub.characterClassManager.SetPlayersClass(RoleType.NtfCommander, ev.Player.GameObject, false, false);
																				}
																			}
																		}
																		else
																		{
																			bool flag53 = onlyRandom <= 100;
																			if (flag53)
																			{
																				ev.Player.Broadcast(1, "<color=#ff9900>[2]美汁er汁er</color>");
																				ev.Player.Broadcast(1, "<color=#ff9900>[1]美汁er汁er</color>");
																				ev.Player.Broadcast(4, "<color=#ff9900>回满血，获得黑卡，018，207，大机枪，肾上腺素各一个</color>");
																				ev.Player.ReferenceHub.playerStats.Health = ev.Player.ReferenceHub.playerStats.maxHP;
																				bool flag54 = ev.Player.ReferenceHub.inventory.items.Count > 3;
																				if (flag54)
																				{
																					foreach (Inventory.SyncItemInfo syncItemInfo in ev.Player.ReferenceHub.inventory.items)
																					{
																						ev.Player.ReferenceHub.inventory.CallCmdDropItem(ev.Player.ReferenceHub.inventory.items.IndexOf(syncItemInfo));
																					}
																				}
																				ev.Player.ReferenceHub.inventory.AddNewItem(ItemType.KeycardO5, -4.65664672E+11f, 0, 0, 0);
																				ev.Player.ReferenceHub.inventory.AddNewItem(ItemType.SCP018, -4.65664672E+11f, 0, 0, 0);
																				ev.Player.ReferenceHub.inventory.AddNewItem(ItemType.SCP207, -4.65664672E+11f, 0, 0, 0);
																				ev.Player.ReferenceHub.inventory.AddNewItem(ItemType.GunLogicer, -4.65664672E+11f, 0, 0, 0);
																				ev.Player.ReferenceHub.inventory.AddNewItem(ItemType.Adrenaline, -4.65664672E+11f, 0, 0, 0);
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
										else
										{
											ev.IsAllowed = false;
											ev.Player.Broadcast(1, "<color=#ff9900>虽然你上辈子是老八。但是我绝不会吃如此肮脏的东西</color>");
										}
									}
								}
							}
						}
					}
				}
				else
				{
					bool flag55 = scpspsjdidq.Contains(ev.Player.ReferenceHub.queryProcessor.PlayerId);
					if (flag55)
					{
						bool flag56 = ev.Pickup.info.itemId == ItemType.Adrenaline;
						if (flag56)
						{
							ev.IsAllowed = false;
						}
						bool flag57 = ev.Pickup.info.itemId == ItemType.Medkit;
						if (flag57)
						{
							ev.IsAllowed = false;
						}
						bool flag58 = ev.Pickup.info.itemId == ItemType.SCP500;
						if (flag58)
						{
							ev.IsAllowed = false;
						}
						bool flag59 = ev.Pickup.info.itemId == ItemType.Painkillers;
						if (flag59)
						{
							ev.IsAllowed = false;
						}
					}
					bool flag60 = scp181idq.Contains(ev.Player.ReferenceHub.queryProcessor.PlayerId);
					if (flag60)
					{
						bool flag61 = ev.Pickup.info.itemId == ItemType.KeycardChaosInsurgency ||
							ev.Pickup.info.itemId == ItemType.KeycardContainmentEngineer ||
							ev.Pickup.info.itemId == ItemType.KeycardFacilityManager ||
							ev.Pickup.info.itemId == ItemType.KeycardGuard ||
							ev.Pickup.info.itemId == ItemType.KeycardJanitor ||
							ev.Pickup.info.itemId == ItemType.KeycardNTFCommander ||
							ev.Pickup.info.itemId == ItemType.KeycardNTFLieutenant ||
							ev.Pickup.info.itemId == ItemType.KeycardZoneManager ||
							ev.Pickup.info.itemId == ItemType.KeycardScientist ||
							ev.Pickup.info.itemId == ItemType.KeycardScientistMajor ||
							ev.Pickup.info.itemId == ItemType.KeycardSeniorGuard;
						if (flag61)
						{
							int onlyRandom3 = EventHandlers.GetOnlyRandom(1, 100);
							int num3 = 50;
							bool flag62 = onlyRandom3 <= num3;
							if (flag62)
							{
								ev.IsAllowed = false;
								ev.Player.ReferenceHub.inventory.items.RemoveAt(ev.Pickup.ComponentIndex);
								ev.Player.ReferenceHub.inventory.AddNewItem(ItemType.KeycardO5, -4.65664672E+11f, 0, 0, 0);
							}
						}
						bool flag63 = ev.Pickup.info.itemId == ItemType.GunCOM15 || ev.Pickup.info.itemId == ItemType.GunMP7 || ev.Pickup.info.itemId == ItemType.GunProject90 || ev.Pickup.info.itemId == ItemType.GunUSP;
						if (flag63)
						{
							int onlyRandom4 = EventHandlers.GetOnlyRandom(1, 100);
							int num4 = 50;
							bool flag64 = onlyRandom4 <= num4;
							if (flag64)
							{
								ev.IsAllowed = false;
								int onlyRandom5 = EventHandlers.GetOnlyRandom(1, 2);
								bool flag65 = onlyRandom5 == 1;
								if (flag65)
								{
									ev.Player.ReferenceHub.inventory.items.RemoveAt(ev.Pickup.ComponentIndex);
									ev.Player.ReferenceHub.inventory.AddNewItem(ItemType.GunLogicer, -4.65664672E+11f, 0, 0, 0);
								}
								bool flag66 = onlyRandom5 == 2;
								if (flag66)
								{
									ev.Player.ReferenceHub.inventory.items.RemoveAt(ev.Pickup.ComponentIndex);
									ev.Player.ReferenceHub.inventory.AddNewItem(ItemType.GunE11SR, -4.65664672E+11f, 0, 0, 0);
								}
							}
						}
						bool flag67 = ev.Pickup.info.itemId == ItemType.Painkillers || ev.Pickup.info.itemId == ItemType.Medkit;
						if (flag67)
						{
							int onlyRandom6 = EventHandlers.GetOnlyRandom(1, 100);
							int num5 = 50;
							bool flag68 = onlyRandom6 <= num5;
							if (flag68)
							{
								ev.IsAllowed = false;
								int onlyRandom7 = EventHandlers.GetOnlyRandom(1, 2);
								bool flag69 = onlyRandom7 == 1;
								if (flag69)
								{
									ev.Player.ReferenceHub.inventory.items.RemoveAt(ev.Pickup.ComponentIndex);
									ev.Player.ReferenceHub.inventory.AddNewItem(ItemType.Adrenaline, -4.65664672E+11f, 0, 0, 0);
								}
								bool flag70 = onlyRandom7 == 2;
								if (flag70)
								{
									ev.Player.ReferenceHub.inventory.items.RemoveAt(ev.Pickup.ComponentIndex);
									ev.Player.ReferenceHub.inventory.AddNewItem(ItemType.SCP500, -4.65664672E+11f, 0, 0, 0);
								}
							}
						}
					}
				}
			}
		}
		public void OnPlayerDropItem(DroppingItemEventArgs ev)
		{
			if (scplzridq.Contains(ev.Player.ReferenceHub.queryProcessor.PlayerId))
			{
				if (ev.Item.id == ItemType.GunLogicer) { ev.IsAllowed = false; return; }
			}
			bool flag = scpspsjdidq.Contains(ev.Player.ReferenceHub.queryProcessor.PlayerId);
			if (flag)
			{
				bool flag2 = ev.Item.id == ItemType.Coin;
				if (flag2)
				{
					bool flag3 = !Break;
					if (flag3)
					{
						Break = true;
						ev.Player.ReferenceHub.serverRoles.SetText("蛇之手间谍");
						ev.Player.ReferenceHub.playerStats.HealHPAmount(50);
						ev.Player.ReferenceHub.serverRoles.SetColor("red");
						List<Player> hubs = new List<Player>(Player.List).ToList<Player>();
						int num = 0;
						foreach (Player referenceHub in hubs)
						{
							bool flag4 = referenceHub.ReferenceHub.characterClassManager.CurClass == RoleType.Spectator;
							if (flag4)
							{
								num++;
								bool flag5 = num <= 9;
								if (flag5)
								{
									referenceHub.ReferenceHub.characterClassManager.SetPlayersClass(RoleType.Tutorial, referenceHub.GameObject, false, false);
									referenceHub.ReferenceHub.inventory.AddNewItem(ItemType.GunLogicer, -4.65664672E+11f, 0, 0, 0);
									referenceHub.ReferenceHub.inventory.AddNewItem(ItemType.GunE11SR, -4.65664672E+11f, 0, 0, 0);
									referenceHub.ReferenceHub.inventory.AddNewItem(ItemType.Medkit, -4.65664672E+11f, 0, 0, 0);
									referenceHub.ReferenceHub.inventory.AddNewItem(ItemType.KeycardFacilityManager, -4.65664672E+11f, 0, 0, 0);
									referenceHub.ReferenceHub.inventory.AddNewItem(ItemType.Disarmer, -4.65664672E+11f, 0, 0, 0);
									referenceHub.ReferenceHub.playerStats.SetHPAmount(290);
									referenceHub.Broadcast(8, "你是蛇之手，帮助scp胜利");
									referenceHub.RankName = "蛇之手";
									int onlyRandom = EventHandlers.GetOnlyRandom(1, 4);
									Timing.CallDelayed(1f, () => {
										bool flag6 = onlyRandom == 1;
										if (flag6)
										{
											referenceHub.ReferenceHub.playerMovementSync.OverridePosition(new Vector3(-10f, 1002f, 1f), -1f, false);
										}
										bool flag7 = onlyRandom == 2;
										if (flag7)
										{
											referenceHub.ReferenceHub.playerMovementSync.OverridePosition(new Vector3(-10f, 1002f, 2f), -1f, false);
										}
										bool flag8 = onlyRandom == 3;
										if (flag8)
										{
											referenceHub.ReferenceHub.playerMovementSync.OverridePosition(new Vector3(-11f, 1002f, 1f), -1f, false);
										}
										bool flag9 = onlyRandom == 4;
										if (flag9)
										{
											referenceHub.ReferenceHub.playerMovementSync.OverridePosition(new Vector3(-11f, 1002f, 2f), -1f, false);
										}
									});
									scpspsidq.Add(referenceHub.ReferenceHub.queryProcessor.PlayerId);
								}
							}
						}
						ev.Player.Broadcast(8, "成功召唤0-9人的蛇之手。您的召唤功能已失效！~");
						ev.IsAllowed = true;
					}
				}
			}
		}
		public void OnPlayerJoin(JoinedEventArgs ev)
		{
		}
		public void OnTeamReSpawn(RespawningTeamEventArgs ev)
		{
			Timing.RunCoroutine(DelayTeamRespawn(ev));
		}
		public IEnumerator<float> makescp(int id, string type)
		{
			yield return Timing.WaitForSeconds(0.1f);
			Player eva = Player.Get(id);
			if (type == "181" && eva != null)
			{
				eva.ReferenceHub.characterClassManager.SetClassID(RoleType.ClassD);
				yield return Timing.WaitForSeconds(0.2f);
				eva.ReferenceHub.serverRoles.SetText("SCP-181");
				eva.ReferenceHub.serverRoles.SetColor("orange");
				Log.Info("[SCP-181][Info] 本局的SCP-181是: " + eva.ReferenceHub.nicknameSync.Network_myNickSync.ToString() + "[" + eva.ReferenceHub.queryProcessor.PlayerId.ToString() + "]");
				eva.Broadcast(5, "<color=#FF0000>[SCP-181特殊能力]</color>\n开门有概率开启权限门，伤害有概率免伤\n丢弃部分物品有概率转换");
				if (!scp181idq.Contains(id))
					scp181idq.Add(id);
			}
			if (type == "szsjd" && eva != null)
			{
				eva.ReferenceHub.characterClassManager.SetClassID(RoleType.ClassD);
				yield return Timing.WaitForSeconds(0.2f);
				eva.ReferenceHub.inventory.Clear();
				eva.ReferenceHub.inventory.AddNewItem(ItemType.KeycardChaosInsurgency);
				eva.ReferenceHub.inventory.AddNewItem(ItemType.Coin);
				eva.ReferenceHub.inventory.AddNewItem(ItemType.GunCOM15);
				Log.Info("[蛇之手间谍][Info] 本局的蛇之手间谍是: " + eva.ReferenceHub.nicknameSync.Network_myNickSync.ToString() + "[" + eva.ReferenceHub.queryProcessor.PlayerId.ToString() + "]");
				eva.Broadcast(5, "<color=#FF0000>[蛇之手间谍特殊能力]</color>\n你无法拾取药品，可丢弃硬币召唤9名蛇之手支援\n人类对你只能造成很低的伤害，你需要帮助SCP阵营获得胜利");
				if (!scpspsjdidq.Contains(id))
					scpspsjdidq.Add(id);
			}
			if (type == "lzr" && eva != null)
			{
				eva.ReferenceHub.characterClassManager.SetClassID(RoleType.ChaosInsurgency);
				yield return Timing.WaitForSeconds(0.2f);
				eva.ReferenceHub.serverRoles.SetText("无赖战士");
				eva.ReferenceHub.serverRoles.SetColor("yellow");
				eva.ReferenceHub.playerStats.Health = 250f;
				eva.ReferenceHub.inventory.Clear();
				eva.ReferenceHub.inventory.AddNewItem(ItemType.GunLogicer, -4.65664672E+11f, 0, 0, 0);
				eva.ReferenceHub.inventory.AddNewItem(ItemType.SCP207, -4.65664672E+11f, 0, 0, 0);
				if (!scplzridq.Contains(eva.ReferenceHub.queryProcessor.PlayerId))
					scplzridq.Add(eva.ReferenceHub.queryProcessor.PlayerId);
			}
			if (type == "ysy" && eva != null)
			{
				eva.ReferenceHub.characterClassManager.SetClassID(RoleType.NtfCommander);
				yield return Timing.WaitForSeconds(0.2f);
				eva.ReferenceHub.serverRoles.SetText("燕双鹰");
				eva.ReferenceHub.serverRoles.SetColor("yellow");
				eva.ReferenceHub.playerStats.Health = 200f;
				eva.ReferenceHub.inventory.Clear();
				eva.ReferenceHub.inventory.AddNewItem(ItemType.KeycardNTFCommander, -4.65664672E+11f, 0, 0, 0);
				eva.ReferenceHub.inventory.AddNewItem(ItemType.GunLogicer, -4.65664672E+11f, 0, 0, 0);
				eva.ReferenceHub.inventory.AddNewItem(ItemType.GunUSP, -4.65664672E+11f, 0, 0, 0);
				eva.ReferenceHub.inventory.AddNewItem(ItemType.Radio, -4.65664672E+11f, 0, 0, 0);
				if (!scpysyidq.Contains(eva.ReferenceHub.queryProcessor.PlayerId))
					scpysyidq.Add(eva.ReferenceHub.queryProcessor.PlayerId);
			}
			if (type == "szs" && eva != null)
			{
				eva.ReferenceHub.characterClassManager.SetPlayersClass(RoleType.Tutorial, eva.ReferenceHub.gameObject, false, false);
				eva.ReferenceHub.inventory.AddNewItem(ItemType.GunLogicer, -4.65664672E+11f, 0, 0, 0);
				eva.ReferenceHub.inventory.AddNewItem(ItemType.GunE11SR, -4.65664672E+11f, 0, 0, 0);
				eva.ReferenceHub.inventory.AddNewItem(ItemType.Medkit, -4.65664672E+11f, 0, 0, 0);
				eva.ReferenceHub.inventory.AddNewItem(ItemType.KeycardFacilityManager, -4.65664672E+11f, 0, 0, 0);
				eva.ReferenceHub.inventory.AddNewItem(ItemType.Disarmer, -4.65664672E+11f, 0, 0, 0);
				eva.ReferenceHub.playerStats.SetHPAmount(290);
				eva.Broadcast(8, "你是蛇之手，帮助scp胜利");
				int onlyRandom = EventHandlers.GetOnlyRandom(1, 4);
				bool flag6 = onlyRandom == 1;
				if (flag6)
				{
					eva.ReferenceHub.playerMovementSync.OverridePosition(new Vector3(-10f, 1002f, 1f), -1f, false);
				}
				bool flag7 = onlyRandom == 2;
				if (flag7)
				{
					eva.ReferenceHub.playerMovementSync.OverridePosition(new Vector3(-10f, 1002f, 2f), -1f, false);
				}
				bool flag8 = onlyRandom == 3;
				if (flag8)
				{
					eva.ReferenceHub.playerMovementSync.OverridePosition(new Vector3(-11f, 1002f, 1f), -1f, false);
				}
				bool flag9 = onlyRandom == 4;
				if (flag9)
				{
					eva.ReferenceHub.playerMovementSync.OverridePosition(new Vector3(-11f, 1002f, 2f), -1f, false);
				}
				if (!scpspsidq.Contains(eva.ReferenceHub.queryProcessor.PlayerId))
					scpspsidq.Add(eva.ReferenceHub.queryProcessor.PlayerId);
			}
			if (type == "lb" && eva != null)
			{
				Vector3 position = eva.Position;
				scplbidq.Add(eva.ReferenceHub.queryProcessor.PlayerId);
				eva.ReferenceHub.inventory.ServerDropAll();
				eva.ReferenceHub.characterClassManager.SetClassIDAdv(RoleType.Scp0492,true);
				eva.ReferenceHub.playerMovementSync.OverridePosition(position, 0f, false);
				eva.ReferenceHub.playerStats.SetHPAmount(2500);
				Canteatshit.Add(eva.ReferenceHub.queryProcessor.PlayerId);
			}
		}
		public void RAMessage(SendingRemoteAdminCommandEventArgs ev)
		{
			{
				if (ev.Name == "spawn")
				{
					if (ev.Arguments.Count < 2)
					{
						ev.Sender.RemoteAdminMessage("[Error]请使用 spawn [id] xxx");
						ev.IsAllowed = false;
						return;
					}
					int id = 0;
					int.TryParse(ev.Arguments[0], out id);
					Player eva = Player.Get(id);
					if (eva == null)
					{
						ev.Sender.RemoteAdminMessage("[[Error]不接受非ID单位的玩家");
						ev.IsAllowed = false;
						return;
					}
					Timing.RunCoroutine(makescp(int.Parse(ev.Arguments[0]), ev.Arguments[1]));
					ev.Sender.RemoteAdminMessage("[Info]后台已执行该命令，允许的参数：ysy lzr szsjd 181 szs b");
					ev.IsAllowed = false;
					return;
				}
			}
		}
		public IEnumerator<float> DelayTeamRespawn(RespawningTeamEventArgs ev)
		{

			yield return Timing.WaitForSeconds(0.3f);

			Log.Info("刷新物品");
			if (ev.NextKnownTeam == Respawning.SpawnableTeamType.ChaosInsurgency)
			{
				Log.Info("混沌");
				if (scplzridq.Count < 1)
				{
					Log.Info("刷新");
					lzrspawncount++;
					int onlyRandom = EventHandlers.GetOnlyRandom(0, ev.Players.Count - 1);
					Timing.RunCoroutine(makescp(ev.Players[onlyRandom].ReferenceHub.queryProcessor.PlayerId, "lzr"));
				}
			}
			if (ev.NextKnownTeam == Respawning.SpawnableTeamType.NineTailedFox)
			{
				Log.Info("九尾");
				if (scpysyidq.Count < 1)
				{
					Log.Info("刷新");
					ysyspawncount++;
					int onlyRandom2 = EventHandlers.GetOnlyRandom(0, ev.Players.Count - 1);
					Timing.RunCoroutine(makescp(ev.Players[onlyRandom2].ReferenceHub.queryProcessor.PlayerId, "ysy"));
				}
			}
		}
		public void OnRoundRestart()
		{
			ysyspawncount = 0;
			lzrspawncount = 0;
			SCPadd = new List<int>();
			Break = false;
			scp181idq = new List<int>();
			scpspsjdidq = new List<int>();
			scpspsidq = new List<int>();
			scplbidq = new List<int>();
			scpysyidq = new List<int>();
			scplzridq = new List<int>();
			Baba = new List<Pickup>();
			Baba1 = new List<Pickup>();
			Eatid = new List<int>();
			ready = new List<int>();
			breakid = new List<int>();
			Canteatshit = new List<int>();
			used2 = new List<int>();
		}
		public static int GetOnlyRandom(int min, int max)
		{
			bool flag = min >= max || min < 0;
			int result;
			if (flag)
			{
				result = min;
			}
			else
			{
				int num = max - min;
				RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider();
				byte[] array = new byte[4];
				rngcryptoServiceProvider.GetBytes(array);
				int num2 = BitConverter.ToInt32(array, 0) % num;
				System.Random random = new System.Random((int)DateTime.Now.Ticks + num2 + min);
				result = random.Next(min, max);
			}
			return result;
		}

	}
}
