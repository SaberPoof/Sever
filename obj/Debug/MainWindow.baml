   M S B A M L   `   `   `  ����  ReportGenerator     ReportGenerator.MainWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f8metro,http://metro.mahapps.com/winfx/xaml/controls  5        � Title$  Report Generator��5      $	��600��6-   $	��800��6:      PrimaryColor(  "MahApps.Metro.Controls.MetroWindow   	GlowBrush# �   5        AccentColor#���  5       � WindowStartupLocation$ CenterScreen=�5      .��5
      �   BackgroundColor#���  6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��10q�6.   $
��Left=�6:   .+�5      �� $%��Загрузить данные��6   $��5q�6T   $��10,15q�6_   #���   5      
�������$��16(�6T   $��0q�5        
DropShadow J� Effect# �  5        HoverButtonStyle#��[ 5      ��5   0   �� *LoadDataCommand+5      �� 5      $+��%Сгенерировать отчет��6   $��5q�6]   $��10,15q�6h   #���  5      
�������$��16(�6S   $��0q�5      # �  5      #��[ 5      ��5   3   �� *GenerateReportCommand+5      5      �� 5!   
   $��1��6   $��10q�6"   #���  6.   .@�5#      �� $��Сотрудники��6   .��5$      &   System.Windows.Controls.DataGrid     AutoGenerateColumns .     
IsReadOnly .    HeadersVisibility$ Column=�5%      $��5q�66   ��5$      �� *	Employees+	   Columns	 5&      0  *System.Windows.Controls.DataGridTextColumn  5'      ,  &System.Windows.Controls.DataGridColumn
   Header$
 ID��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6P   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *Id+6P     5(      $
 Фамилия��6-   $ * 6Z    6>   �� *	Surname+6Z     5)      $
 Имя��6-   $ * 6S    6:   �� *Name+6S     5*      $
 Отчество��6-   $ * 6^    6?   �� *
Patronymic+6^     5+      $
 Дата рождения��6-   $
 Auto 6t    6D   �� *DateOfBirth+ �� StringFormat$ d��6t     5,      $
 
Отдел��6-   $
 Auto 6]    6<   �� *DepartmentId+6]     5-      $%
 Количество задач��6-   $
 Auto 6e    6G   �� *	TaskCount+6e   5/      50      �� 53      $��Отделы��6   .��54         .   . $ Column=�55      $��5q�66   ��54      �� *Departments+	 56        57      $
 ID отдела��6-   $
 Auto 6W    6@   �� *Id+6W     58      $#
 Название отдела��6-   $ * 6_    6F   �� *Name+6_   5:      5;      �� 5>      $��Задачи��6   .��5?         .   . $ Column=�5@      $��5q�66   ��5?      �� *Tasks+	 5A        5B      $
 ID задачи��6-   $
 Auto 6W    6@   �� *Id+6W     5C      $
 ID сотрудника��6-   $ * 6c    6D   �� *
EmployeeId+6c   5E      5F      5G      �� 5J   
   $��2��6   $��10q�6!   #���   6-    J� Opacity$	 0.9Q�6Y   .@�5K      �� $)��#Всего сотрудников: ��6   
�������6:   �� 5L      
���������6   �� *Employees.Count+6@   �� 5M      $!��Всего отделов: ��6   
�������66   �� 5N      
���������6   �� *Departments.Count+6B   �� 5O      $��Всего задач: ��6   
�������64   �� 5P      
���������6   �� *Tasks.Count+6<   5Q      5R      5S      