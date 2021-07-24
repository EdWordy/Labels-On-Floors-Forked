using System;
using RimWorld;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x02000012 RID: 18
	public class LabelMaker
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002C12 File Offset: 0x00000E12
		public LabelMaker(CustomRoomLabelManager customRoomLabelManager)
		{
			this._customRoomLabelManager = customRoomLabelManager;
			this._defaultGrowingZonePrefix = "GrowingZone".Translate();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002C36 File Offset: 0x00000E36
		public string GetRoomLabel(Room room)
		{
			if (room == null)
			{
				return string.Empty;
			}
			if (!this._customRoomLabelManager.IsRoomCustomised(room))
			{
				return room.Role.label.ToUpper();
			}
			return this._customRoomLabelManager.GetCustomLabelFor(room);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002C6C File Offset: 0x00000E6C
		public string GetZoneLabel(Zone zone)
		{
			if (zone == null)
			{
				return string.Empty;
			}
			Zone_Growing zone_Growing = zone as Zone_Growing;
			if (zone_Growing == null)
			{
				string label = zone.label;
				return ((label != null) ? label.ToUpper() : null) ?? string.Empty;
			}
			string label2 = zone_Growing.label;
			if (label2 != null && label2.StartsWith(this._defaultGrowingZonePrefix))
			{
				ThingDef plantDefToGrow = zone_Growing.GetPlantDefToGrow();
				string text;
				if (plantDefToGrow == null)
				{
					text = null;
				}
				else
				{
					string label3 = plantDefToGrow.label;
					text = ((label3 != null) ? label3.ToUpper() : null);
				}
				return text ?? string.Empty;
			}
			string label4 = zone_Growing.label;
			return ((label4 != null) ? label4.ToUpper() : null) ?? string.Empty;
		}

		// Token: 0x04000019 RID: 25
		private readonly string _defaultGrowingZonePrefix;

		// Token: 0x0400001A RID: 26
		private readonly CustomRoomLabelManager _customRoomLabelManager;
	}
}
