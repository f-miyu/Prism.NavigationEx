using System;

using Xamarin.Forms;

namespace Prism.NavigationEx.Sample.Views
{
    public class CustomTabbedPage : TabbedPage
    {
        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create("SelectedIndex", typeof(int), typeof(CustomTabbedPage), -1, BindingMode.TwoWay, null, HandleBindingPropertyChangedDelegate);

        public int SelectedIndex
        {
            get => (int)GetValue(SelectedIndexProperty);
            set => SetValue(SelectedIndexProperty, value);
        }

        public CustomTabbedPage()
        {
            SetBinding(SelectedIndexProperty, new Binding("SelectedIndex.Value"));
        }

        static void HandleBindingPropertyChangedDelegate(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is CustomTabbedPage page)) return;

            if (page.Children.Count > (int)newValue)
            {
                //page.CurrentPage = page.Children[(int)newValue];
            }
        }
    }
}

