using Jotunn.Utils;
using UnityEngine;

namespace AsgardLegacy
{
	public class SE_Sentinel_MendingSpirits : StatusEffect
	{
		public SE_Sentinel_MendingSpirits()
		{
			name = "SE_Sentinel_MendingSpirits";
			m_icon = AsgardLegacy.Ability_Sprites[1];
			m_tooltip = "MendingSpirits";
			m_name = m_baseName;
			m_ttl = m_baseTTL;
		}
		public override void UpdateStatusEffect(float dt)
		{
			base.UpdateStatusEffect(dt);
			m_timer -= dt;
			if (m_timer <= 0f)
			{
				m_timer = m_healingInterval;

				var characters = Character.GetAllCharacters();
				foreach (var character in characters)
				{
					if (!character.IsPlayer()
						|| (character.transform.position - m_character.transform.position).magnitude > m_radius
						|| !Utility.LOS_IsValid(character, m_character.transform.position, m_character.GetCenterPoint()))
						continue;

					character.Heal(m_healing);

					Instantiate(ZNetScene.instance.GetPrefab("fx_guardstone_permitted_removed"), character.transform.position, Quaternion.identity);
				}
			}
		}

		public override bool IsDone()
		{
			return AsgardLegacy.al_player.al_class != AsgardLegacy.PlayerClass.Sentinel || base.IsDone();
		}

		public override bool CanAdd(Character character)
		{
			return character.IsPlayer() && AsgardLegacy.al_player.al_class == AsgardLegacy.PlayerClass.Sentinel;
		}

		[Header("SE_Sentinel_MendingSpirits")]
		public static float m_baseTTL = 10f;

		public float m_timer = 0f;
		public float m_healingInterval = 1f;
		public float m_healing = 1f;
		public float m_radius = 1f;
		public static string m_baseName = "Mending Spirits";
	}
}
