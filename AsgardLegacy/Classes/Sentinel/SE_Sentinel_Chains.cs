using Jotunn.Utils;
using UnityEngine;

namespace AsgardLegacy
{
	public class SE_Sentinel_Chains : SE_Stats
	{
		public SE_Sentinel_Chains()
		{
			name = "SE_Sentinel_Chains";
			m_name = m_baseName;
			m_ttl = m_baseTTL;
			m_speedModifier = m_baseSpeedMult;
		}

		public override void ModifySpeed(float baseSpeed, ref float speed)
		{
			speed *= m_speedModifier;

			base.ModifySpeed(baseSpeed, ref speed);
		}

		public override bool CanAdd(Character character)
		{
			return !character.IsPlayer();
		}

		[Header("SE_Sentinel_Chains")]
		public static float m_baseTTL = 5f;

		public static float m_baseSpeedMult = 0f;

		public static string m_baseName = "Chains";
	}
}
