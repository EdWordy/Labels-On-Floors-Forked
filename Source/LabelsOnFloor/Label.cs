using System;
using UnityEngine;

namespace LabelsOnFloor
{
	// Token: 0x02000010 RID: 16
	public class Label
	{
		// Token: 0x0600002D RID: 45 RVA: 0x000029E7 File Offset: 0x00000BE7
		public bool IsValid()
		{
			return this.LabelPlacementData != null && this.LabelMesh != null && this.AssociatedArea != null;
		}

		// Token: 0x04000013 RID: 19
		public Mesh LabelMesh;

		// Token: 0x04000014 RID: 20
		public PlacementData LabelPlacementData;

		// Token: 0x04000015 RID: 21
		public object AssociatedArea;

		// Token: 0x04000016 RID: 22
		public bool IsZone;
	}
}
