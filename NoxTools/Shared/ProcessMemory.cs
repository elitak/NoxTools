using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections;
using System.Threading;
using System.IO;
using System.Text;

public class ProcessMemoryApi
{
	// constants information can be found in <winnt.h>
	public const int PROCESS_VM_READ = 0x0010;
	public const int PROCESS_ALL_ACCESS = 0x1F0FFF;
	public const int MEM_COMMIT = 0x1000;
	public const int MEM_DECOMMIT = 0x4000;
	public const int PAGE_READWRITE = 0x0004;
	[DllImport("user32.dll")] public static extern int FindWindowA(string lpClassName, string lpWindowName);
	[DllImport("user32.dll")] public static extern IntPtr GetWindowThreadProcessId(int hwnd, out int lpdwProcessId);
	[DllImport("kernel32.dll")] public static extern IntPtr OpenProcess(int dwDesiredAccess,  int bInheritHandle,  int dwProcessId);
	[DllImport("kernel32.dll")] public static extern int CloseHandle(IntPtr hObject);
	[DllImport("kernel32.dll")]	public static extern int ReadProcessMemory(IntPtr hProcess,	IntPtr lpBaseAddress, byte[] buffer, int size, out IntPtr lpNumberOfBytesRead);
	[DllImport("kernel32.dll")] public static extern int WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer,  int nSize, out int lpNumberOfBytesWritten);
	[DllImport("kernel32.dll")] public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, int dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, int dwCreationFlags, out int lpThreadId);
	[DllImport("kernel32.dll")] public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, int flAllocationType, int flProtect);
	[DllImport("kernel32.dll")] public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, int dwFreeType);
	[DllImport("kernel32.dll")] public static extern bool GetExitCodeThread(IntPtr hThread, out int returnVal);
	[DllImport("kernel32.dll")] public static extern bool TerminateThread(IntPtr hThread, out int returnVal);
}

//TODO: extend Stream
public class ProcessMemory// : Stream
{
	protected IntPtr processHandle;

	public IntPtr Handle
	{
		get
		{
			return processHandle;
		}
	}

	//TODO: add error checking?
	protected ProcessMemory(IntPtr processHandle)
	{
		this.processHandle = processHandle;
	}

	//close handle on destruction
	~ProcessMemory()
	{
		ProcessMemoryApi.CloseHandle(processHandle);
	}

	//call this to get an instance of ProcessMemory
	public static ProcessMemory OpenProcess(string processName)
	{
		Process[] procs = Process.GetProcessesByName(processName);
				
		if (procs.Length != 1)
			throw new ApplicationException("Process not found.");

		IntPtr hnd = ProcessMemoryApi.OpenProcess(ProcessMemoryApi.PROCESS_ALL_ACCESS, 0, procs[0].Id);
		if (hnd == IntPtr.Zero)
			throw new ApplicationException("Could not open process.");

		return new ProcessMemory(hnd);
	}

	public static ProcessMemory OpenProcessByWindowName(string className, string windowName)
	{
		int procId, wnd = ProcessMemoryApi.FindWindowA(className, windowName);
				
		if (wnd == 0)
			throw new ApplicationException("Window not found.");

		ProcessMemoryApi.GetWindowThreadProcessId(wnd, out procId);

		return new ProcessMemory(ProcessMemoryApi.OpenProcess(ProcessMemoryApi.PROCESS_ALL_ACCESS, 0, procId));
	}

	public void Close()
	{
		ProcessMemoryApi.CloseHandle(processHandle);
	}

	public byte[] Read(int offset, int size)
	{
		byte[] buffer = new byte[size];
		IntPtr numRead;

		//TODO: check to see if process handle is still valid (process still active)
		ProcessMemoryApi.ReadProcessMemory(processHandle, (IntPtr) offset, buffer, size, out numRead);

		return buffer;
	}

	public void Write(int offset, byte[] data)
	{
		int numWritten;
		//FIXME? check for PROCESS_VM_WRITE and PROCESS_VM_OPERATION?
		//TODO: check to see if process handle is still valid (process still active)
		ProcessMemoryApi.WriteProcessMemory(processHandle, (IntPtr) offset, data, data.Length, out numWritten);
		Debug.Assert(numWritten == data.Length, "Wrong number of bytes written.");
	}
		
	protected Hashtable dataEntries = new Hashtable();

	public IDictionary DataAddress
	{
		get
		{
			return (IDictionary) dataEntries.Clone();
		}
	}

	public void AddData(string key, byte[] data)
	{
		int numWritten;
		IntPtr bufferAddress = ProcessMemoryApi.VirtualAllocEx(processHandle, IntPtr.Zero, data.Length, ProcessMemoryApi.MEM_COMMIT, ProcessMemoryApi.PAGE_READWRITE);
		Debug.Assert(bufferAddress != IntPtr.Zero, "Could not allocate memory in target process.");
		ProcessMemoryApi.WriteProcessMemory(processHandle, bufferAddress, data, data.Length, out numWritten);
		Debug.Assert(numWritten == data.Length, "Bad write length returned from WriteProcessMemory()");
		dataEntries.Add(key, bufferAddress);
	}

