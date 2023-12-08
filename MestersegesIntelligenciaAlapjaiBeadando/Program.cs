using System.Runtime.CompilerServices;

namespace MestersegesIntelligenciaAlapjaiBeadando
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char solution;
            bool inputIsValid;

            Console.WriteLine(
                    "Készítő: Drahos István (S94VGD) \n " +
                    "Milton Friedman Egyetem\n " +
                    "2023\n\n");

            Console.WriteLine(
                    "Az alábbi program két állapottér reprezentációs feladat futtatását valósítja meg. \n " +
                    "A.) 2.5-ös feladat\n " +
                    "B.) 2.43-as feladat " +
                    "\n\n Kérem adja meg, hogy melyiket szeretné futtatni (A vagy B)");
            do
            {
                string output = Console.ReadLine();

                inputIsValid = !string.IsNullOrEmpty(output) && output.Length == 1;
                solution = inputIsValid ? output[0] : ' ';

                if (inputIsValid && (solution == 'A' || solution == 'B'))
                {
                    inputIsValid = true;
                }
                else
                {
                    Console.WriteLine("Hibás bemenet! Kérem adjon meg A-t vagy B-t.");
                    inputIsValid = false;
                }


            } while (!inputIsValid);

            if (solution.ToString() == "A")
            {
                Solution1();
            } else {
                Solution2();
            }
        }

        static void Solution1()
        {
            AbsztraktÁllapot k = new FruitExchange(13, 46, 59);
            Csúcs S = new Csúcs(k);
            KeresőfávalKereső kereső = new MélységiKeresés();
            Csúcs T = kereső.Keres(S);
            if (T == null) { Console.WriteLine("No solution was found!"); }
            else
            {
                Console.WriteLine("Got the solution!");
                Csúcs Temp = T;
                while (Temp != null)
                {
                    Console.WriteLine(Temp.GetÁllapot().ToString());
                    Temp = Temp.GetSzülő();
                }
            }
            Console.ReadLine();
        }

        static void Solution2()
        {
            List<String> blueCells = ["0,0", "1,0", "1,2", "1,5", "2,2", "3,0", "3,1", "3,2", "3,4", "5,2"];
            List<String> redCells = ["0,4", "2,1", "2,3", "4,0", "4,1", "4,3", "4,4", "5,3", "5,6", "6,2", "6,3"];

            AbsztraktÁllapot k = new JatekosLeptetoAllapot(7, 7, blueCells, redCells);
            Csúcs S = new Csúcs(k);
            KeresőfávalKereső kereső = new MélységiKeresés();
            Csúcs T = kereső.Keres(S);
            if (T == null) { Console.WriteLine("No solution was found!"); }
            else
            {
                Console.WriteLine("Got the solution!");
                Csúcs Temp = T;
                while (Temp != null)
                {
                    Console.WriteLine(Temp.GetÁllapot().ToString());
                    Temp = Temp.GetSzülő();
                }
            }
            Console.ReadLine();
        }
    }

}





