#!/usr/bin/env pwsh

$baseDir = "$PSScriptRoot/.."
$toolDir = "$baseDir/tools"
$artifactStagingDirectory = $Env:ArtifactStagingDirectory

$name = "Steeltoe"
$description = "Steeltoe"
$descriptionUrl = "https://github.com/SteeltoeOSS"
$appSettings = "$baseDir/sign/appsettings.json"
$signClientUser = $Env:SignClientUser
$signClientSecret = $Env:SignClientSecret

"installing SignClient"
New-Item -ItemType Directory -Force -Path "$toolDir"
dotnet tool install --tool-path "$toolDir" SignClient

"looking for artifacts"
$artifacts = Get-ChildItem $artifactStagingDirectory/Steeltoe*.*nupkg -recurse | Select-Object -ExpandProperty FullName
if ($artifacts) {
    foreach ($artifact in $artifacts)
    {
        "signing $artifact"
        & $toolDir/SignClient Sign --input $artifact --config $appSettings --user $signClientUser --secret $signClientSecret --name $name --description $description --descriptionUrl $descriptionUrl
    }
}
else
{
    "no artifacts found"
}
