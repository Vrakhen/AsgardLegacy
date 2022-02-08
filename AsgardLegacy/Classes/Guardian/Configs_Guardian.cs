using BepInEx.Configuration;

namespace AsgardLegacy
{
	class Configs_Guardian
	{
		public static ConfigEntry<float> al_svr_guardian_shatterFall_staminaCost;
		public static ConfigEntry<float> al_svr_guardian_shatterFall_cooldown;
		public static ConfigEntry<float> al_svr_guardian_shatterFall_damageMultiplierMin;
		public static ConfigEntry<float> al_svr_guardian_shatterFall_damageMultiplierMax;
		public static ConfigEntry<float> al_svr_guardian_shatterFall_radius;
		public static ConfigEntry<float> al_svr_guardian_shatterFall_altitudeMultiplierMin;
		public static ConfigEntry<float> al_svr_guardian_shatterFall_altitudeMultiplierMax;
		public static ConfigEntry<float> al_svr_guardian_shatterFall_pushForce;
		public static ConfigEntry<float> al_svr_guardian_shatterFall_fallDamageReductionMin;
		public static ConfigEntry<float> al_svr_guardian_shatterFall_fallDamageReductionMax;

		public static ConfigEntry<float> al_svr_guardian_aegis_staminaCost;
		public static ConfigEntry<float> al_svr_guardian_aegis_cooldown;
		public static ConfigEntry<float> al_svr_guardian_aegis_durationMin;
		public static ConfigEntry<float> al_svr_guardian_aegis_durationMax;
		public static ConfigEntry<float> al_svr_guardian_aegis_armorMultiplierMin;
		public static ConfigEntry<float> al_svr_guardian_aegis_armorMultiplierMax;
		public static ConfigEntry<float> al_svr_guardian_aegis_damageReductionMin;
		public static ConfigEntry<float> al_svr_guardian_aegis_damageReductionMax;

		public static ConfigEntry<float> al_svr_guardian_iceCrush_staminaCost;
		public static ConfigEntry<float> al_svr_guardian_iceCrush_cooldown;
		public static ConfigEntry<float> al_svr_guardian_iceCrush_damageMultiplierMin;
		public static ConfigEntry<float> al_svr_guardian_iceCrush_damageMultiplierMax;
		public static ConfigEntry<float> al_svr_guardian_iceCrush_radius;

		public static ConfigEntry<float> al_svr_guardian_retribution_staminaCost;
		public static ConfigEntry<float> al_svr_guardian_retribution_cooldown;
		public static ConfigEntry<float> al_svr_guardian_retribution_duration;
		public static ConfigEntry<float> al_svr_guardian_retribution_damageMultiplier;
		public static ConfigEntry<float> al_svr_guardian_retribution_pushForce;
		public static ConfigEntry<float> al_svr_guardian_retribution_radius;

		public static ConfigEntry<float> al_svr_guardian_forceOfNature_hpBonusMin;
		public static ConfigEntry<float> al_svr_guardian_forceOfNature_hpBonusMax;
		public static ConfigEntry<float> al_svr_guardian_forceOfNature_staminaBonusMin;
		public static ConfigEntry<float> al_svr_guardian_forceOfNature_staminaBonusMax;

		public static ConfigEntry<float> al_svr_guardian_bulwark_baseBlockPowerBonusMin;
		public static ConfigEntry<float> al_svr_guardian_bulwark_baseBlockPowerBonusMax;
		public static ConfigEntry<float> al_svr_guardian_bulwark_shieldCounterCooldown;
		public static ConfigEntry<float> al_svr_guardian_bulwark_shieldCounterDuration;
		public static ConfigEntry<float> al_svr_guardian_bulwark_shieldCounterTickInterval;
		public static ConfigEntry<float> al_svr_guardian_bulwark_shieldCounterStaminaRegenMin;
		public static ConfigEntry<float> al_svr_guardian_bulwark_shieldCounterStaminaRegenMax;
		public static ConfigEntry<float> al_svr_guardian_bulwark_towerShieldBlockPowerMin;
		public static ConfigEntry<float> al_svr_guardian_bulwark_towerShieldBlockPowerMax;

