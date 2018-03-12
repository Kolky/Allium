@echo off

@echo Running OpenCover
set nunit_args="Allium.Tests\bin\Debug\Allium.Tests.dll --x86"
set cover_filter="+[*]* -[*.Tests]*"
set cover_dir=Allium.CodeCoverage
set cover_hist_dir=Allium.CodeCoverage.History
packages\OpenCover.4.6.519\tools\OpenCover.Console.exe -target:packages\NUnit.ConsoleRunner.3.8.0\tools\nunit3-console.exe -targetargs:%nunit_args% -filter:%cover_filter% -register:user

rmdir /s /q %cover_dir%

@echo Running ReportGenerator
packages\ReportGenerator.3.1.2\tools\ReportGenerator.exe -reports:results.xml -targetdir:%cover_dir% -historydir:%cover_hist_dir% -verbosity:Info

start "" %cover_dir%\index.htm
del /q results.xml
del /q TestResult.xml

pause