using System.Text.Json.Serialization;
using Barber.Api.Context;
using Barber.Api.Repositories;
using Barber.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
// builder.Services.AddDbContext<AppDbContext>(opt =>
//     opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDbContext<AppDbContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



//usando repository
builder.Services.AddScoped<IServicoRepository, ServicoRepository>();
builder.Services.AddScoped<IOfereceRepository, OfereceRepository>();
builder.Services.AddScoped<IHorarioDisponivelRepository, HorarioDisponivelRepository>();
builder.Services.AddScoped<IHistoricoCorteRepository, HistoricoCorteRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IBarbeiroRepository, BarbeiroRepository>();
builder.Services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
builder.Services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();


//usando repository Generico
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
{
    options.DocumentPath = "/openapi/v1.json";
});
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
