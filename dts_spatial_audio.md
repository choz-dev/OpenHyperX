# NGenuity2.Devices.Headset.DTS
```csharp
using System;

namespace NGenuity2.Devices.Headset.DTS
{
	// Token: 0x0200081D RID: 2077
	public enum DTSAPOCommand
	{
		// Token: 0x04002244 RID: 8772
		None,
		// Token: 0x04002245 RID: 8773
		Initialize,
		// Token: 0x04002246 RID: 8774
		Deinitialize,
		// Token: 0x04002247 RID: 8775
		EnableSurroundSound,
		// Token: 0x04002248 RID: 8776
		EnableEQ,
		// Token: 0x04002249 RID: 8777
		UpdateEQ
	}
}```

```csharp
using System;

namespace NGenuity2.Devices.Headset.DTS
{
	// Token: 0x0200081E RID: 2078
	public enum DTSEffectType
	{
		// Token: 0x0400224B RID: 8779
		StreamEffect
	}
}```

```csharp
using System;

namespace NGenuity2.Devices.Headset.DTS
{
	// Token: 0x0200081F RID: 2079
	public enum DTSRoomType
	{
		// Token: 0x0400224D RID: 8781
		Entertainment,
		// Token: 0x0400224E RID: 8782
		Game,
		// Token: 0x0400224F RID: 8783
		Sports
	}
}```

```csharp
using System;

namespace NGenuity2.Devices.Headset.DTS
{
	// Token: 0x02000820 RID: 2080
	public enum DTSStereoType
	{
		// Token: 0x04002251 RID: 8785
		Traditional,
		// Token: 0x04002252 RID: 8786
		Front,
		// Token: 0x04002253 RID: 8787
		Wide
	}
}
```

# UniversalHeadsetDTSBase
```csharp
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NGenuity2.Devices.Headset;
using NGenuity2.Devices.Headset.DTS;
using NGenuity2.Devices.Universals.Commands;
using NGenuity2.Devices.Universals.Commons;
using NGenuity2.Devices.Universals.Models;
using NGenuity2.Model;
using NGenuity2.Model.Headset;

namespace NGenuity2.Devices.Universals.Headsets
{
	// Token: 0x02000907 RID: 2311
	public class UniversalHeadsetDTSBase : HeadsetDTSHeadset<UniversalCMDBase, PerkeyLightCMD>, IUniversalDevice
	{
		// Token: 0x060032EA RID: 13034 RVA: 0x000CA29C File Offset: 0x000C849C
		public UniversalHeadsetDTSBase()
		{
			this._controller = new UniversalController(this);
			this._controller.ReceivedGenericAudioSettingsResponse += this.OnReceivedGenericAudioSettingsResponse;
			this._controller.ReceivedAdvancedAudioSettingsResponse += this.OnReceivedAdvancedAudioSettingsResponse;
			this._controller.ReceivedWirelessRFDeviceSettingResponse += this.OnReceivedWirelessRFDeviceSettingResponse;
			this._controller.ReceivedDeviceAudioMuteStateChanged += this.OnReceivedAudioMuteStateChangeNotification;
		}

		// Token: 0x060032EB RID: 13035 RVA: 0x000CA326 File Offset: 0x000C8526
		protected override void InitDevice()
		{
			this.AddCommand(new UniversalDTSCMD(new HXCommandHandler(this.OnDTSCommandExecuted))
			{
				Action = DTSAPOCommand.Initialize
			});
		}

		// Token: 0x060032EC RID: 13036 RVA: 0x000CA346 File Offset: 0x000C8546
		public override void PreStop()
		{
			this.AddCommand(new UniversalDTSCMD(new HXCommandHandler(this.OnDTSCommandExecuted))
			{
				Action = DTSAPOCommand.Deinitialize
			});
			base.PreStop();
		}

		// Token: 0x060032ED RID: 13037 RVA: 0x000CA36C File Offset: 0x000C856C
		protected override void OnDeviceInputReportReceived(byte[] buffer)
		{
			base.OnDeviceInputReportReceived(buffer);
			this._controller.HandleReceivedBuffers(buffer);
			if (this._controller.DeviceInformation != null)
			{
				this._channel.WriteAsync(this._controller.DeviceInformation);
			}
		}

		// Token: 0x060032EE RID: 13038 RVA: 0x000CA3A5 File Offset: 0x000C85A5
		public override void ChangeSpatialStatus(bool enabled)
		{
			base.ChangeSpatialStatus(enabled);
			base.SetSpatialEnabled(enabled);
		}

		// Token: 0x060032EF RID: 13039 RVA: 0x000CA3B8 File Offset: 0x000C85B8
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
			this.AddCommand(new UniversalDTSCMD(new HXCommandHandler(this.OnDTSCommandExecuted))
			{
				Action = DTSAPOCommand.EnableSurroundSound,
				Info = flag2
			});
		}

		// Token: 0x060032F0 RID: 13040 RVA: 0x000CA440 File Offset: 0x000C8640
		public Task<DeviceInforPayload> GetDeviceInformationAsync()
		{
			UniversalHeadsetDTSBase.<GetDeviceInformationAsync>d__8 <GetDeviceInformationAsync>d__;
			<GetDeviceInformationAsync>d__.<>t__builder = AsyncTaskMethodBuilder<DeviceInforPayload>.Create();
			<GetDeviceInformationAsync>d__.<>4__this = this;
			<GetDeviceInformationAsync>d__.<>1__state = -1;
			<GetDeviceInformationAsync>d__.<>t__builder.Start<UniversalHeadsetDTSBase.<GetDeviceInformationAsync>d__8>(ref <GetDeviceInformationAsync>d__);
			return <GetDeviceInformationAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060032F1 RID: 13041 RVA: 0x000CA483 File Offset: 0x000C8683
		public void StopAllEngines()
		{
			CommandsEngine queueEngine = this._queueEngine;
			if (queueEngine != null)
			{
				queueEngine.Stop();
			}
			AckChannel<DeviceInforPayload> channel = this._channel;
			if (channel == null)
			{
				return;
			}
			channel.Complete();
		}

		// Token: 0x060032F2 RID: 13042 RVA: 0x000B458D File Offset: 0x000B278D
		protected virtual void CheckAndUpdateSidetoneState(bool isOn)
		{
			this.OnAudioDeviceMuted(AudioDeviceType.Sidetone, !isOn, true);
		}

		// Token: 0x060032F3 RID: 13043 RVA: 0x000B058E File Offset: 0x000AE78E
		protected virtual void CheckAndUpdateMicMuteState(bool muted)
		{
			this.OnAudioDeviceMuted(AudioDeviceType.Microphone, muted, true);
		}

		// Token: 0x060032F4 RID: 13044 RVA: 0x000B45CE File Offset: 0x000B27CE
		protected virtual void CheckAndUpdateAutoPowerOff(int newValue)
		{
			if (newValue != base.AutoPowerOff)
			{
				base.AutoPowerOff = newValue;
				HyperXCenter.Center.OnDeviceUpdated(this);
			}
		}

		// Token: 0x060032F5 RID: 13045 RVA: 0x000B4F09 File Offset: 0x000B3109
		protected virtual void CheckAndUpdateVoicePrompt(bool newValue)
		{
			if (newValue != base.VoicePrompt)
			{
				base.VoicePrompt = newValue;
				HyperXCenter.Center.OnDeviceUpdated(this);
			}
		}

		// Token: 0x060032F6 RID: 13046 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		protected virtual void OnReceivedGenericAudioSettingsResponse(CMDGenericAudioSettingsSubReportID srid, object obj)
		{
		}

		// Token: 0x060032F7 RID: 13047 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		protected virtual void OnReceivedAdvancedAudioSettingsResponse(CMDAdvancedAudioSettingsSubReportID srid, object obj)
		{
		}

		// Token: 0x060032F8 RID: 13048 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		protected virtual void OnReceivedWirelessRFDeviceSettingResponse(CMDWirelessRFDeviceSettingSubReportID srid, object obj)
		{
		}

		// Token: 0x060032F9 RID: 13049 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		protected virtual void OnReceivedAudioMuteStateChangeNotification(ResponseNotifications type, bool muted)
		{
		}

		// Token: 0x060032FA RID: 13050 RVA: 0x000CA4A8 File Offset: 0x000C86A8
		private void OnDTSCommandExecuted(HXCommandBase cmd, object info)
		{
			UniversalDTSCMD universalDTSCMD = cmd as UniversalDTSCMD;
			if (universalDTSCMD != null)
			{
				base.HandleAPOCommand(universalDTSCMD.Action, info);
			}
		}

		// Token: 0x0400245A RID: 9306
		internal readonly UniversalController _controller;

		// Token: 0x0400245B RID: 9307
		protected AckChannel<DeviceInforPayload> _channel = AckChannel.CreateUnbounded<DeviceInforPayload>();
	}
}```

