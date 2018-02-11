
﻿using System;
using System.Collections.Generic;
using AutoService.Models.Common.Enums;
﻿using AutoService.Models.Common.Enums;
            Assert.IsInstanceOfType(employee.Responsibilities, typeof(IList<ResponsibilityType>));
        }

            Assert.IsInstanceOfType(employee.Responsibilities, typeof(List<ResponsibilityType>));
        }


        public void CallValidatorMethodForNullOrEmpty_WhenFirstName_IsChanged()
        {
            //Arrange
            var validator = new Mock<IValidateModel>();
            
            var employee = new Employee("firstname", "testlastname", "testposition", 1000, 10, DepartmentType.Management, validator.Object);
            //Act
            employee.FirstName = "new name";
            
            //Assert
            validator.Verify(x => x.StringForNullEmpty(It.IsAny<string>()), Times.Exactly(4)); // FirstName, LastName, Position + the change of FirstName
        }

       [TestMethod]
       public void CallValidatorMethodForDigitInString_WhenFirstName_IsChanged()
        public void CallValidatorMethod_WhenFirstName_IsEmpty()
            var validator = new Mock<IValidateModel>();

            var employee = new Employee("firstname", "testlastname", "testposition", 1000, 10, DepartmentType.Management, validator.Object);
            //Assert
            employee.Salary = -1000;
            validator.Verify(x => x.NonNegativeValue(It.IsAny<decimal>(), It.IsAny<string>()), Times.Exactly(3)); //salary, rpm + change of salary
            var validator = new Mock<IValidateModel>();
            validator.Setup(x => x.HasDigitInString("joe1234", "last name")).Verifiable();
            //Act
            var employee = new Employee("firstname", "joe1234", "testposition", 1000, 10, DepartmentType.Management, validator.Object);
            //Assert
            validator.Verify();
            Assert.ThrowsException<ArgumentException>(() => employee.IsHired = true);
            validator.Verify(x => x.NonNegativeValue(-10, "rate per minute"), Times.AtLeastOnce);
        }