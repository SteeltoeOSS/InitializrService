#!/usr/bin/env pwsh

$baseDir = "$PSScriptRoot/.."
$toolDir = "$baseDir/tools"
$appSettings = "$baseDir/sign/appsettings.json"
$fileList = "$baseDir/sign/filelist.txt"

"installing signclient"
New-Item -ItemType Directory -Force -Path "$toolDir"
dotnet tool install --tool-path "$toolDir" signclient

"signing"
& $toolDir/SignClient Sign --input "**/*.nupkg" --config "$appSettings" --filelist "$fileList" --user $Env:SignClientUser --secret $Env:SignClientSecret --name "Steeltoe" --description "Steeltoe" --descriptionUrl "https://github.com/SteeltoeOSS"
