// ########################################
// Name  : AppDelegate.cs
// Author  : Surekha
// CreatedOn  : 10-10-2017
// ########################################
using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace expandableListview.iOS
{
    [Register ("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching (UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init ();

            LoadApplication (new App ());

            return base.FinishedLaunching (app, options);
        }
    }
}
