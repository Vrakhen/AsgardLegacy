using UnityEngine;

namespace AsgardLegacy
{
	public class SE_Sentinel_CleansingRoll_CD : StatusEffect
	{
		public SE_Sentinel_CleansingRoll_CD()
		{
			name = "SE_Sentinel_CleansingRoll_CD";
			m_ttl = m_baseTTL;
		}

		public override bool IsDone()
		{
			return AsgardLegacy.al_player.al_class != AsgardLegacy.PlayerClass.Sentinel || base.IsDone();
		}

		public override bool CanAdd(Character character)
		{
			return character.IsPlayer() && AsgardLegacy.al_player.al_class == AsgardLegacy.PlayerClass.Sentinel;
		}

		[Header("SE_Sentinel_CleansingRoll_CD")]
		public static float m_baseTTL = 5f;
	}
}
