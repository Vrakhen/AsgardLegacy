using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace AsgardLegacy
{
	public static class Utility
	{
		public static string[] CleansedSE = { "Frost", "Burning", "Poison", "Lightning", "Tared", "Wet" };
		public static float[] RuneDamageBonus = { .1f, .2f, .3f, .4f, .5f };
		public static float[] RuneResistanceBonus = { .05f, .1f, .15f, .2f, .25f };
		public static float[] RuneDamageMalus = { .0f, .0f, .0f, .0f, .0f };
		public static float[] RuneResistanceMalus = { .05f, .1f, .15f, .2f, .25f };

		public enum LevelUpUnlock
        {
			None,
			Skill,
			Passive
        };

		public static string GetModDataPath(this PlayerProfile profile)
		{
			return Path.Combine(Utils.GetSaveDataPath(), "ModData", ModID, "char_" + profile.GetFilename());
		}

		public static TData LoadModData<TData>(this PlayerProfile profile) where TData : new()
		{
			bool flag = !File.Exists(profile.GetModDataPath());
			TData result;
			if (flag)
			{
				result = Activator.CreateInstance<TData>();
			}
			else
			{
				string text = File.ReadAllText(profile.GetModDataPath());
				result = JsonUtility.FromJson<TData>(text);
			}
			return result;
		}

		public static void SaveModData<TData>(this PlayerProfile profile, TData data)
		{
			Directory.CreateDirectory(Path.GetDirectoryName(profile.GetModDataPath()));
			File.WriteAllText(profile.GetModDataPath(), JsonUtility.ToJson(data));
		}

		public static Texture2D LoadTextureFromAssets(string path)
		{
			Texture2D result;
			try
			{
				byte[] array = File.ReadAllBytes(Path.Combine(Folder, "tvAssets", path));
				Texture2D texture2D = new Texture2D(1, 1);
				ImageConversion.LoadImage(texture2D, array);
				result = texture2D;
			}
			catch
			{
				byte[] array2 = File.ReadAllBytes(Path.Combine(Folder, path));
				Texture2D texture2D2 = new Texture2D(1, 1);
				ImageConversion.LoadImage(texture2D2, array2);
				result = texture2D2;
			}
			return result;
		}

		public static bool TakeInput(Player p)
		{
			if (p.IsDead() || p.InCutscene() || p.IsTeleporting())
				return false;

			return (!Chat.instance || !Chat.instance.HasFocus()) 
				&& !Console.IsVisible() && !TextInput.IsVisible() && !StoreGui.IsVisible() && !InventoryGui.IsVisible() && !Menu.IsVisible() 
				&& (!TextViewer.instance || !TextViewer.instance.IsVisible()) && !Minimap.IsOpen() && !GameCamera.InFreeFly();
		}

		public static void InitiateAbilityStatus(Hud hud)
		{
			var classIsValid = AsgardLegacy.ClassIsValid;
			if (classIsValid)
			{
				var widthRatio = Screen.width / 1920f;
				var heightRatio = Screen.height / 1080f;

				var widthSpacing = AsgardLegacy.iconAlignment.Value.ToLower() == "vertical" ? 0f : 80f * widthRatio;
				var heightSpacing = AsgardLegacy.iconAlignment.Value.ToLower() == "vertical" ? 100f * heightRatio : 0f;

				var startHeight = 106f * heightRatio + AsgardLegacy.icon_Y_Offset.Value;
				var startWidth = 209f * widthRatio + AsgardLegacy.icon_X_Offset.Value;

				AsgardLegacy.abilitiesStatus = new List<RectTransform>();

				var position = new Vector2(startWidth, startHeight);

				for(var i = 0; i < 4; ++i)
				{
					position.x += widthSpacing;
					position.y += heightSpacing;

					var rectTransform = Object.Instantiate(hud.m_statusEffectTemplate, position, Quaternion.identity, hud.m_statusEffectListRoot);
					rectTransform.gameObject.SetActive(true);
					rectTransform.GetComponentInChildren<Text>().text = string.Concat(AsgardLegacy.Ability_Names[i].Replace(' ', '\n'));

					AsgardLegacy.abilitiesStatus.Add(rectTransform);
				}
			}
		}

		public static void RotatePlayerToTarget(Player p)
		{
			Vector3 lookDir = p.GetLookDir();
			lookDir.y = 0f;
			p.transform.rotation = Quaternion.LookRotation(lookDir);
		}

		public static bool LOS_IsValid(Character hit_char, Vector3 splash_center, Vector3 splash_alternate = default(Vector3))
		{
			bool flag = false;
			bool flag2 = GlobalConfigs.ConfigStrings["al_svr_aoeRequiresLoS"] == 0f;
			bool result;
			if (flag2)
			{
				result = true;
			}
			else
			{
				bool flag3 = splash_alternate == default(Vector3);
				if (flag3)
				{
					splash_alternate = splash_center + new Vector3(0f, 0.2f, 0f);
				}
				bool flag4 = hit_char != null;
				if (flag4)
				{
					RaycastHit hit = default(RaycastHit);
					Vector3 vector = hit_char.GetCenterPoint() - splash_center;
					bool flag5 = Physics.Raycast(splash_center, vector, out hit);
					if (flag5)
					{
						bool flag6 = CollidedWithTarget(hit_char, hit_char.GetCollider(), hit);
						if (flag6)
						{
							flag = true;
						}
						else
						{
							for (int i = 0; i < 8; i++)
							{
								Vector3 size = hit_char.GetCollider().bounds.size;
								Vector3 vector2 = hit_char.GetCenterPoint() + new Vector3(size.x * ((float) Random.Range(-i, i) / 6f), size.y * ((float) Random.Range(-i, i) / 4f), size.z * ((float) Random.Range(-i, i) / 6f)) - splash_center;
								bool flag7 = Physics.Raycast(splash_center, vector2, out hit);
								if (flag7)
								{
									bool flag8 = CollidedWithTarget(hit_char, hit_char.GetCollider(), hit);
									if (flag8)
									{
										flag = true;
										break;
									}
								}
							}
						}
					}
					bool flag9 = !flag && splash_alternate != default(Vector3) && splash_alternate != splash_center;
					if (flag9)
					{
						Vector3 vector3 = hit_char.GetCenterPoint() - splash_alternate;
						bool flag10 = Physics.Raycast(splash_alternate, vector3, out hit);
						if (flag10)
						{
							bool flag11 = CollidedWithTarget(hit_char, hit_char.GetCollider(), hit);
							if (flag11)
							{
								flag = true;
							}
							else
							{
								for (int j = 0; j < 8; j++)
								{
									Vector3 size2 = hit_char.GetCollider().bounds.size;
									Vector3 vector4 = hit_char.GetCenterPoint() + new Vector3(size2.x * ((float) Random.Range(-j, j) / 6f), size2.y * ((float) Random.Range(-j, j) / 4f), size2.z * ((float) Random.Range(-j, j) / 6f)) - splash_alternate;
									bool flag12 = Physics.Raycast(splash_alternate, vector4, out hit);
									if (flag12)
									{
										bool flag13 = CollidedWithTarget(hit_char, hit_char.GetCollider(), hit);
										if (flag13)
										{
											flag = true;
											break;
										}
									}
								}
							}
						}
					}
				}
				result = flag;
			}
			return result;
		}

		private static bool CollidedWithTarget(Character chr, Collider col, RaycastHit hit)
		{
			bool flag = hit.collider == chr.GetCollider();
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				Character character = null;
				hit.collider.gameObject.TryGetComponent(out character);
				bool flag2 = character != null;
				List<Component> list = new List<Component>();
				list.Clear();
				hit.collider.gameObject.GetComponents(list);
				bool flag3 = character == null;
				if (flag3)
				{
					character = (Character) hit.collider.GetComponentInParent(typeof(Character));
					flag2 = (character != null);
					bool flag4 = character == null;
					if (flag4)
					{
						character = hit.collider.GetComponentInChildren<Character>();
						flag2 = (character != null);
					}
				}
				bool flag5 = flag2 && character == chr;
				result = flag5;
			}
			return result;
		}

		public static void FindCrosshairObject(Player p, Vector3 originEyePoint, float maxDistance, out GameObject hover, out Character hoverCreature)
		{
			hover = null;
			hoverCreature = null;
			RaycastHit[] array = Physics.RaycastAll(GameCamera.instance.transform.position, GameCamera.instance.transform.forward, 50f, m_interactMask);
			Array.Sort(array, (RaycastHit x, RaycastHit y) => x.distance.CompareTo(y.distance));
			RaycastHit[] array2 = array;
			int num = 0;
			RaycastHit raycastHit;
			for (; ; )
			{
				bool flag = num >= array2.Length;
				if (flag)
				{
					break;
				}
				raycastHit = array2[num];
				bool flag2 = !raycastHit.collider.attachedRigidbody || !(raycastHit.collider.attachedRigidbody.gameObject == p.gameObject);
				if (flag2)
				{
					goto Block_4;
				}
				num++;
			}
			return;
			Block_4:
			bool flag3 = hoverCreature == null;
			if (flag3)
			{
				Character character = raycastHit.collider.attachedRigidbody ? raycastHit.collider.attachedRigidbody.GetComponent<Character>() : raycastHit.collider.GetComponent<Character>();
				bool flag4 = character != null;
				if (flag4)
				{
					hoverCreature = character;
				}
			}
			bool flag5 = Vector3.Distance(originEyePoint, raycastHit.point) < maxDistance;
			if (flag5)
			{
				bool flag6 = raycastHit.collider.GetComponent<Hoverable>() != null;
				if (flag6)
				{
					hover = raycastHit.collider.gameObject;
				}
				else
				{
					bool flag7 = raycastHit.collider.attachedRigidbody;
					if (flag7)
					{
						hover = raycastHit.collider.attachedRigidbody.gameObject;
					}
					else
					{
						hover = raycastHit.collider.gameObject;
					}
				}
			}
		}

		public static int GetAbilityUnlockLevelByIndex(int abilityIndex)
		{
			switch(abilityIndex)
            {
				case 1:
					return (int) GlobalConfigs.al_svr_ability1UnlockLevel;
				case 2:
					return (int) GlobalConfigs.al_svr_ability2UnlockLevel;
				case 3:
					return (int) GlobalConfigs.al_svr_ability3UnlockLevel;
				case 4:
					return (int) GlobalConfigs.al_svr_ability4UnlockLevel;
				default:
					return 0;
            }
		}

		public static bool IsPlayerAbilityUnlockedByIndex(Player player, int abilityIndex)
		{
			return GetPlayerClassLevel(player) >= GetAbilityUnlockLevelByIndex(abilityIndex);
		}

		public static bool IsPlayerAbilityUnlockedByLevel(Player player, float unlockLevel)
		{
			return GetPlayerClassLevel(player) >= (int) unlockLevel;
		}

		public static int GetPlayerClassLevel(Player player)
		{
			return (int) player.GetSkills().GetSkillLevel(AsgardLegacy.ClassLevelSkill);
		}

		public static void SendNotEnoughStaminaMessage(Player player, string abilityName, float staminaCost)
		{
			player.Message(
				MessageHud.MessageType.TopLeft, 
				string.Concat("Not enough stamina for", abilityName, "(", player.GetStamina().ToString("#.#"), "/",	staminaCost, ")"));
		}

		public static void SendNoWeaponEquippedMessage(Player player, string abilityName)
		{
			player.Message(
				MessageHud.MessageType.TopLeft, 
				string.Concat("You need a weapon equipped to cast ", abilityName));
		}

		public static void SendNotInDungeonMessage(Player player, string abilityName)
		{
			player.Message(
				MessageHud.MessageType.TopLeft,
				string.Concat("Cannot use ", abilityName, " in dungeon"));
		}

		public static void SendLevelUpMessage(Player player, int level, LevelUpUnlock unlock, string abilityName)
		{
			var message = "You've reached level " + level + " !";

			switch (unlock)
			{
				case LevelUpUnlock.Skill:
					message += "\nNew  skill unlocked : " + abilityName;
					break;
				case LevelUpUnlock.Passive:
					message += "\nNew  passive unlocked : " + abilityName;
					break;
			}

			player.Message(MessageHud.MessageType.Center, message);
		}

		public static float GetLinearValue(int currentLevel, float minValue, float maxValue, float unlockLevel)
        {
			var level = (int) unlockLevel;
			var ratio = ((float) (currentLevel - level)) /  (GlobalConfigs.al_svr_maxLevel - level);
			return minValue + ratio * (maxValue - minValue);
        }

		public static Player GetPlayerItemHolder(ItemDrop.ItemData item)
		{
			return Player.m_players.FirstOrDefault(player => player.IsItemEquiped(item));
		}

		public static void SetTimer()
		{
            al_timer = Time.time;
		}

		public static bool ReadyTime
		{
			get
			{
				return Time.time > 0.01f + al_timer;
			}
		}

		public static bool Ability1_Input_Down
		{
			get
			{
				return AsgardLegacy.Ability_Hotkeys[0].Value == ""
					? false
					: AsgardLegacy.Ability_Hotkey_Combos[0].Value == ""
						? Input.GetKeyDown(AsgardLegacy.Ability_Hotkeys[0].Value.ToLower()) || Input.GetButtonDown(AsgardLegacy.Ability_Hotkeys[0].Value.ToLower())
						: (Input.GetKeyDown(AsgardLegacy.Ability_Hotkeys[0].Value.ToLower()) && Input.GetKey(AsgardLegacy.Ability_Hotkey_Combos[0].Value.ToLower()))
							|| (Input.GetKey(AsgardLegacy.Ability_Hotkeys[0].Value.ToLower()) && Input.GetKeyDown(AsgardLegacy.Ability_Hotkey_Combos[0].Value.ToLower()))
							|| (Input.GetButtonDown(AsgardLegacy.Ability_Hotkeys[0].Value.ToLower()) && Input.GetButton(AsgardLegacy.Ability_Hotkey_Combos[0].Value.ToLower()))
							|| (Input.GetButton(AsgardLegacy.Ability_Hotkeys[0].Value.ToLower()) && Input.GetButtonDown(AsgardLegacy.Ability_Hotkey_Combos[0].Value.ToLower()));
			}
		}

		public static bool Ability2_Input_Down
		{
			get
			{
				return AsgardLegacy.Ability_Hotkeys[1].Value == ""
					? false
					: AsgardLegacy.Ability_Hotkey_Combos[1].Value == ""
						? Input.GetKeyDown(AsgardLegacy.Ability_Hotkeys[1].Value.ToLower()) || Input.GetButtonDown(AsgardLegacy.Ability_Hotkeys[1].Value.ToLower())
						: (Input.GetKeyDown(AsgardLegacy.Ability_Hotkeys[1].Value.ToLower()) && Input.GetKey(AsgardLegacy.Ability_Hotkey_Combos[1].Value.ToLower()))
							|| (Input.GetKey(AsgardLegacy.Ability_Hotkeys[1].Value.ToLower()) && Input.GetKeyDown(AsgardLegacy.Ability_Hotkey_Combos[1].Value.ToLower()))
							|| (Input.GetButtonDown(AsgardLegacy.Ability_Hotkeys[1].Value.ToLower()) && Input.GetButton(AsgardLegacy.Ability_Hotkey_Combos[1].Value.ToLower()))
							|| (Input.GetButton(AsgardLegacy.Ability_Hotkeys[1].Value.ToLower()) && Input.GetButtonDown(AsgardLegacy.Ability_Hotkey_Combos[1].Value.ToLower()));
			}
		}

		public static bool Ability3_Input_Down
		{
			get
			{
				return AsgardLegacy.Ability_Hotkeys[2].Value == ""
					? false
					: AsgardLegacy.Ability_Hotkey_Combos[2].Value == ""
						? Input.GetKeyDown(AsgardLegacy.Ability_Hotkeys[2].Value.ToLower()) || Input.GetButtonDown(AsgardLegacy.Ability_Hotkeys[2].Value.ToLower())
						: (Input.GetKeyDown(AsgardLegacy.Ability_Hotkeys[2].Value.ToLower()) && Input.GetKey(AsgardLegacy.Ability_Hotkey_Combos[2].Value.ToLower()))
							|| (Input.GetKey(AsgardLegacy.Ability_Hotkeys[2].Value.ToLower()) && Input.GetKeyDown(AsgardLegacy.Ability_Hotkey_Combos[2].Value.ToLower()))
							|| (Input.GetButtonDown(AsgardLegacy.Ability_Hotkeys[2].Value.ToLower()) && Input.GetButton(AsgardLegacy.Ability_Hotkey_Combos[2].Value.ToLower()))
							|| (Input.GetButton(AsgardLegacy.Ability_Hotkeys[2].Value.ToLower()) && Input.GetButtonDown(AsgardLegacy.Ability_Hotkey_Combos[2].Value.ToLower()));
			}
		}

		public static bool Ability4_Input_Down
		{
			get
			{
				return AsgardLegacy.Ability_Hotkeys[3].Value == ""
					? false
					: AsgardLegacy.Ability_Hotkey_Combos[3].Value == ""
						? Input.GetKeyDown(AsgardLegacy.Ability_Hotkeys[3].Value.ToLower()) || Input.GetButtonDown(AsgardLegacy.Ability_Hotkeys[3].Value.ToLower())
						: (Input.GetKeyDown(AsgardLegacy.Ability_Hotkeys[3].Value.ToLower()) && Input.GetKey(AsgardLegacy.Ability_Hotkey_Combos[3].Value.ToLower()))
							|| (Input.GetKey(AsgardLegacy.Ability_Hotkeys[3].Value.ToLower()) && Input.GetKeyDown(AsgardLegacy.Ability_Hotkey_Combos[3].Value.ToLower()))
							|| (Input.GetButtonDown(AsgardLegacy.Ability_Hotkeys[3].Value.ToLower()) && Input.GetButton(AsgardLegacy.Ability_Hotkey_Combos[3].Value.ToLower()))
							|| (Input.GetButton(AsgardLegacy.Ability_Hotkeys[3].Value.ToLower()) && Input.GetButtonDown(AsgardLegacy.Ability_Hotkey_Combos[3].Value.ToLower()));
			}
		}

		public static string ModID;

		public static string Folder;

		private static int m_interactMask = LayerMask.GetMask(new string[]
		{
			"item",
			"piece",
			"piece_nonsolid",
			"Default",
			"static_solid",
			"Default_small",
			"character",
			"character_net",
			"terrain",
			"vehicle"
		});

		private static int m_LOSMask = LayerMask.GetMask(new string[]
		{
			"piece",
			"piece_nonsolid",
			"Default",
			"static_solid",
			"Default_small",
			"terrain",
			"vehicle"
		});

		private static float al_timer;
	}
}
