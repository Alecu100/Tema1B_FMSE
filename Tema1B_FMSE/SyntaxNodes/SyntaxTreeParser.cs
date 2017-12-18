using System.Collections.Generic;

namespace Tema1B_FMSE.SyntaxNodes
{
    public class SyntaxTreeParser
    {
        private Stack<SyntaxNode> _availableSyntaxNodes = new Stack<SyntaxNode>();
        private string _expressionText;
        private int _indexInExpression;

        public SyntaxTree Parse(string expressionText)
        {
            Initialize(expressionText);
            ParseExpression();
            FinishAndPopLastSymbolSyntaxNodeIfNeeded();

            return new SyntaxTree {RootValue = (ValueSyntaxNode) _availableSyntaxNodes.Pop()};
        }

        private void ParseExpression()
        {
            while (CanReadMore())
            {
                var nextFoundToken = ReadNextToken();

                if (LandedOnCloseParanthesis(nextFoundToken) || LandedOnOpenParanthesis(nextFoundToken)
                    || LandedOnDomainOperator(nextFoundToken) != null || LandedOnOperator(nextFoundToken) != null)
                {
                    var literalSyntaxNode = new LiteralSyntaxNode
                                                {
                                                    StartIndex = _indexInExpression,
                                                    EndIndex =
                                                        _indexInExpression + nextFoundToken.Length,
                                                    LiteralValue = nextFoundToken
                                                };

                    PushNewSyntaxNode(literalSyntaxNode);
                    continue;
                }

                if (LandedOnConstant(nextFoundToken) != null)
                {
                    var constantValueSyntaxNode = new ConstantValueSyntaxNode
                    {
                        LiteralValue = nextFoundToken,
                        StartIndex = _indexInExpression,
                        EndIndex = _indexInExpression + nextFoundToken.Length
                    };

                    PushNewSyntaxNode(constantValueSyntaxNode);
                    continue;
                }

                var symbolSyntaxNode = new SymbolSyntaxNode
                {
                    StartIndex = _indexInExpression,
                    EndIndex = _indexInExpression + nextFoundToken.Length,
                    Id = nextFoundToken
                };
                PushNewSyntaxNode(symbolSyntaxNode);
            }
        }

