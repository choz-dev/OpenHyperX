namespace OpenHyperX.Core;

public sealed record HidDeviceFilter(int? VendorId = null, IReadOnlyCollection<int>? ProductIds = null)
{
    public bool Matches(int vendorId, int productId)
    {
        if (VendorId is not null && vendorId != VendorId.Value)
        {
            return false;
        }

        return ProductIds is null || ProductIds.Contains(productId);
    }
}
