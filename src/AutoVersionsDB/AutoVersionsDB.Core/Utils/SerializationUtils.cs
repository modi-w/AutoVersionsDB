﻿using Newtonsoft.Json;
using System;

namespace AutoVersionsDB.Core.Utils
{
    public class SerializationUtils
    {
        public static string JsonSerialize<T>(T obj)
        {
            JsonSerializerSettings settongObj = new Newtonsoft.Json.JsonSerializerSettings()
            {
                DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,

            };

            return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, settongObj);
        }

        public static T JsonDeserialize<T>(string str)
        {

            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            };

            return JsonConvert.DeserializeObject<T>(str, setting);
        }

        public static object JsonDeserialize(string str, Type type)
        {

            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            };

            return Newtonsoft.Json.JsonConvert.DeserializeObject(str, type, setting);
        }
    }
}
