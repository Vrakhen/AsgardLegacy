using Jotunn.Utils;
using UnityEngine;

namespace AsgardLegacy
{
	public class SE_Guardian_Retribution : SE_Stats
	{
		public SE_Guardian_Retribution()
		{
			name = "SE_Guardian_Retribution";
			m_icon = AssetUtils.LoadSpriteFromFile("JotunnModExample/Assets/test_var4.png");// AbilityIcon;
			m_tooltip = "Retribution";
			m_name = m_baseName;
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

		[Header("SE_Guardian_Retribution")]
		public static float m_baseTTL = 5f;

		public static string m_baseName = "Retribution";
	}
}
