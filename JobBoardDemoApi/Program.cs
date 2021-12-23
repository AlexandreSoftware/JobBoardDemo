using JobBoardRepository;
using JobBoardRepository.Interface;
using JobBoardServices;
using JobBoardServices.Interface;
using JobBoardServices.View;
using Serilog;
var builder = WebApplication.CreateBuilder(args);
//serilog
builder.Host.UseSerilog((ctx, lc) =>
    lc.WriteTo.Console());
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IJobService, JobService>();
builder.Services.AddAutoMapper(typeof(JobProfile));
builder.Services.AddTransient<IJobRepository, JobRepository>(); 
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(
        policyBuilder =>
        {
            policyBuilder.AllowAnyHeader();
            policyBuilder.AllowAnyOrigin();
            policyBuilder.AllowAnyMethod();
        }));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();