using Moq;
using NUnit.Framework;
using SB.API.Controllers;
using SB.Model;
using SB.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace SB.UnitTest
{
    [TestFixture]
    public class InventoryItemControllerUnitTest
    {
        private Mock<IInventoryService> IinventoryService;
        //private Mock<IUserService> IuserService;
        [SetUp]
        public void SetUp()
        {
            IinventoryService = new Mock<IInventoryService>();
        }

        [Test]
        public async void GetInventory_Returns_All()
        {
            var fakeItems = await GetFakeInventoryItems();
            //            var returnsResult = await IinventoryService.Setup(x => x.GetAllAsync()).Returns(fakeItems);
            //            InventoryController controller = new InventoryController(IinventoryService.Object)
            //            {
            //                Request = new HttpRequestMessage()
            //                {
            //#pragma warning disable CS0618 // Type or member is obsolete
            //                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
            //#pragma warning restore CS0618 // Type or member is obsolete
            //                }
            //            };

            // Act
            InventoryController controller = new InventoryController(IinventoryService.Object);
            var invItems = controller.GetAllInventory();

            // Assert
            Assert.IsNotNull(invItems, "Result is null");
            Assert.IsInstanceOf(typeof(IEnumerable<CommonResult>), invItems, "Wrong Model");
            Assert.AreEqual(3, invItems, "Got wrong number of Inventory Items");


        }



        [Test]
        public async void  Get_CorrectInventoryItemID_Returns()
        {
            // Arrange   
            var fakeItems = await GetFakeInventoryItems();
//            IinventoryService.Setup(x => x.GetAsyncById(1)).Returns(fakeItems.Where(x => x.ID == 1).FirstOrDefault());

//            InventoryController controller = new InventoryController(IinventoryService.Object)
//            {
//                Request = new HttpRequestMessage()
//                {
//#pragma warning disable CS0618 // Type or member is obsolete
//                    Properties = { { HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration() } }
//#pragma warning restore CS0618 // Type or member is obsolete
//                }
//            };

            // Act
            InventoryController controller = new InventoryController(IinventoryService.Object);
            var actionResult = await controller.GetUserById(1);
            var contentResult = actionResult;//as OkNegotiatedContentResult<CommonResult>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(1, contentResult, "Got wrong number of Inventory Items");
        }

      

        [Test]
        public async void PostSetsLocationHeader()
        {
            // Arrange
            InventoryController controller = new InventoryController(IinventoryService.Object);

            // Act
            Inventory oInventory = new Inventory { Id = 0, Name = "TestItem1", Description = "TestDesc1", Price = 10, Complated = "Success", IsActive = true, OnDate = DateTime.Now };
            var actionResult = await controller.InsertInventory(oInventory); ///.InsertInventory(inv);
            var createdResult = actionResult;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult);
            Assert.AreEqual(4, createdResult);
        }

      


        private static async Task<CommonResult> GetFakeInventoryItems()
        {
            IEnumerable<Inventory> fakeItems = new List<Inventory>
            {
                    new Inventory {Id=1,Name="TestItem1",Description="TestDesc1",Price=10,Complated="Success",IsActive=true,OnDate= DateTime.Now},
                    new Inventory {Id=2,Name="TestItem2",Description="TestDesc2",Price=25,Complated="Canceled",IsActive=true,OnDate= DateTime.Now},
                    new Inventory {Id=3,Name="TestItem3",Description="TestDesc3",Price=52,Complated="On Hold",IsActive=true,OnDate= DateTime.Now},
                    new Inventory {Id=4,Name="TestItem4",Description="TestDesc4",Price=45,Complated="New",IsActive=true,OnDate= DateTime.Now}
            }.AsEnumerable();

            CommonResult oCommonResult = new CommonResult
            {
                Count = fakeItems.Count(),
                Message = "Inventory",
                Status = StatusCode.Sucess,
                Result =  fakeItems
            };
            await Task.Delay(10);
            return oCommonResult;

            
        }
    }
}
