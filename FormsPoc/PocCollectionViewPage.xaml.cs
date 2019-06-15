using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FormsPoc
{
    public partial class PocCollectionViewPage : ContentPage
    {
        List<string> peoples = new List<string>() {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11"};
        public PocCollectionViewPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }

    //public class People
    //{
         
    //}
}