	public void RemoveData(string key)
	{
		bool freed = ProcessMemoryApi.VirtualFreeEx(processHandle, (IntPtr) dataEntries[key], 0, ProcessMemoryApi.MEM_DECOMMIT);
		Debug.Assert(freed, "Unable to free allocated memory in process.");
		dataEntries.Remove(key);
	}

	protected int CallFunction(IntPtr startAddress, params int[] args)
	{
		ArrayList delegatorParams = new ArrayList(new int[] {(int) startAddress, args.Length});
		delegatorParams.AddRange(args);

		Executor exec = new Executor(this, delegatorParams);

		Thread execThread = new Thread(new ThreadStart(exec.Start));
		execThread.Start();

		execThread.Join();//MAX_WAIT);

		//if (execThread.IsAlive)
		//	execThread.Abort();

		return exec.result;
	}

	public int CallFunction(IntPtr startAddress, params object[] args)
		//public int CallFunction(IntPtr startAddress, string args)
	{
		ArrayList intArgs = new ArrayList();
		ArrayList outstandingData = new ArrayList();

		foreach (object obj in args)
		{
			if (obj.GetType() == typeof(int))
				intArgs.Add((int) obj);
			else if (obj.GetType() == typeof(byte))
				intArgs.Add((int) (byte)  obj);
			else if (obj.GetType() == typeof(uint))
				intArgs.Add((int) (uint) obj);
			else if (obj.GetType() == typeof(char[]))//if char[], treat as ascii
			{
				string str = new string((char[]) obj);
				AddData(str, Encoding.ASCII.GetBytes(str));
				outstandingData.Add(str);
				intArgs.Add((int) (IntPtr) DataAddress[str]);
			}
			else if (obj.GetType() == typeof(string))//if string, treat as unicode
			{
				AddData((string) obj, Encoding.Unicode.GetBytes((string) obj));//key it to itself
				outstandingData.Add(obj);
				intArgs.Add((int) (IntPtr) DataAddress[obj]);
			}
		}

		int result = CallFunction(startAddress, (int[]) intArgs.ToArray(typeof(int)));
			
		foreach (string key in outstandingData)
			RemoveData(key);

		return result;
	}

	protected class Executor
	{
		protected byte[] delegatorFunction = {0x55, 0x89, 0xe5, 0x83, 0xec, 0x08, 0x8b, 0x45, 0x08, 0x8b, 0x00, 0x89, 0x45, 0xfc, 0x8b, 0x45, 0x08, 0x83, 0xc0, 0x04, 0x8b, 0x00, 0x89, 0x45, 0xf8, 0x83, 0x7d, 0xf8, 0x00, 0x75, 0x02, 0xeb, 0x15, 0x8b, 0x45, 0xf8, 0xc1, 0xe0, 0x02, 0x03, 0x45, 0x08, 0x83, 0xc0, 0x04, 0xff, 0x30, 0x8d, 0x45, 0xf8, 0xff, 0x08, 0xeb, 0xe3, 0x8b, 0x45, 0xfc, 0xff, 0xd0, 0xc9, 0xc3};
		const int STILL_ACTIVE = 0x103;//from winnt.h

		protected IntPtr delegatorAddress;
		protected ArrayList delegatorParams;
		public int result;
		protected ProcessMemory procMem;

		public Executor(ProcessMemory procMem, ArrayList delegatorParams)
		{
			this.procMem = procMem;
			this.delegatorParams = delegatorParams;
		}

		public void Start()
		{
			IntPtr handle = IntPtr.Zero;
			int threadId;

			try
			{
				while (procMem.DataAddress["__delegatorParams"] != null)
					Thread.Sleep(0);
				procMem.AddData("__delegatorFunction", delegatorFunction);
				BinaryWriter wtr = new BinaryWriter(new MemoryStream());
				foreach (int element in delegatorParams)
					wtr.Write(element);
				procMem.AddData("__delegatorParams", ((MemoryStream)wtr.BaseStream).ToArray());

				handle = ProcessMemoryApi.CreateRemoteThread(procMem.Handle, IntPtr.Zero, 0, (IntPtr) procMem.DataAddress["__delegatorFunction"], (IntPtr) procMem.DataAddress["__delegatorParams"], 0, out threadId);
				while (true)
				{
					ProcessMemoryApi.GetExitCodeThread(handle, out result);
					if (result != STILL_ACTIVE)
						break;
					Thread.Sleep(100);
				}
			}
			catch (ThreadAbortException)
			{
				ProcessMemoryApi.TerminateThread(handle, out result);
			}
			finally
			{
				procMem.RemoveData("__delegatorFunction");
				procMem.RemoveData("__delegatorParams");
			}
		}
	}
}
