using PersonalExpenseTracker.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ExpenseRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<IncomeRepository>();

builder.Services.AddSingleton(so=>{
    var MongoConfig = builder.Configuration.GetSection ("MongoDB");
    return new MongoDBContext(
        MongoConfig["ConnectionString"],
        MongoConfig["DatabaseName"]
     );
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
