using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Fachurnik
{
    public enum FileType
    {
        unknown, // nieznany błędny
        eshopPricefile, // cennik dla klienta
        datPriceFile, // kanał 01,03 lub 04
        rabattstuffe, // plik z rabatami
    }
    class FileManager
    {

        // POLA .............................................................................   
        //...................................................................................

        //RODZAJ PRZETWARZANEGO PLIKU

        public FileType FileType { get; set; }

        //FLAGA CZY TO JEST NOWY PLIK
        public bool IsNewFile { get; set; }

        //FLAGA CZY TO JEST NOWY PLIK2
        public bool IsNewFile2 { get; set; }

        //NAZWA WYBRANEGO PLIKU 
        public string FileName { get; set; }

        //ŚCIEŻKA DO PLIKU
        public string FilePath { get; set; }

        //NAZWA WYBRANEGO PLIKU2 
        public string FileName2 { get; set; }

        //ŚCIEŻKA DO PLIKU2
        public string FilePath2 { get; set; }

        //ŚCIEŻKA PLIKU TYMCZASOWEGO
        public string TempFilePath { get; set; }

        //ŚCIEŻKA PLIKU2 TYMCZASOWEGO
        public string TempFilePath2 { get; set; }

        //ŚCIEŻKA PLIKU Z BŁĘDAMI TYMCZASOWEGO
        public string TempErrorFilePath { get; set; }

        //ŚCIEŻKA PLIKU Z BŁĘDAMI TYMCZASOWEGO
        public string TempErrorFilePath2 { get; set; }

        //IMPROTOWANE LINIE DO PLIKU TYMCZASOWEGO
        public string[] ImportedLines { get; set; }

        //IMPROTOWANE LINIE DO PLIKU2 TYMCZASOWEGO
        public string[] ImportedLines2 { get; set; }

        //ŚCIEŻKA PLIKU DOCELOWEGO NA PULPICIE
        public string DesktopFilePath { get; set; } =
        Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString();

        //LINIE PRZETWORZENE DO PLIKU WYJŚCIOWEGO
        public string[] ConvertedLines { get; set; }

        //NAGŁÓWEK PLIKU
        public string Header{ get; set; }

        // METODY ...........................................................................  
        //...................................................................................


        // WSKAZANIE PLIKU DO WCZYTANIA


        public string GetFileName(string oldFile)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //FLAGA CZY TO JEST NOWY PLIK
                IsNewFile = true;
                //USTAWIENIE ZMIENNYCH PLIKU NAZWA I ŚCIEŻKA
                FilePath = openFileDialog.FileName;
                return FileName = Path.GetFileName(openFileDialog.FileName);
            }

            else
            {
                IsNewFile = false;
                return FileName = oldFile;
            }

        }

        // WSKAZANIE PLIKU2 DO WCZYTANIA
        public string GetFileName2(string oldFile)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                IsNewFile2 = true;
                //USTAWIENIE ZMIENNYCH PLIKU NAZWA I ŚCIEŻKA
                FilePath2 = openFileDialog.FileName;
                return FileName2 = Path.GetFileName(openFileDialog.FileName);
            }

            else
            {
                IsNewFile2 = false;
                return FileName2 = oldFile;
            }

        }

        //WCZYTANIE/SPRAWDZENIE ŚCIEZKI DO PLIKU
        public string GetFilePath(string filePath)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                return "NIE WYBRANO PLIKU";
            }

            else if (!File.Exists(System.IO.Path.GetFullPath(filePath)))
            {
                return "WYBRANY PLIK NIE ISTNIEJE";
            }

            return FilePath = System.IO.Path.GetFullPath(filePath);
        }

        //WCZYTANIE/SPRAWDZENIE ŚCIEZKI DO PLIKU2
        public string GetFilePath2(string filePath)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                return "NIE WYBRANO PLIKU";
            }

            else if (!File.Exists(System.IO.Path.GetFullPath(filePath)))
            {
                return "WYBRANY PLIK NIE ISTNIEJE";
            }

            return FilePath2 = System.IO.Path.GetFullPath(filePath);
        }


        //SPRAWDZENIE CZY PLIK MA FORMAT ".DAT"
        public bool IsFileDat(string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
            {
                MessageBox.Show("NIE WYBRANO PLIKU", "BŁĄD PLIKU", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

            else if (fileName.Contains(".dat"))
            {
                return true;
            }

            else
            {
                MessageBox.Show("NIEPRAWIDŁOWY FORMAT PLIKU", "BŁĄD PLIKU", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }

        //SPRAWDZENIE CZY PLIK MA FORMAT ".CSV"
        public bool IsFileCsv(string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
            {
                MessageBox.Show("NIE WYBRANO PLIKU", "BŁĄD PLIKU", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;

            }

            else if (fileName.Contains(".csv"))
            {
                return true;
            }
            else
            {
                MessageBox.Show("NIEPRAWIDŁOWY FORMAT PLIKU", "BŁĄD PLIKU", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }

        //WCZYTANIE PLIKU DO FOLDERU TEMP ORAZ DO TABLICY IMPORTED LINES
        public void ReadFile(string filePath, bool isHeader, bool copyHeader)
        {
            // UTWORZENIE KOPII WCZYTANEGO PLIKU DO FOLDERU TEMPORARY
            TempFilePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\..\\..\\Temporary\\" + FileName;
            TempErrorFilePath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\..\\..\\Temporary\\Raport_z_walidacji.txt";

            System.IO.File.Copy(filePath, TempFilePath, true);

            // TWORZENIE PLIKU Z BŁĘDAMI WALIDACJI DO FOLDERU TEMPORARY
            File.Create(TempErrorFilePath).Dispose();

            //USUNIĘCIE PIERWSZEGO WIERSZA I ZAPIS DO PLIKU TYMCZASOWEGO
            if (isHeader)
            {
                if (copyHeader)
                {
                    Header = File.ReadAllLines(TempFilePath).First();
                }
                File.WriteAllLines(TempFilePath, File.ReadAllLines(TempFilePath).Skip(1));
            }

            //IMPORT LINII PLIKU DO PAMIĘCI
            ImportedLines = File.ReadAllLines(TempFilePath);
        }

        //ZAPISANIE PLIKU NA PULPICIE
        public void SaveFileToDesktop(string fileName, string tempFilePath)
        {
            //USUNIĘCIE PUSTYCH WIERSZY
            var lines = File.ReadAllLines(tempFilePath).Where(arg => !string.IsNullOrWhiteSpace(arg));
            File.WriteAllLines(tempFilePath, lines);

            int counter = lines.Count();
            //UTWORZENIE PLIKU NA PULPICIE
            System.IO.File.Copy(tempFilePath, DesktopFilePath + "\\" + fileName, true);

            //KOMUNIKAT
            MessageBox.Show("PLIK ZAPISANO NA PULPICIE" + Environment.NewLine
                + $"ILOŚĆ LINII: {counter}", "KONIEC PRZETWARZANIA",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            //USUNIĘCIE Z FOLDERU TEMP
            File.Delete(tempFilePath);

        }

        //ZMIANA  NAGŁÓWKA W PLIKU 
        public void ChangeHeaderInFile(string header, string tempFilePath)
        {
            //DODANIE NAGŁÓWKA
            var txtLines = File.ReadAllLines(tempFilePath).ToList();
            txtLines.Insert(0, header);
            File.WriteAllLines(tempFilePath, txtLines);
        }

        //WCZYTANIE PLIKU CSV DO FOLDERU TEMP ORAZ DO TABLICY IMPORTED LINES
        public void ReadFileCSV(string filePath, bool isHeader)
        {
            // KOPIA WCZYTANEGO PLIKU DO FOLDERU TEMPORARY
            TempFilePath2 = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\..\\..\\Temporary\\" + FileName2;
            TempErrorFilePath2 = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\..\\..\\Temporary\\Raport_z_walidacji.txt";

            System.IO.File.Copy(filePath, TempFilePath2, true);

            // TWORZENIE PLIKU Z WYNIKAMI WALIDACJI DO FOLDERU TEMPORARY
            File.Create(TempErrorFilePath2).Dispose();

            //USUNIĘCIE PIERWSZEGO WIERSZA
            if (isHeader)
            {
                File.WriteAllLines(TempFilePath2, File.ReadAllLines(TempFilePath2).Skip(1));
            }
            //POMINIĘCIE PIERWSZEGO WIERSZA
            ImportedLines2 = File.ReadAllLines(TempFilePath2);
        }


        //--------------------------------------------------------------------------
        // DO SPRAWDZENIA










    }
}
