using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MyApplicationDB
{
    public partial class Form1 : Form
    {
        SqlConnection myConnectionString = new SqlConnection("Data Source=ZAHIDPARVIZ;Initial Catalog=Car-CRUD-Zahid;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "Press the Button below if you want to connect to Database...";
        }

        private void connect_db_Click(object sender, EventArgs e)
        {
            myConnectionString.Open();

            MessageBox.Show("We are connected to DB");
            MessageBox.Show("We are disconnected to DB");


            myConnectionString.Close();
        }

        private void btnCloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            clearData();
        }

        private void clearData()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            photoPbx.Image = null;

            textBox2.Focus();
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            //It is a filter to open files in OpenDailogueBox
            OpenPhoto.Filter = "jpeg|*.jpg|bmp|*.bmp|all files|*.*";

            //To open a dialogueBox to select for photos
            DialogResult res = OpenPhoto.ShowDialog();

            //selection of Image
            if (res == DialogResult.OK)
            {
                photoPbx.Image = Image.FromFile(OpenPhoto.FileName);
            }
            
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if(textBox2.Text.Length < 1 || textBox3.Text.Length < 1 || textBox4.Text.Length < 1)
            {
                MessageBox.Show("Fill in all the fields");
            }

            else
            {
                try
                {
                    myConnectionString.Open();

                    SqlCommand com = new SqlCommand("insert into Cars(Brand, Model, Year, Photo) values ('"+textBox2.Text+"', '"+textBox3.Text+"', "+Convert.ToInt32(textBox4.Text)+" , '"+photoPbx.Image+"')", myConnectionString);

                    try
                    {
                        com.ExecuteNonQuery();

                        MessageBox.Show("New Car saved");

                        clearData();
                    }

                    catch (Exception exx)
                    {

                        MessageBox.Show("Failed to save data..!\n\nError Details: " + exx);
                    }

                    myConnectionString.Close();
                }

                catch (Exception ex)
                {

                    MessageBox.Show("Failed to connect to DB\n\nError Details: " + ex);
                }
            }
        }
    }
}
