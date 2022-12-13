namespace VetAppointment.Tests.UTs
{
    public class ResultOfEntityTests
    {
        [Fact]
        public void TestResultOfEntityInfo()
        {
            Drug drug = new Drug("title", 1);

            Result<Drug> result1 = Result<Drug>.Success(drug);
            Result<Drug> result2 = Result<Drug>.Failure("err");

            Assert.IsTrue(result1.IsSuccess);
            Assert.IsTrue(result2.IsFailure);
            Assert.AreEqual("err", result2.Error);

            if (result1.Entity == null)
                throw new Exception(result1.Error);

            Assert.AreEqual(drug.DrugId, result1.Entity.DrugId);
        }
    }
}
