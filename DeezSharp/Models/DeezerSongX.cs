using System;
using Newtonsoft.Json;

namespace DeezSharp.Models
{
    public class DeezerSongX
    {
        [JsonProperty("SNG_ID")]
        public int SongId { get; set; }

        [JsonProperty("ALB_ID")]
        public int AlbumId { get; set; }

        [JsonProperty("ALB_PICTURE")]
        public string AlbumPicture { get; set; }

        [JsonProperty("ALB_TITLE")]
        public string AlbumTitle { get; set; }

        [JsonProperty("ARTISTS")]
        public DeezerArtistX[] Artists { get; set; }

        [JsonProperty("ART_ID")]
        public int ArtistId { get; set; }

        [JsonProperty("ART_NAME")]
        public string ArtistName { get; set; }

        [JsonProperty("SNG_TITLE")]
        public string SongTitle { get; set; }

        [JsonProperty("VERSION")]
        public string Version { get; set; }

        [JsonProperty("BPM")]
        public float Bpm { get; set; }

        [JsonProperty("DISK_NUMBER")]
        public int DiskNumber { get; set; }

        [JsonProperty("DURATION")]
        public int Duration { get; set; }

        [JsonProperty("GAIN")]
        public float Gain { get; set; }

        [JsonProperty("GENRE_ID")]
        public int GenreId { get; set; }

        [JsonProperty("DIGITAL_RELEASE_DATE")]
        public DateTime ReleaseDateDigital { get; set; }

        [JsonProperty("PHYSICAL_RELEASE_DATE")]
        public DateTime ReleaseDatePhysical { get; set; }

        [JsonProperty("RANK_SNG")]
        public int SongRank { get; set; }

        [JsonProperty("TRACK_NUMBER")]
        public int TrackNumber { get; set; }


        [JsonProperty("MD5_ORIGIN")]
        internal string OriginMd5 { get; set; }

        [JsonProperty("MEDIA_VERSION")]
        internal int MediaVersion { get; set; }

        [JsonProperty("__TYPE__")]
        internal string Type { get; set; }
    }
}
