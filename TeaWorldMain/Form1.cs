using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeaWorldMain
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBTeaWorld"].ConnectionString;

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
            {
                sqlConnection.Close();
            }
        }

        //1.PAYMENTMETHOD
        //View
        private async void tabPage1_Click(object sender, EventArgs e)
        {
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.View = View.Details;

            listView1.Columns.Add("PaymentMethodID");
            listView1.Columns.Add("PaymentMethodName");

            await LoadPaymentMethodAsync();
        }

        private async Task LoadPaymentMethodAsync()
        {
            SqlDataReader sqlReader = null;

            SqlCommand getPaymentMethod = new SqlCommand(" SELECT * FROM [TeaWorld].[PaymentMethod]", sqlConnection);

            try
            {
                sqlReader = await getPaymentMethod.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[] {

                        Convert.ToString(sqlReader["PaymentMethodID"]),
                         Convert.ToString(sqlReader["PaymentMethodName"]),

                    });

                    listView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();

            await LoadPaymentMethodAsync();
        }

        //ADD
        private async void button2_Click(object sender, EventArgs e)
        {
            SqlCommand commandInsertPaymentMethod = new SqlCommand(" Insert into [TeaWorld].[PaymentMethod] (PaymentMethodID, PaymentMethodName) " +
                "Values (@PaymentMethodID, @PaymentMethodName)",
             sqlConnection);

            commandInsertPaymentMethod.Parameters.AddWithValue("PaymentMethodID", Convert.ToInt16(textBox1.Text));
            commandInsertPaymentMethod.Parameters.AddWithValue("PaymentMethodName", textBox2.Text);

            try
            {
                await commandInsertPaymentMethod.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //2.CUSTOMERS
        //View
        private async void tabPage2_Click(object sender, EventArgs e)
        {
            listView2.GridLines = true;
            listView2.FullRowSelect = true;
            listView2.View = View.Details;

            listView2.Columns.Add("CustomerID");
            listView2.Columns.Add("FirstName");
            listView2.Columns.Add("LastName");
            listView2.Columns.Add("PhoneNumber");
            listView2.Columns.Add("EDRPOU");
            listView2.Columns.Add("BIC");
            listView2.Columns.Add("IBAN");

            await LoadCustomersAsync();
        }

        private async Task LoadCustomersAsync()
        {
            SqlDataReader sqlReader = null;

            SqlCommand getCustomers = new SqlCommand(" SELECT * FROM [TeaWorld].[Customers]", sqlConnection);

            try
            {
                sqlReader = await getCustomers.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[] {

                        Convert.ToString(sqlReader["CustomerID"]),
                         Convert.ToString(sqlReader["FirstName"]),
                          Convert.ToString(sqlReader["LastName"]),
                            Convert.ToString(sqlReader["PhoneNumber"]),
                             Convert.ToString(sqlReader["EDRPOU"]),
                              Convert.ToString(sqlReader["BIC"]),
                              Convert.ToString(sqlReader["IBAN"]),

                    });

                    listView2.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            listView2.Items.Clear();

            await LoadCustomersAsync();
        }

        //ADD
        private async void button4_Click(object sender, EventArgs e)
        {
            SqlCommand commandCustomers = new SqlCommand(" Insert into [TeaWorld].[Customers] (CustomerID, FirstName, LastName, PhoneNumber, EDRPOU, BIC, IBAN) VALUES (@CustomerID, @FirstName, @LastName, @PhoneNumber, @EDRPOU, @BIC, @IBAN )",
             sqlConnection);

            commandCustomers.Parameters.AddWithValue("CustomerID", Convert.ToInt16(textBox3.Text));
            commandCustomers.Parameters.AddWithValue("FirstName", textBox4.Text);
            commandCustomers.Parameters.AddWithValue("LastName", textBox5.Text);
            commandCustomers.Parameters.AddWithValue("PhoneNumber", textBox6.Text);
            commandCustomers.Parameters.AddWithValue("EDRPOU", textBox7.Text);
            commandCustomers.Parameters.AddWithValue("BIC", Convert.ToInt32(textBox8.Text));
            commandCustomers.Parameters.AddWithValue("IBAN", textBox9.Text);

            try
            {
                await commandCustomers.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //3.EMPLOYEES
        //VIEW
        private async void tabPage3_Click(object sender, EventArgs e)
        {
            listView3.GridLines = true;
            listView3.FullRowSelect = true;
            listView3.View = View.Details;

            listView3.Columns.Add("EmployeeID");
            listView3.Columns.Add("FirstName");
            listView3.Columns.Add("LastName");
            listView3.Columns.Add("PhoneNumber");
            listView3.Columns.Add("Address");

            await LoadEmployeesAsync();
        }

        private async Task LoadEmployeesAsync()
        {
            SqlDataReader sqlReader = null;

            SqlCommand getEmployees = new SqlCommand(" SELECT * FROM [dbo].[Employees]", sqlConnection);

            try
            {
                sqlReader = await getEmployees.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[] {

                        Convert.ToString(sqlReader["EmployeeId"]),
                         Convert.ToString(sqlReader["FirstName"]),
                          Convert.ToString(sqlReader["LastName"]),
                            Convert.ToString(sqlReader["PhoneNumber"]),
                             Convert.ToString(sqlReader["Address"]),

                    });

                    listView3.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            listView3.Items.Clear();

            await LoadEmployeesAsync();
        }



        //ADD
        private async void button6_Click(object sender, EventArgs e)
        {
            SqlCommand commandInsertEmployees = new SqlCommand(" INSERT INTO [dbo].[Employees] (EmployeeID, FirstName, LastName, PhoneNumber, Address)  VALUES (@EmployeeID, @FirstName, @LastName, @PhoneNumber, @Address) ",
             sqlConnection);

            commandInsertEmployees.Parameters.AddWithValue("EmployeeID", Convert.ToInt16(textBox10.Text));
            commandInsertEmployees.Parameters.AddWithValue("FirstName", textBox11.Text);
            commandInsertEmployees.Parameters.AddWithValue("LastName", textBox12.Text);
            commandInsertEmployees.Parameters.AddWithValue("PhoneNumber", textBox13.Text);
            commandInsertEmployees.Parameters.AddWithValue("Address", Convert.ToString(textBox14.Text));

            try
            {
                await commandInsertEmployees.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //4.ORDERS
        //VIEW
        private async void tabPage4_Click(object sender, EventArgs e)
        {
            listView4.GridLines = true;
            listView4.FullRowSelect = true;
            listView4.View = View.Details;

            listView4.Columns.Add("OrderID");
            listView4.Columns.Add("CustomerID");
            listView4.Columns.Add("EmployeeID");
            listView4.Columns.Add("PaymentDate");
            listView4.Columns.Add("DateTimeOrder");
            listView4.Columns.Add("PaymentMethodID");

            await LoadOrdersAsync();
        }

        private async Task LoadOrdersAsync()
        {
            SqlDataReader sqlReader = null;

            SqlCommand getOrders = new SqlCommand(" SELECT * FROM [dbo].[Orders]", sqlConnection);

            try
            {
                sqlReader = await getOrders.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[] {

                        Convert.ToString(sqlReader["OrderID"]),
                         Convert.ToString(sqlReader["CustomerID"]),
                          Convert.ToString(sqlReader["EmployeeID"]),
                           Convert.ToString(sqlReader["PaymentDate"]),
                            Convert.ToString(sqlReader["DateTimeOrder"]),
                             Convert.ToString(sqlReader["PaymentMethodID"]),

                    });

                    listView4.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            listView4.Items.Clear();

            await LoadOrdersAsync();
        }

        //ADD
        private async void button7_Click(object sender, EventArgs e)
        {
            SqlCommand commandInsertOrders = new SqlCommand(" INSERT INTO [dbo].[Orders] (OrderID, CustomerID, EmployeeID, PaymentDate, DateTimeOrder, PaymentMethodID) VALUES (@OrderID, @CustomerID, @EmployeeID, @PaymentDate, @DateTimeOrder, @PaymentMethodID) ",
             sqlConnection);

            commandInsertOrders.Parameters.AddWithValue("OrderID", Convert.ToInt64(textBox19.Text));
            commandInsertOrders.Parameters.AddWithValue("CustomerID", Convert.ToInt64(textBox18.Text));
            commandInsertOrders.Parameters.AddWithValue("EmployeeID", Convert.ToInt64(textBox17.Text));
            commandInsertOrders.Parameters.AddWithValue("PaymentDate", Convert.ToDateTime(textBox16.Text));
            commandInsertOrders.Parameters.AddWithValue("DateTimeOrder", Convert.ToDateTime(textBox15.Text));
            commandInsertOrders.Parameters.AddWithValue("PaymentMethodID", Convert.ToInt64(textBox20.Text));

            try
            {
                await commandInsertOrders.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //5. TeaType
        //VIEW
        private async void tabPage5_Click(object sender, EventArgs e)
        {
            listView5.GridLines = true;
            listView5.FullRowSelect = true;
            listView5.View = View.Details;

            listView5.Columns.Add("TeaTypeID");
            listView5.Columns.Add("TeaTypeName");


            await LoadTeaTypeAsync();
        }

        private async Task LoadTeaTypeAsync()
        {
            SqlDataReader sqlReader = null;

            SqlCommand getTeaType = new SqlCommand(" SELECT * FROM [dbo].[TeaType]", sqlConnection);

            try
            {
                sqlReader = await getTeaType.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[] {

                        Convert.ToString(sqlReader["TeaTypeID"]),
                         Convert.ToString(sqlReader["TeaTypeName"]),
                    });

                    listView5.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }
        }

        private async void button10_Click(object sender, EventArgs e)
        {
            listView5.Items.Clear();

            await LoadTeaTypeAsync();
        }

        //ADD
        private async void button9_Click(object sender, EventArgs e)
        {
            SqlCommand commandInsertTeatype = new SqlCommand(" INSERT INTO [dbo].[TeaType] (TeaTypeID, TeaTypeName) VALUES (@TeaTypeID, @TeaTypeName)",
             sqlConnection);

            commandInsertTeatype.Parameters.AddWithValue("TeaTypeID", Convert.ToInt64(textBox26.Text));
            commandInsertTeatype.Parameters.AddWithValue("TeaTypeName", textBox25.Text);

            try
            {
                await commandInsertTeatype.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //COUNTRIES
        //View
        private async void tabPage6_Click(object sender, EventArgs e)
        {
            listView6.GridLines = true;
            listView6.FullRowSelect = true;
            listView6.View = View.Details;

            listView6.Columns.Add("CountryID");
            listView6.Columns.Add("CountryName");


            await LoadCountryAsync();
        }

        private async Task LoadCountryAsync()
        {
            SqlDataReader sqlReader = null;

            SqlCommand getCountry = new SqlCommand(" SELECT * FROM [dbo].[Countries]", sqlConnection);

            try
            {
                sqlReader = await getCountry.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[] {

                        Convert.ToString(sqlReader["CountryID"]),
                         Convert.ToString(sqlReader["CountryName"]),
                    });

                    listView6.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }
        }

        private async void button12_Click(object sender, EventArgs e)
        {
            listView6.Items.Clear();

            await LoadCountryAsync();
        }

        //add
        private async void button11_Click(object sender, EventArgs e)
        {
            SqlCommand commandInsertCountry = new SqlCommand(" INSERT INTO [dbo].[Countries] (CountryID, CountryName) VALUES (@CountryID, @CountryName)",
             sqlConnection);

            commandInsertCountry.Parameters.AddWithValue("CountryID", Convert.ToInt64(textBox22.Text));
            commandInsertCountry.Parameters.AddWithValue("CountryName", textBox21.Text);

            try
            {
                await commandInsertCountry.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        //Brand
        //View
        private async void tabPage7_Click(object sender, EventArgs e)
        {
            listView7.GridLines = true;
            listView7.FullRowSelect = true;
            listView7.View = View.Details;

            listView7.Columns.Add("BrandID");
            listView7.Columns.Add("BrandName");


            await LoadBrandAsync();
        }

        private async Task LoadBrandAsync()
        {
            SqlDataReader sqlReader = null;

            SqlCommand getBrands = new SqlCommand(" SELECT * FROM [dbo].[Brand]", sqlConnection);

            try
            {
                sqlReader = await getBrands.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[] {

                        Convert.ToString(sqlReader["BrandID"]),
                         Convert.ToString(sqlReader["BrandName"]),
                    });

                    listView7.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }
        }

        private async void button14_Click(object sender, EventArgs e)
        {
            listView7.Items.Clear();

            await LoadBrandAsync();
        }

        //add
        private async void button13_Click(object sender, EventArgs e)
        {
            SqlCommand commandInsertBrand = new SqlCommand(" INSERT INTO [dbo].[Brand] (BrandID, BrandName) VALUES (@BrandID, @BrandName)",
             sqlConnection);

            commandInsertBrand.Parameters.AddWithValue("BrandID", Convert.ToInt64(textBox24.Text));
            commandInsertBrand.Parameters.AddWithValue("BrandName", textBox23.Text);

            try
            {
                await commandInsertBrand.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //WEIGHT
        //VIEW
        private async void tabPage8_Click(object sender, EventArgs e)
        {
            listView8.GridLines = true;
            listView8.FullRowSelect = true;
            listView8.View = View.Details;

            listView8.Columns.Add("WeightID");
            listView8.Columns.Add("WeightInGrams");


            await LoadWeightAsync();
        }

        private async Task LoadWeightAsync()
        {
            SqlDataReader sqlReader = null;

            SqlCommand getWeight = new SqlCommand(" SELECT * FROM [dbo].[Weight]", sqlConnection);

            try
            {
                sqlReader = await getWeight.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[] {

                        Convert.ToString(sqlReader["WeightID"]),
                         Convert.ToString(sqlReader["WeightInGrams"]),
                    });

                    listView8.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }
        }

        private async void button16_Click(object sender, EventArgs e)
        {
            listView8.Items.Clear();

            await LoadWeightAsync();
        }

        //add
        private async void button15_Click(object sender, EventArgs e)
        {
            SqlCommand commandInsertWeight = new SqlCommand(" INSERT INTO [dbo].[Weight] (WeightID, WeightInGrams) VALUES (@WeightID, @WeightInGrams)",
             sqlConnection);

            commandInsertWeight.Parameters.AddWithValue("WeightID", Convert.ToInt64(textBox28.Text));
            commandInsertWeight.Parameters.AddWithValue("WeightInGrams", Convert.ToInt64(textBox27.Text));

            try
            {
                await commandInsertWeight.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //COMPONENTS
        //view
        private async void tabPage9_Click(object sender, EventArgs e)
        {
            listView9.GridLines = true;
            listView9.FullRowSelect = true;
            listView9.View = View.Details;

            listView9.Columns.Add("ComponentID");
            listView9.Columns.Add("Componentname");


            await LoadComponentsAsync();
        }

        private async Task LoadComponentsAsync()
        {
            SqlDataReader sqlReader = null;

            SqlCommand getComponents = new SqlCommand(" SELECT * FROM [dbo].[Component]", sqlConnection);

            try
            {
                sqlReader = await getComponents.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[] {

                        Convert.ToString(sqlReader["ComponentID"]),
                         Convert.ToString(sqlReader["ComponentName"]),
                    });

                    listView9.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }
        }

        private async void button18_Click(object sender, EventArgs e)
        {
            listView9.Items.Clear();

            await LoadComponentsAsync();
        }

        //add
        private async void button17_Click(object sender, EventArgs e)
        {
            SqlCommand commandInsertComponent = new SqlCommand(" INSERT INTO [dbo].[Component] (ComponentID, ComponentName) VALUES (@ComponentID, @ComponentName)",
             sqlConnection);

            commandInsertComponent.Parameters.AddWithValue("ComponentID", Convert.ToInt64(textBox30.Text));
            commandInsertComponent.Parameters.AddWithValue("ComponentName", textBox29.Text);

            try
            {
                await commandInsertComponent.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //LeafType
        //view
        private async void tabPage10_Click(object sender, EventArgs e)
        {
            listView10.GridLines = true;
            listView10.FullRowSelect = true;
            listView10.View = View.Details;

            listView10.Columns.Add("ComponentID");
            listView10.Columns.Add("Componentname");


            await LoadLeafTypeAsync();
        }

        private async Task LoadLeafTypeAsync()
        {
            SqlDataReader sqlReader = null;

            SqlCommand getLeafType = new SqlCommand(" SELECT * FROM [dbo].[LeafType]", sqlConnection);

            try
            {
                sqlReader = await getLeafType.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[] {

                        Convert.ToString(sqlReader["LeafTypeID"]),
                         Convert.ToString(sqlReader["LeafTypeName"]),
                    });

                    listView10.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }
        }

        private async void button20_Click(object sender, EventArgs e)
        {
            listView10.Items.Clear();

            await LoadLeafTypeAsync();
        }

        //add
        private async void button19_Click(object sender, EventArgs e)
        {
            SqlCommand commandInsertComponent = new SqlCommand(" INSERT INTO [dbo].[LeafType] (LeafTypeID, LeafTypeName) VALUES (@LeafTypeID, @LeafTypeName)",
             sqlConnection);

            commandInsertComponent.Parameters.AddWithValue("LeafTypeID", Convert.ToInt64(textBox32.Text));
            commandInsertComponent.Parameters.AddWithValue("LeafTypeName", textBox31.Text);

            try
            {
                await commandInsertComponent.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //CATALOG
        //View
        private async void tabPage11_Click(object sender, EventArgs e)
        {
            listView11.GridLines = true;
            listView11.FullRowSelect = true;
            listView11.View = View.Details;
            listView11.Columns.Add("TeaID");
            listView11.Columns.Add("TeaName");
            listView11.Columns.Add("TeaTypeID");
            listView11.Columns.Add("CountryID");
            listView11.Columns.Add("LeafTypeID");
            listView11.Columns.Add("TeaContentsID");
            listView11.Columns.Add("Price");
            listView11.Columns.Add("WeightID");
            listView11.Columns.Add("BrandID");

            await LoadCatalogAsync();
        }

        private async Task LoadCatalogAsync()
        {
            SqlDataReader sqlReader = null;

            SqlCommand getCatalog = new SqlCommand(" SELECT * FROM [dbo].[Catalog]", sqlConnection);

            try
            {
                sqlReader = await getCatalog.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[] {

                        Convert.ToString(sqlReader["TeaID"]),
                         Convert.ToString(sqlReader["TeaName"]),
                         Convert.ToString(sqlReader["TeaTypeID"]),
                         Convert.ToString(sqlReader["CountryID"]),
                         Convert.ToString(sqlReader["LeafTypeID"]),
                         Convert.ToString(sqlReader["TeaContentsID"]),
                         Convert.ToString(sqlReader["Price"]),
                         Convert.ToString(sqlReader["WeightID"]),
                         Convert.ToString(sqlReader["BrandID"]),
                    });

                    listView11.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }
        }

        private async void button22_Click(object sender, EventArgs e)
        {
            listView11.Items.Clear();

            await LoadCatalogAsync();
        }

        //add
        private async void button21_Click(object sender, EventArgs e)
        {
            SqlCommand commandInsertCatalog = new SqlCommand(@"INSERT INTO [dbo].[Catalog] 
                                                (TeaID, TeaName, TeaTypeID, CountryID, LeafTypeID, TeaContentsID, Price, WeightID, BrandID) 
                                                VALUES (@TeaID, @TeaName, @TeaTypeID, @CountryID, @LeafTypeID, @TeaContentsID, @Price, @WeightID, @BrandID)",
                                                    sqlConnection);

            commandInsertCatalog.Parameters.AddWithValue("TeaID", Convert.ToInt64(textBox39.Text));
            commandInsertCatalog.Parameters.AddWithValue("TeaName", textBox38.Text);
            commandInsertCatalog.Parameters.AddWithValue("TeaTypeID", Convert.ToInt64(textBox37.Text));
            commandInsertCatalog.Parameters.AddWithValue("CountryID", Convert.ToInt64(textBox36.Text));
            commandInsertCatalog.Parameters.AddWithValue("LeafTypeID", Convert.ToInt64(textBox35.Text));

            if (textBox34.Text != "")
            {
                commandInsertCatalog.Parameters.AddWithValue("TeaContentsID", Convert.ToInt64(textBox34.Text));
            }
            else
            {
                commandInsertCatalog.Parameters.AddWithValue("TeaContentsID", DBNull.Value);
            }

            decimal price;


            if (textBox41.Text != "")
            {
                if (Decimal.TryParse(textBox41.Text, out price))
                {
                    commandInsertCatalog.Parameters.AddWithValue("Price", price);
                }
                else
                {
                    MessageBox.Show("Неправильний формат ціни.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                commandInsertCatalog.Parameters.AddWithValue("Price", DBNull.Value);
            }

            commandInsertCatalog.Parameters.AddWithValue("WeightID", Convert.ToInt64(textBox40.Text));
            commandInsertCatalog.Parameters.AddWithValue("BrandID", Convert.ToInt64(textBox33.Text));

            try
            {
                await commandInsertCatalog.ExecuteNonQueryAsync();
                MessageBox.Show("Дані успішно додано!", "Успіх", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //TeaContents
        //View
        private async void tabPage12_Click(object sender, EventArgs e)
        {
            listView12.GridLines = true;
            listView12.FullRowSelect = true;
            listView12.View = View.Details;

            listView12.Columns.Add("TeaContensID");
            listView12.Columns.Add("TeaID");


            await LoadTeaContentsAsync();
        }

        private async Task LoadTeaContentsAsync()
        {
            SqlDataReader sqlReader = null;

            SqlCommand getTeaContents = new SqlCommand(" SELECT * FROM [dbo].[TeaContents]", sqlConnection);

            try
            {
                sqlReader = await getTeaContents.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[] {

                        Convert.ToString(sqlReader["TeaContentsID"]),
                         Convert.ToString(sqlReader["TeaID"]),
                    });

                    listView12.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }
        }

        private async void button24_Click(object sender, EventArgs e)
        {
            listView12.Items.Clear();

            await LoadTeaContentsAsync();
        }

        //add
        private async void button23_Click(object sender, EventArgs e)
        {
            SqlCommand commandInsertTeaContents = new SqlCommand(" INSERT INTO [dbo].[TeaContents] (TeaContentsID, TeaID) VALUES (@TeaContentsID, @TeaID)",
             sqlConnection);

            commandInsertTeaContents.Parameters.AddWithValue("TeaContentsID", Convert.ToInt64(textBox43.Text));
            commandInsertTeaContents.Parameters.AddWithValue("TeaID", Convert.ToInt64(textBox42.Text));

            try
            {
                await commandInsertTeaContents.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        //TeaContentsComponents
        //view
        private async void tabPage13_Click(object sender, EventArgs e)
        {
            listView13.GridLines = true;
            listView13.FullRowSelect = true;
            listView13.View = View.Details;

            listView13.Columns.Add("TeaContentsComponentsID");
            listView13.Columns.Add("TeaContensID");
            listView13.Columns.Add("ComponentID");


            await LoadTeaContentsComponentsAsync();
        }

        private async Task LoadTeaContentsComponentsAsync()
        {
            SqlDataReader sqlReader = null;

            SqlCommand getTeaContentsComponents = new SqlCommand(" SELECT * FROM [dbo].[TeaContentsComponents]", sqlConnection);

            try
            {
                sqlReader = await getTeaContentsComponents.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[] {

                        Convert.ToString(sqlReader["TeaContentsComponentsID"]),
                         Convert.ToString(sqlReader["TeaContentsID"]),
                         Convert.ToString(sqlReader["ComponentID"]),
                    });

                    listView13.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }
        }

        private async void button26_Click(object sender, EventArgs e)
        {
            listView13.Items.Clear();

            await LoadTeaContentsComponentsAsync();
        }

        //add
        private async void button25_Click(object sender, EventArgs e)
        {
            SqlCommand commandInsertTeaContentsComponents = new SqlCommand(@"INSERT INTO [dbo].[TeaContentsComponents] (TeaContentsComponentsID, TeaContentsID, ComponentID)
                                                       VALUES (@TeaContentsComponentsID, @TeaContentsID, @ComponentID)",
                                                           sqlConnection);

            commandInsertTeaContentsComponents.Parameters.AddWithValue("TeaContentsComponentsID", Convert.ToInt64(textBox45.Text));
            commandInsertTeaContentsComponents.Parameters.AddWithValue("TeaContentsID", Convert.ToInt64(textBox44.Text));
            commandInsertTeaContentsComponents.Parameters.AddWithValue("ComponentID", Convert.ToInt64(textBox46.Text));

            try
            {
                await commandInsertTeaContentsComponents.ExecuteNonQueryAsync();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //ContentOfTheOrder
        //view
        private async void tabPage14_Click(object sender, EventArgs e)
        {
            listView14.GridLines = true;
            listView14.FullRowSelect = true;
            listView14.View = View.Details;

            listView14.Columns.Add("OrderID");
            listView14.Columns.Add("TeaID");
            listView14.Columns.Add("Quantity");


            await LoadContentOfTheOrderAsync();
        }

        private async Task LoadContentOfTheOrderAsync()
        {
            SqlDataReader sqlReader = null;

            SqlCommand getTeaContentOfTheOrder = new SqlCommand(" SELECT * FROM [dbo].[ContentOfTheOrder]", sqlConnection);

            try
            {
                sqlReader = await getTeaContentOfTheOrder.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[] {

                        Convert.ToString(sqlReader["OrderID"]),
                         Convert.ToString(sqlReader["TeaID"]),
                         Convert.ToString(sqlReader["Quantity"]),
                    });

                    listView14.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }
        }

        private async void button28_Click(object sender, EventArgs e)
        {
            listView14.Items.Clear();

            await LoadContentOfTheOrderAsync();
        }

        //add
        private async void button27_Click(object sender, EventArgs e)
        {
            SqlCommand commandInsertTeaContentsComponents = new SqlCommand(@"INSERT INTO [dbo].[ContentOfTheOrder] (OrderID, TeaID, Quantity)
                                                       VALUES (@OrderID, @TeaID, @Quantity)",
                                                           sqlConnection);

            commandInsertTeaContentsComponents.Parameters.AddWithValue("OrderID", Convert.ToInt64(textBox49.Text));
            commandInsertTeaContentsComponents.Parameters.AddWithValue("TeaID", Convert.ToInt64(textBox48.Text));
            commandInsertTeaContentsComponents.Parameters.AddWithValue("Quantity", Convert.ToInt64(textBox47.Text));

            try
            {
                await commandInsertTeaContentsComponents.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Analytics
        //view
        private async void tbutton29_Click(object sender, EventArgs e)
        {
            listView15.GridLines = true;
            listView15.FullRowSelect = true;
            listView15.View = View.Details;

            listView15.Columns.Add("TeaID");
            listView15.Columns.Add("TeaName");
            listView15.Columns.Add("TeaTypeID");
            listView15.Columns.Add("CountryID");
            listView15.Columns.Add("LeafTypeID");
            listView15.Columns.Add("TeaContentsID");
            listView15.Columns.Add("Price");
            listView15.Columns.Add("WeightID");
            listView15.Columns.Add("BrandID");
            listView15.Columns.Add("ComponentID");
            listView15.Columns.Add("ComponentName");

            await LoadCatalogAndComponentsAsync();
        }

        private async Task LoadCatalogAndComponentsAsync()
        {
            SqlDataReader sqlReader = null;

            SqlCommand getCatalogAndComponents = new SqlCommand("SELECT c.TeaID,c.TeaName,c.TeaTypeID,c.CountryID,c.LeafTypeID,c.TeaContentsID,c.Price,c.WeightID,c.BrandID,\r\n  " +
                " comp.ComponentID,\r\n    comp.ComponentName\r\nFROM Catalog c\r\nLEFT JOIN (\r\n    SELECT tcc.TeaContentsID,\r\n        comp.ComponentID,\r\n        comp.ComponentName\r\n" +
                "    FROM TeaContentsComponents tcc\r\n    JOIN Component comp ON tcc.ComponentID = comp.ComponentID\r\n)" +
                " AS comp ON c.TeaContentsID = comp.TeaContentsID; ", sqlConnection);

            try
            {
                sqlReader = await getCatalogAndComponents.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[] {

                        Convert.ToString(sqlReader["TeaID"]),
                         Convert.ToString(sqlReader["TeaName"]),
                         Convert.ToString(sqlReader["TeaTypeID"]),
                         Convert.ToString(sqlReader["CountryID"]),
                         Convert.ToString(sqlReader["LeafTypeID"]),
                         Convert.ToString(sqlReader["TeaContentsID"]),
                         Convert.ToString(sqlReader["Price"]),
                         Convert.ToString(sqlReader["WeightID"]),
                         Convert.ToString(sqlReader["BrandID"]),
                         Convert.ToString(sqlReader["ComponentID"]),
                         Convert.ToString(sqlReader["ComponentName"]),
                    });

                    listView15.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }
        }

        private async void button30_Click(object sender, EventArgs e)
        {
            listView15.Items.Clear();
            listView15.View = View.List; 
            listView15.Columns.Clear(); 
        }


        private async void button31_Click(object sender, EventArgs e)
        {
            listView15.GridLines = true;
            listView15.FullRowSelect = true;
            listView15.View = View.Details;

            listView15.Columns.Add("TeaID");
            listView15.Columns.Add("TeaName");
            listView15.Columns.Add("ComponentCount");
            listView15.Columns.Add("ComponentNames");
          

            await LoadCatalogAndComponentsAmountAsync();
        }

        private async Task LoadCatalogAndComponentsAmountAsync()
        {
            SqlDataReader sqlReader = null;

            SqlCommand getCatalogAndComponentsAmount = new SqlCommand("SELECT c.TeaID,c.TeaName, COUNT(tcc.ComponentID) AS ComponentCount," +
                " STRING_AGG(cm.ComponentName, ', ') AS ComponentNames\r\nFROM Catalog c\r\nLEFT JOIN TeaContents tc ON c.TeaID = tc.TeaID" +
                "\r\nLEFT JOIN TeaContentsComponents tcc ON tc.TeaContentsID = tcc.TeaContentsID" +
                "\r\nLEFT JOIN Component cm ON tcc.ComponentID = cm.ComponentID\r\nGROUP BY c.TeaID,c.TeaName; ", sqlConnection);

            try
            {
                sqlReader = await getCatalogAndComponentsAmount.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[] {

                        Convert.ToString(sqlReader["TeaID"]),
                         Convert.ToString(sqlReader["TeaName"]),
                         Convert.ToString(sqlReader["ComponentCount"]),
                         Convert.ToString(sqlReader["ComponentNames"]),
                         
                    });

                    listView15.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }
        }

        private async void button32_Click(object sender, EventArgs e)
        {
            listView15.GridLines = true;
            listView15.FullRowSelect = true;
            listView15.View = View.Details;
            listView15.Columns.Add("CountryName");
            listView15.Columns.Add("TeaCount");
            listView15.Columns.Add("AvgPrice");

            await LoadTeaCountryAmountAsync();
        }

        private async Task LoadTeaCountryAmountAsync()
        {
            SqlDataReader sqlReader = null;

            SqlCommand getCatalogAndComponentsAmount = new SqlCommand(" SELECT co.CountryName, COUNT(c.TeaID) AS TeaCount, AVG(c.Price) AS AvgPrice" +
                "\r\nFROM Catalog c" +
                "\r\nJOIN Countries co ON c.CountryID = co.CountryID" +
                "\r\nGROUP BY co.CountryName;", sqlConnection);

            try
            {
                sqlReader = await getCatalogAndComponentsAmount.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    ListViewItem item = new ListViewItem(new string[] {

                        Convert.ToString(sqlReader["CountryName"]),
                         Convert.ToString(sqlReader["TeaCount"]),
                         Convert.ToString(sqlReader["AvgPrice"]),
                    });

                    listView15.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null && !sqlReader.IsClosed)
                {
                    sqlReader.Close();
                }
            }
        }



    }
}
