```csharp
using System;

namespace NGenuity2.Devices
{
	// Token: 0x02000640 RID: 1600
	public class APO
	{
		// Token: 0x04001BF2 RID: 7154
		public const int DSP_MODE_UNSET = 0;

		// Token: 0x04001BF3 RID: 7155
		public const int DSP_MODE_MOVIE = 1;

		// Token: 0x04001BF4 RID: 7156
		public const int DSP_MODE_MUSIC = 2;

		// Token: 0x04001BF5 RID: 7157
		public const int DSP_MODE_VOICE = 3;

		// Token: 0x04001BF6 RID: 7158
		public const int DSP_MODE_GAME = 4;

		// Token: 0x04001BF7 RID: 7159
		public const int EQ_FILTER_PEAKING = 0;

		// Token: 0x04001BF8 RID: 7160
		public const int EQ_FILTER_LOW_SHELF = 1;

		// Token: 0x04001BF9 RID: 7161
		public const int EQ_FILTER_HIGH_SHELF = 2;

		// Token: 0x04001BFA RID: 7162
		public const int EQ_FILTER_LOW_PASS = 3;

		// Token: 0x04001BFB RID: 7163
		public const int EQ_FILTER_HIGH_PASS = 4;

		// Token: 0x04001BFC RID: 7164
		public const int EQ_FILTER_ALL_PASS = 5;
	}
}

using System;

namespace NGenuity2.Devices
{
	// Token: 0x02000658 RID: 1624
	public class AudioDevice
	{
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060020BF RID: 8383 RVA: 0x000598C1 File Offset: 0x00057AC1
		// (set) Token: 0x060020C0 RID: 8384 RVA: 0x000598C9 File Offset: 0x00057AC9
		public string DeviceId { get; set; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060020C1 RID: 8385 RVA: 0x000598D2 File Offset: 0x00057AD2
		// (set) Token: 0x060020C2 RID: 8386 RVA: 0x000598DA File Offset: 0x00057ADA
		public bool Muted { get; set; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060020C3 RID: 8387 RVA: 0x000598E3 File Offset: 0x00057AE3
		// (set) Token: 0x060020C4 RID: 8388 RVA: 0x000598EB File Offset: 0x00057AEB
		public float Volume { get; set; }
	}
}

using System;

namespace NGenuity2.Devices
{
	// Token: 0x02000641 RID: 1601
	public enum AudioDeviceType
	{
		// Token: 0x04001BFE RID: 7166
		None,
		// Token: 0x04001BFF RID: 7167
		Sound,
		// Token: 0x04001C00 RID: 7168
		Microphone,
		// Token: 0x04001C01 RID: 7169
		Sidetone
	}
}

using System;
using System.Linq;
using System.Threading.Tasks;
using NGenuity2.Common.Commands.Devices;
using NGenuity2.Devices.UVC.DirectShow;
using Windows.Foundation.Collections;

namespace NGenuity2.Devices
{
	// Token: 0x0200066B RID: 1643
	internal class CameraSettingCommnad : HyperXDeviceCommand
	{
		// Token: 0x06002185 RID: 8581 RVA: 0x00060600 File Offset: 0x0005E800
		protected override Task<ValueSet> RunAsync()
		{
			UVCDeviceBase uvcdeviceBase = HyperXCenter.Center.FindDevice(base.DeviceId) as UVCDeviceBase;
			if (uvcdeviceBase == null)
			{
				return base.RunAsync();
			}
			HyperXDeviceCommandType target = base.Target;
			if (target != HyperXDeviceCommandType.CameraValSetting)
			{
				if (target == HyperXDeviceCommandType.VideoValSetting)
				{
					uvcdeviceBase.SetValue((Win32API.VideoProcAmpProperty)this.Args.ElementAt(0), (int)this.Args.ElementAt(1));
				}
			}
			else
			{
				uvcdeviceBase.SetValue((Win32API.CameraControlProperty)this.Args.ElementAt(0), (int)this.Args.ElementAt(1));
			}
			return base.RunAsync();
		}
	}
}

using System;

namespace NGenuity2.Devices
{
	// Token: 0x02000642 RID: 1602
	public enum ChargingStatus
	{
		// Token: 0x04001C03 RID: 7171
		NoCharging,
		// Token: 0x04001C04 RID: 7172
		WireCharging,
		// Token: 0x04001C05 RID: 7173
		WirelessCharging,
		// Token: 0x04001C06 RID: 7174
		FullCharged,
		// Token: 0x04001C07 RID: 7175
		ChargeError
	}
}

using System;

namespace NGenuity2.Devices
{
	// Token: 0x02000643 RID: 1603
	public enum CompositeDeviceDirection
	{
		// Token: 0x04001C09 RID: 7177
		Right = 1,
		// Token: 0x04001C0A RID: 7178
		Left,
		// Token: 0x04001C0B RID: 7179
		Top = 4,
		// Token: 0x04001C0C RID: 7180
		Bottom = 8,
		// Token: 0x04001C0D RID: 7181
		Auto = 16
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NGenuity2.Devices
{
	// Token: 0x0200066C RID: 1644
	public class DevicePropertyKey
	{
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06002187 RID: 8583 RVA: 0x000606A2 File Offset: 0x0005E8A2
		// (set) Token: 0x06002188 RID: 8584 RVA: 0x000606AA File Offset: 0x0005E8AA
		public Win32API.SPRDP Property { get; set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06002189 RID: 8585 RVA: 0x000606B3 File Offset: 0x0005E8B3
		// (set) Token: 0x0600218A RID: 8586 RVA: 0x000606BB File Offset: 0x0005E8BB
		public Dictionary<string, object> Values { get; private set; } = new Dictionary<string, object>();

		// Token: 0x0600218B RID: 8587 RVA: 0x000606C4 File Offset: 0x0005E8C4
		internal void AddValue(string devicePath, IntPtr pBuffer, int outputSize, Win32API.RegistryDataType dataType)
		{
			object obj = null;
			switch (dataType)
			{
			case Win32API.RegistryDataType.SZ:
			case Win32API.RegistryDataType.EXPAND_SZ:
				obj = Marshal.PtrToStringAnsi(pBuffer);
				break;
			case Win32API.RegistryDataType.BINARY:
				obj = new byte[outputSize];
				Marshal.Copy(pBuffer, (byte[])obj, 0, outputSize);
				break;
			}
			this.Values.Add(devicePath, obj);
		}
	}
}

using System;

namespace NGenuity2.Devices
{
	// Token: 0x02000644 RID: 1604
	public enum DramType
	{
		// Token: 0x04001C0F RID: 7183
		Onboard,
		// Token: 0x04001C10 RID: 7184
		Predator,
		// Token: 0x04001C11 RID: 7185
		Fury
	}
}

using System;

namespace NGenuity2.Devices
{
	// Token: 0x02000659 RID: 1625
	public class DriverInfo
	{
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060020C6 RID: 8390 RVA: 0x000598F4 File Offset: 0x00057AF4
		// (set) Token: 0x060020C7 RID: 8391 RVA: 0x000598FC File Offset: 0x00057AFC
		public string FileName { get; set; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060020C8 RID: 8392 RVA: 0x00059905 File Offset: 0x00057B05
		// (set) Token: 0x060020C9 RID: 8393 RVA: 0x0005990D File Offset: 0x00057B0D
		public Version Version { get; set; }
	}
}

using System;

namespace NGenuity2.Devices
{
	// Token: 0x02000655 RID: 1621
	// (Invoke) Token: 0x060020A9 RID: 8361
	public delegate void FirmwareProgressHandler(HyperXDeviceModel model, UpdateFirmwareState state, int progress);
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32.SafeHandles;
using NGenuity2.Common;
using Windows.Foundation;

namespace NGenuity2.Devices
{
	// Token: 0x0200066D RID: 1645
	public sealed class HidDevice : IDisposable
	{
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x0600218D RID: 8589 RVA: 0x00060748 File Offset: 0x0005E948
		public bool IsValid
		{
			get
			{
				return this.hDev != null || this.hDevEvent != null;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x0600218E RID: 8590 RVA: 0x0006075D File Offset: 0x0005E95D
		public int FeatureReportByteLength
		{
			get
			{
				return (int)this.caps.FeatureReportByteLength;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x0600218F RID: 8591 RVA: 0x0006076A File Offset: 0x0005E96A
		public int OutputReportByteLength
		{
			get
			{
				return (int)this.caps.OutputReportByteLength;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06002190 RID: 8592 RVA: 0x00060777 File Offset: 0x0005E977
		public int InputReportByteLength
		{
			get
			{
				return (int)this.caps.InputReportByteLength;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06002191 RID: 8593 RVA: 0x00060784 File Offset: 0x0005E984
		public ushort UsageId
		{
			get
			{
				return this.caps.Usage;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06002192 RID: 8594 RVA: 0x00060791 File Offset: 0x0005E991
		public ushort UsagePage
		{
			get
			{
				return this.caps.UsagePage;
			}
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x000607A0 File Offset: 0x0005E9A0
		private void OpenThread(object state)
		{
			AutoResetEvent autoResetEvent = (AutoResetEvent)state;
			try
			{
				this.hDev = this.Open(this.devicePath, this.access);
				this.hDevEvent = this.Open(this.devicePath, this.eventAccess);
				this.devicePath.IndexOf("16ea");
				if (this.hDev == null && this.hDevEvent == null)
				{
					throw new NotSupportedException();
				}
				SafeFileHandle safeFileHandle = this.hDev;
				if (safeFileHandle != null && !safeFileHandle.IsInvalid)
				{
					this.GetDeviceInfo(this.hDev);
				}
				else
				{
					SafeFileHandle safeFileHandle2 = this.hDevEvent;
					if (safeFileHandle2 != null && !safeFileHandle2.IsInvalid)
					{
						this.GetDeviceInfo(this.hDevEvent);
					}
				}
				this.inited = true;
			}
			catch (Exception ex)
			{
				Logger.WriteLine(ex.Message ?? "", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\HidDevice.cs", 84);
			}
			autoResetEvent.Set();
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x00060894 File Offset: 0x0005EA94
		public HidDevice(string path, uint access = 3221225472U, uint eventAccess = 2147483648U)
		{
			this.access = access;
			this.eventAccess = eventAccess;
			this.devicePath = path;
			this.inited = false;
			this.autoEvent = new AutoResetEvent(false);
			new Thread(new ParameterizedThreadStart(this.OpenThread)).Start(this.autoEvent);
			this.autoEvent.WaitOne(2000);
			if (!this.inited)
			{
				throw new Exception("Failed to open Hid device " + path);
			}
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x00060918 File Offset: 0x0005EB18
		private void GetDeviceInfo(SafeFileHandle hDevice)
		{
			IntPtr zero = IntPtr.Zero;
			Win32API.HIDD_ATTRIBUTES hidd_ATTRIBUTES = default(Win32API.HIDD_ATTRIBUTES);
			hidd_ATTRIBUTES.Size = (uint)Marshal.SizeOf<Win32API.HIDD_ATTRIBUTES>(hidd_ATTRIBUTES);
			if (Win32API.HidD_GetAttributes(hDevice, ref hidd_ATTRIBUTES))
			{
				this.Version = hidd_ATTRIBUTES.VersionNumber;
				this.VendorId = hidd_ATTRIBUTES.VendorID;
				this.ProductId = hidd_ATTRIBUTES.ProductID;
			}
			Win32API.HidD_GetPreparsedData(hDevice, ref zero);
			Win32API.HidP_GetCaps(zero, ref this.caps);
			Win32API.HidD_FreePreparsedData(zero);
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x0006098C File Offset: 0x0005EB8C
		private SafeFileHandle Open(string devicePath, uint access)
		{
			SafeFileHandle safeFileHandle = Win32API.CreateFile(devicePath, access, 3U, IntPtr.Zero, 3U, 0U, IntPtr.Zero);
			if (safeFileHandle.IsInvalid)
			{
				safeFileHandle.Dispose();
				return null;
			}
			return safeFileHandle;
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x000609C1 File Offset: 0x0005EBC1
		public byte[] GetInputReport()
		{
			return this.GetInputReport(0);
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x000609CC File Offset: 0x0005EBCC
		public byte[] GetInputReport(byte reportId)
		{
			byte[] array = new byte[(int)this.caps.InputReportByteLength];
			array[0] = reportId;
			if (!Win32API.HidD_GetInputReport(this.hDev, array, array.Length))
			{
				if (this.debug)
				{
					Console.WriteLine("Failed to get input report");
				}
				throw new NotSupportedException();
			}
			return array;
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x00060A18 File Offset: 0x0005EC18
		public byte[] ReadInputReport(byte reportId, int timeout)
		{
			if (this.fsRead == null)
			{
				this.fsRead = new FileStream(this.hDev, FileAccess.Read, (int)this.caps.OutputReportByteLength, false);
			}
			byte[] array = new byte[(int)this.caps.InputReportByteLength];
			array[0] = reportId;
			this.fsRead.Read(array, 0, array.Length);
			return array;
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x00060A72 File Offset: 0x0005EC72
		public byte[] GetFeatureReport()
		{
			return this.GetFeatureReport(0);
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x00060A7B File Offset: 0x0005EC7B
		public byte[] GetFeatureReport(byte reportId)
		{
			return this.GetFeatureReport(new byte[]
			{
				reportId
			});
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x00060A90 File Offset: 0x0005EC90
		public byte[] GetFeatureReport(byte[] reportBuffer)
		{
			byte[] array = new byte[(int)this.caps.FeatureReportByteLength];
			Array.Copy(reportBuffer, array, Math.Min(reportBuffer.Length, array.Length));
			if (!Win32API.HidD_GetFeature(this.hDev, array, array.Length))
			{
				if (this.debug)
				{
					Console.WriteLine("Failed to get feature report");
				}
				throw new Exception();
			}
			return array;
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x00060AEA File Offset: 0x0005ECEA
		public byte[] CreateOutputReportBuffer()
		{
			return new byte[(int)this.caps.OutputReportByteLength];
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x00060AFC File Offset: 0x0005ECFC
		public byte[] CreateFeatureReportBuffer()
		{
			return new byte[(int)this.caps.FeatureReportByteLength];
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x00060B10 File Offset: 0x0005ED10
		public bool SetOutputReport(byte[] buffer)
		{
			byte[] array = buffer;
			if (buffer.Length != (int)this.caps.OutputReportByteLength)
			{
				array = new byte[(int)this.caps.OutputReportByteLength];
				Array.Copy(buffer, array, Math.Min(buffer.Length, array.Length));
			}
			if (this.use_hidd_setoutput)
			{
				return Win32API.HidD_SetOutputReport(this.hDev, array, array.Length);
			}
			if (this.fsWrite == null)
			{
				this.fsWrite = new FileStream(this.hDev, FileAccess.Write, (int)this.caps.OutputReportByteLength, false);
			}
			try
			{
				this.fsWrite.Write(array, 0, array.Length);
			}
			catch (Exception ex)
			{
				if (Win32API.HidD_SetOutputReport(this.hDev, array, array.Length))
				{
					this.use_hidd_setoutput = true;
					return true;
				}
				if (this.debug)
				{
					Console.WriteLine(ex);
				}
				throw ex;
			}
			return true;
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x00060BE4 File Offset: 0x0005EDE4
		public bool SetFeatureReport(byte[] buffer)
		{
			byte[] array = buffer;
			if (buffer.Length != (int)this.caps.FeatureReportByteLength)
			{
				array = new byte[(int)this.caps.FeatureReportByteLength];
				Array.Copy(buffer, array, Math.Min(buffer.Length, array.Length));
			}
			bool flag = Win32API.HidD_SetFeature(this.hDev, array, array.Length);
			if (!flag)
			{
				Console.WriteLine(string.Format("Failed to HidD_SetFeature error code: {0}", Marshal.GetLastWin32Error()));
			}
			return flag;
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x00060C54 File Offset: 0x0005EE54
		public void Close()
		{
			lock (this)
			{
				try
				{
					SafeFileHandle safeFileHandle = this.hDev;
					if (safeFileHandle != null)
					{
						safeFileHandle.Dispose();
					}
				}
				catch (Exception)
				{
				}
				try
				{
					SafeFileHandle safeFileHandle2 = this.hDevEvent;
					if (safeFileHandle2 != null)
					{
						safeFileHandle2.Dispose();
					}
				}
				catch (Exception)
				{
				}
				try
				{
					FileStream fileStream = this.fsEvent;
					if (fileStream != null)
					{
						fileStream.Dispose();
					}
				}
				catch (Exception)
				{
				}
				try
				{
					FileStream fileStream2 = this.fsWrite;
					if (fileStream2 != null)
					{
						fileStream2.Dispose();
					}
					FileStream fileStream3 = this.fsRead;
					if (fileStream3 != null)
					{
						fileStream3.Dispose();
					}
				}
				catch (Exception)
				{
				}
				this.fsEvent = null;
				this.fsRead = null;
				this.fsWrite = null;
				this.hDev = null;
				this.hDevEvent = null;
			}
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x00060D48 File Offset: 0x0005EF48
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x00060D53 File Offset: 0x0005EF53
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x00060D5C File Offset: 0x0005EF5C
		public static List<string> FindDevice(string pattern, ref Guid classGuid, Win32API.DiGetClassFlags flags, List<DevicePropertyKey> properties = null)
		{
			List<string> list = new List<string>();
			IntPtr intPtr = Win32API.SetupDiGetClassDevs(ref classGuid, null, IntPtr.Zero, flags);
			string value = null;
			uint num = 0U;
			for (;;)
			{
				Win32API.DeviceInfoData structure = default(Win32API.DeviceInfoData);
				structure.cbSize = Marshal.SizeOf<Win32API.DeviceInfoData>(structure);
				if (!Win32API.SetupDiEnumDeviceInfo(intPtr, num, ref structure))
				{
					break;
				}
				Win32API.DeviceInterfaceData structure2 = default(Win32API.DeviceInterfaceData);
				structure2.cbSize = Marshal.SizeOf<Win32API.DeviceInterfaceData>(structure2);
				if (Win32API.SetupDiEnumDeviceInterfaces(intPtr, 0U, ref classGuid, num, out structure2))
				{
					uint deviceInterfaceDetailDataSize = 0U;
					Win32API.SetupDiGetDeviceInterfaceDetail(intPtr, ref structure2, IntPtr.Zero, 0U, out deviceInterfaceDetailDataSize, IntPtr.Zero);
					Win32API.DeviceInterfaceDetailData deviceInterfaceDetailData = default(Win32API.DeviceInterfaceDetailData);
					if (Marshal.SizeOf<IntPtr>(IntPtr.Zero) == 8)
					{
						deviceInterfaceDetailData.cbSize = 8;
					}
					else
					{
						deviceInterfaceDetailData.cbSize = 4 + Marshal.SystemDefaultCharSize;
					}
					if (Win32API.SetupDiGetDeviceInterfaceDetail(intPtr, ref structure2, ref deviceInterfaceDetailData, deviceInterfaceDetailDataSize, out deviceInterfaceDetailDataSize, IntPtr.Zero))
					{
						string universalString = Utils.GetUniversalString(deviceInterfaceDetailData.DevicePath);
						if (Utils.IsCultureInvariantMatch(universalString, new string[]
						{
							pattern
						}))
						{
							if (properties != null)
							{
								IntPtr intPtr2 = Marshal.AllocHGlobal(1024);
								foreach (DevicePropertyKey devicePropertyKey in properties)
								{
									Win32API.RegistryDataType dataType;
									int outputSize;
									Win32API.SetupDiGetDeviceRegistryProperty(intPtr, ref structure, devicePropertyKey.Property, out dataType, intPtr2, 1024, out outputSize);
									devicePropertyKey.AddValue(universalString, intPtr2, outputSize, dataType);
								}
								Marshal.FreeHGlobal(intPtr2);
							}
							list.Add(universalString);
							Console.WriteLine(value);
						}
					}
				}
				num += 1U;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error != 259 && lastWin32Error == 1008)
			{
			}
			if (intPtr != IntPtr.Zero)
			{
				Win32API.SetupDiDestroyDeviceInfoList(intPtr);
			}
			return list;
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x00060F14 File Offset: 0x0005F114
		public static List<string> WaitAndGetHidDevicePathes(int defaultResponseTimeout, params string[] patterns)
		{
			DateTime t = DateTime.Now.AddMilliseconds((double)defaultResponseTimeout);
			List<string> list = HidDevice.FindHidDevice(patterns);
			if (list.Count == 0)
			{
				while (list.Count == 0 && t >= DateTime.Now)
				{
					Thread.Sleep(500);
					list = HidDevice.FindHidDevice(patterns);
				}
			}
			return list;
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x00060F6C File Offset: 0x0005F16C
		public static List<string> FindHidDevice(params string[] patterns)
		{
			Guid empty = Guid.Empty;
			Win32API.HidD_GetHidGuid(out empty);
			List<string> list = new List<string>();
			for (int i = 0; i < patterns.Length; i++)
			{
				list = HidDevice.FindDevice(patterns[i], ref empty, Win32API.DiGetClassFlags.DIGCF_PRESENT | Win32API.DiGetClassFlags.DIGCF_DEVICEINTERFACE, null);
				if (list.Count > 0)
				{
					return list;
				}
			}
			return list;
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x00060FB8 File Offset: 0x0005F1B8
		public static HidDevice FromId(string deviceId, uint access = 3221225472U)
		{
			HidDevice hidDevice = new HidDevice(deviceId, access, 2147483648U);
			if (hidDevice.IsValid)
			{
				return hidDevice;
			}
			hidDevice.Dispose();
			return null;
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060021A8 RID: 8616 RVA: 0x00060FE5 File Offset: 0x0005F1E5
		// (set) Token: 0x060021A9 RID: 8617 RVA: 0x00060FED File Offset: 0x0005F1ED
		public ushort ProductId { get; private set; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060021AA RID: 8618 RVA: 0x00060FF6 File Offset: 0x0005F1F6
		// (set) Token: 0x060021AB RID: 8619 RVA: 0x00060FFE File Offset: 0x0005F1FE
		public ushort VendorId { get; private set; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060021AC RID: 8620 RVA: 0x00061007 File Offset: 0x0005F207
		// (set) Token: 0x060021AD RID: 8621 RVA: 0x0006100F File Offset: 0x0005F20F
		public ushort Version { get; private set; }

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x060021AE RID: 8622 RVA: 0x00061018 File Offset: 0x0005F218
		// (remove) Token: 0x060021AF RID: 8623 RVA: 0x00061050 File Offset: 0x0005F250
		public event TypedEventHandler<HidDevice, byte[]> _inputReportReceived;

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x060021B0 RID: 8624 RVA: 0x00061088 File Offset: 0x0005F288
		// (remove) Token: 0x060021B1 RID: 8625 RVA: 0x0006110C File Offset: 0x0005F30C
		public event TypedEventHandler<HidDevice, byte[]> InputReportReceived
		{
			add
			{
				if (this._inputReportReceived == null)
				{
					this.asyncReading = true;
					this.inputReport = new byte[(int)this.caps.InputReportByteLength];
					this.fsEvent = new FileStream(this.hDevEvent, FileAccess.Read, (int)this.caps.InputReportByteLength, false);
					this.fsEvent.BeginRead(this.inputReport, 0, this.inputReport.Length, new AsyncCallback(this.OnInputReportReceived), this.inputReport);
				}
				this._inputReportReceived += value;
			}
			remove
			{
				this._inputReportReceived -= value;
				if (this._inputReportReceived == null)
				{
					try
					{
						FileStream fileStream = this.fsEvent;
						if (fileStream != null)
						{
							fileStream.Dispose();
						}
					}
					catch (Exception value2)
					{
						Console.WriteLine(value2);
					}
					this.fsEvent = null;
					this.asyncReading = true;
				}
			}
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x00061160 File Offset: 0x0005F360
		private void OnInputReportReceived(IAsyncResult result)
		{
			try
			{
				FileStream fileStream = this.fsEvent;
				if (fileStream != null)
				{
					fileStream.EndRead(result);
				}
				if (this._inputReportReceived != null)
				{
					byte[] array = new byte[this.inputReport.Length];
					Array.Copy(this.inputReport, array, array.Length);
					if (this.debug)
					{
						Console.WriteLine(string.Format("Got input report {0}, fire event", array.Length));
					}
					try
					{
						TypedEventHandler<HidDevice, byte[]> inputReportReceived = this._inputReportReceived;
						if (inputReportReceived != null)
						{
							inputReportReceived.Invoke(this, array);
						}
					}
					catch (Exception value)
					{
						Console.WriteLine(value);
					}
					if (this.debug)
					{
						Console.WriteLine("event fired!");
					}
				}
				if (this.asyncReading && this.fsEvent != null && this.fsEvent.CanRead)
				{
					FileStream fileStream2 = this.fsEvent;
					if (fileStream2 != null)
					{
						fileStream2.BeginRead(this.inputReport, 0, this.inputReport.Length, new AsyncCallback(this.OnInputReportReceived), this.inputReport);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x04001E49 RID: 7753
		private SafeFileHandle hDev;

		// Token: 0x04001E4A RID: 7754
		private SafeFileHandle hDevEvent;

		// Token: 0x04001E4B RID: 7755
		private Win32API.HidP_Caps caps;

		// Token: 0x04001E4C RID: 7756
		private bool asyncReading;

		// Token: 0x04001E4D RID: 7757
		private FileStream fsEvent;

		// Token: 0x04001E4E RID: 7758
		private FileStream fsWrite;

		// Token: 0x04001E4F RID: 7759
		private FileStream fsRead;

		// Token: 0x04001E50 RID: 7760
		private byte[] inputReport;

		// Token: 0x04001E51 RID: 7761
		private string devicePath;

		// Token: 0x04001E52 RID: 7762
		private bool debug;

		// Token: 0x04001E53 RID: 7763
		private bool inited;

		// Token: 0x04001E54 RID: 7764
		private bool disposed;

		// Token: 0x04001E55 RID: 7765
		private AutoResetEvent autoEvent;

		// Token: 0x04001E56 RID: 7766
		private uint access;

		// Token: 0x04001E57 RID: 7767
		private uint eventAccess;

		// Token: 0x04001E58 RID: 7768
		private bool use_hidd_setoutput;
	}
}

using System;

namespace NGenuity2.Devices
{
	// Token: 0x0200065C RID: 1628
	public abstract class HXCommandBase
	{
		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060020CF RID: 8399 RVA: 0x00059916 File Offset: 0x00057B16
		// (set) Token: 0x060020D0 RID: 8400 RVA: 0x0005991E File Offset: 0x00057B1E
		public int Delay { get; set; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060020D1 RID: 8401 RVA: 0x00059927 File Offset: 0x00057B27
		// (set) Token: 0x060020D2 RID: 8402 RVA: 0x0005992F File Offset: 0x00057B2F
		public bool Skip { get; set; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060020D3 RID: 8403 RVA: 0x00059938 File Offset: 0x00057B38
		// (set) Token: 0x060020D4 RID: 8404 RVA: 0x00059940 File Offset: 0x00057B40
		public byte ProfileID { get; set; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060020D5 RID: 8405 RVA: 0x00059949 File Offset: 0x00057B49
		// (set) Token: 0x060020D6 RID: 8406 RVA: 0x00059951 File Offset: 0x00057B51
		public bool ResetLighting { get; set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060020D7 RID: 8407 RVA: 0x0005995A File Offset: 0x00057B5A
		// (set) Token: 0x060020D8 RID: 8408 RVA: 0x00059962 File Offset: 0x00057B62
		public bool Force { get; set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060020D9 RID: 8409 RVA: 0x0005996B File Offset: 0x00057B6B
		// (set) Token: 0x060020DA RID: 8410 RVA: 0x00059973 File Offset: 0x00057B73
		public bool Succeeded { get; set; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060020DB RID: 8411 RVA: 0x0005997C File Offset: 0x00057B7C
		// (set) Token: 0x060020DC RID: 8412 RVA: 0x00059984 File Offset: 0x00057B84
		public HXCommandHandler Handler { get; set; }

		// Token: 0x060020DD RID: 8413 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void Execute(HyperXDevice device)
		{
		}
	}
}

using System;
using System.Collections.Generic;

namespace NGenuity2.Devices
{
	// Token: 0x0200065E RID: 1630
	public sealed class HXCommandCollection<T> : HXCommandBase where T : HXCommandBase
	{
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060020E0 RID: 8416 RVA: 0x00059995 File Offset: 0x00057B95
		// (set) Token: 0x060020E1 RID: 8417 RVA: 0x0005999D File Offset: 0x00057B9D
		public List<T> Commands { get; private set; }

		// Token: 0x060020E2 RID: 8418 RVA: 0x000599A6 File Offset: 0x00057BA6
		public HXCommandCollection()
		{
			this.Commands = new List<T>();
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x000599BC File Offset: 0x00057BBC
		public override void Execute(HyperXDevice device)
		{
			if (this.Commands.Count > 0)
			{
				foreach (T t in this.Commands)
				{
					try
					{
						t.Execute(device);
					}
					catch (Exception ex)
					{
						throw ex;
					}
				}
			}
			if (base.Handler != null)
			{
				base.Handler(this, null);
			}
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x00059A48 File Offset: 0x00057C48
		public void AddCommand(T cmd)
		{
			this.Commands.Add(cmd);
		}
	}
}

using System;

namespace NGenuity2.Devices
{
	// Token: 0x0200065D RID: 1629
	public abstract class HXCommandDummyShowLighting : HXCommandBase
	{
	}
}

using System;

namespace NGenuity2.Devices
{
	// Token: 0x0200065A RID: 1626
	// (Invoke) Token: 0x060020CC RID: 8396
	public delegate void HXCommandHandler(HXCommandBase cmd, object info);
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using NGenuity2.Analytics;
using NGenuity2.Common;
using NGenuity2.Common.Commands.Devices;
using NGenuity2.Common.Communication;
using NGenuity2.Common.Devices;
using NGenuity2.Common.Monitors.Models;
using NGenuity2.Common.RPCFunctions;
using NGenuity2.Devices.Headset;
using NGenuity2.Devices.Keyboard;
using NGenuity2.Devices.Microphone;
using NGenuity2.Devices.Monitors;
using NGenuity2.Devices.Mouse;
using NGenuity2.Devices.Mousepad;
using NGenuity2.Devices.Universals;
using NGenuity2.Devices.Universals.Headsets;
using NGenuity2.Devices.Universals.Keyboards;
using NGenuity2.Devices.Universals.Models;
using NGenuity2.Devices.Universals.Mouses;
using NGenuity2.Devices.Universals.ThreeInOneDongles;
using NGenuity2.Model;
using NGenuity2.Monitors;
using NGenuity2.PublisherCache;
using PublisherStorageLib.Models;
using Windows.Foundation;

namespace NGenuity2.Devices
{
	// Token: 0x0200065F RID: 1631
	public sealed class HyperXCenter
	{
		// Token: 0x1400002E RID: 46
		// (add) Token: 0x060020E5 RID: 8421 RVA: 0x00059A58 File Offset: 0x00057C58
		// (remove) Token: 0x060020E6 RID: 8422 RVA: 0x00059A90 File Offset: 0x00057C90
		public event TypedEventHandler<object, HyperXDevice> DeviceAdded;

		// Token: 0x1400002F RID: 47
		// (add) Token: 0x060020E7 RID: 8423 RVA: 0x00059AC8 File Offset: 0x00057CC8
		// (remove) Token: 0x060020E8 RID: 8424 RVA: 0x00059B00 File Offset: 0x00057D00
		public event TypedEventHandler<object, HyperXDevice> DeviceRemoved;

		// Token: 0x14000030 RID: 48
		// (add) Token: 0x060020E9 RID: 8425 RVA: 0x00059B38 File Offset: 0x00057D38
		// (remove) Token: 0x060020EA RID: 8426 RVA: 0x00059B70 File Offset: 0x00057D70
		public event TypedEventHandler<object, HyperXDevice> DeviceFirmwareUpdated;

		// Token: 0x14000031 RID: 49
		// (add) Token: 0x060020EB RID: 8427 RVA: 0x00059BA8 File Offset: 0x00057DA8
		// (remove) Token: 0x060020EC RID: 8428 RVA: 0x00059BE0 File Offset: 0x00057DE0
		public event TypedEventHandler<object, HyperXDevice> DeviceNameChanged;

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x060020ED RID: 8429 RVA: 0x00059C18 File Offset: 0x00057E18
		// (remove) Token: 0x060020EE RID: 8430 RVA: 0x00059C50 File Offset: 0x00057E50
		public event TypedEventHandler<object, HyperXDevice> DeviceUpdated;

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x060020EF RID: 8431 RVA: 0x00059C88 File Offset: 0x00057E88
		// (remove) Token: 0x060020F0 RID: 8432 RVA: 0x00059CC0 File Offset: 0x00057EC0
		public event TypedEventHandler<object, HyperXMonitorBase> MonitorAdded;

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x060020F1 RID: 8433 RVA: 0x00059CF8 File Offset: 0x00057EF8
		// (remove) Token: 0x060020F2 RID: 8434 RVA: 0x00059D30 File Offset: 0x00057F30
		public event TypedEventHandler<object, HyperXMonitorBase> MonitorRemoved;

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x060020F3 RID: 8435 RVA: 0x00059D68 File Offset: 0x00057F68
		// (remove) Token: 0x060020F4 RID: 8436 RVA: 0x00059DA0 File Offset: 0x00057FA0
		public event TypedEventHandler<object, HyperXMonitorBase> MonitorDataRefreshed;

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x060020F5 RID: 8437 RVA: 0x00059DD8 File Offset: 0x00057FD8
		// (remove) Token: 0x060020F6 RID: 8438 RVA: 0x00059E10 File Offset: 0x00058010
		public event TypedEventHandler<object, HyperXMonitorBase> MonitorOsdOn;

		// Token: 0x14000037 RID: 55
		// (add) Token: 0x060020F7 RID: 8439 RVA: 0x00059E48 File Offset: 0x00058048
		// (remove) Token: 0x060020F8 RID: 8440 RVA: 0x00059E80 File Offset: 0x00058080
		public event TypedEventHandler<object, List<ScreenMirrorInfo>> AllMirrorChanged;

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060020F9 RID: 8441 RVA: 0x00059EB8 File Offset: 0x000580B8
		public static HyperXCenter Center
		{
			get
			{
				if (HyperXCenter._center == null)
				{
					object centerGuard = HyperXCenter._centerGuard;
					lock (centerGuard)
					{
						if (HyperXCenter._center == null)
						{
							HyperXCenter._center = new HyperXCenter();
						}
					}
				}
				return HyperXCenter._center;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060020FA RID: 8442 RVA: 0x00059F10 File Offset: 0x00058110
		public HyperXCompositeDevice CompositeDevice
		{
			get
			{
				return this._compositeDevice;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060020FB RID: 8443 RVA: 0x00059F18 File Offset: 0x00058118
		// (set) Token: 0x060020FC RID: 8444 RVA: 0x00059F20 File Offset: 0x00058120
		public Updater Updater { get; set; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060020FD RID: 8445 RVA: 0x00059F29 File Offset: 0x00058129
		// (set) Token: 0x060020FE RID: 8446 RVA: 0x00059F36 File Offset: 0x00058136
		public bool UpgradeMode
		{
			get
			{
				return this.Updater.UpgradeMode;
			}
			set
			{
				this.Updater.UpgradeMode = value;
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060020FF RID: 8447 RVA: 0x00059F44 File Offset: 0x00058144
		public IReadOnlyList<ScreenMirrorInfo> AllMirrorInfo
		{
			get
			{
				return this._allMirrorInfo;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06002100 RID: 8448 RVA: 0x00059F4C File Offset: 0x0005814C
		public IReadOnlyList<HyperXDevice> Devices
		{
			get
			{
				return this._devices;
			}
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x00059F54 File Offset: 0x00058154
		public bool DeviceHasNewerVersion(int ver, ushort vid, ushort pid)
		{
			uint key = (uint)((int)vid << 16 | (int)pid);
			return this._embededFirmware.ContainsKey(key) && (int)this._embededFirmware[key] > ver;
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x00059F88 File Offset: 0x00058188
		private HyperXCenter()
		{
			this.Updater = new Updater();
			this._deviceQueue = new Queue<HyperXCenter.DeviceAction>();
			this._devices = new List<HyperXDevice>();
			this._dfuList = new List<string>();
			this._embededFirmware.Add(156309182U, 113);
			this._embededFirmware.Add(156309212U, 2126);
			this._embededFirmware.Add(156309221U, 2122);
			this._embededFirmware.Add(156309237U, 2122);
			this._embededFirmware.Add(66061713U, 2122);
			this._embededFirmware.Add(66061969U, 2122);
			this._embededFirmware.Add(156309222U, 2108);
			this._embededFirmware.Add(156309238U, 2108);
			this._embededFirmware.Add(156309265U, 2106);
			this._embededFirmware.Add(156309266U, 2106);
			this._embededFirmware.Add(66061711U, 2106);
			this._embededFirmware.Add(66061967U, 2106);
			this._embededFirmware.Add(156309300U, 2107);
			this._embededFirmware.Add(156309301U, 2107);
			this._embededFirmware.Add(66063502U, 2107);
			this._embededFirmware.Add(66063758U, 2107);
			this._embededFirmware.Add(66061199U, 2104);
			this._embededFirmware.Add(66061455U, 2104);
			this._embededFirmware.Add(156309334U, 2102);
			this._embededFirmware.Add(66063247U, 2102);
			this._embededFirmware.Add(66063503U, 2102);
			this._embededFirmware.Add(156309203U, 1127);
			this._embededFirmware.Add(156309220U, 1124);
			this._embededFirmware.Add(156309241U, 1124);
			this._embededFirmware.Add(156309214U, 1122);
			this._embededFirmware.Add(156309207U, 1123);
			this._embededFirmware.Add(156309289U, 1105);
			this._embededFirmware.Add(156309290U, 1105);
			this._embededFirmware.Add(156309218U, 1108);
			this._embededFirmware.Add(156309217U, 4104);
			this._embededFirmware.Add(156309240U, 1107);
			this._embededFirmware.Add(156309239U, 1107);
			this._embededFirmware.Add(156309287U, 1104);
			this._embededFirmware.Add(156309288U, 1104);
			this._embededFirmware.Add(66061454U, 1107);
			this._embededFirmware.Add(66060942U, 4106);
			this._embededFirmware.Add(66061710U, 1107);
			this._embededFirmware.Add(66061198U, 4106);
			this._embededFirmware.Add(156309226U, 4107);
			this._embededFirmware.Add(156309229U, 4110);
			this._embededFirmware.Add(156309269U, 4102);
			this._embededFirmware.Add(156309272U, 4100);
			this._embededHostRFFirmware.Add(156309272U, 4101);
			this._embededClientRFFirmware.Add(156309272U, 3101);
			this._embededFirmware.Add(66060683U, 4107);
			this._embededClientRFFirmware.Add(66061707U, 3107);
			this._embededFirmware.Add(66061974U, 4108);
			this._embededClientRFFirmware.Add(66061195U, 3108);
			this._embededFirmware.Add(66062218U, 4109);
			this._embededClientRFFirmware.Add(66062218U, 3105);
			this._embededHostRFFirmware.Add(66062218U, 6107);
			this._embededFirmware.Add(66062748U, 4103);
			this._embededFirmware.Add(66064060U, 4107);
			this._embededFirmware.Add(66062519U, 3109);
			this._embededFirmware.Add(66063517U, 4110);
			this._embededFirmware.Add(66061751U, 4109);
			this._embededFirmware.Add(66064302U, 4105);
			this._embededFirmware.Add(66062014U, 4124);
			this._embededFirmware.Add(66062493U, 4110);
			this._embededFirmware.Add(156309279U, 6103);
			this._embededFirmware.Add(66061452U, 6100);
			this._embededFirmware.Add(66060940U, 6103);
			this._embededFirmware.Add(66061964U, 6103);
			this._embededFirmware.Add(156309280U, 6103);
			this._embededFirmware.Add(156309364U, 6103);
			this._embededHostRFFirmware.Add(156309279U, 4101);
			this._embededHostRFFirmware.Add(66061452U, 4102);
			this._embededHostRFFirmware.Add(66060940U, 4101);
			this._embededHostRFFirmware.Add(66061964U, 4101);
			this._embededFirmware.Add(156309263U, 4112);
			this._embededFirmware.Add(66062219U, 4112);
			this._embededFirmware.Add(66061714U, 4104);
			this._embededFirmware.Add(66062731U, 4106);
			this._embededFirmware.Add(66062988U, 4109);
			this._embededFirmware.Add(66062732U, 6113);
			this._embededFirmware.Add(66062767U, 6107);
			this._embededFirmware.Add(66063023U, 6107);
			this._embededFirmware.Add(66062255U, 4107);
			this._embededFirmware.Add(66062260U, 4107);
			this._embededFirmware.Add(66060981U, 6113);
			this._embededFirmware.Add(66061237U, 6113);
			this._embededFirmware.Add(66063748U, 4105);
			this._embededFirmware.Add(66064052U, 4105);
			this._embededFirmware.Add(156309253U, 6101);
			this._embededFirmware.Add(156309254U, 6101);
			this._embededFirmware.Add(156309313U, 6104);
			this._embededFirmware.Add(156309314U, 6104);
			this._embededFirmware.Add(66063255U, 1111);
			this._embededFirmware.Add(66061493U, 1101);
			this._embededFirmware.Add(66063767U, 1115);
			this._embededFirmware.Add(66064280U, 4108);
			this._embededFirmware.Add(66063264U, 1111);
			this._embededFirmware.Add(66063776U, 4111);
			this._embededFirmware.Add(66063288U, 4107);
			this._embededFirmware.Add(66063032U, 1107);
			this._embededFirmware.Add(66064288U, 2108);
			this._embededFirmware.Add(66060961U, 2109);
			this._embededFirmware.Add(66063029U, 4103);
			this._embededClientRFFirmware.Add(66063029U, 1109);
			this._embededFirmware.Add(66060729U, 4110);
			this._embededFirmware.Add(66064056U, 2110);
			this._embededFirmware.Add(66064317U, 4110);
			this._embededClientRFFirmware.Add(66064317U, 1110);
			this._embededFirmware.Add(66063549U, 4122);
			this._embededFirmware.Add(66063805U, 4122);
			this._embededFirmware.Add(66063037U, 1132);
			this._embededFirmware.Add(66063293U, 1132);
			this._embededClientRFFirmware.Add(66063037U, 1132);
			this._embededClientRFFirmware.Add(66063293U, 1132);
			this._embededFirmware.Add(66061503U, 1132);
			this._embededFirmware.Add(66061759U, 1132);
			this._embededFirmware.Add(66062015U, 4122);
			this._embededFirmware.Add(66062271U, 4122);
			this._embededClientRFFirmware.Add(66061503U, 1132);
			this._embededClientRFFirmware.Add(66061759U, 1132);
			this._embededFirmware.Add(66063041U, 4112);
			this._embededFirmware.Add(66060735U, 4119);
			this._embededFirmware.Add(66060741U, 4119);
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x0005A9C0 File Offset: 0x00058BC0
		internal void OnDeviceAdded(HyperXDevice device)
		{
			Settings.Instance.DetectDevice(device.Model);
			if (device != null && device.DeviceType == HyperXDeviceType.Headset)
			{
				this._headsetCount++;
				if (this._headsetCount == 1 && Preset.CurrentPreset.Headset.EQAutoOptimize)
				{
					AudioFunctions.StartHeadsetGameMonitoring();
				}
			}
			bool flag = Preset.BasePreset.OverridePreset(device);
			foreach (Preset preset in Preset.Presets)
			{
				flag |= preset.OverridePreset(device);
			}
			if (flag)
			{
				Preset.SaveAllUpdatedPresets();
			}
			if (this.DeviceAdded != null)
			{
				Logger.WriteLine(string.Format("DeviceAdded: OnDeviceAdded, Device: {0}", device.Model), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 339);
				this.DeviceAdded.Invoke(this, device);
			}
			Logger.WriteLine(string.Format("{0} Added.", device.Model), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 343);
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x0005AACC File Offset: 0x00058CCC
		internal void RefreshDevice(HyperXDevice device)
		{
			TypedEventHandler<object, HyperXDevice> deviceAdded = this.DeviceAdded;
			if (deviceAdded == null)
			{
				return;
			}
			deviceAdded.Invoke(this, device);
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x0005AAE0 File Offset: 0x00058CE0
		internal void OnDeviceUpdated(HyperXDevice device)
		{
			bool flag = false;
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				flag = this._devices.Contains(device);
			}
			if (flag)
			{
				TypedEventHandler<object, HyperXDevice> deviceUpdated = this.DeviceUpdated;
				if (deviceUpdated == null)
				{
					return;
				}
				deviceUpdated.Invoke(this, device);
			}
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x0005AB40 File Offset: 0x00058D40
		public void RemoveDeviceName(string name)
		{
			List<string> deviceNames = this._deviceNames;
			lock (deviceNames)
			{
				this._deviceNames.Remove(name);
			}
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x0005AB88 File Offset: 0x00058D88
		public string GetNewDeviceName(string baseName)
		{
			List<string> deviceNames = this._deviceNames;
			lock (deviceNames)
			{
				if (this._deviceNames.FirstOrDefault((string o) => string.Equals(baseName, o)) != null)
				{
					string newname = string.Empty;
					Func<string, bool> <>9__1;
					for (int i = 2; i < 9999; i++)
					{
						newname = string.Format("{0} {1}", baseName, i);
						IEnumerable<string> deviceNames2 = this._deviceNames;
						Func<string, bool> predicate;
						if ((predicate = <>9__1) == null)
						{
							predicate = (<>9__1 = ((string o) => string.Equals(newname, o)));
						}
						if (deviceNames2.FirstOrDefault(predicate) == null)
						{
							baseName = newname;
							break;
						}
					}
				}
				this._deviceNames.Add(baseName);
			}
			return baseName;
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x0005AC80 File Offset: 0x00058E80
		public bool HasNewerBuiltinFirmware(HyperXDevice device)
		{
			if (device == null)
			{
				return false;
			}
			if (device.UpgradeMode)
			{
				return true;
			}
			foreach (uint num in this._embededFirmware.Keys)
			{
				ushort vid = (ushort)(num >> 16);
				ushort pid = (ushort)num;
				if (device.HasNewerVersion(vid, pid, this._embededFirmware[num]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x0005AD08 File Offset: 0x00058F08
		public bool HasNewerFirmwareByUniversalDeviceInfo(HyperXDevice device)
		{
			IUniversalDevice universalDevice = device as IUniversalDevice;
			if (universalDevice != null)
			{
				DeviceInforPayload result = universalDevice.GetDeviceInformationAsync().Result;
				return device.HasNewerFirmwareByUniversalDeviceInfo(result);
			}
			return false;
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x0005AD34 File Offset: 0x00058F34
		public bool IsWirelessProductFirmwareAvailable(DeviceInforPayload deviceInformation)
		{
			if (deviceInformation != null && deviceInformation.IsWirelessProduct && deviceInformation.SourceState == HidSourceState.Primary)
			{
				uint key = (uint)(66060288 | (deviceInformation.SecondaryProductID & 65535));
				if (this._embededFirmware.ContainsKey(key))
				{
					ushort num = this._embededFirmware[key];
					ushort num2 = ushort.Parse(deviceInformation.SecondaryFirmwareVersion);
					if (num > num2)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x0005AD94 File Offset: 0x00058F94
		public Version GetMinimunMainFirmwareVersion(uint deviceId)
		{
			if (!this._embededFirmware.ContainsKey(deviceId))
			{
				return null;
			}
			ushort num = this._embededFirmware[deviceId];
			return new Version((int)(num / 1000), (int)(num % 1000 / 100), (int)(num % 100 / 10), (int)(num % 10));
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x0005ADE0 File Offset: 0x00058FE0
		public Version GetMinimunRFHostFirmwareVersion(uint deviceId)
		{
			if (!this._embededHostRFFirmware.ContainsKey(deviceId))
			{
				return null;
			}
			ushort num = this._embededHostRFFirmware[deviceId];
			return new Version((int)(num / 1000), (int)(num % 1000 / 100), (int)(num % 100 / 10), (int)(num % 10));
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x0005AE2C File Offset: 0x0005902C
		public Version GetMinimunRFClientFirmwareVersion(uint deviceId)
		{
			if (!this._embededClientRFFirmware.ContainsKey(deviceId))
			{
				return null;
			}
			ushort num = this._embededClientRFFirmware[deviceId];
			return new Version((int)(num / 1000), (int)(num % 1000 / 100), (int)(num % 100 / 10), (int)(num % 10));
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x0005AE78 File Offset: 0x00059078
		public ushort? GetMinimunRFClientFirmwareVersions(uint pidVidNumber)
		{
			if (!this._embededClientRFFirmware.ContainsKey(pidVidNumber))
			{
				return null;
			}
			return new ushort?(this._embededClientRFFirmware[pidVidNumber]);
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600210F RID: 8463 RVA: 0x0005AEAE File Offset: 0x000590AE
		// (set) Token: 0x06002110 RID: 8464 RVA: 0x0005AEB6 File Offset: 0x000590B6
		public HyperXDevice CurrentDevice { get; set; }

		// Token: 0x06002111 RID: 8465 RVA: 0x0005AEC0 File Offset: 0x000590C0
		public void ApplyPresetToAllDevices(Preset preset)
		{
			if (preset == null)
			{
				Console.WriteLine("Warning!!! Applying null preset");
				return;
			}
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				foreach (HyperXDevice hyperXDevice in this._devices)
				{
					if (hyperXDevice.DeviceType == HyperXDeviceType.Keyboard || hyperXDevice.DeviceType == HyperXDeviceType.Mouse || hyperXDevice.DeviceType == HyperXDeviceType.DRAM || hyperXDevice.DeviceType == HyperXDeviceType.Headset || hyperXDevice.DeviceType == HyperXDeviceType.Mousepad || hyperXDevice.DeviceType == HyperXDeviceType.Microphone || hyperXDevice.DeviceType == HyperXDeviceType.Composite)
					{
						hyperXDevice.SetupPreset(preset);
					}
				}
			}
			if (preset.Headset.EQAutoOptimize)
			{
				if (this._headsetCount > 0)
				{
					AudioFunctions.StartHeadsetGameMonitoring();
					return;
				}
			}
			else
			{
				AudioFunctions.StopHeadsetGameMonitoring();
			}
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x0005AFB0 File Offset: 0x000591B0
		public void RemoveDevice(HyperXDevice device)
		{
			if (device == null)
			{
				return;
			}
			if (device.CanLink && device.Linked)
			{
				HyperXCompositeDevice compositeDevice = this._compositeDevice;
				if (compositeDevice != null)
				{
					compositeDevice.UnlinkDevice(device);
				}
			}
			bool flag = false;
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				flag = this._devices.Remove(device);
			}
			if (this.DeviceRemoved != null)
			{
				this.DeviceRemoved.Invoke(this, device);
			}
			if (flag)
			{
				List<string> deviceNames = this._deviceNames;
				lock (deviceNames)
				{
					for (int i = 0; i < this._deviceNames.Count; i++)
					{
						if (string.Equals(device.Name, this._deviceNames[i]))
						{
							this._deviceNames.RemoveAt(i);
							break;
						}
					}
				}
				if (device.DeviceType == HyperXDeviceType.Headset)
				{
					this._headsetCount--;
				}
				if (this._headsetCount == 0)
				{
					AudioFunctions.StopHeadsetGameMonitoring();
				}
				Task.Factory.StartNew(delegate(object d)
				{
					((HyperXDevice)d).Stop(false);
				}, device);
				this.AddDeviceWhenRemove(device.DeviceID);
			}
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x0005B104 File Offset: 0x00059304
		public void OnDeviceFirmwareUpdated(HyperXDevice device)
		{
			if (this.DeviceFirmwareUpdated != null)
			{
				this.DeviceFirmwareUpdated.Invoke(this, device);
			}
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x0005B11C File Offset: 0x0005931C
		private void InitializeCommonDevice(HyperXDevice device, string id)
		{
			ushort vID;
			ushort pID;
			Utils.ParseVIDPID(id, out vID, out pID);
			device.SetVIDPID(vID, pID);
			this.CheckDeviceControlByWhom(device);
			device.OpenDevice(id);
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x0005B14C File Offset: 0x0005934C
		private void InitializeNotificationDevice(HyperXDevice device, string id)
		{
			ushort vID;
			ushort pID;
			Utils.ParseVIDPID(id, out vID, out pID);
			device.SetVIDPID(vID, pID);
			this.CheckDeviceControlByWhom(device);
			device.OpenNotification(id);
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x0005B17A File Offset: 0x0005937A
		private void InitializeSecondaryDevice(HyperXDevice device, string id)
		{
			this.CheckDeviceControlByWhom(device);
			device.OpenSecondaryDevice(id);
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x0005B18C File Offset: 0x0005938C
		private void AddDeviceSecondaryEndpoint<T>(string id) where T : HyperXDevice
		{
			if (this.Updater.UpgradeMode)
			{
				return;
			}
			Logger.WriteLine("Adding: " + id, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 659);
			T t = default(T);
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				t = (this._devices.FirstOrDefault((HyperXDevice o) => o.IsDevice(id)) as T);
			}
			if (t == null)
			{
				ConstructorInfo[] constructors = typeof(T).GetConstructors();
				ParameterInfo[] parameters = constructors[0].GetParameters();
				ConstructorInfo constructorInfo = constructors[0];
				object[] parameters2 = parameters;
				t = (T)((object)constructorInfo.Invoke(parameters2));
				this.InitializeSecondaryDevice(t, id);
				devices = this._devices;
				lock (devices)
				{
					this._devices.Add(t);
					return;
				}
			}
			this.InitializeSecondaryDevice(t, id);
			if (t.IsValidDevice)
			{
				if (t.ControlBy == HyperXDevice.ControlBySoftware.NGENUITY)
				{
					t.Start();
					t.SetupPreset(Preset.CurrentPreset);
					this.CheckDeviceLightSync(t);
				}
				this.OnDeviceAdded(t);
				return;
			}
			this.RemoveDeviceName(t.Name);
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x0005B31C File Offset: 0x0005951C
		private void AddMousePulsefireHaste2WirelessEndpoint(string deviceID, bool isDongle)
		{
			if (this.Updater.UpgradeMode)
			{
				return;
			}
			Logger.WriteLine("Adding: " + deviceID, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 714);
			MousePulsefireHaste2Wireless mousePulsefireHaste2Wireless = null;
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				mousePulsefireHaste2Wireless = (this._devices.FirstOrDefault((HyperXDevice o) => o.IsDevice(deviceID)) as MousePulsefireHaste2Wireless);
			}
			if (mousePulsefireHaste2Wireless == null)
			{
				mousePulsefireHaste2Wireless = new MousePulsefireHaste2Wireless(isDongle);
				ushort vID;
				ushort pID;
				Utils.ParseVIDPID(deviceID, out vID, out pID);
				mousePulsefireHaste2Wireless.SetVIDPID(vID, pID);
				mousePulsefireHaste2Wireless.OpenMousePulsefireHaste2Wireless(deviceID);
				if (!mousePulsefireHaste2Wireless.UpgradeMode || !this.HasNewerBuiltinFirmware(mousePulsefireHaste2Wireless))
				{
					mousePulsefireHaste2Wireless.OpenNotification(deviceID);
					mousePulsefireHaste2Wireless.UpdateID();
					mousePulsefireHaste2Wireless.SetupDevice();
				}
				if (mousePulsefireHaste2Wireless.IsValidDevice)
				{
					mousePulsefireHaste2Wireless.Start();
					mousePulsefireHaste2Wireless.SetupPreset(Preset.CurrentPreset);
					this.OnDeviceAdded(mousePulsefireHaste2Wireless);
				}
				else
				{
					this.RemoveDeviceName(mousePulsefireHaste2Wireless.Name);
				}
				devices = this._devices;
				lock (devices)
				{
					this._devices.Add(mousePulsefireHaste2Wireless);
				}
			}
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x0005B470 File Offset: 0x00059670
		private Task AddDeviceUniversalEndpoint<T>(string deviceID, bool inDFUmode) where T : HyperXDevice
		{
			HyperXCenter.<AddDeviceUniversalEndpoint>d__86<T> <AddDeviceUniversalEndpoint>d__;
			<AddDeviceUniversalEndpoint>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<AddDeviceUniversalEndpoint>d__.<>4__this = this;
			<AddDeviceUniversalEndpoint>d__.deviceID = deviceID;
			<AddDeviceUniversalEndpoint>d__.inDFUmode = inDFUmode;
			<AddDeviceUniversalEndpoint>d__.<>1__state = -1;
			<AddDeviceUniversalEndpoint>d__.<>t__builder.Start<HyperXCenter.<AddDeviceUniversalEndpoint>d__86<T>>(ref <AddDeviceUniversalEndpoint>d__);
			return <AddDeviceUniversalEndpoint>d__.<>t__builder.Task;
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x0005B4C4 File Offset: 0x000596C4
		private void AddDeviceNotificationEndpoint<T>(string id) where T : HyperXDevice
		{
			if (this.Updater.UpgradeMode)
			{
				return;
			}
			Logger.WriteLine("Adding: " + id, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 830);
			T t = default(T);
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				t = (this._devices.FirstOrDefault((HyperXDevice o) => o.IsDevice(id)) as T);
			}
			if (t == null)
			{
				ConstructorInfo[] constructors = typeof(T).GetConstructors();
				ParameterInfo[] parameters = constructors[0].GetParameters();
				ConstructorInfo constructorInfo = constructors[0];
				object[] parameters2 = parameters;
				t = (T)((object)constructorInfo.Invoke(parameters2));
				this.InitializeNotificationDevice(t, id);
				devices = this._devices;
				lock (devices)
				{
					this._devices.Add(t);
					return;
				}
			}
			this.InitializeNotificationDevice(t, id);
			if (t.IsValidDevice)
			{
				if (t.ControlBy == HyperXDevice.ControlBySoftware.NGENUITY)
				{
					t.Start();
					t.SetupPreset(Preset.CurrentPreset);
					this.CheckDeviceLightSync(t);
				}
				this.OnDeviceAdded(t);
				return;
			}
			this.RemoveDeviceName(t.Name);
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x0005B654 File Offset: 0x00059854
		private void AddDeviceMajorEndpoint<T>(string id) where T : HyperXDevice
		{
			if (this.Updater.UpgradeMode)
			{
				return;
			}
			Logger.WriteLine("Adding: " + id, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 886);
			T t = default(T);
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				t = (this._devices.FirstOrDefault((HyperXDevice o) => o.IsDevice(id)) as T);
			}
			if (t == null)
			{
				ConstructorInfo[] constructors = typeof(T).GetConstructors();
				ParameterInfo[] parameters = constructors[0].GetParameters();
				ConstructorInfo constructorInfo = constructors[0];
				object[] parameters2 = parameters;
				t = (T)((object)constructorInfo.Invoke(parameters2));
				this.InitializeCommonDevice(t, id);
				devices = this._devices;
				lock (devices)
				{
					this._devices.Add(t);
					return;
				}
			}
			this.InitializeCommonDevice(t, id);
			if (t.IsValidDevice)
			{
				if (t.ControlBy == HyperXDevice.ControlBySoftware.NGENUITY)
				{
					t.Start();
					t.SetupPreset(Preset.CurrentPreset);
					this.CheckDeviceLightSync(t);
				}
				this.OnDeviceAdded(t);
				return;
			}
			this.RemoveDeviceName(t.Name);
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x0005B7E4 File Offset: 0x000599E4
		private void AddDevice<T>(string id) where T : HyperXDevice
		{
			if (this.Updater.UpgradeMode)
			{
				return;
			}
			ConstructorInfo[] constructors = typeof(T).GetConstructors();
			ParameterInfo[] parameters = constructors[0].GetParameters();
			ConstructorInfo constructorInfo = constructors[0];
			object[] parameters2 = parameters;
			T t = (T)((object)constructorInfo.Invoke(parameters2));
			this.InitializeCommonDevice(t, id);
			if (t.IsValidDevice)
			{
				List<HyperXDevice> devices = this._devices;
				lock (devices)
				{
					this._devices.Add(t);
				}
				if (t.ControlBy == HyperXDevice.ControlBySoftware.NGENUITY)
				{
					t.Start();
					t.SetupPreset(Preset.CurrentPreset);
					this.CheckDeviceLightSync(t);
				}
				this.OnDeviceAdded(t);
				return;
			}
			this.RemoveDeviceName(t.Name);
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x0005B8D4 File Offset: 0x00059AD4
		private void AddDeviceDFUEndpoint<T>(string id) where T : HyperXDevice
		{
			if (this.Updater.UpgradeMode)
			{
				return;
			}
			Logger.WriteLine("Adding: " + id, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 972);
			ConstructorInfo[] constructors = typeof(T).GetConstructors();
			ParameterInfo[] parameters = constructors[0].GetParameters();
			ConstructorInfo constructorInfo = constructors[0];
			object[] parameters2 = parameters;
			T t = (T)((object)constructorInfo.Invoke(parameters2));
			this.InitializeCommonDevice(t, id);
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				this._devices.Add(t);
			}
			this.OnDeviceAdded(t);
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x0005B98C File Offset: 0x00059B8C
		internal void AddMonitorDevice(HyperXMonitorBase newMonitor)
		{
			object obj = this.monitorLocker;
			lock (obj)
			{
				if (this.monitors == null)
				{
					this.monitors = new List<HyperXMonitorBase>();
				}
				int num = this.monitors.FindIndex((HyperXMonitorBase x) => x.PhysicalData.DeviceID == newMonitor.PhysicalData.DeviceID);
				if (num == -1)
				{
					if (newMonitor.OsdData != null)
					{
						this.monitors.Add(newMonitor);
						TypedEventHandler<object, HyperXMonitorBase> monitorAdded = this.MonitorAdded;
						if (monitorAdded != null)
						{
							monitorAdded.Invoke(this, newMonitor);
						}
						Logger.WriteLine("AddMonitorDevice: " + newMonitor.PhysicalData.DeviceID, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 1010);
					}
				}
				else
				{
					this.monitors[num] = newMonitor;
				}
			}
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x0005BA78 File Offset: 0x00059C78
		internal void RemoveMonitorDevice(string deviceID)
		{
			object obj = this.monitorLocker;
			lock (obj)
			{
				if (this.monitors != null)
				{
					HyperXMonitorBase hyperXMonitorBase = this.monitors.FirstOrDefault((HyperXMonitorBase x) => x.PhysicalData.DeviceID == deviceID);
					if (hyperXMonitorBase != null)
					{
						this.monitors.Remove(hyperXMonitorBase);
						TypedEventHandler<object, HyperXMonitorBase> monitorRemoved = this.MonitorRemoved;
						if (monitorRemoved != null)
						{
							monitorRemoved.Invoke(this, hyperXMonitorBase);
						}
					}
				}
			}
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x0005BB04 File Offset: 0x00059D04
		public void AddTIOChildDevice(HyperXDevice device)
		{
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				this._devices.Add(device);
			}
			this.CheckDeviceLightSync(device);
			this.OnDeviceAdded(device);
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x0005BB58 File Offset: 0x00059D58
		public void RemoveTIOChildDevice(HyperXDevice device)
		{
			if (device.CanLink && device.Linked)
			{
				HyperXCompositeDevice compositeDevice = this._compositeDevice;
				if (compositeDevice != null)
				{
					compositeDevice.UnlinkDevice(device);
				}
			}
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				this._devices.Remove(device);
			}
			if (this.DeviceRemoved != null)
			{
				this.DeviceRemoved.Invoke(this, device);
			}
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x0005BBD8 File Offset: 0x00059DD8
		[MethodImpl(MethodImplOptions.Synchronized)]
		public void AddDevice(string id)
		{
			HyperXCenter.<>c__DisplayClass95_0 CS$<>8__locals1 = new HyperXCenter.<>c__DisplayClass95_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.id = id;
			Regex regex = new Regex("\\{[0-9a-f\\-]+\\}", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.CultureInvariant);
			try
			{
				Match match = regex.Match(CS$<>8__locals1.id);
				string did = match.Value;
				this.RemoveDeviceWhenAdd(CS$<>8__locals1.id);
				if (!this.DeivceNotAdd(CS$<>8__locals1.id))
				{
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_16BE&MI_02&Col05"
					}))
					{
						this.AddDeviceMajorEndpoint<KeyboardAlloyElite>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_16BE&MI_02&Col07"
					}))
					{
						this.AddDeviceNotificationEndpoint<KeyboardAlloyElite>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_16BE&MI_01&Col05"
					}))
					{
						this.AddDeviceDFUEndpoint<KeyboardAlloyElite>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_16DC&MI_02&Col05"
					}))
					{
						this.AddDeviceMajorEndpoint<KeyboardAlloyFPS>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_16DC&MI_02&Col07"
					}))
					{
						this.AddDeviceNotificationEndpoint<KeyboardAlloyFPS>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_16E5&MI_03",
						"HID#VID_03F0&PID_0591&MI_03"
					}))
					{
						this.AddDeviceMajorEndpoint<KeyboardAlloyOrigins>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_16F5",
						"HID#VID_03F0&PID_0691"
					}))
					{
						this.AddDeviceDFUEndpoint<KeyboardAlloyOrigins>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_16E5&MI_01&Col05",
						"HID#VID_03F0&PID_0591&MI_01&Col05"
					}))
					{
						this.AddDeviceNotificationEndpoint<KeyboardAlloyOrigins>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_1711&MI_03",
						"HID#VID_03F0&PID_058F&MI_03"
					}))
					{
						this.AddDeviceMajorEndpoint<KeyboardAlloyEliteII>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_1711&MI_01&Col05",
						"HID#VID_03F0&PID_058F&MI_01&Col05"
					}))
					{
						this.AddDeviceNotificationEndpoint<KeyboardAlloyEliteII>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_1712",
						"HID#VID_03F0&PID_068F"
					}))
					{
						this.AddDeviceDFUEndpoint<KeyboardAlloyEliteII>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_16E6&MI_02",
						"HID#VID_03F0&PID_098F&MI_02"
					}))
					{
						this.AddDevice<KeyboardAlloyOriginsCore>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_16F6",
						"HID#VID_03F0&PID_0A8F"
					}))
					{
						this.AddDeviceDFUEndpoint<KeyboardAlloyOriginsCore>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_1734&MI_03",
						"HID#VID_03F0&PID_0C8E&MI_03"
					}))
					{
						this.AddDeviceMajorEndpoint<KeyboardAlloyOrigins60>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_1734&MI_01&Col05",
						"HID#VID_03F0&PID_0C8E&MI_01&Col05"
					}))
					{
						this.AddDeviceNotificationEndpoint<KeyboardAlloyOrigins60>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_1735",
						"HID#VID_03F0&PID_0D8E"
					}))
					{
						this.AddDeviceDFUEndpoint<KeyboardAlloyOrigins60>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_038F&MI_03"
					}))
					{
						this.AddDeviceMajorEndpoint<KeyboardAlloyOrigins65>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_038F&MI_01&Col05"
					}))
					{
						this.AddDeviceNotificationEndpoint<KeyboardAlloyOrigins65>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_048F"
					}))
					{
						this.AddDeviceDFUEndpoint<KeyboardAlloyOrigins65>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_1756&MI_00",
						"HID#VID_03F0&PID_0B8F&MI_00",
						"HID#VID_03F0&PID_0C8F&MI_00"
					}))
					{
						this.AddDevice<KeyboardAlloyMKW100>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_16D3&MI_01&Col05",
						"HID#VID_03F0&PID_0490&MI_01&Col05"
					}))
					{
						this.AddDeviceMajorEndpoint<MousePulsefireSurge>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_16D3&MI_02",
						"HID#VID_03F0&PID_0490&MI_02"
					}))
					{
						this.AddDeviceNotificationEndpoint<MousePulsefireSurge>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_16E4&MI_01&Col05",
						"HID#VID_03F0&PID_0290&MI_01&Col05"
					}))
					{
						this.AddDeviceMajorEndpoint<MousePulsefireRaid>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_16E4&MI_02",
						"HID#VID_03F0&PID_0290&MI_02"
					}))
					{
						this.AddDeviceNotificationEndpoint<MousePulsefireRaid>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_16F9&MI_01&Col05",
						"HID#VID_03F0&PID_0390&MI_01&Col05"
					}))
					{
						this.AddDeviceDFUEndpoint<MousePulsefireRaid>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_16D7&MI_01&Col05",
						"HID#VID_03F0&PID_0E8F&MI_01&Col05"
					}))
					{
						this.AddDeviceMajorEndpoint<MousePulsefireFPSPro>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_16D7&MI_02",
						"HID#VID_03F0&PID_0E8F&MI_02"
					}))
					{
						this.AddDeviceNotificationEndpoint<MousePulsefireFPSPro>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_16DE&MI_01&Col05",
						"HID#VID_03F0&PID_0D8F&MI_01&Col05"
					}))
					{
						this.AddDeviceMajorEndpoint<MousePulsefireCore>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_16DE&MI_02",
						"HID#VID_03F0&PID_0D8F&MI_02"
					}))
					{
						this.AddDeviceNotificationEndpoint<MousePulsefireCore>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_16E1&MI_02",
						"HID#VID_03F0&PID_068E&MI_02"
					}) || Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_16E2&MI_01",
						"HID#VID_03F0&PID_088E&MI_01"
					}) || Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_078E&MI_01"
					}) || Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_16F7&MI_01"
					}) || Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_16F8&MI_01",
						"HID#VID_03F0&PID_098E&MI_01"
					}))
					{
						this.AddDevice<MousePulsefireDart>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_1729&MI_02",
						"HID#VID_03F0&PID_0693&MI_02"
					}) || Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_172A",
						"HID#VID_03F0&PID_0793"
					}))
					{
						this.AddDevice<MousePulsefireFPSProCD>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_1727&MI_03",
						"HID#VID_03F0&PID_0F8F&MI_03"
					}))
					{
						this.AddDeviceMajorEndpoint<MousePulsefireHaste>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_1727&MI_01&Col04",
						"HID#VID_03F0&PID_0F8F&MI_01&Col04"
					}))
					{
						this.AddDeviceNotificationEndpoint<MousePulsefireHaste>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_1728",
						"HID#VID_03F0&PID_0190"
					}))
					{
						this.AddDeviceDFUEndpoint<MousePulsefireHaste>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_028E&MI_02"
					}) || Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_038E&MI_01"
					}) || Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_048E&MI_02"
					}) || Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_058E&MI_01"
					}))
					{
						this.AddDevice<MousePulsefireHasteWireless>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_1705&MI_00",
						"HID#VID_03F0&PID_0493&MI_00"
					}) || Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_1706",
						"HID#VID_03F0&PID_0593"
					}))
					{
						this.AddDeviceMajorEndpoint<MousepadFURYUltra>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_1705&MI_01&Col02",
						"HID#VID_03F0&PID_0493&MI_01&Col02"
					}))
					{
						this.AddDeviceNotificationEndpoint<MousepadFURYUltra>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_1741&MI_01",
						"HID#VID_03F0&PID_0F8D&MI_01"
					}) || Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_1742",
						"HID#VID_03F0&PID_018E"
					}))
					{
						this.AddDevice<MousepadPulsefireMatRGB>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_16EA&MI_05&Col03"
					}))
					{
						this.AddDevice<HeadsetCloudFlightS>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_16ED&MI_05&Col03",
						"HID#VID_03F0&PID_0C91&MI_05&Col03"
					}))
					{
						ushort vID;
						ushort pID;
						Utils.ParseVIDPID(CS$<>8__locals1.id, out vID, out pID);
						try
						{
							using (HidDevice hidDevice = new HidDevice(CS$<>8__locals1.id, 3221225472U, 2147483648U))
							{
								if (hidDevice.Version < 16644)
								{
									return;
								}
							}
						}
						catch (Exception ex)
						{
							throw ex;
						}
						HeadsetCloudAlphaS headsetCloudAlphaS = null;
						List<HyperXDevice> devices = this._devices;
						lock (devices)
						{
							headsetCloudAlphaS = (this._devices.FirstOrDefault(delegate(HyperXDevice o)
							{
								if (!string.IsNullOrEmpty(o.DeviceID) && Utils.IsCultureInvariantMatch(o.DeviceID, new string[]
								{
									"HID#VID_0951&PID_16ED&MI_05&Col01"
								}))
								{
									string deviceID = o.DeviceID;
									return deviceID != null && deviceID.IndexOf(did) > -1;
								}
								return false;
							}) as HeadsetCloudAlphaS);
						}
						if (headsetCloudAlphaS != null)
						{
							headsetCloudAlphaS.SetVIDPID(vID, pID);
							headsetCloudAlphaS.OpenDevice(CS$<>8__locals1.id);
							this.OnDeviceAdded(headsetCloudAlphaS);
							headsetCloudAlphaS.Start();
							return;
						}
						headsetCloudAlphaS = new HeadsetCloudAlphaS();
						headsetCloudAlphaS.SetVIDPID(vID, pID);
						headsetCloudAlphaS.OpenDevice(CS$<>8__locals1.id);
						if (headsetCloudAlphaS.IsValidDevice)
						{
							devices = this._devices;
							lock (devices)
							{
								this._devices.Add(headsetCloudAlphaS);
							}
							headsetCloudAlphaS.Start();
							this.OnDeviceAdded(headsetCloudAlphaS);
							headsetCloudAlphaS.SetupPreset(Preset.CurrentPreset);
						}
						else
						{
							this.RemoveDeviceName(headsetCloudAlphaS.Name);
						}
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_16ED&MI_05&Col01"
					}))
					{
						ushort vID2;
						ushort pID2;
						Utils.ParseVIDPID(CS$<>8__locals1.id, out vID2, out pID2);
						try
						{
							using (HidDevice hidDevice2 = new HidDevice(CS$<>8__locals1.id, 3221225472U, 2147483648U))
							{
								if (hidDevice2.Version >= 16644)
								{
									return;
								}
							}
						}
						catch (Exception ex2)
						{
							throw ex2;
						}
						HeadsetCloudAlphaS headsetCloudAlphaS2 = null;
						List<HyperXDevice> devices = this._devices;
						lock (devices)
						{
							headsetCloudAlphaS2 = (this._devices.FirstOrDefault(delegate(HyperXDevice o)
							{
								if (!string.IsNullOrEmpty(o.DeviceID) && Utils.IsCultureInvariantMatch(o.DeviceID, new string[]
								{
									"HID#VID_0951&PID_16ED&MI_05&Col03",
									"HID#VID_03F0&PID_0C91&MI_05&Col03"
								}))
								{
									string deviceID = o.DeviceID;
									return deviceID != null && deviceID.IndexOf(did) > -1;
								}
								return false;
							}) as HeadsetCloudAlphaS);
						}
						if (headsetCloudAlphaS2 != null)
						{
							return;
						}
						headsetCloudAlphaS2 = new HeadsetCloudAlphaS();
						headsetCloudAlphaS2.SetVIDPID(vID2, pID2);
						headsetCloudAlphaS2.OpenDevice(CS$<>8__locals1.id);
						devices = this._devices;
						lock (devices)
						{
							this._devices.Add(headsetCloudAlphaS2);
						}
						this.OnDeviceAdded(headsetCloudAlphaS2);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_1709&MI_03&Col02"
					}))
					{
						this.AddDevice<HeadsetCloudStingerCore>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_170B&MI_03&Col03"
					}))
					{
						this.AddDevice<HeadsetCloudStingerCoreWireless>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_1715&MI_03&Col02"
					}))
					{
						this.AddDevice<HeadsetCloudStingerS>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_1718&MI_03&Col03",
						"HID#VID_03F0&PID_0C8A&MI_03&Col03"
					}))
					{
						this.AddDevice<HeadsetCloudIIWireless>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_1743&MI_03&Col01",
						"HID#VID_0951&PID_1765&MI_03&Col01",
						"HID#VID_03F0&PID_098D&MI_03&Col01"
					}))
					{
						this.AddDevice<HeadsetCloudAlphaWireless>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_018B&MI_03&Col03",
						"HID#VID_03F0&PID_0696&MI_03&Col03",
						"HID#VID_03F0&PID_058B&Col02",
						"HID#VID_03F0&PID_038B&Col02"
					}))
					{
						this.AddDevice<HeadsetCloudIIWirelessDTS>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_078A&mi_03&col04"
					}))
					{
						this.AddDeviceMajorEndpoint<HeadsetCloudMIXBuds>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_078A&mi_03&col05"
					}))
					{
						this.AddDeviceNotificationEndpoint<HeadsetCloudMIXBuds>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_0D93&mi_03&col03"
					}))
					{
						this.AddDevice<HeadsetCloudStinger2Wireless>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_0995&MI_03&Col02"
					}))
					{
						this.AddDevice<HeadsetCloud2CoreWireless>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_069F&MI_03&Col02"
					}))
					{
						this.AddDevice<HeadsetCloud2CoreWirelessTread>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_16C4&mi_03&col03",
						"HID#VID_0951&PID_1723&mi_03&col03",
						"HID#VID_03F0&PID_0E90&mi_03&col03"
					}))
					{
						this.AddDevice<HeadsetCloudFlightWireless>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_099C&Col03",
						"HID#VID_03F0&PID_0EBC&Col03",
						"HID#VID_03F0&PID_0AA0&MI_03&Col03",
						"HID#VID_03F0&PID_089F&MI_03&Col03",
						"HID#VID_03F0&PID_0A9F&MI_03&Col03",
						"HID#VID_03F0&PID_08B7&MI_03&Col03"
					}))
					{
						this.AddDevice<HeadsetRalphie>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_089D&MI_03&Col02"
					}))
					{
						this.AddDevice<HeadsetCloudIII>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_0C9D&MI_03&Col01",
						"HID#VID_03F0&PID_05B7&MI_03&Col01",
						"HID#VID_03F0&PID_0E9D",
						"HID#VID_03F0&PID_06B7"
					}))
					{
						this.AddDevice<HeadsetCloud3Wireless>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_039E&MI_03&Col05"
					}))
					{
						this.AddDevice<HeadsetCloudMixBuds2>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_0FAE&MI_03&Col06"
					}))
					{
						this.AddDevice<HeadsetCloudMix2>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_06BE&MI_03&Col05"
					}))
					{
						this.AddDevice<HeadsetCloud3SWireless>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_171F&MI_00"
					}))
					{
						this.AddDeviceMajorEndpoint<MicrophoneQuadCastS>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_048C&MI_00"
					}))
					{
						this.AddDeviceMajorEndpoint<MicrophoneQuadCastSWhite>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_068C&MI_00"
					}))
					{
						this.AddDeviceMajorEndpoint<MicrophoneQuadCastSWhite2S>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_0F8B&MI_00"
					}))
					{
						this.AddDeviceMajorEndpoint<MicrophoneQuadCastS>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_028C&MI_00"
					}))
					{
						this.AddDeviceMajorEndpoint<MicrophoneQuadCastS2S>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_171F&MI_01&Col02"
					}))
					{
						this.AddDeviceNotificationEndpoint<MicrophoneQuadCastS>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_048C&MI_01&Col02"
					}))
					{
						this.AddDeviceNotificationEndpoint<MicrophoneQuadCastSWhite>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_068C&MI_01&Col02"
					}))
					{
						this.AddDeviceNotificationEndpoint<MicrophoneQuadCastSWhite2S>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_0F8B&MI_01&Col02"
					}))
					{
						this.AddDeviceNotificationEndpoint<MicrophoneQuadCastS>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_028C&MI_01&Col02"
					}))
					{
						this.AddDeviceNotificationEndpoint<MicrophoneQuadCastS2S>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_0951&PID_1720",
						"HID#VID_0951&PID_1774"
					}))
					{
						this.AddDeviceDFUEndpoint<MicrophoneQuadCastS>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_0A8C&MI_03"
					}))
					{
						this.AddDeviceMajorEndpoint<MicrophoneDuocast>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_098C&MI_00"
					}))
					{
						this.AddDeviceSecondaryEndpoint<MicrophoneDuocast>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_088C",
						"HID#VID_0951&PID_175F"
					}))
					{
						this.AddDeviceDFUEndpoint<MicrophoneDuocast>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_078B&MI_02&Col01",
						"HID#VID_0951&PID_170F&MI_02&Col01",
						"HID#VID_03F0&PID_0592&MI_02&Col01",
						"HID#VID_03F0&PID_098B&MI_02"
					}))
					{
						this.AddDevice<MicrophoneSoloCast>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_0992&MI_02&Col02",
						"HID#VID_03F0&PID_0B8B&MI_02&Col02"
					}))
					{
						this.AddDevice<MicrophoneSoloCastCNXT>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_09AF&MI_00"
					}))
					{
						this.AddDeviceMajorEndpoint<MicrophoneQuadCast2>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_07B4&MI_02&Col04"
					}) || Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_07AF&MI_03&Col04"
					}))
					{
						this.AddDeviceSecondaryEndpoint<MicrophoneQuadCast2>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_0AAF"
					}))
					{
						this.AddDeviceDFUEndpoint<MicrophoneQuadCast2>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_02B5&MI_01&Col02"
					}))
					{
						Task.Run(delegate()
						{
							HyperXCenter.<>c__DisplayClass95_0.<<AddDevice>b__2>d <<AddDevice>b__2>d;
							<<AddDevice>b__2>d.<>t__builder = AsyncTaskMethodBuilder.Create();
							<<AddDevice>b__2>d.<>4__this = CS$<>8__locals1;
							<<AddDevice>b__2>d.<>1__state = -1;
							<<AddDevice>b__2>d.<>t__builder.Start<HyperXCenter.<>c__DisplayClass95_0.<<AddDevice>b__2>d>(ref <<AddDevice>b__2>d);
							return <<AddDevice>b__2>d.<>t__builder.Task;
						}).Wait();
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_03B5"
					}))
					{
						Task.Run(delegate()
						{
							HyperXCenter.<>c__DisplayClass95_0.<<AddDevice>b__3>d <<AddDevice>b__3>d;
							<<AddDevice>b__3>d.<>t__builder = AsyncTaskMethodBuilder.Create();
							<<AddDevice>b__3>d.<>4__this = CS$<>8__locals1;
							<<AddDevice>b__3>d.<>1__state = -1;
							<<AddDevice>b__3>d.<>t__builder.Start<HyperXCenter.<>c__DisplayClass95_0.<<AddDevice>b__3>d>(ref <<AddDevice>b__3>d);
							return <<AddDevice>b__3>d.<>t__builder.Task;
						}).Wait();
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_0D84&MI_03&Col06",
						"HID#VID_03F0&PID_0EB4"
					}))
					{
						this.AddDeviceSecondaryEndpoint<MicrophoneQuadCast2S>(CS$<>8__locals1.id);
					}
					if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_0B97&MI_02"
					}))
					{
						this.AddDeviceUniversalEndpoint<MousePulsefireHaste2>(CS$<>8__locals1.id, false);
					}
					else if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
					{
						"HID#VID_03F0&PID_0C97"
					}))
					{
						this.AddDeviceUniversalEndpoint<MousePulsefireHaste2>(CS$<>8__locals1.id, true);
					}
					else
					{
						if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
						{
							"HID#VID_03F0&PID_0F98&MI_02"
						}))
						{
							try
							{
								using (HidDevice hidDevice3 = new HidDevice(CS$<>8__locals1.id, 3221225472U, 2147483648U))
								{
									if (hidDevice3.Version <= 16644)
									{
										this.AddMousePulsefireHaste2WirelessEndpoint(CS$<>8__locals1.id, true);
									}
									else
									{
										this.AddDeviceUniversalEndpoint<MousePulsefireHaste2WirelessGen2>(CS$<>8__locals1.id, false);
									}
								}
								goto IL_1868;
							}
							catch (Exception ex3)
							{
								throw ex3;
							}
						}
						if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
						{
							"HID#VID_03F0&PID_0199&MI_01"
						}))
						{
							this.AddDeviceUniversalEndpoint<MousePulsefireHaste2WirelessGen2>(CS$<>8__locals1.id, true);
						}
						else
						{
							if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
							{
								"HID#VID_03F0&PID_0D97&MI_02"
							}))
							{
								try
								{
									using (HidDevice hidDevice4 = new HidDevice(CS$<>8__locals1.id, 3221225472U, 2147483648U))
									{
										if (hidDevice4.Version <= 4361)
										{
											this.AddMousePulsefireHaste2WirelessEndpoint(CS$<>8__locals1.id, false);
										}
										else
										{
											this.AddDeviceUniversalEndpoint<MousePulsefireHaste2WirelessGen2>(CS$<>8__locals1.id, false);
										}
									}
									goto IL_1868;
								}
								catch (Exception ex4)
								{
									throw ex4;
								}
							}
							if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
							{
								"HID#VID_03F0&PID_0E97&MI_01"
							}))
							{
								this.AddMousePulsefireHaste2WirelessEndpoint(CS$<>8__locals1.id, false);
							}
							else if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
							{
								"HID#VID_03F0&PID_0BA0&MI_02",
								"HID#VID_03F0&PID_0DA0&MI_02"
							}))
							{
								this.AddDeviceUniversalEndpoint<MousePulsefireHaste2MiniWireless>(CS$<>8__locals1.id, false);
							}
							else if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
							{
								"HID#VID_03F0&PID_0CA0",
								"HID#VID_03F0&PID_0EA0"
							}))
							{
								this.AddDeviceUniversalEndpoint<MousePulsefireHaste2MiniWireless>(CS$<>8__locals1.id, true);
							}
							else if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
							{
								"HID#VID_03F0&PID_0FA0&MI_02"
							}))
							{
								this.AddDeviceUniversalEndpoint<KeyboardAlloyRise>(CS$<>8__locals1.id, false);
							}
							else if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
							{
								"HID#VID_03F0&PID_01A1&MI_02"
							}))
							{
								this.AddDeviceUniversalEndpoint<KeyboardAlloyRise>(CS$<>8__locals1.id, true);
							}
							else if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
							{
								"HID#VID_03F0&PID_02A1&MI_02"
							}))
							{
								this.AddDeviceUniversalEndpoint<KeyboardAlloyRise75>(CS$<>8__locals1.id, false);
							}
							else if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
							{
								"HID#VID_03F0&PID_03A1&MI_02"
							}))
							{
								this.AddDeviceUniversalEndpoint<KeyboardAlloyRise75>(CS$<>8__locals1.id, true);
							}
							else if (!Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
							{
								"HID#VID_03F0&PID_04B5&MI_02"
							}) && !Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
							{
								"HID#VID_03F0&PID_05B5"
							}))
							{
								if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
								{
									"HID#VID_03F0&PID_0AB5&MI_02"
								}))
								{
									this.AddDeviceUniversalEndpoint<MousePulsefireHaste2CoreWireless>(CS$<>8__locals1.id, false);
								}
								else if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
								{
									"HID#VID_03F0&PID_0BB5&MI_01"
								}))
								{
									this.AddDeviceUniversalEndpoint<MousePulsefireHaste2CoreWireless>(CS$<>8__locals1.id, true);
								}
								else if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
								{
									"HID#VID_03F0&PID_01B9&MI_02"
								}))
								{
									this.AddDeviceUniversalEndpoint<KeyboardAlloyRise75Wireless>(CS$<>8__locals1.id, false);
								}
								else if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
								{
									"HID#VID_03F0&PID_02B9"
								}))
								{
									this.AddDeviceUniversalEndpoint<KeyboardAlloyRise75Wireless>(CS$<>8__locals1.id, true);
								}
								else if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
								{
									"HID#VID_03F0&PID_0EB8&MI_02"
								}))
								{
									this.AddDeviceUniversalEndpoint<KeyboardAlloyRise75Wireless>(CS$<>8__locals1.id, false);
								}
								else if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
								{
									"HID#VID_03F0&PID_0FB8"
								}))
								{
									this.AddDeviceUniversalEndpoint<KeyboardAlloyRise75Wireless>(CS$<>8__locals1.id, true);
								}
								else if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
								{
									"HID#VID_03F0&PID_0BB8&MI_02",
									"HID#VID_03F0&PID_0AB8&MI_02"
								}))
								{
									this.AddDeviceUniversalEndpoint<MousePulsefireHaste2SWireless>(CS$<>8__locals1.id, false);
								}
								else if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
								{
									"HID#VID_03F0&PID_0CB8",
									"HID#VID_03F0&PID_0DB8"
								}))
								{
									this.AddDeviceUniversalEndpoint<MousePulsefireHaste2SWireless>(CS$<>8__locals1.id, true);
								}
								else if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
								{
									"HID#VID_03F0&PID_0FBD&MI_02"
								}))
								{
									this.AddDeviceUniversalEndpoint<MousePulsefireFuseWireless>(CS$<>8__locals1.id, false);
								}
								else if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
								{
									"HID#VID_03F0&PID_0EBE&MI_02"
								}))
								{
									this.AddDeviceUniversalEndpoint<MousePulsefireSaga>(CS$<>8__locals1.id, false);
								}
								else if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
								{
									"HID#VID_03F0&PID_0FBE&MI_01"
								}))
								{
									this.AddDeviceUniversalEndpoint<MousePulsefireSaga>(CS$<>8__locals1.id, true);
								}
								else if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
								{
									"HID#VID_03F0&PID_0CBD&MI_03"
								}))
								{
									this.AddDeviceUniversalEndpoint<MousePulsefireHaste2Pro>(CS$<>8__locals1.id, false);
								}
								else if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
								{
									"HID#VID_03F0&PID_0DBD&MI_01"
								}))
								{
									this.AddDeviceUniversalEndpoint<MousePulsefireHaste2Pro>(CS$<>8__locals1.id, true);
								}
								else if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
								{
									"HID#VID_03F0&PID_0ABD&MI_02"
								}))
								{
									this.AddDeviceUniversalEndpoint<MousePulsefireHaste2Pro>(CS$<>8__locals1.id, false);
								}
								else if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
								{
									"HID#VID_03F0&PID_0BBD"
								}))
								{
									this.AddDeviceUniversalEndpoint<MousePulsefireHaste2Pro>(CS$<>8__locals1.id, true);
								}
								else if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
								{
									"HID#VID_03F0&PID_06BF&MI_03",
									"HID#VID_03F0&PID_04BF&MI_02"
								}))
								{
									this.AddDeviceUniversalEndpoint<MousePulsefireSagaPro>(CS$<>8__locals1.id, false);
								}
								else if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
								{
									"HID#VID_03F0&PID_07BF&MI_01",
									"HID#VID_03F0&PID_05BF"
								}))
								{
									this.AddDeviceUniversalEndpoint<MousePulsefireSagaPro>(CS$<>8__locals1.id, false);
								}
								else if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
								{
									HyperXCenter.GetEmblemsEndpointSymbolString(),
									"HID#VID_03F0&PID_01C5&Col05"
								}))
								{
									this.AddDeviceUniversalEndpoint<TIOEmblems>(CS$<>8__locals1.id, false);
								}
								else if (Utils.IsCultureInvariantMatch(CS$<>8__locals1.id, new string[]
								{
									"HID#VID_03F0&PID_0AC1&MI_03&Col04"
								}))
								{
									this.AddDeviceUniversalEndpoint<HeadsetCloudFlight2>(CS$<>8__locals1.id, false);
								}
							}
						}
					}
					IL_1868:
					Logger.WriteLine("Added: " + CS$<>8__locals1.id, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 1890);
					this.GACollectDevicesConnected();
				}
			}
			catch (Exception ex5)
			{
				Logger.WriteLine("Failed to open device:" + ex5.Message + ", ID: " + CS$<>8__locals1.id, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 1896);
			}
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x0005D5E0 File Offset: 0x0005B7E0
		public void ClearDeviceNames()
		{
			List<string> deviceNames = this._deviceNames;
			lock (deviceNames)
			{
				this._deviceNames.Clear();
			}
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x0005D628 File Offset: 0x0005B828
		internal void OnDeviceNameChanged(HyperXDevice device)
		{
			if (this.DeviceNameChanged != null)
			{
				this.DeviceNameChanged.Invoke(this, device);
			}
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x0005D640 File Offset: 0x0005B840
		public bool ContainsDevice(HyperXDevice device)
		{
			bool result = false;
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				result = this._devices.Contains(device);
			}
			return result;
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x0005D68C File Offset: 0x0005B88C
		public bool ContainsDevice(HyperXDeviceType type)
		{
			bool result = false;
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				result = (this._devices.FirstOrDefault((HyperXDevice o) => o.DeviceType == type) != null);
			}
			return result;
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x0005D6F4 File Offset: 0x0005B8F4
		public bool ContainsDevice(HyperXDeviceModel model)
		{
			bool result = false;
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				result = (this._devices.FirstOrDefault((HyperXDevice o) => o.Model == model) != null);
			}
			return result;
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x0005D75C File Offset: 0x0005B95C
		public void AddVirtualDevice(HyperXDeviceModel model)
		{
			HyperXDevice hyperXDevice = null;
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				if (this._devices.Matches((HyperXDevice o) => o.Model == model))
				{
					return;
				}
			}
			HyperXDeviceModel model2 = model;
			if (model2 <= HyperXDeviceModel.KeyboardAlloyOrigins65)
			{
				if (model2 <= HyperXDeviceModel.KeyboardAlloyElite_II)
				{
					switch (model2)
					{
					case HyperXDeviceModel.KeyboardAlloyElite:
						hyperXDevice = new KeyboardAlloyElite
						{
							IsSimulator = true
						};
						break;
					case (HyperXDeviceModel)2:
					case (HyperXDeviceModel)4:
						break;
					case HyperXDeviceModel.KeyboardAlloyFPS:
						hyperXDevice = new KeyboardAlloyFPS
						{
							IsSimulator = true
						};
						break;
					case HyperXDeviceModel.KeyboardAlloyOrigins:
						hyperXDevice = new KeyboardAlloyOrigins
						{
							IsSimulator = true
						};
						break;
					case HyperXDeviceModel.KeyboardAlloyOriginsCore:
						hyperXDevice = new KeyboardAlloyOriginsCore
						{
							IsSimulator = true
						};
						break;
					default:
						if (model2 == HyperXDeviceModel.KeyboardAlloyElite_II)
						{
							hyperXDevice = new KeyboardAlloyEliteII
							{
								IsSimulator = true
							};
						}
						break;
					}
				}
				else if (model2 != HyperXDeviceModel.KeyboardAlloyOrigins60)
				{
					if (model2 == HyperXDeviceModel.KeyboardAlloyOrigins65)
					{
						hyperXDevice = new KeyboardAlloyOrigins65
						{
							IsSimulator = true
						};
					}
				}
				else
				{
					hyperXDevice = new KeyboardAlloyOrigins60
					{
						IsSimulator = true
					};
				}
			}
			else if (model2 <= HyperXDeviceModel.MousePad_FURYUltra)
			{
				switch (model2)
				{
				case HyperXDeviceModel.MousePulsefireSurge:
					hyperXDevice = new MousePulsefireSurge
					{
						IsSimulator = true
					};
					break;
				case HyperXDeviceModel.MousePulsefireRaid:
					hyperXDevice = new MousePulsefireRaid
					{
						IsSimulator = true
					};
					break;
				case HyperXDeviceModel.MousePulsefireFPSPro:
					hyperXDevice = new MousePulsefireFPSProCD
					{
						IsSimulator = true
					};
					break;
				case HyperXDeviceModel.MousePulsefireCore:
					hyperXDevice = new MousePulsefireCore
					{
						IsSimulator = true
					};
					break;
				case HyperXDeviceModel.MousePulsefireDart:
					hyperXDevice = new MousePulsefireDart
					{
						IsSimulator = true
					};
					break;
				case HyperXDeviceModel.MousePulsefireFPSProCD:
					hyperXDevice = new MousePulsefireFPSProCD
					{
						IsSimulator = true
					};
					break;
				case HyperXDeviceModel.MousePulsefireHaste:
					hyperXDevice = new MousePulsefireHaste
					{
						IsSimulator = true
					};
					break;
				case HyperXDeviceModel.MousePulsefireHasteWireless:
					hyperXDevice = new MousePulsefireHasteWireless
					{
						IsSimulator = true
					};
					break;
				default:
					if (model2 == HyperXDeviceModel.MousePad_FURYUltra)
					{
						hyperXDevice = new MousepadFURYUltra
						{
							IsSimulator = true
						};
					}
					break;
				}
			}
			else if (model2 != HyperXDeviceModel.MousePad_PulsefireMatRGB)
			{
				switch (model2)
				{
				case HyperXDeviceModel.Headset_CloudFlightS:
					hyperXDevice = new HeadsetCloudFlightS
					{
						IsSimulator = true
					};
					break;
				case HyperXDeviceModel.Headset_StingerCoreWired:
					hyperXDevice = new HeadsetCloudStingerCore
					{
						IsSimulator = true
					};
					break;
				case HyperXDeviceModel.Headset_StingerCoreWireless:
					hyperXDevice = new HeadsetCloudStingerCoreWireless
					{
						IsSimulator = true
					};
					break;
				case HyperXDeviceModel.Headset_CloudAlphaS:
					hyperXDevice = new HeadsetCloudAlphaS
					{
						IsSimulator = true
					};
					break;
				case HyperXDeviceModel.Headset_ResolverUltraGame:
				case HyperXDeviceModel.Headset_CloudMIXBuds:
				case HyperXDeviceModel.Headset_CloudStinger2Wireless:
				case HyperXDeviceModel.Headset_CloudFlightWireless:
					break;
				case HyperXDeviceModel.Headset_StingerS:
					hyperXDevice = new HeadsetCloudStingerS
					{
						IsSimulator = true
					};
					break;
				case HyperXDeviceModel.Headset_CloudIIWireless:
					hyperXDevice = new HeadsetCloudFlightS
					{
						IsSimulator = true
					};
					break;
				case HyperXDeviceModel.Headset_CloudAlphaWireless:
					hyperXDevice = new HeadsetCloudAlphaWireless
					{
						IsSimulator = true
					};
					break;
				case HyperXDeviceModel.Headset_CloudIIWirelessDTS:
					hyperXDevice = new HeadsetCloudIIWirelessDTS
					{
						IsSimulator = true
					};
					break;
				case HyperXDeviceModel.Headset_Cloud2CoreWireless:
					hyperXDevice = new HeadsetCloud2CoreWireless
					{
						IsSimulator = true
					};
					break;
				case HyperXDeviceModel.Headset_Cloud2CoreWirelessTread:
					hyperXDevice = new HeadsetCloud2CoreWirelessTread
					{
						IsSimulator = true
					};
					break;
				case HyperXDeviceModel.Headset_Ralphie:
					hyperXDevice = new HeadsetRalphie
					{
						IsSimulator = true
					};
					break;
				case HyperXDeviceModel.Headset_CloudIII:
					hyperXDevice = new HeadsetCloudIII
					{
						IsSimulator = true
					};
					break;
				default:
					switch (model2)
					{
					case HyperXDeviceModel.Microphone_QuadcastS:
						hyperXDevice = new MicrophoneQuadCastS
						{
							IsSimulator = true
						};
						break;
					case HyperXDeviceModel.Microphone_Duocast:
						hyperXDevice = new MicrophoneDuocast
						{
							IsSimulator = true
						};
						break;
					case HyperXDeviceModel.Microphone_Solocast:
						hyperXDevice = new MicrophoneSoloCast
						{
							IsSimulator = true
						};
						break;
					case HyperXDeviceModel.Microphone_QuadCast2:
						hyperXDevice = new MicrophoneQuadCast2
						{
							IsSimulator = true
						};
						break;
					case HyperXDeviceModel.Microphone_Quadcast2S:
						hyperXDevice = new MicrophoneQuadCast2S
						{
							IsSimulator = true
						};
						break;
					}
					break;
				}
			}
			else
			{
				hyperXDevice = new MousepadPulsefireMatRGB
				{
					IsSimulator = true
				};
			}
			if (hyperXDevice != null)
			{
				hyperXDevice.SetupSimulator();
				devices = this._devices;
				lock (devices)
				{
					this._devices.Add(hyperXDevice);
				}
				this.OnDeviceAdded(hyperXDevice);
				hyperXDevice.ApplyPresetAndEffects(Preset.CurrentPreset);
			}
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x0005DB6C File Offset: 0x0005BD6C
		public void ScanUnloadedDevices()
		{
			this.CreateCompositeDevice();
			DeviceDetection.RescanAllDevices();
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x0005DB7C File Offset: 0x0005BD7C
		public void StartDeviceInitialization(IntPtr handle, string publisherCachePath)
		{
			List<DeviceDetection.DeviceInfo> list = DeviceDetection.DetectAllDevices();
			this.InitializePublisherCacheHelper(publisherCachePath, list);
			this.CreateCompositeDevice();
			DeviceDetection.Start(handle, list);
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x0005DBA4 File Offset: 0x0005BDA4
		public HyperXDevice FindDevice(string id)
		{
			HyperXDevice result = null;
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				result = this._devices.FirstOrDefault((HyperXDevice dev) => string.Equals(dev.DeviceID, id) || string.Equals(dev.NotificationDeviceID, id));
			}
			return result;
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x0005DC08 File Offset: 0x0005BE08
		public HyperXDevice FindDevice(ushort vid, ushort pid)
		{
			HyperXDevice result = null;
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				result = this._devices.FirstOrDefault((HyperXDevice dev) => dev.VendorID == vid && dev.ProductID == pid);
			}
			return result;
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x0005DC74 File Offset: 0x0005BE74
		public T FindDevice<T>() where T : HyperXDevice
		{
			HyperXDevice hyperXDevice = null;
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				hyperXDevice = this._devices.FirstOrDefault((HyperXDevice dev) => dev is T);
			}
			return (T)((object)hyperXDevice);
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x0005DCE4 File Offset: 0x0005BEE4
		public void FindDeviceAndSafeExecuteAction(Func<HyperXDevice, bool> predicate, Action<HyperXDevice> action)
		{
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				HyperXDevice hyperXDevice = this._devices.FirstOrDefault(predicate);
				if (hyperXDevice != null)
				{
					action(hyperXDevice);
				}
			}
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x0005DD38 File Offset: 0x0005BF38
		public bool IsDeviceExist(HyperXDeviceModel targetModel)
		{
			bool result = false;
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				result = this._devices.Exists((HyperXDevice dev) => dev.Model == targetModel);
			}
			return result;
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x0005DD9C File Offset: 0x0005BF9C
		private void StopAllDevicesThread()
		{
			List<HyperXDevice> list = new List<HyperXDevice>();
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				list.AddRange(this._devices);
			}
			foreach (HyperXDevice hyperXDevice in list)
			{
				hyperXDevice.PreStop();
			}
			foreach (HyperXDevice hyperXDevice2 in list)
			{
				hyperXDevice2.Stop(true);
			}
			lock (this)
			{
				AutoResetEvent autoResetEvent = this.stopResetEvent;
				if (autoResetEvent != null)
				{
					autoResetEvent.Set();
				}
			}
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x0005DE98 File Offset: 0x0005C098
		public void StopAllDevices()
		{
			this.stopResetEvent = new AutoResetEvent(false);
			new Thread(new ThreadStart(this.StopAllDevicesThread)).Start();
			this.stopResetEvent.WaitOne(3000);
			lock (this)
			{
				this.stopResetEvent.Dispose();
				this.stopResetEvent = null;
			}
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x0005DF14 File Offset: 0x0005C114
		public void RemoveAllDevices()
		{
			Logger.WriteLine("Reset all devices", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 2210);
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				try
				{
					this._devices.ForEach(delegate(HyperXDevice o)
					{
						if (o.DeviceType != HyperXDeviceType.DRAM)
						{
							if (this.DeviceRemoved != null)
							{
								this.DeviceRemoved.Invoke(this, o);
							}
							this.RemoveDeviceName(o.Name);
							o.Stop(true);
						}
					});
				}
				catch
				{
				}
				this._devices.RemoveAll((HyperXDevice o) => o.DeviceType != HyperXDeviceType.DRAM);
			}
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x0005DFB8 File Offset: 0x0005C1B8
		private void CreateCompositeDevice()
		{
			this._compositeDevice = new HyperXCompositeDevice();
			this._compositeDevice.ApplyPresetAndEffects(Preset.CurrentPreset);
			this._compositeDevice.Start();
			this._devices.Add(this._compositeDevice);
			this.OnDeviceAdded(this._compositeDevice);
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x0005E008 File Offset: 0x0005C208
		public HyperXDevice CreateCompositeDevice(string deviceId1, string deviceId2)
		{
			HyperXDevice hyperXDevice = this.Devices.FirstOrDefault((HyperXDevice dev) => dev.Model == HyperXDeviceModel.Composite);
			if (hyperXDevice != null)
			{
				return hyperXDevice;
			}
			HyperXDevice centerDevice = this.FindDevice(deviceId1);
			HyperXDevice device = this.FindDevice(deviceId2);
			HyperXCompositeDevice hyperXCompositeDevice = new HyperXCompositeDevice();
			hyperXCompositeDevice.CenterDevice = centerDevice;
			hyperXCompositeDevice.LinkDevice(device, CompositeDeviceDirection.Right);
			hyperXCompositeDevice.ApplyPresetAndEffects(Preset.CurrentPreset);
			hyperXCompositeDevice.Start();
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				this._devices.Add(hyperXCompositeDevice);
			}
			this.OnDeviceAdded(hyperXCompositeDevice);
			return hyperXCompositeDevice;
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public void DismissCompositeDevice()
		{
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x0005E0C4 File Offset: 0x0005C2C4
		private List<HyperXCenter.DriverInfo> GetInstalledHyperXDriverInfo()
		{
			List<HyperXCenter.DriverInfo> list = new List<HyperXCenter.DriverInfo>();
			Process process = new Process();
			process.StartInfo.FileName = "C:\\Windows\\System32\\cmd.exe";
			process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.RedirectStandardError = true;
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.Arguments = "/c chcp 936 & pnputil /enum-drivers";
			process.Start();
			string text = process.StandardOutput.ReadToEnd();
			string info = process.StandardError.ReadToEnd();
			if (process.ExitCode != 0)
			{
				Logger.WriteLine(info, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 2318);
				Logger.WriteLine(text, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 2319);
				return null;
			}
			if (!text.Contains("No published driver packages were found on the system"))
			{
				List<HyperXCenter.DriverInfo> list2 = this.ParsePnputilDriverInfo(text);
				if (list2 != null)
				{
					list.AddRange(list2);
				}
			}
			return list;
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x0005E1A0 File Offset: 0x0005C3A0
		private List<HyperXCenter.DriverInfo> ParsePnputilDriverInfo(string str)
		{
			string pattern = "\\d+\\.\\d+\\.\\d+\\.\\d+";
			string[] array = str.Split(new string[]
			{
				Environment.NewLine
			}, StringSplitOptions.None);
			if (array.Count<string>() < 5)
			{
				return null;
			}
			List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].Contains("Provider Name") && array[i].ToLower().Contains("hyperx"))
				{
					Dictionary<string, string> dictionary = new Dictionary<string, string>();
					string[] array2 = array[i].Split(new char[]
					{
						':'
					});
					if (array2.Length >= 2)
					{
						dictionary.Add(array2[0].Trim(), array2[1].Trim());
					}
					bool flag = false;
					bool flag2 = false;
					int num = 0;
					while (!flag || !flag2)
					{
						num++;
						int num2 = i - num;
						int num3 = i + num;
						if (num2 < 0)
						{
							flag = true;
						}
						if (num3 > array.Length - 1)
						{
							flag2 = true;
						}
						if (!flag)
						{
							if (string.IsNullOrEmpty(array[num2]))
							{
								flag = true;
							}
							else
							{
								string[] array3 = array[num2].Split(new char[]
								{
									':'
								});
								if (array3.Length >= 2)
								{
									dictionary.Add(array3[0].Trim(), array3[1].Trim());
								}
							}
						}
						if (!flag2)
						{
							if (string.IsNullOrEmpty(array[num3]))
							{
								flag2 = true;
							}
							else
							{
								string[] array4 = array[num3].Split(new char[]
								{
									':'
								});
								if (array4.Length >= 2)
								{
									dictionary.Add(array4[0].Trim(), array4[1].Trim());
								}
							}
						}
					}
					list.Add(dictionary);
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			List<HyperXCenter.DriverInfo> list2 = new List<HyperXCenter.DriverInfo>();
			foreach (Dictionary<string, string> dictionary2 in list)
			{
				HyperXCenter.DriverInfo item = new HyperXCenter.DriverInfo
				{
					PublishedName = string.Empty,
					OriginalName = string.Empty,
					ProviderName = string.Empty,
					ClassName = string.Empty,
					ClassGUID = string.Empty,
					ExtensionID = string.Empty,
					Version = null,
					SignerName = string.Empty
				};
				if (dictionary2.ContainsKey("Published Name"))
				{
					item.PublishedName = dictionary2["Published Name"];
				}
				if (dictionary2.ContainsKey("Original Name"))
				{
					item.OriginalName = dictionary2["Original Name"];
				}
				if (dictionary2.ContainsKey("Provider Name"))
				{
					item.ProviderName = dictionary2["Provider Name"];
				}
				if (dictionary2.ContainsKey("Class Name"))
				{
					item.ClassName = dictionary2["Class Name"];
				}
				if (dictionary2.ContainsKey("Class GUID"))
				{
					item.ClassGUID = dictionary2["Class GUID"];
				}
				if (dictionary2.ContainsKey("Extension ID"))
				{
					item.ExtensionID = dictionary2["Extension ID"];
				}
				if (dictionary2.ContainsKey("Driver Version"))
				{
					Match match = Regex.Match(dictionary2["Driver Version"], pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
					if (match.Success)
					{
						item.Version = new Version(match.Value);
					}
				}
				if (dictionary2.ContainsKey("Signer Name"))
				{
					item.SignerName = dictionary2["Signer Name"];
				}
				list2.Add(item);
			}
			return list2;
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x0005E534 File Offset: 0x0005C734
		private HyperXCenter.DriverInfo? ParseInfDTSDriverInfo(string infFilePath)
		{
			string input = File.ReadAllText(infFilePath);
			string pattern = "DriverVer\\s*=\\s*[\\d/\\-]+\\s*,\\s*(?<Version>\\d+\\.\\d+\\.\\d+\\.\\d+)";
			Match match = Regex.Match(input, pattern);
			if (match.Success)
			{
				return new HyperXCenter.DriverInfo?(new HyperXCenter.DriverInfo
				{
					OriginalName = Path.GetFileName(infFilePath),
					Version = new Version(match.Groups["Version"].Value)
				});
			}
			return null;
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x0005E5A4 File Offset: 0x0005C7A4
		private List<HyperXCenter.DriverInfo> GetRequiredDTSDriverInfo()
		{
			List<HyperXCenter.DriverInfo> list = new List<HyperXCenter.DriverInfo>();
			string installedLocation = Utils.InstalledLocation;
			HyperXCenter.DriverInfo? driverInfo = this.ParseInfDTSDriverInfo(Path.Combine(installedLocation, "Assets\\Native\\AudioDTS\\dtshpxv2_hyperx_ext.inf"));
			if (driverInfo != null)
			{
				list.Add(driverInfo.Value);
			}
			driverInfo = this.ParseInfDTSDriverInfo(Path.Combine(installedLocation, "Assets\\Native\\AudioDTS\\dtshpxv2apo4xservice.inf"));
			if (driverInfo != null)
			{
				list.Add(driverInfo.Value);
			}
			driverInfo = this.ParseInfDTSDriverInfo(Path.Combine(installedLocation, "Assets\\Native\\AudioDTS\\dtsapo4xhpxv2x64.inf"));
			if (driverInfo != null)
			{
				list.Add(driverInfo.Value);
			}
			return list;
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x0005E638 File Offset: 0x0005C838
		internal bool RemoveDTSDrivers()
		{
			IEnumerable<HyperXCenter.DriverInfo> installedHyperXDriverInfo = this.GetInstalledHyperXDriverInfo();
			List<HyperXCenter.DriverInfo> dtsDriverList = this.GetRequiredDTSDriverInfo();
			List<HyperXCenter.DriverInfo> list = (from x in installedHyperXDriverInfo
			where dtsDriverList.Exists((HyperXCenter.DriverInfo y) => y.OriginalName.Equals(x.OriginalName))
			select x).ToList<HyperXCenter.DriverInfo>();
			if (list.Count == 0)
			{
				Logger.WriteLine("No driver needs to be removed.", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 2532);
				return false;
			}
			Process process = new Process();
			process.StartInfo.FileName = "cmd.exe";
			process.StartInfo.Verb = "runas";
			process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			string text = "/C ";
			for (int i = 0; i < list.Count<HyperXCenter.DriverInfo>(); i++)
			{
				Logger.WriteLine("Remove " + list[i].OriginalName + ", " + list[i].PublishedName, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 2544);
				if (i == list.Count<HyperXCenter.DriverInfo>() - 1)
				{
					text = text + "pnputil -d " + list[i].PublishedName + " -u -f";
				}
				else
				{
					text = text + "pnputil -d " + list[i].PublishedName + " -u -f&";
				}
			}
			process.StartInfo.Arguments = text;
			Logger.WriteLine("Remove drivers cmd args: " + text, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 2556);
			process.Start();
			process.WaitForExit();
			if (process.ExitCode == 0)
			{
				Logger.WriteLine("Drivers removed successfully.", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 2562);
				return true;
			}
			Logger.WriteLine("Drivers removed unsuccessfully", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 2567);
			return false;
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x0005E7E0 File Offset: 0x0005C9E0
		public void InstallDrivers()
		{
			IEnumerable<HyperXCenter.DriverInfo> installedHyperXDriverInfo = this.GetInstalledHyperXDriverInfo();
			List<HyperXCenter.DriverInfo> requiredDTSDriverInfos = this.GetRequiredDTSDriverInfo();
			List<HyperXCenter.DriverInfo> list5 = (from installed in installedHyperXDriverInfo
			where requiredDTSDriverInfos.Exists((HyperXCenter.DriverInfo required) => required.OriginalName.Equals(installed.OriginalName))
			select installed).ToList<HyperXCenter.DriverInfo>();
			bool flag = false;
			Logger.WriteLine(string.Format("Installed DTS Driver: {0}", list5.Count), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 2579);
			foreach (HyperXCenter.DriverInfo driverInfo in list5)
			{
				Logger.WriteLine(string.Format("PublishedName: {0}, OriginalName:{1}, Version: {2}", driverInfo.PublishedName, driverInfo.OriginalName, driverInfo.Version), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 2582);
			}
			if (list5.Count != 0 && list5.Count < requiredDTSDriverInfos.Count)
			{
				Logger.WriteLine("Missing DTS drivers, Remove installed DTS driver and reinstall", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 2587);
				this.RemoveDTSDrivers();
				flag = true;
			}
			else if (list5.Count == requiredDTSDriverInfos.Count && list5.Exists((HyperXCenter.DriverInfo installed) => requiredDTSDriverInfos.Exists((HyperXCenter.DriverInfo required) => required.OriginalName.Equals(installed.OriginalName) && required.Version > installed.Version)))
			{
				Logger.WriteLine("Outdated DTS drivers, Remove all DTS drivers and reinstall", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 2596);
				this.RemoveDTSDrivers();
				flag = true;
			}
			List<List<string>> drivers = new List<List<string>>();
			List<string> list2 = new List<string>();
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				foreach (HyperXDevice hyperXDevice in this._devices)
				{
					if (hyperXDevice.DriverInstallationState > DriverInstallationState.Installed || flag)
					{
						List<string> list3 = hyperXDevice.CheckDrivers();
						if (list3 != null && list3.Count > 0)
						{
							drivers.Add(list3);
						}
						if (hyperXDevice.NeedDTSAPO)
						{
							list2.Add(hyperXDevice.DeviceID);
						}
					}
				}
			}
			List<string> allInfFiles = new List<string>();
			List<string> exclusiveFiles = new List<string>();
			List<string> sharedFiles = new List<string>();
			Action<string> <>9__6;
			drivers.ForEach(delegate(List<string> list)
			{
				Action<string> action;
				if ((action = <>9__6) == null)
				{
					action = (<>9__6 = delegate(string file)
					{
						if (!allInfFiles.Contains(file))
						{
							allInfFiles.Add(file);
						}
					});
				}
				list.ForEach(action);
			});
			allInfFiles.ForEach(delegate(string file)
			{
				if (drivers.FindAll((List<string> list) => list.Contains(file)).Count > 1)
				{
					sharedFiles.Add(file);
					return;
				}
				exclusiveFiles.Add(file);
			});
			List<string> list4 = new List<string>();
			list4.AddRange(exclusiveFiles);
			list4.AddRange(sharedFiles);
			try
			{
				string args = string.Format("HWND={0} \"LogPath={1}\" ", MainForm.CurrentForm.WindowHandle.ToInt64(), Utils.LocalFolder);
				list2.ForEach(delegate(string o)
				{
					args = args + "DTSDevice=" + o + " ";
				});
				list4.ForEach(delegate(string o)
				{
					args = args + "\"" + o + "\" ";
				});
				Logger.WriteLine("Install Drivers ...args: " + args, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 2663);
				list4.ForEach(delegate(string o)
				{
					Logger.WriteLine(o, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 2664);
				});
				this._driverInstallationExitCode = 0;
				int num = 0;
				using (Process process = new Process())
				{
					process.StartInfo.FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NGenuity2DriverInstaller.exe");
					process.StartInfo.Arguments = args;
					process.StartInfo.UseShellExecute = true;
					process.StartInfo.CreateNoWindow = false;
					process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
					process.StartInfo.Verb = "runas";
					process.Start();
					process.WaitForExit(180000);
					Logger.WriteLine(string.Format("NGenuityDriverInstaller returns {0:X8} DriverInstallation Code: {1}", process.ExitCode, this._driverInstallationExitCode), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 2684);
					num = process.ExitCode;
					process.Close();
					if (((long)num & (long)((ulong)-268435456)) == (long)((ulong)-1073741824))
					{
						num = this._driverInstallationExitCode;
					}
				}
				devices = this._devices;
				lock (devices)
				{
					this._devices.ForEach(delegate(HyperXDevice o)
					{
						o.DriverInstallationState = DriverInstallationState.Installed;
					});
				}
				if (num == 0)
				{
					num = -10000;
					Thread.Sleep(2000);
				}
				this.OnUpdateDriverInstallationProgress(1000, num);
			}
			catch (Exception arg)
			{
				Logger.WriteLine(string.Format("Failed to install driver {0}", arg), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 2712);
				this.OnUpdateDriverInstallationProgress(0, -1000);
			}
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x0005ED00 File Offset: 0x0005CF00
		private void RemoveDeviceWhenAdd(string id)
		{
			Logger.WriteLine("RemoveDeviceWhenAdd: " + id, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 2724);
			string targetDeviceID = string.Empty;
			if (Utils.IsCultureInvariantMatch(id, new string[]
			{
				"HID#VID_03F0&PID_048E&MI_02"
			}))
			{
				targetDeviceID = "HID#VID_03F0&PID_028E&MI_02";
			}
			else if (Utils.IsCultureInvariantMatch(id, new string[]
			{
				"HID#VID_0951&PID_16E2&MI_01"
			}))
			{
				targetDeviceID = "HID#VID_0951&PID_16E1&MI_02";
			}
			if (targetDeviceID == string.Empty)
			{
				return;
			}
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				HyperXDevice hyperXDevice = this._devices.FirstOrDefault((HyperXDevice o) => !string.IsNullOrEmpty(o.DeviceID) && Utils.IsCultureInvariantMatch(o.DeviceID, new string[]
				{
					targetDeviceID
				}));
				if (hyperXDevice != null)
				{
					Logger.WriteLine(string.Format("RemoveDeviceWhenAdd: found dongle, remove {0}", hyperXDevice.Model), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 2747);
					this.RemoveDevice(hyperXDevice);
				}
			}
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x0005EE04 File Offset: 0x0005D004
		private bool DeivceNotAdd(string id)
		{
			Logger.WriteLine("DeivceNotAdd: " + id, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 2760);
			string targetDeviceID = string.Empty;
			if (Utils.IsCultureInvariantMatch(id, new string[]
			{
				"HID#VID_03F0&PID_028E&MI_02"
			}))
			{
				targetDeviceID = "HID#VID_03F0&PID_048E&MI_02";
			}
			else if (Utils.IsCultureInvariantMatch(id, new string[]
			{
				"HID#VID_0951&PID_16E1&MI_02"
			}))
			{
				targetDeviceID = "HID#VID_0951&PID_16E2&MI_01";
			}
			if (targetDeviceID == string.Empty)
			{
				return false;
			}
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				if (this._devices.FirstOrDefault((HyperXDevice o) => !string.IsNullOrEmpty(o.DeviceID) && Utils.IsCultureInvariantMatch(o.DeviceID, new string[]
				{
					targetDeviceID
				})) != null)
				{
					Logger.WriteLine("DeivceNotAdd: mouse found, no need to add dongle", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 2784);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x0005EEF8 File Offset: 0x0005D0F8
		private void AddDeviceWhenRemove(string id)
		{
			Logger.WriteLine("AddDeviceWhenRemove: " + id, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 2798);
			string text = string.Empty;
			if (Utils.IsCultureInvariantMatch(id, new string[]
			{
				"HID#VID_03F0&PID_048E&MI_02"
			}))
			{
				Logger.WriteLine("Add back dongle since mouse was removed", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 2803);
				text = "HID#VID_03F0&PID_028E&MI_02";
			}
			else if (Utils.IsCultureInvariantMatch(id, new string[]
			{
				"HID#VID_0951&PID_16E2&MI_01"
			}))
			{
				text = "HID#VID_0951&PID_16E1&MI_02";
			}
			if (text == string.Empty)
			{
				return;
			}
			DeviceDetection.AddDevice(text);
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x0005EF88 File Offset: 0x0005D188
		private void GACollectDevicesConnected()
		{
			if (this._devices.FindAll((HyperXDevice d) => d.DeviceType != HyperXDeviceType.Composite).Count != 0)
			{
				new Thread(delegate()
				{
					for (;;)
					{
						if (this.lastDeviceCount != this._devices.Count)
						{
							object obj = this.watchLock;
							lock (obj)
							{
								this.stopWatch = Stopwatch.StartNew();
							}
							this.count = this._devices.Count;
							this.lastDeviceCount = this._devices.Count;
							Thread.Sleep(100);
						}
						else
						{
							Stopwatch stopwatch = this.stopWatch;
							if (stopwatch != null && stopwatch.Elapsed.TotalSeconds > (double)10)
							{
								object obj = this.watchLock;
								lock (obj)
								{
									this.count--;
									if (this.count == 0)
									{
										StringBuilder stringBuilder = new StringBuilder();
										string value = string.Empty;
										foreach (HyperXDevice hyperXDevice in this._devices.FindAll((HyperXDevice d) => d.DeviceType != HyperXDeviceType.Composite))
										{
											stringBuilder.Append(HyperXDeviceUtils.GetShortDeviceTitle(hyperXDevice.Model) ?? "");
											stringBuilder.Append(", ");
										}
										if (stringBuilder.Length != 0)
										{
											value = stringBuilder.Remove(stringBuilder.Length - 2, 2).ToString();
											stringBuilder = stringBuilder.Clear();
											AnalyticsCenter.GAEvent<string>("Statistic", "MultiConnectedDevices", "NA", new Dictionary<byte, string>
											{
												{
													AnalyticsCenter.customDimMap["Devices"],
													value
												}
											});
										}
									}
									break;
								}
							}
							Thread.Sleep(1000);
						}
					}
				}).Start();
			}
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x0005EFDC File Offset: 0x0005D1DC
		public void InitializePublisherCacheHelper(string publisherCachePath, List<DeviceDetection.DeviceInfo> deviceInfos)
		{
			if (this._publisherCacheHelper == null)
			{
				List<DeviceInfo> list = new List<DeviceInfo>();
				foreach (DeviceDetection.DeviceInfo deviceInfo in deviceInfos)
				{
					list.Add(new DeviceInfo
					{
						VendorId = deviceInfo.VID,
						ProductId = deviceInfo.PID
					});
				}
				this._publisherCacheHelper = new PublisherCacheHelper(publisherCachePath, new Action(this.PublisherStorage_ConfigChanged), list);
				return;
			}
			Logger.WriteLine("Already created PublisherCacheHelper", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 2920);
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x0005F084 File Offset: 0x0005D284
		public bool CheckAndAddDeviceMetaData(int vID, int pID, string devicePath)
		{
			object locker = this._locker;
			lock (locker)
			{
				if (this._publisherCacheHelper == null)
				{
					this.InitializePublisherCacheHelper("", new List<DeviceDetection.DeviceInfo>
					{
						new DeviceDetection.DeviceInfo(vID, pID, devicePath)
					});
				}
				if (!this._publisherCacheHelper.CheckAndAddDeviceMetaData(vID, pID))
				{
					Logger.WriteLine("Device was already added or fail to add it", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 2943);
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x0005F110 File Offset: 0x0005D310
		public void TakeControlOfDeviceLighting(HyperXDevice device)
		{
			if (this._publisherCacheHelper == null)
			{
				Logger.WriteLine("_publisherCacheHelper == null, must initialize _publisherCacheHelper before using this method.", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 2959);
				return;
			}
			if (this._publisherCacheHelper.SetDeviceControl(device.VendorID, device.ProductID, 0))
			{
				device.SetupDevice();
				HyperXCenter.Center.OnDeviceUpdated(device);
				device.Start();
				device.SetupPreset(Preset.CurrentPreset);
				device.OnContorlByUpdateUI();
				return;
			}
			Logger.WriteLine("SetDeviceControl: Write config file failed.", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 2976);
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x0005F194 File Offset: 0x0005D394
		private void PublisherStorage_ConfigChanged()
		{
			if (this._publisherCacheHelper == null)
			{
				Logger.WriteLine("_publisherCacheHelper == null, must initialize _publisherCacheHelper before using this method.", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 2984);
				return;
			}
			if (!this._publisherCacheHelper.UpdateConfigModels())
			{
				Logger.WriteLine("PublisherStorage_ConfigChanged UpdateConfig fail.", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 2991);
				return;
			}
			foreach (DeviceMetaData deviceMetaData in this._publisherCacheHelper.ConfigModels.DeviceConfigs)
			{
				foreach (HyperXDevice hyperXDevice in this.Devices)
				{
					if (hyperXDevice.Model != HyperXDeviceModel.Composite && (int)hyperXDevice.VendorID == deviceMetaData.DeviceInfo.VendorId && (int)hyperXDevice.ProductID == deviceMetaData.DeviceInfo.ProductId)
					{
						if (deviceMetaData.ControlledBy.Contains("OLS"))
						{
							hyperXDevice.ControlBy = HyperXDevice.ControlBySoftware.OLS;
							hyperXDevice.StopEffectEngine();
							hyperXDevice.PauseCommandTask();
							this._compositeDevice.UnlinkDevice(hyperXDevice);
						}
						else
						{
							hyperXDevice.ControlBy = HyperXDevice.ControlBySoftware.NGENUITY;
						}
						HyperXCenter.Center.OnDeviceUpdated(hyperXDevice);
						hyperXDevice.OnContorlByUpdateUI();
						this._compositeDevice.OnContorlByUpdateUI();
						break;
					}
				}
			}
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x0005F2F8 File Offset: 0x0005D4F8
		private void CheckDeviceControlByWhom(HyperXDevice device)
		{
			if (this._publisherCacheHelper == null)
			{
				Logger.WriteLine("_publisherCacheHelper == null, must initialize _publisherCacheHelper before using this method.", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 3048);
				return;
			}
			if (this._publisherCacheHelper.ConfigModels.DeviceConfigs.Count <= 0)
			{
				return;
			}
			Logger.WriteLine(string.Format("Device Name: {0} PID: {1}", device.Model, device.ProductID), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 3057);
			foreach (DeviceMetaData deviceMetaData in this._publisherCacheHelper.ConfigModels.DeviceConfigs)
			{
				if (device.Model == HyperXDeviceModel.Composite)
				{
					break;
				}
				if ((int)device.VendorID == deviceMetaData.DeviceInfo.VendorId && (int)device.ProductID == deviceMetaData.DeviceInfo.ProductId)
				{
					Logger.WriteLine("Find match, the ControlBy of config is " + deviceMetaData.ControlledBy, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 3067);
					if (deviceMetaData.ControlledBy.Contains("OLS"))
					{
						device.ControlBy = HyperXDevice.ControlBySoftware.OLS;
						device.StopEffectEngine();
						device.PauseCommandTask();
						break;
					}
					device.ControlBy = HyperXDevice.ControlBySoftware.NGENUITY;
					break;
				}
			}
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x0005F440 File Offset: 0x0005D640
		private void CheckDeviceLightSync(HyperXDevice device)
		{
			if (!device.IsSimulator && device.Model != HyperXDeviceModel.Composite && device.CanLink)
			{
				int subDeviceCount = this._compositeDevice.GetSubDeviceCount(device.Model);
				int num = Settings.Instance.LightSyncDevices.Count((HyperXDeviceModel ld) => ld == device.Model);
				if (subDeviceCount < num)
				{
					this._compositeDevice.LinkDevice(device, CompositeDeviceDirection.Auto);
				}
			}
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x0005F4D0 File Offset: 0x0005D6D0
		internal void SetAllMirrorInfo(List<MonitorInfo> allMonitors)
		{
			try
			{
				if (!allMonitors.Any<MonitorInfo>())
				{
					Logger.WriteLine("No elements in allMonitors.", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 3118);
				}
				else
				{
					this._allMirrorInfo = new List<ScreenMirrorInfo>();
					decimal d = allMonitors.Min((MonitorInfo monitor) => monitor.ScaleRate);
					foreach (MonitorInfo monitorInfo in allMonitors)
					{
						decimal scaleRate = monitorInfo.ScaleRate;
						decimal value = monitorInfo.DisplayInfo.Monitor.X * d;
						decimal value2 = monitorInfo.DisplayInfo.Monitor.Y * d;
						decimal value3 = monitorInfo.DisplayInfo.Monitor.Width * scaleRate;
						decimal value4 = monitorInfo.DisplayInfo.Monitor.Height * scaleRate;
						ScreenMirrorInfo item = new ScreenMirrorInfo
						{
							IsPrimary = monitorInfo.DisplayInfo.IsPrimary,
							FriendlyDeviceName = monitorInfo.FriendlyDeviceName,
							ModelName = monitorInfo.ModelName,
							DevicePath = monitorInfo.DevicePath,
							Mirror = new MirrorRange
							{
								Width = (int)value3,
								Height = (int)value4,
								X = (int)value,
								Y = (int)value2
							}
						};
						this._allMirrorInfo.Add(item);
					}
					TypedEventHandler<object, List<ScreenMirrorInfo>> allMirrorChanged = this.AllMirrorChanged;
					if (allMirrorChanged != null)
					{
						allMirrorChanged.Invoke(this, this._allMirrorInfo);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.WriteLine("SetAllMirrorInfo, " + ex.Message, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCenter.cs", 3156);
			}
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x0005F6E4 File Offset: 0x0005D8E4
		internal void AddWebcam(string symbolicLink, Type type)
		{
			UVCDeviceBase uvcdeviceBase = (UVCDeviceBase)Activator.CreateInstance(type, new object[]
			{
				symbolicLink
			});
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				this._devices.Add(uvcdeviceBase);
			}
			this.OnDeviceAdded(uvcdeviceBase);
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x0005F748 File Offset: 0x0005D948
		internal void RemoveWebcam(UVCDeviceBase device)
		{
			if (device == null)
			{
				return;
			}
			bool flag = false;
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				flag = this._devices.Remove(device);
			}
			if (this.DeviceRemoved != null)
			{
				this.DeviceRemoved.Invoke(this, device);
			}
			if (flag)
			{
				List<string> deviceNames = this._deviceNames;
				lock (deviceNames)
				{
					for (int i = 0; i < this._deviceNames.Count; i++)
					{
						if (string.Equals(device.Name, this._deviceNames[i]))
						{
							this._deviceNames.RemoveAt(i);
							break;
						}
					}
				}
			}
			device.Dispose();
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x0005F820 File Offset: 0x0005DA20
		public static bool Is25C1()
		{
			string motherboardProduct = Utils.GetMotherboardProduct();
			for (int i = 0; i < HyperXCenter._model25C1.Count<string>(); i++)
			{
				if (motherboardProduct == HyperXCenter._model25C1[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x0005F85A File Offset: 0x0005DA5A
		public static string GetEmblemsEndpointSymbolString()
		{
			if (HyperXCenter.Is25C1())
			{
				return "HID#VID_03F0&PID_01BF&MI_05&Col02";
			}
			return "HID#VID_03F0&PID_01BF&MI_04&Col02";
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x0005F870 File Offset: 0x0005DA70
		public void LoadAllDevices()
		{
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				foreach (HyperXDevice hyperXDevice in this._devices)
				{
					if (!string.IsNullOrEmpty(hyperXDevice.DeviceID) && this.DeviceAdded != null)
					{
						Logger.WriteLine(string.Format("DeviceAdded: LoadAllDevices, Device: {0}", hyperXDevice.Model), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Devices\\HyperXCenter.cs", 29);
						this.DeviceAdded.Invoke(this, hyperXDevice);
					}
				}
				if (this.monitors != null)
				{
					foreach (HyperXMonitorBase hyperXMonitorBase in this.monitors)
					{
						TypedEventHandler<object, HyperXMonitorBase> monitorAdded = this.MonitorAdded;
						if (monitorAdded != null)
						{
							monitorAdded.Invoke(this, hyperXMonitorBase);
						}
					}
				}
				if (this.AllMirrorChanged != null)
				{
					TypedEventHandler<object, List<ScreenMirrorInfo>> allMirrorChanged = this.AllMirrorChanged;
					if (allMirrorChanged != null)
					{
						allMirrorChanged.Invoke(this, this._allMirrorInfo);
					}
				}
			}
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x0005F9A4 File Offset: 0x0005DBA4
		public void ReloadDevice(string deviceId)
		{
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				foreach (HyperXDevice hyperXDevice in this._devices)
				{
					if (string.Equals(hyperXDevice.DeviceID, deviceId, StringComparison.OrdinalIgnoreCase) && this.DeviceAdded != null)
					{
						this.DeviceAdded.Invoke(this, hyperXDevice);
					}
				}
			}
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x0005FA3C File Offset: 0x0005DC3C
		internal void OnUpdateDriverInstallationProgress(int progress, int reason)
		{
			this._driverInstallationExitCode = reason;
			CommandTransporter.SendCommandAsync<HyperXCenterCommand<int, int>>(new HyperXCenterCommand<int, int>
			{
				CommandType = HyperXCenterCommandType.UpdateDriverInstallationProgress,
				Arg1 = progress,
				Arg2 = reason
			});
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x0005FA68 File Offset: 0x0005DC68
		internal void SetOsdData(string deviceID, OsdConfig osd)
		{
			if (this.monitors != null)
			{
				HyperXMonitorBase hyperXMonitorBase = this.monitors.FirstOrDefault((HyperXMonitorBase x) => x.PhysicalData.DeviceID == deviceID);
				if (hyperXMonitorBase != null)
				{
					hyperXMonitorBase.SetOsdData(osd);
				}
			}
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x0005FAAC File Offset: 0x0005DCAC
		internal void ResetOsdData(string deviceID, OsdReset reset)
		{
			if (this.monitors != null)
			{
				HyperXMonitorBase hyperXMonitorBase = this.monitors.FirstOrDefault((HyperXMonitorBase x) => x.PhysicalData.DeviceID == deviceID);
				if (hyperXMonitorBase != null)
				{
					hyperXMonitorBase.ResetOsdData(reset);
				}
			}
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x0005FAF0 File Offset: 0x0005DCF0
		internal void RequestFreshOsd(HyperXMonitorBase monitor)
		{
			if (monitor != null)
			{
				TypedEventHandler<object, HyperXMonitorBase> monitorDataRefreshed = this.MonitorDataRefreshed;
				if (monitorDataRefreshed == null)
				{
					return;
				}
				monitorDataRefreshed.Invoke(this, monitor);
			}
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x0005FB08 File Offset: 0x0005DD08
		internal void ResetMonitors()
		{
			if (this.monitors != null)
			{
				foreach (string deviceID in (from x in this.monitors
				select x.PhysicalData.DeviceID).ToList<string>())
				{
					this.RemoveMonitorDevice(deviceID);
				}
			}
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x0005FB8C File Offset: 0x0005DD8C
		internal void NotifyOsdOn(string deviceID)
		{
			object obj = this.monitorLocker;
			lock (obj)
			{
				if (this.monitors != null)
				{
					HyperXMonitorBase hyperXMonitorBase = this.monitors.FirstOrDefault((HyperXMonitorBase x) => x.PhysicalData.DeviceID == deviceID);
					if (hyperXMonitorBase != null)
					{
						TypedEventHandler<object, HyperXMonitorBase> monitorOsdOn = this.MonitorOsdOn;
						if (monitorOsdOn != null)
						{
							monitorOsdOn.Invoke(this, hyperXMonitorBase);
						}
					}
				}
			}
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x0005FC0C File Offset: 0x0005DE0C
		internal void UpdateMontorDevice(PhysicalMonitorInfo vcpDevice)
		{
			object obj = this.monitorLocker;
			lock (obj)
			{
				int num = this.monitors.FindIndex((HyperXMonitorBase x) => x.PhysicalData.DeviceID == vcpDevice.DeviceID);
				if (num != -1)
				{
					this.monitors[num].UpdateVCPHandle(vcpDevice);
				}
			}
		}

		// Token: 0x04001E1A RID: 7706
		private static HyperXCenter _center;

		// Token: 0x04001E1B RID: 7707
		private static object _centerGuard = new object();

		// Token: 0x04001E1C RID: 7708
		private HyperXCompositeDevice _compositeDevice;

		// Token: 0x04001E27 RID: 7719
		private int _headsetCount;

		// Token: 0x04001E28 RID: 7720
		private Dictionary<uint, ushort> _embededFirmware = new Dictionary<uint, ushort>();

		// Token: 0x04001E29 RID: 7721
		private Dictionary<uint, ushort> _embededHostRFFirmware = new Dictionary<uint, ushort>();

		// Token: 0x04001E2A RID: 7722
		private Dictionary<uint, ushort> _embededClientRFFirmware = new Dictionary<uint, ushort>();

		// Token: 0x04001E2C RID: 7724
		private List<HyperXDevice> _devices;

		// Token: 0x04001E2D RID: 7725
		private List<HyperXMonitorBase> monitors;

		// Token: 0x04001E2E RID: 7726
		private List<ScreenMirrorInfo> _allMirrorInfo;

		// Token: 0x04001E2F RID: 7727
		private readonly object monitorLocker = new object();

		// Token: 0x04001E30 RID: 7728
		private List<string> _deviceNames = new List<string>();

		// Token: 0x04001E31 RID: 7729
		private Queue<HyperXCenter.DeviceAction> _deviceQueue;

		// Token: 0x04001E32 RID: 7730
		private List<string> _dfuList;

		// Token: 0x04001E34 RID: 7732
		private AutoResetEvent stopResetEvent;

		// Token: 0x04001E35 RID: 7733
		private Stopwatch stopWatch;

		// Token: 0x04001E36 RID: 7734
		private readonly object watchLock = new object();

		// Token: 0x04001E37 RID: 7735
		private int count;

		// Token: 0x04001E38 RID: 7736
		private int lastDeviceCount;

		// Token: 0x04001E39 RID: 7737
		private PublisherCacheHelper _publisherCacheHelper;

		// Token: 0x04001E3A RID: 7738
		private readonly object _locker = new object();

		// Token: 0x04001E3B RID: 7739
		private static readonly string[] _model25C1 = new string[]
		{
			"8E41",
			"8D41",
			"8D42",
			"8D87",
			"8DD5",
			"8D88",
			"8DD6"
		};

		// Token: 0x04001E3C RID: 7740
		private int _driverInstallationExitCode;

		// Token: 0x02000B58 RID: 2904
		private class DeviceAction
		{
			// Token: 0x1700082C RID: 2092
			// (get) Token: 0x0600413E RID: 16702 RVA: 0x00102D4A File Offset: 0x00100F4A
			// (set) Token: 0x0600413F RID: 16703 RVA: 0x00102D52 File Offset: 0x00100F52
			public string DeviceId { get; set; }

			// Token: 0x1700082D RID: 2093
			// (get) Token: 0x06004140 RID: 16704 RVA: 0x00102D5B File Offset: 0x00100F5B
			// (set) Token: 0x06004141 RID: 16705 RVA: 0x00102D63 File Offset: 0x00100F63
			public bool Added { get; set; }
		}

		// Token: 0x02000B59 RID: 2905
		private struct DriverInfo
		{
			// Token: 0x1700082E RID: 2094
			// (get) Token: 0x06004143 RID: 16707 RVA: 0x00102D6C File Offset: 0x00100F6C
			// (set) Token: 0x06004144 RID: 16708 RVA: 0x00102D74 File Offset: 0x00100F74
			public string PublishedName { get; set; }

			// Token: 0x1700082F RID: 2095
			// (get) Token: 0x06004145 RID: 16709 RVA: 0x00102D7D File Offset: 0x00100F7D
			// (set) Token: 0x06004146 RID: 16710 RVA: 0x00102D85 File Offset: 0x00100F85
			public string OriginalName { get; set; }

			// Token: 0x17000830 RID: 2096
			// (get) Token: 0x06004147 RID: 16711 RVA: 0x00102D8E File Offset: 0x00100F8E
			// (set) Token: 0x06004148 RID: 16712 RVA: 0x00102D96 File Offset: 0x00100F96
			public string ProviderName { get; set; }

			// Token: 0x17000831 RID: 2097
			// (get) Token: 0x06004149 RID: 16713 RVA: 0x00102D9F File Offset: 0x00100F9F
			// (set) Token: 0x0600414A RID: 16714 RVA: 0x00102DA7 File Offset: 0x00100FA7
			public string ClassName { get; set; }

			// Token: 0x17000832 RID: 2098
			// (get) Token: 0x0600414B RID: 16715 RVA: 0x00102DB0 File Offset: 0x00100FB0
			// (set) Token: 0x0600414C RID: 16716 RVA: 0x00102DB8 File Offset: 0x00100FB8
			public string ClassGUID { get; set; }

			// Token: 0x17000833 RID: 2099
			// (get) Token: 0x0600414D RID: 16717 RVA: 0x00102DC1 File Offset: 0x00100FC1
			// (set) Token: 0x0600414E RID: 16718 RVA: 0x00102DC9 File Offset: 0x00100FC9
			public string ExtensionID { get; set; }

			// Token: 0x17000834 RID: 2100
			// (get) Token: 0x0600414F RID: 16719 RVA: 0x00102DD2 File Offset: 0x00100FD2
			// (set) Token: 0x06004150 RID: 16720 RVA: 0x00102DDA File Offset: 0x00100FDA
			public Version Version { get; set; }

			// Token: 0x17000835 RID: 2101
			// (get) Token: 0x06004151 RID: 16721 RVA: 0x00102DE3 File Offset: 0x00100FE3
			// (set) Token: 0x06004152 RID: 16722 RVA: 0x00102DEB File Offset: 0x00100FEB
			public string SignerName { get; set; }
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NGenuity2.Common;
using NGenuity2.Effects;
using NGenuity2.Effects.Composite;
using NGenuity2.Model;
using Windows.Foundation;

namespace NGenuity2.Devices
{
	// Token: 0x0200064A RID: 1610
	public class HyperXCompositeDevice : HyperXDevice
	{
		// Token: 0x06001E3A RID: 7738 RVA: 0x00051220 File Offset: 0x0004F420
		public override void Serialize(BinaryWriter bw)
		{
			base.Serialize(bw);
			Dictionary<string, Dictionary<int, int>> subDeviceKeys = this.SubDeviceKeys;
			lock (subDeviceKeys)
			{
				bw.Write(this.SubDeviceKeys.Count);
				foreach (KeyValuePair<string, Dictionary<int, int>> keyValuePair in this.SubDeviceKeys)
				{
					bw.Write(keyValuePair.Key);
					bw.Write(keyValuePair.Value.Count);
					foreach (KeyValuePair<int, int> keyValuePair2 in keyValuePair.Value)
					{
						bw.Write(keyValuePair2.Key);
						bw.Write(keyValuePair2.Value);
					}
				}
			}
			bw.Write(937797155);
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x00051330 File Offset: 0x0004F530
		public override void Deserialize(BinaryReader br, int version)
		{
			base.Deserialize(br, version);
			Dictionary<string, Dictionary<int, int>> subDeviceKeys = this.SubDeviceKeys;
			lock (subDeviceKeys)
			{
				this.SubDeviceKeys.Clear();
				int num = br.ReadInt32();
				for (int i = 0; i < num; i++)
				{
					string key = br.ReadString();
					int num2 = br.ReadInt32();
					Dictionary<int, int> dictionary = new Dictionary<int, int>();
					for (int j = 0; j < num2; j++)
					{
						int key2 = br.ReadInt32();
						int value = br.ReadInt32();
						dictionary.Add(key2, value);
					}
					this.SubDeviceKeys.Add(key, dictionary);
				}
			}
			br.ReadInt32();
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06001E3C RID: 7740 RVA: 0x000513EC File Offset: 0x0004F5EC
		// (set) Token: 0x06001E3D RID: 7741 RVA: 0x000513F4 File Offset: 0x0004F5F4
		public IReadOnlyCollection<HyperXDevice> Devices { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06001E3E RID: 7742 RVA: 0x000513FD File Offset: 0x0004F5FD
		public IReadOnlyCollection<HyperXDevice> LeftDevices
		{
			get
			{
				return this._leftDevices;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06001E3F RID: 7743 RVA: 0x00051405 File Offset: 0x0004F605
		public IReadOnlyCollection<HyperXDevice> RightDevices
		{
			get
			{
				return this._rightDevices;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06001E40 RID: 7744 RVA: 0x0005140D File Offset: 0x0004F60D
		public IReadOnlyCollection<HyperXDevice> TopDevices
		{
			get
			{
				return this._topDevices;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06001E41 RID: 7745 RVA: 0x00051415 File Offset: 0x0004F615
		public IReadOnlyCollection<HyperXDevice> BottomDevices
		{
			get
			{
				return this._bottomDevices;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06001E42 RID: 7746 RVA: 0x0005141D File Offset: 0x0004F61D
		// (set) Token: 0x06001E43 RID: 7747 RVA: 0x00051425 File Offset: 0x0004F625
		public Dictionary<string, Dictionary<int, int>> SubDeviceKeys { get; set; } = new Dictionary<string, Dictionary<int, int>>();

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06001E44 RID: 7748 RVA: 0x0005142E File Offset: 0x0004F62E
		// (set) Token: 0x06001E45 RID: 7749 RVA: 0x00051438 File Offset: 0x0004F638
		public HyperXDevice CenterDevice
		{
			get
			{
				return this._centerDevice;
			}
			set
			{
				List<HyperXDevice> devices = this._devices;
				lock (devices)
				{
					if (this._centerDevice != value && this._centerDevice != null)
					{
						this._centerDevice.KeyReceived -= new TypedEventHandler<HyperXDevice, KeyEventAgrs>(this.SubDevice_KeyReceived);
						this._devices.Remove(this._centerDevice);
						this._centerDevice.Linked = false;
						this._centerDevice = null;
					}
					if (!this.IsFlatDevice(value))
					{
						return;
					}
					this._centerDevice = value;
					if (this._centerDevice != null)
					{
						this._centerDevice.Linked = true;
						this._devices.Add(this._centerDevice);
						this._centerDevice.KeyReceived += new TypedEventHandler<HyperXDevice, KeyEventAgrs>(this.SubDevice_KeyReceived);
					}
				}
				this.ComputeLayout();
				HyperXCenter.Center.OnDeviceUpdated(this);
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06001E46 RID: 7750 RVA: 0x0001C463 File Offset: 0x0001A663
		public override bool IsOpened
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06001E47 RID: 7751 RVA: 0x0001C463 File Offset: 0x0001A663
		public override bool IsValidDevice
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x00051520 File Offset: 0x0004F720
		public HyperXCompositeDevice()
		{
			this._devices = new List<HyperXDevice>();
			this._leftDevices = new List<HyperXDevice>();
			this._rightDevices = new List<HyperXDevice>();
			this._topDevices = new List<HyperXDevice>();
			this._bottomDevices = new List<HyperXDevice>();
			this._mappingKeys = new Dictionary<KeyMap, KeyMap>();
			base.Connected = true;
			base.DeviceID = Guid.NewGuid().ToString();
			base.Name = "Light Sync";
			base.DeviceType = HyperXDeviceType.Composite;
			base.Model = HyperXDeviceModel.Composite;
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x000515C4 File Offset: 0x0004F7C4
		public override void ChangeBrightness(int brightness)
		{
			base.ChangeBrightness(brightness);
			Settings.Instance.CompositeBrightness = brightness;
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				this._devices.ForEach(delegate(HyperXDevice device)
				{
					device.ChangeBrightness(brightness);
				});
			}
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x00051640 File Offset: 0x0004F840
		public override EffectImplBase CreateEffect(EffectItemBase item)
		{
			if (!(item is CompositeEffect))
			{
				return null;
			}
			CompositeEffect compositeEffect = (CompositeEffect)item;
			switch (compositeEffect.Type)
			{
			case CompositeEffectType.LoopedBreathing:
				return new CompositeBreathingEffectImpl((CompositeBreathingEffect)compositeEffect);
			case CompositeEffectType.LoopedConfetti:
				return new CompositeConfettiEffectImpl((CompositeConfettiEffect)compositeEffect);
			case CompositeEffectType.LoopedSolid:
				return new CompositeSolidEffectImpl((CompositeSolidEffect)compositeEffect);
			case CompositeEffectType.LoopedTwilight:
				return new CompositeTwilightEffectImpl((CompositeTwilightEffect)compositeEffect);
			case CompositeEffectType.LoopedSun:
				return new CompositeSunEffectImpl((CompositeSunEffect)compositeEffect);
			case CompositeEffectType.LoopedCycle:
				return new CompositeCycleEffectImpl((CompositeCycleEffect)compositeEffect);
			default:
				return null;
			}
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x000516D0 File Offset: 0x0004F8D0
		public override void ApplyEffects()
		{
			base.ApplyEffects();
			if (base.Engine == null)
			{
				return;
			}
			if (base.Preset != null)
			{
				foreach (CompositeEffect compositeEffect in base.Preset.Composite.Effects)
				{
					compositeEffect.ChangeDevice(this);
					if (compositeEffect.Visible)
					{
						EffectImplBase effectImplBase = this.CreateEffect(compositeEffect);
						if (effectImplBase != null)
						{
							base.Engine.AddEffect(effectImplBase);
						}
					}
				}
			}
			List<HyperXDevice> devices = this._devices;
			if (devices == null)
			{
				return;
			}
			devices.ForEach(delegate(HyperXDevice dev)
			{
				dev.ClearLightingCommands();
			});
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x00051794 File Offset: 0x0004F994
		protected void OnDeviceAdded(HyperXDevice device)
		{
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				if (device != null)
				{
					device.KeyReceived += new TypedEventHandler<HyperXDevice, KeyEventAgrs>(this.SubDevice_KeyReceived);
					this._devices.Add(device);
				}
			}
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x000517F0 File Offset: 0x0004F9F0
		protected void OnDeviceRemoved(HyperXDevice device)
		{
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				if (device != null)
				{
					device.KeyReceived -= new TypedEventHandler<HyperXDevice, KeyEventAgrs>(this.SubDevice_KeyReceived);
					this._devices.Remove(device);
				}
			}
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x0005184C File Offset: 0x0004FA4C
		private void SubDevice_KeyReceived(HyperXDevice sender, KeyEventAgrs args)
		{
			List<KeyMap> keys = base.Keys;
			lock (keys)
			{
				if (args.State == 1)
				{
					KeyMap keyMap = null;
					List<KeyMap> keys2 = sender.Keys;
					lock (keys2)
					{
						if (sender.DeviceType == HyperXDeviceType.Mouse || sender.DeviceType == HyperXDeviceType.Headset)
						{
							keyMap = sender.Keys[0];
						}
						else
						{
							keyMap = sender.Keys.FirstOrDefault((KeyMap o) => o.Key == args.KeyCode);
						}
					}
					if (keyMap != null)
					{
						foreach (KeyValuePair<KeyMap, KeyMap> keyValuePair in this._mappingKeys)
						{
							if (keyValuePair.Value == keyMap)
							{
								EffectEngine engine = base.Engine;
								if (engine == null)
								{
									break;
								}
								Dictionary<KeyMap, KeyMap>.Enumerator enumerator;
								keyValuePair = enumerator.Current;
								int column = keyValuePair.Key.Column;
								keyValuePair = enumerator.Current;
								engine.TriggerKey(column, keyValuePair.Key.Row);
								break;
							}
						}
					}
				}
			}
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x0005197C File Offset: 0x0004FB7C
		private bool IsFlatDevice(HyperXDevice device)
		{
			if (device == null)
			{
				return false;
			}
			HyperXDeviceType deviceType = device.DeviceType;
			if (deviceType <= HyperXDeviceType.Mousepad)
			{
				if (deviceType - HyperXDeviceType.Keyboard > 1)
				{
					if (deviceType != HyperXDeviceType.Mousepad)
					{
						return false;
					}
					return true;
				}
			}
			else if (deviceType != HyperXDeviceType.Headset && deviceType != HyperXDeviceType.Microphone)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x000519B4 File Offset: 0x0004FBB4
		private void ComputeLayout()
		{
			List<KeyMap> list = new List<KeyMap>();
			this._mappingKeys.Clear();
			base.ClearEngineEffects();
			Dictionary<string, Dictionary<int, int>> dictionary = new Dictionary<string, Dictionary<int, int>>();
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				if (this._devices.Count > 0)
				{
					base.FramePerSecond = this._devices.Min((HyperXDevice dev) => dev.FramePerSecond);
				}
			}
			int num = 0;
			int val = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			int num6 = 0;
			if (this._leftDevices.Count > 0)
			{
				int num7 = this._leftDevices.Sum((HyperXDevice device) => device.Keys.MaxGreaterThanZero((KeyMap key) => key.LinkColum + 1));
				val = this._leftDevices.MaxGreaterThanZero((HyperXDevice device) => device.Keys.MaxGreaterThanZero((KeyMap key) => key.LinkRow + 1));
			}
			if (this._rightDevices.Count > 0)
			{
				int num8 = this._rightDevices.Sum((HyperXDevice device) => device.Keys.MaxGreaterThanZero((KeyMap key) => key.LinkColum + 1));
				num5 = this._rightDevices.MaxGreaterThanZero((HyperXDevice device) => device.Keys.MaxGreaterThanZero((KeyMap key) => key.LinkRow + 1));
			}
			if (this._topDevices.Count > 0)
			{
				this._topDevices.MaxGreaterThanZero((HyperXDevice device) => device.Keys.MaxGreaterThanZero((KeyMap key) => key.LinkColum + 1));
				num2 = this._topDevices.Sum((HyperXDevice device) => device.Keys.MaxGreaterThanZero((KeyMap key) => key.LinkRow + 1));
			}
			if (this._bottomDevices.Count > 0)
			{
				this._bottomDevices.MaxGreaterThanZero((HyperXDevice device) => device.Keys.MaxGreaterThanZero((KeyMap key) => key.LinkColum + 1));
				num6 = this._bottomDevices.Sum((HyperXDevice device) => device.Keys.MaxGreaterThanZero((KeyMap key) => key.LinkRow + 1));
			}
			if (this.CenterDevice != null)
			{
				num3 = this._centerDevice.Keys.MaxGreaterThanZero((KeyMap key) => key.LinkColum + 1);
				num4 = this._centerDevice.Keys.MaxGreaterThanZero((KeyMap key) => key.LinkRow + 1);
			}
			num = num2 + Math.Max(Math.Max(num4, num5), val) + num6;
			if (this._centerDevice != null)
			{
				int num9 = (num - num4) / 2;
				int num10 = 0;
				Dictionary<int, int> dictionary2 = new Dictionary<int, int>();
				foreach (KeyMap keyMap in this._centerDevice.Keys)
				{
					KeyMap keyMap2 = keyMap.Clone();
					keyMap2.Row = keyMap.LinkRow + num9;
					keyMap2.Column = keyMap.LinkColum + num10;
					this._mappingKeys.Add(keyMap2, keyMap);
					list.Add(keyMap2);
					dictionary2.Add(keyMap2.Row << 16 | keyMap2.Column, keyMap.Row << 16 | keyMap.Column);
				}
				if (dictionary2.Count > 0)
				{
					dictionary.Add(this._centerDevice.DeviceID, dictionary2);
				}
				num10 = num3;
				num9 = (num - num5) / 2;
				foreach (HyperXDevice hyperXDevice in this._rightDevices)
				{
					dictionary2 = new Dictionary<int, int>();
					foreach (KeyMap keyMap3 in hyperXDevice.Keys)
					{
						KeyMap keyMap4 = keyMap3.Clone();
						keyMap4.Row = keyMap3.LinkRow + num9;
						keyMap4.Column = keyMap3.LinkColum + num10;
						this._mappingKeys.Add(keyMap4, keyMap3);
						list.Add(keyMap4);
						dictionary2.Add(keyMap4.Row << 16 | keyMap4.Column, keyMap3.Row << 16 | keyMap3.Column);
					}
					if (dictionary2.Count > 0)
					{
						dictionary.Add(hyperXDevice.DeviceID, dictionary2);
					}
					num10 += hyperXDevice.Keys.MaxGreaterThanZero((KeyMap key) => key.LinkColum + 1);
				}
			}
			List<KeyMap> keys = base.Keys;
			lock (keys)
			{
				base.Keys.Clear();
				base.Keys.AddRange(list);
			}
			Dictionary<string, Dictionary<int, int>> subDeviceKeys = this.SubDeviceKeys;
			lock (subDeviceKeys)
			{
				this.SubDeviceKeys.Clear();
				foreach (KeyValuePair<string, Dictionary<int, int>> keyValuePair in dictionary)
				{
					this.SubDeviceKeys.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			this.ApplyEffects();
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x00051FD8 File Offset: 0x000501D8
		public int GetSubDeviceCount(HyperXDeviceModel model)
		{
			int result = 0;
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				result = this._devices.Count((HyperXDevice o) => o.Model == model);
			}
			return result;
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x0005203C File Offset: 0x0005023C
		public void LinkDevice(HyperXDevice device, CompositeDeviceDirection direction)
		{
			if (device == null)
			{
				return;
			}
			Logger.WriteLine(string.Format("LinkDevice {0}", device.Model), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXCompositeDevice.cs", 379);
			if (device == null)
			{
				throw new ArgumentNullException();
			}
			if (this._centerDevice == device)
			{
				return;
			}
			if (this._centerDevice == null)
			{
				this.CenterDevice = device;
				return;
			}
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				this._rightDevices.Remove(device);
				this._leftDevices.Remove(device);
				this._topDevices.Remove(device);
				this._bottomDevices.Remove(device);
				if (this.IsFlatDevice(device))
				{
					switch (direction)
					{
					case CompositeDeviceDirection.Right:
						this._rightDevices.Add(device);
						break;
					case CompositeDeviceDirection.Left:
						this._leftDevices.Add(device);
						break;
					case (CompositeDeviceDirection)3:
						break;
					case CompositeDeviceDirection.Top:
						this._topDevices.Add(device);
						break;
					default:
						if (direction != CompositeDeviceDirection.Bottom)
						{
							if (direction == CompositeDeviceDirection.Auto)
							{
								this._rightDevices.Add(device);
							}
						}
						else
						{
							this._bottomDevices.Add(device);
						}
						break;
					}
				}
				device.Linked = true;
			}
			try
			{
				this.ComputeLayout();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			this.OnDeviceAdded(device);
			HyperXCenter.Center.OnDeviceUpdated(this);
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x00052194 File Offset: 0x00050394
		public void UnlinkDevice(HyperXDevice device)
		{
			if (device == null)
			{
				return;
			}
			bool flag = false;
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				flag = this._leftDevices.Remove(device);
				flag |= this._rightDevices.Remove(device);
				flag |= this._topDevices.Remove(device);
				flag |= this._bottomDevices.Remove(device);
				if (device == this._centerDevice)
				{
					this._centerDevice.KeyReceived -= new TypedEventHandler<HyperXDevice, KeyEventAgrs>(this.SubDevice_KeyReceived);
					this._centerDevice = null;
					if (this._rightDevices.Count > 0)
					{
						this._centerDevice = this._rightDevices[0];
						this._rightDevices.Remove(this._centerDevice);
					}
					flag = true;
				}
				device.Linked = false;
			}
			this.OnDeviceRemoved(device);
			if (flag)
			{
				this.ComputeLayout();
				HyperXCenter.Center.OnDeviceUpdated(this);
			}
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x0005228C File Offset: 0x0005048C
		protected override void RenderFrameToDevice(List<LightingItem> items)
		{
			base.RenderFrameToDevice(items);
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				List<KeyMap> keys = base.Keys;
				lock (keys)
				{
					foreach (HyperXDevice hyperXDevice in this._devices)
					{
						foreach (KeyMap keyMap in hyperXDevice.Keys)
						{
							Dictionary<KeyMap, KeyMap>.Enumerator enu = this._mappingKeys.GetEnumerator();
							bool flag3 = false;
							Func<LightingItem, bool> <>9__0;
							while (enu.MoveNext())
							{
								KeyValuePair<KeyMap, KeyMap> keyValuePair = enu.Current;
								if (keyValuePair.Value == keyMap)
								{
									Func<LightingItem, bool> predicate;
									if ((predicate = <>9__0) == null)
									{
										predicate = (<>9__0 = delegate(LightingItem o)
										{
											int x = o.X;
											KeyValuePair<KeyMap, KeyMap> keyValuePair2 = enu.Current;
											if (x == keyValuePair2.Key.Column)
											{
												int y = o.Y;
												keyValuePair2 = enu.Current;
												return y == keyValuePair2.Key.Row;
											}
											return false;
										});
									}
									LightingItem lightingItem = items.FirstOrDefault(predicate);
									if (lightingItem != null)
									{
										keyMap.Color = lightingItem.Color;
										flag3 = true;
										break;
									}
									break;
								}
							}
							if (!flag3)
							{
								keyMap.Color = Color.FromRGB(0, 0, 0);
							}
						}
						hyperXDevice.SetLightings(hyperXDevice.Keys);
						hyperXDevice.OnLightsChanged();
					}
				}
			}
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public override void SetAllLEDOff()
		{
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public override void SetLightings(IList<KeyMap> keys)
		{
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x0005245C File Offset: 0x0005065C
		public override void Stop(bool waitUntilStopped)
		{
			List<HyperXDevice> devices = this._devices;
			lock (devices)
			{
				this._devices.Clear();
			}
			base.Stop(waitUntilStopped);
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x000524A8 File Offset: 0x000506A8
		public override void Start()
		{
			base.Start();
			base.StartEffectEngine();
		}

		// Token: 0x04001D29 RID: 7465
		private const int SERIALIZER_TAILER = 937797155;

		// Token: 0x04001D2A RID: 7466
		private List<HyperXDevice> _devices;

		// Token: 0x04001D2B RID: 7467
		private List<HyperXDevice> _leftDevices;

		// Token: 0x04001D2C RID: 7468
		private List<HyperXDevice> _rightDevices;

		// Token: 0x04001D2D RID: 7469
		private List<HyperXDevice> _topDevices;

		// Token: 0x04001D2E RID: 7470
		private List<HyperXDevice> _bottomDevices;

		// Token: 0x04001D2F RID: 7471
		private HyperXDevice _centerDevice;

		// Token: 0x04001D30 RID: 7472
		private Dictionary<KeyMap, KeyMap> _mappingKeys;
	}
}

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
}

using System;
using System.ComponentModel;

namespace NGenuity2.Devices
{
	// Token: 0x02000646 RID: 1606
	public enum HyperXDeviceModel
	{
		// Token: 0x04001C13 RID: 7187
		Unknown,
		// Token: 0x04001C14 RID: 7188
		KeyboardAlloyElite,
		// Token: 0x04001C15 RID: 7189
		KeyboardAlloyFPS = 3,
		// Token: 0x04001C16 RID: 7190
		KeyboardAlloyOrigins = 5,
		// Token: 0x04001C17 RID: 7191
		KeyboardAlloyOriginsCore,
		// Token: 0x04001C18 RID: 7192
		KeyboardAlloyElite_II = 13,
		// Token: 0x04001C19 RID: 7193
		KeyboardAlloyOrigins60 = 17,
		// Token: 0x04001C1A RID: 7194
		KeyboardAlloyOrigins65,
		// Token: 0x04001C1B RID: 7195
		KeyboardAlloyMKW100,
		// Token: 0x04001C1C RID: 7196
		MousePulsefireSurge = 50,
		// Token: 0x04001C1D RID: 7197
		MousePulsefireRaid,
		// Token: 0x04001C1E RID: 7198
		MousePulsefireFPSPro,
		// Token: 0x04001C1F RID: 7199
		MousePulsefireCore,
		// Token: 0x04001C20 RID: 7200
		MousePulsefireDart,
		// Token: 0x04001C21 RID: 7201
		MousePulsefireFPSProCD,
		// Token: 0x04001C22 RID: 7202
		MousePulsefireHaste,
		// Token: 0x04001C23 RID: 7203
		MousePulsefireHasteWireless,
		// Token: 0x04001C24 RID: 7204
		MousePad_FURYUltra = 80,
		// Token: 0x04001C25 RID: 7205
		MousePad_PulsefireMatRGB,
		// Token: 0x04001C26 RID: 7206
		Headset_CloudFlightS = 100,
		// Token: 0x04001C27 RID: 7207
		Headset_StingerCoreWired,
		// Token: 0x04001C28 RID: 7208
		Headset_StingerCoreWireless,
		// Token: 0x04001C29 RID: 7209
		Headset_CloudAlphaS,
		// Token: 0x04001C2A RID: 7210
		Headset_ResolverUltraGame,
		// Token: 0x04001C2B RID: 7211
		Headset_StingerS,
		// Token: 0x04001C2C RID: 7212
		Headset_CloudIIWireless,
		// Token: 0x04001C2D RID: 7213
		Headset_CloudAlphaWireless,
		// Token: 0x04001C2E RID: 7214
		Headset_CloudIIWirelessDTS,
		// Token: 0x04001C2F RID: 7215
		Headset_CloudMIXBuds,
		// Token: 0x04001C30 RID: 7216
		Headset_CloudStinger2Wireless,
		// Token: 0x04001C31 RID: 7217
		Headset_CloudFlightWireless,
		// Token: 0x04001C32 RID: 7218
		Headset_Cloud2CoreWireless,
		// Token: 0x04001C33 RID: 7219
		Headset_Cloud2CoreWirelessTread,
		// Token: 0x04001C34 RID: 7220
		Headset_Ralphie,
		// Token: 0x04001C35 RID: 7221
		Headset_CloudIII,
		// Token: 0x04001C36 RID: 7222
		Headset_Cloud3Wireless,
		// Token: 0x04001C37 RID: 7223
		Headset_CloudMixBuds2,
		// Token: 0x04001C38 RID: 7224
		Headset_CloudMix2,
		// Token: 0x04001C39 RID: 7225
		Headset_Cloud3SWireless,
		// Token: 0x04001C3A RID: 7226
		Microphone_QuadcastS = 150,
		// Token: 0x04001C3B RID: 7227
		Microphone_Duocast,
		// Token: 0x04001C3C RID: 7228
		Microphone_Solocast,
		// Token: 0x04001C3D RID: 7229
		Microphone_QuadCast2,
		// Token: 0x04001C3E RID: 7230
		Microphone_Quadcast2S,
		// Token: 0x04001C3F RID: 7231
		DramPredator = 200,
		// Token: 0x04001C40 RID: 7232
		[Description("HyperX Vision S")]
		Webcam_VisionS = 300,
		// Token: 0x04001C41 RID: 7233
		Composite = 800,
		// Token: 0x04001C42 RID: 7234
		[Description("HyperX Pulsefire Haste 2")]
		UniversalMousePulsefireHaste2 = 900,
		// Token: 0x04001C43 RID: 7235
		[Description("HyperX Pulsefire Haste 2 Wireless")]
		UniversalMousePulsefireHaste2Wireless,
		// Token: 0x04001C44 RID: 7236
		[Description("HyperX Pulsefire Haste 2 Mini Wireless")]
		UniversalMousePulsefireHaste2MiniWireless,
		// Token: 0x04001C45 RID: 7237
		[Description("HyperX Alloy Rise")]
		UniversalKeyboardAlloyRise,
		// Token: 0x04001C46 RID: 7238
		[Description("HyperX Alloy Rise 75")]
		UniversalKeyboardAlloyRise75,
		// Token: 0x04001C47 RID: 7239
		[Description("HyperX Pulsefire Haste 2 Core")]
		UniversalMousePulsefireHaste2Core,
		// Token: 0x04001C48 RID: 7240
		[Description("HyperX Pulsefire Haste 2 Core Wireless")]
		UniversalMousePulsefireHaste2CoreWireless,
		// Token: 0x04001C49 RID: 7241
		[Description("HyperX Pulsefire Haste 2 S")]
		UniversalMousePulsefireHaste2SWireless,
		// Token: 0x04001C4A RID: 7242
		[Description("HyperX Pulsefire Fuse Wireless")]
		UniversalMousePulsefireFuseWireless,
		// Token: 0x04001C4B RID: 7243
		[Description("HyperX Alloy Rise 75 Wireless")]
		UniversalKeyboardAlloyRise75Wireless,
		// Token: 0x04001C4C RID: 7244
		[Description("HyperX Pulsefire Saga")]
		UniversalMousePulsefireSaga,
		// Token: 0x04001C4D RID: 7245
		[Description("HyperX Pulsefire Haste 2 Pro")]
		UniversalMousePulsefireHaste2Pro,
		// Token: 0x04001C4E RID: 7246
		[Description("HyperX Pulsefire Saga Pro")]
		UniversalMousePulsefireSagaPro,
		// Token: 0x04001C4F RID: 7247
		[Description("HyperX Wireless Embedded Receiver")]
		UniversalTIOEmblems,
		// Token: 0x04001C50 RID: 7248
		[Description("HyperX Cloud Flight 2")]
		Headset_CloudFlight2
	}
}

using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace NGenuity2.Devices
{
	// Token: 0x02000645 RID: 1605
	public static class HyperXDeviceModelExtensions
	{
		// Token: 0x06001E23 RID: 7715 RVA: 0x0004F920 File Offset: 0x0004DB20
		public static string GetDisplayName(this HyperXDeviceModel model)
		{
			FieldInfo field = model.GetType().GetField(model.ToString());
			DescriptionAttribute descriptionAttribute = ((field != null) ? field.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault<object>() : null) as DescriptionAttribute;
			if (descriptionAttribute != null)
			{
				return descriptionAttribute.Description;
			}
			return model.ToString();
		}
	}
}

using System;

namespace NGenuity2.Devices
{
	// Token: 0x02000647 RID: 1607
	public enum HyperXDeviceType
	{
		// Token: 0x04001C52 RID: 7250
		Unknown,
		// Token: 0x04001C53 RID: 7251
		Keyboard,
		// Token: 0x04001C54 RID: 7252
		Mouse,
		// Token: 0x04001C55 RID: 7253
		Mousepad = 4,
		// Token: 0x04001C56 RID: 7254
		Headset = 8,
		// Token: 0x04001C57 RID: 7255
		DRAM = 16,
		// Token: 0x04001C58 RID: 7256
		Microphone = 32,
		// Token: 0x04001C59 RID: 7257
		Webcam,
		// Token: 0x04001C5A RID: 7258
		ThreeInOneDongle,
		// Token: 0x04001C5B RID: 7259
		Composite = 128
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using NGenuity2.Common;

namespace NGenuity2.Devices
{
	// Token: 0x02000648 RID: 1608
	public static class HyperXDeviceUtils
	{
		// Token: 0x06001E24 RID: 7716 RVA: 0x0004F984 File Offset: 0x0004DB84
		public static bool IsDevice(ushort productId, params string[] patterns)
		{
			for (int i = 0; i < patterns.Length; i++)
			{
				ushort num;
				ushort num2;
				if (Utils.ParseVIDPID(patterns[i], out num, out num2) && num2 == productId)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x0004F9B8 File Offset: 0x0004DBB8
		public static bool IsCurrentDevice(HyperXDeviceModel deviceModel)
		{
			HyperXDeviceModel hyperXDeviceModel = HyperXDeviceModel.Unknown;
			if (HyperXCenter.Center.CurrentDevice != null)
			{
				hyperXDeviceModel = HyperXCenter.Center.CurrentDevice.Model;
			}
			return hyperXDeviceModel == deviceModel;
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06001E26 RID: 7718 RVA: 0x0004F9E8 File Offset: 0x0004DBE8
		public static HyperXDeviceType CurrentDeviceType
		{
			get
			{
				HyperXDevice currentDevice = HyperXCenter.Center.CurrentDevice;
				if (currentDevice == null)
				{
					return HyperXDeviceType.Unknown;
				}
				return currentDevice.DeviceType;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06001E27 RID: 7719 RVA: 0x0004FA0C File Offset: 0x0004DC0C
		public static HyperXDeviceModel CurrentDeviceModel
		{
			get
			{
				HyperXDevice currentDevice = HyperXCenter.Center.CurrentDevice;
				if (currentDevice == null)
				{
					return HyperXDeviceModel.Unknown;
				}
				return currentDevice.Model;
			}
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x0004FA30 File Offset: 0x0004DC30
		private static void InitKeyboards()
		{
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_16E6&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_16F6");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_098F&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0A8F");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_1711&MI_03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_1711&MI_01&Col05");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_1712");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_058F&MI_03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_058F&MI_01&Col05");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_068F");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_16BE&MI_02&Col05");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_16BE&MI_02&Col07");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_16BE&MI_01&Col05");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_16DC&MI_02&Col05");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_16DC&MI_02&Col07");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_16E5&MI_03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_16F5");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_16E5&MI_01&Col05");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0591&MI_03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0691");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0591&MI_01&Col05");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_1734&MI_03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_1735");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_1734&MI_01&Col05");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0C8E&MI_03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0D8E");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0C8E&MI_01&Col05");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_038F&MI_03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_048F");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_038F&MI_01&Col05");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_1756&MI_00");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0B8F&MI_00");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0C8F&MI_00");
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x0004FC2C File Offset: 0x0004DE2C
		private static void InitMouses()
		{
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_16E4&MI_01&Col05");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_16F9&MI_01&Col05");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_16E4&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0290&MI_01&Col05");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0390&MI_01&Col05");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0290&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_16D3&MI_01&Col05");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_16D3&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0490&MI_01&Col05");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0490&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_16DE&MI_01&Col05");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_16DE&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0D8F&MI_01&Col05");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0D8F&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_16E2&MI_01");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_16E1&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_16F8&MI_01");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_16F7&MI_01");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_088E&MI_01");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_068E&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_098E&MI_01");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_078E&MI_01");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_1729&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_172A");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0693&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0793");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_16D7&MI_01&Col05");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_16D7&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0E8F&MI_01&Col05");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0E8F&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_1727&MI_03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_1728");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_1727&MI_01&Col04");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0F8F&MI_03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0190");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0F8F&MI_01&Col04");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_028E&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_038E&MI_01");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_048E&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_058E&MI_01");
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x0004FE94 File Offset: 0x0004E094
		private static void InitUniversalDevices()
		{
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0B97&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0C97");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0F98&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0D97&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0199&MI_01");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0E97&MI_01");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0CBD&MI_03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0ABD&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0DBD&MI_01");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0BBD");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0BA0&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0DA0&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0CA0");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0EA0");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_04B5&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_05B5");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0FA0&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_01A1&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_02A1&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_03A1&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0AB5&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0BB5&MI_01");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_01B9&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_02B9");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0EB8&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0FB8");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0BB8&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0DB8");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0AB8&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0CB8");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0FBD&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0EBE&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0FBE&MI_01");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_04BF&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_06BF&MI_03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_05BF");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_07BF&MI_01");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_01BF&MI_05&Col02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_01BF&MI_04&Col02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_01C5&Col05");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0AC1&MI_03&Col04");
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x00050108 File Offset: 0x0004E308
		private static void InitMousepads()
		{
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_1705&MI_00");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_1706");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_1705&MI_01&Col02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0493&MI_00");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0593");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0493&MI_01&Col02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_1741&MI_01");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_1742");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0F8D&MI_01");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_018E");
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x000501AC File Offset: 0x0004E3AC
		private static void InitHeadsets()
		{
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_1715&MI_03&Col02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_1709&MI_03&Col02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_170B&MI_03&Col03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_16ED&MI_05&Col03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0C91&MI_05&Col03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_16ED&MI_05&Col01");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_16EA&MI_05&Col03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_1718&MI_03&Col03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0C8A&MI_03&Col03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_1743&MI_03&Col01");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_1765&MI_03&Col01");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_098D&MI_03&Col01");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_018B&MI_03&Col03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0696&MI_03&Col03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_058B&Col02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_038B&Col02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_078A&mi_03&col04");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_078A&mi_03&col05");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0D93&mi_03&col03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_16C4&mi_03&col03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_1723&mi_03&col03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0E90&mi_03&col03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0995&MI_03&Col02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_069F&MI_03&Col02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_099C&Col03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0EBC&Col03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0AA0&MI_03&Col03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_089F&MI_03&Col03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0A9F&MI_03&Col03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_08B7&MI_03&Col03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_089D&MI_03&Col02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0C9D&MI_03&Col01");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_05B7&MI_03&Col01");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0E9D");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_06B7");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_039E&MI_03&Col05");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0FAE&MI_03&Col06");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_06BE&MI_03&Col05");
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x000503F4 File Offset: 0x0004E5F4
		private static void InitMicrophones()
		{
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_171F&MI_00");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_171F&MI_01&Col02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_048C&MI_00");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_048C&MI_01&Col02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_068C&MI_00");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_068C&MI_01&Col02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0F8B&MI_00");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0F8B&MI_01&Col02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_028C&MI_00");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_028C&MI_01&Col02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_1720");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_1774");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_09AF&MI_00");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0AAF");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_07AF&MI_03&Col04");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_07B4&MI_02&Col04");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_02B5&MI_01&Col02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_03B5");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0D84&MI_03&Col06");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0EB4");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0A8C&MI_03");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_098C&MI_00");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_088C");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_175F");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_0951&PID_170F&MI_02&Col01");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_078B&MI_02&Col01");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0592&MI_02&Col01");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_098B&MI_02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0992&MI_02&Col02");
			HyperXDeviceUtils.DEVICE_LIST.Add("HID#VID_03F0&PID_0B8B&MI_02&Col02");
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x000505C4 File Offset: 0x0004E7C4
		static HyperXDeviceUtils()
		{
			HyperXDeviceUtils.InitKeyboards();
			HyperXDeviceUtils.InitMouses();
			HyperXDeviceUtils.InitMousepads();
			HyperXDeviceUtils.InitHeadsets();
			HyperXDeviceUtils.InitMicrophones();
			HyperXDeviceUtils.InitUniversalDevices();
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x00050640 File Offset: 0x0004E840
		public static string GetSupportLink(HyperXDeviceModel model)
		{
			string empty = string.Empty;
			if (model <= HyperXDeviceModel.MousePulsefireHaste)
			{
				if (model <= HyperXDeviceModel.KeyboardAlloyElite_II)
				{
					switch (model)
					{
					case HyperXDeviceModel.KeyboardAlloyElite:
						return "https://support.hyperx.com/Keyboards/Alloy-Elite/";
					case (HyperXDeviceModel)2:
					case (HyperXDeviceModel)4:
						break;
					case HyperXDeviceModel.KeyboardAlloyFPS:
						return "https://support.hyperx.com/Keyboards/Alloy-FPS/";
					case HyperXDeviceModel.KeyboardAlloyOrigins:
						return "https://support.hyperx.com/Keyboards/Alloy-Origins/";
					case HyperXDeviceModel.KeyboardAlloyOriginsCore:
						return "https://support.hyperx.com/Keyboards/Alloy-Origins-Core/";
					default:
						if (model == HyperXDeviceModel.KeyboardAlloyElite_II)
						{
							return "https://support.hyperx.com/Keyboards/Alloy-Elite-2/";
						}
						break;
					}
				}
				else
				{
					if (model == HyperXDeviceModel.KeyboardAlloyOrigins60)
					{
						return "https://support.hyperx.com/Keyboards/Alloy-Origins-60/";
					}
					if (model == HyperXDeviceModel.KeyboardAlloyMKW100)
					{
						return "https://support.hyperx.com/Keyboards/MKW100/";
					}
					switch (model)
					{
					case HyperXDeviceModel.MousePulsefireSurge:
						return "https://support.hyperx.com/Mice/Pulsefire-Surge/";
					case HyperXDeviceModel.MousePulsefireRaid:
						return "https://support.hyperx.com/Mice/Pulsefire-Raid/";
					case HyperXDeviceModel.MousePulsefireFPSPro:
					case HyperXDeviceModel.MousePulsefireFPSProCD:
						return "https://support.hyperx.com/Mice/Pulsefire-FPS-Pro/";
					case HyperXDeviceModel.MousePulsefireCore:
						return "https://support.hyperx.com/Mice/Pulsefire-Core/";
					case HyperXDeviceModel.MousePulsefireDart:
						return "https://support.hyperx.com/Mice/Pulsefire-Dart/";
					case HyperXDeviceModel.MousePulsefireHaste:
						return "https://support.hyperx.com/Mice/Pulsefire-Haste/";
					}
				}
			}
			else if (model <= HyperXDeviceModel.MousePad_PulsefireMatRGB)
			{
				if (model == HyperXDeviceModel.MousePad_FURYUltra)
				{
					return "https://support.hyperx.com/Mouse-Pads/FURY-Ultra/";
				}
				if (model == HyperXDeviceModel.MousePad_PulsefireMatRGB)
				{
					return "https://support.hyperx.com/Mouse-Pads/Pulsefire-Mat-RGB/";
				}
			}
			else
			{
				switch (model)
				{
				case HyperXDeviceModel.Headset_CloudFlightS:
					return "https://support.hyperx.com/Headsets/Cloud-Flight-S/";
				case HyperXDeviceModel.Headset_StingerCoreWired:
					return "https://support.hyperx.com/Headsets/Cloud-Stinger-Core-7-1/";
				case HyperXDeviceModel.Headset_StingerCoreWireless:
					return "https://support.hyperx.com/Headsets/Cloud-Stinger-Core-Wireless-7-1/";
				case HyperXDeviceModel.Headset_CloudAlphaS:
					return "https://support.hyperx.com/Headsets/Cloud-Alpha-S/";
				case HyperXDeviceModel.Headset_ResolverUltraGame:
				case HyperXDeviceModel.Headset_CloudAlphaWireless:
				case HyperXDeviceModel.Headset_CloudIIWirelessDTS:
				case HyperXDeviceModel.Headset_CloudMIXBuds:
					break;
				case HyperXDeviceModel.Headset_StingerS:
					return "https://support.hyperx.com/Headsets/Cloud-Stinger-S/";
				case HyperXDeviceModel.Headset_CloudIIWireless:
					return "https://support.hyperx.com/Headsets/Cloud-II-Wireless/";
				case HyperXDeviceModel.Headset_CloudStinger2Wireless:
					return "https://support.hyperx.com/Headsets/Cloud-Stinger-2-Wireless/";
				case HyperXDeviceModel.Headset_CloudFlightWireless:
					return "https://support.hyperx.com/Headsets/Cloud-Flight/";
				case HyperXDeviceModel.Headset_Cloud2CoreWireless:
					return "https://support.hyperx.com/Headsets/Cloud-II-Core-Wireless/";
				case HyperXDeviceModel.Headset_Cloud2CoreWirelessTread:
					return "https://support.hyperx.com/Headsets/";
				case HyperXDeviceModel.Headset_Ralphie:
					return "https://support.hyperx.com/Headsets/";
				case HyperXDeviceModel.Headset_CloudIII:
					return "https://support.hyperx.com/Headsets/Cloud-III/";
				case HyperXDeviceModel.Headset_Cloud3Wireless:
					return "https://support.hyperx.com/Headsets/Cloud-III-Wireless/";
				case HyperXDeviceModel.Headset_CloudMixBuds2:
					return "https://support.hyperx.com/Earbuds/";
				case HyperXDeviceModel.Headset_CloudMix2:
					return "https://support.hyperx.com/Headsets/";
				case HyperXDeviceModel.Headset_Cloud3SWireless:
					return "https://support.hyperx.com/Headsets/";
				default:
					switch (model)
					{
					case HyperXDeviceModel.Microphone_QuadcastS:
						return "https://support.hyperx.com/Microphones/Quadcast-S/";
					case HyperXDeviceModel.Microphone_Duocast:
						break;
					case HyperXDeviceModel.Microphone_Solocast:
						return "https://support.hyperx.com/Microphones/SoloCast/";
					case HyperXDeviceModel.Microphone_QuadCast2:
					case HyperXDeviceModel.Microphone_Quadcast2S:
						return "https://support.hyperx.com/Microphones/";
					default:
						switch (model)
						{
						case HyperXDeviceModel.UniversalMousePulsefireHaste2:
							return "https://support.hyperx.com/Mice/Pulsefire-Haste-2/";
						case HyperXDeviceModel.UniversalMousePulsefireHaste2Wireless:
							return "https://support.hyperx.com/Mice/Pulsefire-Haste-2-Wireless/";
						case HyperXDeviceModel.UniversalMousePulsefireHaste2MiniWireless:
							return "https://support.hyperx.com/Mice/Pulsefire-Haste-2-Mini/";
						case HyperXDeviceModel.UniversalMousePulsefireHaste2Core:
						case HyperXDeviceModel.UniversalMousePulsefireHaste2CoreWireless:
						case HyperXDeviceModel.UniversalMousePulsefireHaste2SWireless:
						case HyperXDeviceModel.UniversalMousePulsefireFuseWireless:
							return "https://support.hyperx.com/Mice/";
						case HyperXDeviceModel.UniversalTIOEmblems:
							return "https://support.hyperx.com";
						}
						break;
					}
					break;
				}
			}
			return "https://support.hyperx.com";
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x00050924 File Offset: 0x0004EB24
		public static bool IsSupportedDevice(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return false;
			}
			Logger.WriteLine("Found id: " + id, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Common\\Devices\\HyperXDeviceUtils.cs", 1069);
			foreach (string text in HyperXDeviceUtils.DEVICE_LIST)
			{
				if (Utils.IsCultureInvariantMatch(id, new string[]
				{
					text
				}))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x000509AC File Offset: 0x0004EBAC
		public static string GetDeviceTitle(HyperXDevice device)
		{
			string result;
			try
			{
				if (Utils.IsUniversalDevice(device.Model) || device is UVCDeviceBase)
				{
					result = device.Model.GetDisplayName();
				}
				else
				{
					if (device.Model == HyperXDeviceModel.Headset_Ralphie)
					{
						HyperXHeadsetDevice hyperXHeadsetDevice = device as HyperXHeadsetDevice;
						if (hyperXHeadsetDevice != null)
						{
							return HyperXDeviceUtils.GetBuildInHeadsetDeviceTitle(device.Model, hyperXHeadsetDevice.RemoteDeviceModel);
						}
					}
					result = HyperXDeviceUtils.GetDeviceTitleByModel(device.Model);
				}
			}
			catch (Exception)
			{
				result = string.Format("{0}", device.Model);
			}
			return result;
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x00050A3C File Offset: 0x0004EC3C
		public static string GetDeviceTitleByModel(HyperXDeviceModel deviceModel)
		{
			if (Utils.IsUniversalDevice(deviceModel))
			{
				return deviceModel.GetDisplayName();
			}
			if (deviceModel <= HyperXDeviceModel.MousePulsefireHasteWireless)
			{
				switch (deviceModel)
				{
				case HyperXDeviceModel.Unknown:
					return string.Empty;
				case HyperXDeviceModel.KeyboardAlloyElite:
					return "HyperX Alloy Elite RGB";
				case (HyperXDeviceModel)2:
				case (HyperXDeviceModel)4:
					break;
				case HyperXDeviceModel.KeyboardAlloyFPS:
					return "HyperX Alloy FPS RGB";
				case HyperXDeviceModel.KeyboardAlloyOrigins:
					return "HyperX Alloy Origins";
				case HyperXDeviceModel.KeyboardAlloyOriginsCore:
					return "HyperX Alloy Origins Core";
				default:
					switch (deviceModel)
					{
					case HyperXDeviceModel.KeyboardAlloyElite_II:
						return "HyperX Alloy Elite 2";
					case (HyperXDeviceModel)14:
					case (HyperXDeviceModel)15:
					case (HyperXDeviceModel)16:
						break;
					case HyperXDeviceModel.KeyboardAlloyOrigins60:
						return "HyperX Alloy Origins 60";
					case HyperXDeviceModel.KeyboardAlloyOrigins65:
						return "HyperX Alloy Origins 65";
					case HyperXDeviceModel.KeyboardAlloyMKW100:
						return "HyperX Alloy MKW 100";
					default:
						switch (deviceModel)
						{
						case HyperXDeviceModel.MousePulsefireSurge:
							return "HyperX Pulsefire Surge";
						case HyperXDeviceModel.MousePulsefireRaid:
							return "HyperX Pulsefire Raid";
						case HyperXDeviceModel.MousePulsefireFPSPro:
						case HyperXDeviceModel.MousePulsefireFPSProCD:
							return "HyperX Pulsefire FPS Pro";
						case HyperXDeviceModel.MousePulsefireCore:
							return "HyperX Pulsefire Core";
						case HyperXDeviceModel.MousePulsefireDart:
							return "HyperX Pulsefire Dart";
						case HyperXDeviceModel.MousePulsefireHaste:
							return "HyperX Pulsefire Haste";
						case HyperXDeviceModel.MousePulsefireHasteWireless:
							return "HyperX Pulsefire Haste Wireless";
						}
						break;
					}
					break;
				}
			}
			else if (deviceModel <= HyperXDeviceModel.MousePad_PulsefireMatRGB)
			{
				if (deviceModel == HyperXDeviceModel.MousePad_FURYUltra)
				{
					return "HyperX FURY Ultra";
				}
				if (deviceModel == HyperXDeviceModel.MousePad_PulsefireMatRGB)
				{
					return "HyperX Pulsefire Mat";
				}
			}
			else
			{
				switch (deviceModel)
				{
				case HyperXDeviceModel.Headset_CloudFlightS:
					return "HyperX Cloud Flight S";
				case HyperXDeviceModel.Headset_StingerCoreWired:
					return "HyperX Cloud Stinger Core Wired + 7.1";
				case HyperXDeviceModel.Headset_StingerCoreWireless:
					return "HyperX Cloud Stinger Core Wireless + 7.1";
				case HyperXDeviceModel.Headset_CloudAlphaS:
					return "HyperX Cloud Alpha S";
				case HyperXDeviceModel.Headset_ResolverUltraGame:
				case HyperXDeviceModel.Headset_Ralphie:
					break;
				case HyperXDeviceModel.Headset_StingerS:
					return "HyperX Cloud Stinger S";
				case HyperXDeviceModel.Headset_CloudIIWireless:
					return "HyperX Cloud II Wireless";
				case HyperXDeviceModel.Headset_CloudAlphaWireless:
					return "HyperX Cloud Alpha Wireless";
				case HyperXDeviceModel.Headset_CloudIIWirelessDTS:
					return "HyperX Cloud II Wireless";
				case HyperXDeviceModel.Headset_CloudMIXBuds:
					return "HyperX Cloud MIX Buds";
				case HyperXDeviceModel.Headset_CloudStinger2Wireless:
					return "HyperX Cloud Stinger 2 Wireless";
				case HyperXDeviceModel.Headset_CloudFlightWireless:
					return "HyperX Cloud Flight Wireless";
				case HyperXDeviceModel.Headset_Cloud2CoreWireless:
					return "HyperX Cloud II Core Wireless";
				case HyperXDeviceModel.Headset_Cloud2CoreWirelessTread:
					return "HyperX Cloud II Core Wireless";
				case HyperXDeviceModel.Headset_CloudIII:
					return "HyperX Cloud III";
				case HyperXDeviceModel.Headset_Cloud3Wireless:
					return "HyperX Cloud III Wireless";
				case HyperXDeviceModel.Headset_CloudMixBuds2:
					return "HyperX Cloud Mix Buds 2";
				case HyperXDeviceModel.Headset_CloudMix2:
					return "HyperX Cloud Mix 2";
				case HyperXDeviceModel.Headset_Cloud3SWireless:
					return "HyperX Cloud III S Wireless";
				default:
					switch (deviceModel)
					{
					case HyperXDeviceModel.Microphone_QuadcastS:
						return "HyperX QuadCast S";
					case HyperXDeviceModel.Microphone_Duocast:
						return "HyperX DuoCast";
					case HyperXDeviceModel.Microphone_Solocast:
						return "HyperX SoloCast";
					case HyperXDeviceModel.Microphone_QuadCast2:
						return "HyperX QuadCast 2";
					case HyperXDeviceModel.Microphone_Quadcast2S:
						return "HyperX QuadCast 2 S";
					}
					break;
				}
			}
			return string.Empty;
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x00050C65 File Offset: 0x0004EE65
		public static string GetBuildInHeadsetDeviceTitle(HyperXDeviceModel model, HyperXRemoteDeviceModel remoteModel)
		{
			if (model == HyperXDeviceModel.Headset_Ralphie)
			{
				if (remoteModel == HyperXRemoteDeviceModel.Unknown)
				{
					return "HyperX Internal Wireless Audio Adapter";
				}
				if (remoteModel == HyperXRemoteDeviceModel.Remote_Headset_Cloud2CoreWirelessTread)
				{
					return "HyperX Cloud II Core Wireless";
				}
				if (remoteModel == HyperXRemoteDeviceModel.Remote_Headset_Cloud3WirelessAtlas)
				{
					return "HyperX Cloud III Wireless";
				}
			}
			return string.Empty;
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x00050C94 File Offset: 0x0004EE94
		public static string GetShortDeviceTitle(HyperXDeviceModel model)
		{
			try
			{
				if (Utils.IsUniversalDevice(model))
				{
					return model.GetDisplayName().Replace("HyperX ", "");
				}
				if (model <= HyperXDeviceModel.KeyboardAlloyMKW100)
				{
					switch (model)
					{
					case HyperXDeviceModel.Unknown:
						goto IL_353;
					case HyperXDeviceModel.KeyboardAlloyElite:
						return "Alloy Elite RGB";
					case (HyperXDeviceModel)2:
					case (HyperXDeviceModel)4:
						break;
					case HyperXDeviceModel.KeyboardAlloyFPS:
						return "Alloy FPS RGB";
					case HyperXDeviceModel.KeyboardAlloyOrigins:
						return "Alloy Origins";
					case HyperXDeviceModel.KeyboardAlloyOriginsCore:
						return "Alloy Origins Core";
					default:
						switch (model)
						{
						case HyperXDeviceModel.KeyboardAlloyElite_II:
							return "Alloy Elite 2";
						case HyperXDeviceModel.KeyboardAlloyOrigins60:
							return "Alloy Origins 60";
						case HyperXDeviceModel.KeyboardAlloyOrigins65:
							return "Alloy Origins 65";
						case HyperXDeviceModel.KeyboardAlloyMKW100:
							return "Alloy MKW 100";
						}
						break;
					}
				}
				else
				{
					switch (model)
					{
					case HyperXDeviceModel.MousePulsefireSurge:
						return "Pulsefire Surge";
					case HyperXDeviceModel.MousePulsefireRaid:
						return "Pulsefire Raid";
					case HyperXDeviceModel.MousePulsefireFPSPro:
					case HyperXDeviceModel.MousePulsefireFPSProCD:
						return "Pulsefire FPS Pro";
					case HyperXDeviceModel.MousePulsefireCore:
						return "Pulsefire Core";
					case HyperXDeviceModel.MousePulsefireDart:
						return "Pulsefire Dart";
					case HyperXDeviceModel.MousePulsefireHaste:
						return "Pulsefire Haste";
					case HyperXDeviceModel.MousePulsefireHasteWireless:
						return "Pulsefire Haste Wireless";
					default:
						switch (model)
						{
						case HyperXDeviceModel.MousePad_FURYUltra:
							return "FURY Ultra";
						case HyperXDeviceModel.MousePad_PulsefireMatRGB:
							return "Pulsefire Mat";
						case (HyperXDeviceModel)82:
						case (HyperXDeviceModel)83:
						case (HyperXDeviceModel)84:
						case (HyperXDeviceModel)85:
						case (HyperXDeviceModel)86:
						case (HyperXDeviceModel)87:
						case (HyperXDeviceModel)88:
						case (HyperXDeviceModel)89:
						case (HyperXDeviceModel)90:
						case (HyperXDeviceModel)91:
						case (HyperXDeviceModel)92:
						case (HyperXDeviceModel)93:
						case (HyperXDeviceModel)94:
						case (HyperXDeviceModel)95:
						case (HyperXDeviceModel)96:
						case (HyperXDeviceModel)97:
						case (HyperXDeviceModel)98:
						case (HyperXDeviceModel)99:
						case HyperXDeviceModel.Headset_ResolverUltraGame:
							break;
						case HyperXDeviceModel.Headset_CloudFlightS:
							return "Cloud Flight S";
						case HyperXDeviceModel.Headset_StingerCoreWired:
							return "Stinger Core Wired";
						case HyperXDeviceModel.Headset_StingerCoreWireless:
							return "Stinger Core Wireless";
						case HyperXDeviceModel.Headset_CloudAlphaS:
							return "Cloud Alpha S";
						case HyperXDeviceModel.Headset_StingerS:
							return "Cloud Stinger S";
						case HyperXDeviceModel.Headset_CloudIIWireless:
							return "Cloud II Wireless";
						case HyperXDeviceModel.Headset_CloudAlphaWireless:
							return "Cloud Alpha Wireless";
						case HyperXDeviceModel.Headset_CloudIIWirelessDTS:
							return "Cloud II Wireless";
						case HyperXDeviceModel.Headset_CloudMIXBuds:
							return "Cloud MIX Buds";
						case HyperXDeviceModel.Headset_CloudStinger2Wireless:
							return "Cloud Stinger 2 Wireless";
						case HyperXDeviceModel.Headset_CloudFlightWireless:
							return "Cloud Flight Wireless";
						case HyperXDeviceModel.Headset_Cloud2CoreWireless:
							return "Cloud II Core Wireless";
						case HyperXDeviceModel.Headset_Cloud2CoreWirelessTread:
							return "Cloud II Core Wireless";
						case HyperXDeviceModel.Headset_Ralphie:
						{
							HyperXHeadsetDevice hyperXHeadsetDevice = HyperXCenter.Center.Devices.FirstOrDefault((HyperXDevice dev) => dev.Model == HyperXDeviceModel.Headset_Ralphie) as HyperXHeadsetDevice;
							if (hyperXHeadsetDevice != null)
							{
								return HyperXDeviceUtils.GetBuildInHeadsetShortDeviceTitle(model, hyperXHeadsetDevice.RemoteDeviceModel);
							}
							goto IL_353;
						}
						case HyperXDeviceModel.Headset_CloudIII:
							return "Cloud III";
						case HyperXDeviceModel.Headset_Cloud3Wireless:
							return "Cloud III Wireless";
						case HyperXDeviceModel.Headset_CloudMixBuds2:
							return "Cloud Mix Buds 2";
						case HyperXDeviceModel.Headset_CloudMix2:
							return "Cloud Mix 2";
						case HyperXDeviceModel.Headset_Cloud3SWireless:
							return "Cloud III S Wireless";
						default:
							switch (model)
							{
							case HyperXDeviceModel.Microphone_QuadcastS:
								return "QuadCast S";
							case HyperXDeviceModel.Microphone_Duocast:
								return "DuoCast";
							case HyperXDeviceModel.Microphone_Solocast:
								return "SoloCast";
							case HyperXDeviceModel.Microphone_QuadCast2:
								return "QuadCast 2";
							case HyperXDeviceModel.Microphone_Quadcast2S:
								return "QuadCast 2 S";
							}
							break;
						}
						break;
					}
				}
				return "Unknown device";
				IL_353:;
			}
			catch
			{
				return string.Format("{0}", model);
			}
			return string.Empty;
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x00051030 File Offset: 0x0004F230
		public static string GetBuildInHeadsetShortDeviceTitle(HyperXDeviceModel model, HyperXRemoteDeviceModel remoteModel)
		{
			if (model == HyperXDeviceModel.Headset_Ralphie)
			{
				if (remoteModel == HyperXRemoteDeviceModel.Unknown)
				{
					return "Internal Wireless Audio Adapter";
				}
				if (remoteModel == HyperXRemoteDeviceModel.Remote_Headset_Cloud2CoreWirelessTread)
				{
					return "Cloud II Core Wireless";
				}
				if (remoteModel == HyperXRemoteDeviceModel.Remote_Headset_Cloud3WirelessAtlas)
				{
					return "Cloud III Wireless";
				}
			}
			return string.Empty;
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06001E36 RID: 7734 RVA: 0x00051060 File Offset: 0x0004F260
		public static bool CurrentDeviceIsLegacyDevice
		{
			get
			{
				HyperXDeviceModel model = HyperXCenter.Center.CurrentDevice.Model;
				if (model <= HyperXDeviceModel.KeyboardAlloyElite)
				{
					if (model == HyperXDeviceModel.Unknown)
					{
						return false;
					}
					if (model != HyperXDeviceModel.KeyboardAlloyElite)
					{
						return false;
					}
				}
				else if (model != HyperXDeviceModel.KeyboardAlloyMKW100 && model != HyperXDeviceModel.MousePulsefireDart)
				{
					return false;
				}
				return true;
			}
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x00051098 File Offset: 0x0004F298
		public static int GetSupportOnBoardProfileCount(HyperXDevice device)
		{
			if (device.Model == HyperXDeviceModel.MousePulsefireRaid || device.Model == HyperXDeviceModel.MousePulsefireCore || device.Model == HyperXDeviceModel.MousePulsefireDart || device.Model == HyperXDeviceModel.MousePulsefireHaste || device.Model == HyperXDeviceModel.MousePulsefireHasteWireless || device.Model == HyperXDeviceModel.MousePad_FURYUltra || device.Model == HyperXDeviceModel.Microphone_QuadcastS || device.Model == HyperXDeviceModel.Microphone_Quadcast2S || device.Model == HyperXDeviceModel.Microphone_Duocast || device.Model == HyperXDeviceModel.Microphone_Solocast || device.Model == HyperXDeviceModel.UniversalMousePulsefireHaste2 || device.Model == HyperXDeviceModel.UniversalMousePulsefireHaste2Wireless || device.Model == HyperXDeviceModel.UniversalMousePulsefireHaste2MiniWireless || device.Model == HyperXDeviceModel.UniversalMousePulsefireHaste2Core || device.Model == HyperXDeviceModel.UniversalMousePulsefireHaste2CoreWireless || device.Model == HyperXDeviceModel.UniversalMousePulsefireHaste2SWireless || device.Model == HyperXDeviceModel.UniversalMousePulsefireFuseWireless || device.Model == HyperXDeviceModel.UniversalMousePulsefireSaga || device.Model == HyperXDeviceModel.UniversalMousePulsefireHaste2Pro || device.Model == HyperXDeviceModel.UniversalMousePulsefireSagaPro || device.Model == HyperXDeviceModel.Headset_CloudFlight2)
			{
				return 1;
			}
			if (device.Model == HyperXDeviceModel.UniversalKeyboardAlloyRise || device.Model == HyperXDeviceModel.UniversalKeyboardAlloyRise75 || device.Model == HyperXDeviceModel.UniversalKeyboardAlloyRise75Wireless)
			{
				return 10;
			}
			return 3;
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06001E38 RID: 7736 RVA: 0x000511F2 File Offset: 0x0004F3F2
		public static Dictionary<ushort, HyperXDeviceModel> SupportTIOChildDevices
		{
			get
			{
				return HyperXDeviceUtils._dicSupportedTIOChildDevices;
			}
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x000511F9 File Offset: 0x0004F3F9
		public static HyperXDeviceModel GetTIOChildHyperXDeviceType(ushort vid, ushort pid)
		{
			if (vid != 1008)
			{
				return HyperXDeviceModel.Unknown;
			}
			if (!HyperXDeviceUtils._dicSupportedTIOChildDevices.ContainsKey(pid))
			{
				return HyperXDeviceModel.Unknown;
			}
			return HyperXDeviceUtils._dicSupportedTIOChildDevices[pid];
		}

		// Token: 0x04001C5C RID: 7260
		public const string DEVICE_KB_ALLOY_ELITE = "HID#VID_0951&PID_16BE&MI_02&Col05";

		// Token: 0x04001C5D RID: 7261
		public const string DEVICE_KBN_ALLOY_ELITE = "HID#VID_0951&PID_16BE&MI_02&Col07";

		// Token: 0x04001C5E RID: 7262
		public const string DEVICE_KB_ALLOY_ELITE_DFU = "HID#VID_0951&PID_16BE&MI_01&Col05";

		// Token: 0x04001C5F RID: 7263
		public const string DEVICE_KB_ALLOY_FPS = "HID#VID_0951&PID_16DC&MI_02&Col05";

		// Token: 0x04001C60 RID: 7264
		public const string DEVICE_KBN_ALLOY_FPS = "HID#VID_0951&PID_16DC&MI_02&Col07";

		// Token: 0x04001C61 RID: 7265
		public const string DEVICE_KB_ALLOY_ORIGINS = "HID#VID_0951&PID_16E5&MI_03";

		// Token: 0x04001C62 RID: 7266
		public const string DEVICE_KBN_ALLOY_ORIGINS = "HID#VID_0951&PID_16E5&MI_01&Col05";

		// Token: 0x04001C63 RID: 7267
		public const string DEVICE_KB_ALLOY_ORIGINS_DFU = "HID#VID_0951&PID_16F5";

		// Token: 0x04001C64 RID: 7268
		public const string DEVICE_KB_ALLOY_ORIGINS_HP = "HID#VID_03F0&PID_0591&MI_03";

		// Token: 0x04001C65 RID: 7269
		public const string DEVICE_KBN_ALLOY_ORIGINS_HP = "HID#VID_03F0&PID_0591&MI_01&Col05";

		// Token: 0x04001C66 RID: 7270
		public const string DEVICE_KB_ALLOY_ORIGINS_DFU_HP = "HID#VID_03F0&PID_0691";

		// Token: 0x04001C67 RID: 7271
		public const string DEVICE_KB_ALLOY_ORIGINS_CORE = "HID#VID_0951&PID_16E6&MI_02";

		// Token: 0x04001C68 RID: 7272
		public const string DEVICE_KB_ALLOY_ORIGINS_CORE_DFU = "HID#VID_0951&PID_16F6";

		// Token: 0x04001C69 RID: 7273
		public const string DEVICE_KB_ALLOY_ORIGINS_CORE_HP = "HID#VID_03F0&PID_098F&MI_02";

		// Token: 0x04001C6A RID: 7274
		public const string DEVICE_KB_ALLOY_ORIGINS_CORE_DFU_HP = "HID#VID_03F0&PID_0A8F";

		// Token: 0x04001C6B RID: 7275
		public const string DEVICE_KB_ALLOY_ELITE_II = "HID#VID_0951&PID_1711&MI_03";

		// Token: 0x04001C6C RID: 7276
		public const string DEVICE_KBN_ALLOY_ELITE_II = "HID#VID_0951&PID_1711&MI_01&Col05";

		// Token: 0x04001C6D RID: 7277
		public const string DEVICE_KB_ALLOY_ELITE_II_DFU = "HID#VID_0951&PID_1712";

		// Token: 0x04001C6E RID: 7278
		public const string DEVICE_KB_ALLOY_ELITE_II_HP = "HID#VID_03F0&PID_058F&MI_03";

		// Token: 0x04001C6F RID: 7279
		public const string DEVICE_KBN_ALLOY_ELITE_II_HP = "HID#VID_03F0&PID_058F&MI_01&Col05";

		// Token: 0x04001C70 RID: 7280
		public const string DEVICE_KB_ALLOY_ELITE_II_DFU_HP = "HID#VID_03F0&PID_068F";

		// Token: 0x04001C71 RID: 7281
		public const string DEVICE_KB_ALLOY_ORIGINS_60 = "HID#VID_0951&PID_1734&MI_03";

		// Token: 0x04001C72 RID: 7282
		public const string DEVICE_KBN_ALLOY_ORIGINS_60 = "HID#VID_0951&PID_1734&MI_01&Col05";

		// Token: 0x04001C73 RID: 7283
		public const string DEVICE_KB_ALLOY_ORIGINS_60_DFU = "HID#VID_0951&PID_1735";

		// Token: 0x04001C74 RID: 7284
		public const string DEVICE_KB_ALLOY_ORIGINS_60_HP = "HID#VID_03F0&PID_0C8E&MI_03";

		// Token: 0x04001C75 RID: 7285
		public const string DEVICE_KBN_ALLOY_ORIGINS_60_HP = "HID#VID_03F0&PID_0C8E&MI_01&Col05";

		// Token: 0x04001C76 RID: 7286
		public const string DEVICE_KB_ALLOY_ORIGINS_60_DFU_HP = "HID#VID_03F0&PID_0D8E";

		// Token: 0x04001C77 RID: 7287
		public const string DEVICE_KB_ALLOY_ORIGINS_65 = "HID#VID_03F0&PID_038F&MI_03";

		// Token: 0x04001C78 RID: 7288
		public const string DEVICE_KBN_ALLOY_ORIGINS_65 = "HID#VID_03F0&PID_038F&MI_01&Col05";

		// Token: 0x04001C79 RID: 7289
		public const string DEVICE_KB_ALLOY_ORIGINS_65_DFU = "HID#VID_03F0&PID_048F";

		// Token: 0x04001C7A RID: 7290
		public const string DEVICE_KB_ALLOY_MKW100 = "HID#VID_0951&PID_1756&MI_00";

		// Token: 0x04001C7B RID: 7291
		public const string DEVICE_KB_ALLOY_MKW100_HP = "HID#VID_03F0&PID_0B8F&MI_00";

		// Token: 0x04001C7C RID: 7292
		public const string DEVICE_KB_ALLOY_MKW100_HP_DFU = "HID#VID_03F0&PID_0C8F&MI_00";

		// Token: 0x04001C7D RID: 7293
		public const string DEVICE_MS_PULSEFIRE_SURGE = "HID#VID_0951&PID_16D3&MI_01&Col05";

		// Token: 0x04001C7E RID: 7294
		public const string DEVICE_MSN_PULSEFIRE_SURGE = "HID#VID_0951&PID_16D3&MI_02";

		// Token: 0x04001C7F RID: 7295
		public const string DEVICE_MS_PULSEFIRE_SURGE_HP = "HID#VID_03F0&PID_0490&MI_01&Col05";

		// Token: 0x04001C80 RID: 7296
		public const string DEVICE_MSN_PULSEFIRE_SURGE_HP = "HID#VID_03F0&PID_0490&MI_02";

		// Token: 0x04001C81 RID: 7297
		public const string DEVICE_MS_PULSEFIRE_RAID = "HID#VID_0951&PID_16E4&MI_01&Col05";

		// Token: 0x04001C82 RID: 7298
		public const string DEVICE_MSN_PULSEFIRE_RAID = "HID#VID_0951&PID_16E4&MI_02";

		// Token: 0x04001C83 RID: 7299
		public const string DEVICE_MS_PULSEFIRE_RAID_DFU = "HID#VID_0951&PID_16F9&MI_01&Col05";

		// Token: 0x04001C84 RID: 7300
		public const string DEVICE_MS_PULSEFIRE_RAID_HP = "HID#VID_03F0&PID_0290&MI_01&Col05";

		// Token: 0x04001C85 RID: 7301
		public const string DEVICE_MSN_PULSEFIRE_RAID_HP = "HID#VID_03F0&PID_0290&MI_02";

		// Token: 0x04001C86 RID: 7302
		public const string DEVICE_MS_PULSEFIRE_RAID_DFU_HP = "HID#VID_03F0&PID_0390&MI_01&Col05";

		// Token: 0x04001C87 RID: 7303
		public const string DEVICE_MS_PULSEFIRE_DART_MOUSE = "HID#VID_0951&PID_16E2&MI_01";

		// Token: 0x04001C88 RID: 7304
		public const string DEVICE_MS_PULSEFIRE_DART_MOUSE_DFU = "HID#VID_0951&PID_16F8&MI_01";

		// Token: 0x04001C89 RID: 7305
		public const string DEVICE_MS_PULSEFIRE_DART_DONGLE = "HID#VID_0951&PID_16E1&MI_02";

		// Token: 0x04001C8A RID: 7306
		public const string DEVICE_MS_PULSEFIRE_DART_DONGLE_DFU2 = "HID#VID_0951&PID_16F7&MI_01";

		// Token: 0x04001C8B RID: 7307
		public const string DEVICE_MS_PULSEFIRE_DART_MOUSE_HP = "HID#VID_03F0&PID_088E&MI_01";

		// Token: 0x04001C8C RID: 7308
		public const string DEVICE_MS_PULSEFIRE_DART_MOUSE_DFU_HP = "HID#VID_03F0&PID_098E&MI_01";

		// Token: 0x04001C8D RID: 7309
		public const string DEVICE_MS_PULSEFIRE_DART_DONGLE_HP = "HID#VID_03F0&PID_068E&MI_02";

		// Token: 0x04001C8E RID: 7310
		public const string DEVICE_MS_PULSEFIRE_DART_DONGLE_DFU_HP = "HID#VID_03F0&PID_078E&MI_01";

		// Token: 0x04001C8F RID: 7311
		public const string DEVICE_MS_PULSEFIRE_FPS_PRO = "HID#VID_0951&PID_16D7&MI_01&Col05";

		// Token: 0x04001C90 RID: 7312
		public const string DEVICE_MSN_PULSEFIRE_FPS_PRO = "HID#VID_0951&PID_16D7&MI_02";

		// Token: 0x04001C91 RID: 7313
		public const string DEVICE_MS_PULSEFIRE_FPS_PRO_HP = "HID#VID_03F0&PID_0E8F&MI_01&Col05";

		// Token: 0x04001C92 RID: 7314
		public const string DEVICE_MSN_PULSEFIRE_FPS_PRO_HP = "HID#VID_03F0&PID_0E8F&MI_02";

		// Token: 0x04001C93 RID: 7315
		public const string DEVICE_MS_PULSEFIRE_FPS_PRO_CD = "HID#VID_0951&PID_1729&MI_02";

		// Token: 0x04001C94 RID: 7316
		public const string DEVICE_MS_PULSEFIRE_FPS_PRO_CD_DFU = "HID#VID_0951&PID_172A";

		// Token: 0x04001C95 RID: 7317
		public const string DEVICE_MS_PULSEFIRE_FPS_PRO_CD_HP = "HID#VID_03F0&PID_0693&MI_02";

		// Token: 0x04001C96 RID: 7318
		public const string DEVICE_MS_PULSEFIRE_FPS_PRO_CD_DFU_HP = "HID#VID_03F0&PID_0793";

		// Token: 0x04001C97 RID: 7319
		public const string DEVICE_MS_PULSEFIRE_CORE = "HID#VID_0951&PID_16DE&MI_01&Col05";

		// Token: 0x04001C98 RID: 7320
		public const string DEVICE_MSN_PULSEFIRE_CORE = "HID#VID_0951&PID_16DE&MI_02";

		// Token: 0x04001C99 RID: 7321
		public const string DEVICE_MS_PULSEFIRE_CORE_HP = "HID#VID_03F0&PID_0D8F&MI_01&Col05";

		// Token: 0x04001C9A RID: 7322
		public const string DEVICE_MSN_PULSEFIRE_CORE_HP = "HID#VID_03F0&PID_0D8F&MI_02";

		// Token: 0x04001C9B RID: 7323
		public const string DEVICE_MS_PULSEFIRE_HASTE = "HID#VID_0951&PID_1727&MI_03";

		// Token: 0x04001C9C RID: 7324
		public const string DEVICE_MS_PULSEFIRE_HASTE_DFU = "HID#VID_0951&PID_1728";

		// Token: 0x04001C9D RID: 7325
		public const string DEVICE_MSN_PULSEFIRE_HASTE = "HID#VID_0951&PID_1727&MI_01&Col04";

		// Token: 0x04001C9E RID: 7326
		public const string DEVICE_MS_PULSEFIRE_HASTE_HP = "HID#VID_03F0&PID_0F8F&MI_03";

		// Token: 0x04001C9F RID: 7327
		public const string DEVICE_MS_PULSEFIRE_HASTE_DFU_HP = "HID#VID_03F0&PID_0190";

		// Token: 0x04001CA0 RID: 7328
		public const string DEVICE_MSN_PULSEFIRE_HASTE_HP = "HID#VID_03F0&PID_0F8F&MI_01&Col04";

		// Token: 0x04001CA1 RID: 7329
		public const string DEVICE_MS_PULSEFIRE_HASTE_WIRELESS_MOUSE = "HID#VID_03F0&PID_048E&MI_02";

		// Token: 0x04001CA2 RID: 7330
		public const string DEVICE_MS_PULSEFIRE_HASTE_WIRELESS_MOUSE_DFU = "HID#VID_03F0&PID_058E&MI_01";

		// Token: 0x04001CA3 RID: 7331
		public const string DEVICE_MS_PULSEFIRE_HASTE_WIRELESS_DONGLE = "HID#VID_03F0&PID_028E&MI_02";

		// Token: 0x04001CA4 RID: 7332
		public const string DEVICE_MS_PULSEFIRE_HASTE_WIRELESS_DONGLE_DFU = "HID#VID_03F0&PID_038E&MI_01";

		// Token: 0x04001CA5 RID: 7333
		public const string DEVICE_HS_CLOUD_FLIGHT_S = "HID#VID_0951&PID_16EA&MI_05&Col03";

		// Token: 0x04001CA6 RID: 7334
		public const string DEVICE_HS_CLOUD_ALPHA_S = "HID#VID_0951&PID_16ED&MI_05&Col03";

		// Token: 0x04001CA7 RID: 7335
		public const string DEVICE_HS_CLOUD_ALPHA_S_DFU = "HID#VID_0951&PID_16ED&MI_05&Col01";

		// Token: 0x04001CA8 RID: 7336
		public const string DEVICE_HS_CLOUD_ALPHA_S_2 = "HID#VID_03F0&PID_0C91&MI_05&Col03";

		// Token: 0x04001CA9 RID: 7337
		public const string DEVICE_HS_CLOUD_II_WIRELESS = "HID#VID_0951&PID_1719&Col02";

		// Token: 0x04001CAA RID: 7338
		public const string DEVICE_HS_CLOUD_II_WIRELESS_DONGLE = "HID#VID_0951&PID_1718&MI_03&Col03";

		// Token: 0x04001CAB RID: 7339
		public const string DEVICE_HS_CLOUD_II_WIRELESS_DONGLE2 = "HID#VID_03F0&PID_0C8A&MI_03&Col03";

		// Token: 0x04001CAC RID: 7340
		public const string DEVICE_HS_CLOUD_ALPHA_WIRELESS_DONGLE = "HID#VID_0951&PID_1743&MI_03&Col01";

		// Token: 0x04001CAD RID: 7341
		public const string DEVICE_HS_CLOUD_ALPHA_WIRELESS_DONGLE2 = "HID#VID_0951&PID_1765&MI_03&Col01";

		// Token: 0x04001CAE RID: 7342
		public const string DEVICE_HS_CLOUD_ALPHA_WIRELESS_DONGLE3 = "HID#VID_03F0&PID_098D&MI_03&Col01";

		// Token: 0x04001CAF RID: 7343
		public const string DEVICE_HS_CLOUD_II_WIRELESS_DTS_HEADSET_ONSEMI = "HID#VID_03F0&PID_058B&Col02";

		// Token: 0x04001CB0 RID: 7344
		public const string DEVICE_HS_CLOUD_II_WIRELESS_DTS_DONGLE_ONSEMI = "HID#VID_03F0&PID_018B&MI_03&Col03";

		// Token: 0x04001CB1 RID: 7345
		public const string DEVICE_HS_CLOUD_II_WIRELESS_DTS_HEADSET_TI = "HID#VID_03F0&PID_038B&Col02";

		// Token: 0x04001CB2 RID: 7346
		public const string DEVICE_HS_CLOUD_II_WIRELESS_DTS_DONGLE_TI = "HID#VID_03F0&PID_0696&MI_03&Col03";

		// Token: 0x04001CB3 RID: 7347
		public const string DEVICE_HS_CLOUD_III = "HID#VID_03F0&PID_089D&MI_03&Col02";

		// Token: 0x04001CB4 RID: 7348
		public const string DEVICE_HS_STINGER_CORE_WIRED = "HID#VID_0951&PID_1709&MI_03&Col02";

		// Token: 0x04001CB5 RID: 7349
		public const string DEVICE_HS_STINGER_CORE_WIRELESS = "HID#VID_0951&PID_170B&MI_03&Col03";

		// Token: 0x04001CB6 RID: 7350
		public const string DEVICE_HS_STINGER_S = "HID#VID_0951&PID_1715&MI_03&Col02";

		// Token: 0x04001CB7 RID: 7351
		public const string DEVICE_HS_CLOUD_MIX_BUDS = "HID#VID_03F0&PID_078A&mi_03&col04";

		// Token: 0x04001CB8 RID: 7352
		public const string DEVICE_HS_CLOUD_MIX_BUDS_NOTIFICATION = "HID#VID_03F0&PID_078A&mi_03&col05";

		// Token: 0x04001CB9 RID: 7353
		public const string DEVICE_HS_CLOUD_STINGER2_WIRELESS = "HID#VID_03F0&PID_0D93&mi_03&col03";

		// Token: 0x04001CBA RID: 7354
		public const string DEVICE_HS_CLOUD_2_CORE_WIRELESS_DONGLE = "HID#VID_03F0&PID_0995&MI_03&Col02";

		// Token: 0x04001CBB RID: 7355
		public const string DEVICE_HS_CLOUD_2_CORE_WIRELESS_TREAD_DONGLE = "HID#VID_03F0&PID_069F&MI_03&Col02";

		// Token: 0x04001CBC RID: 7356
		public const string DEVICE_HS_CLOUD_FLIGHT_WIRELESS = "HID#VID_0951&PID_16C4&mi_03&col03";

		// Token: 0x04001CBD RID: 7357
		public const string DEVICE_HS_CLOUD_FLIGHT_WIRELESS_CD = "HID#VID_0951&PID_1723&mi_03&col03";

		// Token: 0x04001CBE RID: 7358
		public const string DEVICE_HS_CLOUD_FLIGHT_WIRELESS_CD_HP = "HID#VID_03F0&PID_0E90&mi_03&col03";

		// Token: 0x04001CBF RID: 7359
		public const string DEVICE_HS_RALPHIE_ULL_1 = "HID#VID_03F0&PID_099C&Col03";

		// Token: 0x04001CC0 RID: 7360
		public const string DEVICE_HS_RALPHIE_ULL_2 = "HID#VID_03F0&PID_0EBC&Col03";

		// Token: 0x04001CC1 RID: 7361
		public const string DEVICE_HS_RALPHIE_ULL_TREAD = "HID#VID_03F0&PID_0AA0&MI_03&Col03";

		// Token: 0x04001CC2 RID: 7362
		public const string DEVICE_HS_RALPHIE_ULL_TREAD_EML = "HID#VID_03F0&PID_089F&MI_03&Col03";

		// Token: 0x04001CC3 RID: 7363
		public const string DEVICE_HS_RALPHIE_ULL_TREAD_EPW = "HID#VID_03F0&PID_0A9F&MI_03&Col03";

		// Token: 0x04001CC4 RID: 7364
		public const string DEVICE_HS_RALPHIE_ULL_ATLAS = "HID#VID_03F0&PID_08B7&MI_03&Col03";

		// Token: 0x04001CC5 RID: 7365
		public const string DEVICE_HS_CLOUD_3_WIRELESS_DONGLE = "HID#VID_03F0&PID_0C9D&MI_03&Col01";

		// Token: 0x04001CC6 RID: 7366
		public const string DEVICE_HS_CLOUD_3_WIRELESS_ATLAS_DONGLE = "HID#VID_03F0&PID_05B7&MI_03&Col01";

		// Token: 0x04001CC7 RID: 7367
		public const string DEVICE_HS_CLOUD_3_WIRELESS_HEADSET = "HID#VID_03F0&PID_0E9D";

		// Token: 0x04001CC8 RID: 7368
		public const string DEVICE_HS_CLOUD_3_WIRELESS_ATLAS_HEADSET = "HID#VID_03F0&PID_06B7";

		// Token: 0x04001CC9 RID: 7369
		public const string DEVICE_HS_CLOUD_MIX_BUDS_2_DONGLE = "HID#VID_03F0&PID_039E&MI_03&Col05";

		// Token: 0x04001CCA RID: 7370
		public const string DEVICE_HS_CLOUD_MIX_2_DONGLE = "HID#VID_03F0&PID_0FAE&MI_03&Col06";

		// Token: 0x04001CCB RID: 7371
		public const string DEVICE_HS_CLOUD_3_S_WIRELESS_DONGLE = "HID#VID_03F0&PID_06BE&MI_03&Col05";

		// Token: 0x04001CCC RID: 7372
		public const string DEVICE_MIC_QUADCAST_S = "HID#VID_0951&PID_171F&MI_00";

		// Token: 0x04001CCD RID: 7373
		public const string DEVICE_MICN_QUADCAST_S = "HID#VID_0951&PID_171F&MI_01&Col02";

		// Token: 0x04001CCE RID: 7374
		public const string DEVICE_MIC_HP_QUADCAST_S_WHITE = "HID#VID_03F0&PID_048C&MI_00";

		// Token: 0x04001CCF RID: 7375
		public const string DEVICE_MICN_HP_QUADCAST_S_WHITE = "HID#VID_03F0&PID_048C&MI_01&Col02";

		// Token: 0x04001CD0 RID: 7376
		public const string DEVICE_MIC_HP_QUADCAST_S_2S_WHITE = "HID#VID_03F0&PID_068C&MI_00";

		// Token: 0x04001CD1 RID: 7377
		public const string DEVICE_MICN_HP_QUADCAST_S_2S_WHITE = "HID#VID_03F0&PID_068C&MI_01&Col02";

		// Token: 0x04001CD2 RID: 7378
		public const string DEVICE_MIC_HP_QUADCAST_S_BLACK = "HID#VID_03F0&PID_0F8B&MI_00";

		// Token: 0x04001CD3 RID: 7379
		public const string DEVICE_MICN_HP_QUADCAST_S_BLACK = "HID#VID_03F0&PID_0F8B&MI_01&Col02";

		// Token: 0x04001CD4 RID: 7380
		public const string DEVICE_MIC_HP_QUADCAST_S_2S_BLACK = "HID#VID_03F0&PID_028C&MI_00";

		// Token: 0x04001CD5 RID: 7381
		public const string DEVICE_MICN_HP_QUADCAST_S_2S_BLACK = "HID#VID_03F0&PID_028C&MI_01&Col02";

		// Token: 0x04001CD6 RID: 7382
		public const string DEVICE_CMEDIA_QUADCAST_S = "VID_0951&PID_171D&mi_03&col01";

		// Token: 0x04001CD7 RID: 7383
		public const string DEVICE_CMEDIA_HP_QUADCAST_S = "VID_03F0&PID_0D8B&mi_03&col01";

		// Token: 0x04001CD8 RID: 7384
		public const string DEVICE_CMEDIA_HP_QUADCAST_S_2S = "VID_03F0&PID_0294&mi_03&col01";

		// Token: 0x04001CD9 RID: 7385
		public const string DEVICE_MIC_QUADCAST_S_DFU = "HID#VID_0951&PID_1720";

		// Token: 0x04001CDA RID: 7386
		public const string DEVICE_MIC_QUADCAST_S_2S_WB_DFU = "HID#VID_0951&PID_1774";

		// Token: 0x04001CDB RID: 7387
		public const string DEVICE_MIC_HP_QUADCAST_2_CONTROLLER = "HID#VID_03F0&PID_09AF&MI_00";

		// Token: 0x04001CDC RID: 7388
		public const string DEVICE_MIC_HP_QUADCAST_2_CONTROLLER_DFU = "HID#VID_03F0&PID_0AAF";

		// Token: 0x04001CDD RID: 7389
		public const string DEVICE_MIC_HP_QUADCAST_2_CODEC_UNPLUGGED = "HID#VID_03F0&PID_07B4&MI_02&Col04";

		// Token: 0x04001CDE RID: 7390
		public const string DEVICE_MIC_HP_QUADCAST_2_CODEC_PLUGGED = "HID#VID_03F0&PID_07AF&MI_03&Col04";

		// Token: 0x04001CDF RID: 7391
		public const string DEVICE_MIC_HP_QUADCAST_2S_CONTROLLER = "HID#VID_03F0&PID_02B5&MI_01&Col02";

		// Token: 0x04001CE0 RID: 7392
		public const string DEVICE_MIC_HP_QUADCAST_2S_CONTROLLER_DFU = "HID#VID_03F0&PID_03B5";

		// Token: 0x04001CE1 RID: 7393
		public const string DEVICE_MIC_HP_QUADCAST_2S_CODEC = "HID#VID_03F0&PID_0D84&MI_03&Col06";

		// Token: 0x04001CE2 RID: 7394
		public const string DEVICE_MIC_HP_QUADCAST_2S_CODEC_DFU = "HID#VID_03F0&PID_0EB4";

		// Token: 0x04001CE3 RID: 7395
		public const string DEVICE_MIC_DUOCAST_AUDIO = "HID#VID_03F0&PID_0A8C&MI_03";

		// Token: 0x04001CE4 RID: 7396
		public const string DEVICE_MIC_DUOCAST_CONTROLLER = "HID#VID_03F0&PID_098C&MI_00";

		// Token: 0x04001CE5 RID: 7397
		public const string DEVICE_MIC_DUOCAST_CONTROLLER_DFU_HP = "HID#VID_03F0&PID_088C";

		// Token: 0x04001CE6 RID: 7398
		public const string DEVICE_MIC_DUOCAST_CONTROLLER_DFU_KINGSTON = "HID#VID_0951&PID_175F";

		// Token: 0x04001CE7 RID: 7399
		public const string DEVICE_MIC_KINGSTON_SOLOCAST_BLACK = "HID#VID_0951&PID_170F&MI_02&Col01";

		// Token: 0x04001CE8 RID: 7400
		public const string DEVICE_MIC_HP_SOLOCAST_BLACK = "HID#VID_03F0&PID_078B&MI_02&Col01";

		// Token: 0x04001CE9 RID: 7401
		public const string DEVICE_MIC_HP_SOLOCAST_WHITE = "HID#VID_03F0&PID_0592&MI_02&Col01";

		// Token: 0x04001CEA RID: 7402
		public const string DEVICE_MIC_HP_SOLOCAST_BLACK_2ND_SOURCE = "HID#VID_03F0&PID_098B&MI_02";

		// Token: 0x04001CEB RID: 7403
		public const string DEVICE_MIC_HP_SOLOCAST_WHITE_3RD_SOURCE = "HID#VID_03F0&PID_0992&MI_02&Col02";

		// Token: 0x04001CEC RID: 7404
		public const string DEVICE_MIC_HP_SOLOCAST_BLACK_3RD_SOURCE = "HID#VID_03F0&PID_0B8B&MI_02&Col02";

		// Token: 0x04001CED RID: 7405
		public const string DEVICE_MP_FURY_ULTRA = "HID#VID_0951&PID_1705&MI_00";

		// Token: 0x04001CEE RID: 7406
		public const string DEVICE_MP_FURY_ULTRA_DFU = "HID#VID_0951&PID_1706";

		// Token: 0x04001CEF RID: 7407
		public const string DEVICE_MPN_FURY_ULTRA = "HID#VID_0951&PID_1705&MI_01&Col02";

		// Token: 0x04001CF0 RID: 7408
		public const string DEVICE_MP_FURY_ULTRA_HP = "HID#VID_03F0&PID_0493&MI_00";

		// Token: 0x04001CF1 RID: 7409
		public const string DEVICE_MP_FURY_ULTRA_DFU_HP = "HID#VID_03F0&PID_0593";

		// Token: 0x04001CF2 RID: 7410
		public const string DEVICE_MPN_FURY_ULTRA_HP = "HID#VID_03F0&PID_0493&MI_01&Col02";

		// Token: 0x04001CF3 RID: 7411
		public const string DEVICE_MP_PULSEFIRE_MAT_RGB = "HID#VID_0951&PID_1741&MI_01";

		// Token: 0x04001CF4 RID: 7412
		public const string DEVICE_MP_PULSEFIRE_MAT_RGB_DFU1 = "HID#VID_0951&PID_1742";

		// Token: 0x04001CF5 RID: 7413
		public const string DEVICE_MP_PULSEFIRE_MAT_RGB_HP = "HID#VID_03F0&PID_0F8D&MI_01";

		// Token: 0x04001CF6 RID: 7414
		public const string DEVICE_MP_PULSEFIRE_MAT_DFU_HP = "HID#VID_03F0&PID_018E";

		// Token: 0x04001CF7 RID: 7415
		public const string DEVICE_MS_PULSEFIRE_HASTE2 = "HID#VID_03F0&PID_0B97&MI_02";

		// Token: 0x04001CF8 RID: 7416
		public const string DEVICE_MS_PULSEFIRE_HASTE2_DFU = "HID#VID_03F0&PID_0C97";

		// Token: 0x04001CF9 RID: 7417
		public const string DEVICE_MS_PULSEFIRE_HASTE2_WIRELESS_DONGLE = "HID#VID_03F0&PID_0F98&MI_02";

		// Token: 0x04001CFA RID: 7418
		public const string DEVICE_MS_PULSEFIRE_HASTE2_WIRELESS_MOUSE = "HID#VID_03F0&PID_0D97&MI_02";

		// Token: 0x04001CFB RID: 7419
		public const string DEVICE_MS_PULSEFIRE_HASTE2_WIRELESS_DONGLE_DFU = "HID#VID_03F0&PID_0199&MI_01";

		// Token: 0x04001CFC RID: 7420
		public const string DEVICE_MS_PULSEFIRE_HASTE2_WIRELESS_MOUSE_DFU = "HID#VID_03F0&PID_0E97&MI_01";

		// Token: 0x04001CFD RID: 7421
		public const string DEVICE_MS_PULSEFIRE_HASTE2_PRO_DONGLE = "HID#VID_03F0&PID_0CBD&MI_03";

		// Token: 0x04001CFE RID: 7422
		public const string DEVICE_MS_PULSEFIRE_HASTE2_PRO_MOUSE = "HID#VID_03F0&PID_0ABD&MI_02";

		// Token: 0x04001CFF RID: 7423
		public const string DEVICE_MS_PULSEFIRE_HASTE2_PRO_DONGLE_DFU = "HID#VID_03F0&PID_0DBD&MI_01";

		// Token: 0x04001D00 RID: 7424
		public const string DEVICE_MS_PULSEFIRE_HASTE2_PRO_MOUSE_DFU = "HID#VID_03F0&PID_0BBD";

		// Token: 0x04001D01 RID: 7425
		public const string DEVICE_MS_PULSEFIRE_HASTE2_MINI_WIRELESS_MOUSE = "HID#VID_03F0&PID_0BA0&MI_02";

		// Token: 0x04001D02 RID: 7426
		public const string DEVICE_MS_PULSEFIRE_HASTE2_MINI_WIRELESS_MOUSE_DFU = "HID#VID_03F0&PID_0CA0";

		// Token: 0x04001D03 RID: 7427
		public const string DEVICE_MS_PULSEFIRE_HASTE2_MINI_WIRELESS_DONGLE = "HID#VID_03F0&PID_0DA0&MI_02";

		// Token: 0x04001D04 RID: 7428
		public const string DEVICE_MS_PULSEFIRE_HASTE2_MINI_WIRELESS_DONGLE_DFU = "HID#VID_03F0&PID_0EA0";

		// Token: 0x04001D05 RID: 7429
		public const string DEVICE_MS_PULSEFIRE_HASTE2_CORE = "HID#VID_03F0&PID_04B5&MI_02";

		// Token: 0x04001D06 RID: 7430
		public const string DEVICE_MS_PULSEFIRE_HASTE2_CORE_DFU = "HID#VID_03F0&PID_05B5";

		// Token: 0x04001D07 RID: 7431
		public const string DEVICE_KB_ALLOY_RISE = "HID#VID_03F0&PID_0FA0&MI_02";

		// Token: 0x04001D08 RID: 7432
		public const string DEVICE_KB_ALLOY_RISE_DFU = "HID#VID_03F0&PID_01A1&MI_02";

		// Token: 0x04001D09 RID: 7433
		public const string DEVICE_KB_ALLOY_RISE_75 = "HID#VID_03F0&PID_02A1&MI_02";

		// Token: 0x04001D0A RID: 7434
		public const string DEVICE_KB_ALLOY_RISE_75_DFU = "HID#VID_03F0&PID_03A1&MI_02";

		// Token: 0x04001D0B RID: 7435
		public const string DEVICE_KB_ALLOY_RISE_75_WIRELESS_DONGLE = "HID#VID_03F0&PID_01B9&MI_02";

		// Token: 0x04001D0C RID: 7436
		public const string DEVICE_KB_ALLOY_RISE_75_WIRELESS_DONGLE_DFU = "HID#VID_03F0&PID_02B9";

		// Token: 0x04001D0D RID: 7437
		public const string DEVICE_KB_ALLOY_RISE_75_WIRELESS_KEYBOARD = "HID#VID_03F0&PID_0EB8&MI_02";

		// Token: 0x04001D0E RID: 7438
		public const string DEVICE_KB_ALLOY_RISE_75_WIRELESS_KEYBOARD_DFU = "HID#VID_03F0&PID_0FB8";

		// Token: 0x04001D0F RID: 7439
		public const string DEVICE_MS_PULSEFIRE_HASTE2_CORE_WIRELESS_DONGLE = "HID#VID_03F0&PID_0AB5&MI_02";

		// Token: 0x04001D10 RID: 7440
		public const string DEVICE_MS_PULSEFIRE_HASTE2_CORE_WIRELESS_DONGLE_DFU = "HID#VID_03F0&PID_0BB5&MI_01";

		// Token: 0x04001D11 RID: 7441
		public const string DEVICE_MS_PULSEFIRE_HASTE_2S_WIRELESS_MOUSE = "HID#VID_03F0&PID_0AB8&MI_02";

		// Token: 0x04001D12 RID: 7442
		public const string DEVICE_MS_PULSEFIRE_HASTE_2S_WIRELESS_MOUSE_DFU = "HID#VID_03F0&PID_0CB8";

		// Token: 0x04001D13 RID: 7443
		public const string DEVICE_MS_PULSEFIRE_HASTE_2S_WIRELESS_DONGLE = "HID#VID_03F0&PID_0BB8&MI_02";

		// Token: 0x04001D14 RID: 7444
		public const string DEVICE_MS_PULSEFIRE_HASTE_2S_WIRELESS_DONGLE_DFU = "HID#VID_03F0&PID_0DB8";

		// Token: 0x04001D15 RID: 7445
		public const string DEVICE_MS_PULSEFIRE_FUSE_WIRELESS = "HID#VID_03F0&PID_0FBD&MI_02";

		// Token: 0x04001D16 RID: 7446
		public const string DEVICE_MS_PULSEFIRE_FUSE_WIRELESS_DFU = "HID#VID_03F0&PID_0FBD&MI_01&COL02";

		// Token: 0x04001D17 RID: 7447
		public const string DEVICE_MS_PULSEFIRE_SAGA = "HID#VID_03F0&PID_0EBE&MI_02";

		// Token: 0x04001D18 RID: 7448
		public const string DEVICE_MS_PULSEFIRE_SAGA_DFU = "HID#VID_03F0&PID_0FBE&MI_01";

		// Token: 0x04001D19 RID: 7449
		public const string DEVICE_MS_PULSEFIRE_SAGA_PRO_MOUSE = "HID#VID_03F0&PID_04BF&MI_02";

		// Token: 0x04001D1A RID: 7450
		public const string DEVICE_MS_PULSEFIRE_SAGA_PRO_MOUSE_DFU = "HID#VID_03F0&PID_05BF";

		// Token: 0x04001D1B RID: 7451
		public const string DEVICE_MS_PULSEFIRE_SAGA_PRO_DONGLE = "HID#VID_03F0&PID_06BF&MI_03";

		// Token: 0x04001D1C RID: 7452
		public const string DEVICE_MS_PULSEFIRE_SAGA_PRO_DONGLE_DFU = "HID#VID_03F0&PID_07BF&MI_01";

		// Token: 0x04001D1D RID: 7453
		public const string DEVICE_TIO_EMBLEMS_PAIRED = "HID#VID_03F0&PID_01BF&MI_05&Col02";

		// Token: 0x04001D1E RID: 7454
		public const string DEVICE_TIO_EMBLEMS_PAIRED_ONE_2_ONE = "HID#VID_03F0&PID_01BF&MI_04&Col02";

		// Token: 0x04001D1F RID: 7455
		public const string DEVICE_TIO_EMBLEMS_NOT_PAIRED = "HID#VID_03F0&PID_01C5&Col05";

		// Token: 0x04001D20 RID: 7456
		public const string DEVICE_HS_CLOUD_FLIGHT_2_WIRELESS_DONGLE = "HID#VID_03F0&PID_0AC1&MI_03&Col04";

		// Token: 0x04001D21 RID: 7457
		public const string DEVICE_HS_CLOUD_FLIGHT_2_WIRELESS_HEADSET = "HID#VID_03F0&PID_0BC1&MI_03&Col04";

		// Token: 0x04001D22 RID: 7458
		public static readonly List<string> DEVICE_LIST = new List<string>();

		// Token: 0x04001D23 RID: 7459
		private static Dictionary<ushort, HyperXDeviceModel> _dicSupportedTIOChildDevices = new Dictionary<ushort, HyperXDeviceModel>
		{
			{
				1470,
				HyperXDeviceModel.Headset_Cloud3SWireless
			},
			{
				2749,
				HyperXDeviceModel.UniversalMousePulsefireHaste2Pro
			},
			{
				1215,
				HyperXDeviceModel.UniversalMousePulsefireSagaPro
			},
			{
				3009,
				HyperXDeviceModel.Headset_CloudFlight2
			}
		};
	}
}

using System;
using System.Collections.Generic;
using NGenuity2.Common;
using NGenuity2.Effects;

namespace NGenuity2.Devices
{
	// Token: 0x02000662 RID: 1634
	public class HyperXDramDevice : HyperXDevice
	{
		// Token: 0x0600215C RID: 8540 RVA: 0x0003557B File Offset: 0x0003377B
		public override EffectImplBase CreateEffect(EffectItemBase item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x0003557B File Offset: 0x0003377B
		public override void SetLightings(IList<KeyMap> keys)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x0003557B File Offset: 0x0003377B
		public override void SetAllLEDOff()
		{
			throw new NotImplementedException();
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NGenuity2.Audio;
using NGenuity2.Common;
using NGenuity2.Common.Commands.Audio;
using NGenuity2.Common.Devices;
using NGenuity2.Common.RPCFunctions;
using NGenuity2.Effects;
using NGenuity2.Effects.Headset;
using NGenuity2.Model;
using NGenuity2.Model.Headset;
using Windows.Foundation;

namespace NGenuity2.Devices
{
	// Token: 0x0200064C RID: 1612
	public abstract class HyperXHeadsetDevice : HyperXDevice
	{
		// Token: 0x06001FD9 RID: 8153 RVA: 0x00057010 File Offset: 0x00055210
		public override void Serialize(BinaryWriter bw)
		{
			base.Serialize(bw);
			bw.Write((int)this.RemoteDeviceModel);
			bw.Write(this.RemoteVersion);
			bw.Write(this.CapVolume);
			bw.Write(this.CapMicrophone);
			bw.Write(this.CapSidetone);
			bw.Write(this.CapSurroundSound);
			bw.Write(this.CapGameChat);
			bw.Write(this.SurroundSound);
			bw.Write(this.VoicePrompt);
			bw.Write(this.EQEnabled);
			bw.Write(this.SpatialEnabled);
			bw.Write(this.GameChatBalance);
			bw.Write(this.LeftBattery);
			bw.Write(this.RightBattery);
			bw.Write(this.CaseBattery);
			bw.Write((int)this.ANCState);
			bw.Write(this.RequireResetSirk);
			bw.Write(this.RequireAirPairing);
			Utils.SerializePersistObjects<EqualizerPreset>(this.BuildInEqualizers, bw);
			bw.Write(this.SelectedBuildInEqualizerIndex);
			bw.Write(2116567778);
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x00057120 File Offset: 0x00055320
		public override void Deserialize(BinaryReader br, int version)
		{
			this.BuildInEqualizers.Clear();
			base.Deserialize(br, version);
			this.RemoteDeviceModel = (HyperXRemoteDeviceModel)br.ReadInt32();
			this.RemoteVersion = br.ReadInt32();
			this.CapVolume = br.ReadBoolean();
			this.CapMicrophone = br.ReadBoolean();
			this.CapSidetone = br.ReadBoolean();
			this.CapSurroundSound = br.ReadBoolean();
			this.CapGameChat = br.ReadBoolean();
			this.SurroundSound = br.ReadBoolean();
			this.VoicePrompt = br.ReadBoolean();
			this.EQEnabled = br.ReadBoolean();
			this.SpatialEnabled = br.ReadBoolean();
			this.GameChatBalance = br.ReadInt32();
			this.LeftBattery = br.ReadByte();
			this.RightBattery = br.ReadByte();
			this.CaseBattery = br.ReadByte();
			this.ANCState = (ANCState)br.ReadInt32();
			this.RequireResetSirk = br.ReadBoolean();
			this.RequireAirPairing = br.ReadBoolean();
			Utils.DeserializePersistObjects<EqualizerPreset>(this.BuildInEqualizers, br, version);
			this.SelectedBuildInEqualizerIndex = br.ReadInt32();
			br.ReadInt32();
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06001FDB RID: 8155 RVA: 0x00057238 File Offset: 0x00055438
		// (set) Token: 0x06001FDC RID: 8156 RVA: 0x00057240 File Offset: 0x00055440
		public HyperXRemoteDeviceModel RemoteDeviceModel { get; protected set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06001FDD RID: 8157 RVA: 0x00057249 File Offset: 0x00055449
		// (set) Token: 0x06001FDE RID: 8158 RVA: 0x00057251 File Offset: 0x00055451
		public int RemoteVersion { get; protected set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06001FDF RID: 8159 RVA: 0x0005725A File Offset: 0x0005545A
		// (set) Token: 0x06001FE0 RID: 8160 RVA: 0x00057262 File Offset: 0x00055462
		public bool CapVolume { get; protected set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06001FE1 RID: 8161 RVA: 0x0005726B File Offset: 0x0005546B
		// (set) Token: 0x06001FE2 RID: 8162 RVA: 0x00057273 File Offset: 0x00055473
		public bool CapMicrophone { get; set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06001FE3 RID: 8163 RVA: 0x0005727C File Offset: 0x0005547C
		// (set) Token: 0x06001FE4 RID: 8164 RVA: 0x00057284 File Offset: 0x00055484
		public bool CapSidetone { get; set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06001FE5 RID: 8165 RVA: 0x0005728D File Offset: 0x0005548D
		// (set) Token: 0x06001FE6 RID: 8166 RVA: 0x00057295 File Offset: 0x00055495
		public bool CapSurroundSound { get; set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06001FE7 RID: 8167 RVA: 0x0005729E File Offset: 0x0005549E
		// (set) Token: 0x06001FE8 RID: 8168 RVA: 0x000572A6 File Offset: 0x000554A6
		public bool CapGameChat { get; set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06001FE9 RID: 8169 RVA: 0x000572AF File Offset: 0x000554AF
		// (set) Token: 0x06001FEA RID: 8170 RVA: 0x000572B7 File Offset: 0x000554B7
		public bool SurroundSound { get; set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06001FEB RID: 8171 RVA: 0x000572C0 File Offset: 0x000554C0
		// (set) Token: 0x06001FEC RID: 8172 RVA: 0x000572C8 File Offset: 0x000554C8
		public bool VoicePrompt { get; set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06001FED RID: 8173 RVA: 0x000572D1 File Offset: 0x000554D1
		// (set) Token: 0x06001FEE RID: 8174 RVA: 0x000572D9 File Offset: 0x000554D9
		public bool EQEnabled { get; set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06001FEF RID: 8175 RVA: 0x000572E2 File Offset: 0x000554E2
		// (set) Token: 0x06001FF0 RID: 8176 RVA: 0x000572EA File Offset: 0x000554EA
		public bool SpatialEnabled { get; set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06001FF1 RID: 8177 RVA: 0x000572F3 File Offset: 0x000554F3
		// (set) Token: 0x06001FF2 RID: 8178 RVA: 0x000572FB File Offset: 0x000554FB
		public virtual int GameChatBalance { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06001FF3 RID: 8179 RVA: 0x00057304 File Offset: 0x00055504
		// (set) Token: 0x06001FF4 RID: 8180 RVA: 0x0005730C File Offset: 0x0005550C
		public byte LeftBattery { get; set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06001FF5 RID: 8181 RVA: 0x00057315 File Offset: 0x00055515
		// (set) Token: 0x06001FF6 RID: 8182 RVA: 0x0005731D File Offset: 0x0005551D
		public byte RightBattery { get; set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06001FF7 RID: 8183 RVA: 0x00057326 File Offset: 0x00055526
		// (set) Token: 0x06001FF8 RID: 8184 RVA: 0x0005732E File Offset: 0x0005552E
		public byte CaseBattery { get; set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06001FF9 RID: 8185 RVA: 0x00057337 File Offset: 0x00055537
		// (set) Token: 0x06001FFA RID: 8186 RVA: 0x0005733F File Offset: 0x0005553F
		public ANCState ANCState { get; set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06001FFB RID: 8187 RVA: 0x00057348 File Offset: 0x00055548
		// (set) Token: 0x06001FFC RID: 8188 RVA: 0x00057350 File Offset: 0x00055550
		public int SelectedBuildInEqualizerIndex { get; set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06001FFD RID: 8189 RVA: 0x00057359 File Offset: 0x00055559
		// (set) Token: 0x06001FFE RID: 8190 RVA: 0x00057361 File Offset: 0x00055561
		public List<EqualizerPreset> BuildInEqualizers { get; set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06001FFF RID: 8191 RVA: 0x0005736A File Offset: 0x0005556A
		// (set) Token: 0x06002000 RID: 8192 RVA: 0x00057372 File Offset: 0x00055572
		public bool RequireResetSirk { get; set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06002001 RID: 8193 RVA: 0x0005737B File Offset: 0x0005557B
		// (set) Token: 0x06002002 RID: 8194 RVA: 0x00057383 File Offset: 0x00055583
		public bool RequireAirPairing { get; set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06002003 RID: 8195 RVA: 0x0005738C File Offset: 0x0005558C
		// (set) Token: 0x06002004 RID: 8196 RVA: 0x00057394 File Offset: 0x00055594
		public string APODeviceId { get; set; }

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06002005 RID: 8197 RVA: 0x000573A0 File Offset: 0x000555A0
		// (remove) Token: 0x06002006 RID: 8198 RVA: 0x000573D8 File Offset: 0x000555D8
		public event TypedEventHandler<object, bool> DSPModeChanged;

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06002007 RID: 8199 RVA: 0x00057410 File Offset: 0x00055610
		// (remove) Token: 0x06002008 RID: 8200 RVA: 0x00057448 File Offset: 0x00055648
		public event TypedEventHandler<object, int> GameChatBalanceChanged;

		// Token: 0x06002009 RID: 8201 RVA: 0x00057480 File Offset: 0x00055680
		public virtual Task<bool> InitAPOAsync()
		{
			HyperXHeadsetDevice.<InitAPOAsync>d__94 <InitAPOAsync>d__;
			<InitAPOAsync>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<InitAPOAsync>d__.<>1__state = -1;
			<InitAPOAsync>d__.<>t__builder.Start<HyperXHeadsetDevice.<InitAPOAsync>d__94>(ref <InitAPOAsync>d__);
			return <InitAPOAsync>d__.<>t__builder.Task;
		}

		// Token: 0x0600200A RID: 8202 RVA: 0x000574BC File Offset: 0x000556BC
		public override void Start()
		{
			base.Start();
			HXAudio.Instance.RegisterHyperXDevice(this);
			if (this._audioDeviceChangedCommand == null)
			{
				this._audioDeviceChangedCommand = new AudioDeviceChangedCommand();
				this._audioDeviceChangedCommand.DeviceId = base.DeviceID;
				this._audioDeviceChangedCommand.Subscribe();
			}
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x00057509 File Offset: 0x00055709
		public override void Stop(bool waitUntilStopped)
		{
			HXAudio.Instance.UnregisterHyperXDevice(this);
			base.Stop(waitUntilStopped);
			AudioDeviceChangedCommand audioDeviceChangedCommand = this._audioDeviceChangedCommand;
			if (audioDeviceChangedCommand != null)
			{
				audioDeviceChangedCommand.Unsubscribe();
			}
			this._audioDeviceChangedCommand = null;
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x00057538 File Offset: 0x00055738
		protected virtual void EnableEQAutoOptimize()
		{
			Preset safePreset = base.GetSafePreset();
			if (safePreset == null)
			{
				return;
			}
			if (safePreset.Headset.EQAutoOptimize)
			{
				if (this.SurroundSound)
				{
					AudioFunctions.StartHeadsetGameMonitoring();
					return;
				}
				AudioFunctions.StopHeadsetGameMonitoring();
				this.ApplyGameEQ(KnownGames.Unknown);
			}
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x00057577 File Offset: 0x00055777
		protected void OnDSPModeChanged(bool on)
		{
			this.SurroundSound = on;
			if (this.DSPModeChanged != null)
			{
				this.DSPModeChanged.Invoke(this, on);
			}
			if (Settings.Instance.ShowNotifications)
			{
				bool notifySurroundSoundStatus = Settings.Instance.NotifySurroundSoundStatus;
			}
			this.EnableEQAutoOptimize();
		}

		// Token: 0x0600200E RID: 8206 RVA: 0x000575B4 File Offset: 0x000557B4
		private void NotifyGameBalance(int balance)
		{
			string text = HyperXDeviceUtils.GetDeviceTitle(this);
			text = Settings.Instance.GetDeviceName(base.DeviceID, text);
			int num = (int)((float)balance / 8f * 100f);
			string text2 = "Game ";
			List<int> list = new List<int>
			{
				-100,
				-87,
				-75,
				-62,
				-50,
				-37,
				-25,
				-12,
				0,
				12,
				25,
				37,
				50,
				62,
				75,
				87,
				100
			};
			for (int i = 0; i < list.Count; i++)
			{
				if (num == list[i])
				{
					text2 += "|";
				}
				else
				{
					text2 += "-";
				}
			}
			text2 += " Chat";
			NotificationCenter.PopMessage("com.ngenuity.gamechatbalance", text, text2, true);
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x000576D0 File Offset: 0x000558D0
		public void OnGameChatBalanceChanged(int balance)
		{
			this.GameChatBalance = balance;
			if (this.GameChatBalanceChanged != null)
			{
				this.GameChatBalanceChanged.Invoke(this, balance);
			}
			Preset safePreset = base.GetSafePreset();
			if (safePreset != null)
			{
				safePreset.Headset.GameChatBalance = balance;
				safePreset.Updated = true;
			}
			if (Settings.Instance.ShowNotifications && Settings.Instance.NotifyGameChatBalance)
			{
				this.NotifyGameBalance(balance);
			}
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x00057738 File Offset: 0x00055938
		public HyperXHeadsetDevice()
		{
			base.DeviceType = HyperXDeviceType.Headset;
			this.CapGameChat = true;
			this.CapMicrophone = true;
			this.CapSidetone = true;
			this.CapSurroundSound = true;
			this.CapGameChat = true;
			this.BuildInEqualizers = new List<EqualizerPreset>();
			base.CanLink = true;
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x00057788 File Offset: 0x00055988
		public override EffectImplBase CreateEffect(EffectItemBase item)
		{
			switch (((HeadsetEffect)item).Type)
			{
			case HeadsetEffectType.LoopedBreathing:
				return new HeadsetBreathingEffectImpl(item);
			case HeadsetEffectType.LoopedCycle:
				return new HeadsetCycleEffectImpl(item);
			case HeadsetEffectType.LoopedSolid:
				return new HeadsetSolidEffectImpl(item);
			case HeadsetEffectType.LoopedWave:
				return new HeadsetWaveEffectImpl(item);
			default:
				return null;
			}
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x000577D8 File Offset: 0x000559D8
		public override void UpdateName()
		{
			base.UpdateName();
			string resourceString = Utils.GetResourceString("Headset");
			base.Name = HyperXCenter.Center.GetNewDeviceName(resourceString);
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x00057807 File Offset: 0x00055A07
		public virtual void ApplyVolumeSettings()
		{
			base.GetSafePreset();
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x00057807 File Offset: 0x00055A07
		public virtual void ApplyMicrophoneSettings()
		{
			base.GetSafePreset();
		}

		// Token: 0x06002015 RID: 8213 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void ApplySurroundSettings()
		{
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void ApplyGamechatSettings()
		{
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void ApplyEQSettings(List<EQSetting> settings, int saveProfile = 0)
		{
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void ApplyGameEQ(KnownGames game)
		{
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x00057810 File Offset: 0x00055A10
		public override void ApplyPreset(Preset preset)
		{
			base.ApplyPreset(preset);
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x00057819 File Offset: 0x00055A19
		public virtual void ChangeEQStatus(bool enabled)
		{
			this.EQEnabled = enabled;
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x00057822 File Offset: 0x00055A22
		public virtual void ChangeSpatialStatus(bool enabled)
		{
			this.SpatialEnabled = enabled;
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x0005782B File Offset: 0x00055A2B
		public virtual void ChangeVoicePrompt(bool enabled)
		{
			this.VoicePrompt = enabled;
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x00057834 File Offset: 0x00055A34
		public virtual void ChangeMicrophoneMuteStatus(bool muted)
		{
			this.MicrophoneMuted = muted;
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void EnableBuildInEqualizer(bool enable)
		{
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void SwitchBuildInEqualizer(EqualizerPreset equalizerToBeSwitch)
		{
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void SaveBuildInEqualizer(EqualizerPreset equalizerToBeSave)
		{
		}

		// Token: 0x06002021 RID: 8225 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void RealTimeBuildInEqualizer(EqualizerPreset equalizer)
		{
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x00057840 File Offset: 0x00055A40
		public override void OnKeyReceived(KeyCode key, int state)
		{
			base.OnKeyReceived(key, state);
			if (state == 1)
			{
				Preset preset = null;
				lock (this)
				{
					preset = base.Preset;
				}
				if (preset != null)
				{
					if (preset.Headset.UseBaseKeyAssignments)
					{
						preset = Preset.BasePreset;
					}
					List<KeyAssignment> assignments = preset.Headset.Assignments;
					lock (assignments)
					{
						foreach (KeyAssignment keyAssignment in preset.Headset.Assignments)
						{
							if (keyAssignment.AssignmentType == AssignmentType.Open && keyAssignment.Key == key)
							{
								AppFunctions.OpenKeyAssignment(keyAssignment);
							}
							if (keyAssignment.AssignmentType == AssignmentType.Multimedia && keyAssignment.Key == key)
							{
								switch (keyAssignment.Multimedia)
								{
								case MultimediaType.VolumeUp:
									WindowsShortcutFunctions.VolumeUp();
									break;
								case MultimediaType.VolumeDown:
									WindowsShortcutFunctions.VolumeDown();
									break;
								case MultimediaType.MuteVolume:
									WindowsShortcutFunctions.Mute();
									break;
								case MultimediaType.MicVolumeUp:
									WindowsShortcutFunctions.MicVolumeUp();
									break;
								case MultimediaType.MicVolumeDown:
									WindowsShortcutFunctions.MicVolumeDown();
									break;
								case MultimediaType.MuteMic:
									WindowsShortcutFunctions.MuteMicrophone();
									break;
								case MultimediaType.MuteAll:
									WindowsShortcutFunctions.MuteAll();
									break;
								case MultimediaType.PlayPause:
									WindowsShortcutFunctions.PlayPause();
									break;
								case MultimediaType.NextTrack:
									WindowsShortcutFunctions.NextTrack();
									break;
								case MultimediaType.PreviousTrack:
									WindowsShortcutFunctions.PreviousTrack();
									break;
								case MultimediaType.Stop:
									WindowsShortcutFunctions.StopPlayer();
									break;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public override void SetLightings(IList<KeyMap> keys)
		{
		}

		// Token: 0x06002024 RID: 8228 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public override void SetAllLEDOff()
		{
		}

		// Token: 0x06002025 RID: 8229 RVA: 0x00057A2C File Offset: 0x00055C2C
		public override void ResetToDefault()
		{
			base.ResetToDefault();
			IReadOnlyList<MMDevice> audioDevices = base.AudioDevices;
			lock (audioDevices)
			{
				foreach (MMDevice mmdevice in base.AudioDevices)
				{
					if (mmdevice.DataFlow == AudioDeviceDataFlow.Render)
					{
						mmdevice.Muted = false;
					}
					if (mmdevice.DataFlow == AudioDeviceDataFlow.Capture)
					{
						mmdevice.Muted = false;
					}
				}
			}
		}

		// Token: 0x04001DB3 RID: 7603
		private const int SERIALIZER_TAILER = 2116567778;

		// Token: 0x04001DCB RID: 7627
		protected AudioDeviceChangedCommand _audioDeviceChangedCommand;
	}
}

using System;
using System.Collections.Generic;

namespace NGenuity2.Devices
{
	// Token: 0x02000663 RID: 1635
	public abstract class HyperXHeadsetDevice<T1, T2> : HyperXHeadsetDevice where T1 : HXCommandBase where T2 : T1
	{
		// Token: 0x06002160 RID: 8544 RVA: 0x0005FF2C File Offset: 0x0005E12C
		public virtual void AddCommand(T1 cmd)
		{
			base.AddCommandInternal(cmd);
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x0005FF3A File Offset: 0x0005E13A
		public virtual void AddCommand(HXCommandCollection<T1> cmd)
		{
			base.AddCommandInternal(cmd);
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x0005FF44 File Offset: 0x0005E144
		public override void ClearLightingCommands()
		{
			List<HXCommandBase> commands = base.Commands;
			lock (commands)
			{
				base.Commands.RemoveAll((HXCommandBase o) => o is T2);
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using NGenuity2.Common;
using NGenuity2.Common.Effects.MatrixConfigs.Commons;
using NGenuity2.Common.Effects.MatrixConfigs.Devices;
using NGenuity2.Common.RPCFunctions;
using NGenuity2.Effects;
using NGenuity2.Effects.Keyboard;
using NGenuity2.Effects.Matrix;
using NGenuity2.Model;

namespace NGenuity2.Devices
{
	// Token: 0x0200064D RID: 1613
	public abstract class HyperXKeyboardDevice : HyperXDevice
	{
		// Token: 0x06002026 RID: 8230 RVA: 0x00057AC0 File Offset: 0x00055CC0
		public override void Serialize(BinaryWriter bw)
		{
			base.Serialize(bw);
			bw.Write(this.HasBigEnter);
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x00057AD5 File Offset: 0x00055CD5
		public override void Deserialize(BinaryReader br, int version)
		{
			base.Deserialize(br, version);
			this.HasBigEnter = br.ReadBoolean();
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06002028 RID: 8232 RVA: 0x00057AEB File Offset: 0x00055CEB
		// (set) Token: 0x06002029 RID: 8233 RVA: 0x00057AF3 File Offset: 0x00055CF3
		public bool HasBigEnter { get; set; }

		// Token: 0x0600202A RID: 8234 RVA: 0x00057AFC File Offset: 0x00055CFC
		public HyperXKeyboardDevice()
		{
			base.DeviceType = HyperXDeviceType.Keyboard;
			base.ProfileSlots = 3;
			base.CanLink = true;
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x00057B1C File Offset: 0x00055D1C
		public override void UpdateName()
		{
			base.UpdateName();
			string resourceString = Utils.GetResourceString("Keyboard");
			base.Name = HyperXCenter.Center.GetNewDeviceName(resourceString);
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x00057B4B File Offset: 0x00055D4B
		internal void TriggerKey(KeyCode key)
		{
			EffectEngine effectEngine = this._effectEngine;
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public virtual void ChangeLayout()
		{
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x00057810 File Offset: 0x00055A10
		public override void ApplyPreset(Preset preset)
		{
			base.ApplyPreset(preset);
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x00057B54 File Offset: 0x00055D54
		public override void ApplyBasicSettings()
		{
			base.ApplyBasicSettings();
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x00057B5C File Offset: 0x00055D5C
		public override void ChangeBrightness(int brightness)
		{
			Settings.Instance.KeyboardBrightness = brightness;
			base.ChangeBrightness(brightness);
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x00057B70 File Offset: 0x00055D70
		public override void OnGameModeChanged(bool gameMode)
		{
			base.OnGameModeChanged(gameMode);
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x00057B7C File Offset: 0x00055D7C
		public override void ApplyEffects()
		{
			base.ApplyEffects();
			if (base.Engine == null)
			{
				return;
			}
			if (base.Preset != null)
			{
				List<KeyboardEffect> effects;
				if (base.Preset.Keyboard.UseBaseEffects)
				{
					effects = Preset.BasePreset.Keyboard.Effects;
				}
				else
				{
					effects = base.Preset.Keyboard.Effects;
				}
				foreach (KeyboardEffect keyboardEffect in effects)
				{
					keyboardEffect.ChangeDevice(this);
					if (keyboardEffect.Visible)
					{
						EffectImplBase effectImplBase = this.CreateEffect(keyboardEffect);
						if (effectImplBase != null)
						{
							base.Engine.AddEffect(effectImplBase);
						}
					}
				}
			}
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x00057C3C File Offset: 0x00055E3C
		public override void OnLightLevelChanged(int level)
		{
			Settings.Instance.KeyboardBrightness = level;
			base.OnLightLevelChanged(level);
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x00057C50 File Offset: 0x00055E50
		public override void ApplyPresetAndEffects(Preset preset)
		{
			base.ApplyPresetAndEffects(preset);
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x00057C5C File Offset: 0x00055E5C
		public override EffectImplBase CreateEffect(EffectItemBase item)
		{
			if (!(item is KeyboardEffect))
			{
				return null;
			}
			KeyboardEffect keyboardEffect = (KeyboardEffect)item;
			KeyboardEffectType type = keyboardEffect.Type;
			if (type <= KeyboardEffectType.TriggeredZephyr)
			{
				switch (type)
				{
				case KeyboardEffectType.LoopedBreathing:
					return new KeyboardBreathingEffectImpl((KeyboardBreathingEffect)keyboardEffect);
				case KeyboardEffectType.LoopedBreeze:
				case (KeyboardEffectType)3:
				case KeyboardEffectType.LoopedMoon:
				case KeyboardEffectType.LoopedRain:
				case KeyboardEffectType.LoopedSnow:
				case KeyboardEffectType.LoopedSunrise:
				case KeyboardEffectType.LoopedSunset:
				case KeyboardEffectType.LoopedRoyal:
					break;
				case KeyboardEffectType.LoopedCycle:
					return new KeyboardCycleEffectImpl((KeyboardCycleEffect)keyboardEffect);
				case KeyboardEffectType.LoopedHourglass:
					return new KeyboardHourglassEffectImpl((KeyboardHourglassEffect)keyboardEffect);
				case KeyboardEffectType.LoopedSolid:
					return new KeyboardSolidEffectImpl((KeyboardSolidEffect)keyboardEffect);
				case KeyboardEffectType.LoopedTwilight:
					return new KeyboardTwilightEffectImpl((KeyboardTwilightEffect)keyboardEffect);
				case KeyboardEffectType.LoopedWave:
					return new KeyboardWaveEffectImpl((KeyboardWaveEffect)keyboardEffect);
				case KeyboardEffectType.LoopedSwipe:
					return new KeyboardSwipeEffectImpl((KeyboardSwipeEffect)keyboardEffect);
				case KeyboardEffectType.LoopedConfetti:
					return new KeyboardConfettiEffectImpl((KeyboardConfettiEffect)keyboardEffect);
				case KeyboardEffectType.LoopedStacking:
					return new KeyboardStackedEffectImpl((KeyboardStackingEffect)keyboardEffect);
				case KeyboardEffectType.LoopedPaint:
					return new KeyboardSolidEffectImpl((KeyboardPaintEffect)keyboardEffect);
				default:
					switch (type)
					{
					case KeyboardEffectType.LoopedPlayback:
						return new KeyboardPlaybackEffectImpl((KeyboardPlaybackEffect)keyboardEffect);
					case KeyboardEffectType.TriggeredBlaze:
						return new KeyboardBlazeEffectImpl((KeyboardBlazeEffect)keyboardEffect);
					case KeyboardEffectType.TriggeredExplosion:
						return new KeyboardExplosionEffectImpl((KeyboardExplosionEffect)keyboardEffect);
					case KeyboardEffectType.TriggeredFade:
						return new KeyboardFadeEffectImpl((KeyboardFadeEffect)keyboardEffect);
					case KeyboardEffectType.TriggeredFireworks:
						return new KeyboardFireworksEffectImpl((KeyboardFireworksEffect)keyboardEffect);
					case KeyboardEffectType.TriggeredFrost:
						return new KeyboardFrostEffectImpl((KeyboardFrostEffect)keyboardEffect);
					case KeyboardEffectType.TriggeredImplosion:
						return new KeyboardImplosionEffectImpl((KeyboardImplosionEffect)keyboardEffect);
					}
					break;
				}
			}
			else
			{
				switch (type)
				{
				case KeyboardEffectType.ReactiveThermometer:
				case KeyboardEffectType.ReactiveSnake:
				case KeyboardEffectType.ReactiveInvaders:
					break;
				case KeyboardEffectType.ScreenMirror:
				{
					KeybaordConfigs keybaordConfigs = (KeybaordConfigs)keyboardEffect;
					Tuple<int, int> matrixSize = MatrixHelper.GetMatrixSize(keybaordConfigs);
					return new ScreenMirror(keybaordConfigs, matrixSize.Item1, matrixSize.Item2);
				}
				case KeyboardEffectType.VideoCapture:
				{
					KeybaordConfigs keybaordConfigs2 = (KeybaordConfigs)keyboardEffect;
					Tuple<int, int> matrixSize = MatrixHelper.GetMatrixSize(keybaordConfigs2);
					return new VideoCapture(keybaordConfigs2, matrixSize.Item1, matrixSize.Item2);
				}
				default:
					if (type == KeyboardEffectType.LoopedSun)
					{
						return new KeyboardSunEffectImpl((KeyboardSunEffect)keyboardEffect);
					}
					break;
				}
			}
			return null;
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x00057E60 File Offset: 0x00056060
		public override void OnKeyReceived(KeyCode key, int state)
		{
			base.OnKeyReceived(key, state);
			if (state == 1)
			{
				Preset preset = null;
				lock (this)
				{
					preset = base.Preset;
				}
				if (preset != null)
				{
					List<KeyAssignment> assignments = preset.Keyboard.Assignments;
					lock (assignments)
					{
						foreach (KeyAssignment keyAssignment in preset.Keyboard.Assignments)
						{
							if (keyAssignment.AssignmentType == AssignmentType.Open && keyAssignment.Key == key)
							{
								AppFunctions.OpenKeyAssignment(keyAssignment);
							}
							if (keyAssignment.AssignmentType == AssignmentType.Multimedia && keyAssignment.Key == key)
							{
								if (keyAssignment.Multimedia == MultimediaType.MuteMic)
								{
									WindowsShortcutFunctions.MuteMicrophone();
								}
								if (keyAssignment.Multimedia == MultimediaType.MuteAll)
								{
									WindowsShortcutFunctions.MuteAll();
								}
								if (keyAssignment.Multimedia == MultimediaType.MicVolumeDown)
								{
									WindowsShortcutFunctions.MicVolumeDown();
								}
								if (keyAssignment.Multimedia == MultimediaType.MicVolumeUp)
								{
									WindowsShortcutFunctions.MicVolumeUp();
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x00057F8C File Offset: 0x0005618C
		public override void ApplyKeyAssignments()
		{
			base.ApplyKeyAssignments();
			this.ApplyKeyAssignments(Preset.CurrentPreset.Keyboard.Assignments);
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x00057FAC File Offset: 0x000561AC
		protected Preset GenerateSyncSnapshot()
		{
			Preset safePreset = base.GetSafePreset();
			if (safePreset == null)
			{
				return null;
			}
			Preset preset = new Preset();
			if (safePreset.Keyboard.UseBaseKeyAssignments)
			{
				Preset.BasePreset.Keyboard.DeepCopyTo(preset.Keyboard);
			}
			else
			{
				safePreset.Keyboard.DeepCopyTo(preset.Keyboard);
			}
			preset.Keyboard.Effects.Clear();
			if (safePreset.Keyboard.UseBaseEffects)
			{
				preset.Keyboard.Effects.AddRange(Preset.BasePreset.Keyboard.Effects);
			}
			else
			{
				preset.Keyboard.Effects.AddRange(safePreset.Keyboard.Effects);
			}
			return preset;
		}
	}
}

using System;
using System.Collections.Generic;

namespace NGenuity2.Devices
{
	// Token: 0x02000664 RID: 1636
	public abstract class HyperXKeyboardDevice<T1, T2> : HyperXKeyboardDevice where T1 : HXCommandBase where T2 : T1
	{
		// Token: 0x06002164 RID: 8548 RVA: 0x0005FFB4 File Offset: 0x0005E1B4
		public virtual void AddCommand(T1 cmd)
		{
			base.AddCommandInternal(cmd);
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x0005FF3A File Offset: 0x0005E13A
		public virtual void AddCommand(HXCommandCollection<T1> cmd)
		{
			base.AddCommandInternal(cmd);
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x0005FFC4 File Offset: 0x0005E1C4
		public override void ClearLightingCommands()
		{
			List<HXCommandBase> commands = base.Commands;
			lock (commands)
			{
				base.Commands.RemoveAll((HXCommandBase o) => o is T2);
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using NGenuity2.Audio;
using NGenuity2.Common;
using NGenuity2.Common.Commands.Audio;
using NGenuity2.Common.Devices;
using NGenuity2.Common.Effects.MatrixConfigs.Commons;
using NGenuity2.Common.Effects.MatrixConfigs.Devices;
using NGenuity2.Effects;
using NGenuity2.Effects.Matrix;
using NGenuity2.Effects.Microphone;
using NGenuity2.Model;

namespace NGenuity2.Devices
{
	// Token: 0x0200064E RID: 1614
	public class HyperXMicrophoneDevice : HyperXDevice
	{
		// Token: 0x06002039 RID: 8249 RVA: 0x0005805A File Offset: 0x0005625A
		public override void Serialize(BinaryWriter bw)
		{
			base.Serialize(bw);
			bw.Write((int)this.PolarPattern);
			bw.Write(this.ReverseLights);
			bw.Write(this.MicrophoneGain);
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x00058087 File Offset: 0x00056287
		public override void Deserialize(BinaryReader br, int version)
		{
			base.Deserialize(br, version);
			this.PolarPattern = (PolarPattern)br.ReadInt32();
			this.ReverseLights = br.ReadBoolean();
			this.MicrophoneGain = br.ReadInt32();
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600203B RID: 8251 RVA: 0x000580B5 File Offset: 0x000562B5
		// (set) Token: 0x0600203C RID: 8252 RVA: 0x000580BD File Offset: 0x000562BD
		public PolarPattern PolarPattern { get; set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600203D RID: 8253 RVA: 0x000580C6 File Offset: 0x000562C6
		// (set) Token: 0x0600203E RID: 8254 RVA: 0x000580CE File Offset: 0x000562CE
		public bool ReverseLights { get; set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600203F RID: 8255 RVA: 0x000580D7 File Offset: 0x000562D7
		// (set) Token: 0x06002040 RID: 8256 RVA: 0x000580DF File Offset: 0x000562DF
		public int MicrophoneGain { get; set; }

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x06002041 RID: 8257 RVA: 0x000580E8 File Offset: 0x000562E8
		// (remove) Token: 0x06002042 RID: 8258 RVA: 0x00058120 File Offset: 0x00056320
		public event EventHandler<PolarPattern> PolarPatternChanged;

		// Token: 0x1400002D RID: 45
		// (add) Token: 0x06002043 RID: 8259 RVA: 0x00058158 File Offset: 0x00056358
		// (remove) Token: 0x06002044 RID: 8260 RVA: 0x00058190 File Offset: 0x00056390
		public event EventHandler<int> MicrophoenGainChanged;

		// Token: 0x06002045 RID: 8261 RVA: 0x000581C5 File Offset: 0x000563C5
		public HyperXMicrophoneDevice()
		{
			base.DeviceType = HyperXDeviceType.Microphone;
			base.ProfileSlots = 1;
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x000581DC File Offset: 0x000563DC
		public override void UpdateName()
		{
			base.UpdateName();
			string resourceString = Utils.GetResourceString("Microphone");
			base.Name = HyperXCenter.Center.GetNewDeviceName(resourceString);
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x0005820C File Offset: 0x0005640C
		public override EffectImplBase CreateEffect(EffectItemBase e)
		{
			MicrophoneEffect microphoneEffect = (MicrophoneEffect)e;
			MicrophoneEffectType type = microphoneEffect.Type;
			switch (type)
			{
			case MicrophoneEffectType.LoopedSolid:
				return new MicrophoneSolidEffectImpl(microphoneEffect);
			case MicrophoneEffectType.LoopedBlink:
				return new MicrophoneBlinkEffectImpl(microphoneEffect);
			case MicrophoneEffectType.LoopedCycle:
				return new MicrophoneCycleEffectImpl(microphoneEffect);
			case MicrophoneEffectType.LoopedLightning:
				return new MicrophoneLightningEffectImpl(microphoneEffect);
			case MicrophoneEffectType.LoopedWave:
				return new MicrophoneWaveEffectImpl(microphoneEffect);
			case MicrophoneEffectType.LoopedVUMeter:
				return new MicrophoneVUMeterEffectImpl(microphoneEffect, this);
			default:
			{
				Tuple<int, int> matrixSize;
				if (type == MicrophoneEffectType.ScreenMirror)
				{
					MicrophoneConfig microphoneConfig = (MicrophoneConfig)microphoneEffect;
					matrixSize = MatrixHelper.GetMatrixSize(microphoneConfig);
					return new ScreenMirror(microphoneConfig, matrixSize.Item1, matrixSize.Item2);
				}
				if (type != MicrophoneEffectType.VideoCapture)
				{
					return null;
				}
				MicrophoneConfig microphoneConfig2 = (MicrophoneConfig)microphoneEffect;
				matrixSize = MatrixHelper.GetMatrixSize(microphoneConfig2);
				return new VideoCapture(microphoneConfig2, matrixSize.Item1, matrixSize.Item2);
			}
			}
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x000582C3 File Offset: 0x000564C3
		public virtual void ApplyShowLights(bool muted)
		{
			this.ReverseLights = muted;
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x000582CC File Offset: 0x000564CC
		public override void ApplyEffects()
		{
			base.ApplyEffects();
			Preset safePreset = base.GetSafePreset();
			if (safePreset != null)
			{
				List<MicrophoneEffect> effects;
				if (safePreset.Microphone.UseBaseEffects)
				{
					effects = Preset.BasePreset.Microphone.Effects;
				}
				else
				{
					effects = safePreset.Microphone.Effects;
				}
				foreach (MicrophoneEffect microphoneEffect in effects)
				{
					microphoneEffect.ChangeDevice(this);
					if (microphoneEffect.Visible)
					{
						EffectImplBase effectImplBase = this.CreateEffect(microphoneEffect);
						if (effectImplBase != null)
						{
							base.Engine.AddEffect(effectImplBase);
						}
					}
				}
			}
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x0005837C File Offset: 0x0005657C
		protected virtual void OnPolarPatternChanged(PolarPattern pattern)
		{
			if (this.PolarPattern != pattern)
			{
				this.PolarPattern = pattern;
				EventHandler<PolarPattern> polarPatternChanged = this.PolarPatternChanged;
				if (polarPatternChanged == null)
				{
					return;
				}
				polarPatternChanged(this, pattern);
			}
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x000583A0 File Offset: 0x000565A0
		protected virtual void OnMicrophoneGainChanged(int gain)
		{
			if (this.MicrophoneGain != gain)
			{
				this.MicrophoneGain = gain;
				EventHandler<int> microphoenGainChanged = this.MicrophoenGainChanged;
				if (microphoenGainChanged == null)
				{
					return;
				}
				microphoenGainChanged(this, gain);
			}
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x000583C4 File Offset: 0x000565C4
		public override void ChangeBrightness(int brightness)
		{
			Settings.Instance.MicrophoneBrightness = brightness;
			base.ChangeBrightness(brightness);
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x000583D8 File Offset: 0x000565D8
		public virtual void ChangePolarPattern(PolarPattern pattern)
		{
			this.PolarPattern = pattern;
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x000583E1 File Offset: 0x000565E1
		public virtual void ChangeMicrophoneGain(int gain)
		{
			this.MicrophoneGain = gain;
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x00057834 File Offset: 0x00055A34
		public virtual void ChangeMicrophoneMuteStatus(bool muted)
		{
			this.MicrophoneMuted = muted;
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public override void SetAllLEDOff()
		{
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public override void SetLightings(IList<KeyMap> keys)
		{
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x000583EC File Offset: 0x000565EC
		public override void Start()
		{
			base.Start();
			HXAudio.Instance.RegisterHyperXDevice(this);
			if (this._audioDeviceConnectedCommand == null)
			{
				this._audioDeviceConnectedCommand = new AudioDeviceConnectedCommand();
				this._audioDeviceConnectedCommand.DeviceId = base.DeviceID;
				this._audioDeviceConnectedCommand.Subscribe();
			}
			if (this._audioDeviceChangedCommand == null)
			{
				this._audioDeviceChangedCommand = new AudioDeviceChangedCommand();
				this._audioDeviceChangedCommand.DeviceId = base.DeviceID;
				this._audioDeviceChangedCommand.Subscribe();
			}
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x00058468 File Offset: 0x00056668
		public override void Stop(bool waitUntilStopped)
		{
			HXAudio.Instance.UnregisterHyperXDevice(this);
			base.Stop(waitUntilStopped);
			if (this._audioDeviceConnectedCommand != null)
			{
				this._audioDeviceConnectedCommand.Unsubscribe();
				this._audioDeviceConnectedCommand = null;
			}
			if (this._audioDeviceChangedCommand != null)
			{
				this._audioDeviceChangedCommand.Unsubscribe();
				this._audioDeviceChangedCommand = null;
			}
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x000584BC File Offset: 0x000566BC
		public override void ResetToDefault()
		{
			base.ResetToDefault();
			IReadOnlyList<MMDevice> audioDevices = base.AudioDevices;
			lock (audioDevices)
			{
				foreach (MMDevice mmdevice in base.AudioDevices)
				{
					if (mmdevice.DataFlow == AudioDeviceDataFlow.Render)
					{
						mmdevice.Muted = false;
						mmdevice.SidetoneMuted = true;
					}
					if (mmdevice.DataFlow == AudioDeviceDataFlow.Capture)
					{
						mmdevice.Muted = false;
					}
				}
			}
		}

		// Token: 0x04001DD2 RID: 7634
		private AudioDeviceConnectedCommand _audioDeviceConnectedCommand;

		// Token: 0x04001DD3 RID: 7635
		private AudioDeviceChangedCommand _audioDeviceChangedCommand;
	}
}

using System;
using System.Collections.Generic;

namespace NGenuity2.Devices
{
	// Token: 0x02000665 RID: 1637
	public class HyperXMicrophoneDevice<T1, T2> : HyperXMicrophoneDevice where T1 : HXCommandBase where T2 : T1
	{
		// Token: 0x06002168 RID: 8552 RVA: 0x00060034 File Offset: 0x0005E234
		public virtual void AddCommand(T1 cmd)
		{
			base.AddCommandInternal(cmd);
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x0005FF3A File Offset: 0x0005E13A
		public virtual void AddCommand(HXCommandCollection<T1> cmd)
		{
			base.AddCommandInternal(cmd);
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x00060044 File Offset: 0x0005E244
		public override void ClearLightingCommands()
		{
			List<HXCommandBase> commands = base.Commands;
			lock (commands)
			{
				base.Commands.RemoveAll((HXCommandBase o) => o is T2);
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NGenuity2.Common;
using NGenuity2.Common.RPCFunctions;
using NGenuity2.Effects;
using NGenuity2.Effects.Mouse;
using NGenuity2.Model;

namespace NGenuity2.Devices
{
	// Token: 0x0200064F RID: 1615
	public abstract class HyperXMouseDevice : HyperXDevice
	{
		// Token: 0x06002055 RID: 8277 RVA: 0x00058558 File Offset: 0x00056758
		public override void Serialize(BinaryWriter bw)
		{
			base.Serialize(bw);
			bw.Write(this.MaxDPI);
			bw.Write(this.DPIMaps.Count);
			for (int i = 0; i < this.DPIMaps.Count; i++)
			{
				KeyValuePair<int, int> keyValuePair = this.DPIMaps[i];
				bw.Write(keyValuePair.Key);
				bw.Write(keyValuePair.Value);
			}
			bw.Write(this.SuppportLift);
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x000585D4 File Offset: 0x000567D4
		public override void Deserialize(BinaryReader br, int version)
		{
			base.Deserialize(br, version);
			this.MaxDPI = br.ReadInt32();
			int num = br.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				br.ReadInt32();
				br.ReadInt32();
			}
			this.SuppportLift = br.ReadBoolean();
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06002057 RID: 8279 RVA: 0x00058622 File Offset: 0x00056822
		// (set) Token: 0x06002058 RID: 8280 RVA: 0x0005862A File Offset: 0x0005682A
		public int MaxDPI { get; set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06002059 RID: 8281 RVA: 0x00058633 File Offset: 0x00056833
		// (set) Token: 0x0600205A RID: 8282 RVA: 0x0005863B File Offset: 0x0005683B
		public bool SuppportLift { get; set; }

		// Token: 0x0600205B RID: 8283 RVA: 0x00058644 File Offset: 0x00056844
		public HyperXMouseDevice()
		{
			base.DeviceType = HyperXDeviceType.Mouse;
			this.DPIMaps = new List<KeyValuePair<int, int>>();
			this.MaxDPI = 16000;
			base.CanLink = true;
			this.SetupDPIMaps();
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x0600205C RID: 8284 RVA: 0x00058676 File Offset: 0x00056876
		// (set) Token: 0x0600205D RID: 8285 RVA: 0x0005867E File Offset: 0x0005687E
		public List<KeyValuePair<int, int>> DPIMaps { get; private set; }

		// Token: 0x0600205E RID: 8286 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		protected virtual void SetupDPIMaps()
		{
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x00058688 File Offset: 0x00056888
		public int GetDPIKey(int dpi)
		{
			if (this.DPIMaps.Count == 0)
			{
				return 0;
			}
			for (int i = 0; i < this.DPIMaps.Count; i++)
			{
				if (this.DPIMaps[i].Value == dpi)
				{
					return this.DPIMaps[i].Key;
				}
			}
			if (dpi < this.DPIMaps[0].Value)
			{
				return this.DPIMaps[0].Key;
			}
			if (dpi > this.DPIMaps[this.DPIMaps.Count - 1].Value)
			{
				return this.DPIMaps[this.DPIMaps.Count - 1].Key;
			}
			for (int j = 0; j < this.DPIMaps.Count - 1; j++)
			{
				if (this.DPIMaps[j].Value < dpi && this.DPIMaps[j + 1].Value > dpi)
				{
					return this.DPIMaps[j].Key;
				}
			}
			return 0;
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x000587B4 File Offset: 0x000569B4
		public override void ChangeBrightness(int brightness)
		{
			Settings.Instance.MouseBrightness = brightness;
			base.ChangeBrightness(brightness);
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x000587C8 File Offset: 0x000569C8
		public override void UpdateName()
		{
			base.UpdateName();
			string resourceString = Utils.GetResourceString("Mouse");
			base.Name = HyperXCenter.Center.GetNewDeviceName(resourceString);
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x000587F8 File Offset: 0x000569F8
		public override void ApplyEffects()
		{
			base.ApplyEffects();
			Preset safePreset = base.GetSafePreset();
			try
			{
				if (safePreset != null)
				{
					List<MouseEffect> effects;
					if (safePreset.Mouse.UseBaseEffects)
					{
						effects = Preset.BasePreset.Mouse.Effects;
					}
					else
					{
						effects = safePreset.Mouse.Effects;
					}
					foreach (MouseEffect mouseEffect in effects)
					{
						mouseEffect.ChangeDevice(this);
						if (mouseEffect.Visible)
						{
							EffectImplBase effectImplBase = this.CreateEffect(mouseEffect);
							if (effectImplBase != null)
							{
								EffectEngine engine = base.Engine;
								if (engine != null)
								{
									engine.AddEffect(effectImplBase);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.WriteLine("Failed to Apply Effects: " + ex.Message + ", Device = " + HyperXDeviceUtils.GetShortDeviceTitle(base.Model), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\HyperXMouseDevice.cs", 122);
			}
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x00057B54 File Offset: 0x00055D54
		public override void ApplyBasicSettings()
		{
			base.ApplyBasicSettings();
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x000588F0 File Offset: 0x00056AF0
		public override EffectImplBase CreateEffect(EffectItemBase item)
		{
			MouseEffect mouseEffect = (MouseEffect)item;
			MouseEffectType type = mouseEffect.Type;
			switch (type)
			{
			case MouseEffectType.LoopedBreathing:
				return new MouseBreathingEffectImpl(mouseEffect);
			case MouseEffectType.LoopedCycle:
				return new MouseColorCycleEffectImpl(mouseEffect);
			case MouseEffectType.LoopedSolid:
				return new MouseSolidEffectImpl(mouseEffect);
			case MouseEffectType.LoopedWave:
				return new MouseWaveEffectImpl(mouseEffect);
			case MouseEffectType.LoopedPulse:
				return new MousePulseEffectImpl(mouseEffect);
			default:
				if (type == MouseEffectType.TriggeredFade)
				{
					return new MouseTriggerFadeEffectImpl(mouseEffect);
				}
				if (type != MouseEffectType.TriggeredDPI)
				{
					return null;
				}
				return new MouseTriggerDPIEffectImpl(mouseEffect);
			}
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x00058968 File Offset: 0x00056B68
		public void TriggerDPILevel(Color color)
		{
			MouseTriggerDPIEffect mouseTriggerDPIEffect = new MouseTriggerDPIEffect(base.Keys);
			mouseTriggerDPIEffect.Color.SetSolidColor(color);
			List<LightingItem> list = mouseTriggerDPIEffect.Lightings.SafeClone<LightingItem>();
			foreach (LightingItem lightingItem in list)
			{
				lightingItem.Color = color;
			}
			foreach (LightingItem lightingItem2 in mouseTriggerDPIEffect.SelectedLightings.SafeClone<LightingItem>())
			{
				lightingItem2.Color = color;
			}
			TriggerEffectImplBase triggerEffectImplBase = (TriggerEffectImplBase)this.CreateEffect(mouseTriggerDPIEffect);
			LightingItem lightingItem3 = list.First<LightingItem>();
			triggerEffectImplBase.TriggerX = lightingItem3.X;
			triggerEffectImplBase.TriggerY = lightingItem3.Y;
			base.Engine.TriggerEffect(triggerEffectImplBase);
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x00058A58 File Offset: 0x00056C58
		public override void OnMouseDPILevelChanged(int preset)
		{
			base.OnMouseDPILevelChanged(preset);
			if (base.Preset != null)
			{
				base.Preset.Mouse.DPIStage = preset;
				base.Preset.Updated = true;
				try
				{
					if (base.Model == HyperXDeviceModel.MousePulsefireDart)
					{
						this.TriggerDPILevel(base.Preset.Mouse.DPIs[preset].Color);
					}
				}
				catch (Exception)
				{
				}
				if (Settings.Instance.ShowNotifications && Settings.Instance.NotifyDPIChange)
				{
					this.NotifyDPIChange();
				}
			}
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x00058AF0 File Offset: 0x00056CF0
		public int Ref2RealDPI(int val)
		{
			double num = (double)(val - 200) * (double)(this.MaxDPI - 200) / 15800.0;
			double num2 = num - Math.Floor(num);
			if (num2 > 0.5)
			{
				num2 = 1.0;
			}
			else
			{
				num2 = 0.0;
			}
			int num3 = (int)(num + num2) + 200;
			if (num3 > this.MaxDPI)
			{
				num3 = this.MaxDPI;
			}
			return num3;
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x00058B64 File Offset: 0x00056D64
		private void NotifyDPIChange()
		{
			try
			{
				string text = HyperXDeviceUtils.GetDeviceTitle(this);
				text = Settings.Instance.GetDeviceName(base.DeviceID, text);
				int dpi = base.Preset.Mouse.DPIs[base.Preset.Mouse.DPIStage].Dpi;
				string message = string.Format(Utils.GetResourceString("DPISetTo"), dpi);
				NotificationCenter.PopMessage("com.ngenuity.dpichange", text, message, true);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x00058BF0 File Offset: 0x00056DF0
		public override void OnKeyReceived(KeyCode key, int state)
		{
			base.OnKeyReceived(key, state);
			if (state == 1)
			{
				if (base.Engine != null)
				{
					base.Engine.TriggerKey(0, 0);
				}
				Preset preset = null;
				lock (this)
				{
					preset = base.Preset;
				}
				if (preset != null)
				{
					if (preset.Mouse.UseBaseKeyAssignments)
					{
						preset = Preset.BasePreset;
					}
					List<KeyAssignment> assignments = preset.Mouse.Assignments;
					lock (assignments)
					{
						foreach (KeyAssignment keyAssignment in preset.Mouse.Assignments)
						{
							if (keyAssignment.AssignmentType == AssignmentType.Open && keyAssignment.Key == key)
							{
								AppFunctions.OpenKeyAssignment(keyAssignment);
							}
							if (keyAssignment.AssignmentType == AssignmentType.Multimedia && keyAssignment.Key == key)
							{
								if (keyAssignment.Multimedia == MultimediaType.MuteMic)
								{
									WindowsShortcutFunctions.MuteMicrophone();
								}
								if (keyAssignment.Multimedia == MultimediaType.MuteAll)
								{
									WindowsShortcutFunctions.MuteAll();
								}
								if (keyAssignment.Multimedia == MultimediaType.MicVolumeDown)
								{
									WindowsShortcutFunctions.MicVolumeDown();
								}
								if (keyAssignment.Multimedia == MultimediaType.MicVolumeUp)
								{
									WindowsShortcutFunctions.MicVolumeUp();
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x00058D44 File Offset: 0x00056F44
		public override void PreStop()
		{
			base.PreStop();
		}
	}
}

using System;
using System.Collections.Generic;

namespace NGenuity2.Devices
{
	// Token: 0x02000666 RID: 1638
	public abstract class HyperXMouseDevice<T1, T2> : HyperXMouseDevice where T1 : HXCommandBase where T2 : T1
	{
		// Token: 0x0600216C RID: 8556 RVA: 0x000600B4 File Offset: 0x0005E2B4
		public virtual void AddCommand(T1 cmd)
		{
			if (!this._stopping || !(cmd is T2))
			{
				base.AddCommandInternal(cmd);
			}
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x0005FF3A File Offset: 0x0005E13A
		public virtual void AddCommand(HXCommandCollection<T1> cmd)
		{
			base.AddCommandInternal(cmd);
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x000600D8 File Offset: 0x0005E2D8
		public override void ClearLightingCommands()
		{
			List<HXCommandBase> commands = base.Commands;
			lock (commands)
			{
				base.Commands.RemoveAll((HXCommandBase o) => o is T2);
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using NGenuity2.Common;
using NGenuity2.Effects;
using NGenuity2.Effects.MousePad;
using NGenuity2.Model;

namespace NGenuity2.Devices
{
	// Token: 0x02000650 RID: 1616
	public class HyperXMousePadDevice : HyperXDevice
	{
		// Token: 0x0600206B RID: 8299 RVA: 0x00058D4C File Offset: 0x00056F4C
		public override void Serialize(BinaryWriter bw)
		{
			base.Serialize(bw);
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x00058D55 File Offset: 0x00056F55
		public override void Deserialize(BinaryReader br, int version)
		{
			base.Deserialize(br, version);
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x00058D5F File Offset: 0x00056F5F
		public HyperXMousePadDevice()
		{
			base.DeviceType = HyperXDeviceType.Mousepad;
			base.CanLink = true;
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x00058D78 File Offset: 0x00056F78
		public override void UpdateName()
		{
			base.UpdateName();
			string resourceString = Utils.GetResourceString("Mousepad");
			base.Name = HyperXCenter.Center.GetNewDeviceName(resourceString);
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x00058DA8 File Offset: 0x00056FA8
		public override EffectImplBase CreateEffect(EffectItemBase e)
		{
			EffectImplBase result = null;
			MousePadEffect mousePadEffect = (MousePadEffect)e;
			MousePadEffectType type = mousePadEffect.Type;
			switch (type)
			{
			case MousePadEffectType.LoopedBreathing:
				result = new MousePadBreathingEffectImpl(mousePadEffect);
				break;
			case MousePadEffectType.LoopedCycle:
				result = new MousePadColorCycleEffectImpl(mousePadEffect);
				break;
			case MousePadEffectType.LoopedSolid:
				result = new MousePadSolidEffectImpl(mousePadEffect);
				break;
			case MousePadEffectType.LoopedWave:
				result = new MousePadWaveEffectImpl(mousePadEffect);
				break;
			case MousePadEffectType.LoopedPulse:
				break;
			default:
				if (type != MousePadEffectType.TriggeredMouse)
				{
				}
				break;
			}
			return result;
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x00058E0C File Offset: 0x0005700C
		public override void ApplyEffects()
		{
			base.ApplyEffects();
			if (base.Preset != null)
			{
				List<MousePadEffect> effects;
				if (base.Preset.Mousepad.UseBaseEffects)
				{
					effects = Preset.BasePreset.Mousepad.Effects;
				}
				else
				{
					effects = base.Preset.Mousepad.Effects;
				}
				foreach (MousePadEffect mousePadEffect in effects)
				{
					mousePadEffect.ChangeDevice(this);
					if (mousePadEffect.Visible)
					{
						EffectImplBase effectImplBase = this.CreateEffect(mousePadEffect);
						if (effectImplBase != null)
						{
							base.Engine.AddEffect(effectImplBase);
						}
					}
				}
				this.ChangeBrightness(Settings.Instance.MousePadBrightness);
			}
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public override void SetAllLEDOff()
		{
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public override void SetLightings(IList<KeyMap> keys)
		{
		}
	}
}

using System;
using System.Collections.Generic;

namespace NGenuity2.Devices
{
	// Token: 0x02000667 RID: 1639
	public class HyperXMousePadDevice<T1, T2> : HyperXMousePadDevice where T1 : HXCommandBase where T2 : T1
	{
		// Token: 0x06002170 RID: 8560 RVA: 0x00060148 File Offset: 0x0005E348
		public virtual void AddCommand(T1 cmd)
		{
			base.AddCommandInternal(cmd);
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x0005FF3A File Offset: 0x0005E13A
		public virtual void AddCommand(HXCommandCollection<T1> cmd)
		{
			base.AddCommandInternal(cmd);
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x00060158 File Offset: 0x0005E358
		public override void ClearLightingCommands()
		{
			List<HXCommandBase> commands = base.Commands;
			lock (commands)
			{
				base.Commands.RemoveAll((HXCommandBase o) => o is T2);
			}
		}
	}
}

using System;

namespace NGenuity2.Devices
{
	// Token: 0x02000657 RID: 1623
	public enum HyperXRemoteDeviceModel
	{
		// Token: 0x04001E03 RID: 7683
		Unknown,
		// Token: 0x04001E04 RID: 7684
		Remote_Headset_Cloud2CoreWirelessTread = 113,
		// Token: 0x04001E05 RID: 7685
		Remote_Headset_Cloud3WirelessAtlas = 116,
		// Token: 0x04001E06 RID: 7686
		Remote_Headset_Cloud3SWireless = 119,
		// Token: 0x04001E07 RID: 7687
		Remote_Mouse_PulsefireHaste2Pro = 911
	}
}

using System;
using System.IO;
using NGenuity2.Common;

namespace NGenuity2.Devices
{
	// Token: 0x02000651 RID: 1617
	public abstract class HyperXTIODevice : HyperXDevice
	{
		// Token: 0x06002073 RID: 8307 RVA: 0x00058D4C File Offset: 0x00056F4C
		public override void Serialize(BinaryWriter bw)
		{
			base.Serialize(bw);
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x00058D55 File Offset: 0x00056F55
		public override void Deserialize(BinaryReader br, int version)
		{
			base.Deserialize(br, version);
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x00058ED4 File Offset: 0x000570D4
		protected HyperXTIODevice()
		{
			base.DeviceType = HyperXDeviceType.ThreeInOneDongle;
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x00058EE4 File Offset: 0x000570E4
		public override void UpdateName()
		{
			base.UpdateName();
			string resourceString = Utils.GetResourceString("ThreeInOneDongle");
			base.Name = HyperXCenter.Center.GetNewDeviceName(resourceString);
		}
	}
}

using System;
using System.Collections.Generic;

namespace NGenuity2.Devices
{
	// Token: 0x02000668 RID: 1640
	public abstract class HyperXTIODevice<T1, T2> : HyperXTIODevice where T1 : HXCommandBase where T2 : T1
	{
		// Token: 0x06002174 RID: 8564 RVA: 0x000601C8 File Offset: 0x0005E3C8
		public virtual void AddCommand(T1 cmd)
		{
			base.AddCommandInternal(cmd);
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x0005FF3A File Offset: 0x0005E13A
		public virtual void AddCommand(HXCommandCollection<T1> cmd)
		{
			base.AddCommandInternal(cmd);
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x000601D8 File Offset: 0x0005E3D8
		public override void ClearLightingCommands()
		{
			List<HXCommandBase> commands = base.Commands;
			lock (commands)
			{
				base.Commands.RemoveAll((HXCommandBase o) => o is T2);
			}
		}
	}
}

using System;
using NGenuity2.Common.Devices;

namespace NGenuity2.Devices
{
	// Token: 0x02000669 RID: 1641
	public interface IHasANCControl
	{
		// Token: 0x06002178 RID: 8568
		void ChangeANCState(ANCState newState);
	}
}

using System;
using Windows.Foundation;

namespace NGenuity2.Devices
{
	// Token: 0x02000661 RID: 1633
	public interface IPairing
	{
		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06002157 RID: 8535
		// (set) Token: 0x06002158 RID: 8536
		PairingStates PairingState { get; set; }

		// Token: 0x06002159 RID: 8537
		void Pairing();

		// Token: 0x14000038 RID: 56
		// (add) Token: 0x0600215A RID: 8538
		// (remove) Token: 0x0600215B RID: 8539
		event TypedEventHandler<HyperXDevice, PairingStates> PairingStateReport;
	}
}

using System;

namespace NGenuity2.Devices
{
	// Token: 0x0200065B RID: 1627
	public enum OnboardTriggerEffect
	{
		// Token: 0x04001E0E RID: 7694
		None,
		// Token: 0x04001E0F RID: 7695
		Fade,
		// Token: 0x04001E10 RID: 7696
		Explosion,
		// Token: 0x04001E11 RID: 7697
		HyperXFlame
	}
}

using System;

namespace NGenuity2.Devices
{
	// Token: 0x02000660 RID: 1632
	public enum PairingStates
	{
		// Token: 0x04001E3E RID: 7742
		Start,
		// Token: 0x04001E3F RID: 7743
		Processing,
		// Token: 0x04001E40 RID: 7744
		Success,
		// Token: 0x04001E41 RID: 7745
		Fail
	}
}

using System;

namespace NGenuity2.Devices
{
	// Token: 0x02000649 RID: 1609
	public enum PolarPattern
	{
		// Token: 0x04001D25 RID: 7461
		Bidirectional,
		// Token: 0x04001D26 RID: 7462
		Cardioid,
		// Token: 0x04001D27 RID: 7463
		Omnidirectional,
		// Token: 0x04001D28 RID: 7464
		Stereo
	}
}

using System;

namespace NGenuity2.Devices
{
	// Token: 0x02000653 RID: 1619
	public class SpeakerAngle
	{
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06002097 RID: 8343 RVA: 0x000596F8 File Offset: 0x000578F8
		// (set) Token: 0x06002098 RID: 8344 RVA: 0x00059700 File Offset: 0x00057900
		public int LeftSpeaker { get; set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06002099 RID: 8345 RVA: 0x00059709 File Offset: 0x00057909
		// (set) Token: 0x0600209A RID: 8346 RVA: 0x00059711 File Offset: 0x00057911
		public int RightSpeaker { get; set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600209B RID: 8347 RVA: 0x0005971A File Offset: 0x0005791A
		// (set) Token: 0x0600209C RID: 8348 RVA: 0x00059722 File Offset: 0x00057922
		public int RearLeftSpeaker { get; set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600209D RID: 8349 RVA: 0x0005972B File Offset: 0x0005792B
		// (set) Token: 0x0600209E RID: 8350 RVA: 0x00059733 File Offset: 0x00057933
		public int RearRightSpeaker { get; set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600209F RID: 8351 RVA: 0x0005973C File Offset: 0x0005793C
		// (set) Token: 0x060020A0 RID: 8352 RVA: 0x00059744 File Offset: 0x00057944
		public int SurroundLeftSpeaker { get; set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060020A1 RID: 8353 RVA: 0x0005974D File Offset: 0x0005794D
		// (set) Token: 0x060020A2 RID: 8354 RVA: 0x00059755 File Offset: 0x00057955
		public int SurroundRightSpeaker { get; set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060020A3 RID: 8355 RVA: 0x0005975E File Offset: 0x0005795E
		// (set) Token: 0x060020A4 RID: 8356 RVA: 0x00059766 File Offset: 0x00057966
		public int CenterSpeaker { get; set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060020A5 RID: 8357 RVA: 0x0005976F File Offset: 0x0005796F
		// (set) Token: 0x060020A6 RID: 8358 RVA: 0x00059777 File Offset: 0x00057977
		public int BiasSpeaker { get; set; }

		// Token: 0x060020A7 RID: 8359 RVA: 0x00059780 File Offset: 0x00057980
		public SpeakerAngle(int rightSpeaker, int leftSpeaker, int surroundRightSpeaker, int surroundLeftSpeaker, int rearRightSpeaker, int rearLeftSpeaker, int centerSpeaker, int biasSpeaker)
		{
			this.LeftSpeaker = rightSpeaker;
			this.RightSpeaker = leftSpeaker;
			this.RearLeftSpeaker = rearRightSpeaker;
			this.RearRightSpeaker = rearLeftSpeaker;
			this.SurroundLeftSpeaker = surroundRightSpeaker;
			this.SurroundRightSpeaker = surroundLeftSpeaker;
			this.CenterSpeaker = centerSpeaker;
			this.BiasSpeaker = biasSpeaker;
		}
	}
}

using System;

namespace NGenuity2.Devices
{
	// Token: 0x02000654 RID: 1620
	public enum UpdateFirmwareState
	{
		// Token: 0x04001DE6 RID: 7654
		Normal,
		// Token: 0x04001DE7 RID: 7655
		EnterBootloader,
		// Token: 0x04001DE8 RID: 7656
		Updating,
		// Token: 0x04001DE9 RID: 7657
		ExitBootloader,
		// Token: 0x04001DEA RID: 7658
		PlugAdapter = 160,
		// Token: 0x04001DEB RID: 7659
		PlugProduct = 176,
		// Token: 0x04001DEC RID: 7660
		PlugCase = 128,
		// Token: 0x04001DED RID: 7661
		UpdatingCase = 144,
		// Token: 0x04001DEE RID: 7662
		PlugProductWithPic = 177,
		// Token: 0x04001DEF RID: 7663
		UpdatingAdapter = 192,
		// Token: 0x04001DF0 RID: 7664
		OnlyUpdateAdapter,
		// Token: 0x04001DF1 RID: 7665
		UpdatingProduct = 208,
		// Token: 0x04001DF2 RID: 7666
		OnlyUpdateProduct = 225,
		// Token: 0x04001DF3 RID: 7667
		ReplugAdapter = 224,
		// Token: 0x04001DF4 RID: 7668
		Reconnecting = 226,
		// Token: 0x04001DF5 RID: 7669
		AdditionalInformation,
		// Token: 0x04001DF6 RID: 7670
		Failure = 254,
		// Token: 0x04001DF7 RID: 7671
		SuccessAndReboot = 253,
		// Token: 0x04001DF8 RID: 7672
		Success = 255
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using NGenuity2.Common;
using NGenuity2.Devices.Mouse;
using NGenuity2.Updaters;
using NGenuity2.Updaters.Universals;

namespace NGenuity2.Devices
{
	// Token: 0x0200066A RID: 1642
	public sealed class Updater
	{
		// Token: 0x14000039 RID: 57
		// (add) Token: 0x06002179 RID: 8569 RVA: 0x00060248 File Offset: 0x0005E448
		// (remove) Token: 0x0600217A RID: 8570 RVA: 0x00060280 File Offset: 0x0005E480
		public event FirmwareProgressHandler Updating;

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600217C RID: 8572 RVA: 0x000602B5 File Offset: 0x0005E4B5
		// (set) Token: 0x0600217D RID: 8573 RVA: 0x000602BD File Offset: 0x0005E4BD
		public HyperXDevice Device { get; set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600217E RID: 8574 RVA: 0x000602C6 File Offset: 0x0005E4C6
		// (set) Token: 0x0600217F RID: 8575 RVA: 0x000602CE File Offset: 0x0005E4CE
		public bool UpgradeMode { get; set; }

		// Token: 0x06002180 RID: 8576 RVA: 0x000602D7 File Offset: 0x0005E4D7
		public void OnUpdating(HyperXDeviceModel updateModel, UpdateFirmwareState state, int progress)
		{
			this.Device_Updating(updateModel, state, progress);
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x000602E4 File Offset: 0x0005E4E4
		public void UpgradeUniversalDevice()
		{
			try
			{
				ValueTuple<bool, int> valueTuple = new UniversalDeviceUpdater().Update(this.Device);
				if (!valueTuple.Item1)
				{
					this.OnUpdating(this.updateModel, UpdateFirmwareState.Failure, valueTuple.Item2);
				}
			}
			catch (Exception info)
			{
				Logger.WriteLine(AppDomain.CurrentDomain.BaseDirectory ?? "", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\Updater.cs", 49);
				Logger.WriteLine(info, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\Updater.cs", 50);
				this.OnUpdating(this.updateModel, UpdateFirmwareState.Failure, 0);
			}
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x00060374 File Offset: 0x0005E574
		public bool DoUpgrade()
		{
			this.UpgradeMode = true;
			string requiredVersions = this.Device.RequiredVersions;
			HyperXCenter.Center.Updater.UpgradeMode = true;
			this.updateModel = this.Device.Model;
			List<string> list = new List<string>();
			if (this.updateModel == HyperXDeviceModel.MousePulsefireDart)
			{
				using (List<HyperXDevice>.Enumerator enumerator = (from d in HyperXCenter.Center.Devices
				where d.PairID == this.Device.PairID && d.Model == HyperXDeviceModel.MousePulsefireDart
				select d).ToList<HyperXDevice>().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						HyperXDevice hyperXDevice = enumerator.Current;
						hyperXDevice.RetriveUpdateDeviceInfo(list);
						hyperXDevice.CloseDevice();
						HyperXCenter.Center.RemoveDevice(hyperXDevice);
					}
					goto IL_D0;
				}
			}
			this.Device.RetriveUpdateDeviceInfo(list);
			this.Device.CloseDevice();
			HyperXCenter.Center.RemoveDevice(this.Device);
			IL_D0:
			bool result = false;
			try
			{
				FirmwareUpdaterHelper firmwareUpdaterHelper = new FirmwareUpdaterHelper();
				HyperXDeviceModel hyperXDeviceModel = this.updateModel;
				if (hyperXDeviceModel != HyperXDeviceModel.MousePulsefireHasteWireless)
				{
					if (hyperXDeviceModel != HyperXDeviceModel.Headset_Ralphie)
					{
						goto IL_187;
					}
				}
				else
				{
					MousePulsefireHasteWireless mousePulsefireHasteWireless = this.Device as MousePulsefireHasteWireless;
					object updateCheckLock = mousePulsefireHasteWireless.UpdateCheckLock;
					lock (updateCheckLock)
					{
						PulsefireHasteWirelessUpdater.UpdateTarget updateTarget = mousePulsefireHasteWireless.UpdateTarget;
						Logger.WriteLine(string.Format("updateTarget: {0}", updateTarget), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\Updater.cs", 99);
						result = firmwareUpdaterHelper.UpdateHasteWireless(updateTarget);
						goto IL_187;
					}
				}
				HyperXHeadsetDevice hyperXHeadsetDevice = this.Device as HyperXHeadsetDevice;
				if (hyperXHeadsetDevice != null)
				{
					if (hyperXHeadsetDevice.RemoteDeviceModel == HyperXRemoteDeviceModel.Unknown)
					{
						result = firmwareUpdaterHelper.UpdateRalphieModule(list);
					}
					else
					{
						result = firmwareUpdaterHelper.UpdateRalphieHeadset(hyperXHeadsetDevice.RemoteDeviceModel, list);
					}
				}
				IL_187:
				if (this.updateModel != HyperXDeviceModel.MousePulsefireHasteWireless)
				{
					result = firmwareUpdaterHelper.UpdateFirmware(this.updateModel, list, requiredVersions);
				}
			}
			catch (Exception ex)
			{
				Logger.WriteLine(AppDomain.CurrentDomain.BaseDirectory ?? "", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\Updater.cs", 130);
				Logger.WriteLine(ex, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\Updater.cs", 131);
				this.OnUpdating(this.updateModel, UpdateFirmwareState.Failure, 0);
				Logger.WriteLine("Update Firmware Failed: " + ex.Message, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\Updater.cs", 133);
			}
			this.UpgradeMode = false;
			HyperXCenter.Center.ScanUnloadedDevices();
			return result;
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x000605C8 File Offset: 0x0005E7C8
		private void Device_Updating(HyperXDeviceModel model, UpdateFirmwareState state, int progress)
		{
			FirmwareProgressHandler updating = this.Updating;
			if (updating == null)
			{
				return;
			}
			updating(model, state, progress);
		}

		// Token: 0x04001E43 RID: 7747
		private HyperXDeviceModel updateModel;

		// Token: 0x04001E46 RID: 7750
		private bool dialogIsPopup;
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using MediaFoundation;
using MediaFoundation.Misc;
using NGenuity2.Common;
using NGenuity2.Devices.UVC;
using NGenuity2.Devices.UVC.DirectShow;
using NGenuity2.Effects;
using NGenuity2.Model;
using NGenuity2.Webcam;

namespace NGenuity2.Devices
{
	// Token: 0x02000652 RID: 1618
	public abstract class UVCDeviceBase : HyperXDevice
	{
		// Token: 0x06002077 RID: 8311 RVA: 0x00058F13 File Offset: 0x00057113
		public override void Serialize(BinaryWriter bw)
		{
			base.Serialize(bw);
			this.WebcamConfig.Serialize(bw);
			this.CaptureAdvancedConfig.Serialize(bw);
			this.DefaultVideoFormat.Serialize(bw);
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x00058F40 File Offset: 0x00057140
		public override void Deserialize(BinaryReader br, int version)
		{
			base.Deserialize(br, version);
			this.WebcamConfig.Deserialize(br, version);
			this.CaptureAdvancedConfig.Deserialize(br, version);
			WebcamModel.VideoFormat defaultVideoFormat = this.DefaultVideoFormat;
			if (defaultVideoFormat == null)
			{
				return;
			}
			defaultVideoFormat.Deserialize(br, version);
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06002079 RID: 8313 RVA: 0x00058F76 File Offset: 0x00057176
		// (set) Token: 0x0600207A RID: 8314 RVA: 0x00058F7E File Offset: 0x0005717E
		public int CamID { get; private set; }

		// Token: 0x0600207B RID: 8315
		protected abstract WebcamModel.CameraControlConfig CreateCameraSetting();

		// Token: 0x0600207C RID: 8316
		protected abstract WebcamModel.VideoControlConfig CreateVideoSetting();

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600207D RID: 8317 RVA: 0x00058F87 File Offset: 0x00057187
		// (set) Token: 0x0600207E RID: 8318 RVA: 0x00058F8F File Offset: 0x0005718F
		private protected WebcamModel.WebcamConfig WebcamConfig { protected get; private set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600207F RID: 8319 RVA: 0x00058F98 File Offset: 0x00057198
		// (set) Token: 0x06002080 RID: 8320 RVA: 0x00058FA0 File Offset: 0x000571A0
		private protected CaptureAdvanceModel.CaptureAdvancedConfig CaptureAdvancedConfig { protected get; private set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06002081 RID: 8321
		public abstract WebcamModel.VideoFormat DefaultVideoFormat { get; }

		// Token: 0x06002082 RID: 8322
		protected abstract string GetFriendlyName();

		// Token: 0x06002083 RID: 8323 RVA: 0x0003557B File Offset: 0x0003377B
		public override EffectImplBase CreateEffect(EffectItemBase item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x0003557B File Offset: 0x0003377B
		public override void SetAllLEDOff()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x0003557B File Offset: 0x0003377B
		public override void SetLightings(IList<KeyMap> keys)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x00058FA9 File Offset: 0x000571A9
		protected override void InitDevice()
		{
			base.InitDevice();
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x00058FB4 File Offset: 0x000571B4
		public UVCDeviceBase(string symbolicLink)
		{
			base.CanLink = false;
			ushort num;
			ushort num2;
			NGenuity2.Common.Utils.ParseVIDPID(symbolicLink, out num, out num2);
			base.VendorID = num;
			base.ProductID = num2;
			base.DeviceID = symbolicLink;
			base.DeviceType = HyperXDeviceType.Webcam;
			this.CamID = WebcamHelper.GetCamIDDS(symbolicLink);
			if (this.CamID < 0)
			{
				throw new Exception("Cannot get cam ID.");
			}
			NGenuity2.Devices.UVC.Utils.GetDeviceMediaSource(string.Format("\\\\?\\USB#VID_{0:x4}&PID_{1:x4}", num, num2).ToUpperInvariant(), out this._imfSource);
			if (this._imfSource == null)
			{
				throw new ArgumentException("_imfSource == null");
			}
			this._imc = (this._imfSource as Win32API.IAMCameraControl);
			this._imv = (this._imfSource as Win32API.IAMVideoProcAmp);
			List<WebcamModel.VideoFormat> supportedVideoFormats;
			if (!COMBase.Succeeded(NGenuity2.Devices.UVC.Utils.EnumerateCaptureFormats(this._imfSource, out supportedVideoFormats, true)))
			{
				COMBase.SafeRelease(this._imfSource);
				Logger.WriteLine("MFCreateAttributes error", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\UVC\\UVCDeviceBase.cs", 96);
				return;
			}
			this.WebcamConfig = new WebcamModel.WebcamConfig(this.DefaultVideoFormat, supportedVideoFormats, this.CreateCameraSetting(), this.CreateVideoSetting());
			this.CaptureAdvancedConfig = new CaptureAdvanceModel.CaptureAdvancedConfig();
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x000590D0 File Offset: 0x000572D0
		protected bool GetCameraCurrenValue(Win32API.CameraControlProperty property, out int value, out bool isAuto)
		{
			value = 0;
			isAuto = false;
			int num;
			Win32API.CameraControlFlags cameraControlFlags;
			if (!NGenuity2.Devices.UVC.Utils.CheckHResult(this._imc.Get(property, out num, out cameraControlFlags), "_imc.Get"))
			{
				return false;
			}
			value = num;
			isAuto = (cameraControlFlags == Win32API.CameraControlFlags.Auto);
			return true;
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x0005910C File Offset: 0x0005730C
		protected WebcamModel.MetaData GetCameraMetaData(Win32API.CameraControlProperty property)
		{
			int min;
			int max;
			int step;
			int @default;
			Win32API.CameraControlFlags cameraControlFlags;
			if (!NGenuity2.Devices.UVC.Utils.CheckHResult(this._imc.GetRange(property, out min, out max, out step, out @default, out cameraControlFlags), "_imc.GetRange"))
			{
				return null;
			}
			return new WebcamModel.MetaData(min, max, step, @default, cameraControlFlags == (Win32API.CameraControlFlags)(Convert.ToInt32(Win32API.CameraControlFlags.Auto) | Convert.ToInt32(Win32API.CameraControlFlags.Manual)));
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x00059164 File Offset: 0x00057364
		protected bool GetVideoCurrenValue(Win32API.VideoProcAmpProperty property, out int value, out bool isAuto)
		{
			value = 0;
			isAuto = false;
			int num;
			Win32API.VideoProcAmpFlags videoProcAmpFlags;
			if (!NGenuity2.Devices.UVC.Utils.CheckHResult(this._imv.Get(property, out num, out videoProcAmpFlags), "_imv.Get"))
			{
				return false;
			}
			value = num;
			isAuto = (videoProcAmpFlags == Win32API.VideoProcAmpFlags.Auto);
			return true;
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x000591A0 File Offset: 0x000573A0
		protected WebcamModel.MetaData GetVideoMetaData(Win32API.VideoProcAmpProperty property)
		{
			int min;
			int max;
			int step;
			int @default;
			Win32API.VideoProcAmpFlags videoProcAmpFlags;
			if (!NGenuity2.Devices.UVC.Utils.CheckHResult(this._imv.GetRange(property, out min, out max, out step, out @default, out videoProcAmpFlags), "_imv.GetRange"))
			{
				return null;
			}
			return new WebcamModel.MetaData(min, max, step, @default, videoProcAmpFlags == (Win32API.VideoProcAmpFlags)(Convert.ToInt32(Win32API.VideoProcAmpFlags.Auto) | Convert.ToInt32(Win32API.VideoProcAmpFlags.Manual)));
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x000591F8 File Offset: 0x000573F8
		public bool SetValue(Win32API.CameraControlProperty property, int value)
		{
			HResult hResult = this._imc.Set(property, value, Win32API.CameraControlFlags.Manual);
			this.WebcamConfig.CameraControlConfig.Properties.FirstOrDefault((WebcamModel.CameraProperty x) => x.Type == property).Current = value;
			return NGenuity2.Devices.UVC.Utils.CheckHResult(hResult, "SetValue CameraControlProperty");
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x00059258 File Offset: 0x00057458
		public bool SetFlagBoolean(Win32API.CameraControlProperty property, bool auto)
		{
			HResult hResult;
			if (auto)
			{
				hResult = this._imc.Set(property, 1, Win32API.CameraControlFlags.Auto);
			}
			else
			{
				hResult = this._imc.Set(property, 1, Win32API.CameraControlFlags.Manual);
			}
			this.WebcamConfig.CameraControlConfig.Properties.FirstOrDefault((WebcamModel.CameraProperty x) => x.Type == property).IsAuto = auto;
			return NGenuity2.Devices.UVC.Utils.CheckHResult(hResult, "SetAuto CameraControlProperty");
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x000592D4 File Offset: 0x000574D4
		public bool SetValue(Win32API.VideoProcAmpProperty property, int value)
		{
			HResult hResult = this._imv.Set(property, value, Win32API.VideoProcAmpFlags.Manual);
			this.WebcamConfig.VideoControlConfig.Properties.FirstOrDefault((WebcamModel.VideoProperty x) => x.Type == property).Current = value;
			return NGenuity2.Devices.UVC.Utils.CheckHResult(hResult, "SetValue VideoProcAmpProperty");
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x00059334 File Offset: 0x00057534
		public bool SetFlagBoolean(Win32API.VideoProcAmpProperty property, bool auto)
		{
			HResult hResult;
			if (auto)
			{
				hResult = this._imv.Set(property, 1, Win32API.VideoProcAmpFlags.Auto);
			}
			else
			{
				hResult = this._imv.Set(property, 1, Win32API.VideoProcAmpFlags.Manual);
			}
			this.WebcamConfig.VideoControlConfig.Properties.FirstOrDefault((WebcamModel.VideoProperty x) => x.Type == property).IsAuto = auto;
			return NGenuity2.Devices.UVC.Utils.CheckHResult(hResult, "SetAuto VideoProcAmpProperty");
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x000593B0 File Offset: 0x000575B0
		public virtual bool SetAdvanceBoolean(CaptureAdvanceModel.CommandType commandType, bool isOn)
		{
			if (commandType != CaptureAdvanceModel.CommandType.AutoFraming)
			{
				if (commandType != CaptureAdvanceModel.CommandType.Flip)
				{
					if (commandType != CaptureAdvanceModel.CommandType.View)
					{
						throw new NotImplementedException();
					}
					this.CaptureAdvancedConfig.View = isOn;
					return true;
				}
				else
				{
					this.CaptureAdvancedConfig.Flip = isOn;
				}
			}
			else
			{
				this.CaptureAdvancedConfig.HPPVSDKConfig.AutoFrame = isOn;
			}
			return WebcamHelper.Instance.SetBoolean(commandType, isOn);
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x0005940C File Offset: 0x0005760C
		public virtual bool SetAdvanceValue<T>(CaptureAdvanceModel.CommandType commandType, T val)
		{
			if (commandType != CaptureAdvanceModel.CommandType.AutoFraming)
			{
				if (commandType != CaptureAdvanceModel.CommandType.View)
				{
					throw new NotImplementedException();
				}
				if (val is int)
				{
					int num = val as int;
					if (!NGenuity2.Devices.UVC.Utils.CheckHResult(this._imc.Set(Win32API.CameraControlProperty.Zoom, num, Win32API.CameraControlFlags.Manual), "SetHPPVSDKValue CameraControlProperty.Zoom"))
					{
						return false;
					}
					this.CaptureAdvancedConfig.ViewVal = num;
					return true;
				}
			}
			else if (val is CaptureAdvanceModel.AUTOFRAMING_PART_BODY)
			{
				CaptureAdvanceModel.AUTOFRAMING_PART_BODY partBody = val as CaptureAdvanceModel.AUTOFRAMING_PART_BODY;
				this.CaptureAdvancedConfig.HPPVSDKConfig.PartBody = partBody;
			}
			return WebcamHelper.Instance.SetValue<T>(commandType, val);
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x000594AD File Offset: 0x000576AD
		public bool SetResolution(WebcamModel.VideoFormat videoFormat)
		{
			this.WebcamConfig.CurrentVideoFormat = videoFormat;
			return true;
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x000594BC File Offset: 0x000576BC
		public void Dispose()
		{
			if (this._imv != null)
			{
				Marshal.ReleaseComObject(this._imv);
				this._imv = null;
			}
			if (this._imc != null)
			{
				Marshal.ReleaseComObject(this._imc);
				this._imc = null;
			}
			if (this._imfSource != null)
			{
				Marshal.ReleaseComObject(this._imfSource);
				this._imfSource = null;
			}
			WebcamHelper.Instance.StopHPPVSDK();
		}

		// Token: 0x06002094 RID: 8340 RVA: 0x00059525 File Offset: 0x00057725
		public override void PreStop()
		{
			this.Dispose();
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x00059530 File Offset: 0x00057730
		public override void ResetToDefault()
		{
			foreach (WebcamModel.CameraProperty cameraProperty in this.WebcamConfig.CameraControlConfig.Properties)
			{
				this.SetValue(cameraProperty.Type, cameraProperty.Data.Default);
				if (cameraProperty.Data.Automatable)
				{
					this.SetFlagBoolean(cameraProperty.Type, true);
				}
			}
			foreach (WebcamModel.VideoProperty videoProperty in this.WebcamConfig.VideoControlConfig.Properties)
			{
				this.SetValue(videoProperty.Type, videoProperty.Data.Default);
				if (videoProperty.Data.Automatable)
				{
					this.SetFlagBoolean(videoProperty.Type, true);
				}
			}
			this.CaptureAdvancedConfig.HPPVSDKConfig.AutoFrame = false;
			this.CaptureAdvancedConfig.HPPVSDKConfig.PartBody = CaptureAdvanceModel.AUTOFRAMING_PART_BODY.PART_NONE;
			this.SetAdvanceBoolean(CaptureAdvanceModel.CommandType.AutoFraming, this.CaptureAdvancedConfig.HPPVSDKConfig.AutoFrame);
			this.CaptureAdvancedConfig.HPPVSDKConfig.BackgroundRender = false;
			this.CaptureAdvancedConfig.HPPVSDKConfig.BackgroundType = CaptureAdvanceModel.BACKGROUD_TRANSFORM_TYPE.BACKGROUD_NONE;
			this.CaptureAdvancedConfig.Flip = false;
			this.SetAdvanceBoolean(CaptureAdvanceModel.CommandType.Flip, this.CaptureAdvancedConfig.Flip);
			this.WebcamConfig.CurrentVideoFormat = this.DefaultVideoFormat;
			this.SetResolution(this.WebcamConfig.CurrentVideoFormat);
			this.WebcamConfig.IsAdjustmentCheck = false;
			base.ResetToDefault();
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x000596E4 File Offset: 0x000578E4
		public void SetWebcomfigVal(Common type, int val)
		{
			if (type == Common.AdjustmentCheck)
			{
				this.WebcamConfig.IsAdjustmentCheck = (val == 1);
			}
		}

		// Token: 0x04001DD7 RID: 7639
		private IMFMediaSource _imfSource;

		// Token: 0x04001DD8 RID: 7640
		protected Win32API.IAMVideoProcAmp _imv;

		// Token: 0x04001DD9 RID: 7641
		protected Win32API.IAMCameraControl _imc;
	}
}

using System;

namespace NGenuity2.Devices
{
	// Token: 0x02000656 RID: 1622
	public class VirtualSurroundProfile
	{
		// Token: 0x060020AC RID: 8364 RVA: 0x000597D0 File Offset: 0x000579D0
		public VirtualSurroundProfile(decimal rightSpeaker, decimal leftSpeaker, decimal rearRightSpeaker, decimal rearLeftSpeaker, decimal surroundRightSpeaker, decimal surroundLeftSpeaker, decimal centerSpeaker, decimal biasSpeaker, decimal zoom)
		{
			this.LeftSpeaker = leftSpeaker;
			this.RightSpeaker = rightSpeaker;
			this.RearLeftSpeaker = rearLeftSpeaker;
			this.RearRightSpeaker = rearRightSpeaker;
			this.SurroundLeftSpeaker = surroundLeftSpeaker;
			this.SurroundRightSpeaker = surroundRightSpeaker;
			this.CenterSpeaker = centerSpeaker;
			this.BiasSpeaker = biasSpeaker;
			this.Zoom = zoom;
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060020AD RID: 8365 RVA: 0x00059828 File Offset: 0x00057A28
		// (set) Token: 0x060020AE RID: 8366 RVA: 0x00059830 File Offset: 0x00057A30
		public decimal LeftSpeaker { get; set; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060020AF RID: 8367 RVA: 0x00059839 File Offset: 0x00057A39
		// (set) Token: 0x060020B0 RID: 8368 RVA: 0x00059841 File Offset: 0x00057A41
		public decimal RightSpeaker { get; set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060020B1 RID: 8369 RVA: 0x0005984A File Offset: 0x00057A4A
		// (set) Token: 0x060020B2 RID: 8370 RVA: 0x00059852 File Offset: 0x00057A52
		public decimal RearLeftSpeaker { get; set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060020B3 RID: 8371 RVA: 0x0005985B File Offset: 0x00057A5B
		// (set) Token: 0x060020B4 RID: 8372 RVA: 0x00059863 File Offset: 0x00057A63
		public decimal RearRightSpeaker { get; set; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060020B5 RID: 8373 RVA: 0x0005986C File Offset: 0x00057A6C
		// (set) Token: 0x060020B6 RID: 8374 RVA: 0x00059874 File Offset: 0x00057A74
		public decimal SurroundLeftSpeaker { get; set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060020B7 RID: 8375 RVA: 0x0005987D File Offset: 0x00057A7D
		// (set) Token: 0x060020B8 RID: 8376 RVA: 0x00059885 File Offset: 0x00057A85
		public decimal SurroundRightSpeaker { get; set; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060020B9 RID: 8377 RVA: 0x0005988E File Offset: 0x00057A8E
		// (set) Token: 0x060020BA RID: 8378 RVA: 0x00059896 File Offset: 0x00057A96
		public decimal CenterSpeaker { get; set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060020BB RID: 8379 RVA: 0x0005989F File Offset: 0x00057A9F
		// (set) Token: 0x060020BC RID: 8380 RVA: 0x000598A7 File Offset: 0x00057AA7
		public decimal BiasSpeaker { get; set; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060020BD RID: 8381 RVA: 0x000598B0 File Offset: 0x00057AB0
		// (set) Token: 0x060020BE RID: 8382 RVA: 0x000598B8 File Offset: 0x00057AB8
		public decimal Zoom { get; set; }
	}
}
```

# NGENUITY.COMMON.DEVICES
```csharp
using System;

namespace NGenuity2.Common.Devices
{
	// Token: 0x020009DA RID: 2522
	public enum ANCState
	{
		// Token: 0x040028A5 RID: 10405
		Unknown,
		// Token: 0x040028A6 RID: 10406
		On,
		// Token: 0x040028A7 RID: 10407
		Off,
		// Token: 0x040028A8 RID: 10408
		Transparency
	}
}

using System;

namespace NGenuity2.Common.Devices
{
	// Token: 0x020009D9 RID: 2521
	public enum AudioDeviceDataFlow
	{
		// Token: 0x040028A2 RID: 10402
		Render,
		// Token: 0x040028A3 RID: 10403
		Capture
	}
}

using System;

namespace NGenuity2.Common.Devices
{
	// Token: 0x020009DB RID: 2523
	public enum ColorSkins
	{
		// Token: 0x040028AA RID: 10410
		BlackBlack,
		// Token: 0x040028AB RID: 10411
		WhiteWhite,
		// Token: 0x040028AC RID: 10412
		BlackRed,
		// Token: 0x040028AD RID: 10413
		BlackBlue,
		// Token: 0x040028AE RID: 10414
		BlackGreen,
		// Token: 0x040028AF RID: 10415
		WhitePink,
		// Token: 0x040028B0 RID: 10416
		WhiteBlue,
		// Token: 0x040028B1 RID: 10417
		WhiteGreen,
		// Token: 0x040028B2 RID: 10418
		GrayGreen,
		// Token: 0x040028B3 RID: 10419
		BlackWhite,
		// Token: 0x040028B4 RID: 10420
		SilverBlack,
		// Token: 0x040028B5 RID: 10421
		BlueBlack,
		// Token: 0x040028B6 RID: 10422
		GrayGray = 13,
		// Token: 0x040028B7 RID: 10423
		TTT = 16,
		// Token: 0x040028B8 RID: 10424
		AimLabs,
		// Token: 0x040028B9 RID: 10425
		Naruto,
		// Token: 0x040028BA RID: 10426
		Itachi,
		// Token: 0x040028BB RID: 10427
		Riot
	}
}

using System;

namespace NGenuity2.Common.Devices
{
	// Token: 0x020009DC RID: 2524
	public enum CountryLayouts
	{
		// Token: 0x040028BD RID: 10429
		KEY_104_US,
		// Token: 0x040028BE RID: 10430
		KEY_104_RU,
		// Token: 0x040028BF RID: 10431
		KEY_105_UK,
		// Token: 0x040028C0 RID: 10432
		KEY_105_LA,
		// Token: 0x040028C1 RID: 10433
		KEY_105_DE,
		// Token: 0x040028C2 RID: 10434
		KEY_105_NO,
		// Token: 0x040028C3 RID: 10435
		KEY_105_FR,
		// Token: 0x040028C4 RID: 10436
		KEY_106_BR,
		// Token: 0x040028C5 RID: 10437
		KEY_108_JP,
		// Token: 0x040028C6 RID: 10438
		KEY_87_US,
		// Token: 0x040028C7 RID: 10439
		KEY_87_RU,
		// Token: 0x040028C8 RID: 10440
		KEY_88_UK,
		// Token: 0x040028C9 RID: 10441
		KEY_88_LA,
		// Token: 0x040028CA RID: 10442
		KEY_88_DE,
		// Token: 0x040028CB RID: 10443
		KEY_88_NO,
		// Token: 0x040028CC RID: 10444
		KEY_88_FR,
		// Token: 0x040028CD RID: 10445
		KEY_89_BR,
		// Token: 0x040028CE RID: 10446
		KEY_91_JP,
		// Token: 0x040028CF RID: 10447
		KEY_104_KR,
		// Token: 0x040028D0 RID: 10448
		KEY_105_SWISS,
		// Token: 0x040028D1 RID: 10449
		KEY_105_TU,
		// Token: 0x040028D2 RID: 10450
		KEY_87_KR,
		// Token: 0x040028D3 RID: 10451
		KEY_88_SWISS,
		// Token: 0x040028D4 RID: 10452
		KEY_88_TU,
		// Token: 0x040028D5 RID: 10453
		KEY_104_TW,
		// Token: 0x040028D6 RID: 10454
		KEY_87_TW,
		// Token: 0x040028D7 RID: 10455
		KEY_104_TU,
		// Token: 0x040028D8 RID: 10456
		KEY_104_TH,
		// Token: 0x040028D9 RID: 10457
		KEY_104_AE,
		// Token: 0x040028DA RID: 10458
		KEY_105_AE,
		// Token: 0x040028DB RID: 10459
		KEY_105_IT,
		// Token: 0x040028DC RID: 10460
		KEY_105_ES,
		// Token: 0x040028DD RID: 10461
		KEY_87_TU,
		// Token: 0x040028DE RID: 10462
		KEY_87_TH,
		// Token: 0x040028DF RID: 10463
		KEY_87_AE,
		// Token: 0x040028E0 RID: 10464
		KEY_88_AE,
		// Token: 0x040028E1 RID: 10465
		KEY_88_IT,
		// Token: 0x040028E2 RID: 10466
		KEY_88_ES,
		// Token: 0x040028E3 RID: 10467
		KEY_61_US,
		// Token: 0x040028E4 RID: 10468
		KEY_61_RU,
		// Token: 0x040028E5 RID: 10469
		KEY_62_UK,
		// Token: 0x040028E6 RID: 10470
		KEY_62_LA,
		// Token: 0x040028E7 RID: 10471
		KEY_62_DE,
		// Token: 0x040028E8 RID: 10472
		KEY_62_NO,
		// Token: 0x040028E9 RID: 10473
		KEY_62_FR,
		// Token: 0x040028EA RID: 10474
		KEY_63_BR,
		// Token: 0x040028EB RID: 10475
		KEY_65_JP,
		// Token: 0x040028EC RID: 10476
		KEY_61_KR,
		// Token: 0x040028ED RID: 10477
		KEY_62_SWISS,
		// Token: 0x040028EE RID: 10478
		KEY_62_TU,
		// Token: 0x040028EF RID: 10479
		KEY_61_TW,
		// Token: 0x040028F0 RID: 10480
		KEY_61_TU,
		// Token: 0x040028F1 RID: 10481
		KEY_61_TH,
		// Token: 0x040028F2 RID: 10482
		KEY_61_AE,
		// Token: 0x040028F3 RID: 10483
		KEY_62_AE,
		// Token: 0x040028F4 RID: 10484
		KEY_62_IT,
		// Token: 0x040028F5 RID: 10485
		KEY_62_ES,
		// Token: 0x040028F6 RID: 10486
		KEY_67_US,
		// Token: 0x040028F7 RID: 10487
		KEY_67_RU,
		// Token: 0x040028F8 RID: 10488
		KEY_68_UK,
		// Token: 0x040028F9 RID: 10489
		KEY_68_LA,
		// Token: 0x040028FA RID: 10490
		KEY_68_DE,
		// Token: 0x040028FB RID: 10491
		KEY_68_NO,
		// Token: 0x040028FC RID: 10492
		KEY_68_FR,
		// Token: 0x040028FD RID: 10493
		KEY_69_BR,
		// Token: 0x040028FE RID: 10494
		KEY_71_JP,
		// Token: 0x040028FF RID: 10495
		KEY_67_KR,
		// Token: 0x04002900 RID: 10496
		KEY_68_SWISS,
		// Token: 0x04002901 RID: 10497
		KEY_68_TU,
		// Token: 0x04002902 RID: 10498
		KEY_67_TW,
		// Token: 0x04002903 RID: 10499
		KEY_67_TU,
		// Token: 0x04002904 RID: 10500
		KEY_67_TH,
		// Token: 0x04002905 RID: 10501
		KEY_67_AE,
		// Token: 0x04002906 RID: 10502
		KEY_68_AE,
		// Token: 0x04002907 RID: 10503
		KEY_68_IT,
		// Token: 0x04002908 RID: 10504
		KEY_68_ES,
		// Token: 0x04002909 RID: 10505
		UNDEFINED = 255
	}
}

using System;

namespace NGenuity2.Common.Devices
{
	// Token: 0x020009DD RID: 2525
	public enum DriverInstallationState
	{
		// Token: 0x0400290B RID: 10507
		Installed,
		// Token: 0x0400290C RID: 10508
		NotInstalled,
		// Token: 0x0400290D RID: 10509
		NeedRegisterAPO
	}
}

using System;
using System.IO;

namespace NGenuity2.Common.Devices
{
	// Token: 0x020009DE RID: 2526
	public static class ExtraPropertyUtils
	{
		// Token: 0x06003BE9 RID: 15337 RVA: 0x000F1704 File Offset: 0x000EF904
		public static long ExtraPropertyValue(this bool val)
		{
			return (val > false) ? 1L : 0L;
		}

		// Token: 0x06003BEA RID: 15338 RVA: 0x000F170C File Offset: 0x000EF90C
		public static long ExtraPropertyValue(this float val)
		{
			long result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					binaryWriter.Write(val);
				}
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					result = (long)((ulong)binaryReader.ReadUInt32());
				}
			}
			return result;
		}

		// Token: 0x06003BEB RID: 15339 RVA: 0x000F1788 File Offset: 0x000EF988
		public static long ExtraPropertyValue(this double val)
		{
			long result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					binaryWriter.Write(val);
				}
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					result = binaryReader.ReadInt64();
				}
			}
			return result;
		}

		// Token: 0x06003BEC RID: 15340 RVA: 0x000F1804 File Offset: 0x000EFA04
		public static float Float(this long val)
		{
			uint value = (uint)(val & (long)((ulong)-1));
			float result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					binaryWriter.Write(value);
				}
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					result = binaryReader.ReadSingle();
				}
			}
			return result;
		}

		// Token: 0x06003BED RID: 15341 RVA: 0x000F1888 File Offset: 0x000EFA88
		public static double Double(this long val)
		{
			double result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					binaryWriter.Write(val);
				}
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					result = binaryReader.ReadDouble();
				}
			}
			return result;
		}

		// Token: 0x06003BEE RID: 15342 RVA: 0x000F1904 File Offset: 0x000EFB04
		public static bool Boolean(this long val)
		{
			return val != 0L;
		}
	}
}```