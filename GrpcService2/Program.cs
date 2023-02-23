/*using GrpcService2.Domain.Interfaces;
using GrpcService2.Domain.Entities;
using GrpcService2.Infrastructure.Data;
using GrpcService2.Infrastructure.Repositories;
using GrpcService2.Domain.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpcReflection();
// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<ProductService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
app.MapGrpcReflectionService();
app.Run();*/




using AutoMapper;
using GrpcService2.Domain.Extensions;
using GrpcService2.Domain.Interfaces;
using GrpcService2.Domain.Entities;
using GrpcService2.Domain.Services;
using GrpcService2.Infrastructure.Data;
using GrpcService2.Infrastructure.Repositories;
using GrpcService2.Domain.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/*
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);*/
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddAutoMapper(typeof(MapperInitializer));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//config sql
//builder.Services.AddDbContext<DemoContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<DemoContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), x => x.UseNetTopologySuite()));
//config grpc
builder.Services.AddGrpcReflection();
builder.Services.AddGrpc();


//Repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IDetailRepository, DetailRepository>();


//Service
builder.Services.AddScoped(typeof(IBaseService<,,>), typeof(BaseService<,,>));
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IDetailService, DetailService>();

//unitOfworkk
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


var app = builder.Build();

//cofig grpc
// Configure the HTTP request pipeline.
app.MapGrpcService<grpcProductService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
app.MapGrpcReflectionService();

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();