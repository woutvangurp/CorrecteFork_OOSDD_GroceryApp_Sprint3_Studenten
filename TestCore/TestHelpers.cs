using Grocery.Core.Helpers;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using Grocery.Core.Services;
using Microsoft.VisualBasic.CompilerServices;
using Moq;

namespace TestCore {

    public class AuthTests {
        [SetUp]
        public void Setup() {
        }

        //happy flow
        [Test]
        public void TestLoginSucceedsWithValidData() {
            //arrange
            Mock<IClientService> mockClientService = new Mock<IClientService>();
            string password = "user1";
            string hashedPassword = "IunRhDKa+fWo8+4/Qfj7Pg==.kDxZnUQHCZun6gLIE6d9oeULLRIuRmxmH2QKJv2IM08=";
            string email = "user1@mail.com";
            var expectedClient = new Client(1, "M.J. Curie", email, hashedPassword);

            mockClientService.Setup(x => x.Get(email)).Returns(expectedClient);

            AuthService authService = new AuthService(mockClientService.Object);

            //act
            Client? testClient = authService.Login(email, password);

            //assert
            Assert.IsNotNull(testClient);
            Assert.AreEqual("M.J. Curie", testClient.name);
            Assert.AreEqual("user1@mail.com", testClient.EmailAddress);
            Assert.AreEqual(1, testClient.Id);

            mockClientService.Verify(x => x.Get("user1@mail.com"), Times.Once);
        }

