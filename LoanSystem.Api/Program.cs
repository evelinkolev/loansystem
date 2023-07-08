using LoanSystem.Data;
using LoanSystem.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddData(builder.Configuration)
    .AddServices(builder.Configuration);



builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase();
app.InitializeRolesAsync().Wait();
//app.InitializeUserRolesAsync().Wait();

app.Run();
