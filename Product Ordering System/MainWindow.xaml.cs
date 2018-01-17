using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
namespace Product_Ordering_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        string cs = @"Data Source=ASHIK\SQLEXPRESS;Initial Catalog=Product_ordering;Integrated Security=True";
        
        private void bgtnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        
        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            if (txt_userid.Text == "" || txt_pass.Password == "" || txtSeckey.Text=="")
            {
                MessageBox.Show("Please provide UserName and Password and Security Key");
                return;
            }
            try
            {
                //Create SqlConnection
                SqlConnection con = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("Select * from loginInfo where username=@username and pass=@pass and sec_key=@sec_key", con);
                cmd.Parameters.AddWithValue("@username", txt_userid.Text);
                cmd.Parameters.AddWithValue("@pass", txt_pass.Password);
                cmd.Parameters.AddWithValue("@sec_key", txtSeckey.Text);
                con.Open();
                SqlDataAdapter adapt = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds);

                con.Close();
                int count = ds.Tables[0].Rows.Count;

                if (count == 1)
                {
                    this.Hide();
                    mainInterface mi = new mainInterface();
                    mi.Show();
                }
                else
                {
                    MessageBox.Show("Login Failed!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
