   M S B A M L   `   `   `  ����E  APerlinNoiz, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:PerlinNoiz
PerlinNoiz  5   	       PerlinNoiz.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:PerlinNoiz     5   	   -   5          �Name$	  win��5   	    � Title$ 
MainWindow��5	   	   $	��250��6   $	��250��6)   .��5
      � -   5         �Name$
 grid��5
       � RowDefinitions 5   
   �� 5      $	��59* �6   �� 5      $	��22* �6   �� 5      $	��22* �6   �� 5      $	��66* �6   �� 5      $	��65* �6    � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� -   5        $��FirstPerlinAlg��5      $��0,0,0,2q�6J   �� 5   
   -   5        $��SecondPerlinAlg��5      $��1��6L   $��0,0,0,2q�6\   �� 5   
   $��Маштаб��6   $��1��6!   $��11(�61   $��0,0,0,1q�6?   $��1��6Q   �� 5   
   -   5         ��Name$
 Mash��5      $��1��6    �� 5   
   $��Семя��6   $��11(�6   $��2��6-   $��1��6:   $��0,0,0,1q�6J   �� 5   
   -   5        $
 Seed��5      $��2��6    �� 5!   
   -   5         ��Name$
 List��5!      $��3��6    $��2��6C    � UseLayoutRounding .  .��5"      �� $1��+Сохранёные изображения��6   $��WrapWithOverflow=�66    �� TextAlignment$ Center=�6V   5#      �� 5%   
   -   5        $��2��5%      $��1��6   $��	63,22,0,0q�6.   $��2��6A   .��5&      �� $.��(Список Постов Сервера��6   $
��Wrap=�65   $ Center=�6I   5'      �� 5)   
   -	   5        $ Server��5)      $��2��6!   $��1��6.   $��	0,22,62,0q�6N   $��2��6a   .��5*      �� $0��*Подключиться к серверу��6   $
��Wrap=�66   $ Center=�6J   5+      �� 5,   
   -
   5        $
 Help��5,      $��Помощь��6   $��4��60   $��1��6=   5.      5/      