using Jotunn.Utils;
using System.Linq;
using UnityEngine;

namespace AsgardLegacy
{
	public class SE_Ranger_ShadowStalk : SE_Stats
	{
		public SE_Ranger_ShadowStalk()
		{
			name = "SE_Ranger_ShadowStalk";
			m_icon = AssetUtils.LoadSpriteFromFile("JotunnModExample/Assets/test_var1.png");// AbilityIcon;
			m_tooltip = "ShadowStalk";
			m_name = m_baseName;
			m_ttl = m_baseTTL;
		}

		public override void ModifySpeed(float baseSpeed, ref float speed)
		{
			if (m_character.IsSneaking())
				speed *= crouchSpeedAmount;
			else if (speedDuration > 0f)
				speed *= speedAmount;

			base.ModifySpeed(baseSpeed, ref speed);
		}

		public override void UpdateStatusEffect(float dt)
		{
			base.UpdateStatusEffect(dt);
			m_timer -= dt;
			if (m_timer <= 0f)
			{
				m_timer = 1f;
				speedDuration -= 1f;
			}
		}

		public override bool CanAdd(Character character)
		{
			return character.IsPlayer();
		}

		public static Sprite AbilityIcon;

		public static GameObject GO_SEFX;

		[Header("SE_Ranger_ShadowStalk")]
		public static float m_baseTTL = 20f;

		public float speedDuration = 3f;

		public float speedAmount = 1.75f;

		public float crouchSpeedAmount = 1.75f;

		private float m_timer = 1f;

		public static string m_baseName = "Shadow Stalk";
	}
}
