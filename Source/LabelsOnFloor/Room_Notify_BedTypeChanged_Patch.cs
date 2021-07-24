using System;
using HarmonyLib;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x0200001B RID: 27
	[HarmonyPatch(typeof(Room), "Notify_BedTypeChanged")]
	public class Room_Notify_BedTypeChanged_Patch
	{
		// Token: 0x06000068 RID: 104 RVA: 0x0000374E File Offset: 0x0000194E
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
			labelPlacementHandler.AddOrUpdateRoom(__instance);
		}
	}
}
