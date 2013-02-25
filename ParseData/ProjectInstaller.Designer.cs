namespace ParseData
{
	partial class ProjectInstaller
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{ 
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.ParseDataProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
			this.ParseDataInstaller = new System.ServiceProcess.ServiceInstaller();
			// 
			// ParseDataProcessInstaller
			// 
			this.ParseDataProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
			this.ParseDataProcessInstaller.Password = null;
			this.ParseDataProcessInstaller.Username = null;
			// 
			// ParseDataInstaller
			// 
			this.ParseDataInstaller.Description = "解析导入的数据";
			this.ParseDataInstaller.DisplayName = "数据解析";
			this.ParseDataInstaller.ServiceName = "ParseData";
			// 
			// ProjectInstaller
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ParseDataProcessInstaller,
            this.ParseDataInstaller});

		}

		#endregion

		private System.ServiceProcess.ServiceProcessInstaller ParseDataProcessInstaller;
		private System.ServiceProcess.ServiceInstaller ParseDataInstaller;
	}
}