using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Microsoft.Win32;

using NoxShared;

namespace NoxBagTool
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class MainWindow : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private Bag bag;
		public MainWindow()
		{
			try
			{
				System.Diagnostics.Debug.Listeners.Add(new System.Diagnostics.TextWriterTraceListener("Debug.log"));
				System.Diagnostics.Debug.AutoFlush = true;
				System.Diagnostics.Debug.WriteLine(String.Format("Started at {0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));
				RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Westwood\\Nox");
				if (key == null)
				{
					MessageBox.Show("Can not find the Nox directory in the registry. You can try reinstalling Nox to fix this.", "Error");
					Environment.Exit(1);
				}
				string installPath = (string) key.GetValue("InstallPath");
				string bagPath = installPath.Substring(0, installPath.LastIndexOf("\\") + 1) + "Audio.bag";
				bag = new AudioBag(bagPath);
			}
			catch (Exception ex)
			{
				MessageBox.Show("Something went very wrong. See the message in the following message box for details.", "Fatal Error");
				MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
				throw ex;
			}
			//Bag bag = new VideoBag("c:\\Westwood\\Nox\\Video.bag");
			//bag = new AudioBag("c:\\Westwood\\Nox\\Audio.bag");

			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(88, 11);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(184, 20);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "Enter directory to extract to here";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(8, 11);
			this.button1.Name = "button1";
			this.button1.TabIndex = 1;
			this.button1.Text = "Extract";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// MainWindow
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(280, 45);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox1);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainWindow";
			this.Text = "Nox Audio.bag Extractor by d00d3r";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainWindow());
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			FileDialog dlg = new OpenFileDialog();

			//dlg.

			if (dlg.ShowDialog() == DialogResult.OK)
				textBox1.Text = dlg.FileName;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			bag.ExtractAll(textBox1.Text);
		}
	}
}
