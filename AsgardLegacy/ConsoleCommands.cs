using UnityEngine;
using Object = UnityEngine.Object;

namespace AsgardLegacy
{
	public static class ConsoleCommands
	{
		public static bool CheatRaiseSkill(Skills skill_instance, string name, float value, Player player)
		{
			if (name.ToLower() != "classlevel")
				return false;

			player.RaiseSkill(AsgardLegacy.ClassLevelSkill, value);
			return true;
		}
		public static bool CheatResetSkill(Skills skill_instance, string name, float value, Player player)
		{
			if (name.ToLower() != "classlevel")
				return false;

			player.RaiseSkill(AsgardLegacy.ClassLevelSkill, value);
			return true;
		}

		public static void CheatChangeClass(string className)
		{
			switch (className.ToLower())
			{
				case "guardian":
					AsgardLegacy.al_player.al_class = AsgardLegacy.PlayerClass.Guardian;
					break;
				case "berserker":
					AsgardLegacy.al_player.al_class = AsgardLegacy.PlayerClass.Berserker;
					break;
				case "ranger":
					AsgardLegacy.al_player.al_class = AsgardLegacy.PlayerClass.Ranger;
					break;
				case "sentinel":
					AsgardLegacy.al_player.al_class = AsgardLegacy.PlayerClass.Sentinel;
					break;
				case "none":
					AsgardLegacy.al_player.al_class = AsgardLegacy.PlayerClass.None;
					break;
			}

			Console.instance.Print("Class changed to " + className);
			AsgardLegacy.UpdateALPlayer(Player.m_localPlayer);
			AsgardLegacy.NameCooldowns();

			if (AsgardLegacy.abilitiesStatus == null)
				return;

			foreach (var rectTransform in AsgardLegacy.abilitiesStatus)
			{
				if (rectTransform.gameObject != null)
					Object.Destroy(rectTransform.gameObject);
			}
			AsgardLegacy.abilitiesStatus.Clear();
		}
	}
}
