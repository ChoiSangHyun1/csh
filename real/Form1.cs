using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace real
{
    public partial class Form1 : Form
    {
        string ser =
            "server = DESKTOP-16Q2SA2\\MSSQLSERVER1; database = school; uid = sa; password = 123;";
        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        //입력
        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ser))
            {
                conn.Open();
                string sqlstr = @"INSERT INTO student
                                VALUES(@학번, @이름, @나이);";
                SqlCommand cmd = new SqlCommand(sqlstr, conn);
                cmd.Parameters.Add("@학번", SqlDbType.Int);
                cmd.Parameters.Add("@이름", SqlDbType.VarChar);
                cmd.Parameters.Add("@나이", SqlDbType.Int);
                cmd.Parameters["@학번"].Value = textBox1.Text;
                cmd.Parameters["@이름"].Value = textBox2.Text;
                cmd.Parameters["@나이"].Value = textBox3.Text;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return;
                }
            }
            MessageBox.Show(textBox2.Text + "의 학생정보가 입력되었습니다.");
            AddItem();
            ClearTextBox();
        }

        //검색
        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ser);
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT * FROM student;";
            SqlDataReader reader = cmd.ExecuteReader();
            listView1.Items.Clear();
            ReadRow(reader);
            reader.Close();
            conn.Close();
        }

        //삭제
        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ser);
            conn.Open();
            SqlCommand com = new SqlCommand();
            com.CommandText = ("DELETE FROM student WHERE ");
        }

        // 레코드 출력
        private void ReadRow(SqlDataReader reader)
        {
            while (reader.Read())
            {
                string studentNumber = (reader["학번"].ToString());
                string studentName = (reader["이름"].ToString());
                string studentAge = (reader["나이"].ToString());

                string[] str = new string[] { studentNumber, studentName, studentAge };
                ListViewItem listViewItem = new ListViewItem(str);
                listView1.Items.Add(listViewItem);
            }
        }

        // 텍스트박스 클리어
        private void ClearTextBox()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        // 리스트뷰 출력
        private void AddItem()
        {
            string[] box = new string[] { textBox1.Text, textBox2.Text, textBox3.Text };
            ListViewItem listViewItem = new ListViewItem(box);
            listView1.Items.Add(listViewItem);
        }
    }
}