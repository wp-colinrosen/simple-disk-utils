using UnityEngine;
using System.Collections;
using System.IO;
using SimpleDiskUtils;
using SimpleDiskUtils.Sample;

public class TestDiskUtils : MonoBehaviour
{
	[SerializeField]
	private TextMesh text;

	private string obj = "A";


	private void Update()
	{
		if (obj.Length >= 3000000)
			return;

		obj += obj;

		// Append until obj size is at least 3 MB
		if (obj.Length < 3000000)
			return;

		StartCoroutine(Tests());
	}


	private IEnumerator Tests()
	{
		text.text = "";

		var dir = Application.persistentDataPath + "/TestDiskUtils/";
		var storePath = Application.persistentDataPath + "/TestDiskUtils/Test.txt";


#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
		foreach (var drive in Directory.GetLogicalDrives())
		{
			if (drive != "C:/")
			{
				dir = drive + "TestDiskUtils/";
				storePath = drive + "TestDiskUtils/Test.txt";
			}

			PrintDebugLn();
			PrintDebugLn(">>> NOW TESTING ON DRIVE " + drive + " <<<");
#elif UNITY_ANDROID
			var drive = "external";
#else
			var drive = string.Empty;
#endif

			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);

			if (File.Exists(storePath))
				File.Delete(storePath);


			PrintStorageStats(drive);

			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);

			FileTools.SaveFile(obj, storePath);

			PrintDebugLn("===== FILE ADDED!!! (Test File is around 3-4 MB) =====");

			PrintStorageStats(drive);

			if (File.Exists(storePath))
			{
				File.Delete(storePath);
				PrintDebugLn("===== FILE DELETED!!! =====");
			}
			else
			{
				PrintDebugLn("===== File not found: most likely also failed on create =====");
			}

			PrintStorageStats(drive);

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
		}
#endif

		yield return null;
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

	private void PrintStorageStats(string drive)
	{
		PrintDebugLn("=========== AVAILABLE SPACE  : " + DiskUtils.CheckAvailableSpace(drive) + " MB ===========");
		PrintDebugLn("=========== BUSY SPACE  : " + DiskUtils.CheckBusySpace(drive) + " MB ===========");
		PrintDebugLn("=========== TOTAL SPACE : " + DiskUtils.CheckTotalSpace(drive) + " MB ===========");
	}
}