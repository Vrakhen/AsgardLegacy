using Jotunn.Utils;

namespace AsgardLegacy
{
    class SE_Ability4_CD : SE_Stats
	{
		public SE_Ability4_CD()
		{
			base.name = "SE_Ability4_CD";
			base.m_ttl = 2f;
		}

		public override bool CanAdd(Character character)
		{
			return character.IsPlayer();
		}
	}
}
