using BepInEx.Configuration;

namespace AsgardLegacy
{
	class Configs_Sentinel
	{
		public static ConfigEntry<float> al_svr_sentinel_rejuvenatingStrike_staminaCost;
		public static ConfigEntry<float> al_svr_sentinel_rejuvenatingStrike_cooldown;
		public static ConfigEntry<float> al_svr_sentinel_rejuvenatingStrike_duration;
		public static ConfigEntry<float> al_svr_sentinel_rejuvenatingStrike_radius;
		public static ConfigEntry<float> al_svr_sentinel_rejuvenatingStrike_damageMultiplierMin;
		public static ConfigEntry<float> al_svr_sentinel_rejuvenatingStrike_damageMultiplierMax;
		public static ConfigEntry<float> al_svr_sentinel_rejuvenatingStrike_staminaMin;
		public static ConfigEntry<float> al_svr_sentinel_rejuvenatingStrike_staminaMax;

		public static ConfigEntry<float> al_svr_sentinel_mendingSpirit_staminaCost;
		public static ConfigEntry<float> al_svr_sentinel_mendingSpirit_cooldown;
		public static ConfigEntry<float> al_svr_sentinel_mendingSpirit_radius;
		public static ConfigEntry<float> al_svr_sentinel_mendingSpirit_healingInterval;
		public static ConfigEntry<float> al_svr_sentinel_mendingSpirit_healingDurationMin;
		public static ConfigEntry<float> al_svr_sentinel_mendingSpirit_healingDurationMax;
		public static ConfigEntry<float> al_svr_sentinel_mendingSpirit_healingAmountMin;
		public static ConfigEntry<float> al_svr_sentinel_mendingSpirit_healingAmountMax;

		public static ConfigEntry<float> al_svr_sentinel_chainsOfLight_staminaCost;
		public static ConfigEntry<float> al_svr_sentinel_chainsOfLight_cooldown;
		public static ConfigEntry<float> al_svr_sentinel_chainsOfLight_radius;
		public static ConfigEntry<float> al_svr_sentinel_chainsOfLight_damageMultiplierMin;
		public static ConfigEntry<float> al_svr_sentinel_chainsOfLight_damageMultiplierMax;
		public static ConfigEntry<float> al_svr_sentinel_chainsOfLight_rootDurationMin;
		public static ConfigEntry<float> al_svr_sentinel_chainsOfLight_rootDurationMax;

		public static ConfigEntry<float> al_svr_sentinel_purgingFlames_staminaCost;
		public static ConfigEntry<float> al_svr_sentinel_purgingFlames_cooldown;
		public static ConfigEntry<float> al_svr_sentinel_purgingFlames_duration;
		public static ConfigEntry<float> al_svr_sentinel_purgingFlames_interval;
		public static ConfigEntry<float> al_svr_sentinel_purgingFlames_radius;
		public static ConfigEntry<float> al_svr_sentinel_purgingFlames_healthOverTimeMin;
		public static ConfigEntry<float> al_svr_sentinel_purgingFlames_healthOverTimeMax;
		public static ConfigEntry<float> al_svr_sentinel_purgingFlames_staminaOverTimeMin;
		public static ConfigEntry<float> al_svr_sentinel_purgingFlames_staminaOverTimeMax;
		public static ConfigEntry<float> al_svr_sentinel_purgingFlames_damageMultiplierMin;
		public static ConfigEntry<float> al_svr_sentinel_purgingFlames_damageMultiplierMax;

		public static ConfigEntry<float> al_svr_sentinel_oneHandedMaster_damageBonusMin;
		public static ConfigEntry<float> al_svr_sentinel_oneHandedMaster_damageBonusMax;
		public static ConfigEntry<float> al_svr_sentinel_oneHandedMaster_staminaBonusMin;
		public static ConfigEntry<float> al_svr_sentinel_oneHandedMaster_staminaBonusMax;

		public static ConfigEntry<float> al_svr_sentinel_dwarvenFortitude_armorBonusMin;
		public static ConfigEntry<float> al_svr_sentinel_dwarvenFortitude_armorBonusMax;
		public static ConfigEntry<float> al_svr_sentinel_dwarvenFortitude_elementalResistMin;
		public static ConfigEntry<float> al_svr_sentinel_dwarvenFortitude_elementalResistMax;

