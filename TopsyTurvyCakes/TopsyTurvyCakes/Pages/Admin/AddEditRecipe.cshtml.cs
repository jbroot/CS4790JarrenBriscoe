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

            Recipe postRecipe = await recipesService.FindAsync(Id.GetValueOrDefault()) ?? new Recipe();
            postRecipe.Name = Recipe.Name;
            postRecipe.Description = Recipe.Description;
            postRecipe.Ingredients = Recipe.Ingredients;
            postRecipe.Directions = Recipe.Directions;

            //save image
            if (image != null)
            {
                postRecipe.SetImage(image);
            }
            await recipesService.SaveAsync(Recipe);
            return RedirectToPage("/Recipe", new { id = postRecipe.Id });
        }

        public async Task<IActionResult> OnPostDelete()
        {
            await recipesService.DeleteAsync(Id.Value);
            return RedirectToPage("/Index");
        }
    }
}