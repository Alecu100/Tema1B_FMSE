using System.Collections.Generic;

namespace Tema1B_FMSE.SyntaxNodes
{
    public class BlockExpressionSyntaxNode : ValueSyntaxNode
    {
        public LiteralSyntaxNode LeftParanthesis { get; set; }

        public LiteralSyntaxNode RightParanthesis { get; set; }

        public ValueSyntaxNode InnerValue { get; set; }

        public override void AssignChild(SyntaxNode child)
        {
            if (LeftParanthesis == null)
            {
                var literalSyntaxNode = (LiteralSyntaxNode) child;

                LeftParanthesis = literalSyntaxNode;
                literalSyntaxNode.Parent = this;
                StartIndex = literalSyntaxNode.StartIndex;
                EndIndex = literalSyntaxNode.EndIndex;

                return;
            }

            if (InnerValue == null)
            {
                var valueSyntaxNode = (ValueSyntaxNode) child;

                InnerValue = valueSyntaxNode;
                valueSyntaxNode.Parent = this;
                EndIndex = valueSyntaxNode.EndIndex;

                return;
            }

            if (RightParanthesis == null)
            {
                var literalSyntaxNode = (LiteralSyntaxNode) child;

                RightParanthesis = literalSyntaxNode;
                literalSyntaxNode.Parent = this;
                EndIndex = literalSyntaxNode.EndIndex;
                _isFinishedReading = true;
            }
        }

        public override IEnumerable<SyntaxNode> Children
        {
            get
            {
                return new List<SyntaxNode>
                {
                    LeftParanthesis,
                    InnerValue,
                    RightParanthesis
                };
            }
        }
    }
}