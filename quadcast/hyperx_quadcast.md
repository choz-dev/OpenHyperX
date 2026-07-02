# CMDMICQuadCast2CommandBase
```csharp
using System;

namespace NGenuity2.Devices.Microphone.QuadCast2
{
	// Token: 0x020007AE RID: 1966
	public abstract class CMDMICQuadCast2CommandBase : HXCommandBase
	{
	}
}```

# CMDMICQuadCast2GetDeviceInfo
```csharp
using System;

namespace NGenuity2.Devices.Microphone.QuadCast2
{
	// Token: 0x020007AF RID: 1967
	public class CMDMICQuadCast2GetDeviceInfo : CMDMICQuadCast2CommandBase
	{
		// Token: 0x06002C2A RID: 11306 RVA: 0x000B3918 File Offset: 0x000B1B18
		public override void Execute(HyperXDevice device)
		{
			byte[] array = device.CreateBuffer();
			array[0] = 119;
			array[1] = 129;
			device.SetSecondaryOutputReport(array);
		}
	}
}```

# CMDMICQuadCast2GetHighPassState
```csharp
using System;

namespace NGenuity2.Devices.Microphone.QuadCast2
{
	// Token: 0x020007B4 RID: 1972
	public class CMDMICQuadCast2GetHighPassState : CMDMICQuadCast2CommandBase
	{
		// Token: 0x06002C38 RID: 11320 RVA: 0x000B3A68 File Offset: 0x000B1C68
		public override void Execute(HyperXDevice device)
		{
			byte[] array = device.CreateBuffer();
			array[0] = 119;
			array[1] = 135;
			device.SetSecondaryOutputReport(array);
		}
	}
}```

# CMDMICQuadCast2GetMicrophoneMuteState
```csharp
using System;

namespace NGenuity2.Devices.Microphone.QuadCast2
{
	// Token: 0x020007B3 RID: 1971
	public class CMDMICQuadCast2GetMicrophoneMuteState : CMDMICQuadCast2CommandBase
	{
		// Token: 0x06002C36 RID: 11318 RVA: 0x000B3A40 File Offset: 0x000B1C40
		public override void Execute(HyperXDevice device)
		{
			byte[] array = device.CreateBuffer();
			array[0] = 119;
			array[1] = 134;
			device.SetSecondaryOutputReport(array);
		}
	}
}```

# CMDMICQuadCast2GetPolarPattern
```csharp
using System;

namespace NGenuity2.Devices.Microphone.QuadCast2
{
	// Token: 0x020007B1 RID: 1969
	public class CMDMICQuadCast2GetPolarPattern : CMDMICQuadCast2CommandBase
	{
		// Token: 0x06002C30 RID: 11312 RVA: 0x000B39D0 File Offset: 0x000B1BD0
		public override void Execute(HyperXDevice device)
		{
			byte[] array = device.CreateBuffer();
			array[0] = 119;
			array[1] = 133;
			device.SetSecondaryOutputReport(array);
		}
	}
}```

# CMDMICQuadCast2SCommandBase
```csharp
using System;
using System.Collections.Generic;
using NGenuity2.Devices.Universals.Commands;
using NGenuity2.Devices.Universals.Commons;

namespace NGenuity2.Devices.Microphone.Quadcast2S
{
	// Token: 0x020007B7 RID: 1975
	public abstract class CMDMICQuadCast2SCommandBase : UniversalCMDBase
	{
		// Token: 0x06002C43 RID: 11331 RVA: 0x000B3BC9 File Offset: 0x000B1DC9
		protected CMDMICQuadCast2SCommandBase()
		{
			base.IsUniversalCommand = false;
			base.Force = true;
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x000B3BE0 File Offset: 0x000B1DE0
		public override List<byte[]> CreateBuffers()
		{
			byte[] array = new byte[64];
			array[0] = RequestReports.Device.ToByte();
			array[1] = 1;
			return new List<byte[]>
			{
				array
			};
		}
	}
}```

# CMDMICQuadCast2SetHighPassState
```csharp
using System;

namespace NGenuity2.Devices.Microphone.QuadCast2
{
	// Token: 0x020007B5 RID: 1973
	public class CMDMICQuadCast2SetHighPassState : CMDMICQuadCast2CommandBase
	{
		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06002C3A RID: 11322 RVA: 0x000B3A90 File Offset: 0x000B1C90
		// (set) Token: 0x06002C3B RID: 11323 RVA: 0x000B3A98 File Offset: 0x000B1C98
		public bool Enable { get; set; }

		// Token: 0x06002C3C RID: 11324 RVA: 0x000B3AA4 File Offset: 0x000B1CA4
		public override void Execute(HyperXDevice device)
		{
			byte[] array = device.CreateBuffer();
			array[0] = 119;
			array[1] = 7;
			array[2] = ((this.Enable > false) ? 1 : 0);
			device.SetSecondaryOutputReport(array);
		}
	}
}```

# CMDMICQuadCast2SetMicrophoneMuteState
```csharp
using System;

namespace NGenuity2.Devices.Microphone.QuadCast2
{
	// Token: 0x020007B2 RID: 1970
	public class CMDMICQuadCast2SetMicrophoneMuteState : CMDMICQuadCast2CommandBase
	{
		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06002C32 RID: 11314 RVA: 0x000B39F8 File Offset: 0x000B1BF8
		// (set) Token: 0x06002C33 RID: 11315 RVA: 0x000B3A00 File Offset: 0x000B1C00
		public bool Muted { get; set; }

		// Token: 0x06002C34 RID: 11316 RVA: 0x000B3A0C File Offset: 0x000B1C0C
		public override void Execute(HyperXDevice device)
		{
			byte[] array = device.CreateBuffer();
			array[0] = 119;
			array[1] = 6;
			array[2] = ((this.Muted > false) ? 1 : 0);
			device.SetSecondaryOutputReport(array);
		}
	}
}```

# CMDMICQuadCast2SetPolarPattern
```csharp
using System;

namespace NGenuity2.Devices.Microphone.QuadCast2
{
	// Token: 0x020007B0 RID: 1968
	public class CMDMICQuadCast2SetPolarPattern : CMDMICQuadCast2CommandBase
	{
		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06002C2C RID: 11308 RVA: 0x000B3948 File Offset: 0x000B1B48
		// (set) Token: 0x06002C2D RID: 11309 RVA: 0x000B3950 File Offset: 0x000B1B50
		public PolarPattern? PolarPattern { get; set; }

		// Token: 0x06002C2E RID: 11310 RVA: 0x000B395C File Offset: 0x000B1B5C
		public override void Execute(HyperXDevice device)
		{
			byte[] array = device.CreateBuffer();
			array[0] = 119;
			array[1] = 5;
			if (this.PolarPattern == null)
			{
				return;
			}
			switch (this.PolarPattern.Value)
			{
			case NGenuity2.Devices.PolarPattern.Bidirectional:
				array[2] = 3;
				break;
			case NGenuity2.Devices.PolarPattern.Cardioid:
				array[2] = 0;
				break;
			case NGenuity2.Devices.PolarPattern.Omnidirectional:
				array[2] = 1;
				break;
			case NGenuity2.Devices.PolarPattern.Stereo:
				array[2] = 2;
				break;
			default:
				return;
			}
			device.SetSecondaryOutputReport(array);
		}
	}
}```

# CMDMICQuadCast2SGetBalance
```csharp
using System;

namespace NGenuity2.Devices.Microphone.Quadcast2S
{
	// Token: 0x020007C2 RID: 1986
	public class CMDMICQuadCast2SGetBalance : CMDMICQuadCast2SCommandBase
	{
		// Token: 0x06002C63 RID: 11363 RVA: 0x000B4010 File Offset: 0x000B2210
		public override void Execute(HyperXDevice device)
		{
			byte[] array = device.CreateBuffer();
			array[0] = 119;
			array[1] = 138;
			device.SetSecondaryOutputReport(array);
		}
	}
}```

# CMDMICQuadCast2SGetDeviceInfo
```csharp
using System;

namespace NGenuity2.Devices.Microphone.Quadcast2S
{
	// Token: 0x020007B8 RID: 1976
	public class CMDMICQuadCast2SGetDeviceInfo : CMDMICQuadCast2SCommandBase
	{
		// Token: 0x06002C45 RID: 11333 RVA: 0x000B3C10 File Offset: 0x000B1E10
		public override void Execute(HyperXDevice device)
		{
			byte[] array = device.CreateBuffer();
			array[0] = 119;
			array[1] = 129;
			device.SetSecondaryOutputReport(array);
		}
	}
}```

# CMDMICQuadCast2SGetHighPassState
```csharp
using System;

namespace NGenuity2.Devices.Microphone.Quadcast2S
{
	// Token: 0x020007BD RID: 1981
	public class CMDMICQuadCast2SGetHighPassState : CMDMICQuadCast2SCommandBase
	{
		// Token: 0x06002C53 RID: 11347 RVA: 0x000B3DF8 File Offset: 0x000B1FF8
		public override void Execute(HyperXDevice device)
		{
			byte[] array = device.CreateBuffer();
			array[0] = 119;
			array[1] = 135;
			device.SetSecondaryOutputReport(array);
		}
	}
}```

# CMDMICQuadCast2SGetMicrophoneGain
```csharp
using System;

namespace NGenuity2.Devices.Microphone.Quadcast2S
{
	// Token: 0x020007C0 RID: 1984
	public class CMDMICQuadCast2SGetMicrophoneGain : CMDMICQuadCast2SCommandBase
	{
		// Token: 0x06002C5D RID: 11357 RVA: 0x000B3F50 File Offset: 0x000B2150
		public override void Execute(HyperXDevice device)
		{
			byte[] array = device.CreateBuffer();
			array[0] = 119;
			array[1] = 136;
			device.SetSecondaryOutputReport(array);
		}
	}
}```

# CMDMICQuadCast2SGetMicrophoneMuteState
```csharp
using System;

namespace NGenuity2.Devices.Microphone.Quadcast2S
{
	// Token: 0x020007BC RID: 1980
	public class CMDMICQuadCast2SGetMicrophoneMuteState : CMDMICQuadCast2SCommandBase
	{
		// Token: 0x06002C51 RID: 11345 RVA: 0x000B3DD0 File Offset: 0x000B1FD0
		public override void Execute(HyperXDevice device)
		{
			byte[] array = device.CreateBuffer();
			array[0] = 119;
			array[1] = 134;
			device.SetSecondaryOutputReport(array);
		}
	}
}```

# CMDMICQuadCast2SGetPolarPattern
```csharp
using System;

namespace NGenuity2.Devices.Microphone.Quadcast2S
{
	// Token: 0x020007BA RID: 1978
	public class CMDMICQuadCast2SGetPolarPattern : CMDMICQuadCast2SCommandBase
	{
		// Token: 0x06002C4B RID: 11339 RVA: 0x000B3D14 File Offset: 0x000B1F14
		public override void Execute(HyperXDevice device)
		{
			byte[] array = device.CreateBuffer();
			array[0] = 119;
			array[1] = 133;
			device.SetSecondaryOutputReport(array);
		}
	}
}```

# CMDMICQuadCast2ShowLighting
```csharp
using System;
using System.Threading;
using NGenuity2.Common;

namespace NGenuity2.Devices.Microphone.QuadCast2
{
	// Token: 0x020007B6 RID: 1974
	public class CMDMICQuadCast2ShowLighting : CMDMICQuadCast2CommandBase
	{
		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06002C3E RID: 11326 RVA: 0x000B3AD5 File Offset: 0x000B1CD5
		// (set) Token: 0x06002C3F RID: 11327 RVA: 0x000B3ADD File Offset: 0x000B1CDD
		public Color[] Colors { get; private set; }

		// Token: 0x06002C40 RID: 11328 RVA: 0x000B3AE8 File Offset: 0x000B1CE8
		public CMDMICQuadCast2ShowLighting()
		{
			base.ProfileID = 0;
			base.Skip = false;
			this.Colors = new Color[2];
			for (int i = 0; i < this.Colors.Length; i++)
			{
				this.Colors[i] = default(Color);
			}
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x00054C08 File Offset: 0x00052E08
		protected byte[] CreateBuffer()
		{
			byte[] array = new byte[264];
			array[0] = 7;
			return array;
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x000B3B3C File Offset: 0x000B1D3C
		public override void Execute(HyperXDevice device)
		{
			byte[] array = this.CreateBuffer();
			array[1] = 4;
			array[2] = 242;
			array[9] = 1;
			device.SetFeatureReport(array);
			Thread.Sleep(2);
			Utils.ZeroBuffer(array);
			array[1] = 129;
			array[2] = this.Colors[1].R;
			array[3] = 0;
			array[4] = 0;
			array[5] = 129;
			array[6] = this.Colors[0].R;
			array[7] = 0;
			array[8] = 0;
			device.SetFeatureReport(array);
			Thread.Sleep(2);
		}
	}
}```

# CMDMICQuadCast2SSetBalance
```csharp
using System;
using NGenuity2.Common;

namespace NGenuity2.Devices.Microphone.Quadcast2S
{
	// Token: 0x020007C1 RID: 1985
	public class CMDMICQuadCast2SSetBalance : CMDMICQuadCast2SCommandBase
	{
		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06002C5F RID: 11359 RVA: 0x000B3F78 File Offset: 0x000B2178
		// (set) Token: 0x06002C60 RID: 11360 RVA: 0x000B3F80 File Offset: 0x000B2180
		public int Level { get; set; }

		// Token: 0x06002C61 RID: 11361 RVA: 0x000B3F8C File Offset: 0x000B218C
		public override void Execute(HyperXDevice device)
		{
			byte[] array = device.CreateBuffer();
			array[0] = 119;
			array[1] = 10;
			array[2] = (byte)this.Level;
			Logger.WriteLine(string.Format("Set Balance: {0}", this.Level), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\Microphone\\Quadcast2S\\CMDMICQuadcast2SCommandBase.cs", 187);
			Logger.WriteLine(string.Format("Codec -> {0:X2} {1:X2} {2:X2}", array[0], array[1], array[2]), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\Microphone\\Quadcast2S\\CMDMICQuadcast2SCommandBase.cs", 188);
			device.SetSecondaryOutputReport(array);
		}
	}
}```

# CMDMICQuadCast2SSetHighPassState
```csharp
using System;
using NGenuity2.Common;

namespace NGenuity2.Devices.Microphone.Quadcast2S
{
	// Token: 0x020007BE RID: 1982
	public class CMDMICQuadCast2SSetHighPassState : CMDMICQuadCast2SCommandBase
	{
		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06002C55 RID: 11349 RVA: 0x000B3E20 File Offset: 0x000B2020
		// (set) Token: 0x06002C56 RID: 11350 RVA: 0x000B3E28 File Offset: 0x000B2028
		public bool Enable { get; set; }

		// Token: 0x06002C57 RID: 11351 RVA: 0x000B3E34 File Offset: 0x000B2034
		public override void Execute(HyperXDevice device)
		{
			byte[] array = device.CreateBuffer();
			array[0] = 119;
			array[1] = 7;
			array[2] = ((this.Enable > false) ? 1 : 0);
			Logger.WriteLine(string.Format("Set Highpass: {0}", this.Enable), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\Microphone\\Quadcast2S\\CMDMICQuadcast2SCommandBase.cs", 140);
			Logger.WriteLine(string.Format("Codec -> {0:X2} {1:X2} {2:X2}", array[0], array[1], array[2]), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\Microphone\\Quadcast2S\\CMDMICQuadcast2SCommandBase.cs", 141);
			device.SetSecondaryOutputReport(array);
		}
	}
}```

# CMDMICQuadCast2SSetMicrophoneGain
```csharp
using System;
using NGenuity2.Common;

namespace NGenuity2.Devices.Microphone.Quadcast2S
{
	// Token: 0x020007BF RID: 1983
	public class CMDMICQuadCast2SSetMicrophoneGain : CMDMICQuadCast2SCommandBase
	{
		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06002C59 RID: 11353 RVA: 0x000B3EBA File Offset: 0x000B20BA
		// (set) Token: 0x06002C5A RID: 11354 RVA: 0x000B3EC2 File Offset: 0x000B20C2
		public int Gain { get; set; }

		// Token: 0x06002C5B RID: 11355 RVA: 0x000B3ECC File Offset: 0x000B20CC
		public override void Execute(HyperXDevice device)
		{
			byte[] array = device.CreateBuffer();
			array[0] = 119;
			array[1] = 8;
			array[2] = (byte)this.Gain;
			Logger.WriteLine(string.Format("Set MixBalance: {0}", this.Gain), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\Microphone\\Quadcast2S\\CMDMICQuadcast2SCommandBase.cs", 157);
			Logger.WriteLine(string.Format("Codec -> {0:X2} {1:X2} {2:X2}", array[0], array[1], array[2]), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\Microphone\\Quadcast2S\\CMDMICQuadcast2SCommandBase.cs", 158);
			device.SetSecondaryOutputReport(array);
		}
	}
}```

