using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServiceProxy;

namespace QuestionsGui
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            string address = $"{ConfigurationManager.AppSettings["ServerAddress"]}:{ConfigurationManager.AppSettings["ServerPort"]}";
            ServiceProxy.ServiceProxy.Instance.Init(address);
        }

        private int id;
        private async void button1_Click(object sender, EventArgs e)
        {

            var result = await ServiceProxy.ServiceProxy.Instance.GetNextQuestion();   
            id = result.Id;            
            textBox1.Text = result.QuestionContent;
            //MessageBox.Show(result.ToString());
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var result = await ServiceProxy.ServiceProxy.Instance.IsAnswerCorrect(textBox2.Text, id);
            if (result == true)
                MessageBox.Show("Prawidlowa");
            else
            {
                MessageBox.Show("Nieprawidlowa");
            }
            //var result = await ServiceProxy.ServiceProxy.Instance.GetNextQuestion();

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {

        }
    }
}
