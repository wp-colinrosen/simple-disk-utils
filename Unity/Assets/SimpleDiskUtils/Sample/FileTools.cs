using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SimpleDiskUtils.Sample
{
	public static class FileTools
	{
		/// <summary>
		/// Deletes the file.
		/// </summary>
		/// <param name="filePath">File path.</param>
		public static void DeleteFile(string filePath)
		{
#if UNITY_IOS
			if (!filePath.StartsWith("/private"))
				filePath = "/private" + filePath;
#endif

			if (File.Exists(filePath))
				File.Delete(filePath);
		}

		/// <summary>
		/// Saves object to file.
		/// </summary>
		/// <param name="obj">Object.</param>
		/// <param name="filePath">File path.</param>
		public static void SaveFile(object obj, string filePath)
		{
			if (!obj.GetType().IsSerializable)
				throw new ArgumentException("Passed data is invalid: not serializable.", "obj");

			var i = filePath.Length;
			while (i > 0 && filePath[i - 1] != '/')
				--i;

			if (i <= 0)
				SaveFile(obj, "", filePath);
			else
				SaveFile(obj, filePath[..i], filePath[i..]);
		}

		/// <summary>
		/// Saves object to file.
		/// </summary>
		/// <param name="obj">Serializable Object.</param>
		/// <param name="dirPath">Directory path.</param>
		/// <param name="fileName">File name.</param>
		public static void SaveFile(object obj, string dirPath, string fileName)
		{
			if (!obj.GetType().IsSerializable)
				throw new ArgumentException("Passed data is invalid: not serializable.", nameof(obj));

			string filePath;

			if (dirPath == "")
			{
				filePath = fileName;
			}
			else
			{
				if (dirPath.EndsWith("/"))
					filePath = dirPath + fileName;
				else
					filePath = dirPath + "/" + fileName;

				if (!Directory.Exists(dirPath))
					Directory.CreateDirectory(dirPath);
			}

			File.WriteAllBytes(filePath, ObjectToByteArray(obj));
		}

		/// <summary>
		/// Loads the file.
		/// </summary>
		/// <returns>The file.</returns>
		/// <param name="filePath">File path.</param>
		/// <typeparam name="T">Return type of the loaded object.</typeparam>
		public static T LoadFile<T>(string filePath)
		{
			return File.Exists(filePath)
				? ByteArrayToObject<T>(File.ReadAllBytes(filePath))
				: default;
		}

		/// <summary>
		/// Saves a string to text file.
		/// </summary>
		/// <param name="str">String.</param>
		/// <param name="filePath">File path.</param>
		public static void SaveTextFile(string str, string filePath)
		{
			var i = filePath.Length;
			while (i > 0 && filePath[i - 1] != '/')
				--i;

			if (i <= 0)
				SaveTextFile(str, "", filePath);
			else
				SaveTextFile(str, filePath[..i], filePath[i..]);
		}

		/// <summary>
		/// Saves a string to text file.
		/// </summary>
		/// <param name="str">String.</param>
		/// <param name="dirPath">Directory path.</param>
		/// <param name="fileName">File name.</param>
		public static void SaveTextFile(string str, string dirPath, string fileName)
		{
			string filePath;

			if (dirPath == "")
			{
				filePath = fileName;
			}
			else
			{
				if (dirPath.EndsWith("/"))
					filePath = dirPath + fileName;
				else
					filePath = dirPath + "/" + fileName;

				if (!Directory.Exists(dirPath))
					Directory.CreateDirectory(dirPath);
			}


			var sw = new StreamWriter(filePath);
			sw.WriteLine(str);
			sw.Close();
		}

		/// <summary>
		/// Loads the file.
		/// </summary>
		/// <returns>The file.</returns>
		/// <param name="filePath">File path.</param>
		/// <typeparam name="T">Return type of the loaded object.</typeparam>
		public static string LoadTextFile<T>(string filePath)
		{
			if (File.Exists(filePath))
			{
				using var sr = new StreamReader(filePath);
				return sr.ReadToEnd();
			}

			return null;
		}


		public static byte[] ObjectToByteArray(object obj)
		{
			if (obj == null)
				return null;

			if (obj is byte[] byteArr)
				return byteArr;

			var bf = new BinaryFormatter();
			using (var ms = new MemoryStream())
			{
				bf.Serialize(ms, obj);
				return ms.ToArray();
			}
		}

		public static T ByteArrayToObject<T>(byte[] bytes)
		{
			using (var memStream = new MemoryStream())
			{
				var bf = new BinaryFormatter();
				memStream.Write(bytes, 0, bytes.Length);
				memStream.Seek(0, SeekOrigin.Begin);
				return (T)bf.Deserialize(memStream);
			}
		}
	}
}