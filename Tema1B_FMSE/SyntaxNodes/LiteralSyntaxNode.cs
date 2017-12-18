using System.Collections.Generic;

namespace Tema1B_FMSE.SyntaxNodes
{
    public class LiteralSyntaxNode : SyntaxNode
    {
        public LiteralSyntaxNode()
        {
            _isFinishedReading = true;
        }

        public override void AssignChild(SyntaxNode child)
        {
            
        }

        public override IEnumerable<SyntaxNode> Children
        {
            get
            {
                return new List<SyntaxNode>();
            }
        }

        public string LiteralValue { get; set; }
    }
}