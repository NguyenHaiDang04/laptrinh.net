param(
    [switch]$SkipBuild,
    [string]$OutputDirectory = (Join-Path $PSScriptRoot 'TestOutput'),
    [string]$MSBuild
)

$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Data

$taskApps = @(
    @{ Folder = 'Bai6_3_ProjectTask'; App = 'ProjectTask'; Database = 'ProjectTaskDB' },
    @{ Folder = 'Bai6_4_Garage'; App = 'Garage'; Database = 'GarageDB' },
    @{ Folder = 'Bai6_5_Karaoke'; App = 'Karaoke'; Database = 'KaraokeDB' }
)

if (-not $SkipBuild -and -not $MSBuild) {
    $vswhere = Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\Installer\vswhere.exe'
    if (Test-Path -LiteralPath $vswhere) {
        $MSBuild = & $vswhere -latest -products '*' -requires Microsoft.Component.MSBuild -find 'MSBuild\Current\Bin\MSBuild.exe' |
            Select-Object -First 1
    }
    if (-not $MSBuild) { throw 'MSBuild was not found. Install Visual Studio with .NET desktop development.' }
}

function Invoke-TestSql([string]$Sql) {
    $connection = New-Object System.Data.SqlClient.SqlConnection(
        'Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=20;Pooling=False'
    )
    try {
        $connection.Open()
        foreach ($batch in [regex]::Split($Sql, '(?im)^\s*GO[ \t]*\r?$')) {
            if ([string]::IsNullOrWhiteSpace($batch)) { continue }
            $command = $connection.CreateCommand()
            try {
                $command.CommandText = $batch
                $command.CommandTimeout = 60
                [void]$command.ExecuteNonQuery()
            } finally { $command.Dispose() }
        }
    } finally { $connection.Dispose() }
}

$taskOutput = [IO.Path]::GetFullPath($OutputDirectory)
$oldConnection = [Environment]::GetEnvironmentVariable('LTNET_BAI6_CONNECTION', 'Process')
try {
    foreach ($app in $taskApps) {
        $folder = Join-Path $PSScriptRoot $app.Folder
        if (-not $SkipBuild) {
            & $MSBuild (Join-Path $folder ($app.App + '.sln')) /t:Build /p:Configuration=Debug /v:minimal /nologo
            if ($LASTEXITCODE -ne 0) { throw ('Build failed: ' + $app.App) }
        }

        $exe = Join-Path $folder ($app.App + '\bin\Debug\' + $app.App + '.exe')
        if (-not (Test-Path -LiteralPath $exe)) { throw ('Build the executable first: ' + $exe) }
        $dbName = 'LTNET_Bai6_Test_' + $app.App + '_' + [guid]::NewGuid().ToString('N')
        $sql = [IO.File]::ReadAllText((Join-Path $folder 'Database\ScriptDB.sql')).Replace($app.Database, $dbName)
        $out = Join-Path $taskOutput $app.Folder
        [void][IO.Directory]::CreateDirectory($out)
        Write-Output ('=== ' + $app.App + ' ===')
        try {
            Invoke-TestSql $sql
            Invoke-TestSql $sql
            $env:LTNET_BAI6_CONNECTION = "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=$dbName;Integrated Security=True;Connect Timeout=10"
            $run = Start-Process -FilePath $exe -ArgumentList @('--verify', ('"' + $out + '"')) -WindowStyle Hidden -PassThru
            try {
                if (-not $run.WaitForExit(120000)) {
                    $run.Kill()
                    $run.WaitForExit()
                    throw ('Test timed out: ' + $app.App)
                }
                $exitCode = $run.ExitCode
            } finally { $run.Dispose() }

            $log = Join-Path $out 'verification.txt'
            if (Test-Path -LiteralPath $log) { Get-Content -LiteralPath $log -Encoding UTF8 }
            if ($exitCode -ne 0) { throw ('Test failed: ' + $app.App) }
            foreach ($image in '01_main_screen.png','02_crud_screen.png','03_transaction_screen.png',
                '04_report_screen.png','05_error_handling.png') {
                if (-not (Test-Path -LiteralPath (Join-Path $out $image))) { throw ('Missing image: ' + $image) }
            }
        } finally {
            # Only the randomly named database created for this run is cleaned up.
            Invoke-TestSql "IF DB_ID(N'$dbName') IS NOT NULL BEGIN ALTER DATABASE [$dbName] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE [$dbName]; END"
        }
    }
} finally {
    [Environment]::SetEnvironmentVariable('LTNET_BAI6_CONNECTION', $oldConnection, 'Process')
}
Write-Output ('All three exercises passed. Evidence: ' + $taskOutput)
