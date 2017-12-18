using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tema1B_FMSE.SyntaxNodes
{
    public class DomainValueSyntaxNode : SyntaxNode
    {
        public LiteralSyntaxNode Operator { get; set; }

        public EOperationKinds OperationKind { get; set; }

        public ValueSyntaxNode InnerValue { get; set; }


        public override void AssignChild(SyntaxNode child)
        {
            if (Operator == null)
            {
                var literalSyntaxNode = (LiteralSyntaxNode)child;

                Operator = literalSyntaxNode;
                literalSyntaxNode.Parent = this;
                StartIndex = literalSyntaxNode.StartIndex;
                EndIndex = literalSyntaxNode.EndIndex;

                OperationKind = OperatorsInformation.MappedOperationKinds[literalSyntaxNode.LiteralValue];

                return;
            }

            if (InnerValue == null)
            {
                ValueSyntaxNode valueSyntaxNode = (ValueSyntaxNode)child;

                InnerValue = valueSyntaxNode;
                valueSyntaxNode.Parent = this;
                EndIndex = valueSyntaxNode.EndIndex;
                _isFinishedReading = true;
            }
        }

        public override IEnumerable<SyntaxNode> Children
        {
            get
            {
                return new List<SyntaxNode> { Operator, InnerValue };
            }
        }
    }
}
