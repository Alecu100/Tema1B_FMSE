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
                if (!(child is LiteralSyntaxNode))
                {
                    throw new SyntaxTreeParserException("Expected literal expression at index", child.StartIndex);
                }

                var literalSyntaxNode = (LiteralSyntaxNode) child;

                if (literalSyntaxNode.LiteralValue != "(")
                {
                    throw new SyntaxTreeParserException("Expected open paranthesis at index", child.StartIndex);
                }

                LeftParanthesis = literalSyntaxNode;
                literalSyntaxNode.Parent = this;
                StartIndex = literalSyntaxNode.StartIndex;
                EndIndex = literalSyntaxNode.EndIndex;

                return;
            }

            if (InnerValue == null)
            {
                
            }

        }
    }
}