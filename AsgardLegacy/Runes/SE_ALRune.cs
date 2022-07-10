namespace AsgardLegacy
{
    class SE_ALRune : StatusEffect
	{
		public SE_ALRune()
		{
			name = "SE_ALRune";
			m_ttl = 600f;
		}

		public override bool CanAdd(Character character)
		{
			return character.IsPlayer();
		}

		public HitData.DamageType RuneType;
		public float RuneDamageBonus;
		public float RuneResistanceBonus;
		public float RuneDamageMalus;
		public float RuneResistanceMalus;
	}
}
