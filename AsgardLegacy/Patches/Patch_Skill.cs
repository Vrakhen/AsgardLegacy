using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AsgardLegacy
{
    class Patch_Skill
	{
		[HarmonyPatch(typeof(Skills), nameof(Skills.GetSkillDef))]
		public static class GetSkillDef_Patch
		{
			public static void Postfix(Skills __instance, Skills.SkillType type, List<Skills.SkillDef> ___m_skills, ref Skills.SkillDef __result)
			{
				var methodInfo = AccessTools.Method(typeof(Localization), "AddWord", null, null);

				if (__result != null || AsgardLegacy.ClassLevelSkillDef == null || ___m_skills.Contains(AsgardLegacy.ClassLevelSkillDef))
					return;

				___m_skills.Add(AsgardLegacy.ClassLevelSkillDef);
				var methodBase = methodInfo;
				var instance = Localization.instance;
				var array = new object[] { "skill_" + AsgardLegacy.ClassLevelSkillDef.m_skill, AsgardLegacy.ClassLevelSkillName };
				methodBase.Invoke(instance, array);

				__result = ___m_skills.FirstOrDefault((Skills.SkillDef x) => x.m_skill == type);
			}
		}

		[HarmonyPatch(typeof(Skills), nameof(Skills.IsSkillValid))]
		public static class ValidSkill_Patch
		{
			public static bool Prefix(Skills.SkillType type, ref bool __result)
			{
				bool result;
				if (type == AsgardLegacy.ClassLevelSkill)
				{
					__result = true;
					result = false;
				}
				else
					result = true;

				return result;
			}
		}

		[HarmonyPatch(typeof(Player), nameof(Player.RaiseSkill))]
		public class RaiseSkill_Patch
		{
			public static bool Prefix(Player __instance, Skills.SkillType skill)
			{
				if (skill != AsgardLegacy.ClassLevelSkill)
					return true;

				if (__instance.GetSkills().GetSkillLevel(AsgardLegacy.ClassLevelSkill) >= SkillData.max_level)
					return false;

				return true;
			}
		}

		[HarmonyPatch(typeof(Player), nameof(Player.OnSkillLevelup))]
		public class OnSkillLevelup_Patch
		{
			public static bool Prefix(Player __instance, Skills.SkillType skill)
			{
				if (skill != AsgardLegacy.ClassLevelSkill)
					return true;
				
				var unlock = Utility.LevelUpUnlock.None;
				var abilityName = "";
				var newSkillLevel = (int) __instance.GetSkills().GetSkillLevel(AsgardLegacy.ClassLevelSkill);
				if(newSkillLevel == (int) GlobalConfigs.al_svr_ability1UnlockLevel)
                {
					unlock = Utility.LevelUpUnlock.Skill;
					abilityName = AsgardLegacy.Ability_Names[0];
				}
				else if(newSkillLevel == (int) GlobalConfigs.al_svr_ability2UnlockLevel)
                {
					unlock = Utility.LevelUpUnlock.Skill;
					abilityName = AsgardLegacy.Ability_Names[1];
				}
				else if(newSkillLevel == (int) GlobalConfigs.al_svr_ability3UnlockLevel)
                {
					unlock = Utility.LevelUpUnlock.Skill;
					abilityName = AsgardLegacy.Ability_Names[2];
				}
				else if(newSkillLevel == (int) GlobalConfigs.al_svr_ability4UnlockLevel)
                {
					unlock = Utility.LevelUpUnlock.Skill;
					abilityName = AsgardLegacy.Ability_Names[3];
				}
				else if(newSkillLevel == (int) GlobalConfigs.al_svr_passive1UnlockLevel)
                {
					unlock = Utility.LevelUpUnlock.Passive;
					abilityName = AsgardLegacy.Passive_Names[0];
				}
				else if(newSkillLevel == (int) GlobalConfigs.al_svr_passive2UnlockLevel)
                {
					unlock = Utility.LevelUpUnlock.Passive;
					abilityName = AsgardLegacy.Passive_Names[1];
				}
				else if(newSkillLevel == (int) GlobalConfigs.al_svr_passive3UnlockLevel)
                {
					unlock = Utility.LevelUpUnlock.Passive;
					abilityName = AsgardLegacy.Passive_Names[2];
				}
				else if(newSkillLevel == (int) GlobalConfigs.al_svr_passive4UnlockLevel)
                {
					unlock = Utility.LevelUpUnlock.Passive;
					abilityName = AsgardLegacy.Passive_Names[3];
				}
				else if(newSkillLevel == (int) GlobalConfigs.al_svr_passive5UnlockLevel)
                {
					unlock = Utility.LevelUpUnlock.Passive;
					abilityName = AsgardLegacy.Passive_Names[4];
				}
				else if(newSkillLevel == (int) GlobalConfigs.al_svr_passive6UnlockLevel)
                {
					unlock = Utility.LevelUpUnlock.Passive;
					abilityName = AsgardLegacy.Passive_Names[5];
				}

				Utility.SendLevelUpMessage(__instance, newSkillLevel, unlock, abilityName);

				return true;
			}
		}

		[HarmonyPatch(typeof(Skills), nameof(Skills.LowerAllSkills))]
		public class PreventClassLevelSkillDecrease_Patch
		{
			public static bool Prefix(float factor, Dictionary<Skills.SkillType, Skills.Skill> ___m_skillData, Player ___m_player)
			{
				foreach (var keyValuePair in ___m_skillData)
				{
					if (keyValuePair.Key == AsgardLegacy.ClassLevelSkill)
						continue;

					float num = keyValuePair.Value.m_level * factor;
					keyValuePair.Value.m_level -= num;
					keyValuePair.Value.m_accumulator = 0f;
				}
				___m_player.Message(MessageHud.MessageType.TopLeft, "$msg_skills_lowered", 0, null);

				return false;
			}
		}

	}
}