# CMDMICQuadCast2SSetMicrophoneMuteState
```csharp
using System;
using NGenuity2.Common;

namespace NGenuity2.Devices.Microphone.Quadcast2S
{
	// Token: 0x020007BB RID: 1979
	public class CMDMICQuadCast2SSetMicrophoneMuteState : CMDMICQuadCast2SCommandBase
	{
		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06002C4D RID: 11341 RVA: 0x000B3D3C File Offset: 0x000B1F3C
		// (set) Token: 0x06002C4E RID: 11342 RVA: 0x000B3D44 File Offset: 0x000B1F44
		public bool Muted { get; set; }

		// Token: 0x06002C4F RID: 11343 RVA: 0x000B3D50 File Offset: 0x000B1F50
		public override void Execute(HyperXDevice device)
		{
			byte[] array = device.CreateBuffer();
			array[0] = 119;
			array[1] = 6;
			array[2] = ((this.Muted > false) ? 1 : 0);
			Logger.WriteLine(string.Format("Set Mute: {0}", this.Muted), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\Microphone\\Quadcast2S\\CMDMICQuadcast2SCommandBase.cs", 100);
			Logger.WriteLine(string.Format("Codec -> {0:X2} {1:X2} {2:X2}", array[0], array[1], array[2]), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\Microphone\\Quadcast2S\\CMDMICQuadcast2SCommandBase.cs", 101);
			device.SetSecondaryOutputReport(array);
		}
	}
}```

# CMDMICQuadCast2SSetPolarPattern
```csharp
using System;
using NGenuity2.Common;

namespace NGenuity2.Devices.Microphone.Quadcast2S
{
	// Token: 0x020007B9 RID: 1977
	public class CMDMICQuadCast2SSetPolarPattern : CMDMICQuadCast2SCommandBase
	{
		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06002C47 RID: 11335 RVA: 0x000B3C40 File Offset: 0x000B1E40
		// (set) Token: 0x06002C48 RID: 11336 RVA: 0x000B3C48 File Offset: 0x000B1E48
		public PolarPattern? PolarPattern { get; set; }

		// Token: 0x06002C49 RID: 11337 RVA: 0x000B3C54 File Offset: 0x000B1E54
		public override void Execute(HyperXDevice device)
		{
			byte[] array = device.CreateBuffer();
			array[0] = 119;
			array[1] = 5;
			if (this.PolarPattern == null)
			{
				return;
			}
			switch (this.PolarPattern.Value)
			{
			case NGenuity2.Devices.PolarPattern.Bidirectional:
				array[2] = 0;
				break;
			case NGenuity2.Devices.PolarPattern.Cardioid:
				array[2] = 1;
				break;
			case NGenuity2.Devices.PolarPattern.Omnidirectional:
				array[2] = 2;
				break;
			case NGenuity2.Devices.PolarPattern.Stereo:
				array[2] = 3;
				break;
			default:
				return;
			}
			Logger.WriteLine(string.Format("Set Polar Pattern: {0}", this.PolarPattern), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\Microphone\\Quadcast2S\\CMDMICQuadcast2SCommandBase.cs", 72);
			Logger.WriteLine(string.Format("Codec -> {0:X2} {1:X2} {2:X2}", array[0], array[1], array[2]), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\Microphone\\Quadcast2S\\CMDMICQuadcast2SCommandBase.cs", 73);
			device.SetSecondaryOutputReport(array);
		}
	}
}```

# CMDMICQuadCastSApplyBuiltinEffect
```csharp
using System;
using System.Threading;

namespace NGenuity2.Devices.Microphone
{
	// Token: 0x02000787 RID: 1927
	public class CMDMICQuadCastSApplyBuiltinEffect : CMDMICQuadCastSCommandBase
	{
		// Token: 0x06002B89 RID: 11145 RVA: 0x000B1ED4 File Offset: 0x000B00D4
		public override void Execute(HyperXDevice device)
		{
			base.Execute(device);
			byte cmd = 35;
			byte[] array = device.CreateBuffer();
			base.InitCommand(array, cmd);
			array[9] = 1;
			device.RequestAck();
			device.SetFeatureReport(array);
			device.WaitAck();
			if (!base.VerifyCommandAsyc(device, array[2]))
			{
				return;
			}
			base.ZeroBuffer(array);
			array[1] = 2;
			array[2] = byte.MaxValue;
			array[3] = byte.MaxValue;
			array[4] = byte.MaxValue;
			array[63] = 170;
			array[64] = 85;
			device.SetFeatureReport(array);
			Thread.Sleep(50);
			base.InitCommand(array, 2);
			device.RequestAck();
			device.SetFeatureReport(array);
			device.WaitAck();
			base.VerifyCommandAsyc(device, array[2]);
		}
	}
}```

# CMDMICQuadCastSCommandBase
```csharp
using System;
using System.Threading;
using NGenuity2.Common;

namespace NGenuity2.Devices.Microphone
{
	// Token: 0x02000780 RID: 1920
	public abstract class CMDMICQuadCastSCommandBase : HXCommandBase
	{
		// Token: 0x06002B61 RID: 11105 RVA: 0x000B1984 File Offset: 0x000AFB84
		protected bool WaitAckAsync(HyperXDevice device, byte cmd, int timeout)
		{
			Thread.Sleep(timeout);
			byte[] featureReport = device.GetFeatureReport();
			return featureReport != null && featureReport[2] == cmd && featureReport[4] == 1;
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x000B19B0 File Offset: 0x000AFBB0
		protected bool VerifyCommandAsyc(HyperXDevice device, byte cmd)
		{
			byte[] featureReport = device.GetFeatureReport();
			return featureReport != null && featureReport[2] == cmd && featureReport[4] == 1;
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x000B19D6 File Offset: 0x000AFBD6
		protected bool WaitAckAsync(HyperXDevice device, byte cmd)
		{
			return this.WaitAckAsync(device, cmd, 10);
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x000682AA File Offset: 0x000664AA
		protected void ZeroBuffer(byte[] buffer)
		{
			Utils.ZeroBuffer(buffer);
			buffer[0] = 7;
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x000B19E2 File Offset: 0x000AFBE2
		protected void InitCommand(byte[] buffer, byte cmd)
		{
			this.ZeroBuffer(buffer);
			buffer[1] = 4;
			buffer[2] = cmd;
		}
	}
}```

# CMDMICQuadcastSGetCurrentProfile
```csharp
using System;

namespace NGenuity2.Devices.Microphone
{
	// Token: 0x02000781 RID: 1921
	public class CMDMICQuadcastSGetCurrentProfile : CMDMICQuadCastSCommandBase
	{
		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06002B67 RID: 11111 RVA: 0x000B19F3 File Offset: 0x000AFBF3
		// (set) Token: 0x06002B68 RID: 11112 RVA: 0x000B19FB File Offset: 0x000AFBFB
		public byte Brightness { get; set; }

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06002B69 RID: 11113 RVA: 0x000B1A04 File Offset: 0x000AFC04
		// (set) Token: 0x06002B6A RID: 11114 RVA: 0x000B1A0C File Offset: 0x000AFC0C
		public bool ReverseLights { get; internal set; }

		// Token: 0x06002B6C RID: 11116 RVA: 0x000B1A20 File Offset: 0x000AFC20
		public override void Execute(HyperXDevice device)
		{
			byte[] array = device.CreateBuffer();
			array[1] = 4;
			array[2] = 86;
			array[9] = 1;
			device.RequestAck();
			device.SetFeatureReport(array);
			device.WaitAck();
			base.VerifyCommandAsyc(device, array[2]);
			array = device.GetFeatureReport();
			if (array != null)
			{
				this.Brightness = (byte)((float)array[13] * 100f / 255f);
				this.ReverseLights = (array[17] == 1);
			}
		}
	}
}```

# CMDMICQuadCastSGetDeviceInfo
```csharp
using System;

namespace NGenuity2.Devices.Microphone
{
	// Token: 0x02000783 RID: 1923
	public class CMDMICQuadCastSGetDeviceInfo : CMDMICQuadCastSCommandBase
	{
		// Token: 0x06002B74 RID: 11124 RVA: 0x000B1BDC File Offset: 0x000AFDDC
		public override void Execute(HyperXDevice device)
		{
			byte[] array = device.CreateBuffer();
			array[1] = 4;
			array[2] = 5;
			array[9] = 1;
			device.RequestAck();
			device.SetFeatureReport(array);
			device.WaitAck();
			base.VerifyCommandAsyc(device, array[2]);
			array = device.GetFeatureReport();
		}
	}
}```

# CMDMICQuadCastSGetDeviceStatus
```csharp
using System;

namespace NGenuity2.Devices.Microphone
{
	// Token: 0x02000784 RID: 1924
	public class CMDMICQuadCastSGetDeviceStatus : CMDMICQuadCastSCommandBase
	{
		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06002B75 RID: 11125 RVA: 0x000B1C23 File Offset: 0x000AFE23
		// (set) Token: 0x06002B76 RID: 11126 RVA: 0x000B1C2B File Offset: 0x000AFE2B
		public PolarPattern Pattern { get; set; }

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06002B77 RID: 11127 RVA: 0x000B1C34 File Offset: 0x000AFE34
		// (set) Token: 0x06002B78 RID: 11128 RVA: 0x000B1C3C File Offset: 0x000AFE3C
		public bool Muted { get; set; }

		// Token: 0x06002B7A RID: 11130 RVA: 0x000B1C48 File Offset: 0x000AFE48
		public override void Execute(HyperXDevice device)
		{
			byte[] array = device.CreateBuffer();
			array[1] = 4;
			array[2] = 88;
			array[9] = 1;
			device.RequestAck();
			device.SetFeatureReport(array);
			device.WaitAck();
			base.VerifyCommandAsyc(device, array[2]);
			array = device.GetFeatureReport();
			if (array != null)
			{
				if (device.AudioDeviceVendorId == 1008 && device.AudioDeviceProductIds.Contains(660))
				{
					this.Muted = (array[6] == 1);
					this.Pattern = (PolarPattern)array[7];
					return;
				}
				this.Muted = (array[13] == 1);
				this.Pattern = (PolarPattern)array[17];
			}
		}
	}
}```

# CMDMICQuadcastSGetDeviceStatus2ndSource
```csharp
using System;

namespace NGenuity2.Devices.Microphone
{
	// Token: 0x02000785 RID: 1925
	public class CMDMICQuadcastSGetDeviceStatus2ndSource : CMDMICQuadCastSCommandBase
	{
		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06002B7B RID: 11131 RVA: 0x000B1CDF File Offset: 0x000AFEDF
		// (set) Token: 0x06002B7C RID: 11132 RVA: 0x000B1CE7 File Offset: 0x000AFEE7
		public PolarPattern Pattern { get; set; }

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06002B7D RID: 11133 RVA: 0x000B1CF0 File Offset: 0x000AFEF0
		// (set) Token: 0x06002B7E RID: 11134 RVA: 0x000B1CF8 File Offset: 0x000AFEF8
		public bool Muted { get; set; }

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06002B7F RID: 11135 RVA: 0x000B1D01 File Offset: 0x000AFF01
		// (set) Token: 0x06002B80 RID: 11136 RVA: 0x000B1D09 File Offset: 0x000AFF09
		public byte Brightness { get; set; }

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06002B81 RID: 11137 RVA: 0x000B1D12 File Offset: 0x000AFF12
		// (set) Token: 0x06002B82 RID: 11138 RVA: 0x000B1D1A File Offset: 0x000AFF1A
		public bool ReverseLights { get; internal set; }

		// Token: 0x06002B84 RID: 11140 RVA: 0x000B1D24 File Offset: 0x000AFF24
		public override void Execute(HyperXDevice device)
		{
			byte[] array = device.CreateBuffer();
			array[1] = 4;
			array[2] = 88;
			array[9] = 1;
			device.RequestAck();
			device.SetFeatureReport(array);
			device.WaitAck();
			array = device.GetFeatureReport();
			if (array != null)
			{
				this.Muted = (array[6] == 1);
				this.Pattern = (PolarPattern)array[7];
				this.Brightness = (byte)((float)array[8] * 100f / 255f);
				this.ReverseLights = (array[9] == 1);
			}
		}
	}
}```

# CMDMICQuadCastSResetToDefault
```csharp
using System;
using System.Threading;

namespace NGenuity2.Devices.Microphone
{
	// Token: 0x02000789 RID: 1929
	public class CMDMICQuadCastSResetToDefault : CMDMICQuadCastSCommandBase
	{
		// Token: 0x06002B92 RID: 11154 RVA: 0x000B22FC File Offset: 0x000B04FC
		public override void Execute(HyperXDevice device)
		{
			byte cmd = 35;
			byte[] array = device.CreateBuffer();
			base.InitCommand(array, cmd);
			array[9] = 1;
			device.SetFeatureReport(array);
			Thread.Sleep(10);
			device.GetFeatureReport();
			base.ZeroBuffer(array);
			array[1] = 3;
			array[60] = 50;
			array[63] = 170;
			array[64] = 85;
			device.SetFeatureReport(array);
			Thread.Sleep(50);
			base.InitCommand(array, 2);
			device.SetFeatureReport(array);
			Thread.Sleep(10);
			device.GetFeatureReport();
		}
	}
}```

# CMDMICQuadcastSSetCurrentProfile
```csharp
using System;
using System.Threading;
using NGenuity2.Common;

namespace NGenuity2.Devices.Microphone
{
	// Token: 0x02000782 RID: 1922
	public class CMDMICQuadcastSSetCurrentProfile : CMDMICQuadCastSCommandBase
	{
		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06002B6D RID: 11117 RVA: 0x000B1A90 File Offset: 0x000AFC90
		// (set) Token: 0x06002B6E RID: 11118 RVA: 0x000B1A98 File Offset: 0x000AFC98
		public byte Brightness { get; set; }

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06002B6F RID: 11119 RVA: 0x000B1AA1 File Offset: 0x000AFCA1
		// (set) Token: 0x06002B70 RID: 11120 RVA: 0x000B1AA9 File Offset: 0x000AFCA9
		public bool ReverseLights { get; internal set; }

		// Token: 0x06002B71 RID: 11121 RVA: 0x000B1AB2 File Offset: 0x000AFCB2
		public CMDMICQuadcastSSetCurrentProfile()
		{
			base.Skip = true;
			base.ResetLighting = true;
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x000B1AC8 File Offset: 0x000AFCC8
		public override void Execute(HyperXDevice device)
		{
			byte[] array = device.CreateBuffer();
			array[1] = 4;
			array[2] = 86;
			array[9] = 1;
			device.RequestAck();
			device.SetFeatureReport(array);
			device.WaitAck();
			base.VerifyCommandAsyc(device, array[2]);
			device.GetFeatureReport();
			Utils.ZeroBuffer(array);
			array[1] = 4;
			array[2] = 87;
			array[9] = 1;
			device.RequestAck();
			device.SetFeatureReport(array);
			device.WaitAck();
			base.VerifyCommandAsyc(device, array[2]);
			Utils.ZeroBuffer(array);
			array[13] = (byte)(this.Brightness * 255.0m / 100.0m);
			array[17] = ((this.ReverseLights > false) ? 1 : 0);
			array[63] = 170;
			array[64] = 85;
			device.SetFeatureReport(array);
			Thread.Sleep(10);
			Utils.ZeroBuffer(array);
			array[1] = 4;
			array[2] = 2;
			device.RequestAck();
			device.SetFeatureReport(array);
			device.WaitAck();
			base.VerifyCommandAsyc(device, array[2]);
		}
	}
}```

# CMDMICQuadCastSShowLighting
```csharp
using System;
using System.Threading;
using NGenuity2.Common;

namespace NGenuity2.Devices.Microphone
{
	// Token: 0x02000786 RID: 1926
	public class CMDMICQuadCastSShowLighting : CMDMICQuadCastSCommandBase
	{
		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06002B85 RID: 11141 RVA: 0x000B1D9D File Offset: 0x000AFF9D
		// (set) Token: 0x06002B86 RID: 11142 RVA: 0x000B1DA5 File Offset: 0x000AFFA5
		public Color[] Colors { get; private set; }

		// Token: 0x06002B87 RID: 11143 RVA: 0x000B1DB0 File Offset: 0x000AFFB0
		public CMDMICQuadCastSShowLighting()
		{
			base.ProfileID = 0;
			base.Skip = false;
			this.Colors = new Color[2];
			for (int i = 0; i < this.Colors.Length; i++)
			{
				this.Colors[i] = default(Color);
			}
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x000B1E04 File Offset: 0x000B0004
		public override void Execute(HyperXDevice device)
		{
			byte[] array = device.CreateBuffer();
			array[1] = 4;
			array[2] = 242;
			array[9] = 1;
			device.SetFeatureReport(array);
			Thread.Sleep(2);
			Utils.ZeroBuffer(array);
			array[1] = 129;
			array[2] = this.Colors[0].R;
			array[3] = this.Colors[0].G;
			array[4] = this.Colors[0].B;
			array[5] = 129;
			array[6] = this.Colors[1].R;
			array[7] = this.Colors[1].G;
			array[8] = this.Colors[1].B;
			device.SetFeatureReport(array);
			Thread.Sleep(2);
		}
	}
}```

