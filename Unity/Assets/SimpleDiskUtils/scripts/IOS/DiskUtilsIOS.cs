using System.Runtime.InteropServices;

namespace SimpleDiskUtils
{
	public class DiskUtilsIOS : INativeDiskUtils
	{
		/// <summary>
		/// Checks the available space.
		/// </summary>
		/// <returns>The available space in MB.</returns>
		public int CheckAvailableSpace(string drive = null)
		{
			var ret = getAvailableDiskSpace();
			return int.Parse(ret.ToString());
		}

		/// <summary>
		/// Checks the total space.
		/// </summary>
		/// <returns>The total space in MB.</returns>
		public int CheckTotalSpace(string drive = null)
		{
			var ret = getTotalDiskSpace();
			return int.Parse(ret.ToString());
		}

		/// <summary>
		/// Checks the busy space.
		/// </summary>
		/// <returns>The busy space in MB.</returns>
		public int CheckBusySpace(string drive = null)
		{
			var ret = getBusyDiskSpace();
			return int.Parse(ret.ToString());
		}


		[DllImport("__Internal")]
		private static extern ulong getAvailableDiskSpace();

		[DllImport("__Internal")]
		private static extern ulong getTotalDiskSpace();

		[DllImport("__Internal")]
		private static extern ulong getBusyDiskSpace();
	}
}