        private void PushNewSyntaxNode(SyntaxNode syntaxNode)
        {
            _indexInExpression = syntaxNode.EndIndex;

            if (syntaxNode is LiteralSyntaxNode)
            {
                var literalSyntaxNode = (LiteralSyntaxNode)syntaxNode;

                if (LandedOnBinaryOperator(literalSyntaxNode.LiteralValue))
                {
                    FinishAndPopLastSymbolSyntaxNodeIfNeeded();

                    var binaryExpressionSyntaxNode = new BinaryExpressionSyntaxNode();

                    var valueSyntaxNode = (ValueSyntaxNode)_availableSyntaxNodes.Pop();
                    valueSyntaxNode.IsFinishedReading = true;

                    binaryExpressionSyntaxNode.AssignChild(valueSyntaxNode);
                    binaryExpressionSyntaxNode.AssignChild(literalSyntaxNode);

                    PushSyntaxNodeClosingPreviousIfAvailable(binaryExpressionSyntaxNode);

                    return;
                }

                if (LandedOnUnaryOperator(literalSyntaxNode.LiteralValue))
                {
                    var unaryExpressionSyntaxNode = new UnaryExpressionSyntaxNode();
                    unaryExpressionSyntaxNode.AssignChild(literalSyntaxNode);

                    AssignValueNodeToLastNodePopingIfNeeded(unaryExpressionSyntaxNode);

                    PushSyntaxNodeClosingPreviousIfAvailable(unaryExpressionSyntaxNode);

                    return;
                }

                if (LandedOnDomainOperator(literalSyntaxNode.LiteralValue) != null)
                {
                    var domainValueSyntaxNode = new DomainValueSyntaxNode();
                    domainValueSyntaxNode.AssignChild(literalSyntaxNode);

                    PushSyntaxNodeClosingPreviousIfAvailable(domainValueSyntaxNode);
                }

                if (literalSyntaxNode.LiteralValue == "(")
                {
                    PushSyntaxNodeClosingPreviousIfAvailable(literalSyntaxNode);

                    return;
                }

                if (literalSyntaxNode.LiteralValue == ")")
                {
                    FinishAndPopLastSymbolSyntaxNodeIfNeeded();
                    var valueSyntaxNode = (ValueSyntaxNode) _availableSyntaxNodes.Pop();
                    var previousLiteralSyntaxNode = (LiteralSyntaxNode) _availableSyntaxNodes.Pop();

                    valueSyntaxNode.IsFinishedReading = true;
                    previousLiteralSyntaxNode.IsFinishedReading = true;

                    var blockExpressionSyntaxNode = new BlockExpressionSyntaxNode();

                    blockExpressionSyntaxNode.AssignChild(previousLiteralSyntaxNode);
                    blockExpressionSyntaxNode.AssignChild(valueSyntaxNode);
                    blockExpressionSyntaxNode.AssignChild(literalSyntaxNode);
                    blockExpressionSyntaxNode.SymbolKind = valueSyntaxNode.SymbolKind;

                    AssignValueNodeToLastNodePopingIfNeeded(blockExpressionSyntaxNode);

                    if (blockExpressionSyntaxNode.Parent == null)
                    {
                        PushSyntaxNodeClosingPreviousIfAvailable(blockExpressionSyntaxNode);
                    }

                    return;
                }
            }

            if (syntaxNode is ConstantValueSyntaxNode)
            {
                var constantValueSyntaxNode = (ConstantValueSyntaxNode)syntaxNode;

                if (_availableSyntaxNodes.Count > 0 && !_availableSyntaxNodes.Peek().IsFinishedReading)
                {
                    AssignValueNodeToLastNodePopingIfNeeded(constantValueSyntaxNode);

                    if (constantValueSyntaxNode.Parent == null)
                    {
                        _availableSyntaxNodes.Push(syntaxNode);
                    }

                    return;
                }
            }

            if (syntaxNode is SymbolSyntaxNode)
            {
                var symbolSyntaxNode = (SymbolSyntaxNode)syntaxNode;

                if (symbolSyntaxNode.Id == " ")
                {
                    return;
                }

                if (_availableSyntaxNodes.Count > 0 && !_availableSyntaxNodes.Peek().IsFinishedReading)
                {
                    AssignValueNodeToLastNodePopingIfNeeded(symbolSyntaxNode);

                    _availableSyntaxNodes.Push(syntaxNode);

                    return;
                }

                if (_availableSyntaxNodes.Count > 0 && _availableSyntaxNodes.Peek() is SymbolSyntaxNode)
                {
                    AssignValueNodeToLastNodePopingIfNeeded(symbolSyntaxNode);

                    return;
                }

                _availableSyntaxNodes.Push(symbolSyntaxNode);
            }
        }

        private void AssignValueNodeToLastNodePopingIfNeeded(ValueSyntaxNode valueSyntaxNode)
        {
            if (_availableSyntaxNodes.Count > 0)
            {
                _availableSyntaxNodes.Peek().AssignChild(valueSyntaxNode);

                if (_availableSyntaxNodes.Peek().IsFinishedReading && _availableSyntaxNodes.Peek().Parent != null)
                {
                    _availableSyntaxNodes.Pop();
                }
            }
        }

        private void PushSyntaxNodeClosingPreviousIfAvailable(SyntaxNode syntaxNodeToPush)
        {
            if (_availableSyntaxNodes.Count > 0)
            {
                _availableSyntaxNodes.Peek().IsFinishedReading = true;

                PopSymbolSyntaxNodeIfUsedAndFinished();
            }

            if (_availableSyntaxNodes.Count > 0 && (_availableSyntaxNodes.Peek() is DomainValueSyntaxNode) && _availableSyntaxNodes.Peek().IsFinishedReading && (syntaxNodeToPush is ValueSyntaxNode))
            {
                var domainValueSyntaxNode = (DomainValueSyntaxNode)_availableSyntaxNodes.Peek();
                var valueSyntaxNodeToPush = (ValueSyntaxNode)syntaxNodeToPush;

                if (domainValueSyntaxNode.OperationKind == EOperationKinds.Any || domainValueSyntaxNode.OperationKind == EOperationKinds.Exists)
                {
                    valueSyntaxNodeToPush.AssignDomainValue(domainValueSyntaxNode);
                }

                _availableSyntaxNodes.Pop();
            }

            _availableSyntaxNodes.Push(syntaxNodeToPush);
        }

