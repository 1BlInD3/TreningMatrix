   M S B A M L   `   `   `  ����E  ATreningelo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:Treningelo.WindowsTreningelo.Windows  5   	   =#clr-namespace:Treningelo.ViewModelsTreningelo.ViewModels  5   	   +    %Treningelo.Windows.TrainingEditWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:Treningelo.Windows     5   	   :
viewmodels#clr-namespace:Treningelo.ViewModels     5   	     � 
ResizeMode$  	CanResize=�5       � SizeToContent$ WidthAndHeight=�62   $��439.333��6R   .��5	      � $��0,0,0,50q�6    � ColumnDefinitions 5
   
   �� 5      $
��AUTO �6   �� 5      $	��250 �6    � RowDefinitions 5   
   �� 5      $
��AUTO �6   �� 5      $
��AUTO �6   �� 5      $
��AUTO �6   �� 5      $
��AUTO �6   �� 5      $
��AUTO �6   �� 5      $
��AUTO �6   �� 5      $
��AUTO �6   �� 5      $
��AUTO �6   �� 5      $
��AUTO �6   �� 5      $
��AUTO �6   �� 5      $
��AUTO �6   .+�5   
   �� $��Törzsszám:��6   �� 5   
   $��Név:��6   $��1��5      �� 5   
   $��	Terület:��6   $��2��5      �� 5    
   $��Gyártósor:��6   $��3��5!      �� 5"   
   $��Munkaállomás:��6   $��4��5#      �� 5$   
   $��Tréning kezdete:��6   $��5��5%      �� 5&   
   $��Tréning vége:��6   $��6��5'      �� 5(   
   $��
Mentorál:��6   $��7��5)      �� 5+   
   $��1��5,      $
��Left=�6$   ��5+      ��  �� Path$ 
DolgozoTsz� �� UpdateSourceTrigger$ PropertyChanged=�5,   $   �� 5-   
   $��1��5.      $
��Left=�6!   $��1��5/      ��5-      �� $ 
DolgozoNev�$ PropertyChanged=�5/      �� 50   
   $ �� IsSynchronizedWithCurrentItem$
 TrueY�52       �� DisplayMemberPath$	 Nev��53       �� SelectedIndex$ 0��54      $��2��6)   $��1��66   ��50      �� $ 	Teruletek�	 �� SelectedItem	 51      �� $ SelectedTerulet�54   6   �� 55   
   $
 TrueY�57      $	 Nev��58      $ 0��59      $��3��6)   $��1��66   ��55      �� $ Sorok�	 56      �� $ SelectedSor�59   6   �� 5:   
   $
 TrueY�5<      $	 Nev��5=      $ 0��5>      $��4��6)   $��1��66   ��5:      �� $ 	Allomasok�	 5;      �� $ SelectedAllomas�5>   6   (  "System.Windows.Controls.DatePicker  5?   
   $��5��5@      $��1��6"   $��3q�62   $��Center=�6=   $��14(�6X   
   SelectedDate
 5?      �� $ TreningStart�$ PropertyChanged=�5@   X     5A   
   $��6��5C      $��1��6"   $��3q�62   $��Center=�6=   $��14(�6X   
 5A      �� $ 
TreningEnd�$ PropertyChanged=� �� TargetNullValue$  ��   DisplayDateStart 5B      �� $ TreningStart�5C   X   �� 5D   
   $��7��5F      $��1��6    $��5,0,0,0q�60    d� 	IsChecked 5D      �� $ Mentoral�$ PropertyChanged=�{�5E      �� $ 
IsComplete�$ PropertyChanged=�5F   0   �� 5H   
   $��Megjegyzés:��6   $��9��6&   $	��Top=�63   �� 5I   
   $��1��5J      $��9��6"    �� 	MaxLength$	 100��6/   $��50��6?   $	��Top=�6K    �� TextWrapping$
 Wrap=�5K       �� AcceptsReturn .  �� 	IsEnabled . ��5I      �� $ 
Megjegyzes�$ PropertyChanged=�$  ��5K   ;   � 5M   
   $��10��6   $��1��6   $��
0,54,0,-54q�6-    5N      �� 5O      �� 5P      .+�5R      �� $��0��6   $��Mégse��6%    J� InputBindings 5S      d� 5T       d� Gesture$ 	LeftClicka�6#    �� Command 5U   #   �� $ CancelEditCommand� �� CommandParameter 5V   #   ��  �� RelativeSource �� *FindAncestor+ �� AncestorType" �5X      �� 5Y      -   5         ��Name$	 btn��5Y      $��1��6"   $��OK��62   {�6?   �� $ IsTrainingValid� 5Z      d� 5[      $ 	LeftClicka�6#    5\   #   �� $ EndEditCommand� 5]   #   ��  �� *FindAncestor+" �5_      5`      �� 5a   
   $��Mentor
��6   $
��Left=�6+   $��
42,10,0,-4q�6F   $��10��6Z   $	��Top=�6h   $��28��6�   $��61��6�   �� 5b   
   $��1��5c      $
��Left=�6   $��38��68   $��
5,10,0,-15q�6D   $��10��6X   $
 Wrap=�6f   $	��Top=�6z   $	��240��6�   ��5b      �� $ Mentor�$ PropertyChanged=�$  ��5c   �   5d      5e      