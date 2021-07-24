using System;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x02000023 RID: 35
	public class PlacementDataFinderForZones
	{
		// Token: 0x06000078 RID: 120 RVA: 0x000037CC File Offset: 0x000019CC
		public static PlacementData GetData(Zone zone, Map map, int labelLength)
		{
			return EdgeFinder.GetBestPlacementData(zone.Cells, (IntVec3 c) => c.Fogged(map), (IntVec3 c) => true, (IntVec3 c) => true, labelLength);
		}
	}
}
