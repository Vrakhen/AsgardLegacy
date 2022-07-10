using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace AsgardLegacy
{
	class Class_Berserker_Patch
	{
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
	}
}