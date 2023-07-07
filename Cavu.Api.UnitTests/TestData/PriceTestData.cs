using System.Collections;

namespace Cavu.Api.UnitTests.TestData
{
    internal class PriceTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            //winter
            yield return new object[] { new DateTime(2023, 04, 3), new DateTime(2023, 04, 7), 90 };
            yield return new object[] { new DateTime(2023, 04, 8), new DateTime(2023, 04, 9), 40 };
            yield return new object[] { new DateTime(2023, 04, 3), new DateTime(2023, 04, 9), 130 };
            //spring
            yield return new object[] { new DateTime(2023, 04, 3), new DateTime(2023, 04, 7), 90 };
            yield return new object[] { new DateTime(2023, 04, 8), new DateTime(2023, 04, 9), 40 };
            yield return new object[] { new DateTime(2023, 04, 3), new DateTime(2023, 04, 9), 130 };
            //summer
            yield return new object[] { new DateTime(2023, 07, 3), new DateTime(2023, 07, 7), 100 };
            yield return new object[] { new DateTime(2023, 07, 8), new DateTime(2023, 07, 9), 50 };
            yield return new object[] { new DateTime(2023, 07, 3), new DateTime(2023, 07, 9), 150 };
            //autumn
            yield return new object[] { new DateTime(2023, 07, 3), new DateTime(2023, 07, 7), 100 };
            yield return new object[] { new DateTime(2023, 07, 8), new DateTime(2023, 07, 9), 50 };
            yield return new object[] { new DateTime(2023, 07, 3), new DateTime(2023, 07, 9), 150 };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}