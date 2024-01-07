using Microsoft.EntityFrameworkCore;
using POSWEB.Server.Context;
using POSWEB.Server.GraphQLSchema;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
#region register db context provider

builder.Services.AddDbContext<ApplicationDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDbContext")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

#endregion

#region register graphql services



builder.Services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutations>();

#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseAuthorization();


app.UseCors(builder => builder
     .AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader());


app.MapControllers();

app.MapFallbackToFile("/index.html");
app.MapGraphQL();

app.Run();
