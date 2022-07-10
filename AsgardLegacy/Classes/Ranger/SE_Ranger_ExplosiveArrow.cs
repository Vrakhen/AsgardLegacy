using Jotunn.Utils;
using UnityEngine;

namespace AsgardLegacy
{
	public class SE_Ranger_ExplosiveArrow : SE_Stats
	{
		public SE_Ranger_ExplosiveArrow()
		{
			name = "SE_Ranger_ExplosiveArrow";
			m_icon = AsgardLegacy.Ability_Sprites[0];
			m_tooltip = "ExplosiveArrow";
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

		[Header("SE_Ranger_ExplosiveArrow")]
		public static float m_baseTTL = 10f;

		public static string m_baseName = "Explosive Arrow";
	}
}
