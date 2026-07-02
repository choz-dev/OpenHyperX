namespace OpenHyperX.Core;

public sealed record HidDeviceFilter(int? VendorId = null, IReadOnlyCollection<int>? ProductIds = null)
{
    public IReadOnlyCollection<int>? VendorIds { get; init; }

    public bool Matches(int vendorId, int productId)
    {
        if (VendorId is not null && vendorId != VendorId.Value)
        {
            return false;
        }

        if (VendorIds is not null && !VendorIds.Contains(vendorId))
        {
            return false;
        }

        return ProductIds is null || ProductIds.Contains(productId);
    }
}
