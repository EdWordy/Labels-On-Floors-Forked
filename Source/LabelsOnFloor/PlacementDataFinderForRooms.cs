using System;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x02000025 RID: 37
	public class PlacementDataFinderForRooms
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00003860 File Offset: 0x00001A60
		public PlacementDataFinderForRooms(Map map)
		{
			this._map = map;
			foreach (Building t in map.listerBuildings.allBuildingsColonist)
			{
				this._blockedCells.AddRange(t.OccupiedRect().Cells);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000038E4 File Offset: 0x00001AE4
		public PlacementData GetData(Room room, int labelLength)
		{
			return EdgeFinder.GetBestPlacementData(room.Cells.ToList<IntVec3>(), (IntVec3 c) => false, (IntVec3 c) => !this._map.thingGrid.CellContains(c, ThingDefOf.Wall), new Func<IntVec3, bool>(this.IsCellVisible), labelLength);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000393C File Offset: 0x00001B3C
		private bool IsCellVisible(IntVec3 cell)
		{
			return !this._blockedCells.Any((IntVec3 c) => c == cell);
		}

		// Token: 0x04000033 RID: 51
		private readonly List<IntVec3> _blockedCells = new List<IntVec3>();

		// Token: 0x04000034 RID: 52
		private readonly Map _map;
	}
}
