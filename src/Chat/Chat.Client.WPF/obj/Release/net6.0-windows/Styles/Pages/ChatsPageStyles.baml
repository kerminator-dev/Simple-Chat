   M S B A M L   `   `   `  ���� J  FChat.Client.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Chat.Client.WPF.ConvertersChat.Client.WPF.Converters  a5clr-namespace:Chat.Client.WPF.ViewModels.UserControls'Chat.Client.WPF.ViewModels.UserControls  U QSystem.Runtime, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a7,clr-namespace:System;assembly=System.RuntimeSystem C&clr-namespace:Chat.Client.WPF.ControlsChat.Client.WPF.Controls  W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089?   9Chat.Client.WPF.Converters.HashToAvatarForeColorConverter   ForeColorConverter?  9Chat.Client.WPF.Converters.HashToAvatarBackColorConverter  BackColorConverter=  7Chat.Client.WPF.Converters.OnlineStatusToColorConverter  OnlineStatusConverter  DefaultForegroundBrush  SelectedChatBackGradient  �� 
StartPoint �� EndPoint � Color � Offset  AvatarFontSize  MessageMarginLeft u� Left
 u� Top u� Right u� Bottom  MessageMarginRight �� 
TargetType	 �� Property
 �� Value �� 
TargetType J� Opacity �� CornerRadius ��Name � Triggers :� RoutedEvent �� 
TargetName �� TargetProperty	 W� To g� Duration W� EasingFunction0  *System.Windows.Media.Animation.QuarticEase7  1System.Windows.Media.Animation.EasingFunctionBase   
EasingMode �� Triggers �� Triggers   ListBoxItemEmergingAnimation W� From8   2Chat.Client.WPF.Controls.PlaceholderTextBoxControl 	 MessageTextBox 
 DefaultListBox  SmoothListBox �� BasedOn  MessageListBox  MessageListBoxItemStyle ��Name �� Padding&    Chat.Client.WPF.Controls.Message  Name   TimeForeground   SecondaryBackground    Text!   Time" �� Value# �� Binding$ �� 
TargetName% P� Property& P� Value' �� Color( �� Opacity  MessageTemplate) �� DataType  ChatListBoxItemStyle* � ColumnDefinitions+ � RowDefinitions%   Chat.Client.WPF.Controls.Avatar,   Fill- �� 	Converter.   Label  ScrollBarTrackThumb/ �Name0 ]�Name1 ]� Thumb2 n�Name3 ]� IncreaseRepeatButton4 ��Name5 ]� DecreaseRepeatButton6 P� 
SourceName  ButtonSelectBrush  	DarkBrush7 �� Angle�� F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     7
converters(clr-namespace:Chat.Client.WPF.Converters  <vm5clr-namespace:Chat.Client.WPF.ViewModels.UserControls  4sys,clr-namespace:System;assembly=System.Runtime  3controls&clr-namespace:Chat.Client.WPF.Controls  :x,http://schemas.microsoft.com/winfx/2006/xaml     %�  &	        &	      &	 
     &	      &	       &	 �     &	 �     &	 �     '�� �     7  &	 ,    &		 �    &	
 �    &	 �    7 
 &	 �    7  &	 ?    7  7  7  &	 #    &	 B    7  7  7  7   7  7  &	 �    '�� b           ��#8591b6���� $  0.2,0&�$ 0.5,1&�.��� $ #5c82ff��$ 0Q�� $ #677fed��$
 0.45Q�� $ #4b9ad8��$ 1Q�X�18Q�u� $	 100Q�$ 10Q�$ 10Q�$ 10Q�u� $ 10Q�$ 10Q�$ 10Q�$	 100Q��� " ��.��� 	 �@�Cursor$

 Hand���� 	 � ��$
 0q��� 	 � ��
 �� " ��.R��� $��Part_Border��8��  $	 0.8Q�$ 16��._��� $ PART_contentpresenter��$��Center=�$��Center=� :� $ Border.MouseEnter��.[��� .���� $ Part_Border��.��W� $ Opacity� 1$ 0:0:0.3D�   $ 	EaseInOut=�:� $ Button.MouseLeave��.[��� .���� $ Part_Border��.��W� $ Opacity� 0.8$ 0:0:0.3D�   $ 	EaseInOut=� :� $ Border.MouseEnter��.[��� .���� :� $ Button.MouseLeave��.[��� .����  �� " ��.��� 	 �@�LayoutTransform
 �� 	transform�� :� $ Loaded��[��� .���� .��W� $ Opacity� 0 1$ 0:0:0.5D��� "  .��� 	 �@�Cursor$
 IBeam���� 	 � ��$
 0q��� 	 � ��$
 15 10q��� 	 � ��$
 16(��� 	 � ��$
 Stretch=��� 	 �@��TextWrapping$

 Wrap=��� 	 �@��AcceptsReturn$

 True���� " ��.��� 	 � ��$
 0q��� " ��8   .��� 	 � ��$
 False���� " ��8   .��� 	 �@��SelectionMode$
 Multiple=��� 	 � ��$
 Visible=��� " ��.��� 	 � ��
 �� " ��.R��� $ Border��$	��0 0q�$	 5 0q�
������ ._�  $ Message��8��  $��16(�$
��Left=�
�������$
��auto��
������� �	#FF949494 �	#FFECECEC  �� *
OutputText+! �� *Time+ �� $
" True��# �� *
IsFromMe+.a��� $$ Message��8
  	 � ���� $$ Message��8
  	 � ���� $$ Message��	 � ��

 ������� $$ Message��	 �@ SecondaryBackground
 �#556ee6�� $$ Message��	 � ��$
 Right=��� $$ Message��	 �@ TimeForeground

 �����P� % �@J�IsMouseOver$
& true��. ��� $$ Border��	 � ��
 �� $' White��$	( 0.4Q�P� % �@��
IsSelected$
& True��. ��� $$ Border��	 � ��
 �� $' White��$	( 0.5Q��� $) vm:MessageViewModel���� " ��8   .��� 	 �@�OverridesDefaultStyle$

 true���� 	 �@�Cursor$

 Hand���� 	 � ��
 �� " ��.R��� $��Border��
������ $ 10��$��	10 5 10 5q�$ 10q�._�� * �� $
��auto ��� $��* �+ �� �� .+�  $��0��$��0��$��40��$
��Left=�$��40��8�� $��Medium"�$��2��$��0 0 10 0q�, �� *Avatar.Hash+8-  ���� *Avatar.Hash+8-  . �� *Avatar.Label+�� $��ContactUsername��$��1��$��0��$��14(����#8691b2$
��Bold"�$
��Left=�$��CharacterEllipsis=����� *ContactUsername+�� $��ContentPreview��$��1��$��1��8�� $
��Left=�$��CharacterEllipsis=����� *ContentPreview+ P� % �@J�IsMouseOver$
& true��. ��� $$ Border��	 � ��
 �	#FFEAEAEAP� % �@��
IsSelected$
& True��. ��� $$ Border��8
  	 � ���� $$ ContactUsername��	 � ��

 ������� $$ ContentPreview��	 � ��

 ������� " n�.��� 	 � ��
 �� " n�.R�� $
/ Grid��.+��� $��Stretch=�$��Stretch=�$
��Auto��$
��Auto��
������ �� $ CornerScrollBarRectangle��$ 5��$��Stretch=�$��Stretch=�$
��Auto��$
��Auto��$��0,1,0,1q�#��z�� P� % �@�Tag$& 
Horizontal��. ��� $$ CornerScrollBarRectangle��	 � ��$

 Auto���� $$ CornerScrollBarRectangle��	 � ��$
 6���� " ��.��� 	 � ��$
 0q��� 	 �@��IsFlicksEnabled$
 false���� 	 � ��
 �#ADABAB�� 	 � ��

 ���� �� 	 � ��$
 7���� 	 � ��
 �� " ��.R�� $/ GridRoot��$��7��#��z��+ �� $��0.00001* �.+�]� $0 
PART_Track��$��0��$
��true��$|�false��1 n� $2 Thumb��#��z��#���  3 �� $4 PageUp��$��ScrollBar.PageDownCommand��$ 0Q�$|�false��5 �� $4 PageDown��$��ScrollBar.PageUpCommand��$ 0Q�$|�false�� P� $6 Thumb��% �@J�IsMouseOver$
& true��. ��� #
 �  $$ Thumb��	 � ��P� $6 Thumb��% �@n�
IsDragging$
& true��. ��� #
 �  $$ Thumb��	 � ��P� % � {�$& false��. ��� $$ Thumb��	 � y�$
 	Collapsed=�P� % �@��Orientation$& 
Horizontal=�. ��� $$ GridRoot��	 �@�LayoutTransform
 �� $	7 -90Q��� $$ 
PART_Track��	 �@�LayoutTransform
 �� $	7 -90Q��� 	 � ��$

 Auto���� 	 � ��$
 8���� $$ Thumb��	 �@�Tag$
 
Horizontal���� $$ PageDown��	 � ��$
 ScrollBar.PageLeftCommand���� $$ PageUp��	 � ��$ 
 ScrollBar.PageRightCommand��