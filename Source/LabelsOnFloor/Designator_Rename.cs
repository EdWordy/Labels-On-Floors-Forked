using System;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x02000004 RID: 4
	public class Designator_Rename : Designator
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000022B8 File Offset: 0x000004B8
		public Designator_Rename()
		{
			this.defaultLabel = "Rename".Translate();
			this.defaultDesc = "FALCLF.RenameDesc".Translate();
			this.icon = Resources.Rename;
			this.useMouseIcon = true;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002308 File Offset: 0x00000508
		public override AcceptanceReport CanDesignateCell(IntVec3 loc)
		{
			Map currentMap = Find.CurrentMap;
			return RoomRoleFinder.GetRoomAtLocation(loc, currentMap) != null || loc.GetZone(currentMap) != null;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002338 File Offset: 0x00000538
		public override void DesignateSingleCell(IntVec3 c)
		{
			Map currentMap = Find.CurrentMap;
			Zone zone = c.GetZone(currentMap);
			if (zone != null)
			{
				Find.WindowStack.Add(new Dialog_RenameZone(zone));
				return;
			}
			Room room = GridsUtility.GetRoom(c, currentMap);
			if (room != null)
			{
				Find.WindowStack.Add(Main.Instance.GetRoomRenamer(room, c));
			}
		}
	}
}
