# HyperXDevice
```csharp
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using NGenuity2.Audio;
using NGenuity2.Common;
using NGenuity2.Common.Commands.Devices.Events;
using NGenuity2.Common.Devices;
using NGenuity2.Common.Universals;
using NGenuity2.Devices.Universals;
using NGenuity2.Devices.Universals.Commands;
using NGenuity2.Devices.Universals.Commons;
using NGenuity2.Devices.Universals.Models;
using NGenuity2.Effects;
using NGenuity2.Effects.Headset;
using NGenuity2.Effects.Keyboard;
using NGenuity2.Effects.Microphone;
using NGenuity2.Effects.Mouse;
using NGenuity2.Effects.MousePad;
using NGenuity2.Model;
using Windows.Foundation;

namespace NGenuity2.Devices
{
	// Token: 0x0200064B RID: 1611
	public abstract class HyperXDevice
	{
		// Token: 0x06001E59 RID: 7769 RVA: 0x000524B8 File Offset: 0x000506B8
		public virtual void Serialize(BinaryWriter bw)
		{
			bw.Write((byte)this.DeviceType);
			bw.Write((int)this.Model);
			bw.Write(this.ID.ToByteArray());
			bw.Write(this.VendorID);
			bw.Write(this.ProductID);
			bw.Write(this.PairID);
			bw.Write(this.DeviceID);
			bw.Write(this.Name);
			bw.Write(this.Connected);
			bw.Write(this.IsSimulator);
			bw.Write(this.Synchronizing);
			bw.Write((int)this.DriverInstallationState);
			bw.Write(this.DriverActivated);
			bw.Write(this.IsValidDevice);
			bw.Write(this.IsOpened);
			bw.Write((int)this.ColorSkin);
			long position = bw.BaseStream.Position;
			int count = this.ExtraProperties.Count;
			bw.Write(count);
			foreach (KeyValuePair<int, long> keyValuePair in this.ExtraProperties)
			{
				bw.Write(keyValuePair.Key);
				bw.Write(keyValuePair.Value);
			}
			bw.Write(this.OnboardProfileId);
			Guid guid = Guid.Empty;
			Preset safePreset = this.GetSafePreset();
			if (safePreset != null)
			{
				guid = safePreset.ID;
			}
			bw.Write(guid.ToByteArray());
			for (int i = 0; i < this.SyncedPresets.Length; i++)
			{
				bw.Write(this.SyncedPresets[i].ToByteArray());
			}
			bw.Write(this.CurrentPresetID);
			bw.Write(this.ProfileSlots);
			bw.Write(this.LowBatteryThreshold);
			bw.Write(this.LowBatteryThresholdMin);
			bw.Write(this.LowBatteryThresholdMax);
			bw.Write((int)this.ChargingStatus);
			bw.Write(this.Battery);
			bw.Write(this.Sleeping);
			bw.Write(this.AutoPowerOff);
			bw.Write(this.IsWirelessProduct);
			bw.Write(this.UniqueID);
			bw.Write(this.IsDongle);
			bw.Write(this.CountryCode);
			bw.Write(this.GameMode);
			Utils.SerializePersistObjects<KeyMap>(this.Keys, bw);
			Utils.SerializePersistObjects<KeyMap>(this.FnKeys, bw);
			bw.Write((int)this.FunctionLockKey);
			bw.Write(this.FunctionLockEnabled);
			this.FunctionLockColor.Serialize(bw);
			bw.Write(this.SpeakerAvailable);
			bw.Write(this.MicrophoneAvailable);
			bw.Write(this.SoundMuted);
			bw.Write(this.MicrophoneMuted);
			bw.Write(this.SidetoneMuted);
			bw.Write(this.SoundVolume);
			bw.Write(this.MicrophoneVolume);
			bw.Write(this.SidetoneVolume);
			bw.Write(this.Linked);
			bw.Write(this.CanLink);
			bw.Write(this.CapPollingRate);
			bw.Write(this.CapGameMode);
			bw.Write(this.CapSensorDPIs);
			bw.Write(this.CapKeyAssignments);
			bw.Write(this.Version);
			bw.Write(this.ProductNeedUpdate);
			bw.Write(this.IsTIOChildDevice);
			bw.Write((int)this.ControlBy);
			bw.Write(321405651);
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x00052834 File Offset: 0x00050A34
		public virtual void Deserialize(BinaryReader br, int version)
		{
			this.DeviceType = (HyperXDeviceType)br.ReadByte();
			this.Model = (HyperXDeviceModel)br.ReadInt32();
			byte[] b = br.ReadBytes(16);
			this.ID = new Guid(b);
			this.VendorID = br.ReadUInt16();
			this.ProductID = br.ReadUInt16();
			this.PairID = br.ReadInt32();
			this.DeviceID = br.ReadString();
			string text = br.ReadString();
			if (string.IsNullOrEmpty(text))
			{
				this.Name = text;
			}
			this.Connected = br.ReadBoolean();
			this.IsSimulator = br.ReadBoolean();
			this.Synchronizing = br.ReadBoolean();
			this.DriverInstallationState = (DriverInstallationState)br.ReadInt32();
			this.DriverActivated = br.ReadBoolean();
			br.ReadBoolean();
			br.ReadBoolean();
			this.ColorSkin = (ColorSkins)br.ReadInt32();
			int num = br.ReadInt32();
			this.ExtraProperties.Clear();
			for (int i = 0; i < num; i++)
			{
				int key = br.ReadInt32();
				long value = br.ReadInt64();
				this.ExtraProperties.Add(key, value);
			}
			this.OnboardProfileId = br.ReadInt32();
			byte[] b2 = br.ReadBytes(16);
			new Guid(b2);
			for (int j = 0; j < this.SyncedPresets.Length; j++)
			{
				b2 = br.ReadBytes(16);
				this.SyncedPresets[j] = new Guid(b2);
			}
			this.CurrentPresetID = br.ReadByte();
			this.ProfileSlots = br.ReadInt32();
			this.LowBatteryThreshold = br.ReadInt32();
			this.LowBatteryThresholdMin = br.ReadInt32();
			this.LowBatteryThresholdMax = br.ReadInt32();
			this.ChargingStatus = (ChargingStatus)br.ReadInt32();
			this.Battery = br.ReadInt32();
			this.Sleeping = br.ReadBoolean();
			this.AutoPowerOff = br.ReadInt32();
			this.IsWirelessProduct = br.ReadBoolean();
			this.UniqueID = br.ReadString();
			this.IsDongle = br.ReadBoolean();
			this.CountryCode = br.ReadInt32();
			this.GameMode = br.ReadBoolean();
			List<KeyMap> list = new List<KeyMap>();
			Utils.DeserializePersistObjects<KeyMap>(list, br, version);
			if (this.Keys.Count != list.Count)
			{
				list.DeepCopyObjectTo(this.Keys);
			}
			list.Clear();
			Utils.DeserializePersistObjects<KeyMap>(list, br, version);
			if (this.FnKeys.Count == 0)
			{
				this.FnKeys.AddRange(list);
			}
			this.FunctionLockKey = (KeyCode)br.ReadInt32();
			this.FunctionLockEnabled = br.ReadBoolean();
			this.FunctionLockColor = new Color(br, version);
			this.SpeakerAvailable = br.ReadBoolean();
			this.MicrophoneAvailable = br.ReadBoolean();
			this.SoundMuted = br.ReadBoolean();
			this.MicrophoneMuted = br.ReadBoolean();
			this.SidetoneMuted = br.ReadBoolean();
			this.SoundVolume = br.ReadSingle();
			this.MicrophoneVolume = br.ReadSingle();
			this.SidetoneVolume = br.ReadSingle();
			this.Linked = br.ReadBoolean();
			this.CanLink = br.ReadBoolean();
			this.CapPollingRate = br.ReadBoolean();
			this.CapGameMode = br.ReadBoolean();
			this.CapSensorDPIs = br.ReadBoolean();
			this.CapKeyAssignments = br.ReadBoolean();
			this.Version = br.ReadInt32();
			this.ProductNeedUpdate = br.ReadBoolean();
			this.IsTIOChildDevice = br.ReadBoolean();
			this.ControlBy = (HyperXDevice.ControlBySoftware)br.ReadInt32();
			br.ReadInt32();
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void UpdateName()
		{
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06001E5C RID: 7772 RVA: 0x00052BA1 File Offset: 0x00050DA1
		// (set) Token: 0x06001E5D RID: 7773 RVA: 0x00052BA9 File Offset: 0x00050DA9
		public HyperXDeviceType DeviceType { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06001E5E RID: 7774 RVA: 0x00052BB2 File Offset: 0x00050DB2
		// (set) Token: 0x06001E5F RID: 7775 RVA: 0x00052BBA File Offset: 0x00050DBA
		public HyperXDeviceModel Model { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06001E60 RID: 7776 RVA: 0x00052BC3 File Offset: 0x00050DC3
		// (set) Token: 0x06001E61 RID: 7777 RVA: 0x00052BCB File Offset: 0x00050DCB
		public ushort VendorID { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06001E62 RID: 7778 RVA: 0x00052BD4 File Offset: 0x00050DD4
		// (set) Token: 0x06001E63 RID: 7779 RVA: 0x00052BDC File Offset: 0x00050DDC
		public ushort ProductID { get; set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06001E64 RID: 7780 RVA: 0x00052BE5 File Offset: 0x00050DE5
		// (set) Token: 0x06001E65 RID: 7781 RVA: 0x00052BED File Offset: 0x00050DED
		public int PairID { get; set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06001E66 RID: 7782 RVA: 0x00052BF6 File Offset: 0x00050DF6
		// (set) Token: 0x06001E67 RID: 7783 RVA: 0x00052BFE File Offset: 0x00050DFE
		public string DeviceID { get; protected set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06001E68 RID: 7784 RVA: 0x00052C07 File Offset: 0x00050E07
		// (set) Token: 0x06001E69 RID: 7785 RVA: 0x00052C0F File Offset: 0x00050E0F
		public string SecondaryDeviceID { get; protected set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06001E6A RID: 7786 RVA: 0x00052C18 File Offset: 0x00050E18
		// (set) Token: 0x06001E6B RID: 7787 RVA: 0x00052C20 File Offset: 0x00050E20
		public string Name { get; internal set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06001E6C RID: 7788 RVA: 0x00052C29 File Offset: 0x00050E29
		// (set) Token: 0x06001E6D RID: 7789 RVA: 0x00052C3C File Offset: 0x00050E3C
		public bool Connected
		{
			get
			{
				return this._connected || this.IsSimulator;
			}
			protected set
			{
				if (this._connected != value)
				{
					this._connected = value;
					string arg = value ? "connected" : "disconnected";
					Logger.WriteLine(string.Format("{0} {1}", this.Model, arg), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXDevice.cs", 83);
					this.OnConnectedStatusChanged(value);
				}
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06001E6E RID: 7790 RVA: 0x00052C92 File Offset: 0x00050E92
		// (set) Token: 0x06001E6F RID: 7791 RVA: 0x00052C9A File Offset: 0x00050E9A
		public bool IsSimulator { get; internal set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06001E70 RID: 7792 RVA: 0x00052CA3 File Offset: 0x00050EA3
		// (set) Token: 0x06001E71 RID: 7793 RVA: 0x00052CAB File Offset: 0x00050EAB
		public bool Synchronizing { get; protected set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06001E72 RID: 7794 RVA: 0x00052CB4 File Offset: 0x00050EB4
		// (set) Token: 0x06001E73 RID: 7795 RVA: 0x00052CBC File Offset: 0x00050EBC
		public DriverInstallationState DriverInstallationState { get; set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06001E74 RID: 7796 RVA: 0x00052CC5 File Offset: 0x00050EC5
		// (set) Token: 0x06001E75 RID: 7797 RVA: 0x00052CCD File Offset: 0x00050ECD
		public bool DriverActivated { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06001E76 RID: 7798 RVA: 0x00052CD6 File Offset: 0x00050ED6
		internal virtual bool NeedDTSAPO { get; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06001E77 RID: 7799 RVA: 0x00052CDE File Offset: 0x00050EDE
		public virtual bool IsOpened
		{
			get
			{
				return this.Device != null || this.IsSimulator || this is UVCDeviceBase;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06001E78 RID: 7800 RVA: 0x00052CFB File Offset: 0x00050EFB
		public virtual bool IsValidDevice
		{
			get
			{
				return this.IsOpened || this.UpgradeMode || this.IsSimulator;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06001E79 RID: 7801 RVA: 0x00052D15 File Offset: 0x00050F15
		// (set) Token: 0x06001E7A RID: 7802 RVA: 0x00052D1D File Offset: 0x00050F1D
		public ColorSkins ColorSkin { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06001E7B RID: 7803 RVA: 0x00052D26 File Offset: 0x00050F26
		// (set) Token: 0x06001E7C RID: 7804 RVA: 0x00052D2E File Offset: 0x00050F2E
		public bool IsWirelessProduct { get; set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06001E7D RID: 7805 RVA: 0x00052D37 File Offset: 0x00050F37
		// (set) Token: 0x06001E7E RID: 7806 RVA: 0x00052D3F File Offset: 0x00050F3F
		public string UniqueID { get; set; } = string.Empty;

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06001E7F RID: 7807 RVA: 0x00052D48 File Offset: 0x00050F48
		// (set) Token: 0x06001E80 RID: 7808 RVA: 0x00052D50 File Offset: 0x00050F50
		public bool IsDongle { get; set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06001E81 RID: 7809 RVA: 0x00052D59 File Offset: 0x00050F59
		// (set) Token: 0x06001E82 RID: 7810 RVA: 0x00052D61 File Offset: 0x00050F61
		public Dictionary<int, long> ExtraProperties { get; protected set; } = new Dictionary<int, long>();

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06001E83 RID: 7811 RVA: 0x00052D6A File Offset: 0x00050F6A
		// (set) Token: 0x06001E84 RID: 7812 RVA: 0x00052D72 File Offset: 0x00050F72
		public int OnboardProfileId { get; set; } = -1;

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06001E85 RID: 7813 RVA: 0x00052D7B File Offset: 0x00050F7B
		// (set) Token: 0x06001E86 RID: 7814 RVA: 0x00052D83 File Offset: 0x00050F83
		public Guid ActivedPresetGUID { get; set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06001E87 RID: 7815 RVA: 0x00052D8C File Offset: 0x00050F8C
		// (set) Token: 0x06001E88 RID: 7816 RVA: 0x00052D94 File Offset: 0x00050F94
		public Guid[] SyncedPresets { get; private set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x00052D9D File Offset: 0x00050F9D
		// (set) Token: 0x06001E8A RID: 7818 RVA: 0x00052DA5 File Offset: 0x00050FA5
		public byte CurrentPresetID { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06001E8B RID: 7819 RVA: 0x00052DAE File Offset: 0x00050FAE
		// (set) Token: 0x06001E8C RID: 7820 RVA: 0x00052DB6 File Offset: 0x00050FB6
		public int ProfileSlots { get; protected set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06001E8D RID: 7821 RVA: 0x00052DBF File Offset: 0x00050FBF
		// (set) Token: 0x06001E8E RID: 7822 RVA: 0x00052DC7 File Offset: 0x00050FC7
		public int LowBatteryThreshold { get; set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06001E8F RID: 7823 RVA: 0x00052DD0 File Offset: 0x00050FD0
		// (set) Token: 0x06001E90 RID: 7824 RVA: 0x00052DD8 File Offset: 0x00050FD8
		public int LowBatteryThresholdMin { get; set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06001E91 RID: 7825 RVA: 0x00052DE1 File Offset: 0x00050FE1
		// (set) Token: 0x06001E92 RID: 7826 RVA: 0x00052DE9 File Offset: 0x00050FE9
		public int LowBatteryThresholdMax { get; set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06001E93 RID: 7827 RVA: 0x00052DF2 File Offset: 0x00050FF2
		// (set) Token: 0x06001E94 RID: 7828 RVA: 0x00052DFC File Offset: 0x00050FFC
		public ChargingStatus ChargingStatus
		{
			get
			{
				return this._chargingStatus;
			}
			set
			{
				if (value == ChargingStatus.FullCharged)
				{
					this._chargingCounter++;
				}
				else
				{
					this._chargingCounter--;
					if (this._chargingCounter <= 0)
					{
						this._chargingCounter = 0;
						if (this.fullBatteryChanged)
						{
							this.fullBatteryChanged = false;
						}
					}
				}
				this._chargingStatus = value;
				this.OnChargingStatusChanged(this._chargingStatus);
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06001E95 RID: 7829 RVA: 0x00052E5D File Offset: 0x0005105D
		// (set) Token: 0x06001E96 RID: 7830 RVA: 0x00052E65 File Offset: 0x00051065
		public int Battery
		{
			get
			{
				return this._battery;
			}
			set
			{
				this._battery = value;
				if (this.BatteryUpdated != null)
				{
					this.BatteryUpdated.Invoke(this, value);
				}
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06001E97 RID: 7831 RVA: 0x00052E83 File Offset: 0x00051083
		// (set) Token: 0x06001E98 RID: 7832 RVA: 0x00052E8B File Offset: 0x0005108B
		public bool Sleeping
		{
			get
			{
				return this._sleeping;
			}
			internal set
			{
				if (this._sleeping != value)
				{
					this._sleeping = value;
					this.OnSleepingStatusChanged(value);
				}
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06001E99 RID: 7833 RVA: 0x00052EA4 File Offset: 0x000510A4
		// (set) Token: 0x06001E9A RID: 7834 RVA: 0x00052EAC File Offset: 0x000510AC
		public int AutoPowerOff { get; internal set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06001E9B RID: 7835 RVA: 0x00052EB5 File Offset: 0x000510B5
		// (set) Token: 0x06001E9C RID: 7836 RVA: 0x00052EBD File Offset: 0x000510BD
		public int CountryCode { get; set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06001E9D RID: 7837 RVA: 0x00052EC6 File Offset: 0x000510C6
		// (set) Token: 0x06001E9E RID: 7838 RVA: 0x00052ECE File Offset: 0x000510CE
		public List<KeyMap> Keys { get; protected set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06001E9F RID: 7839 RVA: 0x00052ED7 File Offset: 0x000510D7
		// (set) Token: 0x06001EA0 RID: 7840 RVA: 0x00052EDF File Offset: 0x000510DF
		public List<KeyMap> FnKeys { get; protected set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06001EA1 RID: 7841 RVA: 0x00052EE8 File Offset: 0x000510E8
		// (set) Token: 0x06001EA2 RID: 7842 RVA: 0x00052EF0 File Offset: 0x000510F0
		public KeyCode FunctionLockKey { get; set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06001EA3 RID: 7843 RVA: 0x00052EF9 File Offset: 0x000510F9
		// (set) Token: 0x06001EA4 RID: 7844 RVA: 0x00052F01 File Offset: 0x00051101
		public bool FunctionLockEnabled
		{
			get
			{
				return this._functionLockEnabled;
			}
			set
			{
				if (value != this._functionLockEnabled)
				{
					this._functionLockEnabled = value;
					if (this.FunctionLockStatusChanged != null)
					{
						this.FunctionLockStatusChanged.Invoke(this, value);
					}
				}
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x00052F28 File Offset: 0x00051128
		// (set) Token: 0x06001EA6 RID: 7846 RVA: 0x00052F30 File Offset: 0x00051130
		public Color FunctionLockColor { get; set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06001EA7 RID: 7847 RVA: 0x00052F39 File Offset: 0x00051139
		// (set) Token: 0x06001EA8 RID: 7848 RVA: 0x00052F41 File Offset: 0x00051141
		public virtual bool SpeakerAvailable { get; set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06001EA9 RID: 7849 RVA: 0x00052F4A File Offset: 0x0005114A
		// (set) Token: 0x06001EAA RID: 7850 RVA: 0x00052F52 File Offset: 0x00051152
		public virtual bool MicrophoneAvailable { get; set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06001EAB RID: 7851 RVA: 0x00052F5B File Offset: 0x0005115B
		// (set) Token: 0x06001EAC RID: 7852 RVA: 0x00052F63 File Offset: 0x00051163
		public virtual bool SoundMuted { get; set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06001EAD RID: 7853 RVA: 0x00052F6C File Offset: 0x0005116C
		// (set) Token: 0x06001EAE RID: 7854 RVA: 0x00052F74 File Offset: 0x00051174
		public virtual bool MicrophoneMuted { get; set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06001EAF RID: 7855 RVA: 0x00052F7D File Offset: 0x0005117D
		// (set) Token: 0x06001EB0 RID: 7856 RVA: 0x00052F85 File Offset: 0x00051185
		public virtual bool SidetoneMuted { get; set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06001EB1 RID: 7857 RVA: 0x00052F8E File Offset: 0x0005118E
		// (set) Token: 0x06001EB2 RID: 7858 RVA: 0x00052F96 File Offset: 0x00051196
		public virtual float SoundVolume { get; set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06001EB3 RID: 7859 RVA: 0x00052F9F File Offset: 0x0005119F
		// (set) Token: 0x06001EB4 RID: 7860 RVA: 0x00052FA7 File Offset: 0x000511A7
		public virtual float MicrophoneVolume { get; set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06001EB5 RID: 7861 RVA: 0x00052FB0 File Offset: 0x000511B0
		// (set) Token: 0x06001EB6 RID: 7862 RVA: 0x00052FB8 File Offset: 0x000511B8
		public virtual float SidetoneVolume { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06001EB7 RID: 7863 RVA: 0x00052FC1 File Offset: 0x000511C1
		// (set) Token: 0x06001EB8 RID: 7864 RVA: 0x00052FC9 File Offset: 0x000511C9
		public bool Linked
		{
			get
			{
				return this._linked;
			}
			set
			{
				this._linked = value;
				this.ClearLightingCommands();
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06001EB9 RID: 7865 RVA: 0x00052FD8 File Offset: 0x000511D8
		// (set) Token: 0x06001EBA RID: 7866 RVA: 0x00052FE0 File Offset: 0x000511E0
		public bool CanLink { get; set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06001EBB RID: 7867 RVA: 0x00052FE9 File Offset: 0x000511E9
		// (set) Token: 0x06001EBC RID: 7868 RVA: 0x00052FF1 File Offset: 0x000511F1
		public bool CapPollingRate { get; protected set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06001EBD RID: 7869 RVA: 0x00052FFA File Offset: 0x000511FA
		// (set) Token: 0x06001EBE RID: 7870 RVA: 0x00053002 File Offset: 0x00051202
		public bool CapGameMode { get; protected set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06001EBF RID: 7871 RVA: 0x0005300B File Offset: 0x0005120B
		// (set) Token: 0x06001EC0 RID: 7872 RVA: 0x00053013 File Offset: 0x00051213
		public bool CapSensorDPIs { get; protected set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06001EC1 RID: 7873 RVA: 0x0005301C File Offset: 0x0005121C
		// (set) Token: 0x06001EC2 RID: 7874 RVA: 0x00053024 File Offset: 0x00051224
		public bool CapKeyAssignments { get; protected set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06001EC3 RID: 7875 RVA: 0x0005302D File Offset: 0x0005122D
		// (set) Token: 0x06001EC4 RID: 7876 RVA: 0x00053035 File Offset: 0x00051235
		public virtual bool ProductNeedUpdate { get; protected set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06001EC5 RID: 7877 RVA: 0x0005303E File Offset: 0x0005123E
		// (set) Token: 0x06001EC6 RID: 7878 RVA: 0x00053048 File Offset: 0x00051248
		public int Version
		{
			get
			{
				return this._version;
			}
			set
			{
				this._version = value;
				string text = value.ToString("0000");
				this.Firmware = string.Concat(new string[]
				{
					text.Substring(0, 1),
					".",
					text.Substring(1, 1),
					".",
					text.Substring(2, 1),
					".",
					text.Substring(3, 1)
				});
			}
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x000530C0 File Offset: 0x000512C0
		public bool HasNewerVersion(ushort vid, ushort pid, ushort minVersion)
		{
			if (this.Device != null && this.Device.VendorId == vid && this.Device.ProductId == pid)
			{
				return Utils.HexVersion2DecimalVersion(this.Device.Version) < minVersion;
			}
			return this.SecondaryDevice != null && this.SecondaryDevice.VendorId == vid && this.SecondaryDevice.ProductId == pid && Utils.HexVersion2DecimalVersion(this.SecondaryDevice.Version) < minVersion;
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06001EC8 RID: 7880 RVA: 0x0005313E File Offset: 0x0005133E
		// (set) Token: 0x06001EC9 RID: 7881 RVA: 0x00053146 File Offset: 0x00051346
		public bool IsTIOChildDevice { get; set; }

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06001ECA RID: 7882 RVA: 0x00053150 File Offset: 0x00051350
		// (remove) Token: 0x06001ECB RID: 7883 RVA: 0x00053188 File Offset: 0x00051388
		public event TypedEventHandler<HyperXDevice, KeyEventAgrs> KeyReceived;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06001ECC RID: 7884 RVA: 0x000531C0 File Offset: 0x000513C0
		// (remove) Token: 0x06001ECD RID: 7885 RVA: 0x000531F8 File Offset: 0x000513F8
		public event TypedEventHandler<string, List<KeyMap>> LightsChanged;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06001ECE RID: 7886 RVA: 0x00053230 File Offset: 0x00051430
		// (remove) Token: 0x06001ECF RID: 7887 RVA: 0x00053268 File Offset: 0x00051468
		public event TypedEventHandler<object, bool> GameModeChanged;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06001ED0 RID: 7888 RVA: 0x000532A0 File Offset: 0x000514A0
		// (remove) Token: 0x06001ED1 RID: 7889 RVA: 0x000532D8 File Offset: 0x000514D8
		public event TypedEventHandler<object, int> BrightnessLevelChanged;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06001ED2 RID: 7890 RVA: 0x00053310 File Offset: 0x00051510
		// (remove) Token: 0x06001ED3 RID: 7891 RVA: 0x00053348 File Offset: 0x00051548
		public event TypedEventHandler<object, BrightnessAmbientPayload> BrightnessAmbientChanged;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06001ED4 RID: 7892 RVA: 0x00053380 File Offset: 0x00051580
		// (remove) Token: 0x06001ED5 RID: 7893 RVA: 0x000533B8 File Offset: 0x000515B8
		public event TypedEventHandler<HyperXDevice, bool> FunctionLockStatusChanged;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06001ED6 RID: 7894 RVA: 0x000533F0 File Offset: 0x000515F0
		// (remove) Token: 0x06001ED7 RID: 7895 RVA: 0x00053428 File Offset: 0x00051628
		public event TypedEventHandler<object, int> MouseDPILevelChanged;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06001ED8 RID: 7896 RVA: 0x00053460 File Offset: 0x00051660
		// (remove) Token: 0x06001ED9 RID: 7897 RVA: 0x00053498 File Offset: 0x00051698
		public event TypedEventHandler<object, int> PolligRateChanged;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06001EDA RID: 7898 RVA: 0x000534D0 File Offset: 0x000516D0
		// (remove) Token: 0x06001EDB RID: 7899 RVA: 0x00053508 File Offset: 0x00051708
		public event TypedEventHandler<object, ChargingStatus> ChargingStatusChanged;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06001EDC RID: 7900 RVA: 0x00053540 File Offset: 0x00051740
		// (remove) Token: 0x06001EDD RID: 7901 RVA: 0x00053578 File Offset: 0x00051778
		public event TypedEventHandler<object, int> BatteryUpdated;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06001EDE RID: 7902 RVA: 0x000535B0 File Offset: 0x000517B0
		// (remove) Token: 0x06001EDF RID: 7903 RVA: 0x000535E8 File Offset: 0x000517E8
		public event TypedEventHandler<object, bool> SleepingStatusChanged;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06001EE0 RID: 7904 RVA: 0x00053620 File Offset: 0x00051820
		// (remove) Token: 0x06001EE1 RID: 7905 RVA: 0x00053658 File Offset: 0x00051858
		public event TypedEventHandler<object, bool> ConnectedStatusChanged;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06001EE2 RID: 7906 RVA: 0x00053690 File Offset: 0x00051890
		// (remove) Token: 0x06001EE3 RID: 7907 RVA: 0x000536C8 File Offset: 0x000518C8
		public event TypedEventHandler<object, int> SyncProgressUpdated;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06001EE4 RID: 7908 RVA: 0x00053700 File Offset: 0x00051900
		// (remove) Token: 0x06001EE5 RID: 7909 RVA: 0x00053738 File Offset: 0x00051938
		public event TypedEventHandler<HyperXDevice, bool> DevicePaired;

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06001EE6 RID: 7910 RVA: 0x00053770 File Offset: 0x00051970
		// (remove) Token: 0x06001EE7 RID: 7911 RVA: 0x000537A8 File Offset: 0x000519A8
		public event TypedEventHandler<MMDevice, HyperXDevice> AudioDeviceConnected;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06001EE8 RID: 7912 RVA: 0x000537E0 File Offset: 0x000519E0
		// (remove) Token: 0x06001EE9 RID: 7913 RVA: 0x00053818 File Offset: 0x00051A18
		public event TypedEventHandler<AudioDeviceType, bool> AudioDeviceMuted;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06001EEA RID: 7914 RVA: 0x00053850 File Offset: 0x00051A50
		// (remove) Token: 0x06001EEB RID: 7915 RVA: 0x00053888 File Offset: 0x00051A88
		public event TypedEventHandler<AudioDeviceType, bool> AudioDeviceAvailabilityChanged;

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06001EEC RID: 7916 RVA: 0x000538C0 File Offset: 0x00051AC0
		// (remove) Token: 0x06001EED RID: 7917 RVA: 0x000538F8 File Offset: 0x00051AF8
		public event TypedEventHandler<AudioDeviceType, float> AudioDeviceVolumeUpdated;

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06001EEE RID: 7918 RVA: 0x00053930 File Offset: 0x00051B30
		// (remove) Token: 0x06001EEF RID: 7919 RVA: 0x00053968 File Offset: 0x00051B68
		public event TypedEventHandler<AudioDeviceType, float> AudioDeviceMeterUpdated;

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06001EF0 RID: 7920 RVA: 0x000539A0 File Offset: 0x00051BA0
		// (remove) Token: 0x06001EF1 RID: 7921 RVA: 0x000539D8 File Offset: 0x00051BD8
		public event TypedEventHandler<HyperXDevice, float> AudioDeviceTestProgressUpdated;

		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06001EF2 RID: 7922 RVA: 0x00053A10 File Offset: 0x00051C10
		// (remove) Token: 0x06001EF3 RID: 7923 RVA: 0x00053A48 File Offset: 0x00051C48
		public event TypedEventHandler<HyperXDevice, int> OnboardProfileChanged;

		// Token: 0x14000028 RID: 40
		// (add) Token: 0x06001EF4 RID: 7924 RVA: 0x00053A80 File Offset: 0x00051C80
		// (remove) Token: 0x06001EF5 RID: 7925 RVA: 0x00053AB8 File Offset: 0x00051CB8
		public event EventHandler<HyperXDevice> ControlByUpdateUI;

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06001EF6 RID: 7926 RVA: 0x00053AED File Offset: 0x00051CED
		// (set) Token: 0x06001EF7 RID: 7927 RVA: 0x00053AF5 File Offset: 0x00051CF5
		public bool DFUNeedReboot { get; set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06001EF8 RID: 7928 RVA: 0x00053AFE File Offset: 0x00051CFE
		// (set) Token: 0x06001EF9 RID: 7929 RVA: 0x00053B06 File Offset: 0x00051D06
		public bool NeedFactoryReset { get; set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06001EFA RID: 7930 RVA: 0x00053B0F File Offset: 0x00051D0F
		// (set) Token: 0x06001EFB RID: 7931 RVA: 0x00053B17 File Offset: 0x00051D17
		public bool Busy { get; set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06001EFC RID: 7932 RVA: 0x00053B20 File Offset: 0x00051D20
		// (set) Token: 0x06001EFD RID: 7933 RVA: 0x00053B28 File Offset: 0x00051D28
		public bool Pairing
		{
			get
			{
				return this._pairing;
			}
			set
			{
				if (this._pairing != value)
				{
					this._pairing = value;
					this.OnDevicePaired(value);
				}
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06001EFE RID: 7934 RVA: 0x00053B41 File Offset: 0x00051D41
		// (set) Token: 0x06001EFF RID: 7935 RVA: 0x00053B49 File Offset: 0x00051D49
		public int IOTimeout { get; set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06001F00 RID: 7936 RVA: 0x00053B52 File Offset: 0x00051D52
		// (set) Token: 0x06001F01 RID: 7937 RVA: 0x00053B5A File Offset: 0x00051D5A
		public int FramePerSecond
		{
			get
			{
				return this._deviceFramePerSecond;
			}
			protected set
			{
				this._deviceFramePerSecond = value;
				this.UIFramePerSecond = value;
				if (value != 0)
				{
					this._deviceframeInterval = 10000000L / (long)value;
				}
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06001F02 RID: 7938 RVA: 0x00053B7C File Offset: 0x00051D7C
		// (set) Token: 0x06001F03 RID: 7939 RVA: 0x00053B84 File Offset: 0x00051D84
		public int UIFramePerSecond
		{
			get
			{
				return this._uiFramePerSecond;
			}
			set
			{
				this._uiFramePerSecond = value;
				if (value != 0)
				{
					this._uiframeInterval = 10000000L / (long)value;
				}
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06001F04 RID: 7940 RVA: 0x00053B9F File Offset: 0x00051D9F
		// (set) Token: 0x06001F05 RID: 7941 RVA: 0x00053BA7 File Offset: 0x00051DA7
		public virtual bool UpgradeMode { get; protected set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06001F06 RID: 7942 RVA: 0x00053BB0 File Offset: 0x00051DB0
		// (set) Token: 0x06001F07 RID: 7943 RVA: 0x00053BB8 File Offset: 0x00051DB8
		public virtual string RequiredVersions { get; set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06001F08 RID: 7944 RVA: 0x00053BC1 File Offset: 0x00051DC1
		// (set) Token: 0x06001F09 RID: 7945 RVA: 0x00053BC9 File Offset: 0x00051DC9
		public HyperXDevice.ControlBySoftware ControlBy { get; set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06001F0A RID: 7946 RVA: 0x00053BD2 File Offset: 0x00051DD2
		// (set) Token: 0x06001F0B RID: 7947 RVA: 0x00053BDA File Offset: 0x00051DDA
		protected HidDevice Device { get; set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06001F0C RID: 7948 RVA: 0x00053BE3 File Offset: 0x00051DE3
		// (set) Token: 0x06001F0D RID: 7949 RVA: 0x00053BEB File Offset: 0x00051DEB
		protected HidDevice SecondaryDevice { get; set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06001F0E RID: 7950 RVA: 0x00053BF4 File Offset: 0x00051DF4
		// (set) Token: 0x06001F0F RID: 7951 RVA: 0x00053BFC File Offset: 0x00051DFC
		protected HidDevice NotificationDevice { get; set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06001F10 RID: 7952 RVA: 0x00053C05 File Offset: 0x00051E05
		// (set) Token: 0x06001F11 RID: 7953 RVA: 0x00053C0D File Offset: 0x00051E0D
		public string NotificationDeviceID { get; protected set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06001F12 RID: 7954 RVA: 0x00053C16 File Offset: 0x00051E16
		// (set) Token: 0x06001F13 RID: 7955 RVA: 0x00053C1E File Offset: 0x00051E1E
		public string DFUDeviceID { get; set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06001F14 RID: 7956 RVA: 0x00053C27 File Offset: 0x00051E27
		// (set) Token: 0x06001F15 RID: 7957 RVA: 0x00053C2F File Offset: 0x00051E2F
		public Guid ID { get; protected set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06001F16 RID: 7958 RVA: 0x00053C38 File Offset: 0x00051E38
		// (set) Token: 0x06001F17 RID: 7959 RVA: 0x00053C40 File Offset: 0x00051E40
		public List<HXCommandBase> Commands { get; set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06001F18 RID: 7960 RVA: 0x00053C49 File Offset: 0x00051E49
		// (set) Token: 0x06001F19 RID: 7961 RVA: 0x00053C51 File Offset: 0x00051E51
		public Preset Preset { get; protected set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06001F1A RID: 7962 RVA: 0x00053C5A File Offset: 0x00051E5A
		// (set) Token: 0x06001F1B RID: 7963 RVA: 0x00053C62 File Offset: 0x00051E62
		public Preset BuiltinPreset { get; set; }

		// Token: 0x06001F1C RID: 7964 RVA: 0x00053C6C File Offset: 0x00051E6C
		public Preset GetSafePreset()
		{
			Preset result = null;
			lock (this)
			{
				result = this.Preset;
			}
			return result;
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x00053CAC File Offset: 0x00051EAC
		public virtual LightingItem GetLinkLightLayout(KeyMap key)
		{
			return new LightingItem
			{
				X = key.Column,
				Y = key.Row
			};
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x00053CCC File Offset: 0x00051ECC
		public void CloneKeys(List<KeyMap> list)
		{
			List<KeyMap> keys = this.Keys;
			lock (keys)
			{
				list.Clear();
				foreach (KeyMap keyMap in this.Keys)
				{
					if (this.Connected)
					{
						list.Add(keyMap.Clone());
					}
				}
			}
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x00053D5C File Offset: 0x00051F5C
		public void CloneFnKeys(List<KeyMap> list)
		{
			List<KeyMap> fnKeys = this.FnKeys;
			lock (fnKeys)
			{
				list.Clear();
				foreach (KeyMap keyMap in this.FnKeys)
				{
					if (this.Connected)
					{
						list.Add(keyMap.Clone());
					}
				}
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06001F20 RID: 7968 RVA: 0x00053DEC File Offset: 0x00051FEC
		public EffectEngine Engine
		{
			get
			{
				return this._effectEngine;
			}
		}

		// Token: 0x06001F21 RID: 7969
		public abstract void SetLightings(IList<KeyMap> keys);

		// Token: 0x06001F22 RID: 7970 RVA: 0x00053DF4 File Offset: 0x00051FF4
		public HyperXDevice()
		{
			this.DeviceType = HyperXDeviceType.Unknown;
			this.Model = HyperXDeviceModel.Unknown;
			this.Commands = new List<HXCommandBase>();
			this.ID = Guid.NewGuid();
			this.Keys = new List<KeyMap>();
			this.FnKeys = new List<KeyMap>();
			this.UpgradeMode = false;
			this.FramePerSecond = 30;
			this.MaxSyncFrameCount = 60;
			this.LowBatteryThreshold = 15;
			this.LowBatteryThresholdMin = 5;
			this.LowBatteryThresholdMax = 25;
			this.IOTimeout = 500;
			this.DFUNeedReboot = true;
			this.UpdateDeviceList = new List<uint>();
			this.Sleeping = false;
			this.AutoPowerOff = 0;
			this.RequiredVersions = string.Empty;
			this.SnapshotPresetIDs = new Guid[10];
			this.SyncedPresets = new Guid[10];
			this.UpdateName();
			this._effectEngine = new EffectEngine();
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public void SubscribeLights()
		{
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public void UnsubscribeLights()
		{
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x00053F0C File Offset: 0x0005210C
		public void StartEffectEngine()
		{
			if (this.Device != null || this.Model == HyperXDeviceModel.Composite || this.IsSimulator)
			{
				this._effectEngine.DeviceModel = this.Model;
				this._effectEngine.Start(new Action<List<LightingItem>>(this.OnLightingItemFilled));
			}
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x00053F5F File Offset: 0x0005215F
		public void StopEffectEngine()
		{
			this._effectEngine.Stop();
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x00053F6C File Offset: 0x0005216C
		public void UpdateID()
		{
			if (string.IsNullOrEmpty(this.DeviceID))
			{
				return;
			}
			string value = new Regex("\\{[0-9a-f\\-]+\\}", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.CultureInvariant).Match(this.DeviceID).Value;
			this.ID = Guid.Parse(value);
			this.Name = Settings.Instance.GetDeviceName(this.DeviceID, this.Name);
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public void UpdateSyncIcons()
		{
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x00053FCF File Offset: 0x000521CF
		public virtual void OnKeyReceived(KeyCode key, int state)
		{
			if (this.KeyReceived != null)
			{
				this.KeyReceived.Invoke(this, new KeyEventAgrs(key, state));
			}
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void ChangeBrightness(int brightness)
		{
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x00053FEC File Offset: 0x000521EC
		public virtual void ChangeAutoPowerOff(int autoPowerOff)
		{
			this.AutoPowerOff = autoPowerOff;
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void OnKeyReceived(int x, int y, int state)
		{
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06001F2D RID: 7981 RVA: 0x00053FF5 File Offset: 0x000521F5
		// (set) Token: 0x06001F2E RID: 7982 RVA: 0x00053FFD File Offset: 0x000521FD
		public bool batteryThresholdNotified { get; set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06001F2F RID: 7983 RVA: 0x00054006 File Offset: 0x00052206
		// (set) Token: 0x06001F30 RID: 7984 RVA: 0x0005400E File Offset: 0x0005220E
		public bool battery5PercentageNotified { get; set; }

		// Token: 0x06001F31 RID: 7985 RVA: 0x00054018 File Offset: 0x00052218
		internal void CheckBatteryNotification()
		{
			if (!Settings.Instance.ShowNotifications || !Settings.Instance.NotifyBatteryLowPower)
			{
				return;
			}
			if (this.Battery == 0)
			{
				return;
			}
			string icon = string.Empty;
			HyperXDeviceType deviceType = this.DeviceType;
			if (deviceType <= HyperXDeviceType.Headset)
			{
				switch (deviceType)
				{
				case HyperXDeviceType.Keyboard:
				case (HyperXDeviceType)3:
				case HyperXDeviceType.Mousepad:
					break;
				case HyperXDeviceType.Mouse:
					icon = "ms-appx:///Assets/Toast/mouse.png";
					break;
				default:
					if (deviceType == HyperXDeviceType.Headset)
					{
						icon = "ms-appx:///Assets/Toast/headset.png";
					}
					break;
				}
			}
			else if (deviceType != HyperXDeviceType.DRAM && deviceType != HyperXDeviceType.Microphone)
			{
			}
			if (!this.battery5PercentageNotified && this.Battery <= this.LowBatteryThresholdMin)
			{
				this.battery5PercentageNotified = true;
				this.batteryThresholdNotified = true;
				NotificationCenter.PopMessage(icon, this.Name, string.Format(Utils.GetResourceString("LowBatteryWarningMessage"), this.Battery));
			}
			if (!this.batteryThresholdNotified && this.Battery <= this.LowBatteryThreshold)
			{
				this.batteryThresholdNotified = true;
				NotificationCenter.PopMessage(icon, this.Name, string.Format(Utils.GetResourceString("LowBatteryWarningMessage"), this.Battery));
			}
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x00054120 File Offset: 0x00052320
		protected void OnLightingItemFilled(List<LightingItem> items)
		{
			if (this.Linked)
			{
				return;
			}
			List<KeyMap> keys = this.Keys;
			lock (keys)
			{
				foreach (KeyMap keyMap in this.Keys)
				{
					bool flag2 = false;
					foreach (LightingItem lightingItem in items)
					{
						if (keyMap.Row == lightingItem.Y && keyMap.Column == lightingItem.X)
						{
							keyMap.Color = Color.FromRGB(lightingItem.Color.R, lightingItem.Color.G, lightingItem.Color.B);
							flag2 = true;
							break;
						}
					}
					if (!flag2)
					{
						keyMap.Color = Color.FromRGB(0, 0, 0);
					}
				}
			}
			if (this._deviceframeInterval > 0L && DateTime.Now.Ticks - this._lastDeviceFrameTimestamp >= this._deviceframeInterval)
			{
				this.RenderFrameToDevice(items);
				this._lastDeviceFrameTimestamp = DateTime.Now.Ticks;
			}
			this.OnLightsChanged();
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x00054298 File Offset: 0x00052498
		public void OnLightsChanged()
		{
			if (this._uiframeInterval > 0L && DateTime.Now.Ticks - this._lastUIFrameTimestamp >= this._uiframeInterval)
			{
				List<KeyMap> list = new List<KeyMap>();
				this.CloneKeys(list);
				TypedEventHandler<string, List<KeyMap>> lightsChanged = this.LightsChanged;
				if (lightsChanged != null)
				{
					lightsChanged.Invoke(this.DeviceID, list);
				}
				this._lastUIFrameTimestamp = DateTime.Now.Ticks;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06001F34 RID: 7988 RVA: 0x00054303 File Offset: 0x00052503
		// (set) Token: 0x06001F35 RID: 7989 RVA: 0x0005430B File Offset: 0x0005250B
		public bool CapLowBatteryThreshold { get; protected set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06001F36 RID: 7990 RVA: 0x00054314 File Offset: 0x00052514
		// (set) Token: 0x06001F37 RID: 7991 RVA: 0x0005431C File Offset: 0x0005251C
		public bool CapMacros { get; protected set; }

		// Token: 0x06001F38 RID: 7992 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		protected virtual void InitDevice()
		{
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x00054328 File Offset: 0x00052528
		private void InitDeviceRunloop(object state)
		{
			AutoResetEvent autoResetEvent = (AutoResetEvent)state;
			try
			{
				this.InitDevice();
				this._deviceInited = true;
			}
			catch (Exception ex)
			{
				Logger.WriteLine("Device Initialization Failed:" + ex.Message + ", device ID: " + this.DeviceID, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXDevice.cs", 784);
			}
			autoResetEvent.Set();
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x00054390 File Offset: 0x00052590
		public virtual void SetupDevice()
		{
			if (this.ControlBy == HyperXDevice.ControlBySoftware.NGENUITY)
			{
				AutoResetEvent autoResetEvent = new AutoResetEvent(false);
				new Thread(new ParameterizedThreadStart(this.InitDeviceRunloop)).Start(autoResetEvent);
				if (!autoResetEvent.WaitOne(3000))
				{
					throw new Exception("Initialize device timed out!");
				}
				if (!this._deviceInited)
				{
					throw new Exception("Failed to initialize device!");
				}
			}
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x000543EE File Offset: 0x000525EE
		public virtual bool ApplyExtraProperty(int key, long value)
		{
			if (!this.ExtraProperties.ContainsKey(key))
			{
				this.ExtraProperties[key] = value;
				return true;
			}
			if (this.ExtraProperties[key] != value)
			{
				this.ExtraProperties[key] = value;
				return true;
			}
			return false;
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void ApplyKeyAssignments(IEnumerable<KeyAssignment> keyAssignments)
		{
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void ApplyKeyAssignments(Preset preset)
		{
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void ApplyLowBatteryThreshold(int threshold)
		{
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void ApplySensorDPIs()
		{
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void ApplyMacros()
		{
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void ApplyPollingRate()
		{
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void ApplyBacklightFadeTime()
		{
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void ApplyGameMode()
		{
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void ApplySidetoneSettings()
		{
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x0005442C File Offset: 0x0005262C
		public virtual void ApplyBasicSettings()
		{
			this.SetAllLEDOff();
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void ApplyKeyAssignments()
		{
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x00054434 File Offset: 0x00052634
		public virtual void ApplyPreset(Preset preset)
		{
			lock (this)
			{
				if (this.Preset != preset)
				{
					this.Preset = preset;
				}
			}
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x0005447C File Offset: 0x0005267C
		public virtual void SetupPreset(Preset preset)
		{
			this.ApplyPresetAndEffects(preset);
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x00054488 File Offset: 0x00052688
		public virtual void ApplyPresetAndEffects(Preset preset)
		{
			if (preset.Mouse.DPIs.Count == 0)
			{
				preset.Mouse.ResetDynamicConfig(this);
			}
			if (preset.Keyboard.PollingRate == 0 || preset.Keyboard.BacklightFadeTime == 0)
			{
				preset.Keyboard.ResetDynamicConfig(this);
			}
			this.OnboardProfileId = -1;
			this.ClearLightingCommands();
			this.ApplyPreset(preset);
			this.ApplyEffects();
			this.ClearLightingCommands();
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x000544F9 File Offset: 0x000526F9
		public virtual void ApplyEffects()
		{
			if (this.Engine != null)
			{
				this.Engine.ClearEffects();
			}
			this.ClearLightingCommands();
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x00054514 File Offset: 0x00052714
		public virtual void SetControlByNG2()
		{
			Logger.WriteLine("SetControlByNG2", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXDevice.cs", 947);
			this.ControlBy = HyperXDevice.ControlBySoftware.NGENUITY;
			HyperXCenter.Center.TakeControlOfDeviceLighting(this);
		}

		// Token: 0x06001F4C RID: 8012 RVA: 0x0005453C File Offset: 0x0005273C
		public void ClearEngineEffects()
		{
			EffectEngine engine = this.Engine;
			if (engine == null)
			{
				return;
			}
			engine.ClearEffects();
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x0005454E File Offset: 0x0005274E
		public virtual void ResetToDefault()
		{
			this.SyncedPresets.For(delegate(int i)
			{
				this.SyncedPresets[i] = Guid.Empty;
			});
			this.SnapshotPresetIDs.For(delegate(int i)
			{
				this.SnapshotPresetIDs[i] = Guid.Empty;
			});
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x0005457E File Offset: 0x0005277E
		public virtual void SwitchToOnboardProfile(int profileId)
		{
			this.CurrentPresetID = (byte)profileId;
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06001F4F RID: 8015 RVA: 0x00054588 File Offset: 0x00052788
		// (set) Token: 0x06001F50 RID: 8016 RVA: 0x00054590 File Offset: 0x00052790
		public bool GameMode { get; set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06001F51 RID: 8017 RVA: 0x00054599 File Offset: 0x00052799
		// (set) Token: 0x06001F52 RID: 8018 RVA: 0x000545A1 File Offset: 0x000527A1
		public int LightLevel { get; set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06001F53 RID: 8019 RVA: 0x000545AA File Offset: 0x000527AA
		// (set) Token: 0x06001F54 RID: 8020 RVA: 0x000545B2 File Offset: 0x000527B2
		public int PresetIndex { get; set; }

		// Token: 0x06001F55 RID: 8021 RVA: 0x000545BC File Offset: 0x000527BC
		public virtual void OnGameModeChanged(bool gameMode)
		{
			this.GameMode = gameMode;
			Preset currentPreset = Preset.CurrentPreset;
			if (this.DeviceType == HyperXDeviceType.Keyboard)
			{
				currentPreset.Keyboard.GameMode = gameMode;
			}
			if (this.GameModeChanged != null)
			{
				this.GameModeChanged.Invoke(this, gameMode);
			}
			this.ClearLightingCommands();
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x00054606 File Offset: 0x00052806
		public virtual void OnLightLevelChanged(int level)
		{
			this.LightLevel = level;
			if (this.BrightnessLevelChanged != null)
			{
				this.BrightnessLevelChanged.Invoke(this, level);
			}
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x00054624 File Offset: 0x00052824
		public virtual void OnBrightnessAmbientChanged(BrightnessAmbientPayload payload)
		{
			if (this.BrightnessAmbientChanged != null)
			{
				this.BrightnessAmbientChanged.Invoke(this, payload);
			}
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x0005463B File Offset: 0x0005283B
		public virtual void OnMouseDPILevelChanged(int dpiLevel)
		{
			if (this.MouseDPILevelChanged != null)
			{
				this.MouseDPILevelChanged.Invoke(this, dpiLevel);
			}
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x00054652 File Offset: 0x00052852
		protected virtual void OnAudioDeviceAvailabilityChanged(AudioDeviceType type, bool available)
		{
			TypedEventHandler<AudioDeviceType, bool> audioDeviceAvailabilityChanged = this.AudioDeviceAvailabilityChanged;
			if (audioDeviceAvailabilityChanged == null)
			{
				return;
			}
			audioDeviceAvailabilityChanged.Invoke(type, available);
		}

		// Token: 0x06001F5A RID: 8026 RVA: 0x00054666 File Offset: 0x00052866
		public virtual void OnPollingRateChanged(PollingRates PollingRate)
		{
			if (this.PolligRateChanged != null)
			{
				this.PolligRateChanged.Invoke(this, (int)PollingRate);
			}
		}

		// Token: 0x06001F5B RID: 8027 RVA: 0x00054680 File Offset: 0x00052880
		public virtual void OnChargingStatusChanged(ChargingStatus status)
		{
			if (this.ChargingStatusChanged != null)
			{
				this.ChargingStatusChanged.Invoke(this, status);
			}
			if (Settings.Instance.ShowNotifications && Settings.Instance.NotifyBatteryLowPower && status == ChargingStatus.FullCharged && !this.fullBatteryChanged && this._chargingCounter >= 5)
			{
				this.fullBatteryChanged = true;
				this.NotifyBatteryFullyCharged();
			}
		}

		// Token: 0x06001F5C RID: 8028 RVA: 0x000546DC File Offset: 0x000528DC
		public virtual void OnConnectedStatusChanged(bool connected)
		{
			if (this.ConnectedStatusChanged != null)
			{
				this.ConnectedStatusChanged.Invoke(this, connected);
			}
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x000546F3 File Offset: 0x000528F3
		public virtual void OnSleepingStatusChanged(bool sleeping)
		{
			if (this.SleepingStatusChanged != null)
			{
				this.SleepingStatusChanged.Invoke(this, sleeping);
			}
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void ClearLightingCommands()
		{
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x0005470C File Offset: 0x0005290C
		protected void AddCommandInternal(HXCommandBase cmd)
		{
			if (this.IsSimulator)
			{
				return;
			}
			if ((this.UpgradeMode || this._stopped) && !cmd.Force)
			{
				return;
			}
			if (this._queueEngine != null)
			{
				UniversalCMDBase universalCMDBase = cmd as UniversalCMDBase;
				if (universalCMDBase != null && universalCMDBase.IsUniversalCommand)
				{
					this._queueEngine.AddCommand(universalCMDBase);
					return;
				}
			}
			List<HXCommandBase> commands = this.Commands;
			lock (commands)
			{
				if (cmd.Skip)
				{
					List<HXCommandBase> list = new List<HXCommandBase>();
					Type type = cmd.GetType();
					foreach (HXCommandBase hxcommandBase in this.Commands)
					{
						Type type2 = hxcommandBase.GetType();
						if (type == type2 && hxcommandBase.ProfileID == cmd.ProfileID)
						{
							list.Add(hxcommandBase);
						}
					}
					foreach (HXCommandBase item in list)
					{
						this.Commands.Remove(item);
					}
				}
				this.Commands.Add(cmd);
			}
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x00054864 File Offset: 0x00052A64
		public virtual bool IsDevice(string deviceId)
		{
			if (string.IsNullOrEmpty(deviceId))
			{
				return false;
			}
			string pattern = "VID_(?<VID>[0-9a-fA-F]+)&PID_(?<PID>[0-9a-fA-F]+).+?(?<ID>\\{[0-9a-f\\-]+\\})";
			Match match = Regex.Match(deviceId, pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
			string value = match.Groups["VID"].Value;
			string value2 = match.Groups["PID"].Value;
			string value3 = match.Groups["ID"].Value;
			if (!string.IsNullOrEmpty(this.DeviceID))
			{
				match = Regex.Match(this.DeviceID, pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
				if (match.Success)
				{
					string value4 = match.Groups["VID"].Value;
					string value5 = match.Groups["PID"].Value;
					string value6 = match.Groups["ID"].Value;
					if (string.Equals(value, value4) && string.Equals(value2, value5) && string.Equals(value3, value6))
					{
						return true;
					}
				}
			}
			if (!string.IsNullOrEmpty(this.NotificationDeviceID))
			{
				match = Regex.Match(this.NotificationDeviceID, pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
				if (match.Success)
				{
					string value7 = match.Groups["VID"].Value;
					string value8 = match.Groups["PID"].Value;
					string value9 = match.Groups["ID"].Value;
					if (string.Equals(value, value7) && string.Equals(value2, value8) && string.Equals(value3, value9))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06001F61 RID: 8033 RVA: 0x000549F2 File Offset: 0x00052BF2
		// (set) Token: 0x06001F62 RID: 8034 RVA: 0x000549FA File Offset: 0x00052BFA
		public string Firmware { get; protected set; }

		// Token: 0x06001F63 RID: 8035 RVA: 0x00054A04 File Offset: 0x00052C04
		private void ProcessCommandTask()
		{
			HyperXDevice.<ProcessCommandTask>d__464 <ProcessCommandTask>d__;
			<ProcessCommandTask>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<ProcessCommandTask>d__.<>4__this = this;
			<ProcessCommandTask>d__.<>1__state = -1;
			<ProcessCommandTask>d__.<>t__builder.Start<HyperXDevice.<ProcessCommandTask>d__464>(ref <ProcessCommandTask>d__);
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06001F64 RID: 8036 RVA: 0x00054A3B File Offset: 0x00052C3B
		// (set) Token: 0x06001F65 RID: 8037 RVA: 0x00054A43 File Offset: 0x00052C43
		public bool IOError { get; set; }

		// Token: 0x06001F66 RID: 8038 RVA: 0x00054A4C File Offset: 0x00052C4C
		public virtual void Start()
		{
			if (this._running)
			{
				return;
			}
			this._stopped = false;
			this._running = true;
			if (!this.IsSimulator)
			{
				Task.Factory.StartNew(new Action(this.ProcessCommandTask));
			}
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x00054A84 File Offset: 0x00052C84
		public virtual void PreStop()
		{
			this._stopping = true;
			this._stopTime = DateTime.Now;
			List<HXCommandBase> commands = this.Commands;
			lock (commands)
			{
				this.Commands.Clear();
			}
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x00054ADC File Offset: 0x00052CDC
		public void PauseCommandTask()
		{
			this._stopped = true;
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x00054AE5 File Offset: 0x00052CE5
		public void ResumeCommandTask()
		{
			this._stopped = false;
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x00054AF0 File Offset: 0x00052CF0
		public virtual void Stop(bool waitUntilStopped)
		{
			try
			{
				Logger.WriteLine(string.Format("Stopping {0} ...", this.Model), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXDevice.cs", 1278);
				EffectEngine effectEngine = this._effectEngine;
				if (effectEngine != null)
				{
					effectEngine.Stop();
				}
				if (!waitUntilStopped)
				{
					this.PreStop();
				}
				this._stopped = true;
				while (!this.IsSimulator && this._running && waitUntilStopped)
				{
					try
					{
						Thread.Sleep(100);
					}
					catch (Exception info)
					{
						Logger.WriteLine(info, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXDevice.cs", 1297);
					}
				}
				this._effectEngine = null;
				this.CloseDevice();
			}
			catch (Exception ex)
			{
				Logger.WriteLine("Failed to Stop Device " + ex.Message, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXDevice.cs", 1308);
			}
			TimeSpan timeSpan = DateTime.Now.Subtract(this._stopTime);
			Logger.WriteLine(string.Format("{0} Stopped. Total {1} ms.", this.Model, timeSpan.TotalMilliseconds), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXDevice.cs", 1312);
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x00054C08 File Offset: 0x00052E08
		public virtual byte[] CreateBuffer()
		{
			byte[] array = new byte[264];
			array[0] = 7;
			return array;
		}

		// Token: 0x06001F6C RID: 8044
		public abstract EffectImplBase CreateEffect(EffectItemBase item);

		// Token: 0x06001F6D RID: 8045 RVA: 0x00054C18 File Offset: 0x00052E18
		public virtual void AddPreviewEffect(EffectItemBase effect)
		{
			effect.ChangeDevice(this);
			EffectImplBase effect2 = this.CreateEffect(effect);
			EffectEngine engine = this.Engine;
			if (engine == null)
			{
				return;
			}
			engine.AddEffect(effect2);
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x00054C48 File Offset: 0x00052E48
		public virtual void SetOutputReport(HidDevice device, byte[] buffer)
		{
			try
			{
				this.IOError = false;
				device.SetOutputReport(buffer);
			}
			catch (Exception ex)
			{
				this.IOError = true;
				throw ex;
			}
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x00054C80 File Offset: 0x00052E80
		public virtual void SetOutputReport(int cmd, byte[] buffer)
		{
			this.SetOutputReport(buffer);
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x00054C89 File Offset: 0x00052E89
		public virtual byte[] GetInputReport(int cmd)
		{
			return this.GetInputReport();
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x00054C91 File Offset: 0x00052E91
		public virtual void SetOutputReport(byte[] buffer)
		{
			if (this.Device != null)
			{
				this.SetOutputReport(this.Device, buffer);
				return;
			}
			throw new IOException("SetOutputReport");
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x00054CB3 File Offset: 0x00052EB3
		public virtual void SetSecondaryOutputReport(byte[] buffer)
		{
			if (this.SecondaryDevice != null)
			{
				this.SetOutputReport(this.SecondaryDevice, buffer);
				return;
			}
			throw new IOException("SetOutputReport");
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x00054CD8 File Offset: 0x00052ED8
		public virtual void SetFeatureReport(HidDevice device, byte[] buffer)
		{
			try
			{
				this.IOError = false;
				device.SetFeatureReport(buffer);
			}
			catch (Exception innerException)
			{
				this.IOError = true;
				throw new IOException("SetFeatureReport", innerException);
			}
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x00054D1C File Offset: 0x00052F1C
		public virtual void SetFeatureReport(byte[] buffer)
		{
			if (this.Device != null)
			{
				this.SetFeatureReport(this.Device, buffer);
				return;
			}
			throw new IOException("SetFeatureReport");
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x00054D3E File Offset: 0x00052F3E
		public virtual void SetSecondaryFeatureReport(byte[] buffer)
		{
			if (this.SecondaryDevice != null)
			{
				this.SetFeatureReport(this.SecondaryDevice, buffer);
				return;
			}
			throw new IOException("SetFeatureReport");
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x00054D60 File Offset: 0x00052F60
		public virtual void SendOutputReport(HidDevice device, byte[] buffer)
		{
			try
			{
				this.IOError = false;
				device.SetOutputReport(buffer);
			}
			catch (Exception innerException)
			{
				this.IOError = true;
				throw new IOException("SendOutputReport", innerException);
			}
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x00054DA4 File Offset: 0x00052FA4
		public virtual void SendOutputReport(byte[] buffer)
		{
			if (this.Device != null)
			{
				this.SendOutputReport(this.Device, buffer);
				return;
			}
			throw new IOException("SendOutputReport");
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x00054DC6 File Offset: 0x00052FC6
		public virtual void SendSecondaryOutputReport(byte[] buffer)
		{
			if (this.SecondaryDevice != null)
			{
				this.SendOutputReport(this.SecondaryDevice, buffer);
				return;
			}
			throw new IOException("SendOutputReport");
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x00054DE8 File Offset: 0x00052FE8
		public virtual byte[] GetInputReport()
		{
			if (this.Device != null)
			{
				try
				{
					return this.Device.GetInputReport();
				}
				catch (Exception)
				{
				}
			}
			return null;
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x00054E24 File Offset: 0x00053024
		public virtual byte[] GetSecondaryInputReport()
		{
			if (this.SecondaryDevice != null)
			{
				try
				{
					return this.SecondaryDevice.GetInputReport();
				}
				catch (Exception)
				{
				}
			}
			return null;
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x00054E60 File Offset: 0x00053060
		public virtual byte[] GetFeatureReport(byte reportId)
		{
			if (this.Device != null)
			{
				try
				{
					return this.Device.GetFeatureReport(reportId);
				}
				catch (Exception)
				{
					goto IL_25;
				}
				goto IL_1A;
				IL_25:
				return null;
			}
			IL_1A:
			throw new IOException("GetFeatureReport");
		}

		// Token: 0x06001F7C RID: 8060 RVA: 0x00054EA8 File Offset: 0x000530A8
		public virtual byte[] GetFeatureReport()
		{
			if (this.Device != null)
			{
				try
				{
					return this.Device.GetFeatureReport(7);
				}
				catch (Exception)
				{
					goto IL_25;
				}
				goto IL_1A;
				IL_25:
				return null;
			}
			IL_1A:
			throw new IOException("GetFeatureReport");
		}

		// Token: 0x06001F7D RID: 8061 RVA: 0x00054EF0 File Offset: 0x000530F0
		public virtual byte[] GetSecondaryFeatureReport(byte reportId = 0)
		{
			if (this.SecondaryDevice != null)
			{
				try
				{
					return this.SecondaryDevice.GetFeatureReport(reportId);
				}
				catch (Exception)
				{
					goto IL_25;
				}
				goto IL_1A;
				IL_25:
				return null;
			}
			IL_1A:
			throw new IOException("GetFeatureReport");
		}

		// Token: 0x06001F7E RID: 8062
		public abstract void SetAllLEDOff();

		// Token: 0x06001F7F RID: 8063 RVA: 0x00054F38 File Offset: 0x00053138
		public int CalibrateBattery(int rawBattery, int loopCount = 5)
		{
			if (this._calibratedBatteries.Count == 0 && this._battery == rawBattery)
			{
				return -1;
			}
			this._calibratedBatteries.Add(rawBattery);
			if (this._calibratedBatteries.Count < loopCount)
			{
				return -1;
			}
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			for (int i = 0; i < this._calibratedBatteries.Count; i++)
			{
				int key;
				if (dictionary.TryGetValue(this._calibratedBatteries[i], out key))
				{
					Dictionary<int, int> dictionary2 = dictionary;
					key = this._calibratedBatteries[i];
					int num = dictionary2[key];
					dictionary2[key] = num + 1;
				}
				else
				{
					dictionary.Add(this._calibratedBatteries[i], 1);
				}
			}
			int max = 0;
			int result;
			if (dictionary.Count == 1)
			{
				result = dictionary.Keys.First<int>();
			}
			else
			{
				foreach (int num2 in dictionary.Values)
				{
					if (max < num2)
					{
						max = num2;
					}
				}
				result = dictionary.FirstOrDefault((KeyValuePair<int, int> n) => n.Value == max).Key;
			}
			this._calibratedBatteries.Clear();
			return result;
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06001F80 RID: 8064 RVA: 0x00055088 File Offset: 0x00053288
		// (set) Token: 0x06001F81 RID: 8065 RVA: 0x00055090 File Offset: 0x00053290
		public List<uint> UpdateDeviceList { get; protected set; }

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06001F82 RID: 8066 RVA: 0x0005509C File Offset: 0x0005329C
		// (remove) Token: 0x06001F83 RID: 8067 RVA: 0x000550D4 File Offset: 0x000532D4
		public event FirmwareProgressHandler Updating;

		// Token: 0x06001F84 RID: 8068 RVA: 0x00055109 File Offset: 0x00053309
		protected virtual void OnUpdating(UpdateFirmwareState state, int progress)
		{
			if (this.Updating != null)
			{
				this.Updating(this.Model, state, progress);
			}
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x00055126 File Offset: 0x00053326
		protected virtual void OnAudioMeterUpdated(AudioDeviceType type, float peakVolume)
		{
			TypedEventHandler<AudioDeviceType, float> audioDeviceMeterUpdated = this.AudioDeviceMeterUpdated;
			if (audioDeviceMeterUpdated == null)
			{
				return;
			}
			audioDeviceMeterUpdated.Invoke(type, peakVolume);
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x0005513C File Offset: 0x0005333C
		protected virtual void OnAudioDeviceMuted(AudioDeviceType type, bool muted, bool triggerBySystem)
		{
			Preset safePreset = this.GetSafePreset();
			switch (type)
			{
			case AudioDeviceType.Sound:
				if (this.SoundMuted != muted)
				{
					this.SoundMuted = muted;
					if (safePreset != null)
					{
						if (this.DeviceType == HyperXDeviceType.Headset)
						{
							safePreset.Headset.SoundMuted = muted;
						}
						if (this.DeviceType == HyperXDeviceType.Microphone)
						{
							safePreset.Microphone.SoundMuted = muted;
						}
					}
					if (this.AudioDeviceMuted != null && triggerBySystem)
					{
						this.AudioDeviceMuted.Invoke(type, muted);
						return;
					}
				}
				break;
			case AudioDeviceType.Microphone:
				if (this.MicrophoneMuted != muted)
				{
					this.MicrophoneMuted = muted;
					if (safePreset != null)
					{
						if (this.DeviceType == HyperXDeviceType.Headset)
						{
							safePreset.Headset.MicrophoneMuted = muted;
						}
						if (this.DeviceType == HyperXDeviceType.Microphone)
						{
							safePreset.Microphone.MicrophoneMuted = muted;
						}
					}
					if (this.AudioDeviceMuted != null && triggerBySystem)
					{
						this.AudioDeviceMuted.Invoke(type, muted);
						return;
					}
				}
				break;
			case AudioDeviceType.Sidetone:
				if (this.SidetoneMuted != muted)
				{
					this.SidetoneMuted = muted;
					if (safePreset != null)
					{
						if (this.DeviceType == HyperXDeviceType.Headset)
						{
							safePreset.Headset.SidetoneMuted = muted;
						}
						if (this.DeviceType == HyperXDeviceType.Microphone)
						{
							safePreset.Microphone.SidetoneMuted = muted;
						}
					}
					if (this.AudioDeviceMuted != null && triggerBySystem)
					{
						this.AudioDeviceMuted.Invoke(type, muted);
					}
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x00055278 File Offset: 0x00053478
		protected virtual void OnAudioDeviceVolumeUpdated(AudioDeviceType type, float volume, bool triggerBySystem)
		{
			Preset safePreset = this.GetSafePreset();
			switch (type)
			{
			case AudioDeviceType.Sound:
				if (this.SoundVolume != volume)
				{
					this.SoundVolume = volume;
					if (safePreset != null)
					{
						if (this.DeviceType == HyperXDeviceType.Headset)
						{
							safePreset.Headset.SoundVolume = volume;
						}
						if (this.DeviceType == HyperXDeviceType.Microphone)
						{
							safePreset.Microphone.SoundVolume = volume;
						}
					}
					if (this.AudioDeviceVolumeUpdated != null && triggerBySystem)
					{
						this.AudioDeviceVolumeUpdated.Invoke(type, volume);
						return;
					}
				}
				break;
			case AudioDeviceType.Microphone:
				if (this.MicrophoneVolume != volume)
				{
					this.MicrophoneVolume = volume;
					if (safePreset != null)
					{
						if (this.DeviceType == HyperXDeviceType.Headset)
						{
							safePreset.Headset.MicrophoneVolume = volume;
						}
						if (this.DeviceType == HyperXDeviceType.Microphone)
						{
							safePreset.Microphone.MicrophoneVolume = volume;
						}
					}
					if (this.AudioDeviceVolumeUpdated != null && triggerBySystem)
					{
						this.AudioDeviceVolumeUpdated.Invoke(type, volume);
						return;
					}
				}
				break;
			case AudioDeviceType.Sidetone:
				if (this.SidetoneVolume != volume)
				{
					this.SidetoneVolume = volume;
					if (safePreset != null)
					{
						if (this.DeviceType == HyperXDeviceType.Headset)
						{
							safePreset.Headset.SidetoneVolume = volume;
						}
						if (this.DeviceType == HyperXDeviceType.Microphone)
						{
							safePreset.Microphone.SidetoneVolume = volume;
						}
					}
					if (this.AudioDeviceVolumeUpdated != null && triggerBySystem)
					{
						this.AudioDeviceVolumeUpdated.Invoke(type, volume);
					}
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x000553B4 File Offset: 0x000535B4
		public virtual void OnAudioDeviceChanged(AudioDeviceType type, float volume, bool muted, bool triggerBySystem)
		{
			this.OnAudioDeviceMuted(type, muted, triggerBySystem);
			this.OnAudioDeviceVolumeUpdated(type, volume, triggerBySystem);
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x000553CC File Offset: 0x000535CC
		public virtual void CloseMainDevice()
		{
			lock (this)
			{
				if (this.Device != null)
				{
					try
					{
						this.Device.InputReportReceived -= new TypedEventHandler<HidDevice, byte[]>(this.DeviceInputReportReceived);
						this.Device.Dispose();
					}
					catch (Exception)
					{
					}
					this.Device = null;
				}
			}
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x00055444 File Offset: 0x00053644
		public void CloseNotificationDevice()
		{
			if (this.NotificationDevice != null)
			{
				try
				{
					this.NotificationDevice.InputReportReceived -= new TypedEventHandler<HidDevice, byte[]>(this.DeviceInputReportReceived);
					this.NotificationDevice.Dispose();
				}
				catch (Exception)
				{
				}
				finally
				{
					this.NotificationDevice = null;
				}
			}
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x000554A4 File Offset: 0x000536A4
		public void CloseSecondaryDevice()
		{
			if (this.SecondaryDevice != null)
			{
				try
				{
					this.SecondaryDevice.InputReportReceived -= new TypedEventHandler<HidDevice, byte[]>(this.DeviceInputReportReceived);
					this.SecondaryDevice.Dispose();
				}
				catch (Exception)
				{
				}
				finally
				{
					this.SecondaryDevice = null;
				}
			}
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x00055504 File Offset: 0x00053704
		public void CloseDevice()
		{
			this.CloseMainDevice();
			this.CloseNotificationDevice();
			this.CloseSecondaryDevice();
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x00055518 File Offset: 0x00053718
		public virtual void RetriveUpdateDeviceInfo(List<string> deviceInfo)
		{
			if (!string.IsNullOrEmpty(this.DeviceID))
			{
				deviceInfo.Add(this.DeviceID);
			}
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x00055534 File Offset: 0x00053734
		public virtual void OpenNotification(string deviceId)
		{
			if (this.NotificationDevice != null)
			{
				this.CloseNotificationDevice();
			}
			HidDevice hidDevice = HidDevice.FromId(deviceId, 3221225472U);
			this.NotificationDevice = hidDevice;
			this.NotificationDeviceID = deviceId;
			if (hidDevice != null)
			{
				hidDevice.InputReportReceived += new TypedEventHandler<HidDevice, byte[]>(this.DeviceInputReportReceived);
			}
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x0005557E File Offset: 0x0005377E
		public void OpenNotificationTunnel()
		{
			if (this.Device != null)
			{
				this.Device.InputReportReceived += new TypedEventHandler<HidDevice, byte[]>(this.DeviceInputReportReceived);
			}
		}

		// Token: 0x06001F90 RID: 8080 RVA: 0x0005559F File Offset: 0x0005379F
		public void OpenSecondaryEndpointNotificationTunnel()
		{
			if (this.SecondaryDevice != null)
			{
				this.SecondaryDevice.InputReportReceived += new TypedEventHandler<HidDevice, byte[]>(this.DeviceInputReportReceived);
			}
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x000555C0 File Offset: 0x000537C0
		private void DeviceInputReportReceived(HidDevice sender, byte[] buffer)
		{
			try
			{
				this.OnDeviceInputReportReceived(buffer);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		protected virtual void OnDeviceInputReportReceived(byte[] buffer)
		{
		}

		// Token: 0x06001F93 RID: 8083 RVA: 0x000555EC File Offset: 0x000537EC
		protected virtual void OnDevicePaired(bool paired)
		{
			if (this.DevicePaired != null)
			{
				this.DevicePaired.Invoke(this, paired);
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06001F94 RID: 8084 RVA: 0x00015297 File Offset: 0x00013497
		protected virtual bool DeviceIsReady
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001F95 RID: 8085 RVA: 0x00055604 File Offset: 0x00053804
		public virtual void RequestAck()
		{
			lock (this)
			{
				if (this._sem == null)
				{
					this._sem = new Semaphore(0, 1);
				}
			}
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x00055650 File Offset: 0x00053850
		protected bool WaitAck(int timeout)
		{
			bool result = false;
			try
			{
				Semaphore sem = this._sem;
				bool? flag = (sem != null) ? new bool?(sem.WaitOne(timeout)) : null;
				if (flag != null && flag.Value)
				{
					result = true;
				}
			}
			catch (Exception ex)
			{
				Logger.WriteLine(ex.Message, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXDevice.cs", 1995);
			}
			lock (this)
			{
				Semaphore sem2 = this._sem;
				if (sem2 != null)
				{
					sem2.Dispose();
				}
				this._sem = null;
			}
			return result;
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x000556FC File Offset: 0x000538FC
		public virtual bool WaitAck()
		{
			return this.WaitAck(this.IOTimeout);
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x0005570C File Offset: 0x0005390C
		public virtual void EchoAck()
		{
			lock (this)
			{
				try
				{
					Semaphore sem = this._sem;
					if (sem != null)
					{
						sem.Release();
					}
				}
				catch (Exception ex)
				{
					Logger.WriteLine(ex.Message, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXDevice.cs", 2021);
				}
			}
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x00055778 File Offset: 0x00053978
		public virtual void OpenDevice(string deviceId)
		{
			this.SyncedPresets.For(delegate(int i)
			{
				this.SyncedPresets[i] = Guid.Empty;
			});
			this.SnapshotPresetIDs.For(delegate(int i)
			{
				this.SnapshotPresetIDs[i] = Guid.Empty;
			});
			string text = (deviceId == null) ? "" : deviceId;
			this.DeviceID = deviceId;
			if (this.Model == HyperXDeviceModel.KeyboardAlloyMKW100)
			{
				this.Device = HidDevice.FromId(text, 1073741824U);
			}
			else
			{
				this.Device = HidDevice.FromId(text, 3221225472U);
			}
			this.Connected = false;
			if (this.Device != null)
			{
				string text2 = this.Device.Version.ToString("X4");
				this.Version = (int)this.Device.Version;
				Logger.WriteLine(string.Format("Open Device: {0}[{1}({2})] {3}", new object[]
				{
					this.Model,
					this.Version,
					text2,
					deviceId
				}), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXDevice.cs", 2052);
				this.UpgradeMode = string.Equals(text2, "0001");
				if (this.UpgradeMode)
				{
					this.DFUDeviceID = text;
				}
				this.Connected = true;
				if (!HyperXCenter.Center.Updater.UpgradeMode && HyperXCenter.Center.HasNewerBuiltinFirmware(this) && Settings.Instance.ShowNotifications && Settings.Instance.NotifyFirmwareUpdateAvailable)
				{
					Logger.WriteLine("Notify Firmware Update", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXDevice.cs", 2066);
					this.NotifyFirmwareUpdate();
					return;
				}
			}
			else
			{
				Logger.WriteLine(string.Format("Could not open device: {0}", text), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXDevice.cs", 2072);
			}
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x00055908 File Offset: 0x00053B08
		public bool OpenUniversalDevice(string deviceId, bool inDFUmode)
		{
			try
			{
				this.SyncedPresets.For(delegate(int i)
				{
					this.SyncedPresets[i] = Guid.Empty;
				});
				this.SnapshotPresetIDs.For(delegate(int i)
				{
					this.SnapshotPresetIDs[i] = Guid.Empty;
				});
				this.DeviceID = deviceId;
				this.Device = HidDevice.FromId(deviceId, 3221225472U);
				this.Connected = (this.Device != null);
				Logger.WriteLine(string.Format("Open Device: {0}[{1}] {2}", this.Model, this.Version, this.DeviceID), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXDevice.cs", 2090);
			}
			catch (Exception ex)
			{
				Logger.WriteLine("Could not open device: " + deviceId + ", error: " + ex.Message, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXDevice.cs", 2094);
				return false;
			}
			if (!this.Connected)
			{
				Logger.WriteLine("Could not open device: " + deviceId + ", Device not connected", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXDevice.cs", 2100);
				return false;
			}
			return true;
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x00055A08 File Offset: 0x00053C08
		public void InitUniversalDevice()
		{
			SpinWait.SpinUntil(() => false, 500);
			this.InitDevice();
			this._deviceInited = true;
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x00055A44 File Offset: 0x00053C44
		public void SetDfuUpgrade()
		{
			this.Device.Version.ToString("X4");
			this.Version = (int)this.Device.Version;
			this.UpgradeMode = true;
			this.DFUDeviceID = this.DeviceID;
			this.NotifyFirmwareUpdate();
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x00055A94 File Offset: 0x00053C94
		public Task CheckNewFirmwareAvailable(IUniversalDevice universalDevice)
		{
			HyperXDevice.<CheckNewFirmwareAvailable>d__535 <CheckNewFirmwareAvailable>d__;
			<CheckNewFirmwareAvailable>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<CheckNewFirmwareAvailable>d__.<>4__this = this;
			<CheckNewFirmwareAvailable>d__.universalDevice = universalDevice;
			<CheckNewFirmwareAvailable>d__.<>1__state = -1;
			<CheckNewFirmwareAvailable>d__.<>t__builder.Start<HyperXDevice.<CheckNewFirmwareAvailable>d__535>(ref <CheckNewFirmwareAvailable>d__);
			return <CheckNewFirmwareAvailable>d__.<>t__builder.Task;
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x00055AE0 File Offset: 0x00053CE0
		private bool OtaDeviceHasNewerBuiltinFirmware(DeviceInforPayload deviceInfo)
		{
			if (deviceInfo != null)
			{
				ushort num = ushort.Parse(deviceInfo.SecondaryFirmwareVersion);
				uint num2 = (uint)this.VendorID;
				num2 <<= 16;
				num2 |= (uint)this.ProductID;
				ushort? minimunRFClientFirmwareVersions = HyperXCenter.Center.GetMinimunRFClientFirmwareVersions(num2);
				if (minimunRFClientFirmwareVersions != null)
				{
					ushort? num3 = minimunRFClientFirmwareVersions;
					int? num4 = (num3 != null) ? new int?((int)num3.GetValueOrDefault()) : null;
					int num5 = (int)num;
					return num4.GetValueOrDefault() > num5 & num4 != null;
				}
			}
			return false;
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x00055B64 File Offset: 0x00053D64
		public bool HasNewerFirmwareByUniversalDeviceInfo(DeviceInforPayload deviceInfo)
		{
			if (deviceInfo != null)
			{
				ushort num = ushort.Parse(deviceInfo.SecondaryFirmwareVersion);
				uint num2 = (uint)this.VendorID;
				num2 <<= 16;
				num2 |= (uint)deviceInfo.SecondaryProductID;
				ushort? minimunRFClientFirmwareVersions = HyperXCenter.Center.GetMinimunRFClientFirmwareVersions(num2);
				if (minimunRFClientFirmwareVersions != null)
				{
					ushort? num3 = minimunRFClientFirmwareVersions;
					int? num4 = (num3 != null) ? new int?((int)num3.GetValueOrDefault()) : null;
					int num5 = (int)num;
					return num4.GetValueOrDefault() > num5 & num4 != null;
				}
			}
			return false;
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x00055BE8 File Offset: 0x00053DE8
		public void OpenMousePulsefireHaste2Wireless(string deviceId)
		{
			this.SyncedPresets.For(delegate(int i)
			{
				this.SyncedPresets[i] = Guid.Empty;
			});
			this.SnapshotPresetIDs.For(delegate(int i)
			{
				this.SnapshotPresetIDs[i] = Guid.Empty;
			});
			string text = (deviceId == null) ? "" : deviceId;
			this.DeviceID = deviceId;
			this.Device = HidDevice.FromId(text, 3221225472U);
			this.Connected = false;
			if (this.Device != null)
			{
				string text2 = this.Device.Version.ToString("X4");
				this.Version = (int)this.Device.Version;
				Logger.WriteLine(string.Format("Open Device: {0}[{1}({2})] {3}", new object[]
				{
					this.Model,
					this.Version,
					text2,
					deviceId
				}), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXDevice.cs", 2226);
				if (4096 > this.Device.Version)
				{
					this.UpgradeMode = true;
					this.DFUDeviceID = text;
				}
				this.Connected = true;
				if (!HyperXCenter.Center.Updater.UpgradeMode && HyperXCenter.Center.HasNewerBuiltinFirmware(this) && Settings.Instance.ShowNotifications && Settings.Instance.NotifyFirmwareUpdateAvailable)
				{
					Logger.WriteLine("Notify Firmware Update", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXDevice.cs", 2240);
					this.NotifyFirmwareUpdate();
					return;
				}
			}
			else
			{
				Logger.WriteLine(string.Format("Could not open device: {0}", text), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXDevice.cs", 2246);
			}
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x00055D58 File Offset: 0x00053F58
		public virtual void OpenSecondaryDevice(string deviceId)
		{
			if (this.SecondaryDevice != null)
			{
				this.CloseNotificationDevice();
			}
			HidDevice hidDevice = HidDevice.FromId(deviceId, 3221225472U);
			if (hidDevice != null)
			{
				this.SecondaryDevice = hidDevice;
				this.SecondaryDeviceID = deviceId;
				this.SecondaryDevice.InputReportReceived += new TypedEventHandler<HidDevice, byte[]>(this.DeviceInputReportReceived);
				if (!HyperXCenter.Center.Updater.UpgradeMode && HyperXCenter.Center.HasNewerBuiltinFirmware(this) && Settings.Instance.ShowNotifications && Settings.Instance.NotifyFirmwareUpdateAvailable)
				{
					Logger.WriteLine("Notify Firmware Update", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXDevice.cs", 2268);
					this.NotifyFirmwareUpdate();
				}
			}
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x00055DF8 File Offset: 0x00053FF8
		protected void SetSyncAndSnapshotPresetIds(int presetId, Guid snaptshotId)
		{
			Guid guid = Guid.Empty;
			Preset safePreset = this.GetSafePreset();
			if (safePreset != null)
			{
				guid = safePreset.ID;
			}
			if (presetId < 0 || presetId >= this.SyncedPresets.Length)
			{
				return;
			}
			this.SyncedPresets[presetId] = guid;
			this.SnapshotPresetIDs[presetId] = snaptshotId;
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void OpenDongle(string deviceId)
		{
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x00055E48 File Offset: 0x00054048
		public void NotifyBatteryFullyCharged()
		{
			string text = HyperXDeviceUtils.GetDeviceTitle(this);
			text = Settings.Instance.GetDeviceName(this.DeviceID, text);
			NotificationCenter.PopMessage(text, Utils.GetResourceString("BatteryFullyCharged"));
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x00055E80 File Offset: 0x00054080
		public virtual void NotifyFirmwareUpdate()
		{
			string text = HyperXDeviceUtils.GetDeviceTitle(this);
			text = Settings.Instance.GetDeviceName(this.DeviceID, text);
			NotificationCenter.PopMessage(this.Model.ToString(), text, Utils.GetResourceString("FirmwareUpdateAvailable"), false);
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x00055ECB File Offset: 0x000540CB
		public void SetVIDPID(ushort vID, ushort pID)
		{
			this.VendorID = vID;
			this.ProductID = pID;
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		protected virtual void RenderFrameToDevice(List<LightingItem> items)
		{
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x00055EDB File Offset: 0x000540DB
		protected virtual void FlushSyncFramesToDevice(List<List<LightingItem>> frames, int presetId)
		{
			this.Synchronizing = true;
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x00055EE4 File Offset: 0x000540E4
		protected virtual void OnPresetSynced(HXCommandBase cmd, object info)
		{
			this.Synchronizing = false;
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x00055EED File Offset: 0x000540ED
		protected virtual void OnOnboardProfileChanged(int profileId)
		{
			TypedEventHandler<HyperXDevice, int> onboardProfileChanged = this.OnboardProfileChanged;
			if (onboardProfileChanged == null)
			{
				return;
			}
			onboardProfileChanged.Invoke(this, profileId);
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x00055F04 File Offset: 0x00054104
		protected virtual void OnUpdateSyncProgress(HXCommandBase cmd, object info)
		{
			if (this.SyncProgressUpdated != null)
			{
				int num = (int)info;
				this.SyncProgressUpdated.Invoke(this, num);
			}
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x00055F2D File Offset: 0x0005412D
		public virtual void OnContorlByUpdateUI()
		{
			EventHandler<HyperXDevice> controlByUpdateUI = this.ControlByUpdateUI;
			if (controlByUpdateUI == null)
			{
				return;
			}
			controlByUpdateUI(this, this);
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06001FAD RID: 8109 RVA: 0x00055F41 File Offset: 0x00054141
		// (set) Token: 0x06001FAE RID: 8110 RVA: 0x00055F49 File Offset: 0x00054149
		public int MaxSyncFrameCount { get; set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06001FAF RID: 8111 RVA: 0x00055F52 File Offset: 0x00054152
		// (set) Token: 0x06001FB0 RID: 8112 RVA: 0x00055F5A File Offset: 0x0005415A
		public int TotalSyncTime { get; set; }

		// Token: 0x06001FB1 RID: 8113 RVA: 0x00055F64 File Offset: 0x00054164
		public void SyncPreset(int presetId)
		{
			if (presetId >= HyperXDeviceUtils.GetSupportOnBoardProfileCount(this))
			{
				return;
			}
			string deviceTitle = HyperXDeviceUtils.GetDeviceTitle(this);
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("device", deviceTitle);
			dictionary.Add("profile", presetId);
			List<List<LightingItem>> frames = this.GenerateSyncFrames() ?? new List<List<LightingItem>>();
			this.FlushSyncFramesToDevice(frames, presetId);
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x00055FC0 File Offset: 0x000541C0
		public Preset GetSnapshot(int presetId)
		{
			try
			{
				return Preset.LoadSnapshot(this.SnapshotPresetIDs[presetId]);
			}
			catch (Exception)
			{
			}
			return null;
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x00055FF8 File Offset: 0x000541F8
		public List<List<LightingItem>> GenerateSyncFrames()
		{
			Preset preset = null;
			lock (this)
			{
				preset = this.Preset;
			}
			if (preset == null)
			{
				return null;
			}
			List<EffectImplBase> list = new List<EffectImplBase>();
			if (this is HyperXKeyboardDevice)
			{
				List<KeyboardEffect> effects = preset.Keyboard.Effects;
				if (preset.Keyboard.UseBaseEffects)
				{
					effects = Preset.BasePreset.Keyboard.Effects;
				}
				for (int i = 0; i < effects.Count; i++)
				{
					if (effects[i].Visible)
					{
						EffectImplBase effectImplBase = this.CreateEffect(effects[i]);
						if (!(effectImplBase is TriggerEffectImplBase) || effectImplBase is ReactiveEffectImplBase)
						{
							list.Add(effectImplBase);
						}
					}
				}
			}
			if (this is HyperXMouseDevice)
			{
				List<MouseEffect> effects2 = preset.Mouse.Effects;
				if (preset.Mouse.UseBaseEffects)
				{
					effects2 = Preset.BasePreset.Mouse.Effects;
				}
				for (int j = 0; j < effects2.Count; j++)
				{
					if (effects2[j].Visible)
					{
						EffectImplBase effectImplBase = this.CreateEffect(effects2[j]);
						if (!(effectImplBase is TriggerEffectImplBase))
						{
							list.Add(effectImplBase);
						}
					}
				}
			}
			if (this is HyperXMousePadDevice)
			{
				List<MousePadEffect> effects3 = preset.Mousepad.Effects;
				if (preset.Mousepad.UseBaseEffects)
				{
					effects3 = Preset.BasePreset.Mousepad.Effects;
				}
				for (int k = 0; k < effects3.Count; k++)
				{
					if (effects3[k].Visible)
					{
						EffectImplBase effectImplBase = this.CreateEffect(effects3[k]);
						if (!(effectImplBase is TriggerEffectImplBase))
						{
							list.Add(effectImplBase);
						}
					}
				}
			}
			if (this is HyperXHeadsetDevice)
			{
				List<HeadsetEffect> effects4 = preset.Headset.Effects;
				if (preset.Headset.UseBaseEffects)
				{
					effects4 = Preset.BasePreset.Headset.Effects;
				}
				for (int l = 0; l < effects4.Count; l++)
				{
					if (effects4[l].Visible)
					{
						EffectImplBase effectImplBase = this.CreateEffect(effects4[l]);
						if (!(effectImplBase is TriggerEffectImplBase))
						{
							list.Add(effectImplBase);
						}
					}
				}
			}
			if (this is HyperXMicrophoneDevice)
			{
				List<MicrophoneEffect> effects5 = preset.Microphone.Effects;
				for (int m = 0; m < effects5.Count; m++)
				{
					if (effects5[m].Visible)
					{
						EffectImplBase effectImplBase = this.CreateEffect(effects5[m]);
						if (!(effectImplBase is TriggerEffectImplBase))
						{
							list.Add(effectImplBase);
						}
					}
				}
			}
			List<List<LightingItem>> list2;
			if (list.Count > 0)
			{
				list2 = this.Engine.Sync(list, this.FramePerSecond, this.MaxSyncFrameCount);
				this.TotalSyncTime = this.Engine.TotalSyncTime;
			}
			else
			{
				list2 = new List<List<LightingItem>>();
				list2.Add(new List<LightingItem>());
				foreach (KeyMap keyMap in this.Keys)
				{
					list2[0].Add(new LightingItem
					{
						X = keyMap.Row,
						Y = keyMap.Column,
						On = true,
						Color = Color.White(0)
					});
				}
				this.TotalSyncTime = 50;
			}
			return list2;
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void EnterPairMode()
		{
		}

		// Token: 0x06001FB5 RID: 8117 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void CancelPairMode()
		{
		}

		// Token: 0x06001FB6 RID: 8118 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void ResetSirk()
		{
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void FactoryReset()
		{
		}

		// Token: 0x06001FB8 RID: 8120 RVA: 0x00056358 File Offset: 0x00054558
		public virtual void SetupSimulator()
		{
			this.DeviceID = Guid.NewGuid().ToString();
			this.StartEffectEngine();
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x00056384 File Offset: 0x00054584
		protected List<string> CheckDrivers(List<DriverInfo> drivers)
		{
			List<string> list = new List<string>();
			Process process = Process.Start(new ProcessStartInfo("pnputil", "/enum-drivers")
			{
				RedirectStandardOutput = true,
				UseShellExecute = false,
				CreateNoWindow = true,
				WindowStyle = ProcessWindowStyle.Hidden
			});
			string text = process.StandardOutput.ReadToEnd();
			process.WaitForExit();
			process.Dispose();
			string[] array = text.Split(new char[]
			{
				'\n'
			});
			string pattern = "\\d+\\.\\d+\\.\\d+\\.\\d+";
			bool flag = false;
			bool flag2 = false;
			foreach (DriverInfo driverInfo in drivers)
			{
				if (Path.GetFileName(driverInfo.FileName).IndexOf("hyperxuac", StringComparison.InvariantCultureIgnoreCase) > -1)
				{
					flag2 = true;
				}
				bool flag3 = false;
				for (int i = 0; i < array.Length; i++)
				{
					if (!string.IsNullOrEmpty(array[i]) && Utils.IsCultureInvariantMatch(array[i], new string[]
					{
						Path.GetFileName(driverInfo.FileName)
					}))
					{
						Logger.WriteLine("Found installed driver: " + array[i], "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXDevice.cs", 2636);
						flag3 = true;
						bool flag4 = false;
						if (array[i].IndexOf("dtsapo4xhpxv2x64", StringComparison.InvariantCultureIgnoreCase) > -1)
						{
							for (int j = i + 1; j < Math.Min(i + 4, array.Length); j++)
							{
								if (array[j].IndexOf("HyperX", StringComparison.InvariantCultureIgnoreCase) > -1)
								{
									flag4 = true;
									break;
								}
							}
							if (!flag4)
							{
								flag = true;
							}
							flag4 = false;
						}
						for (int k = i + 4; k < array.Length; k++)
						{
							if (string.IsNullOrEmpty(array[k].Trim()))
							{
								i = k;
								break;
							}
							Match match = Regex.Match(array[k], pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
							if (match.Success)
							{
								Version version = new Version(match.Value);
								Logger.WriteLine("Version of found driver: " + match.Value, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXDevice.cs", 2670);
								if (version.CompareTo(driverInfo.Version) >= 0)
								{
									flag4 = true;
									i = k;
									break;
								}
								if (Path.GetFileName(driverInfo.FileName).IndexOf("hyperxuac", StringComparison.InvariantCultureIgnoreCase) > -1)
								{
									flag = true;
									i = k;
								}
							}
						}
						if ((!flag4 || flag) && !list.Contains(driverInfo.FileName))
						{
							list.Add(driverInfo.FileName);
						}
						if (flag4)
						{
							if (list.Contains(driverInfo.FileName))
							{
								list.Remove(driverInfo.FileName);
								break;
							}
							break;
						}
					}
				}
				if (!flag3 && !list.Contains(driverInfo.FileName))
				{
					list.Add(driverInfo.FileName);
				}
			}
			if (list.Count > 0 && flag2)
			{
				list.Clear();
				foreach (DriverInfo driverInfo2 in drivers)
				{
					list.Add(driverInfo2.FileName);
				}
			}
			return list;
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x0000FBE7 File Offset: 0x0000DDE7
		public virtual List<string> CheckDrivers()
		{
			return null;
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x000566B4 File Offset: 0x000548B4
		protected void AddDriverInfo(List<DriverInfo> info, string infPath)
		{
			string input = File.ReadAllText(infPath);
			string pattern = "DriverVer\\s*=\\s*[\\d/\\-]+\\s*,\\s*(?<Version>\\d+\\.\\d+\\.\\d+\\.\\d+)";
			Match match = Regex.Match(input, pattern);
			if (match.Success)
			{
				info.Add(new DriverInfo
				{
					FileName = infPath,
					Version = new Version(match.Groups["Version"].Value)
				});
			}
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x0005670E File Offset: 0x0005490E
		internal void StartCommandsQueueEngine()
		{
			if (this._queueEngine == null)
			{
				this._queueEngine = new CommandsEngine();
				this._queueEngine.Start(this);
			}
		}

		// Token: 0x06001FBD RID: 8125 RVA: 0x00056730 File Offset: 0x00054930
		internal Task SetCommandsAck(byte[] ackBuffers)
		{
			HyperXDevice.<SetCommandsAck>d__572 <SetCommandsAck>d__;
			<SetCommandsAck>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<SetCommandsAck>d__.<>4__this = this;
			<SetCommandsAck>d__.ackBuffers = ackBuffers;
			<SetCommandsAck>d__.<>1__state = -1;
			<SetCommandsAck>d__.<>t__builder.Start<HyperXDevice.<SetCommandsAck>d__572>(ref <SetCommandsAck>d__);
			return <SetCommandsAck>d__.<>t__builder.Task;
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x0005677C File Offset: 0x0005497C
		protected void TryApplyBasicSettings(int maxRetry)
		{
			int num = 0;
			while (this.GetSafePreset() == null)
			{
				SpinWait.SpinUntil(() => false, 500);
				num++;
			}
			if (num == maxRetry)
			{
				return;
			}
			this.ApplyBasicSettings();
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x00015297 File Offset: 0x00013497
		public virtual SubReportIdDeviceType GetSubReportIdDeviceType()
		{
			return SubReportIdDeviceType.MainDevice;
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x000567D0 File Offset: 0x000549D0
		public void SendDiscordCertificateDeviceNotificationAsync(string name = null)
		{
			HyperXDevice.<SendDiscordCertificateDeviceNotificationAsync>d__575 <SendDiscordCertificateDeviceNotificationAsync>d__;
			<SendDiscordCertificateDeviceNotificationAsync>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
			<SendDiscordCertificateDeviceNotificationAsync>d__.<>4__this = this;
			<SendDiscordCertificateDeviceNotificationAsync>d__.name = name;
			<SendDiscordCertificateDeviceNotificationAsync>d__.<>1__state = -1;
			<SendDiscordCertificateDeviceNotificationAsync>d__.<>t__builder.Start<HyperXDevice.<SendDiscordCertificateDeviceNotificationAsync>d__575>(ref <SendDiscordCertificateDeviceNotificationAsync>d__);
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x0005680F File Offset: 0x00054A0F
		private void Device_SidetoneChanged(MMDevice device, float volume, bool muted, bool triggerBySystem)
		{
			this.OnAudioDeviceChanged(AudioDeviceType.Sidetone, volume, muted, triggerBySystem);
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x0005681C File Offset: 0x00054A1C
		private void Device_AudioVolumeChanged(MMDevice device, float volume, bool muted, bool triggerBySystem)
		{
			if (device.DataFlow == AudioDeviceDataFlow.Render)
			{
				this.OnAudioDeviceChanged(AudioDeviceType.Sound, volume, muted, triggerBySystem);
				return;
			}
			this.OnAudioDeviceChanged(AudioDeviceType.Microphone, volume, muted, triggerBySystem);
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06001FC3 RID: 8131 RVA: 0x0005683D File Offset: 0x00054A3D
		public ushort AudioDeviceVendorId
		{
			get
			{
				return this.VendorID;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06001FC4 RID: 8132 RVA: 0x00056848 File Offset: 0x00054A48
		public List<ushort> AudioDeviceProductIds
		{
			get
			{
				if (this.ProductID == 5919)
				{
					return new List<ushort>
					{
						5917
					};
				}
				if (this.ProductID == 1164 || this.ProductID == 3979)
				{
					return new List<ushort>
					{
						3467
					};
				}
				if (this.ProductID == 652 || this.ProductID == 1676)
				{
					return new List<ushort>
					{
						660
					};
				}
				if (this.ProductID == 2444)
				{
					return new List<ushort>
					{
						2700
					};
				}
				if (this.ProductID == 2479)
				{
					return new List<ushort>
					{
						1967,
						1972
					};
				}
				if (this.ProductID == 693)
				{
					return new List<ushort>
					{
						3460
					};
				}
				return new List<ushort>
				{
					this.ProductID
				};
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06001FC5 RID: 8133 RVA: 0x0005693F File Offset: 0x00054B3F
		public IReadOnlyList<MMDevice> AudioDevices
		{
			get
			{
				return this._audioDevices;
			}
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x00056948 File Offset: 0x00054B48
		public virtual void AddAudioDevice(MMDevice device)
		{
			List<MMDevice> audioDevices = this._audioDevices;
			lock (audioDevices)
			{
				if (this._audioDevices.Any((MMDevice o) => o.Id == device.Id))
				{
					return;
				}
				this._audioDevices.Add(device);
			}
			if (this.AudioDeviceConnected != null)
			{
				this.AudioDeviceConnected.Invoke(device, this);
			}
			Logger.WriteLine(string.Format("{0}: {1}{2}", this.Model, device.DataFlow, device.Id), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXDevice.Audio.cs", 144);
			device.AudioVolumeChanged += this.Device_AudioVolumeChanged;
			device.SidetoneChanged += this.Device_SidetoneChanged;
			if (device.DataFlow == AudioDeviceDataFlow.Render)
			{
				this.Device_AudioVolumeChanged(device, device.Volume, device.Muted, true);
				this.SoundVolume = device.Volume;
				this.SoundMuted = device.Muted;
			}
			else
			{
				this.Device_AudioVolumeChanged(device, device.Volume, device.Muted, true);
				this.MicrophoneVolume = device.Volume;
				if (this.Model != HyperXDeviceModel.Headset_CloudIIWireless)
				{
					this.MicrophoneMuted = device.Muted;
				}
			}
			if (device.SidetoneEnabled)
			{
				this.Device_SidetoneChanged(device, device.SidetoneVolume, device.SidetoneMuted, true);
				this.SidetoneVolume = device.SidetoneVolume;
				if (this.Model != HyperXDeviceModel.Headset_CloudIIWireless)
				{
					this.SidetoneMuted = device.SidetoneMuted;
				}
			}
			bool flag2 = this._audioDevices.Any((MMDevice o) => o.DataFlow == AudioDeviceDataFlow.Render);
			bool flag3 = this._audioDevices.Any((MMDevice o) => o.DataFlow == AudioDeviceDataFlow.Capture);
			if (this.SpeakerAvailable != flag2)
			{
				this.SpeakerAvailable = flag2;
				this.OnAudioDeviceAvailabilityChanged(AudioDeviceType.Sound, this.SpeakerAvailable);
			}
			if (this.MicrophoneAvailable != flag3)
			{
				this.MicrophoneAvailable = flag3;
				this.OnAudioDeviceAvailabilityChanged(AudioDeviceType.Microphone, this.MicrophoneAvailable);
			}
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x00056BD8 File Offset: 0x00054DD8
		public virtual void RemoveAudioDevice(MMDevice device)
		{
			Logger.WriteLine(string.Format("{0}: {1}", this.Model, device.Id), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXDevice.Audio.cs", 194);
			this.Device_AudioVolumeChanged(device, 0f, device.Muted, false);
			device.AudioVolumeChanged -= this.Device_AudioVolumeChanged;
			device.SidetoneChanged -= this.Device_SidetoneChanged;
			this._audioDevices.Remove(device);
			bool flag = this._audioDevices.Any((MMDevice o) => o.DataFlow == AudioDeviceDataFlow.Render);
			bool flag2 = this._audioDevices.Any((MMDevice o) => o.DataFlow == AudioDeviceDataFlow.Capture);
			if (this.SpeakerAvailable != flag)
			{
				this.SpeakerAvailable = flag;
				this.OnAudioDeviceAvailabilityChanged(AudioDeviceType.Sound, this.SpeakerAvailable);
			}
			if (this.MicrophoneAvailable != flag2)
			{
				this.MicrophoneAvailable = flag2;
				this.OnAudioDeviceAvailabilityChanged(AudioDeviceType.Microphone, this.SpeakerAvailable);
			}
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x00056CE4 File Offset: 0x00054EE4
		public virtual void RemoveAudioDevice(string audioEndpointId)
		{
			List<MMDevice> audioDevices = this._audioDevices;
			lock (audioDevices)
			{
				this._audioDevices.RemoveAll((MMDevice o) => o.Id == audioEndpointId);
			}
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x00056D44 File Offset: 0x00054F44
		public void TestMicrophone()
		{
			MMDevice mmdevice = null;
			List<MMDevice> audioDevices = this._audioDevices;
			lock (audioDevices)
			{
				mmdevice = this._audioDevices.FirstOrDefault((MMDevice o) => o.DataFlow == AudioDeviceDataFlow.Capture);
			}
			if (mmdevice != null)
			{
				mmdevice.Capture();
			}
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x00056DB8 File Offset: 0x00054FB8
		public void TestSpeaker()
		{
			MMDevice mmdevice = null;
			MMDevice mmdevice2 = null;
			List<MMDevice> audioDevices = this._audioDevices;
			lock (audioDevices)
			{
				mmdevice = this._audioDevices.FirstOrDefault((MMDevice o) => o.DataFlow == AudioDeviceDataFlow.Capture);
				mmdevice2 = this._audioDevices.FirstOrDefault((MMDevice o) => o.DataFlow == AudioDeviceDataFlow.Render);
			}
			if (mmdevice == null)
			{
				return;
			}
			if (mmdevice2 == null || mmdevice2.IsRendering)
			{
				return;
			}
			if (mmdevice.IsCapturing)
			{
				mmdevice.Stop();
			}
			byte[] audioBuffer = mmdevice.AudioBuffer;
			mmdevice.AudioBuffer = null;
			if (audioBuffer == null)
			{
				return;
			}
			mmdevice2.AudioBuffer = audioBuffer;
			mmdevice2.WaveFormat = mmdevice.WaveFormat;
			mmdevice2.TestProgressUpdated += this.Speaker_TestProgressUpdated;
			mmdevice2.Play();
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x00056EAC File Offset: 0x000550AC
		private void Speaker_TestProgressUpdated(object sender, float progress)
		{
			TypedEventHandler<HyperXDevice, float> audioDeviceTestProgressUpdated = this.AudioDeviceTestProgressUpdated;
			if (audioDeviceTestProgressUpdated == null)
			{
				return;
			}
			audioDeviceTestProgressUpdated.Invoke(this, progress);
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x00056EC0 File Offset: 0x000550C0
		public void StopAudioDevices()
		{
			List<MMDevice> list = new List<MMDevice>();
			List<MMDevice> audioDevices = this._audioDevices;
			lock (audioDevices)
			{
				list.AddRange(this._audioDevices);
			}
			foreach (MMDevice mmdevice in list)
			{
				if (mmdevice.DataFlow == AudioDeviceDataFlow.Render)
				{
					mmdevice.TestProgressUpdated -= this.Speaker_TestProgressUpdated;
				}
				mmdevice.Stop();
			}
		}

		// Token: 0x06001FCD RID: 8141 RVA: 0x00056F68 File Offset: 0x00055168
		public void StartAudioMeter(bool forceStart = false)
		{
			MMDevice mmdevice = this.AudioDevices.FirstOrDefault((MMDevice o) => o.DataFlow == AudioDeviceDataFlow.Capture);
			if (mmdevice == null)
			{
				return;
			}
			mmdevice.StartAudioMeter(false);
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x00056F9F File Offset: 0x0005519F
		public void StopAudioMeter()
		{
			MMDevice mmdevice = this.AudioDevices.FirstOrDefault((MMDevice o) => o.DataFlow == AudioDeviceDataFlow.Capture);
			if (mmdevice == null)
			{
				return;
			}
			mmdevice.StopAudioMeter(false);
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06001FCF RID: 8143 RVA: 0x00056FD6 File Offset: 0x000551D6
		// (set) Token: 0x06001FD0 RID: 8144 RVA: 0x00056FDE File Offset: 0x000551DE
		public Guid[] SnapshotPresetIDs { get; private set; }

		// Token: 0x04001D33 RID: 7475
		private const int SERIALIZER_TAILER = 321405651;

		// Token: 0x04001D3C RID: 7484
		private bool _connected;

		// Token: 0x04001D4F RID: 7503
		private ChargingStatus _chargingStatus;

		// Token: 0x04001D50 RID: 7504
		private int _chargingCounter;

		// Token: 0x04001D51 RID: 7505
		private int _battery = -1;

		// Token: 0x04001D52 RID: 7506
		private bool _sleeping;

		// Token: 0x04001D58 RID: 7512
		private bool _functionLockEnabled;

		// Token: 0x04001D62 RID: 7522
		private bool _linked;

		// Token: 0x04001D69 RID: 7529
		private int _version;

		// Token: 0x04001D84 RID: 7556
		private bool _pairing;

		// Token: 0x04001D85 RID: 7557
		private long _lastDeviceFrameTimestamp;

		// Token: 0x04001D86 RID: 7558
		private long _deviceframeInterval;

		// Token: 0x04001D87 RID: 7559
		private long _lastUIFrameTimestamp;

		// Token: 0x04001D88 RID: 7560
		private long _uiframeInterval;

		// Token: 0x04001D89 RID: 7561
		private int _deviceFramePerSecond;

		// Token: 0x04001D8A RID: 7562
		private int _uiFramePerSecond;

		// Token: 0x04001D8B RID: 7563
		private List<int> _calibratedBatteries = new List<int>();

		// Token: 0x04001D99 RID: 7577
		protected EffectEngine _effectEngine;

		// Token: 0x04001D9E RID: 7582
		private bool _deviceInited;

		// Token: 0x04001DA2 RID: 7586
		private bool fullBatteryChanged;

		// Token: 0x04001DA4 RID: 7588
		private bool _running;

		// Token: 0x04001DA5 RID: 7589
		private bool _stopped;

		// Token: 0x04001DA6 RID: 7590
		protected bool _stopping;

		// Token: 0x04001DA7 RID: 7591
		private DateTime _stopTime;

		// Token: 0x04001DAB RID: 7595
		private Semaphore _sem;

		// Token: 0x04001DAC RID: 7596
		private const int APPLICATION_VERSION_START = 4096;

		// Token: 0x04001DAD RID: 7597
		private const int DEVICE_ENUMERATE_TIME = 500;

		// Token: 0x04001DB0 RID: 7600
		protected CommandsEngine _queueEngine;

		// Token: 0x04001DB1 RID: 7601
		private List<MMDevice> _audioDevices = new List<MMDevice>();

		// Token: 0x02000B49 RID: 2889
		public enum ControlBySoftware
		{
			// Token: 0x04002F8A RID: 12170
			NGENUITY,
			// Token: 0x04002F8B RID: 12171
			OLS
		}
	}
}```


