using System;
using HarmonyLib;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x02000022 RID: 34
	[HarmonyPatch(typeof(Zone), "AddCell")]
	public class Zone_AddCell_Patch
	{
		// Token: 0x06000076 RID: 118 RVA: 0x000037AF File Offset: 0x000019AF
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
