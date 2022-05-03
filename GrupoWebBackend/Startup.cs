using System;
using GrupoWebBackend.DomainAdoptionsRequests.Domain.Repositories;
using GrupoWebBackend.DomainAdoptionsRequests.Domain.Services;
using GrupoWebBackend.DomainAdoptionsRequests.Persistence.Repositories;
using GrupoWebBackend.DomainAdoptionsRequests.Services;

namespace GrupoWebBackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add CORS Support
            services.AddCors();
            
            services.AddControllers();
            
            // Lowercase Endpoints
            services.AddRouting(options => options.LowercaseUrls = true);
            
            // Configure AppSettings object 
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("GrupoWebBackend-api-in-memory");
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "GrupoWebBackend", Version = "v1"});
                c.EnableAnnotations();
            });
            
            // DbContext
            services.AddDbContext<AppDbContext>();
            
            // Dependency Injection Configuration

            services.AddScoped<IAdoptionsRequestsRepository,AdoptionsRequestsRepository>();
            services.AddScoped<IAdoptionsRequestsService,AdoptionsRequestsService>();

            
            // AutoMapper Configuration
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GrupoWebBackend v1"));
            }

            // Apply CORS Policies
            app.UseCors(p => p
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            
            // Integrate Error Handling Middleware
            app.UseMiddleware<ErrorHandlerMiddleware>();
            
            // Integrate JWT Authorization Middleware
            app.UseMiddleware<JwtMiddleware>();
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
