using System.Threading;

namespace Tema1B_FMSE.SyntaxNodes
{
    public class BinaryExpressionSyntaxNode : ValueSyntaxNode
    {
        public LiteralSyntaxNode Operator { get; set; }

        public ValueSyntaxNode LeftValue { get; set; }

        public ValueSyntaxNode RightValue { get; set; }

        public EOperationKinds OperationKind { get; set; }

        public override void AssignChild(SyntaxNode child)
        {
            if (LeftValue == null)
            {
                if (!(child is ValueSyntaxNode))
                {
                    throw new SyntaxTreeParserException("Expected value expression at index", child.StartIndex);
                }

                var valueSyntaxNode = (ValueSyntaxNode) child;
                LeftValue = valueSyntaxNode;
                valueSyntaxNode.Parent = this;

                StartIndex = valueSyntaxNode.StartIndex;
                EndIndex = valueSyntaxNode.EndIndex;

                return;
            }

            if (Operator == null)
            {
                if (!(child is LiteralSyntaxNode))
                {
                    throw new SyntaxTreeParserException("Expected literal expression at index", child.StartIndex);
                }

                var literalSyntaxNode = (LiteralSyntaxNode)child;

                if (literalSyntaxNode.LiteralValue == "and")
                {
                    OperationKind = EOperationKinds.And;
                } else if (literalSyntaxNode.LiteralValue == "or")
                {
                    OperationKind = EOperationKinds.Or;
                } else if (literalSyntaxNode.LiteralValue == "imp")
                {
                    OperationKind = EOperationKinds.Implication;
                }

                Operator = literalSyntaxNode;
                literalSyntaxNode.Parent = this;
                EndIndex = literalSyntaxNode.EndIndex;

                return;
            }

            if (RightValue == null)
            {
                if (!(child is ValueSyntaxNode))
                {
                    throw new SyntaxTreeParserException("Expected value expression at index", child.StartIndex);
                }

                var valueSyntaxNode = (ValueSyntaxNode)child;
                RightValue = valueSyntaxNode;
                valueSyntaxNode.Parent = this;
                EndIndex = valueSyntaxNode.EndIndex;
                _isFinishedReading = true;

                return;
            }

            throw new SyntaxTreeParserException("Too many parameters passed to binary operator", StartIndex);
        }
    }
}