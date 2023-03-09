namespace Fachurnik
{
    partial class Fachurnik
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Tworzenie pliku grupy rabatowej do eShopa");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Tworzenie pliku cenowego do eShop z pliku .dat z SAPa");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Tworzenie pliku cenowego do eShopa z pliku csv ");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Dodanie numeracji własnej do pliku cenowego do eShopa z pliku csv");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("eShop", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Tworzenie pliku cenowego do katalogu BMEcat");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("BMECat", new System.Windows.Forms.TreeNode[] {
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Tworzenie katalogu CIF");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Ariba", new System.Windows.Forms.TreeNode[] {
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Walidator pliku cenowego z SAPa");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Walidator pliku csv ( spr ilości kolumn )");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Walidacja plików", new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Positiveliste");
            this.exit = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // exit
            // 
            this.exit.Location = new System.Drawing.Point(1684, 47);
            this.exit.Margin = new System.Windows.Forms.Padding(4);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(153, 78);
            this.exit.TabIndex = 0;
            this.exit.Text = "EXIT";
            this.exit.UseVisualStyleBackColor = true;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.Location = new System.Drawing.Point(93, 94);
            this.treeView1.Margin = new System.Windows.Forms.Padding(4);
            this.treeView1.Name = "treeView1";
            treeNode1.ForeColor = System.Drawing.Color.DarkGreen;
            treeNode1.Name = "rabattstuffe";
            treeNode1.Text = "Tworzenie pliku grupy rabatowej do eShopa";
            treeNode2.ForeColor = System.Drawing.Color.DarkGreen;
            treeNode2.Name = "preisdateiSAP";
            treeNode2.Text = "Tworzenie pliku cenowego do eShop z pliku .dat z SAPa";
            treeNode3.ForeColor = System.Drawing.Color.DarkGreen;
            treeNode3.Name = "preisdateiSAPCSV";
            treeNode3.Text = "Tworzenie pliku cenowego do eShopa z pliku csv ";
            treeNode4.ForeColor = System.Drawing.Color.DarkGreen;
            treeNode4.Name = "customerNumberEshop";
            treeNode4.Text = "Dodanie numeracji własnej do pliku cenowego do eShopa z pliku csv";
            treeNode5.BackColor = System.Drawing.Color.White;
            treeNode5.ForeColor = System.Drawing.Color.DarkOrange;
            treeNode5.Name = "eShop";
            treeNode5.Text = "eShop";
            treeNode6.ForeColor = System.Drawing.Color.OrangeRed;
            treeNode6.Name = "preisdateizuBME";
            treeNode6.Text = "Tworzenie pliku cenowego do katalogu BMEcat";
            treeNode7.BackColor = System.Drawing.Color.White;
            treeNode7.ForeColor = System.Drawing.Color.SteelBlue;
            treeNode7.Name = "BMECat";
            treeNode7.Text = "BMECat";
            treeNode8.ForeColor = System.Drawing.Color.Orange;
            treeNode8.Name = "catalogCSV";
            treeNode8.Text = "Tworzenie katalogu CIF";
            treeNode9.BackColor = System.Drawing.Color.White;
            treeNode9.ForeColor = System.Drawing.Color.Lime;
            treeNode9.Name = "Ariba";
            treeNode9.Text = "Ariba";
            treeNode10.ForeColor = System.Drawing.Color.DarkGreen;
            treeNode10.Name = "validator";
            treeNode10.Text = "Walidator pliku cenowego z SAPa";
            treeNode11.ForeColor = System.Drawing.Color.OrangeRed;
            treeNode11.Name = "validatorCSV";
            treeNode11.Text = "Walidator pliku csv ( spr ilości kolumn )";
            treeNode12.BackColor = System.Drawing.Color.White;
            treeNode12.ForeColor = System.Drawing.Color.Navy;
            treeNode12.Name = "SAP";
            treeNode12.Text = "Walidacja plików";
            treeNode13.Name = "Positiveliste";
            treeNode13.Text = "Positiveliste";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode7,
            treeNode9,
            treeNode12,
            treeNode13});
            this.treeView1.Size = new System.Drawing.Size(1071, 508);
            this.treeView1.TabIndex = 1;
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(771, 654);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(213, 69);
            this.button1.TabIndex = 2;
            this.button1.Text = "TEST BACKGROUNDWORKER";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Fachurnik
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1917, 816);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.exit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Fachurnik";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FACHURNIK";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button button1;
    }
}

