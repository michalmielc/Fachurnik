using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Collections;

namespace Fachurnik
{
    class Validator : Items

    {
        // POLA ......................................................   
        //............................................................ 


        //INFORMACJA O BŁĘDZIE
        public string ErrorMessage { get; set; }


        // METODY ......................................................   
        //..............................................................


        //ROZPOZNANIE KANAŁU DYSTRYBUCJI PLIK DAT
        public int MakeOutDatFile(string filePath)
        {
            string header = File.ReadLines(FilePath).First();
            int fileIdentity;
            int count = 0;
            char charToCount = '|';
            foreach (char c in header)
            {
                if (c == charToCount)
                {
                    count++;
                }
            }

            switch (count)
            {
                case 18:
                    // plik Rabattstuffe
                    if (header.Substring(0, 3) == "|2|")
                    {
                        fileIdentity = 3;
                        break;
                    }
                    // plik eshop
                    else
                    {
                        fileIdentity = 1;
                        break;
                    }
                // plik pozostałe kanały 01/03/04
                case 8:
                    fileIdentity = 2;
                    break;
                // plik błąd
                default:
                    fileIdentity = 0;
                    break;
            }

            //USTALENIE RODZAJU PLIKU I ZAPISANIE GO DO ZMIENNEJ RODZAJ PLIKU
            FileType = (FileType)fileIdentity;
            return fileIdentity;

        }

