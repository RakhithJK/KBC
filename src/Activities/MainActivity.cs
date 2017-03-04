﻿using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using System;

namespace KBC
{
    [Activity(Label = "KBC", MainLauncher = true)]
    public class MainActivity : Activity
    {
        bool playSounds;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var newGameButton = FindViewById<Button>(Resource.Id.newGameButton);
            newGameButton.SetColor(Extensions.OptionDefaultColor);
            newGameButton.Click += NewGame;

            var helpButton = FindViewById<Button>(Resource.Id.helpButton);
            helpButton.SetColor(Extensions.OptionDefaultColor);
            helpButton.Click += Help;

            var settings = GetSharedPreferences("Preferences", 0);
            playSounds = settings.GetBoolean(nameof(playSounds), false);

            var playSoundsCheck = FindViewById<CheckBox>(Resource.Id.playSoundsCheck);
            playSoundsCheck.Checked = playSounds;
            playSoundsCheck.CheckedChange += (s, e) => playSounds = e.IsChecked;
        }

        void Help(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(HelpActivity));
            StartActivity(i);
        }

        protected override void OnPause()
        {
            base.OnPause();

            var settings = GetSharedPreferences("Preferences", 0);
            var editor = settings.Edit();

            editor.PutBoolean(nameof(playSounds), playSounds);

            editor.Commit();
        }

        void NewGame(object sender, EventArgs e)
        {
            var i = new Intent(this, typeof(GameActivity));
            StartActivity(i);            
        }
    }
}
