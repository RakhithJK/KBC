﻿using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.Linq;

namespace KBC
{
    public class MoneyTreeFragment : DialogFragment
    {
        public static MoneyTreeFragment Create(int Answered)
        {
            var f = new MoneyTreeFragment();
     
            var args = new Bundle();
            args.PutInt(nameof(Answered), Answered);
            f.Arguments = args;
                        
            return f;
        }

        LinearLayout layout;
        
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Dialog?.SetTitle("Money Tree");
            
            var scroller = new ScrollView(Activity);
            layout = new LinearLayout(Activity)
            {
                Orientation = Orientation.Vertical
            };

            if (Dialog != null)
                layout.SetPadding(25, 25, 25, 25);

            scroller.AddView(layout);
                                 
            var answered = Arguments.GetInt("Answered");

            for (int i = Question.Amounts.Length - 1; i >= 0; --i)
            {
                var b = new TextView(Activity)
                {
                    Text = $"{(i + 1).ToString().PadLeft(2, ' ')}.\t\t ₹{Question.Amounts[i]}",
                    TextAlignment = TextAlignment.ViewEnd,
                    LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent),
                    TextSize = 20
                };

                b.SetBackgroundColor(i == answered ? Color.Orange : Color.Transparent);

                if (Question.SafeLevels.Contains(i))
                    b.SetTextColor(Color.Yellow);
                else if (i < answered)
                    b.SetTextColor(Color.LightGreen);

                layout.AddView(b);
            }

            return scroller;
        }

        public void Update(int Answered = -1)
        {
            if (Answered != -1)
                Arguments.PutInt(nameof(Answered), Answered);

            var n = layout.ChildCount;

            for (int i = 1; i <= n; ++i)
            {
                var textView = (TextView)layout.GetChildAt(n - i);
                
                textView.SetBackgroundColor(i == Answered + 1 ? Color.Orange : Color.Transparent);

                if (Question.SafeLevels.Contains(i - 1))
                    textView.SetTextColor(Color.Yellow);
                else if (i <= Answered)
                    textView.SetTextColor(Color.LightGreen);
            }
        }
    }
}

