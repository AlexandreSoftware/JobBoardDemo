using System.Data.Common;
using JobBoardRepository;
using JobBoardRepository.Interface;
using JobBoardServices;
using JobBoardServices.Interface;
using JobBoardServices.Profile;
using JobBoardServices.Service;
using JobBoardServices.View;
using Serilog;
var builder = WebApplication.CreateBuilder(args);
//serilog
builder.Host.UseSerilog((ctx, lc) =>
    
    lc
        .WriteTo.Console()
        .WriteTo.Seq("http://localhost:5341")
        .WriteTo.Elasticsearch("http://localhost:9200")
        .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IJobService, JobService>();
builder.Services.AddTransient<IApplicantService, ApplicantService>();
builder.Services.AddTransient<IJobApplicantService, JobApplicantService>();
builder.Services.AddAutoMapper(typeof(JobProfile),typeof(ApplicantProfile));
builder.Services.AddTransient<IDapperWrapper,DapperWrapper>(x=>new DapperWrapper(builder.Configuration.GetValue<string>("DefaultConnection")));
builder.Services.AddTransient<IJobRepository, JobRepository>();
builder.Services.AddTransient <IApplicantRepository,ApplicantRepository>();
builder.Services.AddTransient<IJobApplicantRepository,JobApplicantRepository>();
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

if (app.Environment.IsProduction())
{
    Seeder.PublishMigrate(builder.Configuration.GetValue<string>("DefaultConnectionNodb"));
}
if (app.Environment.IsDevelopment())
{
    Seeder.MigrateAndSeed(builder.Configuration.GetValue<string>("DefaultConnectionNodb"),
    builder.Configuration.GetValue<string>("DefaultConnection"));
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();