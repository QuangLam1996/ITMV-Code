   M S B A M L   `   `   `  ���� L  HITM Semiconductor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ITM_SemiconductorITM_Semiconductor  )    #ITM_Semiconductor.PgMechanicalMenu3   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1localclr-namespace:ITM_Semiconductor       N� Title$  PgMechanicalMenu3��.��� 
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
btSetting1��$��60��$	��215��.���� $ 
Horizontal=�.+��� $(��"/Folder/PageUI/Icon/button off.png��$��Center=�$��0 5 0 0q���  �� Padding$ 10q�$��
MES       ��$��10q�$��Center=��� -   $ 
btSetting2��$��60��$	��215��.���� $ 
Horizontal=�.+��� $(��"/Folder/PageUI/Icon/button off.png��$��Center=�$��0 5 0 0q��� $ 10q�$��Barcode ��$��10q�$��Center=��� -   $ 
btSetting3��$��60��$	��215��.���� $ 
Horizontal=�.+��� $(��"/Folder/PageUI/Icon/button off.png��$��Center=�$��0 5 0 0q��� $ 10q�$��
Device    ��$��10q�$��Center=��� -   $ 
btSetting4��$��60��$	��215��  ssButton#��[ $��0 5 5 5q�$��0q�.���� $ 
Horizontal=�.+��� $'��!/Folder/PageUI/Icon/button on.png��$��Center=�$��0 5 0 0q��� $ 10q�$��FTP         ��
���   �$��10q�$��Center=�� $��0��$��0 5 0 0q� � RowDefinitions �� $��60 ��� .+�� $��0��.+�� $��0��.+���  �� CornerRadius$
  10 ������ $	 0,0&�$	 1,1&�.��� $	 #879ec0��$
 0Q�� $	 White��$
 1Q��� $
��Bold"�
�������$��30(�$��0,0,10,0q�   $ Black��$	 320Q�$ 5Q�$	 0.3Q�.��FTP Server IV3� $��1�� �� $
��0.3* ��� $��0.15* ��� $��0.35* ��� $
��0.2* ��� $
��0.3* �.+�� $��0�� �� $
��2.5* ��� $��* ��� $��* ��� $��0��$��18(�$��1q�$��Arial)�$��SemiBold"�$��4 0 0 2q� ��%�   '��       '�� @     '�� �     �� " ��.���  � ��$ 	10,5,10,5q���  � ��$ 15(��� " ��.���  � ��$ Center=���  � ��$ 2,2,2,2q���  � ��$ 24(��� " ��.���  � ��$ Center=���  � ��$ 20(�.���  �� $��0.05* ��� $
��0.8* ��� $��0.05* ��� $
��1.5* ��� $��0.05* ��� $��1* ��� $��0.05* �.+�� $��1��$��0��$��2q�.+��� $!��/Folder/PageUI/Icon/FTP.png���� -    ��Name$ txtLogs��$��3��$��16(� �� TextWrapping$
 Wrap=�" �� VerticalScrollBarVisibility$ Visible=�$	��Top=�$
��Left=�
���   �
����������� *rxLog+� $��5��$��0�� ��%F  '��       7   '�� �     �� " ��8   .���  � ��$ 24(���  � ��$
 Bold"���  � ��$ 0,5,5,5q���  � �� �#6787c0��  � ��$	 0.7q���  � ��
 �   ��� " ��.���  � ��$ Center=���  � ��$ 23(���  � ��$ Arial)���  � ��$ Normal"���  � ��
 ����� �� $��1* ��� $��1* � �� $��1* ��� $��1* ��� $��0* ��� $
��0.9* �.+��� -   $ 	btSetting��$��0��$��0��.���� $ 
Horizontal=�.+��� $&�� /Folder/PageUI/Icon/setting2.png��$��2q��� $��Center=�$ 15,0,0,0q�	Setting� $��3��$��0��$��2�� �� $��90 ��� $��* �