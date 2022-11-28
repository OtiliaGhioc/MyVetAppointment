using VetAppointment.Domain.Entities;

namespace VetAppointment.Tests.UTs
{
    public class DrugStockTests
    {
        [Fact]
        public void WhenRemoveDrugsFromPublicStock_ThenShouldReturnOk()
        {
            //Arange
            var title = "drug1";
            var price = 30;
            var drug = new Drug(title, price);
            var quantity = 20;
            var stock = new DrugStock(drug, quantity);
            var quantityUpdate = 5;

            //Act
            var result = stock.RemoveDrugsFromPublicStock(quantityUpdate);

            //Assert
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void WhenRemoveMoreDrugsFromPublicStockThanActualStock_ThenShouldReturnFailure()
        {
            //Arange
            var title = "drug1";
            var price = 30;
            var drug = new Drug(title, price);
            var quantity = 20;
            var stock = new DrugStock(drug, quantity);
            var quantityUpdate = 50;

            //Act
            var result = stock.RemoveDrugsFromPublicStock(quantityUpdate);

            //Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be("Not enough stock!");
        }

        [Fact]
        public void WhenRemoveNegativeQuantityOfDrugsFromPublicStock_ThenShouldReturnFailure()
        {
            //Arange
            var title = "drug1";
            var price = 30;
            var drug = new Drug(title, price);
            var quantity = 20;
            var stock = new DrugStock(drug, quantity);
            var quantityUpdate = -2;

            //Act
            var result = stock.RemoveDrugsFromPublicStock(quantityUpdate);

            //Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be("Cannot add negative stock!");
        }
    }
}
