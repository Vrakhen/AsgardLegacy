using HarmonyLib;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AsgardLegacy.Runes
{
    class RuneTable_Patch
	{
		[HarmonyPatch(typeof(Player), nameof(Player.PlacePiece), null)]
		public class AsgardLegacy_PlacePiece_Patch
		{
			public static void Postfix(Piece piece, bool __result, GameObject ___m_placementGhost)
			{
				if (!__result)
					return;

				if (piece.m_name != "Rune Table"
					&& piece.m_name != "Elder Shrine"
					&& piece.m_name != "Bone Shrine"
					&& piece.m_name != "Bone Shrine"
					&& piece.m_name != "Bone Shrine")
					return;

				foreach(var p in Player.GetAllPlayers())
					p.ShowTutorial("al_Runes", false);

				Object.Instantiate(ZNetScene.instance.GetPrefab("sfx_build_hammer_default"), ___m_placementGhost.transform.position, Quaternion.identity);
				Object.Instantiate(ZNetScene.instance.GetPrefab("vfx_Place_workbench"), ___m_placementGhost.transform.position, Quaternion.identity);
			}
		}

		[HarmonyPatch(typeof(Piece), nameof(Piece.DropResources), null)]
		public class AsgardLegacy_DropResources_Patch
		{
			public static void Postfix(Piece __instance)
			{
				if (__instance.m_name != "Rune Table"
					&& __instance.m_name != "Elder Shrine"
					&& __instance.m_name != "Bone Shrine")
					return;

				Object.Instantiate(ZNetScene.instance.GetPrefab("vfx_SawDust"), __instance.transform.position, Quaternion.identity);
			}
		}
	}
}
