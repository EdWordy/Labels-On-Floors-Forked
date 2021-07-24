using System;
using UnityEngine;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x02000024 RID: 36
	[StaticConstructorOnStartup]
	public class Resources
	{
		// Token: 0x04000031 RID: 49
		public static Texture2D Font = ContentFinder<Texture2D>.Get("Consolas", true);

		// Token: 0x04000032 RID: 50
		public static Texture2D Rename = ContentFinder<Texture2D>.Get("Rename", true);
	}
}
