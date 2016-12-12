using bombsweeper;

namespace bombsweeperTests
{
    public class FakeBoard : Board
    {
        public FakeBoard() : base(1)
        {
            
        }

        public override void ExecuteBoardCommand(Coordinate getCell, BoardCommand boardCommand)
        {
            CalledCell = getCell;
            CalledCommand = boardCommand;
        }

        public BoardCommand CalledCommand { get; set; }

        public Coordinate CalledCell { get; set; }
    }
}