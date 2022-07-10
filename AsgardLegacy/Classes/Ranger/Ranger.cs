using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace AsgardLegacy
{
	class Ranger
	{
		public static void ProcessInput(Player player)
		{
			var currentLevel = Utility.GetPlayerClassLevel(player);
			if (Utility.Ability1_Input_Down && currentLevel >= GlobalConfigs.al_svr_ability1UnlockLevel)
			{
				if (!player.GetSEMan().HaveStatusEffect("SE_Ability1_CD"))
				{
					if (player.HaveStamina(GlobalConfigs_Ranger.al_svr_ranger_explosiveArrow_staminaCost - 1f))
						ActivateExplosiveArrow();
					else
						Utility.SendNotEnoughStaminaMessage(player, SE_Ranger_ExplosiveArrow.m_baseName, GlobalConfigs_Ranger.al_svr_ranger_explosiveArrow_staminaCost);
				}
			}
			else if (Utility.Ability2_Input_Down && currentLevel >= GlobalConfigs.al_svr_ability2UnlockLevel)
			{
				if (!player.GetSEMan().HaveStatusEffect("SE_Ability2_CD"))
				{
					if (player.HaveStamina(GlobalConfigs_Ranger.al_svr_ranger_shadowStalk_staminaCost - 1f))
						ActivateShadowStalk(currentLevel);
					else
						Utility.SendNotEnoughStaminaMessage(player, SE_Ranger_ShadowStalk.m_baseName, GlobalConfigs_Ranger.al_svr_ranger_shadowStalk_staminaCost);
				}
			}
			else if (Utility.Ability3_Input_Down && currentLevel >= GlobalConfigs.al_svr_ability3UnlockLevel)
			{
				if (!player.GetSEMan().HaveStatusEffect("SE_Ability3_CD"))
				{
					if (player.HaveStamina(GlobalConfigs_Ranger.al_svr_ranger_rapidFire_staminaCost - 1f))
						ActivateRapidFire(currentLevel);
					else
						Utility.SendNotEnoughStaminaMessage(player, SE_Ranger_RapidFire.m_baseName, GlobalConfigs_Ranger.al_svr_ranger_rapidFire_staminaCost);
				}
			}
			else if (Utility.Ability4_Input_Down && currentLevel >= GlobalConfigs.al_svr_ability4UnlockLevel)
			{
				if (!player.GetSEMan().HaveStatusEffect("SE_Ability4_CD") && player.HaveStamina(GlobalConfigs_Ranger.al_svr_ranger_rangerMark_staminaCost))
				{
					if (player.HaveStamina(GlobalConfigs_Ranger.al_svr_ranger_shadowStalk_staminaCost - 1f))
						ActivateRangerMark(currentLevel);
					else
						Utility.SendNotEnoughStaminaMessage(player, SE_Ranger_RangerMark.m_baseName, GlobalConfigs_Ranger.al_svr_ranger_rangerMark_staminaCost);
				}
			}
		}

		private static void ActivateExplosiveArrow()
		{
			var player = Player.m_localPlayer;

			var se_Ability1_CD = (SE_Ability1_CD) ScriptableObject.CreateInstance(typeof(SE_Ability1_CD));
			se_Ability1_CD.m_ttl = GlobalConfigs_Ranger.al_svr_ranger_explosiveArrow_cooldown;
			player.GetSEMan().AddStatusEffect(se_Ability1_CD, true);

			var se_ExplosiveArrow = (SE_Ranger_ExplosiveArrow) ScriptableObject.CreateInstance(typeof(SE_Ranger_ExplosiveArrow));
			se_ExplosiveArrow.m_ttl = GlobalConfigs_Ranger.al_svr_ranger_explosiveArrow_duration;
			player.GetSEMan().AddStatusEffect(se_ExplosiveArrow, false);

			player.UseStamina(GlobalConfigs_Ranger.al_svr_ranger_explosiveArrow_staminaCost);
		}

		private static void ActivateShadowStalk(int currentLevel)
		{
			var player = Player.m_localPlayer;

			var se_Ability2_CD = (SE_Ability2_CD) ScriptableObject.CreateInstance(typeof(SE_Ability2_CD));
			se_Ability2_CD.m_ttl = GlobalConfigs_Ranger.al_svr_ranger_shadowStalk_cooldown;
			player.GetSEMan().AddStatusEffect(se_Ability2_CD, true);

			Object.Instantiate(ZNetScene.instance.GetPrefab("vfx_odin_despawn"), player.transform.position, Quaternion.identity);
			Object.Instantiate(ZNetScene.instance.GetPrefab("sfx_wraith_death"), player.transform.position, Quaternion.identity);

			var se_Ranger_ShadowStalk = (SE_Ranger_ShadowStalk) ScriptableObject.CreateInstance(typeof(SE_Ranger_ShadowStalk));
			se_Ranger_ShadowStalk.m_ttl = Utility.GetLinearValue(
				currentLevel,
				GlobalConfigs_Ranger.al_svr_ranger_shadowStalk_durationMin,
				GlobalConfigs_Ranger.al_svr_ranger_shadowStalk_durationMax,
				GlobalConfigs.al_svr_ability2UnlockLevel);
			se_Ranger_ShadowStalk.crouchSpeedAmount = Utility.GetLinearValue(
				currentLevel,
				GlobalConfigs_Ranger.al_svr_ranger_shadowStalk_crouchSpeedMultiplierMin,
				GlobalConfigs_Ranger.al_svr_ranger_shadowStalk_crouchSpeedMultiplierMax,
				GlobalConfigs.al_svr_ability2UnlockLevel);
			se_Ranger_ShadowStalk.speedAmount = Utility.GetLinearValue(
				currentLevel,
				GlobalConfigs_Ranger.al_svr_ranger_shadowStalk_speedMultiplierMin,
				GlobalConfigs_Ranger.al_svr_ranger_shadowStalk_speedMultiplierMax,
				GlobalConfigs.al_svr_ability2UnlockLevel);
			se_Ranger_ShadowStalk.speedDuration = Utility.GetLinearValue(
				currentLevel,
				GlobalConfigs_Ranger.al_svr_ranger_shadowStalk_speedDurationMin,
				GlobalConfigs_Ranger.al_svr_ranger_shadowStalk_speedDurationMax,
				GlobalConfigs.al_svr_ability2UnlockLevel);
			player.GetSEMan().AddStatusEffect(se_Ranger_ShadowStalk, false);

			var characters = new List<Character>();
			Character.GetCharactersInRange(player.GetCenterPoint(), 500f, characters);
			foreach (var character in characters)
			{
				if (character.GetBaseAI() == null || !(character.GetBaseAI() is MonsterAI) || !character.GetBaseAI().IsEnemey(player))
					continue;

				var monsterAI = character.GetBaseAI() as MonsterAI;

				if (monsterAI == null || monsterAI.GetTargetCreature() != player)
					continue;

				Traverse.Create(monsterAI).Field("m_alerted").SetValue(false);
				Traverse.Create(monsterAI).Field("m_targetCreature").SetValue(null);
			}

			player.UseStamina(GlobalConfigs_Ranger.al_svr_ranger_shadowStalk_staminaCost);

			player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainBuffCast);
		}

		private static void ActivateRapidFire(int currentLevel)
		{
			var player = Player.m_localPlayer;

			var se_Ability3_CD = (SE_Ability3_CD) ScriptableObject.CreateInstance(typeof(SE_Ability3_CD));
			se_Ability3_CD.m_ttl = GlobalConfigs_Ranger.al_svr_ranger_rapidFire_cooldown;
			player.GetSEMan().AddStatusEffect(se_Ability3_CD, true);

			var se_RangerRapidFire = (SE_Ranger_RapidFire) ScriptableObject.CreateInstance(typeof(SE_Ranger_RapidFire));
			se_RangerRapidFire.m_ttl = Utility.GetLinearValue(
				currentLevel,
				GlobalConfigs_Ranger.al_svr_ranger_rapidFire_durationMin,
				GlobalConfigs_Ranger.al_svr_ranger_rapidFire_durationMax,
				GlobalConfigs.al_svr_ability3UnlockLevel);
			player.GetSEMan().AddStatusEffect(se_RangerRapidFire, false);

			Object.Instantiate(ZNetScene.instance.GetPrefab("fx_guardstone_deactivate"), player.GetCenterPoint(), Quaternion.identity);
			Object.Instantiate(ZNetScene.instance.GetPrefab("sfx_metal_blocked"), player.transform.position, Quaternion.identity);

			player.UseStamina(GlobalConfigs_Ranger.al_svr_ranger_rapidFire_staminaCost);
		}

		private static void ActivateRangerMark(int currentLevel)
		{
			var player = Player.m_localPlayer;

			var se_Ability4_CD = (SE_Ability4_CD) ScriptableObject.CreateInstance(typeof(SE_Ability4_CD));
			se_Ability4_CD.m_ttl = GlobalConfigs_Ranger.al_svr_ranger_rangerMark_cooldown;
			player.GetSEMan().AddStatusEffect(se_Ability4_CD, true);

			var se_RangerMark = (SE_Ranger_RangerMark) ScriptableObject.CreateInstance(typeof(SE_Ranger_RangerMark));
			se_RangerMark.m_ttl = Utility.GetLinearValue(
				currentLevel,
				GlobalConfigs_Ranger.al_svr_ranger_rangerMark_durationMin,
				GlobalConfigs_Ranger.al_svr_ranger_rangerMark_durationMax,
				GlobalConfigs.al_svr_ability4UnlockLevel);
			player.GetSEMan().AddStatusEffect(se_RangerMark, false);

			player.StartEmote("challenge", true);
			Object.Instantiate(ZNetScene.instance.GetPrefab("fx_guardstone_deactivate"), player.GetCenterPoint(), Quaternion.identity);
			Object.Instantiate(ZNetScene.instance.GetPrefab("sfx_metal_blocked"), player.transform.position, Quaternion.identity);

			player.UseStamina(GlobalConfigs_Ranger.al_svr_ranger_rangerMark_staminaCost);

			player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainBuffCast);
		}

		public static void ExplosiveArrow(Player player, Vector3 hitPoint, Projectile projectile)
		{
			var nbHits = 0;
			var currentLevel = Utility.GetPlayerClassLevel(player);
			var allCharacters = Character.GetAllCharacters();
			foreach (var character in allCharacters)
			{
				if (!BaseAI.IsEnemy(player, character)
					|| (character.transform.position - hitPoint).magnitude > GlobalConfigs_Ranger.al_svr_ranger_explosiveArrow_radius
					|| !Utility.LOS_IsValid(character, hitPoint))
					continue;

				var projectileDamage = projectile.m_damage;

				nbHits++;
				var hitData = new HitData();
				hitData.m_damage.m_blunt = projectileDamage.m_pierce;
				hitData.m_damage.m_fire = projectileDamage.m_fire;
				hitData.m_damage.m_frost = projectileDamage.m_frost;
				hitData.m_damage.m_lightning = projectileDamage.m_lightning;
				hitData.m_damage.m_poison = projectileDamage.m_poison;

				hitData.m_damage.Modify(Utility.GetLinearValue(currentLevel,
						GlobalConfigs_Ranger.al_svr_ranger_explosiveArrow_damageMin,
						GlobalConfigs_Ranger.al_svr_ranger_explosiveArrow_damageMax,
						GlobalConfigs.al_svr_ability1UnlockLevel));

				hitData.m_pushForce = GlobalConfigs_Ranger.al_svr_ranger_explosiveArrow_pushForce;
				hitData.m_point = character.GetEyePoint();
				hitData.m_dir = character.transform.position - player.transform.position;

				character.Damage(hitData);
			}

			if (nbHits > 0)
				player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainAoeBaseHit);
			if (nbHits > 1)
				player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainAoeMultipleHit * (nbHits - 1));

			Object.Instantiate(ZNetScene.instance.GetPrefab("vfx_gdking_stomp"), hitPoint, Quaternion.identity);
			Object.Instantiate(ZNetScene.instance.GetPrefab("sfx_troll_rock_destroyed"), hitPoint, Quaternion.identity);
		}
	}
}
