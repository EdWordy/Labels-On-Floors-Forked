using System;
using HarmonyLib;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x02000019 RID: 25
	[HarmonyPatch(typeof(ListerBuildings), "Remove")]
	public class ListerBuildings_Remove_Patch
	{
		// Token: 0x06000064 RID: 100 RVA: 0x0000372C File Offset: 0x0000192C
		private static void Postfix(ref Building b)
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
			labelPlacementHandler.SetDirtyIfAreaIsOnMap(b.Map);
		}
	}
}
