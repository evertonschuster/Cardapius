using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexata.BI.Application.Workflows.SendOrderBI.Dtos.Nominatim
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public class Address
    {
        [JsonProperty("road")]
        public string Road { get; set; }

        [JsonProperty("hamlet")]
        public string Hamlet { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("municipality")]
        public string Municipality { get; set; }

        [JsonProperty("state_district")]
        public string StateDistrict { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("ISO3166-2-lvl4")]
        public string Iso3166Lvl4 { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("postcode")]
        public string Postcode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }
    }

    public class LocationResponse
    {
        [JsonProperty("place_id")]
        public long PlaceId { get; set; }

        [JsonProperty("licence")]
        public string Licence { get; set; }

        [JsonProperty("osm_type")]
        public string OsmType { get; set; }

        [JsonProperty("osm_id")]
        public long OsmId { get; set; }

        [JsonProperty("lat")]
        public string Lat { get; set; }

        [JsonProperty("lon")]
        public string Lon { get; set; }

        [JsonProperty("class")]
        public string Class { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("place_rank")]
        public int PlaceRank { get; set; }

        [JsonProperty("importance")]
        public double Importance { get; set; }

        [JsonProperty("addresstype")]
        public string AddressType { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("extratags")]
        public object Extratags { get; set; }

        [JsonProperty("boundingbox")]
        public List<string> BoundingBox { get; set; }
    }
}
