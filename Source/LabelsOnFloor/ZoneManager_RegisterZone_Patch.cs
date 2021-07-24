using System;
using HarmonyLib;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x0200001F RID: 31
	[HarmonyPatch(typeof(ZoneManager), "RegisterZone")]
	public class ZoneManager_RegisterZone_Patch
	{
		// Token: 0x06000070 RID: 112 RVA: 0x0000378D File Offset: 0x0000198D
		private static void Postfix(ref Zone newZone)
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
			labelPlacementHandler.SetDirtyIfAreaIsOnMap(newZone.Map);
		}
	}
}
