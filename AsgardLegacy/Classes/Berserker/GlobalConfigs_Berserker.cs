using System.Collections.Generic;

namespace AsgardLegacy
{
	class GlobalConfigs_Berserker
	{
		public static Dictionary<string, float> ConfigStrings = new Dictionary<string, float>();

		public static float al_svr_berserker_charge_staminaCost
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_charge_staminaCost") ? ConfigStrings["al_svr_berserker_charge_staminaCost"] : 0f; } }
		public static float al_svr_berserker_charge_cooldown
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_charge_cooldown") ? ConfigStrings["al_svr_berserker_charge_cooldown"] : 0f; } }
		public static float al_svr_berserker_charge_damageMultiplierMin
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_charge_damageMultiplierMin") ? ConfigStrings["al_svr_berserker_charge_damageMultiplierMin"] : 0f; } }
		public static float al_svr_berserker_charge_damageMultiplierMax
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_charge_damageMultiplierMax") ? ConfigStrings["al_svr_berserker_charge_damageMultiplierMax"] : 0f; } }
		public static float al_svr_berserker_charge_range
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_charge_range") ? ConfigStrings["al_svr_berserker_charge_range"] : 0f; } }

		public static float al_svr_berserker_dreadfulRoar_staminaCost
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_dreadfulRoar_staminaCost") ? ConfigStrings["al_svr_berserker_dreadfulRoar_staminaCost"] : 0f; } }
		public static float al_svr_berserker_dreadfulRoar_cooldown
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_dreadfulRoar_cooldown") ? ConfigStrings["al_svr_berserker_dreadfulRoar_cooldown"] : 0f; } }
		public static float al_svr_berserker_dreadfulRoar_radius
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_dreadfulRoar_radius") ? ConfigStrings["al_svr_berserker_dreadfulRoar_radius"] : 0f; } }
		public static float al_svr_berserker_dreadfulRoar_durationMin
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_dreadfulRoar_durationMin") ? ConfigStrings["al_svr_berserker_dreadfulRoar_durationMin"] : 0f; } }
		public static float al_svr_berserker_dreadfulRoar_durationMax
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_dreadfulRoar_durationMax") ? ConfigStrings["al_svr_berserker_dreadfulRoar_durationMax"] : 0f; } }
		public static float al_svr_berserker_dreadfulRoar_slowValueMin
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_dreadfulRoar_slowValueMin") ? ConfigStrings["al_svr_berserker_dreadfulRoar_slowValueMin"] : 0f; } }
		public static float al_svr_berserker_dreadfulRoar_slowValueMax
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_dreadfulRoar_slowValueMax") ? ConfigStrings["al_svr_berserker_dreadfulRoar_slowValueMax"] : 1f; } }
		public static float al_svr_berserker_dreadfulRoar_weakenValueMin
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_dreadfulRoar_weakenValueMin") ? ConfigStrings["al_svr_berserker_dreadfulRoar_weakenValueMin"] : 1f; } }
		public static float al_svr_berserker_dreadfulRoar_weakenValueMax
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_dreadfulRoar_weakenValueMax") ? ConfigStrings["al_svr_berserker_dreadfulRoar_weakenValueMax"] : 1f; } } 

		public static float al_svr_berserker_ragingStorm_staminaCost
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_ragingStorm_staminaCost") ? ConfigStrings["al_svr_berserker_ragingStorm_staminaCost"] : 0f; } }
		public static float al_svr_berserker_ragingStorm_cooldown
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_ragingStorm_cooldown") ? ConfigStrings["al_svr_berserker_ragingStorm_cooldown"] : 0f; } }
		public static float al_svr_berserker_ragingStorm_durationMin
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_ragingStorm_durationMin") ? ConfigStrings["al_svr_berserker_ragingStorm_durationMin"] : 0f; } }
		public static float al_svr_berserker_ragingStorm_durationMax
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_ragingStorm_durationMax") ? ConfigStrings["al_svr_berserker_ragingStorm_durationMax"] : 0f; } }
		public static float al_svr_berserker_ragingStorm_damageMultiplierMin
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_ragingStorm_damageMultiplierMin") ? ConfigStrings["al_svr_berserker_ragingStorm_damageMultiplierMin"] : 0f; } }
		public static float al_svr_berserker_ragingStorm_damageMultiplierMax
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_ragingStorm_damageMultiplierMax") ? ConfigStrings["al_svr_berserker_ragingStorm_damageMultiplierMax"] : 0f; } }

