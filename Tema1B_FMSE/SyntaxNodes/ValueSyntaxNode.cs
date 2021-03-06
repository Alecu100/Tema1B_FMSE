﻿namespace Tema1B_FMSE.SyntaxNodes
{
    using System.Collections.Generic;
    using System.Windows.Documents;

    public abstract class ValueSyntaxNode : SyntaxNode
    {
        private DomainValueSyntaxNode _domainValue;

        public ESymbolKinds SymbolKind { get; set; }

        public DomainValueSyntaxNode DomainValue
        {
            get
            {
                return _domainValue;
            }
        }

        public override IEnumerable<SyntaxNode> Children
        {
            get
            {
                var children = new List<SyntaxNode>();
                children.Add(DomainValue);
                return children;
            }
        }

        public void AssignDomainValue(DomainValueSyntaxNode domainValue)
        {
            _domainValue = domainValue;
            domainValue.Parent = this;
        }
    }
}