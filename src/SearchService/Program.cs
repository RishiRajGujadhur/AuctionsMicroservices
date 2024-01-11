using Contracts;
using MassTransit;
using SearchService;
using SearchService.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMassTransit(x=>
  {
    x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false));
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
