using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace SkinCare
{
    public partial class QuizForm : Form
    {
        private User currentUser; // بيانات المسخدم من كلاس فورم 1
        private int currentQuestion = 0;
        private int[] answers = new int[8]; 
        private Label lblProgress;
        private Label lblQuestion;
        private Button[] optionButtons = new Button[4];
        private Button btnBack;
        private ProgressBar progressBar; // شريط التقدم للاختبار
        private FlowLayoutPanel flowLayout;

        private string[] questions = {
            "بعد غسل وجهك بساعتين، كيف تشعرين ببشرتك؟",
            "كيف تبدو مسام وجهك؟",
            "هل تعانين من احمرار أو حساسية؟",
            "كيف يبدو وجهك بعد الاستيقاظ صباحاً؟",
            "كيف تصفين بشرتك بشكل عام؟",
            "كيف بشرتك في فصل الشتاء؟",
            "هل تظهر حبوب على وجهك؟",
            "كيف بشرتك بعد ممارسة الرياضة؟"
        };

        private string[][] options = {
            new[] { "لامع وزيتي جداً", "لامع قليلاً في الجبهة", "طبيعي ومريح", "جاف ومشدود" },
            new[] { "كبيرة وواضحة جداً", "واضحة في منطقة T", "متوسطة وطبيعية", "صغيرة جداً" },
            new[] { "نعم دائماً وبسهولة", "أحياناً مع بعض المنتجات", "نادراً", "لا أبداً" },
            new[] { "لامع ودهني كثيراً", "لامع في المنتصف فقط", "طبيعي ومشرق", "جاف ومتقشر" },
            new[] { "دهنية ولامعة دائماً", "مختلطة دهنية ومناطق جافة", "طبيعية ومتوازنة", "جافة أو حساسة" },
            new[] { "دهنية أكثر من المعتاد", "لا تتغير كثيراً", "تصبح أكثر توازناً", "جافة جداً ومتشققة" },
            new[] { "نعم باستمرار وكثيرة", "أحياناً في منطقة T", "نادراً جداً", "لا تقريباً أبداً" },
            new[] { "لامع جداً وزيتي", "لامع في بعض المناطق", "طبيعي ومنتعش", "جاف ومتشنج" }
        };

        private int[][] scores = {
            new[] { 4, 3, 2, 1 }, new[] { 4, 3, 2, 1 }, new[] { 4, 3, 2, 1 }, new[] { 4, 3, 2, 1 },
            new[] { 4, 3, 2, 1 }, new[] { 4, 3, 2, 1 }, new[] { 4, 3, 2, 1 }, new[] { 4, 3, 2, 1 }
        };

        public QuizForm(User user)
        {
            InitializeComponent();
            currentUser = user;
            SetupForm();
            ShowQuestion(0);
        }

        private void SetupForm()
        {
            this.Text = "اختبار نوع البشرة";
            this.Size = new Size(650, 580);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(255, 245, 240);

            flowLayout = new FlowLayoutPanel();
            flowLayout.Dock = DockStyle.Fill;
            flowLayout.FlowDirection = FlowDirection.TopDown;
            flowLayout.Padding = new Padding(40);
            flowLayout.WrapContents = false;
            flowLayout.AutoScroll = true;

            progressBar = new ProgressBar();
            progressBar.Minimum = 0;
            progressBar.Maximum = 8;
            progressBar.Size = new Size(560, 18);

            lblProgress = new Label();
            lblProgress.Font = new Font("Segoe UI", 10);
            lblProgress.ForeColor = Color.FromArgb(150, 100, 110);
            lblProgress.AutoSize = true;

            lblQuestion = new Label();
            lblQuestion.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblQuestion.ForeColor = Color.FromArgb(100, 50, 70);
            lblQuestion.Size = new Size(560, 60);

            for (int i = 0; i < 4; i++)
            {
                optionButtons[i] = new Button();
                optionButtons[i].Font = new Font("Segoe UI", 11);
                optionButtons[i].BackColor = Color.White;
                optionButtons[i].ForeColor = Color.FromArgb(80, 50, 60);
                optionButtons[i].FlatStyle = FlatStyle.Flat;
                optionButtons[i].Size = new Size(560, 50);
                optionButtons[i].TextAlign = ContentAlignment.MiddleLeft;
                optionButtons[i].FlatAppearance.BorderColor = Color.FromArgb(220, 190, 200);
                int index = i;
                optionButtons[i].Click += (s, e) => OptionSelected(index);
            }

            btnBack = new Button();
            btnBack.Text = "رجوع";
            btnBack.Font = new Font("Segoe UI", 10);
            btnBack.BackColor = Color.FromArgb(240, 220, 225);
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.Size = new Size(110, 38);
            btnBack.Visible = false;
            btnBack.Click += new EventHandler(BtnBack_Click);

            flowLayout.Controls.Add(progressBar);
            flowLayout.Controls.Add(lblProgress);
            flowLayout.Controls.Add(lblQuestion);
            for (int i = 0; i < 4; i++)
                flowLayout.Controls.Add(optionButtons[i]);
            flowLayout.Controls.Add(btnBack);

            this.Controls.Add(flowLayout);
        }

        private void ShowQuestion(int index)
        {
            currentQuestion = index;
            lblProgress.Text = "السؤال " + (index + 1) + " من 8";
            progressBar.Value = index;
            lblQuestion.Text = questions[index];
            btnBack.Visible = index > 0;

            for (int i = 0; i < 4; i++)
            {
                optionButtons[i].Text = "  " + options[index][i];
                optionButtons[i].BackColor = Color.White;
                optionButtons[i].ForeColor = Color.FromArgb(80, 50, 60);
            }
        }

        private void OptionSelected(int optionIndex)
        {
            optionButtons[optionIndex].BackColor = Color.FromArgb(220, 130, 150);
            optionButtons[optionIndex].ForeColor = Color.White;
            answers[currentQuestion] = scores[currentQuestion][optionIndex];
            currentUser.SetAnswer(currentQuestion, answers[currentQuestion]);
            System.Threading.Thread.Sleep(300);

            if (currentQuestion < questions.Length - 1)
                ShowQuestion(currentQuestion + 1);
            else
            {
                SkinAnalyzer analyzer = new SkinAnalyzer();
                string skinTypeKey = analyzer.AnalyzeSkin(answers);
                currentUser.SetDetectedSkinType(analyzer.CreateSkinType(skinTypeKey));
                ResultForm resultForm = new ResultForm(currentUser);
                resultForm.Show();
                this.Close();
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (currentQuestion > 0)
                ShowQuestion(currentQuestion - 1);
        }

        private void QuizForm_Load(object sender, EventArgs e)
        {

        }
    }
}