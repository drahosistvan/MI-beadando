namespace MestersegesIntelligenciaAlapjaiBeadando
{
    class JatekosLeptetoAllapot : AbsztraktÁllapot
    {
        Cell[,] field;
        int rows;
        int columns;
        int playerPositionX;
        int playerPositionY;
        List<String> blueCells;
        List<String> redCells;

        Movement previousMovement;

        public JatekosLeptetoAllapot(int rows, int cols, List<string> blueCellPositions, List<string> redCellPositions)
        {
            // set starting state
            this.rows = rows;
            this.columns = cols;

            this.blueCells = blueCellPositions;
            this.redCells = redCellPositions;

            field = new Cell[rows, columns];

            for (int x = 0; x < columns; x++)
            {
                List<string> cellList = new List<string>();
                for (int y = 0; y < rows; y++)
                {
                    bool isStart = (x == rows - 1 && y == 0);
                    bool isEnd = (x == 0 && y == columns - 1);


                    CellColor color = CellColor.White;
                    string cellString = x + "," + y;
                    if (blueCells.Contains(cellString)) { color = CellColor.Blue; }
                    if (redCells.Contains(cellString)) { color = CellColor.Red; }

                    CellType type = CellType.Standard;
                    if (isStart)
                    {
                        type = CellType.Start;
                        playerPositionX = x;
                        playerPositionY = y;
                    }
                    if (isEnd) { type = CellType.End; }

                    field[x, y] = new Cell(x, y, color, type);
                    cellList.Add(color.ToString());
                }
                Console.WriteLine(string.Join('\t', cellList));
            }
        }

        public Cell getCurrentPlayerCell()
        {
            return field[playerPositionX, playerPositionY];
        }
        public override bool ÁllapotE()
        { // if the cell position is valid
            return playerPositionX >= 0 && playerPositionX < columns && playerPositionY >= 0 && playerPositionY < rows;
        }
        public override bool CélÁllapotE()
        {
            // if the player's current position is the end cell.
            return getCurrentPlayerCell().type == CellType.End;
        }
        private bool preOp(Movement movement)
        {
            // previous movement is always available
            List<Movement> availableMovements = [previousMovement];

            switch (getCurrentPlayerCell().color)
            {
                // if cell is blue, then left movement compared to the previous one is allowed
                case CellColor.Blue:
                    switch (previousMovement)
                    {
                        case Movement.Up:
                            availableMovements.Add(Movement.Left);
                            break;
                        case Movement.Down:
                            availableMovements.Add(Movement.Right);
                            break;
                        case Movement.Left:
                            availableMovements.Add(Movement.Down);
                            break;
                        case Movement.Right:
                            availableMovements.Add(Movement.Up);
                            break;
                    }
                    break;

                // if cell is blue, then right movement compared to the previous one is allowed
                case CellColor.Red:
                    switch (previousMovement)
                    {
                        case Movement.Up:
                            availableMovements.Add(Movement.Right);
                            break;
                        case Movement.Down:
                            availableMovements.Add(Movement.Left);
                            break;
                        case Movement.Left:
                            availableMovements.Add(Movement.Up);
                            break;
                        case Movement.Right:
                            availableMovements.Add(Movement.Down);
                            break;
                    }
                    break;
            }

            if (availableMovements.Contains(movement)) { return true; }

            return false;
        }
        // calculate the movement
        private bool op(Movement movement)
        {
            if (!preOp(movement)) return false;
            JatekosLeptetoAllapot mentes = (JatekosLeptetoAllapot)Clone();
            switch (movement)
            {
                case Movement.Left:
                    playerPositionY -= 1;
                    break;
                case Movement.Right:
                    playerPositionY += 1;
                    break;
                case Movement.Up:
                    playerPositionX -= 1;
                    break;
                case Movement.Down:
                    playerPositionX += 1;
                    break;
            }
            previousMovement = movement;


            if (ÁllapotE()) return true;
            this.playerPositionX = mentes.playerPositionX;
            this.playerPositionY = mentes.playerPositionY;
            this.previousMovement = mentes.previousMovement;
            return false;
        }
        public override int OperátorokSzáma() { return 4; }
        public override bool SzuperOperátor(int i)
        {
            switch (i)
            {
                case 0: return op(Movement.Up);
                case 1: return op(Movement.Down);
                case 2: return op(Movement.Left);
                case 3: return op(Movement.Right);
                default: return false;
            }
        }
        public override string ToString()
        {
            return "Player steps to: " + playerPositionX + "," + playerPositionY;
        }
        public override bool Equals(Object a)
        {
            JatekosLeptetoAllapot aa = (JatekosLeptetoAllapot)a;
            return aa.playerPositionX == playerPositionX && aa.playerPositionY == playerPositionY && aa.previousMovement == previousMovement;
        }
        // Ha két példány egyenlő, akkor a hasítókódjuk is egyenlő.
        public override int GetHashCode()
        {
            return playerPositionX.GetHashCode() + playerPositionY.GetHashCode() + previousMovement.GetHashCode();
        }
    }

}
