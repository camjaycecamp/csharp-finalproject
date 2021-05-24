using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personally_Controlled_Health_Record
{
    /*
     * this class houses the vast, VAST majority of the application, primarily consisting of all of the user's
     * personal and medical details, from the nitty gritty to contact information. the user can view, edit, and 
     * add to nearly all of these fields, and their changes are saved for later viewing
     */
    public partial class frmPersonalHealthRecord : Form
    {
        // class-relevant fields for easy references to the logged-in user's credentials
        static string loginID, primaryID, password;

        // basic class constructor that summons all of the form-populating methods
        public frmPersonalHealthRecord(string passedLoginID, string passedPrimaryID, string passedPassword)
        {
            loginID = passedLoginID;
            primaryID = passedPrimaryID;
            password = passedPassword;

            InitializeComponent();

            PD_LoadPATIENT_ID();
            PD_LoadPRIMARY_CARE();
            MD_LoadPER_DETAILS();
            MD_LoadALLERGY();
            MD_LoadIMMUNIZATION();
            MD_LoadMEDICATION();
            MD_LoadTEST();
            MD_LoadCONDITION();
            MD_LoadMED_PROC();
        }

        // loads the pchr dataset into the table adapters for use in the form
        private void frmPersonalHealthRecord_Load(object sender, EventArgs e)
        {
            this.aLLERGY_TBLTableAdapter.Fill(this.pchr42563DataSet.ALLERGY_TBL);
            this.pATIENT_TBLTableAdapter.Fill(this.pchr42563DataSet.PATIENT_TBL);
        }

        // method that displays an 'Under Construction' message box when either of the latter two tabs are selected
        private void tabPHR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabPHR.SelectedTab == tabPHR.TabPages[2]
                || tabPHR.SelectedTab == tabPHR.TabPages[3])
            {
                MessageBox.Show("Sorry, but this tab is currently under construction. Check again in the next update!", "Unavailable :(");
                tabPHR.SelectedTab = tabPHR.TabPages[0];
            }
        }



        /*
         * methods that load each category of the patient's data and injects it into the GUI via SqlReaders and SqlCommands.
         * these essentially function the same to one another, so there's no need to describe each individual method. the 
         * general schema is: form an Sql connection to the target table in the database, populate the relevant fields in
         * the relevant tab with the accessed information, if an error occurs, display it, and close the connection
         * when successfully completed, moving onto the next method
         */
        private void PD_LoadPATIENT_ID()
        {
            SqlConnection patientConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                    "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
            string selectStatement = "SELECT LAST_NAME, FIRST_NAME, DATE_Of_BIRTH, ADDRESS_STREET, ADDRESS_CITY, " +
                "ADDRESS_STATE, ADDRESS_ZIP, PHONE_HOME, PHONE_MOBILE, PRIMARY_ID, USERNAME, PASSWORD, GENDER, " +
                "TITLE, INITIALS, PHONE_WORK, FAX_NUMBER, EMAIL, NEXT_OF_KIN, KIN_RELATIONSHIP, KIN_STREET, KIN_STATE, " +
                "KIN_CITY, KIN_PHONE_HOME, KIN_PHONE_MOBILE, KIN_PHONE_WORK, KIN_ZIP, KIN_FAX_NUMBER, KIN_EMAIL," +
                "INSURER, INSURANCE_PLAN, INSURANCE_NUMBER, PROFILE_PIC " +
                "FROM PATIENT_TBL WHERE PATIENT_ID = @PATIENT_ID";
            SqlCommand selectCommand = new SqlCommand(selectStatement, patientConnection);
            selectCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
            try
            {
                patientConnection.Open();
                SqlDataReader PDReader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (PDReader.Read())
                {
                    txtPHR_PD_LDUsername.Text = PDReader["USERNAME"].ToString();
                    txtPHR_PD_PDIdentityNumber.Text = PDReader["PRIMARY_ID"].ToString();
                    cmbPHR_PD_PDTitle.Text = PDReader["TITLE"].ToString();
                    txtPHR_PD_PDInitials.Text = PDReader["INITIALS"].ToString();
                    if (PDReader["GENDER"].ToString() == "True")
                    {
                        rdoPHR_PD_PDGenderMale.Checked = true;
                    }
                    else if (PDReader["GENDER"].ToString() == "False")
                    {
                        rdoPHR_PD_PDGenderFemale.Checked = true;
                    }
                    txtPHR_PD_PDLastName.Text = PDReader["LAST_NAME"].ToString();
                    txtPHR_PD_PDFirstName.Text = PDReader["FIRST_NAME"].ToString();
                    dtpPHR_PD_PDDateOfBirth.Value = (DateTime)PDReader["DATE_Of_BIRTH"];
                    txtPHR_PD_CDAddress.Text = PDReader["ADDRESS_STREET"].ToString();
                    txtPHR_PD_CDState.Text = PDReader["ADDRESS_STATE"].ToString();
                    txtPHR_PD_CDCity.Text = PDReader["ADDRESS_CITY"].ToString();
                    txtPHR_PD_CDPostalCode.Text = PDReader["ADDRESS_ZIP"].ToString();
                    txtPHR_PD_CDHomeTelephone.Text = PDReader["PHONE_HOME"].ToString();
                    txtPHR_PD_CDMobileTelephone.Text = PDReader["PHONE_MOBILE"].ToString();
                    txtPHR_PD_CDWorkTelephone.Text = PDReader["PHONE_WORK"].ToString();
                    txtPHR_PD_CDFaxNumber.Text = PDReader["FAX_NUMBER"].ToString();
                    txtPHR_PD_CDEmail.Text = PDReader["EMAIL"].ToString();
                    txtPHR_PD_ECDNextOfKin.Text = PDReader["NEXT_OF_KIN"].ToString();
                    txtPHR_PD_ECDRelationship.Text = PDReader["KIN_RELATIONSHIP"].ToString();
                    txtPHR_PD_ECDAddress.Text = PDReader["KIN_STREET"].ToString();
                    txtPHR_PD_ECDState.Text = PDReader["KIN_STATE"].ToString();
                    txtPHR_PD_ECDCity.Text = PDReader["KIN_CITY"].ToString();
                    txtPHR_PD_ECDPostalCode.Text = PDReader["KIN_ZIP"].ToString();
                    txtPHR_PD_ECDHomeTelephone.Text = PDReader["KIN_PHONE_HOME"].ToString();
                    txtPHR_PD_ECDMobileTelephone.Text = PDReader["KIN_PHONE_MOBILE"].ToString();
                    txtPHR_PD_ECDWorkTelephone.Text = PDReader["KIN_PHONE_WORK"].ToString();
                    txtPHR_PD_ECDFaxNumber.Text = PDReader["KIN_FAX_NUMBER"].ToString();
                    txtPHR_PD_ECDEmail.Text = PDReader["KIN_EMAIL"].ToString();
                    txtPHR_PD_HIDInsurer.Text = PDReader["INSURER"].ToString();
                    txtPHR_PD_HIDInsurancePlan.Text = PDReader["INSURANCE_PLAN"].ToString();
                    txtPHR_PD_HIDInsuranceNumber.Text = PDReader["INSURANCE_NUMBER"].ToString();
                    try
                    {
                        Byte[] profilePic = (Byte[])PDReader["PROFILE_PIC"];
                        MemoryStream mem = new MemoryStream(profilePic);
                        picPHR_PDProfilePicture.Image = Image.FromStream(mem);
                    }
                    catch {}
                    patientConnection.Close();
                }
                else
                {
                    MessageBox.Show("Could not read.", "Error in PATIENT_ID");
                    patientConnection.Close();
                }
            }
            catch (Exception ex)
            {
                patientConnection.Close();
                MessageBox.Show(ex.ToString(), "Error in PATIENT_ID");
                throw ex;
            }
        }

        private void PD_LoadPRIMARY_CARE() 
        {
            SqlConnection primaryCareConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                    "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
            string selectStatement = "SELECT FULL_NAME, TITLE, SPECIALTY, PHONE_OFFICE, PHONE_MOBILE, " +
                "FAX_NUMBER, EMAIL FROM PRIMARY_CARE_TBL WHERE PRIMARY_ID = @PRIMARY_ID";
            SqlCommand selectCommand = new SqlCommand(selectStatement, primaryCareConnection);
            selectCommand.Parameters.AddWithValue("@PRIMARY_ID", primaryID);
            try
            {
                primaryCareConnection.Open();
                SqlDataReader PDReader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (PDReader.Read())
                {
                    txtPHR_PD_PCPFullName.Text = (PDReader["FULL_NAME"].ToString());
                    txtPHR_PD_PCPSpecialty.Text = PDReader["SPECIALTY"].ToString();
                    txtPHR_PD_PCPMobileTelephone.Text = PDReader["PHONE_MOBILE"].ToString();
                    txtPHR_PD_PCPWorkTelephone.Text = PDReader["PHONE_OFFICE"].ToString();
                    txtPHR_PD_PCPFaxNumber.Text = PDReader["FAX_NUMBER"].ToString();
                    txtPHR_PD_PCPEmail.Text = PDReader["EMAIL"].ToString();
                    primaryCareConnection.Close();
                }
                else
                {
                    primaryCareConnection.Close();
                }
            }
            catch (Exception ex)
            {
                primaryCareConnection.Close();
                MessageBox.Show(ex.ToString(), "Error in PRIMARY_CARE");
                throw ex;
            }
        }

        private void MD_LoadPER_DETAILS() 
        {
            SqlConnection personalDetailsConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                    "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
            string selectStatement = "SELECT BLOOD_TYPE, ORGAN_DONOR, HIV_STATUS, HEIGHT_INCHES, WEIGHT_LBS " +
                "FROM PER_DETAILS_TBL WHERE PATIENT_ID = @PATIENT_ID";
            SqlCommand selectCommand = new SqlCommand(selectStatement, personalDetailsConnection);
            selectCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
            try
            {
                personalDetailsConnection.Open();
                SqlDataReader MDReader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (MDReader.Read())
                {
                    cmbPHR_MD_PeMDBloodGroup.Text = MDReader["BLOOD_TYPE"].ToString();
                    if (MDReader["ORGAN_DONOR"].ToString() == "True")
                    {
                        chkPHR_MD_PeMDOrganDonor.Checked = true;
                    }
                    else if (MDReader["ORGAN_DONOR"].ToString() == "False") 
                    {
                        chkPHR_MD_PeMDOrganDonor.Checked = false;
                    }

                    if (MDReader["HIV_STATUS"].ToString() == "True")
                    {
                        rdoPHR_MD_PeMD_HSPositive.Checked = true;
                    }
                    else if (MDReader["HIV_STATUS"].ToString() == "False")
                    {
                        rdoPHR_MD_PeMD_HSNegative.Checked = true;
                    }
                    else 
                    {
                        rdoPHR_MD_PeMD_HSUnknown.Checked = true;
                    }
                    txtPHR_MD_PeMDHeight.Text = MDReader["HEIGHT_INCHES"].ToString();
                    txtPHR_MD_PeMDWeight.Text = MDReader["WEIGHT_LBS"].ToString();
                    personalDetailsConnection.Close();
                }
                else
                {
                    MessageBox.Show("Could not read.", "Error in PER_DETAILS");
                    personalDetailsConnection.Close();
                }
            }
            catch (Exception ex)
            {
                personalDetailsConnection.Close();
                MessageBox.Show(ex.ToString(), "Error in PER_DETAILS");
                throw ex;
            }
        }
        
        private void MD_LoadALLERGY() 
        {
            SqlConnection allergyConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                    "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
            string selectStatement = "SELECT ALLERGY_ID, ALLERGEN, ONSET_DATE, " +
                "NOTE FROM ALLERGY_TBL WHERE PATIENT_ID = @PATIENT_ID";
            SqlCommand selectCommand = new SqlCommand(selectStatement, allergyConnection);
            selectCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
            try
            {
                allergyConnection.Open();
                SqlDataReader MDReader = selectCommand.ExecuteReader();
                while (MDReader.Read()) 
                {
                    ListViewItem item = new ListViewItem(MDReader["ALLERGEN"].ToString());
                    item.SubItems.Add(MDReader["ONSET_DATE"].ToString());
                    item.SubItems.Add(MDReader["ALLERGY_ID"].ToString());
                    item.SubItems.Add(MDReader["NOTE"].ToString());

                    lvwPHR_MD_ADAllergies.Items.Add(item);

                    txtPHR_MD_ADAllergicTo.Text = "";
                    rtxPHR_MD_ADNote.Text = "";
                }
                allergyConnection.Close();
            }
            catch (Exception ex)
            {
                allergyConnection.Close();
                MessageBox.Show(ex.ToString(), "Error in ALLERGY");
                throw ex;
            }
        }

        private void MD_LoadIMMUNIZATION() 
        {
            SqlConnection immunizationConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                    "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
            string selectStatement = "SELECT IMMUNIZATION_ID, IMMUNIZATION, DATE, NOTE " +
                "FROM IMMUNIZATION_TBL WHERE PATIENT_ID = @PATIENT_ID";
            SqlCommand selectCommand = new SqlCommand(selectStatement, immunizationConnection);
            selectCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
            try
            {
                immunizationConnection.Open();
                SqlDataReader MDReader = selectCommand.ExecuteReader();
                while (MDReader.Read())
                {
                    ListViewItem item = new ListViewItem(MDReader["IMMUNIZATION"].ToString());
                    item.SubItems.Add(MDReader["DATE"].ToString());
                    item.SubItems.Add(MDReader["IMMUNIZATION_ID"].ToString());
                    item.SubItems.Add(MDReader["NOTE"].ToString());

                    lvwPHR_MD_IDImmunizations.Items.Add(item);

                    txtPHR_MD_IDImmunization.Text = "";
                    rtxPHR_MD_IDNote.Text = "";
                }
                immunizationConnection.Close();
            }
            catch (Exception ex)
            {
                immunizationConnection.Close();
                MessageBox.Show(ex.ToString(), "Error in IMMUNIZATION");
                throw ex;
            }
        }

        private void MD_LoadMEDICATION()
        {
            SqlConnection medicationConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                    "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
            string selectStatement = "SELECT MED_ID, MEDICATION, DATE, CHRONIC, NOTE " +
                "FROM MEDICATION_TBL WHERE PATIENT_ID = @PATIENT_ID";
            SqlCommand selectCommand = new SqlCommand(selectStatement, medicationConnection);
            selectCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
            try
            {
                medicationConnection.Open();
                SqlDataReader MDReader = selectCommand.ExecuteReader();
                while (MDReader.Read())
                {
                    ListViewItem item = new ListViewItem(MDReader["MEDICATION"].ToString());
                    item.SubItems.Add(MDReader["CHRONIC"].ToString());
                    item.SubItems.Add(MDReader["DATE"].ToString());
                    item.SubItems.Add(MDReader["MED_ID"].ToString());
                    item.SubItems.Add(MDReader["NOTE"].ToString());

                    lvwPHR_MD_PrMDMedications.Items.Add(item);

                    txtPHR_MD_PrMDMedication.Text = "";
                    rtxPHR_MD_PrMDNote.Text = "";
                }
                medicationConnection.Close();
            }
            catch (Exception ex)
            {
                medicationConnection.Close();
                MessageBox.Show(ex.ToString(), "Error in MEDICATION");
                throw ex;
            }
        }

        private void MD_LoadTEST()
        {
            SqlConnection testConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                    "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
            string selectStatement = "SELECT TEST_ID, TEST, RESULT, DATE, NOTE " +
                "FROM TEST_TBL WHERE PATIENT_ID = @PATIENT_ID";
            SqlCommand selectCommand = new SqlCommand(selectStatement, testConnection);
            selectCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
            try
            {
                testConnection.Open();
                SqlDataReader MDReader = selectCommand.ExecuteReader();
                while (MDReader.Read())
                {
                    ListViewItem item = new ListViewItem(MDReader["TEST"].ToString());
                    item.SubItems.Add(MDReader["DATE"].ToString());
                    item.SubItems.Add(MDReader["RESULT"].ToString());
                    item.SubItems.Add(MDReader["TEST_ID"].ToString());
                    item.SubItems.Add(MDReader["NOTE"].ToString());

                    lvwPHR_MD_TRDTestResults.Items.Add(item);

                    txtPHR_MD_TRDTest.Text = "";
                    rtxPHR_MD_TRDNote.Text = "";
                }
                testConnection.Close();
            }
            catch (Exception ex)
            {
                testConnection.Close();
                MessageBox.Show(ex.ToString(), "Error in TEST");
                throw ex;
            }
        }

        private void MD_LoadCONDITION()
        {
            SqlConnection conditionConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                    "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
            string selectStatement = "SELECT CONDITION_ID, CONDITION, ONSET_DATE, SEVERITY, NOTE " +
                "FROM CONDITION WHERE PATIENT_ID = @PATIENT_ID";
            SqlCommand selectCommand = new SqlCommand(selectStatement, conditionConnection);
            selectCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
            try
            {
                conditionConnection.Open();
                SqlDataReader MDReader = selectCommand.ExecuteReader();
                while (MDReader.Read())
                {
                    ListViewItem item = new ListViewItem(MDReader["CONDITION"].ToString());
                    item.SubItems.Add(MDReader["ONSET_DATE"].ToString());
                    if (MDReader["SEVERITY"].ToString() == "True")
                    {
                        item.SubItems.Add("Chronic");
                    }
                    else if (MDReader["SEVERITY"].ToString() == "False") 
                    {
                        item.SubItems.Add("Acute");
                    }
                    item.SubItems.Add(MDReader["CONDITION_ID"].ToString());
                    item.SubItems.Add(MDReader["NOTE"].ToString());

                    lvwPHR_MD_MCDMedicalConditions.Items.Add(item);

                    txtPHR_MD_MCDCondition.Text = "";
                    rtxPHR_MD_MCDNote.Text = "";
                }
                conditionConnection.Close();
            }
            catch (Exception ex)
            {
                conditionConnection.Close();
                MessageBox.Show(ex.ToString(), "Error in CONDITION");
                throw ex;
            }
        }

        private void MD_LoadMED_PROC()
        {
            SqlConnection procedureConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                    "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
            string selectStatement = "SELECT PROCEDURE_ID, MED_PROCEDURE, DATE, DOCTOR, NOTE " +
                "FROM MED_PROC_TBL WHERE PATIENT_ID = @PATIENT_ID";
            SqlCommand selectCommand = new SqlCommand(selectStatement, procedureConnection);
            selectCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
            try
            {
                procedureConnection.Open();
                SqlDataReader MDReader = selectCommand.ExecuteReader();
                while (MDReader.Read())
                {
                    ListViewItem item = new ListViewItem(MDReader["MED_PROCEDURE"].ToString());
                    item.SubItems.Add(MDReader["DATE"].ToString());
                    item.SubItems.Add(MDReader["DOCTOR"].ToString());
                    item.SubItems.Add(MDReader["PROCEDURE_ID"].ToString());
                    item.SubItems.Add(MDReader["NOTE"].ToString());

                    lvwPHR_MD_MPDMedicalProcedures.Items.Add(item);

                    txtPHR_MD_MPDProcedure.Text = "";
                    rtxPHR_MD_MPDNote.Text = "";
                }
                procedureConnection.Close();
            }
            catch (Exception ex)
            {
                procedureConnection.Close();
                MessageBox.Show(ex.ToString(), "Error in MED_PROC");
                throw ex;
            }
        }



        /*
         * modified load methods for the cancel and save link labels of certain groups. these are more specialized
         * and only load data relevant to the target group rather than all data the table has to offer
         */
        private void PD_LoadPersonalDetails()
        {
            SqlConnection patientConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                    "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
            string selectStatement = "SELECT LAST_NAME, FIRST_NAME, DATE_Of_BIRTH, PRIMARY_ID, GENDER, " +
                "TITLE, INITIALS FROM PATIENT_TBL WHERE PATIENT_ID = @PATIENT_ID";
            SqlCommand selectCommand = new SqlCommand(selectStatement, patientConnection);
            selectCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
            try
            {
                patientConnection.Open();
                SqlDataReader PDReader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (PDReader.Read())
                {
                    txtPHR_PD_PDIdentityNumber.Text = PDReader["PRIMARY_ID"].ToString();
                    cmbPHR_PD_PDTitle.Text = PDReader["TITLE"].ToString();
                    txtPHR_PD_PDInitials.Text = PDReader["INITIALS"].ToString();
                    if (PDReader["GENDER"].ToString() == "True")
                    {
                        rdoPHR_PD_PDGenderMale.Checked = true;
                    }
                    else if (PDReader["GENDER"].ToString() == "False")
                    {
                        rdoPHR_PD_PDGenderFemale.Checked = true;
                    }
                    txtPHR_PD_PDLastName.Text = PDReader["LAST_NAME"].ToString();
                    txtPHR_PD_PDFirstName.Text = PDReader["FIRST_NAME"].ToString();
                    dtpPHR_PD_PDDateOfBirth.Value = (DateTime)PDReader["DATE_Of_BIRTH"];
                    patientConnection.Close();
                }
                else
                {
                    MessageBox.Show("Could not read.", "Error in PersonalDetails");
                    patientConnection.Close();
                }
            }
            catch (Exception ex)
            {
                patientConnection.Close();
                MessageBox.Show(ex.ToString(), "Error in PersonalDetails");
                throw ex;
            }
        }

        private void PD_LoadContactDetails()
        {
            SqlConnection patientConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                    "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
            string selectStatement = "SELECT ADDRESS_STREET, ADDRESS_CITY, ADDRESS_STATE, ADDRESS_ZIP, " +
                "PHONE_HOME, PHONE_MOBILE, PHONE_WORK, FAX_NUMBER, EMAIL " +
                "FROM PATIENT_TBL WHERE PATIENT_ID = @PATIENT_ID";
            SqlCommand selectCommand = new SqlCommand(selectStatement, patientConnection);
            selectCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
            try
            {
                patientConnection.Open();
                SqlDataReader PDReader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (PDReader.Read())
                {
                    txtPHR_PD_CDAddress.Text = PDReader["ADDRESS_STREET"].ToString();
                    txtPHR_PD_CDState.Text = PDReader["ADDRESS_STATE"].ToString();
                    txtPHR_PD_CDCity.Text = PDReader["ADDRESS_CITY"].ToString();
                    txtPHR_PD_CDPostalCode.Text = PDReader["ADDRESS_ZIP"].ToString();
                    txtPHR_PD_CDHomeTelephone.Text = PDReader["PHONE_HOME"].ToString();
                    txtPHR_PD_CDMobileTelephone.Text = PDReader["PHONE_MOBILE"].ToString();
                    txtPHR_PD_CDWorkTelephone.Text = PDReader["PHONE_WORK"].ToString();
                    txtPHR_PD_CDFaxNumber.Text = PDReader["FAX_NUMBER"].ToString();
                    txtPHR_PD_CDEmail.Text = PDReader["EMAIL"].ToString();
                    patientConnection.Close();
                }
                else
                {
                    MessageBox.Show("Could not read.", "Error in ContactDetails");
                    patientConnection.Close();
                }
            }
            catch (Exception ex)
            {
                patientConnection.Close();
                MessageBox.Show(ex.ToString(), "Error in ContactDetails");
                throw ex;
            }
        }

        private void PD_LoadEmergencyContactDetails()
        {
            SqlConnection patientConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                    "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
            string selectStatement = "SELECT NEXT_OF_KIN, KIN_RELATIONSHIP, KIN_STREET, KIN_STATE, " +
                "KIN_CITY, KIN_PHONE_HOME, KIN_PHONE_MOBILE, KIN_PHONE_WORK, KIN_ZIP, KIN_FAX_NUMBER, KIN_EMAIL " +
                "FROM PATIENT_TBL WHERE PATIENT_ID = @PATIENT_ID";
            SqlCommand selectCommand = new SqlCommand(selectStatement, patientConnection);
            selectCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
            try
            {
                patientConnection.Open();
                SqlDataReader PDReader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (PDReader.Read())
                {
                    txtPHR_PD_ECDNextOfKin.Text = PDReader["NEXT_OF_KIN"].ToString();
                    txtPHR_PD_ECDRelationship.Text = PDReader["KIN_RELATIONSHIP"].ToString();
                    txtPHR_PD_ECDAddress.Text = PDReader["KIN_STREET"].ToString();
                    txtPHR_PD_ECDState.Text = PDReader["KIN_STATE"].ToString();
                    txtPHR_PD_ECDCity.Text = PDReader["KIN_CITY"].ToString();
                    txtPHR_PD_ECDPostalCode.Text = PDReader["KIN_ZIP"].ToString();
                    txtPHR_PD_ECDHomeTelephone.Text = PDReader["KIN_PHONE_HOME"].ToString();
                    txtPHR_PD_ECDMobileTelephone.Text = PDReader["KIN_PHONE_MOBILE"].ToString();
                    txtPHR_PD_ECDWorkTelephone.Text = PDReader["KIN_PHONE_WORK"].ToString();
                    txtPHR_PD_ECDFaxNumber.Text = PDReader["KIN_FAX_NUMBER"].ToString();
                    txtPHR_PD_ECDEmail.Text = PDReader["KIN_EMAIL"].ToString();
                    patientConnection.Close();
                }
                else
                {
                    MessageBox.Show("Could not read.", "Error in EmergencyContactDetails");
                    patientConnection.Close();
                }
            }
            catch (Exception ex)
            {
                patientConnection.Close();
                MessageBox.Show(ex.ToString(), "Error in EmergencyContactDetails");
                throw ex;
            }
        }

        private void PD_LoadHealthInsuranceDetails()
        {
            SqlConnection patientConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                    "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
            string selectStatement = "SELECT INSURER, INSURANCE_PLAN, INSURANCE_NUMBER " +
                "FROM PATIENT_TBL WHERE PATIENT_ID = @PATIENT_ID";
            SqlCommand selectCommand = new SqlCommand(selectStatement, patientConnection);
            selectCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
            try
            {
                patientConnection.Open();
                SqlDataReader PDReader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (PDReader.Read())
                {
                    txtPHR_PD_HIDInsurer.Text = PDReader["INSURER"].ToString();
                    txtPHR_PD_HIDInsurancePlan.Text = PDReader["INSURANCE_PLAN"].ToString();
                    txtPHR_PD_HIDInsuranceNumber.Text = PDReader["INSURANCE_NUMBER"].ToString();
                    patientConnection.Close();
                }
                else
                {
                    MessageBox.Show("Could not read.", "Error in HealthInsuranceDetails");
                    patientConnection.Close();
                }
            }
            catch (Exception ex)
            {
                patientConnection.Close();
                MessageBox.Show(ex.ToString(), "Error in HealthInsuranceDetails");
                throw ex;
            }
        }



        /*
         * method that simply opens a file dialog and allows the user to choose a profile picture for the current user profile.
         * when a picture is chosen, it is saved into the corresponding row in the PATIENT_TBL
         */
        private void llbPHR_PDChangeProfilePicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog chooseProfilePicture = new OpenFileDialog();
            chooseProfilePicture.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (chooseProfilePicture.ShowDialog() == DialogResult.OK) 
            {
                Bitmap profilePicture = new Bitmap(chooseProfilePicture.FileName);
                picPHR_PDProfilePicture.Image = profilePicture;
                byte[] pictureBytes;
                FileStream fs = new FileStream(chooseProfilePicture.FileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                pictureBytes = br.ReadBytes((int)fs.Length);
                fs.Close();
                br.Close();

                try
                {
                    SqlConnection updateConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                    "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
                    string updateString = "UPDATE PATIENT_TBL SET " +
                        "PROFILE_PIC = @PROFILE_PIC " +
                        "WHERE PATIENT_ID = @PATIENT_ID";
                    SqlCommand updateCommand = new SqlCommand(updateString, updateConnection);
                    updateCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
                    updateCommand.Parameters.AddWithValue("@PROFILE_PIC", pictureBytes);
                    updateConnection.Open();
                    updateCommand.ExecuteNonQuery();
                    updateConnection.Close();
                }
                catch (Exception ex) 
                {
                    MessageBox.Show(ex.ToString(), "");
                }
            }
        }



        /*
         * methods that allow detailed viewing of the items in each list view control on the medical details tab. when the
         * user double clicks item, they're shown a message box with all of the details displayed more cleanly than the
         * dimensions of the list view allows
         */
        private void lvwPHR_MD_ADAllergies_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show(("Onset Date: " + lvwPHR_MD_ADAllergies.SelectedItems[0].SubItems[1].Text 
                + "\nAllergy ID: " + lvwPHR_MD_ADAllergies.SelectedItems[0].SubItems[2].Text
                + "\nNote: " + lvwPHR_MD_ADAllergies.SelectedItems[0].SubItems[3].Text), 
                ("Allergen: " + lvwPHR_MD_ADAllergies.SelectedItems[0].SubItems[0].Text));
        }

        private void lvwPHR_MD_IDImmunizations_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show(("Date: " + lvwPHR_MD_IDImmunizations.SelectedItems[0].SubItems[1].Text
                + "\nImmunization ID: " + lvwPHR_MD_IDImmunizations.SelectedItems[0].SubItems[2].Text
                + "\nNote: " + lvwPHR_MD_IDImmunizations.SelectedItems[0].SubItems[3].Text),
                ("Immunization: " + lvwPHR_MD_IDImmunizations.SelectedItems[0].SubItems[0].Text));
        }

        private void lvwPHR_MD_PrMDMedications_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show(("Chronic: " + lvwPHR_MD_PrMDMedications.SelectedItems[0].SubItems[1].Text
                + "\nPrescribed on: " + lvwPHR_MD_PrMDMedications.SelectedItems[0].SubItems[2].Text
                + "\nMedication ID: " + lvwPHR_MD_PrMDMedications.SelectedItems[0].SubItems[3].Text
                + "\nNote: " + lvwPHR_MD_PrMDMedications.SelectedItems[0].SubItems[4].Text),
                ("Medication: " + lvwPHR_MD_PrMDMedications.SelectedItems[0].SubItems[0].Text));
        }

        private void lvwPHR_MD_TRDTestResults_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show(("Date: " + lvwPHR_MD_TRDTestResults.SelectedItems[0].SubItems[1].Text
                + "\nResult: " + lvwPHR_MD_TRDTestResults.SelectedItems[0].SubItems[2].Text
                + "\nTest ID: " + lvwPHR_MD_TRDTestResults.SelectedItems[0].SubItems[3].Text
                + "\nNote: " + lvwPHR_MD_TRDTestResults.SelectedItems[0].SubItems[4].Text),
                ("Test: " + lvwPHR_MD_TRDTestResults.SelectedItems[0].SubItems[0].Text));
        }

        private void lvwPHR_MD_MCDMedicalConditions_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show(("Onset: " + lvwPHR_MD_MCDMedicalConditions.SelectedItems[0].SubItems[1].Text
                + "\nSeverity: " + lvwPHR_MD_MCDMedicalConditions.SelectedItems[0].SubItems[2].Text
                + "\nCondition ID: " + lvwPHR_MD_MCDMedicalConditions.SelectedItems[0].SubItems[3].Text
                + "\nNote: " + lvwPHR_MD_MCDMedicalConditions.SelectedItems[0].SubItems[4].Text),
                ("Condition: " + lvwPHR_MD_MCDMedicalConditions.SelectedItems[0].SubItems[0].Text));
        }

        private void lvwPHR_MD_MPDMedicalProcedures_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show(("Date: " + lvwPHR_MD_MPDMedicalProcedures.SelectedItems[0].SubItems[1].Text
                + "\nPerformed By: " + lvwPHR_MD_MPDMedicalProcedures.SelectedItems[0].SubItems[2].Text
                + "\nProcedure ID: " + lvwPHR_MD_MPDMedicalProcedures.SelectedItems[0].SubItems[3].Text
                + "\nNote: " + lvwPHR_MD_MPDMedicalProcedures.SelectedItems[0].SubItems[4].Text),
                ("Procedure: " + lvwPHR_MD_MPDMedicalProcedures.SelectedItems[0].SubItems[0].Text));
        }



        /*
         * methods that respond to changes in the change password group of the personal details tab.
         * meeting all requirements of the group allows the user to permanently change their password.
         * these methods are different enough from the rest of the personal details tab control methods
         * to warrant unique and more detailed documentation
         */
        // method that changes the password should all validation requirements be met
        private void llbPHR_PD_LDChangePassword_Click(object sender, EventArgs e)
        {
            if ((txtPHR_PD_LD_CPOldPassword.Text != "") && (txtPHR_PD_LD_CPNewPassword.Text != "")
                && (txtPHR_PD_LD_CPConfirmNewPassword.Text != ""))
            {
                if ((txtPHR_PD_LD_CPOldPassword.Text == password)
                    && (txtPHR_PD_LD_CPNewPassword.Text == txtPHR_PD_LD_CPConfirmNewPassword.Text))
                {
                    SqlConnection updateConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                    "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
                    string updateStatement = "UPDATE PATIENT_TBL SET " +
                        "PASSWORD = @PASSWORD " +
                        "WHERE PATIENT_ID = @PATIENT_ID"; ;
                    SqlCommand updateCommand = new SqlCommand(updateStatement, updateConnection);
                    updateCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
                    updateCommand.Parameters.AddWithValue("@PASSWORD", txtPHR_PD_LD_CPNewPassword.Text);
                    try
                    {
                        updateConnection.Open();
                        updateCommand.ExecuteNonQuery();
                        updateConnection.Close();
                        MessageBox.Show("Your password has been changed successfully!", "Success");
                    }
                    catch (SqlException ex) 
                    {
                        updateConnection.Close();
                        MessageBox.Show(ex.ToString());
                        throw ex;
                    }
                }
                else 
                {
                    MessageBox.Show("Oops, looks like you wanted to change your password, but either your old " +
                        "password is incorrect or the new password fields don't match!", "Password Error");
                }
            }
            else 
            {
                MessageBox.Show("Oops, looks like you wanted to change your password, but one " +
                    "of the \nfields is empty! In order to change your password, all fields must be filled.", "Password Error");
            }
        }

        // method that empties the password fields of the change password group should the cancel button be pressed
        private void llbPHR_PD_LDCancel_Click(object sender, EventArgs e) 
        {
            if (llbPHR_PD_LD_CPCancel.Enabled == true) 
            {
                txtPHR_PD_LD_CPOldPassword.Text = "";
                txtPHR_PD_LD_CPNewPassword.Text = "";
                txtPHR_PD_LD_CPConfirmNewPassword.Text = "";
                llbPHR_PD_LD_CPCancel.Enabled = false;
            }
        }

        // method that enables the cancel button of the change password group should any of the text boxes not be empty, and disables if the opposite is true
        private void txtPHR_PD_LD_CPTextChanged(object sender, EventArgs e)
        {
            if ((txtPHR_PD_LD_CPOldPassword.Text == "") || (txtPHR_PD_LD_CPNewPassword.Text == "")
                || (txtPHR_PD_LD_CPConfirmNewPassword.Text == ""))
            {
                llbPHR_PD_LD_CPCancel.Enabled = false;
                llbPHR_PD_LD_CPChangePassword.Enabled = false;
            }
            else
            {
                llbPHR_PD_LD_CPCancel.Enabled = true;
                llbPHR_PD_LD_CPChangePassword.Enabled = true;
            }
        }



        /*
         * methods that implement the editing, saving and restoring of the patient's data in various groups of the personal details tab.
         * 
         * the edit methods enable editing of the fields in each group related to the patient's data as well as the 'save' and 'cancel'
         * link labels. they also disable the 'edit' link label
         * 
         * the save methods create an Sql connection and update the corresponding table with the information in the relevant group's fields,
         * before disabling editing of the fields, the cancel link label, the save link label, and enabling the edit link label
         * 
         * the cancel link labels reload the relevant group with the corresponding load method, whether general or specialized, before disabling
         * editing of the fields, the save link label, the cancel link label, and enabling the edit link label. reloading the group allows the
         * previous data to be restored and preserved
         */
         // personal details group methods
        private void llbPHR_PD_PDEdit_Click(object sender, EventArgs e)
        {
            if (llbPHR_PD_PDEdit.Enabled == true) 
            {
                txtPHR_PD_PDIdentityNumber.ReadOnly = false;
                txtPHR_PD_PDFirstName.ReadOnly = false;
                txtPHR_PD_PDLastName.ReadOnly = false;
                txtPHR_PD_PDInitials.ReadOnly = false;
                cmbPHR_PD_PDTitle.Enabled = true;
                rdoPHR_PD_PDGenderMale.Enabled = true;
                rdoPHR_PD_PDGenderFemale.Enabled = true;
                dtpPHR_PD_PDDateOfBirth.Enabled = true;
                llbPHR_PD_PDEdit.Enabled = false;
                llbPHR_PD_PDSave.Enabled = true;
                llbPHR_PD_PDCancel.Enabled = true;
            }
        }

        private void llbPHR_PD_PDSave_Click(object sender, EventArgs e)
        {
            if (llbPHR_PD_PDSave.Enabled == true) 
            {
                SqlConnection updateConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
                string updateStatement = "UPDATE PATIENT_TBL SET " +
                    "PRIMARY_ID = @PRIMARY_ID, " +
                    "TITLE = @TITLE, " +
                    "INITIALS = @INITIALS, " +
                    "GENDER = @GENDER, " +
                    "LAST_NAME = @LAST_NAME, " +
                    "FIRST_NAME = @FIRST_NAME, " +
                    "DATE_Of_BIRTH = @DATE_Of_BIRTH " +
                    "WHERE PATIENT_ID = @PATIENT_ID";
                SqlCommand updateCommand = new SqlCommand(updateStatement, updateConnection);
                updateCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
                updateCommand.Parameters.AddWithValue("@PRIMARY_ID", txtPHR_PD_PDIdentityNumber.Text);
                updateCommand.Parameters.AddWithValue("@TITLE", cmbPHR_PD_PDTitle.Text);
                updateCommand.Parameters.AddWithValue("@INITIALS", txtPHR_PD_PDInitials.Text);
                if (rdoPHR_PD_PDGenderMale.Checked == true)
                {
                    updateCommand.Parameters.AddWithValue("@GENDER", "True");
                }
                else if (rdoPHR_PD_PDGenderFemale.Checked == true)
                {
                    updateCommand.Parameters.AddWithValue("@GENDER", "False");
                }
                updateCommand.Parameters.AddWithValue("@LAST_NAME", txtPHR_PD_PDLastName.Text);
                updateCommand.Parameters.AddWithValue("@FIRST_NAME", txtPHR_PD_PDFirstName.Text);
                updateCommand.Parameters.AddWithValue("@DATE_Of_BIRTH", dtpPHR_PD_PDDateOfBirth.Value);

                try
                {
                    updateConnection.Open();
                    updateCommand.ExecuteNonQuery();
                    updateConnection.Close();
                    PD_LoadPersonalDetails();
                }
                catch (SqlException ex)
                {
                    updateConnection.Close();
                    MessageBox.Show(ex.ToString());
                    throw ex;
                }
                txtPHR_PD_PDIdentityNumber.ReadOnly = true;
                txtPHR_PD_PDFirstName.ReadOnly = true;
                txtPHR_PD_PDLastName.ReadOnly = true;
                txtPHR_PD_PDInitials.ReadOnly = true;
                cmbPHR_PD_PDTitle.Enabled = false;
                rdoPHR_PD_PDGenderMale.Enabled = false;
                rdoPHR_PD_PDGenderFemale.Enabled = false;
                dtpPHR_PD_PDDateOfBirth.Enabled = false;
                llbPHR_PD_PDEdit.Enabled = true;
                llbPHR_PD_PDSave.Enabled = false;
                llbPHR_PD_PDCancel.Enabled = false;
            }
        }

        private void llbPHR_PD_PDCancel_Click(object sender, EventArgs e)
        {
            if (llbPHR_PD_PDCancel.Enabled == true) 
            {
                PD_LoadPersonalDetails();
                txtPHR_PD_PDIdentityNumber.ReadOnly = true;
                txtPHR_PD_PDFirstName.ReadOnly = true;
                txtPHR_PD_PDLastName.ReadOnly = true;
                txtPHR_PD_PDInitials.ReadOnly = true;
                cmbPHR_PD_PDTitle.Enabled = false;
                rdoPHR_PD_PDGenderMale.Enabled = false;
                rdoPHR_PD_PDGenderFemale.Enabled = false;
                dtpPHR_PD_PDDateOfBirth.Enabled = false;
                llbPHR_PD_PDEdit.Enabled = true;
                llbPHR_PD_PDSave.Enabled = false;
                llbPHR_PD_PDCancel.Enabled = false;
            }
        }

        // contact details group methods
        private void llbPHR_PD_CDEdit_Click(object sender, EventArgs e)
        {
            if (llbPHR_PD_CDEdit.Enabled == true) 
            {
                txtPHR_PD_CDAddress.ReadOnly = false;
                txtPHR_PD_CDState.ReadOnly = false;
                txtPHR_PD_CDCity.ReadOnly = false;
                txtPHR_PD_CDPostalCode.ReadOnly = false;
                txtPHR_PD_CDHomeTelephone.ReadOnly = false;
                txtPHR_PD_CDMobileTelephone.ReadOnly = false;
                txtPHR_PD_CDWorkTelephone.ReadOnly = false;
                txtPHR_PD_CDFaxNumber.ReadOnly = false;
                txtPHR_PD_CDEmail.ReadOnly = false;
                llbPHR_PD_CDEdit.Enabled = false;
                llbPHR_PD_CDSave.Enabled = true;
                llbPHR_PD_CDCancel.Enabled = true;
            }
        }

        private void llbPHR_PD_CDSave_Click(object sender, EventArgs e)
        {
            if (llbPHR_PD_CDSave.Enabled == true) 
            {
                SqlConnection updateConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
                string updateStatement = "UPDATE PATIENT_TBL SET " +
                    "ADDRESS_STREET = @ADDRESS_STREET, " +
                    "ADDRESS_STATE = @ADDRESS_STATE, " +
                    "ADDRESS_CITY = @ADDRESS_CITY, " +
                    "ADDRESS_ZIP = @ADDRESS_ZIP, " +
                    "PHONE_HOME = @PHONE_HOME, " +
                    "PHONE_MOBILE = @PHONE_MOBILE, " +
                    "PHONE_WORK = @PHONE_WORK, " +
                    "FAX_NUMBER = @FAX_NUMBER, " +
                    "EMAIL = @EMAIL " +
                    "WHERE PATIENT_ID = @PATIENT_ID";
                SqlCommand updateCommand = new SqlCommand(updateStatement, updateConnection);
                updateCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
                updateCommand.Parameters.AddWithValue("@ADDRESS_STREET", txtPHR_PD_CDAddress.Text);
                updateCommand.Parameters.AddWithValue("@ADDRESS_STATE", txtPHR_PD_CDState.Text);
                updateCommand.Parameters.AddWithValue("@ADDRESS_CITY", txtPHR_PD_CDCity.Text);
                updateCommand.Parameters.AddWithValue("@ADDRESS_ZIP", txtPHR_PD_CDPostalCode.Text);
                updateCommand.Parameters.AddWithValue("@PHONE_HOME", txtPHR_PD_CDHomeTelephone.Text);
                updateCommand.Parameters.AddWithValue("@PHONE_MOBILE", txtPHR_PD_CDMobileTelephone.Text);
                updateCommand.Parameters.AddWithValue("@PHONE_WORK", txtPHR_PD_CDWorkTelephone.Text);
                updateCommand.Parameters.AddWithValue("@FAX_NUMBER", txtPHR_PD_CDFaxNumber.Text);
                updateCommand.Parameters.AddWithValue("@EMAIL", txtPHR_PD_CDEmail.Text);

                try
                {
                    updateConnection.Open();
                    updateCommand.ExecuteNonQuery();
                    updateConnection.Close();
                    PD_LoadContactDetails();
                }
                catch (SqlException ex)
                {
                    updateConnection.Close();
                    MessageBox.Show(ex.ToString());
                    throw ex;
                }
                txtPHR_PD_CDAddress.ReadOnly = true;
                txtPHR_PD_CDState.ReadOnly = true;
                txtPHR_PD_CDCity.ReadOnly = true;
                txtPHR_PD_CDPostalCode.ReadOnly = true;
                txtPHR_PD_CDHomeTelephone.ReadOnly = true;
                txtPHR_PD_CDMobileTelephone.ReadOnly = true;
                txtPHR_PD_CDWorkTelephone.ReadOnly = true;
                txtPHR_PD_CDFaxNumber.ReadOnly = true;
                txtPHR_PD_CDEmail.ReadOnly = true;
                llbPHR_PD_CDEdit.Enabled = true;
                llbPHR_PD_CDSave.Enabled = false;
                llbPHR_PD_CDCancel.Enabled = false;
            }
        }

        private void llbPHR_PD_CDCancel_Click(object sender, EventArgs e)
        {
            if (llbPHR_PD_CDCancel.Enabled == true)
            {
                PD_LoadContactDetails();
                txtPHR_PD_CDAddress.ReadOnly = true;
                txtPHR_PD_CDState.ReadOnly = true;
                txtPHR_PD_CDCity.ReadOnly = true;
                txtPHR_PD_CDPostalCode.ReadOnly = true;
                txtPHR_PD_CDHomeTelephone.ReadOnly = true;
                txtPHR_PD_CDMobileTelephone.ReadOnly = true;
                txtPHR_PD_CDWorkTelephone.ReadOnly = true;
                txtPHR_PD_CDFaxNumber.ReadOnly = true;
                txtPHR_PD_CDEmail.ReadOnly = true;
                llbPHR_PD_CDEdit.Enabled = true;
                llbPHR_PD_CDSave.Enabled = false;
                llbPHR_PD_CDCancel.Enabled = false;
            }
        }

        // emergency contact details group methods
        private void llbPHR_PD_ECDEdit_Click(object sender, EventArgs e)
        {
            if (llbPHR_PD_ECDEdit.Enabled == true) 
            {
                txtPHR_PD_ECDNextOfKin.ReadOnly = false;
                txtPHR_PD_ECDRelationship.ReadOnly = false;
                txtPHR_PD_ECDAddress.ReadOnly = false;
                txtPHR_PD_ECDState.ReadOnly = false;
                txtPHR_PD_ECDCity.ReadOnly = false;
                txtPHR_PD_ECDPostalCode.ReadOnly = false;
                txtPHR_PD_ECDHomeTelephone.ReadOnly = false;
                txtPHR_PD_ECDMobileTelephone.ReadOnly = false;
                txtPHR_PD_ECDWorkTelephone.ReadOnly = false;
                txtPHR_PD_ECDFaxNumber.ReadOnly = false;
                txtPHR_PD_ECDEmail.ReadOnly = false;
                llbPHR_PD_ECDEdit.Enabled = false;
                llbPHR_PD_ECDSave.Enabled = true;
                llbPHR_PD_ECDCancel.Enabled = true;
            }
        }

        private void llbPHR_PD_ECDSave_Click(object sender, EventArgs e)
        {
            if (llbPHR_PD_ECDSave.Enabled == true) 
            {
                SqlConnection updateConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
                string updateStatement = "UPDATE PATIENT_TBL SET " +
                    "NEXT_OF_KIN = @NEXT_OF_KIN, " +
                    "KIN_RELATIONSHIP = @KIN_RELATIONSHIP, " +
                    "KIN_STREET = @KIN_STREET, " +
                    "KIN_STATE = @KIN_STATE, " +
                    "KIN_CITY = @KIN_CITY, " +
                    "KIN_ZIP = @KIN_ZIP, " +
                    "KIN_PHONE_HOME = @KIN_PHONE_HOME, " +
                    "KIN_PHONE_MOBILE = @KIN_PHONE_MOBILE, " +
                    "KIN_PHONE_WORK = @KIN_PHONE_WORK, " +
                    "KIN_FAX_NUMBER = @KIN_FAX_NUMBER," +
                    "KIN_EMAIL = @KIN_EMAIL " +
                    "WHERE PATIENT_ID = @PATIENT_ID";
                SqlCommand updateCommand = new SqlCommand(updateStatement, updateConnection);
                updateCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
                updateCommand.Parameters.AddWithValue("@NEXT_OF_KIN", txtPHR_PD_ECDNextOfKin.Text);
                updateCommand.Parameters.AddWithValue("@KIN_RELATIONSHIP", txtPHR_PD_ECDRelationship.Text);
                updateCommand.Parameters.AddWithValue("@KIN_STREET", txtPHR_PD_ECDAddress.Text);
                updateCommand.Parameters.AddWithValue("@KIN_STATE", txtPHR_PD_ECDState.Text);
                updateCommand.Parameters.AddWithValue("@KIN_CITY", txtPHR_PD_ECDCity.Text);
                updateCommand.Parameters.AddWithValue("@KIN_ZIP", txtPHR_PD_ECDPostalCode.Text);
                updateCommand.Parameters.AddWithValue("@KIN_PHONE_HOME", txtPHR_PD_ECDHomeTelephone.Text);
                updateCommand.Parameters.AddWithValue("@KIN_PHONE_MOBILE", txtPHR_PD_ECDMobileTelephone.Text);
                updateCommand.Parameters.AddWithValue("@KIN_PHONE_WORK", txtPHR_PD_ECDWorkTelephone.Text);
                updateCommand.Parameters.AddWithValue("@KIN_FAX_NUMBER", txtPHR_PD_ECDFaxNumber.Text);
                updateCommand.Parameters.AddWithValue("@KIN_EMAIL", txtPHR_PD_ECDEmail.Text);

                try
                {
                    updateConnection.Open();
                    updateCommand.ExecuteNonQuery();
                    updateConnection.Close();
                    PD_LoadEmergencyContactDetails();
                    llbPHR_PD_ECDEdit.Enabled = true;
                    llbPHR_PD_ECDSave.Enabled = false;
                    llbPHR_PD_ECDCancel.Enabled = false;
                }
                catch (SqlException ex)
                {
                    updateConnection.Close();
                    MessageBox.Show(ex.ToString());
                    throw ex;
                }
                txtPHR_PD_ECDNextOfKin.ReadOnly = true;
                txtPHR_PD_ECDRelationship.ReadOnly = true;
                txtPHR_PD_ECDAddress.ReadOnly = true;
                txtPHR_PD_ECDState.ReadOnly = true;
                txtPHR_PD_ECDCity.ReadOnly = true;
                txtPHR_PD_ECDPostalCode.ReadOnly = true;
                txtPHR_PD_ECDHomeTelephone.ReadOnly = true;
                txtPHR_PD_ECDMobileTelephone.ReadOnly = true;
                txtPHR_PD_ECDWorkTelephone.ReadOnly = true;
                txtPHR_PD_ECDFaxNumber.ReadOnly = true;
                txtPHR_PD_ECDEmail.ReadOnly = true;
                llbPHR_PD_ECDEdit.Enabled = true;
                llbPHR_PD_ECDSave.Enabled = false;
                llbPHR_PD_ECDCancel.Enabled = false;
            }
        }

        private void llbPHR_PD_ECDCancel_Click(object sender, EventArgs e)
        {
            if (llbPHR_PD_ECDCancel.Enabled == true)
            {
                PD_LoadEmergencyContactDetails();
                txtPHR_PD_ECDNextOfKin.ReadOnly = true;
                txtPHR_PD_ECDRelationship.ReadOnly = true;
                txtPHR_PD_ECDAddress.ReadOnly = true;
                txtPHR_PD_ECDState.ReadOnly = true;
                txtPHR_PD_ECDCity.ReadOnly = true;
                txtPHR_PD_ECDPostalCode.ReadOnly = true;
                txtPHR_PD_ECDHomeTelephone.ReadOnly = true;
                txtPHR_PD_ECDMobileTelephone.ReadOnly = true;
                txtPHR_PD_ECDWorkTelephone.ReadOnly = true;
                txtPHR_PD_ECDFaxNumber.ReadOnly = true;
                txtPHR_PD_ECDEmail.ReadOnly = true;
                llbPHR_PD_ECDEdit.Enabled = true;
                llbPHR_PD_ECDSave.Enabled = false;
                llbPHR_PD_ECDCancel.Enabled = false;
            }
        }

        // primary care provider group methods
        private void llbPHR_PD_PCPEdit_Click(object sender, EventArgs e)
        {
            if (llbPHR_PD_PCPEdit.Enabled == true) 
            {
                txtPHR_PD_PCPFullName.ReadOnly = false;
                txtPHR_PD_PCPSpecialty.ReadOnly = false;
                txtPHR_PD_PCPMobileTelephone.ReadOnly = false;
                txtPHR_PD_PCPWorkTelephone.ReadOnly = false;
                txtPHR_PD_PCPFaxNumber.ReadOnly = false;
                txtPHR_PD_PCPEmail.ReadOnly = false;
                llbPHR_PD_PCPEdit.Enabled = false;
                llbPHR_PD_PCPSave.Enabled = true;
                llbPHR_PD_PCPCancel.Enabled = true;
            }
        }

        private void llbPHR_PD_PCPSave_Click(object sender, EventArgs e)
        {
            if (llbPHR_PD_PCPSave.Enabled == true) 
            {
                SqlConnection updateConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
                string updateStatement = "UPDATE PRIMARY_CARE_TBL SET " +
                    "FULL_NAME = @FULL_NAME, " +
                    "SPECIALTY = @SPECIALTY, " +
                    "PHONE_MOBILE = @PHONE_MOBILE, " +
                    "PHONE_OFFICE = @PHONE_OFFICE, " +
                    "FAX_NUMBER = @FAX_NUMBER, " +
                    "EMAIL = @EMAIL " +
                    "WHERE PRIMARY_ID = @PRIMARY_ID";
                SqlCommand updateCommand = new SqlCommand(updateStatement, updateConnection);
                updateCommand.Parameters.AddWithValue("@PRIMARY_ID", primaryID);
                updateCommand.Parameters.AddWithValue("@FULL_NAME", txtPHR_PD_PCPFullName.Text);
                updateCommand.Parameters.AddWithValue("@SPECIALTY", txtPHR_PD_PCPSpecialty.Text);
                updateCommand.Parameters.AddWithValue("@PHONE_MOBILE", txtPHR_PD_PCPMobileTelephone.Text);
                updateCommand.Parameters.AddWithValue("@PHONE_OFFICE", txtPHR_PD_PCPWorkTelephone.Text);
                updateCommand.Parameters.AddWithValue("@FAX_NUMBER", txtPHR_PD_PCPFaxNumber.Text);
                updateCommand.Parameters.AddWithValue("@EMAIL", txtPHR_PD_PCPEmail.Text);

                try
                {
                    updateConnection.Open();
                    updateCommand.ExecuteNonQuery();
                    updateConnection.Close();
                    PD_LoadPRIMARY_CARE();
                }
                catch (SqlException ex)
                {
                    updateConnection.Close();
                    MessageBox.Show(ex.ToString());
                    throw ex;
                }
            }
            txtPHR_PD_PCPFullName.ReadOnly = true;
            txtPHR_PD_PCPSpecialty.ReadOnly = true;
            txtPHR_PD_PCPMobileTelephone.ReadOnly = true;
            txtPHR_PD_PCPWorkTelephone.ReadOnly = true;
            txtPHR_PD_PCPFaxNumber.ReadOnly = true;
            txtPHR_PD_PCPEmail.ReadOnly = true;
            llbPHR_PD_PCPEdit.Enabled = true;
            llbPHR_PD_PCPSave.Enabled = false;
            llbPHR_PD_PCPCancel.Enabled = false;
        }

        private void llbPHR_PD_PCPCancel_Click(object sender, EventArgs e)
        {
            if (llbPHR_PD_PCPCancel.Enabled == true)
            {
                PD_LoadPRIMARY_CARE();
                txtPHR_PD_PCPFullName.ReadOnly = true;
                txtPHR_PD_PCPSpecialty.ReadOnly = true;
                txtPHR_PD_PCPMobileTelephone.ReadOnly = true;
                txtPHR_PD_PCPWorkTelephone.ReadOnly = true;
                txtPHR_PD_PCPFaxNumber.ReadOnly = true;
                txtPHR_PD_PCPEmail.ReadOnly = true;
                llbPHR_PD_PCPEdit.Enabled = true;
                llbPHR_PD_PCPSave.Enabled = false;
                llbPHR_PD_PCPCancel.Enabled = false;
            }
        }

        // health insurance details group methods
        private void llbPHR_PD_HIDEdit_Click(object sender, EventArgs e)
        {
            if (llbPHR_PD_HIDEdit.Enabled == true) 
            {
                txtPHR_PD_HIDInsurer.ReadOnly = false;
                txtPHR_PD_HIDInsurancePlan.ReadOnly = false;
                txtPHR_PD_HIDInsuranceNumber.ReadOnly = false;
                llbPHR_PD_HIDEdit.Enabled = false;
                llbPHR_PD_HIDSave.Enabled = true;
                llbPHR_PD_HIDCancel.Enabled = true;
            }
        }

        private void llbPHR_PD_HIDSave_Click(object sender, EventArgs e)
        {
            if (llbPHR_PD_HIDSave.Enabled == true) 
            {
                SqlConnection updateConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
                string updateStatement = "UPDATE PATIENT_TBL SET " +
                    "INSURER = @INSURER, " +
                    "INSURANCE_PLAN = @INSURANCE_PLAN, " +
                    "INSURANCE_NUMBER = @INSURANCE_NUMBER " +
                    "WHERE PATIENT_ID = @PATIENT_ID";
                SqlCommand updateCommand = new SqlCommand(updateStatement, updateConnection);
                updateCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
                updateCommand.Parameters.AddWithValue("@INSURER", txtPHR_PD_HIDInsurer.Text);
                updateCommand.Parameters.AddWithValue("@INSURANCE_PLAN", txtPHR_PD_HIDInsurancePlan.Text);
                updateCommand.Parameters.AddWithValue("@INSURANCE_NUMBER", txtPHR_PD_HIDInsuranceNumber.Text);

                try
                {
                    updateConnection.Open();
                    updateCommand.ExecuteNonQuery();
                    updateConnection.Close();
                    PD_LoadHealthInsuranceDetails();
                }
                catch (SqlException ex)
                {
                    updateConnection.Close();
                    MessageBox.Show(ex.ToString());
                    throw ex;
                }
            }
            txtPHR_PD_HIDInsurer.ReadOnly = true;
            txtPHR_PD_HIDInsurancePlan.ReadOnly = true;
            txtPHR_PD_HIDInsuranceNumber.ReadOnly = true;
            llbPHR_PD_HIDEdit.Enabled = true;
            llbPHR_PD_HIDSave.Enabled = false;
            llbPHR_PD_HIDCancel.Enabled = false;
        }

        private void llbPHR_PD_HIDCancel_Click(object sender, EventArgs e)
        {
            if (llbPHR_PD_HIDCancel.Enabled == true)
            {
                PD_LoadHealthInsuranceDetails();
                txtPHR_PD_HIDInsurer.ReadOnly = true;
                txtPHR_PD_HIDInsurancePlan.ReadOnly = true;
                txtPHR_PD_HIDInsuranceNumber.ReadOnly = true;
                llbPHR_PD_HIDEdit.Enabled = true;
                llbPHR_PD_HIDSave.Enabled = false;
                llbPHR_PD_HIDCancel.Enabled = false;
            }
        }



        /*
         * methods that implement the editing, saving and restoring of the patient's data in various groups of the medical details tab.
         * 
         * the edit methods enable editing of the fields in each group related to the patient's data as well as the 'save' and 'cancel'
         * link labels. they also disable the 'edit' link label. the edit methods in the medical details tab also enable the selection of
         * the 'add' and 'remove selected' link labels in each group should certain conditions be met
         * 
         * the save methods create an Sql connection and update the corresponding table with the information in the relevant group's fields,
         * before disabling editing of the fields, the cancel link label, the save link label, and enabling the edit link label. the save methods
         * of the medical details tab differ from those of the personal details tab in that they also clear the fields related to adding new list
         * view items as well as employ a special process for saving the list view items in each group. all entries tied to the user's patient id
         * in the table that corresponds to the group are wiped, before each item from the group's list view is added as a new row to the table
         * 
         * the cancel link labels reload the relevant group with the corresponding load method, whether general or specialized, before disabling
         * editing of the fields, the save link label, the cancel link label, and enabling the edit link label. reloading the group allows the
         * previous data to be restored and preserved
         */
         // personal medical details group details
        private void llbPHR_MD_PeMDEdit_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_PeMDEdit.Enabled == true) 
            {
                cmbPHR_MD_PeMDBloodGroup.Enabled = true;
                chkPHR_MD_PeMDOrganDonor.Enabled = true;
                rdoPHR_MD_PeMD_HSNegative.Enabled = true;
                rdoPHR_MD_PeMD_HSPositive.Enabled = true;
                rdoPHR_MD_PeMD_HSUnknown.Enabled = true;
                txtPHR_MD_PeMDHeight.ReadOnly = false;
                txtPHR_MD_PeMDWeight.ReadOnly = false;
                llbPHR_MD_PeMDEdit.Enabled = false;
                llbPHR_MD_PeMDSave.Enabled = true;
                llbPHR_MD_PeMDCancel.Enabled = true;
            }
        }

        private void llbPHR_MD_PeMDSave_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_PeMDSave.Enabled == true)
            {
                SqlConnection updateConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
                string updateStatement = "UPDATE PER_DETAILS_TBL SET " +
                    "BLOOD_TYPE = @BLOOD_TYPE, " +
                    "ORGAN_DONOR = @ORGAN_DONOR, " +
                    "HIV_STATUS = @HIV_STATUS," +
                    "HEIGHT_INCHES = @HEIGHT_INCHES," +
                    "WEIGHT_LBS = @WEIGHT_LBS " +
                    "WHERE PATIENT_ID = @PATIENT_ID";
                SqlCommand updateCommand = new SqlCommand(updateStatement, updateConnection);
                updateCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
                updateCommand.Parameters.AddWithValue("@BLOOD_TYPE", cmbPHR_MD_PeMDBloodGroup.Text);
                updateCommand.Parameters.AddWithValue("@ORGAN_DONOR", chkPHR_MD_PeMDOrganDonor.Checked);
                if (rdoPHR_MD_PeMD_HSPositive.Checked)
                {
                    updateCommand.Parameters.AddWithValue("@HIV_STATUS", "True");
                }
                else if (rdoPHR_MD_PeMD_HSNegative.Checked)
                {
                    updateCommand.Parameters.AddWithValue("@HIV_STATUS", "False");
                }
                else if (rdoPHR_MD_PeMD_HSUnknown.Checked)
                {
                    updateCommand.Parameters.AddWithValue("@HIV_STATUS", DBNull.Value);
                }
                updateCommand.Parameters.AddWithValue("@HEIGHT_INCHES", txtPHR_MD_PeMDHeight.Text);
                updateCommand.Parameters.AddWithValue("@WEIGHT_LBS", txtPHR_MD_PeMDWeight.Text);

                try
                {
                    updateConnection.Open();
                    updateCommand.ExecuteNonQuery();
                    updateConnection.Close();
                    MD_LoadPER_DETAILS();
                }
                catch (SqlException ex)
                {
                    updateConnection.Close();
                    MessageBox.Show(ex.ToString());
                    throw ex;
                }
                cmbPHR_MD_PeMDBloodGroup.Enabled = false;
                chkPHR_MD_PeMDOrganDonor.Enabled = false;
                rdoPHR_MD_PeMD_HSNegative.Enabled = false;
                rdoPHR_MD_PeMD_HSPositive.Enabled = false;
                rdoPHR_MD_PeMD_HSUnknown.Enabled = false;
                txtPHR_MD_PeMDHeight.ReadOnly = true;
                txtPHR_MD_PeMDWeight.ReadOnly = true;
                llbPHR_MD_PeMDEdit.Enabled = true;
                llbPHR_MD_PeMDSave.Enabled = false;
                llbPHR_MD_PeMDCancel.Enabled = false;
            }
        }

        private void llbPHR_MD_PeMDCancel_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_PeMDCancel.Enabled == true)
            {
                cmbPHR_MD_PeMDBloodGroup.Enabled = false;
                chkPHR_MD_PeMDOrganDonor.Enabled = false;
                rdoPHR_MD_PeMD_HSNegative.Enabled = false;
                rdoPHR_MD_PeMD_HSPositive.Enabled = false;
                rdoPHR_MD_PeMD_HSUnknown.Enabled = false;
                txtPHR_MD_PeMDHeight.ReadOnly = true;
                txtPHR_MD_PeMDWeight.ReadOnly = true;
                llbPHR_MD_PeMDEdit.Enabled = true;
                llbPHR_MD_PeMDSave.Enabled = false;
                llbPHR_MD_PeMDCancel.Enabled = false;
                MD_LoadPER_DETAILS();
            }
        }

        // allergy details group methods
        private void llbPHR_MD_ADEdit_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_ADEdit.Enabled == true)
            {
                txtPHR_MD_ADAllergicTo.ReadOnly = false;
                dtpPHR_MD_ADOnset.Enabled = true;
                rtxPHR_MD_ADNote.ReadOnly = false;
                llbPHR_MD_ADEdit.Enabled = false;
                llbPHR_MD_ADSave.Enabled = true;
                llbPHR_MD_ADCancel.Enabled = true;
            }
        }

        private void llbPHR_MD_ADSave_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_ADSave.Enabled == true)
            {
                SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
                string deleteStatement = "DELETE FROM ALLERGY_TBL " +
                    "WHERE PATIENT_ID = @PATIENT_ID";
                SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
                deleteCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
                try
                {
                    connection.Open();
                    deleteCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (SqlException ex)
                {
                    connection.Close();
                    MessageBox.Show(ex.ToString());
                    throw ex;
                }

                string insertStatement = "INSERT ALLERGY_TBL " +
                    "(PATIENT_ID, ALLERGEN, ONSET_DATE, ALLERGY_ID, NOTE) " +
                    "VALUES (@PATIENT_ID, @ALLERGEN, @ONSET_DATE, @ALLERGY_ID, @NOTE)";
                for (int i = 0; i < lvwPHR_MD_ADAllergies.Items.Count; i++) 
                {
                    SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
                    insertCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
                    insertCommand.Parameters.AddWithValue("@ALLERGEN", lvwPHR_MD_ADAllergies.Items[i].SubItems[0].Text);
                    insertCommand.Parameters.AddWithValue("@ONSET_DATE", Convert.ToDateTime(lvwPHR_MD_ADAllergies.Items[i].SubItems[1].Text));
                    insertCommand.Parameters.AddWithValue("@ALLERGY_ID", lvwPHR_MD_ADAllergies.Items[i].SubItems[2].Text);
                    insertCommand.Parameters.AddWithValue("@NOTE", lvwPHR_MD_ADAllergies.Items[i].SubItems[3].Text);
                    try
                    {
                        connection.Open();
                        insertCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (SqlException ex)
                    {
                        connection.Close();
                        MessageBox.Show(ex.ToString());
                        throw ex;
                    }
                }

                txtPHR_MD_ADAllergicTo.ReadOnly = true;
                dtpPHR_MD_ADOnset.Enabled = false;
                rtxPHR_MD_ADNote.ReadOnly = true;
                llbPHR_MD_ADAdd.Enabled = false;
                llbPHR_MD_ADRemoveSelected.Enabled = false;
                llbPHR_MD_ADEdit.Enabled = true;
                llbPHR_MD_ADSave.Enabled = false;
                llbPHR_MD_ADCancel.Enabled = false;
                lvwPHR_MD_ADAllergies.Items.Clear();
                MD_LoadALLERGY();
            }
        }

        private void llbPHR_MD_ADCancel_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_ADCancel.Enabled == true)
            {
                txtPHR_MD_ADAllergicTo.ReadOnly = true;
                dtpPHR_MD_ADOnset.Enabled = false;
                rtxPHR_MD_ADNote.ReadOnly = true;
                llbPHR_MD_ADAdd.Enabled = false;
                llbPHR_MD_ADRemoveSelected.Enabled = false;
                llbPHR_MD_ADEdit.Enabled = true;
                llbPHR_MD_ADSave.Enabled = false;
                llbPHR_MD_ADCancel.Enabled = false;
                lvwPHR_MD_ADAllergies.Items.Clear();
                MD_LoadALLERGY();
            }
        }

        // immunization details group methods
        private void llbPHR_MD_IDEdit_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_IDEdit.Enabled == true)
            {
                txtPHR_MD_IDImmunization.ReadOnly = false;
                dtpPHR_MD_IDDate.Enabled = true;
                rtxPHR_MD_IDNote.ReadOnly = false;
                llbPHR_MD_IDEdit.Enabled = false;
                llbPHR_MD_IDSave.Enabled = true;
                llbPHR_MD_IDCancel.Enabled = true;
            }
        }

        private void llbPHR_MD_IDSave_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_IDSave.Enabled == true)
            {
                SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
                string deleteStatement = "DELETE FROM IMMUNIZATION_TBL " +
                    "WHERE PATIENT_ID = @PATIENT_ID";
                SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
                deleteCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
                try
                {
                    connection.Open();
                    deleteCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (SqlException ex)
                {
                    connection.Close();
                    MessageBox.Show(ex.ToString());
                    throw ex;
                }

                string insertStatement = "INSERT IMMUNIZATION_TBL " +
                    "(PATIENT_ID, IMMUNIZATION, DATE, IMMUNIZATION_ID, NOTE) " +
                    "VALUES (@PATIENT_ID, @IMMUNIZATION, @DATE, @IMMUNIZATION_ID, @NOTE)";
                for (int i = 0; i < lvwPHR_MD_IDImmunizations.Items.Count; i++)
                {
                    SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
                    insertCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
                    insertCommand.Parameters.AddWithValue("@IMMUNIZATION", lvwPHR_MD_IDImmunizations.Items[i].SubItems[0].Text);
                    insertCommand.Parameters.AddWithValue("@DATE", Convert.ToDateTime(lvwPHR_MD_IDImmunizations.Items[i].SubItems[1].Text));
                    insertCommand.Parameters.AddWithValue("@IMMUNIZATION_ID", lvwPHR_MD_IDImmunizations.Items[i].SubItems[2].Text);
                    insertCommand.Parameters.AddWithValue("@NOTE", lvwPHR_MD_IDImmunizations.Items[i].SubItems[3].Text);
                    try
                    {
                        connection.Open();
                        insertCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (SqlException ex)
                    {
                        connection.Close();
                        MessageBox.Show(ex.ToString());
                        throw ex;
                    }
                }

                txtPHR_MD_IDImmunization.ReadOnly = true;
                dtpPHR_MD_IDDate.Enabled = false;
                rtxPHR_MD_IDNote.ReadOnly = true;
                llbPHR_MD_IDAdd.Enabled = false;
                llbPHR_MD_IDRemoveSelected.Enabled = false;
                llbPHR_MD_IDEdit.Enabled = true;
                llbPHR_MD_IDSave.Enabled = false;
                llbPHR_MD_IDCancel.Enabled = false;
                lvwPHR_MD_IDImmunizations.Items.Clear();
                MD_LoadIMMUNIZATION();
            }
        }

        private void llbPHR_MD_IDCancel_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_IDCancel.Enabled == true)
            {
                txtPHR_MD_IDImmunization.ReadOnly = true;
                dtpPHR_MD_IDDate.Enabled = false;
                rtxPHR_MD_IDNote.ReadOnly = true;
                llbPHR_MD_IDAdd.Enabled = false;
                llbPHR_MD_IDRemoveSelected.Enabled = false;
                llbPHR_MD_IDEdit.Enabled = true;
                llbPHR_MD_IDSave.Enabled = false;
                llbPHR_MD_IDCancel.Enabled = false;
                lvwPHR_MD_IDImmunizations.Items.Clear();
                MD_LoadIMMUNIZATION();
            }
        }

        // prescribed medication details group methods
        private void llbPHR_MD_PrMDEdit_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_PrMDEdit.Enabled == true)
            {
                txtPHR_MD_PrMDMedication.ReadOnly = false;
                dtpPHR_MD_PrMDPrescribed.Enabled = true;
                chkPHR_MD_PrMDChronic.Enabled = true;
                rtxPHR_MD_PrMDNote.ReadOnly = false;
                llbPHR_MD_PrMDEdit.Enabled = false;
                llbPHR_MD_PrMDSave.Enabled = true;
                llbPHR_MD_PrMDCancel.Enabled = true;
            }
        }

        private void llbPHR_MD_PrMDSave_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_PrMDSave.Enabled == true)
            {
                SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
                string deleteStatement = "DELETE FROM MEDICATION_TBL " +
                    "WHERE PATIENT_ID = @PATIENT_ID";
                SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
                deleteCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
                try
                {
                    connection.Open();
                    deleteCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (SqlException ex)
                {
                    connection.Close();
                    MessageBox.Show(ex.ToString());
                    throw ex;
                }

                string insertStatement = "INSERT MEDICATION_TBL " +
                    "(PATIENT_ID, MEDICATION, CHRONIC, DATE, MED_ID, NOTE) " +
                    "VALUES (@PATIENT_ID, @MEDICATION, @CHRONIC, @DATE, @MED_ID, @NOTE)";
                for (int i = 0; i < lvwPHR_MD_PrMDMedications.Items.Count; i++)
                {
                    SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
                    insertCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
                    insertCommand.Parameters.AddWithValue("@MEDICATION", lvwPHR_MD_PrMDMedications.Items[i].SubItems[0].Text);
                    if (lvwPHR_MD_PrMDMedications.Items[i].SubItems[1].Text == "True")
                    {
                        insertCommand.Parameters.AddWithValue("@CHRONIC", "True");
                    }
                    else if (lvwPHR_MD_PrMDMedications.Items[i].SubItems[1].Text == "False")
                    {
                        insertCommand.Parameters.AddWithValue("@CHRONIC", "False");
                    }
                    insertCommand.Parameters.AddWithValue("@DATE", Convert.ToDateTime(lvwPHR_MD_PrMDMedications.Items[i].SubItems[2].Text));
                    insertCommand.Parameters.AddWithValue("@MED_ID", lvwPHR_MD_PrMDMedications.Items[i].SubItems[3].Text);
                    insertCommand.Parameters.AddWithValue("@NOTE", lvwPHR_MD_PrMDMedications.Items[i].SubItems[4].Text);
                    try
                    {
                        connection.Open();
                        insertCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (SqlException ex)
                    {
                        connection.Close();
                        MessageBox.Show(ex.ToString());
                        throw ex;
                    }
                }

                txtPHR_MD_PrMDMedication.ReadOnly = true;
                dtpPHR_MD_PrMDPrescribed.Enabled = false;
                chkPHR_MD_PrMDChronic.Enabled = false;
                rtxPHR_MD_PrMDNote.ReadOnly = true;
                llbPHR_MD_PrMDAdd.Enabled = false;
                llbPHR_MD_PrMDRemoveSelected.Enabled = false;
                llbPHR_MD_PrMDEdit.Enabled = true;
                llbPHR_MD_PrMDSave.Enabled = false;
                llbPHR_MD_PrMDCancel.Enabled = false;
                lvwPHR_MD_PrMDMedications.Items.Clear();
                MD_LoadMEDICATION();
            }
        }

        private void llbPHR_MD_PrMDCancel_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_PrMDCancel.Enabled == true)
            {
                txtPHR_MD_PrMDMedication.ReadOnly = true;
                dtpPHR_MD_PrMDPrescribed.Enabled = false;
                chkPHR_MD_PrMDChronic.Enabled = false;
                rtxPHR_MD_PrMDNote.ReadOnly = true;
                llbPHR_MD_PrMDAdd.Enabled = false;
                llbPHR_MD_PrMDRemoveSelected.Enabled = false;
                llbPHR_MD_PrMDEdit.Enabled = true;
                llbPHR_MD_PrMDSave.Enabled = false;
                llbPHR_MD_PrMDCancel.Enabled = false;
                lvwPHR_MD_PrMDMedications.Items.Clear();
                MD_LoadMEDICATION();
            }
        }

        // test result details group methods
        private void llbPHR_MD_TRDEdit_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_TRDEdit.Enabled == true)
            {
                txtPHR_MD_TRDTest.ReadOnly = false;
                dtpPHR_MD_TRDDate.Enabled = true;
                txtPHR_MD_TRDResult.ReadOnly = false;
                rtxPHR_MD_TRDNote.ReadOnly = false;
                llbPHR_MD_TRDEdit.Enabled = false;
                llbPHR_MD_TRDSave.Enabled = true;
                llbPHR_MD_TRDCancel.Enabled = true;
            }
        }

        private void llbPHR_MD_TRDSave_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_TRDSave.Enabled == true)
            {
                SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
                string deleteStatement = "DELETE FROM TEST_TBL " +
                    "WHERE PATIENT_ID = @PATIENT_ID";
                SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
                deleteCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
                try
                {
                    connection.Open();
                    deleteCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (SqlException ex)
                {
                    connection.Close();
                    MessageBox.Show(ex.ToString());
                    throw ex;
                }

                string insertStatement = "INSERT TEST_TBL " +
                    "(PATIENT_ID, TEST, DATE, RESULT, TEST_ID, NOTE) " +
                    "VALUES (@PATIENT_ID, @TEST, @DATE, @RESULT, @TEST_ID, @NOTE)";
                for (int i = 0; i < lvwPHR_MD_TRDTestResults.Items.Count; i++)
                {
                    SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
                    insertCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
                    insertCommand.Parameters.AddWithValue("@TEST", lvwPHR_MD_TRDTestResults.Items[i].SubItems[0].Text);
                    insertCommand.Parameters.AddWithValue("@DATE", Convert.ToDateTime(lvwPHR_MD_TRDTestResults.Items[i].SubItems[1].Text));
                    insertCommand.Parameters.AddWithValue("@RESULT", lvwPHR_MD_TRDTestResults.Items[i].SubItems[2].Text);
                    insertCommand.Parameters.AddWithValue("@TEST_ID", lvwPHR_MD_TRDTestResults.Items[i].SubItems[3].Text);
                    insertCommand.Parameters.AddWithValue("@NOTE", lvwPHR_MD_TRDTestResults.Items[i].SubItems[4].Text);
                    try
                    {
                        connection.Open();
                        insertCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (SqlException ex)
                    {
                        connection.Close();
                        MessageBox.Show(ex.ToString());
                        throw ex;
                    }
                }

                txtPHR_MD_TRDTest.ReadOnly = true;
                dtpPHR_MD_TRDDate.Enabled = false;
                txtPHR_MD_TRDResult.ReadOnly = true;
                rtxPHR_MD_TRDNote.ReadOnly = true;
                llbPHR_MD_TRDAdd.Enabled = false;
                llbPHR_MD_TRDRemoveSelected.Enabled = false;
                llbPHR_MD_TRDEdit.Enabled = true;
                llbPHR_MD_TRDSave.Enabled = false;
                llbPHR_MD_TRDCancel.Enabled = false;
                lvwPHR_MD_TRDTestResults.Items.Clear();
                MD_LoadTEST();
            }
        }

        private void llbPHR_MD_TRDCancel_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_TRDCancel.Enabled == true)
            {
                txtPHR_MD_TRDTest.ReadOnly = true;
                dtpPHR_MD_TRDDate.Enabled = false;
                txtPHR_MD_TRDResult.ReadOnly = true;
                rtxPHR_MD_TRDNote.ReadOnly = true;
                llbPHR_MD_TRDAdd.Enabled = false;
                llbPHR_MD_TRDRemoveSelected.Enabled = false;
                llbPHR_MD_TRDEdit.Enabled = true;
                llbPHR_MD_TRDSave.Enabled = false;
                llbPHR_MD_TRDCancel.Enabled = false;
                lvwPHR_MD_TRDTestResults.Items.Clear();
                MD_LoadTEST();
            }
        }

        // medical condition details group methods
        private void llbPHR_MD_MCDEdit_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_MCDEdit.Enabled == true)
            {
                txtPHR_MD_MCDCondition.ReadOnly = false;
                dtpPHR_MD_MCDOnset.Enabled = true;
                rdoPHR_MD_MCDAcute.Enabled = true;
                rdoPHR_MD_MCDChronic.Enabled = true;
                rtxPHR_MD_MCDNote.ReadOnly = false;
                llbPHR_MD_MCDEdit.Enabled = false;
                llbPHR_MD_MCDSave.Enabled = true;
                llbPHR_MD_MCDCancel.Enabled = true;
            }
        }

        private void llbPHR_MD_MCDSave_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_MCDSave.Enabled == true)
            {
                SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
                string deleteStatement = "DELETE FROM CONDITION " +
                    "WHERE PATIENT_ID = @PATIENT_ID";
                SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
                deleteCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
                try
                {
                    connection.Open();
                    deleteCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (SqlException ex)
                {
                    connection.Close();
                    MessageBox.Show(ex.ToString());
                    throw ex;
                }

                string insertStatement = "INSERT CONDITION " +
                    "(PATIENT_ID, CONDITION, ONSET_DATE, SEVERITY, CONDITION_ID, NOTE) " +
                    "VALUES (@PATIENT_ID, @CONDITION, @ONSET_DATE, @SEVERITY, @CONDITION_ID, @NOTE)";
                for (int i = 0; i < lvwPHR_MD_MCDMedicalConditions.Items.Count; i++)
                {
                    SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
                    insertCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
                    insertCommand.Parameters.AddWithValue("@CONDITION", lvwPHR_MD_MCDMedicalConditions.Items[i].SubItems[0].Text);
                    insertCommand.Parameters.AddWithValue("@ONSET_DATE", Convert.ToDateTime(lvwPHR_MD_MCDMedicalConditions.Items[i].SubItems[1].Text));
                    if (lvwPHR_MD_MCDMedicalConditions.Items[i].SubItems[2].Text == "Chronic")
                    {
                        insertCommand.Parameters.AddWithValue("@SEVERITY", "True");
                    }
                    else if (lvwPHR_MD_MCDMedicalConditions.Items[i].SubItems[2].Text == "Acute")
                    {
                        insertCommand.Parameters.AddWithValue("@SEVERITY", "False");
                    }
                    insertCommand.Parameters.AddWithValue("@CONDITION_ID", lvwPHR_MD_MCDMedicalConditions.Items[i].SubItems[3].Text);
                    insertCommand.Parameters.AddWithValue("@NOTE", lvwPHR_MD_MCDMedicalConditions.Items[i].SubItems[4].Text);
                    try
                    {
                        connection.Open();
                        insertCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (SqlException ex)
                    {
                        connection.Close();
                        MessageBox.Show(ex.ToString());
                        throw ex;
                    }
                }

                txtPHR_MD_MCDCondition.ReadOnly = true;
                dtpPHR_MD_MCDOnset.Enabled = false;
                rdoPHR_MD_MCDAcute.Enabled = false;
                rdoPHR_MD_MCDChronic.Enabled = false;
                rtxPHR_MD_MCDNote.ReadOnly = true;
                llbPHR_MD_MCDAdd.Enabled = false;
                llbPHR_MD_MCDRemoveSelected.Enabled = false;
                llbPHR_MD_MCDEdit.Enabled = true;
                llbPHR_MD_MCDSave.Enabled = false;
                llbPHR_MD_MCDCancel.Enabled = false;
                lvwPHR_MD_MCDMedicalConditions.Items.Clear();
                MD_LoadCONDITION();
            }
        }

        private void llbPHR_MD_MCDCancel_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_MCDCancel.Enabled == true)
            {
                txtPHR_MD_MCDCondition.ReadOnly = true;
                dtpPHR_MD_MCDOnset.Enabled = false;
                rdoPHR_MD_MCDAcute.Enabled = false;
                rdoPHR_MD_MCDChronic.Enabled = false;
                rtxPHR_MD_MCDNote.ReadOnly = true;
                llbPHR_MD_MCDAdd.Enabled = false;
                llbPHR_MD_MCDRemoveSelected.Enabled = false;
                llbPHR_MD_MCDEdit.Enabled = true;
                llbPHR_MD_MCDSave.Enabled = false;
                llbPHR_MD_MCDCancel.Enabled = false;
                lvwPHR_MD_MCDMedicalConditions.Items.Clear();
                MD_LoadCONDITION();
            }
        }

        // medical procedure details group methods
        private void llbPHR_MD_MPDEdit_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_MPDEdit.Enabled == true)
            {
                txtPHR_MD_MPDProcedure.ReadOnly = false;
                dtpPHR_MD_MPDDate.Enabled = true;
                txtPHR_MD_MPDPerformedBy.ReadOnly = false;
                rtxPHR_MD_MPDNote.ReadOnly = false;
                llbPHR_MD_MPDEdit.Enabled = false;
                llbPHR_MD_MPDSave.Enabled = true;
                llbPHR_MD_MPDCancel.Enabled = true;
            }
        }

        private void llbPHR_MD_MPDSave_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_MPDSave.Enabled == true)
            {
                SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                    "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
                string deleteStatement = "DELETE FROM MED_PROC_TBL " +
                    "WHERE PATIENT_ID = @PATIENT_ID";
                SqlCommand deleteCommand = new SqlCommand(deleteStatement, connection);
                deleteCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
                try
                {
                    connection.Open();
                    deleteCommand.ExecuteNonQuery();
                    connection.Close();
                }
                catch (SqlException ex)
                {
                    connection.Close();
                    MessageBox.Show(ex.ToString());
                    throw ex;
                }

                string insertStatement = "INSERT MED_PROC_TBL " +
                    "(PATIENT_ID, MED_PROCEDURE, DATE, DOCTOR, PROCEDURE_ID, NOTE) " +
                    "VALUES (@PATIENT_ID, @MED_PROCEDURE, @DATE, @DOCTOR, @PROCEDURE_ID, @NOTE)";
                for (int i = 0; i < lvwPHR_MD_MPDMedicalProcedures.Items.Count; i++)
                {
                    SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
                    insertCommand.Parameters.AddWithValue("@PATIENT_ID", loginID);
                    insertCommand.Parameters.AddWithValue("@MED_PROCEDURE", lvwPHR_MD_MPDMedicalProcedures.Items[i].SubItems[0].Text);
                    insertCommand.Parameters.AddWithValue("@DATE", Convert.ToDateTime(lvwPHR_MD_MPDMedicalProcedures.Items[i].SubItems[1].Text));
                    insertCommand.Parameters.AddWithValue("@DOCTOR", lvwPHR_MD_MPDMedicalProcedures.Items[i].SubItems[2].Text);
                    insertCommand.Parameters.AddWithValue("@PROCEDURE_ID", lvwPHR_MD_MPDMedicalProcedures.Items[i].SubItems[3].Text);
                    insertCommand.Parameters.AddWithValue("@NOTE", lvwPHR_MD_MPDMedicalProcedures.Items[i].SubItems[4].Text);
                    try
                    {
                        connection.Open();
                        insertCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (SqlException ex)
                    {
                        connection.Close();
                        MessageBox.Show(ex.ToString());
                        throw ex;
                    }
                }

                txtPHR_MD_MPDProcedure.ReadOnly = true;
                dtpPHR_MD_MPDDate.Enabled = false;
                txtPHR_MD_MPDPerformedBy.ReadOnly = true;
                rtxPHR_MD_MPDNote.ReadOnly = true;
                llbPHR_MD_MPDAdd.Enabled = false;
                llbPHR_MD_MPDRemoveSelected.Enabled = false;
                llbPHR_MD_MPDEdit.Enabled = true;
                llbPHR_MD_MPDSave.Enabled = false;
                llbPHR_MD_MPDCancel.Enabled = false;
                lvwPHR_MD_MPDMedicalProcedures.Items.Clear();
                MD_LoadMED_PROC();
            }
        }

        private void llbPHR_MD_MPDCancel_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_MPDCancel.Enabled == true)
            {
                txtPHR_MD_MPDProcedure.ReadOnly = true;
                dtpPHR_MD_MPDDate.Enabled = false;
                txtPHR_MD_MPDPerformedBy.ReadOnly = true;
                rtxPHR_MD_MPDNote.ReadOnly = true;
                llbPHR_MD_MPDAdd.Enabled = false;
                llbPHR_MD_MPDRemoveSelected.Enabled = false;
                llbPHR_MD_MPDEdit.Enabled = true;
                llbPHR_MD_MPDSave.Enabled = false;
                llbPHR_MD_MPDCancel.Enabled = false;
                lvwPHR_MD_MPDMedicalProcedures.Items.Clear();
                MD_LoadMED_PROC();
            }
        }



        /*
         * methods for the functionality of the 'add' and 'remove selected' link labels of the medical 
         * details tab's groups. should the group be in 'edit mode' (meaning that the group's 'edit' link
         * label has been selected, enabling editing of the group's fields), and should the link label be
         * enabled, the methods will then continue their separate processes
         * 
         * in the case of the methods for the 'add' link labels, the relevant group adds a new item to the
         * list view dependent on the fields above the 'add' link label
         * 
         * in the case of the methods for the 'remove' selected link label, the relevant group removes the
         * item highlighted by the user from the list view
         */
        // allergy details group methods
        private void llbPHR_MD_ADAdd_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_ADAdd.Enabled == true && llbPHR_MD_ADEdit.Enabled == false)
            {
                Random r = new Random();
                int i = r.Next(9999999);

                ListViewItem item = new ListViewItem(txtPHR_MD_ADAllergicTo.Text);
                item.SubItems.Add(dtpPHR_MD_ADOnset.Text);
                item.SubItems.Add(i.ToString());
                item.SubItems.Add(rtxPHR_MD_ADNote.Text);

                lvwPHR_MD_ADAllergies.Items.Add(item);

                txtPHR_MD_ADAllergicTo.Text = "";
                rtxPHR_MD_ADNote.Text = "";
                llbPHR_MD_ADAdd.Enabled = false;
            }
        }

        private void llbPHR_MD_ADRemoveSelected_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_ADRemoveSelected.Enabled == true && llbPHR_MD_ADEdit.Enabled == false)
            {
                lvwPHR_MD_ADAllergies.SelectedItems[0].Remove();
                llbPHR_MD_ADRemoveSelected.Enabled = false;
            }

            if (lvwPHR_MD_ADAllergies.SelectedItems.Count > 0 && llbPHR_MD_ADEdit.Enabled == false)
            {
                llbPHR_MD_ADRemoveSelected.Enabled = true;
            }
            else 
            {
                llbPHR_MD_ADRemoveSelected.Enabled = false;
            }
        }

        // immunization details group methods
        private void llbPHR_MD_IDAdd_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_IDAdd.Enabled == true && llbPHR_MD_IDEdit.Enabled == false)
            {
                Random r = new Random();
                int i = r.Next(9999999);

                ListViewItem item = new ListViewItem(txtPHR_MD_IDImmunization.Text);
                item.SubItems.Add(dtpPHR_MD_IDDate.Text);
                item.SubItems.Add(i.ToString());
                item.SubItems.Add(rtxPHR_MD_IDNote.Text);

                lvwPHR_MD_IDImmunizations.Items.Add(item);

                txtPHR_MD_IDImmunization.Text = "";
                rtxPHR_MD_IDNote.Text = "";
                llbPHR_MD_IDAdd.Enabled = false;
            }
        }

        private void llbPHR_MD_IDRemoveSelected_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_IDRemoveSelected.Enabled == true && llbPHR_MD_IDEdit.Enabled == false)
            {
                lvwPHR_MD_IDImmunizations.SelectedItems[0].Remove();
                llbPHR_MD_IDRemoveSelected.Enabled = false;
            }

            if (lvwPHR_MD_IDImmunizations.SelectedItems.Count > 0 && llbPHR_MD_IDEdit.Enabled == false)
            {
                llbPHR_MD_IDRemoveSelected.Enabled = true;
            }
            else
            {
                llbPHR_MD_IDRemoveSelected.Enabled = false;
            }
        }

        // prescribed medication details group methods
        private void llbPHR_MD_PrMDAdd_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_PrMDAdd.Enabled == true && llbPHR_MD_PrMDEdit.Enabled == false)
            {
                Random r = new Random();
                int i = r.Next(9999999);

                ListViewItem item = new ListViewItem(txtPHR_MD_PrMDMedication.Text);
                if (chkPHR_MD_PrMDChronic.Checked == true)
                {
                    item.SubItems.Add("True");
                }
                else if (chkPHR_MD_PrMDChronic.Checked == false) 
                {
                    item.SubItems.Add("False");
                }
                item.SubItems.Add(dtpPHR_MD_PrMDPrescribed.Text);
                item.SubItems.Add(i.ToString());
                item.SubItems.Add(rtxPHR_MD_PrMDNote.Text);

                lvwPHR_MD_PrMDMedications.Items.Add(item);

                txtPHR_MD_PrMDMedication.Text = "";
                chkPHR_MD_PrMDChronic.Checked = false;
                rtxPHR_MD_PrMDNote.Text = "";
                llbPHR_MD_PrMDAdd.Enabled = false;
            }
        }

        private void llbPHR_MD_PrMDRemoveSelected_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_PrMDRemoveSelected.Enabled == true && llbPHR_MD_PrMDEdit.Enabled == false)
            {
                lvwPHR_MD_PrMDMedications.SelectedItems[0].Remove();
                llbPHR_MD_PrMDRemoveSelected.Enabled = false;
            }

            if (lvwPHR_MD_PrMDMedications.SelectedItems.Count > 0 && llbPHR_MD_PrMDEdit.Enabled == false)
            {
                llbPHR_MD_PrMDRemoveSelected.Enabled = true;
            }
            else 
            {
                llbPHR_MD_PrMDRemoveSelected.Enabled = false;
            }
        }

        // test result details group methods
        private void llbPHR_MD_TRDAdd_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_TRDAdd.Enabled == true && llbPHR_MD_TRDEdit.Enabled == false)
            {
                Random r = new Random();
                int i = r.Next(9999999);

                ListViewItem item = new ListViewItem(txtPHR_MD_TRDTest.Text);
                item.SubItems.Add(dtpPHR_MD_TRDDate.Text);
                item.SubItems.Add(txtPHR_MD_TRDResult.Text);
                item.SubItems.Add(i.ToString());
                item.SubItems.Add(rtxPHR_MD_TRDNote.Text);

                lvwPHR_MD_TRDTestResults.Items.Add(item);

                txtPHR_MD_TRDTest.Text = "";
                txtPHR_MD_TRDResult.Text = "";
                rtxPHR_MD_IDNote.Text = "";
                llbPHR_MD_IDAdd.Enabled = false;
            }
        }

        private void llbPHR_MD_TRDRemoveSelected_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_TRDRemoveSelected.Enabled == true && llbPHR_MD_TRDEdit.Enabled == false)
            {
                lvwPHR_MD_TRDTestResults.SelectedItems[0].Remove();
                llbPHR_MD_TRDRemoveSelected.Enabled = false;
            }

            if (lvwPHR_MD_TRDTestResults.SelectedItems.Count > 0 && llbPHR_MD_TRDEdit.Enabled == false)
            {
                llbPHR_MD_TRDRemoveSelected.Enabled = true;
            }
            else
            {
                llbPHR_MD_TRDRemoveSelected.Enabled = false;
            }
        }

        // medical condition details group methods
        private void llbPHR_MD_MCDAdd_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_MCDAdd.Enabled == true && llbPHR_MD_MCDEdit.Enabled == false)
            {
                Random r = new Random();
                int i = r.Next(9999999);

                ListViewItem item = new ListViewItem(txtPHR_MD_MCDCondition.Text);
                item.SubItems.Add(dtpPHR_MD_MCDOnset.Text);
                if (rdoPHR_MD_MCDAcute.Checked == true)
                {
                    item.SubItems.Add("Acute");
                }
                else if (rdoPHR_MD_MCDChronic.Checked == true)
                {
                    item.SubItems.Add("Chronic");
                }
                item.SubItems.Add(i.ToString());
                item.SubItems.Add(rtxPHR_MD_MCDNote.Text);

                lvwPHR_MD_MCDMedicalConditions.Items.Add(item);

                txtPHR_MD_MCDCondition.Text = "";
                rdoPHR_MD_MCDAcute.Checked = true;
                rtxPHR_MD_MCDNote.Text = "";
                llbPHR_MD_MCDAdd.Enabled = false;
            }
        }

        private void llbPHR_MD_MCDRemoveSelected_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_MCDRemoveSelected.Enabled == true && llbPHR_MD_MCDEdit.Enabled == false)
            {
                lvwPHR_MD_MCDMedicalConditions.SelectedItems[0].Remove();
                llbPHR_MD_MCDRemoveSelected.Enabled = false;
            }

            if (lvwPHR_MD_MCDMedicalConditions.SelectedItems.Count > 0 && llbPHR_MD_MCDEdit.Enabled == false)
            {
                llbPHR_MD_MCDRemoveSelected.Enabled = true;
            }
            else
            {
                llbPHR_MD_MCDRemoveSelected.Enabled = false;
            }
        }

        // medical procedure details group methods
        private void llbPHR_MD_MPDAdd_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_MPDAdd.Enabled == true && llbPHR_MD_MPDEdit.Enabled == false)
            {
                Random r = new Random();
                int i = r.Next(9999999);

                ListViewItem item = new ListViewItem(txtPHR_MD_MPDProcedure.Text);
                item.SubItems.Add(dtpPHR_MD_MPDDate.Text);
                item.SubItems.Add(txtPHR_MD_MPDPerformedBy.Text);
                item.SubItems.Add(i.ToString());
                item.SubItems.Add(rtxPHR_MD_MPDNote.Text);

                lvwPHR_MD_MPDMedicalProcedures.Items.Add(item);

                txtPHR_MD_MPDProcedure.Text = "";
                txtPHR_MD_MPDPerformedBy.Text = "";
                rtxPHR_MD_MPDNote.Text = "";
                llbPHR_MD_MPDAdd.Enabled = false;
            }
        }

        private void llbPHR_MD_MPDRemoveSelected_Click(object sender, EventArgs e)
        {
            if (llbPHR_MD_MPDRemoveSelected.Enabled == true && llbPHR_MD_MPDEdit.Enabled == false) 
            {
                lvwPHR_MD_MPDMedicalProcedures.SelectedItems[0].Remove();
                llbPHR_MD_MPDRemoveSelected.Enabled = false;
            }

            if (lvwPHR_MD_MPDMedicalProcedures.SelectedItems.Count > 0 && llbPHR_MD_MPDEdit.Enabled == false)
            {
                llbPHR_MD_MPDRemoveSelected.Enabled = true;
            }
            else
            {
                llbPHR_MD_MPDRemoveSelected.Enabled = false;
            }
        }



        /*
         * methods that enable the 'add' buttons of the medical details tab's groups should the 
         * right fields be filled and enable the 'remove selected' buttons should an item in the
         * corresponding group's list view be selected
         */
        private void txtPHR_MD_ADAllergicTo_TextChanged(object sender, EventArgs e)
        {
            if (txtPHR_MD_ADAllergicTo.Text != "")
            {
                llbPHR_MD_ADAdd.Enabled = true;
            }
            else 
            {
                llbPHR_MD_ADAdd.Enabled = false;
            }
        }

        private void txtPHR_MD_IDImmunization_TextChanged(object sender, EventArgs e)
        {
            if (txtPHR_MD_IDImmunization.Text != "")
            {
                llbPHR_MD_IDAdd.Enabled = true;
            }
            else
            {
                llbPHR_MD_IDAdd.Enabled = false;
            }
        }

        private void txtPHR_MD_PrMDMedication_TextChanged(object sender, EventArgs e)
        {
            if (txtPHR_MD_PrMDMedication.Text != "")
            {
                llbPHR_MD_PrMDAdd.Enabled = true;
            }
            else
            {
                llbPHR_MD_PrMDAdd.Enabled = false;
            }
        }

        private void txtPHR_MD_TRDTest_TextChanged(object sender, EventArgs e)
        {
            if (txtPHR_MD_TRDTest.Text != "")
            {
                llbPHR_MD_TRDAdd.Enabled = true;
            }
            else
            {
                llbPHR_MD_TRDAdd.Enabled = false;
            }
        }

        private void txtPHR_MD_MCDCondition_TextChanged(object sender, EventArgs e)
        {
            if (txtPHR_MD_MCDCondition.Text != "")
            {
                llbPHR_MD_MCDAdd.Enabled = true;
            }
            else
            {
                llbPHR_MD_MCDAdd.Enabled = false;
            }
        }

        private void txtPHR_MD_MPDProcedure_TextChanged(object sender, EventArgs e)
        {
            if (txtPHR_MD_MPDProcedure.Text != "")
            {
                llbPHR_MD_MPDAdd.Enabled = true;
            }
            else
            {
                llbPHR_MD_MPDAdd.Enabled = false;
            }
        }
    }
}
