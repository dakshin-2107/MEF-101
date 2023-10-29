using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
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

namespace ExtensibleWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        [Import(typeof(UserControl))]
        UserControl controlToLoad;

        public MainWindow()
        {


            InitializeComponent();

            AssemblyCatalog catalog = new AssemblyCatalog(typeof(InventoryControlSytem.MainForm).Assembly);
            CompositionContainer container = new CompositionContainer(catalog);

            container.ComposeParts(this);

            this.mainGrid.Children.Add(controlToLoad);

        }
    }
}
