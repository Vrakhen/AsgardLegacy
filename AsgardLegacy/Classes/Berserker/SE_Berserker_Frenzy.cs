﻿using Jotunn.Utils;
using UnityEngine;

namespace AsgardLegacy
{
	public class SE_Berserker_Frenzy : SE_Stats
	{
		public SE_Berserker_Frenzy()
		{
			name = "SE_Berserker_Frenzy";
			m_icon = AssetUtils.LoadSpriteFromFile("JotunnModExample/Assets/test_var3.png");
			m_name = m_baseName;
			m_ttl = m_baseTTL;
			m_damageModifier = m_baseDamageMult;
		}

		public override void ModifyAttack(Skills.SkillType skill, ref HitData hitData)
		{
			hitData.m_damage.m_lightning += hitData.m_damage.GetTotalPhysicalDamage() * m_damageModifier;

			base.ModifyAttack(skill, ref hitData);
		}

		public override bool CanAdd(Character character)
		{
			return character.IsPlayer();
		}

		[Header("SE_Berserker_Frenzy")]
		public static float m_baseTTL = 5f;

		public static float m_baseDamageMult = .75f;

		public static string m_baseName = "Frenzy";
	}
}
