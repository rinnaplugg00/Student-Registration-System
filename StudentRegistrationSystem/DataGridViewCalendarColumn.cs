using System;
using System.Windows.Forms;

namespace StudentRegistrationSystem
{
    public sealed class DataGridViewCalendarColumn : DataGridViewColumn
    {
        public DataGridViewCalendarColumn() : base(new DataGridViewCalendarCell())
        {
            DefaultCellStyle.Format = "dd.MM.yyyy";
        }

        public override DataGridViewCell CellTemplate
        {
            get => base.CellTemplate;
            set
            {
                if (value != null && !(value is DataGridViewCalendarCell))
                    throw new InvalidCastException("CellTemplate must be a DataGridViewCalendarCell.");

                base.CellTemplate = value;
            }
        }
    }

    internal sealed class DataGridViewCalendarCell : DataGridViewTextBoxCell
    {
        public DataGridViewCalendarCell()
        {
            Style.Format = "dd.MM.yyyy";
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            var ctl = DataGridView.EditingControl as DataGridViewCalendarEditingControl;
            if (ctl == null)
                return;

            if (Value == null || Value == DBNull.Value)
            {
                ctl.Checked = false;
                ctl.Value = DateTime.Today;
            }
            else
            {
                DateTime value;
                if (Value is DateTime time)
                    value = time;
                else
                    value = Convert.ToDateTime(Value);

                ctl.Checked = true;
                ctl.Value = value;
            }
        }

        public override Type EditType => typeof(DataGridViewCalendarEditingControl);

        public override Type ValueType => typeof(object);

        public override object DefaultNewRowValue => DBNull.Value;

        protected override object GetFormattedValue(
            object value,
            int rowIndex,
            ref DataGridViewCellStyle cellStyle,
            System.ComponentModel.TypeConverter valueTypeConverter,
            System.ComponentModel.TypeConverter formattedValueTypeConverter,
            DataGridViewDataErrorContexts context)
        {
            if (value == null || value == DBNull.Value)
                return string.Empty;

            if (value is DateTime dateTime)
                return dateTime.ToString("dd.MM.yyyy");

            return base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
        }

        public override object ParseFormattedValue(
            object formattedValue,
            DataGridViewCellStyle cellStyle,
            System.ComponentModel.TypeConverter formattedValueTypeConverter,
            System.ComponentModel.TypeConverter valueTypeConverter)
        {
            if (formattedValue == null || formattedValue == DBNull.Value || string.IsNullOrWhiteSpace(formattedValue.ToString()))
                return DBNull.Value;

            if (formattedValue is DateTime dateTime)
                return dateTime.Date;

            if (DateTime.TryParse(formattedValue.ToString(), out DateTime parsed))
                return parsed.Date;

            return DBNull.Value;
        }
    }

    internal sealed class DataGridViewCalendarEditingControl : DateTimePicker, IDataGridViewEditingControl
    {
        private DataGridView _dataGridView;
        private bool _valueChanged;
        private int _rowIndex;

        public DataGridViewCalendarEditingControl()
        {
            Format = DateTimePickerFormat.Custom;
            CustomFormat = "dd.MM.yyyy";
            ShowCheckBox = true;
        }

        public object EditingControlFormattedValue
        {
            get => Checked ? Value.Date : (object)string.Empty;
            set
            {
                if (value == null || value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()))
                {
                    Checked = false;
                    Value = DateTime.Today;
                    return;
                }

                if (value is DateTime dateTime)
                {
                    Checked = true;
                    Value = dateTime.Date;
                    return;
                }

                if (DateTime.TryParse(value.ToString(), out DateTime parsed))
                {
                    Checked = true;
                    Value = parsed.Date;
                }
            }
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return Checked ? Value.Date : (object)DBNull.Value;
        }

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            Font = dataGridViewCellStyle.Font;
            CalendarForeColor = dataGridViewCellStyle.ForeColor;
            CalendarMonthBackground = dataGridViewCellStyle.BackColor;
        }

        public int EditingControlRowIndex
        {
            get => _rowIndex;
            set => _rowIndex = value;
        }

        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default:
                    return !dataGridViewWantsInputKey;
            }
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {
        }

        public bool RepositionEditingControlOnValueChange => false;

        public DataGridView EditingControlDataGridView
        {
            get => _dataGridView;
            set => _dataGridView = value;
        }

        public bool EditingControlValueChanged
        {
            get => _valueChanged;
            set => _valueChanged = value;
        }

        public Cursor EditingPanelCursor => base.Cursor;

        protected override void OnValueChanged(EventArgs eventargs)
        {
            _valueChanged = true;
            EditingControlDataGridView?.NotifyCurrentCellDirty(true);
            base.OnValueChanged(eventargs);
        }

        protected override void OnCloseUp(EventArgs eventargs)
        {
            base.OnCloseUp(eventargs);
            _valueChanged = true;
            EditingControlDataGridView?.NotifyCurrentCellDirty(true);
        }
    }
}
