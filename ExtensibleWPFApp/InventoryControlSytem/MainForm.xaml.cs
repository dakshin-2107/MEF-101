using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
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
    /// Interaction logic for MainForm.xaml
    /// </summary>
    /// 
        [Export(typeof(UserControl))]
    public partial class MainForm : UserControl 
    {

         [ImportMany(typeof(IControl))]
          List<Lazy<IControl>> controls {get; set;}

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnShowIAddInventoryForm_Click(object sender, RoutedEventArgs e)
        {
            //Load the file

            IControl addInventoryControl = ((from uc in controls
                                                where uc.Value.ToString() == "InventoryControlSytem.AddInventory"
                                                select uc.Value
                                                   ).FirstOrDefault());

            LoadControl.Children.Clear();
            LoadControl.Children.Add(addInventoryControl as UserControl);

            
        }

        private void btnShowIAddInventoryForm_Copy_Click(object sender, RoutedEventArgs e)
        {

            IControl addInventoryControl = ((from uc in controls
                                             where uc.Value.ToString() == "InventoryControlSytem.SearchInventory"
                                             select uc.Value
                                                   ).FirstOrDefault());


            LoadControl.Children.Clear();
            LoadControl.Children.Add(addInventoryControl as UserControl);
        }
    }
}
