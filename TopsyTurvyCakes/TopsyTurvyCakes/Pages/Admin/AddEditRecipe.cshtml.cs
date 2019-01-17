using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopsyTurvyCakes.Models;
using Microsoft.AspNetCore.Authorization;
//use IFormFile
using Microsoft.AspNetCore.Http;
using System.IO;
using System.ComponentModel.DataAnnotations;

namespace TopsyTurvyCakes.Pages.Admin
{
    [Authorize]
    public class AddEditRecipeModel : PageModel
    {
        [FromRoute]
        public long? Id { get; set; }
        public bool IsNewRecipe {
            get { return Id == null; }
        }

        [BindProperty]
        public IFormFile image { get; set; }

        [BindProperty]
        public Recipe Recipe { get; set; }
        public IRecipesService recipesService { get; set; }
        public AddEditRecipeModel(IRecipesService recipesService)
        {
            this.recipesService = recipesService;
        }

        [HttpGet]
        public async Task OnGetAsync()
        {
            Recipe = await recipesService.FindAsync(Id.GetValueOrDefault()) ?? new Recipe();
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //update save and redirect methods to point to this local instance instead of recipe property
            //copy posted data and update this instance you just retrieved from the database. 
            //1st copy name property, description, ingredients, and directions properties. image data is still here
            Recipe postRecipe = await recipesService.FindAsync(Recipe.Id) ?? new Recipe();
            postRecipe.Name = Recipe.Name;
            postRecipe.Description = Recipe.Description;
            postRecipe.Ingredients = Recipe.Ingredients;
            postRecipe.Directions = Recipe.Directions;
            //TODO:
            //retrieve the uploaded data from image property
            //copy to temporary stream then read image's bytes
            //use system.io.MemoryStream in using statement
            //copy uploaded image bytes from property to stream with CopyToAsync
            //convert to bytes and assign to the recipe object with memory streams ToArray()
            //get image's content type from metadata

            if (image != null)
            {
                Recipe.SetImage(image);
                using (var targetStream = new MemoryStream())
                {
                    await image.CopyToAsync(targetStream);
                    postRecipe.Image = targetStream.ToArray();
                }
                postRecipe.ImageContentType = image.ContentType;
            }
            else
            {
                postRecipe.Image = Recipe.Image;
            }
            
            await recipesService.SaveAsync(postRecipe);
            return RedirectToPage("/Recipe", new { id = postRecipe.Id });
        }

        public async Task<IActionResult> OnPostDelete()
        {
            await recipesService.DeleteAsync(Id.Value);
            return RedirectToPage("/Index");
        }
    }
}