   M S B A M L   `   `   `  ���� L  HITM Semiconductor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ITM_SemiconductorITM_Semiconductor      ITM_Semiconductor.PgMenu   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1localclr-namespace:ITM_Semiconductor        
Page_Style#��[    N� Title$  PgMenu��.��� 
�������$��0,0,0,0q� � RowDefinitions �� $��20 ��� $
��100* ��� $��20 ��� $
��500* �.+�� $��0������  �� 
StartPoint$	 0,0&� �� EndPoint$	 0,1&�.���  � Color$ White�� � Offset$ 0Q�� $ #879ec0��$ 1Q�� $��1�����#879ec0 � ColumnDefinitions �� $��3* ��� $��5* � � 	Resources  �� 
TargetType	 �� Property
 �� Value  sButton �� BasedOn��%�  '��       '�� �     7  '��     �� " ��.��� 	 � ��
 �	#dd222222�� 	 � ��$	
 5,0q��� 	 � ��$
 Center=��� 	 � ��$
 Center=��� 	 � ��$
 24(��� " ��8   .��� 	 � ��$
 24(��� 	 � ��$

 Bold"��� 	 � ��$
 0,5,5,5q��� 	 � ��
 �#6787c0�� 	 � ��$
 0q��� " ��.��� 	 � ��$
 Center=��� 	 � ��$
 22(��� 	 � ��$

 Bold"��� 	 � ��

 �����.+�� $��0��.+��� -    ��Name$ txtMenu��$
��Bold"�
�������$��65(�$��0,0,10,0q� J� Effect 3  -System.Windows.Media.Effects.DropShadowEffect     Color$ Black��   	Direction$	 320Q�   ShadowDepth$ 5Q�   Opacity$	 0.3Q�.��	Main Menu� $��1��.+��� 
������� �� Padding$ 15q�$��	0 0 50 10q� �� CornerRadius$ 15��._�� .+���  �� Orientation$ 
Horizontal=�$
��Left=�.+��� $��70��$��70��$��	15 0 15 0q�$ 30��._��� -    ��Name$ myImage��$
��Fill=�$��Icon/avatar.png���� $��Center=�.+��� -   $ lblMode��$
��Left=�$��Log in��$��32(����#8F8F8F�� -   $ lblCurrentTime��$��Log in - Time��$��20(����#9C9C9C�� $ 
Horizontal=�$��Right=�$��0 0 20 0q�.+��� -    ��Name$ btLogin��$��60��.���� $ 
Horizontal=�.+��� $��Icon/login.png��$��10q��� $��Log in��$��10q��� -   $ btLogout��$��60��.���� $ 
Horizontal=�.+��� $��Icon/logout.png��$��10q��� $��Log out��$��10q�� $��2������ $	 0,0&�$	 0,1&�.��� $ White��$ 1Q�� $ #879ec0��$ 0Q�� $��3��.+��� $
  10 �����#EEEEEE._�� .+��� .+��� $��Design by Hiiep�����#00868B �� Right$ 10�� �� Bottom$ 10��A�  A� Columns$ 0�� A� Rows$ 4��$��100,0,100,0q�$	��480�� ��%a  '��       7  '�� �     �� " ��8   .��� 	 � ��
 �	#dd222222�� 	 � ��
 �#dddddd�� 	 � ��$
 0q��� 	 � ��$
 30(��� 	 � ��$

 Bold"��� 	 � ��$
 14q��� 	 � ��$
 Stretch=��� 	 � ��$
 Center=��� 	 � ��
 �#6787c0�� " ��.��� 	 � ��$
 25(��� 	 � ��$

 Bold"��� 	 � ��

 �����.+��� -   $ 
btTeaching��   $ Black��$	 320Q�$ 5Q�$	 0.6Q�   
BlurRadius$ 20Q�.���� $ 
Horizontal=�.+��� $��40,0,0,0q�$��Icon/menu-grid.png��   $ 7Q�$ Black��$	 0.2Q�$	 315Q��� $��Center=� �� Padding$ 20,0,0,0q�   $ 5Q�$ Black��$	 0.2Q�$	 315Q�Teaching Menu�� -   $ btMechanical��   $ Black��$	 320Q�$ 5Q�$	 0.6Q�$ 20Q�.���� $ 
Horizontal=�.+��� $��40,0,0,0q�$��Icon/menu-mechanic.png��   $ 7Q�$ Black��$	 0.2Q�$	 315Q��� $��Center=�$ 20,0,0,0q�   $ 7Q�$ Black��$	 0.2Q�$	 315Q�Mechanical Menu�� -	   $ btSystem��   $ Black��$	 320Q�$ 5Q�$	 0.6Q�$ 20Q�.���� $ 
Horizontal=�.+��� $��40,0,0,0q�$��Icon/menu-system.png��   $ 7Q�$ Black��$	 0.2Q�$	 315Q��� $��Center=�$ 20,0,0,0q�   $ 7Q�$ Black��$	 0.2Q�$	 315Q�System Menu�� -
   $ btManual��   $ Black��$	 320Q�$ 5Q�$	 0.6Q�$ 20Q�.���� $ 
Horizontal=�.+��� $��40,0,0,0q�$��Icon/menu-manual.png��   $ 7Q�$ Black��$	 0.2Q�$	 315Q��� $��Center=�$ 20,0,0,0q�   $ 7Q�$ Black��$	 0.2Q�$	 315Q�Manual Operation�� -   $ btStatus��   $ Black��$	 320Q�$ 5Q�$	 0.6Q�$ 20Q�.���� $ 
Horizontal=�.+��� $��40,0,0,0q�$��Icon/Status.png��   $ 7Q�$ Black��$	 0.2Q�$	 315Q��� $��Center=�$ 20,0,0,0q�   $ 7Q�$ Black��$	 0.2Q�$	 315Q�Status Menu�� -   $ btModel��   $ Black��$	 320Q�$ 5Q�$	 0.6Q�$ 20Q�.���� $ 
Horizontal=�.+��� $��40,0,0,0q�$��Icon/menu-save.png��   $ 7Q�$ Black��$	 0.2Q�$	 315Q��� $��Center=�$ 20,0,0,0q�   $ 7Q�$ Black��$	 0.2Q�$	 315Q�Model�� -   $ btSuperUser��   $ Black��$	 320Q�$ 5Q�$	 0.6Q�$ 20Q�.���� $ 
Horizontal=�.+��� $��40,0,0,0q�$��Icon/menu-admin.png��   $ 7Q�$ Black��$	 0.2Q�$	 315Q��� $��Center=�$ 20,0,0,0q�   $ 7Q�$ Black��$	 0.2Q�$	 315Q�Super User Menu�� -   $ btAssignMenu��   $ Black��$	 320Q�$ 5Q�$	 0.6Q�$ 20Q�.���� $ 
Horizontal=�.+��� $��40,0,0,0q�$��Icon/team.png��   $ 7Q�$ Black��$	 0.2Q�$	 315Q��� $��Center=�$ 20,0,0,0q�   $ 7Q�$ Black��$	 0.2Q�$	 315Q�Assign Menu