# CMDMICQuadCastSSync
```csharp
using System;
using System.Threading;
using NGenuity2.Common;

namespace NGenuity2.Devices.Microphone
{
	// Token: 0x02000788 RID: 1928
	public class CMDMICQuadCastSSync : CMDMICQuadCastSCommandBase
	{
		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06002B8B RID: 11147 RVA: 0x000B1F87 File Offset: 0x000B0187
		// (set) Token: 0x06002B8C RID: 11148 RVA: 0x000B1F8F File Offset: 0x000B018F
		public Color[][] Colors { get; private set; }

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06002B8D RID: 11149 RVA: 0x000B1F98 File Offset: 0x000B0198
		// (set) Token: 0x06002B8E RID: 11150 RVA: 0x000B1FA0 File Offset: 0x000B01A0
		public byte Interval { get; set; }

		// Token: 0x06002B8F RID: 11151 RVA: 0x000B1FA9 File Offset: 0x000B01A9
		public CMDMICQuadCastSSync(Color[][] colors)
		{
			this.Colors = colors;
		}

		// Token: 0x06002B90 RID: 11152 RVA: 0x000B1FB8 File Offset: 0x000B01B8
		public override void Execute(HyperXDevice device)
		{
			byte cmd = 83;
			base.Handler(this, 0);
			int num = this.Colors.Length * 2;
			int num2 = 0;
			byte[] array = new byte[this.Colors.Length * 8];
			for (int i = 0; i < this.Colors.Length; i++)
			{
				for (int j = 0; j < this.Colors[i].Length; j++)
				{
					int num3 = i * 8 + j * 4;
					array[num3] = 129;
					array[num3 + 1] = this.Colors[i][j].R;
					array[num3 + 2] = this.Colors[i][j].G;
					array[num3 + 3] = this.Colors[i][j].B;
				}
			}
			num = array.Length / 64;
			if (array.Length % 64 > 0)
			{
				num++;
			}
			int num4 = num + 4;
			byte[] array2 = device.CreateBuffer();
			base.InitCommand(array2, cmd);
			Utils.ConvertShort2Bytes(num, array2, 9);
			device.RequestAck();
			device.SetFeatureReport(array2);
			device.WaitAck();
			if (!base.VerifyCommandAsyc(device, array2[2]))
			{
				return;
			}
			num2++;
			base.Handler(this, (int)((float)num2 / (float)num4 * 1000f));
			for (int k = 0; k < num; k++)
			{
				base.ZeroBuffer(array2);
				int num5 = array.Length - k * 64;
				if (num5 > 64)
				{
					num5 = 64;
				}
				Array.Copy(array, k * 64, array2, 1, num5);
				device.SetFeatureReport(array2);
				Thread.Sleep(10);
				num2++;
				base.Handler(this, (int)((float)num2 / (float)num4 * 1000f));
			}
			base.InitCommand(array2, 2);
			device.RequestAck();
			device.SetFeatureReport(array2);
			device.WaitAck();
			if (!base.VerifyCommandAsyc(device, array2[2]))
			{
				return;
			}
			num2++;
			base.Handler(this, (int)((float)num2 / (float)num4 * 1000f));
			cmd = 35;
			base.InitCommand(array2, cmd);
			array2[9] = 1;
			device.RequestAck();
			device.SetFeatureReport(array2);
			device.WaitAck();
			if (!base.VerifyCommandAsyc(device, array2[2]))
			{
				return;
			}
			num2++;
			base.Handler(this, (int)((float)num2 / (float)num4 * 1000f));
			base.ZeroBuffer(array2);
			array2[1] = 8;
			array2[59] = 0;
			array2[60] = this.Interval;
			Utils.ConvertShort2Bytes(this.Colors.Length, array2, 61);
			array2[63] = 170;
			array2[64] = 85;
			device.SetFeatureReport(array2);
			num2++;
			base.Handler(this, (int)((float)num2 / (float)num4 * 1000f));
			Thread.Sleep(50);
			base.InitCommand(array2, 2);
			device.RequestAck();
			device.SetFeatureReport(array2);
			device.WaitAck();
			base.VerifyCommandAsyc(device, array2[2]);
			num2++;
			base.Handler(this, 1000);
			Thread.Sleep(1000);
			base.Handler(this, 10000);
		}
	}
}```

# MicrophoneQuadCast2
```csharp
using System;
using System.Collections.Generic;
using NGenuity2.Audio;
using NGenuity2.Common;
using NGenuity2.Common.Devices;
using NGenuity2.Devices.Microphone.QuadCast2;

namespace NGenuity2.Devices.Microphone
{
	// Token: 0x0200077E RID: 1918
	public class MicrophoneQuadCast2 : HyperXMicrophoneDevice<CMDMICQuadCast2CommandBase, CMDMICQuadCast2ShowLighting>
	{
		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06002B0E RID: 11022 RVA: 0x000AE766 File Offset: 0x000AC966
		// (set) Token: 0x06002B0F RID: 11023 RVA: 0x000AE77A File Offset: 0x000AC97A
		public bool FilterHighPass
		{
			get
			{
				return base.ExtraProperties[100].Boolean();
			}
			set
			{
				base.ExtraProperties[100] = value.ExtraPropertyValue();
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06002B10 RID: 11024 RVA: 0x000AE78F File Offset: 0x000AC98F
		// (set) Token: 0x06002B11 RID: 11025 RVA: 0x000AE7A3 File Offset: 0x000AC9A3
		public bool IsJackPlugged
		{
			get
			{
				return base.ExtraProperties[101].Boolean();
			}
			set
			{
				base.ExtraProperties[101] = value.ExtraPropertyValue();
			}
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x000B003C File Offset: 0x000AE23C
		public MicrophoneQuadCast2()
		{
			base.Model = HyperXDeviceModel.Microphone_QuadCast2;
			base.DFUNeedReboot = false;
			base.FramePerSecond = 25;
			base.MaxSyncFrameCount = 720;
			base.Engine.DeviceModel = base.Model;
			this.SetupKeys();
			base.CurrentPresetID = 4;
			base.CanLink = false;
			base.ColorSkin = ColorSkins.BlackBlack;
			this.FilterHighPass = false;
			this.IsJackPlugged = false;
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x000B00B0 File Offset: 0x000AE2B0
		private void SetupKeys()
		{
			base.Keys.Add(new KeyMap(KeyCode.TopLed1, 433, 29, 24, 49, 0, 0, 0, 0));
			base.Keys.Add(new KeyMap(KeyCode.TopLed2, 410, 223, 56, 30, 1, 1, 1, 0));
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06002B14 RID: 11028 RVA: 0x000AF9A4 File Offset: 0x000ADBA4
		protected override bool DeviceIsReady
		{
			get
			{
				return base.Device != null && base.SecondaryDevice != null;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06002B15 RID: 11029 RVA: 0x000AF9B9 File Offset: 0x000ADBB9
		public override bool IsOpened
		{
			get
			{
				return base.IsOpened || !string.IsNullOrEmpty(base.SecondaryDeviceID);
			}
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x000B0108 File Offset: 0x000AE308
		protected override void InitDevice()
		{
			this.GetBasicInfo();
			this.ApplyBasicSettings();
			if (!this.UpgradeMode)
			{
				base.StartEffectEngine();
			}
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x000B0124 File Offset: 0x000AE324
		public override void OpenDevice(string deviceId)
		{
			base.OpenDevice(deviceId);
			if (Utils.IsCultureInvariantMatch(deviceId, new string[]
			{
				"HID#VID_03F0&PID_0AAF"
			}))
			{
				this.UpgradeMode = true;
			}
			if (this.UpgradeMode)
			{
				this.NotifyFirmwareUpdate();
			}
			base.UpdateID();
			if (!this.UpgradeMode && base.NotificationDevice == null && !string.IsNullOrEmpty(base.NotificationDeviceID))
			{
				base.OpenNotification(base.NotificationDeviceID);
			}
			if (this.IsOpened && !this.UpgradeMode && !HyperXCenter.Center.HasNewerBuiltinFirmware(this) && this.DeviceIsReady)
			{
				this.SetupDevice();
			}
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x000B01C0 File Offset: 0x000AE3C0
		public override void OpenSecondaryDevice(string deviceId)
		{
			if (base.SecondaryDevice != null)
			{
				base.CloseSecondaryDevice();
			}
			HidDevice hidDevice = HidDevice.FromId(deviceId, 3221225472U);
			if (hidDevice != null)
			{
				base.SecondaryDevice = hidDevice;
				base.SecondaryDeviceID = deviceId;
				base.OpenSecondaryEndpointNotificationTunnel();
			}
			if (this.IsOpened && this.DeviceIsReady)
			{
				this.SetupDevice();
			}
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x000B0214 File Offset: 0x000AE414
		public override bool IsDevice(string deviceId)
		{
			ushort num;
			ushort num2;
			Utils.ParseVIDPID(deviceId, out num, out num2);
			if (!string.IsNullOrEmpty(base.DeviceID))
			{
				ushort num3;
				ushort num4;
				Utils.ParseVIDPID(base.DeviceID, out num3, out num4);
				ushort num5;
				ushort num6;
				Utils.ParseVIDPID("HID#VID_03F0&PID_07B4&MI_02&Col04", out num5, out num6);
				ushort num7;
				ushort num8;
				Utils.ParseVIDPID("HID#VID_03F0&PID_07AF&MI_03&Col04", out num7, out num8);
				if (num == num3)
				{
					if (num2 == num4)
					{
						return true;
					}
					if (num2 == num6 || num2 == num8)
					{
						return true;
					}
				}
			}
			if (!string.IsNullOrEmpty(base.SecondaryDeviceID))
			{
				ushort num9;
				ushort num10;
				Utils.ParseVIDPID(base.SecondaryDeviceID, out num9, out num10);
				ushort num11;
				ushort num12;
				Utils.ParseVIDPID("HID#VID_03F0&PID_09AF&MI_00", out num11, out num12);
				if (num == num9)
				{
					if (num2 == num10)
					{
						return true;
					}
					if (num2 == num12)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x000B02C0 File Offset: 0x000AE4C0
		public override void ChangePolarPattern(PolarPattern pattern)
		{
			if (pattern != base.PolarPattern)
			{
				base.ChangePolarPattern(pattern);
				this.AddCommand(new CMDMICQuadCast2SetPolarPattern
				{
					PolarPattern = new PolarPattern?(pattern)
				});
			}
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x000B02F6 File Offset: 0x000AE4F6
		public override void AddAudioDevice(MMDevice device)
		{
			base.AddAudioDevice(device);
			if (device.PID == 1972 || device.PID == 4020)
			{
				this.CheckAndUpdateJackPlugState(false);
			}
			if (device.PID == 1967)
			{
				this.CheckAndUpdateJackPlugState(true);
			}
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x000B0334 File Offset: 0x000AE534
		public override bool ApplyExtraProperty(int key, long value)
		{
			if (base.ApplyExtraProperty(key, value))
			{
				if (key == 100)
				{
					this.AddCommand(new CMDMICQuadCast2SetHighPassState
					{
						Enable = value.Boolean()
					});
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x000AFC03 File Offset: 0x000ADE03
		public override void RetriveUpdateDeviceInfo(List<string> deviceInfo)
		{
			base.RetriveUpdateDeviceInfo(deviceInfo);
			if (!string.IsNullOrEmpty(base.SecondaryDeviceID))
			{
				deviceInfo.Add(base.SecondaryDeviceID);
			}
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x00057B54 File Offset: 0x00055D54
		public override void ApplyBasicSettings()
		{
			base.ApplyBasicSettings();
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x000AFC25 File Offset: 0x000ADE25
		public override void ApplyShowLights(bool muted)
		{
			base.ApplyShowLights(muted);
			this.ClearLightingCommands();
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x000B036C File Offset: 0x000AE56C
		protected override void OnDeviceInputReportReceived(byte[] buffer)
		{
			base.OnDeviceInputReportReceived(buffer);
			if (buffer[0] == 119)
			{
				byte b = buffer[1];
				switch (b)
				{
				case 5:
				{
					PolarPattern? polarPattern = this.ParseToPorlarPattern(buffer[2]);
					if (polarPattern != null)
					{
						this.CheckAndUpdatePolarPattern(polarPattern.Value);
						return;
					}
					break;
				}
				case 6:
					this.CheckAndUpdateMicMuteState(buffer[2] == 1);
					break;
				case 7:
				case 8:
				case 9:
				case 10:
					break;
				default:
					switch (b)
					{
					case 129:
						BitConverter.ToString(buffer, 7, 1) + BitConverter.ToString(buffer, 6, 1);
						return;
					case 130:
					case 131:
					case 132:
						break;
					case 133:
					{
						PolarPattern? polarPattern2 = this.ParseToPorlarPattern(buffer[2]);
						if (polarPattern2 != null)
						{
							this.CheckAndUpdatePolarPattern(polarPattern2.Value);
							return;
						}
						break;
					}
					case 134:
						this.CheckAndUpdateMicMuteState(buffer[2] == 1);
						return;
					case 135:
						this.CheckAndUpdateHighPassState(buffer[2] == 1);
						return;
					default:
						return;
					}
					break;
				}
			}
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x00063880 File Offset: 0x00061A80
		protected override void RenderFrameToDevice(List<LightingItem> items)
		{
			base.RenderFrameToDevice(items);
			this.SetLightings(base.Keys);
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x000B0458 File Offset: 0x000AE658
		public override void SetLightings(IList<KeyMap> keys)
		{
			base.SetLightings(keys);
			CMDMICQuadCast2ShowLighting cmdmicquadCast2ShowLighting = new CMDMICQuadCast2ShowLighting();
			lock (keys)
			{
				foreach (KeyMap keyMap in keys)
				{
					cmdmicquadCast2ShowLighting.Colors[(int)keyMap.LEDMatrix.X] = keyMap.Color;
				}
			}
			this.AddCommand(cmdmicquadCast2ShowLighting);
		}

		// Token: 0x06002B23 RID: 11043 RVA: 0x000B04F0 File Offset: 0x000AE6F0
		private PolarPattern? ParseToPorlarPattern(byte raw)
		{
			PolarPattern? result = null;
			switch (raw)
			{
			case 0:
				result = new PolarPattern?(PolarPattern.Cardioid);
				break;
			case 1:
				result = new PolarPattern?(PolarPattern.Omnidirectional);
				break;
			case 2:
				result = new PolarPattern?(PolarPattern.Stereo);
				break;
			case 3:
				result = new PolarPattern?(PolarPattern.Bidirectional);
				break;
			}
			return result;
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x000B0544 File Offset: 0x000AE744
		private void GetBasicInfo()
		{
			CMDMICQuadCast2GetDeviceInfo cmd = new CMDMICQuadCast2GetDeviceInfo();
			this.AddCommand(cmd);
			CMDMICQuadCast2GetPolarPattern cmd2 = new CMDMICQuadCast2GetPolarPattern();
			this.AddCommand(cmd2);
			CMDMICQuadCast2GetMicrophoneMuteState cmd3 = new CMDMICQuadCast2GetMicrophoneMuteState();
			this.AddCommand(cmd3);
			CMDMICQuadCast2GetHighPassState cmd4 = new CMDMICQuadCast2GetHighPassState();
			this.AddCommand(cmd4);
		}

		// Token: 0x06002B25 RID: 11045 RVA: 0x000B0585 File Offset: 0x000AE785
		private void CheckAndUpdatePolarPattern(PolarPattern newValue)
		{
			this.OnPolarPatternChanged(newValue);
		}

		// Token: 0x06002B26 RID: 11046 RVA: 0x000B058E File Offset: 0x000AE78E
		private void CheckAndUpdateMicMuteState(bool muted)
		{
			this.OnAudioDeviceMuted(AudioDeviceType.Microphone, muted, true);
		}

		// Token: 0x06002B27 RID: 11047 RVA: 0x000B0599 File Offset: 0x000AE799
		private void CheckAndUpdateHighPassState(bool isEnable)
		{
			if (this.FilterHighPass != isEnable)
			{
				this.FilterHighPass = isEnable;
				HyperXCenter.Center.OnDeviceUpdated(this);
			}
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x000B05B6 File Offset: 0x000AE7B6
		private void CheckAndUpdateJackPlugState(bool isPlugged)
		{
			if (this.IsJackPlugged != isPlugged)
			{
				this.IsJackPlugged = isPlugged;
				HyperXCenter.Center.OnDeviceUpdated(this);
			}
		}

		// Token: 0x0400215D RID: 8541
		private const int PTY_HIGHPASS = 100;

		// Token: 0x0400215E RID: 8542
		private const int PTY_IS_JACK_PLUGED = 101;
	}
}```

