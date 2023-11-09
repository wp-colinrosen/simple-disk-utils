using System;
using UnityEngine;

namespace SimpleDiskUtils
{
	public class DiskUtilsAndroid : INativeDiskUtils
	{
		public int CheckAvailableSpace(string drive = null)
		{
			using var androidClass = new AndroidJavaClass("com.dikra.diskutils.DiskUtils");
			var isExternalStorage = string.Equals(drive, "external", StringComparison.InvariantCultureIgnoreCase);
			return androidClass.CallStatic<int>("availableSpace", isExternalStorage);
		}

		public int CheckTotalSpace(string drive = null)
		{
			using var androidClass = new AndroidJavaClass("com.dikra.diskutils.DiskUtils");
			var isExternalStorage = string.Equals(drive, "external", StringComparison.InvariantCultureIgnoreCase);
			return androidClass.CallStatic<int>("totalSpace", isExternalStorage);
		}

		public int CheckBusySpace(string drive = null)
		{
			using var androidClass = new AndroidJavaClass("com.dikra.diskutils.DiskUtils");
			var isExternalStorage = string.Equals(drive, "external", StringComparison.InvariantCultureIgnoreCase);
			return androidClass.CallStatic<int>("busySpace", isExternalStorage);
		}
		
		public string[] GetDrives()
		{
			return new[] { "internal", "external" };
		}
	}
}