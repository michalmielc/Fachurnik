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
    public partial class eShopItemCustomNumber : Form
    {

        // DEKLARACJA KLAS. KLASY 
        Description description = new Description();
        Validator validator = new Validator();
        string fileName = "brak pliku";
        string fileName2 = "brak pliku";

        public eShopItemCustomNumber()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void eShopItemCustomNumber_Shown(object sender, EventArgs e)
        {
            textBox1.Text = description.eshopFormCustomerItemNumberDescription();
        }

        // WCZYTANIE PLIKU DAT
        private void button4_Click(object sender, EventArgs e)
        {
            fileName = validator.GetFileName(fileName);
            label4.Text = validator.GetFilePath(validator.FilePath);

            if (!validator.IsFileDat(validator.FilePath))
            {
                button3.Visible = false;
                label3.Visible = true;
                radioButton1.Checked = false;
                radioButton1.BackColor = default;
            }

            else if (validator.IsNewFile == false)
            {
                return;
            }
            else
            {

                button3.Visible = true;
                label3.Visible = false;

                 int fileType = validator.MakeOutDatFile(validator.FilePath);

                switch (fileType)
                {
                    case 1:
                        radioButton1.Checked = true;
                        textBox2.Text = fileName;
                        radioButton1.BackColor = Color.Yellow;
                        break;
                    case 0:
                        button3.Visible = false;
                        label3.Visible = true;
                        radioButton1.Checked = false;
                        radioButton1.BackColor = default;
                        MessageBox.Show("BŁĘDNA STRUKTURA NAGŁÓWKA PLIKU! SPRAWDŹ PIERWSZĄ LINIĘ!", "BŁĄD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    default:
                        button3.Visible = false;
                        label3.Visible = true;
                        radioButton1.Checked = false;
                        radioButton1.BackColor = default;
                        MessageBox.Show("BŁĘDNA STRUKTURA PLIKU! DOPUSZCZALNY FORMAT PLIK CENOWY DO ESHOP!", "BŁĄD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

        
          
            }
        }

        // WCZYTANIE PLIKU CSV
        private void button2_Click(object sender, EventArgs e)
        {
            fileName2 = validator.GetFileName2(fileName2);
            label1.Text = validator.GetFilePath2(validator.FilePath2);

            if (!validator.IsFileCsv(validator.FilePath2))
            {
                button3.Visible = false;
                label2.Visible = true;
                radioButton5.BackColor = default;
                radioButton5.Checked= false;

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

                if (columnAMount == 2)

                {
                    radioButton5.Checked = true;
                    radioButton5.BackColor = Color.Yellow;
                }
                else

                {
                    button3.Visible = false;
                    label2.Visible = true;
                    MessageBox.Show("BŁĘDNA STRUKTURA PLIKU! DOPUSZCZALNY FORMAT PLIK *.csv DWUKOLUMNOWY", "BŁĄD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // WYWOŁANIE WALIDACJI PLIKU CENOWEGO DO ESHOPA TYP DAT

            if (!validator.ValidationDat(validator.FilePath, true, true))
            {
                return;
            }

         
            // WYWOŁANIE WALIDACJI PLIKU CSV Z NUMERACJĄ WŁASNĄ TYP CSV
            bool isHeader = true;

            if (!checkBox2.Checked)
            {
                isHeader = false;
            }
            if (!validator.ValidationCsv(validator.FilePath2,2, isHeader, validator.FileName2, 0))
            {
                return;
            }


           // POŁĄCZENIE TABEL NR WŁASNA ORAZ CENNIKA. ZAPISANIE DANYCH W TEMP
            validator.JoinCustomerItemNoToPriceFileDat();

            //PODMIANA NAGŁÓWKA
            validator.ChangeHeaderInFile(validator.Header, validator.TempFilePath);

            //ZAPIS NA DYSK
            validator.SaveFileToDesktop(textBox2.Text, validator.TempFilePath);
        }
    }

}
