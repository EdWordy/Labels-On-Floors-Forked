using System;
using HarmonyLib;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x02000009 RID: 9
	[HarmonyPatch(typeof(DynamicDrawManager), "DrawDynamicThings")]
	public class DynamicDrawManager_DrawDynamicThings_Patch
	{
		// Token: 0x06000018 RID: 24 RVA: 0x0000247C File Offset: 0x0000067C
		private static bool Prefix(ref DynamicDrawManager __instance)
		{
			Main instance = Main.Instance;
			if (instance != null)
			{
				instance.Draw();
			}
			return true;
		}
	}
}
