using System;
using System.Collections.Generic;
using UnityEngine;

namespace LabelsOnFloor
{
	// Token: 0x02000015 RID: 21
	public class MeshHandler
	{
		// Token: 0x0600005B RID: 91 RVA: 0x000034C5 File Offset: 0x000016C5
		public MeshHandler(FontHandler fontHandler)
		{
			this._fontHandler = fontHandler;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000034E0 File Offset: 0x000016E0
		public Mesh GetMeshFor(string label)
		{
			if (string.IsNullOrEmpty(label))
			{
				return null;
			}
			if (!this._fontHandler.IsFontLoaded())
			{
				return null;
			}
			if (!this._cachedMeshes.ContainsKey(label))
			{
				this._cachedMeshes[label] = this.CreateMeshFor(label);
			}
			return this._cachedMeshes[label];
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003534 File Offset: 0x00001734
		private Mesh CreateMeshFor(string label)
		{
			List<Vector3> list = new List<Vector3>();
			List<Vector2> list2 = new List<Vector2>();
			List<int> list3 = new List<int>();
			Vector2 vector = default(Vector2);
			vector.x = 1f;
			vector.y = 2f;
			Vector2 vector2 = vector;
			IEnumerable<CharBoundsInTexture> boundsInTextureFor = this._fontHandler.GetBoundsInTextureFor(label);
			int num = 0;
			float num2 = 0f;
			float num3 = vector2.y - 0.4f;
			foreach (CharBoundsInTexture charBoundsInTexture in boundsInTextureFor)
			{
				list.Add(new Vector3(num2, 0f, -0.4f));
				list.Add(new Vector3(num2, 0f, num3));
				list.Add(new Vector3(num2 + vector2.x, 0f, num3));
				list.Add(new Vector3(num2 + vector2.x, 0f, -0.4f));
				num2 += vector2.x;
				list2.Add(new Vector2(charBoundsInTexture.Left, 0f));
				list2.Add(new Vector2(charBoundsInTexture.Left, 1f));
				list2.Add(new Vector2(charBoundsInTexture.Right, 1f));
				list2.Add(new Vector2(charBoundsInTexture.Right, 0f));
				list3.Add(num);
				list3.Add(num + 1);
				list3.Add(num + 2);
				list3.Add(num);
				list3.Add(num + 2);
				list3.Add(num + 3);
				num += 4;
			}
			Mesh mesh = new Mesh();
			mesh.name = "NewPlaneMesh()";
			mesh.vertices = list.ToArray();
			mesh.uv = list2.ToArray();
			mesh.SetTriangles(list3, 0);
			mesh.RecalculateNormals();
			mesh.RecalculateBounds();
			return mesh;
		}

		// Token: 0x0400002F RID: 47
		private readonly Dictionary<string, Mesh> _cachedMeshes = new Dictionary<string, Mesh>();

		// Token: 0x04000030 RID: 48
		private readonly FontHandler _fontHandler;
	}
}
