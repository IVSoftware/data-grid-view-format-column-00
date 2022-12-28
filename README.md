A DataGridView Column can be formatted using the Column.DefaultCellStyle.Format property (e.g. "F2"). The underlying data stays at full resolution. This can be observed by hovering over a cell to see the underlying value to (in this case) four places.

![cell-with-hover](https://github.com/IVSoftware/data-grid-view-format-column-00/blob/master/data-grid-view-format-column/Screenshots/cell-with-hover.png)

***
**Data grid view with binding.**

    public partial class MainForm : Form
    {
        public MainForm() => InitializeComponent();
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            dataGridView.DataSource = Rows;
            Rows.Add(new MyRow());
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.DefaultCellStyle.Format = "F2";
            }
            // Show full resolution in editing control.
            dataGridView.EditingControlShowing += (sender, e) =>
            {
                var value = dataGridView.CurrentCell.Value;
                if (value != null)
                {
                    dataGridView.EditingControl.Text = $"{value}";
                }
            };
            dataGridView.KeyDown += (sender, e) =>
            {
                if(e.KeyData.Equals(Keys.Control | Keys.C ))
                {
                    e.SuppressKeyPress = e.Handled = true;
                    copyAllToClipboard();
                }
            };
        }
        BindingList<MyRow> Rows = new BindingList<MyRow>();
    }

***
 
When a cell goes into edit mode, an EditingControl is superimposed on the cell. In the above code, the `EditingControlShowing` event is handled so that the full resolution is visible in that control.

![cell-with-hover](https://github.com/IVSoftware/data-grid-view-format-column-00/blob/master/data-grid-view-format-column/Screenshots/cell-active-edit.png)

***
To copy-paste it (for example to an Excel spreadsheet) iterate the cells to create a tab-separated values string.

    private void copyAllToClipboard()
    {
        List<string>
            rowBuilder = new List<string>(),
            columnBulder = new List<string>();

        for (int row = 0; row < dataGridView.RowCount; row++)
        {
            for (int col = 0; col < dataGridView.ColumnCount; col++)
            {
                var cell = dataGridView[col, row];
                columnBulder.Add(cell.Value == null ? string.Empty : $"{cell.Value}");
            }
            rowBuilder.Add(string.Join("\t", columnBulder));
            columnBulder.Clear();
        }
        var tsv = string.Join(Environment.NewLine, rowBuilder);
        Clipboard.SetText(tsv);
    }


![cell-with-hover](https://github.com/IVSoftware/data-grid-view-format-column-00/blob/master/data-grid-view-format-column/Screenshots/excel-copy-paste.png)

 ***
 This class represents a row with two columns.

    class MyRow
    {
        public decimal ValueA { get; set; }
        public decimal ValueB { get; set; }
    }

