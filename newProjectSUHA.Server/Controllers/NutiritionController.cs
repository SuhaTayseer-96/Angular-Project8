﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using newProjectSUHA.Server.Dtos;
using newProjectSUHA.Server.Models;

namespace newProjectSUHA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutiritionController : ControllerBase
    {

        private readonly MyDbContext _db;

        public NutiritionController(MyDbContext db)
        {
            _db = db;
        }

        //================ User Cycle ================
        [HttpGet("RecipesCategory")]
        public IActionResult Food()
        {
            var food = _db.RecipesCategories.ToList();
            return Ok(food);
        }

        [HttpGet("Recipes/{id}")]
        public IActionResult SubFood(int id)
        {
            var SubFood = _db.Recipes.Where(a => a.CategoryId == id);
            return Ok(SubFood);
        }
        [HttpGet("Recipesdetels/{id}")]
        public IActionResult Recipesdetels(int id)
        {
            var SubFood = _db.Recipes.Where(a => a.Id == id).FirstOrDefault();
            return Ok(SubFood);
        }



        [HttpGet("Tips")]
        public IActionResult Tips()
        {
            var Tips = _db.Tips.ToList();
            return Ok(Tips);
        }

        //================== Admin Cycle ==================
        [HttpGet("RecipesBlog")]
        public IActionResult GetLatestNews()
        {
            var blog = _db.Recipes

                .OrderByDescending(n => n.Id)
                .Take(5)
                .Select(n => new
                {
                    CategoryId = n.CategoryId, // Include PostId
                    Name = n.Name,
                    Image = n.Image,
                    Description = n.Description,
                    NutritionalFacts = n.NutritionalFacts
                })
                .ToList();

            return Ok(blog);
        }

        [HttpPost("recipepost")]
        public IActionResult recipepost([FromForm] RecipeDTO Recipedto) {
            Recipe newitime = new Recipe()
            {
                Name= Recipedto.Name,
                Image = Recipedto.Image,
                Description = Recipedto.Description,
                NutritionalFacts=Recipedto.NutritionalFacts,
                CategoryId = Recipedto.CategoryId,

            };

            _db.Recipes.Add(newitime);
            _db.SaveChanges();

            return Ok(newitime);
        }


        
    }
}
