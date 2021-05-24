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
     * this class represents the entirety of the login form, allowing the user to enter a username
     * and password before accessing the information tied to said username and password should they
     * match a patient ID. the user can also access the register form from this form
     */
    public partial class frmLogin : Form
    {
        // class-relevant fields passed to the personal health record form upon successful login
        static string passedLoginID;
        static string passedPrimaryID;
        static string passedPassword;

        // basic constructor
        public frmLogin()
        {
            InitializeComponent();
        }

        // opens the register form when the corresponding button is clicked
        private void btnLgnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            var regForm = new frmRegister();
            regForm.Show();
            regForm.FormClosed += (s, args) => this.Close();
        }

        /*
         * initiates the login process when the corresponding button is clicked, checking the validateLogin method first
         * before opening the personal health record form with the passed fields
         */
        private void btnLgnLogin_Click(object sender, EventArgs e)
        {
            if (validateLogin())
            {
                this.Hide();
                var recordForm = new frmPersonalHealthRecord(passedLoginID, passedPrimaryID, passedPassword);
                recordForm.Show();
                recordForm.FormClosed += (s, args) => this.Close();
            }
        }

        // closes the form and the application
        private void btnLgnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /*
         * opens an Sql connection to the PATIENT_TBL database and checks for patients tied to the given username and password.
         * if there's a match, the user is successfully logged in
         */
        private bool validateLogin() 
        {
            SqlConnection loginConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                    "|DataDirectory|\\pchr42563.mdf;Integrated Security=True;Connect Timeout=30");
            string selectStatement = "SELECT PATIENT_ID, PRIMARY_ID FROM PATIENT_TBL WHERE USERNAME = @USERNAME AND PASSWORD = @PASSWORD";
            SqlCommand selectCommand = new SqlCommand(selectStatement, loginConnection);
            selectCommand.Parameters.AddWithValue("@USERNAME", txtLgnUsername.Text);
            selectCommand.Parameters.AddWithValue("@PASSWORD", txtLgnPassword.Text);
            try
            {
                loginConnection.Open();
                SqlDataReader loginReader = selectCommand.ExecuteReader(CommandBehavior.SingleRow);
                if (loginReader.Read())
                {
                    passedLoginID = loginReader["PATIENT_ID"].ToString();
                    passedPrimaryID = loginReader["PRIMARY_ID"].ToString();
                    passedPassword = txtLgnPassword.Text;
                }
                else 
                {
                    MessageBox.Show("Sorry, but it looks like there's no username or password under those entries.", "No Account Found");
                    loginConnection.Close();
                    return false;
                }
            }
            catch (Exception ex) 
            {
                loginConnection.Close();
                return false;
            }
            loginConnection.Close();
            return true;
        }
    }
}
