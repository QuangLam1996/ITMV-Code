   M S B A M L   `   `   `  ����L  HITM Semiconductor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ITM_SemiconductorITM_Semiconductor  5   	   %    ITM_Semiconductor.WndAutoUpdate   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:ITM_Semiconductor     5   	     � Topmost  .  � WindowStartupLocation$ CenterScreen=�5	   	    � Title$ WndAutoUpdate��5
   	   $	��250��6   $	��700��6,   .��5      � -   5        
������  � RowDefinitions 5   
   �� 5      $��5* �6   6*   �� 5      $��3* �6   6*    � 	Resources 5   
    �� 
TargetType   sButton �� BasedOn �� Property �� Value��%�   '��       7   �� 5      " ��6   8   .�5      ��  � ��$ 24(�6-   �� 5       � ��$
 Bold"�6/   �� 5       � ��$ 0,5,5,5q�6+   �� 5       � �� �#0080806/   5      5      .+�5   
   � $��0��6   
����ح�65   �� 5   
   .+�5      �� $��0��6   $��60��6"   
����ح�$��Center=�6E   $$��Bahnschrift SemiBold Condensed)�6g   $
��Bold"�6�   
���   �$��30(�6�   $��Center=�6�   $��Center=�6�   $��10,10,10,00q�6�   .��6  GEĐã có phiên bản mới . Vui lòng xác nhận để câp nhật5!      �� 5"      -   5        $��
lblMessage��5"   !   $��0��6   $��60��63   
����ح�$��Center=�6V   $$��Bahnschrift SemiBold Condensed)�6x   $
��Bold"�6�   
���   �$��30(�6�   $��Center=�6�   $��Center=�6�   $��
10,0,10,00q�6  .��6%  Nội Dung UpDate5$      5%      �� 5'   
   	 �� Orientation$	 
Horizontal=�6   $��1��6.   $��0 15 0 0q�6;   .+�5(      �� -   5        
 ��Name$
 btnYes��5(      $��
80 0 10 10q�6%   $	��120��69   $��50��6E   .��5)      �� $	 
Horizontal=�6   .+�5+      �� 
�������.��6/   	Yes (Y)68   5,      5.      �� 5/      -   5        $
 btnNo��5/      $��280 0 10 10q�6$   $	��120��69   $��50��6E   .��50      �� $	 
Horizontal=�6   .+�52      �� 
�������.��6/   No (N)67   53      54      55      59      5;      