using System;
using HarmonyLib;
using RimWorld;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x02000005 RID: 5
	[HarmonyPatch(typeof(Building_Bed), "FacilityChanged")]
	public class Building_Bed_Medical_Patch
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000238C File Offset: 0x0000058C
		public static void Postfix(ref Building_Bed __instance)
		{
			Room room = __instance.GetRoom(RegionType.Normal | RegionType.Portal);
			if (room != null)
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
				labelPlacementHandler.AddOrUpdateRoom(room);
			}
		}
	}
}
