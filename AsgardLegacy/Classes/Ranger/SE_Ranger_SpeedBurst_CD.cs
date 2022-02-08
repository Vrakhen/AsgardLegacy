using UnityEngine;

namespace AsgardLegacy
{
	public class SE_Ranger_SpeedBurst_CD : SE_Stats
	{
		public SE_Ranger_SpeedBurst_CD()
		{
			name = "SE_Ranger_SpeedBurst_CD";
			m_ttl = m_baseTTL;
		}

		public override bool IsDone()
		{
			return AsgardLegacy.al_player.al_class != AsgardLegacy.PlayerClass.Ranger || base.IsDone();
		}

		public override bool CanAdd(Character character)
		{
			return character.IsPlayer() && AsgardLegacy.al_player.al_class == AsgardLegacy.PlayerClass.Ranger;
		}

		[Header("SE_Ranger_SpeedBurst_CD")]
		public static float m_baseTTL = 10f;
	}
}
