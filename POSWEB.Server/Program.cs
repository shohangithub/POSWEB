using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using POSWEB.Server.Authentication;
using POSWEB.Server.Authentication.OptionSetup;
using POSWEB.Server.GraphQLSchema;
using POSWEB.Server;
using Infrastructure;
using POSWEB.Server.Middlewares;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentation()
        //.AddApplication()
        .AddInfrastructure(builder.Configuration);



//#region register graphql services



//builder.Services.AddGraphQLServer()
//                .AddQueryType<Query>()
//                .AddMutationType<Mutations>();

//#endregion


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


//#endregion

var app = builder.Build();

app.UseExceptionHandler();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

//else
//{
//    app.UseDeveloperExceptionPage();
//    app.UseMigrationsEndPoint();
//}

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
//app.MapGraphQL();

app.Run();
