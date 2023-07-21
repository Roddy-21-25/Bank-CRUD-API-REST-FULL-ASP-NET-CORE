using AplicationDomain.Layer___Bank_Api.Entities;
using AplicationDomain.Layer___Bank_Api.Interfacez;
using AplicationDomain.Layer___Bank_Api.Paginations_Options;
using AplicationDomain.Layer___Bank_Api.Services;
using Infraestructure.Layer___Bank_Api.Data;
using Infraestructure.Layer___Bank_Api.Filters;
using Infraestructure.Layer___Bank_Api.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// JWT Config
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],

        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration["Authentication:SecretKey"]
            ))
    };
});

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalFiltersExceptions>();
});

builder.Services.AddHttpContextAccessor();

// Sql Path
builder.Services.AddDbContext<BANKContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("BankSqlPath")));

// Crud Repository DI
builder.Services.AddScoped(typeof(ICrudRepository<>), typeof(BaseCrudRepository<>));
builder.Services.AddTransient<IClientServices, ClientServices>();
builder.Services.AddTransient<IClientAccountServices, ClientAccountServices>();
builder.Services.AddTransient<IBankAdminRepository, BankAdminRepository>();
builder.Services.AddSingleton<IPasswordHasher, PasswordServices>();

// URI ID
builder.Services.AddSingleton<IUriClientServices>(provider =>
{
    var _httpContext = provider.GetRequiredService<IHttpContextAccessor>();
    var request = _httpContext.HttpContext.Request;
    var finalUrl = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());

    return new UriClientServices(finalUrl);
});

// PAgination
builder.Services.Configure<PaginationDefaultValues>(builder.Configuration.GetSection("Pagination"));

// Password Hash
builder.Services.Configure<PasswordOptionsValues>(builder.Configuration.GetSection("PasswordOptions"));

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(doc =>
{
    doc.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Bank Api",
        Version = "v1",
        Description = "This is an API about a CLient of an BANK, just a practice:\n" +
        "You cant use for your projects, all the informations here, are not real, i genereted all of one in Chat GPT " +
        "The Client and Client Account endpoint, are Just Two CRUD.\n" +
        "The others EndPoints Are Just New Features About the Project Bank"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPaht = Path.Combine(AppContext.BaseDirectory, xmlFile);
    doc.IncludeXmlComments(xmlPaht);

    // Agregar una capa de seguridad extra
    // Bearer = solo el nombre
    doc.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        // atributos del boton.
        In = ParameterLocation.Header,
        Description = "Insert the Token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    // agregar requisitos de seguridad
    doc.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Bank Api");

    });
}

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
