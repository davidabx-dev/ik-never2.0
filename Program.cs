var builder = WebApplication.CreateBuilder(args);

// CORS Total
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
var app = builder.Build();

// A ordem exata para evitar bloqueios do Kestrel
app.UseCors("PermitirTudo");
app.MapControllers();

// TRAVA DE SÊNIOR: Força o servidor a ignorar o launchSettings.json e rodar APENAS em HTTP puro na porta 5281
app.Run("http://localhost:5281");
