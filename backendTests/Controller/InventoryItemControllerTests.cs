using backend.Controllers;
using backend.Helpers;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace backendTests.Controller
{
    public class InventoryItemControllerTests
    {
        private readonly Mock<IInventoryRepository> _mockRepo;
        private readonly InventoryController _controller;

        public InventoryItemControllerTests()
        {
            _mockRepo = new();
            _controller = new InventoryController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllItems_ReturnsOkResult_WhenModelStateIsValid()
        {
            // Arrange
            var query = new QueryObject();
            var items = new PagedResult<InventoryItem>
            {
                Items =
                [
                    new InventoryItem(1, "Item 1", 1, 5),
                    new InventoryItem(2, "Item 2", 10, 15),
                ],
                TotalCount = 2,
                TotalPages = 1,
            };

            _mockRepo.Setup((repo) => repo.GetAllAsync(query)).ReturnsAsync(items);

            // Act
            var result = await _controller.GetAllItems(query);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedItems = Assert.IsType<PagedResult<InventoryItem>>(okResult.Value);
            Assert.Equal(2, returnedItems.TotalCount);
            Assert.Equal(1, returnedItems.TotalPages);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WhenItemExistsAsync()
        {
            // Arrange
            var itemId = 1;
            var item = new InventoryItem
            {
                Id = itemId,
                Name = "Item 1",
                Quantity = 0,
                Price = 0,
            };

            _mockRepo.Setup(repo => repo.GetByIdAsync(itemId)).ReturnsAsync(item);

            // Act
            var result = await _controller.GetById(itemId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedItem = Assert.IsType<InventoryItem>(okResult.Value);
            Assert.Equal(itemId, item.Id);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenItemDoesNotExist()
        {
            // Arrange
            var itemId = 1;
            _mockRepo.Setup(repo => repo.GetByIdAsync(itemId)).ReturnsAsync((InventoryItem?)null);

            // Act
            var result = await _controller.GetById(itemId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task Add_ReturnsCreatedResult_WhenItemIsValid()
        {
            // Arrange
            var newItem = new InventoryItemDTO { Name = "NewItem" };
            var createdItem = new InventoryItem { Id = 1, Name = "NewItem" };

            _mockRepo.Setup(repo => repo.CreateAsync(newItem)).ReturnsAsync(createdItem);

            // Act
            var result = await _controller.Add(newItem);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal("GetById", createdAtActionResult.ActionName);
            Assert.Equal(createdItem.Id, createdAtActionResult?.RouteValues?["id"]);
        }

        [Fact]
        public async Task Update_ReturnsOkResult_WhenItemIsUpdated()
        {
            // Arrange
            var itemId = 1;
            var updatedItem = new InventoryItemDTO { Name = "UpdatedItem" };
            var returnedItem = new InventoryItem { Id = itemId, Name = "UpdatedItem" };

            _mockRepo
                .Setup(repo => repo.UpdateAsync(itemId, updatedItem))
                .ReturnsAsync(returnedItem);

            // Act
            var result = await _controller.Update(itemId, updatedItem);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var item = Assert.IsType<InventoryItem>(okResult.Value);
            Assert.Equal(itemId, item.Id);
        }

        [Fact]
        public async Task Update_ReturnsNotFound_WhenItemDoesNotExist()
        {
            // Arrange
            var itemId = 1;
            var updatedItem = new InventoryItemDTO { Name = "UpdatedItem" };

            _mockRepo
                .Setup(repo => repo.UpdateAsync(itemId, updatedItem))
                .ReturnsAsync((InventoryItem?)null);

            // Act
            var result = await _controller.Update(itemId, updatedItem);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteItem_ReturnsNoContent_WhenItemIsDeleted()
        {
            // Arrange
            var itemId = 1;

            _mockRepo
                .Setup(repo => repo.DeleteAsync(itemId))
                .ReturnsAsync(new InventoryItem { Id = itemId });

            // Act
            var result = await _controller.DeleteItem(itemId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteItem_ReturnsNotFound_WhenItemDoesNotExist()
        {
            // Arrange
            var itemId = 1;

            _mockRepo.Setup(repo => repo.DeleteAsync(itemId)).ReturnsAsync((InventoryItem?)null);

            // Act
            var result = await _controller.DeleteItem(itemId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
