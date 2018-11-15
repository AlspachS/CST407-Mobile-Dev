using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WebberScrapper.Classes;

namespace WebberScrapper.Utilities
{
    class RecipeBookSQLHelper : SQLiteOpenHelper
    {
        private static String DBName = "Cookbook";
        private static int DBVersion = 1;

        public RecipeBookSQLHelper(Context context) : base(context, DBName, null, DBVersion)
        { }

        public override void OnCreate(SQLiteDatabase db)
        {
            db.ExecSQL("CREATE TABLE RECIPE (_id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "NAME TEXT, INGREDIENTS TEXT, DIRECTIONS TEXT);");
        }

        public static void InsertRecipe(SQLiteDatabase db, string name, string ingredients, string directions)
        {
            ContentValues recipe = new ContentValues();
            recipe.Put("NAME", name);
            recipe.Put("INGREDIENTS", ingredients);
            recipe.Put("DIRECTIONS", directions);
            db.Replace("RECIPE", null, recipe);
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            
        }
    }
}