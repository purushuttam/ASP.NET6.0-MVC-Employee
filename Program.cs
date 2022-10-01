using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Options;
using Demo.Repository;
using Demo.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDbConnnection")));
builder.Services.AddControllersWithViews();
builder.Services.AddMvc().AddXmlSerializerFormatters();
builder.Services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();

/*builder.Services.AddDbContext<DeptContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DepartmentDbConnnection")));
builder.Services.AddControllersWithViews();
builder.Services.AddMvc().AddXmlSerializerFormatters();*/
//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDbConnnection")));

builder.Services.AddScoped<IDepartmentRepository, SQLDepartmentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//HTTPS Redirection Middleware (UseHttpsRedirection) redirects HTTP requests to HTTPS.
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=Login}/{id?}");

app.Run();
