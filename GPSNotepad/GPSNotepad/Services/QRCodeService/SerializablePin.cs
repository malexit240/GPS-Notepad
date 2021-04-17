using System;
using GPSNotepad.Entities;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace GPSNotepad.Services.QRCodeService
{
    [Serializable]
    struct SerializablePin
    {
        public string Name;

        public string Description;

        public bool Favorite;

        public double Longitude;

        public double Latitude;

        public static SerializablePin? CreateFromBase64String(string source)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            SerializablePin? pin = null;

            try
            {
                byte[] byteArray = Convert.FromBase64String(source);

                using var stream = new MemoryStream();

                for (int i = 0; i < byteArray.Length; i++)
                {
                    stream.WriteByte(byteArray[i]);
                }

                stream.Seek(0, SeekOrigin.Begin);

                pin = formatter.Deserialize(stream) as SerializablePin?;
            }
            catch (Exception) { }

            return pin;

        }

        public Pin ToModelPin()
        {
            return new Pin()
            {
                Name = this.Name,
                Description = this.Description,
                Favorite = this.Favorite,
                Latitude = this.Latitude,
                Longitude = this.Longitude
            };
        }

        public string GetBase64Form()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            byte[] byteArray;

            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, this);

                stream.Seek(0, SeekOrigin.Begin);

                byteArray = new byte[stream.Length];

                for (int i = 0; i < stream.Length; i++)
                {
                    byteArray[i] = Convert.ToByte(stream.ReadByte());
                }
            }

            return Convert.ToBase64String(byteArray);
        }
    }
}
