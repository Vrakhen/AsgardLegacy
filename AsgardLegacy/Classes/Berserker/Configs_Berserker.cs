using BepInEx.Configuration;

namespace AsgardLegacy
{
	class Configs_Berserker
	{
		public static ConfigEntry<float> al_svr_berserker_charge_staminaCost;
		public static ConfigEntry<float> al_svr_berserker_charge_cooldown;
		public static ConfigEntry<float> al_svr_berserker_charge_damageMultiplierMin;
		public static ConfigEntry<float> al_svr_berserker_charge_damageMultiplierMax;
		public static ConfigEntry<float> al_svr_berserker_charge_range;

		public static ConfigEntry<float> al_svr_berserker_dreadfulRoar_staminaCost;
		public static ConfigEntry<float> al_svr_berserker_dreadfulRoar_cooldown;
		public static ConfigEntry<float> al_svr_berserker_dreadfulRoar_radius;
		public static ConfigEntry<float> al_svr_berserker_dreadfulRoar_durationMin;
		public static ConfigEntry<float> al_svr_berserker_dreadfulRoar_durationMax;
		public static ConfigEntry<float> al_svr_berserker_dreadfulRoar_slowValueMin;
		public static ConfigEntry<float> al_svr_berserker_dreadfulRoar_slowValueMax;
		public static ConfigEntry<float> al_svr_berserker_dreadfulRoar_weakenValueMin;
		public static ConfigEntry<float> al_svr_berserker_dreadfulRoar_weakenValueMax;

		public static ConfigEntry<float> al_svr_berserker_ragingStorm_staminaCost;
		public static ConfigEntry<float> al_svr_berserker_ragingStorm_cooldown;
		public static ConfigEntry<float> al_svr_berserker_ragingStorm_durationMin;
		public static ConfigEntry<float> al_svr_berserker_ragingStorm_durationMax;
		public static ConfigEntry<float> al_svr_berserker_ragingStorm_damageMultiplierMin;
		public static ConfigEntry<float> al_svr_berserker_ragingStorm_damageMultiplierMax;

		public static ConfigEntry<float> al_svr_berserker_frenzy_staminaCost;
		public static ConfigEntry<float> al_svr_berserker_frenzy_cooldown;
		public static ConfigEntry<float> al_svr_berserker_frenzy_duration;
		public static ConfigEntry<float> al_svr_berserker_frenzy_attackSpeedMultiplierMin;
		public static ConfigEntry<float> al_svr_berserker_frenzy_attackSpeedMultiplierMax;

		public static ConfigEntry<float> al_svr_berserker_twoHandedExpert_damageBonusMin;
		public static ConfigEntry<float> al_svr_berserker_twoHandedExpert_damageBonusMax;
		public static ConfigEntry<float> al_svr_berserker_twoHandedExpert_staminaBonusMin;
		public static ConfigEntry<float> al_svr_berserker_twoHandedExpert_staminaBonusMax;

		public static ConfigEntry<float> al_svr_berserker_reckless_bonusDamageMin;
		public static ConfigEntry<float> al_svr_berserker_reckless_bonusDamageMax;
		public static ConfigEntry<float> al_svr_berserker_reckless_bonusMitigationMin;
		public static ConfigEntry<float> al_svr_berserker_reckless_bonusMitigationMax;

		public static ConfigEntry<float> al_svr_berserker_adrenalineRush_cooldown;
		public static ConfigEntry<float> al_svr_berserker_adrenalineRush_duration;
		public static ConfigEntry<float> al_svr_berserker_adrenalineRush_hpPercentMin;
		public static ConfigEntry<float> al_svr_berserker_adrenalineRush_hpPercentMax;
		public static ConfigEntry<float> al_svr_berserker_adrenalineRush_bonusStaminaMin;
		public static ConfigEntry<float> al_svr_berserker_adrenalineRush_bonusStaminaMax;

		public static ConfigEntry<float> al_svr_berserker_denyPain_cooldown;
		public static ConfigEntry<float> al_svr_berserker_denyPain_duration;