# CMDHSCloudAlphaWirelessBase
```csharp
using System;

namespace NGenuity2.Devices.Headset.CloudAlphaWireless
{
	// Token: 0x02000876 RID: 2166
	public abstract class CMDHSCloudAlphaWirelessBase : HXCommandBase
	{
		// Token: 0x0600306E RID: 12398 RVA: 0x000C5C1C File Offset: 0x000C3E1C
		public bool IsDongle(HyperXDevice device)
		{
			return device.ProductID == 5955 || device.ProductID == 5989 || device.ProductID == 2445;
		}
	}
}```

# CMDHSCloudAlphaWirelessAPO
```csharp
using System;
using NGenuity2.Devices.Headset.DTS;

namespace NGenuity2.Devices.Headset.CloudAlphaWireless
{
	// Token: 0x02000875 RID: 2165
	public class CMDHSCloudAlphaWirelessAPO : CMDHSCloudAlphaWirelessBase
	{
		// Token: 0x06003068 RID: 12392 RVA: 0x000C5BC4 File Offset: 0x000C3DC4
		public CMDHSCloudAlphaWirelessAPO(HXCommandHandler handler)
		{
			base.Force = true;
			base.Handler = handler;
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06003069 RID: 12393 RVA: 0x000C5BDA File Offset: 0x000C3DDA
		// (set) Token: 0x0600306A RID: 12394 RVA: 0x000C5BE2 File Offset: 0x000C3DE2
		public DTSAPOCommand Action { get; set; }

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x0600306B RID: 12395 RVA: 0x000C5BEB File Offset: 0x000C3DEB
		// (set) Token: 0x0600306C RID: 12396 RVA: 0x000C5BF3 File Offset: 0x000C3DF3
		public object Info { get; set; }

		// Token: 0x0600306D RID: 12397 RVA: 0x000C5BFC File Offset: 0x000C3DFC
		public override void Execute(HyperXDevice device)
		{
			base.Execute(device);
			HXCommandHandler handler = base.Handler;
			if (handler == null)
			{
				return;
			}
			handler(this, this.Info);
		}
	}
}```

