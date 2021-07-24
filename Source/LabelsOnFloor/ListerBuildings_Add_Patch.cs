using System;
using HarmonyLib;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x02000018 RID: 24
	[HarmonyPatch(typeof(ListerBuildings), "Add")]
	public class ListerBuildings_Add_Patch
	{
		// Token: 0x06000062 RID: 98 RVA: 0x0000372C File Offset: 0x0000192C
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
