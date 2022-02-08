using System.Collections.Generic;

namespace AsgardLegacy
{
	class GlobalConfigs_Guardian
	{
		public static Dictionary<string, float> ConfigStrings = new Dictionary<string, float>();

		public static float al_svr_guardian_shatterFall_staminaCost
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_shatterFall_staminaCost") ? ConfigStrings["al_svr_guardian_shatterFall_staminaCost"] : 0f; } }
		public static float al_svr_guardian_shatterFall_cooldown
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_shatterFall_cooldown") ? ConfigStrings["al_svr_guardian_shatterFall_cooldown"] : 0f; } }
		public static float al_svr_guardian_shatterFall_damageMultiplierMin
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_shatterFall_damageMultiplierMin") ? ConfigStrings["al_svr_guardian_shatterFall_damageMultiplierMin"] : 0f; } }
		public static float al_svr_guardian_shatterFall_damageMultiplierMax
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_shatterFall_damageMultiplierMax") ? ConfigStrings["al_svr_guardian_shatterFall_damageMultiplierMax"] : 0f; } }
		public static float al_svr_guardian_shatterFall_radius
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_shatterFall_radius") ? ConfigStrings["al_svr_guardian_shatterFall_radius"] : 0f; } }
		public static float al_svr_guardian_shatterFall_altitudeMultiplierMin
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_shatterFall_altitudeMultiplierMin") ? ConfigStrings["al_svr_guardian_shatterFall_altitudeMultiplierMin"] : 0f; } }
		public static float al_svr_guardian_shatterFall_altitudeMultiplierMax
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_shatterFall_altitudeMultiplierMax") ? ConfigStrings["al_svr_guardian_shatterFall_altitudeMultiplierMax"] : 0f; } }
		public static float al_svr_guardian_shatterFall_pushForce
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_shatterFall_pushForce") ? ConfigStrings["al_svr_guardian_shatterFall_pushForce"] : 0f; } }
		public static float al_svr_guardian_shatterFall_fallDamageReductionMin
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_shatterFall_fallDamageReductionMin") ? ConfigStrings["al_svr_guardian_shatterFall_fallDamageReductionMin"] : 0f; } }
		public static float al_svr_guardian_shatterFall_fallDamageReductionMax
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_shatterFall_fallDamageReductionMax") ? ConfigStrings["al_svr_guardian_shatterFall_fallDamageReductionMax"] : 0f; } }

		public static float al_svr_guardian_aegis_staminaCost
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_aegis_staminaCost") ? ConfigStrings["al_svr_guardian_aegis_staminaCost"] : 0f; } }
		public static float al_svr_guardian_aegis_cooldown
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_aegis_cooldown") ? ConfigStrings["al_svr_guardian_aegis_cooldown"] : 0f; } }
		public static float al_svr_guardian_aegis_durationMin
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_aegis_durationMin") ? ConfigStrings["al_svr_guardian_aegis_durationMin"] : 0f; } }
		public static float al_svr_guardian_aegis_durationMax
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_aegis_durationMax") ? ConfigStrings["al_svr_guardian_aegis_durationMax"] : 0f; } }
		public static float al_svr_guardian_aegis_armorMultiplierMin
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_aegis_armorMultiplierMin") ? ConfigStrings["al_svr_guardian_aegis_armorMultiplierMin"] : 0f; } }
		public static float al_svr_guardian_aegis_armorMultiplierMax
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_aegis_armorMultiplierMax") ? ConfigStrings["al_svr_guardian_aegis_armorMultiplierMax"] : 0f; } }
		public static float al_svr_guardian_aegis_damageReductionMin
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_aegis_damageReductionMin") ? ConfigStrings["al_svr_guardian_aegis_damageReductionMin"] : 1f; } }
		public static float al_svr_guardian_aegis_damageReductionMax
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_aegis_damageReductionMax") ? ConfigStrings["al_svr_guardian_aegis_damageReductionMax"] : 1f; } }

		public static float al_svr_guardian_iceCrush_staminaCost
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_iceCrush_staminaCost") ? ConfigStrings["al_svr_guardian_iceCrush_staminaCost"] : 0f; } }
		public static float al_svr_guardian_iceCrush_cooldown
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_iceCrush_cooldown") ? ConfigStrings["al_svr_guardian_iceCrush_cooldown"] : 0f; } }
		public static float al_svr_guardian_iceCrush_damageMultiplierMin
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_iceCrush_damageMultiplierMin") ? ConfigStrings["al_svr_guardian_iceCrush_damageMultiplierMin"] : 0f; } }
		public static float al_svr_guardian_iceCrush_damageMultiplierMax
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_iceCrush_damageMultiplierMax") ? ConfigStrings["al_svr_guardian_iceCrush_damageMultiplierMax"] : 0f; } }
		public static float al_svr_guardian_iceCrush_radius
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_iceCrush_radius") ? ConfigStrings["al_svr_guardian_iceCrush_radius"] : 0f; } }
		
