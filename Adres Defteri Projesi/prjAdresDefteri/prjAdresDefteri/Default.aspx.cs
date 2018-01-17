using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace prjAdresDefteri
{
    public partial class Default : System.Web.UI.Page
    {
        string ConnString = @"Data Source=DESKTOP-J7VCPEP ; Integrated Security=True; Initial Catalog=dbAdresDefteri"; //database bağlantılarını ayarladık


        protected void Page_Load(object sender, EventArgs e) 
        {
            if (!IsPostBack) // eğer sayfa ilk kez açılıyorsa gridview i doldur. Eğer ilk defa sayfa yüklenmiyorsa devam et. Gridview in içindeki elemanların çoklanmasını önler.
            {
                PopulateGridView();  // bu method gridview oluşturdu ilk açıldığında 
            }
        }

        void PopulateGridView()
        {
            DataTable dt = new DataTable(); // ADO.NET
            using (SqlConnection conn = new SqlConnection(ConnString)) // database e bağlanmak için bağlantı açıyoruz
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Adres", conn); // Adres tablosundaki verileri çekiyoruz
                da.Fill(dt);   // verileri dataadapter a dolduruyoruz

            }
            if (dt.Rows.Count > 0)  // eğer Adres tablosunda kayıt varsa gridview in içine dolduruyoruz
            {
                ViewState["myViewState"] = dt; // Sonradan kullanacağız. verileri saklamak için default olarak kullanılan tekniktir. Sayfa post back olduğunda ve geri geldiğinde aynı sayfadaki değerlerin saklanması işlemini ViewState yapar. 
                gwAdresDefteri.DataSource = dt;
                gwAdresDefteri.DataBind();
            }
            else
            {
                dt.Rows.Add(dt.NewRow()); // Adres tablosunda veri yoksa yeni bir satır (Row) oluşturup içine "Kayıt Yok" yazdırıyoruz 
                gwAdresDefteri.DataSource = dt;
                gwAdresDefteri.DataBind();
                gwAdresDefteri.Rows[0].Cells.Clear();
                gwAdresDefteri.Rows[0].Cells.Add(new TableCell());
                gwAdresDefteri.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count;
                gwAdresDefteri.Rows[0].Cells[0].Text = "Kayıt Yok";
                gwAdresDefteri.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;




            }
        }

        protected void gwAdresDefteri_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("AddNew")) // eğer + butonuna tıkladıysak sağağıdaki gibi bir bağlantı açıp veri kaydediyoruz
                {
                    using (SqlConnection conn = new SqlConnection(ConnString))
                    {
                        conn.Open();
                        string query = "INSERT INTO Adres(Ad,Soyad,TelefonNo,EMail) VALUES (@Ad,@Soyad,@TelefonNo,@EMail) ";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Ad", (gwAdresDefteri.FooterRow.FindControl("txtAdFooter") as TextBox).Text.Trim()); // Trim kullandk ki textbox in içinde boş karakter girdiysek temizlesin
                        cmd.Parameters.AddWithValue("@Soyad", (gwAdresDefteri.FooterRow.FindControl("txtSoyadFooter") as TextBox).Text.Trim());
                        cmd.Parameters.AddWithValue("@TelefonNo", (gwAdresDefteri.FooterRow.FindControl("txtTelefonNoFooter") as TextBox).Text.Trim());
                        cmd.Parameters.AddWithValue("@EMail", (gwAdresDefteri.FooterRow.FindControl("txtEMailFooter") as TextBox).Text.Trim());
                        cmd.ExecuteNonQuery(); //Yazdığımız Sorguyu Çalıştırır İşleve Sokar 
                        PopulateGridView(); // İşlem bittikten sonra gridview in içine tekrar dolduruyoruz verileri
                        txtBasarili.Text = "Veri Kaydedildi";
                        txtHata.Text = "";

                    }
                }
            }
            catch (Exception ex)  // Try blogunda bir sorunla karşılaşırsak ex.message sayesinde sorunun ne olduğunu anlayacağız
            {

                txtBasarili.Text = "";
                txtHata.Text = ex.Message;

            }

        }

        protected void gwAdresDefteri_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gwAdresDefteri.EditIndex = e.NewEditIndex; // se.tiğimiz satır üzerinden update yada cancel gibi işlemlerimize izin verecek. Satırı aktif hala getirecek
            PopulateGridView();
        }

        protected void gwAdresDefteri_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gwAdresDefteri.EditIndex = -1;  // gridview üzerinde değişiklik yapmaktan vazgeçersek gridview i eski haline geri dönderiyoruz.
            PopulateGridView();   // gridview i tekrar yükledik
        }

        protected void gwAdresDefteri_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(ConnString))  // database bağlantı ayarlarını yaptık 
                {
                    conn.Open();
                    string query = "UPDATE Adres SET Ad=@Ad,Soyad=@Soyad,TelefonNo=@TelefonNo,EMail=@EMail WHERE AdresID=@ID ";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Ad", (gwAdresDefteri.Rows[e.RowIndex].FindControl("txtAd") as TextBox).Text.Trim());
                    cmd.Parameters.AddWithValue("@Soyad", (gwAdresDefteri.Rows[e.RowIndex].FindControl("txtSoyad") as TextBox).Text.Trim());
                    cmd.Parameters.AddWithValue("@TelefonNo", (gwAdresDefteri.Rows[e.RowIndex].FindControl("txtTelefonNo") as TextBox).Text.Trim());
                    cmd.Parameters.AddWithValue("@EMail", (gwAdresDefteri.Rows[e.RowIndex].FindControl("txtEMail") as TextBox).Text.Trim());
                    cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(gwAdresDefteri.DataKeys[e.RowIndex].Value.ToString()));
                    cmd.ExecuteNonQuery();  // Update sorgusunu çalıştırdık ve işlettik
                    gwAdresDefteri.EditIndex = -1; // İşlem yaptığımız satırdan çıkabilmek için edit index i -1 yaptık
                    PopulateGridView();
                    txtBasarili.Text = "Veri Güncellendi";
                    txtHata.Text = "";

                }

            }
            catch (Exception ex)
            {

                txtBasarili.Text = "";
                txtHata.Text = ex.Message;

            }
        }

        protected void gwAdresDefteri_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                using (SqlConnection conn = new SqlConnection(ConnString))
                {
                    conn.Open();
                    string query = "DELETE FROM Adres WHERE AdresID=@ID ";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(gwAdresDefteri.DataKeys[e.RowIndex].Value.ToString()));
                    cmd.ExecuteNonQuery();
                    PopulateGridView();
                    txtBasarili.Text = "Veri Silindi";
                    txtHata.Text = "";

                }

            }
            catch (Exception ex)
            {

                txtBasarili.Text = "";
                txtHata.Text = ex.Message;

            }
        }

        protected void searchButton_Click(object sender, EventArgs e)
        {
            string searchTerm = searchBox.Text.ToLower(); // textbox ın içine yazılan değeri aldık ve küçük harfe çevirdik


            if (searchTerm.Length >= 3) // aranan kelime en az 3 harfli olmalı 

            {

                if (ViewState["myViewState"] == null) // Verilerimiz gridview e yüklenirken bir kopyasını da ViewState e almıştık
                    return;                           // Eğer hiç veri yoksa devam edecek çalışmaya


                DataTable dt = ViewState["myViewState"] as DataTable;  // viewstate içindeki verilerimizi kullanabilmek için datatable a çevirdik


                DataTable dtNew = dt.Clone();  // datatable ımızı (yani database den gelen verilerimizi) kopyaladık


                foreach (DataRow row in dt.Rows) // datatable ımızın içinde Ad ve Soyad kolonuna göre arama yaptık
                {

                    if (row["Ad"].ToString().ToLower().Contains(searchTerm) || row["Soyad"].ToString().ToLower().Contains(searchTerm) || row["EMail"].ToString().ToLower().Contains(searchTerm))
                    {

                        dtNew.Rows.Add(row.ItemArray);  // Eğer aradığımız kayıdı bulursa kopyasını aldığımız dtNew Adlı dataTable ın içine attı
                    }
                }

                //rebind the grid
                gwAdresDefteri.DataSource = dtNew; // ve dtNew a attığı veriyi gridview imizde gösterdi
                gwAdresDefteri.DataBind();
            }
        }

        protected void reset_Click(object sender, EventArgs e)
        {
            searchBox.Text = string.Empty;  // Arama yaptıktan sonra textbox ın içindeki veriyi sildik

            if (ViewState["myViewState"] == null)  // Verilerimiz gridview e yüklenirken bir kopyasını da ViewState e almıştık
                return;                             // Eğer hiç veri yoksa devam edecek çalışmaya


            DataTable dt = ViewState["myViewState"] as DataTable; // Veritabanımızda olan bütün kayıtları ViewState ten aldık vetekrardan bir DataTable ın içine atttık

            //rebind the grid
            gwAdresDefteri.DataSource = dt;  //Datatable daki verilerimizi gridview in içine attık tekrardan
            gwAdresDefteri.DataBind();  // Datatable dan dönen verilerin gridview e atamasını yapar. (Ad verileri Ad kolonuna atar) DataBind olmadan gridview de veri görünmez
        }
    }
}