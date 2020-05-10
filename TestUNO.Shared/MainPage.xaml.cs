using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TestUNO
{


   /// <summary>
   /// An empty page that can be used on its own or navigated to within a Frame.
   /// </summary>
   public sealed partial class MainPage : Page
   {
      public MainPage()
      {
         this.InitializeComponent();

         // - - -   - - -  

         string Token = wsToken.NewToken().GetToken();
         Token = Token + Environment.NewLine + (string)(ApplicationData.Current.LocalSettings.Values["Page"]);

         tb.Text = Token;

         ApplicationData.Current.LocalSettings.Values["Page"] = DateTime.Now.ToString();
      }

      private async void MainPage_Loaded(object sender, RoutedEventArgs e)
      {
         HttpClient http = new HttpClient
         {
            BaseAddress = new Uri(@"http://wsstorecheckdev.zpf.fr/"),
         };

         try
         {
            string BasicAuth = "StoreCheck:ZPF";
            byte[] byteArray = Encoding.ASCII.GetBytes(BasicAuth);
            http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

            string Login = "ME";
            string Password = "ME";
            Password = UserViewModel.Current.Salt(Login, Password);

            var st = await http.GetStringAsync(new Uri($@"http://wsDev.ZPF.rocks/StoreCheck/User/Login/{Login}/{Password}"));

            var dlg = new MessageDialog(st);
            await dlg.ShowAsync();
         }
         catch (Exception ex)
         {
            var dlg = new MessageDialog(ex.Message);
            await dlg.ShowAsync();
         };

      }
   }
}
