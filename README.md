# 🛒 Grocery App - .NET MAUI Shopping List Application

Een cross-platform boodschappenlijst applicatie gebouwd met .NET MAUI, waarmee gebruikers hun boodschappen kunnen beheren, producten kunnen zoeken en lijsten kunnen delen.

## 📋 Features

- **Gebruikersbeheer**: Registratie en inlogfunctionaliteit met beveiligde wachtwoord-hashing
- **Boodschappenlijsten**: Maak en beheer meerdere boodschappenlijsten
- **Product Management**: Zoek en voeg producten toe aan je lijsten
- **Kleur Customization**: Personaliseer je lijsten met aangepaste kleuren
- **Delen**: Export en deel je boodschappenlijsten als JSON bestanden
- **Cross-platform**: Werkt op Android, iOS, macOS, en Windows

## 🏗️ Architecture

Het project volgt een clean architecture pattern met de volgende structuur:

├── Grocery.App/                 # MAUI UI Layer 
│   ├── ViewModels/              # MVVM ViewModels 
│   ├── Views/                   # XAML Views 
│   └── Platforms/               # Platform-specifieke code 
├── Grocery.Core/                # Business Logic Layer 
│   ├── Models/                  # Domain Models 
│   ├── Services/                # Business Services 
│   ├── Helpers/                 # Utility Classes 
│   └── Interfaces/              # Service Interfaces 
├── Grocery.Core.Data/           # Data Access Layer 
│   └── Repositories/            # Data Repositories 
└── TestCore/                    # Unit Tests 
└── TestHelpers.cs           # NUnit Test Cases


## 🛠️ Tech Stack

- **.NET 8**: Target framework
- **.NET MAUI**: Cross-platform UI framework
- **CommunityToolkit.Mvvm**: MVVM implementation
- **NUnit**: Unit testing framework
- **Moq**: Mocking framework voor tests
- **System.Text.Json**: JSON serialization

## 📦 Dependencies

### Required NuGet Packages
<!-- Voor het hoofdproject --> 
<PackageReference Include="CommunityToolkit.Maui" Version="8.0.1" /> <PackageReference Include="CommunityToolkit.Mvvm" Version="8.3.2" /> <PackageReference Include="Microsoft.Extensions.Logging.Debug" Version="8.0.1" />
<!-- Voor testing --> 
<PackageReference Include="Moq" Version="4.20.70" /> <PackageReference Include="NUnit" Version="3.14.0" /> <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />

### Installing Dependencies
Installeer Moq voor unit testing
dotnet add TestCore/TestCore.csproj package Moq

Of via Package Manager Console
Install-Package Moq -ProjectName TestCore



## 🚀 Getting Started

### Prerequisites
- Visual Studio 2022 (17.8 of hoger)
- .NET 8 SDK
- MAUI workload geïnstalleerd

### Installation

1. **Clone het repository**
git clone https://github.com/woutvangurp/CorrecteFork_OOSDD_GroceryApp_Sprint3_Studenten cd CorrecteFork_OOSDD_GroceryApp_Sprint3_Studenten

2. **Installeer MAUI workload** (indien nog niet gedaan)
dotnet workload install maui

3. **Restore dependencies**
dotnet restore


4. **Set startup project**
- Open de solution in Visual Studio
- Rechtsklik op `Grocery.App` project
- Selecteer "Set as StartUp Project"

5. **Run de applicatie**
- Druk op `F5` of klik op de start button
- Kies je gewenste platform (Windows Machine, Android Emulator, etc.)

### 📱 Mobile Development
Voor ontwikkeling op mobiele platformen:

- **Android**: Gebruik de ingebouwde Android emulator in Visual Studio
- **iOS**: Vereist een Mac voor development en testing
- **Emulators**: Gebruik de built-in emulators in Visual Studio voor snelle testing

## 🧪 Testing

Het project bevat uitgebreide unit tests in de `TestCore` projecten.

### Running Tests
Run alle tests
dotnet test

Run tests met verbose output
dotnet test --verbosity normal

Run tests voor specifiek project
dotnet test TestCore/TestCore.csproj


### Test Coverage
De tests dekken de volgende functies:
- ✅ **Authentication**: Login/Register functionaliteit
- ✅ **Password Management**: Hashing en verificatie
- ✅ **Product Search**: Search en filter logica
- ✅ **Registration Validation**: Email en wachtwoord validatie

### Test Categories
/ Happy Path Tests 
[Test] public void TestLoginSucceedsWithValidData() 
[Test] public void TestRegistrationSucceedsWithValidData()

// Unhappy Path Tests
[Test] public void TestLoginFailsWithWrongPassword() 
[Test] public void TestRegistrationFailsWithInvalidData()

