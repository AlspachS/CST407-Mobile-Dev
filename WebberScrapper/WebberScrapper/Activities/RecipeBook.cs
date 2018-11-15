using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WebberScrapper.Fragments;
using WebberScrapper.Utilities;

namespace WebberScrapper.Activities
{
    [Activity(Label = "RecipeBook")]
    public class RecipeBook : ListActivity
    {
        private static SQLiteDatabase db;
        private static ICursor cursor;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SQLiteOpenHelper recipeBookHelper = new RecipeBookSQLHelper(this);
            db = recipeBookHelper.ReadableDatabase;

            cursor = db.Query("RECIPE", new string[] { "_id, NAME" }, null, null, null, null, null);
            CursorAdapter recipes = new SimpleCursorAdapter(this, Android.Resource.Layout.SimpleListItem1, cursor, new string[] { "NAME" }, new int[] { Android.Resource.Id.Text1 }, 0);

            ListAdapter = recipes;

            ListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                Intent intent = new Intent(this, typeof(RecipeActivity));
                intent.PutExtra(RecipeActivity.EXTRA_RECIPE_NUM, e.Position);
                StartActivity(intent);
            };

            ListView.ItemLongClick += (object sender, AdapterView.ItemLongClickEventArgs e) =>
            {
                //Toast.MakeText(this, ListAdapter.GetItem(e.Position).ToString(), ToastLength.Short).Show();
                // create ingredient fragment so ingredients can be previewed

                FragmentTransaction ft = FragmentManager.BeginTransaction();
                Fragment prev = FragmentManager.FindFragmentByTag("dialog");

                if(prev != null)
                {
                    ft.Remove(prev);
                }
                ft.AddToBackStack(null);

                Bundle strings = new Bundle();
                int recipeNumber = e.Position;

                cursor = db.Query("RECIPE", new string[] { "NAME", "INGREDIENTS", "DIRECTIONS" }, "_id = ?", new string[] { (++recipeNumber).ToString() }, null, null, null);
                if (cursor.MoveToFirst())
                {
                    strings.PutString("RName", cursor.GetString(0));
                    strings.PutString("RIngredients", cursor.GetString(1));
                }

                IngredientsDialog newFragment = IngredientsDialog.NewInstance(strings);
                newFragment.Show(ft, "dialog");
            };
        }
    }
}