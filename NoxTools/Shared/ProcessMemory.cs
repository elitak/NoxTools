using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections;
using System.Threading;
using System.IO;

public class ProcessMemoryApi
{
	// constants information can be found in <winnt.h>
	public const int PROCESS_VM_READ = 0x0010;
	public const int PROCESS_ALL_ACCESS = 0x1F0FFF;
	[DllImport("user32.dll")] public static extern int FindWindowA(string lpClassName, string lpWindowName);
	[DllImport("user32.dll")] public static extern IntPtr GetWindowThreadProcessId(int hwnd, out int lpdwProcessId);
	[DllImport("kernel32.dll")] public static extern IntPtr OpenProcess(int dwDesiredAccess,  int bInheritHandle,  int dwProcessId);
	[DllImport("kernel32.dll")] public static extern int CloseHandle(IntPtr hObject);
	[DllImport("kernel32.dll")]	public static extern int ReadProcessMemory(IntPtr hProcess,	IntPtr lpBaseAddress, byte[] buffer, uint size, out IntPtr lpNumberOfBytesRead);
	[DllImport("kernel32.dll")] public static extern int WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer,  uint nSize, out uint lpNumberOfBytesWritten);
	[DllImport("madRmt.dll")] public static extern IntPtr AllocMemEx(uint size, IntPtr processHandle);
	[DllImport("madRmt.dll")] public static extern bool FreeMemEx(IntPtr mem, IntPtr processHandle);
	[DllImport("madRmt.dll")] public static extern IntPtr CreateRemoteThreadEx(IntPtr processHandle, int threadAttr, uint stackSize, IntPtr startAddr, IntPtr args, int creationFlags, out uint threadId);
	[DllImport("kernel32.dll")] public static extern bool GetExitCodeThread(IntPtr hThread, out uint returnVal);
	[DllImport("kernel32.dll")] public static extern bool TerminateThread(IntPtr hThread, out uint returnVal);
	//[DllImport("madRmt.dll")] public static extern bool RemoteExecute(IntPtr processHandle, int func, out uint funcResult, object args, uint size);
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

	//TODO: add error checking
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

		return new ProcessMemory(ProcessMemoryApi.OpenProcess(ProcessMemoryApi.PROCESS_ALL_ACCESS, 0, procs[0].Id));
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

	public byte[] Read(uint offset, uint size)
	{
		byte[] buffer = new byte[size];
		IntPtr numRead;

		ProcessMemoryApi.ReadProcessMemory(processHandle, (IntPtr) offset, buffer, size, out numRead);

		return buffer;
	}

	public void Write(uint offset, byte[] data)
	{
		uint numWritten;
		ProcessMemoryApi.WriteProcessMemory(processHandle, (IntPtr) offset, data, (uint) data.Length, out numWritten);
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
		uint numWritten;
		IntPtr bufferAddress = ProcessMemoryApi.AllocMemEx((uint) data.Length, processHandle);
		ProcessMemoryApi.WriteProcessMemory(processHandle, bufferAddress, data, (uint) data.Length, out numWritten);
		Debug.Assert(numWritten == data.Length, "Bad write length returned from WriteProcessMemory()");
		dataEntries.Add(key, bufferAddress);
	}

	public void RemoveData(string key)
	{
		ProcessMemoryApi.FreeMemEx((IntPtr) dataEntries[key], processHandle);
		dataEntries.Remove(key);
	}

	protected uint CallFunction(IntPtr startAddress, params uint[] args)
	{
		ArrayList delegatorParams = new ArrayList(new uint[] {(uint) startAddress, (uint) args.Length});
		delegatorParams.AddRange(args);

		DateTime time1 = DateTime.Now;
		Console.WriteLine("setting up thread at {0}.{1}s", time1.Second, time1.Millisecond);
		Executor exec = new Executor(this, delegatorParams);

		Thread execThread = new Thread(new ThreadStart(exec.Start));
		execThread.Start();

		execThread.Join();//MAX_WAIT);
		DateTime time2 = DateTime.Now;
		Console.WriteLine("thread finished at {0}.{1}s", time2.Second, time2.Millisecond);
		time2.Subtract(time1);
		Console.WriteLine("total time was {0}.{1}s\n", time2.Second, time2.Millisecond);

		//if (execThread.IsAlive)
		//	execThread.Abort();

		return exec.result;
	}

	public uint CallFunction(IntPtr startAddress, params object[] args)
		//public uint CallFunction(IntPtr startAddress, string args)
	{
		ArrayList uintArgs = new ArrayList();
		ArrayList outstandingData = new ArrayList();

		foreach (object obj in args)
		{
			if (obj.GetType() == typeof(int))
				uintArgs.Add((uint) (int) obj);
			else if (obj.GetType() == typeof(byte))
				uintArgs.Add((uint) (byte)  obj);
			else if (obj.GetType() == typeof(uint))
				uintArgs.Add((uint) obj);
			else if (obj.GetType() == typeof(string))
			{
				AddData(obj as string, System.Text.Encoding.Unicode.GetBytes(obj as string));//key it to itself
				outstandingData.Add(obj);
				uintArgs.Add((uint) (IntPtr) DataAddress[obj]);
			}
		}

		uint result = CallFunction(startAddress, (uint[]) uintArgs.ToArray(typeof(uint)));
			
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
		public uint result;
		protected ProcessMemory procMem;

		public Executor(ProcessMemory procMem, ArrayList delegatorParams)
		{
			this.procMem = procMem;
			this.delegatorParams = delegatorParams;
		}

		public void Start()
		{
			IntPtr handle = IntPtr.Zero;
			uint threadId;

			try
			{
				DateTime time = DateTime.Now;
				Console.WriteLine("begin waiting to write params {0}.{1}s", time.Second, time.Millisecond);
				while (procMem.DataAddress["__delegatorParams"] != null)
					Thread.SpinWait(1);
				time = DateTime.Now;
				Console.WriteLine("done waiting to write params {0}.{1}s", time.Second, time.Millisecond);
				procMem.AddData("__delegatorFunction", delegatorFunction);
				BinaryWriter wtr = new BinaryWriter(new MemoryStream());
				foreach (uint element in delegatorParams)
					wtr.Write(element);
				procMem.AddData("__delegatorParams", ((MemoryStream)wtr.BaseStream).ToArray());
				time = DateTime.Now;
				Console.WriteLine("wrote params and function {0}.{1}s", time.Second, time.Millisecond);

				handle = ProcessMemoryApi.CreateRemoteThreadEx(procMem.Handle, 0, 0, (IntPtr) procMem.DataAddress["__delegatorFunction"], (IntPtr) procMem.DataAddress["__delegatorParams"], 0, out threadId);
				while (true)
				{
					ProcessMemoryApi.GetExitCodeThread(handle, out result);
					if (result != STILL_ACTIVE)
						break;
					Thread.SpinWait(1);
				}
				time = DateTime.Now;
				Console.WriteLine("done try block {0}.{1}s", time.Second, time.Millisecond);
			}
			catch (ThreadAbortException)
			{
				DateTime time = DateTime.Now;
				Console.WriteLine("aborting {0}.{1}s", time.Second, time.Millisecond);
				ProcessMemoryApi.TerminateThread(handle, out result);
			}
			finally
			{
				procMem.RemoveData("__delegatorFunction");
				procMem.RemoveData("__delegatorParams");
				DateTime time = DateTime.Now;
				Console.WriteLine("thread done. (finally block) {0}.{1}s", time.Second, time.Millisecond);
			}
		}
	}
}
