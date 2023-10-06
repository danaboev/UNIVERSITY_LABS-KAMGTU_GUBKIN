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
    public partial class AuthForm : Form
    {
        User user;
        public AuthForm(User _user)
        {
            InitializeComponent();
            this.user = _user;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void buttonJoin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxLogin.Text) && !string.IsNullOrWhiteSpace(textBoxPassword.Text))
            {
                SqlConnection connect = new SqlConnection(Properties.Settings.Default.TestsConnectionString);
                SqlDataAdapter adapter = new SqlDataAdapter(string.Format("select Name,SurName,Login,Role,idUsers from Users where Login = '{0}' and Password = '{1}'",textBoxLogin.Text,textBoxPassword.Text), connect);
                DataTable table = new DataTable();
                adapter.Fill(table);
                if(table.Rows.Count!=0)
                {
                    user.ID = int.Parse(table.Rows[0]["idUsers"].ToString());
                    user.Name = table.Rows[0]["Name"].ToString();
                    user.SurName = table.Rows[0]["SurName"].ToString();
                    user.Login = table.Rows[0]["Login"].ToString();
                    user.UserRole = (Role)int.Parse(table.Rows[0]["Role"].ToString());

                    Close();
                }
                else
                    MessageBox.Show("Логин или пароль неверны!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Поля логин и пароль не могут быть пустыми!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void linkLabelRegistration_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Registration reg = new Registration();
            reg.ShowDialog();
        }

     
    }
}
