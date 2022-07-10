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
		public static string[] Passive_Names = new string[6];
		public static string[] Ability_Descriptions = new string[4];

		public static bool isChanneling = false;
		public static bool shouldUseForsakenPower = false;

		public static List<RectTransform> abilitiesStatus = new List<RectTransform>();

		public static readonly int ClassLevelSkillID = 781;
		public static Skills.SkillType ClassLevelSkill = (Skills.SkillType) ClassLevelSkillID;
		public static Skills.SkillDef ClassLevelSkillDef;
		public static string ClassLevelSkillName = "ClassLevel";

		public static Type ShaderReplacer;

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
					case PlayerClass.Sentinel:
						return 4;
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
			Utility.ModID = "valheim.vrakhen.asgardlegacy";
			Utility.Folder = Path.GetDirectoryName(Info.Location);
			ZLog.Log("AsgardLegacy attempting to find assets in the directory with " + Info.Location);

			runeTableAssets = AssetUtils.LoadAssetBundle(Utility.Folder + "/Assets/al_runetable");

			chosenClass = Config.Bind("General", "chosenClass", "None", "Assigns a class to the player if no class is assigned.\nThis will not overwrite an existing class selection.\nA value of None will not attempt to assign any class.");
			al_svr_allowAltarClassChange = Config.Bind("General", "al_svr_allowAltarClassChange", true, "Allows class changing at the altar; if disabled, the only way to change class will be via console or the mod configs.");
			al_svr_enforceConfigClass = Config.Bind("General", "al_svr_enforceConfigClass", false, "True - always sets the player class to this value when the player logs in. False - uses player profile to determine class\nDoes not apply if the chosen class is None.");
			al_svr_aoeRequiresLoS = Config.Bind("General", "al_svr_aoeRequiresLoS", false, "True - all AoE attacks require Line of Sight to the impact point.\nFalse - uses default game behavior for AoE attacks.");

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
			GlobalConfigs_Guardian.ConfigStrings.Clear();
			GlobalConfigs_Berserker.ConfigStrings.Clear();
			GlobalConfigs_Sentinel.ConfigStrings.Clear();

			Configs_Ranger.InitializeConfig(Config);
			Configs_Guardian.InitializeConfig(Config);
			Configs_Berserker.InitializeConfig(Config);
			Configs_Sentinel.InitializeConfig(Config);

			var classIcon = runeTableAssets.LoadAsset<Sprite>("AL_class_icon");
			Ability_Sprites[0] = runeTableAssets.LoadAsset<Sprite>("AL_rune_stone_ice_icon");
			Ability_Sprites[1] = runeTableAssets.LoadAsset<Sprite>("AL_rune_stone_lightning_icon");
			Ability_Sprites[2] = runeTableAssets.LoadAsset<Sprite>("AL_rune_stone_poison_icon");
			Ability_Sprites[3] = runeTableAssets.LoadAsset<Sprite>("AL_rune_stone_fire_icon");
			Jotunn.Logger.LogInfo(classIcon.name);

			RuneTable.AddRuneTablePiece();
			RunesController.AddRuneTableRecipes();

			/*TextAsset txt = runeTableAssets.LoadAsset<TextAsset>("AsgardLegacy") as TextAsset;

			// Load the assembly and get a type (class) from it
			var assembly = System.Reflection.Assembly.Load(txt.bytes);
			ShaderReplacer = assembly.GetType("ShaderReplacerBehaviour");

			Debug.Log("AL : " + txt.name);
			Debug.Log("AL : " + assembly.CodeBase);
			Debug.Log("AL : " + ShaderReplacer.FullName);*/

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

		public static void SetALPlayer(Player p)
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
						case "sentinel":
							al_player.al_class = PlayerClass.Sentinel;
							break;
					}
				}
				NameCooldowns();
			}
		}
	
		public static void UpdateALPlayer(Player p)
		{
			foreach (al_Player al_Player in al_playerList)
			{
				if (p.GetPlayerName() != al_Player.al_name)
					continue;

				al_Player.al_class = al_player.al_class;
				p.GetSkills().ResetSkill(ClassLevelSkill);
				p.GetSkills().RaiseSkill(ClassLevelSkill);
				SaveALPlayer_Patch.Postfix(Game.instance.GetPlayerProfile(), Game.instance.GetPlayerProfile().GetFilename(), Game.instance.GetPlayerProfile().GetName());
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
					Passive_Names[0] = "Force of Nature";
					Passive_Names[1] = "Bulwark";
					Passive_Names[2] = "War Cry";
					Passive_Names[3] = "Immovable";
					Passive_Names[4] = "Absorb Elements";
					Passive_Names[5] = "Undying Will";
					Player.m_localPlayer.ShowTutorial("al_Guardian");
					break;
				case PlayerClass.Berserker:
					ZLog.Log("Asgard Legacy : Berserker");
					Ability_Names[0] = "Charge";
					Ability_Names[1] = "Dreadful Roar";
					Ability_Names[2] = "Raging Storm";
					Ability_Names[3] = "Frenzy";
					Passive_Names[0] = "Two-handed Expert";
					Passive_Names[1] = "Reckless";
					Passive_Names[2] = "Siphon Life";
					Passive_Names[3] = "Adrenaline Rush";
					Passive_Names[4] = "Rebuke";
					Passive_Names[5] = "Deny Pain";
					Player.m_localPlayer.ShowTutorial("al_Berserker");
					break;
				case PlayerClass.Ranger:
					ZLog.Log("Asgard Legacy : Ranger");
					Ability_Names[0] = "Explosive Arrow";
					Ability_Names[1] = "Shadow Stalk";
					Ability_Names[2] = "Rapid Fire";
					Ability_Names[3] = "Ranger Mark";
					Passive_Names[0] = "Bow Specialist";
					Passive_Names[1] = "Longstrider";
					Passive_Names[2] = "Speed Burst";
					Passive_Names[3] = "Go For The Eyes";
					Passive_Names[4] = "Elemental Touch";
					Passive_Names[5] = "Ammo Saver";
					Player.m_localPlayer.ShowTutorial("al_Ranger");
					break;
				case PlayerClass.Sentinel:
					ZLog.Log("Asgard Legacy : Sentinel");
					Ability_Names[0] = "Rejuvenating Strike";
					Ability_Names[1] = "Mending Spirits";
					Ability_Names[2] = "Chains of Light";
					Ability_Names[3] = "Purging Flames";
					Passive_Names[0] = "One-handed Master";
					Passive_Names[1] = "Dwarven Fortitude";
					Passive_Names[2] = "Healer's Gift";
					Passive_Names[3] = "Cleansing Roll";
					Passive_Names[4] = "Vengeful Wave";
					Passive_Names[5] = "Powerful Build";
					Player.m_localPlayer.ShowTutorial("al_Sentinel");
					break;
				default:
					ZLog.Log("Asgard Legacy: --None--");
					break;
			}
		}

		[HarmonyPatch(typeof(ZNet), "Awake")]
		[HarmonyPriority(2147483647)]
		public static class ZNet_AL_Register
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
		public static class SaveALPlayer_Patch
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
		public class LoadALPlayer_Patch
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
				var path = Utils.GetSaveDataPath(Game.instance.GetPlayerProfile().m_fileSource) + "/characters/tv/" + m_filename + "_tv.fch";
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
		public class CheatRaiseSkill_AL_Patch
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
		public class Cheats_AL_Patch
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
							component.color = i == 0 && al_player.al_class == PlayerClass.Berserker && player.transform.position.y > 1000f
								? abilityCooldownColor
								: Color.white;

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
				var raiseSkill = false;
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
				else if(item.m_shared.m_name.Contains("item_wood") && al_player.al_class != PlayerClass.Sentinel)
				{
					user.Message(MessageHud.MessageType.Center, "Tyr granted you the powers of a Sentinel", 0, null);
					al_player.al_class = PlayerClass.Sentinel;
					classSetup = true;
				}
				else if(item.m_shared.m_name.Contains("item_trophy_eikthyr")
					&& user.GetSkills().GetSkillLevel(ClassLevelSkill) < 3)
				{
					user.Message(MessageHud.MessageType.Center, "Eikthyr granted you strength", 0, null);
					user.GetSkills().GetSkill(ClassLevelSkill).m_level = 3f;
					user.GetSkills().GetSkill(ClassLevelSkill).m_accumulator = 0f;
					raiseSkill = true;
				}
				else if(item.m_shared.m_name.Contains("item_trophy_elder")
					&& user.GetSkills().GetSkillLevel(ClassLevelSkill) >= 3
					&& user.GetSkills().GetSkillLevel(ClassLevelSkill) < 6)
				{
					user.Message(MessageHud.MessageType.Center, "The Elder granted you strength", 0, null);
					user.GetSkills().GetSkill(ClassLevelSkill).m_level = 6f;
					user.GetSkills().GetSkill(ClassLevelSkill).m_accumulator = 0f;
					raiseSkill = true;
				}
				else if(item.m_shared.m_name.Contains("item_trophy_bonemass")
					&& user.GetSkills().GetSkillLevel(ClassLevelSkill) >= 6
					&& user.GetSkills().GetSkillLevel(ClassLevelSkill) < 9)
				{
					user.Message(MessageHud.MessageType.Center, "Bonemass granted you strength", 0, null);
					user.GetSkills().GetSkill(ClassLevelSkill).m_level = 9f;
					user.GetSkills().GetSkill(ClassLevelSkill).m_accumulator = 0f;
					raiseSkill = true;
				}
				else if(item.m_shared.m_name.Contains("item_trophy_dragonqueen")
					&& user.GetSkills().GetSkillLevel(ClassLevelSkill) >= 9
					&& user.GetSkills().GetSkillLevel(ClassLevelSkill) < 12)
				{
					user.Message(MessageHud.MessageType.Center, "Moder granted you strength", 0, null);
					user.GetSkills().GetSkill(ClassLevelSkill).m_level = 12f;
					user.GetSkills().GetSkill(ClassLevelSkill).m_accumulator = 0f;
					raiseSkill = true;
				}
				else if(item.m_shared.m_name.Contains("item_trophy_goblinking")
					&& user.GetSkills().GetSkillLevel(ClassLevelSkill) >= 12
					&& user.GetSkills().GetSkillLevel(ClassLevelSkill) < 15)
				{
					user.Message(MessageHud.MessageType.Center, "Yagluth granted you strength", 0, null);
					user.GetSkills().GetSkill(ClassLevelSkill).m_level = 15f;
					user.GetSkills().GetSkill(ClassLevelSkill).m_accumulator = 0f;
					raiseSkill = true;
				}

				if (classSetup || raiseSkill)
				{
					user.GetInventory().RemoveItem(item.m_shared.m_name, 1);
					user.ShowRemovedMessage(item, 1);

					if(classSetup)
					{
						UpdateALPlayer(Player.m_localPlayer);
						NameCooldowns();

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
					}

					if (___m_itemSpawnPoint && ___m_fuelAddedEffects != null)
						___m_fuelAddedEffects.Create(___m_itemSpawnPoint.position, __instance.transform.rotation, null, 1f, -1);

					Instantiate(ZNetScene.instance.GetPrefab("fx_GP_Activation"), user.GetCenterPoint(), Quaternion.identity);
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
                SetALPlayer(__instance);
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