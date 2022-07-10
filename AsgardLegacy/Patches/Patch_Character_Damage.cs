using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace AsgardLegacy
{
    class Patch_Character_Damage
    {
		[HarmonyPatch(typeof(Character), nameof(Character.Damage), null)]
		public class Patch_Damage
		{
			public static bool Prefix(Character __instance, ref HitData hit)
			{
				if (hit.GetAttacker() == null)
					return true;

				var hitData = new HitData();

				if (hit.GetAttacker().IsPlayer())
				{
					var player = hit.GetAttacker() as Player;
					if (player != Player.m_localPlayer)
						return true;

					var seMan = player.GetSEMan();
					var playerLevel = Utility.GetPlayerClassLevel(player);

					if(seMan.HaveStatusEffect("SE_ALRune"))
                    {
						var seRune = (SE_ALRune) seMan.GetStatusEffect("SE_ALRune");
						var runeDamageBonus = seRune.RuneDamageBonus;
						var runeDamageMalus = seRune.RuneDamageMalus;
						switch (seRune.RuneType)
                        {
							case HitData.DamageType.Physical:
								hitData.m_damage.m_blunt += runeDamageBonus * hit.m_damage.m_blunt;
								hitData.m_damage.m_pierce += runeDamageBonus * hit.m_damage.m_pierce;
								hitData.m_damage.m_slash += runeDamageBonus * hit.m_damage.m_slash;

								break;
							case HitData.DamageType.Fire:
								hitData.m_damage.m_fire += runeDamageBonus * hit.m_damage.GetTotalDamage();
								hitData.m_damage.m_frost -= runeDamageMalus * hit.m_damage.m_frost;
								hitData.m_damage.m_lightning -= runeDamageMalus * hit.m_damage.m_lightning;
								hitData.m_damage.m_poison -= runeDamageMalus * hit.m_damage.m_poison;

								break;
							case HitData.DamageType.Frost:
								hitData.m_damage.m_fire -= runeDamageMalus * hit.m_damage.m_fire;
								hitData.m_damage.m_frost += runeDamageBonus * hit.m_damage.GetTotalDamage();
								hitData.m_damage.m_lightning -= runeDamageMalus * hit.m_damage.m_lightning;
								hitData.m_damage.m_poison -= runeDamageMalus * hit.m_damage.m_poison;

								break;
							case HitData.DamageType.Lightning:
								hitData.m_damage.m_fire -= runeDamageMalus * hit.m_damage.m_fire;
								hitData.m_damage.m_frost -= runeDamageMalus * hit.m_damage.m_frost;
								hitData.m_damage.m_lightning += runeDamageBonus * hit.m_damage.GetTotalDamage();
								hitData.m_damage.m_poison -= runeDamageMalus * hit.m_damage.m_poison;

								break;
							case HitData.DamageType.Poison:
								hitData.m_damage.m_fire -= runeDamageMalus * hit.m_damage.GetTotalDamage();
								hitData.m_damage.m_frost -= runeDamageMalus * hit.m_damage.m_frost;
								hitData.m_damage.m_lightning -= runeDamageMalus * hit.m_damage.m_lightning;
								hitData.m_damage.m_poison += runeDamageBonus * hit.m_damage.GetTotalDamage();

								break;
                        }
                    }

					switch (AsgardLegacy.al_player.al_class)
					{
						case AsgardLegacy.PlayerClass.Berserker:
							{
								if (seMan.HaveStatusEffect("SE_Berserker_RagingStorm")
									|| seMan.HaveStatusEffect("SE_Berserker_Frenzy"))
									player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainAoeMultipleHit);

								if (playerLevel >= GlobalConfigs.al_svr_passive1UnlockLevel
									&& player.m_rightItem != null
									&& player.m_rightItem.IsWeapon()
									&& player.m_rightItem.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon)
								{
									var tmpDamage = hit.m_damage.Clone();
									tmpDamage.Modify(Utility.GetLinearValue(
										playerLevel,
										GlobalConfigs_Berserker.al_svr_berserker_twoHandedExpert_damageBonusMin,
										GlobalConfigs_Berserker.al_svr_berserker_twoHandedExpert_damageBonusMax,
										GlobalConfigs.al_svr_passive1UnlockLevel));
									hitData.m_damage.Add(tmpDamage);
								}

								if (playerLevel >= GlobalConfigs.al_svr_passive2UnlockLevel)
								{
									var mult = Utility.GetLinearValue(
										playerLevel,
										GlobalConfigs_Berserker.al_svr_berserker_reckless_bonusDamageMin,
										GlobalConfigs_Berserker.al_svr_berserker_reckless_bonusDamageMax,
										GlobalConfigs.al_svr_passive2UnlockLevel) * (1f - player.GetHealthPercentage());
									hitData.m_damage.m_blunt += mult * hit.m_damage.m_blunt;
									hitData.m_damage.m_pierce += mult * hit.m_damage.m_pierce;
									hitData.m_damage.m_slash += mult * hit.m_damage.m_slash;
								}

								break;
							}
						case AsgardLegacy.PlayerClass.Ranger:
							{
								/*if (seMan.HaveStatusEffect("SE_Ranger_ExplosiveArrow")
									&& player.m_leftItem != null
									&& player.m_leftItem.IsWeapon()
									&& player.m_leftItem.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow)
									return false;*/

								if (playerLevel >= GlobalConfigs.al_svr_passive1UnlockLevel
									&& player.m_leftItem != null
									&& player.m_leftItem.IsWeapon()
									&& player.m_leftItem.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow)
								{
									var tmpDamage = hit.m_damage.Clone();
									tmpDamage.Modify(Utility.GetLinearValue(
										playerLevel,
										GlobalConfigs_Ranger.al_svr_ranger_bowSpecialist_damageMultiplierMin,
										GlobalConfigs_Ranger.al_svr_ranger_bowSpecialist_damageMultiplierMax,
										GlobalConfigs.al_svr_passive1UnlockLevel));
									hitData.m_damage.Add(tmpDamage);
								}

								if (playerLevel >= GlobalConfigs.al_svr_passive4UnlockLevel)
								{
									var mult = Utility.GetLinearValue(
										playerLevel,
										GlobalConfigs_Ranger.al_svr_ranger_goForTheEyes_damageMultiplierMin,
										GlobalConfigs_Ranger.al_svr_ranger_goForTheEyes_damageMultiplierMax,
										GlobalConfigs.al_svr_passive4UnlockLevel);
									hitData.m_backstabBonus *= mult;

									if (__instance.IsStaggering())
										hitData.ApplyModifier(mult);
								}

								if (playerLevel >= GlobalConfigs.al_svr_passive5UnlockLevel)
								{
									var elementalChance = Utility.GetLinearValue(
										playerLevel,
										GlobalConfigs_Ranger.al_svr_ranger_elementalTouch_percentChanceMin,
										GlobalConfigs_Ranger.al_svr_ranger_elementalTouch_percentChanceMax,
										GlobalConfigs.al_svr_passive5UnlockLevel);
									if (Random.Range(0, 100) < elementalChance)
									{
										var elemDamage = hit.GetTotalDamage() * Utility.GetLinearValue(
											playerLevel,
											GlobalConfigs_Ranger.al_svr_ranger_elementalTouch_damagePercentMin,
											GlobalConfigs_Ranger.al_svr_ranger_elementalTouch_damagePercentMax,
											GlobalConfigs.al_svr_passive5UnlockLevel);
										var element = Random.Range(0, 3);
										switch(element)
                                        {
											case 0:
												hitData.m_damage.m_fire += elemDamage;
												break;
											case 1:
												hitData.m_damage.m_frost += elemDamage;
												break;
											case 2:
												hitData.m_damage.m_lightning += elemDamage;
												break;
											case 3:
												hitData.m_damage.m_poison += elemDamage;
												break;
                                        }
									}
								}

								if (playerLevel >= GlobalConfigs.al_svr_passive6UnlockLevel
									&& player.m_leftItem != null
									&& player.m_leftItem.IsWeapon()
									&& player.m_leftItem.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow)
								{
									if (Random.Range(0, 100) < GlobalConfigs_Ranger.al_svr_ranger_ammoSaver_regainChancePercent)
									{
										var ammo = player.GetAmmoItem();
										if (ammo != null)
											ammo.m_stack += 1;
									}
								}

								if (seMan.HaveStatusEffect("SE_Ranger_ShadowStalk"))
									seMan.RemoveStatusEffect("SE_Ranger_ShadowStalk", true);

								if (seMan.HaveStatusEffect("SE_Ranger_RapidFire"))
								{
									var mult = Utility.GetLinearValue(
										playerLevel,
										GlobalConfigs_Ranger.al_svr_ranger_rapidFire_damageReductionMin,
										GlobalConfigs_Ranger.al_svr_ranger_rapidFire_damageReductionMax,
										GlobalConfigs.al_svr_ability2UnlockLevel);
									var tmpDamage = hit.m_damage.Clone();
									tmpDamage.Modify(mult);
									hitData.m_damage.Add(tmpDamage, -1);

									player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainAoeMultipleHit);
								}

								if (seMan.HaveStatusEffect("SE_Ranger_RangerMark") && BaseAI.IsEnemy(__instance, player))
								{
									var se_Ranger_RangerMarked = (SE_Ranger_RangerMarked) ScriptableObject.CreateInstance(typeof(SE_Ranger_RangerMarked));
									se_Ranger_RangerMarked.m_ttl = Utility.GetLinearValue(
										playerLevel,
										GlobalConfigs_Ranger.al_svr_ranger_rangerMark_durationMin,
										GlobalConfigs_Ranger.al_svr_ranger_rangerMark_durationMax,
										GlobalConfigs.al_svr_ability4UnlockLevel);
									se_Ranger_RangerMarked.m_damageMultiplier = Utility.GetLinearValue(
										playerLevel,
										GlobalConfigs_Ranger.al_svr_ranger_rangerMark_damageMultiplierMin,
										GlobalConfigs_Ranger.al_svr_ranger_rangerMark_damageMultiplierMax,
										GlobalConfigs.al_svr_ability4UnlockLevel);
									__instance.GetSEMan().AddStatusEffect(se_Ranger_RangerMarked, true);

									Object.Instantiate(ZNetScene.instance.GetPrefab("fx_backstab"), __instance.transform.position, Quaternion.identity);
									Object.Instantiate(ZNetScene.instance.GetPrefab("sfx_ProjectileHit"), __instance.transform.position, Quaternion.identity);

									seMan.RemoveStatusEffect("SE_Ranger_RangerMark", true);
									player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainAoeBaseHit);
								}

								break;
							}
						case AsgardLegacy.PlayerClass.Sentinel:
							{
								if (playerLevel >= GlobalConfigs.al_svr_passive1UnlockLevel
									&& player.m_rightItem != null
									&& player.m_rightItem.IsWeapon()
									&& player.m_rightItem.m_shared.m_itemType == ItemDrop.ItemData.ItemType.OneHandedWeapon)
								{
									var tmpDamage = hit.m_damage.Clone();
									tmpDamage.Modify(Utility.GetLinearValue(
										playerLevel,
										GlobalConfigs_Sentinel.al_svr_sentinel_oneHandedMaster_damageBonusMin,
										GlobalConfigs_Sentinel.al_svr_sentinel_oneHandedMaster_damageBonusMax,
										GlobalConfigs.al_svr_passive1UnlockLevel));
									hitData.m_damage.Add(tmpDamage);
								}

								if (seMan.HaveStatusEffect("SE_Sentinel_RejuvenatingStrike") && BaseAI.IsEnemy(__instance, player))
								{
									Object.Instantiate(ZNetScene.instance.GetPrefab("sfx_eikthyr_hit"), __instance.transform.position, Quaternion.identity);

									hitData.m_damage.m_lightning += hit.m_damage.GetTotalPhysicalDamage()
										* Utility.GetLinearValue(
											playerLevel,
											GlobalConfigs_Sentinel.al_svr_sentinel_rejuvenatingStrike_damageMultiplierMin,
											GlobalConfigs_Sentinel.al_svr_sentinel_rejuvenatingStrike_damageMultiplierMax,
											GlobalConfigs.al_svr_ability1UnlockLevel);

									Sentinel.RejuvImpactEffect(player, __instance.GetInstanceID(), hitData);

									seMan.RemoveStatusEffect("SE_Sentinel_RejuvenatingStrike", true);
									player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainAoeBaseHit);
								}

								break;
							}
					}

					if (__instance.GetSEMan().HaveStatusEffect("SE_Ranger_RangerMarked"))
					{
						var mark = (SE_Ranger_RangerMarked) __instance.GetSEMan().GetStatusEffect("SE_Ranger_RangerMarked");
						var tmpDamage = hit.m_damage.Clone();
						tmpDamage.Modify(mark.m_damageMultiplier);
						hitData.m_damage.Add(tmpDamage);
					}
				}
				else if (__instance.IsPlayer())
				{
					var player = __instance as Player;
					if (player != Player.m_localPlayer)
						return true;

					var seMan = player.GetSEMan();
					var playerLevel = Utility.GetPlayerClassLevel(player);

					if (seMan.HaveStatusEffect("SE_ALRune"))
					{
						var seRune = (SE_ALRune) seMan.GetStatusEffect("SE_ALRune");
						var runeResistBonus = seRune.RuneResistanceBonus;
						var runeResistMalus = seRune.RuneResistanceMalus;
						switch (seRune.RuneType)
						{
							case HitData.DamageType.Fire:
								hitData.m_damage.m_fire -= runeResistBonus * hit.m_damage.m_fire;
								hitData.m_damage.m_frost += runeResistMalus * hit.m_damage.m_frost;
								hitData.m_damage.m_lightning += runeResistMalus * hit.m_damage.m_lightning;
								hitData.m_damage.m_poison += runeResistMalus * hit.m_damage.m_poison;

								break;
							case HitData.DamageType.Frost:
								hitData.m_damage.m_fire += runeResistMalus * hit.m_damage.m_fire;
								hitData.m_damage.m_frost -= runeResistBonus * hit.m_damage.m_frost;
								hitData.m_damage.m_lightning += runeResistMalus * hit.m_damage.m_lightning;
								hitData.m_damage.m_poison += runeResistMalus * hit.m_damage.m_poison;

								break;
							case HitData.DamageType.Lightning:
								hitData.m_damage.m_fire += runeResistMalus * hit.m_damage.m_fire;
								hitData.m_damage.m_frost += runeResistMalus * hit.m_damage.m_frost;
								hitData.m_damage.m_lightning -= runeResistBonus * hit.m_damage.m_lightning;
								hitData.m_damage.m_poison += runeResistMalus * hit.m_damage.m_poison;

								break;
							case HitData.DamageType.Poison:
								hitData.m_damage.m_fire += runeResistMalus * hit.m_damage.m_fire;
								hitData.m_damage.m_frost += runeResistMalus * hit.m_damage.m_frost;
								hitData.m_damage.m_lightning += runeResistMalus * hit.m_damage.m_lightning;
								hitData.m_damage.m_poison -= runeResistBonus * hit.m_damage.m_poison;

								break;
						}
					}

					switch (AsgardLegacy.al_player.al_class)
                    {
						case AsgardLegacy.PlayerClass.Berserker:
							{
								if (playerLevel >= GlobalConfigs.al_svr_passive5UnlockLevel)
								{
									var mult = Utility.GetLinearValue(
										playerLevel,
										GlobalConfigs_Berserker.al_svr_berserker_rebuke_reflectPercentMin,
										GlobalConfigs_Berserker.al_svr_berserker_rebuke_reflectPercentMax,
										GlobalConfigs.al_svr_passive5UnlockLevel);

									var attacker = hit.GetAttacker();
									var reflectHit = new HitData()
									{
										m_attacker = __instance.GetZDOID(),
										m_dir = hit.m_dir * -1,
										m_point = attacker.transform.localPosition,
										m_damage = { m_pierce = hit.GetTotalDamage() * mult }
									};

									attacker.Damage(reflectHit);
								}

								if (seMan.HaveStatusEffect("SE_Berserker_DenyPain"))
									return false;

								if (playerLevel >= GlobalConfigs.al_svr_passive2UnlockLevel)
                                {
									var mult = Utility.GetLinearValue(
										playerLevel,
										GlobalConfigs_Berserker.al_svr_berserker_reckless_bonusMitigationMin,
										GlobalConfigs_Berserker.al_svr_berserker_reckless_bonusMitigationMax,
										GlobalConfigs.al_svr_passive2UnlockLevel) * (1f - player.GetHealthPercentage());
									hitData.m_damage.m_blunt -= mult * hit.m_damage.m_blunt;
									hitData.m_damage.m_pierce -= mult * hit.m_damage.m_pierce;
									hitData.m_damage.m_slash -= mult * hit.m_damage.m_slash;
								}

								if (playerLevel >= GlobalConfigs.al_svr_passive4UnlockLevel
									&& !seMan.HaveStatusEffect("SE_Berserker_AdrenalineRush_CD")
									&& player.GetHealthPercentage() <= Utility.GetLinearValue(
										playerLevel,
										GlobalConfigs_Berserker.al_svr_berserker_adrenalineRush_hpPercentMin,
										GlobalConfigs_Berserker.al_svr_berserker_adrenalineRush_hpPercentMax,
										GlobalConfigs.al_svr_passive4UnlockLevel))
								{
									var se_AdrenalineRush = (SE_Berserker_AdrenalineRush) ScriptableObject.CreateInstance(typeof(SE_Berserker_AdrenalineRush));
									var se_AdrenalineRush_CD = (SE_Berserker_AdrenalineRush_CD) ScriptableObject.CreateInstance(typeof(SE_Berserker_AdrenalineRush_CD));
									se_AdrenalineRush.m_ttl = GlobalConfigs_Berserker.al_svr_berserker_adrenalineRush_duration;
									se_AdrenalineRush_CD.m_ttl = GlobalConfigs_Berserker.al_svr_berserker_adrenalineRush_cooldown;
									seMan.AddStatusEffect(se_AdrenalineRush, true);
									seMan.AddStatusEffect(se_AdrenalineRush_CD, true);
								}

								break;
                            }
						case AsgardLegacy.PlayerClass.Guardian:
                            {
								if(Guardian.shouldGuardianImpact)
								{
									hitData.m_damage.m_damage -= hit.m_damage.m_damage * Utility.GetLinearValue(
												playerLevel,
												GlobalConfigs_Guardian.al_svr_guardian_shatterFall_fallDamageReductionMin,
												GlobalConfigs_Guardian.al_svr_guardian_shatterFall_fallDamageReductionMax,
												(int) GlobalConfigs.al_svr_ability1UnlockLevel);
								}

								break;
                            }
						case AsgardLegacy.PlayerClass.Sentinel:
							{
								if (playerLevel >= GlobalConfigs.al_svr_passive1UnlockLevel)
								{
									var modifier = Utility.GetLinearValue(
											playerLevel,
											GlobalConfigs_Sentinel.al_svr_sentinel_dwarvenFortitude_elementalResistMin,
											GlobalConfigs_Sentinel.al_svr_sentinel_dwarvenFortitude_elementalResistMax,
											GlobalConfigs.al_svr_passive1UnlockLevel);
									hitData.m_damage.m_fire -= modifier * hit.m_damage.m_fire;
									hitData.m_damage.m_frost -= modifier * hit.m_damage.m_frost;
									hitData.m_damage.m_poison -= modifier * hit.m_damage.m_poison;
									hitData.m_damage.m_lightning -= modifier * hit.m_damage.m_lightning;
								}

								if (playerLevel >= GlobalConfigs.al_svr_passive5UnlockLevel
									&& !seMan.HaveStatusEffect("SE_Sentinel_VengefulWave_CD")
									&& player.GetHealthPercentage() <= Utility.GetLinearValue(
										playerLevel,
										GlobalConfigs_Sentinel.al_svr_sentinel_vengefulWave_hpPercentMin,
										GlobalConfigs_Sentinel.al_svr_sentinel_vengefulWave_hpPercentMax,
										GlobalConfigs.al_svr_passive5UnlockLevel))
								{
									var se_VengefulWave_CD = (SE_Sentinel_VengefulWave_CD) ScriptableObject.CreateInstance(typeof(SE_Sentinel_VengefulWave_CD));
									se_VengefulWave_CD.m_ttl = GlobalConfigs_Sentinel.al_svr_sentinel_vengefulWave_cooldown;
									seMan.AddStatusEffect(se_VengefulWave_CD, true);

									var characters = new List<Character>();
									Character.GetCharactersInRange(player.GetCenterPoint(), GlobalConfigs_Sentinel.al_svr_sentinel_vengefulWave_radius, characters);
									foreach (var character in characters)
									{
										if (character.GetBaseAI() == null || character.IsBoss() || !(character.GetBaseAI() is MonsterAI) || !character.GetBaseAI().IsEnemey(player))
											continue;

										character.Stagger(character.transform.position - player.transform.position);
									}

								}

								break;
                            }
					}
				}

				hit.m_damage.Add(hitData.m_damage);

				return true;
			}
		}
	}
}
