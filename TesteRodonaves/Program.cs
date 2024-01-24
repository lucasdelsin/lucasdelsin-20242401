
using Microsoft.EntityFrameworkCore;
using TesteRodonaves.Data;

namespace TesteRodonaves
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddDbContext<RodonavesDbContext>(options => options.UseInMemoryDatabase("RodonavesDb"));
            builder.Services.AddEntityFrameworkNpgsql()
                .AddDbContext<RodonavesDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("TesteRodonavesConnectionString")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
