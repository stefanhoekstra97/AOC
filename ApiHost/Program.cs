

var hostBuilder = WebApplication.CreateBuilder();

hostBuilder.Services.AddControllers();
hostBuilder.Services.AddSwaggerGen();


var app = hostBuilder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();


app.Run();

