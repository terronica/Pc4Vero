using EcommercePC4.MLModels;

var builder = WebApplication.CreateBuilder(args);

//RecommendationService
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<SentimentService>();
builder.Services.AddSingleton<RecommendationService>();

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

var env = app.Services.GetRequiredService<IWebHostEnvironment>();
var modelBuilder = new SentimentModelBuilder(env);
modelBuilder.BuildAndSaveModel();

if (!File.Exists(Path.Combine(env.ContentRootPath, "MLModels", "recommendation_model.zip")))
{
    var trainer = new RecommendationModelTrainer(env);
    trainer.TrainAndSaveModel();
}

// Middlewares
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//EcommercePC4.MLModels.SentimentModelTrainer.TrainAndSaveModel();


app.Run();
