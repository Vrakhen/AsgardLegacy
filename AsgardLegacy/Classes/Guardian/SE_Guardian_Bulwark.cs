using UnityEngine;

namespace AsgardLegacy
{
	public class SE_Guardian_Bulwark : SE_Stats
	{
		public SE_Guardian_Bulwark()
		{
			name = "SE_Guardian_Bulwark";
			m_ttl = m_baseTTL;
			m_activationAnimation = "vfx_Potion_stamina_medium";
	}

		public override void UpdateStatusEffect(float dt)
		{
			base.UpdateStatusEffect(dt);
			m_timer -= dt;
			if (m_timer <= 0f)
			{
				m_timer = m_interval;
				m_character.AddStamina(m_staminaModifier);
			}
		}

		public override bool IsDone()
		{
			return AsgardLegacy.al_player.al_class != AsgardLegacy.PlayerClass.Guardian || base.IsDone();
		}

		public override bool CanAdd(Character character)
		{
			return character.IsPlayer() && AsgardLegacy.al_player.al_class == AsgardLegacy.PlayerClass.Guardian;
		}

		[Header("SE_Guardian_Bulwark")]
		public static float m_baseTTL = 5f;
		public float m_staminaModifier = 5f;
		public float m_interval = .5f;
		public float m_timer = 0f;
	}
}
