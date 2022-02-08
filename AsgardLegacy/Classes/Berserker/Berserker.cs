using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace AsgardLegacy
{
	class Berserker
	{
		public static void ProcessInput(Player player)
		{
			var currentLevel = Utility.GetPlayerClassLevel(player);
			if (Utility.Ability1_Input_Down && currentLevel >= GlobalConfigs.al_svr_ability1UnlockLevel)
			{
				if (!player.GetSEMan().HaveStatusEffect("SE_Ability1_CD"))
				{
					if (player.HaveStamina(GlobalConfigs_Berserker.al_svr_berserker_charge_staminaCost - 1f))
					{
						if (player.m_rightItem != null && player.m_rightItem.IsWeapon())
							ActivateCharge(currentLevel);
						else
							Utility.SendNoWeaponEquippedMessage(player, "Charge");
					}
					else
						Utility.SendNotEnoughStaminaMessage(player, "Charge", GlobalConfigs_Berserker.al_svr_berserker_charge_staminaCost);
				}
			}
			else if (Utility.Ability2_Input_Down && currentLevel >= GlobalConfigs.al_svr_ability2UnlockLevel)
			{
				if (!player.GetSEMan().HaveStatusEffect("SE_Ability2_CD"))
				{
					if (player.HaveStamina(GlobalConfigs_Berserker.al_svr_berserker_dreadfulRoar_staminaCost - 1f))
						ActivateDreadfulRoar(currentLevel);
					else
						Utility.SendNotEnoughStaminaMessage(player, "Dreadful Roar", GlobalConfigs_Berserker.al_svr_berserker_dreadfulRoar_staminaCost);
				}
			}
			else if (Utility.Ability3_Input_Down && currentLevel >= GlobalConfigs.al_svr_ability3UnlockLevel)
			{
				if (!player.GetSEMan().HaveStatusEffect("SE_Ability3_CD"))
				{
					if (player.HaveStamina(GlobalConfigs_Berserker.al_svr_berserker_ragingStorm_staminaCost - 1f))
						ActivateRagingStorm(currentLevel);
					else
						Utility.SendNotEnoughStaminaMessage(player, "Raging Storm", GlobalConfigs_Berserker.al_svr_berserker_ragingStorm_staminaCost);
				}
			}
			else if (Utility.Ability4_Input_Down && currentLevel >= GlobalConfigs.al_svr_ability4UnlockLevel)
			{
				if (!player.GetSEMan().HaveStatusEffect("SE_Ability4_CD"))
				{
					if (player.HaveStamina(GlobalConfigs_Berserker.al_svr_berserker_frenzy_staminaCost - 1f))
						ActivateFrenzy(currentLevel);
					else
						Utility.SendNotEnoughStaminaMessage(player, "Frenzy", GlobalConfigs_Berserker.al_svr_berserker_frenzy_staminaCost);
				}
			}
			else if (Input.GetKeyDown(KeyCode.PageUp))
				player.RaiseSkill(AsgardLegacy.ClassLevelSkill);
		}

		private static void ActivateCharge(int currentLevel)
		{
			var player = Player.m_localPlayer;

			if (player.GetSEMan().HaveStatusEffect("SE_Ability1_CD"))
				return;

			var se_Ability1_CD = ScriptableObject.CreateInstance<SE_Ability1_CD>();
			se_Ability1_CD.m_ttl = GlobalConfigs_Berserker.al_svr_berserker_charge_cooldown;
			player.GetSEMan().AddStatusEffect(se_Ability1_CD, false);

			((ZSyncAnimation) typeof(Player).GetField("m_zanim", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Player.m_localPlayer)).SetTrigger("mace_secondary");

			player.UseStamina(GlobalConfigs_Berserker.al_svr_berserker_charge_staminaCost);

			player.StartCoroutine(WaitForCharge(1f, player, currentLevel));
		}
		
		private static void ActivateDreadfulRoar(int currentLevel)
		{
			var player = Player.m_localPlayer;

			var se_Ability2_CD = (SE_Ability2_CD) ScriptableObject.CreateInstance(typeof(SE_Ability2_CD));
			se_Ability2_CD.m_ttl = GlobalConfigs_Berserker.al_svr_berserker_dreadfulRoar_cooldown;
			player.GetSEMan().AddStatusEffect(se_Ability2_CD, true);

			((ZSyncAnimation) typeof(Player).GetField("m_zanim", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Player.m_localPlayer)).SetTrigger("eat");

			player.UseStamina(GlobalConfigs_Berserker.al_svr_berserker_dreadfulRoar_staminaCost);

			player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainBuffCast);

			player.StartCoroutine(WaitForRoar(.5f, player, currentLevel));
		}
		
		private static void ActivateRagingStorm(int currentLevel)
		{
			var player = Player.m_localPlayer;

			var se_Ability3_CD = (SE_Ability3_CD) ScriptableObject.CreateInstance(typeof(SE_Ability3_CD));
			se_Ability3_CD.m_ttl = GlobalConfigs_Berserker.al_svr_berserker_ragingStorm_cooldown;
			player.GetSEMan().AddStatusEffect(se_Ability3_CD, true);

			((ZSyncAnimation) typeof(Player).GetField("m_zanim", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Player.m_localPlayer)).SetTrigger("emote_cheer");

			player.UseStamina(GlobalConfigs_Berserker.al_svr_berserker_ragingStorm_staminaCost);

			player.StartCoroutine(WaitForRagingStorm(.5f, player, currentLevel));
		}
		
		private static void ActivateFrenzy(int currentLevel)
		{
			var player = Player.m_localPlayer;

			var se_Ability4_CD = (SE_Ability4_CD) ScriptableObject.CreateInstance(typeof(SE_Ability4_CD));
			se_Ability4_CD.m_ttl = GlobalConfigs_Berserker.al_svr_berserker_frenzy_cooldown;
			player.GetSEMan().AddStatusEffect(se_Ability4_CD, true);

			AsgardLegacy.shouldUseForsakenPower = false;
			((ZSyncAnimation) typeof(Player).GetField("m_zanim", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Player.m_localPlayer)).SetTrigger("gpower");

			player.UseStamina(GlobalConfigs_Berserker.al_svr_berserker_frenzy_staminaCost);

			player.StartCoroutine(WaitForFrenzy(1.5f, player, currentLevel));
		}

		public static IEnumerator WaitForCharge(float waitTime, Player player, int currentLevel)
		{
			yield return new WaitForSeconds(waitTime);

			Object.Instantiate(ZNetScene.instance.GetPrefab("sfx_perfectblock"), player.transform.position, Quaternion.identity);
			Object.Instantiate(ZNetScene.instance.GetPrefab("vfx_stonegolem_attack_hit"), player.transform.position, Quaternion.identity);

			var damageMultiplier = Utility.GetLinearValue(currentLevel,
						GlobalConfigs_Berserker.al_svr_berserker_charge_damageMultiplierMin,
						GlobalConfigs_Berserker.al_svr_berserker_charge_damageMultiplierMax,
						(int) GlobalConfigs.al_svr_ability1UnlockLevel);

			if (player.m_rightItem.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon
				&& Utility.IsPlayerAbilityUnlockedByLevel(player, GlobalConfigs.al_svr_passive1UnlockLevel))
				damageMultiplier *= Utility.GetLinearValue(currentLevel,
						GlobalConfigs_Berserker.al_svr_berserker_twoHandedExpert_damageBonusMin,
						GlobalConfigs_Berserker.al_svr_berserker_twoHandedExpert_damageBonusMax,
						(int) GlobalConfigs.al_svr_passive1UnlockLevel);

			var hitData = new HitData();
			hitData.m_damage.m_blunt = player.m_rightItem.GetDamage().m_blunt * damageMultiplier;
			hitData.m_damage.m_pierce = player.m_rightItem.GetDamage().m_pierce * damageMultiplier;
			hitData.m_damage.m_slash = player.m_rightItem.GetDamage().m_slash * damageMultiplier;
			hitData.m_damage.m_fire = player.m_rightItem.GetDamage().m_fire * damageMultiplier;
			hitData.m_damage.m_frost = player.m_rightItem.GetDamage().m_frost * damageMultiplier;
			hitData.m_damage.m_lightning = player.m_rightItem.GetDamage().m_lightning * damageMultiplier;
			hitData.m_damage.m_poison = player.m_rightItem.GetDamage().m_poison * damageMultiplier;
			hitData.m_damage.m_spirit = player.m_rightItem.GetDamage().m_spirit * damageMultiplier;

			var lookDir = player.GetLookDir();
			lookDir.y = 0f;
			player.transform.rotation = Quaternion.LookRotation(lookDir);

			var startPoint = player.transform.position;
			var endPoint = player.transform.position + player.transform.forward * GlobalConfigs_Berserker.al_svr_berserker_charge_range;
			endPoint.y = ZoneSystem.instance.GetGroundHeight(endPoint);

			var hitEnemies = new List<int>();
			var raycastHits = Physics.SphereCastAll(startPoint - 2f * player.transform.forward, 2f, endPoint - startPoint, GlobalConfigs_Berserker.al_svr_berserker_charge_range + 2f, Script_Layermask);

			foreach (var raycastHit in raycastHits)
			{
				foreach (var character in Character.GetAllCharacters())
				{
					if (Vector3.Distance(character.transform.position, raycastHit.point) > 2f)
						continue;

					if (!BaseAI.IsEnemy(character, player)
						|| hitEnemies.Contains(character.GetInstanceID()))
						continue;

					hitData.m_point = character.GetCenterPoint();
					hitData.m_dir = character.transform.position - raycastHit.point;
					character.Damage(hitData);
					Object.Instantiate(ZNetScene.instance.GetPrefab("fx_crit"), character.GetCenterPoint(), Quaternion.identity);
					hitEnemies.Add(character.GetInstanceID());
					break;
				}
			}

			if (hitEnemies.Count > 0)
				player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainAoeBaseHit);
			if (hitEnemies.Count > 1)
				player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainAoeMultipleHit * (hitEnemies.Count - 1));

			player.transform.position = endPoint;
		}

		public static IEnumerator WaitForRoar(float waitTime, Player player, int currentLevel)
		{
			yield return new WaitForSeconds(waitTime);

			Object.Instantiate(ZNetScene.instance.GetPrefab("fx_guardstone_activate"), player.transform.position, Quaternion.identity);
			Object.Instantiate(ZNetScene.instance.GetPrefab("sfx_greydwarf_shaman_heal"), player.transform.position, Quaternion.identity);

			var characters = new List<Character>();
			Character.GetCharactersInRange(player.GetCenterPoint(), GlobalConfigs_Berserker.al_svr_berserker_dreadfulRoar_radius, characters);
			foreach (var character in characters)
			{
				if (character.GetBaseAI() == null || !(character.GetBaseAI() is MonsterAI) || !character.GetBaseAI().IsEnemey(player))
					continue;

				var se_Berserker_Weaken = (SE_Berserker_Weaken) ScriptableObject.CreateInstance(typeof(SE_Berserker_Weaken));
				se_Berserker_Weaken.m_ttl = Utility.GetLinearValue(currentLevel,
						GlobalConfigs_Berserker.al_svr_berserker_dreadfulRoar_durationMin,
						GlobalConfigs_Berserker.al_svr_berserker_dreadfulRoar_durationMax,
						(int) GlobalConfigs.al_svr_ability2UnlockLevel);
				se_Berserker_Weaken.m_damageModifier = Utility.GetLinearValue(currentLevel,
						GlobalConfigs_Berserker.al_svr_berserker_dreadfulRoar_weakenValueMin,
						GlobalConfigs_Berserker.al_svr_berserker_dreadfulRoar_weakenValueMax,
						(int) GlobalConfigs.al_svr_ability2UnlockLevel);
				se_Berserker_Weaken.m_speedModifier = Utility.GetLinearValue(currentLevel,
						GlobalConfigs_Berserker.al_svr_berserker_dreadfulRoar_slowValueMin,
						GlobalConfigs_Berserker.al_svr_berserker_dreadfulRoar_slowValueMax,
						(int) GlobalConfigs.al_svr_ability2UnlockLevel);
				character.GetSEMan().AddStatusEffect(se_Berserker_Weaken);
			}
		}
		
		public static IEnumerator WaitForRagingStorm(float waitTime, Player player, int currentLevel)
		{
			yield return new WaitForSeconds(waitTime);

			Object.Instantiate(ZNetScene.instance.GetPrefab("fx_guardstone_deactivate"), player.transform.position, Quaternion.identity);
			Object.Instantiate(ZNetScene.instance.GetPrefab("sfx_build_hammer_metal"), player.transform.position, Quaternion.identity);

			var se_Berserker_RagingStorm = (SE_Berserker_RagingStorm) ScriptableObject.CreateInstance(typeof(SE_Berserker_RagingStorm));
			se_Berserker_RagingStorm.m_ttl = Utility.GetLinearValue(currentLevel,
					GlobalConfigs_Berserker.al_svr_berserker_ragingStorm_durationMin,
					GlobalConfigs_Berserker.al_svr_berserker_ragingStorm_durationMax,
					(int) GlobalConfigs.al_svr_ability3UnlockLevel);
			se_Berserker_RagingStorm.m_damageModifier = Utility.GetLinearValue(currentLevel,
					GlobalConfigs_Berserker.al_svr_berserker_ragingStorm_damageMultiplierMin,
					GlobalConfigs_Berserker.al_svr_berserker_ragingStorm_damageMultiplierMax,
					(int) GlobalConfigs.al_svr_ability3UnlockLevel);
			player.GetSEMan().AddStatusEffect(se_Berserker_RagingStorm);
		}

		public static IEnumerator WaitForFrenzy(float waitTime, Player player, int currentLevel)
		{
			yield return new WaitForSeconds(waitTime);

			Object.Instantiate(ZNetScene.instance.GetPrefab("fx_guardstone_deactivate"), player.transform.position, Quaternion.identity);
			Object.Instantiate(ZNetScene.instance.GetPrefab("sfx_build_hammer_metal"), player.transform.position, Quaternion.identity);

			var se_Berserker_Frenzy = (SE_Berserker_Frenzy) ScriptableObject.CreateInstance(typeof(SE_Berserker_Frenzy));
			se_Berserker_Frenzy.m_ttl = GlobalConfigs_Berserker.al_svr_berserker_frenzy_duration;
			player.GetSEMan().AddStatusEffect(se_Berserker_Frenzy);
		}

		private static int Script_Layermask = LayerMask.GetMask(new string[]
		{
			"character",
			"character_net",
			"character_noenv",
			"ghost",
		});
	}
}
