using NUnit.Framework;

namespace TestCaseSources
{
    public static class UserTestCases
    {
        public static IEnumerable<TestCaseData> GetUserDetailUnsuccessfullyTestCases
        {
            get
            {
                yield return new TestCaseData("default", "ABC")
                    .SetName("Negative Case Get User: existed UserID; invalid Token");

                yield return new TestCaseData("default", "")
                    .SetName("Negative Case Get User: existed UserID; blank Token");

                yield return new TestCaseData("ABC123", "default")
                    .SetName("Negative Case Get User: invalid UserID; existed Token");

                yield return new TestCaseData("ABC", "ABC")
                    .SetName("Negative Case Get User: invalid UserID; invalid Token");
            }
        }
    }
}