# CMDHSCloudAlphaWirelessGetAutoShutdown
```csharp
using System;

namespace NGenuity2.Devices.Headset.CloudAlphaWireless
{
	// Token: 0x02000877 RID: 2167
	public class CMDHSCloudAlphaWirelessGetAutoShutdown : CMDHSCloudAlphaWirelessBase
	{
		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06003070 RID: 12400 RVA: 0x000C5C47 File Offset: 0x000C3E47
		// (set) Token: 0x06003071 RID: 12401 RVA: 0x000C5C4F File Offset: 0x000C3E4F
		public byte? Duration { get; set; }

		// Token: 0x06003072 RID: 12402 RVA: 0x000C5C58 File Offset: 0x000C3E58
		public override void Execute(HyperXDevice device)
		{
			base.Execute(device);
			byte[] array = device.CreateBuffer();
			array[0] = 33;
			array[1] = 187;
			array[2] = 7;
			device.SetOutputReport(array);
			byte[] inputReport = device.GetInputReport();
			if (inputReport != null && inputReport[2] == array[2])
			{
				this.Duration = new byte?(inputReport[3]);
			}
		}
	}
}```

# CMDHSCloudAlphaWirelessGetBatteryInfo
```csharp
using System;

namespace NGenuity2.Devices.Headset.CloudAlphaWireless
{
	// Token: 0x02000878 RID: 2168
	public class CMDHSCloudAlphaWirelessGetBatteryInfo : CMDHSCloudAlphaWirelessBase
	{
		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06003074 RID: 12404 RVA: 0x000C5CB3 File Offset: 0x000C3EB3
		// (set) Token: 0x06003075 RID: 12405 RVA: 0x000C5CBB File Offset: 0x000C3EBB
		public int? Battery { get; set; }

		// Token: 0x06003076 RID: 12406 RVA: 0x000C5CC4 File Offset: 0x000C3EC4
		public override void Execute(HyperXDevice device)
		{
			base.Execute(device);
			byte[] array = device.CreateBuffer();
			array[0] = 33;
			array[1] = 187;
			array[2] = 11;
			device.SetOutputReport(array);
			byte[] inputReport = device.GetInputReport();
			if (inputReport != null && inputReport[2] == array[2])
			{
				this.Battery = new int?((int)inputReport[3]);
				HXCommandHandler handler = base.Handler;
				if (handler == null)
				{
					return;
				}
				handler(this, this.Battery);
			}
		}
	}
}```

