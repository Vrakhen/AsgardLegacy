using System.Collections.Generic;

namespace AsgardLegacy
{
	public class GlobalConfigs_Ranger
	{
		public static Dictionary<string, float> ConfigStrings = new Dictionary<string, float>();

		public static float al_svr_ranger_explosiveArrow_staminaCost
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_explosiveArrow_staminaCost") ? ConfigStrings["al_svr_ranger_explosiveArrow_staminaCost"] : 0f; } }
		public static float al_svr_ranger_explosiveArrow_cooldown
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_explosiveArrow_cooldown") ? ConfigStrings["al_svr_ranger_explosiveArrow_cooldown"] : 0f; } }
		public static float al_svr_ranger_explosiveArrow_duration
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_explosiveArrow_duration") ? ConfigStrings["al_svr_ranger_explosiveArrow_duration"] : 0f; } }
		public static float al_svr_ranger_explosiveArrow_radius
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_explosiveArrow_radius") ? ConfigStrings["al_svr_ranger_explosiveArrow_radius"] : 0f; } }
		public static float al_svr_ranger_explosiveArrow_damageMin
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_explosiveArrow_damageMin") ? ConfigStrings["al_svr_ranger_explosiveArrow_damageMin"] : 0f; } }
		public static float al_svr_ranger_explosiveArrow_damageMax
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_explosiveArrow_damageMax") ? ConfigStrings["al_svr_ranger_explosiveArrow_damageMax"] : 0f; } }
		public static float al_svr_ranger_explosiveArrow_pushForce
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_explosiveArrow_pushForce") ? ConfigStrings["al_svr_ranger_explosiveArrow_pushForce"] : 0f; } }

		public static float al_svr_ranger_shadowStalk_staminaCost
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_shadowStalk_staminaCost") ? ConfigStrings["al_svr_ranger_shadowStalk_staminaCost"] : 0f; } }
		public static float al_svr_ranger_shadowStalk_cooldown
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_shadowStalk_cooldown") ? ConfigStrings["al_svr_ranger_shadowStalk_cooldown"] : 0f; } }
		public static float al_svr_ranger_shadowStalk_durationMin
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_shadowStalk_durationMin") ? ConfigStrings["al_svr_ranger_shadowStalk_durationMin"] : 0f; } }
		public static float al_svr_ranger_shadowStalk_durationMax
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_shadowStalk_durationMax") ? ConfigStrings["al_svr_ranger_shadowStalk_durationMax"] : 0f; } }
		public static float al_svr_ranger_shadowStalk_speedDurationMin
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_shadowStalk_speedDurationMin") ? ConfigStrings["al_svr_ranger_shadowStalk_speedDurationMin"] : 0f; } }
		public static float al_svr_ranger_shadowStalk_speedDurationMax
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_shadowStalk_speedDurationMax") ? ConfigStrings["al_svr_ranger_shadowStalk_speedDurationMax"] : 0f; } }
		public static float al_svr_ranger_shadowStalk_speedMultiplierMin
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_shadowStalk_speedMultiplierMin") ? ConfigStrings["al_svr_ranger_shadowStalk_speedMultiplierMin"] : 1f; } }
		public static float al_svr_ranger_shadowStalk_speedMultiplierMax
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_shadowStalk_speedMultiplierMax") ? ConfigStrings["al_svr_ranger_shadowStalk_speedMultiplierMax"] : 1f; } }
		public static float al_svr_ranger_shadowStalk_crouchSpeedMultiplierMin
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_shadowStalk_crouchSpeedMultiplierMin") ? ConfigStrings["al_svr_ranger_shadowStalk_crouchSpeedMultiplierMin"] : 1f; } }
		public static float al_svr_ranger_shadowStalk_crouchSpeedMultiplierMax
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_shadowStalk_crouchSpeedMultiplierMax") ? ConfigStrings["al_svr_ranger_shadowStalk_crouchSpeedMultiplierMax"] : 1f; } }

