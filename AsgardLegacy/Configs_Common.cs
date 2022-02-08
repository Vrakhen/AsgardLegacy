using BepInEx.Configuration;

namespace AsgardLegacy
{
	class Configs_Common
	{
		public static ConfigEntry<float> al_svr_maxLevel;

		public static ConfigEntry<float> al_svr_ability1UnlockLevel;
		public static ConfigEntry<float> al_svr_ability2UnlockLevel;
		public static ConfigEntry<float> al_svr_ability3UnlockLevel;
		public static ConfigEntry<float> al_svr_ability4UnlockLevel;

		public static ConfigEntry<float> al_svr_passive1UnlockLevel;
		public static ConfigEntry<float> al_svr_passive2UnlockLevel;
		public static ConfigEntry<float> al_svr_passive3UnlockLevel;
		public static ConfigEntry<float> al_svr_passive4UnlockLevel;

		public static ConfigEntry<float> al_svr_skillGainAoeBaseHit;
		public static ConfigEntry<float> al_svr_skillGainAoeMultipleHit;
		public static ConfigEntry<float> al_svr_skillGainBuffCast;
		public static ConfigEntry<float> al_svr_skillGainPassiveTrigger;

		public static void InitializeConfig(ConfigFile config)
		{
			al_svr_maxLevel = config.Bind("Common", "al_svr_maxLevel", 20f);

			al_svr_ability1UnlockLevel = config.Bind("Common", "al_svr_ability1UnlockLevel", 1f);
			al_svr_ability2UnlockLevel = config.Bind("Common", "al_svr_ability2UnlockLevel", 5f);
			al_svr_ability3UnlockLevel = config.Bind("Common", "al_svr_ability3UnlockLevel", 10f);
			al_svr_ability4UnlockLevel = config.Bind("Common", "al_svr_ability4UnlockLevel", 16f);

			al_svr_passive1UnlockLevel = config.Bind("Common", "al_svr_passive1UnlockLevel", 3f);
			al_svr_passive2UnlockLevel = config.Bind("Common", "al_svr_passive2UnlockLevel", 7f);
			al_svr_passive3UnlockLevel = config.Bind("Common", "al_svr_passive3UnlockLevel", 13f);
			al_svr_passive4UnlockLevel = config.Bind("Common", "al_svr_passive4UnlockLevel", 20f);

			al_svr_skillGainAoeBaseHit = config.Bind("Common", "al_svr_skillGainAoeBaseHit", 1f);
			al_svr_skillGainAoeMultipleHit = config.Bind("Common", "al_svr_skillGainAoeMultipleHit", .25f);
			al_svr_skillGainBuffCast = config.Bind("Common", "al_svr_skillGainBuffCast", 1f);
			al_svr_skillGainPassiveTrigger = config.Bind("Common", "al_svr_skillGainPassiveTrigger", .5f);


			GlobalConfigs.ConfigStrings.Add("al_svr_maxLevel", al_svr_maxLevel.Value);

			GlobalConfigs.ConfigStrings.Add("al_svr_ability1UnlockLevel", al_svr_ability1UnlockLevel.Value);
			GlobalConfigs.ConfigStrings.Add("al_svr_ability2UnlockLevel", al_svr_ability2UnlockLevel.Value);
			GlobalConfigs.ConfigStrings.Add("al_svr_ability3UnlockLevel", al_svr_ability3UnlockLevel.Value);
			GlobalConfigs.ConfigStrings.Add("al_svr_ability4UnlockLevel", al_svr_ability4UnlockLevel.Value);

			GlobalConfigs.ConfigStrings.Add("al_svr_passive1UnlockLevel", al_svr_passive1UnlockLevel.Value);
			GlobalConfigs.ConfigStrings.Add("al_svr_passive2UnlockLevel", al_svr_passive2UnlockLevel.Value);
			GlobalConfigs.ConfigStrings.Add("al_svr_passive3UnlockLevel", al_svr_passive3UnlockLevel.Value);
			GlobalConfigs.ConfigStrings.Add("al_svr_passive4UnlockLevel", al_svr_passive4UnlockLevel.Value);

			GlobalConfigs.ConfigStrings.Add("al_svr_skillGainAoeBaseHit", al_svr_skillGainAoeBaseHit.Value);
			GlobalConfigs.ConfigStrings.Add("al_svr_skillGainAoeMultipleHit", al_svr_skillGainAoeMultipleHit.Value);
			GlobalConfigs.ConfigStrings.Add("al_svr_skillGainBuffCast", al_svr_skillGainBuffCast.Value);
			GlobalConfigs.ConfigStrings.Add("al_svr_skillGainPassiveTrigger", al_svr_skillGainPassiveTrigger.Value);
		}
	}
}
