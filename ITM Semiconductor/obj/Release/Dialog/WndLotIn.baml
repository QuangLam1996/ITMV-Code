   M S B A M L   `   `   `  ���� L  HITM Semiconductor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ITM_SemiconductorITM_Semiconductor       ITM_Semiconductor.WndLotIn   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     1localclr-namespace:ITM_Semiconductor       � WindowStyle$
  None=� � Title$ LOT IN��$	��320��$	��480�� � WindowStartupLocation$ CenterScreen=�.���  � RowDefinitions �� $��1* ��� $��3* ��� $��1* �.+�� $��0�����#879ec0.+��� $��0��$��32(�
������ 
�������$
��Bold"�$��Center=�$��Center=�.��LOT IN DATA� $��1��$��0,2,0,2q� � 	Resources  �� 
TargetType �� Property �� Value��%&  '��       '�� �     �� " ��.���  � ��$ 1q���  � ��
 �������  � ��$ 20(���  � ��$ 0,0,0,0q���  � ��$ 2,2,2,2q���  � ��$ Center=���  � ��$ Center=��� " ��.���  � ��$ 2,2,2,2q���  � ��$ 20(���  � ��$ Center=� � ColumnDefinitions �� $��1* ��� $��2* � �� $��1* ��� $��1* ��� $��1* ��� $��1* �.+��� $��0��$��0��.��
WORK GROUP�� -   	 ��Name$	 txtWorkGroup��$��0��$��1���� $��1��$��0��.��	DEVICE ID�� -   $	 txtDeviceId��$��1��$��1���� $��2��$��0��.��LOT ID�� $��2��$��1��
 �� Orientation$
 
Horizontal=�.+��� -   $	 txtLotId��$	��150��$
��Left=��� $��3��$��0��.��	LOT QTY�� -   $	 	txtLotQty��$��3��$��1��A� $��2�� A� Columns$ 2��    ssButton �� BasedOn��%�   '��       7   �� " ��8   .���  � ��$ 	10,5,10,5q���  � ��$ 20(���  � ��$
 Bold"���  � �� �#879ec9��  � ��
 �������  � ��$ Center=�.+��� -    ��Name$
 btOk��$��0q�$��20(�$��
 10 5 10 5q�.��OK�� -   $ btCancel��$��0q�$��20(�$��
 10 5 10 5q�.��CANCEL