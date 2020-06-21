using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DockerCore.Models
{
    public class DataProductRepository : IProductRepository
    {
        private ProductDbContext context;

        public DataProductRepository(ProductDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Product> Products => context.Products;
    }
}
