using OrderManagementApplication.Models;
using System.Net;

namespace OrderManagementAPI.Integration.Test
{
    [TestCaseOrderer(PriorityOrderer.TypeName, PriorityOrderer.AssemblyName)]
    public class OrderTests : BaseTest
    {
        public OrderTests()
        {
        }

        [Fact, TestPriority(5)]
        public async Task Cancel()
        {
            // Act
            var response = await httpClient.DeleteAsync($"/order/{TestConstants.CreatedData.OrderId}");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact, TestPriority(4)]
        public async Task GetById()
        {
            // Act
            var response = await httpClient.GetAsync($"/order/{TestConstants.CreatedData.OrderId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await DeserializeResponseAsync<OrderViewModel>(response);
            Assert.NotNull(result);
            Assert.Equal("testName 2", result.Name);
        }

        [Fact, TestPriority(3)]
        public async Task Put()
        {
            // Arrange
            var order = new OrderViewModel
            {
                Id = TestConstants.CreatedData.OrderId,
                DeliveryAddress = "testAddress 2",
                Name = "testName 2",
                Products = new List<ProductViewModel> { }
            };
            // Act
            var response = await httpClient.PutAsync($"/order", SerializeToHttpContent(order));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact, TestPriority(2)]
        public async Task GetAll()
        {
            // Act
            var response = await httpClient.GetAsync($"/order");

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await DeserializeResponseAsync<IEnumerable<OrderViewModel>>(response);
            Assert.NotEmpty(result);
        }

        [Fact, TestPriority(1)]
        public async Task Create()
        {
            // Arrange
            var order = new OrderViewModel
            {
                Id = TestConstants.CreatedData.OrderId,
                DeliveryAddress = "testAddress",
                Name = "testName",
                Products= new List<ProductViewModel> { }
            };
            // Act
            var response = await httpClient.PostAsync($"/order", SerializeToHttpContent(order));

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

    }
}