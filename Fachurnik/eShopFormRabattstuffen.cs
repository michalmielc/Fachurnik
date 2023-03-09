using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fachurnik
{
    public partial class eShopFormRabattstuffen : Form
    {
        // DEKLARACJA KLAS. KLASY 
        Description description = new Description();
        Validator validator = new Validator();
        string fileName = "brak pliku";

        public eShopFormRabattstuffen()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void eShopForm_Shown(object sender, EventArgs e)
        {
            textBox1.Text = description.eshopFormRabattStuffenDescription();

            comboBox1.DataSource = validator.CatalogueNo;

            comboBox1.SelectedIndex = validator.SetCurrentCatalogYear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fileName = validator.GetFileName(fileName);
            label1.Text = validator.GetFilePath(validator.FilePath);
        
            if (!validator.IsFileDat(validator.FilePath))
            {
                button3.Visible = false;
                label2.Visible = true;
                validator.CheckRadiobuttons (0,this);
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
                        groupBox1.Visible = true;
                        break;
                    case 2:
                        radioButton2.Checked = true;
                        groupBox1.Visible = true;
                        break;
                    case 0:
                        button3.Visible = false;
                        label2.Visible = true;
                        groupBox1.Visible = false;
                        MessageBox.Show("BŁĘDNA STRUKTURA NAGŁÓWKA PLIKU! SPRAWDŹ PIERWSZĄ LINIĘ!", "BŁĄD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case 3:
                        button3.Visible = false;
                        label2.Visible = true;
                        groupBox1.Visible = false;
                        MessageBox.Show("PLIK RABATTSTUFFEN. PRAWIDŁOWY PLIK MUSI BYC Z KAN. 01/04 LUB ESHOP", "BŁĄD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                validator.CheckRadiobuttons(fileType, this);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            // WYWOŁANIE WALIDACJI KONTROLEK TEXTBOX
            if (!textBoxValidation())
            {
                return;
            }

            // WYWOŁANIE WALIDACJI
            if (!validator.ValidationDat(validator.FilePath, true, false))
            {
                return;
            }

            string header = "|2|" + comboBox1.SelectedItem.ToString() + "|" + textBox2.Text + "|" + textBox3.Text + "||07|EUR|||" + textBox4.Text + "||||||||";

            validator.SetItemAfterImport(1);

            validator.ChangeHeaderInFile(header, validator.TempFilePath);

            validator.SaveFileToDesktop( textBox4.Text+".dat", validator.TempFilePath);
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            textBox2.Text = "0108" + (2021 + int.Parse(comboBox1.SelectedIndex.ToString())).ToString();
            textBox3.Text = "3107" + (2021 + 1 + int.Parse(comboBox1.SelectedIndex.ToString())).ToString(); ;
        }

        //ZMIANA KOLORÓW ERROR HANDLING TEXTBOX
        private bool textBoxValidation()
        {
            if (!validator.TextBoxValidator(ref textBox2,8))
            { return false; }

            else if (!validator.TextBoxValidator(ref textBox3,8))
            { return false; }

            else if (!validator.TextBoxValidator(ref textBox4,8))
            { return false; }

            else
            {
                return true;
            }
        }



        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length >= 8)
            {
                textBox2.BackColor = Color.White;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Length >= 8)
            {
                textBox3.BackColor = Color.White;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text.Length >= 8)
            {
                textBox4.BackColor = Color.White;
            }
        }




    }
}
