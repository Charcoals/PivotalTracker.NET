using System;

namespace PivotalTrackerDotNet.Domain
{
    public class FileAttachment
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int UploaderId { get; set; }
        public Person Uploader { get; set; }
        public bool Thumbnailable { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }
        public int Size { get; set; }
        public string DownloadUrl { get; set; }
        public string ContentType { get; set; }
        public bool Uploaded { get; set; }
        public string BigUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string Kind { get; set; }
    }
}