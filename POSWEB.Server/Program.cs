using POSWEB.Server;
using Infrastructure;
using POSWEB.Server.Middlewares;
using POSWEB.Server.GraphQLSchema;
using Persistence;
using Persistence.SeedData;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPresentation()
        //.AddApplication()
        .AddPersistence(builder.Configuration)
        .AddInfrastructure(builder.Configuration);



#region register graphql services



builder.Services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutations>();

#endregion



var app = builder.Build();

#region Exception handler middleware

app.UseExceptionHandler();
app.UseMiddleware<AuthenticationErrorHandler>();

#endregion

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
    await app.InitialiseDatabaseAsync();
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
