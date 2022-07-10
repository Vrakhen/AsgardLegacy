using BepInEx.Configuration;

namespace AsgardLegacy
{
	class Configs_Ranger
	{
		public static ConfigEntry<float> al_svr_ranger_explosiveArrow_staminaCost;
		public static ConfigEntry<float> al_svr_ranger_explosiveArrow_cooldown;
		public static ConfigEntry<float> al_svr_ranger_explosiveArrow_duration;
		public static ConfigEntry<float> al_svr_ranger_explosiveArrow_radius;
		public static ConfigEntry<float> al_svr_ranger_explosiveArrow_damageMin;
		public static ConfigEntry<float> al_svr_ranger_explosiveArrow_damageMax;
		public static ConfigEntry<float> al_svr_ranger_explosiveArrow_pushForce;

		public static ConfigEntry<float> al_svr_ranger_shadowStalk_staminaCost;
		public static ConfigEntry<float> al_svr_ranger_shadowStalk_cooldown;
		public static ConfigEntry<float> al_svr_ranger_shadowStalk_durationMin;
		public static ConfigEntry<float> al_svr_ranger_shadowStalk_durationMax;
		public static ConfigEntry<float> al_svr_ranger_shadowStalk_speedDurationMin;
		public static ConfigEntry<float> al_svr_ranger_shadowStalk_speedDurationMax;
		public static ConfigEntry<float> al_svr_ranger_shadowStalk_speedMultiplierMin;
		public static ConfigEntry<float> al_svr_ranger_shadowStalk_speedMultiplierMax;
		public static ConfigEntry<float> al_svr_ranger_shadowStalk_crouchSpeedMultiplierMin;
		public static ConfigEntry<float> al_svr_ranger_shadowStalk_crouchSpeedMultiplierMax;

		public static ConfigEntry<float> al_svr_ranger_rapidFire_staminaCost;
		public static ConfigEntry<float> al_svr_ranger_rapidFire_cooldown;
		public static ConfigEntry<float> al_svr_ranger_rapidFire_durationMin;
		public static ConfigEntry<float> al_svr_ranger_rapidFire_durationMax;
		public static ConfigEntry<float> al_svr_ranger_rapidFire_drawPercentMin;
		public static ConfigEntry<float> al_svr_ranger_rapidFire_drawPercentMax;
		public static ConfigEntry<float> al_svr_ranger_rapidFire_attackSpeedMultiplierMin;
		public static ConfigEntry<float> al_svr_ranger_rapidFire_attackSpeedMultiplierMax;
		public static ConfigEntry<float> al_svr_ranger_rapidFire_damageReductionMin;
		public static ConfigEntry<float> al_svr_ranger_rapidFire_damageReductionMax;

		public static ConfigEntry<float> al_svr_ranger_rangerMark_staminaCost;
		public static ConfigEntry<float> al_svr_ranger_rangerMark_cooldown;
		public static ConfigEntry<float> al_svr_ranger_rangerMark_durationMin;
		public static ConfigEntry<float> al_svr_ranger_rangerMark_durationMax;
		public static ConfigEntry<float> al_svr_ranger_rangerMark_damageMultiplierMin;
		public static ConfigEntry<float> al_svr_ranger_rangerMark_damageMultiplierMax;

		public static ConfigEntry<float> al_svr_ranger_bowSpecialist_damageMultiplierMin;
		public static ConfigEntry<float> al_svr_ranger_bowSpecialist_damageMultiplierMax;
		public static ConfigEntry<float> al_svr_ranger_bowSpecialist_velocityMultiplierMin;
		public static ConfigEntry<float> al_svr_ranger_bowSpecialist_velocityMultiplierMax;

		public static ConfigEntry<float> al_svr_ranger_longstrider_runnningStaminaDrainReductionMin;
		public static ConfigEntry<float> al_svr_ranger_longstrider_runnningStaminaDrainReductionMax;
		public static ConfigEntry<float> al_svr_ranger_longstrider_jumpingStaminaDrainReductionMin;
		public static ConfigEntry<float> al_svr_ranger_longstrider_jumpingStaminaDrainReductionMax;

