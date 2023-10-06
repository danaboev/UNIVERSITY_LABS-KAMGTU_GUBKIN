using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tests
{
    public partial class FormMain : Form
    {
        User user = new User();
        public FormMain()
        {
            InitializeComponent();
            AuthForm auth = new AuthForm(user);
            auth.ShowDialog();
            toolStripStatusLabel1.Text = string.Format("{0} {1} ({2})",user.Name,user.SurName,user.UserRole);
            FillTable();
        }

        private void FillTable()
        {
            dataGridView1.DataSource = null;
            SqlConnection connect = new SqlConnection(Properties.Settings.Default.TestsConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(string.Format("SELECT About,Result,DateTesting FROM resulttest inner join tests on resulttest.idTest=tests.idTests inner join users on resulttest.idUser = users.idUsers where idUsers={0}", user.ID), connect);
            DataTable table = new DataTable();
            adapter.Fill(table);

            dataGridView1.DataSource = table.DefaultView;
           // dataGridView1.Columns["idUsers"].Visible = false;
        }

        private void createTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateTest createTest = new CreateTest(user);
            createTest.ShowDialog();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectTest selectTest = new SelectTest(user);
            selectTest.ShowDialog();
            FillTable();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
             

        
    }
}
