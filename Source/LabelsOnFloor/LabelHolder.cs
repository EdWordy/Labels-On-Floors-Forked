using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x02000011 RID: 17
	public class LabelHolder
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002A0A File Offset: 0x00000C0A
		public void Clear()
		{
			this._currentLabels.Clear();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002A17 File Offset: 0x00000C17
		public void Add(Label label)
		{
			this._currentLabels.Add(label);
			this._dirty = true;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002A2C File Offset: 0x00000C2C
		public void RemoveLabelForArea(object area)
		{
			this._currentLabels.RemoveAll((Label l) => l.AssociatedArea == area);
			this._dirty = true;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002A65 File Offset: 0x00000C65
		public IEnumerable<Label> GetLabels()
		{
			this.RemoveRoomsWithZones();
			return this._currentLabels;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A74 File Offset: 0x00000C74
		private void RemoveRoomsWithZones()
		{
			if (!this._dirty)
			{
				return;
			}
			this._dirty = false;
			if (!Main.Instance.ShowRoomNames() || !Main.Instance.ShowZoneNames())
			{
				return;
			}
			Map currentMap = Find.CurrentMap;
			HashSet<Room> hashSet = new HashSet<Room>();
			HashSet<Room> hashSet2 = new HashSet<Room>(from l in this._currentLabels
			where !l.IsZone
			select l.AssociatedArea as Room);
			foreach (Label label in this._currentLabels)
			{
				if (label.IsZone)
				{
					Zone zone = label.AssociatedArea as Zone;
					if (zone.Cells.Count >= 1)
					{
						Room roomAtLocation = RoomRoleFinder.GetRoomAtLocation(zone.Cells.First<IntVec3>(), currentMap);
						if (roomAtLocation != null && hashSet2.Contains(roomAtLocation))
						{
							hashSet.Add(roomAtLocation);
						}
					}
				}
			}
			using (HashSet<Room>.Enumerator enumerator2 = hashSet.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					Room room = enumerator2.Current;
					this._currentLabels.RemoveAll((Label l) => l.AssociatedArea == room);
				}
			}
		}

		// Token: 0x04000017 RID: 23
		private readonly List<Label> _currentLabels = new List<Label>();

		// Token: 0x04000018 RID: 24
		private bool _dirty = true;
	}
}
