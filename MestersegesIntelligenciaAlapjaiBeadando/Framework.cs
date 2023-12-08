namespace MestersegesIntelligenciaAlapjaiBeadando
{

    abstract class AbsztraktÁllapot : ICloneable
    {
        // Megvizsgálja, hogy a belső állapot állapot-e.
        // Ha igen, akkor igazat ad vissza, egyébként hamisat.
        public abstract bool ÁllapotE();
        // Megvizsgálja, hogy a belső állapot célállapot-e.
        // Ha igen, akkor igazat ad vissza, egyébként hamisat.
        public abstract bool CélÁllapotE();
        // Visszaadja az alapoperátorok számát.
        public abstract int OperátorokSzáma();
        // A szuper operátoron keresztül lehet elérni az összes operátort.
        // Igazat ad vissza, ha az i.dik alap operátor alkalmazható a belső állapotra.
        // for ciklusból kell hívni 0-tól kezdve az alap operátorok számig. Pl. így:
        // for (int i = 0; i < állapot.OperátorokSzáma(); i++)
        // {
        // AbsztraktÁllapot klón=(AbsztraktÁllapot)állapot.Clone();
        // if (klón.SzuperOperátor(i))
        // {
        // Console.WriteLine("Az {0} állapotra az {1}.dik " +
        // "operátor alkalmazható", állapot, i);
        // }
        // }
        public abstract bool SzuperOperátor(int i);
        // Klónoz. Azért van rá szükség, mert némelyik operátor hatását vissza kell vonnunk.
        // A legegyszerűbb, hogy az állapotot leklónozom. Arra hívom az operátort.
        // Ha gond van, akkor visszatérek az eredeti állapothoz.
        // Ha nincs gond, akkor a klón lesz az állapot, amiből folytatom a keresést.
        // Ez sekély klónozást alkalmaz. Ha elég a sekély klónozás, akkor nem kell felülírni a gyermek osztályban.
        // Ha mély klónozás kell, akkor mindenképp felülírandó.
        public virtual object Clone() { return MemberwiseClone(); }
        // Csak akkor kell felülírni, ha emlékezetes backtracket akarunk használni, vagy mélységi keresést.
        // Egyébként maradhat ez az alap implementáció.
        // Programozás technikailag ez egy kampó metódus, amit az OCP megszegése nélkül írhatok felül.
        public override bool Equals(Object a) { return false; }
        // Ha két példány egyenlő, akkor a hasítókódjuk is egyenlő.
        // Ezért, ha valaki felülírja az Equals metódust, ezt is illik felülírni.
        public override int GetHashCode() { return base.GetHashCode(); }
    }


    // két osztály biztos kell: Csúcs, BackTrack
    // csúcs = (állapot, mélység, szülő-re mutató ref: szülő)
    // S =     (k      , 0      , null)
    // A =     (a      , 1      , S)
    // B =     (b      , 2      , A)
    class Csúcs
    {
        AbsztraktÁllapot állapot;
        public AbsztraktÁllapot GetÁllapot() { return állapot; }
        int mélység;
        public int GetMélység() { return mélység; }
        Csúcs szülő;
        public Csúcs GetSzülő() { return szülő; }



        int következőOperátorIndexe;
        public int GetKövetkezőOperátorIndexe() { return következőOperátorIndexe; }
        public void SetKövetkezőOperátorIndexe(int köv)
        {
            következőOperátorIndexe = köv;
        }
        // konstruktor létrehozza a start csúcs
        public Csúcs(AbsztraktÁllapot k)
        {
            állapot = k;
            mélység = 0;
            szülő = null;
            következőOperátorIndexe = 0;
        }
        public Csúcs(AbsztraktÁllapot állapot, int mélység, Csúcs szülő) : this(állapot)
        {
            this.mélység = mélység;
            this.szülő = szülő;
        }
        // Terminális csúcs-e: Egy csúcs terminális, ha a benne lévő
        // állapot célállapot
        public bool TerminálisE()
        {
            return állapot.CélÁllapotE();
        }
        public override bool Equals(object obj)
        {
            Csúcs másik = obj as Csúcs;
            // két csúcs akkor egyenlő, ha a bennük lévő állapot egyenlő
            return másik.állapot.Equals(this.állapot);
        }
        public override int GetHashCode()
        {
            return állapot.GetHashCode();
        }


    }

    abstract class KeresőfávalKereső
    {
        HashSet<Csúcs> zárt = new HashSet<Csúcs>();
        public abstract void újFelvételeNyiltba(Csúcs új);
        public void Kiterjesztés(Csúcs akt)
        {
            for (int i = 0; i < akt.GetÁllapot().OperátorokSzáma(); i++)
            {
                AbsztraktÁllapot aktÁllapot = akt.GetÁllapot();
                AbsztraktÁllapot újÁllapot = aktÁllapot.Clone() as AbsztraktÁllapot;
                if (újÁllapot.SzuperOperátor(i))
                {
                    Csúcs új = new Csúcs(újÁllapot, akt.GetMélység() + 1, akt);

                    if (zárt.Contains(új)) continue;
                    újFelvételeNyiltba(új);
                }
            }
            // amikor kiterjesztek egy csúcsot, akkor át kell minősíteni
            // zárttá
            // azaz kivesz a nyilatk közül, beteszem a zártak köz.
            // feltételezem, hogy a hívó Pop-pal kivette
            // ezért csak simán beteszem a zártba
            zárt.Add(akt);
        }
        public Csúcs Keres(Csúcs start)
        {
            újFelvételeNyiltba(start);
            while (true)
            {
                Csúcs akt = CsúcsVálasztás();
                if (akt == null) return null;
                if (akt.TerminálisE()) return akt;
                Kiterjesztés(akt);
            }
        }
        public abstract Csúcs CsúcsVálasztás();
    }
    class MélységiKeresés : KeresőfávalKereső
    {
        Stack<Csúcs> nyilt = new Stack<Csúcs>();
        public override void újFelvételeNyiltba(Csúcs új)
        {
            if (!nyilt.Contains(új)) { nyilt.Push(új); }
        }
        public override Csúcs CsúcsVálasztás() { return nyilt.Pop(); }
    }
    class SzélességiKeresés : KeresőfávalKereső
    {
        Queue<Csúcs> nyilt = new Queue<Csúcs>();
        public override void újFelvételeNyiltba(Csúcs új)
        {
            if (!nyilt.Contains(új)) { nyilt.Enqueue(új); }
        }
        public override Csúcs CsúcsVálasztás() { return nyilt.Dequeue(); }

    }
}
