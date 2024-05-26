using System;
using System.Windows.Forms;

namespace Pr_Controls
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeTreeView();
            InitializeListView();
            InitializeContextMenu();
            InitializeMenu();
        }

        private void InitializeTreeView()
        {
            TreeNode semester1 = new TreeNode("Semester 1");
            semester1.Nodes.Add("Mathematics");
            semester1.Nodes.Add("Physics");
            semester1.Nodes.Add("Chemistry");

            TreeNode semester2 = new TreeNode("Semester 2");
            semester2.Nodes.Add("Biology");
            semester2.Nodes.Add("History");
            semester2.Nodes.Add("Computer Science");

            treeView1.Nodes.Add(semester1);
            treeView1.Nodes.Add(semester2);
        }

        private void InitializeListView()
        {
            listView1.View = View.Details;
            listView1.Columns.Add("Group Number", 150);
            listView1.Columns.Add("Student Name", 150);
            listView1.FullRowSelect = true;
            listView1.GridLines = true;
        }

        private void InitializeContextMenu()
        {
            // Добавляем пункты меню в contextMenuStrip1
            ToolStripMenuItem colorMenuItem = new ToolStripMenuItem("Цвет текста");
            ToolStripMenuItem fontMenuItem = new ToolStripMenuItem("Шрифт");

            // Привязываем обработчики событий
            colorMenuItem.Click += new EventHandler(цветТекстаToolStripMenuItem_Click);
            fontMenuItem.Click += new EventHandler(шрифтToolStripMenuItem_Click);

            // Добавляем пункты меню в contextMenuStrip1
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { colorMenuItem, fontMenuItem });

            // Привязываем contextMenuStrip1 к label1
            label1.ContextMenuStrip = contextMenuStrip1;
        }

        private void InitializeMenu()
        {
            // Создаем меню
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("Файл");
            ToolStripMenuItem exitMenuItem = new ToolStripMenuItem("Выход");
            exitMenuItem.Click += new EventHandler(выходToolStripMenuItem_Click);
            fileMenu.DropDownItems.Add(exitMenuItem);

            ToolStripMenuItem dateMenu = new ToolStripMenuItem("Работа с датами");
            ToolStripMenuItem clockMenuItem = new ToolStripMenuItem("Часы");
            clockMenuItem.Click += new EventHandler(часыToolStripMenuItem_Click);
            ToolStripMenuItem remainingTimeMenuItem = new ToolStripMenuItem("Расчет остатка времени");
            remainingTimeMenuItem.Click += new EventHandler(расчетостаткаВремениToolStripMenuItem_Click);
            dateMenu.DropDownItems.Add(clockMenuItem);
            dateMenu.DropDownItems.Add(remainingTimeMenuItem);

            // Добавляем пункты меню в menuStrip1
            menuStrip1.Items.Add(fileMenu);
            menuStrip1.Items.Add(dateMenu);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            MessageBox.Show("You selected: " + e.Node.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string groupNumber = maskedTextBox1.Text.Trim();
            string studentName = maskedTextBox2.Text.Trim();

            if (!string.IsNullOrEmpty(groupNumber) && !string.IsNullOrEmpty(studentName))
            {
                ListViewGroup group = null;
                foreach (ListViewGroup g in listView1.Groups)
                {
                    if (g.Header == groupNumber)
                    {
                        group = g;
                        break;
                    }
                }

                if (group == null)
                {
                    group = new ListViewGroup(groupNumber, groupNumber);
                    listView1.Groups.Add(group);
                }

                ListViewItem item = new ListViewItem(new string[] { groupNumber, studentName });
                item.Group = group;
                listView1.Items.Add(item);

                maskedTextBox1.Clear();
                maskedTextBox2.Clear();
            }
            else
            {
                MessageBox.Show("Please enter both group number and student name.");
            }
        }

        // Обработчик для пункта "Цвет текста" в контекстном меню
        private void цветТекстаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                label1.ForeColor = colorDialog1.Color;
            }
        }

        // Обработчик для пункта "Шрифт" в контекстном меню
        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                label1.Font = fontDialog1.Font;
            }
        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {
            // Optional: Add code here if needed for the Apply event of fontDialog1
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {
            // Optional: Add code here if needed for the Popup event of toolTip1
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Optional: Add code here if needed for the Click event of label1
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Optional: Add code here if needed for the Opening event of contextMenuStrip1
        }

        // Обработчик для пункта "Выход" в меню
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Обработчик для пункта "Часы" в меню
        private void часыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Tick += delegate {
                label2.Text = DateTime.Now.TimeOfDay.ToString() + " далее время по частям: " +
                              DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second.ToString();
            };
        }

        // Обработчик для пункта "Расчет остатка времени" в меню
        private void расчетостаткаВремениToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            DateTime dt1 = dateTimePicker1.Value; // выбранное время
            label2.Text = "до выбранной даты осталось дней: " + (dt1.DayOfYear - DateTime.Now.DayOfYear).ToString();
        }

        // Недостающие обработчики
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // Ваш код для обработки изменения даты здесь
        }

        private void label2_Click(object sender, EventArgs e)
        {
            // Ваш код для обработки клика по метке здесь
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // Ваш код для обработки клика по пункту меню здесь
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Ваш код для обработки тика таймера здесь
        }
    }
}
