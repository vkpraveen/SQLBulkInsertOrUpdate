using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace BulkInsertOrUpdate
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString="Data Source=.;Initial Catalog=PKV;Integrated Security=True";
            IBulkInsertOrUpdate objBulkInsertOrUpdate =new BulkInsertOrUpdate(connectionString);
            int rowsEffected=objBulkInsertOrUpdate.BulkInsertUpdate(GetYourData());
            Console.WriteLine("Number of Rows effected: {0}",rowsEffected);
        }

        private static List<MyBulkClass> GetYourData()
        {
            List<MyBulkClass> listofMyBulkClasses = new List<MyBulkClass>();

            for (int i = 0; i < 1000; i++)
            {
                MyBulkClass objMyBulkClass = new MyBulkClass();
                objMyBulkClass.Id = i;
                /*  Random Password Using RNG  */
                //Using RNG Crypto Service Provider
                byte[] saltpass = new byte[4];
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                rng.GetBytes(saltpass);
                objMyBulkClass.RandomString = Convert.ToBase64String(saltpass);

                listofMyBulkClasses.Add(objMyBulkClass);
            }

            return listofMyBulkClasses;
        }
    }
}
