using System;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x02000002 RID: 2
	public class CustomRoomData : IExposable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public CustomRoomData(Room roomObject, Map map, string label, IntVec3 keyCell)
		{
			this.RoomObject = roomObject;
			this._map = map;
			this.Label = label;
			this._keyCell = keyCell;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002075 File Offset: 0x00000275
		public CustomRoomData()
		{
		}

		// Token: 0x06000003 RID: 3 RVA: 0x0000207D File Offset: 0x0000027D
		public bool IsRoomStillValid()
		{
			return this.RoomObject != null && this._map != null && GridsUtility.GetRoom(this._keyCell, this._map) == this.RoomObject;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020AB File Offset: 0x000002AB
		public void AllocateRoomObjectIfNeeded()
		{
			if (this.RoomObject != null || this._map == null)
			{
				return;
			}
			this.RoomObject = GridsUtility.GetRoom(this._keyCell, this._map);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020D8 File Offset: 0x000002D8
		public void ExposeData()
		{
			Scribe_Values.Look<string>(ref this.Label, "label", "", false);
			Scribe_References.Look<Map>(ref this._map, "map", false);
			Scribe_Values.Look<IntVec3>(ref this._keyCell, "keyCell", default(IntVec3), false);
		}

		// Token: 0x04000001 RID: 1
		public string Label;

		// Token: 0x04000002 RID: 2
		public Room RoomObject;

		// Token: 0x04000003 RID: 3
		private Map _map;

		// Token: 0x04000004 RID: 4
		private IntVec3 _keyCell;
	}
}
