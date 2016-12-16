using StackExchange.Redis;
using System;

namespace RedisIntroduction.Transactions2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var redis = ConnectionMultiplexer.Connect("localhost:6379");
                var db = redis.GetDatabase();
                Console.WriteLine("---------John account is 50---------");
                var value = "John";
                db.StringSet("account-john", 50);
                var valueAcc = db.StringGet("account-john");
                Console.WriteLine(valueAcc);
                Console.ReadKey();
                Console.WriteLine("---------John account is incremented by 100---------");
                db.StringIncrement("account-john", 100);
                valueAcc = db.StringGet("account-john");
                Console.WriteLine(valueAcc);
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
