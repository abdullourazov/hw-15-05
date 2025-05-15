using Infrastructure.Interface;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddSwaggerGen(); // Swagger документация
builder.Services.AddControllers(); // Контроллеры
// builder.Services.AddScoped<IBooksServices, BooksServices>(); 
// builder.Services.AddScoped<IMembersServices, BooksServices>(); 

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", "My App"));
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.Run();
