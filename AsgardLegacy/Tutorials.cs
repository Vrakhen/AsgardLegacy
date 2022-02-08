using HarmonyLib;

namespace AsgardLegacy
{
	public static class Tutorials
	{
		[HarmonyPatch(typeof(Player), "OnSpawned")]
		public static class PlayerMuninNotification_Patch
		{
			public static void Postfix(Player __instance)
			{
				var item = new Tutorial.TutorialText
				{
					m_label = "Asgard Legacy",
					m_name = "Asgard_Legacy",
					m_text = "As a reward of your fervor and your past deeds, Gods of Asgard will grant you a spark of their powers to help you in the battles ahead !" +
						"\n\nGo to Eikthyr's altar and activate the nearby runestone, you will be told more..." +
						"\nThe Allfather is watching over you.",
					m_topic = "Harness the gods' powers !"
				};
				if (!Tutorial.instance.m_texts.Contains(item))
					Tutorial.instance.m_texts.Add(item);

				__instance.ShowTutorial("Asgard_Legacy");

				var item2 = new Tutorial.TutorialText
				{
					m_label = "Classes Sacrifices",
					m_name = "al_Sacrifices",
					m_text = "You can inherit the gods' powers by sacrificing a token on the altar:" +
						"\nHeimdall's Guardian : Stone" +
						"\nThor's Berserker : Flint" +
						"\nUllr's Ranger : Board meat" +
						"\nFreya's Mage : Greydwarf eye" +
						"\nTyr's Sentinel : Wood",
					m_topic = "Token Sacrifice"
				};
				if (!Tutorial.instance.m_texts.Contains(item2))
					Tutorial.instance.m_texts.Add(item2);

				var item3 = new Tutorial.TutorialText
				{
					m_label = "Class : Guardian",
					m_name = "al_Guardian",
					m_text = "Adepts of the god Heimdall, Guardians are righteous defenders who taunt their enemies to protect their allies. Solemn in their duty, their legendary vigor can harness waves of attacks before unleashing an avalanche of their own.",
					m_topic = "Class Guardian"
				};
				if (!Tutorial.instance.m_texts.Contains(item3))
					Tutorial.instance.m_texts.Add(item3);

				Tutorial.TutorialText item4 = new Tutorial.TutorialText
				{
					m_label = "Class: Berserker",
					m_name = "al_Berserker",
					m_text = "Adepts of the god Thor, Berserkers are always on the lookout for trouble. Consumed by their burning hearts and reveling in adrenaline, these explosive fighters pummel their foes with an unrelenting wrath.",
					m_topic = "Class Berserker"
				};
				if (!Tutorial.instance.m_texts.Contains(item4))
					Tutorial.instance.m_texts.Add(item4);

				var item5 = new Tutorial.TutorialText
				{
					m_label = "Class: Ranger",
					m_name = "al_Ranger",
					m_text = "Adepts of the god Ullr, Rangers rely on their all-seeing eyes and mastery with the bow to strike at targets with deadly precision. Striding with inhuman speed, they engage and disengage with their enemies as they see fit." +
					"\n\nExplosive Arrow (Level 1 - Active) : The next arrow you shoot will explode, damaging enemies according to the arrow fired." +
					"\nBow Specialist (Level 3 - Passive) : Increase your arrow damage and velocity." +
					"\nRapid Fire (Level 5 - Active) : For a short period of time, you will attack faster when attacking with a bow, a sword or a knife." +
					"\nSpeed Burst (Level 7 - Passive) : After making a dodge roll, you will gain a burst of speed." +
					"\nShadow Stalk (Level 10 - Active) : Vanish in darkness, gaining a short burst of speed, improving your speed and making you indetectabe while crouching. Lose enemies' focus on cast." +
					"\nLong Strider (Level 13 - Passive) : Decrease stamina drain while running and jumping." +
					"\nBow Specialist (Level 16 - Active) : Temporarily mark an enemy with your next attack. A marked enemy will take more damage from you and your allies." +
					"\nArrow Saver (Level 20 - Passive) : When damaging an enemy with arrows, you have a 50% chance to recover them.",
					m_topic = "Class Ranger"
				};
				if (!Tutorial.instance.m_texts.Contains(item5))
					Tutorial.instance.m_texts.Add(item5);

				var item6 = new Tutorial.TutorialText
				{
					m_label = "Class: Mage",
					m_name = "al_Mage",
					m_text = "Adepts of the goddess Freya, Mages are the devoted safekeepers of the Realm. Masters in the ancient art of rune cratfing, they harness elemental powers to unleash their wrath upon their enemies.",
					m_topic = "Class Mage"
				};
				if (!Tutorial.instance.m_texts.Contains(item6))
					Tutorial.instance.m_texts.Add(item6);

				var item7 = new Tutorial.TutorialText
				{
					m_label = "Class: Sentinel",
					m_name = "al_Sentinel",
					m_text = "Adepts of the god Tyr, Sentinels are devoted fighters who protect their allies and smite their enemies by drawing from the power of the World Tree. Keeping their cool in the midst of battle, they inspire and empower allies to achieve victory.",
					m_topic = "Class Sentinel"
				};
				if (!Tutorial.instance.m_texts.Contains(item7))
					Tutorial.instance.m_texts.Add(item7);
			}
		}

		[HarmonyPatch(typeof(RuneStone), "Interact")]
		public static class ClassSacrificeTutorial_Patch
		{
			public static void Postfix()
			{
				Player.m_localPlayer.ShowTutorial("al_Sacrifices", false);
			}
		}

	}
}
