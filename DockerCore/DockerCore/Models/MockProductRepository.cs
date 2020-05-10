using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DockerCore.Models
{
    public class MockProductRepository : IProductRepository
    {
        private static readonly Product[] DumyData = new Product[] {
            new Product{ ProductID=0, Name ="产品0",Category="分类0",Price=100},
            new Product{ ProductID=1,Name="产品1",Category="分类1",Price=101},
            new Product{ ProductID=2,Name="产品2",Category="分类2",Price=102},
            new Product{ ProductID=3,Name="产品3",Category="分类3",Price=103},
            new Product{ ProductID=4,Name="产品4",Category="分类4",Price=104},
            new Product{ ProductID=5,Name="产品5",Category="分类5",Price=105}
        };

        public IQueryable<Product> Products => DumyData.AsQueryable();
    }
}