# CMDHSCloudAlphaWirelessGetChargeStatus
```csharp
using System;

namespace NGenuity2.Devices.Headset.CloudAlphaWireless
{
	// Token: 0x02000879 RID: 2169
	public class CMDHSCloudAlphaWirelessGetChargeStatus : CMDHSCloudAlphaWirelessBase
	{
		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06003078 RID: 12408 RVA: 0x000C5D34 File Offset: 0x000C3F34
		// (set) Token: 0x06003079 RID: 12409 RVA: 0x000C5D3C File Offset: 0x000C3F3C
		public ChargingStatus? Status { get; set; }

		// Token: 0x0600307A RID: 12410 RVA: 0x000C5D48 File Offset: 0x000C3F48
		public override void Execute(HyperXDevice device)
		{
			base.Execute(device);
			byte[] array = device.CreateBuffer();
			array[0] = 33;
			array[1] = 187;
			array[2] = 12;
			device.SetOutputReport(array);
			byte[] inputReport = device.GetInputReport();
			if (inputReport != null && inputReport[2] == array[2])
			{
				byte b = inputReport[3];
				if (b != 0)
				{
					if (b == 1)
					{
						this.Status = new ChargingStatus?(ChargingStatus.WireCharging);
					}
				}
				else
				{
					this.Status = new ChargingStatus?(ChargingStatus.NoCharging);
				}
				HXCommandHandler handler = base.Handler;
				if (handler == null)
				{
					return;
				}
				handler(this, this.Status);
			}
		}
	}
}```

