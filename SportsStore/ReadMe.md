LINQ allows easy SQL statements in C#

url routing example:
routes.MapRoute(
name: null,
template: "Page{productPage:int}",
defaults: new { controller = "Product",
action = "List", productPage = 1 }
)
Why are all the name fields null?

To enable session:
services.AddMemoryCache();
services.AddSession();
app.UseSession();

