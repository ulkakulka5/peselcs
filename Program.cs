using System;

namespace ConsoleApp1
{
    class Pesel
    {
        static void Main()
        {
            bool x = false;

            while (!x)
            {
                Console.WriteLine("Podaj Imię");
                string? imie = Console.ReadLine();

                Console.WriteLine("Podaj Nazwisko");
                string? nazwisko = Console.ReadLine();

                Console.WriteLine("Podaj Datę urodzenia w formacie YYYY-M-D lub YYYY/M/D");
                string? data = Console.ReadLine();

                Console.WriteLine("Podaj Płeć (K/M)");
                string? plec = Console.ReadLine();

                Console.WriteLine("Podaj Pesel");
                string? pesel = Console.ReadLine();

                string imienazwisko = formatdanych(imie, nazwisko);

                int rokint, miesiacint, dzienint;

                if (!sprawdzdate(data, out rokint, out miesiacint, out dzienint))
                {
                    Console.WriteLine("Błędnie podana data. Spróbuj ponownie.");
                    continue; //bez tego nie dzialalo mi
                }

                if (!sprawdzpesel(pesel, plec, rokint, miesiacint, dzienint))
                {
                    Console.WriteLine("Błędnie podany PESEL lub płeć. Spróbuj ponownie.");
                    continue; //bez tego nie dzialalo mi
                }

                x = true;
            }

            Console.WriteLine("Wszystkie dane zostały poprawnie wprowadzone.");
        }

        static bool sprawdzpesel(string pesel, string plec, int rokint, int miesiacint, int dzienint)
        {
            if (!sprawdzlk(pesel))
            {
                return false;
            }

            if (pesel.Length != 11 || !peselliczby(pesel))
            {
                return false;
            }

            int rokpesel = int.Parse(pesel.Substring(0, 2));
            int miesiacpesel = int.Parse(pesel.Substring(2, 2));
            int dzienpesel = int.Parse(pesel.Substring(4, 2));




            if (miesiacpesel >= 1 && miesiacpesel <= 12)
            {
                rokpesel = 1900 + rokpesel;
            }
            else if (miesiacpesel >= 21 && miesiacpesel <= 32)
            {
                rokpesel = 2000 + rokpesel ;

            }
            else if (miesiacpesel >= 41 && miesiacpesel <= 52)
            {
                rokpesel = 2100 + rokpesel;
            }
            else if (miesiacpesel >= 61 && miesiacpesel <= 72)
            {
                rokpesel = 2200 + rokpesel;
            }
            else if (miesiacpesel >= 81 && miesiacpesel <= 92)
            {
                rokpesel = 1800 + rokpesel;
            }
            else
            {
                return false;
            }




                if (rokpesel >= 1800 && rokpesel <= 1899)
                {
                    miesiacpesel = miesiacpesel - 80;
                }
                else if (rokpesel >= 2000 && rokpesel <= 2099)
                {
                    miesiacpesel = miesiacpesel - 20;
                }
                else if (rokpesel >= 2100 && rokpesel <= 2199)
                {
                    miesiacpesel = miesiacpesel - 40;
                }
                else if (rokpesel >= 2200 && rokpesel <= 2299)
                {
                    miesiacpesel = miesiacpesel - 60;
                }


                if (rokpesel != rokint || miesiacpesel != miesiacint || dzienpesel != dzienint)
                {
                    return false;
                }

                int plecpesel = int.Parse(pesel[9].ToString());
                string plecpesel2 = "";
                string plecpesel3 = "";
                if (plecpesel % 2 == 0)
                {
                    plecpesel2 = "K";
                    plecpesel3 = "k";
                }
                else
                {
                    plecpesel2 = "M";
                    plecpesel3 = "m";
                }
                if (plec != plecpesel2 && plec != plecpesel3)
                {
                    return false;
                }

                return true;
            }

        static bool sprawdzdate(string data, out int rokint, out int miesiacint, out int dzienint)
        {
            string rok = "";
            string miesiac = "";
            string dzien = "";

            rokint = 0;
            miesiacint = 0;
            dzienint = 0;

            if (data.Length == 8 || data.Length == 10)
            {
                if (data[4] == '-' || data[4] == '/')
                {
                    rok = data.Substring(0, 4);

                    if (data.Length == 10)
                    {
                        miesiac = data.Substring(5, 2);
                        dzien = data.Substring(8, 2);
                    }
                    else if (data.Length == 8)
                    {
                        miesiac = data.Substring(5, 1);
                        dzien = data.Substring(7, 1);
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }


            if (int.TryParse(rok, out rokint) && int.TryParse(miesiac, out miesiacint) && int.TryParse(dzien, out dzienint))
            {
                if (rokint >= 1800 && rokint <= 2299)
                {
                    if (miesiacint >= 1 && miesiacint <= 12)
                    {
                        if ((miesiacint == 1 || miesiacint == 3 || miesiacint == 5 || miesiacint == 7 || miesiacint == 8 || miesiacint == 10 || miesiacint == 12) && dzienint <= 31)
                        {
                            return true;
                        }
                        else if ((miesiacint == 4 || miesiacint == 6 || miesiacint == 9 || miesiacint == 11) && dzienint <= 30)
                        {
                            return true;
                        }
                        else if (miesiacint == 2)
                        {
                            if (rokint % 4 == 0 && dzienint <= 29)
                            {
                                return true;
                            }
                            else if (dzienint <= 28)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
        static bool sprawdzlk(string pesel)
        {
            int[] wagi = { 1, 3, 7, 9, 1, 3, 7, 9, 1, 3 };
            int suma = 0;

            for (int i = 0; i < 10; i++)
            {
                suma += int.Parse(pesel[i].ToString()) * wagi[i]; //rozwiazanie zainspiorowane interenetem poniewaz moje nie dzialalo :(
            }
            int reszta = suma % 10;
            int cyfraKontrolna = 10 - reszta;
            if (cyfraKontrolna == 10)
            {
                cyfraKontrolna = 0;
            }

            if (cyfraKontrolna == int.Parse(pesel[10].ToString()))
            {

                return true;
            }
            else
            {
                return false;
            }


        }
        static string formatdanych(string imie, string nazwisko)
        {
            string nazwiskok;
            string imie2 = imie.Substring(0, 1).ToUpper() + imie.Substring(1).ToLower();

            int x = nazwisko.IndexOf('-');
            if (x != -1)
            {
                string nazwisko2 = nazwisko.Substring(0, x);
                string nazwisko3 = nazwisko.Substring(x + 1);
                string nazwisko4 = nazwisko2.Substring(0, 1).ToUpper() + nazwisko2.Substring(1).ToLower();
                string nazwisko5 = nazwisko3.Substring(0, 1).ToUpper() + nazwisko3.Substring(1).ToLower();
                nazwiskok = nazwisko4 + '-' + nazwisko5;
            }
            else
            {
                nazwiskok = nazwisko.Substring(0, 1).ToUpper() + nazwisko.Substring(1).ToLower();
            }
            return imie2 + " " + nazwiskok;
        }

        static bool peselliczby(string pesel)
        {
            foreach (char czyliczba in pesel)
            {
                if (!char.IsDigit(czyliczba))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
// static void Error()
//{
//   Console.WriteLine("Błędnie podane dane");
//  Environment.Exit(0);
// }