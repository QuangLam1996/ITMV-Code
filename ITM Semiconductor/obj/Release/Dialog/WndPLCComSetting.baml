   M S B A M L   `   `   `  ���� L  HITM Semiconductor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ITM_SemiconductorITM_Semiconductor  (    "ITM_Semiconductor.WndPLCComSetting   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     1localclr-namespace:ITM_Semiconductor       � WindowStartupLocation$  CenterScreen=� � Title$ PLC Com Setting��$	��320��$	��420�� � Topmost . .���  � 	Resources  �� 
TargetType �� Property �� Value   sButton �� BasedOn��%�  '��       '�� r     7   '��     '�� A    �� " ��.���  � ��$ Center=���  � ��$ Right=���  � ��$ 10,0,5,0q���  � ��$ 16(��� " ��8   .���  � ��$ 20(���  � ��$
 Bold"���  � ��$ 0,5,5,5q���  � �� �#6787c0��  � ��$ 0q���  � ��
 ������� " ��.���  � ��$ 
10,2,100,2q��� " ��.���  � ��$ 
10,2,100,2q� � ColumnDefinitions �� $��1* ��� $��2* �	 � RowDefinitions	 �� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��50 �.+��� $��0��$��0��	Port Name�� $��1��$��0��
Baudrate�� $��2��$��0��	Data Bits�� $��3��$��0��	Stop Bits�� $��4��$��0��Parity�� $��5��$��0��	Address�� $��6��$��0��	Handshake�� -   
 ��Name$
 
cbPortName��$��0��$��1�� �� SelectedValuePath$ Content���� -   $
 
cbBaudrate��$��1��$��1��$ Content��.@��� 
 � Tag$
 1200��.��1200�� $
 2400��.��2400�� $
 4800��.��4800�� $
 9600��.��9600�� $ 19200��.��19200�� $ 28800��.��28800�� $ 57600��.��57600�� $ 115200��.��115200�� $ 230400��.��230400�� $ 460800��.��460800�� $ 921600��.��921600�� -   $
 
cbDataBits��$��2��$��1��$ Content��.@��� $ 7��.��7�� $ 8��.��8�� $ 9��.��9�� -   $
 
cbStopBits��$��3��$��1��$ Content��.@��� $ 0��.��None�� $ 1��.��One�� $ 2��.��Two�� -   $
 cbParity��$��4��$��1��$ Content��.@��� $ 0��.��None�� $ 1��.��Even�� $ 2��.��Odd�� $ 3��.��Mark�� $ 4��.��Space�� -    ��Name$ txtAddressMB��$��5��$��1��$��Center=�$��Center=�.��1�� -   $
 cbHandshake��$��6��$��1��$ Content��.@��� $ 0��.��None�� $ 1��.��RequestToSend�� $ 2��.��RequestToSendXOnXOff�� $ 3��.��	XOnXOffA� $��7��$��0��$��2�� A� Columns$ 2��$��100,0,100,0q�$��50��.+��� -    ��Name$
 btOk��.��OK�� -	   $ btCancel��.��CANCEL