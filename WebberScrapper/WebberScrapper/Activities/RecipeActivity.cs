using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WebberScrapper.Utilities;

namespace WebberScrapper.Activities
{
    [Activity(Label = "RecipeActivity")]
    public class RecipeActivity : Activity
    {
        public static string EXTRA_RECIPE_NUM = "RecipeNumber";
        private string name;
        private string ingredients;
        private string directions;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Recipe);

            int recipeNumber = Intent.GetIntExtra(RecipeActivity.EXTRA_RECIPE_NUM, 0);
            try
            {
                SQLiteOpenHelper cookbookHelper = new RecipeBookSQLHelper(this);
                SQLiteDatabase db = cookbookHelper.ReadableDatabase;

                ICursor cursor = db.Query("RECIPE", new string[] { "NAME", "INGREDIENTS", "DIRECTIONS" }, "_id = ?", new string[] { (++recipeNumber).ToString() }, null, null, null);

                if (cursor.MoveToFirst())
                {
                    name = cursor.GetString(0);
                    ingredients = cursor.GetString(1);
                    directions = cursor.GetString(2);
                }
            }
            catch (SQLException e)
            {
                Toast.MakeText(this, "Cookbook unavailable", ToastLength.Short).Show();
            }

            TextView recipeName = FindViewById<TextView>(Resource.Id.recipe_name);
            recipeName.Text = name;

            TextView ingredientList = FindViewById<TextView>(Resource.Id.ingredient_list);
            ingredientList.Text = ingredients;

            TextView directionList = FindViewById<TextView>(Resource.Id.direction_list);
            directionList.Text = directions;

            ImageButton editButton = FindViewById<ImageButton>(Resource.Id.edit_recipe);
            editButton.Click += OpenRecipeForEditing;
        }

        private void OpenRecipeForEditing(object sender, EventArgs e)
        {
            ISharedPreferences shared = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editor = shared.Edit();
            editor.PutString("RecipeName", name);
            editor.PutString("RecipeIngredients", ingredients);
            editor.PutString("RecipeDirections", directions);
            editor.Apply();

            StartActivity(new Intent(this, typeof(EditRecipe)));
        }
    }
}