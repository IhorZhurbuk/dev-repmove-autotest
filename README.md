
### **INSTALL SDK IF MISSING**

  - **Windows**: Download and install from [.NET Download](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
  - **Linux (Ubuntu)**:

<!-- end list -->

```bash
wget https://dotnet.microsoft.com/download/dotnet/scripts/v1/dotnet-install.sh -O dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh --version 8.0.100
```

### **INSTALL ALLURE IF MISSING**

**Windows (using Scoop or Chocolatey):**

```bash
scoop install allure
# or
choco install allure
```

**Linux (Ubuntu):**

```bash
sudo apt-add-repository ppa:qameta/allure
sudo apt-get update
sudo apt-get install allure
```

-----

### **Setup and Execution**

**1. Check .NET version**

```bash
dotnet --version # Should be >= 8.0
```

**2. Check Git version**

```bash
git --version
```

**3. Clone the project**

```bash
git clone <repo-url>
cd dev-repmove-autotest
```

**4. Install packages**

```bash
dotnet restore
```

**5. Build the project**

```bash
dotnet build
```

**6. Install browsers**

```bash
pwsh bin/Debug/net8.0/playwright.ps1 install --with-deps
```

**7. Check Allure (optional)**

```bash
allure --version
```

**8. Run tests**

```bash
dotnet test --configuration Debug
```

-----

### **Reporting**

**1. Generate the report**

```bash
allure generate --clean "bin\Debug\net8.0\allure-results" -o "bin\Debug\net8.0\allure-report"
```

**2. Open the report**

```bash
allure open "bin\Debug\net8.0\allure-report"
```

-----

### **Running Tests for Specific Browsers**

**Only Chromium (Chrome/Edge)**

```bash
dotnet test --filter "FullyQualifiedName~Chromium"
```

**Only Firefox**

```bash
dotnet test --filter "FullyQualifiedName~Firefox"
```

**Only WebKit (Safari)**

```bash
dotnet test --filter "FullyQualifiedName~WebKit"
```