﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Runtime.InteropServices;

namespace EasyHealthcare
{
    public partial class doctor : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=localhost;Initial Catalog=HospitalManagement;Integrated Security=True");
        protected void Page_Load(object sender, EventArgs e)
        {
            con.Open();

        }

        protected void uploadNote_Click(object sender, EventArgs e)
        {
            string command = "INSERT INTO attachNote(pid, did, medical_note) values(@pID, @dID, @note)";
            string user = Session["userid"].ToString();
            SqlCommand check_PID = new SqlCommand("SELECT COUNT(*) FROM patient WHERE pid='" + pID.Text + "'", con);
            int userExist = (int)check_PID.ExecuteScalar();

            if (userExist != 0)
            {
                SqlCommand cmd = new SqlCommand(command, con);
                cmd.Parameters.Add("@pID", SqlDbType.Int);
                cmd.Parameters["@pID"].Value = pID.Text;
                cmd.Parameters.Add("@dID", SqlDbType.Int);
                cmd.Parameters["@dID"].Value = user;
                cmd.Parameters.Add("@note", SqlDbType.NVarChar);
                cmd.Parameters["@note"].Value = note.Text;
                cmd.ExecuteNonQuery();
                con.Close();
                Label1.Visible = true;
                Label2.Visible = false;
                pID.Text = "";
                note.Text = "";
            }
            else
            {
                Label2.Visible = true;
                Label1.Visible = false;
            }
        }

        protected void View_Click(object sender, EventArgs e)
        {
            SqlCommand check_PID = new SqlCommand("SELECT COUNT(*) FROM patient WHERE pid='" + pID.Text + "'", con);
            int userExist = (int)check_PID.ExecuteScalar();

            if (userExist != 0)
            {
                // PATIENT INFORMATION
                Label2.Visible = false;
                string getId = "SELECT pid FROM PATIENT WHERE pid='" + pID.Text + "'";
                string getFName = "SELECT pFirstname FROM PATIENT WHERE pid='" + pID.Text + "'";
                string getLName = "SELECT pLastname FROM PATIENT WHERE  pid='" + pID.Text + "'";
                string getAddress = "SELECT pAddress FROM PATIENT WHERE pid='" + pID.Text + "'";
                string getPhone = "SELECT pPhone FROM PATIENT WHERE pid='" + pID.Text + "'";
                SqlCommand cmd1 = new SqlCommand(getId, con);
                SqlCommand cmd2 = new SqlCommand(getFName, con);
                SqlCommand cmd3 = new SqlCommand(getLName, con);
                SqlCommand cmd4 = new SqlCommand(getAddress, con);
                SqlCommand cmd5 = new SqlCommand(getPhone, con);
                pIDlabel.Text = cmd1.ExecuteScalar().ToString();
                firstName.Text = cmd2.ExecuteScalar().ToString();
                lastName.Text = cmd3.ExecuteScalar().ToString();
                address.Text = cmd4.ExecuteScalar().ToString();
                phone.Text = cmd5.ExecuteScalar().ToString();


            }

            else
            {
                Label2.Visible = true;
            }
        }
    }
}