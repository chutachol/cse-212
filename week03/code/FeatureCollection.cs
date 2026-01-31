using System.Text.Json.Serialization;

public class FeatureCollection
{
    [JsonPropertyName("features")]
    public List<Feature> Features { get; set; } = new List<Feature>();
}

public class Feature
{
    [JsonPropertyName("properties")]
    public Properties Properties { get; set; } = new Properties();
}

public class Properties
{
    [JsonPropertyName("mag")]
    public double Mag { get; set; }

    [JsonPropertyName("place")]
    public string Place { get; set; } = "";

    [JsonPropertyName("time")]
    public long Time { get; set; }

    [JsonPropertyName("updated")]
    public long Updated { get; set; }

    [JsonPropertyName("tz")]
    public object Tz { get; set; } = "";

    [JsonPropertyName("url")]
    public string Url { get; set; } = "";

    [JsonPropertyName("detail")]
    public string Detail { get; set; } = "";

    [JsonPropertyName("felt")]
    public object Felt { get; set; } = 0;

    [JsonPropertyName("cdi")]
    public object Cdi { get; set; } = 0.0;

    [JsonPropertyName("mmi")]
    public object Mmi { get; set; } = 0.0;

    [JsonPropertyName("alert")]
    public string Alert { get; set; } = "";

    [JsonPropertyName("status")]
    public string Status { get; set; } = "";

    [JsonPropertyName("tsunami")]
    public int Tsunami { get; set; }

    [JsonPropertyName("sig")]
    public int Sig { get; set; }

    [JsonPropertyName("net")]
    public string Net { get; set; } = "";

    [JsonPropertyName("code")]
    public string Code { get; set; } = "";

    [JsonPropertyName("ids")]
    public string Ids { get; set; } = "";

    [JsonPropertyName("sources")]
    public string Sources { get; set; } = "";

    [JsonPropertyName("types")]
    public string Types { get; set; } = "";

    [JsonPropertyName("nst")]
    public object Nst { get; set; } = 0;

    [JsonPropertyName("dmin")]
    public object Dmin { get; set; } = 0.0;

    [JsonPropertyName("rms")]
    public double Rms { get; set; }

    [JsonPropertyName("gap")]
    public double Gap { get; set; }

    [JsonPropertyName("magType")]
    public string MagType { get; set; } = "";

    [JsonPropertyName("type")]
    public string Type { get; set; } = "";

    [JsonPropertyName("title")]
    public string Title { get; set; } = "";
}