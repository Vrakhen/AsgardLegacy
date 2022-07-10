using UnityEngine;

namespace AsgardLegacy
{
	public class SE_Sentinel_VengefulWave_CD : StatusEffect
	{
		public SE_Sentinel_VengefulWave_CD()
		{
			name = "SE_Sentinel_VengefulWave_CD";
			m_ttl = m_baseTTL;
		}

		public override bool CanAdd(Character character)
		{
			return character.IsPlayer();
		}

		[Header("SE_Sentinel_VengefulWave_CD")]
		public static float m_baseTTL = 60f;
	}
}
