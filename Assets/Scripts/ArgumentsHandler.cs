using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class ArgumentsHandler<T>
    {
        private static ArgumentsHandler<T> instance;
        private T argument;

        private ArgumentsHandler()
        {
        }

        public static ArgumentsHandler<T> GetInstance()
        {
            if (instance == null)
                instance = new ArgumentsHandler<T>();
            return instance;
        }

        public void SetArgs(T item)
        {
            argument = item;
        }

        public T GetArgs()
        {
            return argument;
        }
    }
}
