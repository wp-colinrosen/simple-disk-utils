using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AOT;
using UnityEngine;

namespace SimpleDiskUtils
{
	public class DiskUtilsWebGL : INativeDiskUtils
	{
		private struct UsageData
		{
			public long Usage { get; }
			public long Quota { get; }


			public UsageData(long usage, long quota)
			{
				Usage = usage;
				Quota = quota;
			}
		}


		private const long MEGA_BYTE = 1048576;

		private static Queue<TaskCompletionSource<UsageData>> tasks = new();


		public async Task<int> CheckAvailableSpace(string drive = null)
		{
			var data = await GetUsageData();
			var free = data.Quota - data.Usage;

			return Mathf.RoundToInt(free / (float)MEGA_BYTE);
		}

		public async Task<int> CheckTotalSpace(string drive = null)
		{
			var data = await GetUsageData();
			return Mathf.RoundToInt(data.Quota / (float)MEGA_BYTE);
		}

		public async Task<int> CheckBusySpace(string drive = null)
		{
			var data = await GetUsageData();
			var busy = data.Quota - data.Usage;

			return Mathf.RoundToInt(busy / (float)MEGA_BYTE);
		}

		public string[] GetDrives()
		{
			return Directory.GetLogicalDrives();
		}


		private static Task<UsageData> GetUsageData()
		{
			var promise = new TaskCompletionSource<UsageData>();
			tasks.Enqueue(promise);
			GetEstimate(UsageEstimates);
			return promise.Task;
		}

		[DllImport("__Internal")]
		private static extern void GetEstimate(Action<IntPtr> callback);

		[MonoPInvokeCallback(typeof(Action<IntPtr>))]
		private static void UsageEstimates(IntPtr data)
		{
			var str = Marshal.PtrToStringUTF8(data).Split('|');
			var usage = Convert.ToInt64(str[0]);
			var quota = Convert.ToInt64(str[1]);

			var promise = tasks.Dequeue();
			promise.SetResult(new UsageData(usage, quota));
		}
	}
}