using System;
using HugsLib;
using HugsLib.Settings;
using HugsLib.Utils;
using RimWorld.Planet;
using Verse;

namespace LabelsOnFloor
{
	// Token: 0x02000014 RID: 20
	public class Main : ModBase
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003048 File Offset: 0x00001248
		internal new ModLogger Logger
		{
			get
			{
				return base.Logger;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00003050 File Offset: 0x00001250
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00003057 File Offset: 0x00001257
		internal static Main Instance { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000045 RID: 69 RVA: 0x0000305F File Offset: 0x0000125F
		public override string ModIdentifier
		{
			get
			{
				return "LabelsOnFloor";
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003066 File Offset: 0x00001266
		public Main()
		{
			Main.Instance = this;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000308A File Offset: 0x0000128A
		public Dialog_RenameRoom GetRoomRenamer(Room room, IntVec3 loc)
		{
			return new Dialog_RenameRoom(this._customRoomLabelManager.GetOrCreateCustomRoomDataFor(room, loc));
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000030A0 File Offset: 0x000012A0
		public void Draw()
		{
			if (!this.IsModActive())
			{
				this.LabelPlacementHandler.SetDirty();
				return;
			}
			if (Find.CameraDriver.CurrentZoom > this._maxAllowedZoom)
			{
				return;
			}
			this.LabelPlacementHandler.RegenerateIfNeeded(this._customRoomLabelManager);
			this._labelDrawer.Draw();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000030F5 File Offset: 0x000012F5
		public bool IsModActive()
		{
			return this._enabled && Current.ProgramState == ProgramState.Playing && Find.CurrentMap != null && !WorldRendererUtility.WorldRenderedNow;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000311D File Offset: 0x0000131D
		public bool UseLightText()
		{
			return this._useLightText;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000312A File Offset: 0x0000132A
		public float GetOpacity()
		{
			return (float)this._opacity / 100f;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000313E File Offset: 0x0000133E
		public bool ShowRoomNames()
		{
			return this._showRoomLabels;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000314B File Offset: 0x0000134B
		public bool ShowZoneNames()
		{
			return this._showZoneLabels;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003158 File Offset: 0x00001358
		public float GetMaxFontScale()
		{
			return this._maxFontScale;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003165 File Offset: 0x00001365
		public float GetMinFontScale()
		{
			return this._minFontScale;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003172 File Offset: 0x00001372
		public override void OnGUI()
		{
			if (WorldRendererUtility.WorldRenderedNow)
			{
				LabelPlacementHandler labelPlacementHandler = this.LabelPlacementHandler;
				if (labelPlacementHandler != null)
				{
					labelPlacementHandler.SetDirty();
				}
			}
			base.OnGUI();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003194 File Offset: 0x00001394
		public override void WorldLoaded()
		{
			base.WorldLoaded();
			this._customRoomLabelManager = UtilityWorldObjectManager.GetUtilityWorldObject<CustomRoomLabelManager>();
			this.LabelPlacementHandler = new LabelPlacementHandler(this._labelHolder, new MeshHandler(this._fontHandler), new LabelMaker(this._customRoomLabelManager), new RoomRoleFinder(this._customRoomLabelManager));
			this._labelDrawer = new LabelDrawer(this._labelHolder, this._fontHandler);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000031FB File Offset: 0x000013FB
		public override void MapLoaded(Map map)
		{
			base.MapLoaded(map);
			this.LabelPlacementHandler.SetDirty();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003210 File Offset: 0x00001410
		public override void DefsLoaded()
		{
			this._enabled = base.Settings.GetHandle<bool>("enabled", "FALCLF.Enabled".Translate(), "FALCLF.EnabledDesc".Translate(), true, null, null);
			this._useLightText = base.Settings.GetHandle<bool>("useLightText", "FALCLF.UseLightText".Translate(), "FALCLF.UseLightTextDesc".Translate(), false, null, null);
			this._useLightText.ValueChanged = delegate(bool val)
			{
				FontHandler fontHandler = this._fontHandler;
				if (fontHandler == null)
				{
					return;
				}
				fontHandler.Reset();
			};
			this._opacity = base.Settings.GetHandle<int>("opacity", "FALCLF.TextOpacity".Translate(), "FALCLF.TextOpacityDesc".Translate(), 30, Validators.IntRangeValidator(1, 100), null);
			this._opacity.ValueChanged = delegate(int val)
			{
				FontHandler fontHandler = this._fontHandler;
				if (fontHandler == null)
				{
					return;
				}
				fontHandler.Reset();
			};
			this._showRoomLabels = base.Settings.GetHandle<bool>("showRoomLabels", "FALCLF.ShowRoomLabels".Translate(), "FALCLF.ShowRoomLabelsDesc".Translate(), true, null, null);
			this._showZoneLabels = base.Settings.GetHandle<bool>("showZoneLabels", "FALCLF.ShowZoneLabels".Translate(), "FALCLF.ShowZoneLabelsDesc".Translate(), true, null, null);
			this._maxFontScale = base.Settings.GetHandle<float>("maxFontScale", "FALCLF.MaxFontScale".Translate(), "FALCLF.MaxFontScaleDesc".Translate(), 1f, Validators.FloatRangeValidator(0.1f, 5f), null);
			this._minFontScale = base.Settings.GetHandle<float>("minFontScale", "FALCLF.MinFontScale".Translate(), "FALCLF.MinFontScaleDesc".Translate(), 0.2f, Validators.FloatRangeValidator(0.1f, 1f), null);
			this._maxAllowedZoom = base.Settings.GetHandle<CameraZoomRange>("maxAllowedZoom", "FALCLF.MaxAllowedZoom".Translate(), "FALCLF.MaxAllowedZoomDesc".Translate(), CameraZoomRange.Furthest, null, "FALCLF.enumSetting_");
			this._enabled.ValueChanged = delegate(bool val)
			{
				this.LabelPlacementHandler.SetDirty();
			};
			this._showRoomLabels.ValueChangedd = delegate(bool val)
			{
				this.LabelPlacementHandler.SetDirty();
			};
			this._showZoneLabels.ValueChanged = delegate(bool val)
			{
				this.LabelPlacementHandler.SetDirty();
			};
			this._maxFontScale.ValueChanged = delegate(float val)
			{
				this.LabelPlacementHandler.SetDirty();
			};
			this._minFontScale.ValueChanged = delegate(float val)
			{
				this.LabelPlacementHandler.SetDirty();
			};
		}

		// Token: 0x04000021 RID: 33
		public LabelPlacementHandler LabelPlacementHandler;

		// Token: 0x04000023 RID: 35
		private SettingHandle<bool> _enabled;

		// Token: 0x04000024 RID: 36
		private SettingHandle<bool> _useLightText;

		// Token: 0x04000025 RID: 37
		private SettingHandle<int> _opacity;

		// Token: 0x04000026 RID: 38
		private SettingHandle<bool> _showRoomLabels;

		// Token: 0x04000027 RID: 39
		private SettingHandle<bool> _showZoneLabels;

		// Token: 0x04000028 RID: 40
		private SettingHandle<float> _maxFontScale;

		// Token: 0x04000029 RID: 41
		private SettingHandle<float> _minFontScale;

		// Token: 0x0400002A RID: 42
		private SettingHandle<CameraZoomRange> _maxAllowedZoom;

		// Token: 0x0400002B RID: 43
		private readonly LabelHolder _labelHolder = new LabelHolder();

		// Token: 0x0400002C RID: 44
		private LabelDrawer _labelDrawer;

		// Token: 0x0400002D RID: 45
		private readonly FontHandler _fontHandler = new FontHandler();

		// Token: 0x0400002E RID: 46
		private CustomRoomLabelManager _customRoomLabelManager;
	}
}