# MicrophoneQuadCast2S
```csharp
using System;
using System.Collections.Generic;
using NGenuity2.Audio;
using NGenuity2.Common;
using NGenuity2.Common.Devices;
using NGenuity2.Devices.Microphone.Quadcast2S;
using NGenuity2.Devices.Universals.Microphone;
using NGenuity2.Devices.Universals.Models;
using NGenuity2.Devices.Universals.Updater;
using NGenuity2.Model;

namespace NGenuity2.Devices.Microphone
{
	// Token: 0x0200077D RID: 1917
	internal class MicrophoneQuadCast2S : UniversalMicrophoneBase, IFirmwareUpdateWired, IFirmwareUpdate
	{
		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06002AD5 RID: 10965 RVA: 0x000AE766 File Offset: 0x000AC966
		// (set) Token: 0x06002AD6 RID: 10966 RVA: 0x000AE77A File Offset: 0x000AC97A
		public bool FilterAINR
		{
			get
			{
				return base.ExtraProperties[100].Boolean();
			}
			set
			{
				base.ExtraProperties[100] = value.ExtraPropertyValue();
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06002AD7 RID: 10967 RVA: 0x000AE78F File Offset: 0x000AC98F
		// (set) Token: 0x06002AD8 RID: 10968 RVA: 0x000AE7A3 File Offset: 0x000AC9A3
		public bool FilterHighPass
		{
			get
			{
				return base.ExtraProperties[101].Boolean();
			}
			set
			{
				base.ExtraProperties[101] = value.ExtraPropertyValue();
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06002AD9 RID: 10969 RVA: 0x000AE7B8 File Offset: 0x000AC9B8
		// (set) Token: 0x06002ADA RID: 10970 RVA: 0x000AE7CC File Offset: 0x000AC9CC
		public bool FilterLimiter
		{
			get
			{
				return base.ExtraProperties[102].Boolean();
			}
			set
			{
				base.ExtraProperties[102] = value.ExtraPropertyValue();
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06002ADB RID: 10971 RVA: 0x000AE7E1 File Offset: 0x000AC9E1
		// (set) Token: 0x06002ADC RID: 10972 RVA: 0x000AE7F5 File Offset: 0x000AC9F5
		public bool FilterPresence
		{
			get
			{
				return base.ExtraProperties[103].Boolean();
			}
			set
			{
				base.ExtraProperties[103] = value.ExtraPropertyValue();
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06002ADD RID: 10973 RVA: 0x000AE80A File Offset: 0x000ACA0A
		// (set) Token: 0x06002ADE RID: 10974 RVA: 0x000AE81D File Offset: 0x000ACA1D
		public int MixBalance
		{
			get
			{
				return (int)base.ExtraProperties[200];
			}
			set
			{
				base.ExtraProperties[200] = (long)value;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06002ADF RID: 10975 RVA: 0x000AE831 File Offset: 0x000ACA31
		// (set) Token: 0x06002AE0 RID: 10976 RVA: 0x000AE844 File Offset: 0x000ACA44
		public int FilterLimiterGain
		{
			get
			{
				return (int)base.ExtraProperties[1020];
			}
			set
			{
				base.ExtraProperties[1020] = (long)value;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06002AE1 RID: 10977 RVA: 0x000AE858 File Offset: 0x000ACA58
		// (set) Token: 0x06002AE2 RID: 10978 RVA: 0x000AE86B File Offset: 0x000ACA6B
		public int FilterLimiterAttack
		{
			get
			{
				return (int)base.ExtraProperties[1021];
			}
			set
			{
				base.ExtraProperties[1021] = (long)value;
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06002AE3 RID: 10979 RVA: 0x000AE87F File Offset: 0x000ACA7F
		// (set) Token: 0x06002AE4 RID: 10980 RVA: 0x000AE892 File Offset: 0x000ACA92
		public int FilterLimiterRelease
		{
			get
			{
				return (int)base.ExtraProperties[1022];
			}
			set
			{
				base.ExtraProperties[1022] = (long)value;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06002AE5 RID: 10981 RVA: 0x000AE8A6 File Offset: 0x000ACAA6
		// (set) Token: 0x06002AE6 RID: 10982 RVA: 0x000AE8B9 File Offset: 0x000ACAB9
		public int FilterLimiterThreshold
		{
			get
			{
				return (int)base.ExtraProperties[1023];
			}
			set
			{
				base.ExtraProperties[1023] = (long)value;
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06002AE7 RID: 10983 RVA: 0x000AE8CD File Offset: 0x000ACACD
		// (set) Token: 0x06002AE8 RID: 10984 RVA: 0x000AE8E0 File Offset: 0x000ACAE0
		public int FilterPresenceBoost
		{
			get
			{
				return (int)base.ExtraProperties[1030];
			}
			set
			{
				base.ExtraProperties[1030] = (long)value;
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06002AE9 RID: 10985 RVA: 0x000AE8F4 File Offset: 0x000ACAF4
		// (set) Token: 0x06002AEA RID: 10986 RVA: 0x000AE907 File Offset: 0x000ACB07
		public int FilterPresenceRange
		{
			get
			{
				return (int)base.ExtraProperties[1031];
			}
			set
			{
				base.ExtraProperties[1031] = (long)value;
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06002AEB RID: 10987 RVA: 0x000AE91B File Offset: 0x000ACB1B
		// (set) Token: 0x06002AEC RID: 10988 RVA: 0x000AE92E File Offset: 0x000ACB2E
		public int FilterAINRStrength
		{
			get
			{
				return (int)base.ExtraProperties[1000];
			}
			set
			{
				base.ExtraProperties[1000] = (long)value;
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06002AED RID: 10989 RVA: 0x000AE942 File Offset: 0x000ACB42
		// (set) Token: 0x06002AEE RID: 10990 RVA: 0x000AE955 File Offset: 0x000ACB55
		public int FilterHighPassStrength
		{
			get
			{
				return (int)base.ExtraProperties[1010];
			}
			set
			{
				base.ExtraProperties[1010] = (long)value;
			}
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x000AE96C File Offset: 0x000ACB6C
		public MicrophoneQuadCast2S()
		{
			base.Model = HyperXDeviceModel.Microphone_Quadcast2S;
			base.DFUNeedReboot = false;
			base.FramePerSecond = 20;
			base.MaxSyncFrameCount = 120;
			base.Engine.DeviceModel = base.Model;
			this.SetupKeys();
			base.CurrentPresetID = 0;
			base.CanLink = true;
			base.ColorSkin = ColorSkins.BlackBlack;
			this.FilterAINR = false;
			this.FilterHighPass = false;
			this.FilterLimiter = false;
			this.FilterPresence = false;
			this.FilterHighPassStrength = 80;
			this.FilterAINRStrength = 1;
			this.FilterLimiterGain = 12;
			this.FilterLimiterAttack = 1;
			this.FilterLimiterRelease = 1;
			this.FilterLimiterThreshold = -12;
			this.FilterPresenceBoost = 12;
			this.FilterPresenceRange = 16;
			this.MixBalance = 0;
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x000AEA30 File Offset: 0x000ACC30
		private void SetupKeys()
		{
			base.Keys.Add(new KeyMap(KeyCode.TopLed1, 53, 29, 15, 20, 0, 0, 27));
			base.Keys.Add(new KeyMap(KeyCode.TopLed2, 71, 29, 16, 20, 0, 1, 28));
			base.Keys.Add(new KeyMap(KeyCode.TopLed3, 90, 29, 19, 20, 0, 2, 45));
			base.Keys.Add(new KeyMap(KeyCode.TopLed4, 112, 29, 19, 20, 0, 3, 46));
			base.Keys.Add(new KeyMap(KeyCode.TopLed5, 134, 29, 16, 20, 0, 4, 63));
			base.Keys.Add(new KeyMap(KeyCode.TopLed6, 153, 29, 15, 20, 0, 5, 64));
			base.Keys.Add(new KeyMap(KeyCode.TopLed7, 327, 29, 15, 20, 0, 6, 81));
			base.Keys.Add(new KeyMap(KeyCode.TopLed8, 345, 29, 16, 20, 0, 7, 82));
			base.Keys.Add(new KeyMap(KeyCode.TopLed9, 364, 29, 19, 20, 0, 8, 99));
			base.Keys.Add(new KeyMap(KeyCode.TopLed10, 386, 29, 19, 20, 0, 9, 100));
			base.Keys.Add(new KeyMap(KeyCode.TopLed11, 408, 29, 16, 20, 0, 10, 9));
			base.Keys.Add(new KeyMap(KeyCode.TopLed12, 427, 29, 15, 20, 0, 11, 10));
			base.Keys.Add(new KeyMap(KeyCode.TopLed13, 53, 52, 15, 20, 1, 0, 26));
			base.Keys.Add(new KeyMap(KeyCode.TopLed14, 71, 52, 16, 20, 1, 1, 29));
			base.Keys.Add(new KeyMap(KeyCode.TopLed15, 90, 52, 19, 20, 1, 2, 44));
			base.Keys.Add(new KeyMap(KeyCode.TopLed16, 112, 52, 19, 20, 1, 3, 47));
			base.Keys.Add(new KeyMap(KeyCode.TopLed17, 134, 52, 16, 20, 1, 4, 62));
			base.Keys.Add(new KeyMap(KeyCode.TopLed18, 153, 52, 15, 20, 1, 5, 65));
			base.Keys.Add(new KeyMap(KeyCode.TopLed19, 327, 52, 15, 20, 1, 6, 80));
			base.Keys.Add(new KeyMap(KeyCode.TopLed20, 345, 52, 16, 20, 1, 7, 83));
			base.Keys.Add(new KeyMap(KeyCode.TopLed21, 364, 52, 19, 20, 1, 8, 98));
			base.Keys.Add(new KeyMap(KeyCode.TopLed22, 386, 52, 19, 20, 1, 9, 101));
			base.Keys.Add(new KeyMap(KeyCode.TopLed23, 408, 52, 16, 20, 1, 10, 8));
			base.Keys.Add(new KeyMap(KeyCode.TopLed24, 427, 52, 15, 20, 1, 11, 11));
			base.Keys.Add(new KeyMap(KeyCode.TopLed25, 53, 75, 15, 20, 2, 0, 25));
			base.Keys.Add(new KeyMap(KeyCode.TopLed26, 71, 75, 16, 20, 2, 1, 30));
			base.Keys.Add(new KeyMap(KeyCode.TopLed27, 90, 75, 19, 20, 2, 2, 43));
			base.Keys.Add(new KeyMap(KeyCode.TopLed28, 112, 75, 19, 20, 2, 3, 48));
			base.Keys.Add(new KeyMap(KeyCode.TopLed29, 134, 75, 16, 20, 2, 4, 61));
			base.Keys.Add(new KeyMap(KeyCode.TopLed30, 153, 75, 15, 20, 2, 5, 66));
			base.Keys.Add(new KeyMap(KeyCode.TopLed31, 327, 75, 15, 20, 2, 6, 79));
			base.Keys.Add(new KeyMap(KeyCode.TopLed32, 345, 75, 16, 20, 2, 7, 84));
			base.Keys.Add(new KeyMap(KeyCode.TopLed33, 364, 75, 19, 20, 2, 8, 97));
			base.Keys.Add(new KeyMap(KeyCode.TopLed34, 386, 75, 19, 20, 2, 9, 102));
			base.Keys.Add(new KeyMap(KeyCode.TopLed35, 408, 75, 16, 20, 2, 10, 7));
			base.Keys.Add(new KeyMap(KeyCode.TopLed36, 427, 75, 15, 20, 2, 11, 12));
			base.Keys.Add(new KeyMap(KeyCode.TopLed37, 53, 98, 15, 20, 3, 0, 24));
			base.Keys.Add(new KeyMap(KeyCode.TopLed38, 71, 98, 16, 20, 3, 1, 31));
			base.Keys.Add(new KeyMap(KeyCode.TopLed39, 90, 98, 19, 20, 3, 2, 42));
			base.Keys.Add(new KeyMap(KeyCode.TopLed40, 112, 98, 19, 20, 3, 3, 49));
			base.Keys.Add(new KeyMap(KeyCode.TopLed41, 134, 98, 16, 20, 3, 4, 60));
			base.Keys.Add(new KeyMap(KeyCode.TopLed42, 153, 98, 15, 20, 3, 5, 67));
			base.Keys.Add(new KeyMap(KeyCode.TopLed43, 327, 98, 15, 20, 3, 6, 78));
			base.Keys.Add(new KeyMap(KeyCode.TopLed44, 345, 98, 16, 20, 3, 7, 85));
			base.Keys.Add(new KeyMap(KeyCode.TopLed45, 364, 98, 19, 20, 3, 8, 96));
			base.Keys.Add(new KeyMap(KeyCode.TopLed46, 386, 98, 19, 20, 3, 9, 103));
			base.Keys.Add(new KeyMap(KeyCode.TopLed47, 408, 98, 16, 20, 3, 10, 6));
			base.Keys.Add(new KeyMap(KeyCode.TopLed48, 427, 98, 15, 20, 3, 11, 13));
			base.Keys.Add(new KeyMap(KeyCode.TopLed49, 53, 121, 15, 20, 4, 0, 23));
			base.Keys.Add(new KeyMap(KeyCode.TopLed50, 71, 121, 16, 20, 4, 1, 32));
			base.Keys.Add(new KeyMap(KeyCode.TopLed51, 90, 121, 19, 20, 4, 2, 41));
			base.Keys.Add(new KeyMap(KeyCode.TopLed52, 112, 121, 19, 20, 4, 3, 50));
			base.Keys.Add(new KeyMap(KeyCode.TopLed53, 134, 121, 16, 20, 4, 4, 59));
			base.Keys.Add(new KeyMap(KeyCode.TopLed54, 153, 121, 15, 20, 4, 5, 68));
			base.Keys.Add(new KeyMap(KeyCode.TopLed55, 327, 121, 15, 20, 4, 6, 77));
			base.Keys.Add(new KeyMap(KeyCode.TopLed56, 345, 121, 16, 20, 4, 7, 86));
			base.Keys.Add(new KeyMap(KeyCode.TopLed57, 364, 121, 19, 20, 4, 8, 95));
			base.Keys.Add(new KeyMap(KeyCode.TopLed58, 386, 121, 19, 20, 4, 9, 104));
			base.Keys.Add(new KeyMap(KeyCode.TopLed59, 408, 121, 16, 20, 4, 10, 5));
			base.Keys.Add(new KeyMap(KeyCode.TopLed60, 427, 121, 15, 20, 4, 11, 14));
			base.Keys.Add(new KeyMap(KeyCode.TopLed61, 53, 144, 15, 20, 5, 0, 22));
			base.Keys.Add(new KeyMap(KeyCode.TopLed62, 71, 144, 16, 20, 5, 1, 33));
			base.Keys.Add(new KeyMap(KeyCode.TopLed63, 90, 144, 19, 20, 5, 2, 40));
			base.Keys.Add(new KeyMap(KeyCode.TopLed64, 112, 144, 19, 20, 5, 3, 51));
			base.Keys.Add(new KeyMap(KeyCode.TopLed65, 134, 144, 16, 20, 5, 4, 58));
			base.Keys.Add(new KeyMap(KeyCode.TopLed66, 153, 144, 15, 20, 5, 5, 69));
			base.Keys.Add(new KeyMap(KeyCode.TopLed67, 327, 144, 15, 20, 5, 6, 76));
			base.Keys.Add(new KeyMap(KeyCode.TopLed68, 345, 144, 16, 20, 5, 7, 87));
			base.Keys.Add(new KeyMap(KeyCode.TopLed69, 364, 144, 19, 20, 5, 8, 94));
			base.Keys.Add(new KeyMap(KeyCode.TopLed70, 386, 144, 19, 20, 5, 9, 105));
			base.Keys.Add(new KeyMap(KeyCode.TopLed71, 408, 144, 16, 20, 5, 10, 4));
			base.Keys.Add(new KeyMap(KeyCode.TopLed72, 427, 144, 15, 20, 5, 11, 15));
			base.Keys.Add(new KeyMap(KeyCode.TopLed73, 53, 167, 15, 20, 6, 0, 21));
			base.Keys.Add(new KeyMap(KeyCode.TopLed74, 71, 167, 16, 20, 6, 1, 34));
			base.Keys.Add(new KeyMap(KeyCode.TopLed75, 90, 167, 19, 20, 6, 2, 39));
			base.Keys.Add(new KeyMap(KeyCode.TopLed76, 112, 167, 19, 20, 6, 3, 52));
			base.Keys.Add(new KeyMap(KeyCode.TopLed77, 134, 167, 16, 20, 6, 4, 57));
			base.Keys.Add(new KeyMap(KeyCode.TopLed78, 153, 167, 15, 20, 6, 5, 70));
			base.Keys.Add(new KeyMap(KeyCode.TopLed79, 327, 167, 15, 20, 6, 6, 75));
			base.Keys.Add(new KeyMap(KeyCode.TopLed80, 345, 167, 16, 20, 6, 7, 88));
			base.Keys.Add(new KeyMap(KeyCode.TopLed81, 364, 167, 19, 20, 6, 8, 93));
			base.Keys.Add(new KeyMap(KeyCode.TopLed82, 386, 167, 19, 20, 6, 9, 106));
			base.Keys.Add(new KeyMap(KeyCode.TopLed83, 408, 167, 16, 20, 6, 10, 3));
			base.Keys.Add(new KeyMap(KeyCode.TopLed84, 427, 167, 15, 20, 6, 11, 16));
			base.Keys.Add(new KeyMap(KeyCode.TopLed85, 53, 190, 15, 20, 7, 0, 20));
			base.Keys.Add(new KeyMap(KeyCode.TopLed86, 71, 190, 16, 20, 7, 1, 35));
			base.Keys.Add(new KeyMap(KeyCode.TopLed87, 90, 190, 19, 20, 7, 2, 38));
			base.Keys.Add(new KeyMap(KeyCode.TopLed88, 112, 190, 19, 20, 7, 3, 53));
			base.Keys.Add(new KeyMap(KeyCode.TopLed89, 134, 190, 16, 20, 7, 4, 56));
			base.Keys.Add(new KeyMap(KeyCode.TopLed90, 153, 190, 15, 20, 7, 5, 71));
			base.Keys.Add(new KeyMap(KeyCode.TopLed91, 327, 190, 15, 20, 7, 6, 74));
			base.Keys.Add(new KeyMap(KeyCode.TopLed92, 345, 190, 16, 20, 7, 7, 89));
			base.Keys.Add(new KeyMap(KeyCode.TopLed93, 364, 190, 19, 20, 7, 8, 92));
			base.Keys.Add(new KeyMap(KeyCode.TopLed94, 386, 190, 19, 20, 7, 9, 107));
			base.Keys.Add(new KeyMap(KeyCode.TopLed95, 408, 190, 16, 20, 7, 10, 2));
			base.Keys.Add(new KeyMap(KeyCode.TopLed96, 427, 190, 15, 20, 7, 11, 17));
			base.Keys.Add(new KeyMap(KeyCode.TopLed97, 53, 213, 15, 20, 8, 0, 19));
			base.Keys.Add(new KeyMap(KeyCode.TopLed98, 71, 213, 16, 20, 8, 1, 36));
			base.Keys.Add(new KeyMap(KeyCode.TopLed99, 90, 213, 19, 20, 8, 2, 37));
			base.Keys.Add(new KeyMap(KeyCode.TopLed100, 112, 213, 19, 20, 8, 3, 54));
			base.Keys.Add(new KeyMap(KeyCode.TopLed101, 134, 213, 16, 20, 8, 4, 55));
			base.Keys.Add(new KeyMap(KeyCode.TopLed102, 153, 213, 15, 20, 8, 5, 72));
			base.Keys.Add(new KeyMap(KeyCode.TopLed103, 327, 213, 15, 20, 8, 6, 73));
			base.Keys.Add(new KeyMap(KeyCode.TopLed104, 345, 213, 16, 20, 8, 7, 90));
			base.Keys.Add(new KeyMap(KeyCode.TopLed105, 364, 213, 19, 20, 8, 8, 91));
			base.Keys.Add(new KeyMap(KeyCode.TopLed106, 386, 213, 19, 20, 8, 9, 108));
			base.Keys.Add(new KeyMap(KeyCode.TopLed107, 408, 213, 16, 20, 8, 10, 1));
			base.Keys.Add(new KeyMap(KeyCode.TopLed108, 427, 213, 15, 20, 8, 11, 18));
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06002AF1 RID: 10993 RVA: 0x000AF9A4 File Offset: 0x000ADBA4
		protected override bool DeviceIsReady
		{
			get
			{
				return base.Device != null && base.SecondaryDevice != null;
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06002AF2 RID: 10994 RVA: 0x000AF9B9 File Offset: 0x000ADBB9
		public override bool IsOpened
		{
			get
			{
				return base.IsOpened || !string.IsNullOrEmpty(base.SecondaryDeviceID);
			}
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x000AF9D4 File Offset: 0x000ADBD4
		public override void OpenSecondaryDevice(string deviceId)
		{
			if (base.SecondaryDevice != null)
			{
				base.CloseSecondaryDevice();
			}
			HidDevice hidDevice = HidDevice.FromId(deviceId, 3221225472U);
			if (hidDevice != null)
			{
				if (base.VendorID == 0 || base.ProductID == 0)
				{
					base.VendorID = 1008;
					base.ProductID = 693;
				}
				base.SecondaryDevice = hidDevice;
				base.SecondaryDeviceID = deviceId;
				base.OpenSecondaryEndpointNotificationTunnel();
				this.Start();
				if (!HyperXCenter.Center.Updater.UpgradeMode && HyperXCenter.Center.HasNewerBuiltinFirmware(this) && Settings.Instance.ShowNotifications && Settings.Instance.NotifyFirmwareUpdateAvailable)
				{
					Logger.WriteLine("Notify Firmware Update", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\Microphone\\MicrophoneQuadCast2S.cs", 253);
					this.NotifyFirmwareUpdate();
				}
			}
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x000AFA91 File Offset: 0x000ADC91
		public override void Start()
		{
			base.Start();
			this.GetBasicInfo();
			this.ApplyBasicSettings();
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x000AFAA8 File Offset: 0x000ADCA8
		public override bool IsDevice(string deviceId)
		{
			ushort num;
			ushort num2;
			Utils.ParseVIDPID(deviceId, out num, out num2);
			if (!string.IsNullOrEmpty(base.DeviceID))
			{
				ushort num3;
				ushort num4;
				Utils.ParseVIDPID(base.DeviceID, out num3, out num4);
				ushort num5;
				ushort num6;
				Utils.ParseVIDPID("HID#VID_03F0&PID_0D84&MI_03&Col06", out num5, out num6);
				ushort num7;
				Utils.ParseVIDPID("HID#VID_03F0&PID_0EB4", out num5, out num7);
				if (num == num3)
				{
					if (num2 == num4)
					{
						return true;
					}
					if (num2 == num6 || num2 == num7)
					{
						return true;
					}
				}
			}
			if (!string.IsNullOrEmpty(base.SecondaryDeviceID))
			{
				ushort num8;
				ushort num9;
				Utils.ParseVIDPID(base.SecondaryDeviceID, out num8, out num9);
				ushort num10;
				ushort num11;
				Utils.ParseVIDPID("HID#VID_03F0&PID_02B5&MI_01&Col02", out num10, out num11);
				ushort num5;
				ushort num12;
				Utils.ParseVIDPID("HID#VID_03F0&PID_03B5", out num5, out num12);
				if (num == num8)
				{
					if (num2 == num9)
					{
						return true;
					}
					if (num2 == num11 || num2 == num12)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002AF6 RID: 10998 RVA: 0x000AFB68 File Offset: 0x000ADD68
		public override void ChangePolarPattern(PolarPattern pattern)
		{
			if (pattern != base.PolarPattern)
			{
				base.ChangePolarPattern(pattern);
				this.AddCommand(new CMDMICQuadCast2SSetPolarPattern
				{
					PolarPattern = new PolarPattern?(pattern)
				});
			}
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x000AFBA0 File Offset: 0x000ADDA0
		public override bool ApplyExtraProperty(int key, long value)
		{
			if (base.ApplyExtraProperty(key, value))
			{
				if (key == 101)
				{
					this.AddCommand(new CMDMICQuadCast2SSetHighPassState
					{
						Enable = value.Boolean()
					});
				}
				if (key == 200)
				{
					this.AddCommand(new CMDMICQuadCast2SSetBalance
					{
						Level = (int)value
					});
					this._mixBalanceCheckCout++;
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002AF8 RID: 11000 RVA: 0x000AFC03 File Offset: 0x000ADE03
		public override void RetriveUpdateDeviceInfo(List<string> deviceInfo)
		{
			base.RetriveUpdateDeviceInfo(deviceInfo);
			if (!string.IsNullOrEmpty(base.SecondaryDeviceID))
			{
				deviceInfo.Add(base.SecondaryDeviceID);
			}
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x00057B54 File Offset: 0x00055D54
		public override void ApplyBasicSettings()
		{
			base.ApplyBasicSettings();
		}

		// Token: 0x06002AFA RID: 11002 RVA: 0x000AFC25 File Offset: 0x000ADE25
		public override void ApplyShowLights(bool muted)
		{
			base.ApplyShowLights(muted);
			this.ClearLightingCommands();
		}

		// Token: 0x06002AFB RID: 11003 RVA: 0x000AFC34 File Offset: 0x000ADE34
		public override void ApplySidetoneSettings()
		{
			base.ApplySidetoneSettings();
			Preset safePreset = base.GetSafePreset();
			IReadOnlyList<MMDevice> audioDevices = base.AudioDevices;
			lock (audioDevices)
			{
				MMDevice mmdevice = base.AudioDevices.FirstOrDefault((MMDevice x) => x.DataFlow == AudioDeviceDataFlow.Render);
				if (mmdevice != null && safePreset != null)
				{
					mmdevice.SidetoneMuted = safePreset.Microphone.SidetoneMuted;
				}
			}
		}

		// Token: 0x06002AFC RID: 11004 RVA: 0x000AFCC0 File Offset: 0x000ADEC0
		protected override void FlushSyncFramesToDevice(List<List<LightingItem>> frames, int profileIndex)
		{
			base.FlushSyncFramesToDevice(frames, -1);
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x000AFCCC File Offset: 0x000ADECC
		protected override void OnDeviceInputReportReceived(byte[] buffer)
		{
			if (buffer[0] == 119)
			{
				Logger.WriteLine(string.Format("Codec <- {0:X2} {1:X2} {2:X2}", buffer[0], buffer[1], buffer[2]), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\Microphone\\MicrophoneQuadCast2S.cs", 412);
				byte b = buffer[1];
				switch (b)
				{
				case 5:
				{
					PolarPattern? polarPattern = this.ParseToPorlarPattern(buffer[2]);
					if (polarPattern != null)
					{
						this.CheckAndUpdatePolarPattern(polarPattern.Value);
						return;
					}
					break;
				}
				case 6:
					this.CheckAndUpdateMicMuteState(buffer[2] == 1);
					return;
				case 7:
				case 8:
				case 9:
					break;
				case 10:
					if (this._mixBalanceCheckCout == 0)
					{
						this.CheckAndUpdateMixBalance((int)buffer[2]);
						return;
					}
					this._mixBalanceCheckCout--;
					return;
				default:
					switch (b)
					{
					case 129:
						BitConverter.ToString(buffer, 7, 1) + BitConverter.ToString(buffer, 6, 1);
						return;
					case 130:
					case 131:
					case 132:
					case 137:
						break;
					case 133:
					{
						PolarPattern? polarPattern2 = this.ParseToPorlarPattern(buffer[2]);
						if (polarPattern2 != null)
						{
							this.CheckAndUpdatePolarPattern(polarPattern2.Value);
							return;
						}
						break;
					}
					case 134:
						this.CheckAndUpdateMicMuteState(buffer[2] == 1);
						return;
					case 135:
						this.CheckAndUpdateHighPassState(buffer[2] == 1);
						return;
					case 136:
						this.OnMicrophoneGainChanged((int)buffer[2]);
						return;
					case 138:
						this.CheckAndUpdateMixBalance((int)buffer[2]);
						return;
					default:
						return;
					}
					break;
				}
			}
			else
			{
				base.OnDeviceInputReportReceived(buffer);
			}
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x000AFE2C File Offset: 0x000AE02C
		private PolarPattern? ParseToPorlarPattern(byte raw)
		{
			PolarPattern? result = null;
			switch (raw)
			{
			case 0:
				result = new PolarPattern?(PolarPattern.Bidirectional);
				break;
			case 1:
				result = new PolarPattern?(PolarPattern.Cardioid);
				break;
			case 2:
				result = new PolarPattern?(PolarPattern.Omnidirectional);
				break;
			case 3:
				result = new PolarPattern?(PolarPattern.Stereo);
				break;
			}
			return result;
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x000AFE80 File Offset: 0x000AE080
		private void GetBasicInfo()
		{
			Logger.WriteLine("GetBasicInfo", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\Microphone\\MicrophoneQuadCast2S.cs", 498);
			this.AddCommand(new CMDMICQuadCast2SGetDeviceInfo());
			this.AddCommand(new CMDMICQuadCast2SGetPolarPattern());
			this.AddCommand(new CMDMICQuadCast2SGetMicrophoneMuteState());
			this.AddCommand(new CMDMICQuadCast2SGetHighPassState());
			this.AddCommand(new CMDMICQuadCast2SGetMicrophoneGain());
			this.AddCommand(new CMDMICQuadCast2SGetBalance());
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x000AFEE3 File Offset: 0x000AE0E3
		private void CheckAndUpdatePolarPattern(PolarPattern newValue)
		{
			Logger.WriteLine(string.Format("Update Polar Pattern: {0}", newValue), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\Microphone\\MicrophoneQuadCast2S.cs", 509);
			this.OnPolarPatternChanged(newValue);
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x000AFF0B File Offset: 0x000AE10B
		private void CheckAndUpdateMicMuteState(bool muted)
		{
			Logger.WriteLine(string.Format("Update Muted: {0}", muted), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\Microphone\\MicrophoneQuadCast2S.cs", 515);
			this.OnAudioDeviceMuted(AudioDeviceType.Microphone, muted, true);
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x000AFF35 File Offset: 0x000AE135
		private void CheckAndUpdateHighPassState(bool isEnable)
		{
			Logger.WriteLine(string.Format("Update Highpass: {0}", isEnable), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\Microphone\\MicrophoneQuadCast2S.cs", 521);
			if (this.FilterHighPass != isEnable)
			{
				this.FilterHighPass = isEnable;
				HyperXCenter.Center.OnDeviceUpdated(this);
			}
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		private void CheckAndUpdateJackPlugState(bool isPlugged)
		{
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x000AFF71 File Offset: 0x000AE171
		private void CheckAndUpdateMixBalance(int level)
		{
			Logger.WriteLine(string.Format("Update MixBalance: {0}", level), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity.Devices\\Microphone\\MicrophoneQuadCast2S.cs", 540);
			if (this.MixBalance != level)
			{
				this.MixBalance = level;
				HyperXCenter.Center.OnDeviceUpdated(this);
			}
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x000AFFB0 File Offset: 0x000AE1B0
		public override void AddAudioDevice(MMDevice device)
		{
			if (device.DataFlow == AudioDeviceDataFlow.Render)
			{
				device.InitSystemSidetone();
				this.SidetoneMuted = device.SidetoneMuted;
				this.SidetoneVolume = device.SidetoneVolume;
			}
			base.AddAudioDevice(device);
			if (device.DataFlow == AudioDeviceDataFlow.Capture)
			{
				device.AudioMeterUpdated += this.MMDevice_AudioMeterUpdated;
			}
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x000B0005 File Offset: 0x000AE205
		public override void RemoveAudioDevice(MMDevice device)
		{
			if (device.DataFlow == AudioDeviceDataFlow.Capture)
			{
				device.AudioMeterUpdated -= this.MMDevice_AudioMeterUpdated;
			}
			base.RemoveAudioDevice(device);
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x000B0029 File Offset: 0x000AE229
		private void MMDevice_AudioMeterUpdated(MMDevice device, float peakVolume)
		{
			this.OnAudioMeterUpdated(AudioDeviceType.Microphone, peakVolume);
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		protected override void SetupKeyLayouts(DeviceInforPayload deviceInformation)
		{
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x000B0033 File Offset: 0x000AE233
		protected override int GetMaxLedCount()
		{
			return 108;
		}

		// Token: 0x06002B0A RID: 11018 RVA: 0x0000FBE7 File Offset: 0x0000DDE7
		public string GetDFUEndpoint()
		{
			return null;
		}

		// Token: 0x06002B0B RID: 11019 RVA: 0x0000FBE7 File Offset: 0x0000DDE7
		public string GetUpdateFileName()
		{
			return null;
		}

		// Token: 0x06002B0C RID: 11020 RVA: 0x00015297 File Offset: 0x00013497
		public bool IsDFUMode()
		{
			return false;
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x000B0037 File Offset: 0x000AE237
		public FirmwareODM GetODM()
		{
			return FirmwareODM.Merry;
		}

		// Token: 0x0400214F RID: 8527
		private const int PTY_AINR = 100;

		// Token: 0x04002150 RID: 8528
		private const int PTY_HIGHPASS = 101;

		// Token: 0x04002151 RID: 8529
		private const int PTY_LIMITER = 102;

		// Token: 0x04002152 RID: 8530
		private const int PTY_PRESENCE = 103;

		// Token: 0x04002153 RID: 8531
		private const int PTY_MONITOR_PLAYBACK_BALANCE = 200;

		// Token: 0x04002154 RID: 8532
		private const int PTY_AINR_STRENGTH = 1000;

		// Token: 0x04002155 RID: 8533
		private const int PTY_HIGHPASS_FREQUENCY = 1010;

		// Token: 0x04002156 RID: 8534
		private const int PTY_LIMITER_GAIN = 1020;

		// Token: 0x04002157 RID: 8535
		private const int PTY_LIMITER_ATTACK = 1021;

		// Token: 0x04002158 RID: 8536
		private const int PTY_LIMITER_RELEASE = 1022;

		// Token: 0x04002159 RID: 8537
		private const int PTY_LIMITER_THRESHOLD = 1023;

		// Token: 0x0400215A RID: 8538
		private const int PTY_PRESENCE_BOOST = 1030;

		// Token: 0x0400215B RID: 8539
		private const int PTY_PRESENCE_RANGE = 1031;

		// Token: 0x0400215C RID: 8540
		private int _mixBalanceCheckCout;
	}
}```

