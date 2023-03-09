using System;
using System.Collections;
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
    public partial class Fachurnik : Form
    {
        public Fachurnik()
        {
            InitializeComponent();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Name == "validator")
            {
                validatorForm validatorForm = new validatorForm();
                validatorForm.ShowDialog();
            }

            if (treeView1.SelectedNode.Name == "rabattstuffe")
            {
                eShopFormRabattstuffen eShopFormRabattstuffen = new eShopFormRabattstuffen();
                eShopFormRabattstuffen.ShowDialog();
            }

            if (treeView1.SelectedNode.Name == "preisdateiSAP")
            {
                eShopFormPriceFile eShopFormPriceFile = new eShopFormPriceFile();
                eShopFormPriceFile.ShowDialog();
            }

            if (treeView1.SelectedNode.Name == "customerNumberEshop")
            {
                eShopItemCustomNumber eShopItemCustomNumber = new eShopItemCustomNumber();
                eShopItemCustomNumber.ShowDialog();
            }

            if (treeView1.SelectedNode.Name == "catalogCSV")
            {
                catalogForm catalogForm = new catalogForm();
                catalogForm.ShowDialog();
            }

            if (treeView1.SelectedNode.Name == "preisdateiSAPCSV")
            {
                preisdateiSAPCSV preisdateiSAPCSV = new preisdateiSAPCSV();
                preisdateiSAPCSV.ShowDialog();
            }

            if (treeView1.SelectedNode.Name == "preisdateizuBME")
            {
                BMECatFormFromSAPFile eshopFormCSVForBMECat = new BMECatFormFromSAPFile();
                eshopFormCSVForBMECat.ShowDialog();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressForm progressForm = new progressForm();

            progressForm.Show();

            for (int i = 0; i < 1000000000; i++)
            {
                i = i * i;
            }

           // MessageBox.Show("KONIEC ZADANIA");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hashtable hsh1 = new Hashtable();
            Hashtable hsh2 = new Hashtable();

            hsh1.Add("RAZ", 256232);
            hsh1.Add("AHDEWHUH", "EUYWIUDU");
            hsh1.Add("132123123", "----");
            hsh2.Add("RAZ", "YYYYY");

            MessageBox.Show(hsh2["RAZ"].ToString());
            hsh2["RAZ"] = hsh1["132123123"];
            MessageBox.Show(hsh2["RAZ"].ToString());




        }
    }
}
