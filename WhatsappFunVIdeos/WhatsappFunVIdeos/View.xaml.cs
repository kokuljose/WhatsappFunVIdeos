using Ads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WhatsappFunVIdeos
{

    [XamlCompilation(XamlCompilationOptions.Compile) ]
    public partial class View : ContentPage
    {
       

        public View(string item)
        {
            InitializeComponent();
            WebView browser = new WebView();
            browser.Source = "https://www.youtube.com/embed/" + item;
            
            Content = browser;

        }


    }
}