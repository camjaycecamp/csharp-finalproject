namespace Personally_Controlled_Health_Record
{
    partial class frmRegister
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
            this.grpRegLoginDetails = new System.Windows.Forms.GroupBox();
            this.lblReg_LDConfirmPassword = new System.Windows.Forms.Label();
            this.lblReg_LDPassword = new System.Windows.Forms.Label();
            this.lblReg_LDUsername = new System.Windows.Forms.Label();
            this.txtReg_LDConfirmPassword = new System.Windows.Forms.TextBox();
            this.pATIENT_TBLBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pchr42563DataSet = new Personally_Controlled_Health_Record.pchr42563DataSet();
            this.txtReg_LDPassword = new System.Windows.Forms.TextBox();
            this.txtReg_LDUsername = new System.Windows.Forms.TextBox();
            this.lblRegHeader = new System.Windows.Forms.Label();
            this.grpRegPersonalDetails = new System.Windows.Forms.GroupBox();
            this.rdoReg_PDGenderFemale = new System.Windows.Forms.RadioButton();
            this.rdoReg_PDGenderMale = new System.Windows.Forms.RadioButton();
            this.dtpReg_PDDateOfBirth = new System.Windows.Forms.DateTimePicker();
            this.txtReg_PDFirstName = new System.Windows.Forms.TextBox();
            this.txtReg_PDLastName = new System.Windows.Forms.TextBox();
            this.txtReg_PDInitials = new System.Windows.Forms.TextBox();
            this.lblReg_PDInitials = new System.Windows.Forms.Label();
            this.cmbReg_PDTitle = new System.Windows.Forms.ComboBox();
            this.txtReg_PDIdentityNumber = new System.Windows.Forms.TextBox();
            this.lblReg_PDGender = new System.Windows.Forms.Label();
            this.lblReg_PDDateOfBirth = new System.Windows.Forms.Label();
            this.lblReg_PDFirstName = new System.Windows.Forms.Label();
            this.lblReg_PDLastName = new System.Windows.Forms.Label();
            this.lblReg_PDTitle = new System.Windows.Forms.Label();
            this.lblReg_PDIdentityNumber = new System.Windows.Forms.Label();
            this.btnRegCancel = new System.Windows.Forms.Button();
            this.btnRegRegister = new System.Windows.Forms.Button();
            this.tblRegPatientTableAdapter = new Personally_Controlled_Health_Record.pchr42563DataSetTableAdapters.PATIENT_TBLTableAdapter();
            this.tableAdapterManager = new Personally_Controlled_Health_Record.pchr42563DataSetTableAdapters.TableAdapterManager();
            this.tipRegToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.grpRegLoginDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pATIENT_TBLBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pchr42563DataSet)).BeginInit();
            this.grpRegPersonalDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpRegLoginDetails
            // 
            this.grpRegLoginDetails.Controls.Add(this.lblReg_LDConfirmPassword);
            this.grpRegLoginDetails.Controls.Add(this.lblReg_LDPassword);
            this.grpRegLoginDetails.Controls.Add(this.lblReg_LDUsername);
            this.grpRegLoginDetails.Controls.Add(this.txtReg_LDConfirmPassword);
            this.grpRegLoginDetails.Controls.Add(this.txtReg_LDPassword);
            this.grpRegLoginDetails.Controls.Add(this.txtReg_LDUsername);
            this.grpRegLoginDetails.Location = new System.Drawing.Point(15, 38);
            this.grpRegLoginDetails.Name = "grpRegLoginDetails";
            this.grpRegLoginDetails.Size = new System.Drawing.Size(331, 103);
            this.grpRegLoginDetails.TabIndex = 0;
            this.grpRegLoginDetails.TabStop = false;
            this.grpRegLoginDetails.Text = "Login Details";
            // 
            // lblReg_LDConfirmPassword
            // 
            this.lblReg_LDConfirmPassword.AutoSize = true;
            this.lblReg_LDConfirmPassword.Location = new System.Drawing.Point(6, 74);
            this.lblReg_LDConfirmPassword.Name = "lblReg_LDConfirmPassword";
            this.lblReg_LDConfirmPassword.Size = new System.Drawing.Size(91, 13);
            this.lblReg_LDConfirmPassword.TabIndex = 5;
            this.lblReg_LDConfirmPassword.Text = "Confirm Password";
            // 
            // lblReg_LDPassword
            // 
            this.lblReg_LDPassword.AutoSize = true;
            this.lblReg_LDPassword.Location = new System.Drawing.Point(6, 48);
            this.lblReg_LDPassword.Name = "lblReg_LDPassword";
            this.lblReg_LDPassword.Size = new System.Drawing.Size(53, 13);
            this.lblReg_LDPassword.TabIndex = 4;
            this.lblReg_LDPassword.Text = "Password";
            // 
            // lblReg_LDUsername
            // 
            this.lblReg_LDUsername.AutoSize = true;
            this.lblReg_LDUsername.Location = new System.Drawing.Point(6, 22);
            this.lblReg_LDUsername.Name = "lblReg_LDUsername";
            this.lblReg_LDUsername.Size = new System.Drawing.Size(55, 13);
            this.lblReg_LDUsername.TabIndex = 3;
            this.lblReg_LDUsername.Text = "Username";
            // 
            // txtReg_LDConfirmPassword
            // 
            this.txtReg_LDConfirmPassword.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.pATIENT_TBLBindingSource, "PASSWORD", true));
            this.txtReg_LDConfirmPassword.Location = new System.Drawing.Point(104, 71);
            this.txtReg_LDConfirmPassword.Name = "txtReg_LDConfirmPassword";
            this.txtReg_LDConfirmPassword.Size = new System.Drawing.Size(154, 20);
            this.txtReg_LDConfirmPassword.TabIndex = 2;
            // 
            // pATIENT_TBLBindingSource
            // 
            this.pATIENT_TBLBindingSource.DataMember = "PATIENT_TBL";
            this.pATIENT_TBLBindingSource.DataSource = this.pchr42563DataSet;
            // 
            // pchr42563DataSet
            // 
            this.pchr42563DataSet.DataSetName = "pchr42563DataSet";
            this.pchr42563DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // txtReg_LDPassword
            // 
            this.txtReg_LDPassword.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.pATIENT_TBLBindingSource, "PASSWORD", true));
            this.txtReg_LDPassword.Location = new System.Drawing.Point(104, 45);
            this.txtReg_LDPassword.Name = "txtReg_LDPassword";
            this.txtReg_LDPassword.Size = new System.Drawing.Size(154, 20);
            this.txtReg_LDPassword.TabIndex = 1;
            // 
            // txtReg_LDUsername
            // 
            this.txtReg_LDUsername.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.pATIENT_TBLBindingSource, "USERNAME", true));
            this.txtReg_LDUsername.Location = new System.Drawing.Point(104, 19);
            this.txtReg_LDUsername.Name = "txtReg_LDUsername";
            this.txtReg_LDUsername.Size = new System.Drawing.Size(154, 20);
            this.txtReg_LDUsername.TabIndex = 0;
            // 
            // lblRegHeader
            // 
            this.lblRegHeader.AutoSize = true;
            this.lblRegHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblRegHeader.Location = new System.Drawing.Point(12, 9);
            this.lblRegHeader.Name = "lblRegHeader";
            this.lblRegHeader.Size = new System.Drawing.Size(330, 15);
            this.lblRegHeader.TabIndex = 1;
            this.lblRegHeader.Text = "Please enter all of the following details to register:";
            // 
            // grpRegPersonalDetails
            // 
            this.grpRegPersonalDetails.Controls.Add(this.rdoReg_PDGenderFemale);
            this.grpRegPersonalDetails.Controls.Add(this.rdoReg_PDGenderMale);
            this.grpRegPersonalDetails.Controls.Add(this.dtpReg_PDDateOfBirth);
            this.grpRegPersonalDetails.Controls.Add(this.txtReg_PDFirstName);
            this.grpRegPersonalDetails.Controls.Add(this.txtReg_PDLastName);
            this.grpRegPersonalDetails.Controls.Add(this.txtReg_PDInitials);
            this.grpRegPersonalDetails.Controls.Add(this.lblReg_PDInitials);
            this.grpRegPersonalDetails.Controls.Add(this.cmbReg_PDTitle);
            this.grpRegPersonalDetails.Controls.Add(this.txtReg_PDIdentityNumber);
            this.grpRegPersonalDetails.Controls.Add(this.lblReg_PDGender);
            this.grpRegPersonalDetails.Controls.Add(this.lblReg_PDDateOfBirth);
            this.grpRegPersonalDetails.Controls.Add(this.lblReg_PDFirstName);
            this.grpRegPersonalDetails.Controls.Add(this.lblReg_PDLastName);
            this.grpRegPersonalDetails.Controls.Add(this.lblReg_PDTitle);
            this.grpRegPersonalDetails.Controls.Add(this.lblReg_PDIdentityNumber);
            this.grpRegPersonalDetails.Location = new System.Drawing.Point(15, 147);
            this.grpRegPersonalDetails.Name = "grpRegPersonalDetails";
            this.grpRegPersonalDetails.Size = new System.Drawing.Size(331, 180);
            this.grpRegPersonalDetails.TabIndex = 2;
            this.grpRegPersonalDetails.TabStop = false;
            this.grpRegPersonalDetails.Text = "Personal Details";
            // 
            // rdoReg_PDGenderFemale
            // 
            this.rdoReg_PDGenderFemale.AutoSize = true;
            this.rdoReg_PDGenderFemale.Location = new System.Drawing.Point(158, 150);
            this.rdoReg_PDGenderFemale.Name = "rdoReg_PDGenderFemale";
            this.rdoReg_PDGenderFemale.Size = new System.Drawing.Size(59, 17);
            this.rdoReg_PDGenderFemale.TabIndex = 14;
            this.rdoReg_PDGenderFemale.Text = "Female";
            this.rdoReg_PDGenderFemale.UseVisualStyleBackColor = true;
            // 
            // rdoReg_PDGenderMale
            // 
            this.rdoReg_PDGenderMale.AutoSize = true;
            this.rdoReg_PDGenderMale.Checked = true;
            this.rdoReg_PDGenderMale.Location = new System.Drawing.Point(104, 150);
            this.rdoReg_PDGenderMale.Name = "rdoReg_PDGenderMale";
            this.rdoReg_PDGenderMale.Size = new System.Drawing.Size(48, 17);
            this.rdoReg_PDGenderMale.TabIndex = 13;
            this.rdoReg_PDGenderMale.TabStop = true;
            this.rdoReg_PDGenderMale.Text = "Male";
            this.rdoReg_PDGenderMale.UseVisualStyleBackColor = true;
            // 
            // dtpReg_PDDateOfBirth
            // 
            this.dtpReg_PDDateOfBirth.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.pATIENT_TBLBindingSource, "DATE_Of_BIRTH", true));
            this.dtpReg_PDDateOfBirth.Location = new System.Drawing.Point(104, 124);
            this.dtpReg_PDDateOfBirth.MaxDate = new System.DateTime(2021, 4, 20, 0, 0, 0, 0);
            this.dtpReg_PDDateOfBirth.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpReg_PDDateOfBirth.Name = "dtpReg_PDDateOfBirth";
            this.dtpReg_PDDateOfBirth.Size = new System.Drawing.Size(196, 20);
            this.dtpReg_PDDateOfBirth.TabIndex = 12;
            this.dtpReg_PDDateOfBirth.Value = new System.DateTime(2003, 4, 20, 0, 0, 0, 0);
            // 
            // txtReg_PDFirstName
            // 
            this.txtReg_PDFirstName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.pATIENT_TBLBindingSource, "FIRST_NAME", true));
            this.txtReg_PDFirstName.Location = new System.Drawing.Point(104, 98);
            this.txtReg_PDFirstName.Name = "txtReg_PDFirstName";
            this.txtReg_PDFirstName.Size = new System.Drawing.Size(214, 20);
            this.txtReg_PDFirstName.TabIndex = 11;
            // 
            // txtReg_PDLastName
            // 
            this.txtReg_PDLastName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.pATIENT_TBLBindingSource, "LAST_NAME", true));
            this.txtReg_PDLastName.Location = new System.Drawing.Point(104, 72);
            this.txtReg_PDLastName.Name = "txtReg_PDLastName";
            this.txtReg_PDLastName.Size = new System.Drawing.Size(214, 20);
            this.txtReg_PDLastName.TabIndex = 10;
            // 
            // txtReg_PDInitials
            // 
            this.txtReg_PDInitials.Location = new System.Drawing.Point(241, 45);
            this.txtReg_PDInitials.Name = "txtReg_PDInitials";
            this.txtReg_PDInitials.Size = new System.Drawing.Size(77, 20);
            this.txtReg_PDInitials.TabIndex = 9;
            // 
            // lblReg_PDInitials
            // 
            this.lblReg_PDInitials.AutoSize = true;
            this.lblReg_PDInitials.Location = new System.Drawing.Point(199, 48);
            this.lblReg_PDInitials.Name = "lblReg_PDInitials";
            this.lblReg_PDInitials.Size = new System.Drawing.Size(36, 13);
            this.lblReg_PDInitials.TabIndex = 8;
            this.lblReg_PDInitials.Text = "Initials";
            // 
            // cmbReg_PDTitle
            // 
            this.cmbReg_PDTitle.FormattingEnabled = true;
            this.cmbReg_PDTitle.Items.AddRange(new object[] {
            "Mr.",
            "Ms.",
            "Mrs.",
            "Dr."});
            this.cmbReg_PDTitle.Location = new System.Drawing.Point(104, 45);
            this.cmbReg_PDTitle.Name = "cmbReg_PDTitle";
            this.cmbReg_PDTitle.Size = new System.Drawing.Size(77, 21);
            this.cmbReg_PDTitle.TabIndex = 7;
            // 
            // txtReg_PDIdentityNumber
            // 
            this.txtReg_PDIdentityNumber.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.pATIENT_TBLBindingSource, "PRIMARY_ID", true));
            this.txtReg_PDIdentityNumber.Location = new System.Drawing.Point(104, 19);
            this.txtReg_PDIdentityNumber.Name = "txtReg_PDIdentityNumber";
            this.txtReg_PDIdentityNumber.Size = new System.Drawing.Size(214, 20);
            this.txtReg_PDIdentityNumber.TabIndex = 6;
            // 
            // lblReg_PDGender
            // 
            this.lblReg_PDGender.AutoSize = true;
            this.lblReg_PDGender.Location = new System.Drawing.Point(6, 152);
            this.lblReg_PDGender.Name = "lblReg_PDGender";
            this.lblReg_PDGender.Size = new System.Drawing.Size(42, 13);
            this.lblReg_PDGender.TabIndex = 5;
            this.lblReg_PDGender.Text = "Gender";
            // 
            // lblReg_PDDateOfBirth
            // 
            this.lblReg_PDDateOfBirth.AutoSize = true;
            this.lblReg_PDDateOfBirth.Location = new System.Drawing.Point(6, 127);
            this.lblReg_PDDateOfBirth.Name = "lblReg_PDDateOfBirth";
            this.lblReg_PDDateOfBirth.Size = new System.Drawing.Size(66, 13);
            this.lblReg_PDDateOfBirth.TabIndex = 4;
            this.lblReg_PDDateOfBirth.Text = "Date of Birth";
            // 
            // lblReg_PDFirstName
            // 
            this.lblReg_PDFirstName.AutoSize = true;
            this.lblReg_PDFirstName.Location = new System.Drawing.Point(6, 101);
            this.lblReg_PDFirstName.Name = "lblReg_PDFirstName";
            this.lblReg_PDFirstName.Size = new System.Drawing.Size(57, 13);
            this.lblReg_PDFirstName.TabIndex = 3;
            this.lblReg_PDFirstName.Text = "First Name";
            // 
            // lblReg_PDLastName
            // 
            this.lblReg_PDLastName.AutoSize = true;
            this.lblReg_PDLastName.Location = new System.Drawing.Point(6, 75);
            this.lblReg_PDLastName.Name = "lblReg_PDLastName";
            this.lblReg_PDLastName.Size = new System.Drawing.Size(58, 13);
            this.lblReg_PDLastName.TabIndex = 2;
            this.lblReg_PDLastName.Text = "Last Name";
            // 
            // lblReg_PDTitle
            // 
            this.lblReg_PDTitle.AutoSize = true;
            this.lblReg_PDTitle.Location = new System.Drawing.Point(6, 48);
            this.lblReg_PDTitle.Name = "lblReg_PDTitle";
            this.lblReg_PDTitle.Size = new System.Drawing.Size(27, 13);
            this.lblReg_PDTitle.TabIndex = 1;
            this.lblReg_PDTitle.Text = "Title";
            // 
            // lblReg_PDIdentityNumber
            // 
            this.lblReg_PDIdentityNumber.AutoSize = true;
            this.lblReg_PDIdentityNumber.Location = new System.Drawing.Point(6, 22);
            this.lblReg_PDIdentityNumber.Name = "lblReg_PDIdentityNumber";
            this.lblReg_PDIdentityNumber.Size = new System.Drawing.Size(81, 13);
            this.lblReg_PDIdentityNumber.TabIndex = 0;
            this.lblReg_PDIdentityNumber.Text = "Identity Number";
            // 
            // btnRegCancel
            // 
            this.btnRegCancel.Location = new System.Drawing.Point(185, 342);
            this.btnRegCancel.Name = "btnRegCancel";
            this.btnRegCancel.Size = new System.Drawing.Size(75, 23);
            this.btnRegCancel.TabIndex = 3;
            this.btnRegCancel.Text = "Cancel";
            this.tipRegToolTip.SetToolTip(this.btnRegCancel, "Return to the login form.");
            this.btnRegCancel.UseVisualStyleBackColor = true;
            this.btnRegCancel.Click += new System.EventHandler(this.btnRegCancel_Click);
            // 
            // btnRegRegister
            // 
            this.btnRegRegister.Location = new System.Drawing.Point(271, 342);
            this.btnRegRegister.Name = "btnRegRegister";
            this.btnRegRegister.Size = new System.Drawing.Size(75, 23);
            this.btnRegRegister.TabIndex = 4;
            this.btnRegRegister.Text = "Register";
            this.tipRegToolTip.SetToolTip(this.btnRegRegister, "Register a new patient under these fields.");
            this.btnRegRegister.UseVisualStyleBackColor = true;
            this.btnRegRegister.Click += new System.EventHandler(this.btnRegRegister_Click);
            // 
            // tblRegPatientTableAdapter
            // 
            this.tblRegPatientTableAdapter.ClearBeforeFill = true;
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
            // frmRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 390);
            this.Controls.Add(this.btnRegRegister);
            this.Controls.Add(this.btnRegCancel);
            this.Controls.Add(this.grpRegPersonalDetails);
            this.Controls.Add(this.lblRegHeader);
            this.Controls.Add(this.grpRegLoginDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmRegister";
            this.Text = "Register";
            this.grpRegLoginDetails.ResumeLayout(false);
            this.grpRegLoginDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pATIENT_TBLBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pchr42563DataSet)).EndInit();
            this.grpRegPersonalDetails.ResumeLayout(false);
            this.grpRegPersonalDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpRegLoginDetails;
        private System.Windows.Forms.Label lblReg_LDConfirmPassword;
        private System.Windows.Forms.Label lblReg_LDPassword;
        private System.Windows.Forms.Label lblReg_LDUsername;
        private System.Windows.Forms.TextBox txtReg_LDConfirmPassword;
        private System.Windows.Forms.TextBox txtReg_LDPassword;
        private System.Windows.Forms.TextBox txtReg_LDUsername;
        private System.Windows.Forms.Label lblRegHeader;
        private System.Windows.Forms.GroupBox grpRegPersonalDetails;
        private System.Windows.Forms.RadioButton rdoReg_PDGenderFemale;
        private System.Windows.Forms.RadioButton rdoReg_PDGenderMale;
        private System.Windows.Forms.DateTimePicker dtpReg_PDDateOfBirth;
        private System.Windows.Forms.TextBox txtReg_PDFirstName;
        private System.Windows.Forms.TextBox txtReg_PDLastName;
        private System.Windows.Forms.TextBox txtReg_PDInitials;
        private System.Windows.Forms.Label lblReg_PDInitials;
        private System.Windows.Forms.ComboBox cmbReg_PDTitle;
        private System.Windows.Forms.TextBox txtReg_PDIdentityNumber;
        private System.Windows.Forms.Label lblReg_PDGender;
        private System.Windows.Forms.Label lblReg_PDDateOfBirth;
        private System.Windows.Forms.Label lblReg_PDFirstName;
        private System.Windows.Forms.Label lblReg_PDLastName;
        private System.Windows.Forms.Label lblReg_PDTitle;
        private System.Windows.Forms.Label lblReg_PDIdentityNumber;
        private System.Windows.Forms.Button btnRegCancel;
        private System.Windows.Forms.Button btnRegRegister;
        private pchr42563DataSetTableAdapters.PATIENT_TBLTableAdapter tblRegPatientTableAdapter;
        private System.Windows.Forms.BindingSource pATIENT_TBLBindingSource;
        private pchr42563DataSet pchr42563DataSet;
        private pchr42563DataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.ToolTip tipRegToolTip;
    }
}