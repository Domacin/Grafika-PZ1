   M S B A M L   `   `   `  ????E  APrimer_2.1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Primer_2._1Primer_2._1  5   	   !    Primer_2._1.Transformacije2   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Primer_2._1     5   	     ? Title$  Transformacije2??5   	   $
??1200??6"   $
??1200??60   .??5	      ]? .+?5
   
   ?? -   5         ??Name$ 	DugmeLoad??5
      $??Ucitaj??6$   $	??Top=?6M   6d   o? 5   
   -   5         o?Name$
 Menu??5      $	??20 ??6   $	??Top=?6*   .@?5      m? $??Tools??6   $	??Top=?6&   $??Center=?6;   .@?5      m? -   5        $??Elipsa??5   D   $??Draw Ellipse??6    m? IsCheckable . 6D   m? 5      -   5        $??Poligon??5   D   $??Draw Polygon??6    . 6D   m? 5      -   5        $??AddText??5   @   $??Add Text??6    . 6@   m? 5      -   5        $??
UndoButton??5   )   $
??Undo??6   6;   m? 5      -   5        $??
ReDoButton??5   )   $
??Redo??6   6;   m? 5      -   5        $??Clear??5      6*   5      5      ?? 5   
   $??Bottom=?6   .+?5      ?? $??
SubStation??6   $	??Top=?6*   $??15(?6?   A? 6N   $??10 0 0 0q?6V   $??5??6h   $??5??6r   
??? ? ?6?   6?   ?? 5      $
??Node??6   $	??Top=?6$   $??15(?69   A? 6J   $??10 0 0 0q?6S   $??5??6e   $??5??6o   
???  ??6?   6?   ?? 5      $??Switch??6   $	??Top=?6&   $??15(?6;   A? 6K   $??10 0 0 0q?6T   $??5??6f   $??5??6p   
????  ?6?   6?   5      ?? 5   
   -	   5         ??Name$ slider??5       ? Minimum$ .1Q?6!    ? Maximum$ 10Q?6.    ? Value$ 1Q?6;   $	??Top=?6E   $??-33.6,0,-0.2,0q?6Z   ?? 5   
   $
??Auto=?6   $
??Auto=?6<   .??5       ?? -
   5        $??Glavni??5       $	??800??6#   $	??800??6/   
??????? ? LayoutTransform 5!      ?? 5"      	 ?? ScaleX	 6%   ?? 
 ?? ElementName$
 slider?? ?? Path$ Value? ?? ScaleY 5#   !   ?? $
 slider??$ Value?5'      5)      5*      5+      