# CMDHSCloudAlphaWirelessGetMicBoomStatus
```csharp
using System;

namespace NGenuity2.Devices.Headset.CloudAlphaWireless
{
	// Token: 0x0200087A RID: 2170
	public class CMDHSCloudAlphaWirelessGetMicBoomStatus : CMDHSCloudAlphaWirelessBase
	{
		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x0600307C RID: 12412 RVA: 0x000C5DD1 File Offset: 0x000C3FD1
		// (set) Token: 0x0600307D RID: 12413 RVA: 0x000C5DD9 File Offset: 0x000C3FD9
		public bool Plugged { get; set; }

		// Token: 0x0600307E RID: 12414 RVA: 0x000C5DE4 File Offset: 0x000C3FE4
		public override void Execute(HyperXDevice device)
		{
			base.Execute(device);
			byte[] array = device.CreateBuffer();
			array[0] = 33;
			array[1] = 187;
			array[2] = 8;
			device.SetOutputReport(array);
			byte[] inputReport = device.GetInputReport();
			if (inputReport != null && inputReport[2] == array[2])
			{
				this.Plugged = (inputReport[3] == 1);
			}
		}
	}
}```

# CMDHSCloudAlphaWirelessGetMicMute
```csharp
using System;

namespace NGenuity2.Devices.Headset.CloudAlphaWireless
{
	// Token: 0x0200087B RID: 2171
	public class CMDHSCloudAlphaWirelessGetMicMute : CMDHSCloudAlphaWirelessBase
	{
		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06003080 RID: 12416 RVA: 0x000C5E35 File Offset: 0x000C4035
		// (set) Token: 0x06003081 RID: 12417 RVA: 0x000C5E3D File Offset: 0x000C403D
		public bool? Muted { get; set; }

		// Token: 0x06003082 RID: 12418 RVA: 0x000C5E48 File Offset: 0x000C4048
		public override void Execute(HyperXDevice device)
		{
			base.Execute(device);
			byte[] array = device.CreateBuffer();
			array[0] = 33;
			array[1] = 187;
			array[2] = 10;
			device.SetOutputReport(array);
			byte[] inputReport = device.GetInputReport();
			if (inputReport != null && inputReport[2] == array[2])
			{
				this.Muted = new bool?(inputReport[3] == 1);
				HXCommandHandler handler = base.Handler;
				if (handler == null)
				{
					return;
				}
				handler(this, this.Muted);
			}
		}
	}
}```