		public static ConfigEntry<float> al_svr_guardian_warCry_cooldown;
		public static ConfigEntry<float> al_svr_guardian_warCry_radius;

		public static ConfigEntry<float> al_svr_guardian_undyingWill_cooldown;
		public static ConfigEntry<float> al_svr_guardian_undyingWill_hpPercent;

		public static void InitializeConfig(ConfigFile config)
		{
			al_svr_guardian_shatterFall_staminaCost = config.Bind("Guardian", "al_svr_guardian_shatterFall_staminaCost", 50f);
			al_svr_guardian_shatterFall_cooldown = config.Bind("Guardian", "al_svr_guardian_shatterFall_cooldown", 60f);
			al_svr_guardian_shatterFall_radius = config.Bind("Guardian", "al_svr_guardian_shatterFall_radius", 6f);
			al_svr_guardian_shatterFall_damageMultiplierMin = config.Bind("Guardian", "al_svr_guardian_shatterFall_damageMultiplierMin", 1.1f);
			al_svr_guardian_shatterFall_damageMultiplierMax = config.Bind("Guardian", "al_svr_guardian_shatterFall_damageMultiplierMax", 1.5f);
			al_svr_guardian_shatterFall_altitudeMultiplierMin = config.Bind("Guardian", "al_svr_guardian_shatterFall_altitudeMultiplierMin", .25f);
			al_svr_guardian_shatterFall_altitudeMultiplierMax = config.Bind("Guardian", "al_svr_guardian_shatterFall_altitudeMultiplierMax", 2f);
			al_svr_guardian_shatterFall_pushForce = config.Bind("Guardian", "al_svr_guardian_shatterFall_pushForce", 25f);
			al_svr_guardian_shatterFall_fallDamageReductionMin = config.Bind("Guardian", "al_svr_guardian_shatterFall_fallDamageReductionMin", .25f);
			al_svr_guardian_shatterFall_fallDamageReductionMax = config.Bind("Guardian", "al_svr_guardian_shatterFall_fallDamageReductionMax", .75f);

			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_shatterFall_staminaCost", al_svr_guardian_shatterFall_staminaCost.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_shatterFall_cooldown", al_svr_guardian_shatterFall_cooldown.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_shatterFall_radius", al_svr_guardian_shatterFall_radius.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_shatterFall_damageMultiplierMin", al_svr_guardian_shatterFall_damageMultiplierMin.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_shatterFall_damageMultiplierMax", al_svr_guardian_shatterFall_damageMultiplierMax.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_shatterFall_altitudeMultiplierMin", al_svr_guardian_shatterFall_altitudeMultiplierMin.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_shatterFall_altitudeMultiplierMax", al_svr_guardian_shatterFall_altitudeMultiplierMax.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_shatterFall_pushForce", al_svr_guardian_shatterFall_pushForce.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_shatterFall_fallDamageReductionMin", al_svr_guardian_shatterFall_fallDamageReductionMin.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_shatterFall_fallDamageReductionMax", al_svr_guardian_shatterFall_fallDamageReductionMax.Value);

			al_svr_guardian_aegis_staminaCost = config.Bind("Guardian", "al_svr_guardian_aegis_staminaCost", 25f);
			al_svr_guardian_aegis_cooldown = config.Bind("Guardian", "al_svr_guardian_aegis_cooldown", 180f);
			al_svr_guardian_aegis_durationMin = config.Bind("Guardian", "al_svr_guardian_aegis_durationMin", 5f);
			al_svr_guardian_aegis_durationMax = config.Bind("Guardian", "al_svr_guardian_aegis_durationMax", 5f);
			al_svr_guardian_aegis_armorMultiplierMin = config.Bind("Guardian", "al_svr_guardian_aegis_armorMultiplierMin", 1.5f);
			al_svr_guardian_aegis_armorMultiplierMax = config.Bind("Guardian", "al_svr_guardian_aegis_armorMultiplierMax", 2f);
			al_svr_guardian_aegis_damageReductionMin = config.Bind("Guardian", "al_svr_guardian_aegis_damageReductionMin", .75f);
			al_svr_guardian_aegis_damageReductionMax = config.Bind("Guardian", "al_svr_guardian_aegis_damageReductionMax", .33f);

			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_aegis_staminaCost", al_svr_guardian_aegis_staminaCost.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_aegis_cooldown", al_svr_guardian_aegis_cooldown.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_aegis_durationMin", al_svr_guardian_aegis_durationMin.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_aegis_durationMax", al_svr_guardian_aegis_durationMax.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_aegis_armorMultiplierMin", al_svr_guardian_aegis_armorMultiplierMin.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_aegis_armorMultiplierMax", al_svr_guardian_aegis_armorMultiplierMax.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_aegis_damageReductionMin", al_svr_guardian_aegis_damageReductionMin.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_aegis_damageReductionMax", al_svr_guardian_aegis_damageReductionMax.Value);

