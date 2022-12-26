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
    class MyRow : INotifyPropertyChanged
    {
        decimal _ValueA = 0;
        public decimal ValueA
        {
            get => _ValueA;
            set
            {
                if (!Equals(_ValueA, value))
                {
                    _ValueA = value;
                    OnPropertyChanged();
                }
            }
        }

        decimal _ValueB = 0;
        public decimal ValueB
        {
            get => _ValueB;
            set
            {
                if (!Equals(_ValueB, value))
                {
                    _ValueB = value;
                    OnPropertyChanged();
                }
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}