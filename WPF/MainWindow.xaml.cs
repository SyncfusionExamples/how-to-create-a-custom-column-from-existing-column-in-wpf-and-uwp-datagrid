using Syncfusion.UI.Xaml.Grid;
//using Syncfusion.UI.Xaml.Grid;
//using Syncfusion.UI.Xaml.Grid.Cells;
//using Syncfusion.UI.Xaml.ScrollAxis;
//using Syncfusion.UI.Xaml.Utility;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WpfTestingSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        void datagrid1_AutoGeneratingColumn(object sender, AutoGeneratingColumnArgs e)
        {
            if (e.Column.MappingName == "EmployeeDate1")
            {
                e.Cancel = true;
                this.datagrid1.Columns.Add(new GridDateTimeOffsetColumn()
                {
                    MappingName = "EmployeeDate1",
                    Pattern = Syncfusion.Windows.Shared.DateTimePattern.FullDateTime,
                    UseBindingValue=true
                });
                
                //e.Column.ValueBinding = new Binding() { Path = new PropertyPath("EmployeeDate1"), Converter = new Converter(), Mode = BindingMode.TwoWay };
            }
            //Uncomment to create  GridTextColumn for DateTimeOffset property
            //if (e.Column.MappingName == "EmployeeDate1")
            //{
            //    this.datagrid1.Columns.Add(new GridTextColumn() { MappingName = "EmployeeDate1", HeaderText = "EmployeeDate" });
            //}
        }
    }

}