# HeadsetDTSHeadset<T1, T2>
```csharp
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using DTS_APO_ControllerLib;
using Microsoft.Win32;
using NGenuity2.Audio;
using NGenuity2.Common;
using NGenuity2.Common.Devices;
using NGenuity2.Devices.Headset.DTS;

namespace NGenuity2.Devices.Headset
{
	// Token: 0x02000800 RID: 2048
	public abstract class HeadsetDTSHeadset<T1, T2> : HyperXHeadsetDevice<T1, T2> where T1 : HXCommandBase where T2 : T1
	{
		// Token: 0x06002E9F RID: 11935 RVA: 0x000C0CC0 File Offset: 0x000BEEC0
		public HeadsetDTSHeadset()
		{
			for (int i = 0; i < this._gains.Length; i++)
			{
				this._gains[i] = -10000f;
			}
		}

		// Token: 0x06002EA0 RID: 11936 RVA: 0x00058D44 File Offset: 0x00056F44
		public override void PreStop()
		{
			base.PreStop();
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06002EA1 RID: 11937 RVA: 0x000C0D00 File Offset: 0x000BEF00
		protected IDTSEndpointInfo EndpointInfo1
		{
			get
			{
				return this._endpoint1;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06002EA2 RID: 11938 RVA: 0x000C0D08 File Offset: 0x000BEF08
		protected IDTSEndpointInfo2 EndpointInfo2
		{
			get
			{
				return this._endpoint2;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06002EA3 RID: 11939 RVA: 0x000C0D10 File Offset: 0x000BEF10
		protected IDTSEndpointInfo3 EndpointInfo3
		{
			get
			{
				return this._endpoint3;
			}
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x000C0D18 File Offset: 0x000BEF18
		public override List<string> CheckDrivers()
		{
			List<DriverInfo> list = new List<DriverInfo>();
			string installedLocation = Utils.InstalledLocation;
			base.AddDriverInfo(list, Path.Combine(installedLocation, "Assets\\Native\\AudioDTS\\dtshpxv2_hyperx_ext.inf"));
			base.AddDriverInfo(list, Path.Combine(installedLocation, "Assets\\Native\\AudioDTS\\dtshpxv2apo4xservice.inf"));
			base.AddDriverInfo(list, Path.Combine(installedLocation, "Assets\\Native\\AudioDTS\\dtsapo4xhpxv2x64.inf"));
			return base.CheckDrivers(list);
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06002EA5 RID: 11941 RVA: 0x0001C463 File Offset: 0x0001A663
		internal override bool NeedDTSAPO
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x000C0D70 File Offset: 0x000BEF70
		private bool CheckDTSStatus(string endpointId)
		{
			try
			{
				int num = endpointId.LastIndexOf('{');
				if (num < 0)
				{
					return false;
				}
				endpointId = endpointId.Substring(num).ToLowerInvariant();
				using (RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
				{
					using (RegistryKey registryKey2 = registryKey.OpenSubKey(string.Format("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\MMDevices\\Audio\\Render\\{0}\\FxProperties", endpointId), false))
					{
						string[] subKeyNames = registryKey2.GetSubKeyNames();
						string[] array = registryKey2.GetValue("{d04e05a6-594b-4fb6-a80d-01af5eed7d1d},13") as string[];
						string[] array2 = registryKey2.GetValue("{d3993a3f-99c2-4402-b5ec-a92a0367664b},5") as string[];
						bool flag;
						if (array == null)
						{
							flag = false;
						}
						else
						{
							flag = (array.Count((string o) => string.Equals(o, "{8B778F49-83A0-4EE9-896A-ED52903EDF1F}", StringComparison.InvariantCultureIgnoreCase)) >= 1);
						}
						if (flag && array2 != null && array2.Length == 6)
						{
							if (subKeyNames.Count((string o) => string.Equals(o, "{0CC8F6D7-FA5E-448B-AA61-01227B03E43F}", StringComparison.InvariantCultureIgnoreCase)) == 1)
							{
								if (subKeyNames.Count((string o) => string.Equals(o, "{13C6D2E0-9A3D-4808-BD0B-1F50AAD83D99}", StringComparison.InvariantCultureIgnoreCase)) == 1)
								{
									if (subKeyNames.Count((string o) => string.Equals(o, "{13CBF472-DD1D-4055-A74D-6055827B3D91}", StringComparison.InvariantCultureIgnoreCase)) == 1)
									{
										if (subKeyNames.Count((string o) => string.Equals(o, "{20AF49E8-939F-4843-9C6B-1DCCD83B11DC}", StringComparison.InvariantCultureIgnoreCase)) == 1)
										{
											if (subKeyNames.Count((string o) => string.Equals(o, "{B970A8EF-5733-434D-B624-770402E2500B}", StringComparison.InvariantCultureIgnoreCase)) == 1)
											{
												return true;
											}
										}
									}
								}
							}
						}
					}
				}
			}
			catch (Exception info)
			{
				Logger.WriteLine(info, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 120);
			}
			return false;
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x000C0F80 File Offset: 0x000BF180
		protected bool BindAPO(string deviceId)
		{
			if (string.IsNullOrEmpty(deviceId))
			{
				return false;
			}
			List<string> list = this.CheckDrivers();
			bool flag = list != null && list.Count > 0;
			Logger.WriteLine(string.Format("NeedInstallDriver {0} on {1}", flag, base.Model), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 139);
			if (flag)
			{
				base.DriverInstallationState = DriverInstallationState.NotInstalled;
				Logger.WriteLine(string.Format("DTS APO driver for {0} is not installed!", base.Model), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 143);
				return false;
			}
			string text = string.Empty;
			IReadOnlyList<MMDevice> audioDevices = base.AudioDevices;
			lock (audioDevices)
			{
				MMDevice mmdevice = base.AudioDevices.FirstOrDefault((MMDevice o) => o.DataFlow == AudioDeviceDataFlow.Render);
				text = ((mmdevice != null) ? mmdevice.Id : null);
			}
			try
			{
				this._dtsApo = (DTSAPOSystem)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("7766EC50-C2D3-466F-BC7A-9C339C5908A2")));
				if (this._dtsApo == null)
				{
					Logger.WriteLine("Failed to instantiate DTSAPOSystem", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 170);
				}
			}
			catch (Exception)
			{
				Logger.WriteLine("Failed to instantiate DTSAPOSystem", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 176);
				return false;
			}
			if (this._dtsApo != null)
			{
				try
				{
					this._dtsApo.InitializeSystem(null);
					DTSEndpointEnumerator dtsendpointEnumerator = this._dtsApo.EnumerateDTSEndpoints();
					this._endpoint1 = dtsendpointEnumerator.GetEndpointFromDevice(text);
					if (this._endpoint1 != null)
					{
						Logger.WriteLine("Initialize DTS APO:" + this._endpoint1.FriendlyName, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 194);
						return true;
					}
					Logger.WriteLine("Failed to initialize DTS APO " + text, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 199);
				}
				catch (Exception info)
				{
					Logger.WriteLine("Failed to initialize DTS APO " + text, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 204);
					Logger.WriteLine(info, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 205);
					return false;
				}
			}
			return false;
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x000C1194 File Offset: 0x000BF394
		public override void ChangeEQStatus(bool enabled)
		{
			base.ChangeEQStatus(enabled);
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x000C11A0 File Offset: 0x000BF3A0
		protected void UnbindAPO()
		{
			if (this._endpoint1 != null)
			{
				Marshal.ReleaseComObject(this._endpoint1);
				this._endpoint1 = null;
			}
			if (this._endpoint2 != null)
			{
				Marshal.ReleaseComObject(this._endpoint2);
				this._endpoint2 = null;
			}
			if (this._endpoint3 != null)
			{
				Marshal.ReleaseComObject(this._endpoint3);
				this._endpoint3 = null;
			}
			COMFactory comFactory = this._comFactory;
			if (comFactory == null)
			{
				return;
			}
			comFactory.ReleaseInstance(this._dtsApo);
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x000C1214 File Offset: 0x000BF414
		protected virtual void InitDTSAPO()
		{
			if (!this.BindAPO(base.DeviceID))
			{
				HyperXCenter.Center.OnDeviceUpdated(this);
				return;
			}
			base.DriverActivated = true;
			base.EQEnabled = this.GetSFXGEQEnabled();
			base.SpatialEnabled = this.GetSpatialEnabled();
			HyperXCenter.Center.OnDeviceUpdated(this);
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x000C1268 File Offset: 0x000BF468
		private void ExecuteAPOCommandOnMainThread(DTSAPOCommand action, object info)
		{
			switch (action)
			{
			case DTSAPOCommand.None:
				break;
			case DTSAPOCommand.Initialize:
				this.InitDTSAPO();
				return;
			case DTSAPOCommand.Deinitialize:
				this.UnbindAPO();
				break;
			case DTSAPOCommand.EnableSurroundSound:
				this.SetAPOEnabled((bool)info);
				return;
			case DTSAPOCommand.EnableEQ:
				this.SetSFXGEQEnabled((bool)info);
				return;
			case DTSAPOCommand.UpdateEQ:
				this.SetSFXGEQBands((int[])info);
				return;
			default:
				return;
			}
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x000C12CC File Offset: 0x000BF4CC
		protected void HandleAPOCommand(DTSAPOCommand action, object info)
		{
			if (action == DTSAPOCommand.Initialize)
			{
				bool flag = false;
				for (int i = 0; i < 10; i++)
				{
					IReadOnlyList<MMDevice> audioDevices = base.AudioDevices;
					lock (audioDevices)
					{
						flag = (base.AudioDevices.Count((MMDevice o) => o.DataFlow == AudioDeviceDataFlow.Render) > 0);
					}
					if (flag)
					{
						break;
					}
					Thread.Sleep(500);
				}
				if (flag)
				{
					Thread.Sleep(1000);
				}
			}
			Action method = delegate()
			{
				this.ExecuteAPOCommandOnMainThread(action, info);
			};
			MainForm currentForm = MainForm.CurrentForm;
			if (currentForm == null)
			{
				return;
			}
			currentForm.Invoke(method);
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x000C13A4 File Offset: 0x000BF5A4
		protected bool GetSpatialEnabled()
		{
			bool flag = false;
			bool result;
			lock (this)
			{
				if (this._endpoint1 == null)
				{
					result = false;
				}
				else
				{
					try
					{
						flag = (this._endpoint1.OpMode.SpatialEnable == 1);
					}
					catch (Exception info)
					{
						Logger.WriteLine(info, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 347);
						return false;
					}
					result = flag;
				}
			}
			return result;
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x000C1420 File Offset: 0x000BF620
		protected bool SetSpatialEnabled(bool enabled)
		{
			bool flag = false;
			bool result;
			lock (this)
			{
				if (this._endpoint1 == null)
				{
					result = false;
				}
				else
				{
					Logger.WriteLine(string.Format("SetSpatialEnabled {0}", enabled), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 365);
					try
					{
						this._endpoint1.OpMode.SpatialEnable = ((enabled > false) ? 1 : 0);
						flag = true;
					}
					catch (Exception info)
					{
						Logger.WriteLine(info, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 374);
						return false;
					}
					result = flag;
				}
			}
			return result;
		}

		// Token: 0x06002EAF RID: 11951 RVA: 0x000C14BC File Offset: 0x000BF6BC
		protected bool SetAllSpatialEnabled(bool enabled)
		{
			bool flag = false;
			bool result;
			lock (this)
			{
				if (this._endpoint1 == null)
				{
					result = false;
				}
				else
				{
					Logger.WriteLine(string.Format("SetAllSpatialEnabled {0}", enabled), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 390);
					try
					{
						DTSAPOOpModeEnumerator opModeEnumerator = this._endpoint1.GetOpModeEnumerator();
						for (;;)
						{
							DTSOperatingMode dtsoperatingMode = opModeEnumerator.Next();
							if (dtsoperatingMode == null)
							{
								break;
							}
							dtsoperatingMode.SpatialEnable = ((enabled > false) ? 1 : 0);
							flag = true;
						}
					}
					catch (Exception info)
					{
						Logger.WriteLine(info, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 409);
						return false;
					}
					result = flag;
				}
			}
			return result;
		}

		// Token: 0x06002EB0 RID: 11952 RVA: 0x000C156C File Offset: 0x000BF76C
		protected bool SetAPOEnabled(bool enabled)
		{
			if (this._endpoint1 == null)
			{
				return false;
			}
			if (enabled)
			{
				return this.SetCurrentOperationMode("Game2");
			}
			this.SetCurrentOperationMode("Game1");
			if (!base.EQEnabled)
			{
				this._endpoint1.OpMode.APOEnable = 0;
				Logger.WriteLine("Turn APO Off", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 431);
			}
			return true;
		}

		// Token: 0x06002EB1 RID: 11953 RVA: 0x000C15CC File Offset: 0x000BF7CC
		protected bool SetCurrentOperationMode(string name)
		{
			bool result = false;
			lock (this)
			{
				if (this._endpoint1 == null)
				{
					return result;
				}
				try
				{
					string name2 = this._endpoint1.OpMode.Name;
					DTSAPOOpModeEnumerator opModeEnumerator = this._endpoint1.GetOpModeEnumerator();
					DTSOperatingMode dtsoperatingMode;
					do
					{
						dtsoperatingMode = opModeEnumerator.Next();
						if (dtsoperatingMode == null)
						{
							break;
						}
						name2 = dtsoperatingMode.Name;
					}
					while (name2.IndexOf(name, StringComparison.InvariantCultureIgnoreCase) <= -1);
					if (dtsoperatingMode != null)
					{
						Logger.WriteLine("SetCurrentOperationMode " + name2, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 478);
						if (dtsoperatingMode.AutoContentModeEnable != 0)
						{
							dtsoperatingMode.AutoContentModeEnable = 0;
						}
						if (dtsoperatingMode.APOEnable != 1)
						{
							dtsoperatingMode.APOEnable = 1;
						}
						this._endpoint1.OpMode = dtsoperatingMode;
						Thread.Sleep(10);
						result = true;
					}
				}
				catch (Exception info)
				{
					Logger.WriteLine(info, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 497);
				}
			}
			return result;
		}

		// Token: 0x06002EB2 RID: 11954 RVA: 0x000C16D0 File Offset: 0x000BF8D0
		protected bool SetAllAPOEnabled(bool enabled)
		{
			bool flag = false;
			bool result;
			lock (this)
			{
				if (this._endpoint1 == null)
				{
					result = false;
				}
				else
				{
					Logger.WriteLine(string.Format("SetAllAPOEnabled {0}", enabled), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 516);
					try
					{
						DTSAPOOpModeEnumerator opModeEnumerator = this._endpoint1.GetOpModeEnumerator();
						for (;;)
						{
							DTSOperatingMode dtsoperatingMode = opModeEnumerator.Next();
							if (dtsoperatingMode == null)
							{
								break;
							}
							dtsoperatingMode.APOEnable = ((enabled > false) ? 1 : 0);
							flag = true;
						}
					}
					catch (Exception info)
					{
						Logger.WriteLine(info, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 535);
						return false;
					}
					result = flag;
				}
			}
			return result;
		}

		// Token: 0x06002EB3 RID: 11955 RVA: 0x000C1780 File Offset: 0x000BF980
		protected bool RestoreDTSDefaults()
		{
			bool flag = false;
			bool result;
			lock (this)
			{
				Logger.WriteLine("RestoreDTSDefaults", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 548);
				if (this._endpoint1 == null)
				{
					result = false;
				}
				else
				{
					try
					{
						this._endpoint1.OpMode.RestoreDefaults();
						flag = true;
					}
					catch (Exception info)
					{
						Logger.WriteLine(info, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 561);
						return false;
					}
					result = flag;
				}
			}
			return result;
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x000C1810 File Offset: 0x000BFA10
		protected bool RestoreAllDTSDefaults()
		{
			bool flag = false;
			bool result;
			lock (this)
			{
				Logger.WriteLine("RestoreAllDTSDefaults", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 574);
				if (this._endpoint1 == null)
				{
					result = false;
				}
				else
				{
					try
					{
						DTSAPOOpModeEnumerator opModeEnumerator = this._endpoint1.GetOpModeEnumerator();
						for (;;)
						{
							DTSOperatingMode dtsoperatingMode = opModeEnumerator.Next();
							if (dtsoperatingMode == null)
							{
								break;
							}
							dtsoperatingMode.RestoreDefaults();
							flag = true;
						}
					}
					catch (Exception info)
					{
						Logger.WriteLine(info, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 598);
						return false;
					}
					result = flag;
				}
			}
			return result;
		}

		// Token: 0x06002EB5 RID: 11957 RVA: 0x000C18B4 File Offset: 0x000BFAB4
		protected void SetDSPConfig(bool enabled)
		{
			if (this._endpoint1 == null)
			{
				return;
			}
			Logger.WriteLine(string.Format("SetDSPConfig {0}", enabled), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 612);
			try
			{
				DTSAPOEndpointConfig config = this._endpoint1.GetConfig();
				DTSAPOEndpointConfig dtsapoendpointConfig = DTSAPOEndpointConfig.kDTSAPO_EPCfg_None;
				if (enabled)
				{
					dtsapoendpointConfig = DTSAPOEndpointConfig.kDTSAPO_EPCfg_DSP;
				}
				if (config != dtsapoendpointConfig)
				{
					IDTSEndpointInfo endpoint = this._endpoint1;
					if (endpoint != null)
					{
						endpoint.SetConfig(dtsapoendpointConfig);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x000C1928 File Offset: 0x000BFB28
		protected bool GetCurrentOperationModeControl(string uts, ref int value)
		{
			bool flag = false;
			bool result;
			lock (this)
			{
				if (this._endpoint1 == null)
				{
					result = false;
				}
				else
				{
					try
					{
						IDTSAPOControlEnumerator2 idtsapocontrolEnumerator = (IDTSAPOControlEnumerator2)this._endpoint1.OpMode.get_DTSAPOControlEnumerator(DTSAPOControlType.kDTSAPO_Cont_Public);
						if (idtsapocontrolEnumerator != null)
						{
							DTSAPOCOMControl controlFromName = idtsapocontrolEnumerator.GetControlFromName(uts);
							if (controlFromName != null)
							{
								value = controlFromName.Value;
								Logger.WriteLine(string.Format("GetCurrentOperationModeControl {0} -< {1}", uts, value), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 661);
								flag = true;
							}
						}
					}
					catch (Exception arg)
					{
						Logger.WriteLine(string.Format("GetCurrentOperationModeControl {0}: {1}", uts, arg), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 668);
						return false;
					}
					result = flag;
				}
			}
			return result;
		}

		// Token: 0x06002EB7 RID: 11959 RVA: 0x000C19F8 File Offset: 0x000BFBF8
		protected bool SetCurrentOperationModeControl(string uts, int value)
		{
			bool flag = false;
			bool result;
			lock (this)
			{
				if (this._endpoint1 == null)
				{
					result = false;
				}
				else
				{
					try
					{
						IDTSAPOControlEnumerator2 idtsapocontrolEnumerator = (IDTSAPOControlEnumerator2)this._endpoint1.OpMode.get_DTSAPOControlEnumerator(DTSAPOControlType.kDTSAPO_Cont_Public);
						if (idtsapocontrolEnumerator != null)
						{
							DTSAPOCOMControl controlFromName = idtsapocontrolEnumerator.GetControlFromName(uts);
							if (controlFromName != null)
							{
								Logger.WriteLine(string.Format("{0} : {1}", uts, value), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 702);
								controlFromName.Value = value;
								flag = true;
								Thread.Sleep(10);
							}
						}
					}
					catch (Exception arg)
					{
						Logger.WriteLine(string.Format("{0}: {1}", uts, arg), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 711);
						return false;
					}
					result = flag;
				}
			}
			return result;
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x000C1AD0 File Offset: 0x000BFCD0
		protected bool SetAllOperationModeControl(string uts, int value)
		{
			bool flag = false;
			bool result;
			lock (this)
			{
				if (this._endpoint1 == null)
				{
					result = false;
				}
				else
				{
					Logger.WriteLine(string.Format("{0} : {1}", uts, value), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 735);
					try
					{
						DTSAPOOpModeEnumerator opModeEnumerator = this._endpoint1.GetOpModeEnumerator();
						for (;;)
						{
							DTSOperatingMode dtsoperatingMode = opModeEnumerator.Next();
							dtsoperatingMode.AutoContentModeEnable = 0;
							if (dtsoperatingMode == null)
							{
								break;
							}
							IDTSAPOControlEnumerator2 idtsapocontrolEnumerator = (IDTSAPOControlEnumerator2)dtsoperatingMode.get_DTSAPOControlEnumerator(DTSAPOControlType.kDTSAPO_Cont_Public);
							if (idtsapocontrolEnumerator != null)
							{
								try
								{
									DTSAPOCOMControl controlFromName = idtsapocontrolEnumerator.GetControlFromName(uts);
									if (controlFromName != null)
									{
										controlFromName.Value = value;
										flag = true;
									}
									continue;
								}
								catch (Exception)
								{
									continue;
								}
								break;
							}
						}
					}
					catch (Exception info)
					{
						Logger.WriteLine(info, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 771);
						return false;
					}
					result = flag;
				}
			}
			return result;
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x000C1BBC File Offset: 0x000BFDBC
		protected bool SetStereoPreference(DTSStereoType stereoType)
		{
			string uts = "Eagle-Stereo Mode";
			return this.SetAllOperationModeControl(uts, (int)stereoType);
		}

		// Token: 0x06002EBA RID: 11962 RVA: 0x000C1BD8 File Offset: 0x000BFDD8
		protected bool SetMultiChannelRoom(DTSRoomType roomType)
		{
			string uts = "SFX:Eagle-I3DA Room Index";
			return this.SetAllOperationModeControl(uts, (int)roomType);
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x000C1BF4 File Offset: 0x000BFDF4
		protected bool SetEFXGEQEnabled(bool enabled)
		{
			string uts = "EFX:Eagle-GEQ Enable";
			return this.SetCurrentOperationModeControl(uts, (enabled > false) ? 1 : 0);
		}

		// Token: 0x06002EBC RID: 11964 RVA: 0x000C1C14 File Offset: 0x000BFE14
		protected bool SetMFXGEQEnabled(bool enabled)
		{
			if (this._endpoint1 == null)
			{
				return false;
			}
			string uts = "MFX:Eagle-GEQ Enable";
			bool result = this.SetCurrentOperationModeControl(uts, (enabled > false) ? 1 : 0);
			if (enabled)
			{
				if (this._endpoint1.OpMode.APOEnable == 0)
				{
					this._endpoint1.OpMode.APOEnable = 1;
					Logger.WriteLine("Turn APO On", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 819);
					return result;
				}
			}
			else if (!base.SurroundSound)
			{
				this._endpoint1.OpMode.APOEnable = 0;
				Logger.WriteLine("Turn APO On", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 827);
			}
			return result;
		}

		// Token: 0x06002EBD RID: 11965 RVA: 0x000C1CA4 File Offset: 0x000BFEA4
		protected bool GetMFXGEQEnabled()
		{
			string uts = "MFX:Eagle-GEQ Enable";
			int num = 0;
			this.GetCurrentOperationModeControl(uts, ref num);
			return num == 1;
		}

		// Token: 0x06002EBE RID: 11966 RVA: 0x000C1CC8 File Offset: 0x000BFEC8
		protected bool SetSFXGEQEnabled(bool enabled)
		{
			if (this._endpoint1 == null)
			{
				return false;
			}
			string uts = "SFX:Eagle-GEQ Enable";
			bool result = this.SetCurrentOperationModeControl(uts, (enabled > false) ? 1 : 0);
			if (enabled)
			{
				if (this._endpoint1.OpMode.APOEnable == 0)
				{
					this._endpoint1.OpMode.APOEnable = 1;
					Logger.WriteLine("Turn APO On", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 854);
					return result;
				}
			}
			else if (!base.SurroundSound)
			{
				this._endpoint1.OpMode.APOEnable = 0;
				Logger.WriteLine("Turn APO On", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\Headset\\HeadsetDTSHeadset.cs", 862);
			}
			return result;
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x000C1D58 File Offset: 0x000BFF58
		protected bool GetSFXGEQEnabled()
		{
			string uts = "SFX:Eagle-GEQ Enable";
			int num = 0;
			this.GetCurrentOperationModeControl(uts, ref num);
			return num == 1;
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x000C1D7C File Offset: 0x000BFF7C
		protected bool SetAllEFXGEQBands(int[] gains)
		{
			string format = "EFX:Eagle-GEQ Band{0} Gain";
			if (gains == null || gains.Length > 10)
			{
				throw new ArgumentException();
			}
			for (int i = 0; i < gains.Length; i++)
			{
				if (!this.SetAllOperationModeControl(string.Format(format, i), gains[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x000C1DC8 File Offset: 0x000BFFC8
		protected bool SetAllMFXGEQBands(int[] gains)
		{
			string format = "MFX:Eagle-GEQ Band{0} Gain";
			if (gains == null || gains.Length > 10)
			{
				throw new ArgumentException();
			}
			for (int i = 0; i < gains.Length; i++)
			{
				if (!this.SetAllOperationModeControl(string.Format(format, i), gains[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x000C1E14 File Offset: 0x000C0014
		protected bool SetMFXGEQBands(int[] gains)
		{
			if (!base.EQEnabled)
			{
				return false;
			}
			string format = "MFX:Eagle-GEQ Band{0} Gain";
			if (gains == null || gains.Length > 10)
			{
				throw new ArgumentException();
			}
			for (int i = 0; i < gains.Length; i++)
			{
				if ((float)gains[i] != this._gains[i])
				{
					this._gains[i] = (float)gains[i];
					if (!this.SetCurrentOperationModeControl(string.Format(format, i), gains[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x000C1E84 File Offset: 0x000C0084
		protected bool SetSFXGEQBands(int[] gains)
		{
			if (!base.EQEnabled)
			{
				return false;
			}
			string format = "SFX:Eagle-GEQ Band{0} Gain";
			if (gains == null || gains.Length > 10)
			{
				throw new ArgumentException();
			}
			for (int i = 0; i < gains.Length; i++)
			{
				if ((float)gains[i] != this._gains[i])
				{
					this._gains[i] = (float)gains[i];
					if (!this.SetCurrentOperationModeControl(string.Format(format, i), gains[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x04002210 RID: 8720
		private COMFactory _comFactory;

		// Token: 0x04002211 RID: 8721
		private IDTSAPOSystem _dtsApo;

		// Token: 0x04002212 RID: 8722
		private IDTSEndpointInfo _endpoint1;

		// Token: 0x04002213 RID: 8723
		private IDTSEndpointInfo2 _endpoint2;

		// Token: 0x04002214 RID: 8724
		private IDTSEndpointInfo3 _endpoint3;

		// Token: 0x04002215 RID: 8725
		private float[] _gains = new float[20];
	}
}```

