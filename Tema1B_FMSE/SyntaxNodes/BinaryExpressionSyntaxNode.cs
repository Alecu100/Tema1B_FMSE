using System.Collections.Generic;
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
                var valueSyntaxNode = (ValueSyntaxNode) child;
                LeftValue = valueSyntaxNode;
                valueSyntaxNode.Parent = this;

                StartIndex = valueSyntaxNode.StartIndex;
                EndIndex = valueSyntaxNode.EndIndex;

                return;
            }

            if (Operator == null)
            {
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
                var valueSyntaxNode = (ValueSyntaxNode)child;
                RightValue = valueSyntaxNode;
                valueSyntaxNode.Parent = this;
                EndIndex = valueSyntaxNode.EndIndex;
                _isFinishedReading = true;
            }
        }

        public override IEnumerable<SyntaxNode> Children
        {
            get
            {
                return new List<SyntaxNode> { LeftValue, Operator, RightValue };
            }
        }
    }
}