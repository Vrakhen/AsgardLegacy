using UnityEngine;

namespace AsgardLegacy
{
	public class SE_Ranger_RangerMarked : StatusEffect
	{
		public SE_Ranger_RangerMarked()
		{
			name = "SE_Ranger_RangerMarked";
			m_ttl = m_baseTTL;
		}

		public override bool CanAdd(Character character)
		{
			return !character.IsPlayer();
		}

		public static GameObject GO_SEFX;

		[Header("SE_Ranger_RangerMarked")]
		public static float m_baseTTL = 15f;
	}
}
