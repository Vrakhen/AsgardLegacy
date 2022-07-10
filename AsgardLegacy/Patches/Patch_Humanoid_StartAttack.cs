using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace AsgardLegacy
{
    class Patch_Humanoid_StartAttack
	{

		[HarmonyPatch(typeof(Humanoid), nameof(Humanoid.StartAttack))]
		public static class Patch_SecondaryAttack
		{
			public static void Postfix(Humanoid __instance, bool secondaryAttack)
			{
				if (!secondaryAttack)
					return;

				if (!__instance.IsPlayer())
					return;

				var player = __instance as Player;
				if (player != Player.m_localPlayer)
					return;

				var seMan = player.GetSEMan();
				var playerLevel = Utility.GetPlayerClassLevel(player);
				switch (AsgardLegacy.al_player.al_class)
				{
					case AsgardLegacy.PlayerClass.Berserker:
						{
							if (seMan.HaveStatusEffect("SE_Berserker_DenyPain_CD")
								|| playerLevel < GlobalConfigs.al_svr_passive6UnlockLevel)
								return;

							var se_Berserker_DenyPain = (SE_Berserker_DenyPain) ScriptableObject.CreateInstance(typeof(SE_Berserker_DenyPain));
							var se_Berserker_DenyPain_CD = (SE_Berserker_DenyPain_CD) ScriptableObject.CreateInstance(typeof(SE_Berserker_DenyPain_CD));
							se_Berserker_DenyPain.m_ttl = GlobalConfigs_Berserker.al_svr_berserker_denyPain_duration;
							se_Berserker_DenyPain_CD.m_ttl = GlobalConfigs_Berserker.al_svr_berserker_denyPain_cooldown;
							seMan.AddStatusEffect(se_Berserker_DenyPain, true);
							seMan.AddStatusEffect(se_Berserker_DenyPain_CD, true);

							player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainPassiveTrigger);

							break;
						}
					case AsgardLegacy.PlayerClass.Guardian:
						{
							if (seMan.HaveStatusEffect("SE_Guardian_WarCry_CD") 
								|| playerLevel < GlobalConfigs.al_svr_passive1UnlockLevel)
								return;

							var se_Guardian_WarCry_CD = (SE_Guardian_WarCry_CD) ScriptableObject.CreateInstance(typeof(SE_Guardian_WarCry_CD));
							se_Guardian_WarCry_CD.m_ttl = GlobalConfigs_Guardian.al_svr_guardian_warCry_cooldown;
							seMan.AddStatusEffect(se_Guardian_WarCry_CD, true);

							var hit = false;
							var characters = new List<Character>();
							Character.GetCharactersInRange(player.GetCenterPoint(), GlobalConfigs_Guardian.al_svr_guardian_warCry_radius, characters);
							foreach (var character in characters)
							{
								if (character.GetBaseAI() == null || !(character.GetBaseAI() is MonsterAI) || !character.GetBaseAI().IsEnemey(player))
									continue;

								var monsterAI = character.GetBaseAI() as MonsterAI;

								if (monsterAI == null || monsterAI.GetTargetCreature() == player)
									continue;

								hit = true;
								Traverse.Create(monsterAI).Field("m_alerted").SetValue(true);
								Traverse.Create(monsterAI).Field("m_targetCreature").SetValue(__instance);
							}

							if (hit)
								player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainPassiveTrigger);

							break;
						}
					case AsgardLegacy.PlayerClass.Sentinel:
						{
							if (seMan.HaveStatusEffect("SE_Sentinel_HealersGift_CD")
								|| playerLevel < GlobalConfigs.al_svr_passive3UnlockLevel)
								return;

							var se_HealersGift_CD = (SE_Sentinel_HealersGift_CD) ScriptableObject.CreateInstance(typeof(SE_Sentinel_HealersGift_CD));
							se_HealersGift_CD.m_ttl = GlobalConfigs_Sentinel.al_svr_sentinel_healersGift_cooldown;
							seMan.AddStatusEffect(se_HealersGift_CD, true);

							var se_HealersGift = (SE_Sentinel_HealersGift) ScriptableObject.CreateInstance(typeof(SE_Sentinel_HealersGift));
							se_HealersGift.m_ttl = se_HealersGift.m_healthOverTimeDuration = GlobalConfigs_Sentinel.al_svr_sentinel_healersGift_duration;
							se_HealersGift.m_healthOverTimeInterval = GlobalConfigs_Sentinel.al_svr_sentinel_healersGift_interval;
							se_HealersGift.m_healthOverTime = Utility.GetLinearValue(
									playerLevel,
									GlobalConfigs_Sentinel.al_svr_sentinel_healersGift_healthOverTimeMin,
									GlobalConfigs_Sentinel.al_svr_sentinel_healersGift_healthOverTimeMax,
									(int) GlobalConfigs.al_svr_passive3UnlockLevel);

							var characters = Character.GetAllCharacters();
							foreach (var character in characters)
							{
								if (!character.IsPlayer()
									|| (character.transform.position - player.transform.position).magnitude > GlobalConfigs_Sentinel.al_svr_sentinel_healersGift_radius
									|| !Utility.LOS_IsValid(character, player.transform.position, player.GetCenterPoint()))
									continue;

								character.GetSEMan().AddStatusEffect(se_HealersGift, true);
								Object.Instantiate(ZNetScene.instance.GetPrefab("fx_guardstone_permitted_removed"), player.GetCenterPoint(), Quaternion.identity);
							}

							player.RaiseSkill(AsgardLegacy.ClassLevelSkill, GlobalConfigs.al_svr_skillGainPassiveTrigger);

							break;
                        }
				}
			}
		}
	}
}
