using OrderProcessor.Redis.Helpers;
using OrderProcessor.Redis.Managers;
using StackExchange.Redis;
using System;

namespace OrderProcessor.Redis
{
    public class RedisContext
    {
        private static RedisContext _redisContext;
        private static IDatabase _redis;

        private RedisContext()
        {
            _redis = ConnectionMultiplexer.Connect(Utils.REDIS_HOST).GetDatabase();
        }

        public static RedisContext GetInstance()
        {
            if (_redisContext == null)
                _redisContext = new RedisContext();
            return _redisContext;
        }

        public void set<T>(string key, T obj)
        {
            if (typeof(T) == typeof(string))
                _redis.StringSet(key, obj.ToString());
            else
                _redis.HashSet(key, RedisConverter.ToHashEntries(obj));
        }

        public T get<T>(string key)
        {
            if (typeof(T) == typeof(string))
                return (T)Convert.ChangeType(_redis.StringGet(key), typeof(T));
            else
                return RedisConverter.ConvertFromRedis<T>(_redis.HashGetAll(key));
        }
    }
}