		public static void InitializeConfig(ConfigFile config)
		{
			al_svr_berserker_charge_staminaCost = config.Bind("Berserker", "al_svr_berserker_charge_staminaCost", 50f);
			al_svr_berserker_charge_cooldown = config.Bind("Berserker", "al_svr_berserker_charge_cooldown", 60f);
			al_svr_berserker_charge_range = config.Bind("Berserker", "al_svr_berserker_charge_range", 6f);
			al_svr_berserker_charge_damageMultiplierMin = config.Bind("Berserker", "al_svr_berserker_charge_damageMultiplierMin", 1.1f);
			al_svr_berserker_charge_damageMultiplierMax = config.Bind("Berserker", "al_svr_berserker_charge_damageMultiplierMax", 1.5f);

			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_charge_staminaCost", al_svr_berserker_charge_staminaCost.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_charge_cooldown", al_svr_berserker_charge_cooldown.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_charge_range", al_svr_berserker_charge_range.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_charge_damageMultiplierMin", al_svr_berserker_charge_damageMultiplierMin.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_charge_damageMultiplierMax", al_svr_berserker_charge_damageMultiplierMax.Value);

			al_svr_berserker_dreadfulRoar_staminaCost = config.Bind("Berserker", "al_svr_berserker_dreadfulRoar_staminaCost", 25f);
			al_svr_berserker_dreadfulRoar_cooldown = config.Bind("Berserker", "al_svr_berserker_dreadfulRoar_cooldown", 180f);
			al_svr_berserker_dreadfulRoar_radius = config.Bind("Berserker", "al_svr_berserker_dreadfulRoar_radius", 10f);
			al_svr_berserker_dreadfulRoar_durationMin = config.Bind("Berserker", "al_svr_berserker_dreadfulRoar_durationMin", 5f);
			al_svr_berserker_dreadfulRoar_durationMax = config.Bind("Berserker", "al_svr_berserker_dreadfulRoar_durationMax", 10f);
			al_svr_berserker_dreadfulRoar_slowValueMin = config.Bind("Berserker", "al_svr_berserker_dreadfulRoar_slowValueMin", .75f);
			al_svr_berserker_dreadfulRoar_slowValueMax = config.Bind("Berserker", "al_svr_berserker_dreadfulRoar_slowValueMax", .5f);
			al_svr_berserker_dreadfulRoar_weakenValueMin = config.Bind("Berserker", "al_svr_berserker_dreadfulRoar_weakenValueMin", .8f);
			al_svr_berserker_dreadfulRoar_weakenValueMax = config.Bind("Berserker", "al_svr_berserker_dreadfulRoar_weakenValueMax", .33f);

			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_dreadfulRoar_staminaCost", al_svr_berserker_dreadfulRoar_staminaCost.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_dreadfulRoar_cooldown", al_svr_berserker_dreadfulRoar_cooldown.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_dreadfulRoar_radius", al_svr_berserker_dreadfulRoar_radius.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_dreadfulRoar_durationMin", al_svr_berserker_dreadfulRoar_durationMin.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_dreadfulRoar_durationMax", al_svr_berserker_dreadfulRoar_durationMax.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_dreadfulRoar_slowValueMin", al_svr_berserker_dreadfulRoar_slowValueMin.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_dreadfulRoar_slowValueMax", al_svr_berserker_dreadfulRoar_slowValueMax.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_dreadfulRoar_weakenValueMin", al_svr_berserker_dreadfulRoar_weakenValueMin.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_dreadfulRoar_weakenValueMax", al_svr_berserker_dreadfulRoar_weakenValueMax.Value);

			al_svr_berserker_ragingStorm_staminaCost = config.Bind("Berserker", "al_svr_berserker_ragingStorm_staminaCost", 50f);
			al_svr_berserker_ragingStorm_cooldown = config.Bind("Berserker", "al_svr_berserker_ragingStorm_cooldown", 300f);
			al_svr_berserker_ragingStorm_durationMin = config.Bind("Berserker", "al_svr_berserker_ragingStorm_durationMin", 5f);
			al_svr_berserker_ragingStorm_durationMax = config.Bind("Berserker", "al_svr_berserker_ragingStorm_durationMax", 5f);
			al_svr_berserker_ragingStorm_damageMultiplierMin = config.Bind("Berserker", "al_svr_berserker_ragingStorm_damageMultiplierMin", .25f);
			al_svr_berserker_ragingStorm_damageMultiplierMax = config.Bind("Berserker", "al_svr_berserker_ragingStorm_damageMultiplierMax", .5f);

			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_ragingStorm_staminaCost", al_svr_berserker_ragingStorm_staminaCost.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_ragingStorm_cooldown", al_svr_berserker_dreadfulRoar_cooldown.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_ragingStorm_durationMin", al_svr_berserker_ragingStorm_durationMin.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_ragingStorm_durationMax", al_svr_berserker_ragingStorm_durationMax.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_ragingStorm_damageMultiplierMin", al_svr_berserker_ragingStorm_damageMultiplierMin.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_ragingStorm_damageMultiplierMax", al_svr_berserker_ragingStorm_damageMultiplierMax.Value);

			al_svr_berserker_frenzy_staminaCost = config.Bind("Berserker", "al_svr_berserker_frenzy_staminaCost", 75f);
			al_svr_berserker_frenzy_cooldown = config.Bind("Berserker", "al_svr_berserker_frenzy_cooldown", 600f);
			al_svr_berserker_frenzy_duration = config.Bind("Berserker", "al_svr_berserker_frenzy_duration", 5f);
			al_svr_berserker_frenzy_attackSpeedMultiplierMin = config.Bind("Berserker", "al_svr_berserker_frenzy_attackSpeedMultiplierMin", 1.1f);
			al_svr_berserker_frenzy_attackSpeedMultiplierMax = config.Bind("Berserker", "al_svr_berserker_frenzy_attackSpeedMultiplierMax", 1.25f);

			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_frenzy_staminaCost", al_svr_berserker_frenzy_staminaCost.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_frenzy_cooldown", al_svr_berserker_frenzy_cooldown.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_frenzy_duration", al_svr_berserker_frenzy_duration.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_frenzy_attackSpeedMultiplierMin", al_svr_berserker_frenzy_attackSpeedMultiplierMin.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_frenzy_attackSpeedMultiplierMax", al_svr_berserker_frenzy_attackSpeedMultiplierMax.Value);

