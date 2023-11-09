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


		/// <summary>
		/// Checks the available space.
		/// </summary>
		/// <returns>The available space in MB.</returns>
		/// <param name="drive">If set to <c>'external'</c> is external storage.</param>
		public int CheckAvailableSpace(string drive = null)
		{
			var isExternalStorage = string.Equals(drive, "external", StringComparison.InvariantCultureIgnoreCase);
			return androidClass.CallStatic<int>("availableSpace", isExternalStorage);
		}

		/// <summary>
		/// Checks the total space.
		/// </summary>
		/// <returns>The total space in MB.</returns>
		/// <param name="drive">If set to <c>'external'</c> is external storage.</param>
		public int CheckTotalSpace(string drive = null)
		{
			var isExternalStorage = string.Equals(drive, "external", StringComparison.InvariantCultureIgnoreCase);
			return androidClass.CallStatic<int>("totalSpace", isExternalStorage);
		}

		/// <summary>
		/// Checks the busy space.
		/// </summary>
		/// <returns>The busy space in MB.</returns>
		/// <param name="drive">If set to <c>'external'</c> is external storage.</param>
		public int CheckBusySpace(string drive = null)
		{
			var isExternalStorage = string.Equals(drive, "external", StringComparison.InvariantCultureIgnoreCase);
			return androidClass.CallStatic<int>("busySpace", isExternalStorage);
		}
	}
}