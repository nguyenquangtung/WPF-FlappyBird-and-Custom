   M S B A M L   `   `   `  ����F  BFlappy-bird, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:Flappy_birdFlappy_bird  5   	       Flappy_bird.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:Flappy_bird     5   	     � Title$  Flappy Bird��5   	   $	��490��6   $	��525��6*    � WindowStartupLocation$ CenterScreen=�66    � 
ResizeMode$ NoResize=�6[   .��5	      �� -   5        $��MyCanvas��5	      |�. 
����ح�.+�5
   
   �� $	��145��6   $	��200��6   $��images/clouds.png��6)    �� Left$ 34��6D   
 �� Top$	 155��6U   6h   �� 5   
   $	��145��6   $	��200��6   $��images/clouds2.png��6)   $	 295��6E   $	 144��6W   6j   �� 5   
   $	��390��6   $��66��6   $��images/pipeBottom.png��6(   
 � Tag$
 obs1��6G   $	 100��6R   $	 242��6d   6w   �� 5   
   $	��390��6   $��66��6   $��images/pipeTop.png��6(   $
 obs1��6D   $	 100��6O   $
 -266��6a   6u   �� 5   
   $	��390��6   $��66��6   $��images/pipeBottom.png��6(   $
 obs2��6G   $	 261��6R   $	 327��6d   6w   �� 5   
   $	��390��6   $��66��6   $��images/pipeTop.png��6(   $
 obs2��6D   $	 261��6O   $
 -181��6a   6u   �� 5   
   $	��390��6   $��66��6   $��images/pipeBottom.png��6(   $
 obs3��6G   $	 429��6R   $	 229��6d   �� 5   
   $	��390��6   $��66��6   $��images/pipeTop.png��6(   $
 obs3��6D   $	 429��6O   $
 -279��6a   �� 5   
   -   5        $��
flappyBird��5      $��40��6"   $��50��6.   $��images/flappyBird.png��69   $
��Fill=�6X   $ 22��6g   $	 210��6x   6�   �� 5   
   -   5        $��txtScore��5      $��25(�6    $��	ExtraBold"�6.   $��Score: 0��6E   6Z   5      5      