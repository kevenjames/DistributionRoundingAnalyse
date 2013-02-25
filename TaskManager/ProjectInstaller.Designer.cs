namespace TaskManager
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
			this.TaskManagerProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
			this.TaskManagerInstaller = new System.ServiceProcess.ServiceInstaller();
			// 
			// TaskManagerProcessInstaller
			// 
			this.TaskManagerProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
			this.TaskManagerProcessInstaller.Password = null;
			this.TaskManagerProcessInstaller.Username = null;
			// 
			// TaskManagerInstaller
			// 
			this.TaskManagerInstaller.Description = "管理分布式任务";
			this.TaskManagerInstaller.DisplayName = "任务管理";
			this.TaskManagerInstaller.ServiceName = "TaskManager";
			// 
			// ProjectInstaller
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.TaskManagerProcessInstaller,
            this.TaskManagerInstaller});

		}

		#endregion

		private System.ServiceProcess.ServiceProcessInstaller TaskManagerProcessInstaller;
		private System.ServiceProcess.ServiceInstaller TaskManagerInstaller;
	}
}