		public static ConfigEntry<float> al_svr_ranger_speedBurst_cooldown;
		public static ConfigEntry<float> al_svr_ranger_speedBurst_durationMin;
		public static ConfigEntry<float> al_svr_ranger_speedBurst_durationMax;
		public static ConfigEntry<float> al_svr_ranger_speedBurst_speedMultiplierMin;
		public static ConfigEntry<float> al_svr_ranger_speedBurst_speedMultiplierMax;

		public static ConfigEntry<float> al_svr_ranger_goForTheEyes_damageMultiplierMin;
		public static ConfigEntry<float> al_svr_ranger_goForTheEyes_damageMultiplierMax;

		public static ConfigEntry<float> al_svr_ranger_elementalTouch_percentChanceMin;
		public static ConfigEntry<float> al_svr_ranger_elementalTouch_percentChanceMax;
		public static ConfigEntry<float> al_svr_ranger_elementalTouch_damagePercentMin;
		public static ConfigEntry<float> al_svr_ranger_elementalTouch_damagePercentMax;

		public static ConfigEntry<float> al_svr_ranger_ammoSaver_regainChancePercent;

		public static void InitializeConfig(ConfigFile config)
		{
			al_svr_ranger_explosiveArrow_staminaCost = config.Bind("Ranger", "al_svr_ranger_explosiveArrow_staminaCost", 25f);
			al_svr_ranger_explosiveArrow_cooldown = config.Bind("Ranger", "al_svr_ranger_explosiveArrow_cooldown", 60f);
			al_svr_ranger_explosiveArrow_duration = config.Bind("Ranger", "al_svr_ranger_explosiveArrow_duration", 10f);
			al_svr_ranger_explosiveArrow_radius = config.Bind("Ranger", "al_svr_ranger_explosiveArrow_radius", 6f);
			al_svr_ranger_explosiveArrow_damageMin = config.Bind("Ranger", "al_svr_ranger_explosiveArrow_damageMin", 1.1f);
			al_svr_ranger_explosiveArrow_damageMax = config.Bind("Ranger", "al_svr_ranger_explosiveArrow_damageMax", 1.5f);
			al_svr_ranger_explosiveArrow_pushForce = config.Bind("Ranger", "al_svr_ranger_explosiveArrow_pushForce", 25f);

			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_explosiveArrow_staminaCost", al_svr_ranger_explosiveArrow_staminaCost.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_explosiveArrow_cooldown", al_svr_ranger_explosiveArrow_cooldown.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_explosiveArrow_duration", al_svr_ranger_explosiveArrow_duration.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_explosiveArrow_radius", al_svr_ranger_explosiveArrow_radius.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_explosiveArrow_damageMin", al_svr_ranger_explosiveArrow_damageMin.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_explosiveArrow_damageMax", al_svr_ranger_explosiveArrow_damageMax.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_explosiveArrow_pushForce", al_svr_ranger_explosiveArrow_pushForce.Value);

			al_svr_ranger_shadowStalk_staminaCost = config.Bind("Ranger", "al_svr_ranger_shadowStalk_staminaCost", 35f);
			al_svr_ranger_shadowStalk_cooldown = config.Bind("Ranger", "al_svr_ranger_shadowStalk_cooldown", 180f);
			al_svr_ranger_shadowStalk_durationMin = config.Bind("Ranger", "al_svr_ranger_shadowStalk_durationMin", 5f);
			al_svr_ranger_shadowStalk_durationMax = config.Bind("Ranger", "al_svr_ranger_shadowStalk_durationMax", 15f);
			al_svr_ranger_shadowStalk_speedDurationMin = config.Bind("Ranger", "al_svr_ranger_shadowStalk_speedDurationMin", 2f);
			al_svr_ranger_shadowStalk_speedDurationMax = config.Bind("Ranger", "al_svr_ranger_shadowStalk_speedDurationMax", 5f);
			al_svr_ranger_shadowStalk_speedMultiplierMin = config.Bind("Ranger", "al_svr_ranger_shadowStalk_speedMultiplierMin", 1.2f);
			al_svr_ranger_shadowStalk_speedMultiplierMax = config.Bind("Ranger", "al_svr_ranger_shadowStalk_speedMultiplierMax", 1.2f);
			al_svr_ranger_shadowStalk_crouchSpeedMultiplierMin = config.Bind("Ranger", "al_svr_ranger_shadowStalk_crouchSpeedMultiplierMin", 1.2f);
			al_svr_ranger_shadowStalk_crouchSpeedMultiplierMax = config.Bind("Ranger", "al_svr_ranger_shadowStalk_crouchSpeedMultiplierMax", 1.5f);

			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_shadowStalk_staminaCost", al_svr_ranger_shadowStalk_staminaCost.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_shadowStalk_cooldown", al_svr_ranger_shadowStalk_cooldown.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_shadowStalk_durationMin", al_svr_ranger_shadowStalk_durationMin.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_shadowStalk_durationMax", al_svr_ranger_shadowStalk_durationMax.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_shadowStalk_speedDurationMin", al_svr_ranger_shadowStalk_speedDurationMin.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_shadowStalk_speedDurationMax", al_svr_ranger_shadowStalk_speedDurationMax.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_shadowStalk_speedMultiplierMin", al_svr_ranger_shadowStalk_speedMultiplierMin.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_shadowStalk_speedMultiplierMax", al_svr_ranger_shadowStalk_speedMultiplierMax.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_shadowStalk_crouchSpeedMultiplierMin", al_svr_ranger_shadowStalk_crouchSpeedMultiplierMin.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_shadowStalk_crouchSpeedMultiplierMax", al_svr_ranger_shadowStalk_crouchSpeedMultiplierMax.Value);

