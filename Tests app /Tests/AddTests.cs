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
    public partial class AddTests : Form
    {
        public AddTests()
        {
            InitializeComponent();
        }

        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Задание с открытым ответом!
            //Задание с выбором ответа!
            //Задание на упорядочивание последовательности!
            //Задание на устоновление соответствия!
            switch(comboBoxCategory.SelectedIndex)
            {
                case 0: panelType1.BringToFront(); break;
                case 1: panelType2.BringToFront(); break;
                case 2: panelType3.BringToFront(); break;
                case 3: panelType4.BringToFront(); break;
                default: break;
            }
        }

        int index = 1;
        private void AddInListView()
        {
            ListViewItem listViewItem1 = new ListViewItem(new string[] { index.ToString(), comboBoxCategory.Text, DateTime.Now.ToLongTimeString() });
            listView1.Items.Add(listViewItem1);
            index++;
        }

        private bool WriteDataInDB(string commandText)
        {
            SqlConnection connect = new SqlConnection(Properties.Settings.Default.TestsConnectionString);
            SqlCommand command = new SqlCommand(commandText, connect);
            try
            {
                connect.Open();
                command.ExecuteNonQuery();
                connect.Close();
                return true;
            }
            catch {
                return false;
            }
        }

        private void buttonFourthQuestionAdd_Click(object sender, EventArgs e)
        {
            //Задание на установление соответствия!
            var result = WriteDataInDB(string.Format("insert into questions(Type,Value1) values({0},'{1}')",comboBoxCategory.SelectedIndex,textBox5.Text));
           
            if(result)
                AddInListView();  
        }

        private void buttonThridQuestionAdd_Click(object sender, EventArgs e)
        {
            //Задание на упорядочивание последовательности!
            var result = WriteDataInDB(string.Format("insert into questions(Type,Value1) values({0},'{1}')", comboBoxCategory.SelectedIndex, textBox6.Text));

            if (result)
                AddInListView(); 
        }

        private void buttonFirstQuestionAdd_Click(object sender, EventArgs e)
        {
           // Задание с открытым ответом!
            var result = WriteDataInDB(string.Format("insert into questions(Type,Value1,Value2) values({0},'{1}','{2}')", comboBoxCategory.SelectedIndex, textBox1.Text,textBox2.Text));

            if (result)
                AddInListView(); 
        }

        private void buttonSecondQuestionAdd_Click(object sender, EventArgs e)
        {
            //Задание с выбором ответа!
            var result = WriteDataInDB(string.Format("insert into questions(Type,Value1,Value2) values({0},'{1}','{2}')", comboBoxCategory.SelectedIndex, textBox4.Text, textBoxNewAnswer.Text));

            if (result)
                AddInListView(); 
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddTests_Load(object sender, EventArgs e)
        {

        }
    }
}
