using System;
using System.Collections.Generic;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x0200000A RID: 10
	internal class BestEdges
	{
		// Token: 0x0600001A RID: 26 RVA: 0x0000248F File Offset: 0x0000068F
		public BestEdges()
		{
			this.Row = new List<IntVec3>();
			this.Column = new List<IntVec3>();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024AD File Offset: 0x000006AD
		public bool IsValid()
		{
			return this.Row.Count > 0 && this.Column.Count > 0;
		}

		// Token: 0x04000008 RID: 8
		public readonly List<IntVec3> Row;

		// Token: 0x04000009 RID: 9
		public readonly List<IntVec3> Column;
	}
}
