using OpenHyperX.Devices.QuadCast;

namespace OpenHyperX.App.ViewModels;

public sealed record QuadCastPolarPatternOption(QuadCastPolarPattern Pattern, string Label)
{
    public override string ToString()
    {
        return Label;
    }
}