			al_svr_berserker_twoHandedExpert_damageBonusMin = config.Bind("Berserker", "al_svr_berserker_twoHandedExpert_damageBonusMin", .1f);
			al_svr_berserker_twoHandedExpert_damageBonusMax = config.Bind("Berserker", "al_svr_berserker_twoHandedExpert_damageBonusMax", .5f);
			al_svr_berserker_twoHandedExpert_staminaBonusMin = config.Bind("Berserker", "al_svr_berserker_twoHandedExpert_staminaBonusMin", .95f);
			al_svr_berserker_twoHandedExpert_staminaBonusMax = config.Bind("Berserker", "al_svr_berserker_twoHandedExpert_staminaBonusMax", .75f);

			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_twoHandedExpert_damageBonusMin", al_svr_berserker_twoHandedExpert_damageBonusMin.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_twoHandedExpert_damageBonusMax", al_svr_berserker_twoHandedExpert_damageBonusMax.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_twoHandedExpert_staminaBonusMin", al_svr_berserker_twoHandedExpert_staminaBonusMin.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_twoHandedExpert_staminaBonusMax", al_svr_berserker_twoHandedExpert_staminaBonusMax.Value);

			al_svr_berserker_reckless_bonusDamageMin = config.Bind("Berserker", "al_svr_berserker_reckless_bonusDamageMin", .005f);
			al_svr_berserker_reckless_bonusDamageMax = config.Bind("Berserker", "al_svr_berserker_reckless_bonusDamageMax", .005f);
			al_svr_berserker_reckless_bonusMitigationMin = config.Bind("Berserker", "al_svr_berserker_reckless_bonusMitigationMin", .005f);
			al_svr_berserker_reckless_bonusMitigationMax = config.Bind("Berserker", "al_svr_berserker_reckless_bonusMitigationMax", .005f);

			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_reckless_bonusDamageMin", al_svr_berserker_reckless_bonusDamageMin.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_reckless_bonusDamageMax", al_svr_berserker_reckless_bonusDamageMax.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_reckless_bonusMitigationMin", al_svr_berserker_reckless_bonusMitigationMin.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_reckless_bonusMitigationMax", al_svr_berserker_reckless_bonusMitigationMax.Value);

			al_svr_berserker_adrenalineRush_cooldown = config.Bind("Berserker", "al_svr_berserker_adrenalineRush_cooldown", 60f);
			al_svr_berserker_adrenalineRush_duration = config.Bind("Berserker", "al_svr_berserker_adrenalineRush_duration", 4f);
			al_svr_berserker_adrenalineRush_hpPercentMin = config.Bind("Berserker", "al_svr_berserker_adrenalineRush_hpPercentMin", .25f);
			al_svr_berserker_adrenalineRush_hpPercentMax = config.Bind("Berserker", "al_svr_berserker_adrenalineRush_hpPercentMax", .5f);
			al_svr_berserker_adrenalineRush_bonusStaminaMin = config.Bind("Berserker", "al_svr_berserker_adrenalineRush_bonusStaminaMin", .33f);
			al_svr_berserker_adrenalineRush_bonusStaminaMax = config.Bind("Berserker", "al_svr_berserker_adrenalineRush_bonusStaminaMax", .75f);

			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_adrenalineRush_cooldown", al_svr_berserker_adrenalineRush_cooldown.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_adrenalineRush_duration", al_svr_berserker_adrenalineRush_duration.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_adrenalineRush_hpPercentMin", al_svr_berserker_adrenalineRush_hpPercentMin.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_adrenalineRush_hpPercentMax", al_svr_berserker_adrenalineRush_hpPercentMax.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_adrenalineRush_bonusStaminaMin", al_svr_berserker_adrenalineRush_bonusStaminaMin.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_adrenalineRush_bonusStaminaMax", al_svr_berserker_adrenalineRush_bonusStaminaMax.Value);

			al_svr_berserker_denyPain_cooldown = config.Bind("Berserker", "al_svr_berserker_denyPain_cooldown", 300f);
			al_svr_berserker_denyPain_duration = config.Bind("Berserker", "al_svr_berserker_denyPain_duration", 3f);

			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_denyPain_cooldown", al_svr_berserker_denyPain_cooldown.Value);
			GlobalConfigs_Berserker.ConfigStrings.Add("al_svr_berserker_denyPain_duration", al_svr_berserker_denyPain_duration.Value);
		}
	}
}
