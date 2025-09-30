
### **INSTALL SDK IF MISSING**
Recomended os: Windows 10/11 
  - **Windows**: Download and install from [.NET Download](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
  - **Linux (Ubuntu)**:   
	- sudo apt update
	- sudo apt install dotnet8
 

### **INSTALL ALLURE IF MISSING**
	https://allurereport.org/docs/install/


### **Setup and Execution**

**1. Check .NET version**

dotnet --version # Should be >= 8.0


**2. Check Git version**

git --version


**3. Clone the project**


git clone <repo-url>
cd dev-repmove-autotest


**4. Install packages**


dotnet restore


**5. Build the project**


dotnet build


**6. Install browsers**


pwsh bin/Debug/net8.0/playwright.ps1 install --with-deps


**7. Check Allure (optional)**


allure --version


**8. Run tests**


dotnet test --configuration Debug


-----

### **Reporting**

**1. Generate the report**


allure generate --clean "bin\Debug\net8.0\allure-results" -o "bin\Debug\net8.0\allure-report"


**2. Open the report**


allure open "bin\Debug\net8.0\allure-report"



### **Running Tests for Specific Browsers**

**Only Chromium (Chrome/Edge)**

dotnet test --filter "FullyQualifiedName~Chromium"



**Only Firefox**


dotnet test --filter "FullyQualifiedName~Firefox"


**Only WebKit (Safari)**


dotnet test --filter "FullyQualifiedName~WebKit"
