using StackExchange.Redis;
using System;

namespace RedisIntroduction.Lists
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var redis = ConnectionMultiplexer.Connect("localhost:6379");
                var db = redis.GetDatabase();
                Console.WriteLine("---------Adding countries---------");
                var countries = new RedisValue[] { "Spain", "France", "Italy", "USA" };
                db.ListLeftPush("countries", countries);
                var rv = db.ListRange("countries", 0, -1);
                foreach (var val in rv)
                {
                    Console.WriteLine(val);
                }
                Console.ReadKey();
                Console.WriteLine("---------Adding Colombia---------");
                db.ListRightPush("countries", "Colombia");
                rv = db.ListRange("countries", 0, -1);
                foreach (var val in rv)
                {
                    Console.WriteLine(val);
                }
                Console.ReadKey();
                Console.WriteLine("---------Trimming elements---------");
                db.ListTrim("countries", 0, 2);
                rv = db.ListRange("countries", 0, -1);
                foreach (var val in rv)
                {
                    Console.WriteLine(val);
                }
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey();
            }
        }
    }
}