        private void FinishAndPopLastSymbolSyntaxNodeIfNeeded()
        {
            if (_availableSyntaxNodes.Peek() is SymbolSyntaxNode && _availableSyntaxNodes.Peek().Parent != null)
            {
                var lastSymbolSyntaxNode = _availableSyntaxNodes.Pop();
                lastSymbolSyntaxNode.IsFinishedReading = true;
            }
        } 

        private void PopSymbolSyntaxNodeIfUsedAndFinished()
        {
            if (_availableSyntaxNodes.Peek() is SymbolSyntaxNode && _availableSyntaxNodes.Peek().Parent != null)
            {
                _availableSyntaxNodes.Pop();
            }
        }

        private string ReadNextToken()
        {
            string op = null;

            if ((op = LandedOnOperator(_expressionText, _indexInExpression)) != null)
            {
                return op;
            }

            if (LandedOnCloseParanthesis(_expressionText, _indexInExpression))
            {
                return _expressionText.Substring(_indexInExpression, 1);
            }

            if (LandedOnOpenParanthesis(_expressionText, _indexInExpression))
            {
                return _expressionText.Substring(_indexInExpression, 1);
            }

            string constantValue = null;

            if ((constantValue = LandedOnConstant(_expressionText, _indexInExpression)) != null)
            {
                return constantValue;
            }

            return _expressionText.Substring(_indexInExpression, 1);
        }

        private string LandedOnConstant(string expressionText, int indexInExpression = 0)
        {
            if (char.IsDigit(expressionText[indexInExpression]) || expressionText[indexInExpression] == '+' ||
                expressionText[indexInExpression] == '-')
            {
                var startIndex = indexInExpression;

                while (startIndex < expressionText.Length && (char.IsDigit(expressionText[startIndex]) ||
                       expressionText[startIndex] == '+' ||
                       expressionText[startIndex] == '-'))
                {
                    startIndex++;
                }

                return expressionText.Substring(indexInExpression, startIndex - indexInExpression);
            }


            if (indexInExpression + 5 <= expressionText.Length)
            {
                var falseTest = expressionText.Substring(indexInExpression, 5);

                if (falseTest == "false")
                {
                    return falseTest;
                }
            }

            if (indexInExpression + 3 <= expressionText.Length)
            {
                var trueTest = expressionText.Substring(indexInExpression, 3);

                if (trueTest == "true")
                {
                    return trueTest;
                }
            }

            return null;
        }

        private string LandedOnOperator(string expression, int startIndex = 0)
        {
            foreach (var operatorCode in OperatorsInformation.MappedOperationKinds.Keys)
            {
                if (startIndex + operatorCode.Length <= expression.Length)
                {
                    var op = expression.Substring(startIndex, operatorCode.Length);

                    if (op == operatorCode)
                    {
                        return operatorCode;
                    }
                }
            }

            return null;
        }

        private bool LandedOnBinaryOperator(string expression, int startIndex = 0)
        {
            foreach (var operatorCode in OperatorsInformation.MappedBinaryOperationKinds.Keys)
            {
                if (startIndex + operatorCode.Length <= expression.Length)
                {
                    var op = expression.Substring(startIndex, operatorCode.Length);

                    if (op == operatorCode)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool LandedOnUnaryOperator(string expression, int startIndex = 0)
        {
            foreach (var operatorCode in OperatorsInformation.MappedUnaryOperationKinds.Keys)
            {
                if (startIndex + operatorCode.Length <= expression.Length)
                {
                    var op = expression.Substring(startIndex, operatorCode.Length);

                    if (op == operatorCode)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private string LandedOnDomainOperator(string expression, int startIndex = 0)
        {
            foreach (var operatorCode in OperatorsInformation.MappedDomainOperations)
            {
                if (startIndex + operatorCode.Length <= expression.Length)
                {
                    var op = expression.Substring(startIndex, operatorCode.Length);

                    if (op == operatorCode)
                    {
                        return op;
                    }
                }
            }

            return null;
        }

        private bool LandedOnOpenParanthesis(string expression, int startIndex = 0)
        {
            return expression[startIndex] == '(';
        }

        private bool LandedOnCloseParanthesis(string expression, int startIndex = 0)
        {
            return expression[startIndex] == ')';
        }

        private void Initialize(string expressionText)
        {
            _expressionText = expressionText;
            _availableSyntaxNodes = new Stack<SyntaxNode>();
            _indexInExpression = 0;
        }

        private bool CanReadMore()
        {
            return _indexInExpression < _expressionText.Length;
        }
    }
}