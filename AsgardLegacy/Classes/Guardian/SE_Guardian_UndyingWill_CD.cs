using UnityEngine;

namespace AsgardLegacy
{
	public class SE_Guardian_UndyingWill_CD : SE_Stats
	{
		public SE_Guardian_UndyingWill_CD()
		{
			name = "SE_Guardian_UndyingWill_CD";
			m_tooltip = "Your strength of will have saved you from death";
			m_ttl = m_baseTTL;
		}

		public override bool IsDone()
		{
			return AsgardLegacy.al_player.al_class != AsgardLegacy.PlayerClass.Guardian || base.IsDone();
		}

		public override bool CanAdd(Character character)
		{
			return character.IsPlayer() && AsgardLegacy.al_player.al_class == AsgardLegacy.PlayerClass.Guardian;
		}

		[Header("SE_Guardian_UndyingWill_CD")]
		public static float m_baseTTL = 900f;
	}
}
