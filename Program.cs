var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); ;
builder.Services.AddDbContext<BookShelfHaven5.Models.BookShelfHavenContext>();

// Add session state
builder.Services.AddSession(options =>
{
    // Configure session options here if needed
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Use session middleware
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Register}/{action=Create}/{id?}");

app.Run();
