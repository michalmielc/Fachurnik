using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Fachurnik
{
    public partial class validatorForm : Form
    {
        // DEKLARACJA KLAS. KLASY 
        Description description = new Description();
        Validator validator = new Validator();
        string fileName = "brak pliku";

        public validatorForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void validatorForm_Shown(object sender, EventArgs e)
        {
            textBox1.Text = description.validatorFormDescription();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fileName =  validator.GetFileName(fileName);
            label1.Text = validator.GetFilePath(validator.FilePath);

            if (!validator.IsFileDat(validator.FilePath))
            {
                button3.Visible = false;
                label2.Visible = true;
                validator.CheckRadiobuttons(0, this);
            }

            else if (validator.IsNewFile==false)
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
                        radioButton3.Checked = true;
                        break;
                    case 0:
                        button3.Visible = false;
                        label2.Visible = true;
                        MessageBox.Show("BŁĘDNA STRUKTURA NAGŁÓWKA PLIKU! SPRAWDŹ PIERWSZĄ LINIĘ!", "BŁĄD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                validator.CheckRadiobuttons(fileType, this);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            validator.ValidationDat(validator.FilePath,true,false);
        }


    }
}
