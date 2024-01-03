namespace MestersegesIntelligenciaAlapjaiBeadando
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool inputIsValid;
            string[] validInputs;
            string pickedSolution;

            List<Solution> solutions = solutions = new List<Solution> { new FruitExchangeSolution(), new PlayerStepperSolution() }; ;

            Console.WriteLine(
                    "Készítő: Drahos István (S94VGD) \n " +
                    "Milton Friedman Egyetem\n " +
                    "2023\n\n");

            Console.WriteLine("A program az alábbi állapottér reprezentációs feladatok futtatását valósítja meg.");

            validInputs = new string[solutions.Count];

            int index = 0;
            foreach (var solution in solutions)
            {
                Console.WriteLine("\n\n" + solution.GetNumber());
                Console.WriteLine(solution.GetDescription());

                validInputs[index] = solution.GetNumber();
                index++;
            }

            Console.Write("\n\nKérem adja meg a futtatni kívánt feladat számát: ");

            do
            {
                string output = Console.ReadLine();

                inputIsValid = !string.IsNullOrEmpty(output) && validInputs.Contains(output);

                if (!inputIsValid)
                {
                    Console.WriteLine("\nKérem, hogy megfelelő feladatszámot adjon meg!");
                    Console.Write("Kérem adja meg a futtatni kívánt feladat számát: ");
                }
                
                pickedSolution = output;

            } while (!inputIsValid);

            foreach (var solution in solutions)
            {
                if (solution.GetNumber() == pickedSolution)
                {
                    solution.Solve();
                    break;
                }
            }
        }
    }

    abstract class Solution
    {
        protected string number;
        protected string description;

        public abstract void Solve();
        public abstract string GetNumber();
        public abstract string GetDescription();
    }

    class FruitExchangeSolution : Solution
    {
        public FruitExchangeSolution()
        {
            this.number = "2.5";
            this.description = "Van 13 almánk, 46 körténk és 59 darab barackunk. Egy-egy különböző gyümölcsért cserébe két darabot kapunk a harmadik fajtából a csősztől. Ügyesen csere-berélve érjük el, hogy csak egyetlen fajta gyümölcsünk maradjon!";
        }
        public override void Solve()
        {
            Csúcs startCsúcs;
            GráfKereső kereső;

            startCsúcs = new Csúcs(new FruitExchange(13, 46, 59));
            Console.WriteLine("A kereső egy 47 mélységi korlátos és emlékezetes backtrack.");
            kereső = new BackTrack(startCsúcs, 47, true);
            kereső.megoldásKiírása(kereső.Keresés());

            Console.ReadLine();
        }

        public override string GetNumber()
        {
            return this.number;
        }

        public override string GetDescription()
        {
            return this.description;
        }
    }

    class PlayerStepperSolution : Solution
    {
        public PlayerStepperSolution()
        {
            this.number = "2.43";
            this.description = "A feladat leírásában látható képen látható tábla S jelű mezőjére helyezett figurával a C jelű mezőre kell eljutni. A figurát minden lépésben függőlegesen vagy vízszintesen lehet elmozdítani egy mezővel. A fehér mezőkről előre kell lépni, azaz abba az irányba amelybe a figura a megelőző lépésben lépett. Az előző lépés iránya a figura haladási iránya. A piros mezőkről a figurával előre lehet lépni, vagy pedig a haladási irányhoz képes jobbra fordulva, a kék mezőkről előre, vagy pedig balra fordulva. Az első lépést megelőzően a figuráa haladási iránya észak, tehát az induló mezőről a figurával abba az irányba kell lépni.";
        }
        public override void Solve()
        {
            List<String> blueCells = ["0,0", "1,0", "1,2", "1,5", "2,2", "3,0", "3,1", "3,2", "3,4", "5,2"];
            List<String> redCells = ["0,4", "2,1", "2,3", "4,0", "4,1", "4,3", "4,4", "5,3", "5,6", "6,2", "6,3"];

            Csúcs startCsúcs;
            GráfKereső kereső;

            startCsúcs = new Csúcs(new JatekosLeptetoAllapot(7, 7, blueCells, redCells));
            Console.WriteLine("A kereső egy 27 mélységi korlátos és emlékezetes backtrack.");
            kereső = new MélységiKeresés(startCsúcs, true);
            kereső.megoldásKiírása(kereső.Keresés());

            Console.ReadLine();
        }

        public override string GetNumber()
        {
            return this.number;
        }

        public override string GetDescription()
        {
            return this.description;
        }
    }
}





