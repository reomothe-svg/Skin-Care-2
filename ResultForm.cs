using System;
using System.Drawing;
using System.Windows.Forms;

namespace SkinCare
{
    public partial class ResultForm : Form
    {
        private User currentUser;
        private TableLayoutPanel mainLayout;
        private TabControl tabControl;

        public ResultForm(User user)
        {
            InitializeComponent();
            currentUser = user;
            SetupForm();
        }

        private void SetupForm()
        {
            this.Text = "نتيجة تحليل بشرتك";
            this.Size = new Size(680, 620);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(255, 245, 240);

            mainLayout = new TableLayoutPanel();
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.RowCount = 3;
            mainLayout.ColumnCount = 1;
            mainLayout.Padding = new Padding(20);
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            FlowLayoutPanel headerPanel = new FlowLayoutPanel();
            headerPanel.FlowDirection = FlowDirection.TopDown;
            headerPanel.AutoSize = true;
            headerPanel.Dock = DockStyle.Fill;

            Label lblHello = new Label();
            lblHello.Text = "مرحباً " + currentUser.GetName() + "! 🌸";
            lblHello.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblHello.ForeColor = Color.FromArgb(180, 100, 120);
            lblHello.AutoSize = true;

            Label lblSkin = new Label();
            lblSkin.Text = currentUser.GetDetectedSkinType().GetEmoji() + " نوع بشرتك: " + currentUser.GetDetectedSkinType().GetTypeName();
            lblSkin.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblSkin.ForeColor = Color.FromArgb(140, 70, 90);
            lblSkin.AutoSize = true;

            headerPanel.Controls.Add(lblHello);
            headerPanel.Controls.Add(lblSkin);

            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Segoe UI", 11);

            // Tab 1 - النتيجة
            TabPage tabResult = new TabPage("نتيجتك");
            RichTextBox txtDesc = new RichTextBox();
            txtDesc.Text = "نوع بشرتك: " + currentUser.GetDetectedSkinType().GetTypeName() + "\n\n" +
                           currentUser.GetDetectedSkinType().GetDescription() + "\n\n" +
                           "العمر: " + currentUser.GetAge() + " سنة";
            txtDesc.Font = new Font("Segoe UI", 12);
            txtDesc.ReadOnly = true;
            txtDesc.BorderStyle = BorderStyle.None;
            txtDesc.Dock = DockStyle.Fill;
            tabResult.Controls.Add(txtDesc);

            // Tab 2 - المنتجات
            TabPage tabProducts = new TabPage("المنتجات");
            RichTextBox txtProducts = new RichTextBox();
            txtProducts.Font = new Font("Segoe UI", 11);
            txtProducts.ReadOnly = true;
            txtProducts.BorderStyle = BorderStyle.None;
            txtProducts.Dock = DockStyle.Fill;
            Product[] products = currentUser.GetDetectedSkinType().GetRecommendedProducts();
            txtProducts.Text = "المنتجات الموصى بها:\n\n";
            for (int i = 0; i < products.Length; i++)
                txtProducts.Text += "🧴 " + products[i].GetName() + "\n" +
                      "   النوع: " + products[i].GetCategory() + "\n" +
                      "   " + products[i].GetDescription() + "\n" +
                      "   السعر: $" + products[i].GetPrice() + "\n\n";
            tabProducts.Controls.Add(txtProducts);

            // Tab 3 - الروتين
            TabPage tabRoutine = new TabPage("الروتين");
            RichTextBox txtRoutine = new RichTextBox();
            txtRoutine.Font = new Font("Segoe UI", 11);
            txtRoutine.ReadOnly = true;
            txtRoutine.BorderStyle = BorderStyle.None;
            txtRoutine.Dock = DockStyle.Fill;
            txtRoutine.Text = "روتين العناية المقترح:\n\n";
            string[] routine = currentUser.GetDetectedSkinType().GetSkinCareRoutine();
            for (int i = 0; i < routine.Length; i++)
                txtRoutine.Text += routine[i] + "\n\n";
            tabRoutine.Controls.Add(txtRoutine);

            // Tab 4 - النصائح
            TabPage tabTips = new TabPage("نصائح");
            RichTextBox txtTips = new RichTextBox();
            txtTips.Text = currentUser.GetDetectedSkinType().GetSkinTips();
            txtTips.Font = new Font("Segoe UI", 11);
            txtTips.ReadOnly = true;
            txtTips.BorderStyle = BorderStyle.None;
            txtTips.Dock = DockStyle.Fill;
            tabTips.Controls.Add(txtTips);

            tabControl.TabPages.AddRange(new TabPage[] { tabResult, tabProducts, tabRoutine, tabTips });

            Button btnRestart = new Button();
            btnRestart.Text = "اعادة الاختبار";
            btnRestart.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnRestart.BackColor = Color.FromArgb(220, 130, 150);
            btnRestart.ForeColor = Color.White;
            btnRestart.FlatStyle = FlatStyle.Flat;
            btnRestart.FlatAppearance.BorderSize = 0;
            btnRestart.Size = new Size(200, 42);
            btnRestart.Anchor = AnchorStyles.None;
            btnRestart.Click += (s, e) => { new Form1().Show(); this.Close(); };

            mainLayout.Controls.Add(headerPanel, 0, 0);
            mainLayout.Controls.Add(tabControl, 0, 1);
            mainLayout.Controls.Add(btnRestart, 0, 2);

            this.Controls.Add(mainLayout);
        }

        private void ResultForm_Load(object sender, EventArgs e)
        {

        }
    }
}