using System.Collections.Generic;

namespace AsgardLegacy
{
	class GlobalConfigs_Sentinel
	{
		public static Dictionary<string, float> ConfigStrings = new Dictionary<string, float>();

		public static float al_svr_sentinel_rejuvenatingStrike_staminaCost
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_rejuvenatingStrike_staminaCost") ? ConfigStrings["al_svr_sentinel_rejuvenatingStrike_staminaCost"] : 0f; } }
		public static float al_svr_sentinel_rejuvenatingStrike_cooldown
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_rejuvenatingStrike_cooldown") ? ConfigStrings["al_svr_sentinel_rejuvenatingStrike_cooldown"] : 0f; } }
		public static float al_svr_sentinel_rejuvenatingStrike_duration
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_rejuvenatingStrike_duration") ? ConfigStrings["al_svr_sentinel_rejuvenatingStrike_duration"] : 0f; } }
		public static float al_svr_sentinel_rejuvenatingStrike_radius
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_rejuvenatingStrike_radius") ? ConfigStrings["al_svr_sentinel_rejuvenatingStrike_radius"] : 0f; } }
		public static float al_svr_sentinel_rejuvenatingStrike_damageMultiplierMin
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_rejuvenatingStrike_damageMultiplierMin") ? ConfigStrings["al_svr_sentinel_rejuvenatingStrike_damageMultiplierMin"] : 0f; } }
		public static float al_svr_sentinel_rejuvenatingStrike_damageMultiplierMax
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_rejuvenatingStrike_damageMultiplierMax") ? ConfigStrings["al_svr_sentinel_rejuvenatingStrike_damageMultiplierMax"] : 0f; } }
		public static float al_svr_sentinel_rejuvenatingStrike_staminaMin
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_rejuvenatingStrike_staminaMin") ? ConfigStrings["al_svr_sentinel_rejuvenatingStrike_staminaMin"] : 0f; } }
		public static float al_svr_sentinel_rejuvenatingStrike_staminaMax
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_rejuvenatingStrike_staminaMax") ? ConfigStrings["al_svr_sentinel_rejuvenatingStrike_staminaMax"] : 0f; } }

		public static float al_svr_sentinel_mendingSpirit_staminaCost
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_mendingSpirit_staminaCost") ? ConfigStrings["al_svr_sentinel_mendingSpirit_staminaCost"] : 0f; } }
		public static float al_svr_sentinel_mendingSpirit_cooldown
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_mendingSpirit_cooldown") ? ConfigStrings["al_svr_sentinel_mendingSpirit_cooldown"] : 0f; } }
		public static float al_svr_sentinel_mendingSpirit_radius
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_mendingSpirit_radius") ? ConfigStrings["al_svr_sentinel_mendingSpirit_radius"] : 0f; } }
		public static float al_svr_sentinel_mendingSpirit_healingInterval
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_mendingSpirit_healingInterval") ? ConfigStrings["al_svr_sentinel_mendingSpirit_healingInterval"] : 0f; } }
		public static float al_svr_sentinel_mendingSpirit_healingDurationMin
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_mendingSpirit_healingDurationMin") ? ConfigStrings["al_svr_sentinel_mendingSpirit_healingDurationMin"] : 0f; } }
		public static float al_svr_sentinel_mendingSpirit_healingDurationMax
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_mendingSpirit_healingDurationMax") ? ConfigStrings["al_svr_sentinel_mendingSpirit_healingDurationMax"] : 0f; } }
		public static float al_svr_sentinel_mendingSpirit_healingAmountMin
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_mendingSpirit_healingAmountMin") ? ConfigStrings["al_svr_sentinel_mendingSpirit_healingAmountMin"] : 1f; } }
		public static float al_svr_sentinel_mendingSpirit_healingAmountMax
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_mendingSpirit_healingAmountMax") ? ConfigStrings["al_svr_sentinel_mendingSpirit_healingAmountMax"] : 1f; } }

