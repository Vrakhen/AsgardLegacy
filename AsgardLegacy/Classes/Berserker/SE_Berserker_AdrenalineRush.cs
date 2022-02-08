using UnityEngine;

namespace AsgardLegacy
{
	public class SE_Berserker_AdrenalineRush : StatusEffect
	{
		public SE_Berserker_AdrenalineRush()
		{
			name = "SE_Berserker_AdrenalineRush";
			m_ttl = m_baseTTL;
		}

		public override bool CanAdd(Character character)
		{
			return character.IsPlayer();
		}

		[Header("SE_Berserker_AdrenalineRush")]
		public static float m_baseTTL = 5f;
	}
}
