namespace HomeManagementService.Services;

public class LocationService : ILocationService
{
    private readonly double marginOfError = 0.00011;

    public bool AreCoordinatesClose(string lat1, string long1, string lat2, string long2)
    {
        if (TryParseCoordinate(lat1, long1, out var parsedLat1, out var parsedLong1
            ) && TryParseCoordinate(lat2, long2, out var parsedLat2, out var parsedLong2
            ))
        {
            Console.WriteLine($"Parsed coordinates {parsedLat1}, {parsedLong1}");
            var latDiff = Math.Abs(parsedLat1 - parsedLat2);
            var lonDiff = Math.Abs(parsedLong1 - parsedLong2);

            return latDiff <= marginOfError && lonDiff <= marginOfError;
        }

        return false;
    }

    private bool TryParseCoordinate(string latitude, string longitude, out double lat, out double lon)
    {
        lat = 0;
        lon = 0;

        latitude = latitude.Replace(".", ",");
        longitude = longitude.Replace(".", ",");

        if (double.TryParse(latitude, out lat) && double.TryParse(longitude, out lon)) return true;

        Console.WriteLine($"Failed to parse coordinates {latitude}, {longitude}");
        return false;
    }
}

public interface ILocationService
{
    public bool AreCoordinatesClose(string lat1, string long1, string lat2, string long2);
}