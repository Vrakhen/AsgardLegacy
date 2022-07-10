using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace AsgardLegacy
{
    class Patch_Player_GetBodyArmor
	{

		[HarmonyPatch(typeof(Player), nameof(Player.GetBodyArmor))]
		public static class Patch_GetBodyArmor
		{
			public static void Postfix(Player __instance, ref float __result)
			{
				if (__instance != Player.m_localPlayer)
					return;

				var seMan = __instance.GetSEMan();
				var playerLevel = Utility.GetPlayerClassLevel(__instance);
				switch (AsgardLegacy.al_player.al_class)
				{
					case AsgardLegacy.PlayerClass.Guardian:
						{
							if (!seMan.HaveStatusEffect("SE_Guardian_Aegis"))
								return;

							__result *= Utility.GetLinearValue(playerLevel,
										GlobalConfigs_Guardian.al_svr_guardian_aegis_armorMultiplierMin,
										GlobalConfigs_Guardian.al_svr_guardian_aegis_armorMultiplierMax,
										(int) GlobalConfigs.al_svr_ability2UnlockLevel);

							break;
						}
					case AsgardLegacy.PlayerClass.Sentinel:
						{
							if (playerLevel < GlobalConfigs.al_svr_passive2UnlockLevel)
								return;

							__result *= Utility.GetLinearValue(playerLevel,
										GlobalConfigs_Sentinel.al_svr_sentinel_dwarvenFortitude_armorBonusMin,
										GlobalConfigs_Sentinel.al_svr_sentinel_dwarvenFortitude_armorBonusMax,
										(int) GlobalConfigs.al_svr_ability1UnlockLevel);

							break;
                        }
				}
			}
		}
	}
}
