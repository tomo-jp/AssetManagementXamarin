using System;
using Xamarin.Forms;

namespace AssetManagementXamarin.Behaviors
{
    internal class PlainNumericEntryBehavior : Behavior<Entry>
    {
        protected Func<double, bool> AdditionalCheck;
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += TextChanged_Handler;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= TextChanged_Handler;
        }

        protected virtual void TextChanged_Handler(object sender, TextChangedEventArgs e)
        {
            var entry = sender as Entry;
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                entry.Text = 0.ToString();
                return;
            }

            double val;
            if (!double.TryParse(e.NewTextValue, out val))
                entry.Text = e.OldTextValue;
            else if (!AdditionalCheck?.Invoke(val) ?? false)
                entry.Text = e.OldTextValue;
            else
                entry.Text = val.ToString();
        }
    }
}
