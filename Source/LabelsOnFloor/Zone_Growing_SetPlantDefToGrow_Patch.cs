using System;
using HarmonyLib;
using RimWorld;

namespace LabelsOnFloor
{
	// Token: 0x02000020 RID: 32
	[HarmonyPatch(typeof(Zone_Growing), "SetPlantDefToGrow")]
	public class Zone_Growing_SetPlantDefToGrow_Patch
	{
		// Token: 0x06000072 RID: 114 RVA: 0x000037AF File Offset: 0x000019AF
		private static void Postfix(ref Zone_Growing __instance)
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
