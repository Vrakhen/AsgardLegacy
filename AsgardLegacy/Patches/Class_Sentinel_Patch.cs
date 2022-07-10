using HarmonyLib;

namespace AsgardLegacy
{
	class Class_Sentinel_Patch
	{
		[HarmonyPatch(typeof(Player), nameof(Player.UpdateMovementModifier))]
		public static class Sentinel_UpdateMovementModifier_Patch
		{
			public static void Postfix(Player __instance, ref float ___m_equipmentMovementModifier)
			{
				if (__instance != Player.m_localPlayer
					|| AsgardLegacy.al_player.al_class != AsgardLegacy.PlayerClass.Sentinel)
					return;

				if (!Utility.IsPlayerAbilityUnlockedByLevel(__instance, GlobalConfigs.al_svr_passive6UnlockLevel))
					return;

				___m_equipmentMovementModifier *= GlobalConfigs_Sentinel.al_svr_sentinel_powerfulBuild_reduceWeightPercent;
			}
		}
	}
}
