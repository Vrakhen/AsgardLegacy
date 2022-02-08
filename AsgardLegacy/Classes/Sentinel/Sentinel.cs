using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AsgardLegacy
{
	public class Sentinel
	{
		public static void Process_Input(Player player, float altitude)
		{
			/*var random = new Random();
			var vector = default(Vector3);
			var ability3_Input_Down = Utility.Ability3_Input_Down;
			if (ability3_Input_Down)
			{
				bool flag = !player.GetSEMan().HaveStatusEffect("SE_VL_Ability3_CD");
				if (flag)
				{
					TribesOfValheim.shouldUseForsakenPower = false;
					bool flag2 = player.GetStamina() >= Utility.GetRootCost && !TribesOfValheim.isChanneling;
					if (flag2)
					{
						TribesOfValheim.isChanneling = true;
						StatusEffect statusEffect = (SE_Ability3_CD) ScriptableObject.CreateInstance(typeof(SE_Ability3_CD));
						statusEffect.m_ttl = VL_Utility.GetRootCooldownTime;
						player.GetSEMan().AddStatusEffect(statusEffect, false);
						player.UseStamina(VL_Utility.GetRootCost);
						((ZSyncAnimation) typeof(Player).GetField("m_zanim", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Player.m_localPlayer)).SetTrigger("gpower");
						((ZSyncAnimation) typeof(Player).GetField("m_zanim", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Player.m_localPlayer)).SetSpeed(0.3f);
						float level = player.GetSkills().GetSkillList().FirstOrDefault((Skills.Skill x) => x.m_info == ValheimLegends.ConjurationSkillDef).m_level;
						Class_Druid.rootCount = 0;
						Class_Druid.rootCountTrigger = 32 - Mathf.RoundToInt(0.12f * level);
						Vector3 vector2 = player.transform.right * 2.5f;
						bool flag3 = Random.Range(0f, 1f) < 0.5f;
						if (flag3)
						{
							vector2 *= -1f;
						}
						vector = player.transform.position + player.transform.up * 3f + player.GetLookDir() * 2f + vector2;
						GameObject prefab = ZNetScene.instance.GetPrefab("gdking_root_projectile");
						Class_Druid.GO_Root = Object.Instantiate<GameObject>(prefab, new Vector3(vector.x, vector.y, vector.z), Quaternion.identity);
						Class_Druid.P_Root = Class_Druid.GO_Root.GetComponent<Projectile>();
						Class_Druid.P_Root.name = "Root";
						Class_Druid.P_Root.m_respawnItemOnHit = false;
						Class_Druid.P_Root.m_spawnOnHit = null;
						Class_Druid.P_Root.m_ttl = 35f;
						Class_Druid.P_Root.m_gravity = 0f;
						Class_Druid.P_Root.m_rayRadius = 0.1f;
						Traverse.Create(Class_Druid.P_Root).Field("m_skill").SetValue(ValheimLegends.ConjurationSkill);
						Class_Druid.P_Root.transform.localRotation = Quaternion.LookRotation(player.GetLookDir());
						Class_Druid.GO_Root.transform.localScale = Vector3.one * 1.5f;
						player.RaiseSkill(ValheimLegends.ConjurationSkill, VL_Utility.GetRootSkillGain);
					}
					else
					{
						player.Message(1, string.Concat(new object[]
						{
							"Not enough stamina to channel Root: (",
							player.GetStamina().ToString("#.#"),
							"/",
							VL_Utility.GetRootCost,
							")"
						}), 0, null);
					}
				}
				else
				{
					player.Message(1, "Ability not ready", 0, null);
				}
			}
			else
			{
				bool flag4 = VL_Utility.Ability3_Input_Pressed && player.GetStamina() > VL_Utility.GetRootCostPerUpdate && ValheimLegends.isChanneling && Mathf.Max(0f, altitude - player.transform.position.y) <= 2f;
				if (flag4)
				{
					Class_Druid.rootCount++;
					VL_Utility.SetTimer();
					player.UseStamina(VL_Utility.GetRootCostPerUpdate);
					ValheimLegends.isChanneling = true;
					bool flag5 = Class_Druid.rootCount >= Class_Druid.rootCountTrigger;
					if (flag5)
					{
						player.RaiseSkill(ValheimLegends.ConjurationSkill, 0.06f);
						float level2 = player.GetSkills().GetSkillList().FirstOrDefault((Skills.Skill x) => x.m_info == ValheimLegends.ConjurationSkillDef).m_level;
						Class_Druid.rootCount = 0;
						bool flag6 = Class_Druid.GO_Root != null && Class_Druid.GO_Root.transform != null;
						if (flag6)
						{
							RaycastHit raycastHit = default(RaycastHit);
							Vector3 position = player.transform.position;
							Vector3 vector3 = (!Physics.Raycast(player.GetEyePoint(), player.GetLookDir(), ref raycastHit, float.PositiveInfinity, Class_Druid.Script_Layermask) || !raycastHit.collider) ? (position + player.GetLookDir() * 1000f) : raycastHit.point;
							HitData hitData = new HitData();
							hitData.m_damage.m_pierce = Random.Range(10f + 0.6f * level2, 15f + 1.2f * level2) * VL_GlobalConfigs.g_DamageModifer * VL_GlobalConfigs.c_druidVines;
							hitData.m_pushForce = 2f;
							Vector3 vector4 = Vector3.MoveTowards(Class_Druid.GO_Root.transform.position, vector3, 1f);
							bool flag7 = Class_Druid.P_Root != null && Class_Druid.P_Root.name == "Root";
							if (flag7)
							{
								Class_Druid.P_Root.Setup(player, (vector4 - Class_Druid.GO_Root.transform.position) * 75f, -1f, hitData, null);
								Traverse.Create(Class_Druid.P_Root).Field("m_skill").SetValue(ValheimLegends.ConjurationSkill);
							}
						}
						Class_Druid.GO_Root = null;
						Vector3 vector5 = player.transform.right * 2.5f;
						bool flag8 = Random.Range(0f, 1f) < 0.5f;
						if (flag8)
						{
							vector5 *= -1f;
						}
						vector = player.transform.position + player.transform.up * 3f + player.GetLookDir() * 2f + vector5;
						GameObject prefab2 = ZNetScene.instance.GetPrefab("gdking_root_projectile");
						Class_Druid.GO_Root = Object.Instantiate<GameObject>(prefab2, new Vector3(vector.x, vector.y, vector.z), Quaternion.identity);
						Class_Druid.P_Root = Class_Druid.GO_Root.GetComponent<Projectile>();
						Class_Druid.P_Root.name = "Root";
						Class_Druid.P_Root.m_respawnItemOnHit = false;
						Class_Druid.P_Root.m_spawnOnHit = null;
						Class_Druid.P_Root.m_ttl = (float) (Class_Druid.rootCountTrigger + 1);
						Class_Druid.P_Root.m_gravity = 0f;
						Class_Druid.P_Root.m_rayRadius = 0.1f;
						Traverse.Create(Class_Druid.P_Root).Field("m_skill").SetValue(ValheimLegends.ConjurationSkill);
						Class_Druid.P_Root.transform.localRotation = Quaternion.LookRotation(player.GetLookDir());
						Class_Druid.GO_Root.transform.localScale = Vector3.one * 1.5f;
					}
				}
				else
				{
					bool flag9 = ((VL_Utility.Ability3_Input_Up || player.GetStamina() <= VL_Utility.GetRootCostPerUpdate) && ValheimLegends.isChanneling) || Mathf.Max(0f, altitude - player.transform.position.y) > 2f;
					if (flag9)
					{
						bool flag10 = Class_Druid.GO_Root != null && Class_Druid.GO_Root.transform != null;
						if (flag10)
						{
							RaycastHit raycastHit2 = default(RaycastHit);
							Vector3 position2 = player.transform.position;
							Vector3 vector6 = (!Physics.Raycast(player.GetEyePoint(), player.GetLookDir(), ref raycastHit2, float.PositiveInfinity, Class_Druid.Script_Layermask) || !raycastHit2.collider) ? (position2 + player.GetLookDir() * 1000f) : raycastHit2.point;
							HitData hitData2 = new HitData();
							hitData2.m_damage.m_pierce = 10f;
							hitData2.m_pushForce = 10f;
							hitData2.SetAttacker(player);
							Vector3 vector7 = Vector3.MoveTowards(Class_Druid.GO_Root.transform.position, vector6, 1f);
							Class_Druid.P_Root.Setup(player, (vector7 - Class_Druid.GO_Root.transform.position) * 65f, -1f, hitData2, null);
							Traverse.Create(Class_Druid.P_Root).Field("m_skill").SetValue(ValheimLegends.ConjurationSkill);
						}
						Class_Druid.GO_Root = null;
						ValheimLegends.isChanneling = false;
					}
					else
					{
						bool ability2_Input_Down = VL_Utility.Ability2_Input_Down;
						if (ability2_Input_Down)
						{
							bool flag11 = !player.GetSEMan().HaveStatusEffect("SE_VL_Ability2_CD");
							if (flag11)
							{
								bool flag12 = player.GetStamina() >= VL_Utility.GetDefenderCost;
								if (flag12)
								{
									Vector3 lookDir = player.GetLookDir();
									lookDir.y = 0f;
									player.transform.rotation = Quaternion.LookRotation(lookDir);
									ValheimLegends.shouldUseGuardianPower = false;
									StatusEffect statusEffect2 = (SE_Ability2_CD) ScriptableObject.CreateInstance(typeof(SE_Ability2_CD));
									statusEffect2.m_ttl = VL_Utility.GetDefenderCooldownTime;
									player.GetSEMan().AddStatusEffect(statusEffect2, false);
									player.UseStamina(VL_Utility.GetDefenderCost);
									float level3 = player.GetSkills().GetSkillList().FirstOrDefault((Skills.Skill x) => x.m_info == ValheimLegends.ConjurationSkillDef).m_level;
									((ZSyncAnimation) typeof(Player).GetField("m_zanim", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Player.m_localPlayer)).SetTrigger("gpower");
									Object.Instantiate<GameObject>(ZNetScene.instance.GetPrefab("vfx_WishbonePing"), player.transform.position, Quaternion.identity);
									GameObject prefab3 = ZNetScene.instance.GetPrefab("TentaRoot");
									CharacterTimedDestruction component = prefab3.GetComponent<CharacterTimedDestruction>();
									bool flag13 = component != null;
									if (flag13)
									{
										component.m_timeoutMin = 24f + 0.3f * level3;
										component.m_timeoutMax = component.m_timeoutMin;
										component.m_triggerOnAwake = true;
										component.enabled = true;
									}
									List<Vector3> list = new List<Vector3>();
									list.Clear();
									vector = player.transform.position + player.GetLookDir() * 5f + player.transform.right * 5f;
									list.Add(vector);
									vector = player.transform.position + player.GetLookDir() * 5f + player.transform.right * 5f * -1f;
									list.Add(vector);
									vector = player.transform.position + player.GetLookDir() * 5f * -1f;
									list.Add(vector);
									for (int i = 0; i < list.Count; i++)
									{
										Class_Druid.GO_RootDefender = Object.Instantiate<GameObject>(prefab3, list[i], Quaternion.identity);
										Character component2 = Class_Druid.GO_RootDefender.GetComponent<Character>();
										bool flag14 = component2 != null;
										if (flag14)
										{
											SE_RootsBuff se_RootsBuff = (SE_RootsBuff) ScriptableObject.CreateInstance(typeof(SE_RootsBuff));
											se_RootsBuff.m_ttl = SE_RootsBuff.m_baseTTL;
											se_RootsBuff.damageModifier = 0.5f + 0.015f * level3 * VL_GlobalConfigs.g_DamageModifer * VL_GlobalConfigs.c_druidDefenders;
											se_RootsBuff.staminaRegen = 0.5f + 0.05f * level3;
											se_RootsBuff.summoner = player;
											se_RootsBuff.centerPoint = player.transform.position;
											component2.GetSEMan().AddStatusEffect(se_RootsBuff, false);
											component2.SetMaxHealth(30f + 6f * level3);
											component2.transform.localScale = (0.75f + 0.005f * level3) * Vector3.one;
											component2.m_faction = 0;
											component2.SetTamed(true);
										}
										Object.Instantiate<GameObject>(ZNetScene.instance.GetPrefab("vfx_Potion_stamina_medium"), component2.transform.position, Quaternion.identity);
									}
									GameObject prefab4 = ZNetScene.instance.GetPrefab("VL_Deathsquit");
									CharacterTimedDestruction component3 = prefab4.GetComponent<CharacterTimedDestruction>();
									bool flag15 = component3 != null;
									if (flag15)
									{
										component3.m_timeoutMin = 24f + 0.3f * level3;
										component3.m_timeoutMax = component3.m_timeoutMin;
										component3.m_triggerOnAwake = true;
										component3.enabled = true;
									}
									int num = 2 + Mathf.RoundToInt(0.05f * level3);
									for (int j = 0; j < num; j++)
									{
										vector = player.transform.position + player.transform.up * 4f + (player.GetLookDir() * Random.Range(-(5f + 0.1f * level3), 5f + 0.1f * level3) + player.transform.right * Random.Range(-(5f + 0.1f * level3), 5f + 0.1f * level3));
										GameObject gameObject = Object.Instantiate<GameObject>(prefab4, vector, Quaternion.identity);
										Character component4 = gameObject.GetComponent<Character>();
										component4.m_name = "Drusquito";
										bool flag16 = component4 != null;
										if (flag16)
										{
											SE_Companion se_Companion = (SE_Companion) ScriptableObject.CreateInstance(typeof(SE_Companion));
											se_Companion.m_ttl = 60f;
											se_Companion.damageModifier = 0.05f + 0.0075f * level3 * VL_GlobalConfigs.g_DamageModifer * VL_GlobalConfigs.c_druidDefenders;
											se_Companion.summoner = player;
											component4.GetSEMan().AddStatusEffect(se_Companion, false);
											component4.transform.localScale = (0.4f + 0.005f * level3) * Vector3.one;
											component4.m_faction = 0;
											component4.SetTamed(true);
										}
										Object.Instantiate<GameObject>(ZNetScene.instance.GetPrefab("fx_float_hitwater"), component4.transform.position, Quaternion.identity);
									}
									player.RaiseSkill(ValheimLegends.ConjurationSkill, VL_Utility.GetDefenderSkillGain);
								}
								else
								{
									player.Message(1, string.Concat(new object[]
									{
										"Not enough stamina to summon root defenders: (",
										player.GetStamina().ToString("#.#"),
										"/",
										VL_Utility.GetDefenderCost,
										")"
									}), 0, null);
								}
							}
							else
							{
								player.Message(1, "Ability not ready", 0, null);
							}
						}
						else
						{
							bool ability1_Input_Down = VL_Utility.Ability1_Input_Down;
							if (ability1_Input_Down)
							{
								bool flag17 = !player.GetSEMan().HaveStatusEffect("SE_VL_Ability1_CD");
								if (flag17)
								{
									bool flag18 = player.GetStamina() >= VL_Utility.GetRegenerationCost;
									if (flag18)
									{
										StatusEffect statusEffect3 = (SE_Ability1_CD) ScriptableObject.CreateInstance(typeof(SE_Ability1_CD));
										statusEffect3.m_ttl = VL_Utility.GetRegenerationCooldownTime;
										player.GetSEMan().AddStatusEffect(statusEffect3, false);
										player.UseStamina(VL_Utility.GetRegenerationCost);
										float level4 = player.GetSkills().GetSkillList().FirstOrDefault((Skills.Skill x) => x.m_info == ValheimLegends.AlterationSkillDef).m_level;
										player.StartEmote("cheer", true);
										Class_Druid.GO_CastFX = Object.Instantiate<GameObject>(ZNetScene.instance.GetPrefab("fx_guardstone_permitted_add"), player.GetCenterPoint(), Quaternion.identity);
										Class_Druid.GO_CastFX = Object.Instantiate<GameObject>(ZNetScene.instance.GetPrefab("vfx_WishbonePing"), player.transform.position, Quaternion.identity);
										SE_Regeneration se_Regeneration = (SE_Regeneration) ScriptableObject.CreateInstance(typeof(SE_Regeneration));
										se_Regeneration.m_ttl = SE_Regeneration.m_baseTTL;
										se_Regeneration.m_icon = ZNetScene.instance.GetPrefab("TrophyGreydwarfShaman").GetComponent<ItemDrop>().m_itemData.GetIcon();
										se_Regeneration.m_HealAmount = 0.5f + 0.4f * level4 * VL_GlobalConfigs.g_DamageModifer * VL_GlobalConfigs.c_druidRegen;
										se_Regeneration.doOnce = false;
										List<Character> list2 = new List<Character>();
										list2.Clear();
										Character.GetCharactersInRange(player.GetCenterPoint(), 30f + 0.2f * level4, list2);
										foreach (Character character in list2)
										{
											bool flag19 = !BaseAI.IsEnemy(player, character);
											if (flag19)
											{
												bool flag20 = character == Player.m_localPlayer;
												if (flag20)
												{
													character.GetSEMan().AddStatusEffect(se_Regeneration, true);
												}
												else
												{
													bool flag21 = character.IsPlayer();
													if (flag21)
													{
														character.GetSEMan().AddStatusEffect(se_Regeneration.name, true);
													}
													else
													{
														character.GetSEMan().AddStatusEffect(se_Regeneration, true);
													}
												}
											}
										}
										player.RaiseSkill(ValheimLegends.AlterationSkill, VL_Utility.GetRegenerationSkillGain);
									}
									else
									{
										player.Message(1, string.Concat(new object[]
										{
											"Not enough stamina to for Regeneration: (",
											player.GetStamina().ToString("#.#"),
											"/",
											VL_Utility.GetRegenerationCost,
											")"
										}), 0, null);
									}
								}
								else
								{
									player.Message(1, "Ability not ready", 0, null);
								}
							}
							else
							{
								ValheimLegends.isChanneling = false;
							}
						}
					}
				}
			}*/
		}

		private static int Script_Layermask = LayerMask.GetMask(new string[]
		{
			"Default",
			"static_solid",
			"Default_small",
			"piece_nonsolid",
			"terrain",
			"vehicle",
			"piece",
			"viewblock",
			"character",
			"character_net",
			"character_ghost"
		});

		private static GameObject GO_CastFX;
		private static GameObject GO_Root;
		private static Projectile P_Root;
		private static StatusEffect SE_Root;
		private static GameObject GO_RootDefender;
		private static int rootCount;
		private static int rootCountTrigger;
	}
}
