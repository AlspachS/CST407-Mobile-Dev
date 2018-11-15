using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace WebberScrapper.Classes
{
    class Recipe
    {
        private string mName;
        public string RecipeName
        {
            get { return mName; }
        }

        private string mIngredients;
        public string RecipeIngredients
        {
            get { return mIngredients; }
        }

        private string mDirections;
        public string RecipeDirections
        {
            get { return mDirections; }
        }

        public override string ToString()
        {
            return mName;
        }

        public Recipe(string name, string ingredients, string directions)
        {
            mName = name;
            mIngredients = ingredients;
            mDirections = directions;
        }
    }
}