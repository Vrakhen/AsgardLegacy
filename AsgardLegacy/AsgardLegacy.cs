using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using Jotunn.Utils;
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

		/*
		public static Sprite GuardianSprite;
		public static Sprite BerserkerSprite;
		public static Sprite RangerIcon;
		public static Sprite MageSprite;
		public static Sprite DruidSprite;

		public static Sprite RiposteIcon;
		public static Sprite WeakenIcon;*/

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

			Utility.ModID = "valheim.vrakhen.tribesofvalheim";
			Utility.Folder = Path.GetDirectoryName(Info.Location);
			ZLog.Log("Tribes of Valheims attempting to find tvAssets in the directory with " + Info.Location);

			/*
			var texture = Utility.LoadTextureFromAssets("classLevel.png");
			var classIcon = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));

			texture = Utility.LoadTextureFromAssets("ability1.png");
			Ability_Sprites[0] = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
			texture = Utility.LoadTextureFromAssets("ability2.png");
			Ability_Sprites[1] = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
			texture = Utility.LoadTextureFromAssets("ability3.png");
			Ability_Sprites[2] = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
			texture = Utility.LoadTextureFromAssets("ability4.png");
			Ability_Sprites[3] = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));

			texture = Utility.LoadTextureFromAssets("guardian.png");
			Ability_Sprites[0] = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
			texture = Utility.LoadTextureFromAssets("berserker.png");
			Ability_Sprites[0] = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
			texture = Utility.LoadTextureFromAssets("ranger.png");
			Ability_Sprites[0] = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
			texture = Utility.LoadTextureFromAssets("mage.png");
			Ability_Sprites[0] = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
			texture = Utility.LoadTextureFromAssets("druid.png");
			Ability_Sprites[0] = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));
			*/

			var classIcon = AssetUtils.LoadSpriteFromFile("JotunnModExample/Assets/reee.png");
			Ability_Sprites[0] = AssetUtils.LoadSpriteFromFile("JotunnModExample/Assets/test_var1.png");
			Ability_Sprites[1] = AssetUtils.LoadSpriteFromFile("JotunnModExample/Assets/test_var2.png");
			Ability_Sprites[2] = AssetUtils.LoadSpriteFromFile("JotunnModExample/Assets/test_var3.png");
			Ability_Sprites[3] = AssetUtils.LoadSpriteFromFile("JotunnModExample/Assets/test_var4.png");

			Utility.SetTimer();
			ClassLevelSkillDef = new Skills.SkillDef
			{
				m_skill = (Skills.SkillType) ClassLevelSkillID,
				m_icon = classIcon,
				m_description = "Your class level",
				m_increseStep = 1f
			};
			_Harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), "valheim.vrakhen.tribesofvalheim");
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
					ZLog.Log("Tribes of Valheim : Guardian");
					Ability_Names[0] = "Shatter Fall";
					Ability_Names[1] = "Aegis";
					Ability_Names[2] = "Ice Crush";
					Ability_Names[3] = "Retribution";
					Player.m_localPlayer.ShowTutorial("al_Guardian");
					break;
				case PlayerClass.Berserker:
					ZLog.Log("Tribes of Valheim : Berserker");
					Ability_Names[0] = "Ability 1";
					Ability_Names[1] = "Ability 2";
					Ability_Names[2] = "Ability 3";
					Ability_Names[3] = "Ability 4";
					Player.m_localPlayer.ShowTutorial("al_Berserker");
					break;
				case PlayerClass.Ranger:
					ZLog.Log("Tribes of Valheim : Ranger");
					Ability_Names[0] = "Explosive Arrow";
					Ability_Names[1] = "Rapid Fire";
					Ability_Names[2] = "Shadow Stalk";
					Ability_Names[3] = "Ranger Mark";
					Player.m_localPlayer.ShowTutorial("al_Ranger");
					break;
				case PlayerClass.Mage:
					ZLog.Log("Tribes of Valheim : Mage");
					Ability_Names[0] = "Ability 1";
					Ability_Names[1] = "Ability 2";
					Ability_Names[2] = "Ability 3";
					Ability_Names[3] = "Ability 4";
					Player.m_localPlayer.ShowTutorial("al_Mage");
					break;
				case PlayerClass.Sentinel:
					ZLog.Log("Tribes of Valheim : Druid");
					Ability_Names[0] = "Ability 1";
					Ability_Names[1] = "Ability 2";
					Ability_Names[2] = "Ability 3";
					Ability_Names[3] = "Ability 4";
					Player.m_localPlayer.ShowTutorial("al_Druid");
					break;
				default:
					ZLog.Log("Tribes of Valheim: --None--");
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

		/*[HarmonyPatch(typeof(Character), "Stagger", null)]
		public class al_StaggerPrevention_Patch
		{
			public static bool Prefix(Character __instance)
			{
				bool flag = __instance.GetSEMan().HaveStatusEffect("SE_al_Berserk");
				return !flag;
			}
		}*/

		/*[HarmonyPatch(typeof(Attack), "GetAttackStamina", null)]
		public class AttackStaminaReduction_Patch
		{
			public static void Postfix(Attack __instance, ItemDrop.ItemData ___m_weapon, ref float __result)
			{
				bool flag = ___m_weapon != null && al_player != null && al_player.al_class == PlayerClass.Berserker && ___m_weapon.m_shared.m_itemType == 14;
				if (flag)
				{
					__result *= 0.7f * GlobalConfigs.c_berserkerBonus2h;
				}
			}
		}*/

		[HarmonyPatch(typeof(Character), "Damage", null)]
		public class al_Damage_Patch
		{
			public static bool Prefix(Character __instance, ref HitData hit, float ___m_maxAirAltitude)
			{
				var attacker = hit.GetAttacker();
				bool flag = __instance == Player.m_localPlayer;
				/*if (flag)
				{
					bool inFlight = Guardian.inFlight;
					if (inFlight)
					{
						Guardian.inFlight = false;
						return false;
					}
				}*/

				if (attacker == null)
					return true;
				
				/*
				bool flag3 = attacker != null;
				if (flag3)
				{
					bool flag4 = __instance.m_name == "Shadow Wolf" && !BaseAI.IsEnemy(__instance, attacker);
					if (flag4)
					{
						hit.m_damage.Modify(0.1f);
					}
					Player player = attacker as Player;
					bool flag5 = attacker.GetSEMan().HaveStatusEffect("SE_al_Weaken");
					if (flag5)
					{
						SE_Weaken se_Weaken = (SE_Weaken) attacker.GetSEMan().GetStatusEffect("SE_al_Weaken");
						hit.m_damage.Modify(1f - se_Weaken.damageReduction);
					}
					bool flag6 = attacker.GetSEMan().HaveStatusEffect("SE_al_ShadowStalk");
					if (flag6)
					{
						attacker.GetSEMan().RemoveStatusEffect("SE_al_ShadowStalk", true);
					}
					bool flag7 = attacker.GetSEMan().HaveStatusEffect("SE_al_Rogue");
					if (flag7)
					{
						bool playerUsingDaggerOnly = Class_Rogue.PlayerUsingDaggerOnly;
						if (playerUsingDaggerOnly)
						{
							hit.m_damage.Modify(1.25f);
						}
					}
					bool flag8 = attacker.GetSEMan().HaveStatusEffect("SE_al_Monk");
					if (flag8)
					{
						SE_Monk se_Monk = (SE_Monk) attacker.GetSEMan().GetStatusEffect("SE_al_Monk");
						bool flag9 = Class_Monk.PlayerIsUnarmed && hit.m_damage.m_blunt > 0f;
						if (flag9)
						{
							HitData hitData = hit;
							hitData.m_damage.m_blunt = hitData.m_damage.m_blunt * 1.25f;
							se_Monk.hitCount++;
						}
					}
					bool flag10 = attacker.GetSEMan().HaveStatusEffect("SE_al_Shell");
					if (flag10)
					{
						SE_Shell se_Shell = attacker.GetSEMan().GetStatusEffect("SE_al_Shell") as SE_Shell;
						HitData hitData2 = hit;
						hitData2.m_damage.m_spirit = hitData2.m_damage.m_spirit + se_Shell.spiritDamageOffset;
					}
					bool flag11 = attacker.GetSEMan().HaveStatusEffect("SE_al_BiomeMist");
					if (flag11)
					{
						SE_BiomeMist se_BiomeMist = attacker.GetSEMan().GetStatusEffect("SE_al_BiomeMist") as SE_BiomeMist;
						HitData hitData3 = hit;
						hitData3.m_damage.m_frost = hitData3.m_damage.m_frost + se_BiomeMist.iceDamageOffset;
					}
					bool flag12 = attacker.GetSEMan().HaveStatusEffect("SE_al_BiomeAsh");
					if (flag12)
					{
						SE_BiomeAsh se_BiomeAsh = attacker.GetSEMan().GetStatusEffect("SE_al_BiomeAsh") as SE_BiomeAsh;
						HitData hitData4 = hit;
						hitData4.m_damage.m_fire = hitData4.m_damage.m_fire + se_BiomeAsh.fireDamageOffset;
					}
					bool flag13 = attacker.GetSEMan().HaveStatusEffect("SE_al_Berserk");
					if (flag13)
					{
						SE_Berserk se_Berserk = attacker.GetSEMan().GetStatusEffect("SE_al_Berserk") as SE_Berserk;
						attacker.AddStamina(hit.GetTotalDamage() * se_Berserk.healthAbsorbPercent);
					}
					bool flag14 = attacker.GetSEMan().HaveStatusEffect("SE_al_Execute");
					if (flag14)
					{
						SE_Execute se_Execute = attacker.GetSEMan().GetStatusEffect("SE_al_Execute") as SE_Execute;
						hit.m_staggerMultiplier *= se_Execute.staggerForce;
						HitData hitData5 = hit;
						hitData5.m_damage.m_blunt = hitData5.m_damage.m_blunt * se_Execute.damageBonus;
						HitData hitData6 = hit;
						hitData6.m_damage.m_pierce = hitData6.m_damage.m_pierce * se_Execute.damageBonus;
						HitData hitData7 = hit;
						hitData7.m_damage.m_slash = hitData7.m_damage.m_slash * se_Execute.damageBonus;
						se_Execute.hitCount--;
						bool flag15 = se_Execute.hitCount <= 0;
						if (flag15)
						{
							attacker.GetSEMan().RemoveStatusEffect(se_Execute, true);
						}
					}
					bool flag16 = attacker.GetSEMan().HaveStatusEffect("SE_al_Companion");
					if (flag16)
					{
						SE_Companion se_Companion = attacker.GetSEMan().GetStatusEffect("SE_al_Companion") as SE_Companion;
						hit.m_damage.Modify(se_Companion.damageModifier);
					}
					bool flag17 = attacker.GetSEMan().HaveStatusEffect("SE_al_RootsBuff");
					if (flag17)
					{
						SE_RootsBuff se_RootsBuff = attacker.GetSEMan().GetStatusEffect("SE_al_RootsBuff") as SE_RootsBuff;
						hit.m_damage.Modify(se_RootsBuff.damageModifier);
					}
					bool flag18 = al_player != null && player != null && al_player.al_name == player.GetPlayerName();
					if (flag18)
					{
						bool flag19 = al_player.al_class == PlayerClass.Berserker;
						if (flag19)
						{
							hit.m_damage.Modify(1f + (1f - attacker.GetHealthPercentage()) * 0.4f * GlobalConfigs.c_berserkerBonusDamage);
						}
						else
						{
							bool flag20 = al_player.al_class == PlayerClass.Enchanter;
							if (flag20)
							{
								bool flag21 = Random.value > 0.3f;
								if (flag21)
								{
									float level = player.GetSkills().GetSkillList().FirstOrDefault((Skills.Skill x) => x.m_info == AlterationSkillDef).m_level;
									float value = Random.value;
									bool flag22 = value <= 0.4f;
									if (flag22)
									{
										HitData hitData8 = hit;
										hitData8.m_damage.m_fire = hitData8.m_damage.m_fire + level * GlobalConfigs.c_enchanterBonusElementalTouch;
									}
									else
									{
										bool flag23 = value <= 0.6f;
										if (flag23)
										{
											HitData hitData9 = hit;
											hitData9.m_damage.m_frost = hitData9.m_damage.m_frost + level * GlobalConfigs.c_enchanterBonusElementalTouch;
										}
										else
										{
											HitData hitData10 = hit;
											hitData10.m_damage.m_lightning = hitData10.m_damage.m_lightning + level * GlobalConfigs.c_enchanterBonusElementalTouch;
										}
									}
								}
							}
						}
					}
				}*/
				return true;
			}
		}

		[HarmonyPatch(typeof(Projectile), "OnHit", null)]
		public class Projectile_Hit_Patch
		{
			public static void Postfix(Projectile __instance, Collider collider, Vector3 hitPoint, bool water, float ___m_aoe, int ___m_rayMaskSolids, Character ___m_owner, Vector3 ___m_vel)
			{
				/*
				bool flag = __instance.name == "VL_Charm";
				if (flag)
				{
					bool flag2 = false;
					bool flag3 = __instance.m_aoe > 0f;
					if (flag3)
					{
						Collider[] array = Physics.OverlapSphere(hitPoint, __instance.m_aoe, ___m_rayMaskSolids, 0);
						HashSet<GameObject> hashSet = new HashSet<GameObject>();
						Collider[] array2 = array;
						foreach (Collider collider2 in array2)
						{
							GameObject gameObject = Projectile.FindHitObject(collider2);
							IDestructible component = gameObject.GetComponent<IDestructible>();
							bool flag4 = component != null && !hashSet.Contains(gameObject);
							if (flag4)
							{
								hashSet.Add(gameObject);
								bool flag5 = ValheimLegends.IsValidTarget(component, ref flag2, ___m_owner, __instance.m_dodgeable);
								if (flag5)
								{
									Character character = null;
									gameObject.TryGetComponent<Character>(ref character);
									bool flag6 = character != null;
									bool flag7 = character == null;
									if (flag7)
									{
										character = (Character) gameObject.GetComponentInParent(typeof(Character));
										flag6 = (character != null);
									}
									bool flag8 = flag6 && !character.IsPlayer() && ___m_owner is Player && !character.m_boss;
									if (flag8)
									{
										Player player = ___m_owner as Player;
										SE_Charm se_Charm = (SE_Charm) ScriptableObject.CreateInstance(typeof(SE_Charm));
										se_Charm.m_ttl = SE_Charm.m_baseTTL * VL_GlobalConfigs.c_enchanterCharm;
										se_Charm.summoner = player;
										se_Charm.originalFaction = character.m_faction;
										character.m_faction = player.GetFaction();
										Object.Instantiate<GameObject>(ZNetScene.instance.GetPrefab("fx_boar_pet"), character.GetEyePoint(), Quaternion.identity);
										character.GetSEMan().AddStatusEffect(se_Charm, false);
									}
								}
							}
						}
					}
				}
				else
				{
					bool flag9 = __instance.name == "VL_ValkyrieSpear";
					if (flag9)
					{
						bool flag10 = false;
						bool flag11 = __instance.m_aoe > 0f;
						if (flag11)
						{
							Collider[] array4 = Physics.OverlapSphere(hitPoint, __instance.m_aoe, ___m_rayMaskSolids, 0);
							HashSet<GameObject> hashSet2 = new HashSet<GameObject>();
							Collider[] array5 = array4;
							foreach (Collider collider3 in array5)
							{
								GameObject gameObject2 = Projectile.FindHitObject(collider3);
								IDestructible component2 = gameObject2.GetComponent<IDestructible>();
								bool flag12 = component2 != null && !hashSet2.Contains(gameObject2);
								if (flag12)
								{
									hashSet2.Add(gameObject2);
									bool flag13 = ValheimLegends.IsValidTarget(component2, ref flag10, ___m_owner, __instance.m_dodgeable);
									if (flag13)
									{
										Character character2 = null;
										gameObject2.TryGetComponent<Character>(ref character2);
										bool flag14 = character2 != null;
										bool flag15 = character2 == null;
										if (flag15)
										{
											character2 = (Character) gameObject2.GetComponentInParent(typeof(Character));
											flag14 = (character2 != null);
										}
										bool flag16 = flag14 && !character2.IsPlayer() && ___m_owner is Player && !character2.m_boss;
										if (flag16)
										{
											Player player2 = ___m_owner as Player;
											Vector3 vector = character2.transform.position - player2.transform.position;
											float magnitude = vector.magnitude;
											float num = character2.GetMass() * 0.05f;
											Vector3 vector2;
											vector2..ctor(0f, 4f / num, 0f);
											character2.Stagger(vector);
											Object.Instantiate<GameObject>(ZNetScene.instance.GetPrefab("fx_VL_ParticleLightburst"), character2.transform.position, Quaternion.LookRotation(new Vector3(0f, 1f, 0f)));
											Traverse.Create(character2).Field("m_pushForce").SetValue(vector2);
										}
									}
								}
							}
						}
					}
				}*/
			}
		}


		/*[HarmonyPatch(typeof(Attack), "DoMeleeAttack", null)]
		public class MeleeAttack_Patch
		{
			public static bool Prefix(Attack __instance, Humanoid ___m_character, ref float ___m_damageMultiplier)
			{
				bool flag = ___m_character.GetSEMan().HaveStatusEffect("SE_al_Berserk");
				if (flag)
				{
					SE_Berserk se_Berserk = (SE_Berserk) ___m_character.GetSEMan().GetStatusEffect("SE_al_Berserk");
					___m_damageMultiplier *= se_Berserk.damageModifier;
				}
				return true;
			}
		}*/

		[HarmonyPatch(typeof(ItemDrop.ItemData), "GetBaseBlockPower", new Type[] { typeof(int) })]
		public class BaseBlockPower_Bulwark_Patch
		{
			public static void Postfix(ItemDrop.ItemData __instance, ref float __result)
			{
				if (Guardian.isBlocking)
					__result += 20f;
			}
		}

		/*[HarmonyPatch(typeof(Character), "CheckDeath")]
		public class OnDeath_Patch
		{
			public static bool Prefix(Character __instance)
			{
				bool flag = !__instance.IsDead() && __instance.GetHealth() <= 0f && al_player != null;
				if (flag)
				{
					Player player = __instance as Player;
					bool flag2 = player != null && al_player.al_class == PlayerClass.Priest && player.GetPlayerName() == al_player.al_name;
					if (flag2)
					{
						bool flag3 = !__instance.GetSEMan().HaveStatusEffect("SE_al_DyingLight_CD");
						if (flag3)
						{
							StatusEffect statusEffect = (SE_DyingLight_CD) ScriptableObject.CreateInstance(typeof(SE_DyingLight_CD));
							statusEffect.m_ttl = 600f * GlobalConfigs.c_priestBonusDyingLightCooldown;
							__instance.GetSEMan().AddStatusEffect(statusEffect, false);
							__instance.SetHealth(1f);
							return false;
						}
					}
					else
					{
						bool flag4 = al_player.al_class == PlayerClass.Shaman;
						if (flag4)
						{
							Player localPlayer = Player.m_localPlayer;
							bool flag5 = localPlayer != null && al_player.al_name == localPlayer.GetPlayerName() && Vector3.Distance(localPlayer.transform.position, __instance.transform.position) <= 10f;
							if (flag5)
							{
                                Instantiate(ZNetScene.instance.GetPrefab("fx_al_AbsorbSpirit"), localPlayer.GetCenterPoint(), Quaternion.identity);
								localPlayer.AddStamina(25f * GlobalConfigs.c_shamanBonusSpiritGuide);
							}
						}
					}
				}
				return true;
			}
		}*/

		/*[HarmonyPatch(typeof(HitData), "BlockDamage")]
		public static class BlockDamage_Patch
		{
			public static bool Prefix(HitData __instance, float damage, HitData.DamageTypes ___m_damage)
			{
				bool flag = al_player != null;
				if (flag)
				{
					bool flag2 = al_player.al_class == PlayerClass.Monk && Class_Monk.PlayerIsUnarmed;
					if (flag2)
					{
						bool flag3 = __instance.GetTotalBlockableDamage() >= damage;
						if (flag3)
						{
							SE_Monk se_Monk = (SE_Monk) Player.m_localPlayer.GetSEMan().GetStatusEffect("SE_al_Monk");
							se_Monk.hitCount++;
						}
					}
					else
					{
						bool flag4 = al_player.al_class == PlayerClass.Valkyrie && Class_Valkyrie.PlayerUsingShield;
						if (flag4)
						{
							bool flag5 = __instance.GetTotalBlockableDamage() >= damage;
							if (flag5)
							{
								SE_Valkyrie se_Valkyrie = (SE_Valkyrie) Player.m_localPlayer.GetSEMan().GetStatusEffect("SE_al_Valkyrie");
								se_Valkyrie.hitCount++;
							}
						}
						else
						{
							bool flag6 = al_player.al_class == PlayerClass.Enchanter;
							if (flag6)
							{
								float totalBlockableDamage = __instance.GetTotalBlockableDamage();
								float num = damage / totalBlockableDamage;
								bool flag7 = num > 0f;
								if (flag7)
								{
									float num2 = ___m_damage.m_fire * num;
									float num3 = ___m_damage.m_frost * num;
									float num4 = ___m_damage.m_lightning * num;
									float num5 = num2 + num3 + num4;
									bool flag8 = num5 > 0f;
									if (flag8)
									{
										Player.m_localPlayer.AddStamina(num5 * GlobalConfigs.c_enchanterBonusElementalBlock);
										Player.m_localPlayer.RaiseSkill(AbjurationSkill, num);
                                        Instantiate(ZNetScene.instance.GetPrefab("vfx_Potion_stamina_medium"), Player.m_localPlayer.transform.position, Quaternion.identity);
									}
								}
							}
						}
					}
				}
				return true;
			}
		}*/

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