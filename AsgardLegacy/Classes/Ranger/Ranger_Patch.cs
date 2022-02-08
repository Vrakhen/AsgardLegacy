using HarmonyLib;
using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace AsgardLegacy
{
	class Ranger_Patch
	{
		[HarmonyPatch(typeof(SEMan), nameof(SEMan.ModifyRunStaminaDrain))]
		public static class Ranger_ModifySprintStaminaUse_Patch
		{
			public static void Postfix(SEMan __instance, ref float drain)
			{
				if (!__instance.m_character.IsPlayer() || AsgardLegacy.al_player.al_class != AsgardLegacy.PlayerClass.Ranger)
					return;

				var player = __instance.m_character as Player;
				if (!Utility.IsPlayerAbilityUnlockedByLevel(player, GlobalConfigs.al_svr_passive3UnlockLevel))
					return;

				drain *= 1f - Utility.GetLinearValue(
					Utility.GetPlayerClassLevel(__instance.m_character as Player),
					GlobalConfigs_Ranger.al_svr_ranger_longstrider_runnningStaminaDrainReductionMin,
					GlobalConfigs_Ranger.al_svr_ranger_longstrider_runnningStaminaDrainReductionMax,
					GlobalConfigs.al_svr_ability4UnlockLevel);
			}
		}

		[HarmonyPatch(typeof(SEMan), nameof(SEMan.ModifyJumpStaminaUsage))]
		public static class Ranger_ModifyJumpStaminaUse_Patch
		{
			public static void Postfix(SEMan __instance, ref float staminaUse)
			{
				if (!__instance.m_character.IsPlayer() || AsgardLegacy.al_player.al_class != AsgardLegacy.PlayerClass.Ranger)
					return;

				var player = __instance.m_character as Player;
				if (!Utility.IsPlayerAbilityUnlockedByLevel(player, GlobalConfigs.al_svr_passive3UnlockLevel))
					return;

				staminaUse *= 1f - Utility.GetLinearValue(
					Utility.GetPlayerClassLevel(player),
					GlobalConfigs_Ranger.al_svr_ranger_longstrider_jumpingStaminaDrainReductionMin,
					GlobalConfigs_Ranger.al_svr_ranger_longstrider_jumpingStaminaDrainReductionMax,
					GlobalConfigs.al_svr_passive3UnlockLevel);
			}
		}

		[HarmonyPatch(typeof(Projectile), nameof(Projectile.OnHit))]
		public class Ranger_ExplodingArrowHit_Patch
		{
			private static void Postfix(bool __state, Vector3 hitPoint, Projectile __instance)
			{
				if (!__instance.m_didHit)
					return;

				if (!__instance.m_owner.IsPlayer() || !__instance.m_owner.GetSEMan().HaveStatusEffect("SE_Ranger_ExplosiveArrow"))
					return;

				var player = __instance.m_owner as Player;
				Ranger.ExplosiveArrow(player, hitPoint, __instance);
				__instance.m_owner.GetSEMan().RemoveStatusEffect("SE_Ranger_ExplosiveArrow");
			}
		}

		[HarmonyPatch(typeof(Attack), nameof(Attack.FireProjectileBurst), null)]
		public class Ranger_ProjectileAttack_Patch
		{
			public static bool Prefix(Humanoid ___m_character, ref float ___m_attackDrawPercentage, ref float ___m_projectileVel, ref float ___m_damageMultiplier)
			{
				if (!___m_character.IsPlayer() || AsgardLegacy.al_player.al_class != AsgardLegacy.PlayerClass.Ranger)
					return true;

				var player = ___m_character as Player;

				if (Utility.IsPlayerAbilityUnlockedByLevel(player, GlobalConfigs.al_svr_passive1UnlockLevel))
                {
					___m_projectileVel *= Utility.GetLinearValue(
						Utility.GetPlayerClassLevel(player),
						GlobalConfigs_Ranger.al_svr_ranger_bowSpecialist_velocityMultiplierMin,
						GlobalConfigs_Ranger.al_svr_ranger_bowSpecialist_velocityMultiplierMax,
						GlobalConfigs.al_svr_passive1UnlockLevel);
					___m_damageMultiplier = Utility.GetLinearValue(
						Utility.GetPlayerClassLevel(player),
						GlobalConfigs_Ranger.al_svr_ranger_bowSpecialist_damageMultiplierMin,
						GlobalConfigs_Ranger.al_svr_ranger_bowSpecialist_damageMultiplierMax,
						GlobalConfigs.al_svr_passive1UnlockLevel);
				}

				if (___m_character.GetSEMan().HaveStatusEffect("SE_Ranger_RapidFire"))
					___m_attackDrawPercentage = Mathf.Max(___m_attackDrawPercentage, Utility.GetLinearValue(
						Utility.GetPlayerClassLevel(player),
						GlobalConfigs_Ranger.al_svr_ranger_rapidFire_drawPercentMin,
						GlobalConfigs_Ranger.al_svr_ranger_rapidFire_drawPercentMax,
						GlobalConfigs.al_svr_ability2UnlockLevel));

				return true;
			}
		}

		[HarmonyPatch(typeof(CharacterAnimEvent), nameof(CharacterAnimEvent.FixedUpdate))]
		public static class Ranger_ModifyAttackSpeed_Patch
		{
			private static void Prefix(Character ___m_character, ref Animator ___m_animator)
			{
				if (!___m_character.IsPlayer() || !___m_character.InAttack())
					return;

				var player = ___m_character as Player;
				var currentAttack = player.m_currentAttack;
				if (currentAttack == null)
					return;

				if (player.GetSEMan().HaveStatusEffect("SE_Ranger_RapidFire"))
				{
					if (currentAttack.GetWeapon().m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow)
						___m_animator.speed *= 2f;
					else if (currentAttack.GetWeapon().m_shared.m_name.Contains("sword") || currentAttack.GetWeapon().m_shared.m_name.Contains("knife"))
						___m_animator.speed *= Utility.GetLinearValue(
							Utility.GetPlayerClassLevel(player),
							GlobalConfigs_Ranger.al_svr_ranger_rapidFire_attackSpeedMultiplierMin,
							GlobalConfigs_Ranger.al_svr_ranger_rapidFire_attackSpeedMultiplierMax,
							GlobalConfigs.al_svr_ability2UnlockLevel);
				}
			}
		}

		[HarmonyPatch(typeof(Character), nameof(Character.Damage), null)]
		public class Ranger_Damage_Patch
		{
			public static bool Prefix(Character __instance, ref HitData hit)
			{
				var attacker = hit.GetAttacker();
				if (attacker == null)
					return true;

				if (attacker.GetSEMan().HaveStatusEffect("SE_Ranger_RangerMark") && BaseAI.IsEnemy(__instance, attacker))
				{
					var se_Ranger_RangerMarked = (SE_Ranger_RangerMarked) ScriptableObject.CreateInstance(typeof(SE_Ranger_RangerMarked));
					__instance.GetSEMan().AddStatusEffect(se_Ranger_RangerMarked, true);
					Object.Instantiate(ZNetScene.instance.GetPrefab("fx_backstab"), __instance.transform.position, Quaternion.identity);
					Object.Instantiate(ZNetScene.instance.GetPrefab("sfx_ProjectileHit"), __instance.transform.position, Quaternion.identity);

					attacker.GetSEMan().RemoveStatusEffect("SE_Ranger_RangerMark", true);
					attacker.RaiseSkill(AsgardLegacy.ClassLevelSkill, 0.5f);
				}

				if (!attacker.IsPlayer())
					return true;

				var player = attacker as Player;

				if (__instance.GetSEMan().HaveStatusEffect("SE_Ranger_RangerMarked"))
					hit.m_damage.Modify(Utility.GetLinearValue(
						Utility.GetPlayerClassLevel(player),
						GlobalConfigs_Ranger.al_svr_ranger_rangerMark_damageMultiplierMin,
						GlobalConfigs_Ranger.al_svr_ranger_rangerMark_damageMultiplierMax,
						GlobalConfigs.al_svr_ability4UnlockLevel));

				if (player.GetSEMan().HaveStatusEffect("SE_Ranger_ShadowStalk"))
					player.GetSEMan().RemoveStatusEffect("SE_Ranger_ShadowStalk", true);

				if (player.GetSEMan().HaveStatusEffect("SE_Ranger_RapidFire"))
				{
					hit.m_damage.Modify(1 - Utility.GetLinearValue(
						Utility.GetPlayerClassLevel(player),
						GlobalConfigs_Ranger.al_svr_ranger_rapidFire_damageReductionMin,
						GlobalConfigs_Ranger.al_svr_ranger_rapidFire_damageReductionMax,
						GlobalConfigs.al_svr_ability2UnlockLevel));

					player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainAoeMultipleHit);
				}

				if (AsgardLegacy.al_player.al_class == AsgardLegacy.PlayerClass.Ranger 
					&& player.GetCurrentWeapon().m_shared.m_name.Contains("bow")
					&& Utility.IsPlayerAbilityUnlockedByLevel(player, GlobalConfigs.al_svr_passive4UnlockLevel))
				{
					if (Random.Range(0, 100) < GlobalConfigs_Ranger.al_svr_ranger_ammoSaver_regainChancePercent)
					{
						var ammo = player.GetAmmoItem();
						if (ammo != null)
							ammo.m_stack += 1;
					}
				}

				return true;
			}
		}

		[HarmonyPatch(typeof(Player), nameof(Player.UpdateDodge), null)]
		public class Ranger_DodgePatch_Patch
		{
			public static void Postfix(Player __instance, float ___m_queuedDodgeTimer)
			{
				if (___m_queuedDodgeTimer >= -0.5f || ___m_queuedDodgeTimer <= -0.55f
					|| AsgardLegacy.al_player.al_name != __instance.GetPlayerName() || AsgardLegacy.al_player.al_class != AsgardLegacy.PlayerClass.Ranger)
					return;

				if (__instance.GetSEMan().GetStatusEffect("SE_Ranger_SpeedBurst_CD")
					|| !Utility.IsPlayerAbilityUnlockedByLevel(__instance, GlobalConfigs.al_svr_passive2UnlockLevel))
					return;

				var speedBurst = (SE_Ranger_SpeedBurst) ScriptableObject.CreateInstance(typeof(SE_Ranger_SpeedBurst));
				var speedBurstCD = (SE_Ranger_SpeedBurst_CD) ScriptableObject.CreateInstance(typeof(SE_Ranger_SpeedBurst_CD));
				speedBurst.m_ttl = Utility.GetLinearValue(
						Utility.GetPlayerClassLevel(__instance),
						GlobalConfigs_Ranger.al_svr_ranger_speedBurst_durationMin,
						GlobalConfigs_Ranger.al_svr_ranger_speedBurst_durationMax,
						GlobalConfigs.al_svr_passive2UnlockLevel);
				speedBurst.m_speedModifier = Utility.GetLinearValue(
						Utility.GetPlayerClassLevel(__instance),
						GlobalConfigs_Ranger.al_svr_ranger_speedBurst_speedMultiplierMin,
						GlobalConfigs_Ranger.al_svr_ranger_speedBurst_speedMultiplierMin,
						GlobalConfigs.al_svr_passive2UnlockLevel);
				speedBurstCD.m_ttl = GlobalConfigs_Ranger.al_svr_ranger_speedBurst_cooldown;
				__instance.GetSEMan().AddStatusEffect(speedBurst, true);
				__instance.GetSEMan().AddStatusEffect(speedBurstCD, true);
				Object.Instantiate(ZNetScene.instance.GetPrefab("vfx_HitSparks"), __instance.GetCenterPoint(), Quaternion.identity);
				Object.Instantiate(ZNetScene.instance.GetPrefab("sfx_WishbonePing_med"), __instance.GetCenterPoint(), Quaternion.identity);

				__instance.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainPassiveTrigger);
			}
		}

		[HarmonyPatch(typeof(BaseAI), nameof(BaseAI.CanSenseTarget), new Type[] { typeof(Character) })]
		public class Ranger_CanSee_Shadow_Patch
		{
			public static bool Prefix(Character target, ref bool __result)
			{
				if (target == null)
					return true;

				var player = target as Player;
				if (player != null && player.GetSEMan().HaveStatusEffect("SE_Ranger_ShadowStalk") && player.IsCrouching())
				{
					__result = false;
					return false;
				}

				return true;
			}
		}
	}
}
