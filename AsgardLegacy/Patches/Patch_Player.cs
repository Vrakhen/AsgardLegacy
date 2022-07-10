using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace AsgardLegacy
{
    class Patch_Player
	{
		[HarmonyPatch(typeof(Player), nameof(Player.OnDodgeMortal), null)]
		public class DodgeBreaksChanneling_Patch
		{
			public static void Postfix()
			{
				if (AsgardLegacy.isChanneling)
					AsgardLegacy.isChanneling = false;
			}
		}

		[HarmonyPatch(typeof(Player), nameof(Player.OnJump), null)]
		public class JumpBreaksChanneling_Patch
		{
			public static void Postfix()
			{
				if (AsgardLegacy.isChanneling)
					AsgardLegacy.isChanneling = false;
			}
		}

		[HarmonyPatch(typeof(Player), nameof(Player.ActivateGuardianPower), null)]
		public class PreventGuardianPower_Patch
		{
			public static bool Prefix(ref bool __result)
			{
				bool result;
				if (!AsgardLegacy.shouldUseForsakenPower)
				{
					__result = false;
					result = false;
				}
				else
					result = true;

				return result;
			}
		}

		[HarmonyPatch(typeof(Player), nameof(Player.StartGuardianPower), null)]
		public class StartPowerPrevention_Patch
		{
			public static bool Prefix(ref bool __result)
			{
				bool flag = !AsgardLegacy.shouldUseForsakenPower;
				bool result;
				if (flag)
				{
					__result = false;
					result = false;
				}
				else
				{
					result = true;
				}
				return result;
			}
		}

		[HarmonyPatch(typeof(Player), nameof(Player.CanMove), null)]
		public class Channeling_CancelMove_Patch
		{
			public static void Postfix(ref bool __result)
			{
				if (AsgardLegacy.isChanneling)
					__result = false;
			}
		}

		[HarmonyPatch(typeof(Player), nameof(Player.TakeInput), null)]
		public class Channeling_CancelInput_Patch
		{
			public static void Postfix(ref bool __result)
			{
				if (AsgardLegacy.isChanneling)
					__result = false;
			}
		}

		[HarmonyPatch(typeof(Player), nameof(Player.Update), null)]
		public class AbilityInput_Patch
		{
			public static bool Prefix(Player __instance)
			{
				if (__instance != Player.m_localPlayer)
					return true;

				if (ZInput.GetButtonDown("GP") || ZInput.GetButtonDown("JoyGPower"))
				{
					if(__instance.m_leftItem != null 
						&& __instance.m_leftItem.m_shared.m_name.ToLower().Contains("rune"))
					{
						AsgardLegacy.shouldUseForsakenPower = false;

						Object.Instantiate(ZNetScene.instance.GetPrefab("fx_GP_Activation"), __instance.GetCenterPoint(), Quaternion.identity);
						((ZSyncAnimation) typeof(Player).GetField("m_zanim", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Player.m_localPlayer)).SetTrigger("gpower");

						RunesController.ApplyRuneEffect(__instance, __instance.m_leftItem.m_shared.m_name.ToLower());

						var item = __instance.m_leftItem;
						item.m_durability -= 25f;
						//__instance.UnequipItem(item);
						//__instance.GetInventory().RemoveOneItem(item);

						return false;
                    }

					AsgardLegacy.shouldUseForsakenPower = true;
				}

				return true;
			}

			public static void Postfix(Player __instance)
			{
				if (Utility.ReadyTime)
				{

					if (__instance != Player.m_localPlayer)
						return;

					if (__instance != null && AsgardLegacy.playerEnabled)
					{
						if (Utility.TakeInput(__instance) && !__instance.InPlaceMode())
						{
							switch (AsgardLegacy.al_player.al_class)
							{
								case AsgardLegacy.PlayerClass.Guardian:
									Guardian.ProcessInput(__instance);
									break;
								case AsgardLegacy.PlayerClass.Berserker:
									Berserker.ProcessInput(__instance);
									break;
								case AsgardLegacy.PlayerClass.Ranger:
									Ranger.ProcessInput(__instance);
									break;
								case AsgardLegacy.PlayerClass.Sentinel:
									Sentinel.ProcessInput(__instance);
									break;
							}
						}
					}
				}
			}
		}
		
		[HarmonyPatch(typeof(Character), nameof(Character.OnDeath), null)]
		public class Player_OnDeath_Patch
		{
			public static void Prefix(Character __instance)
			{
				if (!__instance.IsPlayer())
					return;

				var player = __instance as Player;
				if (player != Player.m_localPlayer)
					return;

				AsgardLegacy.isChanneling = false;
				AsgardLegacy.shouldUseForsakenPower = false;

				Guardian.inFlight = false;
				Guardian.shouldGuardianImpact = false;
				Guardian.retributionDamageBlocked = 0f;
			}
		}

	}
}
