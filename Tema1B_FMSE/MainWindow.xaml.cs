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
        /*
         *   Goal g = ctx.MkGoal(true);

            ArraySort asort = ctx.MkArraySort(ctx.IntSort, ctx.MkBitVecSort(32));

            ArrayExpr aex = (ArrayExpr)ctx.MkConst(ctx.MkSymbol("MyArray"), asort);

            Expr sel = ctx.MkSelect(aex, ctx.MkInt(0));

            g.Assert(ctx.MkEq(sel, ctx.MkBV(42, 32)));

            Symbol xs = ctx.MkSymbol("x");

            IntExpr xc = (IntExpr)ctx.MkConst(xs, ctx.IntSort);



            Symbol fname = ctx.MkSymbol("f");

            Sort[] domain = { ctx.IntSort };

            FuncDecl fd = ctx.MkFuncDecl(fname, domain, ctx.IntSort);

            Expr[] fargs = { ctx.MkConst(xs, ctx.IntSort) };

            IntExpr fapp = (IntExpr)ctx.MkApp(fd, fargs);



            g.Assert(ctx.MkEq(ctx.MkAdd(xc, fapp), ctx.MkInt(123)));
        */

        private void BtnProve_OnClick(object sender, RoutedEventArgs e)
        {
            var syntaxTreeParser = new SyntaxTreeParser();

            var parsedSyntaxTree = syntaxTreeParser.Parse(txtExpression.Text);

            trViewSyntaxTree.ItemsSource = new List<SyntaxNode> {parsedSyntaxTree};
        }
    }
}
