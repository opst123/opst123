
public class WeatherModel
{
    public WeatherRecord records { get; set; }
}

public class WeatherRecord
{
    public List<Location> locations { get; set; }
}

public class Location{
    public List<LocationWithWeatherElement> location { get; set; }
}

public class LocationWithWeatherElement
{
    public List<weatherElement> weatherElement { get; set; }
}

public class weatherElement
{
    public List<Time>  Time { get; set; }
}

public class Time
{
    public List<elementValue>  elementValue { get; set; }
}

public class elementValue
{
    public string value { get; set; }
    public string measures { get; set; }
}