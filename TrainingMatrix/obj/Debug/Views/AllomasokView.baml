   M S B A M L   `   `   `  ����E  ATreningelo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Treningelo.ViewsTreningelo.Views  5      =#clr-namespace:Treningelo.ViewModelsTreningelo.ViewModels  5      =#clr-namespace:Treningelo.ConvertersTreningelo.Converters  5      $    Treningelo.Views.AllomasokView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:Treningelo.Views     5      :
viewmodels#clr-namespace:Treningelo.ViewModels     5      :
converters#clr-namespace:Treningelo.Converters     5        � DataContext  5      .  (Treningelo.ViewModels.AllomasokViewModel  5   
    � 	Resources 5         VisibleIfTrueConverter8  2Treningelo.Converters.DashWhenEmptyStringConverter   DashWhenEmptyStringConverter1  +Treningelo.Converters.DashWhenZeroConverter  DashWhenZeroConverter��%W   &	        &	      &	 &     �� 5   
   6'     5   
   62     5   
   6+   5      .��5      �  � RowDefinitions 5   
   �� 5      $
��AUTO �6   �� 5      $��* �6   �� 5      $
��AUTO �6   .+�5   
   �  � ColumnDefinitions 5      �� 5      �� 5      .+�5       � $��5q�6    5!      �� 5"      $��* �6$   �� 5#      $��* �6$   �� 5$      $��* �6$    5&      �� 5'      $
��AUTO �6'   �� 5(      $��* �6'   .+�5+      �� $��Terület��6   $��3q�6*   �� 5,      $��Gyártósor��6   $��1��6,   $��3q�69   �� 5-      $��Munkaállomás��6   $��2��6/   $��3q�6<   �� 5.      $ �� IsSynchronizedWithCurrentItem$
 TrueY�50       �� DisplayMemberPath$	 Nev��51       �� SelectedIndex$ 0��52      $��0��6-   $��1��6:   ��5.      ��  �� Path$ 	Teruletek� �� SelectedItem 5/      �� $ SelectedTerulet�52   :   �� 53      $
 TrueY�55      $	 Nev��56      $ 0��57      $��1��6-   $��1��6:   ��53      �� $ Sorok� 54      �� $ SelectedSor�57   :   �� 58      $
 TrueY�5:      $	 Nev��5;      $ 0��5<      $��2��6-   $��1��6:   ��58      �� $ 	Allomasok� 59      �� $ SelectedAllomas�5<   :   5=      � 5>      $��5q�6   $��1��6    5?      �� 5@      $��* �6$   �� 5A      $��* �6$   �� 5B      $��* �6$    5D      �� 5E      $
��AUTO �6'   �� 5F      $��* �6'   .+�5H      �� $��Anyagok:��6   �� 5I      $��Pontérték:��6   $��1��6-   �� 5J      $��Tréningeltek:��6   $��2��60   �� 5K      $
��Left=�6{   $��1��6�   ��6   �� *SelectedAllomas.Anyag+	 �� 	Converter#	 [ 6�   �� 5L      $
��Left=�6{   $��1��6�   $��1��6�   ��6   �� *SelectedAllomas.PontertekInt+#	 [ 6�   �� 5M      $
��Left=�6v   $��1��6�   $��2��6�   ��6   �� *SelectedAllomas.Trained+#	 [ 6�   5N      5O      � 5Q   
   $��1��6    5R      �� 5S      �� 5T      $
��AUTO �6#   .+�5W      &   System.Windows.Controls.DataGrid  $��1��6   
   AutoGenerateColumns
 .  ��6$   �� $ TrainingsView�   Columns 5X      0  *System.Windows.Controls.DataGridTextColumn  5Y      ,  &System.Windows.Controls.DataGridColumn   Header$ Törzsszám��6H      Width5  /System.Windows.Controls.DataGridLengthConverter$
 AUTO 6[   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   �� *
DolgozoTsz+   	CellStyle 5Z      �� 5[       �� 
TargetType*	  $System.Windows.Controls.DataGridCell" 	 6$   �5\   "   �� 5]   &    �� Property �@��TextAlignment �� Value$ Center=�6P   5_      5a        5b      $
 Név��6H   $
 AUTO 6U    6)   �� *
DolgozoNev+6U     5c      $ Terület��6H   $ 1* 6Y    6)   �� *
TeruletNev+6Y     5d      $ Gyártósor��6D   $ 1* 6W    6)   �� *SorNev+6W     5e      $ Munkaállomás��6H   $ 2* 6^    6)   �� *
AllomasNev+6^     5f      $ Tréning kezdete��6i   $
 AUTO 6�    6)   �� *TreningStart+ �� StringFormat$ {0:yyyy.MM.dd}�� 5g      �� 5h      " 	 6$   �5i   "   �� 5j   &    �@��TextAlignment$ Center=�6P   5l      5n        5o      $ Tréning vége��6g   $
 AUTO 6}    6)   �� *
TreningEnd+$ {0:yyyy.MM.dd}�� 5p      �� 5q      " 	 6$   �5r   "   �� 5s   &    �@��TextAlignment$ Center=�6P   5u      5w        5x      $ Mentor��6D   $ 2* 6T    6)   �� *Mentor+ 5y      �� 5z      " 	 6$   �5{   "   �� 5|   &    �@��TextAlignment$ Center=�6P   5~      5�        5�      $ 2* 6)   $ Megjegyzés��64    6H   �� *StarWhenCommented+ 5�      �� 5�      " 	 6$   �5�   "   �� 5�   &    �@��TextAlignment$ Center=�6P   5�      5�         RowStyle 5�      �� 5�      )
  #System.Windows.Controls.DataGridRow" 
 6    �� Triggers 5�      �� 5�       �� Binding 6*   �� *
IsComplete+ �� Value	 False6I   .a�5�   "   ��  � ��  quinaryBrush# [ 6?   5�      �� 5�       6*   �� *
Mentoral+ True6G   .a�5�   "   ��  � ��  primaryBrush# [ 6?   5�      ^� 5�       ^� 
Conditions 5�   "   �� 5�   &    �� Binding 60   �� *
HasComment+ �� Value True6O   0�5�   "   �� 5�   &    �@�ToolTip 5�   *   _� 5�   .   ��66   �� *
Megjegyzes+5�   '   5�      5�       � ContextMenu 5�      �� 5�      {�6"   �� *HasEditRights+.@�5�      m� $$��Hozzáadás a képzési laphoz��6#    m� CommandParameter 5�   #   ��  �� RelativeSource �� *FindAncestor+ �� AncestorType" ��$# PlacementTarget.SelectedItems� m� Command 5�   #   �� *AddToEmployeesToTrainCommand+5�      5�      � 5�      $��1��6   y�6#   �� *EmployeesToTrain.IsNotEmpty+#	 [   5�      �� 5�      �� 5�      $
��AUTO �6$   .+�5�      �� $��3q�6   ��6%   �� *EmployeesToTrain+ 5�      �� 5�      {�6&   �� *HasEditRights+#	 [  .@�5�      m� $(��"Eltávolítás a képzési lapról��6'    5�   '   �� *PlacementTarget.SelectedItem+ ��   �� Mode$  FindAncestor=�" �� 5�   '   �� *#!RemoveFromEmployeesToTrainCommand+5�      5�      �� 5�      $��1��6   $��Képzési lap nyomtatása��6&   ��5�      �� *PrintTrainingSheetCommand+5�      5�      ]� 5�   
   $��2��6   $��26��6!   $��Stretch=�6-   ��. .+�5�      �� $��1q�6   
���   �$��4q�6=   ._�5�      �� $��30��6   $��20��6'   $��0q�63   #��[ 6>   5�      �� 5�      $��Mentori��6   $��Center=�6'   $	��100��6B   �� 5�      $��1q�6   
���   �$��4q�6=   ._�5�      �� $��30��6   $��20��6'   $��0q�63   
�������6>   5�      �� 5�      $��Normál��6   $��Center=�6&   $	��100��6A   �� 5�      $��1q�6   
���   �$��4q�6=   ._�5�      �� $��30��6   $��20��6'   $��0q�63   #��[ 6>   5�      �� 5�      $��Befejezetlen��6   $��Center=�6,   $	��100��6G   �� 5�      $+��%★ tréningre vonatkozó megjegyzés��6   $��Center=�6@   $��Italic$�6[   $��Right=�6n   5�      5�      5�      