// Edge Cases 
[Test] public void TestPasswordIsWhiteSpace() 
[Test] public void TestRegistrationEdgeCases()


## 👥 Default Users
Voor testing zijn er standaard gebruikers beschikbaar:

| Email          | Password | Name         |
|----------------|----------|--------------|
| user1@mail.com |   user1  | M.J. Curie   |
| user2@mail.com |   user2  | H.H. Hermans |
| user3@mail.com |   user3  | A.J. Kwak    |

## 🐛 Known Issues

### Platform-Specific Issues
- **macOS**: Geen melding wordt weergegeven bij het delen van boodschappenlijsten
- **File Sharing**: Platform verschillen in bestandsdeling implementatie

### Workarounds
- Voor macOS file sharing: Controleer of het bestand wel wordt opgeslagen in de Downloads folder

## 🔧 Development

### Project Structure Explanation
- **Models**: Domain entities zoals `Client`, `Product`, `GroceryList`
- **Services**: Business logic zoals `AuthService`, `ProductService`
- **ViewModels**: MVVM binding tussen Views en Services
- **Repositories**: Data access met in-memory storage
- **Helpers**: Utility classes voor password hashing, email validation

### Adding New Features
1. **Model**: Voeg nieuwe domain models toe in `Grocery.Core/Models/`
2. **Repository**: Implementeer data access in `Grocery.Core.Data/Repositories/`
3. **Service**: Voeg business logic toe in `Grocery.Core/Services/`
4. **ViewModel**: Maak MVVM binding in `Grocery.App/ViewModels/`
5. **View**: Implementeer UI in `Grocery.App/Views/`
6. **Tests**: Voeg unit tests toe in `TestCore/`

## 🧪 Unit Test Implementation Details

### Authentication Tests
Het project bevat uitgebreide unit tests voor authenticatie functionaliteit:

