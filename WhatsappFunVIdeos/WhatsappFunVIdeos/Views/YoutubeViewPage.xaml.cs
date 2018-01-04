using Ads;
using System;
using WhatsappFunVIdeos;
using Xamarin.Forms;
using XamarinForms.Models;
using XamarinForms.ViewModels;

namespace XamarinForms.Views
{
    public class YoutubeViewPage : ContentPage
    {
        
        public YoutubeViewPage()
        {

            var youtubeViewModel = new YoutubeViewModel();

            BindingContext = youtubeViewModel;
            Title = "WhatsApp Fun Videos";
            BackgroundColor = Color.FromHex("#C70039");
            var adBanner = new AdBanner();
            adBanner.Size = AdBanner.Sizes.Standardbanner;
            var dataTemplate = new DataTemplate(() =>
            {

                var titleLabel = new Label
                {
                    TextColor = Color.White,
                    FontSize = 20
                };

                var mediaImage = new Image
                {
                    HeightRequest = 205
                };

                titleLabel.SetBinding(Label.TextProperty, new Binding("Title"));
                mediaImage.SetBinding(Image.SourceProperty, new Binding("MediumThumbnailUrl"));

                return new ViewCell
                {
                    View = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        Padding = new Thickness(0, 0),
                        Children =
                        {

                            titleLabel,
                            mediaImage,
                        }
                    }
                };
            });

            var listView = new ListView
            {
                HasUnevenRows = true
            };

            listView.SetBinding(ListView.ItemsSourceProperty, "YoutubeItems");

            listView.ItemTemplate = dataTemplate;

            listView.ItemTapped += ListViewOnItemTapped;

            Content = new StackLayout
            {
                Padding = new Thickness(0, 0),
                Children =
                {
                    listView,
                    adBanner
                }
            };
        }

        private async void ListViewOnItemTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
        {
            var youtubeItem = itemTappedEventArgs.Item as YoutubeItem;
            string item = youtubeItem?.VideoId;

            await Navigation.PushAsync(new WhatsappFunVIdeos.View(item));
        }
    }
}

///// if you prefer to work with XAML code, 
///// use the following which is similar to the above code


//  <ContentPage.BindingContext>
//    <viewModels:YoutubeViewModel/>
//  </ContentPage.BindingContext>

//  <StackLayout Padding = "5,10"
//               BackgroundColor="White">

//    <Label Text = "Youtube"
//           TextColor="Black"
//           FontSize="22"/>

//    <ListView ItemsSource = "{Binding YoutubeItems}"
//              HasUnevenRows="True">
//      <ListView.ItemTemplate>
//        <DataTemplate>
//          <ViewCell>

//            <StackLayout Orientation = "Vertical"
//                         Padding="10,10,10,20">
//              <Label Text = "{Binding ChannelTitle}"
//                     TextColor="Maroon"
//                     FontSize="22"/>
//              <StackLayout Orientation = "Horizontal" >
//                < Label Text="{Binding ViewCount, StringFormat='{0:n0} views'}"
//                       TextColor="#0D47A1"
//                       FontSize="14"/>
//                <Label Text = "{Binding LikeCount, StringFormat='{0:n0} likes'}"
//                       TextColor="#2196F3"
//                       FontSize="14"/>
//                <Label Text = "{Binding DislikeCount, StringFormat='{0:n0} dislikes'}"
//                         TextColor="#0D47A1"
//                         FontSize="14"/>
//              </StackLayout>
//              <StackLayout Orientation = "Horizontal"
//                           TranslationY="-7">
//                <Label Text = "{Binding FavoriteCount, StringFormat='{0:n0} favorites'}"
//                      TextColor="#2196F3"
//                      FontSize="14"/>
//                <Label Text = "{Binding CommentCount, StringFormat='{0:n0} comments'}"
//                       TextColor="#0D47A1"
//                       FontSize="14"/>
//              </StackLayout>
//              <Label Text = "{Binding Title}"
//                     TextColor="Black"
//                     FontSize="16"/>
//              <Image Source = "{Binding HighThumbnailUrl}"
//                     HeightRequest="200"/>
//              <Label Text = "{Binding Description}"
//                     TextColor="Gray"
//                     FontSize="14"/>
//            </StackLayout>

//          </ViewCell>
//        </DataTemplate>
//      </ListView.ItemTemplate>
//    </ListView>
//  </StackLayout>