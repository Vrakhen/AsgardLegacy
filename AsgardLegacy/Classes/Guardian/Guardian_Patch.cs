using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace AsgardLegacy
{
	class Guardian_Patch
	{
		[HarmonyPatch(typeof(Character), nameof(Character.Damage), null)]
		public class Guardian_Damage_Patch
		{
			public static bool Prefix(Character __instance, ref HitData hit)
			{
				if (hit.GetAttacker() != null)
					return true;

				if (__instance != Player.m_localPlayer || !Guardian.shouldGuardianImpact)
					return true;

				hit.m_damage.m_damage *= 1f - Utility.GetLinearValue(Utility.GetPlayerClassLevel(__instance as Player),
							GlobalConfigs_Guardian.al_svr_guardian_shatterFall_fallDamageReductionMin,
							GlobalConfigs_Guardian.al_svr_guardian_shatterFall_fallDamageReductionMax,
							(int) GlobalConfigs.al_svr_ability1UnlockLevel);

				return true;
			}
		}

		[HarmonyPatch(typeof(Player), nameof(Player.GetBodyArmor), null)]
		public class Guardian_GetArmor_Patch
		{
			public static void Postfix(Player __instance, ref float __result)
			{
				if (!__instance.GetSEMan().HaveStatusEffect("SE_Guardian_Aegis"))
					return;

				__result *= Utility.GetLinearValue(Utility.GetPlayerClassLevel(__instance),
							GlobalConfigs_Guardian.al_svr_guardian_aegis_armorMultiplierMin,
							GlobalConfigs_Guardian.al_svr_guardian_aegis_armorMultiplierMax,
							(int) GlobalConfigs.al_svr_ability2UnlockLevel);
			}
		}

		[HarmonyPatch(typeof(Character), nameof(Character.UpdateGroundContact), null)]
		public class Guardian_ValidateHeight_Patch
		{
			public static void Postfix(Character __instance, float ___m_maxAirAltitude)
			{
				if (__instance != Player.m_localPlayer)
					return;

				if (!Guardian.inFlight)
					return;

				if (Mathf.Max(0f, ___m_maxAirAltitude - __instance.transform.position.y) > 1f)
					Guardian.shouldGuardianImpact = true;
			}
		}

		[HarmonyPatch(typeof(Character), nameof(Character.ResetGroundContact), null)]
		public class Guardian_GroundContact_Patch
		{
			public static void Postfix(Character __instance, float ___m_maxAirAltitude)
			{
				if (__instance != Player.m_localPlayer || !Guardian.shouldGuardianImpact)
					return;

				var altitude = Mathf.Max(0f, ___m_maxAirAltitude - __instance.transform.position.y);
				Guardian.shouldGuardianImpact = false;
				Guardian.ImpactEffect(Player.m_localPlayer, altitude);
				Guardian.inFlight = false;
			}
		}

		[HarmonyPatch(typeof(Humanoid), nameof(Humanoid.BlockAttack), null)]
		public class Guardian_BlockAttack_Patch
		{
			private static float blockableDamage;

			public static bool Prefix(Humanoid __instance, HitData hit, float ___m_blockTimer)
			{
				if (__instance != Player.m_localPlayer)
					return true;

				var player = Player.m_localPlayer;
				var currentBlocker = __instance.GetCurrentBlocker();
				if (currentBlocker == null)
					return false;

				var parry = currentBlocker.m_shared.m_timedBlockBonus > 1f && ___m_blockTimer != -1f && ___m_blockTimer < 0.25f;
				if(parry && !player.GetSEMan().HaveStatusEffect("SE_Guardian_Bulwark_CD")
					&& Utility.IsPlayerAbilityUnlockedByLevel(player, GlobalConfigs.al_svr_passive2UnlockLevel))
                {
					var bulwark = (SE_Guardian_Bulwark) ScriptableObject.CreateInstance(typeof(SE_Guardian_Bulwark));
					var bulwarkCD = (SE_Guardian_Bulwark_CD) ScriptableObject.CreateInstance(typeof(SE_Guardian_Bulwark_CD));
					bulwark.m_ttl =	GlobalConfigs_Guardian.al_svr_guardian_bulwark_shieldCounterDuration;
					bulwark.m_staminaModifier = Utility.GetLinearValue(
							Utility.GetPlayerClassLevel(player),
							GlobalConfigs_Guardian.al_svr_guardian_bulwark_shieldCounterStaminaRegenMin,
							GlobalConfigs_Guardian.al_svr_guardian_bulwark_shieldCounterStaminaRegenMax,
							GlobalConfigs.al_svr_passive2UnlockLevel)
						* player.m_maxStamina;
					bulwark.m_interval = GlobalConfigs_Guardian.al_svr_guardian_bulwark_shieldCounterTickInterval;
					bulwarkCD.m_ttl = GlobalConfigs_Guardian.al_svr_guardian_bulwark_shieldCounterCooldown;
					player.GetSEMan().AddStatusEffect(bulwark, true);
					player.GetSEMan().AddStatusEffect(bulwarkCD, true);

					player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainPassiveTrigger);
				}

				if(player.GetSEMan().HaveStatusEffect("SE_Guardian_Retribution"))
					blockableDamage = hit.GetTotalBlockableDamage();

				return true;
			}
			public static void Postfix(Humanoid __instance, HitData hit)
			{
				if (__instance != Player.m_localPlayer || !__instance.GetSEMan().HaveStatusEffect("SE_Guardian_Retribution"))
					return;

				var blocked = blockableDamage - hit.GetTotalBlockableDamage();
				Guardian.retributionDamageBlocked += blocked;
			}
		}
		
		[HarmonyPatch(typeof(Player), nameof(Player.GetTotalFoodValue))]
		public static class Guardian_GetTotalFoodValue_Patch
		{
			public static void Postfix(Player __instance, ref float hp, ref float stamina)
			{
				if (AsgardLegacy.al_player.al_class != AsgardLegacy.PlayerClass.Guardian || __instance != Player.m_localPlayer)
					return;

				if (!Utility.IsPlayerAbilityUnlockedByLevel(__instance, GlobalConfigs.al_svr_passive1UnlockLevel))
					return;

				hp *= Utility.GetLinearValue(
					Utility.GetPlayerClassLevel(__instance),
					GlobalConfigs_Guardian.al_svr_guardian_forceOfNature_hpBonusMin,
					GlobalConfigs_Guardian.al_svr_guardian_forceOfNature_hpBonusMax,
					GlobalConfigs.al_svr_passive1UnlockLevel);

				stamina *= Utility.GetLinearValue(
					Utility.GetPlayerClassLevel(__instance),
					GlobalConfigs_Guardian.al_svr_guardian_forceOfNature_staminaBonusMin,
					GlobalConfigs_Guardian.al_svr_guardian_forceOfNature_staminaBonusMax,
					GlobalConfigs.al_svr_passive1UnlockLevel);
			}
		}
		
		[HarmonyPatch(typeof(ItemDrop.ItemData), nameof(ItemDrop.ItemData.GetBaseBlockPower), typeof(int))]
		public static class Guardian_GetBaseBlockPower_Patch
		{
			public static void Postfix(ItemDrop.ItemData __instance, ref float __result)
			{
				if (AsgardLegacy.al_player.al_class != AsgardLegacy.PlayerClass.Guardian)
					return;

				var player = Utility.GetPlayerItemHolder(__instance);

				if (player == null)
					return;
				
				if (!Utility.IsPlayerAbilityUnlockedByLevel(player, GlobalConfigs.al_svr_passive2UnlockLevel) || !__instance.m_shared.m_name.ToLower().Contains("shield"))
					return;

				var totalBlockPowerMod = Utility.GetLinearValue(
					Utility.GetPlayerClassLevel(player),
					GlobalConfigs_Guardian.al_svr_guardian_bulwark_baseBlockPowerBonusMin,
					GlobalConfigs_Guardian.al_svr_guardian_bulwark_baseBlockPowerBonusMax,
					GlobalConfigs.al_svr_passive2UnlockLevel);
				
				if (__instance.m_shared.m_timedBlockBonus < 1f)
					totalBlockPowerMod += Utility.GetLinearValue(
					Utility.GetPlayerClassLevel(player),
					GlobalConfigs_Guardian.al_svr_guardian_bulwark_towerShieldBlockPowerMin,
					GlobalConfigs_Guardian.al_svr_guardian_bulwark_towerShieldBlockPowerMax,
					GlobalConfigs.al_svr_passive2UnlockLevel);

				__result *= Mathf.Round(totalBlockPowerMod);
			}
		}

		[HarmonyPatch(typeof(Humanoid), nameof(Humanoid.StartAttack))]
		public static class Guardian_SecondaryAttack_Patch
		{
			public static void Postfix(Humanoid __instance, bool secondaryAttack)
			{
				if (!secondaryAttack)
					return;

				if (AsgardLegacy.al_player.al_class != AsgardLegacy.PlayerClass.Guardian || __instance != Player.m_localPlayer)
					return;

				var player = Player.m_localPlayer;
				if (player.GetSEMan().HaveStatusEffect("SE_Guardian_WarCry_CD") || !Utility.IsPlayerAbilityUnlockedByLevel(player, GlobalConfigs.al_svr_passive1UnlockLevel))
					return;

				var se_Guardian_WarCry_CD = (SE_Guardian_WarCry_CD) ScriptableObject.CreateInstance(typeof(SE_Guardian_WarCry_CD));
				se_Guardian_WarCry_CD.m_ttl = GlobalConfigs_Guardian.al_svr_guardian_warCry_cooldown;
				player.GetSEMan().AddStatusEffect(se_Guardian_WarCry_CD, true);

				var characters = new List<Character>();
				Character.GetCharactersInRange(player.GetCenterPoint(), GlobalConfigs_Guardian.al_svr_guardian_warCry_radius, characters);
				foreach (var character in characters)
				{
					if (character.GetBaseAI() == null || !(character.GetBaseAI() is MonsterAI) || !character.GetBaseAI().IsEnemey(player))
						continue;

					var monsterAI = character.GetBaseAI() as MonsterAI;

					if (monsterAI == null || monsterAI.GetTargetCreature() == player)
						continue;

					Traverse.Create(monsterAI).Field("m_alerted").SetValue(true);
					Traverse.Create(monsterAI).Field("m_targetCreature").SetValue(__instance);
				}
			}
		}
		
		[HarmonyPatch(typeof(Character), "CheckDeath")]
		public class Guardian_OnDeath_Patch
		{
			public static bool Prefix(Character __instance)
			{
				if (__instance.IsDead() || __instance.GetHealth() > 0f || AsgardLegacy.al_player == null)
					return true;

				var player = __instance as Player;
				if (player != Player.m_localPlayer || AsgardLegacy.al_player.al_class != AsgardLegacy.PlayerClass.Guardian)
					return true;

				if (player.GetSEMan().HaveStatusEffect("SE_Guardian_UndyingWill_CD"))
					return true;

				var se_Guardian_UndyingWill_CD = (SE_Guardian_UndyingWill_CD) ScriptableObject.CreateInstance(typeof(SE_Guardian_UndyingWill_CD));
				se_Guardian_UndyingWill_CD.m_ttl = GlobalConfigs_Guardian.al_svr_guardian_undyingWill_cooldown;
				player.GetSEMan().AddStatusEffect(se_Guardian_UndyingWill_CD, true);

				player.SetHealth(GlobalConfigs_Guardian.al_svr_guardian_undyingWill_hpPercent * player.GetMaxHealth());

				return false;
			}
		}

	}
}