        [TestCase("A.J. Kwak", "user3@mail.com", "user3", "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA="),]
        [TestCase("M.J. Curie", "user1@mail.com", "user1", "IunRhDKa+fWo8+4/Qfj7Pg==.kDxZnUQHCZun6gLIE6d9oeULLRIuRmxmH2QKJv2IM08=")]
        public void TestLoginSucceedsWithValidData(string name, string email, string password, string hashedPassword) {
            //arrange
            Mock<IClientService> mockClientService = new Mock<IClientService>();
            Client expectedClient = new Client(1, name, email, hashedPassword);

            mockClientService.Setup(x => x.Get(email)).Returns(expectedClient);
            AuthService authService = new AuthService(mockClientService.Object);

            //act
            Client? testClient = authService.Login(email, password);

            //assert
            Assert.IsNotNull(testClient);
            mockClientService.Verify(x => x.Get(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void TestRegistrationSucceedsWithValidData() {
            // Arrange
            Mock<IClientService> mockClientService = new Mock<IClientService>();
            Client expectedClient = new Client(1, "John Doe", "john@test.com", "hashedpassword");
            mockClientService.Setup(x => x.Create(It.IsAny<Client>())).Returns(expectedClient);

            var authService = new AuthService(mockClientService.Object);
            string firstName = "John";
            string lastName = "Doe";
            string email = "john@test.com";
            string password = "Password123!";
            string verifyPassword = "Password123!";

            // Act
            Client? result = authService.Register(firstName, lastName, email, password, verifyPassword);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("John Doe", result.name);
            Assert.AreEqual("john@test.com", result.EmailAddress);
            mockClientService.Verify(x => x.Create(It.IsAny<Client>()), Times.Once);
        }

        [TestCase("John", "Doe", "john@test.com", "Password123!", "Password123!")]
        [TestCase("Jane", "Smith", "jane@example.com", "SecurePass1!", "SecurePass1!")]
        [TestCase("Bob", "", "bob@domain.com", "MyPassword2@", "MyPassword2@")]
        public void TestRegistrationSucceedsWithValidData(string firstName, string lastName, string email, string password, string verifyPassword) {
            // Arrange
            Mock<IClientService> mockClientService = new Mock<IClientService>();
            string expectedName = string.IsNullOrWhiteSpace(lastName) ? firstName : $"{firstName} {lastName}";
            Client expectedClient = new Client(1, expectedName, email, "hashedpassword");
            mockClientService.Setup(x => x.Create(It.IsAny<Client>())).Returns(expectedClient);

            var authService = new AuthService(mockClientService.Object);

            // Act
            Client? result = authService.Register(firstName, lastName, email, password, verifyPassword);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedName, result.name);
            Assert.AreEqual(email, result.EmailAddress);
            mockClientService.Verify(x => x.Create(It.IsAny<Client>()), Times.Once);
        }

        //unhappy flow
        [Test]
        public void TestRegistrationFailsWithInvalidData() {
            //arrange
            Mock<IClientService> mockClientService = new Mock<IClientService>();
            AuthService authService = new AuthService(mockClientService.Object);

            string firstName = ""; // Invalid: empty first name
            string lastName = "Doe";
            string email = "test@gmail.com";
            string password = "Password123!";
            string verifyPassword = "Password123!";

            //act
            Client? result = authService.Register(firstName, lastName, email, password, verifyPassword);

            //assert
            Assert.IsNull(result);
            mockClientService.Verify(x => x.Create(It.IsAny<Client>()), Times.Never);
        }

        [TestCase("John", "Doe", "invalidemail", "Password123!", "Password123!")] // Invalid email
        [TestCase("Jane", "Smith", "test@valid.mail", "Password123!", "DifferentPass!")] // Passwords do not match
        public void TestRegistrationFailsWithInvalidData(string firstName, string lastName, string Email, string Password, string verifyPassword) {
            //arrange
            Mock<IClientService> mockClientService = new Mock<IClientService>();
            AuthService authService = new AuthService(mockClientService.Object);

            //act
            Client? result = authService.Register(firstName, lastName, Email, Password, verifyPassword);

            //assert
            Assert.IsNull(result);
            mockClientService.Verify(x => x.Create(It.IsAny<Client>()), Times.Never);
        }

        [TestCase("M.J. Curie", "user1@mail.com", "wrongpassword", "IunRhDKa+fWo8+4/Qfj7Pg==.kDxZnUQHCZun6gLIE6d9oeULLRIuRmxmH2QKJv2IM08=")]
        [TestCase("A.J. Kwak", "user3@mail.com", "invalidpass", "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=")]
        [TestCase("M.J. Curie", "user1@mail.com", "", "IunRhDKa+fWo8+4/Qfj7Pg==.kDxZnUQHCZun6gLIE6d9oeULLRIuRmxmH2QKJv2IM08=")]
        [TestCase("A.J. Kwak", "user3@mail.com", "user1", "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=")]
        public void TestLoginFailsWithWrongPassword(string name, string email, string password, string hashedPassword) {
            //arrange
            Mock<IClientService> mockClientService = new Mock<IClientService>();
            var expectedClient = new Client(1, name, email, hashedPassword);
            mockClientService.Setup(x => x.Get(email)).Returns(expectedClient);
            AuthService authService = new AuthService(mockClientService.Object);

            //act
            Client? testClient = authService.Login(email, password);

            //assert
            Assert.IsNull(testClient);
            mockClientService.Verify(x => x.Get(email), Times.Once);
        }

        //edge cases

        [TestCase(" ", " ", "invalidemail", "Password123!", "Password123!")]
        [TestCase("", "", "", "", "")]
        public void TestRegistrationEdgeCases(string firstname, string lastName, string Email, string Password, string verifyPassword) {
            //arrange
            Mock<IClientService> mockClientService = new Mock<IClientService>();
            AuthService authService = new AuthService(mockClientService.Object);

            //act
            Client? result = authService.Register(firstname, lastName, Email, Password, verifyPassword);

            //assert
            Assert.IsNull(result);
            mockClientService.Verify(x => x.Create(It.IsAny<Client>()), Times.Never);
        }

        [Test]
        public void TestLoginFailsWithNonExistentUser() {
            //arrange
            Mock<IClientService> mockClientService = new Mock<IClientService>();
            string email = "nonexistent@mail.com";
            string password = "anypassword";

            mockClientService.Setup(x => x.Get(email)).Returns((Client?)null);
            AuthService authService = new AuthService(mockClientService.Object);

            //act
            Client? testClient = authService.Login(email, password);

            //assert
            Assert.IsNull(testClient);
            mockClientService.Verify(x => x.Get("nonexistent@mail.com"), Times.Once);
        }


    }
    public class TestHelpers {
        [SetUp]
        public void Setup() {
        }

        //Happy flow
        [Test]
        public void TestPasswordHashIsCreated() {
            // Arrange
            string password = "user3";

            // Act
            string passwordHash = PasswordHelper.HashPassword(password);

            // Assert
            Assert.IsNotNull(passwordHash);
            Assert.IsNotEmpty(passwordHash);
            Assert.That(passwordHash.Contains("."), "Hash should contain a dot separator");
        }
        //Happy flow
        [Test]
        public void TestPasswordHelperReturnsTrue() {
            string password = "user3";
            string passwordHash = "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=";
            Assert.IsTrue(PasswordHelper.VerifyPassword(password, passwordHash));
        }

        [TestCase("user1", "IunRhDKa+fWo8+4/Qfj7Pg==.kDxZnUQHCZun6gLIE6d9oeULLRIuRmxmH2QKJv2IM08=")]
        [TestCase("user3", "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=")]
        public void TestPasswordHelperReturnsTrue(string password, string passwordHash) {
            Assert.IsTrue(PasswordHelper.VerifyPassword(password, passwordHash));
        }

        //Unhappy flow
        [Test]
        public void TestPasswordHelperReturnsFalse() {
            //arrange
            string password = "wrongpassword";
            string passwordhash = "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=";
            //act & assert
            Assert.IsFalse(PasswordHelper.VerifyPassword(password, passwordhash));
        }

        [TestCase("user12", "IunRhDKa+fWo8+4/Qfj7Pg==.kDxZnUQHCZun6gLIE6d9oeULLRIuRmxmH2QKJv2IM08=")]
        [TestCase("user32", "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=")]
        public void TestPasswordHelperReturnsFalse(string password, string passwordHash) {
            Assert.IsFalse(PasswordHelper.VerifyPassword(password, passwordHash));
        }

        [Test]
        public void TestPasswordIsWhiteSpace() {
            //arrange
            string password = "";
            string passwordhash = "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=";
            //act & assert
            Assert.IsFalse(PasswordHelper.VerifyPassword(password, passwordhash));
        }

        [TestCase("", "IunRhDKa+fWo8+4/Qfj7Pg==.kDxZnUQHCZun6gLIE6d9oeULLRIuRmxmH2QKJv2IM08=")]
        [TestCase(" ", "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=")]
        public void TestPasswordIsWhiteSpace(string password, string passwordHash) {
            Assert.IsFalse(PasswordHelper.VerifyPassword(password, passwordHash));
        }
    }
}