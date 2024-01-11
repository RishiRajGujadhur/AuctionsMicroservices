using MassTransit;
using SearchService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMassTransit(x=>
  {
    x.UsingRabbitMq((context, cgf) => {
      cgf.ConfigureEndpoints(context);
    });
  });
  
var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

try{
    await DbInitializer.InitDb(app);
}
catch(Exception e){
    Console.WriteLine(e);
}

app.Run();
