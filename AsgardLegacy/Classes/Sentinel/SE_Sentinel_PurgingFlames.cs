using Jotunn.Utils;
using UnityEngine;

namespace AsgardLegacy
{
	public class SE_Sentinel_PurgingFlames : SE_Stats
	{
		public SE_Sentinel_PurgingFlames()
		{
			name = "SE_Sentinel_PurgingFlames";
			m_icon = AsgardLegacy.Ability_Sprites[3];
			m_tooltip = "PurgingFlames";
			m_name = m_baseName;
			m_ttl = m_baseTTL;
		}

		public override bool CanAdd(Character character)
		{
			return character.IsPlayer();
		}

		[Header("SE_Sentinel_PurgingFlames")]
		public static float m_baseTTL = 10f;

		public static string m_baseName = "Purging Flames";
	}
}
