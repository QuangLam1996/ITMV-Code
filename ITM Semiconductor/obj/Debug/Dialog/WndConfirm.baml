   M S B A M L   `   `   `  ����L  HITM Semiconductor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ITM_SemiconductorITM_Semiconductor  5   	   "    ITM_Semiconductor.WndConfirm   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:ITM_Semiconductor     5   	   -   5          � WindowStartupLocation$  CenterScreen=�5   	    � Title$ Message��5	   	   $	��250��6   $	��700��6&   
������� � Topmost . .��5   
   � -   5        
������  � RowDefinitions 5      �� 5   
   $��5* �6   6&   �� 5   
   $��3* �6   6&    � 	Resources 5       �� 
TargetType   sButton �� BasedOn �� Property �� Value��%�   '��       7   �� 5      " ��6   8   .�5      ��  � ��$ 24(�61   �� 5       � ��$
 Bold"�63   �� 5       � ��$ 0,5,5,5q�6/   �� 5       � �� �#00808063   5      5      .+�5      � $��0��6   
����ح�69   �� 5       -   5        $��
lblMessage��5    %   $��0��6   $	��100��67   
����ح�$��Center=�6[   $$��Bahnschrift SemiBold Condensed)�6}   $
��Bold"�6�   
���   �$��30(�6�   $��Center=�6�   $��Center=�6�   $��
10,0,10,00q�6  .��6*  	Message5"      �� 5#      	 �� Orientation$	 
Horizontal=�6   $��1��66   $��0 15 0 0q�6C   .+�5$      �� -   5        
 ��Name$
 btnYes��5$      $��
80 0 10 10q�6-   $	��120��6A   $��50��6M   .��5%      �� $	 
Horizontal=�6%   .+�5'      �� 
�������.��67   	Yes (Y)6@   5(      5*      �� 5+      -   5        $
 btnNo��5+      $��280 0 10 10q�6,   $	��120��6A   $��50��6M   .��5,      �� $	 
Horizontal=�6%   .+�5.      �� 
�������.��67   No (N)6?   5/      50      51      55      58      