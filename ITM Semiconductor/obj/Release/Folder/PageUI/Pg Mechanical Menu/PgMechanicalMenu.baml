   M S B A M L   `   `   `  ���� L  HITM Semiconductor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ITM_SemiconductorITM_Semiconductor  (    "ITM_Semiconductor.PgMechanicalMenu   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1localclr-namespace:ITM_Semiconductor       N� Title$  PgMechanicalMenu��.��� 
������� � ColumnDefinitions �� �� $	��200 �.+�� $��1�� � 	Resources  �� 
TargetType �� Property �� Value   sButton �� BasedOn��%�  '��       '�� �     7   '��     �� " ��.���  � �� �	#dd222222��  � ��$	 5,0q���  � ��$ Center=���  � ��$ Center=���  � ��$ 24(��� " ��8   .���  � ��$ 24(���  � ��$
 Bold"���  � ��$ 0,5,5,5q���  � ��
 �������  � ��$ 0q��� " ��.���  � ��$ Center=���  � ��$ 22(���  � ��$
 Bold"���  � ��
 �   �.+��� ����  �� 
StartPoint$	 0,0&� �� EndPoint$	 1,1&�.��� 	 � Color$	 #6787c0��
 � Offset$
 0Q�� $	 #879ec8��$
 1Q�._�� .+��� .+��  � Points �    -1��� ����!
���#879ec8� ' �    �� �� ���� ��
�����#879ec8A� $��250 450 0 0q�$��40��$��40�����#879ec8A� $��
50 400 0 0q�$��40��$��40�����#879ec8�� $��Bottom=�.+��� $	��ITM��  	titleText#��[ �� $��Semiconductor��  
normalText#��[  J� Opacity$	 0.8Q�$�� 20 0q�$
��Bold"��� $��AUTOMATION TEAM��#��[ $	 0.8Q�$�� 20 0q���  �� Orientation$ Vertical=�.+��� $
��Bold"�$��0 20 0 0q�
�������$��27(�$��Center=� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Color$ Black��   	Direction$	 320Q�   ShadowDepth$ 5Q�   Opacity$	 0.3Q�.��System Menu�� $ Vertical=�$��Center=�$��	0 100 0 0q�.+��� -    ��Name$ 
btSetting1��$��60��$	��215��  ssButton#��[ $��0 5 5 5q�$��0q�.���� $ 
Horizontal=�.+��� $'��!/Folder/PageUI/Icon/button on.png��$��Center=�$��0 5 0 0q���  �� Padding$ 10q�$��
MES       ��
���   �$��10q�$��Center=��� -   $ 
btSetting2��$��60��$	��215��.���� $ 
Horizontal=�.+��� $(��"/Folder/PageUI/Icon/button off.png��$��Center=�$��0 5 0 0q��� $ 10q�$��Barcode ��$��10q�$��Center=��� -   $ 
btSetting3��$��60��$	��215��.���� $ 
Horizontal=�.+��� $(��"/Folder/PageUI/Icon/button off.png��$��Center=�$��0 5 0 0q��� $ 10q�$��
Device    ��$��10q�$��Center=��� -   $ 
btSetting4��$��60��$	��215��.���� $ 
Horizontal=�.+��� $(��"/Folder/PageUI/Icon/button off.png��$��Center=�$��0 5 0 0q��� $ 10q�$��FTP         ��$��10q�$��Center=�� $��0�� � RowDefinitions �� $��60 ��� $	��300 ��� .+�� $��0��.+���  �� CornerRadius$
  10 ������ $	 0,0&�$	 1,1&�.��� $	 #879ec0��$
 0Q�� $	 White��$
 1Q��� $
��Bold"�
�������$��30(�$��0,0,10,0q�   $ Black��$	 320Q�$ 5Q�$	 0.3Q�.��Mechanical Menu�� $��1��$��[Handler Setting]��.���  �� $��1* ��� $��1* �.+�� $��0��$��2,0,2,0q� �� $��1* ��� $
��1.5* ��� $
��1.5* ��� $��2* �.+��� $��0��
�����_�._��� 
�������$��20(�$��Center=�$
��Bold"�$��Center=�Handler IP/Port�� $��1��$��2q����#aaaaaa._��  ��%v   '��       �� " ��.���  � ��$ Center=���  � ��$ Center=���  � ��$ 24(���  � ��$ 0q� �� $��3* ��� $��1* ��� $��3* ��� $��1* ��� $��3* ��� $��1* ��� $��3* �.+��� -   $��txtLocalIp1��$��0��.��192�� $��1�� �� 
IsReadOnly . $��-1��.��.�� -   $��txtLocalIp2��$��2��.��168�� $��3�� . $��-1��.��.�� -   $��txtLocalIp3��$��4��.��72�� $��5�� . $��-1��.��.�� -   $��txtLocalIp4��$��6��.��97�� $��2��$��2q����#aaaaaa._��  �� $��1* ��� $��4* �.+��� $��0��
�����_�
�������$��0,0,0,0q�$��24(�$
��Bold"�$��Center=�$��Center=�.��Port�� -	   $��txtLocalPort��$��1��$��24(�$��0q�$��Center=�$��Center=�.��5010� $��1��$��2,0,2,0q� �� $��1* ��� $
��1.5* ��� $
��1.5* ��� $��2* �.+��� $��0��
�����_�._��� 
�������$��20(�$��Center=�$
��Bold"�$��Center=�
Connection�� $��1��$��1q����#aaaaaa._�A�  A� Columns$ 2�� ��%u   '��       �� " ��.���  � ��$ Center=���  � ��$ Center=���  � ��$ 20(���  � {� .  .+��� -
    ��Name$ 	chkListen��.��Listen�� -   $ chkAccepted��.��
Accepted�� -   $��btSave��$��2��#��[ $��1q�$��	0,2,100,0q�$��Stretch=�$��30,0,0,0q�.���� $ 
Horizontal=�.+��� $��4q�$"��/Folder/PageUI/Icon/save.png���� $��20(�$��20,0,0,0q�$
��Bold"�$��Center=�	Save IP