# MicrophoneQuadCastS
```csharp
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using NGenuity2.Audio;
using NGenuity2.Common;
using NGenuity2.Common.Devices;
using NGenuity2.Model;
using NGenuity2.Updaters;

namespace NGenuity2.Devices.Microphone
{
	// Token: 0x0200078A RID: 1930
	public class MicrophoneQuadCastS : HyperXMicrophoneDevice<CMDMICQuadCastSCommandBase, CMDMICQuadCastSShowLighting>
	{
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06002B93 RID: 11155 RVA: 0x000B2380 File Offset: 0x000B0580
		// (set) Token: 0x06002B94 RID: 11156 RVA: 0x000B2388 File Offset: 0x000B0588
		public Version HeadsetVersion { get; set; }

		// Token: 0x06002B95 RID: 11157 RVA: 0x000B2394 File Offset: 0x000B0594
		public MicrophoneQuadCastS()
		{
			base.Model = HyperXDeviceModel.Microphone_QuadcastS;
			base.DFUNeedReboot = false;
			base.FramePerSecond = 25;
			base.MaxSyncFrameCount = 720;
			base.Engine.DeviceModel = base.Model;
			this.SetupKeys();
			base.CurrentPresetID = 4;
			this.RequiredVersions = string.Format("{0}|{1}", HyperXCenter.Center.GetMinimunMainFirmwareVersion(156309279U), HyperXCenter.Center.GetMinimunRFHostFirmwareVersion(156309279U));
			base.CanLink = true;
			base.ColorSkin = ColorSkins.BlackBlack;
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x000B2428 File Offset: 0x000B0628
		private void SetupKeys()
		{
			base.Keys.Add(new KeyMap(KeyCode.TopLed1, 433, 29, 24, 49, 0, 0, 0, 0));
			base.Keys.Add(new KeyMap(KeyCode.TopLed2, 410, 223, 56, 30, 1, 1, 1, 0));
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06002B97 RID: 11159 RVA: 0x000633A8 File Offset: 0x000615A8
		protected override bool DeviceIsReady
		{
			get
			{
				return base.Device != null && base.NotificationDevice != null;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06002B98 RID: 11160 RVA: 0x00085898 File Offset: 0x00083A98
		public override bool IsOpened
		{
			get
			{
				return base.IsOpened || !string.IsNullOrEmpty(base.NotificationDeviceID);
			}
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x000B2480 File Offset: 0x000B0680
		protected override void InitDevice()
		{
			if (this.IsSecondSource())
			{
				CMDMICQuadcastSGetDeviceStatus2ndSource cmdmicquadcastSGetDeviceStatus2ndSource = new CMDMICQuadcastSGetDeviceStatus2ndSource();
				cmdmicquadcastSGetDeviceStatus2ndSource.Execute(this);
				base.PolarPattern = cmdmicquadcastSGetDeviceStatus2ndSource.Pattern;
				this.MicrophoneMuted = cmdmicquadcastSGetDeviceStatus2ndSource.Muted;
				base.ReverseLights = cmdmicquadcastSGetDeviceStatus2ndSource.ReverseLights;
			}
			else
			{
				CMDMICQuadCastSGetDeviceStatus cmdmicquadCastSGetDeviceStatus = new CMDMICQuadCastSGetDeviceStatus();
				cmdmicquadCastSGetDeviceStatus.Execute(this);
				base.PolarPattern = cmdmicquadCastSGetDeviceStatus.Pattern;
				this.MicrophoneMuted = cmdmicquadCastSGetDeviceStatus.Muted;
				CMDMICQuadcastSGetCurrentProfile cmdmicquadcastSGetCurrentProfile = new CMDMICQuadcastSGetCurrentProfile();
				cmdmicquadcastSGetCurrentProfile.Execute(this);
				base.ReverseLights = cmdmicquadcastSGetCurrentProfile.ReverseLights;
			}
			this.AddCommand(new CMDMICQuadcastSSetCurrentProfile
			{
				Brightness = (byte)Settings.Instance.MicrophoneBrightness,
				ReverseLights = base.ReverseLights
			});
			this.ApplyBasicSettings();
			if (!this.UpgradeMode)
			{
				base.StartEffectEngine();
			}
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x000B2544 File Offset: 0x000B0744
		public override void CloseMainDevice()
		{
			string deviceID = base.DeviceID;
			base.CloseMainDevice();
			base.DeviceID = deviceID;
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x000B2568 File Offset: 0x000B0768
		private void CheckHeadsetVersion()
		{
			string version = new QuadcastUpdater().CheckVersion();
			this.HeadsetVersion = new Version(version);
			Version minimunRFHostFirmwareVersion = HyperXCenter.Center.GetMinimunRFHostFirmwareVersion(156309279U);
			Version minimunMainFirmwareVersion = HyperXCenter.Center.GetMinimunMainFirmwareVersion(156309279U);
			if (this.HeadsetVersion.CompareTo(minimunRFHostFirmwareVersion) < 0)
			{
				this.UpgradeMode = true;
				return;
			}
			this.RequiredVersions = string.Format("{0}|0.0.0.0", minimunMainFirmwareVersion);
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x000B25D4 File Offset: 0x000B07D4
		private bool UpdateHeadsetFirmwareAsync()
		{
			string fwPath = string.Empty;
			fwPath = Path.Combine(Utils.InstalledLocation, "Assets\\Firmware\\0951171D.bin");
			return new QuadcastUpdater().UpdateCMediaCodecFirmware(fwPath);
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x000B2604 File Offset: 0x000B0804
		public override void OpenDevice(string deviceId)
		{
			base.OpenDevice(deviceId);
			base.SendDiscordCertificateDeviceNotificationAsync(null);
			if (Utils.IsCultureInvariantMatch(deviceId, new string[]
			{
				"HID#VID_0951&PID_1720",
				"HID#VID_0951&PID_1774"
			}))
			{
				this.UpgradeMode = true;
			}
			if (this.UpgradeMode)
			{
				this.NotifyFirmwareUpdate();
			}
			base.UpdateID();
			if (!this.UpgradeMode && base.NotificationDevice == null && !string.IsNullOrEmpty(base.NotificationDeviceID))
			{
				base.OpenNotification(base.NotificationDeviceID);
			}
			if (this.IsOpened && !this.UpgradeMode && !HyperXCenter.Center.HasNewerBuiltinFirmware(this) && this.DeviceIsReady)
			{
				this.SetupDevice();
			}
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x000B26AC File Offset: 0x000B08AC
		public override void AddAudioDevice(MMDevice device)
		{
			if (device.DataFlow == AudioDeviceDataFlow.Render)
			{
				device.InitSystemSidetone();
			}
			base.AddAudioDevice(device);
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x00057B54 File Offset: 0x00055D54
		public override void ApplyBasicSettings()
		{
			base.ApplyBasicSettings();
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x000B26C4 File Offset: 0x000B08C4
		public override void ApplyShowLights(bool muted)
		{
			base.ApplyShowLights(muted);
			this.ClearLightingCommands();
			this.AddCommand(new CMDMICQuadcastSSetCurrentProfile
			{
				Brightness = (byte)Settings.Instance.MicrophoneBrightness,
				ReverseLights = base.ReverseLights
			});
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public override void RequestAck()
		{
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x000B2708 File Offset: 0x000B0908
		public override bool WaitAck()
		{
			Thread.Sleep(10);
			return true;
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x000B2714 File Offset: 0x000B0914
		public override void OpenNotification(string deviceId)
		{
			if (base.Device != null)
			{
				base.OpenNotification(deviceId);
			}
			else
			{
				base.NotificationDeviceID = deviceId;
			}
			if (this.IsOpened && !this.UpgradeMode && !HyperXCenter.Center.HasNewerBuiltinFirmware(this) && this.DeviceIsReady)
			{
				this.SetupDevice();
			}
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x000B2764 File Offset: 0x000B0964
		protected override void OnDeviceInputReportReceived(byte[] buffer)
		{
			base.OnDeviceInputReportReceived(buffer);
			if (buffer[0] == 5 && buffer[1] == 17)
			{
				PolarPattern pattern = (PolarPattern)buffer[2];
				this.OnPolarPatternChanged(pattern);
			}
			if (buffer[0] == 5 && buffer[1] == 16)
			{
				bool muted = buffer[2] == 0;
				this.OnAudioDeviceMuted(AudioDeviceType.Microphone, muted, true);
			}
			if (buffer[0] == 5)
			{
				byte b = buffer[1];
			}
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x000B27BC File Offset: 0x000B09BC
		protected override void FlushSyncFramesToDevice(List<List<LightingItem>> frames, int presetId)
		{
			base.FlushSyncFramesToDevice(frames, presetId);
			Color[][] array = new Color[frames.Count][];
			int i;
			int num;
			for (i = 0; i < frames.Count; i = num + 1)
			{
				array[i] = new Color[2];
				List<KeyMap> keys = base.Keys;
				bool flag = false;
				try
				{
					Monitor.Enter(keys, ref flag);
					int j;
					for (j = 0; j < frames[i].Count; j = num + 1)
					{
						KeyMap keyMap = Enumerable.FirstOrDefault<KeyMap>(base.Keys, (KeyMap o) => o.Column == frames[i][j].X && o.Row == frames[i][j].Y);
						if (keyMap != null)
						{
							array[i][(int)keyMap.LEDMatrix.X] = frames[i][j].Color;
						}
						num = j;
					}
				}
				finally
				{
					if (flag)
					{
						Monitor.Exit(keys);
					}
				}
				num = i;
			}
			HXCommandCollection<CMDMICQuadCastSCommandBase> hxcommandCollection = new HXCommandCollection<CMDMICQuadCastSCommandBase>();
			hxcommandCollection.AddCommand(new CMDMICQuadCastSSync(array)
			{
				Handler = new HXCommandHandler(this.OnUpdateSyncProgress),
				Interval = (byte)(1000 / base.FramePerSecond)
			});
			hxcommandCollection.Handler = new HXCommandHandler(this.OnPresetSynced);
			this.AddCommand(hxcommandCollection);
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x00063880 File Offset: 0x00061A80
		protected override void RenderFrameToDevice(List<LightingItem> items)
		{
			base.RenderFrameToDevice(items);
			this.SetLightings(base.Keys);
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x000B2984 File Offset: 0x000B0B84
		public override void ChangeBrightness(int brightness)
		{
			base.ChangeBrightness(brightness);
			this.ClearLightingCommands();
			this.AddCommand(new CMDMICQuadcastSSetCurrentProfile
			{
				Brightness = (byte)brightness,
				ReverseLights = base.ReverseLights
			});
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x000B29C0 File Offset: 0x000B0BC0
		public override void SetLightings(IList<KeyMap> keys)
		{
			base.SetLightings(keys);
			CMDMICQuadCastSShowLighting cmdmicquadCastSShowLighting = new CMDMICQuadCastSShowLighting();
			lock (keys)
			{
				foreach (KeyMap keyMap in keys)
				{
					cmdmicquadCastSShowLighting.Colors[(int)keyMap.LEDMatrix.X] = keyMap.Color;
				}
			}
			this.AddCommand(cmdmicquadCastSShowLighting);
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x000B2A58 File Offset: 0x000B0C58
		private bool IsSecondSource()
		{
			return Utils.IsCultureInvariantMatch(base.DeviceID, new string[]
			{
				"HID#VID_03F0&PID_028C&MI_00|HID#VID_03F0&PID_068C&MI_00|HID#VID_03F0&PID_028C&MI_01&Col02|HID#VID_03F0&PID_068C&MI_01&Col02"
			});
		}
	}
}```

