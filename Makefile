# Phony targets that is not really the name of a file, but just the name for a recipe to be executed.
.PHONY : all build clean coverage install pack test

all : coverage

build : install
	dotnet build src/FunctionalExtras
	dotnet build src/FunctionalExtras.Tests

clean :
	dotnet clean src/functional-extras.sln
	rm -r src/FunctionalExtras.Tests/build/coverage/

coverage : test
	@cd src \
		&& dotnet ~/.nuget/packages/reportgenerator/4.1.5/tools/netcoreapp2.0/ReportGenerator.dll \
			"-reports:./FunctionalExtras.Tests/build/coverage.opencover.xml" \
			"-targetdir:./FunctionalExtras.Tests/build/coverage/"

install :
	dotnet restore src/functional-extras.sln

pack :
	dotnet pack src/FunctionalExtras

test :
	@cd src && dotnet test FunctionalExtras.Tests \
		/p:CollectCoverage=true \
		/p:CoverletOutput="./build/" \
		/p:CoverletOutputFormat=opencover \
		/p:Exclude="[xunit.*]*"
