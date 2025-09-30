# dev-repmove-autotest
INSTALL SDK IF MISSING
- **Windows**: Download and install from [.NET Download](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
- **Linux (Ubuntu)**:

wget https://dotnet.microsoft.com/download/dotnet/scripts/v1/dotnet-install.sh -O dotnet-install.sh
chmod +x dotnet-install.sh
./dotnet-install.sh --version 8.0.100
INSTALL ALLURE IF MISSING
scoop install allure
# or
choco install allure

sudo apt-add-repository ppa:qameta/allure
sudo apt-get update
sudo apt-get install allure

# 1. Перевірити .NET
dotnet --version  # Має бути >= 8.0

# 2. Перевірити Git
git --version

# 3. Клонувати проект
git clone <repo-url>
cd dev-repmove-autotest

# 4. Встановити пакети
dotnet restore

# 5. Збілдити
dotnet build

# 6. Встановити браузери
pwsh bin/Debug/net8.0/playwright.ps1 install --with-deps

# 7. Перевірити Allure (опціонально)
allure --version

# 8. Запустити тести
dotnet test --configuration Debug

# 2. Згенерувати звіт
allure generate --clean "bin\Debug\net8.0\allure-results" -o "bin\Debug\net8.0\allure-report"

# 3. Відкрити звіт
allure open "bin\Debug\net8.0\allure-report"


# Тільки Chromium (Chrome/Edge)
dotnet test --filter "FullyQualifiedName~Chromium"

# Тільки Firefox
dotnet test --filter "FullyQualifiedName~Firefox"

# Тільки WebKit (Safari)
dotnet test --filter "FullyQualifiedName~WebKit"