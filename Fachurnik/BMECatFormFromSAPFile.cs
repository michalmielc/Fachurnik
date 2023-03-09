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
    public partial class BMECatFormFromSAPFile : Form
    {
        Description description = new Description();
        Validator validator = new Validator();
        string fileName = "brak pliku";

        public BMECatFormFromSAPFile()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void eshopFormCSVForBMECat_Shown(object sender, EventArgs e)
        {
            textBox1.Text = description.BMECatFormFromDATDescription();
            comboBox1.DataSource = validator.CatalogueNo;

            //AUTOMATYCZNE USTAWIENIE NR KATALOGU
            comboBox1.SelectedIndex = validator.SetCurrentCatalogYear();

            // DOMYŚLNA WALUTA EUR
            validator.Currency = (Currency)1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fileName = validator.GetFileName(fileName);
            label1.Text = validator.GetFilePath(validator.FilePath);

            if (!validator.IsFileDat(validator.FilePath))
            {
                button3.Visible = false;
                label2.Visible = true;
                validator.CheckRadiobuttons(0, this);
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
                        textBox4.Text = fileName;
                        textBox4.Text = textBox4.Text.Replace("dat", "csv");
                        break;
                    case 2:
                        radioButton2.Checked = true;
                        groupBox1.Visible = true;
                        textBox4.Text = fileName;
                        textBox4.Text = textBox4.Text.Replace("dat", "csv");
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

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            textBox2.Text = "0108" + (2021 + int.Parse(comboBox1.SelectedIndex.ToString())).ToString();
            textBox3.Text = "3107" + (2021 + 1 + int.Parse(comboBox1.SelectedIndex.ToString())).ToString(); ;

        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                label11.Visible = false;
                textBox8.Visible = false;
                validator.Currency = (Currency)1;
            }
        }

        private void radioButton4_Click(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                label11.Visible = true;
                textBox8.Visible = true;
                validator.Currency = (Currency)2;
            }
        }


        private bool textBoxValidation()
        {
            if (!validator.TextBoxValidator(ref textBox2, 8))
            { return false; }

            else if (!validator.TextBoxValidator(ref textBox3, 8))
            { return false; }

            else if (!validator.TextBoxValidator(ref textBox6, 8))
            { return false; }

            else if (!validator.TextBoxValidator(ref textBox7, 6))
            { return false; }

            else if (!validator.TextBoxValidator(ref textBox5, 1))
            { return false; }

            else if (!validator.TextBoxExchangeRateValidator(ref textBox8))
            { return false; }

            else
            {
                return true;
            }
        }

        // AUTOMATYCZNA ZMIANA NAZWY PLIKU
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox4.Text = "";
            textBox4.Text = textBox5.Text + ".csv";

            if (textBox5.Text.Length >= 1)
            {
                textBox5.BackColor = Color.White;
            }

            if (textBox5.Text.Length < 5)
            {
                for (int k = 0; k < 5 - textBox5.Text.Length; k++)
                {
                    textBox4.Text = textBox4.Text.Insert(0, "0");
                }
            }
        }

        // AUTOMATYCZNA ZMIANA KROPKI NA PRZECINEK KURS 
        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            textBox8.Text = textBox8.Text.Replace('.', ',');
            textBox8.SelectionStart = textBox8.Text.Length;
        }

        // WALIDAJCA KOLORYSTYKA TEXTBOX

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
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (textBox6.Text.Length >= 8)
            {
                textBox6.BackColor = Color.White;
            }
        }
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (textBox7.Text.Length >= 6)
            {
                textBox7.BackColor = Color.White;
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

            //working area

            string header =
             //dopisanie brakujących zer do nr klieta.
             "supplierAID; priceAmount1; priceTyp1; lowerBound1;" +
             " tax1; currency1; priceAmount2; priceTyp2; lowerBound2; " +
             "tax2; currency2; dateTime1; dateTime2";

            if ((int)validator.Currency == 1)
            {
                validator.SetItemAfterImport(1);
            }

            else if ((int)validator.Currency == 2)
            {
                double rate = Double.Parse(textBox8.Text.ToString());
                validator.SetItemAfterImport(rate);
            }

            validator.CreatePriceFileCSVToBMECat(textBox2.Text, textBox3.Text);

            validator.ChangeHeaderInFile(header, validator.TempFilePath);

            validator.TempErrorFilePath = Path.ChangeExtension(validator.TempErrorFilePath, ".csv");

            validator.SaveFileToDesktop(textBox4.Text, validator.TempFilePath);
        }

 
    }
}
