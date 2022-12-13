namespace VetAppointment.Tests.UTs
{
    public class BillingEntryDtoTests
    {
        [Fact]
        public void TestBillingEntryDtoInfo()
        {
            BillingEntryDto billingEntryDto = new BillingEntryDto();
            Guid guid= Guid.NewGuid();
            DateTime dateTime = new DateTime(2022, 12, 12);

            billingEntryDto.AppointmentId = guid;
            billingEntryDto.BillingEntryId = guid;
            billingEntryDto.IssuerId = guid;
            billingEntryDto.CustomerId= guid;
            billingEntryDto.PrescriptionId= guid;
            billingEntryDto.Price = 1;
            billingEntryDto.DateTime = dateTime;

            Assert.AreEqual(guid, billingEntryDto.IssuerId);
            Assert.AreEqual(guid, billingEntryDto.AppointmentId);
            Assert.AreEqual(guid, billingEntryDto.BillingEntryId);
            Assert.AreEqual(guid, billingEntryDto.CustomerId);
            Assert.AreEqual(guid, billingEntryDto.PrescriptionId);
            Assert.AreEqual(1, billingEntryDto.Price);
            Assert.AreEqual(dateTime, billingEntryDto.DateTime);
        }
    }
}
