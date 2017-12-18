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
    using System.Collections.ObjectModel;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ExpressionExamples.Add(new ExpressionExample("x and not x"));
            ExpressionExamples.Add(new ExpressionExample("(x and y and not z) imp (x and not z)"));
            ExpressionExamples.Add(new ExpressionExample("(x or not x) imp (x and not x)"));
            ExpressionExamples.Add(new ExpressionExample("any (n) ((n gt 10) imp (any (n) (n gt 15)))"));
            ExpressionExamples.Add(new ExpressionExample("any (n) ((n gt 10) imp (n ls 0))"));
            ExpressionExamples.Add(new ExpressionExample("any (n) ((n gt 10) imp (n gt 5))"));
            ExpressionExamples.Add(new ExpressionExample("any (n) ((n gt 10) imp ((n add 15) gt 40))"));
            ExpressionExamples.Add(new ExpressionExample("any (n) ((n gt 10) imp ((n add 15) gt 23))"));
            ExpressionExamples.Add(new ExpressionExample("(x and not x) or x"));
        }

        public ObservableCollection<ExpressionExample> ExpressionExamples { get; } = new ObservableCollection<ExpressionExample>();
 

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

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var label = (Label)sender;
            var item = (ExpressionExample)label.DataContext;
            txtExpression.Text = item.Text;
        }
    }
}
