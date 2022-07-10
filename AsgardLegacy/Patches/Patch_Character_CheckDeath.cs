using HarmonyLib;
using UnityEngine;

namespace AsgardLegacy.Patches
{
    [HarmonyPatch(typeof(Character), nameof(Character.CheckDeath), null)]
    class Patch_Character_CheckDeath
    {
        public static bool Prefix(Character __instance)
		{
			if (__instance.IsDead() || __instance.GetHealth() > 0f || AsgardLegacy.al_player == null)
				return true;

			if(__instance.IsPlayer())
			{
				var player = __instance as Player;
				if (player != Player.m_localPlayer || AsgardLegacy.al_player.al_class != AsgardLegacy.PlayerClass.Guardian)
					return true;

				if (!Utility.IsPlayerAbilityUnlockedByLevel(player, GlobalConfigs.al_svr_passive6UnlockLevel))
					return true;

				if (player.GetSEMan().HaveStatusEffect("SE_Guardian_UndyingWill_CD"))
					return true;

				var se_Guardian_UndyingWill_CD = (SE_Guardian_UndyingWill_CD) ScriptableObject.CreateInstance(typeof(SE_Guardian_UndyingWill_CD));
				se_Guardian_UndyingWill_CD.m_ttl = GlobalConfigs_Guardian.al_svr_guardian_undyingWill_cooldown;
				player.GetSEMan().AddStatusEffect(se_Guardian_UndyingWill_CD, true);

				player.SetHealth(GlobalConfigs_Guardian.al_svr_guardian_undyingWill_hpPercent * player.GetMaxHealth());

				return false;
			}
			else
			{
				var player = Player.m_localPlayer;

				if (player == null)
					return true;

				var playerLevel = Utility.GetPlayerClassLevel(player);
				if (AsgardLegacy.al_player.al_class != AsgardLegacy.PlayerClass.Berserker
					|| playerLevel < GlobalConfigs.al_svr_passive3UnlockLevel
					|| Vector3.Distance(player.transform.position, __instance.transform.position) > GlobalConfigs_Berserker.al_svr_berserker_siphonLife_radius)
					return true;

				var health = player.GetMaxHealth() * Utility.GetLinearValue(
					playerLevel,
					GlobalConfigs_Berserker.al_svr_berserker_siphonLife_healthGainPercentMin,
					GlobalConfigs_Berserker.al_svr_berserker_siphonLife_healthGainPercentMax,
					GlobalConfigs.al_svr_passive3UnlockLevel);
				var stam = player.GetMaxStamina() * Utility.GetLinearValue(
					playerLevel,
					GlobalConfigs_Berserker.al_svr_berserker_siphonLife_staminaGainPercentMin,
					GlobalConfigs_Berserker.al_svr_berserker_siphonLife_staminaGainPercentMax,
					GlobalConfigs.al_svr_passive3UnlockLevel);
				UnityEngine.Object.Instantiate(ZNetScene.instance.GetPrefab("fx_guardstone_permitted_removed"), player.GetCenterPoint(), Quaternion.identity);
				player.Heal(health);
				player.AddStamina(stam);
			}
			return true;
        }
    }
}
