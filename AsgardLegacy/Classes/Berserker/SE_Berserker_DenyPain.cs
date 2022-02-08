using UnityEngine;

namespace AsgardLegacy
{
	public class SE_Berserker_DenyPain : StatusEffect
	{
		public SE_Berserker_DenyPain()
		{
			name = "SE_Berserker_DenyPain";
			m_ttl = m_baseTTL;
		}

		public override bool CanAdd(Character character)
		{
			return character.IsPlayer();
		}

		[Header("SE_Berserker_DenyPain")]
		public static float m_baseTTL = 5f;
	}
}