# CMDHSCloudAlphaWirelessGetPairingInfo
```csharp
using System;
using NGenuity2.Common;

namespace NGenuity2.Devices.Headset.CloudAlphaWireless
{
	// Token: 0x0200087C RID: 2172
	public class CMDHSCloudAlphaWirelessGetPairingInfo : CMDHSCloudAlphaWirelessBase
	{
		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x06003084 RID: 12420 RVA: 0x000C5EBB File Offset: 0x000C40BB
		// (set) Token: 0x06003085 RID: 12421 RVA: 0x000C5EC3 File Offset: 0x000C40C3
		public int PairID { get; set; }

		// Token: 0x06003086 RID: 12422 RVA: 0x000C5ECC File Offset: 0x000C40CC
		public override void Execute(HyperXDevice device)
		{
			base.Execute(device);
			byte[] array = device.CreateBuffer();
			array[0] = 33;
			array[1] = 187;
			array[2] = 4;
			if (base.IsDongle(device))
			{
				array[2] = 13;
			}
			device.SetOutputReport(array);
			byte[] inputReport = device.GetInputReport();
			if (inputReport != null && inputReport[2] == array[2])
			{
				this.PairID = Utils.BytesToInt(inputReport, 5);
			}
		}
	}
}```

# CMDHSCloudAlphaWirelessGetProductColor
```csharp
using System;

namespace NGenuity2.Devices.Headset.CloudAlphaWireless
{
	// Token: 0x0200087D RID: 2173
	public class CMDHSCloudAlphaWirelessGetProductColor : CMDHSCloudAlphaWirelessBase
	{
		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x06003088 RID: 12424 RVA: 0x000C5F2C File Offset: 0x000C412C
		// (set) Token: 0x06003089 RID: 12425 RVA: 0x000C5F34 File Offset: 0x000C4134
		public byte Color { get; set; }

		// Token: 0x0600308A RID: 12426 RVA: 0x000C5F40 File Offset: 0x000C4140
		public override void Execute(HyperXDevice device)
		{
			base.Execute(device);
			byte[] array = device.CreateBuffer();
			array[0] = 33;
			array[1] = 187;
			array[2] = 14;
			device.SetOutputReport(array);
			byte[] inputReport = device.GetInputReport();
			if (inputReport != null && inputReport[2] == array[2])
			{
				this.Color = inputReport[3];
			}
		}
	}
}```

