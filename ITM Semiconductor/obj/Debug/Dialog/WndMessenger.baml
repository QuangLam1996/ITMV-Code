   M S B A M L   `   `   `  ����L  HITM Semiconductor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ITM_SemiconductorITM_Semiconductor  5   	   $    ITM_Semiconductor.WndMessenger   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:ITM_Semiconductor     5   	   -   5          � Title$  WndMessenger��5   	    � Topmost . $	��260��5	   	   $	��850��6    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�6   .��5   
   � -   5        
������  � 	Resources 5       �� 
TargetType   sButton �� BasedOn �� Property �� Value��%�   '��       7   �� 5      " ��6   8   .�5      ��  � ��$ 24(�61   �� 5       � ��$
 Bold"�63   �� 5       � ��$ 0,5,5,5q�6/   �� 5       � �� �#00808063   5!      5"      	 � RowDefinitions	 5$      �� 5%      $��5* �6    6.   �� 5&      $��3* �6    6.   .+�5)      � $��0��6   
����ح�.+�5+      �� -   5        $��
lblMessage��5+      $
��Bold"�6*   
���   �$��30(�6P   $��Center=�6^   $��Center=�6y   $$��Bahnschrift SemiBold Condensed)�6�   .��6�   	Message5-      5.      � 5/      $��1��6   .+�51      �� -   5        
 ��Name$
 btnYes��51      $	��150��6*   $��50��66   .��52      ��  �� Orientation$ 
Horizontal=�6!   .+�54      �� 
�������.��63   
OK (Enter)6?   55      57      59      5>      5B      