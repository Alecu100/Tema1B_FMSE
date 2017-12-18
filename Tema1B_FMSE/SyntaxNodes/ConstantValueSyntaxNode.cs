using System.Collections.Generic;

namespace Tema1B_FMSE.SyntaxNodes
{
    public class ConstantValueSyntaxNode : ValueSyntaxNode
    {
        public override void AssignChild(SyntaxNode child)
        {
        }

        public string LiteralValue { get; set; }

        public object Value
        {
            get
            {
                if (SymbolKind == ESymbolKinds.Boolean)
                    return bool.Parse(LiteralValue);

                return int.Parse(LiteralValue);
            }
        }

        public override IEnumerable<SyntaxNode> Children
        {
            get
            {
                return new List<SyntaxNode>();
            }
        }
    }
}
