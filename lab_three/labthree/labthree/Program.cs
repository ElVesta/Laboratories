using System;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace labthree
{
    public class Array
    {
        private int[] num;
        public Array(int size)
        {
            num = new int[size];
        }

        public int this[int index]
        {
            get => num[index];
            set => num[index] = value;
        }
        public int Length => num.Length;
        public void Print()
        {
            Console.Write("Массив: ");
            for (int i = 0; i < num.Length; i++)
            {
                Console.Write(num[i] + " ");
            }
            Console.WriteLine();
        }

        public static Array operator -(Array arr,int scal)
        {
            Array result = new Array(arr.Length);
            for (int i = 0; i < arr.Length; i++)
            {
                result[i] = arr[i] - scal;
            }
            return result;
        }

        public static bool operator ==(Array arr,int val)
        {
            for(int i = 0; i < arr.Length;i++)
                if (arr[i] == val) return true;
            return false;
        }
        public static bool operator !=(Array arr,int val)
        {
            return !(arr == val);
        }
        public static bool operator == (Array a1,Array a2)
        {
            if(ReferenceEquals(a1,a2)) return true;
            if(a1 is null || a2 is null || a1.Length != a2.Length) return false;
            for(int i = 0;i < a1.Length; i++)
            {
                if (a1[i] != a2[i]) return false;
            }
            return true;
        }
        public static bool operator !=(Array a1 ,Array a2)
        {
            return !(a1 == a2);
        }

        public static Array operator +(Array a1, Array a2)
        {
            Array result = new Array(a1.Length + a2.Length);
            for (int i = 0; i < a1.Length; i++)
            {
                result[i] = a1[i];
            }
            for (int i = 0; i < a2.Length; i++)
            { 
                result[i + a1.Length] = a2[i];
            }
            return result;
        }
    }
    public class Production
    {
        public int ID = 111;
        public string Name = "Supercompany";

        public void Info()
        {
            Console.WriteLine($"ID:{ID} и название организации - {Name}");
        }
        public class Developer
        {
            public int id = 1;
            public string name = "Генадий";
            public string surname = "Пупкин";
            public string dad = "Альбертович";
            public string section = "переработки бумаги";

            public void Info()
            {
                Console.WriteLine($"Специалист {name} {surname} {dad} с id {id} работает в отделе {section}");
            }
        }
    }

    public static class StaticOperation
    {
        public static int Sum(this Array arr)
        {
            int sum = 0;
            for (int i = 0; i < arr.Length; i++)
                sum += arr[i];
            return sum;
        }

        public static int Raznica(this Array arr)
        {
            if(arr.Length == 0) return 0;
            int min = arr[0], max = arr[0];
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] < min) min = arr[i];
                if (arr[i] > max) max = arr[i];
            }
            return max - min;
        }

        public static int Count(this Array arr)
        {
            return arr.Length;
        }

        public static void Info(this Array arr)
        {
            Console.WriteLine($"Сумма элементов: {arr.Sum()}");
            Console.WriteLine($"Разница между max и min: {arr.Raznica()}");
            Console.WriteLine($"Количество элементов: {arr.Count()}");
        }
        public static string RemoveGlasn(this string str)
        {
            string glasn = "аеёиоуыэюяaeiouAEЁИОУЫЭЮЯAEIOU";
            return new string(str.Where(c => !glasn.Contains(c)).ToArray());
        }
        public static Array RemoveFirstFive(this Array arr)
        {
            int newSize = arr.Length - 5;
            Array result = new Array(newSize);
            for (int i = 0;i < newSize; i++)
            {
                result[i] = arr[i + 5];
            }
            return result;
        }
    }

    class Program
    {
        static void Main()
        {
            Console.Write("Размер массива: ");
            int size = Convert.ToInt32(Console.ReadLine());
            Array array = new Array(size);

            for (int i = 0; i < size; i++)
            {
                Console.Write("Элемент: ");
                array[i] = Convert.ToInt32(Console.ReadLine());
            }
            array.Print();

            Console.Write("Введите число для вычитания: ");
            int scalar = Convert.ToInt32(Console.ReadLine());
            Array result = array - scalar;
            result.Print();

            Console.Write("Введите значение для поиска: ");
            int search = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(array == search ? $"Значение {search} найдено в массиве" : $"Значение {search} отсутствует в массиве");

            Console.WriteLine("Введите элементы второго массива для сравнения:");
            Array array2 = new Array(size);
            for (int i = 0; i < size; i++)
            {
                Console.Write("Элемент: ");
                array2[i] = Convert.ToInt32(Console.ReadLine());
            }
            Console.Write("Результат сравнения: ");
            Console.WriteLine(result != array2 ? "Массивы не равны." : "Массивы равны.");

            Console.WriteLine("\nОбъединение массива с вычитанием и второго массива:");
            Array merged = result + array2;
            merged.Print();

            Console.WriteLine("\nУдаление первых пяти элементов из объединённого массива:");
            Array deleted = StaticOperation.RemoveFirstFive(merged);
            deleted.Print();

            array.Info();

            Console.Write("\nВведите строку для удаления гласных: ");
            string stroka = Console.ReadLine();
            Console.WriteLine($"Без гласных: {stroka.RemoveGlasn()}");
        }
    }
}