using Jotunn.Utils;
using UnityEngine;

namespace AsgardLegacy
{
	public class SE_Ranger_RangerMark : SE_Stats
	{
		public SE_Ranger_RangerMark()
		{
			name = "SE_Ranger_RangerMark";
			m_icon = AsgardLegacy.Ability_Sprites[3];
			m_tooltip = "RangerMark";
			m_name = m_baseName;
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

		public static Sprite AbilityIcon;
		public static GameObject GO_SEFX;

		[Header("SE_Ranger_RangerMark")]
		public static float m_baseTTL = 5f;

		public static string m_baseName = "Ranger Mark";
	}
}
