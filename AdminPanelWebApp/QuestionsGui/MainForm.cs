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
        private int showWindowTimeout;
        private int wrongAnswerTimeout;
        private Timer showWindowTimer;
        private int currentQuestionId;

        private async Task RefreshTimeouts()
        {
            showWindowTimeout = await ServiceProxy.ServiceProxy.Instance.GetTimeoutSetting();
            wrongAnswerTimeout = await ServiceProxy.ServiceProxy.Instance.GetWrongAnswerTimeoutSetting();
        }

        public MainForm()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            string address = $"{ConfigurationManager.AppSettings["ServerAddress"]}:{ConfigurationManager.AppSettings["ServerPort"]}";
            ServiceProxy.ServiceProxy.Instance.Init(address);

            showWindowTimer = new Timer();
            showWindowTimer.Tick+=ShowWindowTimerOnTick;
 
        }

        private async void ShowWindowTimerOnTick(object sender, EventArgs e)//f-cja wywoluje sie co zdefiniowany czas w aplikacji
        {
            await RefreshTimeouts();//refresz danych w settings (zmiana interwału przez admina)
            showWindowTimer.Stop();//stopujemy czas i czekamy na reakcje użytkownika
            await RefreshQuestion();//pobiera z serwera pytanie

            panelQuestion.Show();//pokazujemy panel z pytaniem
            panelTimer.Hide();//chowamy panel z timerem
            Show();//pokazuje pytanie
          //  WindowState = FormWindowState.Normal; //stan okna po minięciu czasu            
        }

        private async Task RefreshQuestion()//pobieranie treści pytania
        {
            var result = await ServiceProxy.ServiceProxy.Instance.GetNextQuestion();
            currentQuestionId = result.Id;
            labelQuestionContent.Text = result.QuestionContent;
        }

        private async void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxAnswer.Text))
                return;

            bool result = await ServiceProxy.ServiceProxy.Instance.IsAnswerCorrect(textBoxAnswer.Text, currentQuestionId);
            if (result)
            {
                MessageBox.Show("Prawidlowa");
                this.Hide();
                showWindowTimer.Start();
            }
            else
            {
                panelQuestion.Hide();
                panelTimer.Show();
                //var someTask = Task.Factory.StartNew(async () =>
                //{
                //    await Task.Delay(wrongAnswerTimeout);
                //    WindowState = FormWindowState.Minimized;
                //    showWindowTimer.Start();
                //});
                //await someTask;                
                Func<Task> task = async () =>//typ reprezenujący zadanie które będzie w przyszłości wykonane (f-cja jako zmienna)
                {
                    Timer timerStopWatch = new Timer() {Enabled = true, Interval = 1000};
                    int currentSeconds = wrongAnswerTimeout;
                    RefreshStopWatchLabel(currentSeconds);
                    timerStopWatch.Tick +=
                        (o, args) =>
                        {
                            currentSeconds--;
                            RefreshStopWatchLabel(currentSeconds);
                        };
                    await Task.Delay(wrongAnswerTimeout * 1000); //f-cja przejdzie dalej po odczekaniu zdewiniowanego czasu (wrongAnswerTimeout)
                    timerStopWatch.Dispose();
                    this.Hide();
                    showWindowTimer.Start();
                };
                await task();
            }
        }

        private void RefreshStopWatchLabel(int currentSeconds)
        {
            labelTime.Text = currentSeconds.ToString();
        }

        private async Task tst()
        {
            await Task.Delay(wrongAnswerTimeout);
            WindowState = FormWindowState.Minimized;
            showWindowTimer.Start();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await RefreshTimeouts();

            showWindowTimer.Interval = showWindowTimeout * 1000;//
            showWindowTimer.Enabled = true;

            Hide();
        }

      
        private async void button1_Click(object sender, EventArgs e)
        {
            

            //var result = await ServiceProxy.ServiceProxy.Instance.GetNextQuestion();   
            //id = result.Id;            
            //textBox1.Text = result.QuestionContent;
            //MessageBox.Show(result.ToString());
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            //var result = await ServiceProxy.ServiceProxy.Instance.IsAnswerCorrect(textBoxAnswer.Text, id);
            //if (result == true)
            //    MessageBox.Show("Prawidlowa");
            //else
            //{
            //    MessageBox.Show("Nieprawidlowa");
            //}
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
     

      
    }
}
