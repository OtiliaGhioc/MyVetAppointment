namespace VetAppointment.Tests.UTs
{
    public class DrugDtoTests
    {
        [Fact]
        public void TestDrugDtoInfo()
        {
            DrugDto drugDto = new DrugDto();
            Guid guid= Guid.NewGuid();
            drugDto.Id = guid;
            Assert.AreEqual(guid, drugDto.Id);
        }
    }
}
