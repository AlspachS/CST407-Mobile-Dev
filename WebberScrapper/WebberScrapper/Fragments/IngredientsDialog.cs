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

namespace WebberScrapper.Fragments
{
    class IngredientsDialog : DialogFragment
    {
        public string RName { get; set; }

        public static IngredientsDialog NewInstance(Bundle bundle)
        {
            IngredientsDialog fragment = new IngredientsDialog();
            fragment.Arguments = bundle;

            return fragment;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.Ingredients, container, false);

            Button closeButton = view.FindViewById<Button>(Resource.Id.close_button);
            closeButton.Click += delegate { Dismiss(); };

            Button copyButton = view.FindViewById<Button>(Resource.Id.copy_button);
            copyButton.Click += delegate
            {
                Intent intent = new Intent(Intent.ActionSend);
                intent.SetType("text/plain");
                intent.PutExtra(Intent.ExtraText, Arguments.GetString("RIngredients"));
                StartActivity(intent);
            };

            //TextView RName = view.FindViewById<TextView>(Resource.Id.ingredient_fragment_name);
            //RName.Text = Arguments.GetString("RName");
            TextView RIngredients = view.FindViewById<TextView>(Resource.Id.ingredient_fragment_ingredient_list);
            RIngredients.Text = Arguments.GetString("RIngredients");

            Dialog.SetTitle(Arguments.GetString("RName"));

            return view;
        }
    }
}