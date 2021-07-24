using System;
using HarmonyLib;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x0200001C RID: 28
	[HarmonyPatch(typeof(Room), "Notify_RoomShapeChanged")]
	public class Room_Notify_RoomShapeOrContainedBedsChanged_Patch
	{
		// Token: 0x0600006A RID: 106 RVA: 0x0000376B File Offset: 0x0000196B
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
