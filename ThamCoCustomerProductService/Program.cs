using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ThamCoCustomerProductService;
using ThamCoCustomerProductService.Data;
using ThamCoCustomerProductService.Repositiory;
using ThamCoCustomerProductService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton(builder.Configuration);
builder.Services.AddDbContext<ProductDbContext>(options =>
{
    var conf = builder.Configuration.GetConnectionString("DbConnection");
    options.UseSqlServer(conf);
});

var autoMapperConfig = new MapperConfiguration(c => c.AddProfile(new MapperProfile()));
IMapper mapper = autoMapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddMemoryCache();
builder.Services.AddScoped<ICompanyProductsRepository, CompanyProductsRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICompanyProductsService, CompanyProductsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");
});

app.Run();
