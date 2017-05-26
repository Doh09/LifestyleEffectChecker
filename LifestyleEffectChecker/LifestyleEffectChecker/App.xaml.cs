﻿using LifestyleEffectChecker.Views;
using LifestyleEffectChecker.Views.CreateEditViews.Action;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ItemsPage = LifestyleEffectChecker.Views.IndexViews.JournalsIndexPage;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace LifestyleEffectChecker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SetMainPage();
        }

        public static void SetMainPage()
        {
            Current.MainPage = new TabbedPage
            {
                Children =
                {
                    new NavigationPage(new NewPartInformationPage())
                    {
                        Title = "PartInformation Page",
                        Icon = Device.OnPlatform("tab_feed.png",null,null)
                    },
                    new NavigationPage(new ItemsPage())
                    {
                        Title = "Browse Journals",
                        Icon = Device.OnPlatform("tab_feed.png",null,null)
                    },
                    new NavigationPage(new AboutPage())
                    {
                        Title = "About",
                        Icon = Device.OnPlatform("tab_about.png",null,null)
                    },
                    new NavigationPage(new SearchPage())
                    {
                        Title = "Search",
                        Icon = Device.OnPlatform("tab_about.png",null,null)
                    },
                    new NavigationPage(new Camera())
                    {
                        Title = "Camera",
                        Icon = Device.OnPlatform("tab_about.png",null,null)
                    },
                }
            };

        }
    }
}
