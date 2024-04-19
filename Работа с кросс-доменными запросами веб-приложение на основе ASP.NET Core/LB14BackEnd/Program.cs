var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllAvailable", builder => //ƒобавл€ем разные политики, которые сможем использовать в дальнейшем по нашему усмотрению
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        options.AddPolicy("NotAllAvailable", builder =>
            {
                builder
                    .WithOrigins("https://localhost.com")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllAvailable");

app.MapGet("/", () => "—оединение успешно установлено!");

app.Run();
