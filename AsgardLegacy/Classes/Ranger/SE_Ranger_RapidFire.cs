using Jotunn.Utils;
using UnityEngine;

namespace AsgardLegacy
{
	public class SE_Ranger_RapidFire : SE_Stats
	{
		public SE_Ranger_RapidFire()
		{
			name = "SE_Ranger_RapidFire";
			m_icon = AsgardLegacy.Ability_Sprites[2];
			m_tooltip = "RapidFire";
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

		[Header("SE_Ranger_RapidFire")]
		public static float m_baseTTL = 5f;

		public static string m_baseName = "Rapid Fire";
	}
}
