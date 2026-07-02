using System.Text.Json;
using OpenHyperX.Core;

namespace OpenHyperX.App.Services;

public sealed class DeviceSettingsStore
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };

    private readonly string _settingsPath;
    private readonly object _sync = new();
    private OpenHyperXSettings _settings;

    public DeviceSettingsStore()
        : this(GetDefaultSettingsPath())
    {
    }

    public DeviceSettingsStore(string settingsPath)
    {
        _settingsPath = settingsPath;
        _settings = LoadSettings(settingsPath);
    }

    public bool IsDarkMode
    {
        get
        {
            lock (_sync)
            {
                return string.Equals(_settings.Theme, "Dark", StringComparison.OrdinalIgnoreCase);
            }
        }
    }

    public AppCloseBehavior CloseBehavior
    {
        get
        {
            lock (_sync)
            {
                return ParseCloseBehavior(_settings.CloseBehavior);
            }
        }
    }

    public bool StartInTrayOnStartup
    {
        get
        {
            lock (_sync)
            {
                return _settings.StartInTrayOnStartup;
            }
        }
    }

    public bool DtsSpatialAudioEnabled
    {
        get
        {
            lock (_sync)
            {
                return _settings.DtsSpatialAudioEnabled;
            }
        }
    }

    public void SaveTheme(bool isDarkMode)
    {
        lock (_sync)
        {
            _settings.Theme = isDarkMode ? "Dark" : "Light";
            Save();
        }
    }

    public void SaveStartInTrayOnStartup(bool startInTray)
    {
        lock (_sync)
        {
            _settings.StartInTrayOnStartup = startInTray;
            Save();
        }
    }

    public void SaveCloseBehavior(AppCloseBehavior closeBehavior)
    {
        lock (_sync)
        {
            _settings.CloseBehavior = closeBehavior.ToString();
            Save();
        }
    }

    public void SaveDtsSpatialAudioEnabled(bool enabled)
    {
        lock (_sync)
        {
            _settings.DtsSpatialAudioEnabled = enabled;
            Save();
        }
    }

    public CloudAlphaWirelessSavedSettings? GetCloudAlphaWirelessSettings(string deviceKey)
    {
        lock (_sync)
        {
            return _settings.CloudAlphaWirelessDevices.TryGetValue(deviceKey, out var deviceSettings)
                ? deviceSettings.Clone()
                : null;
        }
    }

    public void SaveCloudAlphaWirelessSettings(string deviceKey, CloudAlphaWirelessSavedSettings deviceSettings)
    {
        lock (_sync)
        {
            _settings.CloudAlphaWirelessDevices[deviceKey] = deviceSettings.Clone();
            Save();
        }
    }

    public static string CreateCloudAlphaWirelessKey(uint? pairId, HidDeviceInfo deviceInfo)
    {
        return pairId is null
            ? $"cloud-alpha-wireless:{deviceInfo.VendorIdHex}:{deviceInfo.ProductIdHex}"
            : $"cloud-alpha-wireless:pair-{pairId.Value:X8}";
    }

    private void Save()
    {
        var directory = Path.GetDirectoryName(_settingsPath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        var json = JsonSerializer.Serialize(_settings, JsonOptions);
        File.WriteAllText(_settingsPath, json);
    }

    private static OpenHyperXSettings LoadSettings(string settingsPath)
    {
        if (!File.Exists(settingsPath))
        {
            return new OpenHyperXSettings();
        }

        try
        {
            var json = File.ReadAllText(settingsPath);
            return JsonSerializer.Deserialize<OpenHyperXSettings>(json, JsonOptions) ?? new OpenHyperXSettings();
        }
        catch
        {
            return new OpenHyperXSettings();
        }
    }

    private static string GetDefaultSettingsPath()
    {
        var root = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        return Path.Combine(root, "OpenHyperX", "settings.json");
    }

    private static AppCloseBehavior ParseCloseBehavior(string? value)
    {
        return Enum.TryParse<AppCloseBehavior>(value, ignoreCase: true, out var closeBehavior)
            ? closeBehavior
            : AppCloseBehavior.Ask;
    }
}

public sealed class OpenHyperXSettings
{
    public string Theme { get; set; } = "Light";

    public string CloseBehavior { get; set; } = nameof(AppCloseBehavior.Ask);

    public bool StartInTrayOnStartup { get; set; } = true;

    public bool DtsSpatialAudioEnabled { get; set; }

    public Dictionary<string, CloudAlphaWirelessSavedSettings> CloudAlphaWirelessDevices { get; set; } = [];
}

public sealed class CloudAlphaWirelessSavedSettings
{
    public bool? MicMuted { get; set; }

    public bool? MicrophoneMonitoringEnabled { get; set; }

    public bool? VoicePromptEnabled { get; set; }

    public byte? AutoShutdownMinutes { get; set; }

    public CloudAlphaWirelessSavedSettings Clone()
    {
        return new CloudAlphaWirelessSavedSettings
        {
            MicMuted = MicMuted,
            MicrophoneMonitoringEnabled = MicrophoneMonitoringEnabled,
            VoicePromptEnabled = VoicePromptEnabled,
            AutoShutdownMinutes = AutoShutdownMinutes
        };
    }
}
