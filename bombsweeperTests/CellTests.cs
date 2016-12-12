using bombsweeper;
using NUnit.Framework;

namespace bombsweeperTests
{
    // foo

    [TestFixture]
    public class CellTests
    {
        [SetUp]
        public void SetUp()
        {
            _testObj = new Cell();
        }

        private Cell _testObj;

        private void ValidateDisplay(char expected)
        {
            Assert.That(_testObj.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void AddingAdjacencyNumberChangesContent()
        {
            _testObj.AddBombsAroundCellCount(3);
            var content = _testObj.Reveal();
            Assert.That(content, Is.EqualTo('3'));
        }

        [Test]
        public void AddingBombAddsBombToCell()
        {
            _testObj.AddBomb();
            Assert.IsTrue(_testObj.HasBomb());
        }

        [Test]
        public void AttemptToAddAdjacencyCountShouldNotClearBomb()
        {
            _testObj.AddBomb();
            _testObj.AddBombsAroundCellCount(4);
            Assert.IsTrue(_testObj.HasBomb());
        }

        [Test]
        public void CannotRevealAMarkedCell()
        {
            _testObj.ToggleMark();
            _testObj.Reveal();
            Assert.IsFalse(_testObj.IsRevealed);
            Assert.True(_testObj.IsMarked);
        }

        [Test]
        public void CellContentsAreEmptyByDefault()
        {
            Assert.IsFalse(_testObj.HasBomb());
        }

        [Test]
        public void CellIsNotMarkedByDefault()
        {
            Assert.IsFalse(_testObj.IsMarked);
        }

        [Test]
        public void CellIsNotRevealedByDefault()
        {
            Assert.IsFalse(_testObj.IsRevealed);
        }

        [Test]
        public void MarkingCellDoesNotAffectContents()
        {
            _testObj.AddBomb();
            _testObj.ToggleMark();
            Assert.IsTrue(_testObj.HasBomb());
        }

        [Test]
        public void MarkingCellHasNoEffectOnRevealedCells()
        {
            _testObj.Reveal();
            _testObj.ToggleMark();
            Assert.IsFalse(_testObj.IsMarked);
        }

        [Test]
        public void MarkingCellTogglesMarkOnHiddenCells()
        {
            _testObj.ToggleMark();
            Assert.IsTrue(_testObj.IsMarked);
            _testObj.ToggleMark();
            Assert.IsFalse(_testObj.IsMarked);
        }

        [Test]
        public void NewCellDisplaysProperly()
        {
            ValidateDisplay(Cell.Block);
            _testObj.ToggleMark();
            ValidateDisplay(Cell.Check);
            _testObj.ToggleMark();
            ValidateDisplay(Cell.Block);
            _testObj.Reveal();
            ValidateDisplay(Cell.Empty);
            _testObj.AddBomb();
            ValidateDisplay(Cell.Bomb);
        }

        [Test]
        public void RevealingCellDoesNotAffectContents()
        {
            _testObj.AddBomb();
            _testObj.Reveal();
            Assert.IsTrue(_testObj.HasBomb());
        }

        [Test]
        public void RevealingCellRevealsCell()
        {
            _testObj.Reveal();
            Assert.IsTrue(_testObj.IsRevealed);
        }
    }
}