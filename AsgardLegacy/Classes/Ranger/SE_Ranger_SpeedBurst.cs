using UnityEngine;

namespace AsgardLegacy
{
	public class SE_Ranger_SpeedBurst : SE_Stats
	{
		public SE_Ranger_SpeedBurst()
		{
			name = "SE_Ranger_SpeedBurst";
			m_name = "SpeedBurst";
			m_ttl = m_baseTTL;
			m_speedModifier = m_baseSpeedMult;
		}

		public override void ModifySpeed(float baseSpeed, ref float speed)
		{
			speed *= m_speedModifier;
			base.ModifySpeed(baseSpeed, ref speed);
		}

		public override bool IsDone()
		{
			return AsgardLegacy.al_player.al_class != AsgardLegacy.PlayerClass.Ranger || base.IsDone();
		}

		public override bool CanAdd(Character character)
		{
			return character.IsPlayer() && AsgardLegacy.al_player.al_class == AsgardLegacy.PlayerClass.Ranger;
		}

		[Header("SE_Ranger_SpeedBurst")]
		public static float m_baseTTL = 3f;
		public static float m_baseSpeedMult = 1.5f;
	}
}
