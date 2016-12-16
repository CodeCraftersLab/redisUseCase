using StackExchange.Redis;
using System;

namespace RedisIntroduction.Hashes
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var redis = ConnectionMultiplexer.Connect("localhost:6379");
                var db = redis.GetDatabase();
                Console.WriteLine("---------Adding and retrieving user info---------");
                var fields = new HashEntry[]
                {
                    new HashEntry("name","John"),
                    new HashEntry("surname","Smith"),
                    new HashEntry("age",33),
                    new HashEntry("height","178cm"),
                };
                
                db.HashSet("user_info", fields);
                var rv = db.HashGetAll("user_info");
                foreach (var value in rv)
                {
                    Console.WriteLine("Name: " + value.Name + ", Value: " + value.Value);
                }
                Console.ReadKey();
                Console.WriteLine("---------Increment and get age by 10---------");
                db.HashIncrement("user_info", "age", 10);
                var age = db.HashGet("user_info", "age");
                Console.WriteLine("Age is:" + age);
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
