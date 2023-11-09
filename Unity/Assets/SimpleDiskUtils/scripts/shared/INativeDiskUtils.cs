namespace SimpleDiskUtils
{
	public interface INativeDiskUtils
	{
		/// <summary>
		/// Checks the available space.
		/// </summary>
		/// <returns>The available spaces in MB.</returns>
		/// <param name="drive">Disk name. For example, "C:/"</param>
		int CheckAvailableSpace(string drive = null);

		/// <summary>
		/// Checks the total space.
		/// </summary>
		/// <returns>The total space in MB.</returns>
		/// <param name="drive">Disk name. For example, "C:/"</param>
		int CheckTotalSpace(string drive = null);

		/// <summary>
		/// Checks the busy space.
		/// </summary>
		/// <returns>The busy space in MB.</returns>
		/// <param name="drive">Disk name. For example, "C:/"</param>
		int CheckBusySpace(string drive = null);

		/// <summary>
		/// Gets the list of available drives
		/// </summary>
		/// <returns>The list of available drives</returns>
		string[] GetDrives();
	}
}