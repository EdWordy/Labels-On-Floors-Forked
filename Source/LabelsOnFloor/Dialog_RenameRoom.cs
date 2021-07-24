using System;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x02000007 RID: 7
	public class Dialog_RenameRoom : Dialog_Rename
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002421 File Offset: 0x00000621
		public Dialog_RenameRoom(CustomRoomData customRoomData)
		{
			this._customRoomData = customRoomData;
			this.curName = customRoomData.Label;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000243C File Offset: 0x0000063C
		protected override void SetName(string name)
		{
			this._customRoomData.Label = name;
			Main.Instance.LabelPlacementHandler.SetDirty();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002459 File Offset: 0x00000659
		protected override AcceptanceReport NameIsValid(string name)
		{
			return true;
		}

		// Token: 0x04000007 RID: 7
		private readonly CustomRoomData _customRoomData;
	}
}
