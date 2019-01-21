Why is a lambda required here:
services.AddDbContext<ApplicationDbContext>(options =>
 options.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"]));
