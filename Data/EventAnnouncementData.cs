using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TWSA.Helpers;
using TWSA.Models;

namespace TWSA.Data
{
    public static class EventAnnouncementData
    {
        private static readonly List<EventAnnouncement> events = new List<EventAnnouncement>();
        private static readonly string filePath = "Data/events.txt";

        static EventAnnouncementData()
        {
            LoadData();
            if (events.Count == 0) // If no data, load default data
            {
                events.AddRange(DefaultData.GetDefaultEvents());
                SaveData();
            }
        }

        public static void AddEvent(EventAnnouncement eventAnnouncement)
        {
            events.Add(eventAnnouncement);
            SaveData();
        }

        public static List<EventAnnouncement> GetAllEvents()
        {
            return new List<EventAnnouncement>(events);
        }

        private static void SaveData()
        {
            var jsonData = JsonConvert.SerializeObject(events, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fffZ" });
            var encryptedData = EncryptionHelper.Encrypt(jsonData);
            File.WriteAllText(filePath, encryptedData);
        }

        private static void LoadData()
        {
            if (File.Exists(filePath))
            {
                try
                {
                    var encryptedData = File.ReadAllText(filePath);
                    var jsonData = EncryptionHelper.Decrypt(encryptedData);
                    var loadedEvents = JsonConvert.DeserializeObject<List<EventAnnouncement>>(jsonData, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.fffZ" });
                    if (loadedEvents != null)
                    {
                        events.AddRange(loadedEvents);
                    }
                }
                catch (JsonSerializationException ex)
                {
                    // Handle the case where the JSON data is not in the expected format
                    Console.WriteLine($"Error deserializing JSON data: {ex.Message}");
                    // Optionally, log the error or take other appropriate actions
                }
            }
        }
    }
}