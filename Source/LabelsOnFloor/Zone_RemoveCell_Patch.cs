using System;
using HarmonyLib;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x02000021 RID: 33
	[HarmonyPatch(typeof(Zone), "RemoveCell")]
	public class Zone_RemoveCell_Patch
	{
		// Token: 0x06000074 RID: 116 RVA: 0x000037AF File Offset: 0x000019AF
		private static void Postfix(ref Zone __instance)
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
			labelPlacementHandler.AddOrUpdateZone(__instance);
		}
	}
}