# DTS_APO_ControllerLib
```csharp
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DTS_APO_ControllerLib
{
	// Token: 0x02000A45 RID: 2629
	[CompilerGenerated]
	[Guid("BCEBC5E2-18A3-4990-A9E0-A0C64C5C2E8C")]
	[CoClass(typeof(object))]
	[TypeIdentifier]
	[ComImport]
	public interface DTSAPOCOMControl : IDTSAPOControl
	{
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DTS_APO_ControllerLib
{
	// Token: 0x02000A46 RID: 2630
	[CompilerGenerated]
	[Guid("0D98B721-C17C-4CEC-AD9E-ABF7EB6B1D44")]
	[CoClass(typeof(object))]
	[TypeIdentifier]
	[ComImport]
	public interface DTSAPOControlEnumerator : IDTSAPOControlEnumerator
	{
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DTS_APO_ControllerLib
{
	// Token: 0x02000A47 RID: 2631
	[CompilerGenerated]
	[TypeIdentifier("7B68B8E4-09F6-4EFF-8F09-A862E9284BAD", "DTS_APO_ControllerLib.DTSAPOControlType")]
	public enum DTSAPOControlType
	{
		// Token: 0x04002AFD RID: 11005
		kDTSAPO_Cont_Public,
		// Token: 0x04002AFE RID: 11006
		kDTSAPO_Cont_Private,
		// Token: 0x04002AFF RID: 11007
		kDTSAPO_Cont_Default,
		// Token: 0x04002B00 RID: 11008
		kDTSAPO_Cont_NumControls
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DTS_APO_ControllerLib
{
	// Token: 0x02000A48 RID: 2632
	[CompilerGenerated]
	[TypeIdentifier("7B68B8E4-09F6-4EFF-8F09-A862E9284BAD", "DTS_APO_ControllerLib.DTSAPOEndpointConfig")]
	public enum DTSAPOEndpointConfig
	{
		// Token: 0x04002B02 RID: 11010
		kDTSAPO_EPCfg_None,
		// Token: 0x04002B03 RID: 11011
		kDTSAPO_EPCfg_DSP,
		// Token: 0x04002B04 RID: 11012
		kDTSAPO_EPCfg_Count
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DTS_APO_ControllerLib
{
	// Token: 0x02000A49 RID: 2633
	[CompilerGenerated]
	[Guid("37EC876D-B530-496E-8940-44053EA7D3EF")]
	[CoClass(typeof(object))]
	[TypeIdentifier]
	[ComImport]
	public interface DTSAPOOpModeEnumerator : IDTSAPOOpModeEnumerator
	{
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DTS_APO_ControllerLib
{
	// Token: 0x02000A4A RID: 2634
	[CompilerGenerated]
	[CoClass(typeof(object))]
	[Guid("7A00E0CA-5B10-4F6D-8C20-ED4A1A288F32")]
	[TypeIdentifier]
	[ComImport]
	public interface DTSAPOSystem : IDTSAPOSystem
	{
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DTS_APO_ControllerLib
{
	// Token: 0x02000A4B RID: 2635
	[CompilerGenerated]
	[Guid("6F079AA4-8604-4260-9F29-525B3124350B")]
	[CoClass(typeof(object))]
	[TypeIdentifier]
	[ComImport]
	public interface DTSCOMEndpointInfo : IDTSEndpointInfo
	{
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DTS_APO_ControllerLib
{
	// Token: 0x02000A4C RID: 2636
	[CompilerGenerated]
	[Guid("AF721805-05A3-4D9C-9946-06999051168F")]
	[CoClass(typeof(object))]
	[TypeIdentifier]
	[ComImport]
	public interface DTSEndpointEnumerator : IDTSEndpointEnumerator
	{
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DTS_APO_ControllerLib
{
	// Token: 0x02000A4D RID: 2637
	[CompilerGenerated]
	[Guid("93D5CED3-DB72-475E-9EA7-AEAED0BC2AA2")]
	[CoClass(typeof(object))]
	[TypeIdentifier]
	[ComImport]
	public interface DTSOperatingMode : IDTSOperatingMode
	{
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DTS_APO_ControllerLib
{
	// Token: 0x02000A4E RID: 2638
	[CompilerGenerated]
	[Guid("BCEBC5E2-18A3-4990-A9E0-A0C64C5C2E8C")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[TypeIdentifier]
	[ComImport]
	public interface IDTSAPOControl
	{
		// Token: 0x06003EF5 RID: 16117
		void _VtblGap1_4();

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06003EF6 RID: 16118
		// (set) Token: 0x06003EF7 RID: 16119
		[DispId(1610678276)]
		int Value { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] [param: In] set; }
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DTS_APO_ControllerLib
{
	// Token: 0x02000A4F RID: 2639
	[CompilerGenerated]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("0D98B721-C17C-4CEC-AD9E-ABF7EB6B1D44")]
	[TypeIdentifier]
	[ComImport]
	public interface IDTSAPOControlEnumerator
	{
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DTS_APO_ControllerLib
{
	// Token: 0x02000A50 RID: 2640
	[CompilerGenerated]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("92D2EF95-D0F3-4823-8AA3-CA62D2DF4267")]
	[TypeIdentifier]
	[ComImport]
	public interface IDTSAPOControlEnumerator2 : IDTSAPOControlEnumerator
	{
		// Token: 0x06003EF8 RID: 16120
		void _VtblGap1_1();

		// Token: 0x06003EF9 RID: 16121
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		DTSAPOCOMControl GetControlFromName([MarshalAs(UnmanagedType.BStr)] [In] string control_name);
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DTS_APO_ControllerLib
{
	// Token: 0x02000A51 RID: 2641
	[CompilerGenerated]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("37EC876D-B530-496E-8940-44053EA7D3EF")]
	[TypeIdentifier]
	[ComImport]
	public interface IDTSAPOOpModeEnumerator
	{
		// Token: 0x06003EFA RID: 16122
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		DTSOperatingMode Next();
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DTS_APO_ControllerLib
{
	// Token: 0x02000A52 RID: 2642
	[CompilerGenerated]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("7A00E0CA-5B10-4F6D-8C20-ED4A1A288F32")]
	[TypeIdentifier]
	[ComImport]
	public interface IDTSAPOSystem
	{
		// Token: 0x06003EFB RID: 16123
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		DTSEndpointEnumerator EnumerateDTSEndpoints();

		// Token: 0x06003EFC RID: 16124
		[MethodImpl(MethodImplOptions.InternalCall)]
		void InitializeSystem([MarshalAs(UnmanagedType.BStr)] [In] string apo_path);
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DTS_APO_ControllerLib
{
	// Token: 0x02000A53 RID: 2643
	[CompilerGenerated]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("AF721805-05A3-4D9C-9946-06999051168F")]
	[TypeIdentifier]
	[ComImport]
	public interface IDTSEndpointEnumerator
	{
		// Token: 0x06003EFD RID: 16125
		void _VtblGap1_2();

		// Token: 0x06003EFE RID: 16126
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		DTSCOMEndpointInfo GetEndpointFromDevice([MarshalAs(UnmanagedType.BStr)] [In] string device_id);
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DTS_APO_ControllerLib
{
	// Token: 0x02000A54 RID: 2644
	[CompilerGenerated]
	[Guid("6F079AA4-8604-4260-9F29-525B3124350B")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[TypeIdentifier]
	[ComImport]
	public interface IDTSEndpointInfo
	{
		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06003EFF RID: 16127
		[DispId(1610678272)]
		string FriendlyName { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x06003F00 RID: 16128
		void _VtblGap1_2();

		// Token: 0x06003F01 RID: 16129
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: MarshalAs(UnmanagedType.Interface)]
		DTSAPOOpModeEnumerator GetOpModeEnumerator();

		// Token: 0x06003F02 RID: 16130
		void _VtblGap2_1();

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06003F04 RID: 16132
		// (set) Token: 0x06003F03 RID: 16131
		[DispId(1610678277)]
		DTSOperatingMode OpMode { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; [MethodImpl(MethodImplOptions.InternalCall)] [param: MarshalAs(UnmanagedType.Interface)] [param: In] set; }

		// Token: 0x06003F05 RID: 16133
		[MethodImpl(MethodImplOptions.InternalCall)]
		DTSAPOEndpointConfig GetConfig();

		// Token: 0x06003F06 RID: 16134
		[MethodImpl(MethodImplOptions.InternalCall)]
		void SetConfig([In] DTSAPOEndpointConfig config);
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DTS_APO_ControllerLib
{
	// Token: 0x02000A55 RID: 2645
	[CompilerGenerated]
	[Guid("7D388587-DE2F-404C-AF92-29CE8D367A75")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[TypeIdentifier]
	[ComImport]
	public interface IDTSEndpointInfo2 : IDTSEndpointInfo
	{
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DTS_APO_ControllerLib
{
	// Token: 0x02000A56 RID: 2646
	[CompilerGenerated]
	[Guid("2E2A0698-02F5-4A0B-A8B6-C9525C275FF8")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[TypeIdentifier]
	[ComImport]
	public interface IDTSEndpointInfo3 : IDTSEndpointInfo2
	{
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DTS_APO_ControllerLib
{
	// Token: 0x02000A57 RID: 2647
	[CompilerGenerated]
	[Guid("93D5CED3-DB72-475E-9EA7-AEAED0BC2AA2")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[TypeIdentifier]
	[ComImport]
	public interface IDTSOperatingMode
	{
		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06003F07 RID: 16135
		[DispId(1610678272)]
		DTSAPOControlEnumerator DTSAPOControlEnumerator { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.Interface)] get; }

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06003F08 RID: 16136
		[DispId(1610678273)]
		string Name { [MethodImpl(MethodImplOptions.InternalCall)] [return: MarshalAs(UnmanagedType.BStr)] get; }

		// Token: 0x06003F09 RID: 16137
		[MethodImpl(MethodImplOptions.InternalCall)]
		void RestoreDefaults();

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06003F0A RID: 16138
		// (set) Token: 0x06003F0B RID: 16139
		[DispId(1610678275)]
		int APOEnable { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] [param: In] set; }

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06003F0C RID: 16140
		// (set) Token: 0x06003F0D RID: 16141
		[DispId(1610678277)]
		int AutoContentModeEnable { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] [param: In] set; }

		// Token: 0x06003F0E RID: 16142
		void _VtblGap1_4();

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06003F0F RID: 16143
		// (set) Token: 0x06003F10 RID: 16144
		[DispId(1610678283)]
		int SpatialEnable { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] [param: In] set; }
	}
}```
