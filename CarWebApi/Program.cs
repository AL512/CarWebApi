using CarWebApi;
using CarWebApi.Database;
using CarWebApi.Mappings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddPersistence(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Регистрируем медиатор
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

// Добавляем и конфигурируем автомаппер
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(CarApiDbContext).Assembly));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    //Получаем сервис провайдер для разрешения зависимостей
    var serviceProvider = scope.ServiceProvider;
    try
    {
        //получаем контекст
        var context = serviceProvider.GetRequiredService<CarApiDbContext>();
        //Инициализируем БД
        DbInitializer.Initialize(context);
    }
    catch (Exception exception)
    {
        //Log.Fatal(exception, "Ошибка при инициализации приложения");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
