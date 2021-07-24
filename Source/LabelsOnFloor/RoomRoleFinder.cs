using System;
using RimWorld;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x02000026 RID: 38
	public class RoomRoleFinder
	{
		// Token: 0x06000080 RID: 128 RVA: 0x0000398B File Offset: 0x00001B8B
		public RoomRoleFinder(CustomRoomLabelManager customRoomLabelManager)
		{
			this._customRoomLabelManager = customRoomLabelManager;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000399C File Offset: 0x00001B9C
		public bool IsImportantRoom(Room room)
		{
			if (room.Role == RoomRoleDefOf.None)
			{
				return false;
			}
			if (this._customRoomLabelManager.IsRoomCustomised(room))
			{
				return true;
			}
			if (this._emptyRooomRole != null)
			{
				return room.Role != this._emptyRooomRole;
			}
			if (room.Role.defName == "Room")
			{
				this._emptyRooomRole = room.Role;
				return false;
			}
			return true;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003A08 File Offset: 0x00001C08
		public static Room GetRoomAtLocation(IntVec3 loc, Map map)
		{
			if (map == null)
			{
				return null;
			}
			Room room = GridsUtility.GetRoom(loc, map);
			if (room == null)
			{
				return null;
			}
			if (room.Role != RoomRoleDefOf.None)
			{
				return room;
			}
			return null;
		}

		// Token: 0x04000035 RID: 53
		private RoomRoleDef _emptyRooomRole;

		// Token: 0x04000036 RID: 54
		private readonly CustomRoomLabelManager _customRoomLabelManager;
	}
}
