using System;
using System.Collections.Generic;

namespace DemoIOC
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            //container.For<ILogger>().Use<FileLogger>(); //use this or the register types method.
            container.RegisterTypes(typeof(ILogger), typeof(FileLogger));
            var log=container.Resolve<ILogger>();
            Console.WriteLine(log.GetType());

            Console.ReadLine();
        }
    }

    internal class Container
    {

        Dictionary<Type, Type> _map = new Dictionary<Type, Type>();
        Type _sourceType;
        public Container()
        {
        }

        internal Container For<TSource>()
        {
            _sourceType = typeof(TSource);
            return this;
        }

        internal void RegisterTypes(Type TSource, Type TDestination)
        {
            _map.Add(TSource, TDestination);
        }

        internal object Resolve<TSource>()
        {
            return Activator.CreateInstance(_map[typeof(TSource)]);
        }

        internal Container Use<TDestination>()
        {
            _map.Add(_sourceType, typeof(TDestination));
            return this;
        }


    }


    public interface ILogger
    {

    }
    public class FileLogger:ILogger
    {

    }
}
