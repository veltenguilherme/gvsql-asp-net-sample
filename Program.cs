using Persistence;
using Sample.Models;
using Sample.Tables;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var database = new Database(hostName: "localhost",
                            port: 5432,
                            name: "gvsql_aspnet_sample",
                            userName: "postgres",
                            password: "123Mudar",
                            schema: GetSchema());

builder.Services.AddSingleton(s => database);

builder.Services.AddTransient<Persons>();
builder.Services.AddTransient<Users>();
builder.Services.AddTransient<Customers>();
builder.Services.AddTransient<Partners>();
builder.Services.AddTransient<Sales>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler("/error");
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();

static List<Structure> GetSchema()
{
    return new List<Structure>()
            {
                new Structure()
                {
                    Model =  typeof(Person),
                    Table = typeof(Persons)
                },
                new Structure()
                {
                    Model =  typeof(User),
                    Table = typeof(Users)
                },
                new Structure()
                {
                    Model =  typeof(Customer),
                    Table = typeof(Customers)
                },
                new Structure()
                {
                    Model =  typeof(Partner),
                    Table = typeof(Partners)
                },
                new Structure()
                {
                    Model =  typeof(Sale),
                    Table = typeof(Sales)
                }
            };
}