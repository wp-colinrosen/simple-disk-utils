using System;
using UnityEngine;

namespace SimpleDiskUtils
{
	public class DiskUtilsAndroid : INativeDiskUtils
	{
		private AndroidJavaClass androidClass;


		public DiskUtilsAndroid()
		{
			androidClass = new AndroidJavaClass("com.dikra.diskutils.Diskutils");
		}


		public int CheckAvailableSpace(string drive = null)
		{
			var isExternalStorage = string.Equals(drive, "external", StringComparison.InvariantCultureIgnoreCase);
			return androidClass.CallStatic<int>("availableSpace", isExternalStorage);
		}

		public int CheckTotalSpace(string drive = null)
		{
			var isExternalStorage = string.Equals(drive, "external", StringComparison.InvariantCultureIgnoreCase);
			return androidClass.CallStatic<int>("totalSpace", isExternalStorage);
		}

		public int CheckBusySpace(string drive = null)
		{
			var isExternalStorage = string.Equals(drive, "external", StringComparison.InvariantCultureIgnoreCase);
			return androidClass.CallStatic<int>("busySpace", isExternalStorage);
		}
		
		public string[] GetDrives()
		{
			return new[] { "internal", "external" };
		}
	}
}