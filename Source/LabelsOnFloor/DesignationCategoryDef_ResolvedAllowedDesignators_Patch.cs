using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x02000006 RID: 6
	[HarmonyPatch(typeof(DesignationCategoryDef))]
	[HarmonyPatch(("ResolveDesignators" ))]
	public class DesignationCategoryDef_ResolvedAllowedDesignators_Patch
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000023C0 File Offset: 0x000005C0
		private static void Postfix(ref IEnumerable<Designator> ___resolvedDesignators, ref DesignationCategoryDef __instance)
		{
			if (!Main.Instance.IsModActive())
			{
				return;
			}
			if (DesignationCategoryDef_ResolvedAllowedDesignators_Patch.ordersCategoryDef == null && "Orders" == __instance.defName)
			{
				DesignationCategoryDef_ResolvedAllowedDesignators_Patch.ordersCategoryDef = __instance;
			}
			if (__instance != DesignationCategoryDef_ResolvedAllowedDesignators_Patch.ordersCategoryDef || ___resolvedDesignators == null)
			{
				return;
			}
			Designator_Rename item = new Designator_Rename();
			List<Designator> list = ___resolvedDesignators.ToList<Designator>();
			list.Add(item);
			___resolvedDesignators = list;
		}

		// Token: 0x04000006 RID: 6
		private static DesignationCategoryDef ordersCategoryDef;
	}
}
