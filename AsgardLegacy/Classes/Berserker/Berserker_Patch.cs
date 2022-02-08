using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace AsgardLegacy
{
	class Berserker_Patch
	{
		[HarmonyPatch(typeof(Character), nameof(Character.Damage), null)]
		public class Berserker_Damage_Patch
		{
			public static bool Prefix(Character __instance, ref HitData hit)
			{
				if (hit.GetAttacker() == null)
					return true;

				if (hit.GetAttacker().IsPlayer())
				{
					var player = hit.GetAttacker() as Player;
					if (player != Player.m_localPlayer)
						return true;

					if (player.GetSEMan().HaveStatusEffect("SE_Berserker_RagingStorm"))
						player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainAoeMultipleHit);

					if (player.GetSEMan().HaveStatusEffect("SE_Berserker_Frenzy"))
						player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainAoeMultipleHit);

					if (AsgardLegacy.al_player.al_class == AsgardLegacy.PlayerClass.Berserker)
					{
						var totalDamageModif = 1f;
						if (Utility.IsPlayerAbilityUnlockedByLevel(player, GlobalConfigs.al_svr_passive1UnlockLevel)
							&& player.m_rightItem.IsWeapon()
							&& player.m_rightItem.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon)
							totalDamageModif += Utility.GetLinearValue(
								Utility.GetPlayerClassLevel(player),
								GlobalConfigs_Berserker.al_svr_berserker_twoHandedExpert_damageBonusMin,
								GlobalConfigs_Berserker.al_svr_berserker_twoHandedExpert_damageBonusMin,
								GlobalConfigs.al_svr_passive1UnlockLevel);

						if (Utility.IsPlayerAbilityUnlockedByLevel(player, GlobalConfigs.al_svr_passive2UnlockLevel))
							totalDamageModif += Utility.GetLinearValue(
								Utility.GetPlayerClassLevel(player),
								GlobalConfigs_Berserker.al_svr_berserker_reckless_bonusDamageMin,
								GlobalConfigs_Berserker.al_svr_berserker_reckless_bonusDamageMin,
								GlobalConfigs.al_svr_passive2UnlockLevel) * (1f - player.GetHealthPercentage());

						hit.m_damage.Modify(totalDamageModif);
					}
				}
				else if (__instance.IsPlayer())
				{
					var player = __instance as Player;
					if (player != Player.m_localPlayer)
						return true;

					if (AsgardLegacy.al_player.al_class == AsgardLegacy.PlayerClass.Berserker)
					{
						if (player.GetSEMan().HaveStatusEffect("SE_Berserker_DenyPain"))
							return false;

						if (Utility.IsPlayerAbilityUnlockedByLevel(player, GlobalConfigs.al_svr_passive2UnlockLevel))
							hit.m_damage.Modify(1f - Utility.GetLinearValue(
								Utility.GetPlayerClassLevel(player),
								GlobalConfigs_Berserker.al_svr_berserker_reckless_bonusMitigationMin,
								GlobalConfigs_Berserker.al_svr_berserker_reckless_bonusMitigationMin,
								GlobalConfigs.al_svr_passive2UnlockLevel) * (1f - player.GetHealthPercentage()));

						if (Utility.IsPlayerAbilityUnlockedByLevel(player, GlobalConfigs.al_svr_passive3UnlockLevel)
							&& !player.GetSEMan().HaveStatusEffect("SE_Berserker_AdrenalineRush_CD")
							&& player.GetHealthPercentage() <= Utility.GetLinearValue(
								Utility.GetPlayerClassLevel(player),
								GlobalConfigs_Berserker.al_svr_berserker_adrenalineRush_hpPercentMin,
								GlobalConfigs_Berserker.al_svr_berserker_adrenalineRush_hpPercentMax,
								GlobalConfigs.al_svr_passive3UnlockLevel))
						{
							var se_AdrenalineRush = (SE_Berserker_AdrenalineRush) ScriptableObject.CreateInstance(typeof(SE_Berserker_AdrenalineRush));
							var se_AdrenalineRush_CD = (SE_Berserker_AdrenalineRush_CD) ScriptableObject.CreateInstance(typeof(SE_Berserker_AdrenalineRush_CD));
							se_AdrenalineRush.m_ttl = GlobalConfigs_Berserker.al_svr_berserker_adrenalineRush_duration;
							se_AdrenalineRush_CD.m_ttl = GlobalConfigs_Berserker.al_svr_berserker_adrenalineRush_cooldown;
							player.GetSEMan().AddStatusEffect(se_AdrenalineRush, true);
							player.GetSEMan().AddStatusEffect(se_AdrenalineRush_CD, true);
						}
					}
				}

				return true;
			}
		}

		[HarmonyPatch(typeof(CharacterAnimEvent), nameof(CharacterAnimEvent.FixedUpdate))]
		public static class Berserker_ModifyAttackSpeed_Patch
		{
			private static void Prefix(Character ___m_character, ref Animator ___m_animator)
			{
				if (!___m_character.IsPlayer() || !___m_character.InAttack())
					return;

				var player = ___m_character as Player;
				var currentAttack = player.m_currentAttack;
				if (currentAttack == null)
					return;

				if (!player.GetSEMan().HaveStatusEffect("SE_Berserker_Frenzy"))
					return;

				___m_animator.speed *= Utility.GetLinearValue(
					Utility.GetPlayerClassLevel(player),
					GlobalConfigs_Berserker.al_svr_berserker_frenzy_attackSpeedMultiplierMin,
					GlobalConfigs_Berserker.al_svr_berserker_frenzy_attackSpeedMultiplierMax,
					GlobalConfigs.al_svr_ability4UnlockLevel);
			}
		}

		[HarmonyPatch(typeof(Attack), nameof(Attack.GetAttackStamina))]
		public static class Berserker_AttackStamina_Patch
		{
			private static bool Prefix(Character ___m_character, ref float __result)
			{
				if (!___m_character.IsPlayer() || !___m_character.GetSEMan().HaveStatusEffect("SE_Berserker_Frenzy"))
					return true;

				__result = 0f;
				return false;
			}

			private static void Postfix(Attack __instance, Character ___m_character, ref float __result)
			{
				if (!___m_character.IsPlayer() || ___m_character.GetSEMan().HaveStatusEffect("SE_Berserker_Frenzy"))
					return;

				var player = ___m_character as Player;
				if (player != Player.m_localPlayer
					|| AsgardLegacy.al_player.al_class != AsgardLegacy.PlayerClass.Berserker
					|| !Utility.IsPlayerAbilityUnlockedByLevel(player, GlobalConfigs.al_svr_passive1UnlockLevel)
					|| __instance.m_weapon.m_shared.m_itemType != ItemDrop.ItemData.ItemType.TwoHandedWeapon)
					return;

				__result *= Utility.GetLinearValue(
					Utility.GetPlayerClassLevel(player),
					GlobalConfigs_Berserker.al_svr_berserker_twoHandedExpert_staminaBonusMin,
					GlobalConfigs_Berserker.al_svr_berserker_twoHandedExpert_staminaBonusMax,
					GlobalConfigs.al_svr_passive1UnlockLevel);
			}
		}

		[HarmonyPatch(typeof(Player), nameof(Player.UseStamina))]
		public static class Berserker_UseStamina_Patch
		{
			private static bool Prefix(Player __instance, ref float v)
			{
				if (!__instance.GetSEMan().HaveStatusEffect("SE_Berserker_AdrenalineRush"))
					return true;

				v *= 1f - Utility.GetLinearValue(
					Utility.GetPlayerClassLevel(__instance),
					GlobalConfigs_Berserker.al_svr_berserker_adrenalineRush_bonusStaminaMin,
					GlobalConfigs_Berserker.al_svr_berserker_adrenalineRush_bonusStaminaMax,
					GlobalConfigs.al_svr_passive3UnlockLevel);

				return true;
			}
		}

		[HarmonyPatch(typeof(Humanoid), nameof(Humanoid.StartAttack))]
		public static class Berserker_SecondaryAttack_Patch
		{
			public static void Postfix(Humanoid __instance, bool secondaryAttack)
			{
				if (!secondaryAttack)
					return;

				if (AsgardLegacy.al_player.al_class != AsgardLegacy.PlayerClass.Berserker || __instance != Player.m_localPlayer)
					return;

				var player = Player.m_localPlayer;
				if (player.GetSEMan().HaveStatusEffect("SE_Berserker_DenyPain_CD"))
					return;

				if (!Utility.IsPlayerAbilityUnlockedByLevel(player, GlobalConfigs.al_svr_passive4UnlockLevel))
					return;

				var se_Berserker_DenyPain = (SE_Berserker_DenyPain) ScriptableObject.CreateInstance(typeof(SE_Berserker_DenyPain));
				var se_Berserker_DenyPain_CD = (SE_Berserker_DenyPain_CD) ScriptableObject.CreateInstance(typeof(SE_Berserker_DenyPain_CD));
				se_Berserker_DenyPain.m_ttl = GlobalConfigs_Berserker.al_svr_berserker_denyPain_duration;
				se_Berserker_DenyPain_CD.m_ttl = GlobalConfigs_Berserker.al_svr_berserker_denyPain_cooldown;
				player.GetSEMan().AddStatusEffect(se_Berserker_DenyPain, true);
				player.GetSEMan().AddStatusEffect(se_Berserker_DenyPain_CD, true);
			}
		}
	}
}