using System;
using StackExchange.Redis;

namespace RedisIntroduction.HyperLogLog
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var redis = ConnectionMultiplexer.Connect("localhost:6379");
                var db = redis.GetDatabase();
                Console.WriteLine("---------Adding User in hour---------");

                db.HyperLogLogAdd("user:20161215:01", "juan");
                db.HyperLogLogAdd("user:20161215:01", "maria");
                db.HyperLogLogAdd("user:20161215:01", "jose");
                db.HyperLogLogAdd("user:20161215:01", "juan");
                db.HyperLogLogAdd("user:20161215:02", "juan");
                db.HyperLogLogAdd("user:20161215:02", "abel");

                Console.WriteLine("---------find unique user in one hour---------");
                var counterOneHour = db.HyperLogLogLength("user:20161215:01");
                Console.WriteLine($"unique user conected 01 hs {counterOneHour}");

                Console.WriteLine("---------find unique user in two hour---------");
                var counterTwoHour = db.HyperLogLogLength("user:20161215:02");
                Console.WriteLine($"unique user conected 01 hs {counterTwoHour}");

                Console.WriteLine("---------find unique user from 01 to 02 hours---------");
                string newKey = "user:20161215:01-02";
                db.HyperLogLogMerge(newKey, new RedisKey[] {"user:20161215:01", "user:20161215:02"});
                db.KeyExpire(newKey, TimeSpan.FromSeconds(10));
                var counter = db.HyperLogLogLength(newKey);
                Console.WriteLine($"unique user conected 01 to 02 hs {counter}");
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
