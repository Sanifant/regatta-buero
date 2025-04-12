# Path to the file you want to send
$filePath = "C:\Users\stefa\Downloads\2025-04-08_20-30_drv-meldungen_95-lubeck-regatta.xml"

# Read the file content
$fileContent = Get-Content -Path $filePath -Raw

# URL to which you want to send the request
$url = "http://localhost:5015/api/Team"

# Send the file content as the body of the POST request
$response = Invoke-RestMethod -Uri $url -Method Post -Body $fileContent -ContentType "text/xml"

# Output the response
Write-Host $response
