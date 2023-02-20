using EcommerceProjectt.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommerceProjectt.Controllers
{
    public class CardController : Controller
    {
        List<Card> cards = new List<Card>();
        // GET: Card

        public ActionResult AddCard()
        {
            
            return View();
        }
        
        [HttpPost]
        public ActionResult AddCard(Card card,HttpPostedFileBase ImagePath)
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
                string query = "INSERT INTO card VALUES('" + card.Id + "','" + card.Name + "','" + card.Description + "',+'" + card.Price + "','" + card.ImagePath + "','" + card.MerchantId+ "')";
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
        public string uploadimage(HttpPostedFileBase file)
        {
            Random r=new Random();
            string path = "-1";
            int random = r.Next();
            if(file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if(extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png")){
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/Content/Upload/"), random + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path= "~/Content/Upload/" + random+Path.GetFileName(file.FileName);

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
        

        
        public ActionResult GetAllCard(string SearchString="")
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
                string Query = "Select card.Id,card.ImagePath,card.Name,card.Description,card.Price from card inner join merchantcredentials on card.MercantId=merchantcredentials.Email  where '" + a + "'=card.MercantId and card.Name='" + SearchString + "'";
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
                    card.ImagePath = reader.GetString("ImagePath");
                    cards.Add(card);
                }
                connection.Close();
        }
   /*     [HttpPost]
        public ActionResult SearchData()
        {
            MySqlConnection conn = new MySqlConnection(DBConnection.conString);

            string a = Account.Id2;
            conn.Open();
            string Query = "Select card.Id,card.ImagePath,card.Name,card.Description,card.Price from card inner join merchantcredentials on card.MercantId=merchantcredentials.Email  where '" + a + "'=card.MercantId";
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
                card.ImagePath = reader.GetString("ImagePath");
                cards.Add(card);
            }
            conn.Close();
            return View();
        }*/
        private void fetchData()
        {
           
           if (cards.Count > 0)
            {
                cards.Clear();
            }
            MySqlConnection conn = new MySqlConnection(DBConnection.conString);

            string a = Account.Id2;
            conn.Open();
            string Query = "Select card.Id,card.ImagePath,card.Name,card.Description,card.Price from card inner join merchantcredentials on card.MercantId=merchantcredentials.Email  where '"+a+"'=card.MercantId";
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
                card.ImagePath = reader.GetString("ImagePath");
                cards.Add(card);
            }
            conn.Close();
        }



    }
}