using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventoryControlSytem
{
    /// <summary>
    /// Interaction logic for SearchInventory.xaml
    /// </summary>
    /// 
  [Export(typeof(IControl))]
    public partial class SearchInventory : UserControl, IControl
    {
        public SearchInventory()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You clicked Search button in the application.");
        }
    }
}
