Why is a lemma required here:
services.AddDbContext<ApplicationDbContext>(options =>
 options.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"]));
