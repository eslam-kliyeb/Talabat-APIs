using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Talabat.Core.Interfaces.Specifications.SpecificationsHelpers.Sorting
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SortingBrandAndTypeParameters
    {
        NameAsc, NameDesc
    }
}