# MicrophoneQuadCastS2S
```csharp
using System;

namespace NGenuity2.Devices.Microphone
{
	// Token: 0x0200078B RID: 1931
	public class MicrophoneQuadCastS2S : MicrophoneQuadCastS
	{
		// Token: 0x06002BAA RID: 11178 RVA: 0x000B2A73 File Offset: 0x000B0C73
		public MicrophoneQuadCastS2S()
		{
			this.RequiredVersions = string.Format("{0}|{1}", HyperXCenter.Center.GetMinimunMainFirmwareVersion(66060940U), HyperXCenter.Center.GetMinimunRFHostFirmwareVersion(66060940U));
		}
	}
}```

# MicrophoneQuadCastSWhite
```csharp
using System;
using NGenuity2.Common.Devices;

namespace NGenuity2.Devices.Microphone
{
	// Token: 0x0200078D RID: 1933
	public class MicrophoneQuadCastSWhite : MicrophoneQuadCastS
	{
		// Token: 0x06002BAC RID: 11180 RVA: 0x000B2AE6 File Offset: 0x000B0CE6
		public MicrophoneQuadCastSWhite()
		{
			this.RequiredVersions = string.Format("{0}|{1}", HyperXCenter.Center.GetMinimunMainFirmwareVersion(66061452U), HyperXCenter.Center.GetMinimunRFHostFirmwareVersion(66061452U));
			base.ColorSkin = ColorSkins.WhiteWhite;
		}
	}
}```

# MicrophoneQuadCastSWhite2S
```csharp
using System;
using NGenuity2.Common.Devices;

namespace NGenuity2.Devices.Microphone
{
	// Token: 0x0200078C RID: 1932
	public class MicrophoneQuadCastSWhite2S : MicrophoneQuadCastS
	{
		// Token: 0x06002BAB RID: 11179 RVA: 0x000B2AA9 File Offset: 0x000B0CA9
		public MicrophoneQuadCastSWhite2S()
		{
			this.RequiredVersions = string.Format("{0}|{1}", HyperXCenter.Center.GetMinimunMainFirmwareVersion(66061964U), HyperXCenter.Center.GetMinimunRFHostFirmwareVersion(66061964U));
			base.ColorSkin = ColorSkins.WhiteWhite;
		}
	}
}```

# QuadCast2SUpdater
```csharp
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using NGenuity2.Common;
using NGenuity2.Devices;

namespace NGenuity2.Updaters
{
	// Token: 0x02000516 RID: 1302
	public class QuadCast2SUpdater
	{
		// Token: 0x06001527 RID: 5415 RVA: 0x0001E6B8 File Offset: 0x0001C8B8
		public bool UpdateCodec(string fwPath)
		{
			List<string> list = HidDevice.FindHidDevice(new string[]
			{
				"HID#VID_03F0&PID_0D84&MI_03&Col06",
				"HID#VID_03F0&PID_0EB4"
			});
			bool flag = false;
			foreach (string path in list)
			{
				using (HidDevice hidDevice = new HidDevice(path, 3221225472U, 2147483648U))
				{
					int ver;
					int.TryParse(string.Format("{0:X4}", hidDevice.Version), out ver);
					if (HyperXCenter.Center.DeviceHasNewerVersion(ver, hidDevice.VendorId, hidDevice.ProductId))
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				Logger.WriteLine("No need to update QuadCast 2S Codec.", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Updaters\\QuadCast2SUpdater.cs", 36);
				return true;
			}
			string text = string.Empty;
			ushort num;
			ushort num2;
			Utils.ParseVIDPID("HID#VID_03F0&PID_02B5&MI_01&Col02", out num, out num2);
			foreach (string text2 in DeviceDetection.GetUSBDeviceIDs())
			{
				ushort num3;
				ushort num4;
				Utils.ParseVIDPID(text2, out num3, out num4);
				if (num3 == num && num4 == num2)
				{
					string[] array = text2.Split(new char[]
					{
						'\\'
					});
					text = array[array.Length - 1];
					break;
				}
			}
			Logger.WriteLine("Updating Quadcast 2S Codec...", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Updaters\\QuadCast2SUpdater.cs", 57);
			string text3 = string.Empty;
			foreach (string text4 in list)
			{
				if (Utils.IsCultureInvariantMatch(text4, new string[]
				{
					"HID#VID_03F0&PID_0D84&MI_03&Col06"
				}))
				{
					text3 = "8";
					break;
				}
				if (Utils.IsCultureInvariantMatch(text4, new string[]
				{
					"HID#VID_03F0&PID_0EB4"
				}))
				{
					text3 = "9";
					break;
				}
			}
			if (string.IsNullOrEmpty(text3))
			{
				Logger.WriteLine("QuadCast 2S codec not found. Ignore codec update.", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Updaters\\QuadCast2SUpdater.cs", 77);
				return true;
			}
			HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_Quadcast2S, UpdateFirmwareState.UpdatingProduct, 120000);
			string text5 = Path.Combine(Utils.LocalFolder, "Logs");
			using (Process process = new Process())
			{
				process.StartInfo.FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CodecFWUpdater.exe");
				process.StartInfo.Arguments = ((text != string.Empty) ? string.Concat(new string[]
				{
					"-t ",
					text3,
					" -c \"",
					fwPath,
					"\" -l \"",
					text5,
					"\" -sn \"",
					text,
					"\""
				}) : string.Concat(new string[]
				{
					"-t ",
					text3,
					" -c \"",
					fwPath,
					"\" -l \"",
					text5,
					"\""
				}));
				process.StartInfo.UseShellExecute = true;
				process.StartInfo.CreateNoWindow = false;
				process.StartInfo.Verb = "runas";
				HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_Quadcast2S, UpdateFirmwareState.Updating, 200);
				process.Start();
				HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_Quadcast2S, UpdateFirmwareState.Updating, 400);
				if (process.WaitForExit(30000))
				{
					Logger.WriteLine(string.Format("Updating Quadcast 2S Codec, return code: {0}", process.ExitCode), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Updaters\\QuadCast2SUpdater.cs", 97);
					if (process.ExitCode != 0 && process.ExitCode != -1073740791)
					{
						Logger.WriteLine(string.Format("Failed to update QuadCast 2S Codec. {0}", process.ExitCode), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Updaters\\QuadCast2SUpdater.cs", 101);
						HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_Quadcast2S, UpdateFirmwareState.Failure, 0);
						return false;
					}
				}
				else
				{
					Logger.WriteLine("Updating Quadcast 2S Codec timed out, kill the process!", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Updaters\\QuadCast2SUpdater.cs", 108);
					process.Kill();
				}
			}
			HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_Quadcast2S, UpdateFirmwareState.Updating, 999);
			HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_Quadcast2S, UpdateFirmwareState.Normal, 1000);
			Thread.Sleep(1000);
			return true;
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x0001EB5C File Offset: 0x0001CD5C
		public bool UpdateController(string fwPath)
		{
			HyperXDeviceModel updateModel = HyperXDeviceModel.Microphone_Quadcast2S;
			byte b = 0;
			string path = string.Empty;
			List<string> list = HidDevice.FindHidDevice(new string[]
			{
				"VID_03F0&PID_02B5&MI_00",
				"HID#VID_03F0&PID_03B5"
			});
			bool flag = false;
			foreach (string text in list)
			{
				using (HidDevice hidDevice = new HidDevice(text, 3221225472U, 2147483648U))
				{
					int ver;
					int.TryParse(string.Format("{0:X4}", hidDevice.Version), out ver);
					if (HyperXCenter.Center.DeviceHasNewerVersion(ver, hidDevice.VendorId, hidDevice.ProductId))
					{
						path = text;
						flag = true;
						break;
					}
				}
			}
			if (!flag)
			{
				Logger.WriteLine("No need to update QuadCast 2S Controller.", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Updaters\\QuadCast2SUpdater.cs", 143);
				return true;
			}
			HyperXCenter.Center.Updater.OnUpdating(updateModel, UpdateFirmwareState.UpdatingProduct, 220000);
			byte[] array = File.ReadAllBytes(Path.Combine(fwPath, "03F002B5.bin"));
			int num = array.Length / 64;
			int num2 = 20;
			if (array.Length % 64 > 0)
			{
				num++;
			}
			HidDevice hidDevice2 = new HidDevice(path, 3221225472U, 2147483648U);
			HyperXCenter.Center.Updater.OnUpdating(updateModel, UpdateFirmwareState.Updating, 0);
			int num3 = 0;
			double num4 = (double)(4 + num);
			num4 += (double)num2;
			byte[] array2 = hidDevice2.CreateFeatureReportBuffer();
			array2[0] = b;
			array2[1] = 170;
			array2[2] = 85;
			array2[3] = 165;
			array2[4] = 90;
			array2[5] = byte.MaxValue;
			array2[6] = 0;
			array2[7] = 51;
			array2[8] = 204;
			hidDevice2.SetFeatureReport(array2);
			Thread.Sleep(50);
			num3++;
			HyperXCenter.Center.Updater.OnUpdating(updateModel, UpdateFirmwareState.Updating, (int)((double)num3 / num4 * 1000.0));
			Utils.ZeroBuffer(array2);
			array2[0] = b;
			array2[1] = 1;
			array2[2] = 170;
			array2[3] = 85;
			Utils.ConvertShort2Bytes(num, array2, 9);
			hidDevice2.SetFeatureReport(array2);
			Thread.Sleep(50);
			num3++;
			HyperXCenter.Center.Updater.OnUpdating(updateModel, UpdateFirmwareState.Updating, (int)((double)num3 / num4 * 1000.0));
			Utils.ZeroBuffer(array2);
			array2[0] = b;
			array2[1] = 2;
			array2[2] = 170;
			array2[3] = 85;
			hidDevice2.SetFeatureReport(array2);
			Thread.Sleep(50);
			num3++;
			HyperXCenter.Center.Updater.OnUpdating(updateModel, UpdateFirmwareState.Updating, (int)((double)num3 / num4 * 1000.0));
			Utils.ZeroBuffer(array2);
			array2[0] = b;
			array2[1] = 3;
			array2[2] = 170;
			array2[3] = 85;
			array2[5] = byte.MaxValue;
			array2[6] = byte.MaxValue;
			array2[7] = byte.MaxValue;
			array2[8] = byte.MaxValue;
			hidDevice2.SetFeatureReport(array2);
			Thread.Sleep(50);
			num3++;
			HyperXCenter.Center.Updater.OnUpdating(updateModel, UpdateFirmwareState.Updating, (int)((double)num3 / num4 * 1000.0));
			Utils.ZeroBuffer(array2);
			array2[0] = b;
			array2[1] = 4;
			array2[2] = 170;
			array2[3] = 85;
			array2[9] = 128;
			hidDevice2.SetFeatureReport(array2);
			for (int i = 0; i < num2; i++)
			{
				Thread.Sleep(10);
				num3++;
				HyperXCenter.Center.Updater.OnUpdating(updateModel, UpdateFirmwareState.Updating, (int)((double)num3 / num4 * 1000.0));
			}
			Utils.ZeroBuffer(array2);
			array2[0] = b;
			array2[1] = 5;
			array2[2] = 170;
			array2[3] = 85;
			Utils.ConvertShort2Bytes(num, array2, 9);
			hidDevice2.SetFeatureReport(array2);
			Thread.Sleep(50);
			num3++;
			HyperXCenter.Center.Updater.OnUpdating(updateModel, UpdateFirmwareState.Updating, (int)((double)num3 / num4 * 1000.0));
			for (int j = 0; j < num; j++)
			{
				Utils.ZeroBuffer(array2);
				array2[0] = b;
				Array.Copy(array, j * 64, array2, 1, Math.Min(64, array.Length - j * 64));
				hidDevice2.SetFeatureReport(array2);
				Thread.Sleep(10);
				num3++;
				HyperXCenter.Center.Updater.OnUpdating(updateModel, UpdateFirmwareState.Updating, (int)((double)num3 / num4 * 1000.0));
			}
			Utils.ZeroBuffer(array2);
			array2[0] = b;
			array2[1] = 7;
			array2[2] = 170;
			array2[3] = 85;
			hidDevice2.SetFeatureReport(array2);
			Thread.Sleep(50);
			num3++;
			hidDevice2.Dispose();
			HyperXCenter.Center.Updater.OnUpdating(updateModel, UpdateFirmwareState.Success, 1000);
			Thread.Sleep(1000);
			return true;
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0001F044 File Offset: 0x0001D244
		public bool UpdateFirmware(string fwPath)
		{
			if (!this.UpdateCodec(fwPath))
			{
				return false;
			}
			if (!this.UpdateController(fwPath))
			{
				return false;
			}
			HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_Quadcast2S, UpdateFirmwareState.Success, 1000);
			return true;
		}

		// Token: 0x0400178A RID: 6026
		private const string CONTROLLER_BIN_FILE_NAME = "03F002B5.bin";
	}
}```

