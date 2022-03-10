// © 2016 IDesign Inc. All rights reserved 
//Questions? Comments? go to 
//http://www.idesign.net

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AudioLib
{
    public static class CollectionExtensions
    {
        public static IEnumerable<Task> ForEachAsync<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            List<Task> tasks = new List<Task>();

            foreach (T item in collection)
            {
                tasks.Add(Task.Run(() => action(item)));
            }       
            
            return tasks;
        }
    }
}