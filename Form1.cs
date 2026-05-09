using System;
using System.Drawing;
using System.Windows.Forms;

namespace SkinCare
{
    public partial class Form1 : Form
    {
        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblGirl;
        private Label lblName;
        private TextBox txtName;
        private Label lblAge;
        private TextBox txtAge;
        private Button btnStart;
        private TableLayoutPanel tableLayout;

        public Form1()
        {
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            this.Text = "Skin Care Analyzer";
            this.Size = new Size(600, 520);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(255, 245, 240);

            tableLayout = new TableLayoutPanel();
            tableLayout.ColumnCount = 2;
            tableLayout.RowCount = 7;
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.Padding = new Padding(60, 30, 60, 40);
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 35));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40));
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));

            lblTitle = new Label();
            lblTitle.Text = "Skin Care Analyzer";
            lblTitle.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(180, 100, 120);
            lblTitle.AutoSize = true;

            lblSubtitle = new Label();
            lblSubtitle.Text = "اكتشفي نوع بشرتك والمنتجات المناسبة";
            lblSubtitle.Font = new Font("Segoe UI", 11);
            lblSubtitle.ForeColor = Color.FromArgb(130, 80, 90);
            lblSubtitle.AutoSize = true;

            lblGirl = new Label();
            lblGirl.Text = "I'm just a girl 🎀";
            lblGirl.Font = new Font("Segoe UI", 12, FontStyle.Italic);
            lblGirl.ForeColor = Color.FromArgb(180, 130, 150);
            lblGirl.AutoSize = true;

            lblName = new Label();
            lblName.Text = "الاسم:";
            lblName.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblName.AutoSize = true;
            lblName.Anchor = AnchorStyles.Left;

            txtName = new TextBox();
            txtName.Font = new Font("Segoe UI", 11);
            txtName.Dock = DockStyle.Fill;

            lblAge = new Label();
            lblAge.Text = "العمر:";
            lblAge.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            lblAge.AutoSize = true;
            lblAge.Anchor = AnchorStyles.Left;

            txtAge = new TextBox();
            txtAge.Font = new Font("Segoe UI", 11);
            txtAge.Dock = DockStyle.Fill;

            btnStart = new Button();
            btnStart.Text = "ابدأي الاختبار ✨";
            btnStart.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            btnStart.BackColor = Color.FromArgb(220, 130, 150);
            btnStart.ForeColor = Color.White;
            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.Size = new Size(200, 45);
            btnStart.Anchor = AnchorStyles.None;
            btnStart.Click += new EventHandler(BtnStart_Click);

            tableLayout.SetColumnSpan(lblTitle, 2);
            tableLayout.SetColumnSpan(lblSubtitle, 2);
            tableLayout.SetColumnSpan(lblGirl, 2);
            tableLayout.SetColumnSpan(btnStart, 2);

            tableLayout.Controls.Add(lblTitle, 0, 0);
            tableLayout.Controls.Add(lblSubtitle, 0, 1);
            tableLayout.Controls.Add(lblGirl, 0, 2);
            tableLayout.Controls.Add(lblName, 0, 3);
            tableLayout.Controls.Add(txtName, 1, 3);
            tableLayout.Controls.Add(lblAge, 0, 4);
            tableLayout.Controls.Add(txtAge, 1, 4);
            tableLayout.Controls.Add(btnStart, 0, 6);

            this.Controls.Add(tableLayout);
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("من فضلك اكتبي اسمك!", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(txtAge.Text, out int age) || age < 10 || age > 100)
            {
                MessageBox.Show("من فضلك اكتبي عمراً صحيحاً!", "تنبيه",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            User user = new User(txtName.Text.Trim(), age);
            QuizForm quizForm = new QuizForm(user);
            quizForm.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}