		public static float al_svr_guardian_retribution_staminaCost
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_retribution_staminaCost") ? ConfigStrings["al_svr_guardian_retribution_staminaCost"] : 0f; } }
		public static float al_svr_guardian_retribution_cooldown
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_retribution_cooldown") ? ConfigStrings["al_svr_guardian_retribution_cooldown"] : 0f; } }
		public static float al_svr_guardian_retribution_duration
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_retribution_duration") ? ConfigStrings["al_svr_guardian_retribution_duration"] : 0f; } }
		public static float al_svr_guardian_retribution_damageMultiplier
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_retribution_damageMultiplier") ? ConfigStrings["al_svr_guardian_retribution_damageMultiplier"] : 0f; } }
		public static float al_svr_guardian_retribution_pushForce
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_retribution_pushForce") ? ConfigStrings["al_svr_guardian_retribution_pushForce"] : 0f; } }
		public static float al_svr_guardian_retribution_radius
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_retribution_radius") ? ConfigStrings["al_svr_guardian_retribution_radius"] : 0f; } }

		public static float al_svr_guardian_forceOfNature_hpBonusMin
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_forceOfNature_hpBonusMin") ? ConfigStrings["al_svr_guardian_forceOfNature_hpBonusMin"] : 0f; } }
		public static float al_svr_guardian_forceOfNature_hpBonusMax
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_forceOfNature_hpBonusMax") ? ConfigStrings["al_svr_guardian_forceOfNature_hpBonusMax"] : 0f; } }
		public static float al_svr_guardian_forceOfNature_staminaBonusMin
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_forceOfNature_staminaBonusMin") ? ConfigStrings["al_svr_guardian_forceOfNature_staminaBonusMin"] : 0f; } }
		public static float al_svr_guardian_forceOfNature_staminaBonusMax
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_forceOfNature_staminaBonusMax") ? ConfigStrings["al_svr_guardian_forceOfNature_staminaBonusMax"] : 1f; } }

		public static float al_svr_guardian_bulwark_baseBlockPowerBonusMin
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_bulwark_baseBlockPowerBonusMin") ? ConfigStrings["al_svr_guardian_bulwark_baseBlockPowerBonusMin"] : 0f; } }
		public static float al_svr_guardian_bulwark_baseBlockPowerBonusMax
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_bulwark_baseBlockPowerBonusMax") ? ConfigStrings["al_svr_guardian_bulwark_baseBlockPowerBonusMax"] : 0f; } }
		public static float al_svr_guardian_bulwark_shieldCounterCooldown
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_bulwark_shieldCounterCooldown") ? ConfigStrings["al_svr_guardian_bulwark_shieldCounterCooldown"] : 0f; } }
		public static float al_svr_guardian_bulwark_shieldCounterDuration
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_bulwark_shieldCounterDuration") ? ConfigStrings["al_svr_guardian_bulwark_shieldCounterDuration"] : 0f; } }
		public static float al_svr_guardian_bulwark_shieldCounterTickInterval
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_bulwark_shieldCounterTickInterval") ? ConfigStrings["al_svr_guardian_bulwark_shieldCounterTickInterval"] : 0f; } }
		public static float al_svr_guardian_bulwark_shieldCounterStaminaRegenMin
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_bulwark_shieldCounterStaminaRegenMin") ? ConfigStrings["al_svr_guardian_bulwark_shieldCounterStaminaRegenMin"] : 0f; } }
		public static float al_svr_guardian_bulwark_shieldCounterStaminaRegenMax
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_bulwark_shieldCounterStaminaRegenMax") ? ConfigStrings["al_svr_guardian_bulwark_shieldCounterStaminaRegenMax"] : 0f; } }
		public static float al_svr_guardian_bulwark_towerShieldBlockPowerMin
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_bulwark_towerShieldBlockPowerMin") ? ConfigStrings["al_svr_guardian_bulwark_towerShieldBlockPowerMin"] : 0f; } }
		public static float al_svr_guardian_bulwark_towerShieldBlockPowerMax
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_bulwark_towerShieldBlockPowerMax") ? ConfigStrings["al_svr_guardian_bulwark_towerShieldBlockPowerMax"] : 0f; } }

		public static float al_svr_guardian_warCry_cooldown
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_warCry_cooldown") ? ConfigStrings["al_svr_guardian_warCry_cooldown"] : 1f; } }
		public static float al_svr_guardian_warCry_radius
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_warCry_radius") ? ConfigStrings["al_svr_guardian_warCry_radius"] : 1f; } }

		public static float al_svr_guardian_undyingWill_cooldown
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_undyingWill_cooldown") ? ConfigStrings["al_svr_guardian_undyingWill_cooldown"] : 0f; } }
		public static float al_svr_guardian_undyingWill_hpPercent
		{ get { return ConfigStrings.ContainsKey("al_svr_guardian_undyingWill_hpPercent") ? ConfigStrings["al_svr_guardian_undyingWill_hpPercent"] : 0f; } }
	}
}