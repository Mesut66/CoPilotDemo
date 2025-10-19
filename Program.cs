// inside builder.Services configuration
builder.Services.AddControllersWithViews()
    .AddJsonOptions(opts =>
    {
        // Preserve PascalCase property names in JSON
        opts.JsonSerializerOptions.PropertyNamingPolicy = null;
    });
