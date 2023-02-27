using EcommerceProjectt.Models;
using MySql.Data.MySqlClient;
using Renci.SshNet.Security.Cryptography;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EcommerceProjectt.Controllers
{
    public class AccountController : Controller
    {
        
        

        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult login(Account acc)
        {
            if (acc.Select == "Merchant")
            {
                MySqlConnection conn1 = new MySqlConnection(DBConnection.conString);
                conn1.Open();
                string query1 = "select * from merchantcredentials where Email='" + acc.Email + "' and Password='" + acc.Password + "'";
                MySqlCommand cmd = new MySqlCommand(query1, conn1);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read() == true)
                {
                    Account.Id2= reader.GetString("Email");
                    conn1.Clone();
                    return RedirectToAction("GetAllCard","Card");
                }
                else
                {
                    Response.Write("<script>alert('Invalid Username and Password');</script>");
                    ModelState.Clear(); 
                    return View();
                }

            }

            if (acc.Select == "Client")
            {
                MySqlConnection conn1 = new MySqlConnection(DBConnection.conString);
                conn1.Open();
                string query1 = "select * from credentials where Email='" + acc.Email + "' and Password='" + acc.Password + "'";
                MySqlCommand cmd = new MySqlCommand(query1, conn1);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read() == true)
                {
                    Account.Id3 = reader.GetInt32("Id");
                    conn1.Clone();
                    return RedirectToAction("GetAllClientCard", "Card");
                }
                else
                {
                    Response.Write("<script>alert('Invalid Username and Password');</script>");
                    ModelState.Clear();
                    return View();
                }

            }
            return View();

        }

        public ActionResult Registration()
        {

            return View();
        }

        [HttpPost]
         public ActionResult Registration(Account acc)
        {
            
            if (acc.Select == "Merchant")
            {

                MySqlConnection conn1 = new MySqlConnection(DBConnection.conString);
                conn1.Open();
                string query1 = "select * from merchantcredentials where Email='" + acc.Email + "'";
                MySqlCommand cmd = new MySqlCommand(query1, conn1);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read() == true)
                {
                    conn1.Clone();

                    Response.Write("<script>alert('This Email is already Exits ...');</script>");
                    ModelState.Clear();

                }

                else
                {
                    string a=Account.Id2 = acc.Email;
                    MySqlConnection conn = new MySqlConnection(DBConnection.conString);
                    conn.Open();
                    string query = "INSERT INTO merchantcredentials VALUES('" + acc.Id + "','" + acc.Fname + "',+'" + acc.Lname + "','" + acc.Country + "','" + acc.Email + "','" + acc.Password + "','" + a + "')";
                    MySqlCommand cmd1 = new MySqlCommand(query, conn);
                    try
                    {
                        if (cmd1.ExecuteNonQuery() == 1)
                        {
                            conn.Close();
                            return RedirectToAction("login", "Account");
                        }
                        else
                        {
                            conn.Close();
                            Response.Write("<script>alert('Please Fill valid values...');</script>");
                            ModelState.Clear();
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error3 = "An Unexpected Error Occur " + ex;
                        ModelState.Clear();
                    }

                }
            }
            if (acc.Select == "Client")
            {

                MySqlConnection conn1 = new MySqlConnection(DBConnection.conString);
                conn1.Open();
                string query1 = "select * from credentials where Email='" + acc.Email + "'";
                MySqlCommand cmd = new MySqlCommand(query1, conn1);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read() == true)
                {
                    conn1.Clone();

                    Response.Write("<script>alert('This Email is already Exits ...');</script>");
                    ModelState.Clear();

                }

                else
                {

                    MySqlConnection conn = new MySqlConnection(DBConnection.conString);
                    conn.Open();
                    string a = Account.Id2 = acc.Email;
                    string query = "INSERT INTO credentials VALUES('" + acc.Id + "','" + acc.Fname + "',+'" + acc.Lname + "','" + acc.Country + "','" + acc.Email + "','" + acc.Password + "','" + a + "')";
                    MySqlCommand cmd1 = new MySqlCommand(query, conn);
                    try
                    {
                        if (cmd1.ExecuteNonQuery() == 1)
                        {
                            conn.Close();
                            return RedirectToAction("login", "Account");
                        }
                        else
                        {
                            conn.Close();
                            Response.Write("<script>alert('Please Fill valid values...');</script>");
                            ModelState.Clear();
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Error3 = "An Unexpected Error Occur " + ex;
                        ModelState.Clear();
                    }


                }
            }
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }

    }
}