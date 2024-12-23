using BookStore_backend.packages;

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy to the service collection before building the app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevServer",
        policy => policy
            .WithOrigins("http://localhost:4200")  // Allow requests from Angular app
            .AllowAnyMethod()                      // Allow any HTTP method (GET, POST, etc.)
            .AllowAnyHeader());                    // Allow any headers
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register your scoped services
builder.Services.AddScoped<IPKG_USER, PKG_USER>();
builder.Services.AddScoped<IPKG_ADMIN, PKG_ADMIN>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS middleware (using the defined policy)
app.UseCors("AllowAngularDevServer");

app.UseAuthorization();

app.MapControllers();

app.Run();
