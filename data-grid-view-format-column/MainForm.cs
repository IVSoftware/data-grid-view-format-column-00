using System.ComponentModel;

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
        }
        BindingList<MyRow> Rows = new BindingList<MyRow>();
    }
    class MyRow
    { 
        public decimal ValueA { get; set; }
        public decimal ValueB { get; set; }
    }
}