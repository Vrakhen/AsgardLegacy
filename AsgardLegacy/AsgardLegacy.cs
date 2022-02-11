using System;
using Jotunn.Utils;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace AsgardLegacy
{
	[BepInPlugin("AsgardLegacy", "AsgardLegacy", "0.0.1")]
    [BepInDependency(Jotunn.Main.ModGuid)]
	[NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
	public class AsgardLegacy : BaseUnityPlugin
	{
		public static Harmony _Harmony;

		public const string PluginGUID = "valheim.vrakhen.asgardlegacy";
		public const string PluginName = "AsgardLegacy";
		public const string PluginVersion = "0.0.1";
		public const float PluginVersionF = 0.01f;

		public static long ServerID;
		private static readonly Type patchType = typeof(AsgardLegacy);

		public static AssetBundle runeTableAssets;

		public static bool playerEnabled = true;
		public static List<al_Player> al_playerList;
		public static al_Player al_player;

		public static readonly Color abilityCooldownColor = new Color(1f, 0.3f, 0.3f, 0.5f);

		public static ConfigEntry<bool> modEnabled;
		public static ConfigEntry<string>[] Ability_Hotkeys = new ConfigEntry<string>[4];
		public static ConfigEntry<string>[] Ability_Hotkey_Combos = new ConfigEntry<string>[4];

		public static ConfigEntry<float> icon_X_Offset;
		public static ConfigEntry<float> icon_Y_Offset;
		public static ConfigEntry<bool> showAbilityIcons;
		public static ConfigEntry<string> iconAlignment;

		public static ConfigEntry<string> chosenClass;
		public static ConfigEntry<bool> al_svr_allowAltarClassChange;
		public static ConfigEntry<bool> al_svr_enforceConfigClass;
		public static ConfigEntry<bool> al_svr_aoeRequiresLoS;

		public static Sprite[] Ability_Sprites = new Sprite[4];
		public static string[] Ability_Names = new string[4];
		public static string[] Ability_Descriptions = new string[4];

		public static List<RectTransform> abilitiesStatus = new List<RectTransform>();
		public static bool shouldUseForsakenPower = false;
		public static bool isChanneling = false;
		public static int channelingCancelDelay = 0;
		public static bool isChargingDash = false;
		public static int dashCounter = 0;
		public static int logCheck = 0;
		public static int animationCountdown = 0;

		public static readonly int ClassLevelSkillID = 781;
		public static Skills.SkillType ClassLevelSkill = (Skills.SkillType) ClassLevelSkillID;
		public static Skills.SkillDef ClassLevelSkillDef;
		public static string ClassLevelSkillName = "ClassLevel";

		public class al_Player
		{
			public string al_name;
			public PlayerClass al_class;
		}

		public enum PlayerClass
		{
			None,
			Guardian,
			Berserker,
			Ranger,
			Mage,
			Sentinel
		}

		public static int GetPlayerClassNum
		{
			get
			{
				switch (al_player.al_class)
				{
					case PlayerClass.Guardian:
						return 1;
					case PlayerClass.Berserker:
						return 2;
					case PlayerClass.Ranger:
						return 3;
					case PlayerClass.Mage:
						return 4;
					case PlayerClass.Sentinel:
						return 5;
					default:
						return 0;
				}
			}
		}

		public static bool ClassIsValid
		{
			get
			{
				return al_player != null && al_player.al_class != PlayerClass.None;
			}
		}

		private static bool IsValidTarget(IDestructible destr, ref bool hitCharacter, Character owner, bool m_dodgeable)
		{
			var character = destr as Character;
			if (!character)
				return true;

			if (character == owner
				|| (owner != null && !owner.IsPlayer() && !BaseAI.IsEnemy(owner, character))
				|| m_dodgeable && character.IsDodgeInvincible())
				return false;

			hitCharacter = true;
			return true;
		}

		private void Awake()
		{
			chosenClass = Config.Bind("General", "chosenClass", "None", "Assigns a class to the player if no class is assigned.\nThis will not overwrite an existing class selection.\nA value of None will not attempt to assign any class.");
			al_svr_allowAltarClassChange = Config.Bind("General", "al_svr_allowAltarClassChange", true, "Allows class changing at the altar; if disabled, the only way to change class will be via console or the mod configs.");
			al_svr_enforceConfigClass = Config.Bind("General", "al_svr_enforceConfigClass", false, "True - always sets the player class to this value when the player logs in. False - uses player profile to determine class\nDoes not apply if the chosen class is None.");
			al_svr_aoeRequiresLoS = Config.Bind("General", "al_svr_aoeRequiresLoS", true, "True - all AoE attacks require Line of Sight to the impact point.\nFalse - uses default game behavior for AoE attacks.");

			showAbilityIcons = Config.Bind("Display", "showAbilityIcons", true, "Displays Icons on Hud for each ability");
			iconAlignment = Config.Bind("Display", "iconAlignment", "horizontal", "Aligns icons horizontally or vertically off the guardian power icon; options are horizontal or vertical");
			icon_X_Offset = Config.Bind("Display", "icon_X_Offset", 0f, "Offsets the icon bar horizontally. The icon bar is anchored to the Forsaken power icon.");
			icon_Y_Offset = Config.Bind("Display", "icon_Y_Offset", 0f, "Offsets the icon bar vertically. The icon bar is anchored to the Forsaken power icon.");

			Ability_Hotkeys[0] = Config.Bind("Keybinds", "Ability1_Hotkey", "W", "Ability 1 Hotkey\nUse mouse # to bind an ability to a mouse button\nThe # represents the mouse button; mouse 0 is left click, mouse 1 right click, etc");
			Ability_Hotkeys[1] = Config.Bind("Keybinds", "Ability2_Hotkey", "X", "Ability 2 Hotkey");
			Ability_Hotkeys[2] = Config.Bind("Keybinds", "Ability3_Hotkey", "C", "Ability 3 Hotkey");
			Ability_Hotkeys[3] = Config.Bind("Keybinds", "Ability4_Hotkey", "V", "Ability 4 Hotkey");
			Ability_Hotkey_Combos[0] = Config.Bind("Keybinds", "Ability1_Hotkey_Combo", "", "Ability 1 Combination Key - entering a value will trigger the ability only when both the Hotkey and Hotkey_Combo buttons are pressed\nAllows input from a combination of keys when a value is entered for the combo key\nIf only one key is used, leave the combo key blank\nExamples: space, Q, left shift, left ctrl, right alt, right cmd");
			Ability_Hotkey_Combos[1] = Config.Bind("Keybinds", "Ability2_Hotkey_Combo", "", "Ability 2 Combination Key");
			Ability_Hotkey_Combos[2] = Config.Bind("Keybinds", "Ability3_Hotkey_Combo", "", "Ability 3 Combination Key");
			Ability_Hotkey_Combos[3] = Config.Bind("Keybinds", "Ability4_Hotkey_Combo", "", "Ability 4 Combination Key");

			GlobalConfigs.ConfigStrings.Clear();
			Configs_Common.InitializeConfig(Config);
			GlobalConfigs.ConfigStrings.Add("al_svr_enforceConfigClass", al_svr_enforceConfigClass.Value ? 1 : 0);
			GlobalConfigs.ConfigStrings.Add("al_svr_aoeRequiresLoS", al_svr_aoeRequiresLoS.Value ? 1 : 0);
			GlobalConfigs.ConfigStrings.Add("al_svr_allowAltarClassChange", al_svr_allowAltarClassChange.Value ? 1 : 0);

			GlobalConfigs_Ranger.ConfigStrings.Clear();
			Configs_Ranger.InitializeConfig(Config);
			Configs_Guardian.InitializeConfig(Config);
			Configs_Berserker.InitializeConfig(Config);

			Utility.ModID = "valheim.vrakhen.asgardlegacy";
			Utility.Folder = Path.GetDirectoryName(Info.Location);
			ZLog.Log("AsgardLegacy attempting to find assets in the directory with " + Info.Location);

			var classIcon = AssetUtils.LoadSpriteFromFile("D:/Modding/AsgardLegacy/AsgardLegacy/Assets/reee.png");
			Ability_Sprites[0] = AssetUtils.LoadSpriteFromFile("D:/Modding/AsgardLegacy/AsgardLegacy/Assets/test_var1.png");
			Ability_Sprites[1] = AssetUtils.LoadSpriteFromFile("D:/Modding/AsgardLegacy/AsgardLegacy/Assets/test_var2.png");
			Ability_Sprites[2] = AssetUtils.LoadSpriteFromFile("D:/Modding/AsgardLegacy/AsgardLegacy/Assets/test_var3.png");
			Ability_Sprites[3] = AssetUtils.LoadSpriteFromFile("D:/Modding/AsgardLegacy/AsgardLegacy/Assets/test_var4.png");
			Jotunn.Logger.LogInfo(classIcon.name);

			runeTableAssets = AssetUtils.LoadAssetBundle(Utility.Folder + "/Assets/al_runetable");

			RuneTableController.AddRuneTablePiece();
			RuneTableController.AddRuneTableRecipes();

			Utility.SetTimer();
			ClassLevelSkillDef = new Skills.SkillDef
			{
				m_skill = (Skills.SkillType) ClassLevelSkillID,
				m_icon = classIcon,
				m_description = "Your class level",
				m_increseStep = 1f
			};
			_Harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "valheim.vrakhen.asgardlegacy");
		}
		
		private void OnDestroy()
		{
			if (_Harmony != null)
				_Harmony.UnpatchSelf();
		}

		public static void SettvPlayer(Player p)
		{
			al_player = new al_Player();
			foreach (al_Player al_Player in al_playerList)
			{
				if (p.GetPlayerName() != al_Player.al_name)
					continue;

				al_player.al_name = al_Player.al_name;
				al_player.al_class = al_Player.al_class;
				if ((al_player.al_class == PlayerClass.None && chosenClass.Value.ToLower() != "none")
					|| (chosenClass.Value.ToLower() != "none" && GlobalConfigs.ConfigStrings["al_svr_enforceConfigClass"] != 0f))
				{
					switch (chosenClass.Value.ToLower())
					{
						case "guardian":
							al_player.al_class = PlayerClass.Guardian;
							break;
						case "berserker":
							al_player.al_class = PlayerClass.Berserker;
							break;
						case "ranger":
							al_player.al_class = PlayerClass.Ranger;
							break;
						case "mage":
							al_player.al_class = PlayerClass.Mage;
							break;
						case "druid":
							al_player.al_class = PlayerClass.Sentinel;
							break;
					}
				}
				NameCooldowns();
			}
		}
	
		public static void UpdatetvPlayer(Player p)
		{
			foreach (al_Player al_Player in al_playerList)
			{
				if (p.GetPlayerName() != al_Player.al_name)
					continue;

				al_Player.al_class = al_player.al_class;
				p.GetSkills().ResetSkill(ClassLevelSkill);
				p.GetSkills().RaiseSkill(ClassLevelSkill);
				SavetvPlayer_Patch.Postfix(Game.instance.GetPlayerProfile(), Game.instance.GetPlayerProfile().GetFilename(), Game.instance.GetPlayerProfile().GetName());
			}
		}

		public static void NameCooldowns()
		{
			switch (al_player.al_class)
			{
				case PlayerClass.Guardian:
					ZLog.Log("Asgard Legacy : Guardian");
					Ability_Names[0] = "Shatter Fall";
					Ability_Names[1] = "Aegis";
					Ability_Names[2] = "Ice Crush";
					Ability_Names[3] = "Retribution";
					Player.m_localPlayer.ShowTutorial("al_Guardian");
					break;
				case PlayerClass.Berserker:
					ZLog.Log("Asgard Legacy : Berserker");
					Ability_Names[0] = "Ability 1";
					Ability_Names[1] = "Ability 2";
					Ability_Names[2] = "Ability 3";
					Ability_Names[3] = "Ability 4";
					Player.m_localPlayer.ShowTutorial("al_Berserker");
					break;
				case PlayerClass.Ranger:
					ZLog.Log("Asgard Legacy : Ranger");
					Ability_Names[0] = "Explosive Arrow";
					Ability_Names[1] = "Rapid Fire";
					Ability_Names[2] = "Shadow Stalk";
					Ability_Names[3] = "Ranger Mark";
					Player.m_localPlayer.ShowTutorial("al_Ranger");
					break;
				case PlayerClass.Mage:
					ZLog.Log("Asgard Legacy : Mage");
					Ability_Names[0] = "Ability 1";
					Ability_Names[1] = "Ability 2";
					Ability_Names[2] = "Ability 3";
					Ability_Names[3] = "Ability 4";
					Player.m_localPlayer.ShowTutorial("al_Mage");
					break;
				case PlayerClass.Sentinel:
					ZLog.Log("Asgard Legacy : Druid");
					Ability_Names[0] = "Ability 1";
					Ability_Names[1] = "Ability 2";
					Ability_Names[2] = "Ability 3";
					Ability_Names[3] = "Ability 4";
					Player.m_localPlayer.ShowTutorial("al_Druid");
					break;
				default:
					ZLog.Log("Asgard Legacy: --None--");
					break;
			}
		}

		[HarmonyPatch(typeof(ZNet), "Awake")]
		[HarmonyPriority(2147483647)]
		public static class ZNet_al_Register
		{
			public static void Postfix(ZNet __instance, ZRoutedRpc ___m_routedRpc)
			{
				___m_routedRpc.Register("ConfigSync", new Action<long, ZPackage>(ConfigSync.RPC_ConfigSync));
			}
		}

		[HarmonyPatch(typeof(ZNet), "RPC_PeerInfo")]
		public static class ConfigServerSync
		{
			private static void Postfix(ref ZNet __instance, ZRpc rpc)
			{
				MethodBase methodBase = AccessTools.Method(typeof(ZRoutedRpc), "GetServerPeerID", null, null);
				ServerID = (long) methodBase.Invoke(ZRoutedRpc.instance, new object[0]);
				bool flag = !__instance.IsServer();
				if (flag)
				{
					ZRoutedRpc.instance.InvokeRoutedRPC(ServerID, "ConfigSync", new object[]
					{
						new ZPackage()
					});
				}
			}
		}

		[HarmonyPatch(typeof(PlayerProfile), "SavePlayerToDisk", null)]
		public static class SavetvPlayer_Patch
		{
			public static void Postfix(PlayerProfile __instance, string ___m_filename, string ___m_playerName)
			{
				try
				{
					Directory.CreateDirectory(Utils.GetSaveDataPath() + "/characters/tv");
					var text = Utils.GetSaveDataPath() + "/characters/tv/" + ___m_filename + "_tv.fch";
					var text2 = Utils.GetSaveDataPath() + "/characters/tv/" + ___m_filename + "_tv.fch.new";
					var zpackage = new ZPackage();
					zpackage.Write(GetPlayerClassNum);
					var array = zpackage.GenerateHash();
					var array2 = zpackage.GetArray();
					var fileStream = File.Create(text2);
					var binaryWriter = new BinaryWriter(fileStream);
					binaryWriter.Write(array2.Length);
					binaryWriter.Write(array2);
					binaryWriter.Write(array.Length);
					binaryWriter.Write(array);
					binaryWriter.Flush();
					fileStream.Flush(true);
					fileStream.Close();
					fileStream.Dispose();
					if (File.Exists(text))
						File.Delete(text);
					File.Move(text2, text);
				}
				catch (NullReferenceException ex)
				{
				}
			}
		}

		[HarmonyPatch(typeof(PlayerProfile), "LoadPlayerFromDisk", null)]
		public class LoadtvPlayer_Patch
		{
			public static void Postfix(PlayerProfile __instance, string ___m_filename, string ___m_playerName)
			{
				try
				{
					if (al_playerList == null)
						al_playerList = new List<al_Player>();

					al_playerList.Clear();
					var zpackage = LoadPlayerDataFromDisk(___m_filename);

					if (zpackage != null)
					{
						var al_class = zpackage.ReadInt();
						var al_Player = new al_Player();
						al_Player.al_name = ___m_playerName;
						al_Player.al_class = (PlayerClass) al_class;
						al_playerList.Add(al_Player);
					}
				}
				catch (Exception ex)
				{
					ZLog.LogWarning("Exception while loading player tv profile: " + ex.ToString());
				}
			}

			private static ZPackage LoadPlayerDataFromDisk(string m_filename)
			{
				var path = Utils.GetSaveDataPath() + "/characters/tv/" + m_filename + "_tv.fch";
				FileStream fileStream;
				try
				{
					fileStream = File.OpenRead(path);
				}
				catch
				{
					return null;
				}
				byte[] array;
				try
				{
					var binaryReader = new BinaryReader(fileStream);
					var count = binaryReader.ReadInt32();
					array = binaryReader.ReadBytes(count);
					var count2 = binaryReader.ReadInt32();
					binaryReader.ReadBytes(count2);
				}
				catch
				{
					ZLog.LogError("  error loading tv player data");
					fileStream.Dispose();
					return null;
				}
				fileStream.Dispose();
				return new ZPackage(array);
			}
		}

		[HarmonyPatch(typeof(Skills), "CheatRaiseSkill", null)]
		public class CheatRaiseSkill_al_Patch
		{
			public static bool Prefix(Skills __instance, string name, float value, Player ___m_player)
			{
				bool result;
				if (ConsoleCommands.CheatRaiseSkill(__instance, name, value, ___m_player))
				{
					Console.instance.Print(string.Concat(new object[]
					{
						"Skill ",
						name,
						" raised ",
						value
					}));
					result = false;
				}
				else
				{
					result = true;
				}
				return result;
			}
		}

		[HarmonyPatch(typeof(Terminal), "InputText", null)]
		public class Cheats_al_Patch
		{
			public static void Postfix(Console __instance, InputField ___m_input)
			{
				if (!ZNet.instance || !ZNet.instance.IsServer() || !Player.m_localPlayer || !__instance.IsCheatsEnabled() || !playerEnabled)
					return;

				var text = ___m_input.text;
				var array = text.Split(new char[] { ' ' });
				if (array.Length == 0 || array[0] != "al_changeclass")
					return;

				var className = array[1];
				ConsoleCommands.CheatChangeClass(className);
			}
		}

		[HarmonyPatch(typeof(Aoe), "OnHit")]
		public static class Aoe_LOSCheck_Prefix
		{
			private static bool Prefix(Aoe __instance, Collider collider, Vector3 hitPoint, List<GameObject> ___m_hitList, ref bool __result)
			{
				var gameObject = Projectile.FindHitObject(collider);

				bool result;
				if (___m_hitList.Contains(gameObject))
				{
					__result = false;
					result = false;
				}
				else
				{
					var component = gameObject.GetComponent<IDestructible>();
					if (component != null)
					{
						var character = component as Character;
						if (character && !Utility.LOS_IsValid(character, __instance.transform.position, default(Vector3)))
						{
							__result = false;
							return false;
						}
					}
					result = true;
				}
				return result;
			}
		}

		[HarmonyPatch(typeof(Projectile), "IsValidTarget")]
		public static class Projectile_AoE_LOSCheck_Prefix
		{
			private static bool Prefix(Projectile __instance, IDestructible destr, ref bool hitCharacter, ref bool __result)
			{
				var character = destr as Character;
				if (!character)
					return true;

				if (!Utility.LOS_IsValid(character, __instance.transform.position, __instance.transform.position + __instance.GetVelocity() * -1.5f))
				{
					__result = false;
					return false;
				}
				return true;
			}
		}

		[HarmonyPatch(typeof(Player), "ActivateGuardianPower", null)]
		public class ActivatePowerPrevention_Patch
		{
			public static bool Prefix(Player __instance, ref bool __result)
			{
				bool result;
				if (!shouldUseForsakenPower)
				{
					__result = false;
					result = false;
				}
				else
					result = true;

				return result;
			}
		}

		[HarmonyPatch(typeof(Player), "OnDodgeMortal", null)]
		public class DodgeBreaksChanneling_Patch
		{
			public static void Postfix(Player __instance)
			{
				bool isChanneling = AsgardLegacy.isChanneling;
				if (isChanneling)
				{
					AsgardLegacy.isChanneling = false;
				}
			}
		}

		[HarmonyPatch(typeof(Player), "StartGuardianPower", null)]
		public class StartPowerPrevention_Patch
		{
			public static bool Prefix(Player __instance, ref bool __result)
			{
				bool flag = !shouldUseForsakenPower;
				bool result;
				if (flag)
				{
					__result = false;
					result = false;
				}
				else
				{
					result = true;
				}
				return result;
			}
		}

		[HarmonyPatch(typeof(Player), "CanMove", null)]
		public class CanMove_Casting_Patch
		{
			public static void Postfix(Player __instance, ref bool __result)
			{
				if (isChanneling)
					__result = false;
			}
		}

		[HarmonyPatch(typeof(Humanoid), "UseItem")]
		internal class UseItemPatch
		{
			public static bool Prefix(Humanoid __instance, Inventory inventory, ItemDrop.ItemData item, bool fromInventoryGui, Inventory ___m_inventory, ZSyncAnimation ___m_zanim)
			{
				var name = item.m_shared.m_name;
				var player = __instance as Player;
				bool flag = player != null && al_player != null && player.GetPlayerName() == al_player.al_name && al_player.al_class == PlayerClass.Sentinel;

				if (player == null || al_player == null || player.GetPlayerName() != al_player.al_name || al_player.al_class != PlayerClass.Sentinel)
					return true;

				var isSeed = name.Contains("$item_pinecone") || name.Contains("$item_beechseeds") || name.Contains("$item_fircone") || name.Contains("$item_ancientseed") || name.Contains("$item_birchseeds");
				if (isSeed)
				{
					if (inventory == null)
						inventory = ___m_inventory;

					if (!inventory.ContainsItem(item))
						return false;

					var hoverObject = __instance.GetHoverObject();
					var hoverable = hoverObject ? hoverObject.GetComponentInParent<Hoverable>() : null;
					var flag5 = false;
					if (hoverable != null && !fromInventoryGui)
					{
						var componentInParent = hoverObject.GetComponentInParent<Interactable>();
						flag5 = (componentInParent != null && componentInParent.UseItem(__instance, item));
					}

					if (flag5)
						return false;

					/*SE_SeedRegeneration se_SeedRegeneration = (SE_SeedRegeneration) ScriptableObject.CreateInstance(typeof(SE_SeedRegeneration));
					se_SeedRegeneration.m_ttl = SE_SeedRegeneration.m_baseTTL;
					se_SeedRegeneration.m_icon = item.GetIcon();
					bool flag7 = name.Contains("$item_pinecone");
					if (flag7)
					{
						se_SeedRegeneration.m_HealAmount = 10f;
					}
					else
					{
						bool flag8 = name.Contains("$item_ancientseed");
						if (flag8)
						{
							se_SeedRegeneration.m_HealAmount = 20f;
						}
						else
						{
							bool flag9 = name.Contains("$item_fircone");
							if (flag9)
							{
								se_SeedRegeneration.m_HealAmount = 7f;
							}
							else
							{
								bool flag10 = name.Contains("$item_birchseeds");
								if (flag10)
								{
									se_SeedRegeneration.m_HealAmount = 12f;
								}
								else
								{
									se_SeedRegeneration.m_HealAmount = 5f;
								}
							}
						}
					}
					se_SeedRegeneration.m_HealAmount *= GlobalConfigs.c_druidBonusSeeds;
					player.GetSEMan().AddStatusEffect(se_SeedRegeneration, true);*/
					Instantiate(ZNetScene.instance.GetPrefab("vfx_Potion_stamina_medium"), player.transform.position, Quaternion.identity);
					inventory.RemoveOneItem(item);
					__instance.m_consumeItemEffects.Create(Player.m_localPlayer.transform.position, Quaternion.identity, null, 1f, -1);
					___m_zanim.SetTrigger("eat");
					return false;
				}
				return true;
			}
		}

		[HarmonyPatch(typeof(Skills), "GetSkillDef")]
		public static class GetSkillDef_Patch
		{
			public static void Postfix(Skills __instance, Skills.SkillType type, List<Skills.SkillDef> ___m_skills, ref Skills.SkillDef __result)
			{
				MethodInfo methodInfo = AccessTools.Method(typeof(Localization), "AddWord", null, null);

				if (__result != null || ClassLevelSkillDef == null || ___m_skills.Contains(ClassLevelSkillDef))
					return;

				___m_skills.Add(ClassLevelSkillDef);
				MethodBase methodBase = methodInfo;
				object instance = Localization.instance;
				object[] array = new object[] { "skill_" + ClassLevelSkillDef.m_skill , ClassLevelSkillName };
				methodBase.Invoke(instance, array);

				__result = ___m_skills.FirstOrDefault((Skills.SkillDef x) => x.m_skill == type);
			}
		}

		[HarmonyPatch(typeof(Skills), "IsSkillValid")]
		public static class ValidSkill_Patch
		{
			public static bool Prefix(Skills __instance, Skills.SkillType type, ref bool __result)
			{
				bool result;
				if (type == ClassLevelSkill)
				{
					__result = true;
					result = false;
				}
				else
					result = true;

				return result;
			}
		}

		[HarmonyPatch(typeof(Hud), "UpdateStatusEffects")]
		public static class SkillIcon_Patch
		{
			public static void Postfix(Hud __instance)
			{
				if (__instance == null || !ClassIsValid || !showAbilityIcons.Value)
					return;

				if (abilitiesStatus == null)
				{
					abilitiesStatus = new List<RectTransform>();
					abilitiesStatus.Clear();
				}

				if (abilitiesStatus.Count != 4)
				{
					foreach (RectTransform rectTransform in abilitiesStatus)
					{
						Destroy(rectTransform.gameObject);
					}
					abilitiesStatus.Clear();
					Utility.InitiateAbilityStatus(__instance);
				}

				if (abilitiesStatus == null)
					return;

				for (var i = 0; i < abilitiesStatus.Count; i++)
				{
					var rectTransform2 = abilitiesStatus[i];
					var component = rectTransform2.Find("Icon").GetComponent<Image>();
					string text;

					var player = Player.m_localPlayer;

					component.sprite = Ability_Sprites[i];
					var abilityOnCD = player.GetSEMan().HaveStatusEffect("SE_Ability" + (i + 1) + "_CD");

					if (!abilityOnCD)
					{
						if (Utility.IsPlayerAbilityUnlockedByIndex(player, i + 1))
						{
							component.color = Color.white;
							text = Ability_Hotkeys[i].Value;
							var isCombo = Ability_Hotkey_Combos[i].Value != "";
							if (isCombo)
								text += " + " + Ability_Hotkey_Combos[i].Value;
						}
						else
						{
							component.color = abilityCooldownColor;
							text = "Level " + Utility.GetAbilityUnlockLevelByIndex(i + 1);
						}
					}
					else
					{
						component.color = abilityCooldownColor;
						text = StatusEffect.GetTimeString(Player.m_localPlayer.GetSEMan().GetStatusEffect("SE_Ability" + (i + 1) + "_CD").GetRemaningTime(), false, false);
					}

					var component2 = rectTransform2.Find("TimeText").GetComponent<Text>();
					bool flag13 = !string.IsNullOrEmpty(text);
					if (flag13)
					{
						component2.gameObject.SetActive(true);
						component2.text = text;
					}
					else
					{
						component2.gameObject.SetActive(false);
					}
				}
			}
		}

		[HarmonyPatch(typeof(OfferingBowl), "UseItem", null)]
		public class OfferingForClass_Patch
		{
			public static bool Prefix(OfferingBowl __instance, Humanoid user, ItemDrop.ItemData item, Transform ___m_itemSpawnPoint, EffectList ___m_fuelAddedEffects, ref bool __result)
			{
				if (GlobalConfigs.ConfigStrings["al_svr_allowAltarClassChange"] == 0)
					return true;

				var classSetup = false;
				if (item.m_shared.m_name.Contains("item_stone") && al_player.al_class != PlayerClass.Guardian)
				{
					user.Message(MessageHud.MessageType.Center, "Heimdall granted you the powers of a Guardian", 0, null);
					al_player.al_class = PlayerClass.Guardian;
					classSetup = true;
				}
				else if(item.m_shared.m_name.Contains("item_flint") && al_player.al_class != PlayerClass.Berserker)
				{
					user.Message(MessageHud.MessageType.Center, "Thor granted you the powers of a Berserker", 0, null);
					al_player.al_class = PlayerClass.Berserker;
					classSetup = true;
				}
				else if(item.m_shared.m_name.Contains("item_boar_meat") && al_player.al_class != PlayerClass.Ranger)
				{
					user.Message(MessageHud.MessageType.Center, "Ullr granted you the powers of a Ranger", 0, null);
					al_player.al_class = PlayerClass.Ranger;
					classSetup = true;
				}
				else if (item.m_shared.m_name.Contains("item_greydwarfeye") && al_player.al_class != PlayerClass.Mage)
				{
					user.Message(MessageHud.MessageType.Center, "Freya granted you the powers of an Mage", 0, null);
					al_player.al_class = PlayerClass.Mage;
					classSetup = true;
				}
				else if(item.m_shared.m_name.Contains("item_wood") && al_player.al_class != PlayerClass.Sentinel)
				{
					user.Message(MessageHud.MessageType.Center, "Tyr granted you the powers of a Sentinel", 0, null);
					al_player.al_class = PlayerClass.Sentinel;
					classSetup = true;
				}

				if (classSetup)
				{
					user.GetInventory().RemoveItem(item.m_shared.m_name, 1);
					user.ShowRemovedMessage(item, 1);
					UpdatetvPlayer(Player.m_localPlayer);
					NameCooldowns();

					if (___m_itemSpawnPoint && ___m_fuelAddedEffects != null)
						___m_fuelAddedEffects.Create(___m_itemSpawnPoint.position, __instance.transform.rotation, null, 1f, -1);

					Instantiate(ZNetScene.instance.GetPrefab("fx_GP_Activation"), user.GetCenterPoint(), Quaternion.identity);

					if (abilitiesStatus != null)
					{
						foreach (RectTransform rectTransform in abilitiesStatus)
						{
							if (rectTransform.gameObject == null)
								continue;

							Destroy(rectTransform.gameObject);
						}
						abilitiesStatus.Clear();
					}
					__result = false;
					return false;
				}

				return true;
			}
		}

		[HarmonyPatch(typeof(ZNet), "OnDestroy")]
		public static class RemoveHud_Patch
		{
			public static bool Prefix()
			{
				if (abilitiesStatus == null)
					return true;

				foreach (RectTransform rectTransform in abilitiesStatus)
				{
					if (rectTransform.gameObject == null)
						continue;

					Destroy(rectTransform.gameObject);
				}
				abilitiesStatus.Clear();
				abilitiesStatus = null;

				return true;
			}
		}

		[HarmonyPatch(typeof(Player), "OnSpawned", null)]
		public class SetClass_Patch
		{
			public static void Postfix(Player __instance)
			{
                SettvPlayer(__instance);
			}
		}

		[HarmonyPatch(typeof(Player), "RaiseSkill")]
		public class RaiseSkill_Patch
		{
			public static bool Prefix(Player __instance, Skills.SkillType skill)
			{
				if (skill == ClassLevelSkill && __instance.GetSkills().GetSkillLevel(ClassLevelSkill) >= SkillData.max_level)
					return false;

				return true;
			}
		}

		[HarmonyPatch(typeof(Skills), nameof(Skills.LowerAllSkills))]
		public class PreventClassLevelSkillDecrease_Patch
		{
			public static bool Prefix(float factor, Dictionary<Skills.SkillType, Skills.Skill> ___m_skillData, Player ___m_player)
			{
				foreach (var keyValuePair in ___m_skillData)
				{
					if (keyValuePair.Key == ClassLevelSkill)
						continue;

					float num = keyValuePair.Value.m_level * factor;
					keyValuePair.Value.m_level -= num;
					keyValuePair.Value.m_accumulator = 0f;
				}
				___m_player.Message(MessageHud.MessageType.TopLeft, "$msg_skills_lowered", 0, null);

				return false;
			}
		}

		[HarmonyPatch(typeof(Player), "Update", null)]
		public class AbilityInput_Patch
		{
			public static bool Prefix(Player __instance)
			{
				if (ZInput.GetButtonDown("GPower") || ZInput.GetButtonDown("JoyGPower"))
					shouldUseForsakenPower = true;

				return true;
			}

			public static void Postfix(Player __instance, ref float ___m_maxAirAltitude, ref Rigidbody ___m_body, ref Animator ___m_animator, ref float ___m_lastGroundTouch, float ___m_waterLevel)
			{


				if (Input.GetKeyDown(KeyCode.PageDown))
					__instance.GetSkills().GetSkill(ClassLevelSkill).m_level = SkillData.max_level;

				if (Utility.ReadyTime)
				{
					Player localPlayer = Player.m_localPlayer;

					if (localPlayer != null && playerEnabled)
					{
						if (Utility.TakeInput(localPlayer) && !localPlayer.InPlaceMode())
						{
							switch (al_player.al_class)
							{
								case PlayerClass.Guardian:
									Guardian.ProcessInput(localPlayer);
									break;
								case PlayerClass.Berserker:
									Berserker.ProcessInput(localPlayer);
									break;
								case PlayerClass.Ranger:
									Ranger.ProcessInput(localPlayer);
									break;
								case PlayerClass.Mage:

									break;
								case PlayerClass.Sentinel:

									break;
							}
						}
					}
				}

				if (animationCountdown > 0)
                    animationCountdown--;
			}
		}

		[HarmonyPatch(typeof(PlayerProfile), "LoadPlayerData")]
		public static class LoadSkillsPatch
		{
			public static void Postfix(PlayerProfile __instance, Player player)
			{
				var skillData = __instance.LoadModData<SkillData>();
				if (player.GetSkills().GetSkillList().FirstOrDefault((Skills.Skill x) => x.m_info == ClassLevelSkillDef) == null)
				{
					var skill = (Skills.Skill) AccessTools.Method(typeof(Skills), "GetSkill", null, null).Invoke(player.GetSkills(), new object[] { ClassLevelSkill });
					skill.m_level = skillData.level;
					skill.m_accumulator = skillData.accumulator;
				}
			}
		}
	}
}