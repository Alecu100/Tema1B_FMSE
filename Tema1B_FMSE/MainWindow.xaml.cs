using System;
using System.Collections.Generic;
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
using Tema1B_FMSE.SyntaxNodes;
using Microsoft.Z3;

namespace Tema1B_FMSE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
 

        private void BtnProve_OnClick(object sender, RoutedEventArgs e)
        {
            var syntaxTreeParser = new SyntaxTreeParser();

            var parsedSyntaxTree = syntaxTreeParser.Parse(txtExpression.Text);

            trViewSyntaxTree.ItemsSource = new List<SyntaxNode> {parsedSyntaxTree};

            var booleanExpressionsProver = new BooleanExpressionsProver();

            var isSatifiable = booleanExpressionsProver.IsSatisfiable(parsedSyntaxTree);

            if (isSatifiable)
            {
                lblIsSatisfiableVal.Content = "Yes";
            }
            else
            {
                lblIsSatisfiableVal.Content = "No";
            }
        }
    }
}
