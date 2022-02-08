using UnityEngine;

namespace AsgardLegacy
{
	public class SE_Berserker_AdrenalineRush_CD : StatusEffect
	{
		public SE_Berserker_AdrenalineRush_CD()
		{
			name = "SE_Berserker_AdrenalineRush_CD";
			m_ttl = m_baseTTL;
		}

		public override bool CanAdd(Character character)
		{
			return character.IsPlayer();
		}

		[Header("SE_Berserker_AdrenalineRush_CD")]
		public static float m_baseTTL = 60f;
	}
}
