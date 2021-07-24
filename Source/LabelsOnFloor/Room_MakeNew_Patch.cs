using System;
using HarmonyLib;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x0200001A RID: 26
	[HarmonyPatch(typeof(Room), "MakeNew")]
	public class Room_MakeNew_Patch
	{
		// Token: 0x06000066 RID: 102 RVA: 0x0000374E File Offset: 0x0000194E
		private static void Postfix(ref Room __result)
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
			labelPlacementHandler.AddOrUpdateRoom(__result);
		}
	}
}
