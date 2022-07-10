using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace AsgardLegacy
{
	class Guardian
	{
		public static bool inFlight = false;
		public static bool shouldGuardianImpact = false;
		public static float shatterFallDamage = 0f;
		public static float retributionDamageBlocked = 0f;
		public static GameObject GO_CastFX;

		public static void ProcessInput(Player player)
		{
			var currentLevel = Utility.GetPlayerClassLevel(player);
			if (Utility.Ability1_Input_Down && currentLevel >= GlobalConfigs.al_svr_ability1UnlockLevel)
			{
				if (!player.GetSEMan().HaveStatusEffect("SE_Ability1_CD"))
				{
					if (player.HaveStamina(GlobalConfigs_Guardian.al_svr_guardian_shatterFall_staminaCost - 1f))
					{
						if (player.m_rightItem != null && player.m_rightItem.IsWeapon())
							ActivateShatterFall();
						else
							Utility.SendNoWeaponEquippedMessage(player, "Shatter Fall");
					}
					else
						Utility.SendNotEnoughStaminaMessage(player, "Shatter Fall", GlobalConfigs_Guardian.al_svr_guardian_shatterFall_staminaCost);
				}
			}
			else if (Utility.Ability2_Input_Down && currentLevel >= GlobalConfigs.al_svr_ability2UnlockLevel)
			{
				if (!player.GetSEMan().HaveStatusEffect("SE_Ability2_CD"))
				{
					if (player.HaveStamina(GlobalConfigs_Guardian.al_svr_guardian_aegis_staminaCost - 1f))
						ActivateAegis(currentLevel);
					else
						Utility.SendNotEnoughStaminaMessage(player, SE_Guardian_Aegis.m_baseName, GlobalConfigs_Guardian.al_svr_guardian_aegis_staminaCost);
				}
			}
			else if (Utility.Ability3_Input_Down && currentLevel >= GlobalConfigs.al_svr_ability3UnlockLevel)
			{
				if (!player.GetSEMan().HaveStatusEffect("SE_Ability3_CD"))
				{
					if (player.HaveStamina(GlobalConfigs_Guardian.al_svr_guardian_iceCrush_staminaCost - 1f))
					{
						if (player.m_rightItem != null && player.m_rightItem.IsWeapon())
							ActivateIceCrush(currentLevel);
						else
							Utility.SendNoWeaponEquippedMessage(player, "Ice Crush");
					}
					else
						Utility.SendNotEnoughStaminaMessage(player, "Ice Crush", GlobalConfigs_Guardian.al_svr_guardian_iceCrush_staminaCost);
				}
			}
			else if (Utility.Ability4_Input_Down && currentLevel >= GlobalConfigs.al_svr_ability4UnlockLevel)
			{
				if (!player.GetSEMan().HaveStatusEffect("SE_Ability4_CD"))
				{
					if (player.HaveStamina(GlobalConfigs_Guardian.al_svr_guardian_retribution_staminaCost - 1f))
						ActivateRetribution();
					else
						Utility.SendNotEnoughStaminaMessage(player, SE_Guardian_Retribution.m_baseName, GlobalConfigs_Guardian.al_svr_guardian_retribution_staminaCost);
				}
			}
		}

		private static void ActivateShatterFall()
		{
			var player = Player.m_localPlayer;

			if (player.GetSEMan().HaveStatusEffect("SE_Ability1_CD"))
				return;

			var se_Ability1_CD = ScriptableObject.CreateInstance<SE_Ability1_CD>();
			se_Ability1_CD.m_ttl = GlobalConfigs_Guardian.al_svr_guardian_shatterFall_cooldown;
			player.GetSEMan().AddStatusEffect(se_Ability1_CD, false);

			((ZSyncAnimation) typeof(Player).GetField("m_zanim", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Player.m_localPlayer)).SetTrigger("swing_sledge");

			var initVelocity = player.GetVelocity();
			var traverseRigidbody = Traverse.Create(player).Field("m_body").GetValue<Rigidbody>();
			inFlight = true;
			var planarVelocity = new Vector3(traverseRigidbody.velocity.x, 0f, traverseRigidbody.velocity.z);
			traverseRigidbody.velocity = initVelocity * 2f + new Vector3(0f, 15f, 0f) + planarVelocity * 3f;
			var rigidbody = traverseRigidbody;

			shatterFallDamage = player.m_rightItem.GetDamage().GetTotalPhysicalDamage();

			rigidbody.velocity *= 0.8f;

			GO_CastFX = Object.Instantiate(ZNetScene.instance.GetPrefab("sfx_perfectblock"), player.transform.position, Quaternion.identity);
			GO_CastFX = Object.Instantiate(ZNetScene.instance.GetPrefab("vfx_perfectblock"), player.transform.position, Quaternion.identity);

			player.UseStamina(GlobalConfigs_Guardian.al_svr_guardian_shatterFall_staminaCost);

			player.StartCoroutine(WaitForImpact(2f, player));
		}
		
		private static void ActivateAegis(int currentLevel)
		{
			var player = Player.m_localPlayer;

			var se_Ability2_CD = (SE_Ability2_CD) ScriptableObject.CreateInstance(typeof(SE_Ability2_CD));
			se_Ability2_CD.m_ttl = GlobalConfigs_Guardian.al_svr_guardian_aegis_cooldown;
			player.GetSEMan().AddStatusEffect(se_Ability2_CD, true);

			var se_Guardian_Aegis = (SE_Guardian_Aegis) ScriptableObject.CreateInstance(typeof(SE_Guardian_Aegis));
			se_Guardian_Aegis.m_ttl = Utility.GetLinearValue(
				currentLevel,
				GlobalConfigs_Guardian.al_svr_guardian_aegis_durationMin,
				GlobalConfigs_Guardian.al_svr_guardian_aegis_durationMax,
				GlobalConfigs.al_svr_ability2UnlockLevel);
			se_Guardian_Aegis.m_damageModifier = Utility.GetLinearValue(
				currentLevel,
				GlobalConfigs_Guardian.al_svr_guardian_aegis_damageReductionMin,
				GlobalConfigs_Guardian.al_svr_guardian_aegis_damageReductionMax,
				GlobalConfigs.al_svr_ability2UnlockLevel);
			player.GetSEMan().AddStatusEffect(se_Guardian_Aegis, false);

			Object.Instantiate(ZNetScene.instance.GetPrefab("fx_guardstone_deactivate"), player.GetCenterPoint(), Quaternion.identity);
			Object.Instantiate(ZNetScene.instance.GetPrefab("sfx_metal_blocked"), player.transform.position, Quaternion.identity);

			player.UseStamina(GlobalConfigs_Guardian.al_svr_guardian_aegis_staminaCost);

			player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainBuffCast);
		}
		
		private static void ActivateIceCrush(int currentLevel)
		{
			var player = Player.m_localPlayer;

			AsgardLegacy.isChanneling = true;
			((ZSyncAnimation) typeof(Player).GetField("m_zanim", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Player.m_localPlayer)).SetTrigger("swing_sledge");

			player.UseStamina(GlobalConfigs_Guardian.al_svr_guardian_iceCrush_staminaCost);

			player.StartCoroutine(WaitForIceCrush(1f, player, currentLevel));
		}
		
		private static void ActivateRetribution()
		{
			var player = Player.m_localPlayer;

			var se_Ability4_CD = (SE_Ability4_CD) ScriptableObject.CreateInstance(typeof(SE_Ability4_CD));
			se_Ability4_CD.m_ttl = GlobalConfigs_Guardian.al_svr_guardian_retribution_cooldown;
			player.GetSEMan().AddStatusEffect(se_Ability4_CD, true);

			var retribution = (SE_Guardian_Retribution) ScriptableObject.CreateInstance(typeof(SE_Guardian_Retribution));
			retribution.m_ttl = GlobalConfigs_Guardian.al_svr_guardian_retribution_duration;
			player.GetSEMan().AddStatusEffect(retribution, true);

			retributionDamageBlocked = 0f;

			player.UseStamina(GlobalConfigs_Guardian.al_svr_guardian_retribution_staminaCost);

			player.StartCoroutine(DetonateRetribution(GlobalConfigs_Guardian.al_svr_guardian_retribution_duration, player));
		}

		private static IEnumerator WaitForImpact(float waitTime, Player player)
		{
			yield return new WaitForSeconds(waitTime);

			if (!inFlight)
				yield break;

			ImpactEffect(player, 10f);
		}

		public static void ImpactEffect(Player player, float altitude)
		{
			var nbHits = 0;
			var characters = Character.GetAllCharacters();
			inFlight = false;
			shouldGuardianImpact = false;

			var hitData = new HitData();
			hitData.m_damage.m_blunt = shatterFallDamage
					* Utility.GetLinearValue(Utility.GetPlayerClassLevel(player),
						GlobalConfigs_Guardian.al_svr_guardian_shatterFall_damageMultiplierMin,
						GlobalConfigs_Guardian.al_svr_guardian_shatterFall_damageMultiplierMax,
						(int) GlobalConfigs.al_svr_ability1UnlockLevel)
				+ altitude
					* Utility.GetLinearValue(Utility.GetPlayerClassLevel(player),
						GlobalConfigs_Guardian.al_svr_guardian_shatterFall_altitudeMultiplierMin,
						GlobalConfigs_Guardian.al_svr_guardian_shatterFall_altitudeMultiplierMax,
						(int) GlobalConfigs.al_svr_ability1UnlockLevel);
			hitData.m_pushForce = GlobalConfigs_Guardian.al_svr_guardian_shatterFall_pushForce;

			shatterFallDamage = 0f;

			foreach (var character in characters)
			{
				if (!BaseAI.IsEnemy(player, character)
					|| (character.transform.position - player.transform.position).magnitude > GlobalConfigs_Guardian.al_svr_guardian_shatterFall_radius
					|| !Utility.LOS_IsValid(character, player.transform.position, player.GetCenterPoint()))
					continue;

				nbHits++;
				hitData.m_dir = character.transform.position - player.transform.position;
				hitData.m_point = character.GetEyePoint();
				character.Damage(hitData);
			}

			if (nbHits > 0)
				player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainAoeBaseHit);
			if (nbHits > 1)
				player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainAoeMultipleHit * (nbHits - 1));

			((ZSyncAnimation) typeof(Player).GetField("m_zanim", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Player.m_localPlayer)).StopAllCoroutines();
			GO_CastFX = Object.Instantiate(ZNetScene.instance.GetPrefab("vfx_gdking_stomp"), player.transform.position, Quaternion.identity);
			GO_CastFX = Object.Instantiate(ZNetScene.instance.GetPrefab("sfx_troll_rock_destroyed"), player.transform.position, Quaternion.identity);
			GO_CastFX = Object.Instantiate(ZNetScene.instance.GetPrefab("sfx_troll_rock_destroyed"), player.transform.position, Quaternion.identity);
		}

		public static IEnumerator WaitForIceCrush(float waitTime, Player player, int currentLevel)
		{
			yield return new WaitForSeconds(waitTime);

			var se_Ability3_CD = (SE_Ability3_CD) ScriptableObject.CreateInstance(typeof(SE_Ability3_CD));

			if (!AsgardLegacy.isChanneling)
			{
				se_Ability3_CD.m_ttl = GlobalConfigs_Guardian.al_svr_guardian_iceCrush_cooldown / 2f;
				player.GetSEMan().AddStatusEffect(se_Ability3_CD, true);

				yield break;
			}

			se_Ability3_CD.m_ttl = GlobalConfigs_Guardian.al_svr_guardian_iceCrush_cooldown;
			player.GetSEMan().AddStatusEffect(se_Ability3_CD, true);

			AsgardLegacy.isChanneling = false;

			var hitData = new HitData();
			var damage = player.m_rightItem.GetDamage().GetTotalPhysicalDamage()
					* Utility.GetLinearValue(currentLevel,
						GlobalConfigs_Guardian.al_svr_guardian_iceCrush_damageMultiplierMin,
						GlobalConfigs_Guardian.al_svr_guardian_iceCrush_damageMultiplierMax,
						(int) GlobalConfigs.al_svr_ability3UnlockLevel);
			hitData.m_damage.m_blunt = damage / 2f;
			hitData.m_damage.m_frost = damage / 2f;
			hitData.m_pushForce = GlobalConfigs_Guardian.al_svr_guardian_shatterFall_pushForce;

			var nbHits = 0;
			var characters = Character.GetAllCharacters();
			foreach (var character in characters)
			{
				if (!BaseAI.IsEnemy(player, character)
					|| (character.transform.position - player.transform.position).magnitude > GlobalConfigs_Guardian.al_svr_guardian_shatterFall_radius
					|| !Utility.LOS_IsValid(character, player.transform.position, player.GetCenterPoint()))
					continue;

				nbHits++;
				hitData.m_dir = character.transform.position - player.transform.position;
				hitData.m_point = character.GetEyePoint();
				character.Damage(hitData);
			}

			if (nbHits > 0)
				player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainAoeBaseHit);
			if (nbHits > 1)
				player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainAoeMultipleHit * (nbHits - 1));

			var fxPoint = player.GetCenterPoint() + player.GetLookDir().normalized * 2f;
			Object.Instantiate(ZNetScene.instance.GetPrefab("vfx_sledge_hit"), fxPoint, Quaternion.identity);
			var iceFx = Object.Instantiate(ZNetScene.instance.GetPrefab("vfx_iceblocker_destroyed"), fxPoint, Quaternion.identity);
			iceFx.transform.localScale *= 2f;
			Object.Instantiate(ZNetScene.instance.GetPrefab("sfx_dragon_coldball_explode"), player.transform.position, Quaternion.identity);
		}

		public static IEnumerator DetonateRetribution(float waitTime, Player player)
		{
			yield return new WaitForSeconds(waitTime);

			var hitData = new HitData();
			var damage = retributionDamageBlocked * GlobalConfigs_Guardian.al_svr_guardian_retribution_damageMultiplier;
			hitData.m_damage.m_blunt = damage / 2f;
			hitData.m_damage.m_fire = damage / 2f;
			hitData.m_pushForce = GlobalConfigs_Guardian.al_svr_guardian_retribution_pushForce;

			var nbHits = 0;
			var characters = Character.GetAllCharacters();
			foreach (var character in characters)
			{
				if (!BaseAI.IsEnemy(player, character)
					|| (character.transform.position - player.transform.position).magnitude > GlobalConfigs_Guardian.al_svr_guardian_retribution_radius
					|| !Utility.LOS_IsValid(character, player.transform.position, player.GetCenterPoint()))
					continue;

				nbHits++;
				hitData.m_dir = character.transform.position - player.transform.position;
				hitData.m_point = character.GetEyePoint();
				character.Damage(hitData);
			}

			if (nbHits > 0)
				player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainAoeBaseHit);
			if (nbHits > 1)
				player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainAoeMultipleHit * (nbHits - 1));

			((ZSyncAnimation) typeof(Player).GetField("m_zanim", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Player.m_localPlayer)).SetTrigger("emote_cheer");

			Object.Instantiate(ZNetScene.instance.GetPrefab("fx_goblinking_nova"), player.GetCenterPoint(), Quaternion.identity);
			GO_CastFX = Object.Instantiate(ZNetScene.instance.GetPrefab("sfx_troll_rock_destroyed"), player.transform.position, Quaternion.identity);
			GO_CastFX = Object.Instantiate(ZNetScene.instance.GetPrefab("sfx_troll_rock_destroyed"), player.transform.position, Quaternion.identity);
		}
	}
}
