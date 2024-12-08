using NUnit.Framework;
using Moq;
using QQWRFO_HSZF_2024251.Model;
using QQWRFO_HSZF_2024251.Presistance.MsSql;
using QQWRFO_HSZF_2024251.Application;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QQWRFO_HSZF_2024251.Tests
{
    [TestFixture]
    public class PersonServiceTests
    {
        private Mock<IPersonDataProvider> _mockDataProvider;
        private PersonService _personService;

        [SetUp]
        public void SetUp()
        {
            _mockDataProvider = new Mock<IPersonDataProvider>();
            _personService = new PersonService(_mockDataProvider.Object);
        }

        [Test]
        public void CreatePerson_ShouldGenerateUniqueNeptunID()
        {
            // Arrange
            var name = "John Doe";
            var age = 25;
            var specialRequest = "Vegetarian";
            var studentOrTeacher = "1";  // Student

            var existingNeptuns = new List<Person>
            {
                new Person { NeptunID = "ABCDE1" },
                new Person { NeptunID = "ABCDE2" }
            };

            _mockDataProvider.Setup(x => x.GetAllPeople()).Returns(existingNeptuns);

            // Act
            _personService.CreatePerson(name, age, specialRequest, studentOrTeacher);

            // Assert
            _mockDataProvider.Verify(x => x.Add(It.IsAny<Person>()), Times.Once);
        }
        [Test]
        public void CreatePerson_ShouldGenerateUniqueNeptunID2()
        {
            // Arrange
            var name = "Pápusztai Sajt";
            var age = 33;
            var specialRequest = "Illatosan";
            var studentOrTeacher = "2";  // Student

            var existingNeptuns = new List<Person>
            {
                new Person { NeptunID = "QQQQQQ" },
                new Person { NeptunID = "QQWRFO" }
            };

            _mockDataProvider.Setup(x => x.GetAllPeople()).Returns(existingNeptuns);

            // Act
            _personService.CreatePerson(name, age, specialRequest, studentOrTeacher);

            // Assert
            _mockDataProvider.Verify(x => x.Add(It.IsAny<Person>()), Times.Once);
        }
        [Test]
        public void CreatePerson_ShouldGenerateUniqueNeptunID3()
        {
            // Arrange
            var name = "Magyar Péter";
            var age = 1;
            var specialRequest = "Fidesz?";
            var studentOrTeacher = "2";  // Student

            var existingNeptuns = new List<Person>
            {
                new Person { NeptunID = "WWWWWW" },
                new Person { NeptunID = "ASDDS3" }
            };

            _mockDataProvider.Setup(x => x.GetAllPeople()).Returns(existingNeptuns);

            // Act
            _personService.CreatePerson(name, age, specialRequest, studentOrTeacher);

            // Assert
            _mockDataProvider.Verify(x => x.Add(It.IsAny<Person>()), Times.Once);
        }

        [Test]
        public void GetAGroupOfPerson_ShouldReturnStudents_WhenXIs0()
        {
            // Arrange
            var people = new List<Person>
            {
                new Person { Student = Job.Student },
                new Person { Student = Job.Teacher }
            };

            _mockDataProvider.Setup(x => x.GetAllPeople()).Returns(people);

            // Act
            var result = _personService.GetAGroupOfPerson(0);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Student, Is.EqualTo(Job.Student));
        }

        [Test]
        public void GetAGroupOfPerson_ShouldReturnTeachers_WhenXIs1()
        {
            // Arrange
            var people = new List<Person>
            {
                new Person { Student = Job.Student },
                new Person { Student = Job.Teacher }
            };

            _mockDataProvider.Setup(x => x.GetAllPeople()).Returns(people);

            // Act
            var result = _personService.GetAGroupOfPerson(1);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Student, Is.EqualTo(Job.Teacher));
        }

        [Test]
        public void Get10People_ShouldReturnCorrectPeople_WhenRangeIsValid()
        {
            // Arrange
            var people = new List<Person>
            {
                new Person { NeptunID = "ABCDE1" },
                new Person { NeptunID = "ABCDE2" },
                new Person { NeptunID = "ABCDE3" },
                new Person { NeptunID = "ABCDE4" },
                new Person { NeptunID = "ABCDE5" },
                new Person { NeptunID = "ABCDE6" }
            };

            _mockDataProvider.Setup(x => x.GetAllPeople()).Returns(people);

            // Act
            var result = _personService.Get10People(0, 0);

            // Assert
            Assert.That(result, Contains.Substring("Neptun ID: ABCDE1"));
        }

        [Test]
        public void GetPersonByNeptun_ShouldReturnPerson_WhenNeptunExists()
        {
            // Arrange
            var neptun = "ABCDE1";
            var person = new Person { NeptunID = neptun };
            _mockDataProvider.Setup(x => x.GetPersonByNeptun(neptun)).Returns(person);

            // Act
            var result = _personService.GetPersonByNeptun(neptun);

            // Assert
            Assert.That(result.NeptunID, Is.EqualTo(neptun));
        }

        [Test]
        public void ModifyRequest_ShouldUpdatePerson_WhenNeptunExists()
        {
            // Arrange
            var neptun = "ABCDE1";
            var existingPerson = new Person { NeptunID = neptun };
            _mockDataProvider.Setup(x => x.GetPersonByNeptun(neptun)).Returns(existingPerson);

            var name = "Jane Doe";
            var age = 30;
            var specialRequest = "Gluten Free";
            var studentOrTeacher = "1";  // Student
            var orderStatus = "Paid";

            // Act
            _personService.ModifyRequest(neptun, name, age, specialRequest, studentOrTeacher, orderStatus);

            // Assert
            Assert.That(existingPerson.Name, Is.EqualTo(name));
            Assert.That(existingPerson.Age, Is.EqualTo(age));
            Assert.That(existingPerson.SpecialRequest, Is.EqualTo(specialRequest));
            Assert.That(existingPerson.Student, Is.EqualTo(Job.Student));
            Assert.That(existingPerson.OrderStatus, Is.EqualTo(Status.Paid));
        }

        [Test]
        public void CheckNeptun_ShouldReturnTrue_WhenNeptunExists()
        {
            // Arrange
            var neptun = "ABCDE1";
            var existingPerson = new Person { NeptunID = neptun };
            _mockDataProvider.Setup(x => x.GetAllPeople()).Returns(new List<Person> { existingPerson });

            // Act
            var result = _personService.CheckNeptun(neptun);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void CheckNeptun_ShouldReturnFalse_WhenNeptunDoesNotExist()
        {
            // Arrange
            var neptun = "ABCDE1";
            _mockDataProvider.Setup(x => x.GetAllPeople()).Returns(new List<Person>());

            // Act
            var result = _personService.CheckNeptun(neptun);

            // Assert
            Assert.That(result, Is.False);
        }

        

    }
}
