namespace AirBB.Models.DataLayer
{
    /// <summary>
    /// Encapsulates query options to reduce the weight of DbContext.
    /// Allows filtering, sorting, and pagination in a structured way.
    /// </summary>
    public class QueryOptions<T> where T : class
    {
        /// <summary>
        /// List of include paths for eager loading (e.g., "Location", "Residence.Location")
        /// </summary>
        public List<string> Includes { get; set; } = new();

        /// <summary>
        /// Filter predicate function
        /// </summary>
        public Func<T, bool>? Filter { get; set; }

        /// <summary>
        /// Order by function (returns IOrderedEnumerable)
        /// </summary>
        public Func<IEnumerable<T>, IOrderedEnumerable<T>>? OrderBy { get; set; }

        /// <summary>
        /// Page number for pagination (1-based)
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Page size for pagination
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Whether to take only one result
        /// </summary>
        public bool IsSingleResult { get; set; }

        /// <summary>
        /// Add an include path for eager loading
        /// </summary>
        public QueryOptions<T> AddInclude(string includePath)
        {
            Includes.Add(includePath);
            return this;
        }

        /// <summary>
        /// Set the filter predicate
        /// </summary>
        public QueryOptions<T> SetFilter(Func<T, bool> filter)
        {
            Filter = filter;
            return this;
        }

        /// <summary>
        /// Set the order by function
        /// </summary>
        public QueryOptions<T> SetOrderBy(Func<IEnumerable<T>, IOrderedEnumerable<T>> orderBy)
        {
            OrderBy = orderBy;
            return this;
        }

        /// <summary>
        /// Set pagination parameters
        /// </summary>
        public QueryOptions<T> SetPaging(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            return this;
        }

        /// <summary>
        /// Set to single result mode
        /// </summary>
        public QueryOptions<T> AsSingleResult()
        {
            IsSingleResult = true;
            return this;
        }
    }
}
