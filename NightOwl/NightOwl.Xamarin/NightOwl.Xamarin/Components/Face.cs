using System;

namespace NightOwl.Xamarin.Components
{
    public struct Face
    {
        public int Id { get; set; }
        public byte[] PhotoByteArr { get; set; }
        public string PersonName { get; set; }

        public string BlobURI { get; set; }
        public int OwnerId { get; set; }
    }
}
