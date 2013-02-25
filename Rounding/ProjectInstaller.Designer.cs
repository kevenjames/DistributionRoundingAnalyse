namespace Rounding
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
            this.RoundingProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.RoundingInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // RoundingProcessInstaller
            // 
            this.RoundingProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.RoundingProcessInstaller.Password = null;
            this.RoundingProcessInstaller.Username = null;
            // 
            // RoundingInstaller
            // 
            this.RoundingInstaller.DisplayName = "走势分析";
            this.RoundingInstaller.ServiceName = "RoundingAnalyse";
            this.RoundingInstaller.Description = "统计分析走势数据";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.RoundingProcessInstaller,
            this.RoundingInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller RoundingProcessInstaller;
        private System.ServiceProcess.ServiceInstaller RoundingInstaller;
    }
}