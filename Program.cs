using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WorkManagementSystem.Data;
using WorkManagementSystem.Interfaces;
using WorkManagementSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddDbContext<MyAppData>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "WorkManagementSystem", Version = "v1" });
});
builder.Services.AddControllers();
//Register services
builder.Services.AddTransient<ITaskService, TaskService>();
builder.Services.AddTransient<IWorkerService, WorkerService>();
builder.Services.AddTransient<DbSeeder>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAuthentication(option =>
    {
        
        option.DefaultAuthenticateScheme = "Bearer";
        option.DefaultScheme = "Bearer";
        option.DefaultChallengeScheme = "Bearer";
    }).AddJwtBearer(cfg =>
    {
        cfg.RequireHttpsMetadata = false;
        cfg.SaveToken = true;
        cfg.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("AppSettings:Token").Value)),
        };
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.MapControllers();
    app.UseSwaggerUI(
    c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "WorkManagementSystem v1"); }
        );
}
var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

using (var scope = scopedFactory.CreateScope())
{
    var service = scope.ServiceProvider.GetService<DbSeeder>();
    service.Seed();
}


app.UseAuthentication();
app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{ 
    endpoints.MapControllers();
});




app.Run();