		public static float al_svr_sentinel_chainsOfLight_staminaCost
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_chainsOfLight_staminaCost") ? ConfigStrings["al_svr_sentinel_chainsOfLight_staminaCost"] : 0f; } }
		public static float al_svr_sentinel_chainsOfLight_cooldown
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_chainsOfLight_cooldown") ? ConfigStrings["al_svr_sentinel_chainsOfLight_cooldown"] : 0f; } }
		public static float al_svr_sentinel_chainsOfLight_radius
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_chainsOfLight_radius") ? ConfigStrings["al_svr_sentinel_chainsOfLight_radius"] : 0f; } }
		public static float al_svr_sentinel_chainsOfLight_damageMultiplierMin
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_chainsOfLight_damageMultiplierMin") ? ConfigStrings["al_svr_sentinel_chainsOfLight_damageMultiplierMin"] : 0f; } }
		public static float al_svr_sentinel_chainsOfLight_damageMultiplierMax
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_chainsOfLight_damageMultiplierMax") ? ConfigStrings["al_svr_sentinel_chainsOfLight_damageMultiplierMax"] : 0f; } }
		public static float al_svr_sentinel_chainsOfLight_rootDurationMin
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_chainsOfLight_rootDurationMin") ? ConfigStrings["al_svr_sentinel_chainsOfLight_rootDurationMin"] : 0f; } }
		public static float al_svr_sentinel_chainsOfLight_rootDurationMax
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_chainsOfLight_rootDurationMax") ? ConfigStrings["al_svr_sentinel_chainsOfLight_rootDurationMax"] : 0f; } }
		
		public static float al_svr_sentinel_purgingFlames_staminaCost
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_purgingFlames_staminaCost") ? ConfigStrings["al_svr_sentinel_purgingFlames_staminaCost"] : 0f; } }
		public static float al_svr_sentinel_purgingFlames_cooldown
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_purgingFlames_cooldown") ? ConfigStrings["al_svr_sentinel_purgingFlames_cooldown"] : 0f; } }
		public static float al_svr_sentinel_purgingFlames_duration
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_purgingFlames_duration") ? ConfigStrings["al_svr_sentinel_purgingFlames_duration"] : 0f; } }
		public static float al_svr_sentinel_purgingFlames_interval
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_purgingFlames_interval") ? ConfigStrings["al_svr_sentinel_purgingFlames_interval"] : 0f; } }
		public static float al_svr_sentinel_purgingFlames_radius
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_purgingFlames_radius") ? ConfigStrings["al_svr_sentinel_purgingFlames_radius"] : 0f; } }
		public static float al_svr_sentinel_purgingFlames_healthOverTimeMin
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_purgingFlames_healthOverTimeMin") ? ConfigStrings["al_svr_sentinel_purgingFlames_healthOverTimeMin"] : 0f; } }
		public static float al_svr_sentinel_purgingFlames_healthOverTimeMax
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_purgingFlames_healthOverTimeMax") ? ConfigStrings["al_svr_sentinel_purgingFlames_healthOverTimeMax"] : 0f; } }
		public static float al_svr_sentinel_purgingFlames_staminaOverTimeMin
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_purgingFlames_staminaOverTimeMin") ? ConfigStrings["al_svr_sentinel_purgingFlames_staminaOverTimeMin"] : 0f; } }
		public static float al_svr_sentinel_purgingFlames_staminaOverTimeMax
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_purgingFlames_staminaOverTimeMax") ? ConfigStrings["al_svr_sentinel_purgingFlames_staminaOverTimeMax"] : 0f; } }
		public static float al_svr_sentinel_purgingFlames_damageMultiplierMin
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_purgingFlames_damageMultiplierMin") ? ConfigStrings["al_svr_sentinel_purgingFlames_damageMultiplierMin"] : 0f; } }
		public static float al_svr_sentinel_purgingFlames_damageMultiplierMax
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_purgingFlames_damageMultiplierMax") ? ConfigStrings["al_svr_sentinel_purgingFlames_damageMultiplierMax"] : 0f; } }