		public static ConfigEntry<float> al_svr_sentinel_healersGift_cooldown;
		public static ConfigEntry<float> al_svr_sentinel_healersGift_radius;
		public static ConfigEntry<float> al_svr_sentinel_healersGift_duration;
		public static ConfigEntry<float> al_svr_sentinel_healersGift_interval;
		public static ConfigEntry<float> al_svr_sentinel_healersGift_healthOverTimeMin;
		public static ConfigEntry<float> al_svr_sentinel_healersGift_healthOverTimeMax;

		public static ConfigEntry<float> al_svr_sentinel_cleansingRoll_cooldown;

		public static ConfigEntry<float> al_svr_sentinel_vengefulWave_cooldown;
		public static ConfigEntry<float> al_svr_sentinel_vengefulWave_radius;
		public static ConfigEntry<float> al_svr_sentinel_vengefulWave_hpPercentMin;
		public static ConfigEntry<float> al_svr_sentinel_vengefulWave_hpPercentMax;

		public static ConfigEntry<float> al_svr_sentinel_powerfulBuild_reduceWeightPercent;

		public static void InitializeConfig(ConfigFile config)
		{
			al_svr_sentinel_rejuvenatingStrike_staminaCost = config.Bind("Sentinel", "al_svr_sentinel_rejuvenatingStrike_staminaCost", 25f);
			al_svr_sentinel_rejuvenatingStrike_cooldown = config.Bind("Sentinel", "al_svr_sentinel_rejuvenatingStrike_cooldown", 60f);
			al_svr_sentinel_rejuvenatingStrike_duration = config.Bind("Sentinel", "al_svr_sentinel_rejuvenatingStrike_duration", 10f);
			al_svr_sentinel_rejuvenatingStrike_radius = config.Bind("Sentinel", "al_svr_sentinel_rejuvenatingStrike_radius", 3f);
			al_svr_sentinel_rejuvenatingStrike_damageMultiplierMin = config.Bind("Sentinel", "al_svr_sentinel_rejuvenatingStrike_damageMultiplierMin", .1f);
			al_svr_sentinel_rejuvenatingStrike_damageMultiplierMax = config.Bind("Sentinel", "al_svr_sentinel_rejuvenatingStrike_damageMultiplierMax", .5f);
			al_svr_sentinel_rejuvenatingStrike_staminaMin = config.Bind("Sentinel", "al_svr_sentinel_rejuvenatingStrike_staminaMin", 50f);
			al_svr_sentinel_rejuvenatingStrike_staminaMax = config.Bind("Sentinel", "al_svr_sentinel_rejuvenatingStrike_staminaMax", 100f);

			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_rejuvenatingStrike_staminaCost", al_svr_sentinel_rejuvenatingStrike_staminaCost.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_rejuvenatingStrike_cooldown", al_svr_sentinel_rejuvenatingStrike_cooldown.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_rejuvenatingStrike_duration", al_svr_sentinel_rejuvenatingStrike_duration.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_rejuvenatingStrike_radius", al_svr_sentinel_rejuvenatingStrike_radius.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_rejuvenatingStrike_damageMultiplierMin", al_svr_sentinel_rejuvenatingStrike_damageMultiplierMin.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_rejuvenatingStrike_damageMultiplierMax", al_svr_sentinel_rejuvenatingStrike_damageMultiplierMax.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_rejuvenatingStrike_staminaMin", al_svr_sentinel_rejuvenatingStrike_staminaMin.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_rejuvenatingStrike_staminaMax", al_svr_sentinel_rejuvenatingStrike_staminaMax.Value);