			al_svr_guardian_iceCrush_staminaCost = config.Bind("Guardian", "al_svr_guardian_iceCrush_staminaCost", 50f);
			al_svr_guardian_iceCrush_cooldown = config.Bind("Guardian", "al_svr_guardian_iceCrush_cooldown", 300f);
			al_svr_guardian_iceCrush_damageMultiplierMin = config.Bind("Guardian", "al_svr_guardian_iceCrush_damageMultiplierMin", 1.1f);
			al_svr_guardian_iceCrush_damageMultiplierMax = config.Bind("Guardian", "al_svr_guardian_iceCrush_damageMultiplierMax", 1.5f);
			al_svr_guardian_iceCrush_radius = config.Bind("Guardian", "al_svr_guardian_iceCrush_radius", 6f);

			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_iceCrush_staminaCost", al_svr_guardian_iceCrush_staminaCost.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_iceCrush_cooldown", al_svr_guardian_aegis_cooldown.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_iceCrush_damageMultiplierMin", al_svr_guardian_iceCrush_damageMultiplierMin.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_iceCrush_damageMultiplierMax", al_svr_guardian_iceCrush_damageMultiplierMax.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_iceCrush_radius", al_svr_guardian_iceCrush_radius.Value);

			al_svr_guardian_retribution_staminaCost = config.Bind("Guardian", "al_svr_guardian_retribution_staminaCost", 75f);
			al_svr_guardian_retribution_cooldown = config.Bind("Guardian", "al_svr_guardian_retribution_cooldown", 600f);
			al_svr_guardian_retribution_duration = config.Bind("Guardian", "al_svr_guardian_retribution_duration", 5f);
			al_svr_guardian_retribution_damageMultiplier = config.Bind("Guardian", "al_svr_guardian_retribution_damageMultiplier", 1.5f);
			al_svr_guardian_retribution_pushForce = config.Bind("Guardian", "al_svr_guardian_retribution_pushForce", 40f);
			al_svr_guardian_retribution_radius = config.Bind("Guardian", "al_svr_guardian_retribution_radius", 10f);

			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_retribution_staminaCost", al_svr_guardian_retribution_staminaCost.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_retribution_cooldown", al_svr_guardian_retribution_cooldown.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_retribution_duration", al_svr_guardian_retribution_duration.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_retribution_damageMultiplier", al_svr_guardian_retribution_damageMultiplier.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_retribution_pushForce", al_svr_guardian_retribution_pushForce.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_retribution_radius", al_svr_guardian_retribution_radius.Value);

			al_svr_guardian_forceOfNature_hpBonusMin = config.Bind("Guardian", "al_svr_guardian_forceOfNature_hpBonusMin", 1.1f);
			al_svr_guardian_forceOfNature_hpBonusMax = config.Bind("Guardian", "al_svr_guardian_forceOfNature_hpBonusMax", 1.5f);
			al_svr_guardian_forceOfNature_staminaBonusMin = config.Bind("Guardian", "al_svr_guardian_forceOfNature_staminaBonusMin", 1.05f);
			al_svr_guardian_forceOfNature_staminaBonusMax = config.Bind("Guardian", "al_svr_guardian_forceOfNature_staminaBonusMax", 1.25f);

			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_forceOfNature_hpBonusMin", al_svr_guardian_forceOfNature_hpBonusMin.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_forceOfNature_hpBonusMax", al_svr_guardian_forceOfNature_hpBonusMax.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_forceOfNature_staminaBonusMin", al_svr_guardian_forceOfNature_staminaBonusMin.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_forceOfNature_staminaBonusMax", al_svr_guardian_forceOfNature_staminaBonusMax.Value);

			al_svr_guardian_bulwark_baseBlockPowerBonusMin = config.Bind("Guardian", "al_svr_guardian_bulwark_baseBlockPowerBonusMin", 1.1f);
			al_svr_guardian_bulwark_baseBlockPowerBonusMax = config.Bind("Guardian", "al_svr_guardian_bulwark_baseBlockPowerBonusMax", 1.5f);
			al_svr_guardian_bulwark_shieldCounterCooldown = config.Bind("Guardian", "al_svr_guardian_bulwark_shieldCounterCooldown", 60f);
			al_svr_guardian_bulwark_shieldCounterDuration = config.Bind("Guardian", "al_svr_guardian_bulwark_shieldCounterDuration", 5f);
			al_svr_guardian_bulwark_shieldCounterTickInterval = config.Bind("Guardian", "al_svr_guardian_bulwark_shieldCounterTickInterval", 1f);
			al_svr_guardian_bulwark_shieldCounterStaminaRegenMin = config.Bind("Guardian", "al_svr_guardian_bulwark_shieldCounterStaminaRegenMin", .1f);
			al_svr_guardian_bulwark_shieldCounterStaminaRegenMax = config.Bind("Guardian", "al_svr_guardian_bulwark_shieldCounterStaminaRegenMax", .25f);
			al_svr_guardian_bulwark_towerShieldBlockPowerMin = config.Bind("Guardian", "al_svr_guardian_bulwark_towerShieldBlockPowerMin", .15f);
			al_svr_guardian_bulwark_towerShieldBlockPowerMax = config.Bind("Guardian", "al_svr_guardian_bulwark_towerShieldBlockPowerMax", .25f);

			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_bulwark_baseBlockPowerBonusMin", al_svr_guardian_bulwark_baseBlockPowerBonusMin.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_bulwark_baseBlockPowerBonusMax", al_svr_guardian_bulwark_baseBlockPowerBonusMax.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_bulwark_shieldCounterCooldown", al_svr_guardian_bulwark_shieldCounterCooldown.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_bulwark_shieldCounterDuration", al_svr_guardian_bulwark_shieldCounterDuration.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_bulwark_shieldCounterTickInterval", al_svr_guardian_bulwark_shieldCounterTickInterval.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_bulwark_shieldCounterStaminaRegenMin", al_svr_guardian_bulwark_shieldCounterStaminaRegenMin.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_bulwark_shieldCounterStaminaRegenMax", al_svr_guardian_bulwark_shieldCounterStaminaRegenMax.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_bulwark_towerShieldBlockPowerMin", al_svr_guardian_bulwark_towerShieldBlockPowerMin.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_bulwark_towerShieldBlockPowerMax", al_svr_guardian_bulwark_towerShieldBlockPowerMax.Value);

			al_svr_guardian_warCry_cooldown = config.Bind("Guardian", "al_svr_guardian_warCry_cooldown", 120f);
			al_svr_guardian_warCry_radius = config.Bind("Guardian", "al_svr_guardian_warCry_radius", 6f);

			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_warCry_cooldown", al_svr_guardian_warCry_cooldown.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_warCry_radius", al_svr_guardian_warCry_radius.Value);

			al_svr_guardian_undyingWill_cooldown = config.Bind("Guardian", "al_svr_guardian_undyingWill_cooldown", 600f);
			al_svr_guardian_undyingWill_hpPercent = config.Bind("Guardian", "al_svr_guardian_undyingWill_hpPercent", .25f);

			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_undyingWill_cooldown", al_svr_guardian_undyingWill_cooldown.Value);
			GlobalConfigs_Guardian.ConfigStrings.Add("al_svr_guardian_undyingWill_hpPercent", al_svr_guardian_undyingWill_hpPercent.Value);
		}
	}
}
