using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x0200000D RID: 13
	public class FontHandler
	{
		// Token: 0x06000022 RID: 34 RVA: 0x0000276C File Offset: 0x0000096C
		public bool IsFontLoaded()
		{
			if (Resources.Font == null)
			{
				return false;
			}
			if (this._charWidthAsTexturePortion < 0f)
			{
				this._charWidthAsTexturePortion = 35f / (float)Resources.Font.width;
			}
			return true;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000027A4 File Offset: 0x000009A4
		public Material GetMaterial()
		{
			if (this._material == null)
			{
				Color color = Main.Instance.UseLightText() ? Color.white : Color.black;
				color.a = Main.Instance.GetOpacity();
				this._material = MaterialPool.MatFrom(Resources.Font, ShaderDatabase.Transparent, color);
			}
			return this._material;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002805 File Offset: 0x00000A05
		public void Reset()
		{
			this._material = null;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000280E File Offset: 0x00000A0E
		public IEnumerable<CharBoundsInTexture> GetBoundsInTextureFor(string text)
		{
			foreach (char c in text)
			{
				yield return this.GetCharBoundsInTextureFor(c);
			}
			string text2 = null;
			yield break;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002828 File Offset: 0x00000A28
		private CharBoundsInTexture GetCharBoundsInTextureFor(char c)
		{
			float num = (float)this.GetIndexInFontForChar(c) * this._charWidthAsTexturePortion;
			return new CharBoundsInTexture
			{
				Left = num,
				Right = num + this._charWidthAsTexturePortion
			};
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002868 File Offset: 0x00000A68
		private int GetIndexInFontForChar(char c)
		{
			if (c < '!')
			{
				return 0;
			}
			if (c < 'a')
			{
				return (int)(c - ' ');
			}
			if (c < '\u007f')
			{
				return (int)(c - ':');
			}
			return 0;
		}

		// Token: 0x0400000C RID: 12
		private float _charWidthAsTexturePortion = -1f;

		// Token: 0x0400000D RID: 13
		private Material _material;
	}
}
