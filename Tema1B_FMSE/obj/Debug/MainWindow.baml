   M S B A M L   `   `   `  ����F  BTema1B_FMSE, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Tema1B_FMSETema1B_FMSE  5   	   A%clr-namespace:Tema1B_FMSE.SyntaxNodesTema1B_FMSE.SyntaxNodes  5   	       Tema1B_FMSE.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Tema1B_FMSE     5   	   =syntaxNodes%clr-namespace:Tema1B_FMSE.SyntaxNodes     5   	     � Title$  
MainWindow��5	   	   $��543.287��6   $��782.333��6-   .��5
      � .+�5   
   �� -   5         ��Name$ btnProve��5      $��Prove��6#   $
��Left=�63   $��10,0,0,9q�6N   $��75��6`   $��29��5   *   $��Bottom=�66   �� 5   
   -   5         ��Name$ txtExpression��5      $��24��6)   $��	11,41,9,0q�65    �� TextWrapping$
 Wrap=�6H   $&�� any (n) ((n gt 10) imp (n gt 5))��5      $	��Top=�6:   �� 5   
   -   5        $ btnExit��5      $
��Exit��6"   $
��Left=�61   $��90,0,0,9q�6L   $��75��6^   $��29��6i   $��Bottom=�5      �� 5   
   -   5         ��Name$ 	lblLegend��5      $7��1Legend: and=and, or=or, not=not, implication=imp ��6#   $��0,0,10,9q�6_   $��Right=�5      $	��286��6,   $��27��68   $��Bottom=�6D   �� 5   
   -   5        $ lblExpression��5      $��Expression:��6'   $
��Left=�6=   $��	11,10,0,0q�6X   $	��Top=�5      R� 5   
   -   5         R�Name$ trViewSyntaxTree��5      $��11,245,10,43q�6-    � 	Resources 5       �� DataType8   2Tema1B_FMSE.SyntaxNodes.BinaryExpressionSyntaxNode �� ItemsSource	 �� CornerRadius
 �� Path7   1Tema1B_FMSE.SyntaxNodes.UnaryExpressionSyntaxNode3   -Tema1B_FMSE.SyntaxNodes.DomainValueSyntaxNode/   )Tema1B_FMSE.SyntaxNodes.LiteralSyntaxNode5   /Tema1B_FMSE.SyntaxNodes.ConstantValueSyntaxNode.   (Tema1B_FMSE.SyntaxNodes.SymbolSyntaxNode(   "Tema1B_FMSE.SyntaxNodes.SyntaxNode��%D  (��       5   +   *, +)(�� �    5$   +   *, +)(�� \    51   +   *, +)(�� 
    5>   +   *, +)(�� [    5I   +   *, +)(�� �    5V   +   *, +)(�� �	    5c   +   *, +)�� 5      "  6+    5   +   �� *
Children+.R�5      �� ���	#FF000000$��1,1,1,1q�65   $	 4,4,4,4��6O   $��2q�6f   ._�5      � .+�5      �� $��Type: ��6$   �� 5      ��6$   �� $

 Name��� 5      $��OperationKind: ��6$   �� 5      ��6$   �� $
 OperationKind��� 5      $��SymbolKind: ��6$   �� 5       ��6$   �� $
 
SymbolKind�5!      5"      5#      �� 5$      "  6+    5%   +   �� *
Children+.R�5&      �� ���	#FF000000$��1,1,1,1q�65   $	 4,4,4,4��6O   $��2q�6f   ._�5'      � .+�5(      �� $��Type: ��6$   �� 5)      ��6$   �� $

 Name��� 5*      $��OperationKind: ��6$   �� 5+      ��6$   �� $
 OperationKind��� 5,      $��SymbolKind: ��6$   �� 5-      ��6$   �� $
 
SymbolKind�5.      5/      50      �� 51      "  6+    52   +   �� *
Children+.R�53      �� ���	#FF000000$��1,1,1,1q�65   $	 4,4,4,4��6O   $��2q�6f   ._�54      � .+�55      �� $��Type: ��6$   �� 56      ��6$   �� $

 Name��� 57      $��OperationKind: ��6$   �� 58      ��6$   �� $
 OperationKind��� 59      $��SymbolKind: ��6$   �� 5:      ��6$   �� $
 
SymbolKind�5;      5<      5=      �� 5>      "  6+    5?   +   �� *
Children+.R�5@      �� ���	#FF000000$��1,1,1,1q�65   $	 4,4,4,4��6O   $��2q�6f   ._�5A      � .+�5B      �� $��Type: ��6$   �� 5C      ��6$   �� $

 Name��� 5D      $��Text: ��6$   �� 5E      ��6$   �� $
 LiteralValue�5F      5G      5H      �� 5I      "  6+    5J   +   �� *
Children+.R�5K      �� ���	#FF000000$��1,1,1,1q�65   $	 4,4,4,4��6O   $��2q�6f   ._�5L      � .+�5M      �� $��Type: ��6$   �� 5N      ��6$   �� $

 Name��� 5O      $��Text: ��6$   �� 5P      ��6$   �� $
 LiteralValue��� 5Q      $��SymbolKind: ��6$   �� 5R      ��6$   �� $
 
SymbolKind�5S      5T      5U      �� 5V      "  6+    5W   +   �� *
Children+.R�5X      �� ���	#FF000000$��1,1,1,1q�65   $	 4,4,4,4��6O   $��2q�6f   ._�5Y      � .+�5Z      �� $��Type: ��6$   �� 5[      ��6$   �� $

 Name��� 5\      $
��Id: ��6$   �� 5]      ��6$   �� $
 Id��� 5^      $��SymbolKind: ��6$   �� 5_      ��6$   �� $
 
SymbolKind�5`      5a      5b      �� 5c      "  6+    6W   �� *
Children+.R�5d      �� ���	#FF000000$��1,1,1,1q�65   $	 4,4,4,4��6O   $��2q�6f   ._�5e      � .+�5f      �� $��Type: ��6$   �� 5g      ��6$   �� $

 Name�5h      5i      5j      5k      5l      �� 5m   
   -   5        $ lblSyntaxTree��5m      $��Syntax Tree:��6'   $
��Left=�6>   $��
13,214,0,0q�6Y   $	��Top=�5n      �� 5o   
   -   5        $ lblIsSatisfiable��5o      $��Is Satisfiable:��6*   $
��Left=�6D   $��
184,0,0,10q�6_   $��26��5p      $��Bottom=�6   �� 5q   
   -	   5        $ lblIsSatisfiableVal��5q      $�� ��6-   $
��Left=�68   $��
267,0,0,10q�6S   $��26��6g   $��Bottom=�5r      �� 5s   
   -
   5         ��Name$ lstExamples��5s      $	��108��6'   $��11,101,10,0q�64   $	��Top=�5t      ��5u      ��  �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType" �$
 ExpressionExamples���5x      �� 5y      .R�5z      �� -   5        ��5z      �� *Text+65   5{      5}      �� 5~   
   -   5        $ lblExamples��5~      $(��"Examples (double click to insert):��6%   $
��Left=�6R   $��	13,70,0,0q�5      $	��Top=�6#   5�      5�      