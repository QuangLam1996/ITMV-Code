   M S B A M L   `   `   `  ����L  HITM Semiconductor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ITM_SemiconductorITM_Semiconductor  5   	        ITM_Semiconductor.WndLogin   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:ITM_Semiconductor     5   	   -   5          � WindowStartupLocation$  CenterScreen=�5	   	    � WindowStyle$
 None=�6.    � AllowsTransparency .  � Topmost . 
������  � Title$ WndLogin��5
   	   $	��500��6   $	��800��6'   .��5      � -   5         � ColumnDefinitions 5   
   �� 5      $��* �6   6+   �� 5      $
��1.5* �6   6.   .+�5   
   �� -   5        $#��Folder/PageUI/Icon/close1.png��5       K� ZIndex$ 1��67   $��1��6H      imgclose#��[  6X   6z   �� 5   
    �� CornerRadius$ 
 10 0 0 10��6   ��5      �� 5       �� 
StartPoint$	 0,0&�6&   	 �� EndPoint$		 1,1&�67   .��5      � 
 � Color$
 #6787c0��6#    � Offset$ 0Q�63   � 5      $
 #879ec8��6#   $ 1Q�63   5      ._�5      � .+�5      �� .+�5      �  � Points �    -1��� ;X�����#879ec86:   � 5      ' �    �� �� ���� ��
�����#879ec86?   A� 5      $��250 450 0 0q�6   $��40��63   $��40��6>   ���#879ec86J   A� 5      $��
50 400 0 0q�6   $��40��62   $��40��6=   ���#879ec86I   5      �� 5       $��Center=�6   .+�5!      �� $	��ITM��6      	titleText#��[ 6+   �� 5"      $��Semiconductor��6      
normalText#��[ 65    J� Opacity$	 0.8Q�6Y   $�� 20 0q�6g   $
��Bold"�6v   �� 5#      $��AUTOMATION TEAM��6    #��[ 67   $	 0.8Q�6[   $�� 20 0q�6i   �� 5$      $=��7Click here when you really want to change your password��6    #��[ 6_   $	 0.8Q�6�   $�� 20 30q�6�   �� 5%      -   5         ��Name$ btChangePassword��5%      $��Change Password��67    
 Button#��[ 6Q    J� Effect 5&      3  -System.Windows.Media.Effects.DropShadowEffect  5'         Color$ Black��6/      	Direction$	 320Q�6=      ShadowDepth$ 5Q�6M      Opacity$	 0.3Q�6]      
BlurRadius$ 20Q�6k   5)      5*      5+      5,      �� 5-   
   ���#FFFFFF$��1��6&   $ 	0,10,10,0��66   ._�5/      �� $��Center=�6   .+�50      �� -   5        $$��/Folder/PageUI/Icon/close1.png��50      $ 1��6@   $��30��6Q   $��1��6]   #��[  6m   6�   �� 51      $��Sign in to App��6   #��[ 62   ���#6787c0 52        53      $ Black��6+   $	 0.2Q�69   $ 3Q�6G   $ 0Q�6W   55      �� 56       �� Orientation$ 
Horizontal=�6   $
��0 10q�66   $��Center=�6D   .+�57      �� -   5        $ 
btOperator��57   O     ButtonCircle#��[ 6   $	��110��6C   .��58      �� .+�59      �� $&�� /Folder/PageUI/Icon/Operator.png��6$   $��50��6N   6\   �� 5:      $��Operator��6(   #��[ 68   
���   �6q   5;      5<      �� 5=      -   5        $ 	btManager��5=   O   #��[ 6   $	��110��6C   .��5>      �� .+�5?      �� $&�� /Folder/PageUI/Icon/Manager2.png��6$   $��50��6N   6\   �� 5@      $��Manager��6(   #��[ 67   
���   �6p   5A      5B      �� 5C      -   5        $ 
btAutoteam��5C   O   #��[ 6   $	��110��6C   .��5D      �� .+�5E      �� $&�� /Folder/PageUI/Icon/Autotem2.png��6$   $��50��6N   6\   �� 5F      $��	Auto Team��6(   #��[ 69   
���   �6r   5G      5H      5J      �� 5K      $&�� Select a login permission above ��6   #��[ 6D   ���#878787$��0 0 0 15q�6}   �� 5L      $��1q�6   ���#ACB0AF$
��70 7q�6C   $ 5��6Q   ._�5M      � $	��7 9q�6    5N      �� 5O      $
��auto �6/   �� 5P      $��* �6/   .+�5R      �� $ ��/Folder/PageUI/Icon/id.png��6    $��21��6D   6R   �� 5S      -	   5         ��Name$ textId��5S   $   $��Operator��64   
���   �  textHint#��[ 6t   5V      5W      �� 5X      $��1q�6   ���#ACB0AF$
��70 1q�6C   $ 5��6Q   ._�5Y      � $	��7 9q�6    5Z      �� 5[      $
��auto �6/   �� 5\      $��* �6/   .+�5^      �� $!��/Folder/PageUI/Icon/key.png��6    $��20��6E   6S   G� 5`      -
   5         G�Name$ txtPassword��5`   &     textBox#��[ 6[   6|   5b      5c      �� 5d      -   5        $ btSignin��5d   O   $��Sign In��6     
mainButton#��[ 6+   $
��Bold"�6a   5f      �� 5g      -   5        $ btcancel��5g   N   $��Cancel��6   #��[ 6*   $
��Bold"�6`   5i      5j      5l      5m      5n      