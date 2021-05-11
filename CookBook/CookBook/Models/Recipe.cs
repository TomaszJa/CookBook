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
    }
}
