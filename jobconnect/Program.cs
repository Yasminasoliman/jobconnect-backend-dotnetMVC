using jobconnect.Data;
using jobconnect.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultDbContext"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddScoped<jobconnect.Data.AuthRepository>();

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IDataRepository<User>, DataRepository<User>>();
builder.Services.AddScoped<IDataRepository<JobSeeker>, DataRepository<JobSeeker>>();
builder.Services.AddScoped<IDataRepository<Employer>, DataRepository<Employer>>();
builder.Services.AddScoped<IDataRepository<Job>, DataRepository<Job>>();
builder.Services.AddScoped<IDataRepository<Proposal>, DataRepository<Proposal>>();
builder.Services.AddScoped<IDataRepository<SavedJobs>, DataRepository<SavedJobs>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.Run();