		public static float al_svr_ranger_rapidFire_staminaCost
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_rapidFire_staminaCost") ? ConfigStrings["al_svr_ranger_rapidFire_staminaCost"] : 0f; } }
		public static float al_svr_ranger_rapidFire_cooldown
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_rapidFire_cooldown") ? ConfigStrings["al_svr_ranger_rapidFire_cooldown"] : 0f; } }
		public static float al_svr_ranger_rapidFire_durationMin
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_rapidFire_durationMin") ? ConfigStrings["al_svr_ranger_rapidFire_durationMin"] : 0f; } }
		public static float al_svr_ranger_rapidFire_durationMax
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_rapidFire_durationMax") ? ConfigStrings["al_svr_ranger_rapidFire_durationMax"] : 0f; } }
		public static float al_svr_ranger_rapidFire_drawPercentMin
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_rapidFire_drawPercentMin") ? ConfigStrings["al_svr_ranger_rapidFire_drawPercentMin"] : 0f; } }
		public static float al_svr_ranger_rapidFire_drawPercentMax
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_rapidFire_drawPercentMax") ? ConfigStrings["al_svr_ranger_rapidFire_drawPercentMax"] : 0f; } }
		public static float al_svr_ranger_rapidFire_attackSpeedMultiplierMin
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_rapidFire_attackSpeedMultiplierMin") ? ConfigStrings["al_svr_ranger_rapidFire_attackSpeedMultiplierMin"] : 1f; } }
		public static float al_svr_ranger_rapidFire_attackSpeedMultiplierMax
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_rapidFire_attackSpeedMultiplierMax") ? ConfigStrings["al_svr_ranger_rapidFire_attackSpeedMultiplierMax"] : 1f; } }
		public static float al_svr_ranger_rapidFire_damageReductionMin
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_rapidFire_damageReductionMin") ? ConfigStrings["al_svr_ranger_rapidFire_damageReductionMin"] : 0f; } }
		public static float al_svr_ranger_rapidFire_damageReductionMax
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_rapidFire_damageReductionMax") ? ConfigStrings["al_svr_ranger_rapidFire_damageReductionMax"] : 0f; } }

		public static float al_svr_ranger_rangerMark_staminaCost
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_rangerMark_staminaCost") ? ConfigStrings["al_svr_ranger_rangerMark_staminaCost"] : 0f; } }
		public static float al_svr_ranger_rangerMark_cooldown
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_rangerMark_cooldown") ? ConfigStrings["al_svr_ranger_rangerMark_cooldown"] : 0f; } }
		public static float al_svr_ranger_rangerMark_durationMin
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_rangerMark_durationMin") ? ConfigStrings["al_svr_ranger_rangerMark_durationMin"] : 0f; } }
		public static float al_svr_ranger_rangerMark_durationMax
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_rangerMark_durationMax") ? ConfigStrings["al_svr_ranger_rangerMark_durationMax"] : 0f; } }
		public static float al_svr_ranger_rangerMark_damageMultiplierMin
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_rangerMark_damageMultiplierMin") ? ConfigStrings["al_svr_ranger_rangerMark_damageMultiplierMin"] : 1f; } }
		public static float al_svr_ranger_rangerMark_damageMultiplierMax
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_rangerMark_damageMultiplierMax") ? ConfigStrings["al_svr_ranger_rangerMark_damageMultiplierMax"] : 1f; } }
		
		public static float al_svr_ranger_bowSpecialist_damageMultiplierMin
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_bowSpecialist_damageMultiplierMin") ? ConfigStrings["al_svr_ranger_bowSpecialist_damageMultiplierMin"] : 1f; } }
		public static float al_svr_ranger_bowSpecialist_damageMultiplierMax
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_bowSpecialist_damageMultiplierMax") ? ConfigStrings["al_svr_ranger_bowSpecialist_damageMultiplierMax"] : 1f; } }
		public static float al_svr_ranger_bowSpecialist_velocityMultiplierMin
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_bowSpecialist_velocityMultiplierMin") ? ConfigStrings["al_svr_ranger_bowSpecialist_velocityMultiplierMin"] : 1f; } }
		public static float al_svr_ranger_bowSpecialist_velocityMultiplierMax
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_bowSpecialist_velocityMultiplierMax") ? ConfigStrings["al_svr_ranger_bowSpecialist_velocityMultiplierMax"] : 1f; } }
		
