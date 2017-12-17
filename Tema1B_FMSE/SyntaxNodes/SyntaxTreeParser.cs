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

            return new SyntaxTree {RootValue = (ValueSyntaxNode) _availableSyntaxNodes.Pop()};
        }

        private void ParseExpression()
        {
            while (CanReadMore())
            {
                var nextFoundToken = ReadNextToken();


                if (LandedOnAndOperator(nextFoundToken) || LandedOnOrOperator(nextFoundToken) ||
                    LandedOnNotOperator(nextFoundToken) || LandedOnCloseParanthesis(nextFoundToken) ||
                    LandedOnOpenParanthesis(nextFoundToken) || LandedOnImpOperator(nextFoundToken))
                {
                    var literalSyntaxNode = new LiteralSyntaxNode
                    {
                        StartIndex = _indexInExpression,
                        EndIndex = _indexInExpression + nextFoundToken.Length,
                        LiteralValue = nextFoundToken
                    };

                    PushNewSyntaxNode(literalSyntaxNode);
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
            if (syntaxNode is LiteralSyntaxNode)
            {
                var literalSyntaxNode = (LiteralSyntaxNode)syntaxNode;

                if (literalSyntaxNode.LiteralValue == "and" || literalSyntaxNode.LiteralValue == "or" ||
                    literalSyntaxNode.LiteralValue == "imp")
                {
                    var binaryExpressionSyntaxNode = new BinaryExpressionSyntaxNode();

                    var valueSyntaxNode = (ValueSyntaxNode)_availableSyntaxNodes.Pop();
                    valueSyntaxNode.IsFinishedReading = true;

                    binaryExpressionSyntaxNode.AssignChild(valueSyntaxNode);
                    binaryExpressionSyntaxNode.AssignChild(literalSyntaxNode);

                    PushSyntaxNodeClosingPreviousIfAvailable(binaryExpressionSyntaxNode);

                    return;
                }

                if (literalSyntaxNode.LiteralValue == "not")
                {
                    var unaryExpressionSyntaxNode = new UnaryExpressionSyntaxNode();
                    unaryExpressionSyntaxNode.AssignChild(literalSyntaxNode);

                    PushSyntaxNodeClosingPreviousIfAvailable(unaryExpressionSyntaxNode);

                    return;
                }

                if (literalSyntaxNode.LiteralValue == "(")
                {
                    PushSyntaxNodeClosingPreviousIfAvailable(literalSyntaxNode);

                    return;
                }

                if (literalSyntaxNode.LiteralValue == ")")
                {
                    var valueSyntaxNode = (ValueSyntaxNode) _availableSyntaxNodes.Pop();
                    var previousLiteralSyntaxNode = (LiteralSyntaxNode) _availableSyntaxNodes.Pop();

                    valueSyntaxNode.IsFinishedReading = true;
                    previousLiteralSyntaxNode.IsFinishedReading = true;

                    var blockExpressionSyntaxNode = new BlockExpressionSyntaxNode();

                    blockExpressionSyntaxNode.AssignChild(previousLiteralSyntaxNode);
                    blockExpressionSyntaxNode.AssignChild(valueSyntaxNode);
                    blockExpressionSyntaxNode.AssignChild(literalSyntaxNode);

                    PushSyntaxNodeClosingPreviousIfAvailable(blockExpressionSyntaxNode);

                    return;
                }
            }

            var lastSyntaxNode = _availableSyntaxNodes.Peek();


            if (!lastSyntaxNode.IsFinishedReading && (syntaxNode is SymbolSyntaxNode))
            {
                lastSyntaxNode.AssignChild(syntaxNode);

                if (lastSyntaxNode.IsFinishedReading && lastSyntaxNode.Parent != null)
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
            }

            _availableSyntaxNodes.Push(syntaxNodeToPush);
        }

        private string ReadNextToken()
        {
            _indexInExpression++;

            if (LandedOnAndOperator(_expressionText, _indexInExpression))
            {
                return _expressionText.Substring(_indexInExpression, 3);
            }

            if (LandedOnNotOperator(_expressionText, _indexInExpression))
            {
                return _expressionText.Substring(_indexInExpression, 3);
            }

            if (LandedOnOrOperator(_expressionText, _indexInExpression))
            {
                return _expressionText.Substring(_indexInExpression, 2);
            }

            if (LandedOnCloseParanthesis(_expressionText, _indexInExpression))
            {
                return _expressionText.Substring(_indexInExpression, 1);
            }

            if (LandedOnCloseParanthesis(_expressionText, _indexInExpression))
            {
                return _expressionText.Substring(_indexInExpression, 1);
            }

            if (LandedOnImpOperator(_expressionText, _indexInExpression))
            {
                return _expressionText.Substring(_indexInExpression, 3);
            }

            return _expressionText.Substring(_indexInExpression, 1);
        }

        private bool LandedOnOpenParanthesis(string expression, int startIndex = 0)
        {
            return expression[startIndex] == '(';
        }

        private bool LandedOnCloseParanthesis(string expression, int startIndex = 0)
        {
            return expression[startIndex] == ')';
        }

        private bool LandedOnAndOperator(string expression, int startIndex = 0)
        {
            return startIndex + 2 < expression.Length && expression[startIndex] == 'a' &&
                   expression[startIndex + 1] == 'n' && expression[startIndex + 2] == 'd';
        }

        private bool LandedOnOrOperator(string expression, int startIndex = 0)
        {
            return startIndex + 1 < expression.Length && expression[startIndex] == 'o' &&
                   expression[startIndex + 1] == 'r';
        }

        private bool LandedOnNotOperator(string expression, int startIndex = 0)
        {
            return startIndex + 2 < expression.Length && expression[startIndex] == 'n' &&
                   expression[startIndex + 1] == 'o' && expression[startIndex + 2] == 't';
        }

        private bool LandedOnImpOperator(string expression, int startIndex = 0)
        {
            return startIndex + 2 < expression.Length && expression[startIndex] == 'i' &&
                   expression[startIndex] == 'm' && expression[startIndex] == 'p';
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