using System.Collections;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AsgardLegacy
{
	public class Sentinel
	{
		public static void ProcessInput(Player player)
		{
			var currentLevel = Utility.GetPlayerClassLevel(player);
			if (Utility.Ability1_Input_Down && currentLevel >= GlobalConfigs.al_svr_ability1UnlockLevel)
			{
				if (!player.GetSEMan().HaveStatusEffect("SE_Ability1_CD"))
				{
					if (player.HaveStamina(GlobalConfigs_Sentinel.al_svr_sentinel_rejuvenatingStrike_staminaCost - 1f))
					{
						if (player.m_rightItem != null && player.m_rightItem.IsWeapon())
							ActivateRejuvenatingStrike();
						else
							Utility.SendNoWeaponEquippedMessage(player, "Rejuvenating Strike");
					}
					else
						Utility.SendNotEnoughStaminaMessage(player, "Rejuvenating Strike", GlobalConfigs_Sentinel.al_svr_sentinel_rejuvenatingStrike_staminaCost);
				}
			}
			else if (Utility.Ability2_Input_Down && currentLevel >= GlobalConfigs.al_svr_ability2UnlockLevel)
			{
				if (!player.GetSEMan().HaveStatusEffect("SE_Ability2_CD"))
				{
					if (player.HaveStamina(GlobalConfigs_Sentinel.al_svr_sentinel_mendingSpirit_staminaCost - 1f))
						ActivateMendingSpirits(currentLevel);
					else
						Utility.SendNotEnoughStaminaMessage(player, "Mending Spirits", GlobalConfigs_Sentinel.al_svr_sentinel_mendingSpirit_staminaCost);
				}
			}
			else if (Utility.Ability3_Input_Down && currentLevel >= GlobalConfigs.al_svr_ability3UnlockLevel)
			{
				if (!player.GetSEMan().HaveStatusEffect("SE_Ability3_CD"))
				{
					if (player.HaveStamina(GlobalConfigs_Sentinel.al_svr_sentinel_chainsOfLight_staminaCost - 1f))
					{
						if (player.m_rightItem != null && player.m_rightItem.IsWeapon())
							ActivateChainsOfLight(currentLevel);
						else
							Utility.SendNoWeaponEquippedMessage(player, "Chains of Light");
					}
					else
						Utility.SendNotEnoughStaminaMessage(player, "Chains of Light", GlobalConfigs_Sentinel.al_svr_sentinel_chainsOfLight_staminaCost);
				}
			}
			else if (Utility.Ability4_Input_Down && currentLevel >= GlobalConfigs.al_svr_ability4UnlockLevel)
			{
				if (!player.GetSEMan().HaveStatusEffect("SE_Ability4_CD"))
				{
					if (player.HaveStamina(GlobalConfigs_Sentinel.al_svr_sentinel_purgingFlames_staminaCost - 1f))
					{
						if (player.m_rightItem != null && player.m_rightItem.IsWeapon())
							ActivatePurgingFlames(currentLevel);
						else
							Utility.SendNoWeaponEquippedMessage(player, "Purging Flames");
					}
					else
						Utility.SendNotEnoughStaminaMessage(player, "Purging Flames", GlobalConfigs_Sentinel.al_svr_sentinel_purgingFlames_staminaCost);
				}
			}
		}

		private static void ActivateRejuvenatingStrike()
		{
			var player = Player.m_localPlayer;

			if (player.GetSEMan().HaveStatusEffect("SE_Ability1_CD"))
				return;

			var se_Ability1_CD = ScriptableObject.CreateInstance<SE_Ability1_CD>();
			se_Ability1_CD.m_ttl = GlobalConfigs_Sentinel.al_svr_sentinel_rejuvenatingStrike_cooldown;
			player.GetSEMan().AddStatusEffect(se_Ability1_CD, false);

			var se_RejuvenatingStrike = (SE_Sentinel_RejuvenatingStrike) ScriptableObject.CreateInstance(typeof(SE_Sentinel_RejuvenatingStrike));
			se_RejuvenatingStrike.m_ttl = GlobalConfigs_Sentinel.al_svr_sentinel_rejuvenatingStrike_duration;
			player.GetSEMan().AddStatusEffect(se_RejuvenatingStrike, false);

			player.UseStamina(GlobalConfigs_Sentinel.al_svr_sentinel_rejuvenatingStrike_staminaCost);
		}

		private static void ActivateMendingSpirits(int currentLevel)
		{
			var player = Player.m_localPlayer;

			AsgardLegacy.isChanneling = true;
			((ZSyncAnimation) typeof(Player).GetField("m_zanim", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Player.m_localPlayer)).SetTrigger("emote_cheer");

			player.UseStamina(GlobalConfigs_Sentinel.al_svr_sentinel_mendingSpirit_staminaCost);

			player.StartCoroutine(WaitForMendingSpirits(.5f, player, currentLevel));
		}

		private static void ActivateChainsOfLight(int currentLevel)
		{
			var player = Player.m_localPlayer;
			var position = player.transform.position;

			var se_Ability3_CD = (SE_Ability3_CD) ScriptableObject.CreateInstance(typeof(SE_Ability3_CD));
			se_Ability3_CD.m_ttl = GlobalConfigs_Sentinel.al_svr_sentinel_chainsOfLight_cooldown;
			player.GetSEMan().AddStatusEffect(se_Ability3_CD, true);

			var se_Chains = (SE_Sentinel_Chains) ScriptableObject.CreateInstance(typeof(SE_Sentinel_Chains));
			se_Chains.m_ttl = Utility.GetLinearValue(currentLevel,
				GlobalConfigs_Sentinel.al_svr_sentinel_chainsOfLight_rootDurationMin,
				GlobalConfigs_Sentinel.al_svr_sentinel_chainsOfLight_rootDurationMax,
				GlobalConfigs.al_svr_ability3UnlockLevel);
			se_Chains.m_speedModifier = 0f;

			var damage = player.m_rightItem.GetDamage().GetTotalDamage();
			damage *= Utility.GetLinearValue(currentLevel,
				GlobalConfigs_Sentinel.al_svr_sentinel_chainsOfLight_damageMultiplierMin,
				GlobalConfigs_Sentinel.al_svr_sentinel_chainsOfLight_damageMultiplierMax,
				GlobalConfigs.al_svr_ability3UnlockLevel);

			var hitData = new HitData();
			hitData.m_damage.m_fire = damage / 2f;
			hitData.m_damage.m_lightning = damage / 2f;

            var nbHits = 0;
			var allCharacters = Character.GetAllCharacters();
			foreach (var character in allCharacters)
			{
				if (!BaseAI.IsEnemy(player, character)
					|| (character.transform.position - position).magnitude > GlobalConfigs_Sentinel.al_svr_sentinel_chainsOfLight_radius
					|| !Utility.LOS_IsValid(character, position))
					continue;

				nbHits++;
				character.Damage(hitData);
				character.GetSEMan().AddStatusEffect(se_Chains);
			}

			if (nbHits > 0)
				player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainAoeBaseHit);
			if (nbHits > 1)
				player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainAoeMultipleHit * (nbHits - 1));

			Object.Instantiate(ZNetScene.instance.GetPrefab("fx_guardstone_activate"), position, Quaternion.identity);
			Object.Instantiate(ZNetScene.instance.GetPrefab("sfx_Frost_Start"), position, Quaternion.identity);

			player.UseStamina(GlobalConfigs_Sentinel.al_svr_sentinel_chainsOfLight_staminaCost);
		}

		private static void ActivatePurgingFlames(int currentLevel)
		{
			var player = Player.m_localPlayer;

			AsgardLegacy.isChanneling = true;
			AsgardLegacy.shouldUseForsakenPower = false;
			((ZSyncAnimation) typeof(Player).GetField("m_zanim", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Player.m_localPlayer)).SetTrigger("gpower");

			player.UseStamina(GlobalConfigs_Sentinel.al_svr_sentinel_purgingFlames_staminaCost);

			player.StartCoroutine(WaitForPurgingFlames(1.5f, player, currentLevel));
		}

		public static void RejuvImpactEffect(Player player, int targetId, HitData hit)
		{
			var nbHits = 0;
			var characters = Character.GetAllCharacters();

			var stamina = Utility.GetLinearValue(
				Utility.GetPlayerClassLevel(player),
				GlobalConfigs_Sentinel.al_svr_sentinel_rejuvenatingStrike_staminaMin,
				GlobalConfigs_Sentinel.al_svr_sentinel_rejuvenatingStrike_staminaMax,
				(int) GlobalConfigs.al_svr_ability1UnlockLevel);

			foreach (var character in characters)
			{
				if ((character.transform.position - player.transform.position).magnitude > GlobalConfigs_Sentinel.al_svr_sentinel_rejuvenatingStrike_radius
					|| !Utility.LOS_IsValid(character, player.transform.position, player.GetCenterPoint()))
					continue;

				if(character.IsPlayer())
				{
					character.AddStamina(stamina);

					Object.Instantiate(ZNetScene.instance.GetPrefab("fx_guardstone_permitted_add"), character.transform.position, Quaternion.identity);
					Object.Instantiate(ZNetScene.instance.GetPrefab("sfx_Potion_stamina_Start"), character.transform.position, Quaternion.identity);
				}
				else if(character.GetInstanceID() != targetId && BaseAI.IsEnemy(player, character))
				{
					character.Damage(hit);
					nbHits++;
				}
			}

			player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainAoeMultipleHit * nbHits);
		}

		public static IEnumerator WaitForMendingSpirits(float waitTime, Player player, int currentLevel)
		{
			yield return new WaitForSeconds(waitTime);

			var se_Ability2_CD = (SE_Ability2_CD) ScriptableObject.CreateInstance(typeof(SE_Ability2_CD));

			if (!AsgardLegacy.isChanneling)
			{
				se_Ability2_CD.m_ttl = GlobalConfigs_Sentinel.al_svr_sentinel_mendingSpirit_cooldown / 2f;
				player.GetSEMan().AddStatusEffect(se_Ability2_CD, true);

				yield break;
			}

            se_Ability2_CD.m_ttl = GlobalConfigs_Sentinel.al_svr_sentinel_mendingSpirit_cooldown;
			player.GetSEMan().AddStatusEffect(se_Ability2_CD, true);

			AsgardLegacy.isChanneling = false;

			var se_MendingSpirits = (SE_Sentinel_MendingSpirits) ScriptableObject.CreateInstance(typeof(SE_Sentinel_MendingSpirits));

			se_MendingSpirits.m_ttl = Utility.GetLinearValue(
				currentLevel,
				GlobalConfigs_Sentinel.al_svr_sentinel_mendingSpirit_healingDurationMin,
				GlobalConfigs_Sentinel.al_svr_sentinel_mendingSpirit_healingDurationMax,
				(int) GlobalConfigs.al_svr_ability2UnlockLevel);
			se_MendingSpirits.m_healing = Utility.GetLinearValue(
				currentLevel,
				GlobalConfigs_Sentinel.al_svr_sentinel_mendingSpirit_healingAmountMin,
				GlobalConfigs_Sentinel.al_svr_sentinel_mendingSpirit_healingAmountMax,
				(int) GlobalConfigs.al_svr_ability2UnlockLevel);
			se_MendingSpirits.m_healingInterval = GlobalConfigs_Sentinel.al_svr_sentinel_mendingSpirit_healingInterval;
			se_MendingSpirits.m_radius = GlobalConfigs_Sentinel.al_svr_sentinel_mendingSpirit_radius;
			player.GetSEMan().AddStatusEffect(se_MendingSpirits, true);

			player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainBuffCast);

			Object.Instantiate(ZNetScene.instance.GetPrefab("sfx_Potion_health_Start"), player.transform.position, Quaternion.identity);
		}

		public static IEnumerator WaitForPurgingFlames(float waitTime, Player player, int currentLevel)
		{
			yield return new WaitForSeconds(waitTime);

			var se_Ability4_CD = (SE_Ability4_CD) ScriptableObject.CreateInstance(typeof(SE_Ability4_CD));

			if (!AsgardLegacy.isChanneling)
			{
				se_Ability4_CD.m_ttl = GlobalConfigs_Sentinel.al_svr_sentinel_purgingFlames_cooldown / 2f;
				player.GetSEMan().AddStatusEffect(se_Ability4_CD, true);

				yield break;
			}

			se_Ability4_CD.m_ttl = GlobalConfigs_Sentinel.al_svr_sentinel_purgingFlames_cooldown;
			player.GetSEMan().AddStatusEffect(se_Ability4_CD, true);

			AsgardLegacy.isChanneling = false;

			var hitData = new HitData();
			hitData.m_damage.m_fire = player.m_rightItem.GetDamage().GetTotalDamage()
				* Utility.GetLinearValue(
					currentLevel,
					GlobalConfigs_Sentinel.al_svr_sentinel_purgingFlames_damageMultiplierMin,
					GlobalConfigs_Sentinel.al_svr_sentinel_purgingFlames_damageMultiplierMax,
					(int) GlobalConfigs.al_svr_ability4UnlockLevel);

			var se_PurgingFlames = (SE_Sentinel_PurgingFlames) ScriptableObject.CreateInstance(typeof(SE_Sentinel_PurgingFlames));
			se_PurgingFlames.m_staminaOverTimeDuration = se_PurgingFlames.m_healthOverTimeDuration = se_PurgingFlames.m_ttl = GlobalConfigs_Sentinel.al_svr_sentinel_purgingFlames_duration;
			se_PurgingFlames.m_healthOverTimeInterval = GlobalConfigs_Sentinel.al_svr_sentinel_purgingFlames_interval;
			se_PurgingFlames.m_healthOverTime = Utility.GetLinearValue(
					currentLevel,
					GlobalConfigs_Sentinel.al_svr_sentinel_purgingFlames_healthOverTimeMin,
					GlobalConfigs_Sentinel.al_svr_sentinel_purgingFlames_healthOverTimeMax,
					(int) GlobalConfigs.al_svr_ability4UnlockLevel);
			se_PurgingFlames.m_staminaOverTime = Utility.GetLinearValue(
					currentLevel,
					GlobalConfigs_Sentinel.al_svr_sentinel_purgingFlames_staminaOverTimeMin,
					GlobalConfigs_Sentinel.al_svr_sentinel_purgingFlames_staminaOverTimeMax,
					(int) GlobalConfigs.al_svr_ability4UnlockLevel);

			var nbHits = 0;
			var characters = Character.GetAllCharacters();
			foreach (var character in characters)
			{
				if ((character.transform.position - player.transform.position).magnitude > GlobalConfigs_Sentinel.al_svr_sentinel_purgingFlames_radius
					|| !Utility.LOS_IsValid(character, player.transform.position, player.GetCenterPoint()))
					continue;

				nbHits++;
				if (BaseAI.IsEnemy(character, player))
					character.Damage(hitData);
				else if(character.IsPlayer())
                {
					var seMan = character.GetSEMan();
					foreach (var se in Utility.CleansedSE)
					{
						if (!seMan.HaveStatusEffect(se))
							continue;
						seMan.RemoveStatusEffect(se);
					}

					seMan.AddStatusEffect(se_PurgingFlames, true);
					Object.Instantiate(ZNetScene.instance.GetPrefab("vfx_Potion_health_medium"), player.GetCenterPoint(), Quaternion.identity);
					Object.Instantiate(ZNetScene.instance.GetPrefab("vfx_Potion_stamina_medium"), player.GetCenterPoint(), Quaternion.identity);
				}
			}

			player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainAoeBaseHit + GlobalConfigs.al_svr_skillGainAoeMultipleHit * (nbHits - 1));

			Object.Instantiate(ZNetScene.instance.GetPrefab("vfx_spawn_large"), player.GetCenterPoint(), Quaternion.identity);
			Object.Instantiate(ZNetScene.instance.GetPrefab("sfx_bowl_AddItem"), player.transform.position, Quaternion.identity);
		}
	}
}
