   M S B A M L   `   `   `  ���� L  HITM Semiconductor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ITM_SemiconductorITM_Semiconductor  %    ITM_Semiconductor.WndFTPSetting   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     1localclr-namespace:ITM_Semiconductor       � Title$  WndFTPSetting�� � WindowStartupLocation$ CenterScreen=�$	��300��$	��800��.��� -   $��MainGrid�� � 	Resources  �� 
TargetType   sButton �� BasedOn �� Property �� Value��%F  '��       7   '�� �     �� " ��8   .���  � ��$ 24(���  � ��$
 Bold"���  � ��$ 0,5,5,5q���  � �� �#6787c0��  � ��$	 0.7q���  � ��
 �   ��� " ��.���  � ��$ Center=���  � ��$ 23(���  � ��$ Arial)���  � ��$ Normal"���  � ��
 ����� � ColumnDefinitions �� $
��0.8* ��� $��* ��� $
��0.8* ��� $��* � � RowDefinitions �� $��10 ��� $��40 ��� $��40 ��� $��40 ��� $��40 ��� $��60 �.+��� $��1��$��0��
�����_�$��1q�._��� 
�������$��17(�$��Center=�$
��Bold"�$��Center=�FTP IP Server�� $��1��$��2��
�����_�$��1q�._��� 
�������$��17(�$��Center=�$
��Bold"�$��Center=�
Use Name�� $��2��$��0��
�����_�$��1q�._��� 
�������$��17(�$��Center=�$
��Bold"�$��Center=�Port FTP Server�� $��2��$��2��
�����_�$��1q�._��� 
�������$��17(�$��Center=�$
��Bold"�$��Center=�
Password�� $��3��$��0��
�����_�$��1q�._��� 
�������$��17(�$��Center=�$
��Bold"�$��Center=�Folder FTP Server�� $��4��$��0��
�����_�$��1q�._��� 
�������$��17(�$��Center=�$
��Bold"�$��Center=�
Name Image�� -   	 ��Name$	 txtFileName��$��4��$��1��$��3��$��16(�$
��Bold"�$��2q�
�������$��Center=�$��Center=��� -   $	 txtFTPIp��$��1��$��1��$��16(�$
��Bold"�$��2q�
�������$��Center=�$��Center=��� -   $	 
txtFTPUser��$��1��$��3��$��16(�$
��Bold"�$��2q�
�������$��Center=�$��Center=��� -   $	 
txtFTPPort��$��2��$��1��$��16(�$
��Bold"�$��2q�
�������$��Center=�$��Center=��� -   $	 txtFTPPw��$��2��$��3��$��16(�$
��Bold"�$��2q�
�������$��Center=�$��Center=��� -   $	 	txtFolder��$��5��$��3��$��1��$��16(�$
��Bold"�$��2q�
�������$��Center=�$��Center=�.��/A� $��5��$��0��$��5��
 A� Columns$
 2��$
��50,0q�$��60��.+��� -    ��Name$ btnSave��$��0��$��0��$��10,2,2,2q�$��Center=�$��Center=�
�������$��16(�.����  �� Orientation$ 
Horizontal=�.+��� $��3q�$"��/Folder/PageUI/Icon/save.png���� $��Center=� �� Padding$ 20,0,0,0q�Save�� -	   $ 	btnCancel��$��0��$��0��$��10,2,2,2q�$��Center=�$��Center=�
�������$��16(�.���� $ 
Horizontal=�.+��� $��3q�$#��/Folder/PageUI/Icon/Close.png���� $��Center=�$ 20,0,0,0q�Cancel