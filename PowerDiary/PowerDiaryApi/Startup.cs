using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PowerDiaryApi.AutoMapperConfig;
using PowerDiaryBusiness;
using PowerDiaryBusiness.MessagesFormatter;
using PowerDiaryDataAccess.DataAccess;
using PowerDiaryDataAccess.DatabaseModel;

namespace PowerDiaryApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PowerDiaryApi", Version = "v1" });
            });

            services.AddTransient<IEventFormatter, EventFormatter>();
            services.AddSingleton<IChatBusiness, PowerDiaryBusiness.ChatBusiness>();
            services.AddSingleton<IChatRepository, ChatRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();

            services.AddSingleton(option =>
            {
                var contextOptions = new DbContextOptionsBuilder<ChatDbContext>()
                    .UseInMemoryDatabase(databaseName: "PowerDiary")
                    .Options;

                return new ChatDbContext(contextOptions);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PowerDiaryApi v1"));
            }

            app.UseRouting();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
