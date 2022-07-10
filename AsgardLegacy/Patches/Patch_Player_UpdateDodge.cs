using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace AsgardLegacy
{
    class Patch_Player_UpdateDodge
	{

		[HarmonyPatch(typeof(Player), nameof(Player.UpdateDodge))]
		public static class Patch_OnDodge
		{
			public static void Postfix(Player __instance, float ___m_queuedDodgeTimer)
			{
				if (___m_queuedDodgeTimer >= -0.5f || ___m_queuedDodgeTimer <= -0.55f)
					return;

				if (__instance != Player.m_localPlayer)
					return;

				var seMan = __instance.GetSEMan();
				var playerLevel = Utility.GetPlayerClassLevel(__instance);
				switch (AsgardLegacy.al_player.al_class)
				{
					case AsgardLegacy.PlayerClass.Ranger:
						{
							if (seMan.GetStatusEffect("SE_Ranger_SpeedBurst_CD")
								|| playerLevel < GlobalConfigs.al_svr_passive3UnlockLevel)
								return;

							var speedBurst = (SE_Ranger_SpeedBurst) ScriptableObject.CreateInstance(typeof(SE_Ranger_SpeedBurst));
							var speedBurstCD = (SE_Ranger_SpeedBurst_CD) ScriptableObject.CreateInstance(typeof(SE_Ranger_SpeedBurst_CD));
							speedBurst.m_ttl = Utility.GetLinearValue(
									playerLevel,
									GlobalConfigs_Ranger.al_svr_ranger_speedBurst_durationMin,
									GlobalConfigs_Ranger.al_svr_ranger_speedBurst_durationMax,
									GlobalConfigs.al_svr_passive3UnlockLevel);
							speedBurst.m_speedModifier = Utility.GetLinearValue(
									playerLevel,
									GlobalConfigs_Ranger.al_svr_ranger_speedBurst_speedMultiplierMin,
									GlobalConfigs_Ranger.al_svr_ranger_speedBurst_speedMultiplierMax,
									GlobalConfigs.al_svr_passive3UnlockLevel);
							speedBurstCD.m_ttl = GlobalConfigs_Ranger.al_svr_ranger_speedBurst_cooldown;
							seMan.AddStatusEffect(speedBurst, true);
							seMan.AddStatusEffect(speedBurstCD, true);
							Object.Instantiate(ZNetScene.instance.GetPrefab("vfx_HitSparks"), __instance.GetCenterPoint(), Quaternion.identity);
							Object.Instantiate(ZNetScene.instance.GetPrefab("sfx_WishbonePing_med"), __instance.GetCenterPoint(), Quaternion.identity);

							__instance.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainPassiveTrigger);

							break;
						}
					case AsgardLegacy.PlayerClass.Sentinel:
						{
							if (seMan.GetStatusEffect("SE_Sentinel_CleansingRoll_CD")
								|| playerLevel < GlobalConfigs.al_svr_passive4UnlockLevel)
								return;

							var removedSE = false;
							foreach (var se in Utility.CleansedSE)
							{
								if (!seMan.HaveStatusEffect(se))
									continue;

								removedSE = true;
								seMan.RemoveStatusEffect(se);
							}

							if (!removedSE)
								return;

							var cleansingRollCD = (SE_Sentinel_CleansingRoll_CD) ScriptableObject.CreateInstance(typeof(SE_Sentinel_CleansingRoll_CD));
							cleansingRollCD.m_ttl = GlobalConfigs_Sentinel.al_svr_sentinel_cleansingRoll_cooldown;

							seMan.AddStatusEffect(cleansingRollCD, true);
							Object.Instantiate(ZNetScene.instance.GetPrefab("vfx_HitSparks"), __instance.GetCenterPoint(), Quaternion.identity);
							Object.Instantiate(ZNetScene.instance.GetPrefab("sfx_WishbonePing_med"), __instance.GetCenterPoint(), Quaternion.identity);

							__instance.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainPassiveTrigger);

							break;
                        }
				}
			}
		}
	}
}
