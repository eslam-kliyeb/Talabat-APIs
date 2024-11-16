using System.Text.Json.Serialization;

namespace Talabat.Core.Interfaces.Specifications.SpecificationsHelpers.Sorting
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SortingProductParameters
    {
        NameAsc, NameDesc, PriceAsc, PriceDesc
    }
}
