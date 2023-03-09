using EcommerceProjectt.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace EcommerceProjectt.Controllers
{
    public class CardController : Controller
    {
        public static List<Card> cards = new List<Card>();
        public static List<BuyNow> buys = new List<BuyNow>();
        // GET: Card

        public ActionResult AddCard()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddCard(Card card, HttpPostedFileBase ImagePath)
        {

            string path = uploadimage(ImagePath);
            if (path == "-1")
            {
                ViewBag.Error2 = "Please Fill valid values111111111111111111";

            }
            else
            {
                card.ImagePath = path;
                MySqlConnection conn = new MySqlConnection(DBConnection.conString);
                conn.Open();

                card.MerchantId = Account.Id2;
                string query = "INSERT INTO card VALUES('" + card.Id + "','" + card.Name + "','" + card.Description + "',+'" + card.Price + "','" + card.ImagePath + "','" + card.MerchantId + "','" + card.Quantity + "')";
                MySqlCommand cmd1 = new MySqlCommand(query, conn);
                try
                {
                    if (cmd1.ExecuteNonQuery() == 1)
                    {
                        Response.Write("<script>alert('Data Inserted...');</script>");
                        conn.Close();
                        return RedirectToAction("GetAllCard", "Card");
                    }
                    else
                    {
                        conn.Close();
                        ViewBag.Error2 = "Data not inserted";
                        ModelState.Clear();

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error3 = "Please Fill valid values3333333333" + ex;
                    ModelState.Clear();
                }
            }
            return View();
        }
        public ActionResult DeleteItems(int DelId)
        {
            DeleteRecord(DelId);
            return RedirectToAction("GetAllCard", "Card");
        }

        public void DeleteRecord(int DelId)
        {
            MySqlConnection conn = new MySqlConnection(DBConnection.conString);
            conn.Open();
            string query = "DELETE FROM card where Id='" + DelId + "'";
            MySqlCommand cmd1 = new MySqlCommand(query, conn);
            cmd1.ExecuteNonQuery();
        }
        public static int Up;
        public ActionResult UpdateItems(int UpId)
        {
            Up = UpId;
            return View();
        }

        [HttpPost]
        public ActionResult UpdateItems(Card card, HttpPostedFileBase ImagePath)
        {
            string path = uploadimage(ImagePath);
            if (path == "-1")
            {
                ViewBag.Error2 = "Please Fill valid valuee";

            }
            else
            {
                card.ImagePath = path;
                MySqlConnection conn = new MySqlConnection(DBConnection.conString);
                conn.Open();

                string query = "Update card set Name='" + card.Name + "' ,Description='" + card.Description + "',Price='" + card.Price + "',ImagePath='" + card.ImagePath + "' where Id='" + Up + "'";
                MySqlCommand cmd1 = new MySqlCommand(query, conn);
                cmd1.ExecuteNonQuery();
                try
                {
                    if (cmd1.ExecuteNonQuery() == 1)
                    {
                        Response.Write("<script>alert('Data Updated...');</script>");
                        conn.Close();
                    }
                    else
                    {
                        conn.Close();
                        ViewBag.Error2 = "Data not Update";
                        ModelState.Clear();

                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error3 = "An unexpected Error Occur" + ex;
                    ModelState.Clear();
                }
            }
            return RedirectToAction("GetAllCard", "Card");

        }



        public string uploadimage(HttpPostedFileBase file)
        {
            Random r = new Random();
            string path = "-1";
            int random = r.Next();
            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png") || extension.ToLower().Equals(".jfif"))
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/Content/Upload/"), random + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = "~/Content/Upload/" + random + Path.GetFileName(file.FileName);

                    }
                    catch
                    {
                        Response.Write("<script>alert('An Unexpected error occur...');</script>");
                        path = "-1";
                    }

                }
                else
                {
                    Response.Write("<script>alert('Only jpg,jpeg,png format area acceptable ...');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Please Select a file');</script>");
                path = "-1";
            }
            return path;
        }



        public ActionResult GetAllCard(string SearchString = "")
        {
            if (SearchString == " " || SearchString == "  " || SearchString == "   " || SearchString == "" || SearchString == null)
            {
                fetchData();
                return View(cards);
            }
            else
            {
                search(SearchString);
                return View(cards);
            }

        }
        public void search(string SearchString)
        {
            if (cards.Count > 0)
            {
                cards.Clear();
            }
            MySqlConnection connection = new MySqlConnection(DBConnection.conString);
            string a = Account.Id2;
            connection.Open();
            string Query = $"Select card.Id,card.ImagePath,card.Name,card.Description,card.Quantity,card.Price from card inner join merchantcredentials on card.MercantId=merchantcredentials.Email  where '{a}'=card.MercantId and (card.Id LIKE '%{SearchString}%' OR card.Name LIKE '%{SearchString}%' OR card.Description LIKE '%{SearchString}%' OR card.Price LIKE '%{SearchString}%')";
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = Query;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Card card = new Card();
                card.Id = reader.GetInt32("Id");
                card.Name = reader.GetString("Name");
                card.Description = reader.GetString("Description");
                card.Price = reader.GetInt32("Price");
                card.Quantity = reader.GetInt32("Quantity");
                card.ImagePath = reader.GetString("ImagePath");
                cards.Add(card);
            }
            connection.Close();
        }
      

        private void fetchData()
        {

            if (cards.Count > 0)
            {
                cards.Clear();
            }
            MySqlConnection conn = new MySqlConnection(DBConnection.conString);

            string a = Account.Id2;
            conn.Open();
            string Query = "Select card.Id,card.ImagePath,card.Name,card.Description,card.Quantity,card.Price from card inner join merchantcredentials on card.MercantId=merchantcredentials.Email  where '" + a + "'=card.MercantId";
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = Query;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Card card = new Card();
                card.Id = reader.GetInt32("Id");
                card.Name = reader.GetString("Name");
                card.Description = reader.GetString("Description");
                card.Price = reader.GetInt32("Price");
                card.Quantity = reader.GetInt32("Quantity");
                card.ImagePath = reader.GetString("ImagePath");
                cards.Add(card);
            }
            conn.Close();
        }

        public ActionResult GetAllClientCard()
        {
            fetchClientData();
            return View(cards);
        }
        public void Clientsearch(string SearchString)
        {
            if (cards.Count > 0)
            {
                cards.Clear();
            }
            MySqlConnection connection = new MySqlConnection(DBConnection.conString);
            string a = Account.Id2;
            connection.Open();
            string Query = $"SELECT * FROM card WHERE Id LIKE '%{SearchString}%' OR Name LIKE '%{SearchString}%' OR Description LIKE '%{SearchString}%' OR Price LIKE '%{SearchString}%'";
            MySqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = Query;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Card card = new Card();
                card.Id = reader.GetInt32("Id");
                card.Name = reader.GetString("Name");
                card.Description = reader.GetString("Description");
                card.Price = reader.GetInt32("Price");
                card.Quantity = reader.GetInt32("Quantity");
                card.ImagePath = reader.GetString("ImagePath");
                cards.Add(card);
            }
            connection.Close();
        }
        public void fetchClientData()
        {
            if (cards.Count > 0)
            {
                cards.Clear();
            }
            MySqlConnection conn = new MySqlConnection(DBConnection.conString);

            conn.Open();
            string Query = "Select * from card limit 8";
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = Query;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Card card = new Card();
                card.Id = reader.GetInt32("Id");
                card.Name = reader.GetString("Name");
                card.Description = reader.GetString("Description");
                card.Quantity = reader.GetInt32("Quantity");
                card.Price = reader.GetInt32("Price");
                card.ImagePath = reader.GetString("ImagePath");
                cards.Add(card);
            }
            conn.Close();

        }
        public ActionResult AllProducts(string SearchString = "")
        {
            if (SearchString == " " || SearchString == "  " || SearchString == "   " || SearchString == "" || SearchString == null)
            {
                FetchallProducts();
                return View(cards);
            }
            else
            {
                Clientsearch(SearchString);
                return View(cards);
            }
        }

        public void FetchallProducts()
        {
            if (cards.Count > 0)
            {
                cards.Clear();
            }
            MySqlConnection conn = new MySqlConnection(DBConnection.conString);

            conn.Open();
            string Query = "Select * from card";
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = Query;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Card card = new Card();
                card.Id = reader.GetInt32("Id");
                card.Name = reader.GetString("Name");
                card.Description = reader.GetString("Description");
                card.Quantity = reader.GetInt32("Quantity");
                card.Price = reader.GetInt32("Price");
                card.ImagePath = reader.GetString("ImagePath");
                cards.Add(card);
            }
            conn.Close();

        }
        public static int IID;
        public ActionResult ProductDetail(int PId)
        {
            IID = PId;
            if (cards.Count > 0)
            {
                cards.Clear();
            }
            MySqlConnection conn = new MySqlConnection(DBConnection.conString);

            conn.Open();
            string Query = "Select * from card where Id='" + PId + "'";
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = Query;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Card card = new Card();
                card.Id = reader.GetInt32("Id");
                card.Name = reader.GetString("Name");
                card.Description = reader.GetString("Description");
                card.Quantity = reader.GetInt32("Quantity");
                card.Price = reader.GetInt32("Price");
                card.ImagePath = reader.GetString("ImagePath");
                cards.Add(card);
            }
            conn.Close();

            return View(cards);
        }
        public static int OOId;
        public static string RMId;
        public static int PPRC;
        public static int PPRCC;
        public static int QQT;
        public ActionResult Order(int OId)
        {
            OOId = OId;
            if (cards.Count > 0)
            {
                cards.Clear();
            }
            MySqlConnection conn = new MySqlConnection(DBConnection.conString);

            conn.Open();

            string Query = "Select * from card where Id='" + OId + "'";
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = Query;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Card card = new Card();
                card.Id = reader.GetInt32("Id");
                card.Name = reader.GetString("Name");
                card.Description = reader.GetString("Description");
                card.Quantity = reader.GetInt32("Quantity");
                QQT = card.Quantity;
                card.Price = reader.GetInt32("Price");
                PPRCC= card.Price;
                card.ImagePath = reader.GetString("ImagePath");
                RMId = reader.GetString("MercantId");
                cards.Add(card);
            }
            conn.Close();

            return View(cards);
        }

        [HttpPost]
        public ActionResult Order(Card card,int Quantity)
        {
            PPRC = Quantity;
           
            if (card.PayNow == "Pay Now")
            {
                return View("BuyNow");

            }
            else
            {
                return View("InstallementPlan");

            }
        }

        public ActionResult BuyNow()
        {

            return View();
        }

        [HttpPost]
        public ActionResult BuyNow(BuyNow buy)
        {
            
            MySqlConnection conn = new MySqlConnection(DBConnection.conString);
            conn.Open();
            buy.CreId = Account.Id3;
            string query = "INSERT INTO buynow VALUES('" + buy.Id + "','" + buy.Fname + "','" + buy.Lname + "',+'" + buy.Address + "','" + buy.City + "','" + buy.State + "','" + buy.PostalCode + "','" + buy.Country + "','" + OOId + "','" + buy.CreId + "','" + RMId + "')";
            MySqlCommand cmd1 = new MySqlCommand(query, conn);
            try
            {

                if (cmd1.ExecuteNonQuery() == 1)
                {
                    Response.Write("<script>alert('Your Placed an order Successfully...');</script>");
                    conn.Close();
                }
                else
                {
                    conn.Close();
                    ViewBag.Error2 = "Your Order is not placed... ";
                    ModelState.Clear();

                }
            }
            catch (Exception ex)
            {
                conn.Close();
                ViewBag.Error3 = "Please Fill valid values3333333333" + ex;
                ModelState.Clear();
            }
            conn.Close();

            int q= PPRC / PPRCC;
            int qa = QQT - q;
            MySqlConnection conn2 = new MySqlConnection(DBConnection.conString);
            conn2.Open();
            buy.CreId = Account.Id3;
            string query2 = "UPDATE card set Quantity='"+qa+"' where Id='"+ OOId + "'";
            MySqlCommand cmd2 = new MySqlCommand(query2, conn2);
            try
            {

                if (cmd2.ExecuteNonQuery() == 1)
                {
                    conn2.Close();
                }
                else
                {
                    conn2.Close();
                    ViewBag.Error2 = "Your Order is not placed... ";
                    ModelState.Clear();

                }
            }
            catch (Exception ex)
            {
                conn.Close();
                ViewBag.Error3 = "Please Fill valid values3333333333" + ex;
                ModelState.Clear();
            }
            conn.Close();
           

            return View();
        }

        public ActionResult InstallementPlan()
        {

            return View();
        }
        [HttpPost]
        public ActionResult InstallementPlan(BuyNow buy)
        {
            int q = PPRC / PPRCC;
            int qa = QQT - q;

            MySqlConnection conn = new MySqlConnection(DBConnection.conString);
            conn.Open();
            buy.CreId = Account.Id3;
            string query = "INSERT INTO installment VALUES('" + buy.Id + "','" + buy.Fname + "','" + buy.Lname + "',+'" + buy.Address + "','" + buy.City + "','" + buy.State + "','" + buy.PostalCode + "','" + buy.Country + "','"+qa+"','" + OOId + "','" + buy.CreId + "','" + RMId + "','" + buy.intallments + "','" + buy.totalinstall + "','Canceled','false')";
            MySqlCommand cmd1 = new MySqlCommand(query, conn);
            try
            {

                if (cmd1.ExecuteNonQuery() == 1)
                {
                    Response.Write("<script>alert('Your Placed an Installment Successfully...');</script>");
                    conn.Close();
                    return RedirectToAction("GetAllClientCard", "Card");
                }
                else
                {
                    conn.Close();
                    Response.Write("<script>alert('Installemnt not placed');</script>");
                    ModelState.Clear();

                }
            }
            catch 
            {
                Response.Write("<script>alert('An unexpected Error Occurs.');</script>");
                ModelState.Clear();
            }
            return View();
        }

        public ActionResult AllMerOrders()
        {
            string a = Account.Id2;
            if (buys.Count > 0)
            {
                buys.Clear();
            }
            MySqlConnection conn = new MySqlConnection(DBConnection.conString);
            conn.Open();
            string Query = "Select card.ImagePath,card.Name,card.Price,buynow.Fname,buynow.Address,buynow.City,buynow.State,buynow.PostalCode from buynow inner join card on buynow.CardId=card.Id where buynow.MerId='" + a + "' ";
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = Query;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                BuyNow buy = new BuyNow();
                buy.Fname = reader.GetString("Fname");
                buy.Address = reader.GetString("Address");
                buy.City = reader.GetString("City");
                buy.State = reader.GetString("State");
                buy.PostalCode = reader.GetInt32("PostalCode");
                buy.Img = reader.GetString("ImagePath");
                buy.NameOfProduct = reader.GetString("Name");
                buy.PriceOfProduct = reader.GetString("Price");
                buys.Add(buy);
            }
            conn.Close();
            return View(buys);
        }
        public static int InsId;
        public static int CAINID;
        public static int CAInQ;
        public ActionResult AllInstallmets()
        {
            string can = "false";
            string a = Account.Id2;
            if (buys.Count > 0)
            {
                buys.Clear();
            }
            MySqlConnection conn = new MySqlConnection(DBConnection.conString);
            conn.Open();
            string Query = "Select card.ImagePath,card.Name,card.Price,installment.Id,installment.Fname,installment.CardId,installment.RemainingQty,installment.Address,installment.PostalCode,installment.NoOfInstallments,installment.PerInstallment from installment inner join card on installment.CardId=card.Id where '" + a + "'=installment.MerId and Cancel='"+can+"'  ";
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = Query;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                BuyNow buy = new BuyNow();
                buy.Id = reader.GetInt32("Id");
                InsId = buy.Id;
                buy.Fname = reader.GetString("Fname");
                buy.Address = reader.GetString("Address");
                buy.PostalCode = reader.GetInt32("PostalCode");
                buy.Img = reader.GetString("ImagePath");
                buy.NameOfProduct = reader.GetString("Name");
                buy.PriceOfProduct = reader.GetString("Price");
                buy.intallments = reader.GetInt32("NoOfInstallments");
                buy.totalinstall = reader.GetInt32("PerInstallment");
                CAINID = reader.GetInt32("CardId");
                CAInQ = reader.GetInt32("RemainingQty");
                buys.Add(buy);
            }
            conn.Close();
            return View(buys);
        }

        public ActionResult CancelInstallment(int DelId)
        {
            string can = "Canceled";
            string con = "Rejected";
            MySqlConnection conn = new MySqlConnection(DBConnection.conString);
            conn.Open();

            string query = "Update installment set Cancel='" + can + "',Confirm='"+con+"'  where Id='" + DelId + "'";
            MySqlCommand cmd1 = new MySqlCommand(query, conn);
            cmd1.ExecuteNonQuery();
            try
            {
                if (cmd1.ExecuteNonQuery() == 1)
                {

                    Response.Write("<script>alert('You Cancel the Installment...');</script>");
                    conn.Close();
                }
                else
                {
                    conn.Close(); 
                    ViewBag.Error2 = "Error";
                    ModelState.Clear();

                }
            }
            catch (Exception ex)
            {
                ViewBag.Error3 = "An unexpected Error Occur" + ex;
                ModelState.Clear();
            }

            return RedirectToAction("AllInstallmets", "Card");
        }
        public ActionResult ConfirmInstallment(int UpId)
        {
            string acp = "Accepted";
            MySqlConnection conn = new MySqlConnection(DBConnection.conString);
            conn.Open();
            string query = "Update installment set Confirm='" + acp + "' where Id='" + UpId + "'";
            MySqlCommand cmd1 = new MySqlCommand(query, conn);
            cmd1.ExecuteNonQuery();
            try
            {
                if (cmd1.ExecuteNonQuery() == 1)
                {

                    Response.Write("<script>alert('You Accept the Installment...');</script>");
                    conn.Close();
                }
                else
                {
                    conn.Close();
                    ViewBag.Error2 = "Error...";
                    ModelState.Clear();

                }
            }
            catch (Exception ex)
            {
                ViewBag.Error3 = "An unexpected Error Occur" + ex;
                ModelState.Clear();
            }

            MySqlConnection conn2 = new MySqlConnection(DBConnection.conString);
            conn2.Open();
            string query2 = "UPDATE card set Quantity='" + CAInQ + "' where Id='" + CAINID + "'";
            MySqlCommand cmd2 = new MySqlCommand(query2, conn2);
            try
            {

                if (cmd2.ExecuteNonQuery() == 1)
                {
                    conn2.Close();
                }
                else
                {
                    conn2.Close();
      
                    ModelState.Clear();

                }
            }
            catch (Exception ex)
            {
                conn.Close();
                ModelState.Clear();
            }
            conn.Close();

            return RedirectToAction("AllInstallmets", "Card");
        }
        public ActionResult InstallmentResponce()
        {
            /*string can = "false";*/
            int a = Account.Id3;
            if (buys.Count > 0)
            {
                buys.Clear();
            }
            MySqlConnection conn = new MySqlConnection(DBConnection.conString);
            conn.Open();
            string Query = "Select card.ImagePath,card.Name,card.Price,installment.Id,installment.Fname,installment.Address,installment.PostalCode,installment.NoOfInstallments,installment.PerInstallment,installment.Confirm from installment inner join card on installment.CardId=card.Id where '" + a + "'=installment.CreId  ";
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = Query;
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                BuyNow buy = new BuyNow();
                buy.Id = reader.GetInt32("Id");
                InsId = buy.Id;
                buy.Fname = reader.GetString("Fname");
                buy.Address = reader.GetString("Address");
                buy.PostalCode = reader.GetInt32("PostalCode");
                buy.Img = reader.GetString("ImagePath");
                buy.NameOfProduct = reader.GetString("Name");
                buy.PriceOfProduct = reader.GetString("Price");
                buy.intallments = reader.GetInt32("NoOfInstallments");
                buy.totalinstall = reader.GetInt32("PerInstallment");
                buy.Confirm = reader.GetString("Confirm");
                if (buy.Confirm == "Canceled")
                {
                    buy.Confirm = string.Empty;
                }
                buys.Add(buy);
            }
            conn.Close();
            return View(buys);
        }
    }
}