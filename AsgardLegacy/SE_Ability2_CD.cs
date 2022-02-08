using Jotunn.Utils;

namespace AsgardLegacy
{
    class SE_Ability2_CD : SE_Stats
	{
		public SE_Ability2_CD()
		{
			base.name = "SE_Ability2_CD";
			base.m_ttl = 2f;
		}

		public override bool CanAdd(Character character)
		{
			return character.IsPlayer();
		}
	}
}
