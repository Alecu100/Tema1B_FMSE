using System.Collections.Generic;
using Microsoft.Z3;
using Tema1B_FMSE.SyntaxNodes;

namespace Tema1B_FMSE
{
    public class BooleanExpressionsProver
    {
        private Dictionary<string, BoolExpr> _createdBoolSymbols;
        private Dictionary<string, IntExpr> _createdIntSymbols;
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
                return true;

            return false;
        }

        private void ConstructProof(SyntaxTree syntaxTree)
        {
            _expressionTree = (BoolExpr)ConstructExpression(syntaxTree.RootValue);
        }

        private Expr ConstructExpression(SyntaxNode syntaxNode)
        {
            if (syntaxNode is BlockExpressionSyntaxNode)
            {
                var blockExpressionSyntaxNode = (BlockExpressionSyntaxNode) syntaxNode;

                return ConstructExpression(blockExpressionSyntaxNode.InnerValue);
            }

            if (syntaxNode is SymbolSyntaxNode)
            {
                var symbolSyntaxNode = (SymbolSyntaxNode) syntaxNode;

                if (symbolSyntaxNode.SymbolKind == ESymbolKinds.Boolean)
                {
                    if (_createdBoolSymbols.ContainsKey(symbolSyntaxNode.Id))
                        return _createdBoolSymbols[symbolSyntaxNode.Id];

                    var boolExpr = _currentContext.MkBoolConst(symbolSyntaxNode.Id);
                    _createdBoolSymbols[symbolSyntaxNode.Id] = boolExpr;
                    return boolExpr;
                }

                if (_createdIntSymbols.ContainsKey(symbolSyntaxNode.Id))
                    return _createdIntSymbols[symbolSyntaxNode.Id];

                var intExpr = _currentContext.MkIntConst(symbolSyntaxNode.Id);
                _createdIntSymbols[symbolSyntaxNode.Id] = intExpr;
                return intExpr;
            }

            if (syntaxNode is ConstantValueSyntaxNode)
            {
                var constantValueSyntaxNode = (ConstantValueSyntaxNode) syntaxNode;

                if (constantValueSyntaxNode.Value is bool)
                    return _currentContext.MkBool((bool) constantValueSyntaxNode.Value);

                return _currentContext.MkInt((int) constantValueSyntaxNode.Value);
            }

            if (syntaxNode is BinaryExpressionSyntaxNode)
            {
                var binaryExpressionSyntaxNode = (BinaryExpressionSyntaxNode) syntaxNode;

                if (binaryExpressionSyntaxNode.OperationKind == EOperationKinds.And)
                {
                    var leftExpression = (BoolExpr)ConstructExpression(binaryExpressionSyntaxNode.LeftValue);
                    var rightExpression = (BoolExpr)ConstructExpression(binaryExpressionSyntaxNode.RightValue);

                    return _currentContext.MkAnd(leftExpression, rightExpression);
                }


                if (binaryExpressionSyntaxNode.OperationKind == EOperationKinds.Or)
                {
                    var leftExpression = (BoolExpr)ConstructExpression(binaryExpressionSyntaxNode.LeftValue);
                    var rightExpression = (BoolExpr)ConstructExpression(binaryExpressionSyntaxNode.RightValue);

                    return _currentContext.MkOr(leftExpression, rightExpression);
                }


                if (binaryExpressionSyntaxNode.OperationKind == EOperationKinds.Implication)
                {
                    var leftExpression = (BoolExpr)ConstructExpression(binaryExpressionSyntaxNode.LeftValue);
                    var rightExpression = (BoolExpr)ConstructExpression(binaryExpressionSyntaxNode.RightValue);

                    return _currentContext.MkImplies(leftExpression, rightExpression);
                }

                if (binaryExpressionSyntaxNode.OperationKind == EOperationKinds.Add)
                {
                    var leftExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.LeftValue);
                    var rightExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.RightValue);

                    return _currentContext.MkAdd(leftExpression, rightExpression);
                }

                if (binaryExpressionSyntaxNode.OperationKind == EOperationKinds.Divide)
                {
                    var leftExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.LeftValue);
                    var rightExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.RightValue);

                    return _currentContext.MkDiv(leftExpression, rightExpression);
                }

                if (binaryExpressionSyntaxNode.OperationKind == EOperationKinds.Multiply)
                {
                    var leftExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.LeftValue);
                    var rightExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.RightValue);

                    return _currentContext.MkMul(leftExpression, rightExpression);
                }

                if (binaryExpressionSyntaxNode.OperationKind == EOperationKinds.Substract)
                {
                    var leftExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.LeftValue);
                    var rightExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.RightValue);

                    return _currentContext.MkSub(leftExpression, rightExpression);
                }

                if (binaryExpressionSyntaxNode.OperationKind == EOperationKinds.Less)
                {
                    var leftExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.LeftValue);
                    var rightExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.RightValue);

                    return _currentContext.MkLt(leftExpression, rightExpression);
                }

                if (binaryExpressionSyntaxNode.OperationKind == EOperationKinds.LessOrEqual)
                {
                    var leftExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.LeftValue);
                    var rightExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.RightValue);

                    return _currentContext.MkLe(leftExpression, rightExpression);
                }

                if (binaryExpressionSyntaxNode.OperationKind == EOperationKinds.Equal)
                {
                    var leftExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.LeftValue);
                    var rightExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.RightValue);

                    return _currentContext.MkEq(leftExpression, rightExpression);
                }

                if (binaryExpressionSyntaxNode.OperationKind == EOperationKinds.GreaterOrEqual)
                {
                    var leftExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.LeftValue);
                    var rightExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.RightValue);

                    return _currentContext.MkGe(leftExpression, rightExpression);
                }

                if (binaryExpressionSyntaxNode.OperationKind == EOperationKinds.Greater)
                {
                    var leftExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.LeftValue);
                    var rightExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.RightValue);

                    return _currentContext.MkGt(leftExpression, rightExpression);
                }
            }

            if (syntaxNode is UnaryExpressionSyntaxNode)
            {
                var unaryExpressionSyntaxNode = (UnaryExpressionSyntaxNode) syntaxNode;
                var innerExpression = (BoolExpr) ConstructExpression(unaryExpressionSyntaxNode.InnerValue);

                if (unaryExpressionSyntaxNode.OperationKind == EOperationKinds.Not)
                    return _currentContext.MkNot(innerExpression);
            }

            return null;
        }

        private void Initialize()
        {
            _currentContext = new Context();
            _createdBoolSymbols = new Dictionary<string, BoolExpr>();
            _createdIntSymbols = new Dictionary<string, IntExpr>();
        }
    }
}
