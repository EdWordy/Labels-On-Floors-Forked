using System;
using System.Collections.Generic;
using System.Linq;
using HugsLib.Utils;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x02000003 RID: 3
	public class CustomRoomLabelManager : UtilityWorldObject
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002128 File Offset: 0x00000328
		public bool IsRoomCustomised(Room room)
		{
			return this._roomLabels.Any((CustomRoomData rl) => rl.RoomObject == room);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000215C File Offset: 0x0000035C
		public string GetCustomLabelFor(Room room)
		{
			CustomRoomData customRoomData = this._roomLabels.FirstOrDefault((CustomRoomData rl) => rl.RoomObject == room);
			string text;
			if (customRoomData == null)
			{
				text = null;
			}
			else
			{
				string label = customRoomData.Label;
				text = ((label != null) ? label.ToUpper() : null);
			}
			return text ?? string.Empty;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021B0 File Offset: 0x000003B0
		public CustomRoomData GetOrCreateCustomRoomDataFor(Room room, IntVec3 loc)
		{
			CustomRoomData customRoomData = this._roomLabels.FirstOrDefault((CustomRoomData rl) => rl.RoomObject == room);
			if (customRoomData != null)
			{
				return customRoomData;
			}
			customRoomData = new CustomRoomData(room, Find.CurrentMap, "", loc);
			this._roomLabels.Add(customRoomData);
			return this._roomLabels.FirstOrDefault((CustomRoomData rl) => rl.RoomObject == room);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002224 File Offset: 0x00000424
		public void CleanupMissingRooms()
		{
			this._roomLabels.ForEach(delegate(CustomRoomData d)
			{
				d.AllocateRoomObjectIfNeeded();
			});
			this._roomLabels.RemoveAll((CustomRoomData data) => !data.IsRoomStillValid() || string.IsNullOrEmpty(data.Label));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002286 File Offset: 0x00000486
		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Collections.Look<CustomRoomData>(ref this._roomLabels, "roomLabels", LookMode.Deep, Array.Empty<object>());
		}

		// Token: 0x04000005 RID: 5
		private List<CustomRoomData> _roomLabels = new List<CustomRoomData>();
	}
}
