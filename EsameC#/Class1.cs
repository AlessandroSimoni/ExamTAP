namespace EsameC_
{
    public static class Extension
    {
        public static IEnumerable<int> InfiniteComparisonSequence<T>(this IEnumerable<T> source) where T : IComparable<T>
        {
            using var enumSource = source.GetEnumerator();

            if (!enumSource.MoveNext()) yield break;

            int index = 0;

            while (true)
            {
                if (!enumSource.MoveNext()) throw new ArgumentException(nameof(source));

                yield return source.ElementAt(index).CompareTo(source.ElementAt(index + 1));

                index++;
            }
        }
    }
}