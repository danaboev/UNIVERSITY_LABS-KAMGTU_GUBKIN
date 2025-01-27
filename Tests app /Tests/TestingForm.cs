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
    public partial class TestingForm : Form
    {
        List<Questions> listQuest = new List<Questions>();
        Test test;
        User user;
        double point = 0;
        public TestingForm(Test _test,User _user)
        {
            InitializeComponent();
            test = _test;
            user = _user;
            GetFullTest();
            GetQuest();
        }

        private void WriteResult()
        {
            SqlConnection connect = new SqlConnection(Properties.Settings.Default.TestsConnectionString);
            SqlCommand command = new SqlCommand(string.Format("insert into resulttest(idUser,idTest,DateTesting,Result) values({0},{1},'{2}','{3}')",user.ID,test.Index, DateTime.Now.ToString("HH:mm:ss dd:MM:yyyy"),(100.0 * point / dictionary.Count).ToString("#.##")), connect);
            connect.Open();
            command.ExecuteNonQuery();
            connect.Close();
        }
        private void GetFullTest()
        {
            string command = "SELECT * FROM testwithquestions inner join tests ON testwithquestions.idTest=tests.idTests inner join questions on testwithquestions.idQuestion = questions.idQuestions where tests.idTests = " + test.Index.ToString();
            SqlConnection connect = new SqlConnection(Properties.Settings.Default.TestsConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter(string.Format("{0}",command), connect);
            DataTable table = new DataTable();
            adapter.Fill(table);

            for (int i = 0; i < table.Rows.Count; i++)
            {
                listQuest.Add(new Questions
                {
                    CategoryObj = (CategoryQuest)int.Parse(table.Rows[i]["Type"].ToString()),
                    Index = int.Parse(table.Rows[i]["idQuestions"].ToString()),
                    ValueQuestion = table.Rows[i]["Value1"].ToString(),
                    ValueAnswer = table.Rows[i]["Value2"].ToString()
                });
            }
        }
        int index = -1;
        Dictionary<int, ObjQuest> dictionary = new Dictionary<int, ObjQuest>();
        class ObjQuest
        {
            public CategoryQuest _Category { get; set; }
            public object MyClass { get; set; }
        }
        private void GetQuest()
        {
            
            for (int i = 0; i < listQuest.Count; i++)
            {
                switch ((int)listQuest[i].CategoryObj)
                {
                    case 0: dictionary.Add(i, new ObjQuest { _Category = CategoryQuest.Category_1, MyClass = TaskWithOpenAnswer(listQuest[i]) }); break;
                    case 1: dictionary.Add(i, new ObjQuest { _Category =CategoryQuest.Category_2, MyClass =TaskWithSelectedValue(listQuest[i])}); break;
                    case 2: dictionary.Add(i, new ObjQuest { _Category =CategoryQuest.Category_3,MyClass = OrderingSequence(listQuest[i])}); break;
                    case 3: dictionary.Add(i, new ObjQuest { _Category = CategoryQuest.Category_4, MyClass = RelevantIdentified(listQuest[i]) }); break;
                }
            }

            SetQuest();
        }

        private void SetQuest()
        {
            index++;
            if (index == dictionary.Count)
            {
                WriteResult();
                MessageBox.Show(string.Format("Тестируемый: {0} {1} \nРезультат: {2}", user.Name, user.SurName, (100.0 * point / dictionary.Count).ToString("#.##")), "Результат!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Close();
            }
            else
            {
                label5.Text = string.Format("Вопрос: {0}\\{1}", index + 1, dictionary.Count);
                Random r = new Random();
                switch ((int)dictionary[index]._Category)
                {
                    case 0:
                        WithOpenAnswer quest_WithOpenAnswer = (WithOpenAnswer)dictionary[index].MyClass;
                        textBox2.Text = quest_WithOpenAnswer.Question;
                        panel3.BringToFront();
                        break;
                    case 1:
                        WithSelectedValue quest_WithSelectedValue = (WithSelectedValue)dictionary[index].MyClass;
                        checkedListBoxAnswers.Items.Clear();
                        textBox1.Text = quest_WithSelectedValue.Questions;
                        for (int i = 0; i < quest_WithSelectedValue._Quest.Count; i++)
                        {
                            checkedListBoxAnswers.Items.Add(quest_WithSelectedValue._Quest[i].Answer);
                        }
                        panel2.BringToFront();
                        break;
                    case 2:
                        Sequence quest_Sequence = (Sequence)dictionary[index].MyClass;
                        Column2.Items.Clear();
                        dataGridView1.Rows.Clear();
                        var resultMix = quest_Sequence.OrdSeq.OrderBy(x => r.Next()).ToArray();
                        Column2.Items.AddRange(resultMix);

                        for (int i = 0; i < quest_Sequence.OrdSeq.Length; i++)
                        {
                            dataGridView1.Rows.Add((i + 1).ToString());
                        }

                        panel4.BringToFront();
                        break;

                    case 3:
                        dataGridView2.Rows.Clear();
                        RelevantId quest_RelevantId = (RelevantId)dictionary[index].MyClass;
                        Column3.Items.Clear();
                        Column4.Items.Clear();
                        var resultMix2 = quest_RelevantId.Listfirst.OrderBy(x => r.Next()).ToArray();
                        var resultMix3 = quest_RelevantId.ListSecond.OrderBy(x => r.Next()).ToArray();
                        dataGridView2.Rows.Add();
                        dataGridView2.Rows.AddCopies(0, resultMix2.Length - 1);
                        Column3.Items.AddRange(resultMix2);
                        Column4.Items.AddRange(resultMix3);
                        panel5.BringToFront();
                        break;
                }
            }
        }

        /// <summary>
        /// Задание с открытым ответом ///B -scheme
        /// </summary>
        private WithOpenAnswer TaskWithOpenAnswer(Questions quest)
        {
            return new WithOpenAnswer
            {
                ID = quest.Index,
                Answer = quest.ValueAnswer,
                Question = quest.ValueQuestion
            };
        }
        /// <summary>
        /// Задание с выбором ответа
        /// </summary>
        private WithSelectedValue TaskWithSelectedValue(Questions quest)
        {
           
            var array = quest.ValueAnswer.Split(new char[] {'\n','\r'}, StringSplitOptions.RemoveEmptyEntries);
            var listt = new List<_Value>();

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i][0] == '+')
                    listt.Add(new _Value { Answer = array[i].Remove(0, 1), Valid = true });
                else
                    listt.Add(new _Value { Answer = array[i].Remove(0, 1), Valid = false });
            }
            return new WithSelectedValue
            {
                ID = quest.Index,
                Questions = quest.ValueQuestion,
                _Quest = listt
            };
        }
       
        /// <summary>
        /// Задание на упорядочивание последовательности
        /// </summary>
        private Sequence OrderingSequence(Questions quest)
        {
            var array = quest.ValueQuestion.Split(new char[] { '\n', '\r' },StringSplitOptions.RemoveEmptyEntries).ToArray();
          
            return new Sequence
            {
                OrdSeq = array
            };
        }
       

        /// <summary>
        /// Задание на установление соответствия
        /// </summary>
        /// <param name="quest"></param>
        private RelevantId RelevantIdentified(Questions quest)
        {
            List<string> list1 = new List<string>();
            List<string> list2 = new List<string>();
            
            var array1 = quest.ValueQuestion.Split(new char[] { '\n', '\r' },StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < array1.Length; i++)
            {
                var a = array1[i].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                list1.Add(a[0]);
                list2.Add(a[1]);
            }

            return new RelevantId
            {
                ID = quest.Index,
                Listfirst = list1,
                ListSecond = list2
            };
        }
        
        private void buttonFourthQuestion_Click(object sender, EventArgs e)
        {
            // Задание на установление соответствия  //
            ///scheme Е -\/ снизу
            ///
            var _cl = ((RelevantId)dictionary[index].MyClass);

            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].Cells[0].Value != null && dataGridView2.Rows[i].Cells[1].Value != null)
                {
                    var first = dataGridView2.Rows[i].Cells[0].Value.ToString();
                    var second = dataGridView2.Rows[i].Cells[1].Value.ToString();

                    var res = _cl.Listfirst.IndexOf(first);
                    if (!(_cl.ListSecond[res] == second))
                        break;
                    if (i == dataGridView2.Rows.Count - 1)
                        point++;
                }
                else
                    break;
            }

            CheckPoint();
            SetQuest();
        }

        private void buttonThridQuestion_Click(object sender, EventArgs e)
        {//Задание на упорядочивание последовательности ///Д

            var _cl = ((Sequence)dictionary[index].MyClass);
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (!(dataGridView1.Rows[i].Cells[1].Value == _cl.OrdSeq[i]))
                    break;
                if (i == dataGridView1.Rows.Count - 1)
                    point++;
            }
            CheckPoint();
            SetQuest();
        }
        private void buttonSecondQuestion_Click(object sender, EventArgs e)
        {//Задание с выбором ответа/// Г
            
            var _cl = ((WithSelectedValue)dictionary[index].MyClass);

            for (int i = 0; i < checkedListBoxAnswers.CheckedItems.Count; i++)
            {
                if (!_cl._Quest.Find(x => x.Answer == checkedListBoxAnswers.Items[checkedListBoxAnswers.CheckedIndices[i]]).Valid)
                {
                    break;
                }
                if(i==checkedListBoxAnswers.CheckedItems.Count-1)
                    point++;

            }

            CheckPoint();
            SetQuest();
        }

        private void buttonFirstQuestion_Click(object sender, EventArgs e)
        {// Задание с открытым ответом /// Г
            
            if (textBox3.Text.ToLower() == ((WithOpenAnswer)dictionary[index].MyClass).Answer.ToLower())
                point++;

            CheckPoint();
            SetQuest();                
        }
        private void CheckPoint()
        {
            label6.Text = string.Format("Ваш результат: {0}  %", (100.0 * point / dictionary.Count).ToString("#.##"));
        }
    }
    class WithOpenAnswer
    {
        public int ID { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
    class WithSelectedValue
    {
        public int ID { get; set; }
        public string Questions { get; set; }
        public List<_Value> _Quest { get; set; }

    }
    class _Value
    {
        public string Answer { get; set; }
        public bool Valid { get; set; }
    }
    class Sequence
    {
        public string[] OrdSeq { get; set; }
    }
    class RelevantId
    {
        public int ID { get; set; }
        public List<string> Listfirst { get; set; }
        public List<string> ListSecond { get; set; }
    }
}
