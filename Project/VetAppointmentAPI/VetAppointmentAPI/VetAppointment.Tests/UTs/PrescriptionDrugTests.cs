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
            Result<PrescriptionDrug> prescriptionDrug = PrescriptionDrug.CreatePrescriptionDrug(drugStock, 1);

            Assert.IsTrue(prescriptionDrug.IsSuccess);
            Assert.AreEqual(drugStock.DrugStockId, prescriptionDrug.Entity.Stock.DrugStockId);
            Assert.AreEqual(drug.DrugId, prescriptionDrug.Entity.Stock.Type.DrugId);
            Assert.AreEqual(1, prescriptionDrug.Entity.Quantity);
        }
    }
}
