   M S B A M L   `   `   `  ����J  FChat.Client.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:Chat.Client.WPF.ViewModelsChat.Client.WPF.ViewModels  5      C&clr-namespace:Chat.Client.WPF.ControlsChat.Client.WPF.Controls  5      S.clr-namespace:Chat.Client.WPF.ViewModels.Pages Chat.Client.WPF.ViewModels.Pages  5	      +    %Chat.Client.WPF.Views.Pages.ChatsPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      7vm(clr-namespace:Chat.Client.WPF.ViewModels     5      ;controls&clr-namespace:Chat.Client.WPF.Controls     5      @pages.clr-namespace:Chat.Client.WPF.ViewModels.Pages     5	      c _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a7i-http://metro.mahapps.com/winfx/xaml/iconpacks   6N   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3b+http://schemas.microsoft.com/xaml/behaviors  6�   *  $Microsoft.Xaml.Behaviors.Interaction    Triggers  5   
   +  %Microsoft.Xaml.Behaviors.EventTrigger  5         	EventName$ Loaded��6   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5      2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 6(   �� *InitializeViewModelCommand+5       � 	Resources 5   
    �� MergedDictionaries �� Source   ChatBackGradient �� 
StartPoint �� EndPoint	 � Color
 � Offset  SelectedChatBackGradient�� 5   
    5      �� 5      $ /Themes/Generic.xaml?�6%   �� 5      $( "/Styles/Pages/ChatsPageStyles.xaml?�6%   %  &	        &	 �     �� 5      $ 0.2,0&�6;   $ 0.5,1&�6N   .��5      � $	 #eef1fb��6   $
 0Q�6/   � 5       $	 #e6e4fa��6   $	
 0.8Q�6/   � 5!      $	 #d1ddfb��6   $	
 0.9Q�6/   � 5"      $	 #e2d7f6��6   $
 1Q�6/   5$      �� 5&      $ 0.2,0&�6C   $ 0.5,1&�6V   .��5'      � $	 #eef1fb��6   $
 0Q�6/   � 5(      $	 #e6e4fa��6   $	
 0.8Q�6/   � 5)      $	 #d1ddfb��6   $	
 0.9Q�6/   � 5*      $	 #e2d7f6��6   $
 1Q�6/   5,      5-      .��50      �  � ColumnDefinitions 51   
   �� 52      $	��250��6   $	��250 �6.   $	��350��6:   �� 53      $��3 �6   �� 54      $��* �6   .+�57   
   �� ���#ffffff$��0��58      ._�59      �  � RowDefinitions 5:      �� 5;      $
��auto �6$   �� 5<      $��* �6$   .+�5?      �� $��0��6   $��1��5@      $��15 15 15 10q�5A      $	��Top=�5B      $	��200��5C      $��Stretch=�5D      ._�5F      1   +Chat.Client.WPF.Controls.IconTextBoxControl     CornerRadius$ 12��62      IconSize$ 16��5G   2   $��16(�5H   2   $
��10 6q�5I   2      Icon$ magnify=�5J   2   ���#abaeb1   IconForeground �#abaeb1���#f5f5f5$��Stretch=�5N   2   5P      �� 5T      $��1��5U        SmoothListBox#��[ 5V        ChatListBoxItemStyle#��[ 5X      ��5W      �� *Chats+5Z      5\      5]      �� 5_   
   $��1��6   ���	#FFFFFFFF$��0 0 1 0q�5a      ���	#FFE8E8E8$��Stretch=�5d      $��Stretch=�5e      � 5g   
   $��2��6   #��[  6    5h      �� 5i      $
��auto �6    �� 5j      $��* �6    �� 5k      $��40��6    $
��auto �6/   .+�5m      �� 
������ $��Stretch=�5n      $��1��5o      .@�5p      �� 
���  ��$	��100��5q      $	��100��6,   $��Stretch=�5r      $
��auto��5s      $
��auto��5t      ._�5v      �� $L��FSGADDDDDDDDDDDDDDDDDgsdsdhsddhdhshdhsDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD��6    $��Stretch=�6n   $
��auto��6�   $
��Wrap=�6�   5w      &    Chat.Client.WPF.Controls.Message  5x      $��20 10q�6#      IsFromMe$
 True��62      Text$ fsagaSHHSGGAGAsg��6B   
����������#778beb   Time$ 14:04��6�      TimeForeground
 �����   SecondaryBackground �#556ee66�     5y      $��20 10q�6#   $��16(�62   $ False��6@   $ fsagasg��6Q   
�������
�������$ 14:04��6�    �	#FF949494 �	#FFECECEC6�     5z      $��20 10q�6#   $��Stretch=�63   $	��200��6Q   $
��auto��5{   #   $
 True��61   $� ~fsaDFAFSAGSAHGSHAgaSHHSGGAGAsgfsaDFAFSGAsgfsaDFAFSAGSAHGSHAgaSHHSGGAGAsgfsaDFAFSGAsgfsaDFAFSAGSAHGSHAgaSHHSGGAGAsgfsaDFAFSGAsg��6A   
����������#778beb$ 14:04��6�   
 ����� �#556ee66  5~      �� 5�       �� Orientation$ Vertical=�6   $��0��5�      .+�5�      �� $��Stretch=�6   $	��Top=�5�      
�������._�5�      �� $
��Left=�6    $��Username��5�       $��Center=�5�       $��15q�5�       $��24(�5�       
�������5�       5�      �� 5�      $��Stretch=�6   $	��Top=�6:   $��10��6R    J� Opacity$ 1Q�6^   ��5�      �� 5�      $ 0.5,0&�6.   $ 0.5,1&�6A   .��5�      � $	 	#FFD0D0D0��6+   $
 0Q�6=   � 5�      $	 Transparent��6+   $
 1Q�6?   5�      5�      5�      �� 5�      $��3��6   .+�5�      �� $
��auto��5�      $	��200��5�      
������� �� Padding$ 5q�5�      $��0,0,0,0q�5�      ._�5�      �  5�      �� 5�      $��* �6/   �� 5�      $
��AUTO �6/   �� 5�      $
��auto �6/   .+�5�      �� $��0��6"   $��Type message here.��5�   "     DefaultForegroundBrush#��[ 5�   "     MultilineTextBox#��[ 5�      .  (MahApps.Metro.IconPacks.PackIconMaterial  5�      -   5        $
��Icon��5�   )   $	��Top=�5�   -   $��1��5�   )   $��Right=�5�   )   $��	0 12 10 0q�5�   )   $��0 0 0 0q�5�   )      Kind$ 	Paperclip=�5�   )   
�������$��20��5�   )   $��20��65   �� 5�      -   5        $��btnSaveEditedText��5�      $��2��5�   %   $	��4 4q�5�      $��60��5�      $	��Top=�5�      $��15(�5�      $��33��5�      
����������	#FF778BEB.��5�   4   Send5�      5�      5�      5�      5�      5�      5�      