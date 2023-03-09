using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Fachurnik
{
    public enum Currency
    {
        unknown, // nieznana błędny
        EUR,
        PLN
    }
    class Items : FileManager
    {
        // POLA ...........................................................................  
        //...................................................................................

        public string ItemNumber { get; set; }
        public string CustomerItemNumber { get; set; }
        public string ItemSize { get; set; }
        public string ItemName { get; set; }
        public string ItemPrice1 { get; set; }
        public string ItemPrice2 { get; set; }
        public string ItemAmount1 { get; set; }
        public string ItemAmount2 { get; set; }
        public Currency Currency { get; set; }

        //EDYCJE KATALOGU
        public string[] CatalogueNo { get; set; } =
            { "K51","K52","K53","K54","K55","K56",
              "K57","K58","K59","K60","K61","K62",
              "K63","K64","K65","K66"};

        // METODY ...........................................................................  
        //...................................................................................

        //PODZIAŁ WIERSZA WG RODZAJU PLIKU

        //PODZIAŁ WIERSZA Z PLIKU DAT
        public void SetSplitFromItemRecordDat(string line, int fileType)

        {
            var split = line.Split('|');

            if (fileType == 1 || fileType == 3)
            {
                CustomerItemNumber = split[0];
                ItemNumber = split[1];
                ItemSize = split[2];
                ItemPrice1 = split[5];
                ItemPrice2 = split[6];
                ItemAmount1 = split[7];
                ItemAmount2 = split[8];
            }

            else if (fileType == 2)
            {
                CustomerItemNumber = split[0];
                ItemNumber = split[1];
                ItemSize = split[2];
                ItemName = split[3];
                ItemPrice1 = split[4];
                ItemPrice2 = split[5];  // JESLI WYSTĘPUJE ILOŚĆ2
                ItemAmount1 = split[7]; //  W PLIKU JEST TYLKO CENA2
                ItemAmount2 = split[6];
            }

        }

        //KONWERSJA WIERSZA PLIKU
        public void SetItemAfterImport(double exchangeRate)
        {
            // NOWY FORMAT LINII W PLIKU 
            //KONWERSJA WIERSZA PLIK 01/04 NA 02
            if ((int)FileType == 2)
            {
                List<string> linesConverted = new List<string>();

                //KONWERSJA WIERSZY W TABLICY IMPORTED LINES
                foreach (string line in ImportedLines)
                {
                    var split = line.Split('|');
                    CustomerItemNumber = split[0];
                    ItemNumber = split[1];
                    ItemSize = split[2];
                    ItemName = split[3];
                    ItemPrice1 = split[4];
                    ItemPrice2 = split[5];  // JESLI WYSTĘPUJE ILOŚĆ2
                    ItemAmount1 = split[7]; //  W PLIKU JEST TYLKO CENA2
                    ItemAmount2 = split[6];

                    //  USTAWIENIA PRICE1 ORAZ AMOUNT 1 Z PLIKU 01/04 ####

                    ItemPrice1 = ItemPrice1.Replace(".", "");

                    if (String.IsNullOrEmpty(ItemPrice2))
                    {
                        ItemPrice2 = ItemPrice1;
                    }

                    ItemPrice2 = ItemPrice2.Replace(".", "");

                    if (String.IsNullOrEmpty(ItemAmount2))
                    {
                        ItemAmount1 = "1";
                        ItemAmount2 = "1";
                    }

                    else if (!String.IsNullOrEmpty(ItemAmount2))
                    {
                        ItemAmount1 = "1";
                        ItemAmount2 = ItemAmount2.Trim();
                    }

                    // PRZELICZENIE * KURS
                    ItemPrice1 = string.Format("{0:0.00}", exchangeRate * double.Parse(ItemPrice1));
                    ItemPrice2 = string.Format("{0:0.00}", exchangeRate * double.Parse(ItemPrice2));
                    // #######################################

                    linesConverted.Add(CustomerItemNumber + "|" + ItemNumber + "|" + ItemSize + "|||" + ItemPrice1 + "|" + ItemPrice2 + "|" + ItemAmount1 + "|" + ItemAmount2 + "||||");
                }


                //ZAPIS LINII PO KONWERSJI DO PLIKU TYMCZASOWEGO
                ConvertedLines = linesConverted.ToArray();
                File.WriteAllLines(TempFilePath, ConvertedLines);
            }

            //KONWERSJA WIERSZA PLIKU  02
            if ((int)FileType == 1)
            {
                //JEŚLI WALUTĄ JEST EUR TO KONIEC PROCEDURY
                if (exchangeRate == 1)
                {
                    return;
                }

                List<string> linesConverted = new List<string>();

                foreach (string line in ImportedLines)
                {
                    var split = line.Split('|');
                    CustomerItemNumber = split[0];
                    ItemNumber = split[1];
                    ItemSize = split[2];
                    ItemPrice1 = split[5];
                    ItemPrice2 = split[6];
                    ItemAmount1 = split[7];
                    ItemAmount2 = split[8];

                    // PRZELICZENIE * KURS
                    ItemPrice1 = string.Format("{0:0.00}", exchangeRate * double.Parse(ItemPrice1));
                    ItemPrice2 = string.Format("{0:0.00}", exchangeRate * double.Parse(ItemPrice2));
                    // #######################################

                    linesConverted.Add(CustomerItemNumber + "|" + ItemNumber + "|" + ItemSize + "|||" + ItemPrice1 + "|" + ItemPrice2 + "|" + ItemAmount1 + "|" + ItemAmount2 + "||||");
                }


                //ZAPIS LINII PO KONWERSJI DO PLIKU TYMCZASOWEGO
                ConvertedLines = linesConverted.ToArray();
                File.WriteAllLines(TempFilePath, ConvertedLines);
            }
        }

        //PODZIAŁ WIERSZA Z PLIKU CSV CUSTOMER NUMBER
        public void SetSplitFromItemCSVCustomerNo(string line)

        {
            var split = line.Split(';');
            ItemNumber = split[0];
            CustomerItemNumber = split[1];

        }

        //PODZIAŁ WIERSZA Z PLIKU CSV CENNIK DO ESHOPA
        public void SetSplitFromItemCSVPriceFile(string line)

        {
            var split = line.Split(';');
            ItemNumber = split[0];
            ItemPrice1 = split[1];
            ItemPrice2 = split[2];
            ItemAmount1 = split[3];
            ItemAmount2 = split[4];
        }

        //USTAWIENIE AUTOMATYCZNEGO NR KATALOGU WG DATY
        public int SetCurrentCatalogYear()
        {
            int catalogueYear = int.Parse(DateTime.Now.Year.ToString());

            if ((int)DateTime.Now.Month >= 6)
            {
                return catalogueYear - 2021 + 1;
            }

            else
            {
                return catalogueYear - 2021;
            }

        }

    }
}
