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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace Product_Ordering_System
{
    /// <summary>
    /// Interaction logic for mainInterface.xaml
    /// </summary>
    public partial class mainInterface : Window
    {
        public mainInterface()
        {
            InitializeComponent();
        }
        string cs = @"Data Source=ASHIK\SQLEXPRESS;Initial Catalog=Product_ordering;Integrated Security=True";
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime iDate = DateTime.Now;
            txtDate.Text = iDate.ToString("dd/mm/yyyy");
            txtTime.Text = iDate.ToString("HH:MM:ss");
            upDate();

            //For Unite Price
            tbUlaptop.Text = String.Format("{0:C}", 600);
            tbUCdesktop.Text = String.Format("{0:C}", 400);
            tbUHdrive.Text = String.Format("{0:C}", 60);
            tbUPro.Text = String.Format("{0:C}", 70);
            tbUMboard.Text = String.Format("{0:C}", 60);
            tbUGcard.Text = String.Format("{0:C}", 45);
            tbUCrom.Text = String.Format("{0:C}", 20);

            //FOr Sub Total

            tbSLaptop.Text = String.Format("{0:C}", 0);
            tbSCdesktop.Text = String.Format("{0:C}", 0);
            tbSHdrive.Text = String.Format("{0:C}", 0);
            tbSPro.Text = String.Format("{0:C}", 0);
            tbSMboard.Text = String.Format("{0:C}", 0);
            tbSGcard.Text = String.Format("{0:C}", 0);
            tbSCrom.Text = String.Format("{0:C}", 0);

            //FOr Total Amount
            tbSubTotal.Text = String.Format("{0:C}", 0);
            tbTax.Text = String.Format("{0:C}", 0);
            tbTotal.Text = String.Format("{0:C}", 0);

            //For Quentity
            txtQLaptop.Text = "0";
            txtQCDesktop.Text = "0";
            txtQHdrive.Text = "0";
            txtQPro.Text = "0";
            txtQMboard.Text = "0";
            txtQGcard.Text = "0";
            txtQCrom.Text = "0";

            //For Coustomer

            txtCname.Text = "";
            txtCphone.Text = "";
            txtOref.Text = "";
        }
        void upDate()
        {
            SqlConnection con = new SqlConnection(cs);
            try
            {
                con.Open();
                String query = "select * from Details";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter dataAddp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Details");
                dataAddp.Fill(dt);
                gridOrder.ItemsSource = dt.DefaultView;
                dataAddp.Update(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DateTime iDate = DateTime.Now;

            txtDate.Text = iDate.ToString("dd/mm/yyyy");
            txtTime.Text = iDate.ToString("HH:MM:ss");
            string Cname = txtCname.Text;
            string Cphone = txtCphone.Text;
            string Oref = txtOref.Text;
            string Date = txtDate.Text;
            string Time = txtTime.Text;
            SqlConnection con = new SqlConnection(cs);

            SqlCommand cmd = new SqlCommand("insert into Details(Customer_Name,Customer_Phone,Order_No,Order_Date,Order_Time) values(@a,@b,@c,@d,@e)", con);

            cmd.Parameters.Add("@a", SqlDbType.VarChar).Value = Cname;
            cmd.Parameters.Add("@b", SqlDbType.Int).Value = Cphone;
            cmd.Parameters.Add("@c", SqlDbType.VarChar).Value = Oref;
            cmd.Parameters.Add("@d", SqlDbType.VarChar).Value = Date;
            cmd.Parameters.Add("@e", SqlDbType.VarChar).Value = Time;


            con.Open();
            int rows = cmd.ExecuteNonQuery();
            if (rows > 0)
                MessageBox.Show("Save");
            con.Close();
            upDate();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            try
            {
                con.Open();
                String query = "select * from Details";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                SqlDataAdapter dataAddp = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Details");
                dataAddp.Fill(dt);
                gridOrder.ItemsSource = dt.DefaultView;
                dataAddp.Update(dt);
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlcon = new SqlConnection(cs);

            string commandstring = "delete from Details where Order_No= @a";
            SqlCommand sqlcmd = new SqlCommand(commandstring, sqlcon);
            sqlcmd.Parameters.Add("@a", SqlDbType.VarChar).Value = txtGrid.Text;
            sqlcon.Open();
            int rows = sqlcmd.ExecuteNonQuery();
            sqlcon.Close();
            upDate();
            if (rows > 0)
                MessageBox.Show("Information Has Deleted.");
        }


        private void btnupdates_Click(object sender, RoutedEventArgs e)
        {
            string Cname = txtCname.Text;
            string Cphone = txtCphone.Text;
            string Date = txtDate.Text;
            string Time = txtTime.Text;
            string Oref = txtOref.Text;
            SqlConnection sqlcon = new SqlConnection(cs);
            sqlcon.Open();
            string commandstring = "update Details set Customer_Name=@p,Customer_Phone=@q,Order_Date=@r,Order_Time=@s,Order_No=@z where Order_No='" + txtGrid.Text + "'";
            SqlCommand sqlcmd = new SqlCommand(commandstring, sqlcon);

            sqlcmd.Parameters.Add("@p", SqlDbType.VarChar).Value = Cname;
            sqlcmd.Parameters.Add("@q", SqlDbType.Int).Value = Cphone;
            sqlcmd.Parameters.Add("@z", SqlDbType.VarChar).Value = Oref;
            sqlcmd.Parameters.Add("@r", SqlDbType.VarChar).Value = Date;
            sqlcmd.Parameters.Add("@s", SqlDbType.VarChar).Value = Time;
            int rows = sqlcmd.ExecuteNonQuery();
            sqlcon.Close();
            if (rows == 1)
                MessageBox.Show("Information Has Updated.");
            upDate();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            //For Quentity
            txtQLaptop.Text = "0";
            txtQCDesktop.Text = "0";
            txtQHdrive.Text = "0";
            txtQPro.Text = "0";
            txtQMboard.Text = "0";
            txtQGcard.Text = "0";
            txtQCrom.Text = "0";

            //For Unite Price
            tbUlaptop.Text = String.Format("{0:C}", 600);
            tbUCdesktop.Text = String.Format("{0:C}", 400);
            tbUHdrive.Text = String.Format("{0:C}", 60);
            tbUPro.Text = String.Format("{0:C}", 70);
            tbUMboard.Text = String.Format("{0:C}", 60);
            tbUGcard.Text = String.Format("{0:C}", 45);
            tbUCrom.Text = String.Format("{0:C}", 20);

            //FOr Sub Total

            tbSLaptop.Text = String.Format("{0:C}", 0);
            tbSCdesktop.Text = String.Format("{0:C}", 0);
            tbSHdrive.Text = String.Format("{0:C}", 0);
            tbSPro.Text = String.Format("{0:C}", 0);
            tbSMboard.Text = String.Format("{0:C}", 0);
            tbSGcard.Text = String.Format("{0:C}", 0);
            tbSCrom.Text = String.Format("{0:C}", 0);

            //FOr Total Amount
            tbSubTotal.Text = String.Format("{0:C}", 0);
            tbTax.Text = String.Format("{0:C}", 0);
            tbTotal.Text = String.Format("{0:C}", 0);

            //For Coustomer

            txtCname.Text = "";
            txtCphone.Text = "";
            txtOref.Text = "";

        }

        private void btnReceipt_Click(object sender, RoutedEventArgs e)
        {
            rtfReceipt.AppendText("\t\t\t"+"Test Shopping System");
            rtfReceipt.AppendText("\t\t\t"+ "=========================================================" + Environment.NewLine);
            rtfReceipt.AppendText("\t" +"Customer Name: " +txtCname.Text+Environment.NewLine);
            rtfReceipt.AppendText("\t" + "Customer phone: " + txtCphone.Text + Environment.NewLine);
            rtfReceipt.AppendText("\t" + "Order No: " + txtOref.Text + Environment.NewLine);
            rtfReceipt.AppendText("\t"+"Item Type"+"\t\t"+"Quantity"+"\t"+"Unite price"+"\t"+"Sub Total");
            rtfReceipt.AppendText("\t\t\t" + "=========================================================" + Environment.NewLine);
            rtfReceipt.AppendText("\t"+ "Laptop"+"\t\t\t"+txtQLaptop.Text+"\t\t"+ tbUlaptop.Text + "\t\t"+ tbSLaptop.Text+Environment.NewLine);
            rtfReceipt.AppendText("\t" + "Desktop" + "\t\t\t" + txtQCDesktop.Text + "\t\t" + tbUCdesktop.Text + "\t\t" + tbSCdesktop.Text + Environment.NewLine);
            rtfReceipt.AppendText("\t" + "Hard Drive" + "\t\t" + txtQHdrive.Text + "\t\t" + tbUlaptop.Text + "\t\t" + tbSLaptop.Text + Environment.NewLine);
            rtfReceipt.AppendText("\t" + "Processore" + "\t\t" + txtQPro.Text + "\t\t" + tbUPro.Text + "\t\t" + tbSPro.Text + Environment.NewLine);
            rtfReceipt.AppendText("\t" + "Mother Board" + "\t\t" + txtQMboard.Text + "\t\t" + tbUMboard.Text + "\t\t" + tbSMboard.Text + Environment.NewLine);
            rtfReceipt.AppendText("\t" + "Graphics Card" + "\t\t" + txtQGcard.Text + "\t\t" + tbUGcard.Text + "\t\t" + tbSGcard.Text + Environment.NewLine);
            rtfReceipt.AppendText("\t" + "CD/DvD Rom" + "\t\t" + txtQCrom.Text + "\t\t" + tbUCrom.Text + "\t\t" + tbSCrom.Text + Environment.NewLine);
            rtfReceipt.AppendText("\t\t\t" + "=========================================================" + Environment.NewLine);
            rtfReceipt.AppendText("\t" + "Sub Total" + "\t\t\t\t\t\t" + tbSubTotal.Text + Environment.NewLine);
            rtfReceipt.AppendText("\t" + "tax" + "\t\t\t\t\t\t\t" + tbTax.Text + Environment.NewLine);
            rtfReceipt.AppendText("\t\t\t" + "=========================================================" + Environment.NewLine);
            rtfReceipt.AppendText("\t" + "Total Amount" + "\t\t\t\t\t\t" + tbSubTotal.Text + Environment.NewLine);
            rtfReceipt.AppendText("\t" + "Date:"+"\t" +txtDate.Text+"\t\t\t\t" +"Time:"+"\t" + txtTime.Text + Environment.NewLine);



        }

        private void txtQLaptop_GotFocus(object sender, RoutedEventArgs e)
        {
            txtQLaptop.Text = "";
            txtQLaptop.Focus();
        }

        private void txtQCDesktop_GotFocus(object sender, RoutedEventArgs e)
        {
            txtQCDesktop.Text = "";
            txtQCDesktop.Focus();
        }

        private void txtQHdrive_GotFocus(object sender, RoutedEventArgs e)
        {
            txtQHdrive.Text = "";
            txtQHdrive.Focus();
        }

        private void txtQPro_GotFocus(object sender, RoutedEventArgs e)
        {
            txtQPro.Text = "";
            txtQPro.Focus();
        }

        private void txtQMboard_GotFocus(object sender, RoutedEventArgs e)
        {
            txtQMboard.Text = "";
            txtQMboard.Focus();
        }

        private void txtQGcard_GotFocus(object sender, RoutedEventArgs e)
        {
            txtQGcard.Text = "";
            txtQGcard.Focus();
        }

        private void txtQCrom_GotFocus(object sender, RoutedEventArgs e)
        {
            txtQCrom.Text = "";
            txtQCrom.Focus();
        }

        private void btnTotal_Click(object sender, RoutedEventArgs e)
        {
            double tax;
            tax = .45;

            double laptop, Cdesktop, Hdrive, Pro, Mboard, Gcard, Crom;
            laptop = 600; Cdesktop = 400; Hdrive = 60; Pro = 70; Mboard = 60; Gcard = 45; Crom=20;

            double  l_aptop= Convert.ToDouble(txtQLaptop.Text);
            double  C_desktop = Convert.ToDouble(txtQCDesktop.Text);
            double  H_drive = Convert.ToDouble(txtQHdrive.Text);
            double  Pro_s = Convert.ToDouble(txtQPro.Text);
            double  M_board = Convert.ToDouble(txtQMboard.Text);
            double  G_card = Convert.ToDouble(txtQGcard.Text);
            double  C_rom = Convert.ToDouble(txtQCrom.Text);

            Order sell_product = new Order(l_aptop, C_desktop, H_drive, Pro_s, M_board, G_card, C_rom);

            double Subl_aptop = (l_aptop * laptop);
            double SubC_desktop = (C_desktop * Cdesktop);
            double SubH_drive = (H_drive * Hdrive);
            double SubPro_s = (Pro_s * Pro);
            double SubM_board = (M_board * Mboard);
            double SubG_card = (G_card * Gcard);
            double SubC_rom = (C_rom * Crom);

            tbSLaptop.Text = Convert.ToString(Subl_aptop);
            tbSCdesktop.Text = Convert.ToString(SubC_desktop);
            tbSHdrive.Text = Convert.ToString(SubH_drive);
            tbSPro.Text = Convert.ToString(SubPro_s);
            tbSMboard.Text = Convert.ToString(SubM_board);
            tbSGcard.Text = Convert.ToString(SubG_card);
            tbSCrom.Text = Convert.ToString(SubC_rom);

            tbSLaptop.Text = String.Format("{0:C}", Subl_aptop);
            tbSCdesktop.Text = String.Format("{0:C}", SubC_desktop);
            tbSHdrive.Text = String.Format("{0:C}", SubH_drive);
            tbSPro.Text = String.Format("{0:C}", SubPro_s);
            tbSMboard.Text = String.Format("{0:C}", SubM_board);
            tbSGcard.Text = String.Format("{0:C}", SubG_card);
            tbSCrom.Text = String.Format("{0:C}", SubC_rom);

            double product = (Subl_aptop + SubC_desktop + SubH_drive + SubPro_s + SubM_board + SubG_card + SubC_rom);


            tbSubTotal.Text = Convert.ToString(product);
            tbTax.Text = Convert.ToString((product * tax) / 100);

            double iTax = Convert.ToDouble(tbTax.Text);
            tbTotal.Text = Convert.ToString(product + iTax);

            tbSubTotal.Text = String.Format("{0:C}", product);
            tbTax.Text = String.Format("{0:C}", ((product * tax) / 100));
            tbTotal.Text = String.Format("{0:C}", (product + iTax));


        }
    }
}
