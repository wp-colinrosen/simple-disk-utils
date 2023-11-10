using UnityEngine;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using SimpleDiskUtils;
using SimpleDiskUtils.Sample;
using UnityEngine.UI;

public class TestDiskUtils : MonoBehaviour
{
	[SerializeField]
	private Text text;

	private string obj = "A";


	private void Update()
	{
		if (obj.Length >= 3000000)
			return;

		obj += obj;

		// Append until obj size is at least 3 MB
		if (obj.Length < 3000000)
			return;

		Tests();
	}


	private async void Tests()
	{
		text.text = "";

		var dir = Application.persistentDataPath + "/TestDiskUtils/";
		var storePath = Application.persistentDataPath + "/TestDiskUtils/Test.txt";


		foreach (var drive in Directory.GetLogicalDrives())
		{
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
			if (drive != "C:/")
			{
				dir = drive + "TestDiskUtils/";
				storePath = drive + "TestDiskUtils/Test.txt";
			}
#endif

			PrintDebugLn();
			PrintDebugLn(">>> NOW TESTING ON DRIVE " + drive + " <<<");

			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);

			if (File.Exists(storePath))
				File.Delete(storePath);

			await PrintStorageStats(drive);

			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);

			FileTools.SaveFile(obj, storePath);

			PrintDebugLn("===== FILE ADDED!!! (Test File is around 3-4 MB) =====");

			await PrintStorageStats(drive);

			if (File.Exists(storePath))
			{
				File.Delete(storePath);
				PrintDebugLn("===== FILE DELETED!!! =====");
			}
			else
			{
				PrintDebugLn("===== File not found: most likely also failed on create =====");
			}

			await PrintStorageStats(drive);
		}
	}


	private void PrintDebug(string str)
	{
		if (text != null)
			text.text += str;
		Debug.Log(str);
	}

	private void PrintDebugLn(string str = "")
	{
		PrintDebug($"{str}\n");
	}

	private async Task PrintStorageStats(string drive)
	{
		var available = await DiskUtils.CheckAvailableSpace(drive);
		var busy = await DiskUtils.CheckBusySpace(drive);
		var total = await DiskUtils.CheckTotalSpace(drive);

		PrintDebugLn("=========== AVAILABLE SPACE  : " + available + " MB ===========");
		PrintDebugLn("=========== BUSY SPACE  : " + busy + " MB ===========");
		PrintDebugLn("=========== TOTAL SPACE : " + total + " MB ===========");
	}
}