        //FUNKCJA WALIDACJI PLIK DAT
        public bool ValidationDat(string filePath, bool isHeader, bool copyHeader)
        {
            bool validation = true;
            int rowNumber = 2;
            ReadFile(filePath, isHeader, copyHeader);
            List<string> errorLines = new List<string>();

            foreach (string line in ImportedLines)
            {
                if (!LineChecking(line, (int)FileType))
                {
                    validation = false;
                    //ZAPIS BŁĘDÓW
                    errorLines.Add("nr linii: " + rowNumber.ToString() + " " + ErrorMessage + " " + line);
                }
                rowNumber++;
            }

            if (validation)
            {
                MessageBox.Show("PLIK POPRAWNY" + Environment.NewLine +
                          "ilość sprawdzonych linii: " + rowNumber.ToString(),
                          "RAPORT Z WALIDACJI",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }

            else
            {
                string header = "RAPORT Z WALIDACJI PLIKU: " + FileName +
                                Environment.NewLine +
                                "W PRZETWARZANYM PLIKU ZNALEZIONO BŁĘDY." +
                                Environment.NewLine + "LICZBA BŁĘDÓW: " +
                                (errorLines.Count).ToString();

                // DODANIE NAGŁÓWKA/PODSUMOWANIA Z BŁĘDAMI
                errorLines.Insert(0, header);

                File.WriteAllLines(TempErrorFilePath, errorLines);

                SaveFileToDesktop("RAPORT_BŁĘDÓW.txt", TempErrorFilePath);

                MessageBox.Show("W PRZETWARZANYM PLIKU ZNALEZIONO BŁĘDY. LICZBA BŁĘDÓW: " +
                     (errorLines.Count - 1).ToString() + "." + Environment.NewLine + "SZCZEGÓŁY W PLIKU RAPORT_Z_BŁĘDÓW.TXT ZAPISANO NA PULPICIE. ", "BŁĄD WALIDACJI", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        //SPRAWDZENIE LINII, FILETYPE
        public bool LineChecking(string line, int fileType)
        {

            //PUSTY WIERSZ
            if (!CheckEmptyRow(line))
            {
                return false;
            }

            //BRAK NADMIAR PIPE
            if (!CheckPipes(line, fileType))
            {
                return false;
            }

            //USTAWIENIE WARTOŚCI ITEM, NAZWA, CENA 1 ITD.....
            SetSplitFromItemRecordDat(line, fileType);

            //BRAK NUMERU ARTYKUŁU
            if (!CheckItemNumber(ItemNumber))
            {
                return false;
            }

            if (!CheckAmount(ItemAmount1, fileType))
            {
                return false;
            }

            if (!CheckAmount(ItemAmount2, fileType))
            {
                return false;
            }

            if (!CheckPrice(ItemPrice1, fileType))
            {
                return false;
            }

            if (!CheckPrice(ItemPrice2, fileType))
            {
                return false;
            }

            return true;
        }

        //ILOŚĆ ZNAKÓW PIPES "|" W LINII 
        public bool CheckPipes(string line, int fileType)
        {
            int count = 0;
            foreach (char c in line)
            {
                if (c == '|')
                {
                    count++;
                }
            }

            if ((fileType == 1 || fileType == 3) && count == 12)
            { return true; }
            else if (fileType == 2 && count == 15)
            { return true; }
            else
            {
                ErrorMessage = "BŁĘDNA ILOŚĆ WYSTĄPIEŃ SEPARATORA: |";
                return false;
            }
        }

        //WALIDACJA PUSTEGO WIERSZA
        public bool CheckEmptyRow(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                ErrorMessage = "NAPOTKANO PUSTY WIERSZ!";
                return false;
            }
            return true;
        }

        //WALIDACJA CENY 
        public bool CheckPrice(string price, int fileType)
        {
            // WARUNKI SPECJALNE DLA CENNIK 010304
            if (fileType == 2)
            {
                if (!string.IsNullOrEmpty(ItemAmount2) && string.IsNullOrEmpty(ItemPrice2))
                {
                    ErrorMessage = "BRAK CENY2 PRZY CZYM ILOŚĆ2 ISTNIEJE";
                    return false;
                }

                if (string.IsNullOrEmpty(ItemPrice2) && string.IsNullOrEmpty(ItemAmount2))
                {
                    return true;
                }
                //USUNIĘCIE SEPARATORA TYSIĘCY KANAŁ 010304
                price = price.Replace(".", "");
            }

            if (string.IsNullOrEmpty(price))
            {
                ErrorMessage = "BRAK CENY!";
                return false;
            }

            if (!Double.TryParse(price, out double priceConverted))
            {
                ErrorMessage = "NIEPRAWIDŁOWY FORMAT CENY!";
                return false;
            }

            return true;
        }

        //WALIDACJA CENY CSV
        public bool CheckPriceCSV(string price)
        {

            if (string.IsNullOrEmpty(price))
            {
                ErrorMessage = "BRAK CENY ISTNIEJE";
                return false;
            }

            price = price.Replace(".", "");
            price = price.Trim();

            if (!Double.TryParse(price, out double priceConverted))
            {
                ErrorMessage = "NIEPRAWIDŁOWY FORMAT CENY!";
                return false;
            }

            return true;
        }

        //WALIDACJA ILOŚCI 
        public bool CheckAmount(string amount, int fileType)
        {
            // WARUNKI SPECJALNE DLA CENNIK 010304
            if (fileType == 2)
            {
                if (string.IsNullOrEmpty(ItemAmount1))
                {
                    return true;
                }

                else if (string.IsNullOrEmpty(ItemAmount2) && !string.IsNullOrEmpty(ItemPrice2))
                {
                    ErrorMessage = "BRAK ILOŚCI2 PRZY CZYM CENA2 ISTNIEJE!";
                    return false;
                }

                else if (string.IsNullOrEmpty(ItemAmount2) && string.IsNullOrEmpty(ItemPrice2))
                {
                    return true;
                }
                //USUNIĘCIE SPACJI KANAŁ 010304
                amount = amount.Trim();
            }

            if (string.IsNullOrEmpty(amount))
            {
                ErrorMessage = "BRAK ILOŚCI";
                return false;
            }

            if (!int.TryParse(amount, out int amountConverted))
            {
                ErrorMessage = "NIEPRAWIDŁOWY FORMAT ILOŚCI";
                return false;
            }

            return true;
        }

        //WALIDACJA ILOŚCI CSV
        public bool CheckAmountCSV(string amount)
        {
            amount = amount.Trim();

            if (string.IsNullOrEmpty(amount))
            {
                ErrorMessage = "BRAK ILOŚCI";
                return false;
            }

            if (!int.TryParse(amount, out int amountConverted))
            {
                ErrorMessage = "NIEPRAWIDŁOWY FORMAT ILOŚCI";
                return false;
            }

            return true;
        }

        //WALIDACJA NAZWY 
        public bool CheckItemName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                ErrorMessage = "BRAK NAZWY ARTYKUŁU";
                return false;
            }

            foreach (char c in name)
            {
                if (c == ';' || c == '"' || c == '|' || c == '\'')
                {
                    ErrorMessage = "W NAZWIE WYSTĄPIŁ NIEPRAWIDŁOWY ZNAK ; '' | ";
                    return false;
                }
            }

            return true;
        }

        //WALIDACJA NUMERU KATALOGOWEGO 
        public bool CheckItemNumber(string itemNumber)
        {
            if (string.IsNullOrEmpty(itemNumber))
            {
                ErrorMessage = "BRAK NR ARTYKUŁU";
                return false;
            }

            return true;
        }

        //WALIDATOR POLA TEKSTOWEGO
        public bool TextBoxValidator(ref TextBox txb, int lenght)
        {
            if (String.IsNullOrEmpty(txb.Text) || txb.Text.Length < lenght)
            {
                MessageBox.Show($"MINIMALNA DŁ. { lenght} ZNAKÓW", "BŁĄD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txb.BackColor = Color.Red;
                return false;
            }
            txb.BackColor = Color.White;
            return true;
        }

        //ROZPOZNANIE ILOŚCI KOLUMN W PLIKU CSV
        public int MakeOutCsvFileColumnAmount(string filePath)
        {
            string header = File.ReadLines(filePath).First();
            int columnAmount = 1;

            char charToCount = ';';

            foreach (char c in header)
            {
                if (c == charToCount)
                {
                    columnAmount++;
                }
            }

            return columnAmount;
        }

        //WALIDATOR POLA TEKSTOWEGO KURS WALUTY
        public bool TextBoxExchangeRateValidator(ref TextBox txb)
        {
            double rate;

            if (!Double.TryParse(txb.Text, out rate))
            {
                MessageBox.Show("NIEPRAWIDŁOWY FORMAT KURSU", "BŁĄD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txb.BackColor = Color.Red;
                return false;
            }

            rate = Double.Parse(txb.Text);
            if (rate <= 0)
            {
                MessageBox.Show("KURSU MUSI BYĆ >0", "BŁĄD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txb.BackColor = Color.Red;
                return false;
            }

            txb.BackColor = Color.White;
            return true;
        }

        //ZMIANA KOLORÓW WYBÓR PLIKÓW
        public void CheckRadiobuttons(int fileType, Form form)
        {
            foreach (RadioButton rb in form.Controls.OfType<RadioButton>())
            {

                if (rb.Checked && fileType != 0)
                {
                    rb.BackColor = Color.Yellow;
                }

                else
                {
                    rb.BackColor = default;
                }
            }


        }

        public bool ValidationCsv(string filePath, int columnCount, bool isHeader, string fileName, int splitNo)
        {
            bool validation = true;
            int rowNumber = 2;
            ReadFileCSV(filePath, isHeader);
            List<string> errorLines = new List<string>();

            foreach (string line in ImportedLines2)
            {
                if (!LineCheckingCSV(line, columnCount, splitNo))
                {
                    validation = false;
                    //ZAPIS BŁĘDÓW
                    errorLines.Add("nr linii: " + rowNumber.ToString() + " " + ErrorMessage + " " + line);
                }
                rowNumber++;
            }

            if (validation)
            {
                MessageBox.Show("PLIK POPRAWNY" + Environment.NewLine +
                          "ilość sprawdzonych linii: " + rowNumber.ToString(),
                          "RAPORT Z WALIDACJI",
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }

            else
            {
                string header = "RAPORT Z WALIDACJI PLIKU: " + fileName +
                                Environment.NewLine +
                                "W PRZETWARZANYM PLIKU ZNALEZIONO BŁĘDY." +
                                Environment.NewLine + "LICZBA BŁĘDÓW: " +
                                errorLines.Count.ToString();
                File.WriteAllLines(TempErrorFilePath2, errorLines);

                ChangeHeaderInFile(header, TempErrorFilePath2);

                SaveFileToDesktop("RAPORT_BŁĘDÓW.txt", TempErrorFilePath2);
                MessageBox.Show("W PRZETWARZANYM PLIKU ZNALEZIONO BŁĘDY. LICZBA BŁĘDÓW: " +
                     errorLines.Count + "." + Environment.NewLine + "SZCZEGÓŁY W PLIKU RAPORT_Z_BŁĘDÓW.TXT ZAPISANO NA PULPICIE. ", "BŁĄD WALIDACJI", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        public bool LineCheckingCSV(string line, int columnCount, int splitNo)
        {

            //PUSTY WIERSZ
            if (!CheckEmptyRow(line))
            {
                return false;
            }

            //BRAK NADMIAR ŚREDNIKÓW
            if (!CheckSemicolons(line, columnCount))
            {
                return false;
            }

            //USTAWIENIE PODZIAŁU WIERSZA WG WYKONYWANEJ FUNKCJI
            switch (splitNo)
            {
                case 0:
                    SetSplitFromItemCSVCustomerNo(line);
                    break;
                case 1:
                    SetSplitFromItemCSVPriceFile(line);
                    break;
            }

            //BRAK NUMERU ARTYKUŁU
            if (!CheckItemNumber(ItemNumber))
            {
                return false;
            }

            if (splitNo == 1)
            { 
                //WALIDACJA CENY
                if (!CheckPriceCSV(ItemPrice1))
                {
                    return false;
                }
                if (!CheckPriceCSV(ItemPrice2))
                {
                    return false;
                }
                if (!CheckAmountCSV(ItemAmount1))
                {
                    return false;
                }
                if (!CheckAmountCSV(ItemAmount2))
                {
                    return false;
                }
            
            }

            return true;

        }

        // SPRAWDZENIE ILOŚCI ŚREDNIKÓW
        public bool CheckSemicolons(string line, int columnCount)

        {
            int count = 0;
            foreach (char c in line)
            {
                if (c == ';')
                {
                    count++;
                }
            }

            if (count == columnCount - 1)
            { return true; }

            else
            {
                ErrorMessage = "BŁĘDNA ILOŚĆ WYSTĄPIEŃ SEPARATORA: ;";
                return false;
            }

        }

        // KRZYŻOWANIE TABELI NR WŁASNE KLIENTA Z CENNIKIEM DO ESHOPA   
        public void JoinCustomerItemNoToPriceFileDat()
        {
            Hashtable customerItemNoCSV = new Hashtable();
            Hashtable priceFileDAT = new Hashtable();

            // WCZYTANIE NR WŁASNEJ DO TABLICY Z KLUCZEM


            foreach (string line in ImportedLines2)
            {
                var split = line.Split(';');
                CustomerItemNumber = split[1];
                ItemNumber = split[0];
                customerItemNoCSV.Add(ItemNumber, CustomerItemNumber);

            }

            // WCZYTANIE DANYCH CENNIKA DO TABLICY Z KLUCZEM

            foreach (string line in ImportedLines)
            {
                var split = line.Split('|');
                ItemNumber = split[1];
                ItemSize = split[2];
                if (ItemSize != "")
                {
                    ItemNumber = ItemNumber + " " + ItemSize;
                }
                priceFileDAT.Add(ItemNumber, line);
            }


            //PORÓWNANIE TABEL

            foreach (var item in customerItemNoCSV.Keys)
            {
                if (priceFileDAT.ContainsKey(item))
                {
                    priceFileDAT[item] = customerItemNoCSV[item].ToString() + priceFileDAT[item].ToString();
                }
            }


            // LISTA WYJŚCIOWA PO SKRZYŻOWANIU TABEL
            List<string> linesConverted = new List<string>();
            List<string> linesConverted2 = new List<string>();

            //ZAPIS POŁĄCZONYCH TABEL 
            //UWZGLĘDNIENIE SORTOWANIA PO KLUCZU

            foreach (var item in priceFileDAT.Keys)
            {
                linesConverted.Add(item.ToString() + "#" + priceFileDAT[item].ToString());
            }

            linesConverted.Sort();

            // USUNIĘCIE KLUCZA PO KTÓRYM SORTOWANO

            foreach (var line in linesConverted)
            {
                var split = line.Split('#');
                linesConverted2.Add(split[1]);
            }

            string[] linesAfterConvertion = linesConverted2.ToArray();
            File.WriteAllLines(TempFilePath, linesAfterConvertion);
        }

        public void CreatePriceFileDatFromCSV(double exchangeRate)
        {
            // LISTA WYJŚCIOWA PO SKRZYŻOWANIU TABEL
            List<string> linesConverted = new List<string>();

            //WCZYTANIE Z TABLICY PO IMPORCIE Z CSV
            foreach (string line in ImportedLines2)
            {
                var split = line.Split(';');
                ItemNumber = split[0];
               
                if(ItemNumber.Length==6)
                {
                    ItemSize = "";
                }
                else
                {
                    ItemSize = ItemNumber.Substring(7, ItemNumber.Length - 7);
                    ItemNumber = ItemNumber.Substring(0, 6);
                }
              
                ItemPrice1 = split[1];
                ItemPrice2 = split[2];
                ItemAmount1 = split[3];
                ItemAmount2 = split[4];

                ItemPrice1 = string.Format("{0:0.00}", exchangeRate * double.Parse(ItemPrice1));
                ItemPrice2 = string.Format("{0:0.00}", exchangeRate * double.Parse(ItemPrice2));

                linesConverted.Add(
                                    "|"+
                                    ItemNumber + "|" +
                                    ItemSize + "|||" +
                                    ItemPrice1 + "|" +
                                    ItemPrice2 + "|" +
                                    ItemAmount1 + "|" +
                                    ItemAmount2+ "||||");

            }

            string[] linesAfterConvertion = linesConverted.ToArray();
            File.WriteAllLines(TempFilePath2, linesAfterConvertion);
        }

        // TWORZENIE PLIK CSV DO BMECAT
        public void CreatePriceFileCSVToBMECat(string dateFrom, string dateTo)
        {
            // LISTA WYJŚCIOWA PO SKRZYŻOWANIU TABEL
            List<string> linesConverted = new List<string>();
            string newLine = "";
            
            ImportedLines = File.ReadAllLines(TempFilePath);

            //WCZYTANIE Z TABLICY PO IMPORCIE Z CSV
            foreach (string line in ImportedLines)
            {
                var split = line.Split('|');

                    CustomerItemNumber = split[0];
                    ItemNumber = split[1];
                    ItemSize = split[2];

                    if(ItemSize!="")
                    {
                        ItemNumber = ItemNumber + " " + ItemSize;
                    }
                
                    ItemPrice1 = split[5];
                    ItemPrice2 = split[6];
                    ItemAmount1 = split[7];
                    ItemAmount2 = split[8];

                if (String.Compare(ItemAmount1,ItemAmount2)==0)
                {   
                    newLine =
                      ItemNumber + ";" +
                      ItemPrice1 + ";net_customer;" +
                      ItemAmount1 + ";0.23;" + Currency + 
                       ";;;;;;"+ dateFrom+";" +dateTo;
                 
                }

                else
                {
                  newLine =
                    ItemNumber + ";" +
                    ItemPrice1 + ";net_customer;" + ";" +
                    ItemAmount1 + ";0.23;" + Currency + ";" +
                    ItemPrice2 + ";net_customer;" + 
                    ItemAmount2 + ";0.23;" + Currency + ";" +
                    dateFrom.Substring(4,4) +"-"+ dateFrom.Substring(2, 2) + "-" + dateFrom.Substring(0, 2)+ ";"+
                    dateTo.Substring(4, 4) + "-" + dateTo.Substring(2, 2) + "-" + dateTo.Substring(0, 2);
        
                }

                linesConverted.Add(newLine);

                     


            }

            string[] linesAfterConvertion = linesConverted.ToArray();

            File.WriteAllLines(TempFilePath, linesAfterConvertion);
        }

    }
}
