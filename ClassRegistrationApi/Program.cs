using ClassRegistrationApi.Adapters;
using ClassRegistrationApi.Domain;
using HypertheoryApiUtils;

var builder = WebApplication.CreateBuilder(args);
// Routing
// Add the BsonId route Constraint. (e.g. "/professionals/{id:bsonid}")
builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("bsonid", typeof(BsonIdConstraint));
});

// Config and Options
builder.Services.Configure<MongoConnectionOptions>(builder.Configuration.GetSection(MongoConnectionOptions.SectionName));
// Add services to the container.
var scheduleUrl = builder.Configuration.GetValue<string>("scheduleApiUrl");
builder.Services.AddHttpClient<ScheduleHttpAdapter>(client =>
{
    client.BaseAddress = new Uri(scheduleUrl);
});
builder.Services.AddTransient<ILookupCourseSchedules, ScheduleApiLookup>();
builder.Services.AddTransient<ICreateReservations, MongoReservationProcessor>();
builder.Services.AddSingleton<RegistrationMongoAdapter>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
