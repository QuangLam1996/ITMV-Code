   M S B A M L   `   `   `  ���� 
  KeyPad    KeyPad.Keypad   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$  Touch Keyboard Number��$	��400��$	��350�� � ShowInTaskbar .   � Topmost .  � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=� � 	Resources 3  -System.Windows.Media.Effects.DropShadowEffect   DropShadowEffect   ShadowDepth   	Direction   
BlurRadius	 �� 
TargetType
 �� Property �� Value  IPhoneSteelBackground �� EndPoint �� 
StartPoint � Color � Offset  sButton �� BasedOn �� Angle��%�  &	        '��      7   &	 [     '�� �     7  7     $ 1Q�$	 -90Q�$ 3Q��� "	 ��.��� 
 � ��$ 22(��� 
 �@J�Effect8   �� $ 	0.5,1.272&�$ 0.5,0&�.��� $ 	#FF9097A3��$ 0Q�� $ 	#FF444D5B��$ 1Q��� "	 ��8   .�-   �� 
 � ��$ 4q��� 
 �@J�Effect8  �� 
 � ��$ 25(��� 
 � ��$
 Bold"��� 
 � ��$ 0,5,5,5q��� 
 � �� �#879ec8�� 
 � ��$ 0q��� 
 � ��
 ������� 
 � z� �� $ 0.00000000001Q�.��� #���   � RowDefinitions �� �� �� �� ��  � ColumnDefinitions �� �� �� �� .+��� -    ��Name$ button7��$��1��$��0��
�������$��NUMPAD7��$��7���� -   $ button8��$��1��$��1��$��NUMPAD8��$��8���� -   $ button9��$��1��$��2��$��NUMPAD9��$��9���� -   $ buttonBackspace��$��1��$��3��$
��BACK��.��F�  �� Stretch$
 Fill=�$��36��$��24�������1!�_\ќ�� ��~�4� ��S�|�� Lz~��} ĩ^��J� #4Ѵ1| ,�Ć� ���ԟ� ��Q�v� �@��ǭp@�dC!|��Dt� l�1P�Y!��:�p@Q\"z ��:�p@Sln_ ��:�p@$t�\R#$t��5:�$t�Qln_ ���\"z ֨��Y!֨���O!֨�bW!��bW!�kQ�bW!��:�p@S�bW!2U0*��p@��M!��:�p@�Y!��:�p@���	#FF333333�� -   $ button4��$��2��$��0��$��NUMPAD4��$��4���� -   $ button5��$��2��$��1��$��NUMPAD5��$��5���� -	   $ button6��$��2��$��2��$��NUMPAD6��$��6���� -
   $ button1��$��3��$��0��$��NUMPAD1��$��1���� -   $ button2��$��3��$��1��$��NUMPAD2��$��2���� -   $ button3��$��3��$��2��$��NUMPAD3��$��3���� -   $ buttonEnter��$��2��$��3��$��3��$��RETURN��$��Enter���� -   $ button0��$��4��$��1��$��NUMPAD0��$��0���� -   $ buttonMinus��$��4��$��0��$��MINUS��$��-���� -   $ buttonDecimal��$��4��$��2��$��DECIMAL��$��.���� $��0��$��3��$��Center=�$��Right=�$��30(�
�������$
��Bold"�$��4q����� *Result+�� -   $ 	buttonEsc��$��0��$��3��$	��ESC��$	��Esc��