using Microsoft.EntityFrameworkCore;
using AirBB.Models.DataLayer;

namespace AirBB.Models.DataLayer.Repositories
{
    /// <summary>
    /// Generic Repository pattern for CRUD operations.
    /// Reduces code duplication and provides a consistent interface.
    /// </summary>
    public class Repository<T> where T : class
    {
        protected AirBBContext _context;

        public Repository(AirBBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all entities with optional query options
        /// </summary>
        public virtual List<T> GetAll(QueryOptions<T>? options = null)
        {
            var query = _context.Set<T>().AsQueryable();

            if (options != null)
            {
                // Apply includes
                foreach (var include in options.Includes)
                {
                    query = query.Include(include);
                }

                // Apply filter
                var items = query.ToList();
                if (options.Filter != null)
                {
                    items = items.Where(options.Filter).ToList();
                }

                // Apply order by
                if (options.OrderBy != null)
                {
                    items = options.OrderBy(items).ToList();
                }

                // Apply pagination
                if (options.PageSize > 0)
                {
                    int skip = (options.PageNumber - 1) * options.PageSize;
                    items = items.Skip(skip).Take(options.PageSize).ToList();
                }

                return items;
            }

            return query.ToList();
        }

        /// <summary>
        /// Get a single entity by ID
        /// </summary>
        public virtual T? GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        /// <summary>
        /// Get a single entity using query options
        /// </summary>
        public virtual T? GetOne(QueryOptions<T> options)
        {
            var query = _context.Set<T>().AsQueryable();

            // Apply includes
            foreach (var include in options.Includes)
            {
                query = query.Include(include);
            }

            var items = query.ToList();

            // Apply filter
            if (options.Filter != null)
            {
                items = items.Where(options.Filter).ToList();
            }

            return items.FirstOrDefault();
        }

        /// <summary>
        /// Add a new entity
        /// </summary>
        public virtual void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        /// <summary>
        /// Update an existing entity
        /// </summary>
        public virtual void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        /// <summary>
        /// Delete an entity by ID
        /// </summary>
        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
            }
        }

        /// <summary>
        /// Delete an entity
        /// </summary>
        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Save changes to the database
        /// </summary>
        public virtual void Save()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Save changes asynchronously
        /// </summary>
        public virtual async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Check if an entity exists by ID
        /// </summary>
        public virtual bool Exists(int id)
        {
            return _context.Set<T>().Find(id) != null;
        }
    }
}
