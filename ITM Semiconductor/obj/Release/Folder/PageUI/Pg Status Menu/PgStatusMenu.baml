   M S B A M L   `   `   `  ���� L  HITM Semiconductor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ITM_SemiconductorITM_Semiconductor  $    ITM_Semiconductor.PgStatusMenu   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1localclr-namespace:ITM_Semiconductor       N� Title$  PgStatusMenu��.��� 
������� � ColumnDefinitions �� �� $	��200 �.+�� $��0��$��0 5 0 0q� � RowDefinitions �� $��70 ��� $��2.25* �.+�� $��0��.+���  �� CornerRadius$
  10 ������  �� 
StartPoint$	 0,0&� �� EndPoint$	 1,1&�.���  � Color$ #879ec0�� � Offset$ 0Q�� $ White��$ 1Q��� $
��Bold"�$��Center=�
�������$��30(�$��0,0,10,0q� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect  	   Color$	 Black��
   	Direction$	
 320Q�   ShadowDepth$ 5Q�   Opacity$	 0.3Q�.��User Log Monitor�� $��1��$��20(�.@��� $��	Event Log��.���  �� $��9* ��� $��1* �.+�&   System.Windows.Controls.DataGrid  -     Name$ dgridAlarms��$��0��$��0��$��16(�   CanUserAddRows .     CanUserDeleteRows .     SelectionMode$ Single=�   AutoGenerateColumns .  $��Center=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ No��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Id+  $ 	Date Time��$	 200  �� *CreatedTime+  $ 
Jam String��$ 1*  �� *	Message+A� $��1�� A� Columns$ 7�� � 	Resources  �� 
TargetType   mButton �� BasedOn �� Property �� Value��%Q   '��       7   �� " ��8   .���  � ��$ 4,4,0,4q���  � ��$ 20(�.+��� -    ��Name$ btAlarmFirst��.��
|< First�� -   $ btAlarmPrePage��.��<< Pre Page�� -   $ btAlarmPrevious��.��
< Previous�� -   $ btAlarmCurrent��
�������
�������.��	2449/2449�� -   $ btAlarmNext��.��Next >�� -   $ btAlarmNextPage��.��Next Page >>�� -   $ btAlarmLast��.��	Last >|�� $��User Log��.���  �� $��9* ��� $��1* �.+�  -	   $ 	dgridLogs��$��0��$��0��$��16(� .   .  $ Single=� .  $��Center=�   $ No��$ 50  �� *Id+  $ 	Date Time��$	 200  �� *CreatedTime+  $ User ID��$	 160  �� *
Username+  $ Message��$ 1*  �� *	Message+A� $��1��$ 7�� :  4System.Windows.Controls.Primitives.DatePickerTextBox �� Path �� StringFormat �� RelativeSource  �� AncestorType(  "System.Windows.Controls.DatePicker��%�   '��       7   '  C     �� " ��8   .���  � ��$ 4,4,0,4q���  � ��$ 24(��� "  .���  � �� �� .R��� ���� $ SelectedDate�$ 
yyyy-MM-dd�� �� "   .+��� -
   $ btLogPrevious��.��
Previous <  -   !  Name$! 	dtLogDate��$��2,0,2,0q�$��Center=�$��Center=�$��22(�$��Light"�"   FirstDayOfWeek$" Monday=��� -   $ 	btLogNext��.��> Next�� -   $ 
btLogToday��.��Today�� -   $ btLogPrePage��.��
Pre Page�� -   $ btLogCurrent��
�������
�������.��0�� -   $ btLogNextPage��.��	Next Page� $��1��   sButton��%�  '��       '�� �     7  '��     �� " ��.���  � �� �	#dd222222��  � ��$	 5,0q���  � ��$ Center=���  � ��$ Center=���  � ��$ 24(��� " ��8   .���  � ��$ 24(���  � ��$
 Bold"���  � ��$ 0,5,5,5q���  � �� �#BDB76B��  � ��$ 0q��� " ��.���  � ��$ Center=���  � ��$ 22(���  � ��$
 Bold"���  � ��
 �����.+��� ���� $	 0,0&�$	 1,1&�.��� $ #6787c0��$ 0Q�� $ #879ec8��$ 1Q�._�� .+��� .+�� # � Points# �    -1��� ����!
���#879ec8� '# �    �� �� ���� ��
�����#879ec8A� $��250 450 0 0q�$��40��$��40�����#879ec8A� $��
50 400 0 0q�$��40��$��40�����#879ec8�� $��Bottom=�.+��� $	��ITM��  	titleText#��[ �� $��Semiconductor��  
normalText#��[ $ J� Opacity$	$ 0.8Q�$�� 20 0q�$
��Bold"��� $��AUTOMATION TEAM��#��[ $	$ 0.8Q�$�� 20 0q��� % �� Orientation$% Vertical=�.+��� $
��Bold"�$��0 20 0 0q�
�������$��27(�$��Center=�   $	 Black��$	
 320Q�$ 5Q�$	 0.3Q�.��System Menu�� $% Vertical=�$��Center=�$��	0 100 0 0q�.+��� -   $ 
btSetting1��$��60��$	��215��  ssButton#��[ $��0 5 5 5q�.���� $% 
Horizontal=�.+��� $'��!/Folder/PageUI/Icon/button on.png��$��Center=�$��0 5 0 0q��� & �� Padding$& 10q�$��Status��
���   �$��10q�$��Center=