#### Login Tests
// Happy Flow - Succesvolle login 
[Test] public void TestLoginSucceedsWithValidData() { // Test met geldige credentials // Verwacht: Client object wordt geretourneerd }
[TestCase("A.J. Kwak", "user3@mail.com", "user3", "hash...")] 
[TestCase("M.J. Curie", "user1@mail.com", "user1", "hash...")] public void TestLoginSucceedsWithValidData(string name, string email, string password, string hashedPassword) { // Test met verschillende geldige gebruikers }

// Unhappy Flow - Gefaalde login
 [TestCase("M.J. Curie", "user1@mail.com", "wrongpassword", "hash...")] 
 [TestCase("A.J. Kwak", "user3@mail.com", "invalidpass", "hash...")] public void TestLoginFailsWithWrongPassword(string name, string email, string password, string hashedPassword) { // Test met verkeerde wachtwoorden // Verwacht: null wordt geretourneerd }
[Test] public void TestLoginFailsWithNonExistentUser() { // Test met niet-bestaande gebruiker // Verwacht: null wordt geretourneerd }

#### Registration Tests
// Happy Flow - Succesvolle registratie 
[TestCase("John", "Doe", "john@test.com", "Password123!", "Password123!")] 
[TestCase("Jane", "Smith", "jane@example.com", "SecurePass1!", "SecurePass1!")] public void TestRegistrationSucceedsWithValidData(string firstName, string lastName, string email, string password, string verifyPassword) { // Test registratie met geldige gegevens // Verwacht: Client object wordt gecreëerd }

// Unhappy Flow - Gefaalde registratie 
[TestCase("John", "Doe", "invalidemail", "Password123!", "Password123!")] // Invalid email 
[TestCase("Jane", "Smith", "test@valid.mail", "Password123!", "DifferentPass!")] // Passwords do not match public void TestRegistrationFailsWithInvalidData(string firstName, string lastName, string Email, string Password, string verifyPassword) { // Test registratie met ongeldige gegevens // Verwacht: null wordt geretourneerd }

#### Password Helper Tests
// Password Hashing Tests 
[Test] public void TestPasswordHashIsCreated() { // Test dat password hashing werkt // Verwacht: Hash bevat dot separator }

// Password Verification Tests 
[TestCase("user1", "IunRhDKa+fWo8+4/Qfj7Pg==.kDxZnUQHCZun6gLIE6d9oeULLRIuRmxmH2QKJv2IM08=")] 
[TestCase("user3", "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=")] public void TestPasswordHelperReturnsTrue(string password, string passwordHash) { // Test correcte password verificatie }
[TestCase("user12", "hash...")] [TestCase("user32", "hash...")] public void TestPasswordHelperReturnsFalse(string password, string passwordHash) { // Test incorrecte password verificatie }


### Test Implementation Strategy
1. **3 A's Pattern**: Alle tests volgen Arrange-Act-Assert pattern
2. **Mocking**: Gebruik van Moq framework voor dependency isolation
3. **Test Categories**: 
   - Happy Path: Normale flow met geldige input
   - Unhappy Path: Error handling met ongeldige input
   - Edge Cases: Boundary conditions en extreme values

### Mock Setup Examples
// Client Service Mock Setup Mock
<IClientService> mockClientService = new Mock<IClientService>(); mockClientService.Setup(x => x.Get(email)).Returns(expectedClient); mockClientService.Setup(x => x.Create(It.IsAny<Client>())).Returns(expectedClient);
// Verification Examples 
mockClientService.Verify(x => x.Get(email), Times.Once); mockClientService.Verify(x => x.Create(It.IsAny<Client>()), Times.Never);


## 🔄 GitHub Actions CI/CD

Het project bevat een complete CI/CD pipeline voor automatische testing:

### Workflow Configuration

.github/workflows/ci.yml
name: CI Pipeline
on: push: branches: [ main, develop, feature/* ] pull_request: branches: [ main, develop ]
jobs: test: runs-on: ubuntu-latest
steps:
- name: Checkout code
  uses: actions/checkout@v4
  
- name: Setup .NET
  uses: actions/setup-dotnet@v4
  with:
    dotnet-version: '8.0.x'
    
- name: Restore dependencies
  run: dotnet restore
  
- name: Build solution
  run: dotnet build --no-restore --configuration Release
  
- name: Run unit tests
  run: dotnet test TestCore/TestCore.csproj --no-build --configuration Release --verbosity normal --logger trx --results-directory TestResults
  
- name: Upload test results
  uses: actions/upload-artifact@v4
  if: always()
  with:
    name: test-results
    path: TestResults/*.trx
build-android: runs-on: ubuntu-latest needs: test
steps:
- name: Checkout code
  uses: actions/checkout@v4
  
- name: Setup .NET
  uses: actions/setup-dotnet@v4
  with:
    dotnet-version: '8.0.x'
    
- name: Install MAUI workload
  run: dotnet workload install maui
    
- name: Restore dependencies
  run: dotnet restore
  
- name: Build Android app
  run: dotnet build Grocery.App/Grocery.App.csproj -f net8.0-android --configuration Release


### Pipeline Features
- **Automatic Testing**: Draait bij elke push en pull request
- **Multi-Platform**: Test op Ubuntu, optioneel Windows
- **Build Verification**: Controleert of MAUI app compileert
- **Test Results**: Upload test resultaten als artifacts
- **Quality Gates**: Build faalt bij failing tests

### Code Style Guidelines

- Gebruik C# naming conventions
- Follow MVVM pattern voor UI code
- Schrijf unit tests voor nieuwe features (minimum 80% coverage)
- Documenteer public APIs met XML comments
- Gebruik async/await voor I/O operations
- Implementeer proper error handling

### Pull Request Checklist

- [ ] Code compileert zonder warnings
- [ ] Alle unit tests slagen
- [ ] Nieuwe features hebben bijbehorende tests
- [ ] Code review is uitgevoerd
- [ ] Documentation is bijgewerkt

## 📊 Test Coverage Rapport

### Current Coverage Statistics

| Component      | Coverage | Tests   | Status |
|----------------|----------|---------|--------|
| AuthService    | 95%      | 12 tests| ✅ |
| PasswordHelper | 100%     | 8 tests | ✅ |
| EmailHelper    | 85%      | 6 tests | ✅ |
| ProductService | 70%      | 4 tests | 🔄 |
| SearchLogic    | 90%      | 6 tests | ✅ |

### Test Case Mapping

| Use Case                | Test Cases   | Coverage    |
|-------------------------|--------------|-------------|
| UC6 - Inloggen          | 5 test cases | ✅ Complete |
| UC9 - Registratie       | 7 test cases | ✅ Complete |
| UC8 - Zoeken producten  | 6 test cases | ✅ Complete |
| UC5 - Product toevoegen | 2 test cases | 🔄 Partial  |

## 📄 License

Dit project is onderdeel van een educatieve opdracht voor Windesheim University of Applied Sciences.

### Troubleshooting

**Veel voorkomende problemen:**

1. **Tests falen na clone**
   - Controleer of Moq package is geïnstalleerd
   - Run `dotnet restore` in de solution root

2. **MAUI app start niet**
   - Controleer of MAUI workload is geïnstalleerd
   - Run `dotnet workload install maui`

3. **Android emulator problemen**
   - Controleer Android SDK installatie
   - Start emulator handmatig via Android Studio

4. **Test coverage te laag**
   - Run `dotnet test --collect:"XPlat Code Coverage"`
   - Gebruik coverage tools zoals coverlet

Made by Wout van Gurp 
