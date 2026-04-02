using NUnit.Framework;

namespace TestCaseSources
{
    public static class BookTestCases
    {
        public static IEnumerable<TestCaseData> AddABookUnsuccessfullyWithInvalidData
        {
            get
            {
                yield return new TestCaseData("", "", "9781449325862")
                    .SetName("Negative Case Add book: token = blank; userID = blank; isbn = valid");

                yield return new TestCaseData("ABC", "ABC", "9781449325862")
                    .SetName("Negative Case Add book: token = invalid; userID = invalid; isbn = blank");

                yield return new TestCaseData("default", "default", "")
                    .SetName("Negative Case Add book: token = valid; userID = valid; isbn = blank");

                yield return new TestCaseData("default", "default", "ABC")
                    .SetName("Negative Case Add book: token = valid; userID = valid; isbn = invalid");
            }
        }

        public static IEnumerable<TestCaseData> DeleteABookUnuccessfullyWithInvalidData
        {
            get
            {
                yield return new TestCaseData("", "", "9781449325862")
                    .SetName("Negative Case Delete book: token = blank; userID = blank; isbn = valid");

                yield return new TestCaseData("ABC", "ABC", "9781449325862")
                    .SetName("Negative Case Delete book: token = invalid; userID = invalid; isbn = blank");

                yield return new TestCaseData("default", "default", "")
                    .SetName("Negative Case Delete book: token = valid; userID = valid; isbn = blank");

                yield return new TestCaseData("default", "default", "9781449325865")
                    .SetName("Negative Case Delete book: token = valid; userID = valid; isbn = invalid");
            }
        }

        public static IEnumerable<TestCaseData> ReplaceABookUnsuccessfullyWithInvalidData
        {
            get
            {
                yield return new TestCaseData("", "", "9781449325862", "9781449337711")
                    .SetName("Negative Case Replace book: token = blank; userID = blank; new isbn = valid; old isbn = valid");

                yield return new TestCaseData("ABC", "ABC", "9781449325862", "9781449337711")
                    .SetName("Negative Case Replace book: token = invalid; userID = invalid; new isbn = valid; old isbn = valid");

                yield return new TestCaseData("default", "default", "9781449325862", "")
                    .SetName("Negative Case Replace book: token = valid; userID = valid; new isbn = valid; old isbn = blank");

                yield return new TestCaseData("default", "default", "9781449325862", "9781449325862")
                    .SetName("Negative Case Replace book: token = valid; userID = valid; new isbn = vaiid; old isbn = invalid");

                yield return new TestCaseData("default", "default", "9781449325865", "9781449337711")
                    .SetName("Negative Case Replace book: token = valid; userID = valid; new isbn = invalid; old isbn = valid");
            }
        }
    }
}
