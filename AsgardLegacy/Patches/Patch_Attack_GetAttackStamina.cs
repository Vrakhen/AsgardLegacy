using HarmonyLib;
using UnityEngine;

namespace AsgardLegacy.Patches
{
	[HarmonyPatch(typeof(Attack), nameof(Attack.GetAttackStamina))]
	public static class AttackStamina_Patch
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
			var playerLevel = Utility.GetPlayerClassLevel(player);
			if (player != Player.m_localPlayer || playerLevel < GlobalConfigs.al_svr_passive1UnlockLevel)
				return;

			if (AsgardLegacy.al_player.al_class == AsgardLegacy.PlayerClass.Berserker
				&& __instance.m_weapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon)
			{
				__result *= Utility.GetLinearValue(
					playerLevel,
					GlobalConfigs_Berserker.al_svr_berserker_twoHandedExpert_staminaBonusMin,
					GlobalConfigs_Berserker.al_svr_berserker_twoHandedExpert_staminaBonusMax,
					GlobalConfigs.al_svr_passive1UnlockLevel);
			}
			else if(AsgardLegacy.al_player.al_class == AsgardLegacy.PlayerClass.Sentinel
				&& __instance.m_weapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.OneHandedWeapon)
			{
				__result *= Utility.GetLinearValue(
					playerLevel,
					GlobalConfigs_Sentinel.al_svr_sentinel_oneHandedMaster_staminaBonusMin,
					GlobalConfigs_Sentinel.al_svr_sentinel_oneHandedMaster_staminaBonusMax,
					GlobalConfigs.al_svr_passive1UnlockLevel);
			}
		}
	}
}
