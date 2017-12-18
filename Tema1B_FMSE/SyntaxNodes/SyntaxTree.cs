using System.Collections.Generic;

namespace Tema1B_FMSE.SyntaxNodes
{
    public class SyntaxTree : SyntaxNode
    {
        public SyntaxNode RootValue { get; set; }

        public override void AssignChild(SyntaxNode child)
        {
            RootValue = child;
        }

        public override IEnumerable<SyntaxNode> Children
        {
            get
            {
                return new List<SyntaxNode> { RootValue };
            }
        }
    }
}