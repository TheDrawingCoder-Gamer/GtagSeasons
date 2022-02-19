#!/bin/bash

cd $(dirname "$0")
PROJNAME=$(basename -s .csproj "$(ls *.csproj)")
echo $PROJNAME
dotnet build -c Release
mkdir -p "BepInEx/plugins/$PROJNAME"
cp "bin/Release/netstandard2.0/$PROJNAME.dll" "BepInEx/plugins/$PROJNAME/$PROJNAME.dll"
zip -r "$PROJNAME-v.zip" BepInEx
rm -rf BepInEx
