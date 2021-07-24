using System;
using HarmonyLib;
using RimWorld;

namespace LabelsOnFloor
{
	// Token: 0x02000016 RID: 22
	[HarmonyPatch(typeof(GatherSpotLister), "RegisterActivated")]
	public class GatherSpotLister_RegisterActivated_Patch
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00002461 File Offset: 0x00000661
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
