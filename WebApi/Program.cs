using Infrastructure.Date;
using Infrastructure.Interface;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(); 
builder.Services.AddControllers(); 
builder.Services.AddScoped<DataContext, DataContext>();
builder.Services.AddScoped<IBooksServices, BooksServices>(); 
builder.Services.AddScoped<IBorrowingsServices, BorrowingsServices>(); 
builder.Services.AddScoped<IMembersServices, MembersServices>(); 

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

