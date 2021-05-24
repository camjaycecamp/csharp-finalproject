namespace Personally_Controlled_Health_Record
{
    partial class frmLogin
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnLgnCancel = new System.Windows.Forms.Button();
            this.btnLgnLogin = new System.Windows.Forms.Button();
            this.btnLgnRegister = new System.Windows.Forms.Button();
            this.txtLgnUsername = new System.Windows.Forms.TextBox();
            this.txtLgnPassword = new System.Windows.Forms.TextBox();
            this.lblLgnUsername = new System.Windows.Forms.Label();
            this.lblLgnPassword = new System.Windows.Forms.Label();
            this.pchr42563DataSet = new Personally_Controlled_Health_Record.pchr42563DataSet();
            this.patienT_TBLTableAdapter = new Personally_Controlled_Health_Record.pchr42563DataSetTableAdapters.PATIENT_TBLTableAdapter();
            this.tableAdapterManager = new Personally_Controlled_Health_Record.pchr42563DataSetTableAdapters.TableAdapterManager();
            this.tipLgnToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pchr42563DataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLgnCancel
            // 
            this.btnLgnCancel.Location = new System.Drawing.Point(83, 73);
            this.btnLgnCancel.Name = "btnLgnCancel";
            this.btnLgnCancel.Size = new System.Drawing.Size(82, 23);
            this.btnLgnCancel.TabIndex = 0;
            this.btnLgnCancel.Text = "Cancel";
            this.tipLgnToolTip.SetToolTip(this.btnLgnCancel, "Exit the application.");
            this.btnLgnCancel.UseVisualStyleBackColor = true;
            this.btnLgnCancel.Click += new System.EventHandler(this.btnLgnCancel_Click);
            // 
            // btnLgnLogin
            // 
            this.btnLgnLogin.Location = new System.Drawing.Point(193, 73);
            this.btnLgnLogin.Name = "btnLgnLogin";
            this.btnLgnLogin.Size = new System.Drawing.Size(82, 23);
            this.btnLgnLogin.TabIndex = 1;
            this.btnLgnLogin.Text = "Login";
            this.tipLgnToolTip.SetToolTip(this.btnLgnLogin, "Attempt to login with the provided credentials.");
            this.btnLgnLogin.UseVisualStyleBackColor = true;
            this.btnLgnLogin.Click += new System.EventHandler(this.btnLgnLogin_Click);
            // 
            // btnLgnRegister
            // 
            this.btnLgnRegister.Location = new System.Drawing.Point(83, 102);
            this.btnLgnRegister.Name = "btnLgnRegister";
            this.btnLgnRegister.Size = new System.Drawing.Size(192, 23);
            this.btnLgnRegister.TabIndex = 2;
            this.btnLgnRegister.Text = "Register";
            this.tipLgnToolTip.SetToolTip(this.btnLgnRegister, "Register a new patient.");
            this.btnLgnRegister.UseVisualStyleBackColor = true;
            this.btnLgnRegister.Click += new System.EventHandler(this.btnLgnRegister_Click);
            // 
            // txtLgnUsername
            // 
            this.txtLgnUsername.Location = new System.Drawing.Point(83, 12);
            this.txtLgnUsername.Name = "txtLgnUsername";
            this.txtLgnUsername.Size = new System.Drawing.Size(192, 20);
            this.txtLgnUsername.TabIndex = 3;
            // 
            // txtLgnPassword
            // 
            this.txtLgnPassword.Location = new System.Drawing.Point(83, 38);
            this.txtLgnPassword.Name = "txtLgnPassword";
            this.txtLgnPassword.Size = new System.Drawing.Size(192, 20);
            this.txtLgnPassword.TabIndex = 4;
            // 
            // lblLgnUsername
            // 
            this.lblLgnUsername.AutoSize = true;
            this.lblLgnUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblLgnUsername.Location = new System.Drawing.Point(12, 13);
            this.lblLgnUsername.Name = "lblLgnUsername";
            this.lblLgnUsername.Size = new System.Drawing.Size(65, 15);
            this.lblLgnUsername.TabIndex = 5;
            this.lblLgnUsername.Text = "Username";
            // 
            // lblLgnPassword
            // 
            this.lblLgnPassword.AutoSize = true;
            this.lblLgnPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblLgnPassword.Location = new System.Drawing.Point(12, 39);
            this.lblLgnPassword.Name = "lblLgnPassword";
            this.lblLgnPassword.Size = new System.Drawing.Size(61, 15);
            this.lblLgnPassword.TabIndex = 6;
            this.lblLgnPassword.Text = "Password";
            // 
            // pchr42563DataSet
            // 
            this.pchr42563DataSet.DataSetName = "pchr42563DataSet";
            this.pchr42563DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // patienT_TBLTableAdapter
            // 
            this.patienT_TBLTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.ALLERGY_TBLTableAdapter = null;
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.CONDITIONTableAdapter = null;
            this.tableAdapterManager.Connection = null;
            this.tableAdapterManager.IMMUNIZATION_TBLTableAdapter = null;
            this.tableAdapterManager.MED_PROC_TBLTableAdapter = null;
            this.tableAdapterManager.MEDICATION_TBLTableAdapter = null;
            this.tableAdapterManager.PATIENT_TBLTableAdapter = null;
            this.tableAdapterManager.PER_DETAILS_TBLTableAdapter = null;
            this.tableAdapterManager.PRIMARY_CARE_TBLTableAdapter = null;
            this.tableAdapterManager.TEST_TBLTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = Personally_Controlled_Health_Record.pchr42563DataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 136);
            this.Controls.Add(this.lblLgnPassword);
            this.Controls.Add(this.lblLgnUsername);
            this.Controls.Add(this.txtLgnPassword);
            this.Controls.Add(this.txtLgnUsername);
            this.Controls.Add(this.btnLgnRegister);
            this.Controls.Add(this.btnLgnLogin);
            this.Controls.Add(this.btnLgnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmLogin";
            this.Text = "Patient Login";
            ((System.ComponentModel.ISupportInitialize)(this.pchr42563DataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLgnCancel;
        private System.Windows.Forms.Button btnLgnLogin;
        private System.Windows.Forms.Button btnLgnRegister;
        private System.Windows.Forms.TextBox txtLgnUsername;
        private System.Windows.Forms.TextBox txtLgnPassword;
        private System.Windows.Forms.Label lblLgnUsername;
        private System.Windows.Forms.Label lblLgnPassword;
        private pchr42563DataSet pchr42563DataSet;
        private pchr42563DataSetTableAdapters.PATIENT_TBLTableAdapter patienT_TBLTableAdapter;
        private pchr42563DataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.ToolTip tipLgnToolTip;
    }
}

