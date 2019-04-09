using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace SfDataGridDemo
{
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
            var column = cachedColumn as GridDateTimeOffsetColumn;

            if (value == null || DBNull.Value == value)
            {
                if (column.AllowNullValue && column.MaxDate != System.DateTime.MaxValue && column.WaterMark.ToString() == string.Empty)
                    return column.MaxDate;

                if (column.AllowNullValue && column.WaterMark.ToString() != string.Empty)
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
}