			al_svr_ranger_rapidFire_staminaCost = config.Bind("Ranger", "al_svr_ranger_rapidFire_staminaCost", 50f);
			al_svr_ranger_rapidFire_cooldown = config.Bind("Ranger", "al_svr_ranger_rapidFire_cooldown", 300f);
			al_svr_ranger_rapidFire_durationMin = config.Bind("Ranger", "al_svr_ranger_rapidFire_durationMin", 3f);
			al_svr_ranger_rapidFire_durationMax = config.Bind("Ranger", "al_svr_ranger_rapidFire_durationMax", 5f);
			al_svr_ranger_rapidFire_drawPercentMin = config.Bind("Ranger", "al_svr_ranger_rapidFire_drawPercentMin", .75f);
			al_svr_ranger_rapidFire_drawPercentMax = config.Bind("Ranger", "al_svr_ranger_rapidFire_drawPercentMax", .9f);
			al_svr_ranger_rapidFire_attackSpeedMultiplierMin = config.Bind("Ranger", "al_svr_ranger_rapidFire_attackSpeedMultiplierMin", 1.05f);
			al_svr_ranger_rapidFire_attackSpeedMultiplierMax = config.Bind("Ranger", "al_svr_ranger_rapidFire_attackSpeedMultiplierMax", 1.33f);
			al_svr_ranger_rapidFire_damageReductionMin = config.Bind("Ranger", "al_svr_ranger_rapidFire_damageReductionMin", .5f);
			al_svr_ranger_rapidFire_damageReductionMax = config.Bind("Ranger", "al_svr_ranger_rapidFire_damageReductionMax", .2f);

			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_rapidFire_staminaCost", al_svr_ranger_rapidFire_staminaCost.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_rapidFire_cooldown", al_svr_ranger_shadowStalk_cooldown.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_rapidFire_durationMin", al_svr_ranger_rapidFire_durationMin.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_rapidFire_durationMax", al_svr_ranger_rapidFire_durationMax.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_rapidFire_drawPercentMin", al_svr_ranger_rapidFire_drawPercentMin.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_rapidFire_drawPercentMax", al_svr_ranger_rapidFire_drawPercentMax.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_rapidFire_attackSpeedMultiplierMin", al_svr_ranger_rapidFire_attackSpeedMultiplierMin.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_rapidFire_attackSpeedMultiplierMax", al_svr_ranger_rapidFire_attackSpeedMultiplierMax.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_rapidFire_damageReductionMin", al_svr_ranger_rapidFire_damageReductionMin.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_rapidFire_damageReductionMax", al_svr_ranger_rapidFire_damageReductionMax.Value);

