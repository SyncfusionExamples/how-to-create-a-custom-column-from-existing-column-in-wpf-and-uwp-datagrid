using Syncfusion.UI.Xaml.Grid;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfTestingSample
{

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
}
