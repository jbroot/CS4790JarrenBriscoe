Use input type="hidden" to pass values hidden from the user.

Font Awesome has a lot of icons

Use $ before string for interpolation.
$"{product.Name} has been saved";

TempData is a dictionary with values that persist until read.

ViewBag passes data between controller and view. Cannot hold data longer than current HTTP request.

Session data persists until explicitly removed.

asp-validation-for to validate error messages [] in controller.
To add CSS for this: style>
.input-validation-error { border-color: red; background-color: #fee ; }
</style


//creates empty Edit view

public ViewResult Create() => View("Edit", new Product());
