using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace CookBook.Models
{
    public class Recipe
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public RecipeType Type { get; set; } = RecipeType.Other;
        public string Ingredients { get; set; }
        public string Description { get; set; }
        public int PreparationTime { get; set; }
        // Nie działa automatyczna konwersja stringa na URL
        public string StringURL { get; set; }
        public Uri URL { get; set; }
    }
}
