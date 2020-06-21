using Microsoft.AspNetCore.Builder;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DockerCore.Models
{
    public static class SendData
    {
        public static IApplicationBuilder UseDataInitializer(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var dbcontext = scope.ServiceProvider.GetService<ProductDbContext>();
                System.Console.WriteLine("开始执行迁移数据库...");
                dbcontext.Database.Migrate();
                System.Console.WriteLine("数据库迁移完成...");
                if (!dbcontext.Products.Any())
                {
                    System.Console.WriteLine("开始创建种子数据中...");
                    dbcontext.Products.AddRange(
                       new Product("空调", "家用电器", 2750),
                       new Product("电视机", "家用电器", 3750),
                       new Product("电冰箱", "家用电器", 1750),
                       new Product("猪肉", "食品", 31),
                       new Product("牛肉", "食品", 36),
                       new Product("鸡肉", "食品", 12)
                    );
                    dbcontext.SaveChanges();
                }
                else {
                    System.Console.WriteLine("无需创建种子数据中...");
                }
            }
            return builder;
        }
    }
}
