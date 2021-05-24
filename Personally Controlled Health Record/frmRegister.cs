using Personally_Controlled_Health_Record.pchr42563DataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Personally_Controlled_Health_Record
{
    /*
     * this class handles the registration process, taking the bare essential information
     * from the user and using it to create a new entry in PATIENT_TBL
     */
    public partial class frmRegister : Form
    {
        // class-relevant fields
        static int patientID;
        static Random random = new Random();

        // basic constructor
        public frmRegister()
        {
            InitializeComponent();
        }

        /*
         * gathers all of the textbox values, opens an Sql connection, and creates a new row with them
         * should the restrictions placed by validateRegister be adhered to. the user is then taken back
         * to the login screen, where they can potentially use their new username and password to login
         */
        private void btnRegRegister_Click(object sender, EventArgs e)
        {
            if (validateRegister()) 
            {
                SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                    "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
                string insertStatement = "INSERT PATIENT_TBL " +
                    "(PATIENT_ID, LAST_NAME, FIRST_NAME, DATE_Of_BIRTH, PRIMARY_ID, " +
                    "USERNAME, PASSWORD, GENDER, TITLE, INITIALS) " +
                    "VALUES (@PATIENT_ID, @LAST_NAME, @FIRST_NAME, @DATE_Of_BIRTH, " +
                    "@PRIMARY_ID, @USERNAME, @PASSWORD, @GENDER, @TITLE, @INITIALS)";
                SqlCommand insertCommand = new SqlCommand(insertStatement, connection);
                insertCommand.Parameters.AddWithValue("@PATIENT_ID", random.Next(9999999));
                insertCommand.Parameters.AddWithValue("@LAST_NAME", txtReg_PDLastName.Text);
                insertCommand.Parameters.AddWithValue("@FIRST_NAME", txtReg_PDFirstName.Text);
                insertCommand.Parameters.AddWithValue("@DATE_Of_BIRTH", dtpReg_PDDateOfBirth.Value);
                insertCommand.Parameters.AddWithValue("@PRIMARY_ID", txtReg_PDIdentityNumber.Text);
                insertCommand.Parameters.AddWithValue("@USERNAME", txtReg_LDUsername.Text);
                insertCommand.Parameters.AddWithValue("@PASSWORD", txtReg_LDPassword.Text);
                if (rdoReg_PDGenderMale.Checked)
                {
                    insertCommand.Parameters.AddWithValue("@GENDER", 1);
                }
                else if (rdoReg_PDGenderFemale.Checked) 
                {
                    insertCommand.Parameters.AddWithValue("@GENDER", 0);
                }
                insertCommand.Parameters.AddWithValue("@TITLE", cmbReg_PDTitle.SelectedItem.ToString());
                insertCommand.Parameters.AddWithValue("@INITIALS", txtReg_PDInitials.Text);

                try
                {
                    connection.Open();
                    insertCommand.ExecuteNonQuery();
                    string selectStatement = "SELECT IDENT_CURRENT('PATIENT_TBL') FROM PATIENT_TBL";
                    SqlCommand selectCommand = new SqlCommand(selectStatement, connection);
                    selectCommand.ExecuteScalar();
                    connection.Close();
                }
                catch (SqlException ex)
                {
                    connection.Close();
                    throw ex;
                }

                this.Hide();
                var loginForm = new frmLogin();
                loginForm.FormClosed += (s, args) => this.Close();
                loginForm.Show();
            }
        }

        // returns the user to the login form with no changes made when the corresponding button is clicked
        public void btnRegCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            var loginForm = new frmLogin();
            loginForm.FormClosed += (s, args) => this.Close();
            loginForm.Show();
        }

        /*
         * checks a variety of factors, making sure that no field is empty, too long, or incorrect. should the fields pass
         * this test, three Sql connections to the PATIENT_TBL are made: one to check if the username is used by any other row,
         * one to check if the primary id is used by any other row, and one to check if the randomly-rolled patient id is 
         * used by any other row. if the first or second are true, the user's notified, and the method returns false. if the
         * third is true, the patient id is rerolled until it is not shared by any other row. if these Sql connections all 
         * return nothing, the method will successfully create a new row and return the user to the login form
         */
        private bool validateRegister() 
        {
            if (!(txtReg_LDUsername.Text == "") && !(txtReg_LDPassword.Text == "") && !(txtReg_LDConfirmPassword.Text == "")
                && !(txtReg_PDIdentityNumber.Text == "") && !(txtReg_PDLastName.Text == "") && !(txtReg_PDFirstName.Text == ""))
            {
                try
                {
                    Convert.ToInt32(txtReg_PDIdentityNumber.Text);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Oops, your identity number is invalid! Please only enter it as a whole number.", "Invalid Identity Number");
                    return false;
                }

                if (txtReg_LDPassword.Text != txtReg_LDConfirmPassword.Text)
                {
                    MessageBox.Show("Oops, you forgot to confirm your password! Please make sure that the 'Password'" +
                        " and 'Confirm Password' fields match.", "Confirm Password");
                    return false;
                }
                else if ((txtReg_PDLastName.Text.Length > 20) || (txtReg_PDFirstName.Text.Length > 20))
                {
                    MessageBox.Show("Oops, your first and/or last name are too long! Please enter first and last names no longer than 20 characters apiece.", "First/Last Name Too Long");
                    return false;
                }
                else if ((txtReg_LDUsername.Text.Length > 20) || (txtReg_LDPassword.Text.Length > 20))
                {
                    MessageBox.Show("Oops, your username and/or password are too long! Please enter a username and password no longer than 20 characters apiece.", "Username/Password Too Long");
                    return false;
                }
                else if (txtReg_PDIdentityNumber.Text.Length > 20)
                {
                    MessageBox.Show("Oops, your identity too long! Please enter an identity number no longer than 10 characters.", "Identity Number Too Long");
                    return false;
                }
                else if (txtReg_PDInitials.Text.Length > 10)
                {
                    MessageBox.Show("Oops, your initials too long! Please enter initials no longer than 10 characters.", "Initials Too Long");
                    return false;
                }
                else 
                {
                    patientID = random.Next(999999);

                    SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                    "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
                    string selectUsernameStatement = "SELECT PATIENT_ID FROM PATIENT_TBL WHERE USERNAME = @USERNAME";
                    string selectPatientIDStatement = "SELECT USERNAME FROM PATIENT_TBL WHERE PATIENT_ID = @PATIENT_ID";
                    string selectPrimaryIDStatement = "SELECT USERNAME FROM PATIENT_TBL WHERE PRIMARY_ID = @PRIMARY_ID";
                    SqlCommand selectUsernameCommand = new SqlCommand(selectUsernameStatement, connection);
                    SqlCommand selectPatientIDCommand = new SqlCommand(selectPatientIDStatement, connection);
                    SqlCommand selectPrimaryIDCommand = new SqlCommand(selectPrimaryIDStatement, connection);
                    selectUsernameCommand.Parameters.AddWithValue("@USERNAME", txtReg_LDUsername.Text);
                    selectPatientIDCommand.Parameters.AddWithValue("@PATIENT_ID", patientID);
                    selectPrimaryIDCommand.Parameters.AddWithValue("@PRIMARY_ID", txtReg_PDIdentityNumber.Text);
                    try
                    {
                        connection.Open();
                        SqlDataReader usernameReader = selectUsernameCommand.ExecuteReader();
                        if (usernameReader.Read()) 
                        {
                            MessageBox.Show("Sorry, but another patient is already using this username; please choose another.", "Username Taken");
                            return false;
                        }
                        usernameReader.Close();
                        SqlDataReader patientIDReader = selectPatientIDCommand.ExecuteReader();
                        while (patientIDReader.Read()) 
                        {
                            patientID = random.Next(999999);
                        }
                        patientIDReader.Close();
                        SqlDataReader primaryIDReader = selectPrimaryIDCommand.ExecuteReader();
                        if (primaryIDReader.Read()) 
                        {
                            MessageBox.Show("Sorry, but another patient is already using this Primary ID; please choose another.", "Primary ID Taken");
                            return false;
                        }
                        connection.Close();
                    }
                    catch (SqlException ex)
                    {
                        connection.Close();
                        throw ex;
                    }
                    MessageBox.Show("Account successfully created! Login at the 'Login' screen if you wish to view and/or edit your details and records.", "Success");
                    return true;
                }
            }
            else 
            {
                MessageBox.Show("Oops, you forgot to fill in one or more important fields! Please refill them and try again.", "Missing Fields");
                return false;
            }
        }
    }
}
