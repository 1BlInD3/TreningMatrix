   M S B A M L   `   `   `  ����E  ATreningelo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Treningelo.WindowsTreningelo.Windows  5   	   +    %Treningelo.Windows.EmployeeEditWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:Treningelo.Windows     5   	     � 
ResizeMode$  CanMinimize=�5   	    � SizeToContent$ WidthAndHeight=�6"   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��AUTO �6   �� 5      $
��AUTO �6   �� 5      $
��AUTO �6   �� 5      $
��AUTO �6   �� 5      $
��AUTO �6   �� 5      $
��AUTO �6    � ColumnDefinitions 5   
   �� 5      $
��AUTO �6   �� 5      $	��250 �6   .+�5   
   �� $��Törzszám:��6   �� 5   
   $��Név:��6   $��1��6   �� 5   
   $��Mentor:��6   $��2��6"   �� 5   
   $��Orvosi korlátozás:��6   $��3��6-   $	��Top=�6:   �� 5   
   $��Megjegyzés:��6   $��4��6&   $	��Top=�63   �� 5   
   -   5         ��Name$ tszBox��5   v   $��1��6W    �� 	MaxLength$ 10��6g   ��6   ��  �� Path$ 	Torzsszam� �� UpdateSourceTrigger$ PropertyChanged=�6v   �� 5   
   $��1��6Q   $��1��6a   $ 50��6n   ��6   �� $	 Nev�$ PropertyChanged=�6n   �� 5   
   $��1��65   $��2��6E    d� 	IsChecked 6   �� $ Mentor�6E   �� 5   
   $��1��5      $��3��6"   $	 100��6/   $��50��6?   $	��Top=�6K   	 �� TextWrapping$
	 Wrap=�5       
 �� AcceptsReturn
 .  �� 	IsEnabled . ��5      �� $ 	OrvKorlat�$ PropertyChanged=� �� TargetNullValue$  ��5    ;   �� 5!   
   $��1��5"      $��4��6"   $	 250��6/   $��50��6?   $	��Top=�6K   $
	 Wrap=�5#      
 .  . ��5!      �� $ 
Megjegyzes�$ PropertyChanged=�$  ��5#   ;   � 5$   
   $��5��6   $��1��6    5%      �� 5&      �� 5'      .+�5)      �� $��Mégse��6   ��5*      �� *CancelEditCommand+��5+      ��  �� RelativeSource �� *FindAncestor+ �� AncestorType" ��� 5,      $��OK��6   $��1��6"   ��5-      �� *EndEditCommand+��5.      ��  �� *FindAncestor+" �{�5/      �� $ IsTorzsszamValid�50      51      53      