			al_svr_sentinel_mendingSpirit_staminaCost = config.Bind("Sentinel", "al_svr_sentinel_mendingSpirit_staminaCost", 35f);
			al_svr_sentinel_mendingSpirit_cooldown = config.Bind("Sentinel", "al_svr_sentinel_mendingSpirit_cooldown", 180f);
			al_svr_sentinel_mendingSpirit_radius = config.Bind("Sentinel", "al_svr_sentinel_mendingSpirit_radius", 5f);
			al_svr_sentinel_mendingSpirit_healingInterval = config.Bind("Sentinel", "al_svr_sentinel_mendingSpirit_healingInterval", 1f);
			al_svr_sentinel_mendingSpirit_healingDurationMin = config.Bind("Sentinel", "al_svr_sentinel_mendingSpirit_healingDurationMin", 3f);
			al_svr_sentinel_mendingSpirit_healingDurationMax = config.Bind("Sentinel", "al_svr_sentinel_mendingSpirit_healingDurationMax", 5f);
			al_svr_sentinel_mendingSpirit_healingAmountMin = config.Bind("Sentinel", "al_svr_sentinel_mendingSpirit_healingAmountMin", 25f);
			al_svr_sentinel_mendingSpirit_healingAmountMax = config.Bind("Sentinel", "al_svr_sentinel_mendingSpirit_healingAmountMax", 50f);

			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_mendingSpirit_staminaCost", al_svr_sentinel_mendingSpirit_staminaCost.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_mendingSpirit_cooldown", al_svr_sentinel_mendingSpirit_cooldown.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_mendingSpirit_radius", al_svr_sentinel_mendingSpirit_radius.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_mendingSpirit_healingInterval", al_svr_sentinel_mendingSpirit_healingInterval.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_mendingSpirit_healingDurationMin", al_svr_sentinel_mendingSpirit_healingDurationMin.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_mendingSpirit_healingDurationMax", al_svr_sentinel_mendingSpirit_healingDurationMax.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_mendingSpirit_healingAmountMin", al_svr_sentinel_mendingSpirit_healingAmountMin.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_mendingSpirit_healingAmountMax", al_svr_sentinel_mendingSpirit_healingAmountMax.Value);

			al_svr_sentinel_chainsOfLight_staminaCost = config.Bind("Sentinel", "al_svr_sentinel_chainsOfLight_staminaCost", 50f);
			al_svr_sentinel_chainsOfLight_cooldown = config.Bind("Sentinel", "al_svr_sentinel_chainsOfLight_cooldown", 300f);
			al_svr_sentinel_chainsOfLight_radius = config.Bind("Sentinel", "al_svr_sentinel_chainsOfLight_radius", 6f);
			al_svr_sentinel_chainsOfLight_damageMultiplierMin = config.Bind("Sentinel", "al_svr_sentinel_chainsOfLight_damageMultiplierMin", 1.05f);
			al_svr_sentinel_chainsOfLight_damageMultiplierMax = config.Bind("Sentinel", "al_svr_sentinel_chainsOfLight_damageMultiplierMax", 1.25f);
			al_svr_sentinel_chainsOfLight_rootDurationMin = config.Bind("Sentinel", "al_svr_sentinel_chainsOfLight_rootDurationMin", 2f);
			al_svr_sentinel_chainsOfLight_rootDurationMax = config.Bind("Sentinel", "al_svr_sentinel_chainsOfLight_rootDurationMax", 5f);

			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_chainsOfLight_staminaCost", al_svr_sentinel_chainsOfLight_staminaCost.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_chainsOfLight_cooldown", al_svr_sentinel_mendingSpirit_cooldown.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_chainsOfLight_radius", al_svr_sentinel_chainsOfLight_radius.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_chainsOfLight_damageMultiplierMin", al_svr_sentinel_chainsOfLight_damageMultiplierMin.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_chainsOfLight_damageMultiplierMax", al_svr_sentinel_chainsOfLight_damageMultiplierMax.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_chainsOfLight_rootDurationMin", al_svr_sentinel_chainsOfLight_rootDurationMin.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_chainsOfLight_rootDurationMax", al_svr_sentinel_chainsOfLight_rootDurationMax.Value);

