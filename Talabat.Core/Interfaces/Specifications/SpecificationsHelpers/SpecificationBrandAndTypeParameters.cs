using Talabat.Core.Interfaces.Specifications.SpecificationsHelpers.Sorting;

namespace Talabat.Core.Interfaces.Specifications.SortingSpecifications
{
    public class SpecificationBrandAndTypeParameters
    {
        private const int MAXPAGESIZE = 10;
        //=======================================
        public SortingBrandAndTypeParameters? Sort { get; set; }
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 5;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > MAXPAGESIZE) ? MAXPAGESIZE : value; }
        }
        private string? _search;
        public string? Search
        {
            get { return _search; }
            set { _search = value?.Trim().ToLower(); }
        }
    }
}
