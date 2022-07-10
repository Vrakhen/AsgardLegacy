using System.Collections.Generic;

namespace AsgardLegacy
{
	public class GlobalConfigs
	{
		public static Dictionary<string, float> ConfigStrings = new Dictionary<string, float> ();

		public static float al_svr_maxLevel
		{ get { return ConfigStrings.ContainsKey("al_svr_maxLevel") ? ConfigStrings["al_svr_maxLevel"] : 20f; } }

		public static float al_svr_ability1UnlockLevel
		{ get { return ConfigStrings.ContainsKey("al_svr_ability1UnlockLevel") ? ConfigStrings["al_svr_ability1UnlockLevel"] : 1f; } }
		public static float al_svr_ability2UnlockLevel
		{ get { return ConfigStrings.ContainsKey("al_svr_ability2UnlockLevel") ? ConfigStrings["al_svr_ability2UnlockLevel"] : 5f; } }
		public static float al_svr_ability3UnlockLevel
		{ get { return ConfigStrings.ContainsKey("al_svr_ability3UnlockLevel") ? ConfigStrings["al_svr_ability3UnlockLevel"] : 10f; } }
		public static float al_svr_ability4UnlockLevel
		{ get { return ConfigStrings.ContainsKey("al_svr_ability4UnlockLevel") ? ConfigStrings["al_svr_ability4UnlockLevel"] : 16f; } }
		public static float al_svr_passive1UnlockLevel
		{ get { return ConfigStrings.ContainsKey("al_svr_passive1UnlockLevel") ? ConfigStrings["al_svr_passive1UnlockLevel"] : 3f; } }
		public static float al_svr_passive2UnlockLevel
		{ get { return ConfigStrings.ContainsKey("al_svr_passive2UnlockLevel") ? ConfigStrings["al_svr_passive2UnlockLevel"] : 7f; } }
		public static float al_svr_passive3UnlockLevel
		{ get { return ConfigStrings.ContainsKey("al_svr_passive3UnlockLevel") ? ConfigStrings["al_svr_passive3UnlockLevel"] : 12; } }
		public static float al_svr_passive4UnlockLevel
		{ get { return ConfigStrings.ContainsKey("al_svr_passive4UnlockLevel") ? ConfigStrings["al_svr_passive4UnlockLevel"] : 14; } }
		public static float al_svr_passive5UnlockLevel
		{ get { return ConfigStrings.ContainsKey("al_svr_passive5UnlockLevel") ? ConfigStrings["al_svr_passive5UnlockLevel"] : 18; } }
		public static float al_svr_passive6UnlockLevel
		{ get { return ConfigStrings.ContainsKey("al_svr_passive6UnlockLevel") ? ConfigStrings["al_svr_passive6UnlockLevel"] : 20f; } }

		public static float al_svr_skillGainAoeBaseHit
		{ get { return ConfigStrings.ContainsKey("al_svr_skillGainAoeBaseHit") ? ConfigStrings["al_svr_skillGainAoeBaseHit"] : 1f; } }
		public static float al_svr_skillGainAoeMultipleHit
		{ get { return ConfigStrings.ContainsKey("al_svr_skillGainAoeMultipleHit") ? ConfigStrings["al_svr_skillGainAoeMultipleHit"] : .25f; } }
		public static float al_svr_skillGainBuffCast
		{ get { return ConfigStrings.ContainsKey("al_svr_skillGainBuffCast") ? ConfigStrings["al_svr_skillGainBuffCast"] : 1f; } }
		public static float al_svr_skillGainPassiveTrigger
		{ get { return ConfigStrings.ContainsKey("al_svr_skillGainPassiveTrigger") ? ConfigStrings["al_svr_skillGainPassiveTrigger"] : .5f; } }


	}
}