			al_svr_sentinel_purgingFlames_staminaCost = config.Bind("Sentinel", "al_svr_sentinel_purgingFlames_staminaCost", 75f);
			al_svr_sentinel_purgingFlames_cooldown = config.Bind("Sentinel", "al_svr_sentinel_purgingFlames_cooldown", 600f);
			al_svr_sentinel_purgingFlames_duration = config.Bind("Sentinel", "al_svr_sentinel_purgingFlames_duration", 5f);
			al_svr_sentinel_purgingFlames_interval = config.Bind("Sentinel", "al_svr_sentinel_purgingFlames_interval", 1f);
			al_svr_sentinel_purgingFlames_radius = config.Bind("Sentinel", "al_svr_sentinel_purgingFlames_radius", 10f);
			al_svr_sentinel_purgingFlames_healthOverTimeMin = config.Bind("Sentinel", "al_svr_sentinel_purgingFlames_healthOverTimeMin", 100f);
			al_svr_sentinel_purgingFlames_healthOverTimeMax = config.Bind("Sentinel", "al_svr_sentinel_purgingFlames_healthOverTimeMax", 100f);
			al_svr_sentinel_purgingFlames_staminaOverTimeMin = config.Bind("Sentinel", "al_svr_sentinel_purgingFlames_staminaOverTimeMin", 50f);
			al_svr_sentinel_purgingFlames_staminaOverTimeMax = config.Bind("Sentinel", "al_svr_sentinel_purgingFlames_staminaOverTimeMax", 50f);
			al_svr_sentinel_purgingFlames_damageMultiplierMin = config.Bind("Sentinel", "al_svr_sentinel_purgingFlames_damageMultiplierMin", 1.5f);
			al_svr_sentinel_purgingFlames_damageMultiplierMax = config.Bind("Sentinel", "al_svr_sentinel_purgingFlames_damageMultiplierMax", 1.5f);

			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_purgingFlames_staminaCost", al_svr_sentinel_purgingFlames_staminaCost.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_purgingFlames_cooldown", al_svr_sentinel_purgingFlames_cooldown.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_purgingFlames_duration", al_svr_sentinel_purgingFlames_duration.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_purgingFlames_interval", al_svr_sentinel_purgingFlames_interval.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_purgingFlames_radius", al_svr_sentinel_purgingFlames_radius.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_purgingFlames_healthOverTimeMin", al_svr_sentinel_purgingFlames_healthOverTimeMin.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_purgingFlames_healthOverTimeMax", al_svr_sentinel_purgingFlames_healthOverTimeMax.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_purgingFlames_staminaOverTimeMin", al_svr_sentinel_purgingFlames_staminaOverTimeMin.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_purgingFlames_staminaOverTimeMax", al_svr_sentinel_purgingFlames_staminaOverTimeMax.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_purgingFlames_damageMultiplierMin", al_svr_sentinel_purgingFlames_damageMultiplierMin.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_purgingFlames_damageMultiplierMax", al_svr_sentinel_purgingFlames_damageMultiplierMax.Value);

