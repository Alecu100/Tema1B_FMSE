using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tema1B_FMSE.SyntaxNodes;
using Microsoft.Z3;

namespace Tema1B_FMSE
{
    public class BooleanExpressionsProver
    {
        private Dictionary<string, BoolExpr> _createdSymbols;
        private Context _currentContext;
        private BoolExpr _expressionTree;

        public bool IsSatisfiable(SyntaxTree syntaxTree)
        {
            Initialize();
            ConstructProof(syntaxTree);

            return CheckIfIsSatisfiable();
        }

        private bool CheckIfIsSatisfiable()
        {
            var solver = _currentContext.MkSolver();
            solver.Assert(_expressionTree);
            var status = solver.Check();

            if (status == Status.SATISFIABLE)
            {
                return true;
            }

            return false;
        }

        private void ConstructProof(SyntaxTree syntaxTree)
        {
            _expressionTree = ConstructExpression(syntaxTree.RootValue);
        }

        private BoolExpr ConstructExpression(SyntaxNode syntaxNode)
        {
            if (syntaxNode is BlockExpressionSyntaxNode)
            {
                var blockExpressionSyntaxNode = (BlockExpressionSyntaxNode) syntaxNode;

                return ConstructExpression(blockExpressionSyntaxNode.InnerValue);
            }

            if (syntaxNode is SymbolSyntaxNode)
            {
                var symbolSyntaxNode = (SymbolSyntaxNode)syntaxNode;

                if (_createdSymbols.ContainsKey(symbolSyntaxNode.Id))
                {
                    return _createdSymbols[symbolSyntaxNode.Id];
                }

                var boolExpr = _currentContext.MkBoolConst(symbolSyntaxNode.Id);
                _createdSymbols[symbolSyntaxNode.Id] = boolExpr;
                return boolExpr;
            }

            if (syntaxNode is BinaryExpressionSyntaxNode)
            {
                var binaryExpressionSyntaxNode = (BinaryExpressionSyntaxNode) syntaxNode;

                var leftExpression = ConstructExpression(binaryExpressionSyntaxNode.LeftValue);
                var rightExpression = ConstructExpression(binaryExpressionSyntaxNode.RightValue);

                if (binaryExpressionSyntaxNode.OperationKind == EOperationKinds.And)
                {
                    return _currentContext.MkAnd(leftExpression, rightExpression);
                }

                if (binaryExpressionSyntaxNode.OperationKind == EOperationKinds.Or)
                {
                    return _currentContext.MkOr(leftExpression, rightExpression);
                }

                if (binaryExpressionSyntaxNode.OperationKind == EOperationKinds.Implication)
                {
                    return _currentContext.MkImplies(leftExpression, rightExpression);
                }
            }

            if (syntaxNode is UnaryExpressionSyntaxNode)
            {
                var unaryExpressionSyntaxNode = (UnaryExpressionSyntaxNode) syntaxNode;
                var innerExpression = ConstructExpression(unaryExpressionSyntaxNode.InnerValue);

                if (unaryExpressionSyntaxNode.OperationKind == EOperationKinds.Not)
                {
                    return _currentContext.MkNot(innerExpression);
                }
            }

            return null;
        }

        private void Initialize()
        {
            _currentContext = new Context();
            _createdSymbols = new Dictionary<string, BoolExpr>();
        }
    }
}
