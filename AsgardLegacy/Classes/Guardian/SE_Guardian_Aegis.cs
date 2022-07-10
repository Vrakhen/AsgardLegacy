using Jotunn.Utils;
using UnityEngine;

namespace AsgardLegacy
{
	public class SE_Guardian_Aegis : SE_Stats
	{
		public SE_Guardian_Aegis()
		{
			name = "SE_Guardian_Aegis";
			m_icon = AsgardLegacy.Ability_Sprites[1];
			m_tooltip = "Aegis";
			m_name = m_baseName;
			m_ttl = m_baseTTL;
			m_damageModifier = m_baseDamageMult;
		}

		public override void ModifyAttack(Skills.SkillType skill, ref HitData hitData)
		{
			hitData.m_damage.Modify(1f - m_damageModifier);
			base.ModifyAttack(skill, ref hitData);
		}

		public override bool IsDone()
		{
			return AsgardLegacy.al_player.al_class != AsgardLegacy.PlayerClass.Guardian || base.IsDone();
		}

		public override bool CanAdd(Character character)
		{
			return character.IsPlayer() && AsgardLegacy.al_player.al_class == AsgardLegacy.PlayerClass.Guardian;
		}

		[Header("SE_Guardian_Aegis")]
		public static float m_baseTTL = 5f;

		public static float m_baseDamageMult = .75f;

		public static string m_baseName = "Aegis";
	}
}