# CMDHSCloudAlphaWirelessGetSidetoneStatus
```csharp
using System;

namespace NGenuity2.Devices.Headset.CloudAlphaWireless
{
	// Token: 0x0200087E RID: 2174
	public class CMDHSCloudAlphaWirelessGetSidetoneStatus : CMDHSCloudAlphaWirelessBase
	{
		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x0600308C RID: 12428 RVA: 0x000C5F8F File Offset: 0x000C418F
		// (set) Token: 0x0600308D RID: 12429 RVA: 0x000C5F97 File Offset: 0x000C4197
		public bool? On { get; set; }

		// Token: 0x0600308E RID: 12430 RVA: 0x000C5FA0 File Offset: 0x000C41A0
		public override void Execute(HyperXDevice device)
		{
			base.Execute(device);
			byte[] array = device.CreateBuffer();
			array[0] = 33;
			array[1] = 187;
			array[2] = 5;
			device.SetOutputReport(array);
			byte[] inputReport = device.GetInputReport();
			if (inputReport != null && inputReport[2] == array[2])
			{
				this.On = new bool?(inputReport[3] == 1);
				HXCommandHandler handler = base.Handler;
				if (handler == null)
				{
					return;
				}
				handler(this, this.On);
			}
		}
	}
}```

# CMDHSCloudAlphaWirelessGetSidetoneVolume
```csharp
using System;

namespace NGenuity2.Devices.Headset.CloudAlphaWireless
{
	// Token: 0x0200087F RID: 2175
	public class CMDHSCloudAlphaWirelessGetSidetoneVolume : CMDHSCloudAlphaWirelessBase
	{
		// Token: 0x06003090 RID: 12432 RVA: 0x000C6014 File Offset: 0x000C4214
		public override void Execute(HyperXDevice device)
		{
			base.Execute(device);
			byte[] array = device.CreateBuffer();
			array[0] = 33;
			array[1] = 187;
			array[2] = 6;
			device.SetOutputReport(array);
			byte[] inputReport = device.GetInputReport();
			if (inputReport != null)
			{
				byte b = inputReport[2];
				byte b2 = array[2];
			}
		}
	}
}```

# CMDHSCloudAlphaWirelessGetVoicePromptStatus
```csharp
using System;

namespace NGenuity2.Devices.Headset.CloudAlphaWireless
{
	// Token: 0x02000880 RID: 2176
	public class CMDHSCloudAlphaWirelessGetVoicePromptStatus : CMDHSCloudAlphaWirelessBase
	{
		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06003092 RID: 12434 RVA: 0x000C6059 File Offset: 0x000C4259
		// (set) Token: 0x06003093 RID: 12435 RVA: 0x000C6061 File Offset: 0x000C4261
		public bool? On { get; set; }

		// Token: 0x06003094 RID: 12436 RVA: 0x000C606C File Offset: 0x000C426C
		public override void Execute(HyperXDevice device)
		{
			base.Execute(device);
			byte[] array = device.CreateBuffer();
			array[0] = 33;
			array[1] = 187;
			array[2] = 9;
			device.SetOutputReport(array);
			byte[] inputReport = device.GetInputReport();
			if (inputReport != null && inputReport[2] == array[2])
			{
				this.On = new bool?(inputReport[3] == 1);
				HXCommandHandler handler = base.Handler;
				if (handler == null)
				{
					return;
				}
				handler(this, this.On);
			}
		}
	}
}```

# CMDHSCloudAlphaWirelessGetWirelessState
```csharp
using System;

namespace NGenuity2.Devices.Headset.CloudAlphaWireless
{
	// Token: 0x02000881 RID: 2177
	public class CMDHSCloudAlphaWirelessGetWirelessState : CMDHSCloudAlphaWirelessBase
	{
		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06003096 RID: 12438 RVA: 0x000C60DF File Offset: 0x000C42DF
		// (set) Token: 0x06003097 RID: 12439 RVA: 0x000C60E7 File Offset: 0x000C42E7
		public bool Connected { get; private set; }

		// Token: 0x06003098 RID: 12440 RVA: 0x000C60F0 File Offset: 0x000C42F0
		public override void Execute(HyperXDevice device)
		{
			base.Execute(device);
			if (!base.IsDongle(device))
			{
				this.Connected = true;
				return;
			}
			byte[] array = device.CreateBuffer();
			array[0] = 33;
			array[1] = 187;
			array[2] = 3;
			device.SetOutputReport(array);
			byte[] inputReport = device.GetInputReport();
			if (inputReport != null && inputReport[2] == array[2])
			{
				this.Connected = (inputReport[3] == 2);
			}
		}
	}
}```

# CMDHSCloudAlphaWirelessSetAutoShutdown
```csharp
using System;

namespace NGenuity2.Devices.Headset.CloudAlphaWireless
{
	// Token: 0x02000882 RID: 2178
	public class CMDHSCloudAlphaWirelessSetAutoShutdown : CMDHSCloudAlphaWirelessBase
	{
		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x0600309A RID: 12442 RVA: 0x000C6152 File Offset: 0x000C4352
		// (set) Token: 0x0600309B RID: 12443 RVA: 0x000C615A File Offset: 0x000C435A
		public byte Duration { get; set; }

		// Token: 0x0600309C RID: 12444 RVA: 0x000C6164 File Offset: 0x000C4364
		public override void Execute(HyperXDevice device)
		{
			base.Execute(device);
			byte[] array = device.CreateBuffer();
			array[0] = 33;
			array[1] = 187;
			array[2] = 18;
			array[3] = this.Duration;
			device.SetOutputReport(array);
		}
	}
}```

# CMDHSCloudAlphaWirelessSetMicMute
```csharp
using System;

namespace NGenuity2.Devices.Headset.CloudAlphaWireless
{
	// Token: 0x02000883 RID: 2179
	public class CMDHSCloudAlphaWirelessSetMicMute : CMDHSCloudAlphaWirelessBase
	{
		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x0600309E RID: 12446 RVA: 0x000C61A1 File Offset: 0x000C43A1
		// (set) Token: 0x0600309F RID: 12447 RVA: 0x000C61A9 File Offset: 0x000C43A9
		public bool Muted { get; set; }

		// Token: 0x060030A0 RID: 12448 RVA: 0x000C61B4 File Offset: 0x000C43B4
		public override void Execute(HyperXDevice device)
		{
			base.Execute(device);
			byte[] array = device.CreateBuffer();
			array[0] = 33;
			array[1] = 187;
			array[2] = 21;
			if (this.Muted)
			{
				array[3] = 1;
			}
			else
			{
				array[3] = 0;
			}
			device.SetOutputReport(array);
		}
	}
}```

# CMDHSCloudAlphaWirelessSetSidetoneStatus
```csharp
using System;

namespace NGenuity2.Devices.Headset.CloudAlphaWireless
{
	// Token: 0x02000884 RID: 2180
	public class CMDHSCloudAlphaWirelessSetSidetoneStatus : CMDHSCloudAlphaWirelessBase
	{
		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x060030A2 RID: 12450 RVA: 0x000C61FA File Offset: 0x000C43FA
		// (set) Token: 0x060030A3 RID: 12451 RVA: 0x000C6202 File Offset: 0x000C4402
		public bool On { get; set; }

		// Token: 0x060030A4 RID: 12452 RVA: 0x000C620C File Offset: 0x000C440C
		public override void Execute(HyperXDevice device)
		{
			base.Execute(device);
			byte[] array = device.CreateBuffer();
			array[0] = 33;
			array[1] = 187;
			array[2] = 16;
			if (this.On)
			{
				array[3] = 1;
			}
			else
			{
				array[3] = 0;
			}
			device.SetOutputReport(array);
		}
	}
}```

# CMDHSCloudAlphaWirelessSetSidetoneVolume
```csharp
using System;

namespace NGenuity2.Devices.Headset.CloudAlphaWireless
{
	// Token: 0x02000885 RID: 2181
	public class CMDHSCloudAlphaWirelessSetSidetoneVolume : CMDHSCloudAlphaWirelessBase
	{
		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x060030A6 RID: 12454 RVA: 0x000C6252 File Offset: 0x000C4452
		// (set) Token: 0x060030A7 RID: 12455 RVA: 0x000C625A File Offset: 0x000C445A
		public int Volume { get; set; }

		// Token: 0x060030A8 RID: 12456 RVA: 0x000C6264 File Offset: 0x000C4464
		public override void Execute(HyperXDevice device)
		{
			base.Execute(device);
			byte[] array = device.CreateBuffer();
			array[0] = 33;
			array[1] = 187;
			array[2] = 17;
			device.SetOutputReport(array);
		}
	}
}```

# CMDHSCloudAlphaWirelessSetVoicePromptStatus
```csharp
using System;

namespace NGenuity2.Devices.Headset.CloudAlphaWireless
{
	// Token: 0x02000886 RID: 2182
	public class CMDHSCloudAlphaWirelessSetVoicePromptStatus : CMDHSCloudAlphaWirelessBase
	{
		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x060030AA RID: 12458 RVA: 0x000C6298 File Offset: 0x000C4498
		// (set) Token: 0x060030AB RID: 12459 RVA: 0x000C62A0 File Offset: 0x000C44A0
		public bool On { get; set; }

		// Token: 0x060030AC RID: 12460 RVA: 0x000C62AC File Offset: 0x000C44AC
		public override void Execute(HyperXDevice device)
		{
			base.Execute(device);
			byte[] array = device.CreateBuffer();
			array[0] = 33;
			array[1] = 187;
			array[2] = 19;
			if (this.On)
			{
				array[3] = 1;
			}
			else
			{
				array[3] = 0;
			}
			device.SetOutputReport(array);
		}
	}
}```

# CMDHSCloudAlphaWirelessShowLighting
```csharp
using System;

namespace NGenuity2.Devices.Headset.CloudAlphaWireless
{
	// Token: 0x02000887 RID: 2183
	public class CMDHSCloudAlphaWirelessShowLighting : CMDHSCloudAlphaWirelessBase
	{
	}
}```

