   M S B A M L   `   `   `  ����L  HITM Semiconductor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ITM_SemiconductorITM_Semiconductor  5      )    #ITM_Semiconductor.PgMechanicalMenu5   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:ITM_Semiconductor     5        N� Title$  PgMechanicalMenu5��5	      .��5      � 
������� � ColumnDefinitions 5   
   �� 5      6!   �� 5      $	��200 �6   6-   .+�5   
   � $��1��6    � 	Resources 5       �� 
TargetType �� Property �� Value   sButton �� BasedOn��%�  '��       '�� �     7   '�� �    �� 5      " ��6   .�5      ��  � �� �	#dd22222263   �� 5       � ��$	 5,0q�60   �� 5       � ��$ Center=�6A   �� 5       � ��$ Center=�6C   �� 5       � ��$ 24(�61   5      �� 5      " ��6   8   .�5      ��  � ��$ 24(�61   �� 5       � ��$
 Bold"�63   �� 5       � ��$ 0,5,5,5q�6/   �� 5       � ��
 �����63   �� 5        � ��$ 0q�68   5"      �� 5#      " ��6   .�5$      ��  � ��$ Center=�6:   �� 5%       � ��$ 22(�61   �� 5&       � ��$
 Bold"�63   �� 5'       � ��
 �   �63   5)      5+      .+�5,      �� ��5-      �� 5.       �� 
StartPoint$	 0,0&�6*    �� EndPoint$	 1,1&�6;   .��5/      � 	 � Color$	 #6787c0��6'   
 � Offset$
 0Q�67   � 50      $	 #879ec8��6'   $
 1Q�67   51      ._�54      � .+�55      �� .+�56      �  � Points �    -1��� ����!
���#879ec86=   � 57      ' �    �� �� ���� ��
�����#879ec86C   A� 58      $��250 450 0 0q�6"   $��40��67   $��40��6B   ���#879ec86N   A� 59      $��
50 400 0 0q�6"   $��40��66   $��40��6A   ���#879ec86M   5;      �� 5<      $��Bottom=�6!   .+�5=      �� $	��ITM��6$     	titleText#��[ 6/   �� 5>      $��Semiconductor��6$     
normalText#��[ 69    J� Opacity$	 0.8Q�6]   $�� 20 0q�6k   $
��Bold"�6z   �� 5?      $��AUTOMATION TEAM��6$   #��[ 6;   $	 0.8Q�6_   $�� 20 0q�6m   5@      �� 5A       �� Orientation$ Vertical=�6!   .+�5B      �� $
��Bold"�6!   $��0 20 0 0q�63   
�������$��27(�6Y   $��Center=�6g    J� Effect 5C      3  -System.Windows.Media.Effects.DropShadowEffect  5D   "      Color$ Black��63      	Direction$	 320Q�6A      ShadowDepth$ 5Q�6Q      Opacity$	 0.3Q�6a   .��5E   ,   System Menu5F      5G      �� 5H      $ Vertical=�6!   $��Center=�68   $��	0 100 0 0q�6U   .+�5K      �� -   5         ��Name$ 
btSetting1��5K   !   $��60��66   $	��215��6B   .��5L      �� $ 
Horizontal=�6)   .+�5N   "   �� $(��"/Folder/PageUI/Icon/button off.png��6(   $��Center=�6T   $��0 5 0 0q�6p   6�   �� 5O   "    �� Padding$ 10q�6,   $��
MES       ��69   $��10q�6L   $��Center=�6X   5P      5Q      �� 5S      -   5        $ 
btSetting2��5S   !   $��60��65   $	��215��6A     ssButton#��[ 6M   $��0 5 5 5q�6o   $��0q�6�   .��5T      �� $ 
Horizontal=�6)   $
��Left=�6B   .+�5V   "   �� $'��!/Folder/PageUI/Icon/button on.png��6(   $��Center=�6S   $��0 5 0 0q�6o   6�   �� 5X   "   $ 10q�6,   $��Barcode ��69   
���   �$��10q�6\   $��Center=�6h   5Y      5Z      �� 5^      -   5        $ 
btSetting3��5^   !   $��60��66   $	��215��6B   .��5_      �� $ 
Horizontal=�6)   .+�5a   "   �� $(��"/Folder/PageUI/Icon/button off.png��6(   $��Center=�6T   $��0 5 0 0q�6p   6�   �� 5b   "   $ 10q�6,   $��
Device    ��69   $��10q�6L   $��Center=�6X   5c      5d      �� 5j      -   5        $ 
btSetting4��5j   !   $��60��66   $	��215��6B   .��5k      �� $ 
Horizontal=�6)   .+�5m   "   �� $(��"/Folder/PageUI/Icon/button off.png��6(   $��Center=�6T   $��0 5 0 0q�6p   6�   �� 5n   "   $ 10q�6,   $��FTP         ��69   $��10q�6N   $��Center=�6Z   5o      5p      5r      5s      5t      5u      � 5v   
   $��0��6    � RowDefinitions 5w      �� 5x      $��60 �6    6.   �� 5y      6"   .+�5{      � $��0��6    5|      �� 5}      6)   �� 5~      $	��160 �6'   65   �� 5      $	��160 �6'   65    5�      ��%  '��       7   '��     �� 5�      " ��6   8   .�5�      ��  � ��$ 24(�65   �� 5�       � ��$
 Bold"�67   �� 5�       � ��$ 0,5,5,5q�63   �� 5�       � �� �#6787c067   �� 5�       � ��$	 0.7q�6<   �� 5�       � ��
 �   �68   5�      �� 5�      " ��6   .�5�      ��  � ��$ Center=�6>   �� 5�       � ��$ 23(�65   �� 5�       � ��$ Arial)�67   �� 5�       � ��$ Normal"�67   �� 5�       � ��
 �����67   5�      5�      .+�5�      ��  �� CornerRadius$
  10 ��6   ��5�      �� 5�      $	 0,0&�6.   $	 1,1&�6?   .��5�      � $	 #879ec0��6+   $
 0Q�6;   � 5�      $	 White��6+   $
 1Q�69   5�      5�      �� 5�      $
��Bold"�6   
�������$��30(�6>   $��0,0,10,0q�6M    5�        5�      $ Black��6+   $	 320Q�69   $ 5Q�6I   $	 0.3Q�6Y   .��5�   $   Barcode Setting5�      �� 5�      -   5        $ BtnSave��5�   )   $��1��6   .��5�      �� $ 
Horizontal=�6!   .+�5�      �� $"��/Folder/PageUI/Icon/save.png��6    $��2q�6F   �� 5�      $��Center=�6$   $ 15,0,0,0q�6?   Save6X   5�      5�      �� 5�      -   5        $ BtnUndo��5�   )   $��2��6   .��5�      �� $ 
Horizontal=�6!   .+�5�      �� $��3q�6    $%��/Folder/PageUI/Icon/restore.png��6+   �� 5�      $��Center=�6&   $ 20,0,0,0q�6A   Undo6[   5�      5�      5�      � 5�      $��1��6   ���#DDDDDD.+�5�      � $��1��6   $��4q�6$    5�      ��%  '��       '�� �     7   '�� �    �� 5�      " ��6    .�5�      ��  � �� �	#dd2222226;   �� 5�       � ��$	 5,0q�68   �� 5�       � ��$ Center=�6I   �� 5�       � ��$ Center=�6K   �� 5�       � ��$ 24(�69   5�      �� 5�      " ��6    8   .�5�      ��  � ��$ 24(�69   �� 5�       � ��$
 Bold"�6;   �� 5�       � ��$ 0,5,5,5q�67   �� 5�       � �� �#ECECEC6;   �� 5�       � ��$
 0.7 q�6@   �� 5�       � ��
 �   �6<   5�      �� 5�      " ��6    .�5�      ��  � ��$ Center=�6B   �� 5�       � ��$ 18(�69   �� 5�       � ��$ Arial)�6;   �� 5�       � ��$ Light"�6;   �� 5�       � ��
 �   �6;   5�      5�       5�      �� 5�      $	��250 �6(   67   �� 5�      6*   �� 5�      6*   .+�5�      � $��0��6    5�      �� 5�      $��0 �6/   6;   �� 5�      $��* �6/   6;   �� 5�      $	��600 �6/   6=   .+�5�      � $��1��6    5�      �� 5�   "   $��* �60   �� 5�   "   $��45 �60   .+�5�      �� -   5         ��Name$ txtLogs��5�   E   $��0��6&   $��0 0 10 0q�63   
���   �
�������" �� VerticalScrollBarVisibility$ Visible=�5�      ��5�   V   �� *rxLog+5�   D   �� 5�      -   5        $��btnLogClear��5�   2   $��1��6%   $��2,2,12,0q�6E   .��5   "   �� $��Center=�6-   $ 2,0,0,0q�6H   Clear6a   5     5     � 5     $��2��6   
������  5     �� 5  "   $��50 �60   6>   �� 5  "   62   .+�5     � $��0��6#   .+�5	  "   �� $��0��6)   $��4q�66   
�����_�._�5
  &   �� 
�������$
��Bold"�6D   $��20(�6V   $��Center=�5  1   $��Center=�6N   
Connection6u   5  #   5     � 5     $��1��6#    5  "   �� 5  &   69   �� 5  &   69    5  "   �� 5  &   66   �� 5  &   66   �� 5  &   66   �� 5  &   66   .+�5  "   � $��0��6'   $��0��67    5  &   �� 5  *   $��60 �6;   6H   �� 5  *   6=   .+�5  &   �� .��6,   IP60   �� 5   &   -	   5        $ txbIp��5   >   $��1��6.   $��Center=�6M   $��Center=�6q   $��25(�6�   6�   5!  #   � 5"  "   $��0��6'   $��1��67    5#  &   �� 5$  *   $��60 �6;   6H   �� 5%  *   6=   .+�5'  &   �� .��6,   Port62   �� 5(  &   -
   5        $ txbPort��5(  >   $��1��6.   $��Center=�6P   $��Center=�6t   $��25(�6�   6�   5)  #   � 5*  "   $��0��6'   $��2��67    5+  &   �� 5-  *   6=   �� 5.  *   6=   .+�51  &   �� -   5        $ btnOpen��51  -   $��0��6>   $��0��6N   $��2,2,2,2q�6[   $��Center=�6l   $��Center=�6�   
�������$��16(�6�   .��53  *   �� $ 
Horizontal=�65   .+�54  .   �� $"��/Folder/PageUI/Icon/open.png��64   $��3q�6Z   �� 55  .   $��Center=�6:   $ 2,0,0,0q�6U   Open6m   56  +   57  '   �� 58  &   -   5        $ btnClose��58  -   $��2��6?   $��1��6O   $��2,2,2,2q�6\   $��Center=�6m   $��Center=�6�   
�������$��16(�6�   .��5:  *   �� $ 
Horizontal=�65   .+�5;  .   �� $(��"/Folder/PageUI/Icon/disconnect.png��64   $��2q�6`   �� 5<  .   $��Center=�6:   $ 2,0,0,0q�6U   Close6n   5=  +   5>  '   5?  #   � 5@  "   $��0��6'   $��3��67    5A  &   �� 5B  *   6=   �� 5C  *   $��2* �6;   6H   .+�5E  &   �� $��Center=�6,   $��16(�6P   $��Center=�6^   .��6�   Select Bank6�   �� 5F  &   -   5         ��Name$ cbSelectBank��5F  �   $	��2,7q�6/   $��Center=�6<   $��Center=�6`   $��1��6�    �� SelectedValuePath$ Content��6�   .@�5G  *   �� $��Center=�67   $��Center=�6[   
 � Tag$ 01��6}   .��6�   016�   �� 5H  *   $��Center=�67   $��Center=�6[   $ 02��6}   .��6�   026�   �� 5I  *   $��Center=�67   $��Center=�6[   $ 03��6}   .��6�   036�   �� 5J  *   $��Center=�67   $��Center=�6[   $ 04��6}   .��6�   046�   �� 5K  *   $��Center=�67   $��Center=�6[   $ 05��6}   .��6�   056�   �� 5L  *   $��Center=�67   $��Center=�6[   $ 06��6}   .��6�   066�   �� 5M  *   $��Center=�67   $��Center=�6[   $ 07��6}   .��6�   076�   �� 5N  *   $��Center=�67   $��Center=�6[   $ 08��6}   .��6�   086�   �� 5O  *   $��Center=�67   $��Center=�6[   $ 09��6}   .��6�   096�   �� 5P  *   $��Center=�67   $��Center=�6[   $ 10��6}   .��6�   106�   5Q  '   5R  #   � 5S  "   $��1��6'   $��0��67   .+�5T  &   �� -   5        $ btnReadQrCode��5T  -   $��0��6E   $��0��6U   $��2,2,50,2q�6b   $��Center=�6t   $��Center=�6�   
�������$��16(�6�   .��5V  *   �� $ 
Horizontal=�65   .+�5W  .   �� $��Center=�6:   $ 2,0,0,0q�6U   Read QR Code6u   5X  +   5Y  '   5Z  #   � 5[  "   $��1��6'   $��1��67   .+�5\  &   �� -   5        $ 
btnTurning��5\  -   $��0��6A   $��0��6Q   $��2,2,50,2q�6^   $��Center=�6p   $��Center=�6�   
�������$��16(�6�   .��5^  *   �� $ 
Horizontal=�65   .+�5_  .   �� $��Center=�6:   $ 2,0,0,0q�6U   	Turning6p   5`  +   5a  '   5b  #   � 5c  "   $��1��6'   $��2��67   .+�5d  &   �� -   5        $ 	btnForcus��5d  -   $��0��6@   $��0��6P   $��2,2,50,2q�6]   $��Center=�6o   $��Center=�6�   
�������$��16(�6�   .��5f  *   �� $ 
Horizontal=�65   .+�5g  .   �� $��Center=�6:   $ 2,0,0,0q�6U   Forcus6o   5h  +   5i  '   5j  #   5l     5p     5q     5r     5w     5x     5{     5|     