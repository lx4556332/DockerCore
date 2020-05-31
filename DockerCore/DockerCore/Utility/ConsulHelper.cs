using Consul;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DockerCore.Utility
{
    public static class ConsulHelper
    {
        public static void ConsulRegister(this IConfiguration configuration)
        {
            ConsulClient client = new ConsulClient(c =>
            {
                c.Address = new Uri("http://127.0.0.1:8500/");
                c.Datacenter = "dc1";
            });

            var ip = configuration["ip"];
            var port = configuration["port"] == null ? 5643 : int.Parse(configuration["port"]);
            int weight = string.IsNullOrWhiteSpace(configuration["weight"]) ? 1 : int.Parse(configuration["weight"]);

            client.Agent.ServiceRegister(new AgentServiceRegistration()
            {
                ID = "service" + Guid.NewGuid(),
                Name = "TestService",
                Address = ip,
                Port = port,
                Tags = new string[] { weight.ToString() },
                Check = new AgentServiceCheck()
                {
                    Interval = TimeSpan.FromSeconds(12),//间隔12s一次
                    HTTP = $"http://{ip}:{port}/Api/Health",
                    Timeout = TimeSpan.FromSeconds(5),// 检测等待时间
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(20)//失败后多久移除
                }
            });
        }
    }
}
