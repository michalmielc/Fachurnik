using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Fachurnik
{
    public partial class catalogForm : Form
    {
        Description description = new Description();
        Validator validator = new Validator();
        string fileName = "brak pliku";

        public catalogForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void catalogForm_Shown(object sender, EventArgs e)
        {
            // KOD DO POPRAWY

            string directoryPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\..\\..\\CatalogDatabase\\";
            string[] files = Directory.GetFiles(directoryPath, "*.csv", SearchOption.AllDirectories);
            string columns;

            int i = 0;
            foreach (var file in files)
            {

                treeView1.Nodes.Add(Path.GetFileName(files[i]).ToString());
                columns = File.ReadAllLines(file).First();
                var split = columns.Split(';');

                int count = 0;

                foreach (char c in columns)
                {
                    if (c == ';')
                    {
                        treeView1.Nodes[i].Nodes.Add(split[count]);
                        count++;
                    }
                }

      
                i++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fileName = validator.GetFileName(fileName);
            label1.Text = validator.GetFilePath(validator.FilePath);

            if (!validator.IsFileDat(validator.FilePath))
            {
                button3.Visible = false;
                label2.Visible = true;
            }

            else if (validator.IsNewFile == false)
            {
                return;
            }
            else
            {

                button3.Visible = true;
                label2.Visible = false;

                int fileType = validator.MakeOutDatFile(validator.FilePath);

                switch (fileType)
                {
                    case 1:
                        radioButton1.Checked = true;
                        break;
                    case 2:
                        radioButton2.Checked = true;
                        break;

                    case 3:
                        button3.Visible = false;
                        label2.Visible = true;
                        MessageBox.Show("BŁĘDNA STRUKTURA NAGŁÓWKA PLIKU! SPRAWDŹ PIERWSZĄ LINIĘ!", "BŁĄD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 0:
                        button3.Visible = false;
                        label2.Visible = true;
                        MessageBox.Show("BŁĘDNA STRUKTURA NAGŁÓWKA PLIKU! SPRAWDŹ PIERWSZĄ LINIĘ!", "BŁĄD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //listBox1.ite
            //foreach( )
        }

        private void treeView1_Click(object sender, EventArgs e)
        {

        }
    }
}