			al_svr_ranger_rangerMark_staminaCost = config.Bind("Ranger", "al_svr_ranger_rangerMark_staminaCost", 75f);
			al_svr_ranger_rangerMark_cooldown = config.Bind("Ranger", "al_svr_ranger_rangerMark_cooldown", 600f);
			al_svr_ranger_rangerMark_durationMin = config.Bind("Ranger", "al_svr_ranger_rangerMark_durationMin", 10f);
			al_svr_ranger_rangerMark_durationMax = config.Bind("Ranger", "al_svr_ranger_rangerMark_durationMax", 10f);
			al_svr_ranger_rangerMark_damageMultiplierMin = config.Bind("Ranger", "al_svr_ranger_rangerMark_damageMultiplierMin", 1.5f);
			al_svr_ranger_rangerMark_damageMultiplierMax = config.Bind("Ranger", "al_svr_ranger_rangerMark_damageMultiplierMax", 1.5f);

			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_rangerMark_staminaCost", al_svr_ranger_rangerMark_staminaCost.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_rangerMark_cooldown", al_svr_ranger_rangerMark_cooldown.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_rangerMark_durationMin", al_svr_ranger_rangerMark_durationMin.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_rangerMark_durationMax", al_svr_ranger_rangerMark_durationMax.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_rangerMark_damageMultiplierMin", al_svr_ranger_rangerMark_damageMultiplierMin.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_rangerMark_damageMultiplierMax", al_svr_ranger_rangerMark_damageMultiplierMax.Value);

			al_svr_ranger_bowSpecialist_damageMultiplierMin = config.Bind("Ranger", "al_svr_ranger_bowSpecialist_damageMultiplierMin", 1.05f);
			al_svr_ranger_bowSpecialist_damageMultiplierMax = config.Bind("Ranger", "al_svr_ranger_bowSpecialist_damageMultiplierMax", 1.25f);
			al_svr_ranger_bowSpecialist_velocityMultiplierMin = config.Bind("Ranger", "al_svr_ranger_bowSpecialist_velocityMultiplierMin", 1.05f);
			al_svr_ranger_bowSpecialist_velocityMultiplierMax = config.Bind("Ranger", "al_svr_ranger_bowSpecialist_velocityMultiplierMax", 1.5f);

			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_bowSpecialist_damageMultiplierMin", al_svr_ranger_bowSpecialist_damageMultiplierMin.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_bowSpecialist_damageMultiplierMax", al_svr_ranger_bowSpecialist_damageMultiplierMax.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_bowSpecialist_velocityMultiplierMin", al_svr_ranger_bowSpecialist_velocityMultiplierMin.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_bowSpecialist_velocityMultiplierMax", al_svr_ranger_bowSpecialist_velocityMultiplierMax.Value);

			al_svr_ranger_longstrider_runnningStaminaDrainReductionMin = config.Bind("Ranger", "al_svr_ranger_longstrider_runnningStaminaDrainReductionMin", .05f);
			al_svr_ranger_longstrider_runnningStaminaDrainReductionMax = config.Bind("Ranger", "al_svr_ranger_longstrider_runnningStaminaDrainReductionMax", .25f);
			al_svr_ranger_longstrider_jumpingStaminaDrainReductionMin = config.Bind("Ranger", "al_svr_ranger_longstrider_jumpingStaminaDrainReductionMin", .05f);
			al_svr_ranger_longstrider_jumpingStaminaDrainReductionMax = config.Bind("Ranger", "al_svr_ranger_longstrider_jumpingStaminaDrainReductionMax", .25f);

			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_longstrider_runnningStaminaDrainReductionMin", al_svr_ranger_longstrider_runnningStaminaDrainReductionMin.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_longstrider_runnningStaminaDrainReductionMax", al_svr_ranger_longstrider_runnningStaminaDrainReductionMax.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_longstrider_jumpingStaminaDrainReductionMin", al_svr_ranger_longstrider_jumpingStaminaDrainReductionMin.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_longstrider_jumpingStaminaDrainReductionMax", al_svr_ranger_longstrider_jumpingStaminaDrainReductionMax.Value);

			al_svr_ranger_speedBurst_cooldown = config.Bind("Ranger", "al_svr_ranger_speedBurst_cooldown", 60f);
			al_svr_ranger_speedBurst_durationMin = config.Bind("Ranger", "al_svr_ranger_speedBurst_durationMin", 1f);
			al_svr_ranger_speedBurst_durationMax = config.Bind("Ranger", "al_svr_ranger_speedBurst_durationMax", 3f);
			al_svr_ranger_speedBurst_speedMultiplierMin = config.Bind("Ranger", "al_svr_ranger_speedBurst_speedMultiplierMin", 1.05f);
			al_svr_ranger_speedBurst_speedMultiplierMax = config.Bind("Ranger", "al_svr_ranger_speedBurst_speedMultiplierMax", 1.25f);

			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_speedBurst_cooldown", al_svr_ranger_speedBurst_cooldown.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_speedBurst_durationMin", al_svr_ranger_speedBurst_durationMin.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_speedBurst_durationMax", al_svr_ranger_speedBurst_durationMax.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_speedBurst_speedMultiplierMin", al_svr_ranger_speedBurst_speedMultiplierMin.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_speedBurst_speedMultiplierMax", al_svr_ranger_speedBurst_speedMultiplierMax.Value);

			al_svr_ranger_goForTheEyes_damageMultiplierMin = config.Bind("Ranger", "al_svr_ranger_goForTheEyes_damageMultiplierMin", 1.25f);
			al_svr_ranger_goForTheEyes_damageMultiplierMax = config.Bind("Ranger", "al_svr_ranger_goForTheEyes_damageMultiplierMax", 1.5f);

			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_goForTheEyes_damageMultiplierMin", al_svr_ranger_goForTheEyes_damageMultiplierMin.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_goForTheEyes_damageMultiplierMax", al_svr_ranger_goForTheEyes_damageMultiplierMax.Value);

			al_svr_ranger_elementalTouch_percentChanceMin = config.Bind("Ranger", "al_svr_ranger_elementalTouch_percentChanceMin", .1f);
			al_svr_ranger_elementalTouch_percentChanceMax = config.Bind("Ranger", "al_svr_ranger_elementalTouch_percentChanceMax", .25f);
			al_svr_ranger_elementalTouch_damagePercentMin = config.Bind("Ranger", "al_svr_ranger_elementalTouch_damagePercentMin", .1f);
			al_svr_ranger_elementalTouch_damagePercentMax = config.Bind("Ranger", "al_svr_ranger_elementalTouch_damagePercentMax", .25f);

			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_elementalTouch_percentChanceMin", al_svr_ranger_elementalTouch_percentChanceMin.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_elementalTouch_percentChanceMax", al_svr_ranger_elementalTouch_percentChanceMax.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_elementalTouch_damagePercentMin", al_svr_ranger_elementalTouch_damagePercentMin.Value);
			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_elementalTouch_damagePercentMax", al_svr_ranger_elementalTouch_damagePercentMax.Value);

			al_svr_ranger_ammoSaver_regainChancePercent = config.Bind("Ranger", "al_svr_ranger_ammoSaver_regainChancePercent", 50f);

			GlobalConfigs_Ranger.ConfigStrings.Add("al_svr_ranger_ammoSaver_regainChancePercent", al_svr_ranger_ammoSaver_regainChancePercent.Value);
		}
	}
}
