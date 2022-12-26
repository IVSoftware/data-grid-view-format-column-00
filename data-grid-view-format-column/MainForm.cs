using System.ComponentModel;
using System.Runtime.CompilerServices;

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
            dataGridView.EditingControlShowing += (sender, e) =>
            {
                if(dataGridView.CurrentCell.Value is decimal d)
                {
                    dataGridView.EditingControl.Text = $"{d}";
                }
            };
        }
        BindingList<MyRow> Rows = new BindingList<MyRow>();
    }
    class MyRow
    {
        public decimal ValueA { get; set; }
        public decimal ValueB { get; set; }
    }
}