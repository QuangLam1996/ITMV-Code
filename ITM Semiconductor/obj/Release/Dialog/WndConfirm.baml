   M S B A M L   `   `   `  ���� L  HITM Semiconductor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ITM_SemiconductorITM_Semiconductor  "    ITM_Semiconductor.WndConfirm   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     1localclr-namespace:ITM_Semiconductor     -     � WindowStartupLocation$  CenterScreen=� � Title$ Message��$	��250��$	��700��
������� � Topmost . .��� -   
������  � RowDefinitions �� $��5* ��� $��3* � � 	Resources  �� 
TargetType   sButton �� BasedOn �� Property �� Value��%�   '��       7   �� " ��8   .���  � ��$ 24(���  � ��$
 Bold"���  � ��$ 0,5,5,5q���  � �� �#008080.+�� $��0��
����ح��� -   $��
lblMessage��$��0��$	��100��
����ح�$��Center=�$$��Bahnschrift SemiBold Condensed)�$
��Bold"�
���   �$��30(�$��Center=�$��Center=�$��
10,0,10,00q�.��	Message�� 	 �� Orientation$	 
Horizontal=�$��1��$��0 15 0 0q�.+��� -   
 ��Name$
 btnYes��$��
80 0 10 10q�$	��120��$��50��.���� $	 
Horizontal=�.+��� 
�������.��	Yes (Y)�� -   $
 btnNo��$��280 0 10 10q�$	��120��$��50��.���� $	 
Horizontal=�.+��� 
�������.��No (N)