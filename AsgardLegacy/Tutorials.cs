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
					m_label = "Class Sacrifices",
					m_name = "al_Sacrifices",
					m_text = "You can inherit the gods' powers by sacrificing a token on the altar:" +
						"\n\nHeimdall's Guardian : Stone" +
						"\nGuardians are righteous defenders who taunt their enemies to protect their allies. Solemn in their duty, their legendary vigor can harness waves of attacks before unleashing an avalanche of their own." +
						"\n\nThor's Berserker : Flint" +
						"\nBerserkers are always on the lookout for trouble. Consumed by their burning hearts and reveling in adrenaline, these explosive fighters pummel their foes with an unrelenting wrath." +
						"\n\nUllr's Ranger : Board meat" +
						"\nRangers rely on their all-seeing eyes and mastery with the bow to strike at targets with deadly precision. Striding with inhuman speed, they engage and disengage with their enemies as they see fit." +
						"\n\nTyr's Sentinel : Wood" +
						"\nSentinels are devoted fighters who protect their allies and smite their enemies by drawing from the power of the World Tree. Keeping their cool in the midst of battle, they inspire and empower allies to achieve victory.",
					m_topic = "Token Sacrifice"
				};
				if (!Tutorial.instance.m_texts.Contains(item2))
					Tutorial.instance.m_texts.Add(item2);

				var item3 = new Tutorial.TutorialText
				{
					m_label = "Class : Guardian",
					m_name = "al_Guardian",
					m_text = "Adepts of the god Heimdall, Guardians are righteous defenders who taunt their enemies to protect their allies. Solemn in their duty, their legendary vigor can harness waves of attacks before unleashing an avalanche of their own." +
					"\n\nShatter Fall (Level 1 - Active) : Launch into the air and come crashing down, applying damage at the point of impact." +
					"\n\nForce of Nature (Level 3 - Passive) : Increase your max health and stamina." +
					"\n\nAegis (Level 5 - Active) : Highly increase armor but decrease damages." +
					"\n\nBulwark (Level 7 - Passive) : Increase block power with shields. Gain a burst of stamina when parrying, further increase tower shield block power." +
					"\n\nIce Crush (Level 10 - Active) : Create a frost explosion around you." +
					"\n\nWar Cry (Level 12 - Passive) : Taunt enemies around you after using special attack." +
					"\n\nImmovable (Level 14 - Passive) : Reduce knockback on enemy attacks." +
					"\n\nRetribution (Level 16 - Active) : Start collecting damage blocked, then triggers a fiery explosion around you, damaging enemies depending on the damage collected." +
					"\n\nAbsorb Elements (Level 18 - Passive) : Blocked elemental damage is absorbed as stamina." +
					"\n\nUndying Will (Level 20 - Passive) : When taking lethal damage, gain 25% hp instead.",
					m_topic = "Class Guardian"
				};
				if (!Tutorial.instance.m_texts.Contains(item3))
					Tutorial.instance.m_texts.Add(item3);

				Tutorial.TutorialText item4 = new Tutorial.TutorialText
				{
					m_label = "Class: Berserker",
					m_name = "al_Berserker",
					m_text = "Adepts of the god Thor, Berserkers are always on the lookout for trouble. Consumed by their burning hearts and reveling in adrenaline, these explosive fighters pummel their foes with an unrelenting wrath." +
					"\n\nCharge (Level 1 - Active) : Dash forward and strike enemies as you pass." +
					"\n\nTwo-handed expert (Level 3 - Passive) : Bonus to two-handed weapons damage and stamina consumption." +
					"\n\nDreadful Roar (Level 5 - Active) : Shout that slows and weakens enemies around you." +
					"\n\nReckless (Level 7 - Passive) : Gain bonus physical damage and damage mitigation for missing health." +
					"\n\nRaging Storm (Level 10 - Active) : Infuse next attacks with lightning damage" +
					"\n\nSiphon Life (Level 12 - Passive) : Gain health and stamina when a nearby creature dies." +
					"\n\nAdrenaline Rush (Level 14 - Passive) : If being hit at low health, consume less stamina for a short duration." +
					"\n\nFrenzy (Level 16 - Active) : Increase melee attack speed and consume no stamina while attacking" +
					"\n\nRebuke (Level 18 - Passive) : Reflects a portion of incoming damage to attacker." +
					"\n\nDeny Pain (Level 20 - Passive) : Gain short damage immunity after using weapon specal attack.",
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
					"\n\nBow Specialist (Level 3 - Passive) : Increase your arrow damage and velocity." +
					"\n\nShadow Stalk (Level 5 - Active) : Vanish in darkness, gaining a short burst of speed, improving your speed and making you indetectabe while crouching. Lose enemies' focus on cast." +
					"\n\nLong Strider (Level 7 - Passive) : Decrease stamina drain while running and jumping." +
					"\n\nRapid Fire (Level 10 - Active) : For a short period of time, you will attack faster when attacking with a bow, a sword or a knife, but deal less damage." +
					"\n\nSpeed Burst (Level 12 - Passive) : After making a dodge roll, you will gain a burst of speed." +
					"\n\nGo For The Eyes (Level 14 - Passive) : Increase backstab and stagger damage." +
					"\n\nRanger Mark (Level 16 - Active) : Temporarily mark an enemy with your next attack. A marked enemy will take more damage from you and your allies." +
					"\n\nElemental Touch (Level 18 - Passive) : When damaging an enemy, you have a chance to add elemental damage." +
					"\n\nArrow Saver (Level 20 - Passive) : When damaging an enemy with arrows, you have a 50% chance to recover them.",
					m_topic = "Class Ranger"
				};
				if (!Tutorial.instance.m_texts.Contains(item5))
					Tutorial.instance.m_texts.Add(item5);

				var item6 = new Tutorial.TutorialText
				{
					m_label = "Class: Sentinel",
					m_name = "al_Sentinel",
					m_text = "Adepts of the god Tyr, Sentinels are devoted fighters who protect their allies and smite their enemies by drawing from the power of the World Tree. Keeping their cool in the midst of battle, they inspire and empower allies to achieve victory." +
					"\n\nRejuvenating Strike (Level 1 - Active) : Next attack deal additional damage and grants stamina to yourself and allies around." +
					"\n\nOne-Handed Master (Level 3 - Passive) : Bonus to one-handed weapons damage and stamina consumption." +
					"\n\nMending Spirits (Level 5 - Active) : Pulse heals around yourself." +
					"\n\nDwarven Fortitude (Level 7 - Passive) : Increase your armor and elemental resistance." +
					"\n\nChains of Light (Level 10 - Active) : Root enemies with fire and lightning infused chains." +
					"\n\nHealer's Gift (Level 12 - Passive) : Grant yourself and allies health regeneration after using a weapon's special attack." +
					"\n\nCleansing Roll (Level 14 - Passive) : Cleanse status effects after a dodge roll." +
					"\n\nPurging Flames (Level 16 - Active) : Trigger a fiery explosion around you, burning enemies, cleansing status on allies, regenerating their health and stamina." +
					"\n\nVengeful Wave (Level 18 - Passive) : If being hit at low health, triggers a wave that staggers nearby enemies." +
					"\n\nPowerful Build (Level 20 - Passive) : Greatly reduce equipement movement speed penalty.",
					m_topic = "Class Sentinel"
				};
				if (!Tutorial.instance.m_texts.Contains(item6))
					Tutorial.instance.m_texts.Add(item6);

				var item7 = new Tutorial.TutorialText
				{
					m_label = "Odin Runes",
					m_name = "al_Runes",
					m_text = "The Allfather has granted you a part of his knowledge, allowing you to craft runes to help you in your journey." +
					"\nRunes are powerful symbols carved into stone and imbued with magic." +
					"\nConsume them to draw their power, or keep them to craft more powerful runes..." +
					"\n\nYou can consume the power held in a rune the same way you channel the power of one of the Forsaken, with the rune in hand." +
					"\n\nA simple rune only grants you a small physical damage increase." +
					"\n\nElemental runes attune you with their element, infusing your attacks and granting you further resistances." +
					"\nBut be wary, this power comes at a price, as you will become weaker and more vulnerable to other elements.",
					m_topic = "Odin Runes"
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
