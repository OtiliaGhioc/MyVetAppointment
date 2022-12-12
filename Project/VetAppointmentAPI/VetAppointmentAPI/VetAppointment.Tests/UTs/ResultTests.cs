namespace VetAppointment.Tests.UTs
{
    public class ResultTests
    {
        [Fact]
        public void TestResultInfo()
        {
            Result result = Result.Failure("err");
            Assert.IsTrue(Result.Success().IsSuccess);
            Assert.IsTrue(result.IsFailure);
            Assert.AreEqual("err", result.Error);
        }
    }
}
