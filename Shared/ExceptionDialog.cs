using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Web.Mail;
using System.IO;

namespace NoxShared
{
	/// <summary>
	/// Summary description for ExceptionDialog.
	/// </summary>
	public class ExceptionDialog : System.Windows.Forms.Form
	{
		private const string defaultFrom = "user@domain";
		private const string defaultTo = "MyEnemyMyFriend@hotmail.com";

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox boxEmailTo;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button buttonSend;
		private System.Windows.Forms.TextBox boxFrom;
		private System.Windows.Forms.TextBox boxMessage;
		private System.Windows.Forms.TextBox boxNotes;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonCancel;

		public ExceptionDialog(Exception ex)
		{
			InitializeComponent();

			//fill the text box
			ArrayList text = new ArrayList();
			text.Add("Version: " + Application.ProductVersion);
			text.Add("");
			text.Add(ex.Message);
			text.Add(ex.StackTrace);
			text.Add("");
			boxMessage.Lines = (string[]) text.ToArray(typeof(string));
			boxMessage.Select(boxMessage.Text.Length, 0);

			//use default email addresses
			boxFrom.Text = defaultFrom;
			boxEmailTo.Text = defaultTo;

			//save the message to disk
			StreamWriter wtr = new StreamWriter("CrashLog.txt");
			wtr.Write(ex.Message + "\r\n" + ex.Source + "\r\n" + ex.StackTrace + "\r\n\r\n");
			if (ex.InnerException != null)
				wtr.Write(ex.InnerException.Message + "\r\n" + ex.InnerException.Source + "\r\n" + ex.InnerException.StackTrace + "\r\n\r\n");
			wtr.Close();
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.boxMessage = new System.Windows.Forms.TextBox();
			this.buttonSend = new System.Windows.Forms.Button();
			this.boxEmailTo = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.boxFrom = new System.Windows.Forms.TextBox();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.boxNotes = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// boxMessage
			// 
			this.boxMessage.Location = new System.Drawing.Point(8, 8);
			this.boxMessage.Multiline = true;
			this.boxMessage.Name = "boxMessage";
			this.boxMessage.ReadOnly = true;
			this.boxMessage.Size = new System.Drawing.Size(504, 136);
			this.boxMessage.TabIndex = 0;
			this.boxMessage.Text = "";
			// 
			// buttonSend
			// 
			this.buttonSend.Location = new System.Drawing.Point(168, 224);
			this.buttonSend.Name = "buttonSend";
			this.buttonSend.TabIndex = 1;
			this.buttonSend.Text = "Send";
			this.buttonSend.Click += new System.EventHandler(this.button1_Click);
			// 
			// boxEmailTo
			// 
			this.boxEmailTo.Location = new System.Drawing.Point(128, 152);
			this.boxEmailTo.Name = "boxEmailTo";
			this.boxEmailTo.Size = new System.Drawing.Size(152, 20);
			this.boxEmailTo.TabIndex = 2;
			this.boxEmailTo.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 152);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 23);
			this.label1.TabIndex = 3;
			this.label1.Text = "Email Crash Report to:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(56, 192);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 16);
			this.label3.TabIndex = 5;
			this.label3.Text = "Your Email";
			// 
			// boxFrom
			// 
			this.boxFrom.Location = new System.Drawing.Point(128, 192);
			this.boxFrom.Name = "boxFrom";
			this.boxFrom.Size = new System.Drawing.Size(152, 20);
			this.boxFrom.TabIndex = 6;
			this.boxFrom.Text = "";
			// 
			// buttonCancel
			// 
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new System.Drawing.Point(272, 224);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.TabIndex = 7;
			this.buttonCancel.Text = "Cancel";
			// 
			// boxNotes
			// 
			this.boxNotes.Location = new System.Drawing.Point(336, 152);
			this.boxNotes.Multiline = true;
			this.boxNotes.Name = "boxNotes";
			this.boxNotes.Size = new System.Drawing.Size(176, 64);
			this.boxNotes.TabIndex = 8;
			this.boxNotes.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(296, 152);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 16);
			this.label2.TabIndex = 9;
			this.label2.Text = "Notes";
			// 
			// ExceptionDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.buttonCancel;
			this.ClientSize = new System.Drawing.Size(522, 255);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.boxNotes);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.boxFrom);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.boxEmailTo);
			this.Controls.Add(this.buttonSend);
			this.Controls.Add(this.boxMessage);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "ExceptionDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Crash Report";
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			if (boxFrom.Text == defaultFrom || boxFrom.Text.Split('@').Length != 2)
			{
				MessageBox.Show("Please enter your email address.");
				return;
			}

			Hide();

			MailMessage msg = new MailMessage();
			msg.From = boxFrom.Text;
			msg.To = boxEmailTo.Text;
			msg.Subject = "NoxMapEditor Crash Report";
			msg.Body = boxMessage.Text + (boxNotes.Text == "" ? "" : "\n\nNotes:\n" + boxNotes.Text);

			bool sent = false;
			foreach (string server in DnsLib.DnsApi.GetMXRecords(boxEmailTo.Text.Split('@')[1]))
			{
				SmtpMail.SmtpServer = server;
				try
				{
					SmtpMail.Send(msg);
					sent = true;
					break;
				}
				catch (Exception) {}
			}
			if (!sent)
				MessageBox.Show("Couldn't send mail message.");
		}
	}
}
