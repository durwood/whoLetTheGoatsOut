using bombsweeper;
using NUnit.Framework;

namespace bombsweeperTests
{
    [TestFixture]
    public class CellTests
    {
        [SetUp]
        public void SetUp()
        {
            _testObj = new Cell();
        }

        private Cell _testObj;


        [Test]
        public void AddingAdjacencyNumberChangesContent()
        {
            _testObj.AddBombsAroundCellCount(3);
            _testObj.Reveal();
            Assert.That(_testObj.NeighboringBombCount, Is.EqualTo(3));
        }

        [Test]
        public void AddingBombAddsBombToCell()
        {
            _testObj.AddBomb();
            Assert.IsTrue(_testObj.HasBomb);
        }

        [Test]
        public void AttemptToAddAdjacencyCountShouldNotClearBomb()
        {
            _testObj.AddBomb();
            _testObj.AddBombsAroundCellCount(4);
            Assert.IsTrue(_testObj.HasBomb);
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
            Assert.IsFalse(_testObj.HasBomb);
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
            Assert.IsTrue(_testObj.HasBomb);
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
            Assert.That(!_testObj.IsRevealed);
            Assert.That(!_testObj.IsMarked);
            Assert.That(!_testObj.HasBomb);

            _testObj.ToggleMark();
            Assert.That(_testObj.IsMarked);

            _testObj.ToggleMark();
            Assert.That(!_testObj.IsMarked);

            _testObj.Reveal();
            Assert.That(_testObj.IsRevealed);
            Assert.That(_testObj.IsEmpty);

            _testObj.AddBomb();
            Assert.That(_testObj.HasBomb);
        }

        [Test]
        public void RevealingCellDoesNotAffectContents()
        {
            _testObj.AddBomb();
            _testObj.Reveal();
            Assert.IsTrue(_testObj.HasBomb);
        }

        [Test]
        public void RevealingCellRevealsCell()
        {
            _testObj.Reveal();
            Assert.IsTrue(_testObj.IsRevealed);
        }
    }
}