# HeadsetCloudAlphaWireless
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NGenuity2.Common;
using NGenuity2.Devices.Headset.CloudAlphaWireless;
using NGenuity2.Devices.Headset.DTS;
using NGenuity2.Model;
using NGenuity2.Model.Headset;

namespace NGenuity2.Devices.Headset
{
	// Token: 0x020007D2 RID: 2002
	public class HeadsetCloudAlphaWireless : HeadsetDTSHeadset<CMDHSCloudAlphaWirelessBase, CMDHSCloudAlphaWirelessShowLighting>
	{
		// Token: 0x06002D06 RID: 11526 RVA: 0x000B86CD File Offset: 0x000B68CD
		public override byte[] CreateBuffer()
		{
			return new byte[base.Device.OutputReportByteLength];
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x000B86E0 File Offset: 0x000B68E0
		public override byte[] GetInputReport()
		{
			if (base.Device != null)
			{
				try
				{
					Thread.Sleep(20);
					return base.Device.ReadInputReport(33, 1000);
				}
				catch (Exception)
				{
				}
			}
			return null;
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x000B8728 File Offset: 0x000B6928
		protected override void OnDeviceInputReportReceived(byte[] buffer)
		{
			base.OnDeviceInputReportReceived(buffer);
			if (buffer[0] != 33 || buffer[1] != 187)
			{
				return;
			}
			if (buffer[2] == 7)
			{
				base.AutoPowerOff = (int)buffer[3];
				this.CheckInitState(16);
			}
			if (buffer[2] == 9)
			{
				base.VoicePrompt = (buffer[3] == 1);
				this.CheckInitState(32);
			}
			byte b = buffer[2];
			if (buffer[2] == 34 || buffer[2] == 5)
			{
				this.OnAudioDeviceMuted(AudioDeviceType.Sidetone, buffer[3] == 0, true);
				this.CheckInitState(4);
			}
			if (buffer[2] == 35 || buffer[2] == 10)
			{
				this.OnAudioDeviceMuted(AudioDeviceType.Microphone, buffer[3] == 1, true);
				this.CheckInitState(8);
			}
			if (buffer[2] == 36 && buffer[3] == 2 != base.Connected)
			{
				base.Connected = (buffer[3] == 2);
				base.Sleeping = !base.Connected;
				if (base.Connected)
				{
					this.GetBasicInfo();
				}
			}
			if (buffer[2] == 37 || buffer[2] == 11)
			{
				base.Battery = (int)buffer[3];
				this.CheckInitState(1);
				base.CheckBatteryNotification();
			}
			if (buffer[2] == 38 || buffer[2] == 12)
			{
				byte b2 = buffer[3];
				if (b2 != 0)
				{
					if (b2 == 1)
					{
						base.ChargingStatus = ChargingStatus.WireCharging;
					}
				}
				else
				{
					base.ChargingStatus = ChargingStatus.NoCharging;
				}
				this.CheckInitState(2);
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06002D09 RID: 11529 RVA: 0x000B8860 File Offset: 0x000B6A60
		// (set) Token: 0x06002D0A RID: 11530 RVA: 0x000B8872 File Offset: 0x000B6A72
		public override bool UpgradeMode
		{
			get
			{
				return base.UpgradeMode || this.ProductNeedUpdate;
			}
			protected set
			{
				base.UpgradeMode = value;
			}
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x000B887B File Offset: 0x000B6A7B
		public HeadsetCloudAlphaWireless()
		{
			base.Model = HyperXDeviceModel.Headset_CloudAlphaWireless;
			base.Engine.DeviceModel = base.Model;
			base.LowBatteryThreshold = 10;
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x000B88A4 File Offset: 0x000B6AA4
		protected override void InitDevice()
		{
			base.InitDevice();
			CMDHSCloudAlphaWirelessGetWirelessState cmdhscloudAlphaWirelessGetWirelessState = new CMDHSCloudAlphaWirelessGetWirelessState();
			cmdhscloudAlphaWirelessGetWirelessState.Execute(this);
			base.Connected = cmdhscloudAlphaWirelessGetWirelessState.Connected;
			base.Sleeping = !cmdhscloudAlphaWirelessGetWirelessState.Connected;
			CMDHSCloudAlphaWirelessGetPairingInfo cmdhscloudAlphaWirelessGetPairingInfo = new CMDHSCloudAlphaWirelessGetPairingInfo();
			cmdhscloudAlphaWirelessGetPairingInfo.Execute(this);
			base.PairID = cmdhscloudAlphaWirelessGetPairingInfo.PairID;
			CMDHSCloudAlphaWirelessGetAutoShutdown cmdhscloudAlphaWirelessGetAutoShutdown = new CMDHSCloudAlphaWirelessGetAutoShutdown();
			cmdhscloudAlphaWirelessGetAutoShutdown.Execute(this);
			if (cmdhscloudAlphaWirelessGetAutoShutdown.Duration != null)
			{
				base.AutoPowerOff = (int)cmdhscloudAlphaWirelessGetAutoShutdown.Duration.Value;
			}
			if (cmdhscloudAlphaWirelessGetWirelessState.Connected)
			{
				this.GetBasicInfo();
			}
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x000B8938 File Offset: 0x000B6B38
		private void OnCommandExecuted(HXCommandBase cmd, object info)
		{
			CMDHSCloudAlphaWirelessGetBatteryInfo cmdhscloudAlphaWirelessGetBatteryInfo = cmd as CMDHSCloudAlphaWirelessGetBatteryInfo;
			if (cmdhscloudAlphaWirelessGetBatteryInfo != null && cmdhscloudAlphaWirelessGetBatteryInfo.Battery != null)
			{
				base.Battery = cmdhscloudAlphaWirelessGetBatteryInfo.Battery.Value;
				this.CheckInitState(1);
				base.CheckBatteryNotification();
			}
			CMDHSCloudAlphaWirelessGetChargeStatus cmdhscloudAlphaWirelessGetChargeStatus = cmd as CMDHSCloudAlphaWirelessGetChargeStatus;
			if (cmdhscloudAlphaWirelessGetChargeStatus != null && cmdhscloudAlphaWirelessGetChargeStatus.Status != null)
			{
				base.ChargingStatus = cmdhscloudAlphaWirelessGetChargeStatus.Status.Value;
				this.CheckInitState(2);
			}
			CMDHSCloudAlphaWirelessGetSidetoneStatus cmdhscloudAlphaWirelessGetSidetoneStatus = cmd as CMDHSCloudAlphaWirelessGetSidetoneStatus;
			if (cmdhscloudAlphaWirelessGetSidetoneStatus != null && cmdhscloudAlphaWirelessGetSidetoneStatus.On != null)
			{
				this.SidetoneMuted = !cmdhscloudAlphaWirelessGetSidetoneStatus.On.Value;
				this.CheckInitState(4);
			}
			CMDHSCloudAlphaWirelessGetMicMute cmdhscloudAlphaWirelessGetMicMute = cmd as CMDHSCloudAlphaWirelessGetMicMute;
			if (cmdhscloudAlphaWirelessGetMicMute != null && cmdhscloudAlphaWirelessGetMicMute.Muted != null)
			{
				this.MicrophoneMuted = cmdhscloudAlphaWirelessGetMicMute.Muted.Value;
				this.CheckInitState(8);
			}
			CMDHSCloudAlphaWirelessGetAutoShutdown cmdhscloudAlphaWirelessGetAutoShutdown = cmd as CMDHSCloudAlphaWirelessGetAutoShutdown;
			if (cmdhscloudAlphaWirelessGetAutoShutdown != null && cmdhscloudAlphaWirelessGetAutoShutdown.Duration != null)
			{
				base.AutoPowerOff = (int)cmdhscloudAlphaWirelessGetAutoShutdown.Duration.Value;
				this.CheckInitState(16);
			}
			CMDHSCloudAlphaWirelessGetVoicePromptStatus cmdhscloudAlphaWirelessGetVoicePromptStatus = cmd as CMDHSCloudAlphaWirelessGetVoicePromptStatus;
			if (cmdhscloudAlphaWirelessGetVoicePromptStatus != null && cmdhscloudAlphaWirelessGetVoicePromptStatus.On != null)
			{
				base.VoicePrompt = cmdhscloudAlphaWirelessGetVoicePromptStatus.On.Value;
				this.CheckInitState(32);
			}
			CMDHSCloudAlphaWirelessAPO cmdhscloudAlphaWirelessAPO = cmd as CMDHSCloudAlphaWirelessAPO;
			if (cmdhscloudAlphaWirelessAPO != null)
			{
				base.HandleAPOCommand(cmdhscloudAlphaWirelessAPO.Action, info);
			}
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x000B8ABC File Offset: 0x000B6CBC
		private void CheckInitState(int state)
		{
			if (this._initing)
			{
				this._init_state |= state;
				if (this._init_state == 63)
				{
					this._initing = false;
					HyperXCenter.Center.OnDeviceUpdated(this);
				}
			}
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x000B8AF0 File Offset: 0x000B6CF0
		private void GetBasicInfo()
		{
			this._initing = true;
			this._init_state = 0;
			HXCommandCollection<CMDHSCloudAlphaWirelessBase> hxcommandCollection = new HXCommandCollection<CMDHSCloudAlphaWirelessBase>();
			hxcommandCollection.Handler = new HXCommandHandler(this.OnCommandExecuted);
			hxcommandCollection.AddCommand(new CMDHSCloudAlphaWirelessGetBatteryInfo
			{
				Handler = new HXCommandHandler(this.OnCommandExecuted)
			});
			hxcommandCollection.AddCommand(new CMDHSCloudAlphaWirelessGetChargeStatus
			{
				Handler = new HXCommandHandler(this.OnCommandExecuted)
			});
			hxcommandCollection.AddCommand(new CMDHSCloudAlphaWirelessGetSidetoneStatus
			{
				Handler = new HXCommandHandler(this.OnCommandExecuted)
			});
			hxcommandCollection.AddCommand(new CMDHSCloudAlphaWirelessGetMicMute
			{
				Handler = new HXCommandHandler(this.OnCommandExecuted)
			});
			hxcommandCollection.AddCommand(new CMDHSCloudAlphaWirelessGetAutoShutdown
			{
				Handler = new HXCommandHandler(this.OnCommandExecuted)
			});
			hxcommandCollection.AddCommand(new CMDHSCloudAlphaWirelessGetVoicePromptStatus
			{
				Handler = new HXCommandHandler(this.OnCommandExecuted)
			});
			this.AddCommand(hxcommandCollection);
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x000B8BF0 File Offset: 0x000B6DF0
		public override void OpenDevice(string deviceId)
		{
			base.DeviceID = deviceId;
			this.AddCommand(new CMDHSCloudAlphaWirelessAPO(new HXCommandHandler(this.OnCommandExecuted))
			{
				Action = DTSAPOCommand.Initialize
			});
			base.SendDiscordCertificateDeviceNotificationAsync(null);
			base.Connected = false;
			base.Sleeping = true;
			base.OpenDevice(deviceId);
			if (this.IsOpened)
			{
				base.OpenNotificationTunnel();
				base.UpdateID();
				this.SetupDevice();
			}
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x000B48BE File Offset: 0x000B2ABE
		public override void ApplyBasicSettings()
		{
			base.ApplyBasicSettings();
			this.ApplyVolumeSettings();
			this.ApplyMicrophoneSettings();
			this.ApplySidetoneSettings();
			this.ApplySurroundSettings();
			this.ApplyGamechatSettings();
			this.ApplyKeyAssignments();
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x000B8C58 File Offset: 0x000B6E58
		public override void ApplyPreset(Preset preset)
		{
			base.ApplyPreset(preset);
			this.ApplyBasicSettings();
			if (preset == null)
			{
				return;
			}
			EqualizerPreset equalizerPreset = preset.Headset.Equalizers.FirstOrDefault((EqualizerPreset o) => o.Selected);
			if (equalizerPreset == null)
			{
				this.ApplyEQBands(null);
				return;
			}
			this.ApplyEQBands(equalizerPreset.Bands);
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x000B47DD File Offset: 0x000B29DD
		public override void ApplyGameEQ(KnownGames game)
		{
			base.ApplyGameEQ(game);
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x000B8CBD File Offset: 0x000B6EBD
		public override void ChangeEQStatus(bool enabled)
		{
			base.ChangeEQStatus(enabled);
			this.AddCommand(new CMDHSCloudAlphaWirelessAPO(new HXCommandHandler(this.OnCommandExecuted))
			{
				Action = DTSAPOCommand.EnableEQ,
				Info = enabled
			});
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x000B8CF0 File Offset: 0x000B6EF0
		public override void ApplySurroundSettings()
		{
			base.ApplySurroundSettings();
			Preset preset = null;
			lock (this)
			{
				preset = base.Preset;
			}
			if (preset == null)
			{
				return;
			}
			bool flag2 = preset.Headset.DSPMode > HeadsetDSPMode.None;
			base.SurroundSound = flag2;
			this.AddCommand(new CMDHSCloudAlphaWirelessAPO(new HXCommandHandler(this.OnCommandExecuted))
			{
				Action = DTSAPOCommand.EnableSurroundSound,
				Info = flag2
			});
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x000B8D78 File Offset: 0x000B6F78
		public override void ChangeAutoPowerOff(int autoPowerOff)
		{
			base.ChangeAutoPowerOff(autoPowerOff);
			this.AddCommand(new CMDHSCloudAlphaWirelessSetAutoShutdown
			{
				Duration = (byte)autoPowerOff
			});
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x000B8DA4 File Offset: 0x000B6FA4
		public override void ApplySidetoneSettings()
		{
			base.ApplySidetoneSettings();
			Preset safePreset = base.GetSafePreset();
			if (safePreset == null)
			{
				return;
			}
			if (this.SidetoneMuted != safePreset.Headset.SidetoneMuted)
			{
				this.SidetoneMuted = safePreset.Headset.SidetoneMuted;
				this.AddCommand(new CMDHSCloudAlphaWirelessSetSidetoneStatus
				{
					On = !safePreset.Headset.SidetoneMuted
				});
			}
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x000B8E08 File Offset: 0x000B7008
		public override void ChangeVoicePrompt(bool enabled)
		{
			base.ChangeVoicePrompt(enabled);
			this.AddCommand(new CMDHSCloudAlphaWirelessSetVoicePromptStatus
			{
				On = enabled
			});
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x000B8E30 File Offset: 0x000B7030
		public override void ChangeSpatialStatus(bool enabled)
		{
			base.ChangeSpatialStatus(enabled);
			base.SetSpatialEnabled(enabled);
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x000B8E41 File Offset: 0x000B7041
		public override void ApplyEQSettings(List<EQSetting> settings, int saveProfile = 0)
		{
			this.ApplyEQBands(settings);
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x000B8E4A File Offset: 0x000B704A
		public override void PreStop()
		{
			this.AddCommand(new CMDHSCloudAlphaWirelessAPO(new HXCommandHandler(this.OnCommandExecuted))
			{
				Action = DTSAPOCommand.Deinitialize
			});
			base.PreStop();
		}

		// Token: 0x06002D1C RID: 11548 RVA: 0x000B8E70 File Offset: 0x000B7070
		public override void EnterPairMode()
		{
			base.EnterPairMode();
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x000B8E78 File Offset: 0x000B7078
		private void TimerTicked(object state)
		{
			if (base.Connected)
			{
				this.AddCommand(new CMDHSCloudAlphaWirelessGetBatteryInfo
				{
					Handler = new HXCommandHandler(this.OnCommandExecuted)
				});
				this.AddCommand(new CMDHSCloudAlphaWirelessGetChargeStatus
				{
					Handler = new HXCommandHandler(this.OnCommandExecuted)
				});
			}
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x000B8ECB File Offset: 0x000B70CB
		public override void Start()
		{
			base.Start();
			this._timer = new Timer(new TimerCallback(this.TimerTicked), null, new TimeSpan(0L), new TimeSpan(0, 0, 30));
		}

		// Token: 0x06002D1F RID: 11551 RVA: 0x000B8EFB File Offset: 0x000B70FB
		public override void Stop(bool waitUntilStopped)
		{
			base.Stop(waitUntilStopped);
			Timer timer = this._timer;
			if (timer != null)
			{
				timer.Dispose();
			}
			this._timer = null;
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x000B8F1C File Offset: 0x000B711C
		private void ApplyEQBands(List<EQSetting> settings)
		{
			if (settings == null || settings.Count < 10)
			{
				settings = new List<EQSetting>();
				settings.Add(new EQSetting
				{
					BandID = 0,
					Frequency = 32,
					Gain = 0f
				});
				settings.Add(new EQSetting
				{
					BandID = 1,
					Frequency = 64,
					Gain = 0f
				});
				settings.Add(new EQSetting
				{
					BandID = 2,
					Frequency = 125,
					Gain = 0f
				});
				settings.Add(new EQSetting
				{
					BandID = 3,
					Frequency = 250,
					Gain = 0f
				});
				settings.Add(new EQSetting
				{
					BandID = 4,
					Frequency = 500,
					Gain = 0f
				});
				settings.Add(new EQSetting
				{
					BandID = 5,
					Frequency = 1000,
					Gain = 0f
				});
				settings.Add(new EQSetting
				{
					BandID = 6,
					Frequency = 2000,
					Gain = 0f
				});
				settings.Add(new EQSetting
				{
					BandID = 7,
					Frequency = 4000,
					Gain = 0f
				});
				settings.Add(new EQSetting
				{
					BandID = 8,
					Frequency = 8000,
					Gain = 0f
				});
				settings.Add(new EQSetting
				{
					BandID = 9,
					Frequency = 16000,
					Gain = 0f
				});
			}
			int[] array = new int[settings.Count];
			foreach (EQSetting eqsetting in settings)
			{
				if (eqsetting.BandID >= 10)
				{
					return;
				}
				array[eqsetting.BandID] = (int)(eqsetting.Gain * 100f);
			}
			this.AddCommand(new CMDHSCloudAlphaWirelessAPO(new HXCommandHandler(this.OnCommandExecuted))
			{
				Action = DTSAPOCommand.UpdateEQ,
				Info = array
			});
		}

		// Token: 0x040021BC RID: 8636
		private Timer _timer;

		// Token: 0x040021BD RID: 8637
		private bool _initing;

		// Token: 0x040021BE RID: 8638
		private int _init_state;

		// Token: 0x040021BF RID: 8639
		private const int INIT_STATE_BATTERY = 1;

		// Token: 0x040021C0 RID: 8640
		private const int INIT_STATE_CHARGE_STATUS = 2;

		// Token: 0x040021C1 RID: 8641
		private const int INIT_STATE_SIDETONE = 4;

		// Token: 0x040021C2 RID: 8642
		private const int INIT_STATE_MIC = 8;

		// Token: 0x040021C3 RID: 8643
		private const int INIT_STATE_AUTOPOWER = 16;

		// Token: 0x040021C4 RID: 8644
		private const int INIT_STATE_VOICE_PROMPT = 32;
	}
}```

