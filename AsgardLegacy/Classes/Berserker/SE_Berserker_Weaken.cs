using Jotunn.Utils;
using UnityEngine;

namespace AsgardLegacy
{
	public class SE_Berserker_Weaken : SE_Stats
	{
		public SE_Berserker_Weaken()
		{
			name = "SE_Berserker_Weaken";
			m_name = m_baseName;
			m_ttl = m_baseTTL;
			m_damageModifier = m_baseDamageMult;
			m_speedModifier = m_baseSpeedMult;
		}

		public override void ModifyAttack(Skills.SkillType skill, ref HitData hitData)
		{
			hitData.m_damage.Modify(m_damageModifier);

			base.ModifyAttack(skill, ref hitData);
		}

		public override void ModifySpeed(float baseSpeed, ref float speed)
		{
			speed *= m_speedModifier;

			base.ModifySpeed(baseSpeed, ref speed);
		}

		public override bool CanAdd(Character character)
		{
			return !character.IsPlayer();
		}

		[Header("SE_Berserker_Weaken")]
		public static float m_baseTTL = 5f;

		public static float m_baseDamageMult = .75f;
		public static float m_baseSpeedMult = .75f;

		public static string m_baseName = "Weaken";
	}
}
