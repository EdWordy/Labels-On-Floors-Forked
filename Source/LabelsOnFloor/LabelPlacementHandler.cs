using System;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x02000013 RID: 19
	public class LabelPlacementHandler
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002D05 File Offset: 0x00000F05
		public LabelPlacementHandler(LabelHolder labelHolder, MeshHandler meshHandler, LabelMaker labelMaker, RoomRoleFinder roomRoleFinder)
		{
			this._labelHolder = labelHolder;
			this._meshHandler = meshHandler;
			this._labelMaker = labelMaker;
			this._roomRoleFinder = roomRoleFinder;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002D2A File Offset: 0x00000F2A
		public void SetDirty()
		{
			this._labelHolder.Clear();
			this._ready = false;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002D3E File Offset: 0x00000F3E
		public void SetDirtyIfAreaIsOnMap(Map map)
		{
			if (map == null || map == this._map)
			{
				this.SetDirty();
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002D54 File Offset: 0x00000F54
		public void RegenerateIfNeeded(CustomRoomLabelManager customRoomLabelManager)
		{
			if (this._ready && this._map == Find.CurrentMap)
			{
				return;
			}
			customRoomLabelManager.CleanupMissingRooms();
			this._map = Find.CurrentMap;
			this._labelHolder.Clear();
			this._ready = true;
			this.RegenerateRoomLabels();
			this.RegenerateZoneLabels();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002DA6 File Offset: 0x00000FA6
		public void AddOrUpdateRoom(Room room)
		{
			this.AddOrUpdateRoom(room, null);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002DB0 File Offset: 0x00000FB0
		public void AddOrUpdateRoom(Room room, PlacementDataFinderForRooms placementDataFinderForRooms)
		{
			if (!Main.Instance.ShowRoomNames())
			{
				return;
			}
			if (!this._ready || room == null)
			{
				return;
			}
			if (room.Map != this._map)
			{
				return;
			}
			if (room.Fogged || !this._roomRoleFinder.IsImportantRoom(room))
			{
				return;
			}
			string text = this._labelMaker.GetRoomLabel(room);
			if (placementDataFinderForRooms == null)
			{
				placementDataFinderForRooms = new PlacementDataFinderForRooms(this._map);
			}
			this.AddLabelForArea(room, text, () => placementDataFinderForRooms.GetData(room, text.Length));
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002E74 File Offset: 0x00001074
		public void AddOrUpdateZone(Zone zone)
		{
			if (!this._ready || zone == null)
			{
				return;
			}
			if (zone.Map != this._map)
			{
				return;
			}
			string text = this._labelMaker.GetZoneLabel(zone);
			Label label = this.AddLabelForArea(zone, text, () => PlacementDataFinderForZones.GetData(zone, this._map, text.Length));
			if (label != null)
			{
				label.IsZone = true;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002EFC File Offset: 0x000010FC
		private Label AddLabelForArea(object area, string text, Func<PlacementData> placementDataGetter)
		{
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			this._labelHolder.RemoveLabelForArea(area);
			Label label = new Label
			{
				LabelMesh = this._meshHandler.GetMeshFor(text),
				LabelPlacementData = placementDataGetter(),
				AssociatedArea = area
			};
			if (!label.IsValid())
			{
				return null;
			}
			this._labelHolder.Add(label);
			return label;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002F64 File Offset: 0x00001164
		private void RegenerateRoomLabels()
		{
			if (!Main.Instance.ShowRoomNames())
			{
				return;
			}
			PlacementDataFinderForRooms placementDataFinderForRooms = new PlacementDataFinderForRooms(this._map);
			foreach (Room room in this._map.regionGrid.allRooms)
			{
				this.AddOrUpdateRoom(room, placementDataFinderForRooms);
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002FDC File Offset: 0x000011DC
		private void RegenerateZoneLabels()
		{
			if (!Main.Instance.ShowZoneNames())
			{
				return;
			}
			foreach (Zone zone in this._map.zoneManager.AllZones)
			{
				this.AddOrUpdateZone(zone);
			}
		}

		// Token: 0x0400001B RID: 27
		private readonly LabelHolder _labelHolder;

		// Token: 0x0400001C RID: 28
		private readonly LabelMaker _labelMaker;

		// Token: 0x0400001D RID: 29
		private readonly RoomRoleFinder _roomRoleFinder;

		// Token: 0x0400001E RID: 30
		private readonly MeshHandler _meshHandler;

		// Token: 0x0400001F RID: 31
		private Map _map;

		// Token: 0x04000020 RID: 32
		private bool _ready;
	}
}
