   M S B A M L   `   `   `  ����J  FChat.Client.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Chat.Client.WPF.ControlsChat.Client.WPF.Controls  5   	   G(clr-namespace:Chat.Client.WPF.ViewModelsChat.Client.WPF.ViewModels  5   	   &     Chat.Client.WPF.Views.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;controls&clr-namespace:Chat.Client.WPF.Controls     5   	   ?
viewmodels(clr-namespace:Chat.Client.WPF.ViewModels     5   	   $	��400��5
   	   $	��500��5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)    � 	Resources 5         DefaultNavButtonBackground  DefaultNavButtonForeground 
 NavBar �� 
TargetType �� Property �� Value(   "Chat.Client.WPF.Controls.NavButton  	NavButton��%�  &	        &	      &	 <     &	     ��5   
   #6393ff��6<   ��5   
   	White��6:   �� 5   
   " ��6   .�5      ��  � ��
 ���� 6+   �� 5       � ��$ 0q�60   �� 5       � ��$ 0q�6(   �� 5       �@��SelectionMode$ Single=�6.   5      �� 5   
   "  6   .�5      ��  � ��$ 40��6&   �� 5       � ��$ 40��6'   �� 5       � ��$ 0q�6(   5      5      .��5      �  � ColumnDefinitions 5    
   �� 5!      $��60 �6   �� 5"      $��* �6   .+�5$   
   � $��0��6   ���#5c82ff � RowDefinitions 5%      �� 5&      $
��auto �6    �� 5'      $��* �6    .+�5)      ��  �� Orientation$ Vertical=�6   $��0��60   .+�5,      %   Chat.Client.WPF.Controls.Avatar     Fill �#f7d794
�������$��20(�5-   "   $��40��5.   "   $��
00 10 0 10q�5/   &   $��40��50   "   	   Text$	 P��51   "   �� 54      ���	#FF8BA6FF	
 �� X1$
 10��6*   	 �� X2$ 50��62   �� 56      #��[ 6   $��Bottom=�57      .@�58        #��[  6)   $��40��59   %   $��40��5:   %   #��[ 5;   %   $��0q�5<   %   $��0 10 0 0q�5>   %      Icon$ MessageText=�5?   %      Command 5=   %   �� *NavigateToChats+5?   %     5A      #��[  6)   $��40��5B   %   $��40��5C   %   $��0 10 0 0q�5D   %   #��[ 5F   %   $��0q�5G   %   $��Bottom=�5H   %   $	 Cog=�5I   %    5E   %   �� *NavigateToSettings+5I   %   5J      5K      �� 5M      $��Bottom=�6   $��Stretch=�5N      $��1��5O      $	��200��5P      .+�5Q      �  � Points/ �   ��� -1 �� Zb �� �� �� J� Opacity$	 0.2Q�5S      $��2��5T      $	��200��6)   ��5U      �� 5V       �� 
StartPoint$	 0,1&�6.    �� EndPoint$	 0,0&�6?   .��5W      �  � Color$ 	#FF8BA6FF��6+    � Offset$ 0Q�6=   � 5Y      $ 	#FF00FF33��6+   $ 1Q�6=   5Z      5\      � 5^      ) �    �� ���, �� �� ��$	 0.2Q�5`      $��2��5a      $	��200��6)   ��5b      �� 5c      $	 0,1&�6.   $	 0,0&�6?   .��5d      � $ 	#FF8BA6FF��6+   $ 0Q�6=   � 5f      $ 	#FFB318FF��6+   $ 1Q�6=   5h      5j      5l      5n      �� 5o   
   $��1��6   ��6+   �� *
MainPage+5p      5q      5r      