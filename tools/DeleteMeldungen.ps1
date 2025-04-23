# URL der API oder des Upload-Endpunkts
$uri = "https://buero.luebeckregatta.de/api/Finish"

# Pfad zur Datei
# $filePath = "C:\Pfad\zur\datei.txt"

# API-Key (optional)
$apiKey = "37FD7F0F-EDA3-4DCA-983F-C8AED6AADF12"

# Header (falls erforderlich)
$headers = @{
    # "Authorization" = "Bearer $apiKey"
    # Alternativ, wenn die API einen anderen Header verlangt, z. B.:
    "x-api-key" = $apiKey
}

# Dateiinhalt als FormData
#$Form = @{
#    file = Get-Item $filePath
#}

# Anfrage senden
$response = Invoke-WebRequest -Uri $uri -Method Get -Headers $headers

# JSON-Antwort in PowerShell-Objekt umwandeln
$data = $response.Content | ConvertFrom-Json
$data
