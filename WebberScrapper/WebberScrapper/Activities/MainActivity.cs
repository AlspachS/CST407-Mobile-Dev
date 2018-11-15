using Android.App;
using Android.Widget;
using Android.OS;
using WebberScrapper.Activities;
using Android.Content;

namespace WebberScrapper
{
    [Activity(Label = "WebberScrapper", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);

            Button recipeBook = FindViewById<Button>(Resource.Id.recipe_book);
            recipeBook.Click += delegate
            {
                StartActivity(new Intent(this, typeof(RecipeBook)));
            };

            Button addRecipe = FindViewById<Button>(Resource.Id.add_recipe);
            addRecipe.Click += delegate
            {
                StartActivity(new Intent(this, typeof(EditRecipe)));
            };
        }

    }
}

