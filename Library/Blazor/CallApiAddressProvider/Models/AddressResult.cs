namespace Library.Blazor.CallApiAddressProvider.Models
{
    public class AddressResult
    {
        public string? type { get; set; }
        public string? version { get; set; }
        public List<Features>? Features { get; set; }
        public string? attribution { get; set; }
        public string? licence { get; set; }
        public string? query { get; set; }
        public int? limit { get; set; }
    }

    public class Features
    {
        public string? type { get; set; }
        public Geometry? geometry { get; set; }
        public Properties? Properties { get; set; }
    }

    public class Geometry
    {
        public string? type { get; set; }
        public List<double>? coordinates { get; set; }
    }

    public class Properties
    {
        public string? label { get; set; }
        public double? score { get; set; }
        public string? housenumber { get; set; }
        public string? id { get; set; }
        public string? type { get; set; }
        public string? name { get; set; }
        public string? postcode { get; set; }
        public string? citycode { get; set; }
        public double? x { get; set; }
        public double? y { get; set; }
        public string? city { get; set; }
        public string? context { get; set; }
        public double? importance { get; set; }
        public string? street { get; set; }
    }
}