		public static float al_svr_sentinel_oneHandedMaster_damageBonusMin
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_oneHandedMaster_damageBonusMin") ? ConfigStrings["al_svr_sentinel_oneHandedMaster_damageBonusMin"] : 0f; } }
		public static float al_svr_sentinel_oneHandedMaster_damageBonusMax
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_oneHandedMaster_damageBonusMax") ? ConfigStrings["al_svr_sentinel_oneHandedMaster_damageBonusMax"] : 0f; } }
		public static float al_svr_sentinel_oneHandedMaster_staminaBonusMin
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_oneHandedMaster_staminaBonusMin") ? ConfigStrings["al_svr_sentinel_oneHandedMaster_staminaBonusMin"] : 0f; } }
		public static float al_svr_sentinel_oneHandedMaster_staminaBonusMax
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_oneHandedMaster_staminaBonusMax") ? ConfigStrings["al_svr_sentinel_oneHandedMaster_staminaBonusMax"] : 0f; } }
		
		public static float al_svr_sentinel_dwarvenFortitude_armorBonusMin
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_dwarvenFortitude_armorBonusMin") ? ConfigStrings["al_svr_sentinel_dwarvenFortitude_armorBonusMin"] : 0f; } }
		public static float al_svr_sentinel_dwarvenFortitude_armorBonusMax
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_dwarvenFortitude_armorBonusMax") ? ConfigStrings["al_svr_sentinel_dwarvenFortitude_armorBonusMax"] : 0f; } }
		public static float al_svr_sentinel_dwarvenFortitude_elementalResistMin
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_dwarvenFortitude_elementalResistMin") ? ConfigStrings["al_svr_sentinel_dwarvenFortitude_elementalResistMin"] : 0f; } }
		public static float al_svr_sentinel_dwarvenFortitude_elementalResistMax
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_dwarvenFortitude_elementalResistMax") ? ConfigStrings["al_svr_sentinel_dwarvenFortitude_elementalResistMax"] : 1f; } }

		public static float al_svr_sentinel_healersGift_cooldown
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_healersGift_cooldown") ? ConfigStrings["al_svr_sentinel_healersGift_cooldown"] : 0f; } }
		public static float al_svr_sentinel_healersGift_radius
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_healersGift_radius") ? ConfigStrings["al_svr_sentinel_healersGift_radius"] : 0f; } }
		public static float al_svr_sentinel_healersGift_duration
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_healersGift_duration") ? ConfigStrings["al_svr_sentinel_healersGift_duration"] : 0f; } }
		public static float al_svr_sentinel_healersGift_interval
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_healersGift_interval") ? ConfigStrings["al_svr_sentinel_healersGift_interval"] : 0f; } }
		public static float al_svr_sentinel_healersGift_healthOverTimeMin
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_healersGift_healthOverTimeMin") ? ConfigStrings["al_svr_sentinel_healersGift_healthOverTimeMin"] : 0f; } }
		public static float al_svr_sentinel_healersGift_healthOverTimeMax
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_healersGift_healthOverTimeMax") ? ConfigStrings["al_svr_sentinel_healersGift_healthOverTimeMax"] : 0f; } }

		public static float al_svr_sentinel_cleansingRoll_cooldown
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_cleansingRoll_cooldown") ? ConfigStrings["al_svr_sentinel_cleansingRoll_cooldown"] : 1f; } }

		public static float al_svr_sentinel_vengefulWave_cooldown
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_vengefulWave_cooldown") ? ConfigStrings["al_svr_sentinel_vengefulWave_cooldown"] : 0f; } }
		public static float al_svr_sentinel_vengefulWave_radius
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_vengefulWave_radius") ? ConfigStrings["al_svr_sentinel_vengefulWave_radius"] : 0f; } }
		public static float al_svr_sentinel_vengefulWave_hpPercentMin
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_vengefulWave_hpPercentMin") ? ConfigStrings["al_svr_sentinel_vengefulWave_hpPercentMin"] : 0f; } }
		public static float al_svr_sentinel_vengefulWave_hpPercentMax
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_vengefulWave_hpPercentMax") ? ConfigStrings["al_svr_sentinel_vengefulWave_hpPercentMax"] : 0f; } }

		public static float al_svr_sentinel_powerfulBuild_reduceWeightPercent
		{ get { return ConfigStrings.ContainsKey("al_svr_sentinel_powerfulBuild_reduceWeightPercent") ? ConfigStrings["al_svr_sentinel_powerfulBuild_reduceWeightPercent"] : 0f; } }
	}
}