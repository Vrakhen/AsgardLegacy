using HarmonyLib;
using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace AsgardLegacy
{
	class Class_Ranger_Patch
	{
		[HarmonyPatch(typeof(SEMan), nameof(SEMan.ModifyRunStaminaDrain))]
		public static class Ranger_ModifySprintStaminaUse_Patch
		{
			public static void Postfix(SEMan __instance, ref float drain)
			{
				if (!__instance.m_character.IsPlayer() || AsgardLegacy.al_player.al_class != AsgardLegacy.PlayerClass.Ranger)
					return;

				var player = __instance.m_character as Player;
				if (!Utility.IsPlayerAbilityUnlockedByLevel(player, GlobalConfigs.al_svr_passive2UnlockLevel))
					return;

				drain *= 1f - Utility.GetLinearValue(
					Utility.GetPlayerClassLevel(__instance.m_character as Player),
					GlobalConfigs_Ranger.al_svr_ranger_longstrider_runnningStaminaDrainReductionMin,
					GlobalConfigs_Ranger.al_svr_ranger_longstrider_runnningStaminaDrainReductionMax,
					GlobalConfigs.al_svr_passive2UnlockLevel);
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
				if (!Utility.IsPlayerAbilityUnlockedByLevel(player, GlobalConfigs.al_svr_passive2UnlockLevel))
					return;

				staminaUse *= 1f - Utility.GetLinearValue(
					Utility.GetPlayerClassLevel(player),
					GlobalConfigs_Ranger.al_svr_ranger_longstrider_jumpingStaminaDrainReductionMin,
					GlobalConfigs_Ranger.al_svr_ranger_longstrider_jumpingStaminaDrainReductionMax,
					GlobalConfigs.al_svr_passive2UnlockLevel);
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
