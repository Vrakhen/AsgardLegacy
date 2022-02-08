using Jotunn.Utils;

namespace AsgardLegacy
{
    class SE_Ability3_CD : SE_Stats
	{
		public SE_Ability3_CD()
		{
			base.name = "SE_Ability3_CD";
			base.m_ttl = 2f;
		}

		public override bool CanAdd(Character character)
		{
			return character.IsPlayer();
		}
	}
}
