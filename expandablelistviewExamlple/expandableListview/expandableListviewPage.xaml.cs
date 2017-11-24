// ########################################
// Name  : expandableListviewPage.xaml.cs
// Author  : Pradip Dhanraj.
// CreatedOn  : 10-10-2017
// ########################################
using System.Collections.Generic;
using Xamarin.Forms;

namespace expandableListview
{
    public partial class expandableListviewPage : ContentPage
    {
        public expandableListviewPage ()
        {
            InitializeComponent ();
        }
    }


    public class XListView : View
    {
        public List<string> headerList;
        public Dictionary<string, List<string>> childList;
        public ContentPage instance;
    }

}
