using OrderManagementApplication.Models;
using System.Net;

namespace OrderManagementAPI.Integration.Test
{
    [Collection("OrderController")]
    [TestCaseOrderer(PriorityOrderer.TypeName, PriorityOrderer.AssemblyName)]
    public class OrderTests : BaseTest
    {
        public OrderTests()
        {

        }

        [Fact]
        [TestPriority(4)]
        public async Task Cancel()
        {
            // Act
            var response = await httpClient.DeleteAsync($"/order{TestConstants.CreatedData.OrderId}");

            // Assert
            response.EnsureSuccessStatusCode();
            response = await httpClient.GetAsync($"/api/order{TestConstants.CreatedData.OrderId}");
        }

        [Fact]
        [TestPriority(3)]
        public async Task GetById()
        {
            // Act
            var response = await httpClient.GetAsync($"/order/{TestConstants.CreatedData.OrderId}");

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await DeserializeResponseAsync<OrderViewModel>(response);
            Assert.NotNull(result);

            // Act
            await httpClient.DeleteAsync($"/order/{result.Id}");
        }


        [Fact]
        [TestPriority(2)]
        public async Task GetAll()
        {
            // Act
            var response = await httpClient.GetAsync($"/order");

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await DeserializeResponseAsync<IEnumerable<OrderViewModel>>(response);
            Assert.NotEmpty(result);
        }

        [Fact]
        [TestPriority(1)]
        public async Task Create()
        {
            var user = new OrderViewModel
            {
                Id = TestConstants.CreatedData.OrderId,
                DeliveryAddress = "testAddress",
                ProductName = "testName"
            };
            // Act
            var response = await httpClient.PostAsync($"/order", SerializeToHttpContent(user));

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

    }
}