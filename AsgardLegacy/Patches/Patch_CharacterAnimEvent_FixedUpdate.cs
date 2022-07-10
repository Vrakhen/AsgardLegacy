using HarmonyLib;
using UnityEngine;

namespace AsgardLegacy
{
    class Patch_CharacterAnimEvent_FixedUpdate
	{

		[HarmonyPatch(typeof(CharacterAnimEvent), nameof(CharacterAnimEvent.FixedUpdate))]
		public static class Patch_ModifyAttackSpeed
		{
			private static void Prefix(Character ___m_character, ref Animator ___m_animator)
			{
				if (!___m_character.IsPlayer() || !___m_character.InAttack())
					return;

				var player = ___m_character as Player;
				if (player != Player.m_localPlayer)
					return;

				var currentAttack = player.m_currentAttack;
				if (currentAttack == null)
					return;

				var playerLevel = Utility.GetPlayerClassLevel(player);
				switch (AsgardLegacy.al_player.al_class)
				{
					case AsgardLegacy.PlayerClass.Berserker:
						{
							if (player.GetSEMan().HaveStatusEffect("SE_Berserker_Frenzy"))
								___m_animator.speed *= Utility.GetLinearValue(
									playerLevel,
									GlobalConfigs_Berserker.al_svr_berserker_frenzy_attackSpeedMultiplierMin,
									GlobalConfigs_Berserker.al_svr_berserker_frenzy_attackSpeedMultiplierMax,
									GlobalConfigs.al_svr_ability4UnlockLevel);

							break;
						}
					case AsgardLegacy.PlayerClass.Ranger:
						{
							if (player.GetSEMan().HaveStatusEffect("SE_Ranger_RapidFire"))
							{
								if (currentAttack.GetWeapon().m_shared.m_skillType == Skills.SkillType.Bows)
									___m_animator.speed *= 2f;
								else if (currentAttack.GetWeapon().m_shared.m_skillType == Skills.SkillType.Swords
									|| currentAttack.GetWeapon().m_shared.m_skillType == Skills.SkillType.Knives)
									___m_animator.speed *= Utility.GetLinearValue(
										playerLevel,
										GlobalConfigs_Ranger.al_svr_ranger_rapidFire_attackSpeedMultiplierMin,
										GlobalConfigs_Ranger.al_svr_ranger_rapidFire_attackSpeedMultiplierMax,
										GlobalConfigs.al_svr_ability2UnlockLevel);
							}

							break;
						}
				}
			}
		}
	}
}
