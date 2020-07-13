using System;

namespace CSharpStudy
{
    [Serializable]
    public sealed class TestFailException : Exception
    {
        public TestFailException(Object expected, Object actual)
            : base($"Expected: {expected}, Actual: {actual}")
        {
        }
    }
}