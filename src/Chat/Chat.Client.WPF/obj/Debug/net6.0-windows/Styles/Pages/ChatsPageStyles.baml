   M S B A M L   `   `   `  ����J  FChat.Client.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Chat.Client.WPF.ConvertersChat.Client.WPF.Converters  5      a5clr-namespace:Chat.Client.WPF.ViewModels.UserControls'Chat.Client.WPF.ViewModels.UserControls  5      U QSystem.Runtime, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a7,clr-namespace:System;assembly=System.RuntimeSystem 5      C&clr-namespace:Chat.Client.WPF.ControlsChat.Client.WPF.Controls  5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089?   9Chat.Client.WPF.Converters.HashToAvatarForeColorConverter   ForeColorConverter?  9Chat.Client.WPF.Converters.HashToAvatarBackColorConverter  BackColorConverter=  7Chat.Client.WPF.Converters.OnlineStatusToColorConverter  OnlineStatusConverter  DefaultForegroundBrush  SelectedChatBackGradient  �� 
StartPoint �� EndPoint � Color � Offset  AvatarFontSize �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius	 ��Name
 � Triggers :� RoutedEvent �� 
TargetName �� TargetProperty W� From	 W� To g� Duration W� EasingFunction0  *System.Windows.Media.Animation.QuarticEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode �� Triggers �� Triggers   ListBoxItemEmergingAnimation  MultilineTextBox  DefaultListBox 	 SmoothListBox �� BasedOn 
 MessageStyle&    Chat.Client.WPF.Controls.Message   IsFromMe   Text   Time   TimeForeground   SecondaryBackground  ChatListBoxItemStyle �� Padding � ColumnDefinitions � RowDefinitions%   Chat.Client.WPF.Controls.Avatar   Fill �� 	Converter    Text! P� Property" P� Value# �� 
TargetName  ScrollBarTrackThumb$ �Name% ��Name& ]�Name' ]� Thumb( n�Name) ]� IncreaseRepeatButton* ��Name+ J� Opacity, ]� DecreaseRepeatButton- P� 
SourceName  ButtonSelectBrush  	DarkBrush. �� Angle�� 5      F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     7
converters(clr-namespace:Chat.Client.WPF.Converters  5      <vm5clr-namespace:Chat.Client.WPF.ViewModels.UserControls  5      4sys,clr-namespace:System;assembly=System.Runtime  5      3controls&clr-namespace:Chat.Client.WPF.Controls  5      :x,http://schemas.microsoft.com/winfx/2006/xaml     5      %�  &	        &	      &	 &     &	 9     &	 X     &	 !    '�� ;    7  &	 �    &	 �    &	     &		 ^    7  &	
 �    &	 �	    7  7  7  7   7  7  &	 �    '�� J       5      60     5      60     5	   
   62   ��5      #8591b6��64   �� 5      $  0.2,0&�6;   $ 0.5,1&�6N   .��5   
   � $ #5c82ff��6   $ 0Q�6'   � 5   
   $ #677fed��6   $
 0.45Q�6'   � 5   
   $ #4b9ad8��6   $ 1Q�6'   5      X�5      18Q�6,   �� 5      " ��6   .�5   
   ��  �@�Cursor$
 Hand��6#   �� 5   
    � ��$ 0q�6,   �� 5   
    � �� 5      �� 5      " ��6"   .R�5      �� $��Part_Border��6   8��  $ 16��5      ._�5      �� $	 PART_contentpresenter��6/   $��Center=�6N   $��Center=�6k   
 5       :� 5!      $ Border.MouseEnter��6+   .[�5"   "   �� .��5#   &   �� $ Part_Border��61   .��5$   *   W� $ Opacity�6:    15%   6    05&   6   $ 0:0:0.3D�5'   )    5(   .     5)   2   $ 	EaseInOut=�6>   5+   +   5,   '   5-   #   5.      :� 5/      $ Button.MouseLeave��6+   .[�50   "   �� .��51   &   �� $ Part_Border��61   .��52   *   W� $ Opacity�6:    053   6    154   6   $ 0:0:0.3D�55   )    56   .     57   2   $ 	EaseInOut=�6>   59   +   5:   '   5;   #   5<      5>       5?      :� 5@      $ Border.MouseEnter��6'   .[�5A      �� .��5B   "   �� 5N   #   5O      5P      :� 5Q      $ Button.MouseLeave��6'   .[�5R      �� .��5S   "   �� 5\   #   5]      5^      5`      5b       5c   
   5g      �� 5j      " ��6   .�5k   
   ��  �@�LayoutTransform 5l      �� 5m      	transform��6!   5o       5p   
   :� 5q      $ Loaded��6   [�5r      �� 5s      .��5t      �� .��5u      W� $ Opacity�6.    06R    16[   $ 0:0:0.5D�6b   5v      5w      5y      5{      �� 5}      " ��6   .�5~   
   ��  � ��$ 0q�6,   �� 5   
    � ��$ 15 10q�6#   �� 5�   
    � ��$ 16(�6%   �� 5�   
    � ��$ Stretch=�6.   �� 5�   
    �@��TextWrapping$
 Wrap=�6)   �� 5�   
    �@��AcceptsReturn$
 True��6*   5�      �� 5�      " ��6   .�5�   
   ��  � ��$ 0q�6,   5�      �� 5�      " ��6   8   .�5�   
   ��  � ��$ False��6:   5�      �� 5�      " ��6   .�5�   
   ��  � �� 5�      �� 5�      .R�5�        $��20 10q�6'   $��16(�66   $ False��6D   $ fsagasg��6U   ���#556ee6
�������$ 14:04��6�    �	#FF949494 �	#FFECECEC6�   5�      5�      5�      �� 5�      " ��6   8   .�5�   
   ��  �@�OverridesDefaultStyle$
 true��62   �� 5�   
    �@�Cursor$
 Hand��6#   �� 5�   
    � �� 5�      �� 5�      " ��6"   .R�5�      �� $��Border��6   
������ $ 10��6D   $��	10 5 10 5q�6V   $ 10q�6i   ._�5�      �  5�      �� 5�   "   $
��auto �63   �� 5�   "   $��* �63    5�      �� 5�   "   �� 5�   "   .+�5�        $��0��6.   $��0��5�   .   $��40��5�   .   $
��Left=�5�   .   $��40��5�   .   8�� $��Medium"�5�   .   $��2��5�   .   $��0 0 10 0q�5�   .    5�   .   �� *AvatarViewModel.Hash+8  ��5�   .   �� *AvatarViewModel.Hash+8    5�   .   �� *AvatarViewModel.Label+5�   .   �� 5�      $��ContactUsername��5�   (   $��1��5�   (   $��0��5�   (   $��14(�5�   (   ���#8691b2$
��Bold"�5�   (   $
��Left=�5�   (   ��5�   (   �� *ContactUsername+�� 5�      $��ContentPreview��5�   (   $��1��5�   (   $��1��5�   (   8�� $
��Left=�5�   (   ��5�   (   �� *ContentPreview+5�      5�       5�      P� 5�      ! �@J�IsMouseOver$
" true��69   . �5�      �� $# Border��6%    � �� �	#FFEAEAEA6O   5�      P� 5�      ! �@��
IsSelected$
" True��68   . �5�      �� $# Border��6%   8   � ��6O   �� 5�      $# ContactUsername��6%    � ��
 �����6X   �� 5�      $# ContentPreview��6%    � ��
 �����6W   5�      5�      5�      5�      �� 5�      " n�5�   	   .�5�   
   ��  � �� 5�      �� 5�      " n�6"   .R�5�      � $
$ Grid��6   .+�5�      �� $��Stretch=�6$   $��Stretch=�5�   !   $
��Auto��5�   !   $
��Auto��5�   !   
������ 5�   !   �� 5�      $% CornerScrollBarRectangle��6!   $ 5��5�      $��Stretch=�5�      $��Stretch=�5�      $
��Auto��5�      $
��Auto��5�      $��0,1,0,1q�5�      #��z��5�      5�       5�      P� 5�      ! �@�Tag$" 
Horizontal��5�   !   . �5�      �� $# CornerScrollBarRectangle��6%    � ��$
 Auto��5�   !   �� 5�      $# CornerScrollBarRectangle��6%    � ��$ 6��5�   !   5�      5�      5�      5�      �� 5      " ��6   .�5  
   ��  � ��$ 0q�6#   �� 5  
    �@��IsFlicksEnabled$ false��5     �� 5  
    � �� �#ADABAB5     �� 5  
    � ��
 ���� 5     �� 5  
    � ��$ 7��5	     �� 5
  
    � �� 5     �� 5     " ��6"   .R�5     � $$ GridRoot��6   $��7��5     #��z��5      5     �� 5     $��0.00001* �6,   .+�5     ]� $& 
PART_Track��6    $��0��5     $
��true��5     $|�false��5     ' 5     n� 5  "   $( Thumb��6(   #��z��5  %   #���  5  %   ) 5     �� 5  "   $* PageUp��6/   $��ScrollBar.PageDownCommand��5  -   $+ 0Q�5  -   $|�false��5   -   , 5"     �� 5#  "   $* PageDown��6/   $��ScrollBar.PageUpCommand��5$  -   $+ 0Q�5%  -   $|�false��5&  -   5(     5)      5*     P� 5+     $- Thumb��6"   ! �@J�IsMouseOver$
" true��5-  !   . �5.     �� # �  6%   $# Thumb��5/  !    � ��50  !   51     P� 52     $- Thumb��6"   ! �@n�
IsDragging$
" true��54  !   . �55     �� # �  6%   $# Thumb��56  !    � ��57  !   58     P� 5:     ! � {�$" false��5;  !   . �5<     �� $# Thumb��6%    � y�$ 	Collapsed=�5>  !   5?     P� 5@     ! �@��Orientation$" 
Horizontal=�5A  !   . �5B     �� $# GridRoot��6%    �@�LayoutTransform 5D  "   �� 5E  &   $	. -90Q�66   5G     �� 5H     $# 
PART_Track��6%    �@�LayoutTransform 5J  "   �� 5K  &   $	. -90Q�66   5M     �� 5N      � ��$
 Auto��5O  !   �� 5P      � ��$ 8��5Q  !   �� 5R     $# Thumb��6%    �@�Tag$ 
Horizontal��5T  !   �� 5U     $# PageDown��6%    � ��$ ScrollBar.PageLeftCommand��5W  !   �� 5X     $# PageUp��6%    � ��$  ScrollBar.PageRightCommand��5Z  !   5[     5]     5_     5`     5a     