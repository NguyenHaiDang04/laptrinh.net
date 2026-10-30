param([string]$Configuration = 'Release')
$ErrorActionPreference = 'Stop'
$root = $PSScriptRoot
$instance = 'Bt5Verify_' + (Get-Date -Format 'yyyyMMddHHmmss')
$tempDb = Join-Path $env:TEMP $instance
New-Item -ItemType Directory -Force $tempDb | Out-Null
$created = $false
try {
    & SqlLocalDB.exe create $instance -s
    if ($LASTEXITCODE -ne 0) { throw 'Không khởi tạo được SQL Server LocalDB.' }
    $created = $true
    $connection = New-Object System.Data.SqlClient.SqlConnection("Server=(localdb)\$instance;Database=master;Integrated Security=True;Encrypt=False")
    $connection.Open()
    try {
        $sql = Get-Content (Join-Path $root '..\Database\ScriptDB.sql') -Raw -Encoding UTF8
        foreach ($db in @('CafePOSDB','HotelDB','InventoryDB')) {
            $file = (Join-Path $tempDb "$db.mdf").Replace("'", "''")
            $log = (Join-Path $tempDb "${db}_log.ldf").Replace("'", "''")
            $sql = $sql.Replace("CREATE DATABASE $db;", "CREATE DATABASE $db ON (NAME=N'$db', FILENAME=N'$file') LOG ON (NAME=N'${db}_log', FILENAME=N'$log');")
        }
        foreach ($batch in [regex]::Split($sql, '(?im)^\s*GO\s*\r?$')) {
            if ($batch.Trim()) {
                $cmd = $connection.CreateCommand(); $cmd.CommandText = $batch; $cmd.CommandTimeout = 90
                [void]$cmd.ExecuteNonQuery(); $cmd.Dispose()
            }
        }
    } finally { $connection.Dispose() }
    & msbuild (Join-Path $root 'BaiTap5_CSDL.sln') /m /t:Rebuild "/p:Configuration=$Configuration" /verbosity:minimal
    if ($LASTEXITCODE -ne 0) { throw 'Build không thành công.' }
    $exe = Join-Path $root "Verification\bin\$Configuration\Verification.exe"
    $config = "$exe.config"
    (Get-Content $config -Raw).Replace('(localdb)\MSSQLLocalDB', "(localdb)\$instance") | Set-Content $config -Encoding UTF8
    & $exe (Join-Path $root 'images')
    if ($LASTEXITCODE -ne 0) { throw 'Kiểm tra tích hợp không thành công; xem images/verification.txt.' }
} finally {
    if ($created) {
        & SqlLocalDB.exe stop $instance -k | Out-Null
        & SqlLocalDB.exe delete $instance | Out-Null
        Remove-Item $tempDb -Recurse -Force -ErrorAction SilentlyContinue
    }
}
