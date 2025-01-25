using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.Common;
using User.Application.Kafka.Implementation.Producer;
using User.Application.Kafka.Interface;
using User.Application.Manager.Implementation;
using User.Application.Manager.Interface;

namespace User.Application
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            var clientConfig = new ClientConfig()
            {
                SaslUsername = configuration["KafkaConfig:SaslUsername"],
                BootstrapServers = configuration["KafkaConfig:BootstrapServers"],
                SaslPassword = configuration["KafkaConfig:SaslPassword"],
                SaslMechanism = SaslMechanism.Plain,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                EnableSslCertificateVerification = false // to de force ssl in local 
            };


            var producerConfig = new ProducerConfig(clientConfig);
            
            services.AddSingleton(producerConfig);
            
            services.AddSingleton(typeof(IKafkaProducer<,>), typeof(KafkaProducer<,>));
            
            services.AddScoped<IEmployeeManager, EmployeeManager>();
            services.AddScoped<IDepartmentManager, DepartmentManager>();
            services.AddScoped<ICountryManager, CountryManager>();
            services.AddScoped<ICityManager, CityManager>();
            services.AddScoped<IAddressManager, AddressManager>();
            services.AddScoped<ILoginManager, LoginManager>();
            services.AddScoped<IDocumentManager, DocumentManager>();

            services.AddScoped<ILeaveKafkaProducer, LeaveKafkaProducer>();
            return services;
        }
    }
}
