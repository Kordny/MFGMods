#!/usr/bin/env bash
set -e
cd "$(dirname "$0")"
dotnet=${DOTNET_CMD:-dotnet}

$dotnet clean
$dotnet restore
$dotnet build --configuration Release

rm -rf ./Package

mkdir -p ./Package/tmp/BepInEx/plugins/MFGTweaks
cp ./MFGTweaks/bin/Release/netstandard2.1/*.dll ./Package/tmp/BepInEx/plugins/MFGTweaks/

mkdir -p ./Package/tmp/BepInEx/plugins/MFGTileNumbers
cp ./MFGTileNumbers/bin/Release/netstandard2.1/*.dll ./Package/tmp/BepInEx/plugins/MFGTileNumbers/
cp -r ./Resources/MFGTileNumbers ./Package/tmp/BepInEx/plugins/

bsdtar -caf ./Package/MFGMods.zip -C ./Package/tmp BepInEx

rm -rf ./Package/tmp
