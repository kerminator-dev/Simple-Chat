   M S B A M L   `   `   `  ����J  FChat.Client.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Chat.Client.WPF.ControlsChat.Client.WPF.Controls  5      2    ,Chat.Client.WPF.Controls.ChatListItemControl   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:Chat.Client.WPF.Controls     5      .��5	      � .+�5
   
   �� -   5        $��Border��5
      $��0q�6     �� CornerRadius$  10��6*    �� Padding$ 15 10 15 10q�6<    J� SnapsToDevicePixels$
 True��6R   ��6m   �� *
Background+ �� RelativeSource ��  �� AncestorType UserControl._�5      �  � ColumnDefinitions 5      �� 5      $
��auto �6'   �� 5      $��* �6'   .+�5      A� -   5         A�Name$ avatarEllipse��5      $��0��61   $��34��6A   $��34��6M   $��Center=�6X   ��6s   �� *AvatarFillColor+ ��  UserControl�� 5      -   5         ��Name$ avatarTextBlock��5      $��0��65    �� Padding$ 0 0 0 2q�6E   $��17(�6W   	 �� TextAlignment$	 Center=�6e   $��Center=�6|   ��6�   �� *
AvatarText+ ��  UserControl��6�   �� *AvatarForeColor+ ��  UserControl�� 5      $��1��6   
 �� Orientation$
 Vertical=�6-   $��Center=�6D   $��12,0,0,0q�6_   .+�5      �� $��14(�6t   ���#68686e��6    �� *
Username+ ��  UserControl6�   �� 5      ���#aeb1b6��6    �� *ContentPreview+ ��  UserControl6z   5      5      5      5      ��5      �� 5   
    �� 
TargetType" >�6   .�5      ��  �� Property � �� �� Value
 �����6+   �� 5        � �� 5!      �� 5"       �� 
TargetType" >�6&   .R�5#      �� ��6+   �� 5$      5&      5'      5)      