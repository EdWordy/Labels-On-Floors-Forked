using System;
using HarmonyLib;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x0200001E RID: 30
	[HarmonyPatch(typeof(ZoneManager), "DeregisterZone")]
	public class ZoneManager_DeregisterZone_Patch
	{
		// Token: 0x0600006E RID: 110 RVA: 0x0000378D File Offset: 0x0000198D
		private static void Postfix(ref Zone oldZone)
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
			labelPlacementHandler.SetDirtyIfAreaIsOnMap(oldZone.Map);
		}
	}
}
