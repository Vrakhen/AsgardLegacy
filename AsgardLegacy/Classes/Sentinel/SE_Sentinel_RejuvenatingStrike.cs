using Jotunn.Utils;
using UnityEngine;

namespace AsgardLegacy
{
	public class SE_Sentinel_RejuvenatingStrike : StatusEffect
	{
		public SE_Sentinel_RejuvenatingStrike()
		{
			name = "SE_Sentinel_RejuvenatingStrike";
			m_icon = AsgardLegacy.Ability_Sprites[0];
			m_tooltip = "RejuvenatingStrike";
			m_name = m_baseName;
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

		[Header("SE_Sentinel_RejuvenatingStrike")]
		public static float m_baseTTL = 10f;

		public static string m_baseName = "Rejuvenating Strike";
	}
}
