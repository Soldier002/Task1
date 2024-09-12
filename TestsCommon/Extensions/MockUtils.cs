using Moq;
using Moq.Language.Flow;

namespace TestsCommon.Extensions
{
    public static class MockUtils
    {
        public static Mock<T> Create<T>() where T : class => new(MockBehavior.Strict);
    }
}
