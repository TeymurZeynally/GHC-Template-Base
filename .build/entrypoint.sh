#!/usr/bin/env bash

# Dotnet install
sudo apt-get update
sudo apt-get install -y apt-transport-https
sudo apt-get update
sudo apt-get install -y dotnet-sdk-6.0

# Create paths
mkdir -p ./run/cpp
mkdir -p ./run/dotnet
mkdir -p ./run/python
mkdir -p ./run/java

# Dotnet build
echo "Building dotnet"
export DOTNET_CLI_HOME=/tmp
dotnet publish "./dotnet/Project.csproj" --output "./run/dotnet" --configuration Release || exit 1
echo "
	#!/usr/bin/env bash
	dotnet $(realpath ./run/dotnet/Project.dll) \$@" > "./run/dotnet/main" || exit 1
chmod +x "./run/dotnet/main"
echo "Building dotnet done"

# Cpp build
echo "Building c++"
g++ "./cpp/main.cpp" -o "./run/cpp/binary" || exit 1
echo "
	#!/usr/bin/env bash
	$(realpath ./run/cpp/binary) \$@" > "./run/cpp/main" || exit 1
chmod +x "./run/cpp/main"
echo "Building c++ done"

# Python build
echo "Building python"
echo "
	#!/usr/bin/env bash
	python $(realpath ./python/main.py) \$@" > "./run/python/main" || exit 1
chmod +x "./run/python/main"
echo "Building python done"

# Java build
echo "Building java"
javac -d "./run/java" "./java/Program.java" || exit 1
echo "
	#!/usr/bin/env bash
	java $(realpath ./java/Program.java) \$@" > "./run/java/main" || exit 1
chmod +x "./run/java/main"
echo "Building java done"



# Tests build
echo "Building tests"
dotnet publish "./.test/GitHubAutogradingTests.sln" --output "./.test/publish" --configuration Release || exit 1
echo "Building tests done"


dotnet test "./.test/publish/GitHubAutogradingTests.dll" -v d || exit 1

#echo ""
#echo "<===========================================>"
#find "$PWD"
