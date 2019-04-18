.PHONY : all test

all : coverage

build :
	dotnet build src/FunctionalExtras
	dotnet build src/FunctionalExtras.Tests

coverage : test
	cd src \
		&& dotnet ~/.nuget/packages/reportgenerator/4.0.12/tools/netcoreapp2.0/ReportGenerator.dll \
			"-reports:./FunctionalExtras.Tests/build/coverage.opencover.xml" \
			"-targetdir:./FunctionalExtras.Tests/build/coverage/"

test :
	cd src && dotnet test FunctionalExtras.Tests \
		/p:CollectCoverage=true \
		/p:CoverletOutput="./build/" \
		/p:CoverletOutputFormat=opencover \
		/p:Exclude="[xunit.*]*"
