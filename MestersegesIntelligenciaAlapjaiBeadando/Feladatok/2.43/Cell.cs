namespace MestersegesIntelligenciaAlapjaiBeadando
{
    class Cell
    {
        public CellColor color;
        public CellType type;
        public int posX;
        public int posY;

        public Cell(int posX, int posY, CellColor color = CellColor.White, CellType type = CellType.Standard)
        {
            this.color = color;
            this.type = type;
            this.posX = posX;
            this.posY = posY;
        }
    }
}