# QuadCast2Updater
```csharp
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using NGenuity2.Common;
using NGenuity2.Devices;

namespace NGenuity2.Updaters
{
	// Token: 0x02000517 RID: 1303
	public class QuadCast2Updater
	{
		// Token: 0x0600152B RID: 5419 RVA: 0x0001F07B File Offset: 0x0001D27B
		public QuadCast2Updater(List<string> deviceIds)
		{
			this._deviceIds = deviceIds;
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x0001F08C File Offset: 0x0001D28C
		public bool UpdateCodec(string fwPath)
		{
			string text = string.Empty;
			foreach (string text2 in this._deviceIds)
			{
				if (Utils.IsCultureInvariantMatch(text2, new string[]
				{
					"HID#VID_03F0&PID_07AF&MI_03&Col04"
				}))
				{
					text = "6";
					break;
				}
				if (Utils.IsCultureInvariantMatch(text2, new string[]
				{
					"HID#VID_03F0&PID_07B4&MI_02&Col04"
				}))
				{
					text = "7";
					break;
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				Logger.WriteLine("QuadCast 2 codec not found. Ignore codec update.", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Updaters\\QuadCast2Updater.cs", 43);
				return true;
			}
			HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadCast2, UpdateFirmwareState.UpdatingProduct, 120000);
			using (Process process = new Process())
			{
				process.StartInfo.FileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CodecFWUpdater.exe");
				process.StartInfo.Arguments = string.Concat(new string[]
				{
					"-t ",
					text,
					" -c \"",
					fwPath,
					"\""
				});
				process.StartInfo.UseShellExecute = true;
				process.StartInfo.CreateNoWindow = false;
				process.StartInfo.Verb = "runas";
				HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadCast2, UpdateFirmwareState.Updating, 200);
				process.Start();
				HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadCast2, UpdateFirmwareState.Updating, 400);
				process.WaitForExit();
				if (process.ExitCode != 0 && process.ExitCode != -1073740791)
				{
					Logger.WriteLine(string.Format("Failed to update QuadCast 2 Codec. {0}", process.ExitCode), "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Updaters\\QuadCast2Updater.cs", 64);
					HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadCast2, UpdateFirmwareState.Failure, 0);
					return false;
				}
			}
			HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadCast2, UpdateFirmwareState.Updating, 999);
			HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadCast2, UpdateFirmwareState.Normal, 1000);
			Thread.Sleep(1000);
			return true;
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x0001F2E0 File Offset: 0x0001D4E0
		public bool UpdateController(string fwPath)
		{
			HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadCast2, UpdateFirmwareState.UpdatingProduct, 220000);
			List<string> list = this.FindQuadCast2ControllerDFUHidDevice();
			if (list.Count == 0)
			{
				List<string> list2 = HidDevice.FindHidDevice(new string[]
				{
					"HID#VID_03F0&PID_09AF&MI_00"
				});
				if (list2.Count == 0)
				{
					Logger.WriteLine("Device not found.", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Updaters\\QuadCast2Updater.cs", 87);
					HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadCast2, UpdateFirmwareState.Failure, 0);
					return false;
				}
				using (HidDevice hidDevice = new HidDevice(list2[0], 3221225472U, 2147483648U))
				{
					if (hidDevice.IsValid && !hidDevice.SetFeatureReport(new byte[]
					{
						0,
						234
					}))
					{
						Logger.WriteLine("Failed to enter DFU mode.", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Updaters\\QuadCast2Updater.cs", 98);
						HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadCast2, UpdateFirmwareState.Failure, 0);
						return false;
					}
					goto IL_106;
				}
				IL_F5:
				Thread.Sleep(500);
				list = this.FindQuadCast2ControllerDFUHidDevice();
				IL_106:
				if (list.Count != 0)
				{
					goto IL_10E;
				}
				goto IL_F5;
			}
			IL_10E:
			HidDevice hidDevice2 = new HidDevice(list[0], 3221225472U, 2147483648U);
			if (!hidDevice2.IsValid)
			{
				Logger.WriteLine("Failed to initialize DFU device. Path: " + list[0], "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Updaters\\QuadCast2Updater.cs", 116);
				HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadCast2, UpdateFirmwareState.Failure, 0);
				return false;
			}
			byte[] array = File.ReadAllBytes(Path.Combine(fwPath, "03F009AF.bin"));
			int num = (array.Length + 59) / 60;
			int num2 = (array.Length + 255) / 256;
			uint num3 = 134234112U;
			int num4 = 0;
			int num5 = num + 4;
			byte[] array2 = hidDevice2.CreateOutputReportBuffer();
			array2[1] = 194;
			array2[5] = (byte)num3;
			array2[6] = (byte)(num3 >> 8);
			array2[7] = (byte)(num3 >> 16);
			array2[8] = (byte)(num3 >> 24);
			array2[9] = (byte)num2;
			array2[10] = (byte)(num2 >> 8);
			array2[11] = (byte)(num2 >> 16);
			array2[12] = (byte)(num2 >> 24);
			hidDevice2.SetOutputReport(array2);
			num4++;
			HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadCast2, UpdateFirmwareState.Updating, (int)((double)num4 / (double)num5 * 1000.0));
			Thread.Sleep(1000);
			hidDevice2.GetInputReport();
			Utils.ZeroBuffer(array2);
			array2[1] = 195;
			array2[5] = (byte)num3;
			array2[6] = (byte)(num3 >> 8);
			array2[7] = (byte)(num3 >> 16);
			array2[8] = (byte)(num3 >> 24);
			array2[9] = (byte)array.Length;
			array2[10] = (byte)(array.Length >> 8);
			array2[11] = (byte)(array.Length >> 16);
			array2[12] = (byte)(array.Length >> 24);
			hidDevice2.SetOutputReport(array2);
			Thread.Sleep(100);
			hidDevice2.GetInputReport();
			num4++;
			HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadCast2, UpdateFirmwareState.Updating, (int)((double)num4 / (double)num5 * 1000.0));
			for (int i = 0; i < num; i++)
			{
				int num6 = array.Length - 60 * i;
				if (num6 > 60)
				{
					num6 = 60;
				}
				Utils.ZeroBuffer(array2);
				array2[1] = 196;
				array2[3] = (byte)num6;
				array2[4] = (byte)(num6 >> 8);
				Array.Copy(array, i * 60, array2, 5, num6);
				hidDevice2.SetOutputReport(array2);
				Thread.Sleep(50);
				hidDevice2.GetInputReport();
				num4++;
				HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadCast2, UpdateFirmwareState.Updating, (int)((double)num4 / (double)num5 * 1000.0));
			}
			Utils.ZeroBuffer(array2);
			array2[1] = 204;
			hidDevice2.SetOutputReport(array2);
			Thread.Sleep(50);
			hidDevice2.GetInputReport();
			num4++;
			HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadCast2, UpdateFirmwareState.Updating, (int)((double)num4 / (double)num5 * 1000.0));
			Utils.ZeroBuffer(array2);
			array2[1] = 200;
			hidDevice2.SetOutputReport(array2);
			Thread.Sleep(50);
			hidDevice2.GetInputReport();
			num4++;
			HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadCast2, UpdateFirmwareState.Updating, (int)((double)num4 / (double)num5 * 1000.0));
			hidDevice2.Dispose();
			HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadCast2, UpdateFirmwareState.Normal, 1000);
			Thread.Sleep(1000);
			return true;
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x0001F760 File Offset: 0x0001D960
		public bool UpdateFirmware(string fwPath)
		{
			if (!this.UpdateCodec(fwPath))
			{
				return false;
			}
			if (!this.UpdateController(fwPath))
			{
				return false;
			}
			HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadCast2, UpdateFirmwareState.Success, 1000);
			return true;
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x0001F797 File Offset: 0x0001D997
		private List<string> FindQuadCast2ControllerDFUHidDevice()
		{
			return HidDevice.FindHidDevice(new string[]
			{
				"(vid_03f0&pid_0AAF)"
			});
		}

		// Token: 0x0400178B RID: 6027
		private List<string> _deviceIds;

		// Token: 0x0400178C RID: 6028
		private const string CONTROLLER_BIN_FILE_NAME = "03F009AF.bin";

		// Token: 0x0400178D RID: 6029
		private const string CONTROLLER_DFU_PATTERN = "vid_03f0&pid_0AAF";
	}
}```