		public static float al_svr_berserker_frenzy_staminaCost
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_frenzy_staminaCost") ? ConfigStrings["al_svr_berserker_frenzy_staminaCost"] : 0f; } }
		public static float al_svr_berserker_frenzy_cooldown
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_frenzy_cooldown") ? ConfigStrings["al_svr_berserker_frenzy_cooldown"] : 0f; } }
		public static float al_svr_berserker_frenzy_duration
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_frenzy_duration") ? ConfigStrings["al_svr_berserker_frenzy_duration"] : 0f; } }
		public static float al_svr_berserker_frenzy_attackSpeedMultiplierMin
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_frenzy_attackSpeedMultiplierMin") ? ConfigStrings["al_svr_berserker_frenzy_attackSpeedMultiplierMin"] : 0f; } }
		public static float al_svr_berserker_frenzy_attackSpeedMultiplierMax
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_frenzy_attackSpeedMultiplierMax") ? ConfigStrings["al_svr_berserker_frenzy_attackSpeedMultiplierMax"] : 0f; } }

		public static float al_svr_berserker_twoHandedExpert_damageBonusMin
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_twoHandedExpert_damageBonusMin") ? ConfigStrings["al_svr_berserker_twoHandedExpert_damageBonusMin"] : 0f; } }
		public static float al_svr_berserker_twoHandedExpert_damageBonusMax
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_twoHandedExpert_damageBonusMax") ? ConfigStrings["al_svr_berserker_twoHandedExpert_damageBonusMax"] : 0f; } }
		public static float al_svr_berserker_twoHandedExpert_staminaBonusMin
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_twoHandedExpert_staminaBonusMin") ? ConfigStrings["al_svr_berserker_twoHandedExpert_staminaBonusMin"] : 0f; } }
		public static float al_svr_berserker_twoHandedExpert_staminaBonusMax
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_twoHandedExpert_staminaBonusMax") ? ConfigStrings["al_svr_berserker_twoHandedExpert_staminaBonusMax"] : 1f; } }

		public static float al_svr_berserker_reckless_cooldown
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_reckless_cooldown") ? ConfigStrings["al_svr_berserker_reckless_cooldown"] : 0f; } }
		public static float al_svr_berserker_reckless_bonusDamageMin
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_reckless_bonusDamageMin") ? ConfigStrings["al_svr_berserker_reckless_bonusDamageMin"] : 0f; } }
		public static float al_svr_berserker_reckless_bonusDamageMax
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_reckless_bonusDamageMax") ? ConfigStrings["al_svr_berserker_reckless_bonusDamageMax"] : 0f; } }
		public static float al_svr_berserker_reckless_bonusMitigationMin
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_reckless_bonusMitigationMin") ? ConfigStrings["al_svr_berserker_reckless_bonusMitigationMin"] : 0f; } }
		public static float al_svr_berserker_reckless_bonusMitigationMax
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_reckless_bonusMitigationMax") ? ConfigStrings["al_svr_berserker_reckless_bonusMitigationMax"] : 0f; } }

		public static float al_svr_berserker_adrenalineRush_cooldown
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_adrenalineRush_cooldown") ? ConfigStrings["al_svr_berserker_adrenalineRush_cooldown"] : 1f; } }
		public static float al_svr_berserker_adrenalineRush_duration
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_adrenalineRush_duration") ? ConfigStrings["al_svr_berserker_adrenalineRush_duration"] : 1f; } }
		public static float al_svr_berserker_adrenalineRush_hpPercentMin
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_adrenalineRush_hpPercentMin") ? ConfigStrings["al_svr_berserker_adrenalineRush_hpPercentMin"] : 1f; } }
		public static float al_svr_berserker_adrenalineRush_hpPercentMax
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_adrenalineRush_hpPercentMax") ? ConfigStrings["al_svr_berserker_adrenalineRush_hpPercentMax"] : 1f; } }
		public static float al_svr_berserker_adrenalineRush_bonusStaminaMin
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_adrenalineRush_bonusStaminaMin") ? ConfigStrings["al_svr_berserker_adrenalineRush_bonusStaminaMin"] : 1f; } }
		public static float al_svr_berserker_adrenalineRush_bonusStaminaMax
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_adrenalineRush_bonusStaminaMax") ? ConfigStrings["al_svr_berserker_adrenalineRush_bonusStaminaMax"] : 1f; } }

		public static float al_svr_berserker_denyPain_cooldown
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_denyPain_cooldown") ? ConfigStrings["al_svr_berserker_denyPain_cooldown"] : 0f; } }
		public static float al_svr_berserker_denyPain_duration
		{ get { return ConfigStrings.ContainsKey("al_svr_berserker_denyPain_duration") ? ConfigStrings["al_svr_berserker_denyPain_duration"] : 0f; } }
	}
}