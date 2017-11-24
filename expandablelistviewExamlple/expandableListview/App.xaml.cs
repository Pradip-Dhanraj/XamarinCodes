// ########################################
// Name  : App.xaml.cs
// Author  : Pradip Dhanraj.
// CreatedOn  : 10-10-2017
// ########################################
using Xamarin.Forms;

namespace expandableListview
{
    public partial class App : Application
    {
        public App ()
        {
            InitializeComponent ();

            MainPage = new expandableListviewPage ();
        }

        protected override void OnStart ()
        {
            // Handle when your app starts
        }

        protected override void OnSleep ()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume ()
        {
            // Handle when your app resumes
        }
    }
}
