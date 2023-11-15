using System;
using System.Threading.Tasks;
using UnityEngine;

namespace SimpleDiskUtils
{
	public class DiskUtilsAndroid : INativeDiskUtils
	{
		private const string DEFAULT_DRIVE = "external";
        
		
		public Task<int> CheckAvailableSpace(string drive = null)
		{
			using var androidClass = new AndroidJavaClass("com.dikra.diskutils.DiskUtils");
			var isExternalStorage = string.Equals(drive ?? DEFAULT_DRIVE, "external", StringComparison.InvariantCultureIgnoreCase);
			var result = androidClass.CallStatic<int>("availableSpace", isExternalStorage);
			return Task.FromResult(result);
		}

		public Task<int> CheckTotalSpace(string drive = null)
		{
			using var androidClass = new AndroidJavaClass("com.dikra.diskutils.DiskUtils");
			var isExternalStorage = string.Equals(drive ?? DEFAULT_DRIVE, "external", StringComparison.InvariantCultureIgnoreCase);
			var result = androidClass.CallStatic<int>("totalSpace", isExternalStorage);
			return Task.FromResult(result);
		}

		public Task<int> CheckBusySpace(string drive = null)
		{
			using var androidClass = new AndroidJavaClass("com.dikra.diskutils.DiskUtils");
			var isExternalStorage = string.Equals(drive ?? DEFAULT_DRIVE, "external", StringComparison.InvariantCultureIgnoreCase);
			var result = androidClass.CallStatic<int>("busySpace", isExternalStorage);
			return Task.FromResult(result);
		}
		
		public string[] GetDrives()
		{
			return new[] { "internal", "external" };
		}
	}
}