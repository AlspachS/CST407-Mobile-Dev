using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database.Sqlite;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WebberScrapper.Classes;
using WebberScrapper.Utilities;

namespace WebberScrapper.Activities
{
    [Activity(Label = "EditRecipe")]
    public class EditRecipe : Activity
    {
        private static SQLiteDatabase db;
        EditText recipeName;
        EditText recipeIngredients;
        EditText recipeDirections;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EditRecipe);

            recipeName = FindViewById<EditText>(Resource.Id.edit_recipe_name);
            recipeIngredients = FindViewById<EditText>(Resource.Id.edit_recipe_ingredients);
            recipeDirections = FindViewById<EditText>(Resource.Id.edit_recipe_directions);

            SQLiteOpenHelper recipeBookHelper = new RecipeBookSQLHelper(this);
            db = recipeBookHelper.WritableDatabase;

            CheckSharedPreferences();

            Button saveRecipe = FindViewById<Button>(Resource.Id.save_recipe);
            saveRecipe.Click += delegate
            {
                RecipeBookSQLHelper.InsertRecipe(db, recipeName.Text, recipeIngredients.Text, recipeDirections.Text);

                recipeName.Text = String.Empty;
                recipeIngredients.Text = String.Empty;
                recipeDirections.Text = String.Empty;
            };
        }

        private void CheckSharedPreferences()
        {
            ISharedPreferences shared = PreferenceManager.GetDefaultSharedPreferences(this);

            recipeName.Text = shared.GetString("RecipeName", String.Empty);
            recipeIngredients.Text = shared.GetString("RecipeIngredients", String.Empty);
            recipeDirections.Text = shared.GetString("RecipeDirections", String.Empty);

            ISharedPreferencesEditor editor = shared.Edit();
            editor.Clear().Apply();
        }
    }
}