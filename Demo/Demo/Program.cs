using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Quadratic_Probing collision = new Quadratic_Probing();
            System.Console.WriteLine(collision.size);
            collision.Add("nguyen", "thanh tan");
            collision.Add("xin", "Chao");
            collision.printHash();

           
        }
    }
    public class Info
    {
        public object Key { get; set; }
        public object Value { get; set; }
        public Info(object key, object value) { this.Key = key; this.Value = value; }
    }
    public class Quadratic_Probing
    {
        Info inf;
        Info[] array;
        int count;
        public int size;

        #region Get all Keys from list HashTables
        public object[] Keys { get => GetKeys().ToArray(); }
        public IEnumerable<object> GetKeys()
        {
            foreach (Info item in array)
            {
                if (item == null)
                    continue;
                yield return item.Key;
            }
        }
        #endregion

        #region Get all Values from list HashTables
        public object[] Values { get => GetValues().ToArray(); }
        public IEnumerable<object> GetValues()
        {
            foreach (Info item in array)
            {
                if (item == null)
                    continue;
                yield return item.Value;
            }
        }
        #endregion

        #region constructor
        public Quadratic_Probing()
        {
            size = 5;
            array = new Info[size];
            count = 0;
        }
        #endregion

        #region Hash key to index of array
        public int HashCode(object key)
        {
            var sum = 0;
            var arrChars = key.ToString().ToCharArray();
            for (int i = 0; i < arrChars.Length; i++)
            {
                sum += arrChars[i] + (i + 1);
            }
            int hashcode = sum.GetHashCode();
            return (hashcode % size);
        }
        #endregion

        #region Add key and value to list HashTables
        public void Add(object key, object value)
        {
            var index = HashCode(key);
            var trash = 1;
            inf = new Info(key, value);
            if (index > array.Length)
            {
                ReSize(index);
            }
            while (array[index] != null)
            {
                if (index + 1 <= array.Length)
                {
                    index = (HashCode(key) + trash * trash);
                    trash++;
                }
                else
                    ReSize(index);
            }
            array[index] = inf;
            count++;
        }
        #endregion

        #region Get Value from your key, which you input to method
        public object GetValue(object key)
        {
            var index = HashCode(key);
            var trash = 1;
            while (array[index].Key != key && index <= array.Length - 1)
            {
                if (index + 1 <= array.Length)
                {
                    index = HashCode(key) + trash;
                    trash++;
                }
                else
                    ReSize(index);
            }
            if (array[index].Key != key)
            {
                System.Console.WriteLine("Don't find key in HashTables");
                return null;
            }
            else
                return array[index].Value;
        }
        #endregion

        #region Like the name of method it remove something...
        public void Remove(object key)
        {
            var index = HashCode(key);
            var trash = 0;
            while (array[index].Key != key)
            {
                if (index + 1 <= array.Length)
                {
                    index = HashCode(key) + trash;
                    trash++;
                }
                else
                    ReSize(index);
            }
            if (array[index].Key == key)
            {
                System.Console.WriteLine("Remove Success");
                count--;
            }
            else
                System.Console.WriteLine("Don't find key in HashTables");
        }
        #endregion

        #region change new size of array for greater than old size
        public void ReSize(int index)
        {
            size = index + 1;
            Info[] array2 = new Info[size];
            for (int i = 0; i < array.Length; i++)
            {
                array2[i] = array[i];
            }
            array = array2;
        }
        #endregion


        #region [ Clear() ] Xóa Toàn Bộ Key-Value Trong HashTable
        public void Clear()
        {
            size = 1000;
            array = new Info[size];
            count = 0;
        }
        #endregion

        public void printHash()
        {
            Info[] temp = array;

            Console.WriteLine("HashTable Hiện Tại:");
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] != null)
                {
                    Console.WriteLine("key = " + temp[i].Key + ", val = " + temp[i].Value);
                }
            }
            Console.WriteLine();
        }
    }
}
