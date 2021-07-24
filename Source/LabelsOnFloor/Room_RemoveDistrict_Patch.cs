using System;
using HarmonyLib;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x0200001D RID: 29
	[HarmonyPatch(typeof(Room), "RemoveDistrict")]
	public class Room_RemoveDistrict_Patch
	{
		// Token: 0x0600006C RID: 108 RVA: 0x0000376B File Offset: 0x0000196B
		private static void Postfix(ref Room __instance)
		{
			Main instance = Main.Instance;
			if (instance == null)
			{
				return;
			}
			LabelPlacementHandler labelPlacementHandler = instance.LabelPlacementHandler;
			if (labelPlacementHandler == null)
			{
				return;
			}
			labelPlacementHandler.SetDirtyIfAreaIsOnMap(__instance.Map);
		}
	}
}
