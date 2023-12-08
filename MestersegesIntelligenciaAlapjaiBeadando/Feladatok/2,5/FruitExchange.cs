namespace MestersegesIntelligenciaAlapjaiBeadando
{
    class FruitExchange : AbsztraktÁllapot
    {
        int apple; // ennyi almánk van
        int pear; // ennyi körténk van
        int peach; // ennyi barackunk van
        int fruitCount;

        public FruitExchange(int startApple, int startPear, int startPeach)
        {
            // set starting state
            this.apple = startApple;
            this.pear = startPear;
            this.peach = startPeach;
            this.fruitCount = startApple + startPeach + startPear;
        }

        public override bool ÁllapotE()
        {
            // true if sum of current fruits equals the start fruit count
            // and fruits must be positive number or zero
            return (apple + peach + pear == fruitCount) && apple >= 0 && peach >= 0 && pear >= 0;
        }
        public override bool CélÁllapotE()
        {
            // true if only one type of fruits left
            return (this.apple == 0 && this.pear == 0 && this.peach == this.fruitCount) ||
                (this.apple == this.fruitCount && this.pear == 0 && this.peach == 0) ||
                (this.apple == 0 && this.pear == this.fruitCount && this.peach == 0);
        }
        private bool preOp(bool isApple, bool isPear, bool isPeach)
        {
            // According to the problem's description, we must exchange exactly 2 types of fruit.
            // We have to check if all friut exchange is true, all is false or only 1 is true
            // Accaptable cases: 2 fruit type true, 1 false
            if (
                (isApple && isPear && isPeach) ||
                (!isApple && !isPear && !isPeach) ||
                (isApple && !isPear && !isPeach) ||
                (!isApple && isPear && !isPeach) ||
                (!isApple && !isPear && isPeach)
               )
            {
                return false;
            }

            return true;
        }
        // calculate the movement
        private bool op(bool isApple, bool isPear, bool isPeach)
        {
            if (!preOp(isApple, isPear, isPeach)) return false;
            FruitExchange mentes = (FruitExchange)Clone();

            if (!isApple)
            {
                this.apple += 2;
                this.peach--;
                this.pear--;
            }
            else if (!isPear)
            {
                this.apple--;
                this.peach--;
                this.pear += 2;
            }
            else if (!isPeach)
            {
                this.apple--;
                this.peach += 2;
                this.pear--;
            }

            if (ÁllapotE()) return true;

            pear = mentes.pear;
            peach = mentes.peach;
            apple = mentes.apple;

            return false;
        }
        public override int OperátorokSzáma() { return 3; }
        public override bool SzuperOperátor(int i)
        {
            // available cases:
            // 1 apple, 1 pear to 2 peach exchange
            // 1 pear, 1 peach to 2 apple exchange
            // 1 apple, 1 peach to 2 pear exchange

            switch (i)
            {
                case 0: return op(true, true, false);
                case 1: return op(true, false, true);
                case 2: return op(false, true, true);
                default: return false;
            }
        }
        public override string ToString()
        {
            return this.apple + "," + this.pear + "," + this.peach;
        }
        public override bool Equals(Object a)
        {
            FruitExchange aa = (FruitExchange)a;
            // szj és kj számítható, ezért nem kell vizsgálni
            return aa.apple == apple && aa.pear == pear && aa.peach == peach;
        }
        // Ha két példány egyenlő, akkor a hasítókódjuk is egyenlő.
        public override int GetHashCode()
        {
            return apple.GetHashCode() + pear.GetHashCode() + peach.GetHashCode();
        }
    }

}
