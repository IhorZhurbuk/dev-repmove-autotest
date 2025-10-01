
# ?? Dev Repmove Autotest

Automated UI tests powered by **.NET 8**, **Playwright**, and **Allure Reports**.  
This project ensures reliable end-to-end testing across **Chromium, Firefox, and WebKit** browsers.

---

## ?? Prerequisites

### 1. Install .NET SDK (>= 8.0)
- **Windows:** [Download here](https://dotnet.microsoft.com/en-us/download)
- **Linux (Ubuntu):**

  sudo apt update
  sudo apt install dotnet8

* **macOS:**

  brew install --cask dotnet


### 2. Install Allure

Follow the [Allure Installation Guide](https://allurereport.org/docs/install/).

### 3. Install Git

git --version


## ?? Setup and Execution

### 1. Clone the Project


git clone <repo-url>
cd dev-repmove-autotest


### 2. Restore Packages

dotnet restore

### 3. Build the Project

dotnet build

### 4. Install Playwright Browsers

Recommended (cross-platform):

dotnet new tool-manifest
dotnet tool install Microsoft.Playwright.CLI
dotnet tool run playwright install --with-deps


?? **Windows alternative:**

pwsh bin\Debug\net8.0\playwright.ps1 install --with-deps


? **Path notes:**

* Windows ? use backslashes 
* Linux/macOS ? use forward slashes 

### 5. Run Tests

dotnet test --configuration Debug


## ?? Reporting with Allure

### Generate the Report

* **Windows:**


allure generate --clean "bin\Debug\net8.0\allure-results" -o "bin\Debug\net8.0\allure-report"


* **Linux/macOS:**


allure generate --clean bin/Debug/net8.0/allure-results -o bin/Debug/net8.0/allure-report


### Open the Report

* **Windows:**

allure open "bin\Debug\net8.0\allure-report"

* **Linux/macOS:**


allure open bin/Debug/net8.0/allure-report

## ?? Running Tests for Specific Browsers

? **Chromium (Chrome/Edge)**


dotnet test --filter "FullyQualifiedName~Chromium"


? **Firefox**

dotnet test --filter "FullyQualifiedName~Firefox"


? **WebKit (Safari)**


dotnet test --filter "FullyQualifiedName~WebKit"


## ?? Tips

* Always run `dotnet build` before running tests.
* Use `--filter` to speed up debugging by targeting specific test suites.
* Allure reports are best viewed in Chrome/Edge.

---

## ?? Tech Stack

* **.NET 8** – Test framework
* **NUnit** – Unit testing framework
* **Playwright** – Cross-browser automation
* **Allure** – Reporting
