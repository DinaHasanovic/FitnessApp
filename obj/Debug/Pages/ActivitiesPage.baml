   M S B A M L   `   `   `  ����M  IFitness_Aplikacija, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Fitness_Aplikacija.PagesFitness_Aplikacija.Pages  5      -    'Fitness_Aplikacija.Pages.ActivitiesPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:Fitness_Aplikacija.Pages     5        N� Title$  ActivitiesPage��5	       � DataContext 5      ��  �� RelativeSource �� *Self+.��5      ��  �� Orientation$ Vertical=�6   
�������.+�5   
   �� $	��800��6   $��50��6!   $��Center=�6-   $��Center=�6J   $ 
Horizontal=�6e   .+�5      �� -   5        $��Nazad��5      $	��100��6%   $��20��61   $
��Left=�6=   $��
0 10 175 0q�6X   6l   �� 5      $��24(�6   $��Center=�6&   $��Informacije o treningu:��6C   $��0 10 0 0q�6b   5      �� 5   
   .+�5      �� -   5         ��Name$ trainingTextBlock��5      $��18(�63   $��40��6A   $
��Left=�6M   5      �� 5   
   $��Center=�6   $ 
Horizontal=�62   .+�5      �� $ Vertical=�6   $��Center=�60   $��Center=�6M   .+�5      �� $��Vrsta aktivnosti��6   $��Center=�64   �� 5      $	��100��6   $��
10 10 10 5q�6&   $��25��6:   $��16(�6F    �� TextAlignment$ Center=�6T   ��6k   �� *Vrsta+ �� Mode$ TwoWay=�5      �� 5      $ Vertical=�6   $��Center=�60   $��Center=�6M   .+�5      �� $��Trajanje aktivnosti��6   $��Center=�67   �� 5      $ 
Horizontal=�6   .+�5      �� $��50��6   $��
10 10 10 5q�6)   $��25��6=   $��16(�6I   $ Center=�6W   ��6n   �� *Hours+$ TwoWay=��� 5      $��50��6   $��
10 10 10 5q�6)   $��25��6=   $��16(�6I   $ Center=�6W   ��6n   �� *	Minutes+$ TwoWay=�5      5      �� 5      $ Vertical=�6   $��Center=�60   $��Center=�6M   .+�5       �� $��Intenzitet aktivnosti��6   $��Center=�69   �� 5!      -   5         ��Name$ cbInteziteti��5$      $	��100��5!      $��
10 10 10 5q�6'   $��25��6;   $��16(�6G   ��5"      �� *Intensities+ �� SelectedItem 5#      �� *SelectedIntensity+$ TwoWay=�5%      5&      �� 5'      $ Vertical=�6   $��Center=�60   $��Center=�6M   .+�5(      �� $��Broj potrosenih kalorija��6   $��Center=�6<   �� 5)      $	��100��6   $��
10 10 10 5q�6&   $��25��6:   $��16(�6F   $ Center=�6T   ��6k   �� *
Kalorije+$ TwoWay=�5*      5+      �� 5,   
   $	��540��6   $	��150��6!   .+�5-      �� -   5        	 ��Name$	 lvActivities��5-      $	��150��6^   $	��540��6k   $��Center=�6w   $��Center=�6�   $��Center=�6�   $��Center=�6�   
 �� View
 5.      �� 5/      .P�50      �� $��Vrsta��6)    �� Width$	 150��68    �� DisplayMemberBinding 6D   �� *Vrsta+�� 51      $��Trajanje(hh:mm)��6)   $	 120��6B    6N   �� *
Trajanje+�� 52      $��
Intenzitet��6)   $	 120��6=    6I   �� *	Intezitet+�� 53      $��Potrosene kalorije��6)   $	 150��6E    6Q   �� *
Kalorije+54      56      57      �� 58   
   $	��400��6   $��Center=�6!   $��Center=�6>   $��50��6Z   $ 
Horizontal=�6f   .+�59      �� -   5        $	��100��59      $��20��6!   $��Dodaj aktivnost��6-   $��	40 0 10 0q�6_   �� 5:      -   5        $	��100��5:      $��20��6!   $��Izmeni aktivnost��6-   $��0 0 10 0q�6a   �� 5;      -   5        $	��100��5;      $��20��6!   $��Izbrisi aktivnost��6-   $��0 0 10 0q�6c   5<      5=      5>      