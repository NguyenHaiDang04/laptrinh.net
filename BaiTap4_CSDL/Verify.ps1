param([switch]$SkipBuild)
$ErrorActionPreference = 'Stop'
$root = $PSScriptRoot
if (-not $SkipBuild) {
    $vswhere = Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\Installer\vswhere.exe'
    if (-not (Test-Path -LiteralPath $vswhere)) { throw 'Cần Visual Studio với workload .NET desktop development.' }
    $msbuild = & $vswhere -latest -products '*' -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\MSBuild.exe' | Select-Object -First 1
    if (-not $msbuild) { throw 'Không tìm thấy MSBuild.' }
    & $msbuild (Join-Path $root 'BaiTap4_CSDL.sln') /t:Build /p:Configuration=Debug /v:minimal /nologo
    if ($LASTEXITCODE -ne 0) { throw 'Build không thành công.' }
}
$localdb = Get-Command SqlLocalDB.exe -ErrorAction SilentlyContinue
if (-not $localdb) { throw 'Chưa cài SQL Server Express LocalDB. Xem README.md.' }
& $localdb.Source create MSSQLLocalDB -s
if ($LASTEXITCODE -ne 0) { throw 'Không khởi động được LocalDB.' }

$saved = @{}
foreach ($key in @('LTNET_SQL_SERVER','LTNET_DB_NAME','LTNET_DB_DIRECTORY')) { $saved[$key] = [Environment]::GetEnvironmentVariable($key, 'Process') }
try {
    $testName = 'LTNET_Bai4_Verification_' + [DateTime]::UtcNow.ToString('yyyyMMddHHmmss')
    $env:LTNET_SQL_SERVER = '(localdb)\MSSQLLocalDB'
    $env:LTNET_DB_NAME = $testName
    $env:LTNET_DB_DIRECTORY = Join-Path $root ('.verificationData\' + $testName)
    $app = Join-Path $root 'src\Verification\bin\Debug\Verification.exe'
    $docs = Join-Path $root 'docs'
    $process = Start-Process -FilePath $app -ArgumentList @('--verify', ('"' + $docs + '"')) -WindowStyle Hidden -Wait -PassThru
    Get-Content -LiteralPath (Join-Path $docs 'verification.txt')
    if ($process.ExitCode -ne 0) { throw ('Kiểm tra thất bại, exit code ' + $process.ExitCode) }
    Write-Host ('CSDL kiểm tra riêng: ' + $testName)
} finally {
    foreach ($key in $saved.Keys) { [Environment]::SetEnvironmentVariable($key, $saved[$key], 'Process') }
}
