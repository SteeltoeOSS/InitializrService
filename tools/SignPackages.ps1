#!/usr/bin/env pwsh

$baseDir = "$PSScriptRoot/.."
$toolDir = "$baseDir/tools"
$appSettings = "$PSScriptRoot/appsettings.json"

"installing signclient"
New-Item -ItemType Directory -Force -Path "$toolDir"
dotnet tool install --tool-path "$toolDir" signclient

"signing"
& $toolDir/SignClient Sign `
        --input "**/*.nupkg" `
        --config "$appSettings"
        --user "$(SignClientUser)" --secret "$(SignClientSecret)" `
        --name "Steeltoe" --description "Steeltoe" --descriptionUrl "https://github.com/SteeltoeOSS"
