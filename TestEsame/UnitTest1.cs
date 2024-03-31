using Moq;
using EsameC_;

namespace TestEsame
{
    public class Tests
    {
        /// <summary>
        /// This sanity check only verifies
        /// </summary>
        [Test]
        public void SanityCheck()
        {
            var x1 = new[] { 1, 2, 3 }.InfiniteComparisonSequence();
            var x2 = new List<bool>() { true, false }.InfiniteComparisonSequence();
            // Togliendo il commento alla dichiarazione di x4 il test non dovrebbe più compilare
            // var x4 = new List<Fake>() { new Fake(), new Fake(), new Fake() }.InfiniteComparisonSequence();
        }

        [Test]
        public void Test1()
        {
            List<char> source = new List<char>();

            Assert.That(source.InfiniteComparisonSequence(), Is.Empty);
        }

        [Test]
        public void Test2()
        {
            List<bool> source = new List<bool>()
            {
                true, true, true, true, true
            };

            Assert.That(() => source.InfiniteComparisonSequence().ToList(), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void Test3()
        {
            var mock = new Mock<IComparable>[10];

            for (int i = 0; i < mock.Length; i++)
            {
                mock[i] = new Mock<IComparable>();
                mock[i].Setup(m => m.CompareTo(It.IsAny<object>())).Returns(-1);
            }

            var s = mock.Select(m => m.Object.CompareTo(It.IsAny<object>()));

            var final = s.InfiniteComparisonSequence();

            Assert.That(s.InfiniteComparisonSequence(), Is.EqualTo(final));

            for (int i = 0; i < mock.Length - 1; i++)
            {
                mock[i].Verify(m => m.CompareTo(mock[i + 1]), Times.Once);
            }

        }

        [Test]
        [TestCase(new[] { 1, 2, 3 }, new[] { 1 })]
        [TestCase(new[] { 3, 2, 2, 3, 1, 10 }, new[] { 1, 0, -1, 1, -1 })]
        public void Test4(int[] theSource, int[] expected)
        {
            if (theSource.Length != expected.Length + 1) Assert.Inconclusive();

            int size = expected.Length;

            Assert.That(theSource.InfiniteComparisonSequence().Take(size), Is.EqualTo(expected));
        }

        /*

        [Test]
        [TestCase(new[] { 1, 5, 5, 3, -10, 7 }, new[] { -1, 0, 1, 1, -1 })]
        [TestCase(new[] { "qui", "quo", "qua" }, new[] { -1, 1 })]
        [TestCase(new[] { true, false, false, true }, new[] { 1, 0, -1 })]

        public void Test5<T>(T[] theSource, int[] expected) where T : IComparable<T>
        {
            int size = expected.Length;

            Assert.That(theSource.InfiniteComparisonSequence().Take(size), Is.EqualTo(expected));
        }*/
    }
}