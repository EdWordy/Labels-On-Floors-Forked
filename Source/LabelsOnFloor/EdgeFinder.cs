using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x0200000B RID: 11
	public class EdgeFinder
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000024D0 File Offset: 0x000006D0
		public static PlacementData GetBestPlacementData(IList<IntVec3> allCells, Func<IntVec3, bool> shouldBailout, Func<IntVec3, bool> isValidCell, Func<IntVec3, bool> isVisibleCell, int labelLength)
		{
			if (labelLength == 0)
			{
				return null;
			}
			BestEdges edgeCells = EdgeFinder.GetEdgeCells(allCells, shouldBailout, isValidCell);
			if (edgeCells == null)
			{
				return null;
			}
			PlacementData placementData = new PlacementData();
			int num = edgeCells.Row.Count(isVisibleCell);
			int num2 = edgeCells.Column.Count(isVisibleCell);
			if (num < num2)
			{
				placementData.Position = EdgeFinder.GetFirstCellInColumn(edgeCells.Column);
				placementData.Scale = EdgeFinder.GetScalingVector(edgeCells.Column.Count, labelLength);
				placementData.Flipped = true;
			}
			else
			{
				placementData.Position = EdgeFinder.GetFirstCellInRow(edgeCells.Row);
				placementData.Scale = EdgeFinder.GetScalingVector(edgeCells.Row.Count, labelLength);
			}
			if (placementData.Scale.x < Main.Instance.GetMinFontScale())
			{
				return null;
			}
			return placementData;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002588 File Offset: 0x00000788
		private static Vector3 GetScalingVector(int cellCount, int labelLength)
		{
			float num = ((float)cellCount - 0.4f) / (float)labelLength;
			if (num > Main.Instance.GetMaxFontScale())
			{
				num = Main.Instance.GetMaxFontScale();
			}
			return new Vector3(num, 1f, num);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000025C8 File Offset: 0x000007C8
		private static IntVec3 GetFirstCellInRow(IList<IntVec3> cells)
		{
			IntVec3 intVec = cells.First<IntVec3>();
			foreach (IntVec3 intVec2 in cells)
			{
				if (intVec2.x < intVec.x)
				{
					intVec = intVec2;
				}
			}
			return intVec;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002624 File Offset: 0x00000824
		private static IntVec3 GetFirstCellInColumn(IList<IntVec3> cells)
		{
			IntVec3 intVec = cells.First<IntVec3>();
			foreach (IntVec3 intVec2 in cells)
			{
				if (intVec2.z > intVec.z)
				{
					intVec = intVec2;
				}
			}
			return intVec;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002680 File Offset: 0x00000880
		private static BestEdges GetEdgeCells(IEnumerable<IntVec3> allCells, Func<IntVec3, bool> shouldBailout, Func<IntVec3, bool> isValidCell)
		{
			BestEdges bestEdges = new BestEdges();
			int num = int.MaxValue;
			int num2 = int.MaxValue;
			foreach (IntVec3 intVec in allCells)
			{
				if (shouldBailout(intVec))
				{
					return null;
				}
				if (isValidCell(intVec))
				{
					if (intVec.z < num)
					{
						num = intVec.z;
						bestEdges.Row.Clear();
					}
					if (intVec.z == num)
					{
						bestEdges.Row.Add(intVec);
					}
					if (intVec.x < num2)
					{
						num2 = intVec.x;
						bestEdges.Column.Clear();
					}
					if (intVec.x == num2)
					{
						bestEdges.Column.Add(intVec);
					}
				}
			}
			if (!bestEdges.IsValid())
			{
				return null;
			}
			return bestEdges;
		}
	}
}
