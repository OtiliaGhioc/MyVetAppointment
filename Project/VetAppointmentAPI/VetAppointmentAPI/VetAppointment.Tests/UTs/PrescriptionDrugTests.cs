using VetAppointment.Domain.Helpers;

namespace VetAppointment.Tests.UTs
{
    public class PrescriptionDrugTests
    {
        [Fact]
        public void TestPrescriptionDrugInfo()
        {
            Drug drug = new Drug("title", 1);
            DrugStock drugStock = new DrugStock(drug, 2);
            var prescriptionDrug = PrescriptionDrug.CreatePrescriptionDrug(drugStock, 3);

            Assert.AreEqual(Result.Success().IsSuccess, prescriptionDrug.IsSuccess);
            Assert.AreEqual(2, prescriptionDrug.Entity.Stock.Quantity);
            Assert.AreEqual("title", prescriptionDrug.Entity.Stock.Type.Title);
            Assert.AreEqual(1, prescriptionDrug.Entity.Stock.Type.Price);
            Assert.AreEqual(3, prescriptionDrug.Entity.Quantity);
        }
    }
}
