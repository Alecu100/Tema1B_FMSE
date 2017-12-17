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


                if (LandedOnAndOperator(nextFoundToken))
                {
                    var orExpressionSyntaxNode = new BinaryExpressionSyntaxNode {OperationKind = EOperationKinds.Or};

                    var syntaxNode = _availableSyntaxNodes.Pop();

                    if (!(syntaxNode is ValueSyntaxNode))
                    {
                        throw new SyntaxTreeParserException("Expected value before \"or\" operator at index: " + _indexInExpression);
                    }

                    var valueSyntaxNode = (ValueSyntaxNode) syntaxNode;

                    orExpressionSyntaxNode.LeftValue = valueSyntaxNode;

                    continue;
                }

                if (_availableSyntaxNodes.Peek() is SymbolSyntaxNode)
                {
                    var symbolSyntaxNode = (SymbolSyntaxNode) _availableSyntaxNodes.Peek();

                    if (nextFoundToken != " " && !symbolSyntaxNode.IsFinishedReading)
                        symbolSyntaxNode.Id += nextFoundToken;
                    else if (nextFoundToken == " " && !symbolSyntaxNode.IsFinishedReading)
                    {
                        symbolSyntaxNode.IsFinishedReading = true;

                        if ()
                    }

                    continue;
                }

                var symbolSyntaxNode = new SymbolSyntaxNode();

                symbolSyntaxNode.Id = nextFoundToken;

                if ()
            }
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
            return startIndex + 2 <= expression.Length && expression[startIndex] == 'a' &&
                   expression[_indexInExpression + 1] == 'n' && expression[startIndex + 2] == 'd';
        }

        private bool LandedOnOrOperator(string expression, int startIndex = 0)
        {
            return startIndex + 1 <= expression.Length && expression[startIndex] == 'o' &&
                   expression[startIndex + 1] == 'r';
        }

        private bool LandedOnNotOperator(string expression, int startIndex)
        {
            return startIndex + 2 <= expression.Length && expression[startIndex] == 'n' &&
                   expression[_indexInExpression + 1] == 'o' && expression[startIndex + 2] == 't';
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