   M S B A M L   `   `   `  ���� L  HITM Semiconductor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ITM_SemiconductorITM_Semiconductor  )    #ITM_Semiconductor.PgMechanicalMenu1   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1localclr-namespace:ITM_Semiconductor       N� Title$  PgMechanicalMenu1��.��� 
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
Horizontal=�.+��� $(��"/Folder/PageUI/Icon/button off.png��$��Center=�$��0 5 0 0q���  �� Padding$ 10q�$��	Setting 1��$��10q�$��Center=��� -   $ 
btSetting2��$��60��$	��215��  ssButton#��[ $��0 5 5 5q�$��0q�.���� $ 
Horizontal=�$
��Left=�.+��� $'��!/Folder/PageUI/Icon/button on.png��$��Center=�$��0 5 0 0q��� $ 10q�$��Barcode ��
���   �$��10q�$��Center=��� -   $ 
btSetting3��$��60��$	��215��.���� $ 
Horizontal=�.+��� $(��"/Folder/PageUI/Icon/button off.png��$��Center=�$��0 5 0 0q��� $ 10q�$��
Device    ��$��10q�$��Center=��� -   $ 
btSetting4��$��60��$	��215��.���� $ 
Horizontal=�.+��� $(��"/Folder/PageUI/Icon/button off.png��$��Center=�$��0 5 0 0q��� $ 10q�$��	Setting 4��$��10q�$��Center=�� $��0�� � RowDefinitions �� $��60 ��� .+�� $��0�� �� �� $	��160 ��� $	��160 � ��%F  '��       7   '�� �     �� " ��8   .���  � ��$ 24(���  � ��$
 Bold"���  � ��$ 0,5,5,5q���  � �� �#6787c0��  � ��$	 0.7q���  � ��
 �   ��� " ��.���  � ��$ Center=���  � ��$ 23(���  � ��$ Arial)���  � ��$ Normal"���  � ��
 �����.+���  �� CornerRadius$
  10 ������ $	 0,0&�$	 1,1&�.��� $	 #879ec0��$
 0Q�� $	 White��$
 1Q��� $
��Bold"�
�������$��30(�$��0,0,10,0q�   $ Black��$	 320Q�$ 5Q�$	 0.3Q�.��Barcode Setting�� -   $ BtnSave��$��1��.���� $ 
Horizontal=�.+��� $"��/Folder/PageUI/Icon/save.png��$��2q��� $��Center=�$ 15,0,0,0q�Save�� -   $ BtnUndo��$��2��.���� $ 
Horizontal=�.+��� $��3q�$%��/Folder/PageUI/Icon/restore.png���� $��Center=�$ 20,0,0,0q�Undo� $��1�����#DDDDDD.+�� $��1��$��4q� ��%�  '��       '�� �     7   '�� 3    �� " ��.���  � �� �	#dd222222��  � ��$	 5,0q���  � ��$ Center=���  � ��$ Center=���  � ��$ 24(��� " ��8   .���  � ��$ 24(���  � ��$
 Bold"���  � ��$ 0,5,5,5q���  � �� �#ECECEC��  � ��$
 0.7 q���  � ��
 �   ��� " ��.���  � ��$ Center=���  � ��$ 18(���  � ��$ Arial)���  � ��$ Light"���  � ��
 �   � �� $	��250 ��� �� .+�� $��0�� �� $	��250 ��� $��* ��� $	��550 �.+�� $��0��.+��� $
��Fill=�$%��/Folder/PageUI/Icon/Scanner.png��� $��1�� �� $��* ��� $��45 �.+��� -    ��Name$ txtLogs��$��0��$��0 0 10 0q�
���   �
�������" �� VerticalScrollBarVisibility$ Visible=����� *rxLog+�� -   $��btnLogClear��$��1��$��2,2,12,0q�.���� $��Center=�$ 2,0,0,0q�Clear� $��2��
������  �� ��  �� �� �� �� �� .+�� $��0��$��0��.+��� -	   $ 
btnSetting��$��0��$��0��$��2,2,2,2q�$��Center=�$��Center=�
�������$��16(�.���� $ 
Horizontal=�.+��� $��3q�$&�� /Folder/PageUI/Icon/Setting2.png���� $��Center=�$ 5,0,0,0q�Setting Barcode� $��0��$��1�� �� �� .+��� -
   $ btnOpen��$��0��$��0��$��2,2,2,2q�$��Center=�$��Center=�
�������$��16(�.���� $ 
Horizontal=�.+��� $"��/Folder/PageUI/Icon/open.png��$��3q��� $��Center=�$ 2,0,0,0q�Open�� -   $ btnClose��$��2��$��1��$��2,2,2,2q�$��Center=�$��Center=�
�������$��16(�.���� $ 
Horizontal=�.+��� $(��"/Folder/PageUI/Icon/disconnect.png��$��2q��� $��Center=�$ 2,0,0,0q�Close� $��0��$��2�� �� �� $��2* �.+��� $��Center=�$��16(�$��Center=�.��Select Bank�� -    ��Name$ cbSelectBank��$	��2,7q�$��Center=�$��Center=�$��1�� �� SelectedValuePath$ Content��.@��� $��Center=�$��Center=�
 � Tag$ 01��.��01�� $��Center=�$��Center=�$ 02��.��02�� $��Center=�$��Center=�$ 03��.��03�� $��Center=�$��Center=�$ 04��.��04�� $��Center=�$��Center=�$ 05��.��05�� $��Center=�$��Center=�$ 06��.��06�� $��Center=�$��Center=�$ 07��.��07�� $��Center=�$��Center=�$ 08��.��08�� $��Center=�$��Center=�$ 09��.��09�� $��Center=�$��Center=�$ 10��.��10� $��1��$��0��.+��� -   $ btnReadQrCode��$��0��$��0��$��2,2,50,2q�$��Center=�$��Center=�
�������$��16(�.���� $ 
Horizontal=�.+��� $��Center=�$ 2,0,0,0q�Read QR Code� $��1��$��1��.+��� -   $ 
btnTurning��$��0��$��0��$��2,2,50,2q�$��Center=�$��Center=�
�������$��16(�.���� $ 
Horizontal=�.+��� $��Center=�$ 2,0,0,0q�	Turning� $��1��$��2��.+��� -   $ 	btnForcus��$��0��$��0��$��2,2,50,2q�$��Center=�$��Center=�
�������$��16(�.���� $ 
Horizontal=�.+��� $��Center=�$ 2,0,0,0q�Forcus� $��1��$��4��.+��� -   $ 
btnRestore��$��0��$��0��$��	2,2,100,2q�$��Center=�$��Center=�
�������$��16(�.���� $ 
Horizontal=�.+��� $��3q�$%��/Folder/PageUI/Icon/restore.png���� $��Center=�$ 20,0,0,0q�	Restore� $��0��$��4��.+��� -   $ btnSave��$��0��$��0��$��	100,2,2,2q�$��Center=�$��Center=�
�������$��16(�.���� $ 
Horizontal=�.+��� $��3q�$"��/Folder/PageUI/Icon/save.png���� $��Center=�$ 20,0,0,0q�Save