		public static float al_svr_ranger_longstrider_runnningStaminaDrainReductionMin
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_longstrider_runnningStaminaDrainReductionMin") ? ConfigStrings["al_svr_ranger_longstrider_runnningStaminaDrainReductionMin"] : 0f; } }
		public static float al_svr_ranger_longstrider_runnningStaminaDrainReductionMax
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_longstrider_runnningStaminaDrainReductionMax") ? ConfigStrings["al_svr_ranger_longstrider_runnningStaminaDrainReductionMax"] : 0f; } }
		public static float al_svr_ranger_longstrider_jumpingStaminaDrainReductionMin
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_longstrider_jumpingStaminaDrainReductionMin") ? ConfigStrings["al_svr_ranger_longstrider_jumpingStaminaDrainReductionMin"] : 0f; } }
		public static float al_svr_ranger_longstrider_jumpingStaminaDrainReductionMax
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_longstrider_jumpingStaminaDrainReductionMax") ? ConfigStrings["al_svr_ranger_longstrider_jumpingStaminaDrainReductionMax"] : 0f; } }

		public static float al_svr_ranger_speedBurst_cooldown
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_speedBurst_cooldown") ? ConfigStrings["al_svr_ranger_speedBurst_cooldown"] : 0f; } }
		public static float al_svr_ranger_speedBurst_durationMin
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_speedBurst_durationMin") ? ConfigStrings["al_svr_ranger_speedBurst_durationMin"] : 0f; } }
		public static float al_svr_ranger_speedBurst_durationMax
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_speedBurst_durationMax") ? ConfigStrings["al_svr_ranger_speedBurst_durationMax"] : 0f; } }
		public static float al_svr_ranger_speedBurst_speedMultiplierMin
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_speedBurst_speedMultiplierMin") ? ConfigStrings["al_svr_ranger_speedBurst_speedMultiplierMin"] : 1f; } }
		public static float al_svr_ranger_speedBurst_speedMultiplierMax
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_speedBurst_speedMultiplierMax") ? ConfigStrings["al_svr_ranger_speedBurst_speedMultiplierMax"] : 1f; } }

		public static float al_svr_ranger_goForTheEyes_damageMultiplierMin
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_goForTheEyes_damageMultiplierMin") ? ConfigStrings["al_svr_ranger_goForTheEyes_damageMultiplierMin"] : 1f; } }
		public static float al_svr_ranger_goForTheEyes_damageMultiplierMax
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_goForTheEyes_damageMultiplierMax") ? ConfigStrings["al_svr_ranger_goForTheEyes_damageMultiplierMax"] : 1f; } }

		public static float al_svr_ranger_elementalTouch_percentChanceMin
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_elementalTouch_percentChanceMin") ? ConfigStrings["al_svr_ranger_elementalTouch_percentChanceMin"] : 1f; } }
		public static float al_svr_ranger_elementalTouch_percentChanceMax
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_elementalTouch_percentChanceMax") ? ConfigStrings["al_svr_ranger_elementalTouch_percentChanceMax"] : 1f; } }
		public static float al_svr_ranger_elementalTouch_damagePercentMin
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_elementalTouch_damagePercentMin") ? ConfigStrings["al_svr_ranger_elementalTouch_damagePercentMin"] : 1f; } }
		public static float al_svr_ranger_elementalTouch_damagePercentMax
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_elementalTouch_damagePercentMax") ? ConfigStrings["al_svr_ranger_elementalTouch_damagePercentMax"] : 1f; } }

		public static float al_svr_ranger_ammoSaver_regainChancePercent
		{ get { return ConfigStrings.ContainsKey("al_svr_ranger_ammoSaver_regainChancePercent") ? ConfigStrings["al_svr_ranger_ammoSaver_regainChancePercent"] : 0f; } }
	}
}
