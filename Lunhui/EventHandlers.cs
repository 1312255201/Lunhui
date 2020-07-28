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
					Log.Info("������[" + cat + "]�����ڣ�����Ĭ��������");
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
			Log.Info("\n" + "���Ʋ���ͱ���â��V1.0[���ö�ȡ���]" + "\nSCPÿ�λ�Ѫ��" + Config.SCPHealth
				+ "\n�����һ�Σ�" + Config.SCPSecondOnce
				+ "\n106ÿ�λ�Ѫ��" + Config.SCP106Health
				+ "\n049Ѫ����" + Config.SCP049MaxHealth
				+ "\n0492Ѫ����" + Config.SCP0492MaxHealth
				+ "\n096Ѫ����" + Config.SCP096MaxHealth
				+ "\n106Ѫ����" + Config.SCP106MaxHealth
				+ "\n173Ѫ����" + Config.SCP173MaxHealth
				+ "\n939Ѫ����" + Config.SCP939MaxHealth
				+ "\n����������һ�Σ�" + Config.SecondAddHealth + "\n");
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
			float �����˺� = ev.Amount;
			float ���淴�� = 0;
			bool ��֮�ּ������ = scpspsjdidq.Contains(ev.Attacker.ReferenceHub.queryProcessor.PlayerId);
			bool ��֮�ֹ��� = scpspsidq.Contains(ev.Attacker.ReferenceHub.queryProcessor.PlayerId);
			bool ��֮�ּ������ = scpspsjdidq.Contains(ev.Target.ReferenceHub.queryProcessor.PlayerId);
			bool ���˶����� = scp181idq.Contains(ev.Attacker.ReferenceHub.queryProcessor.PlayerId);
			bool �ϰ˹��� = scplbidq.Contains(ev.Attacker.ReferenceHub.queryProcessor.PlayerId);
			bool ����սʿ���� = scplzridq.Contains(ev.Attacker.ReferenceHub.queryProcessor.PlayerId);
			bool ��˫ӥ���� = scpysyidq.Contains(ev.Attacker.ReferenceHub.queryProcessor.PlayerId);
			bool ��֮������ = scpspsidq.Contains(ev.Target.ReferenceHub.queryProcessor.PlayerId);
			bool ��˫ӥ���� = scpysyidq.Contains(ev.Target.ReferenceHub.queryProcessor.PlayerId);
			bool ����սʿ���� = scplzridq.Contains(ev.Target.ReferenceHub.queryProcessor.PlayerId);
			bool �ϰ����� = scplbidq.Contains(ev.Target.ReferenceHub.queryProcessor.PlayerId);
			bool ���˶����� = scp181idq.Contains(ev.Target.ReferenceHub.queryProcessor.PlayerId);
			bool �Բ� = ev.Attacker == ev.Target;
			bool �Ƿ����� = false;
			if (���˶�����)
			{
				bool flag4 = ev.DamageType == DamageTypes.Nuke;
				if (flag4)
				{
					ev.Target.Broadcast(3, "<color=#FF0000>[SCP-181��������]</color>\n<color=green>�������ߺ˵��˺�,��·��,���!</color>");
				}
				int onlyRandom = GetOnlyRandom(1, 100);
				if (onlyRandom <= 50 && ev.DamageType != DamageTypes.Nuke)
				{
					ev.Target.Broadcast(3, "<color=#FF0000>[SCP-181��������]</color>\n<color=green>�ɹ����߸��˺�</color>");
					if (ev.Attacker != null)
					{
						ev.Attacker.Broadcast(3, "<color=#FF0000>[SCP-181��������]</color>\n<color=green>���Ĺ����ѱ�����</color>");
					}
					�����˺� = 0f;
					�Ƿ����� = true;

				}
			}
			if (�Բ�)
			{
				if (����սʿ����)
				{
					if (ev.DamageType == DamageTypes.Scp207)
					{
						�����˺� = 0f;
					}
					if (ev.DamageType == DamageTypes.Falldown)
					{
						�����˺� = 0f;
					}
				}
				if (��˫ӥ����)
				{
					if (ev.DamageType == DamageTypes.Scp207)
					{
						�����˺� = 0f;
					}
					if (ev.DamageType == DamageTypes.Falldown)
					{
						�����˺� = 0f;
					}
				}

			}
			else
			{
				if (��֮�ּ������)
				{
					if (ev.Attacker.ReferenceHub.characterClassManager.IsHuman() && !ev.Attacker.ReferenceHub.characterClassManager.IsAnyScp())
					{
						�����˺� = 3f;
					}
					if (ev.Attacker.ReferenceHub.characterClassManager.IsAnyScp())
					{
						�����˺� = 0f;
					}
					if (ev.DamageType == DamageTypes.RagdollLess)
					{
						�����˺� = 0f;
					}
				}
				if (��֮�ּ������)
				{
					if (ev.Target.Team == Team.SCP)
					{
						�����˺� = 0f;
					}
					if (scpspsidq.Contains(ev.Target.ReferenceHub.queryProcessor.PlayerId))
					{
						�����˺� = 0f;
					}
				}
				if (��֮������)
				{
					if (ev.Attacker.ReferenceHub.characterClassManager.IsAnyScp())
					{
						�����˺� = 0f;
					}
				}
				if (��֮�ֹ���)
				{
					if (scpspsjdidq.Contains(ev.Target.ReferenceHub.queryProcessor.PlayerId))
					{
						�����˺� = 0f;
					}
				}
				if (��˫ӥ����)
				{
					if (!ev.Target.ReferenceHub.characterClassManager.IsAnyScp())
					{
						���淴�� = 5f;
						if (��֮�ּ������)
							���淴�� = 1f;
						�����˺� = �����˺� * 0.3f;
					}
					else
					{
						���淴�� = 20f;
						if (ev.DamageType == DamageTypes.Scp096)
						{
							�����˺� = 12f;
						}
						if (ev.DamageType == DamageTypes.Scp0492)
						{
							�����˺� = 10f;
						}
						if (ev.DamageType == DamageTypes.Scp106)
						{
							�����˺� = 15f;
						}
						if (ev.DamageType == DamageTypes.MicroHid)
						{
							�����˺� = 1.3f;
						}
						if (ev.DamageType == DamageTypes.Pocket)
						{
							�����˺� = 0f;
						}
						if (ev.DamageType == DamageTypes.Grenade)
						{
							�����˺� = (�����˺� * 1.5f) / 100;
						}
						if (ev.DamageType == DamageTypes.Scp173 || ev.DamageType == DamageTypes.Scp939 || ev.DamageType == DamageTypes.Scp049)
						{
							�����˺� = 20f;
						}
					}
					int num = GetOnlyRandom(1, 100);
					if (num <= 20)
					{
						�����˺� = 0f;
					}
				}
				if (��˫ӥ����)
				{
					if (ev.DamageType != DamageTypes.Grenade || ev.DamageType != DamageTypes.MicroHid)
					{
						if (ev.Target.ReferenceHub.characterClassManager.IsAnyScp())
						{
							if (useing2.Contains(ev.Target.ReferenceHub.queryProcessor.PlayerId)) { �����˺� = �����˺� + �����˺� * 1f; } else { �����˺� = �����˺� + �����˺� * 0.5f; }

						}
						else
						{
							int num = GetOnlyRandom(1, 100);
							if (num > 50)
							{
								�����˺� = �����˺� + �����˺� * 0.2f;
							}
							if (!�ϰ�����)
							{
								if (num > 99)
								{
									�����˺� = 999999f;
								}
								if (��֮�ּ������)
								{
									if (num > 94)
									{
										�����˺� = 999999f;
									}
								}
								if (����սʿ����)
								{
									if (num > 89)
									{
										�����˺� = 999999f;
									}
								}
							}
							else
							{
								�����˺� = �����˺� + �����˺� * 2.0f;
							}
						}
					}
					if (ev.DamageType == DamageTypes.Usp && �����˺� != 999999f)
					{
						if (ev.Target.ReferenceHub.characterClassManager.IsAnyScp())
						{ �����˺� = 200f; }
						else { �����˺� = 99f; }
						if (�ϰ�����) { �����˺� = 200f; }
						if (����սʿ����) { �����˺� = 70f; }
					}
					if (��֮�ּ������)
					{
						�����˺� = 5f;
					}
				}
				if (����սʿ����)
				{
					if (ev.Target.ReferenceHub.characterClassManager.IsHuman() && !ev.Target.ReferenceHub.characterClassManager.IsAnyScp())
					{
						if (
						!�ϰ˹��� &&
						!����սʿ���� &&
						!��˫ӥ����)
						{
							�����˺� = 70f;
							if (useing2.Contains(ev.Target.ReferenceHub.queryProcessor.PlayerId))
								�����˺� = 2f;
						}
					}
					else if (ev.Target.ReferenceHub.characterClassManager.IsAnyScp())
					{
						if (ev.DamageType != DamageTypes.Scp106)
							�����˺� = 13f;
						else
						{
							�����˺� = 10f;
						}
						if (useing2.Contains(ev.Target.ReferenceHub.queryProcessor.PlayerId))
							�����˺� = 5f;
					}
					if (��˫ӥ���� && useing2.Contains(ev.Attacker.ReferenceHub.queryProcessor.PlayerId) && GetOnlyRandom(0, 100) < 10)
					{
						�����˺� = 99999f;
					}
					if (��˫ӥ����)
					{
						if (ev.DamageType == DamageTypes.Usp)
							�����˺� = 25f;
						else
							�����˺� = �����˺� * 0.2f;
					}
					if (��֮�ּ������)
					{
						���淴�� = 2f;
					}
				}
				if (����սʿ����)
				{
				}
				if (�ϰ�����)
				{
					if (ev.DamageType == DamageTypes.MicroHid)
					{
						���淴�� = ev.Amount / 25f;
					}
					else if (ev.DamageType != DamageTypes.Grenade)
					{
						���淴�� = 5f;
					}
					if (����սʿ����)
					{
						���淴�� = 1f;
					}
				}
				if (�ϰ˹���)
				{
					�����˺� = 0f;
				}
				if (���˶�����)
				{

				}
				if (���˶�����)
				{
				}
				if (ev.Target.Team == Team.CDP && !��֮�ּ������)
				{
					if (ev.Attacker.Team == Team.CHI && !��֮�ּ������)
					{
						���淴�� = 0f;
						�����˺� = 0f;
					}
					if (ev.Attacker.Team == Team.CDP && !��֮�ּ������)
					{
						���淴�� = 0f;
						�����˺� = 0f;
					}
				}
				if (ev.Target.Team == Team.RSC && !��֮�ּ������)
				{
					if (ev.Attacker.Team == Team.MTF || ev.Attacker.Team == Team.RSC && !��֮�ּ������)
					{
						���淴�� = 0f;
						�����˺� = 0f;
					}
					if (ev.Attacker.Team == Team.RSC || ev.Attacker.Team == Team.RSC && !��֮�ּ������)
					{
						���淴�� = 0f;
						�����˺� = 0f;
					}
				}
				if (ev.Target.Team == Team.MTF && !��֮�ּ������)
				{
					if (ev.Attacker.Team == Team.MTF || ev.Attacker.Team == Team.RSC && !��֮�ּ������)
					{
						���淴�� = 0f;
						�����˺� = 0f;
					}
				}
				if (ev.Target.Team == Team.CHI && !��֮�ּ������)
				{
					if (ev.Attacker.Team == Team.CDP && !��֮�ּ������)
					{
						���淴�� = 0f;
						�����˺� = 0f;
					}
					if (ev.Attacker.Team == Team.CHI && !��֮�ּ������)
					{
						���淴�� = 0f;
						�����˺� = 0f;
					}
				}
				if (ev.Target.Team == Team.SCP || ��֮�ּ������)
				{
					if (ev.Attacker.Team == Team.SCP || ��֮�ּ������)
					{
						���淴�� = 0f;
						�����˺� = 0f;
					}
				}
				ev.Amount = �����˺�;
				ev.Attacker.Health -= ���淴��;
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
					ev.ReturnMessage = "�Ѿ�ʹ�ù��ˡ�";
				}
				else
				{
					bool flag6 = scpysyidq.Contains(ev.Player.ReferenceHub.queryProcessor.PlayerId);
					if (flag6)
					{
						used2.Add(ev.Player.ReferenceHub.queryProcessor.PlayerId);
						Timing.RunCoroutine(test1(ev.Player.ReferenceHub.queryProcessor.PlayerId, 1));
						ev.ReturnMessage = "�������Σ��Ƕ��ѣ�Ŀ��100%���У�����SCPĿ���˺����200%��3���˺���,����ʱ��10��";

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
						ev.Player.Broadcast(7, "[ϵͳ��Ϣ]���޷���ʳ�����ʺ");
						ev.IsAllowed = false;
					}
					else
					{
						bool flag16 = scpspsidq.Contains(ev.Player.ReferenceHub.queryProcessor.PlayerId);
						if (flag16)
						{
							ev.IsAllowed = false;
							ev.Player.Broadcast(1, "<color=#ff9900>�㲻���ϰ˵ļ̳���ร�</color>");
						}
						else
						{
							bool flag17 = scpspsjdidq.Contains(ev.Player.ReferenceHub.queryProcessor.PlayerId);
							if (flag17)
							{
								ev.IsAllowed = false;
								ev.Player.Broadcast(1, "<color=#ff9900>�㲻���ϰ˵ļ̳���ร�</color>");
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
									ev.Player.Broadcast(1, "<color=#ff9900>��Ȼ���ϱ������ϰˡ������Ҿ��������˰���Ķ���</color>");
								}
								else
								{
									bool flag20 = num >= 3;
									if (flag20)
									{
										ev.IsAllowed = false;
										ev.Pickup.Delete();
										Eatid.Add(ev.Player.ReferenceHub.queryProcessor.PlayerId);
										ev.Player.Broadcast(3, "<color=#ff9900>�����ǰ��������Ǿ仰��ֻ�������벻���ģ�û���ϰ��������ġ����ǲ�ҪЦ���Ǳ���������Ҳ����Ц���뿪��ĸ�ĸ���ҳ�ʺ���ѡ������������ˣ��ֵ��ǣ�</color>");
										ev.Player.Broadcast(4, "<color=#ff9900>ԭ�ؿ�ס�������������ٵ�Ѫ--����Ϊֹ���޷�ʹ��500����</color>");
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
												ev.Player.Broadcast(1, "<color=#ff9900>���ϰ˽���Բ�����</color>");
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
													ev.Player.Broadcast(7, "<color=#ff9900>[ϵͳ��Ϣ]��ϲ�����ϰˣ��Ը����ʺ</color>");
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
													ev.Player.Broadcast(3, "<color=#ff9900>�����ǰ���ֻ�������벻���ģ�û���ϰ��������ġ������Ǿ仰���������ϰ���սһ�ѳ����Ρ��ֵ��ǣ������������ˣ�</color>");
													bool flag27 = EventHandlers.GetOnlyRandom(1, 100) <= 50;
													if (flag27)
													{
														ev.Player.Broadcast(4, "<color=#ff9900>ֱ�ӵ�Ѫ80</color>");
														ev.Player.ReferenceHub.playerStats.HurtPlayer(new PlayerStats.HitInfo(80f, ev.Player.ReferenceHub.nicknameSync.Network_myNickSync, DamageTypes.Scp207, ev.Player.ReferenceHub.queryProcessor.PlayerId), ev.Player.GameObject);
													}
													else
													{
														ev.Player.Broadcast(4, "<color=#ff9900>Ч����Ļ��ʾ����ÿ��10���ӿ�סһ�Σ���һ��3���ӣ�ͬʱ��Ѫ30������ס3�Σ�������֮��Ч����ʧ��ʳ��500������</color>");
														Timing.RunCoroutine(Laobakouxue(ev.Player.ReferenceHub.queryProcessor.PlayerId, 30f, ev.Player.Position));
													}
												}
												else
												{
													bool flag28 = num == 2;
													if (flag28)
													{
														ev.Player.Broadcast(3, "<color=#ff9900>�����ǰ�����Ȼ����ͬһʱ�䣬����ͬһ���������ϰ����ٴ���սһ�ѳ����Σ������������ˣ��ֵ��ǣ�</color>");
														bool flag29 = EventHandlers.GetOnlyRandom(1, 100) <= 50;
														if (flag29)
														{
															ev.Player.Broadcast(4, "<color=#ff9900>ֱ�ӵ�Ѫ99</color>");
															ev.Player.ReferenceHub.playerStats.HurtPlayer(new PlayerStats.HitInfo(99f, ev.Player.ReferenceHub.nicknameSync.Network_myNickSync, DamageTypes.Scp207, ev.Player.ReferenceHub.queryProcessor.PlayerId), ev.Player.GameObject);
														}
														else
														{
															ev.Player.Broadcast(4, "<color=#ff9900>Ч����Ļ��ʾ����ÿ��10���ӿ�סһ�Σ���һ��3���ӣ�ͬʱ��Ѫ33������ס3�Σ�������֮��Ч����ʧ��ʳ��500������</color>");
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
													ev.Player.Broadcast(1, "<color=#ff9900>[2]һ������û���գ�����ͳ��ϰ�����С��������ʵ�ݻ��ܱ�</color>");
													ev.Player.Broadcast(1, "<color=#ff9900>[1]һ������û���գ�����ͳ��ϰ�����С��������ʵ�ݻ��ܱ�</color>");
													ev.Player.Broadcast(4, "<color=#ff9900>����Ѫ�����Ÿ���50������������غ�SCP500��һ��</color>");
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
														ev.Player.Broadcast(1, "<color=#ff9900>[2]��ʳ�������ϰˣ�����Ը����ܹϣ����ﵹ���±Ϻ�����˳�����ʳ��</color>");
														ev.Player.Broadcast(1, "<color=#ff9900>[1]��ʳ�������ϰˣ�����Ը����ܹϣ����ﵹ���±Ϻ�����˳�����ʳ��</color>");
														ev.Player.Broadcast(4, "<color=#ff9900>��õ��������ǹ");
														ev.Player.ReferenceHub.inventory.AddNewItem(ItemType.MicroHID, -4.65664672E+11f, 0, 0, 0);
														ev.Player.ReferenceHub.inventory.AddNewItem(ItemType.GunLogicer, -4.65664672E+11f, 0, 0, 0);
													}
													else
													{
														bool flag32 = onlyRandom <= 38;
														if (flag32)
														{
															ev.Player.Broadcast(1, "<color=#ff9900>[2]��˵�����Բ������͹�����û�źã�����������ƹ�˿���ٵ�������֭er֭er</color>");
															ev.Player.Broadcast(1, "<color=#ff9900>[1]��˵�����Բ������͹�����û�źã�����������ƹ�˿���ٵ�������֭er֭er</color>");
															ev.Player.Broadcast(4, "<color=#ff9900>��Ч����ֵ30������һƿ����������һ��");
															ev.Player.ReferenceHub.inventory.AddNewItem(ItemType.SCP207, -4.65664672E+11f, 0, 0, 0);
															ev.Player.ReferenceHub.inventory.AddNewItem(ItemType.Adrenaline, -4.65664672E+11f, 0, 0, 0);
															ev.Player.ReferenceHub.playerStats.Health = ev.Player.ReferenceHub.playerStats.Health + 30f;
														}
														else
														{
															bool flag33 = onlyRandom <= 52;
															if (flag33)
															{
																ev.Player.Broadcast(1, "<color=#ff9900>[2]���鶹�����㽶����򵥵���ҹ�����������㻹�ǳ���������</color>");
																ev.Player.Broadcast(1, "<color=#ff9900>[1]���鶹�����㽶����򵥵���ҹ�����������㻹�ǳ���������</color>");
																ev.Player.Broadcast(4, "<color=#ff9900>��Ч����ֵ20���ظ�����ֵÿ��2Ѫ����40s</color>");
																ev.Player.ReferenceHub.playerStats.Health = ev.Player.ReferenceHub.playerStats.Health + 20f;
																Timing.RunCoroutine(Laobahuixue(ev.Player.ReferenceHub.queryProcessor.PlayerId, 40, 2));
															}
															else
															{
																bool flag34 = onlyRandom <= 67;
																if (flag34)
																{
																	ev.Player.Broadcast(1, "<color=#ff9900>[2]��ʳ�������ϰˣ�����Ը����Ѽ��Ҫ�ʰ�Ѽ����ã���Ѽƨ���ǿ鱦</color>");
																	ev.Player.Broadcast(1, "<color=#ff9900>[1]��ʳ�������ϰˣ�����Ը����Ѽ��Ҫ�ʰ�Ѽ����ã���Ѽƨ���ǿ鱦</color>");
																	ev.Player.Broadcast(4, "<color=#ff9900>������173�Աߣ�����173�����������079��SCP�Աߣ�����SCP����������B�����ԣ�</color>");
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
																		ev.Player.Broadcast(1, "<color=#ff9900>[2]�����ǰ�����������������Զ��֪�����𣬽����ϰ���սһ����������̣��������������ֵ��ǣ�</color>");
																		ev.Player.Broadcast(1, "<color=#ff9900>[1]�����ǰ�����������������Զ��֪�����𣬽����ϰ���սһ����������̣��������������ֵ��ǣ�</color>");
																		ev.Player.Broadcast(4, "<color=#ff9900>�������</color>");
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
																			ev.Player.Broadcast(1, "<color=#ff9900>[2]��²</color>");
																			ev.Player.Broadcast(1, "<color=#ff9900>[1]��²</color>");
																			ev.Player.Broadcast(4, "<color=#ff9900>ֱ�ӱ�Ϊ�ж���Ӫ����װ��Ա</color>");
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
																				ev.Player.Broadcast(1, "<color=#ff9900>[2]��֭er֭er</color>");
																				ev.Player.Broadcast(1, "<color=#ff9900>[1]��֭er֭er</color>");
																				ev.Player.Broadcast(4, "<color=#ff9900>����Ѫ����úڿ���018��207�����ǹ���������ظ�һ��</color>");
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
											ev.Player.Broadcast(1, "<color=#ff9900>��Ȼ���ϱ������ϰˡ������Ҿ��������˰���Ķ���</color>");
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
						ev.Player.ReferenceHub.serverRoles.SetText("��֮�ּ��");
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
									referenceHub.Broadcast(8, "������֮�֣�����scpʤ��");
									referenceHub.RankName = "��֮��";
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
						ev.Player.Broadcast(8, "�ɹ��ٻ�0-9�˵���֮�֡������ٻ�������ʧЧ��~");
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
				Log.Info("[SCP-181][Info] ���ֵ�SCP-181��: " + eva.ReferenceHub.nicknameSync.Network_myNickSync.ToString() + "[" + eva.ReferenceHub.queryProcessor.PlayerId.ToString() + "]");
				eva.Broadcast(5, "<color=#FF0000>[SCP-181��������]</color>\n�����и��ʿ���Ȩ���ţ��˺��и�������\n����������Ʒ�и���ת��");
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
				Log.Info("[��֮�ּ��][Info] ���ֵ���֮�ּ����: " + eva.ReferenceHub.nicknameSync.Network_myNickSync.ToString() + "[" + eva.ReferenceHub.queryProcessor.PlayerId.ToString() + "]");
				eva.Broadcast(5, "<color=#FF0000>[��֮�ּ����������]</color>\n���޷�ʰȡҩƷ���ɶ���Ӳ���ٻ�9����֮��֧Ԯ\n�������ֻ����ɺܵ͵��˺�������Ҫ����SCP��Ӫ���ʤ��");
				if (!scpspsjdidq.Contains(id))
					scpspsjdidq.Add(id);
			}
			if (type == "lzr" && eva != null)
			{
				eva.ReferenceHub.characterClassManager.SetClassID(RoleType.ChaosInsurgency);
				yield return Timing.WaitForSeconds(0.2f);
				eva.ReferenceHub.serverRoles.SetText("����սʿ");
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
				eva.ReferenceHub.serverRoles.SetText("��˫ӥ");
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
				eva.Broadcast(8, "������֮�֣�����scpʤ��");
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
						ev.Sender.RemoteAdminMessage("[Error]��ʹ�� spawn [id] xxx");
						ev.IsAllowed = false;
						return;
					}
					int id = 0;
					int.TryParse(ev.Arguments[0], out id);
					Player eva = Player.Get(id);
					if (eva == null)
					{
						ev.Sender.RemoteAdminMessage("[[Error]�����ܷ�ID��λ�����");
						ev.IsAllowed = false;
						return;
					}
					Timing.RunCoroutine(makescp(int.Parse(ev.Arguments[0]), ev.Arguments[1]));
					ev.Sender.RemoteAdminMessage("[Info]��̨��ִ�и��������Ĳ�����ysy lzr szsjd 181 szs b");
					ev.IsAllowed = false;
					return;
				}
			}
		}
		public IEnumerator<float> DelayTeamRespawn(RespawningTeamEventArgs ev)
		{

			yield return Timing.WaitForSeconds(0.3f);

			Log.Info("ˢ����Ʒ");
			if (ev.NextKnownTeam == Respawning.SpawnableTeamType.ChaosInsurgency)
			{
				Log.Info("����");
				if (scplzridq.Count < 1)
				{
					Log.Info("ˢ��");
					lzrspawncount++;
					int onlyRandom = EventHandlers.GetOnlyRandom(0, ev.Players.Count - 1);
					Timing.RunCoroutine(makescp(ev.Players[onlyRandom].ReferenceHub.queryProcessor.PlayerId, "lzr"));
				}
			}
			if (ev.NextKnownTeam == Respawning.SpawnableTeamType.NineTailedFox)
			{
				Log.Info("��β");
				if (scpysyidq.Count < 1)
				{
					Log.Info("ˢ��");
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