# QuadcastUpdater
```csharp
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using NGenuity2.Common;
using NGenuity2.Devices;

namespace NGenuity2.Updaters
{
	// Token: 0x02000521 RID: 1313
	public class QuadcastUpdater
	{
		// Token: 0x0600157D RID: 5501 RVA: 0x00023C58 File Offset: 0x00021E58
		private int FindBytes(byte[] buffer, byte[] pattern)
		{
			for (int i = 0; i < buffer.Length; i++)
			{
				if (i + pattern.Length > buffer.Length)
				{
					return -1;
				}
				bool flag = true;
				for (int j = 0; j < pattern.Length; j++)
				{
					if (pattern[j] != buffer[i + j])
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x00023CA4 File Offset: 0x00021EA4
		public string CheckVersion()
		{
			List<string> list = HidDevice.FindHidDevice(QuadcastUpdater.QUADCAST_CMEDIA_DEVICE_IDS);
			string result = string.Empty;
			int num = 3;
			try
			{
				if (list.Count > 0)
				{
					using (HidDevice hidDevice = new HidDevice(list[0], 3221225472U, 2147483648U))
					{
						if (hidDevice.IsValid)
						{
							hidDevice.SetOutputReport(new byte[]
							{
								1,
								0,
								0,
								0,
								0,
								72,
								1,
								4
							});
							byte[] array = new byte[256];
							byte[] outputReport = new byte[]
							{
								1,
								0,
								0,
								0,
								0,
								byte.MaxValue,
								1,
								131
							};
							byte[] array2 = new byte[]
							{
								1,
								0,
								0,
								0,
								0,
								128,
								9,
								161,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0
							};
							byte[] outputReport2 = new byte[]
							{
								1,
								0,
								0,
								0,
								0,
								147,
								3,
								8,
								130,
								1
							};
							byte[] outputReport3 = new byte[]
							{
								1,
								0,
								0,
								0,
								0,
								150,
								1,
								1
							};
							byte[] outputReport4 = new byte[]
							{
								1,
								0,
								0,
								0,
								0,
								byte.MaxValue,
								1,
								147
							};
							byte[] array3 = new byte[16];
							int i = 0;
							while (i < 256)
							{
								if (!hidDevice.SetOutputReport(outputReport))
								{
									num = 5;
									throw new Exception();
								}
								Thread.Sleep(10);
								array2[8] = (byte)i;
								if (!hidDevice.SetOutputReport(array2))
								{
									num = 5;
									throw new Exception();
								}
								Thread.Sleep(10);
								if (!hidDevice.SetOutputReport(outputReport2))
								{
									num = 5;
									throw new Exception();
								}
								Thread.Sleep(10);
								if (!hidDevice.SetOutputReport(outputReport3))
								{
									num = 5;
									throw new Exception();
								}
								Thread.Sleep(10);
								for (;;)
								{
									Utils.ZeroBuffer(array3);
									array3 = hidDevice.GetInputReport(1);
									if (array3 == null)
									{
										goto Block_11;
									}
									if (array3[5] == 131)
									{
										break;
									}
									Thread.Sleep(10);
								}
								Array.Copy(array3, 7, array, i, 8);
								if (!hidDevice.SetOutputReport(outputReport4))
								{
									num = 5;
									throw new Exception();
								}
								Thread.Sleep(10);
								for (;;)
								{
									Utils.ZeroBuffer(array3);
									array3 = hidDevice.GetInputReport(1);
									if (array3 == null)
									{
										goto Block_14;
									}
									if (array3[5] == 147)
									{
										break;
									}
									Thread.Sleep(10);
								}
								i += 8;
								continue;
								Block_14:
								num = 5;
								throw new Exception();
								Block_11:
								num = 5;
								throw new Exception();
							}
							int num2 = this.FindBytes(array, new byte[]
							{
								81,
								117,
								97,
								100,
								67,
								97,
								115,
								116,
								32,
								83,
								10
							});
							if (num2 > -1)
							{
								result = string.Format("{0}.{1}.{2}.{3}", new object[]
								{
									(char)array[num2 + 11],
									(char)array[num2 + 12],
									(char)array[num2 + 13],
									(char)array[num2 + 14]
								});
							}
							else
							{
								result = "0.0.0.0";
							}
							num = 0;
						}
						else
						{
							num = 1;
						}
					}
				}
			}
			catch (Exception)
			{
				if (num == 3)
				{
					num = 255;
				}
			}
			return result;
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x00023F60 File Offset: 0x00022160
		public bool UpdateCMediaCodecFirmware(string fwPath)
		{
			HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadcastS, UpdateFirmwareState.UpdatingProduct, 110000);
			int num = 3;
			List<string> list = HidDevice.FindHidDevice(QuadcastUpdater.QUADCAST_CMEDIA_DEVICE_IDS);
			try
			{
				string empty = string.Empty;
				if (list.Count > 0)
				{
					byte[] array = File.ReadAllBytes(fwPath);
					if (array == null || array.Length != 256)
					{
						num = 4;
						throw new Exception();
					}
					using (HidDevice hidDevice = new HidDevice(list[0], 3221225472U, 2147483648U))
					{
						if (hidDevice.IsValid)
						{
							byte[] array2 = new byte[]
							{
								1,
								0,
								0,
								0,
								0,
								128,
								9,
								160,
								0,
								0,
								0,
								0,
								0,
								0,
								0,
								0
							};
							byte[] outputReport = new byte[]
							{
								1,
								0,
								0,
								0,
								0,
								147,
								3,
								1,
								128,
								1
							};
							for (int i = 0; i < array.Length; i++)
							{
								array2[8] = (byte)i;
								array2[10] = array[i];
								if (!hidDevice.SetOutputReport(array2))
								{
									num = 5;
									throw new Exception();
								}
								Thread.Sleep(10);
								if (!hidDevice.SetOutputReport(outputReport))
								{
									num = 5;
									throw new Exception();
								}
								HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadcastS, UpdateFirmwareState.Updating, (int)((float)i / (float)array.Length * 1000f));
								Thread.Sleep(10);
							}
							num = 0;
						}
						else
						{
							num = 1;
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (num == 3)
				{
					num = 255;
				}
				Logger.WriteLine("UpdateCMediaCodecFirmware: " + ex.Message, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Updaters\\QuadcastUpdater.cs", 237);
			}
			return true;
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x00024108 File Offset: 0x00022308
		public bool UpdateWBControllerFirmware(string fwPath, string originalDeviceId)
		{
			HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadcastS, UpdateFirmwareState.UpdatingProduct, 110000);
			List<string> list = this.FindQuadcastSWBControllerDfuPathes();
			if (list.Count == 0)
			{
				List<string> list2 = HidDevice.FindHidDevice(QuadcastUpdater.QUADCAST_S_WB_CONTROLLER_DEVICE_IDS);
				if (list2.Count == 0)
				{
					Logger.WriteLine("Device not found.", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Updaters\\QuadcastUpdater.cs", 254);
					HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadcastS, UpdateFirmwareState.Failure, 0);
					return false;
				}
				using (HidDevice hidDevice = new HidDevice(list2[0], 3221225472U, 2147483648U))
				{
					if (hidDevice.IsValid && !hidDevice.SetFeatureReport(new byte[]
					{
						0,
						234
					}))
					{
						Logger.WriteLine("Failed to enter DFU mode.", "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Updaters\\QuadcastUpdater.cs", 265);
						HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadcastS, UpdateFirmwareState.Failure, 0);
						return false;
					}
					goto IL_103;
				}
				IL_F2:
				Thread.Sleep(500);
				list = this.FindQuadcastSWBControllerDfuPathes();
				IL_103:
				if (list.Count != 0)
				{
					goto IL_10B;
				}
				goto IL_F2;
			}
			IL_10B:
			HidDevice hidDevice2 = new HidDevice(list[0], 3221225472U, 2147483648U);
			if (!hidDevice2.IsValid)
			{
				Logger.WriteLine("Failed to initialize DFU device. Path: " + list[0], "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Updaters\\QuadcastUpdater.cs", 283);
				HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadcastS, UpdateFirmwareState.Failure, 0);
				return false;
			}
			string text = string.Empty;
			if (Utils.IsCultureInvariantMatch(originalDeviceId, new string[]
			{
				"HID#VID_03F0&PID_028C&MI_00"
			}))
			{
				text = Path.Combine(fwPath, "03F0028C.bin");
			}
			else if (Utils.IsCultureInvariantMatch(originalDeviceId, new string[]
			{
				"HID#VID_03F0&PID_068C&MI_00"
			}))
			{
				text = Path.Combine(fwPath, "03F0068C.bin");
			}
			else if (Utils.IsCultureInvariantMatch(originalDeviceId, new string[]
			{
				"HID#VID_0951&PID_1774"
			}))
			{
				text = Path.Combine(fwPath, "03F0028C.bin");
			}
			if (string.IsNullOrEmpty(text))
			{
				Logger.WriteLine("No firmware file found for this device, DeviceID: " + originalDeviceId, "D:\\actions-runner\\_work\\ngenuity\\ngenuity\\NGenuity2Helper\\Updaters\\QuadcastUpdater.cs", 304);
				HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadcastS, UpdateFirmwareState.Failure, 0);
				return false;
			}
			byte[] array = File.ReadAllBytes(text);
			uint num = 134234112U;
			int num2 = (array.Length + 59) / 60;
			int num3 = (array.Length + 255) / 256;
			int num4 = 0;
			int num5 = num2 + 4;
			byte[] array2 = hidDevice2.CreateOutputReportBuffer();
			array2[1] = 194;
			array2[5] = (byte)num;
			array2[6] = (byte)(num >> 8);
			array2[7] = (byte)(num >> 16);
			array2[8] = (byte)(num >> 24);
			array2[9] = (byte)num3;
			array2[10] = (byte)(num3 >> 8);
			array2[11] = (byte)(num3 >> 16);
			array2[12] = (byte)(num3 >> 24);
			hidDevice2.SetOutputReport(array2);
			num4++;
			HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadcastS, UpdateFirmwareState.Updating, (int)((double)num4 / (double)num5 * 1000.0));
			Utils.ZeroBuffer(array2);
			array2[1] = 195;
			array2[5] = (byte)num;
			array2[6] = (byte)(num >> 8);
			array2[7] = (byte)(num >> 16);
			array2[8] = (byte)(num >> 24);
			array2[9] = (byte)array.Length;
			array2[10] = (byte)(array.Length >> 8);
			array2[11] = (byte)(array.Length >> 16);
			array2[12] = (byte)(array.Length >> 24);
			hidDevice2.SetOutputReport(array2);
			Thread.Sleep(100);
			hidDevice2.GetInputReport();
			num4++;
			HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadcastS, UpdateFirmwareState.Updating, (int)((double)num4 / (double)num5 * 1000.0));
			for (int i = 0; i < num2; i++)
			{
				int num6 = array.Length - 60 * i;
				if (num6 > 60)
				{
					num6 = 60;
				}
				Utils.ZeroBuffer(array2);
				array2[1] = 196;
				array2[3] = (byte)num6;
				array2[4] = (byte)(num6 >> 8);
				Array.Copy(array, i * 60, array2, 5, num6);
				hidDevice2.SetOutputReport(array2);
				Thread.Sleep(50);
				hidDevice2.GetInputReport();
				num4++;
				HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadcastS, UpdateFirmwareState.Updating, (int)((double)num4 / (double)num5 * 1000.0));
			}
			Utils.ZeroBuffer(array2);
			array2[1] = 204;
			hidDevice2.SetOutputReport(array2);
			Thread.Sleep(50);
			hidDevice2.GetInputReport();
			num4++;
			HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadcastS, UpdateFirmwareState.Updating, (int)((double)num4 / (double)num5 * 1000.0));
			Utils.ZeroBuffer(array2);
			array2[1] = 200;
			hidDevice2.SetOutputReport(array2);
			Thread.Sleep(50);
			hidDevice2.GetInputReport();
			num4++;
			HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadcastS, UpdateFirmwareState.Updating, (int)((double)num4 / (double)num5 * 1000.0));
			hidDevice2.Dispose();
			HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadcastS, UpdateFirmwareState.Normal, 1000);
			HyperXCenter.Center.Updater.OnUpdating(HyperXDeviceModel.Microphone_QuadcastS, UpdateFirmwareState.Success, 1000);
			return true;
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x00024630 File Offset: 0x00022830
		private List<string> FindQuadcastSWBControllerDfuPathes()
		{
			return HidDevice.FindHidDevice(new string[]
			{
				"(vid_0951&pid_1774)"
			});
		}

		// Token: 0x040017BB RID: 6075
		private static readonly string[] QUADCAST_CMEDIA_DEVICE_IDS = new string[]
		{
			"VID_0951&PID_171D&mi_03&col01",
			"VID_03F0&PID_0D8B&mi_03&col01"
		};

		// Token: 0x040017BC RID: 6076
		private static readonly string[] QUADCAST_S_WB_CONTROLLER_DEVICE_IDS = new string[]
		{
			"HID#VID_03F0&PID_028C&MI_00",
			"HID#VID_03F0&PID_068C&MI_00"
		};

		// Token: 0x040017BD RID: 6077
		private const string QUADCAST_S_WB_CONTROLLER_DFU_PATTERN = "vid_0951&pid_1774";
	}
}```

# HyperXMicrophoneDevice<T1, T2>
```csharp
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
}```

# HyperXMicrophoneDevice
```csharp
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
}```

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
			string value = new Regex("\\{[0-9a-f\\-]+\\}", 545).Match(this.DeviceID).Value;
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
			string text = "VID_(?<VID>[0-9a-fA-F]+)&PID_(?<PID>[0-9a-fA-F]+).+?(?<ID>\\{[0-9a-f\\-]+\\})";
			Match match = Regex.Match(deviceId, text, 513);
			string value = match.Groups["VID"].Value;
			string value2 = match.Groups["PID"].Value;
			string value3 = match.Groups["ID"].Value;
			if (!string.IsNullOrEmpty(this.DeviceID))
			{
				match = Regex.Match(this.DeviceID, text, 513);
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
				match = Regex.Match(this.NotificationDeviceID, text, 513);
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
				result = Enumerable.First<int>(dictionary.Keys);
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
				result = Enumerable.FirstOrDefault<KeyValuePair<int, int>>(dictionary, (KeyValuePair<int, int> n) => n.Value == max).Key;
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
				WindowStyle = 1
			});
			string text = process.StandardOutput.ReadToEnd();
			process.WaitForExit();
			process.Dispose();
			string[] array = text.Split(new char[]
			{
				'\n'
			});
			string text2 = "\\d+\\.\\d+\\.\\d+\\.\\d+";
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
							Match match = Regex.Match(array[k], text2, 513);
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
			string text = File.ReadAllText(infPath);
			string text2 = "DriverVer\\s*=\\s*[\\d/\\-]+\\s*,\\s*(?<Version>\\d+\\.\\d+\\.\\d+\\.\\d+)";
			Match match = Regex.Match(text, text2);
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
				if (Enumerable.Any<MMDevice>(this._audioDevices, (MMDevice o) => o.Id == device.Id))
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
			bool flag2 = Enumerable.Any<MMDevice>(this._audioDevices, (MMDevice o) => o.DataFlow == AudioDeviceDataFlow.Render);
			bool flag3 = Enumerable.Any<MMDevice>(this._audioDevices, (MMDevice o) => o.DataFlow == AudioDeviceDataFlow.Capture);
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
			bool flag = Enumerable.Any<MMDevice>(this._audioDevices, (MMDevice o) => o.DataFlow == AudioDeviceDataFlow.Render);
			bool flag2 = Enumerable.Any<MMDevice>(this._audioDevices, (MMDevice o) => o.DataFlow == AudioDeviceDataFlow.Capture);
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
				mmdevice = Enumerable.FirstOrDefault<MMDevice>(this._audioDevices, (MMDevice o) => o.DataFlow == AudioDeviceDataFlow.Capture);
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
				mmdevice = Enumerable.FirstOrDefault<MMDevice>(this._audioDevices, (MMDevice o) => o.DataFlow == AudioDeviceDataFlow.Capture);
				mmdevice2 = Enumerable.FirstOrDefault<MMDevice>(this._audioDevices, (MMDevice o) => o.DataFlow == AudioDeviceDataFlow.Render);
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
			MMDevice mmdevice = Enumerable.FirstOrDefault<MMDevice>(this.AudioDevices, (MMDevice o) => o.DataFlow == AudioDeviceDataFlow.Capture);
			if (mmdevice == null)
			{
				return;
			}
			mmdevice.StartAudioMeter(false);
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x00056F9F File Offset: 0x0005519F
		public void StopAudioMeter()
		{
			MMDevice mmdevice = Enumerable.FirstOrDefault<MMDevice>(this.AudioDevices, (MMDevice o) => o.DataFlow == AudioDeviceDataFlow.Capture);
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

# CMDMICQuadCastSCommandBase
```csharp
using System;
using System.Threading;
using NGenuity2.Common;

namespace NGenuity2.Devices.Microphone
{
	// Token: 0x02000780 RID: 1920
	public abstract class CMDMICQuadCastSCommandBase : HXCommandBase
	{
		// Token: 0x06002B61 RID: 11105 RVA: 0x000B1984 File Offset: 0x000AFB84
		protected bool WaitAckAsync(HyperXDevice device, byte cmd, int timeout)
		{
			Thread.Sleep(timeout);
			byte[] featureReport = device.GetFeatureReport();
			return featureReport != null && featureReport[2] == cmd && featureReport[4] == 1;
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x000B19B0 File Offset: 0x000AFBB0
		protected bool VerifyCommandAsyc(HyperXDevice device, byte cmd)
		{
			byte[] featureReport = device.GetFeatureReport();
			return featureReport != null && featureReport[2] == cmd && featureReport[4] == 1;
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x000B19D6 File Offset: 0x000AFBD6
		protected bool WaitAckAsync(HyperXDevice device, byte cmd)
		{
			return this.WaitAckAsync(device, cmd, 10);
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x000682AA File Offset: 0x000664AA
		protected void ZeroBuffer(byte[] buffer)
		{
			Utils.ZeroBuffer(buffer);
			buffer[0] = 7;
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x000B19E2 File Offset: 0x000AFBE2
		protected void InitCommand(byte[] buffer, byte cmd)
		{
			this.ZeroBuffer(buffer);
			buffer[1] = 4;
			buffer[2] = cmd;
		}
	}
}```

# UniversalCMDBase
```csharp
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NGenuity2.Devices.Universals.Commons;

namespace NGenuity2.Devices.Universals.Commands
{
	// Token: 0x02000950 RID: 2384
	public abstract class UniversalCMDBase : HXCommandBase
	{
		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x060033E2 RID: 13282 RVA: 0x000CEEAB File Offset: 0x000CD0AB
		// (set) Token: 0x060033E3 RID: 13283 RVA: 0x000CEEB3 File Offset: 0x000CD0B3
		public bool IsUniversalCommand { get; set; } = true;

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x060033E4 RID: 13284 RVA: 0x000CEEBC File Offset: 0x000CD0BC
		// (set) Token: 0x060033E5 RID: 13285 RVA: 0x000CEEC4 File Offset: 0x000CD0C4
		protected ResponseAck Response { get; set; }

		// Token: 0x060033E6 RID: 13286
		public abstract List<byte[]> CreateBuffers();

		// Token: 0x060033E7 RID: 13287 RVA: 0x000CEED0 File Offset: 0x000CD0D0
		public override void Execute(HyperXDevice device)
		{
			foreach (byte[] array in this.CreateBuffers())
			{
				try
				{
					this.ApplyDeviceTypeToSubReportId(device.GetSubReportIdDeviceType(), array);
					device.SetOutputReport(array);
				}
				catch
				{
				}
			}
		}

		// Token: 0x060033E8 RID: 13288 RVA: 0x000CEF44 File Offset: 0x000CD144
		public virtual Task ExecuteAsync(HyperXDevice device, AckChannel<ResponseAck> channel)
		{
			UniversalCMDBase.<ExecuteAsync>d__12 <ExecuteAsync>d__;
			<ExecuteAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<ExecuteAsync>d__.<>4__this = this;
			<ExecuteAsync>d__.device = device;
			<ExecuteAsync>d__.channel = channel;
			<ExecuteAsync>d__.<>1__state = -1;
			<ExecuteAsync>d__.<>t__builder.Start<UniversalCMDBase.<ExecuteAsync>d__12>(ref <ExecuteAsync>d__);
			return <ExecuteAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060033E9 RID: 13289 RVA: 0x000CEF98 File Offset: 0x000CD198
		protected Task<bool> SendCMDToDevice(HyperXDevice device, AckChannel<ResponseAck> channel, byte[] targetBuffers)
		{
			UniversalCMDBase.<SendCMDToDevice>d__13 <SendCMDToDevice>d__;
			<SendCMDToDevice>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
			<SendCMDToDevice>d__.<>4__this = this;
			<SendCMDToDevice>d__.device = device;
			<SendCMDToDevice>d__.channel = channel;
			<SendCMDToDevice>d__.targetBuffers = targetBuffers;
			<SendCMDToDevice>d__.<>1__state = -1;
			<SendCMDToDevice>d__.<>t__builder.Start<UniversalCMDBase.<SendCMDToDevice>d__13>(ref <SendCMDToDevice>d__);
			return <SendCMDToDevice>d__.<>t__builder.Task;
		}

		// Token: 0x060033EA RID: 13290 RVA: 0x000CEFF3 File Offset: 0x000CD1F3
		protected virtual void ApplyDeviceTypeToSubReportId(SubReportIdDeviceType subReportIdDeviceType, byte[] originalBuffer)
		{
			originalBuffer[1] = (byte)((int)originalBuffer[1] | (int)subReportIdDeviceType.ToByte() << 6);
		}

		// Token: 0x0400254A RID: 9546
		protected const int MAX_RETRY = 3;

		// Token: 0x0400254B RID: 9547
		protected int _retry;
	}
}```