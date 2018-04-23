param(
    [string]$AppDir = "c:\app\digesttester",
    [Parameter(Mandatory=$true, ValueFromPipeline=$true)][string]$ClientUserId,
    [String[]]$GirlsUserIds = $null
)
if((Get-Location).ToString().ToLower() -ne $AppDir.ToLower())
{
    Set-Location -Path $AppDir -Verbose   
}

[string]$AppFileName = 'DigestTester.exe'
[string]$GirlsUserIdsStr = $null

[string]$FilePath = $AppDir + "\" + $AppFileName
[string]$DebugLogPath = $AppDir + "\logs\psdebug.log"
[string]$ErrorLogPath = $AppDir + "\logs\pserror.log"

[string]$ArgumentList = $ClientUserId

if($GirlsUserIds)
{
    $GirlsUserIds | ForEach-Object { $GirlUserIdsStr += ($_ + ", ") }
    $GirlsUserIdsStr = ", " + $GirlsUserIdsStr.TrimEnd(',', ' ')
	$ArgumentList = $ArgumentList + $GirlsUserIdsStr
}

try
{
    &Start-Process -FilePath $FilePath `
            -WorkingDirectory $AppDir `
            -ArgumentList $ArgumentList `
            -RedirectStandardOutput $DebugLogPath `
            -RedirectStandardError $ErrorLogPath `
            -WindowStyle Hidden
}
catch
{
    Write-Host 'Something went wrong'
}

Write-Host "Done sending letters to " $ClientUserId