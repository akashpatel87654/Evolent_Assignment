    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Evolent_Unit_Test
    {
        [TestClass]
        public class TestEmployeeController
        {
            [TestMethod]
            public void GetAllEmployee_ShouldReturnAllEmployee()
            {
                var controller = new EmployeeController();
                var result = controller.GetEmployee();
                Assert.AreEqual(2, result.count());
            }

            [TestMethod]
            public void GetEmployee_ShouldReturnCorrectEmployee()
            {
                var testProducts = GetTestProducts();
                var controller = new EmployeeController();
                var result = controller.SearchEmployee('akash');
                Assert.IsNotNull(result);
                Assert.AreEqual("Akash", result[0].FirstName);
            }

            [TestMethod]
            public void CanCreateEmployee()
            {
                var employee = new Employee()
                {
                    Id = new Guid(),
                    FirstName = "Test Demo",
                    LastName = "Demo Test",
                    Email = "demo.patel@gmail.com",
                    Address = "India",
                    Age = 25
                });

                var controller = new EmployeeController();
                var result = controller.PostEmployee('akash');
                Assert.IsNotNull(result);
                Assert.AreEqual("Test Demo", result[0].FirstName);
            }


        }
    }
