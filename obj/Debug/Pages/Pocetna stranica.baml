   M S B A M L   `   `   `  ����M  IFitness_Aplikacija, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Fitness_Aplikacija.PagesFitness_Aplikacija.Pages  5      /    )Fitness_Aplikacija.Pages.Pocetna_stranica   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:Fitness_Aplikacija.Pages     5        N� Title$  Pocetna_stranica��5	       N� 
Background
 ����� � DataContext 5      ��  �� RelativeSource �� *Self+.��5
      ��  �� Orientation$ Vertical=�6   .+�5   
   �� $��Center=�6   $	��Top=�62   $��50��6J   .+�5      �� -   5         ��Name$ WelcomeText��5   &   $��20(�6   6&   5      �� 5   
   $ 
Horizontal=�6   $	��600��6.   $��50��6:   .+�5      �� $ Vertical=�6   $��Center=�60   $��Center=�6M   $��
100 0 20 0q�6h   .+�5      �� $)��#Unesite datum treninga(dd/mm/yyyy):��6   �� 5      $ 
Horizontal=�6   $��Center=�66   $��Center=�6Q   .+�5      �� $��50��6   $��
10 10 10 5q�6)   $��25��6=    �� TextAlignment$ Center=�6k   $��16(�6�   ��6J   �� *Day+ �� Mode$ TwoWay=�6�   �� 5      $��50��6   $��
10 10 10 5q�6)   $��25��6=   $��16(�6I   $ Center=�6z   ��6W   �� *Month+$ TwoWay=�6z   �� 5      $��50��6   $��
10 10 10 5q�6)   $��25��6=   $��16(�6I   $ Center=�6y   ��6W   �� *Year+$ TwoWay=�6y   5      5      �� 5      $ Vertical=�6   $��Center=�60   $��Center=�6M   .+�5      �� $'��!Unesite trajanje treninga(hh:mm):��6   �� 5      $ 
Horizontal=�6   $��Center=�66   $��Center=�6Q   .+�5      �� $��50��6   $��
10 10 10 5q�6)   $��25��6=   $ Center=�6m   $��16(�6�   ��6J   �� *Hours+$ TwoWay=�6�   �� 5      $��50��6   $��
10 10 10 5q�6)   $��25��6=   $��16(�6I   $ Center=�6|   ��6W   �� *	Minutes+$ TwoWay=�6|   5      5      5      �� 5   
   $	��250��6   .+�5       �� -   5         ��Name$ lvTrainings��5       $	��240��6�   $	��240��6�   $��0 5 0 0q�6�   	 �� View	 5!      �� 5"      .P�5#      �� $��Datum��6)   
 �� Width$	
 120��6_    �� DisplayMemberBinding 68   �� *Datum+6_   �� 5$      $��Trajanje��6)   $	
 120��6e    6;   �� *
Trajanje+6e   5%      5'      5(      �� 5)   
   $ 
Horizontal=�6   $	��350��6.   $��Center=�6:   $��Center=�6W   .+�5*      �� -   5        $��Dodaj trening��5*      $��Center=�6C   $��Center=�6`   $��0 0 50 0q�6{   �� 5+      -   5        $��Izbrisi trening��5+      $��Center=�6G   $��Center=�6d   $��0 0 50 0q�6   �� 5,      -   5        $��Detaljniji pregled��5,      $��Center=�6E   $��Center=�6c   5-      5.      5/      