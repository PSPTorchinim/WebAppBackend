using ApiGateway;
using Handlers.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var _configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var systemConfig = new SystemConfiguration();
ConfigurationBinder.Bind(builder.Configuration, systemConfig);

builder.Services.AddAuthentication().AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(systemConfig.TokenConfiguration.Key))
    };
});

builder.Services.AddReverseProxy()
    .LoadFromConfig(_configuration.GetSection("Gateway"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Gateway service by itself- 1.0");
        opt.SwaggerEndpoint("/swagger/serviceA/v1/swagger.json", "ServiceA - 1.0");
    });
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    // map yarp swagger 
    endpoints.MapGetSwaggerForYarp(_configuration);

    // All controller
    endpoints.MapReverseProxy();
    endpoints.MapControllers();
});

app.Run();
