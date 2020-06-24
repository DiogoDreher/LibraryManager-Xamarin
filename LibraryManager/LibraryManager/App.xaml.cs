using LibraryManager.Views;
using LibraryManager.Wrapper;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LibraryManager
{
    public partial class App : Application
    {
        static dbHelper _globalDB;

        public static dbHelper Database
        {
            get
            {
                if (_globalDB == null)
                {
                    _globalDB = new dbHelper(
                            Path.Combine(Environment.GetFolderPath(
                                    Environment.SpecialFolder.LocalApplicationData
                                ), "LibraryManager.db3")
                        );
                }
                return _globalDB;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Intro());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