			al_svr_sentinel_oneHandedMaster_damageBonusMin = config.Bind("Sentinel", "al_svr_sentinel_oneHandedMaster_damageBonusMin", .1f);
			al_svr_sentinel_oneHandedMaster_damageBonusMax = config.Bind("Sentinel", "al_svr_sentinel_oneHandedMaster_damageBonusMax", .5f);
			al_svr_sentinel_oneHandedMaster_staminaBonusMin = config.Bind("Sentinel", "al_svr_sentinel_oneHandedMaster_staminaBonusMin", .9f);
			al_svr_sentinel_oneHandedMaster_staminaBonusMax = config.Bind("Sentinel", "al_svr_sentinel_oneHandedMaster_staminaBonusMax", .5f);

			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_oneHandedMaster_damageBonusMin", al_svr_sentinel_oneHandedMaster_damageBonusMin.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_oneHandedMaster_damageBonusMax", al_svr_sentinel_oneHandedMaster_damageBonusMax.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_oneHandedMaster_staminaBonusMin", al_svr_sentinel_oneHandedMaster_staminaBonusMin.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_oneHandedMaster_staminaBonusMax", al_svr_sentinel_oneHandedMaster_staminaBonusMax.Value);

			al_svr_sentinel_dwarvenFortitude_armorBonusMin = config.Bind("Sentinel", "al_svr_sentinel_dwarvenFortitude_armorBonusMin", 1.05f);
			al_svr_sentinel_dwarvenFortitude_armorBonusMax = config.Bind("Sentinel", "al_svr_sentinel_dwarvenFortitude_armorBonusMax", 1.25f);
			al_svr_sentinel_dwarvenFortitude_elementalResistMin = config.Bind("Sentinel", "al_svr_sentinel_dwarvenFortitude_elementalResistMin",.95f);
			al_svr_sentinel_dwarvenFortitude_elementalResistMax = config.Bind("Sentinel", "al_svr_sentinel_dwarvenFortitude_elementalResistMax", .75f);

			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_dwarvenFortitude_armorBonusMin", al_svr_sentinel_dwarvenFortitude_armorBonusMin.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_dwarvenFortitude_armorBonusMax", al_svr_sentinel_dwarvenFortitude_armorBonusMax.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_dwarvenFortitude_elementalResistMin", al_svr_sentinel_dwarvenFortitude_elementalResistMin.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_dwarvenFortitude_elementalResistMax", al_svr_sentinel_dwarvenFortitude_elementalResistMax.Value);

			al_svr_sentinel_healersGift_cooldown = config.Bind("Sentinel", "al_svr_sentinel_healersGift_cooldown", 120f);
			al_svr_sentinel_healersGift_radius = config.Bind("Sentinel", "al_svr_sentinel_healersGift_radius", 2f);
			al_svr_sentinel_healersGift_duration = config.Bind("Sentinel", "al_svr_sentinel_healersGift_duration", 3f);
			al_svr_sentinel_healersGift_interval = config.Bind("Sentinel", "al_svr_sentinel_healersGift_interval", 1f);
			al_svr_sentinel_healersGift_healthOverTimeMin = config.Bind("Sentinel", "al_svr_sentinel_healersGift_healthOverTimeMin", 10f);
			al_svr_sentinel_healersGift_healthOverTimeMax = config.Bind("Sentinel", "al_svr_sentinel_healersGift_healthOverTimeMax", 25f);

			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_healersGift_cooldown", al_svr_sentinel_healersGift_cooldown.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_healersGift_radius", al_svr_sentinel_healersGift_radius.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_healersGift_duration", al_svr_sentinel_healersGift_duration.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_healersGift_interval", al_svr_sentinel_healersGift_interval.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_healersGift_healthOverTimeMin", al_svr_sentinel_healersGift_healthOverTimeMin.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_healersGift_healthOverTimeMax", al_svr_sentinel_healersGift_healthOverTimeMax.Value);

			al_svr_sentinel_cleansingRoll_cooldown = config.Bind("Sentinel", "al_svr_sentinel_cleansingRoll_cooldown", 60f);

			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_cleansingRoll_cooldown", al_svr_sentinel_cleansingRoll_cooldown.Value);

			al_svr_sentinel_vengefulWave_cooldown = config.Bind("Sentinel", "al_svr_sentinel_vengefulWave_cooldown", 60f);
			al_svr_sentinel_vengefulWave_radius = config.Bind("Sentinel", "al_svr_sentinel_vengefulWave_radius", 6f);
			al_svr_sentinel_vengefulWave_hpPercentMin = config.Bind("Sentinel", "al_svr_sentinel_vengefulWave_hpPercentMin", .1f);
			al_svr_sentinel_vengefulWave_hpPercentMax = config.Bind("Sentinel", "al_svr_sentinel_vengefulWave_hpPercentMax", .25f);

			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_vengefulWave_cooldown", al_svr_sentinel_vengefulWave_cooldown.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_vengefulWave_radius", al_svr_sentinel_vengefulWave_radius.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_vengefulWave_hpPercentMin", al_svr_sentinel_vengefulWave_hpPercentMin.Value);
			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_vengefulWave_hpPercentMax", al_svr_sentinel_vengefulWave_hpPercentMax.Value);

			al_svr_sentinel_powerfulBuild_reduceWeightPercent = config.Bind("Sentinel", "al_svr_sentinel_powerfulBuild_reduceWeightPercent", .2f);

			GlobalConfigs_Sentinel.ConfigStrings.Add("al_svr_sentinel_powerfulBuild_reduceWeightPercent", al_svr_sentinel_powerfulBuild_reduceWeightPercent.Value);
		}
	}
}
