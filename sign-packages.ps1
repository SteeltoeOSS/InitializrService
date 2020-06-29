#!/usr/bin/env pwsh

$baseDir = "$PSScriptRoot"
$toolDir = "$baseDir"
$appSettings = "$PSScriptRoot/appsettings.json"

if ([string]::IsNullOrEmpty($Env:SignClientSecret))
{
    "SignClientSecret not set, exiting"
    Exit 1
}

if ($null -eq $Env:ArtifactStagingDirectory)
{
    "env var ArtifactStagingDirectory not set; using default"
    $artifactStagingDirectory = "."
}
else
{
    $artifactStagingDirectory = $Env:ArtifactStagingDirectory
}
"using artifact staging directory $artifactStagingDirectory"

"installing signclient"
New-Item -ItemType Directory -Force -Path $toolDir
dotnet tool install --tool-path $toolDir signclient

"looking for nugets in $artifactStagingDirectory"
$nupkgs = Get-ChildItem $artifactStagingDirectory/Steeltoe*.*nupkg -recurse | Select-Object -ExpandProperty FullName
if ($nupkgs)
{
    foreach ($nupkg in $nupkgs)
    {
        "signing $nupkg"
        & $toolDir/SignClient 'sign' -c $appSettings -i $nupkg -r $Env:SignClientUser -s $Env:SignClientSecret -n 'Steeltoe' -d 'Steeltoe' -u 'https://github.com/SteeltoeOSS'
    }
}
else
{
    "no nugets found"
}
