using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using POSWEB.Server.Authentication;
using POSWEB.Server.Authentication.OptionSetup;
using POSWEB.Server.Context;
using POSWEB.Server.Entitites;
using POSWEB.Server.GraphQLSchema;
using POSWEB.Server.ServiceContracts;
using POSWEB.Server.Services;
using POSWEB.Server;
using Infrastructure;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentation()
    //.AddApplication()
        .AddInfrastructure(builder.Configuration);

//// Add services to the container.
//#region register db context provider

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//  options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext")));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//#endregion

//#region register graphql services



//builder.Services.AddGraphQLServer()
//                .AddQueryType<Query>()
//                .AddMutationType<Mutations>();

//#endregion


#region register business services
builder.Services.AddScoped<IUserService, UserService>();
#endregion


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//#region JWT configuration
//builder.Services.ConfigureOptions<ConfigureJwtOptions>();
////Jwt configuration starts here
//var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
//var jwtKey = builder.Configuration.GetSection("Jwt:SecretKey").Get<string>();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
// .AddJwtBearer(options =>
// {
//     options.TokenValidationParameters = new TokenValidationParameters()
//     {
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidateLifetime = true,
//         ValidateIssuerSigningKey = true,
//         ValidIssuer = jwtIssuer,
//         ValidAudience = jwtIssuer,
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
//     };
// });



//builder.Services.AddTransient<IJwtProvider, JwtProvider>();

////register authorization handler
//builder.Services.AddAuthorization();
//builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
//builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();


//#endregion

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.UseCors(builder => builder
     .AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader());


app.MapControllers();

app.MapFallbackToFile("/index.html");
app.MapGraphQL();

app.Run();
