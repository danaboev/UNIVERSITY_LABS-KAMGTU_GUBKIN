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
    public partial class CreateTest : Form
    {
        List<Questions> list = new List<Questions>();
        User user;
        public CreateTest(User _user)
        {
            InitializeComponent();
            this.user = _user;

            if (user.UserRole == Role.user)
                buttonAddQuestion.Enabled = false;
            ReadQuestions();
        }
        private void ReadQuestions()
        {
            checkedListBoxQuestions.Items.Clear();
            list.Clear();
            SqlConnection connect = new SqlConnection(Properties.Settings.Default.TestsConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter("select * from questions", connect);
            DataTable table = new DataTable();
            adapter.Fill(table);

            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add(
                    new Questions
                    {
                        CategoryObj = (CategoryQuest)int.Parse(table.Rows[i]["Type"].ToString()),
                        Index = int.Parse(table.Rows[i]["idQuestions"].ToString()),
                        ValueQuestion = table.Rows[i]["Value1"].ToString(),
                        ValueAnswer = table.Rows[i]["Value1"].ToString()
                    });
            }
            list.OrderBy(x => x.CategoryObj);

            foreach (var item in list)
            {
                checkedListBoxQuestions.Items.Add(string.Format("({0}) {1}",item.CategoryObj,item.ValueQuestion));
            }

        }

        private void buttonAddQuestion_Click(object sender, EventArgs e)
        {
            AddTests addQuestions = new AddTests();
            addQuestions.ShowDialog();

            ReadQuestions();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connect = new SqlConnection(Properties.Settings.Default.TestsConnectionString);
                SqlCommand command = new SqlCommand(string.Format("insert into tests(About,idUser) values('{0}',{1}); SELECT CAST(scope_identity() AS int)", textBox1.Text, user.ID), connect);
                connect.Open();
                var idTest = (int)command.ExecuteScalar();
                connect.Close();
                
                List<string> values = new List<string>();

                for (int i = 0; i < checkedListBoxQuestions.CheckedItems.Count; i++)
                {
                    values.Add(string.Format("({0},{1})", list[checkedListBoxQuestions.CheckedIndices[i]].Index, idTest));
                }

                var result = string.Join(",", values);

                command = new SqlCommand(string.Format("insert into testwithquestions(idQuestion,idTest) values {0}", result), connect);
                connect.Open();
                command.ExecuteNonQuery();
                connect.Close();
                MessageBox.Show("Создание прошло успешно!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch
            {
                MessageBox.Show("При создании возникла ошибка!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CreateTest_Load(object sender, EventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

       
    }
}
