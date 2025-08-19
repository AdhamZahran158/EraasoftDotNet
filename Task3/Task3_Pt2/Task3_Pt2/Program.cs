namespace Task3_Pt2
{
    internal class Program
    {
        static void Print(List<int> list)
        {
            if (list.Count == 0)
                Console.WriteLine("List is empty");
            else
            {
                Console.WriteLine("Printing the list components...");
                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine(list[i]);
                }

            }
        }

        static string AddComponent(List<int> list,int added)
        {
            
            bool exists = false; //for dublication !!! {Bonus}
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == added)
                {
                    return("Item already exists");
                }
            }
            
            list.Add(added);
            return($"Added number : {added}");            
        }

        static double CalculateMean(List<int> list)
        {
            int Value = 0;
            for (int i = 0; i < list.Count; i++)
            {
                Value += list[i];
            }
            double meanValue = Value / list.Count;
            return meanValue;
        }

        static string FindIndex(List<int> list , int inp)
        {
            int verified = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == inp)
                {
                    return $"Found your number at index {i}";
                    verified++;
                }
                if ((verified == 0) && (i == list.Count - 1))
                    return "Did not find your number :(";
            }
            return "Not Found";
        }

        static int GetSmallest(List<int> list)
        {
            int min = int.MaxValue;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] < min)
                {
                    min = list[i];
                }
            }
            return min;
        }

        static int GetLargest(List<int> list)
        {
            int max = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] > max)
                    max = list[i];
            }
            return max;
        }

        static string SwapTwoNums(List<int> list , int num1 , int num2)
        {
            bool num1Available = false;
            bool num2Available = false;

            //Check if the two nums exist in the list!
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == num1)
                    num1Available = true;
                if (list[i] == num2)
                    num2Available = true;
            }
            if (!num1Available || !num2Available)
                return ("Enter valid numbers \n");
            else
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] == num1)
                        list[i] = num2;
                    else if (list[i] == num2)
                        list[i] = num1;
                }
                return ($"Swapped number {num1} with {num2} \n");
            }
        }

        static string SortAscending(List<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i; j < list.Count; j++)
                {
                    if (list[j] < list[i])
                    {
                        int temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
            }
            return ("Arranged ascendingly successfully! \nList:");
        }

        static string SortDescending(List<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i; j < list.Count; j++)
                {
                    if (list[j] > list[i])
                    {
                        int temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
            }
           return("Arranged descendingly successfully! \nList:");
        }

        static void Main(string[] args)
        {
            char input;
            List<int> list = new List<int>();
            while (true)
            {
                Console.WriteLine("Main menu:\r\nA : Add number\r\nP : Print The list\r\nM : Mean Value\r\nL : Largest number\r\nS : Smallest number\r\nF : Find a number's index\r\nC : Clear list\r\nX : Swap Two Components\r\nV : Sort Ascendingly\r\n^ : Sort Decsendingly\r\nQ : Quit");
                Console.WriteLine();
                Console.Write("Enter your Operation --->");
                input = System.Convert.ToChar(Console.ReadLine());
                switch (input)
                {
                    case 'P':
                    case 'p':
                        {
                            Print(list);
                            Console.WriteLine();
                            break;
                        }
                    case 'A':
                    case 'a':
                        {
                            Console.Write("Enter a number to add -->");
                            int added = System.Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine(AddComponent(list,added));
                            Console.WriteLine();
                            break;
                        }
                    case 'M':
                    case 'm':
                        {
                            Console.WriteLine("Calculating The mean value...");
                            Console.WriteLine($"The mean value is: {CalculateMean(list)}");
                            Console.WriteLine();
                            break;
                        }
                    case 'F':
                    case 'f':
                        {
                            Console.Write("Enter a Number to find its index -->");
                            int inp = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine(FindIndex(list , inp));
                            Console.WriteLine();
                            break;
                        }
                    case 'S':
                    case 's':
                        {
                            Console.WriteLine("Getting the smllest number...");
                            Console.WriteLine($"The smallest number in the list is {GetSmallest(list)}");
                            Console.WriteLine();
                            break;
                        }
                    case 'L':
                    case 'l':
                        {
                            Console.WriteLine("Getting Largest number...");
                            Console.WriteLine($"The largest number is: {GetLargest(list)}");
                            Console.WriteLine();
                            break;
                        }
                    case 'C':
                    case 'c':
                        {
                            list.Clear();
                            Console.WriteLine("List cleared successfully");
                            Console.WriteLine();
                            break;
                        }
                    case 'Q':
                    case 'q':
                        {
                            break;
                        }
                    case 'X':
                    case 'x':
                        {
                            Console.Write("Enter two Nums to swap between them --> ");   // Swapping items in the list {Bonus}
                            string[] inputs = Console.ReadLine().Split(" ");
                            int num1 = Convert.ToInt32(inputs[0]);
                            int num2 = Convert.ToInt32(inputs[1]);
                            Console.WriteLine(SwapTwoNums(list,num1,num2));
                            break;
                        }
                    case 'V':
                    case 'v':
                        {
                            Console.WriteLine("Sorting ascendingly..."); // Sorting Ascendingly {Bonus}
                            Console.WriteLine(SortAscending(list));
                            Print(list);
                            break;
                        }
                    case '^':
                        {
                            Console.WriteLine("Sorting descendingly..."); // Sorting Descendingly {Bonus}
                            Console.WriteLine(SortDescending(list));
                            Print(list);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid input");
                            Console.WriteLine();
                            break;
                        }

                }
                if (input == 'Q' || input == 'q')
                {
                    Console.WriteLine("Quitted Successfully...");
                    break;
                }
            }
        }
    }
}
