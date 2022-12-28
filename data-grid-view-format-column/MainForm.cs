using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace data_grid_view_format_column
{
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
        BindingList<MyRow> Rows = new BindingList<MyRow>();
    }
    class MyRow
    {
        public decimal ValueA { get; set; }
        public decimal ValueB { get; set; }
    }
}