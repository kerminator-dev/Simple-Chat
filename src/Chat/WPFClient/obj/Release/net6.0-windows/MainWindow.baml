   M S B A M L   `   `   `  ???? D  @WPFClient, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:WPFClient	WPFClient  ;"clr-namespace:WPFClient.ConvertersWPFClient.Converters      WPFClient.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     )localclr-namespace:WPFClient     S OHandyControl, Version=3.4.0.0, Culture=neutral, PublicKeyToken=45be8712787a1e5b0hc'https://handyorg.github.io/handycontrol  3
converters"clr-namespace:WPFClient.Converters    ? Title$  
MainWindow??$	??450??$	??800?? ? 	Resources    MagnifierIconGeometry2  ,WPFClient.Converters.BooleanToColorConverter  UserActiveStatusConverter??%?   &	        &	 ?     ???M44,12 C32,12 22,22 22,34 22,46 32,56 44,56 56,56 66,46 66,34 66,22 56,12 44,12z M44,0 C63,0 78,15 78,34 78,53 63,68 44,68 40,68 36.5,67.5 33,66 L32.5,66 14,90 0,79.5 18,55.5 17,55 C13,49 10,42 10,34 10,15 25,0 44,0z?  .???  ? ColumnDefinitions ?? $	??200??$
??auto ??? $??* ?.+?? $??0?? ? RowDefinitions ?? $
??auto ??? $
??auto ??? $??* ?.+?%  HandyControl.Controls.SearchBar  $??
10 10 10 0q?$??Stretch=?$??0???? $??0q?$??Stretch=?$??1??$??
10 10 10 0q?  TabControlCapsuleSolid#??[ .@??? $??Chats???? $??Active users???? $??Settings???? $??2??$??Visible=?
?????? (  "HandyControl.Controls.ScrollViewer   IsInertiaEnabled$
 True??.  (HandyControl.Controls.ScrollViewerAttach   
IsDisabled$ False??   Orientation$ Vertical=?$??0q????? *Users+????  ?? DataType   WPFClient.User"  .R?? $	??150?? ?? $
??auto ??? $??* ? ?? ?? .+?$  HandyControl.Controls.Gravatar  $??0??$??0??$??2??$??40??$??40??$??0 0 10 0q?	   Id ?? *
Username+A? $??7??$??7??$??27 0 0 0q?$??Bottom=?$??1?????? *
IsOnline+	 ?? 	Converter#	 [ ?? $??1??$??0?????? *
Username+.@?  
   Username$

 fsgs??   IsOnline$
 True??