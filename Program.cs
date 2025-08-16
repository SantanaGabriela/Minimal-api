using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MinimalApi.DTOs;
using MinimalApi.Dominio.Interfaces;
using MinimalApi.Dominio.Servicos;
using MinimalApi.Infraestrutura.Db;
using Microsoft.OpenApi.Models;
using MinimalApi.Dominio.ModelViews;


var builder = WebApplication.CreateBuilder(args);

// Registrando serviço e DbContext
builder.Services.AddScoped<IAdministradorServico, AdministradorServico>();
builder.Services.AddDbContext<DbContexto>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao"))
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGet("/", () => Results.Json(new Home()));

// Rota de login
app.MapPost("/login", ([FromBody] LoginDTO loginDTO, IAdministradorServico administradorServico) =>
{
    var adm = administradorServico.Login(loginDTO);
    return adm != null ? Results.Ok("Login realizado com sucesso!") : Results.Unauthorized();
});

app.UseSwagger();
app.UseSwaggerUI();
app.Run();




