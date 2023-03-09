using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fachurnik
{
    class Description
    {
        // OPIS FORMULARZ WALIDACJI PLIKÓW DAT
        public string validatorFormDescription()
        {
            string description =
             " WALIDACJA PLIKU CENOWEGO \r\n" +
             " PROGRAM ROZPOZNA AUTOMATYCZNIE KANAŁ DYSTRYBUCJI \r\n" +
             " ----------------------------------------------------- \r\n" +
             " 1. WYBIERZ PLIK \r\n" +
             " 2. ROZPOCZNIJ WALIDACJĘ \r\n";

            return description;
        }

        // WORKING AREA
        public string eshopFormRabattStuffenDescription()
        {
            string description =
             " TWORZENIE PLIKU RABATTSTUFFEN \r\n" +
             " PROGRAM ROZPOZNA AUTOMATYCZNIE KANAŁ DYSTRYBUCJI \r\n" +
             " DOPUSZCZALNY FORMAT *.dat. \r\n" +
             " ----------------------------------------------------- \r\n" +
             " 1. WYBIERZ PLIK \r\n" +
             " 2. SPRAWDŹ NR KATALOGU \r\n" +
             " 3. SPRAWDŹ DATĘ OBOWIĄZYWANIA \r\n" +
             " 4. WPROWADŹ NAZWĘ PLIKU\r\n" +
             " 5. ROZPOCZNIJ WALIDACJĘ I TWORZENIE PLIKU\r\n";

            return description;
        }

        public string eshopFormPreisFileDescription()
        {
            string description =
             " TWORZENIE PLIKU CENOWEGO DO ESHOPA \r\n" +
             " PROGRAM ROZPOZNA AUTOMATYCZNIE KANAŁ DYSTRYBUCJI \r\n" +
             " DOPUSZCZALNY FORMAT *.dat. \r\n" +
             " ----------------------------------------------------- \r\n" +
             " 1. WYBIERZ PLIK \r\n" +
             " 2. SPRAWDŹ NR KATALOGU  \r\n" +
             " 3. DATA OBOWIĄZYWANIA UZUPEŁNI SIĘ AUTOMATYCZNIE\r\n" +
             " 4. WPROWADŹ NR KLIENTA  \r\n" +
             " 5. WPROWADŹ NR GRUPY RABATOWEJ\r\n" +
             " 6. WPROWADŹ NR GVL-A \r\n" +
             " 7. NAZWA PLIKU WCZYTA SIĘ AUTOMATYCZNIE \r\n" +
             " 8. WYBIERZ WALUTĘ \r\n" +
             " 9. ROZPOCZNIJ WALIDACJĘ I TWORZENIE PLIKU \r\n";

            return description;
        }

        public string eshopFormCustomerItemNumberDescription()
        {
            string description =
                 " DOPISANIE NUMERACJI WŁASNEJ KLIENTA DO PLIKU CENOWEGO DO ESHOPA \r\n" +
                 " DOPUSZCZALNE FORMATY *.csv *.dat. \r\n" +
                 " W PRZYPADKU PLIKÓW  *.csv OBOWIĄZUJE UKŁAD W KOLUMNACH: \r\n" +
                 " NR KATALOGOWY Z ROZMIAREM; NR WŁASNY KLIENTA  \r\n" +
                 " ----------------------------------------------------- \r\n" +
                 " 1. WYBIERZ PLIK *csv W UKŁADZIE DWUKOLUMNOWYM: \r\n" +
                 "   - numer artykułu z rozmiarem \r\n" +
                 "   - numer własny \r\n" +
                 " 2. WYBIERZ PLIK CENOWY *dat  \r\n" +
                 " 3. ROZPOCZNIJ WALIDACJĘ I TWORZENIE PLIKU \r\n";

            return description;
        }

        public string eshopFormPriceFileFromCSVDescription()
        {
            string description =
                 " TWORZENIE PLIKU CENOWEGO DO ESHOPA \r\n" +
                 " DOPUSZCZALNE FORMATY *.csv *.dat. \r\n" +
                 " OBOWIĄZUJE UKŁAD W 5 KOLUMNACH: \r\n" +
                 " NR KAT. Z ROZMIAREM; CENA1, CENA2, ILOŚĆ1, ILOŚĆ2  \r\n" +
                 " ----------------------------------------------------- \r\n" +
                 " 1. WYBIERZ PLIK \r\n" +
                 " 2. SPRAWDŹ NR KATALOGU  \r\n" +
                 " 3. DATA OBOWIĄZYWANIA UZUPEŁNI SIĘ AUTOMATYCZNIE\r\n" +
                 " 4. WPROWADŹ NR KLIENTA  \r\n" +
                 " 5. WPROWADŹ NR GRUPY RABATOWEJ\r\n" +
                 " 6. WPROWADŹ NR GVL-A \r\n" +
                 " 7. NAZWA PLIKU WCZYTA SIĘ AUTOMATYCZNIE \r\n" +
                 " 8. WYBIERZ WALUTĘ \r\n" +
                 " 9. ROZPOCZNIJ WALIDACJĘ I TWORZENIE PLIKU \r\n";

            return description;
        }

        public string BMECatFormFromDATDescription()
        {
            string description =
             " TWORZENIE PLIKU CSV DO KATALOGU BMECAT\r\n" +
             " PROGRAM ROZPOZNA AUTOMATYCZNIE KANAŁ DYSTRYBUCJI \r\n" +
             " DOPUSZCZALNY FORMAT *.dat. \r\n" +
             " ----------------------------------------------------- \r\n" +
             " 1. WYBIERZ PLIK \r\n" +
             " 2. SPRAWDŹ NR KATALOGU  \r\n" +
             " 3. DATA OBOWIĄZYWANIA UZUPEŁNI SIĘ AUTOMATYCZNIE\r\n" +
             " 4. WPROWADŹ NR KLIENTA  \r\n" +
             " 5. NAZWA PLIKU WCZYTA SIĘ AUTOMATYCZNIE \r\n" +
             " 6. WYBIERZ WALUTĘ \r\n" +
             " 7. ROZPOCZNIJ WALIDACJĘ I TWORZENIE PLIKU \r\n";

            return description;
        }
    }
}
