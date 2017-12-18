namespace Tema1B_FMSE
{
    using System.Collections.Generic;

    using Microsoft.Z3;

    using Tema1B_FMSE.SyntaxNodes;

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

            if (status == Status.SATISFIABLE) return true;

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
                var blockExpressionSyntaxNode = (BlockExpressionSyntaxNode)syntaxNode;

                return ConstructExpression(blockExpressionSyntaxNode.InnerValue);
            }

            if (syntaxNode is SymbolSyntaxNode)
            {
                var symbolSyntaxNode = (SymbolSyntaxNode)syntaxNode;

                if (symbolSyntaxNode.SymbolKind == ESymbolKinds.Boolean)
                {
                    if (_createdBoolSymbols.ContainsKey(symbolSyntaxNode.Id)) return WrapAroundDomainIfNeeded(_createdBoolSymbols[symbolSyntaxNode.Id], symbolSyntaxNode);

                    var boolExpr = _currentContext.MkBoolConst(symbolSyntaxNode.Id);
                    _createdBoolSymbols[symbolSyntaxNode.Id] = boolExpr;
                    return WrapAroundDomainIfNeeded(boolExpr, symbolSyntaxNode);
                }

                if (_createdIntSymbols.ContainsKey(symbolSyntaxNode.Id)) return WrapAroundDomainIfNeeded(_createdIntSymbols[symbolSyntaxNode.Id], symbolSyntaxNode);

                var intExpr = _currentContext.MkIntConst(symbolSyntaxNode.Id);
                _createdIntSymbols[symbolSyntaxNode.Id] = intExpr;
                return WrapAroundDomainIfNeeded(intExpr, symbolSyntaxNode);
            }

            if (syntaxNode is ConstantValueSyntaxNode)
            {
                var constantValueSyntaxNode = (ConstantValueSyntaxNode)syntaxNode;

                if (constantValueSyntaxNode.Value is bool) return _currentContext.MkBool((bool)constantValueSyntaxNode.Value);

                var currentExpression = _currentContext.MkInt((int)constantValueSyntaxNode.Value);
                return WrapAroundDomainIfNeeded(currentExpression, constantValueSyntaxNode);
            }

            if (syntaxNode is BinaryExpressionSyntaxNode)
            {
                var binaryExpressionSyntaxNode = (BinaryExpressionSyntaxNode)syntaxNode;

                if (binaryExpressionSyntaxNode.OperationKind == EOperationKinds.And)
                {
                    var leftExpression = (BoolExpr)ConstructExpression(binaryExpressionSyntaxNode.LeftValue);
                    var rightExpression = (BoolExpr)ConstructExpression(binaryExpressionSyntaxNode.RightValue);

                    var currentExpression = _currentContext.MkAnd(leftExpression, rightExpression);
                    return WrapAroundDomainIfNeeded(currentExpression, binaryExpressionSyntaxNode);
                }

                if (binaryExpressionSyntaxNode.OperationKind == EOperationKinds.Or)
                {
                    var leftExpression = (BoolExpr)ConstructExpression(binaryExpressionSyntaxNode.LeftValue);
                    var rightExpression = (BoolExpr)ConstructExpression(binaryExpressionSyntaxNode.RightValue);

                    var currentExpression = _currentContext.MkOr(leftExpression, rightExpression);
                    return WrapAroundDomainIfNeeded(currentExpression, binaryExpressionSyntaxNode);
                }

                if (binaryExpressionSyntaxNode.OperationKind == EOperationKinds.Implication)
                {
                    var leftExpression = (BoolExpr)ConstructExpression(binaryExpressionSyntaxNode.LeftValue);
                    var rightExpression = (BoolExpr)ConstructExpression(binaryExpressionSyntaxNode.RightValue);

                    var currentExpression = _currentContext.MkImplies(leftExpression, rightExpression);
                    return WrapAroundDomainIfNeeded(currentExpression, binaryExpressionSyntaxNode);
                }

                if (binaryExpressionSyntaxNode.OperationKind == EOperationKinds.Add)
                {
                    var leftExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.LeftValue);
                    var rightExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.RightValue);

                    var currentExpression = _currentContext.MkAdd(leftExpression, rightExpression);
                    return WrapAroundDomainIfNeeded(currentExpression, binaryExpressionSyntaxNode);
                }

                if (binaryExpressionSyntaxNode.OperationKind == EOperationKinds.Divide)
                {
                    var leftExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.LeftValue);
                    var rightExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.RightValue);

                    var currentExpression = _currentContext.MkDiv(leftExpression, rightExpression);
                    return WrapAroundDomainIfNeeded(currentExpression, binaryExpressionSyntaxNode);
                }

                if (binaryExpressionSyntaxNode.OperationKind == EOperationKinds.Multiply)
                {
                    var leftExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.LeftValue);
                    var rightExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.RightValue);

                    var currentExpression = _currentContext.MkMul(leftExpression, rightExpression);
                    return WrapAroundDomainIfNeeded(currentExpression, binaryExpressionSyntaxNode);
                }

                if (binaryExpressionSyntaxNode.OperationKind == EOperationKinds.Substract)
                {
                    var leftExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.LeftValue);
                    var rightExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.RightValue);

                    var currentExpression = _currentContext.MkSub(leftExpression, rightExpression);
                    return WrapAroundDomainIfNeeded(currentExpression, binaryExpressionSyntaxNode);
                }

                if (binaryExpressionSyntaxNode.OperationKind == EOperationKinds.Less)
                {
                    var leftExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.LeftValue);
                    var rightExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.RightValue);

                    var currentExpression = _currentContext.MkLt(leftExpression, rightExpression);
                    return WrapAroundDomainIfNeeded(currentExpression, binaryExpressionSyntaxNode);
                }

                if (binaryExpressionSyntaxNode.OperationKind == EOperationKinds.LessOrEqual)
                {
                    var leftExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.LeftValue);
                    var rightExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.RightValue);

                    var currentExpression = _currentContext.MkLe(leftExpression, rightExpression);
                    return WrapAroundDomainIfNeeded(currentExpression, binaryExpressionSyntaxNode);
                }

                if (binaryExpressionSyntaxNode.OperationKind == EOperationKinds.Equal)
                {
                    var leftExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.LeftValue);
                    var rightExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.RightValue);

                    var currentExpression = _currentContext.MkEq(leftExpression, rightExpression);
                    return WrapAroundDomainIfNeeded(currentExpression, binaryExpressionSyntaxNode);
                }

                if (binaryExpressionSyntaxNode.OperationKind == EOperationKinds.GreaterOrEqual)
                {
                    var leftExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.LeftValue);
                    var rightExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.RightValue);

                    var currentExpression = _currentContext.MkGe(leftExpression, rightExpression);
                    return WrapAroundDomainIfNeeded(currentExpression, binaryExpressionSyntaxNode);
                }

                if (binaryExpressionSyntaxNode.OperationKind == EOperationKinds.Greater)
                {
                    var leftExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.LeftValue);
                    var rightExpression = (IntExpr)ConstructExpression(binaryExpressionSyntaxNode.RightValue);

                    var currentExpression = _currentContext.MkGt(leftExpression, rightExpression);
                    return WrapAroundDomainIfNeeded(currentExpression, binaryExpressionSyntaxNode);
                }
            }

            if (syntaxNode is UnaryExpressionSyntaxNode)
            {
                var unaryExpressionSyntaxNode = (UnaryExpressionSyntaxNode)syntaxNode;
                var innerExpression = (BoolExpr)ConstructExpression(unaryExpressionSyntaxNode.InnerValue);

                if (unaryExpressionSyntaxNode.OperationKind == EOperationKinds.Not) return _currentContext.MkNot(innerExpression);
            }

            return null;
        }

        public Expr WrapAroundDomainIfNeeded(Expr currentExpression, ValueSyntaxNode valueSyntaxNode)
        {
            if (valueSyntaxNode.DomainValue != null)
            {
                var domainExpression = ConstructExpression(valueSyntaxNode.DomainValue.InnerValue);

                if (valueSyntaxNode.DomainValue.OperationKind == EOperationKinds.Any)
                {
                    currentExpression = _currentContext.MkForall(new[] { domainExpression }, currentExpression);
                }
                else if (valueSyntaxNode.DomainValue.OperationKind == EOperationKinds.Exists)
                {
                    currentExpression = _currentContext.MkExists(new[] { domainExpression }, currentExpression);
                }
            }

            return currentExpression;
        }

        private void Initialize()
        {
            _currentContext = new Context(new Dictionary<string, string> { { "mbqi", "true" } });
            _createdBoolSymbols = new Dictionary<string, BoolExpr>();
            _createdIntSymbols = new Dictionary<string, IntExpr>();
        }
    }
}