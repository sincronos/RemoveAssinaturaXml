   M S B A M L   `   `   `  ����N  JRemoveAssinaturaXml, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullM+clr-namespace:RemoveAssinaturaXml.ViewModelRemoveAssinaturaXml.ViewModel  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	   9!clr-namespace:RemoveAssinaturaXmlRemoveAssinaturaXml  5   	   $    RemoveAssinaturaXml.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :vm+clr-namespace:RemoveAssinaturaXml.ViewModel     5   	   9system&clr-namespace:System;assembly=mscorlib     5   	   3local!clr-namespace:RemoveAssinaturaXml     5   	     � Title$/  )Remover Assinatura NFC-e em Contingência��5
   	   $	��700��6:   $	��900��6G    � DataContext 5      7   1RemoveAssinaturaXml.ViewModel.MainWindowViewModel  5   
   6#   .��5      �  � RowDefinitions 5   
   �� 5      $	��14* �6   �� 5      $	��47* �6   �� 5      $	��30* �6   �� 5      $	��33* �6   �� 5      $	��27* �6   �� 5      $	��24* �6   �� 5      $
��481* �6   �� 5      $	��13* �6    � ColumnDefinitions 5   
   �� 5      $��4* �6   �� 5      $
��169* �6   �� 5      $	��15* �6   �� 5      $
��156* �6   �� 5      $
��204* �6   �� 5      $��5* �6   �� 5       $
��137* �6   �� 5!      $��8* �6   �� 5"      $
��188* �6   �� 5#      $��6* �6   .+�5%   
   �� $
��auto��6   $
��auto��6   $��8��6,   $��1��6<   $��5��6I   $(��"Infraestrutura/logo_novo_tetra.png��6Z   �� 5&   
   $"��Informe o local dos arquivos��6   $��1��67   $��1��6D   $��4��6T   6j   �� 5'   
   $��1��6   
�������$��Italic$�64   $��Stretch=�6G   $��2��6e   $��Stretch=�6�   $��4��6�   ��6r   �� *
PathXmls+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�6�   �� 5(   
   $��Localizar pasta��6   $��6��6+   $��Stretch=�6;   $��2��6Y   $��Stretch=�6f   ��6�   �� *LocalPathCommand+�� 5)   
   $��	Processar��6   $��6��6%   $��4��65   $��25��6B   $��Bottom=�6N   $��0,0,0,3q�6i   $��2��6�   ��6z   �� *ProcessCommand+6�   �� 5*   
   $��1��6+   $��5��6;   $��Bottom=�6H   $��10(�6c   $
��Bold"�6q   $��23��6�   $��4��6�   ��6   �� *Status+6�   �� 5,   
   $��Data Inicial��6   $��1��6'   $��3��67   $��Bottom=�6D   $��26��6_   (  "System.Windows.Controls.DatePicker  5-   
   $��1��6   $��4��6%      DisplayDateStart$ 3/1/2016X�6U      DisplayDateEnd$ 	3/31/2018X�6r      DisplayDate 62   �� *	DtInicial+	   SelectedDate	 6�   �� *	DtInicial+$ TwoWay=�

 �� Now �� TargetNullValue# Z
 �� 5.   
   $��
Data Final��6   $��3��6%   $��3��65   $��Bottom=�6B   $��26��6]     5/   
   $��3��6   $��4��6%   $ 3/1/2016X�6S   $ 	3/31/2018X�6p    62   �� *	DtFinal+	 6�   �� *	DtFinal+$ TwoWay=�# Z
 &   System.Windows.Controls.DataGrid  52   
   -   5          Name$ listBox��52         AutoGenerateColumns .     
IsReadOnly . $��1��6v   $��Stretch=�6�   $��6��6�   $��6��6�   $��Stretch=�6�   ��6$   �� *ListaStatus+   Columns 53      0  *System.Windows.Controls.DataGridTextColumn  54      ,  &System.Windows.Controls.DataGridColumn   Header$ Número��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   ��  �� Path$ NumNfce�  55      $ Status��6%    65   �� $ StatusProcesso�  56      $ Chave��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 350 64    6@   �� $ Chave�  57      $ Emissão��6%    66   �� $ 	DtEmissao�  58      $ Total��6%    64   �� $ Valor�5:      �� 5<   
   $��8��6   $��6��6%   .+�5=      �� .+�5>      �� $'��!Ajustar formatação dos campos: ��6   $��11(�6C   $
��Bold"�6Q   $��5,5,0,10q�6c   6w   �� 5?      $	��NCM��6   $
��10,2q�6)    d� 	IsChecked 67   �� *CamposConfig.CbNCM+�� 5@      $
��CEST��6   $
��10,2q�6*    68   �� *CamposConfig.CbCEST+5A      �� 5B      $��8��6   $��6��6)   .+�5C      �� $��Configurações: ��6   $��11(�63   $
��Bold"�6A   $��	5,15,0,10q�6S   6h   �� 5D      $��10,0,0,5q�6    � ToolTip$Q KCampos CSOSN de cód. 103 serão convertidos para 102 e Cód. 400 para 300.��6W    6-   �� *CamposConfig.Rej384+.��5E      �� �� 5F       �� TextWrapping$
 Wrap=�6%   $<��6Tratar Rejeição 394 - CSOSN não permitido para a UF��6:   5G      5H      5I      5J      5L      5M      