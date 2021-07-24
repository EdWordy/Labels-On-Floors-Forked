using System;
using UnityEngine;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x0200000E RID: 14
	public class LabelDrawer
	{
		// Token: 0x06000029 RID: 41 RVA: 0x000028A6 File Offset: 0x00000AA6
		public LabelDrawer(LabelHolder labelHolder, FontHandler fontHandler)
		{
			this._labelHolder = labelHolder;
			this._fontHandler = fontHandler;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000028BC File Offset: 0x00000ABC
		public void Draw()
		{
			CellRect currentViewRect = Find.CameraDriver.CurrentViewRect;
			foreach (Label label in this._labelHolder.GetLabels())
			{
				if (currentViewRect.Contains(label.LabelPlacementData.Position))
				{
					this.DrawLabel(label);
				}
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002930 File Offset: 0x00000B30
		private void DrawLabel(Label label)
		{
			Matrix4x4 matrix4x = default(Matrix4x4);
			Vector3 vector = label.LabelPlacementData.Position.ToVector3();
			vector.x += 0.2f;
			Quaternion quaternion = Quaternion.identity;
			if (label.LabelPlacementData.Flipped)
			{
				quaternion = Quaternion.AngleAxis(90f, Vector3.up);
				vector.z += 1f;
				vector.z -= 0.2f;
			}
			else
			{
				vector.z += 0.2f;
			}
			matrix4x.SetTRS(vector, quaternion, label.LabelPlacementData.Scale);
			Graphics.DrawMesh(label.LabelMesh, matrix4x, this._fontHandler.GetMaterial(), 0);
		}

		// Token: 0x0400000E RID: 14
		private readonly LabelHolder _labelHolder;

		// Token: 0x0400000F RID: 15
		private readonly FontHandler _fontHandler;
	}
}
