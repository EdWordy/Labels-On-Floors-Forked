using System;
using HarmonyLib;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x02000008 RID: 8
	[HarmonyPatch(typeof(Dialog_RenameZone), "SetName")]
	public class Dialog_RenameZone_SetName_Patch
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002461 File Offset: 0x00000661
		private static void Postfix()
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
