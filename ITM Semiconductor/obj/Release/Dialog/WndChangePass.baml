   M S B A M L   `   `   `  ���� L  HITM Semiconductor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ITM_SemiconductorITM_Semiconductor  %    ITM_Semiconductor.WndChangePass   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     1localclr-namespace:ITM_Semiconductor     -     � WindowStartupLocation$  CenterScreen=� � WindowStyle$
 None=� � AllowsTransparency .  � Topmost . 
������  � Title$ WndChangePass��$	��600��$	��800��.��� -    � ColumnDefinitions �� $��* ��� $
��1.5* �.+��� -   $#��Folder/PageUI/Icon/close1.png�� K� ZIndex$ 1��$��1��   imgclose#��[  ��  �� CornerRadius$ 
 10 0 0 10������  �� 
StartPoint$	 0,0&�	 �� EndPoint$		 1,1&�.��� 
 � Color$
 #6787c0�� � Offset$ 0Q�� $
 #879ec8��$ 1Q�._�� .+��� .+��  � Points �    -1��� ;X�����#879ec8� ' �    �� �� ���� ��
�����#879ec8A� $��250 450 0 0q�$��40��$��40�����#879ec8A� $��
50 400 0 0q�$��40��$��40�����#879ec8�� $��Center=�.+��� $	��ITM��  	titleText#��[ �� $��Semiconductor��  
normalText#��[  J� Opacity$	 0.8Q�$�� 20 0q�$
��Bold"��� $��AUTOMATION TEAM��#��[ $	 0.8Q�$�� 20 0q��� ���#FFFFFF$��1��$ 	0,10,10,0��._��� $��Center=�.+��� $��Change Password��#��[ ���#6787c0 J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Color$ Black��   Opacity$	 0.2Q�   ShadowDepth$ 3Q�   	Direction$ 0Q���  �� Orientation$ 
Horizontal=�$
��0 20q�$��Center=�.+��� -    ��Name$ 	btManager��  ButtonCircle#��[ $	��110��.���� .+��� $&�� /Folder/PageUI/Icon/Manager2.png��$��50���� $��Manager��#��[ 
���   ��� -   $ 
btAutoteam��#��[ $	��110��.���� .+��� $&�� /Folder/PageUI/Icon/Autotem2.png��$��50���� $��	Auto Team��#��[ 
���   ��� $&�� Select a login permission above ��#��[ ���#878787$��	0 10 0 15q��� $��1q����#ACB0AF$
��70 7q�$ 5��._�� $	��7 9q� �� $
��auto ��� $��* �.+��� $��Folder/PageUI/Icon/id.png��$��21���� -    ��Name$ textId��$��Operator��
���   �  textHint#��[ �� $��1q����#ACB0AF$
��70 7q�$ 5��._�� $	��7 9q� �� $
��auto ��� $��* �.+��� $ ��Folder/PageUI/Icon/key.png��$��20��G� -    G�Name$ txtOldPassword��  textBox#��[ �� -   $ textOldPassword��$��Old Password��#��[ �� $��1q����#ACB0AF$
��70 7q�$ 5��._�� $	��7 9q� �� $
��auto ��� $��* �.+��� $ ��Folder/PageUI/Icon/key.png��$��20���� -	   $ textNewPassword��$��New Password��#��[ G� -
   $ txtNewPassword��#��[ �� $��1q����#ACB0AF$
��70 7q�$ 5��._�� $	��7 9q� �� $
��auto ��� $��* �.+��� $ ��Folder/PageUI/Icon/key.png��$��20���� -   $ textCfPassword��$��Confirm New Password��#��[ G� -   $ txtCfPassword��#��[ �� -   $ btChangePassword��$��Change Password��  
mainButton#��[ $
��Bold"��� -   $ btcancel��$��Cancel��#��[ $
��Bold"