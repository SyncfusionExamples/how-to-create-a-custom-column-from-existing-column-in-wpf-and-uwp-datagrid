# How to Create a Custom Column from Existing Column in WPF / UWP DataGrid?

This repositories contains the samples to create custom column from existing columnm in [WPF DataGrid](https://www.syncfusion.com/wpf-controls/datagrid) and [UWP DataGrid](https://www.syncfusion.com/uwp-ui-controls/datagrid) (SfDataGrid).

You can create your own column by overriding the [predefined column types](https://help.syncfusion.com/wpf/datagrid/column-types#_Overriding_existing_cell) in DataGrid. For example, the **GridDateTimeColumn** loads the **DateTime** value by default. If you want to display **DateTimeOffset** value, you can create a new column by overriding the **GridDateTimeColumn** class.

In the below code snippet, converter created to format the **DateTimeOffSet** value to DateTime by defining **ValueBinding** (edit) and **DisplayBinding** (non-edit).

### For WPF:
``` c#
public class DateTimeOffsetFormatConverter : IValueConverter
{
    private GridDateTimeOffsetColumn cachedColumn;

    public DateTimeOffsetFormatConverter(GridDateTimeOffsetColumn column)
    {
        cachedColumn = column;
    }

    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        value = ((DateTimeOffset)value).DateTime;
        var column = cachedColumn as GridDateTimeColumn;
        if (value == null || DBNull.Value == value)
        {
            if (column.AllowNullValue && column.MaxDateTime != System.DateTime.MaxValue && column.NullText == string.Empty)
                return column.MaxDateTime;

            if (column.AllowNullValue && column.NullValue != null)
                return column.NullValue;
            else if (column.AllowNullValue && column.NullText != string.Empty)
                return column.NullText;

            if (column.MaxDateTime != System.DateTime.MaxValue)
                return column.MaxDateTime;
        }

        DateTime _columnValue;
        _columnValue = (DateTime)value;

        if (_columnValue < column.MinDateTime)
            _columnValue = column.MinDateTime;

        if (_columnValue > column.MaxDateTime)
            _columnValue = column.MaxDateTime;

        return DateTimeFormatString(_columnValue, column);
    }

    private string DateTimeFormatString(DateTime columnValue, GridDateTimeColumn column)
    {
        switch (column.Pattern)
        {
            case DateTimePattern.ShortDate:
                return columnValue.ToString("d", column.DateTimeFormat);
            case DateTimePattern.LongDate:
                return columnValue.ToString("D", column.DateTimeFormat);
            case DateTimePattern.LongTime:
                return columnValue.ToString("T", column.DateTimeFormat);
            case DateTimePattern.ShortTime:
                return columnValue.ToString("t", column.DateTimeFormat);
            case DateTimePattern.FullDateTime:
                return columnValue.ToString("F", column.DateTimeFormat);
            case DateTimePattern.RFC1123:
                return columnValue.ToString("R", column.DateTimeFormat);
            case DateTimePattern.SortableDateTime:
                return columnValue.ToString("s", column.DateTimeFormat);
            case DateTimePattern.UniversalSortableDateTime:
                return columnValue.ToString("u", column.DateTimeFormat);
            case DateTimePattern.YearMonth:
                return columnValue.ToString("Y", column.DateTimeFormat);
            case DateTimePattern.MonthDay:
                return columnValue.ToString("M", column.DateTimeFormat);
            case DateTimePattern.CustomPattern:
                return columnValue.ToString(column.CustomPattern, column.DateTimeFormat);
            default:
                return columnValue.ToString("MMMM", column.DateTimeFormat);
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class DateTimeOffsetToDateTimeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value == null)
            return null;
        return ((DateTimeOffset)value).DateTime;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value == null)
            return null;
        return value is DateTimeOffset ? value : new DateTimeOffset((DateTime)value);
    }
}
```

In the below code snippet, **GridDateTimeOffsetColumn** column created from **GridDateTimeColumn**.

``` c#
public class GridDateTimeOffsetColumn : GridDateTimeColumn
{
    protected override void SetDisplayBindingConverter()
    {
        if ((DisplayBinding as Binding).Converter == null)
            (DisplayBinding as Binding).Converter = new DateTimeOffsetFormatConverter(this);

        if ((ValueBinding as Binding).Converter == null)
            (ValueBinding as Binding).Converter = new DateTimeOffsetToDateTimeConverter();
    }
}
```

In the below code snippet, created **GridDateTimeOffsetColumn** added to [SfDataGrid.Columns](https://help.syncfusion.com/cr/wpf/Syncfusion.UI.Xaml.Grid.SfDataGrid.html#Syncfusion_UI_Xaml_Grid_SfDataGrid_Columns) collection and specify the Pattern as FullDateTime. Since the **ShortDate** is the default pattern of **GridDateTimeColumn**.

#### XAML

``` xml
<syncfusion:SfDataGrid  x:Name="dataGrid"                                                                       
                        AutoGenerateColumns="False" 
                        ItemsSource="{Binding Orders}">
    <syncfusion:SfDataGrid.Columns>
        <local:GridDateTimeOffsetColumn MappingName="OrderDate" Pattern="FullDateTime"  UseBindingValue="True"/>
    </syncfusion:SfDataGrid.Columns>
</syncfusion:SfDataGrid>
```

#### C#

``` c#
this.datagrid1.Columns.Add(new GridDateTimeOffsetColumn()
{
    MappingName = "OrderDate",
    Pattern = Syncfusion.Windows.Shared.DateTimePattern.FullDateTime,
    UseBindingValue = true
});
```

### For UWP:

``` csharp
public class DateTimeOffsetFormatConverter : IValueConverter
{
    private GridDateTimeOffsetColumn cachedColumn;

    public DateTimeOffsetFormatConverter(GridDateTimeOffsetColumn column)
    {
        cachedColumn = column;
    }

    object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
    {
        value = ((DateTimeOffset)value).DateTime;
        var column = cachedColumn as GridDateTimeColumn;

        if (value == null || DBNull.Value == value)
        {
            if (column.AllowNullValue && column.MaxDate != System.DateTime.MaxValue && column.WaterMark == string.Empty)
                return column.MaxDate;
               
            if (column.AllowNullValue && column.WaterMark != string.Empty)
                return column.WaterMark;

            if (column.MaxDate != System.DateTime.MaxValue)
                return column.MaxDate;
        }

        DateTime _columnValue;
        _columnValue = (DateTime)value;

        if (_columnValue < column.MinDate)
            _columnValue = column.MinDate;

        if (_columnValue > column.MaxDate)
            _columnValue = column.MaxDate;

        return _columnValue.ToString(column.FormatString, CultureInfo.CurrentUICulture);
    }
      
    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}

public class DateTimeOffsetToDateTimeConverter : IValueConverter
{
    object IValueConverter.Convert(object value, Type targetType, object parameter, string language)
    {
        if (value == null)
            return null;
        return ((DateTimeOffset)value).DateTime;
    }

    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (value == null)
            return null;
        return value is DateTimeOffset ? value : new DateTimeOffset((DateTime)value);
    }
}
```

In the below code snippet, **GridDateTimeOffsetColumn** column created from **GridDateTimeColumn**.

``` csharp
public class GridDateTimeOffsetColumn:GridDateTimeColumn
{
    protected override void SetDisplayBindingConverter()
    {
        if ((DisplayBinding as Binding).Converter == null)
            (DisplayBinding as Binding).Converter = new DateTimeOffsetFormatConverter(this);

        if ((ValueBinding as Binding).Converter == null)
            (ValueBinding as Binding).Converter = new DateTimeOffsetToDateTimeConverter();
    }
}
```

In the below code snippet, created **GridDateTimeOffsetColumn** added to **SfDataGrid.Columns** collection and specify the full date-time pattern in **FormatString** as **F**. Since the **ShortDate** is the default pattern of **GridDateTimeColumn**.

#### XAML

``` xml
<syncfusion:SfDataGrid x:Name="dataGrid"
                       AutoGenerateColumns="False"
                       ItemsSource="{Binding Orders}">
    <syncfusion:SfDataGrid.Columns>
        <local:GridDateTimeOffsetColumn FormatString="F"
                                        HeaderText="Order Date"
                                        MappingName="OrderDate" 
                                        AllowInlineEditing="True"
                                        UseBindingValue="True"/>
    </syncfusion:SfDataGrid.Columns>
</syncfusion:SfDataGrid>
```

#### C#

``` csharp
this.dataGrid.Columns.Add(new GridDateTimeOffsetColumn() { HeaderText = "Order Date", MappingName = "OrderDate", FormatString = "F" , AllowInlineEditing = true, UseBindingValue = true });
```