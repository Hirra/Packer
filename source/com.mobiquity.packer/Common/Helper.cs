﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace com.mobiquity.packer.Common
{
    public static class Helper
    {
        public static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public static T DeepCopy<T>(T item)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, item);
            stream.Seek(0, SeekOrigin.Begin);
            T result = (T)formatter.Deserialize(stream);
            stream.Close();
            return result;
        }
    }
}
