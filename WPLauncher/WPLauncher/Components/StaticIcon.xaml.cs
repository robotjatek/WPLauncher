using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WPLauncher.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StaticIcon : ContentView
    {
        public StaticIcon()
        {
            InitializeComponent();
        }
    }
}