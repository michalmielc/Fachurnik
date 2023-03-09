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
    public partial class preisdateiSAPCSV : Form
    {
        // DEKLARACJA KLAS. KLASY 
        Description description = new Description();
        Validator validator = new Validator();
        string fileName2 = "brak pliku";

        public preisdateiSAPCSV()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void preisdateiSAPCSV_Shown(object sender, EventArgs e)
        {
            textBox1.Text = description.eshopFormPreisFileDescription();
            comboBox1.DataSource = validator.CatalogueNo;

            //AUTOMATYCZNE USTAWIENIE NR KATALOGU
            comboBox1.SelectedIndex = validator.SetCurrentCatalogYear();

            // DOMYŚLNA WALUTA EUR
            validator.Currency = (Currency)1;
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

        private void button2_Click(object sender, EventArgs e)
        {

            fileName2 = validator.GetFileName2(fileName2);
            label1.Text = validator.GetFilePath2(validator.FilePath2);

            if (!validator.IsFileCsv(validator.FilePath2))
            {
                button3.Visible = false;
                label2.Visible = true;
                radioButton5.BackColor = default;
                radioButton5.Checked = false;

            }

            else if (validator.IsNewFile2 == false)
            {
                return;
            }

            else
            {
                button3.Visible = true;
                label2.Visible = false;

                int columnAMount = validator.MakeOutCsvFileColumnAmount(validator.FilePath2);

                if (columnAMount == 5)

                {
                    radioButton5.Checked = true;
                    radioButton5.BackColor = Color.Yellow;
                    groupBox1.Visible = true;
                }
                else

                {
                    button3.Visible = false;
                    label2.Visible = true;
                    MessageBox.Show("BŁĘDNA STRUKTURA PLIKU! DOPUSZCZALNY FORMAT PLIK *.csv PIĘCIOKOLUMNOWY", "BŁĄD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            textBox2.Text = "0108" + (2021 + int.Parse(comboBox1.SelectedIndex.ToString())).ToString();
            textBox3.Text = "3107" + (2021 + 1 + int.Parse(comboBox1.SelectedIndex.ToString())).ToString(); ;

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

        // AUTOMATYCZNA ZMIANA KROPKI NA PRZECINEK KURS 
        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            textBox8.Text = textBox8.Text.Replace('.', ',');
            textBox8.SelectionStart = textBox8.Text.Length;
        }
       
        // AUTOMATYCZNA ZMIANA NAZWY PLIKU
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox4.Text = "";
            textBox4.Text = textBox5.Text + ".dat";

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

            // WYWOŁANIE WALIDACJI PLIKU CSV Z NUMERACJĄ WŁASNĄ TYP CSV
            bool isHeader = true;

            if (!checkBox2.Checked)
            {
                isHeader = false;
            }
            if (!validator.ValidationCsv(validator.FilePath2,5, isHeader, validator.FileName2,1))
            {
                return;
            }

            string header =
                //dopisanie brakujących zer do nr klieta.
                textBox4.Text.Substring(0, textBox4.Text.Length - 4) + "|1|" +
                comboBox1.SelectedItem.ToString() + "|" +
                textBox2.Text + "|" +
                textBox3.Text + "|5.00|07|" +
                validator.Currency.ToString() + "||" +
                "0000" + textBox7.Text + "|" +
                textBox6.Text + "|X|||||||";

            // TWORZENIE PLIKU
            if ((int)validator.Currency == 1)
            {
                validator.CreatePriceFileDatFromCSV(1);
            }

            else if ((int)validator.Currency == 2)
            {
                double rate = Double.Parse(textBox8.Text.ToString());
                validator.CreatePriceFileDatFromCSV(rate);
            }


            //PODMIANA NAGŁÓWKA
            validator.ChangeHeaderInFile(header, validator.TempFilePath2);

            //ZAPIS NA DYSK
            validator.SaveFileToDesktop(textBox4.Text, validator.TempFilePath2);
        }
    }
}
