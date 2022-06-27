using MyLibraryASP.Controllers;
using Microsoft.AspNetCore.Mvc;
using MyLibraryASP.Controllers;
using MyLibraryASP.Models;
using System.Collections.Generic;
using Xunit;
using MyLibraryASP.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using MyLibraryASP.Models;

namespace TestProject1
{
    public class AutorsControllerTests
    {
        MyLibraryASPContext context;
        public AutorsControllerTests()
        {

            context = GetFakeContext();
        }

        //Проверка ViewResult на ворзврат не null  (/Auotrs/Index) 
        [Fact]
        public void IndexViewResultNotNull()
        {
            // Arrange
            AutorsController controller = new AutorsController(context);
            // Act
            ViewResult result = controller.Index().Result as ViewResult;
            // Assert
            Assert.NotNull(result);
        }


        [Fact]
        public void DetailsValidId()
        {
            // Arrange
            int id = 1;
            AutorsController controller = new AutorsController(context);
            // Act
            ViewResult? result = controller.Details(id).Result as ViewResult;
            // Assert
            Assert.NotNull(result.Model);
        }



        [Fact]
        public void CreateViewResultNotNull()
        {
            // Arrange
            AutorsController controller = new AutorsController(context);
            // Act
            ViewResult? result = controller.Create() as ViewResult;
            // Assert
            Assert.NotNull(result.ViewData);
        }

        [Fact]
        public void CreateViewRehsultDataNotNull()
        {
            // Arrange
            AutorsController controller = new AutorsController(context);
            Autor newAutor = new Autor
            {
                Id = 4,
                Name = "Autor 4 Name",
                Lastname = "Autor 4 Lastname",
                MiddleName = "Autor 4 MiddleName"
            };
            // Act
            var result = controller.Create(newAutor);
            // Assert
            Assert.Equal(result.Status, TaskStatus.RanToCompletion);
        }

        [Fact]
        public void DeleteViewResultNotNull()
        {
            // Arrange
            AutorsController controller = new AutorsController(context);
            int id = 1;
            // Act
            var result = controller.Delete(id);
            // Assert
            Assert.Equal(result.Status, TaskStatus.RanToCompletion);
        }

        [Fact]
        public void DeleteData()
        {
            // Arrange
            AutorsController controller = new AutorsController(context);
            int id = 3;
            // Act
            var result = controller.DeleteConfirmed(id);
            // Assert
            Assert.Equal(result.Status, TaskStatus.RanToCompletion);
        }

        [Fact]
        public void EditViewResultNotNull()
        {
            // Arrange
            AutorsController controller = new AutorsController(context);
            int id = 1;
            // Act
            var result = controller.Edit(id);
            // Assert
            Assert.Equal(result.Status, TaskStatus.RanToCompletion);
        }


        // Генерацйия тестовых данных 
        private MyLibraryASPContext GetFakeContext()
        {

            var options = new DbContextOptionsBuilder<MyLibraryASPContext>()
            .UseInMemoryDatabase(databaseName: "AutorsContext")
            .Options;

            var autors = new List<Autor>
            {
                new Autor {Id = 1, Name = "Autor 1 Name",
                    Lastname = "Autor 1 Lastname", MiddleName = "Autor 1 MiddleName"},
                new Autor {Id = 2, Name = "Autor 2 Name",
                    Lastname = "Autor 2 Lastname", MiddleName = "Autor 2 MiddleName"},
                new Autor {Id = 3, Name = "Autor 3 Name",
                    Lastname = "Autor 3 Lastname", MiddleName = "Autor 3 MiddleName"}
            };

            var context = new MyLibraryASPContext(options);
            foreach (var item in autors)
            {
                context.Autor.Add(item);
            }
            context.SaveChangesAsync();

            return context;
        }
    }
}