using System;
using HarmonyLib;
using RimWorld;

namespace LabelsOnFloor
{
	// Token: 0x02000017 RID: 23
	[HarmonyPatch(typeof(GatherSpotLister), "RegisterDeactivated")]
	public class GatherSpotLister_RegisterDeactivated_Patch
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00002461 File Offset: 0x00000661
		private static void Postfix(ref CompGatherSpot spot)
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
			labelPlacementHandler.SetDirty();
		}
	}
}
