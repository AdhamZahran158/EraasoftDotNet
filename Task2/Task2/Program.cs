namespace Task2
{
    internal class Program
    {
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
                            Console.WriteLine();
                            break;
                        }
                    case 'A':
                    case 'a':
                        {
                            Console.Write("Enter a number to add -->");
                            int added= System.Convert.ToInt32(Console.ReadLine());
                            bool exists =false; //for dublication !!! {Bonus}
                            for(int i=0; i< list.Count; i++)
                            {
                                if(list[i]==added)
                                { 
                                    Console.WriteLine("Item already exists");
                                    exists = true;
                                }
                            }
                            if (!exists)
                            {
                                list.Add(added);
                                Console.WriteLine($"Added number : {added}");
                                Console.WriteLine();
                            }
                            Console.WriteLine();
                            break;
                        }
                    case 'M':
                    case 'm':
                        {
                            Console.WriteLine("Calculating The mean value...");
                            int Value=0;
                            for(int i = 0; i < list.Count; i++)
                            {
                                Value+= list[i];
                            }
                            double meanValue = Value/list.Count;
                            Console.WriteLine($"The mean value is: {meanValue}");
                            Console.WriteLine();
                            break;
                        }
                    case 'F':
                    case 'f':
                        {
                            Console.Write("Enter a Number to find its index -->");
                            int inp = Convert.ToInt32(Console.ReadLine());
                            int verified = 0;
                            for(int i=0; i < list.Count;i++)
                            {
                                if (list[i] == inp)
                                {
                                    Console.WriteLine($"Found your number at index {i}");
                                    verified++;
                                }
                                if ((verified==0) && (i == list.Count-1))
                                    Console.WriteLine("Did not find your number :(");
                            }
                            Console.WriteLine();
                            break;
                        }
                    case 'S':
                    case 's':
                        {
                            Console.WriteLine("Getting the smllest number...");
                            int min = int.MaxValue;
                            for (int i = 0; i < list.Count; i++)
                            {
                                if (list[i]<min)
                                {
                                    min = list[i];
                                }
                            }
                            Console.WriteLine($"The smallest number in the list is {min}");
                            Console.WriteLine();
                            break;
                        }
                    case 'L':
                    case 'l':
                        {
                            Console.WriteLine("Getting Largest number...");
                            int max = 0;
                            for (int i = 0; i < list.Count;i++)
                            {
                                if (list[i]>max)
                                    max = list[i];
                            }
                            Console.WriteLine($"The largest number is: {max}");
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
                            bool num1Available = false;
                            bool num2Available = false;

                            //Check if the two nums exist in the list!
                            for (int i = 0; i < list.Count; i++)
                            {
                                if (list[i]==num1)
                                    num1Available = true;
                                if (list[i]==num2)
                                    num2Available = true;
                            }
                            if(!num1Available || !num2Available)
                                Console.WriteLine("Enter valid numbers \n");
                            else
                            {
                                for(int i = 0;i < list.Count;i++)
                                {
                                    if(list[i]==num1)
                                        list[i] = num2;
                                    else if (list[i] == num2)
                                        list[i] = num1;
                                }
                                Console.WriteLine($"Swapped number {num1} with {num2} \n");
                            }
                                break;
                        }
                    case 'V':
                    case 'v':
                        {
                            Console.WriteLine("Sorting ascendingly..."); // Sorting Ascendingly {Bonus}
                            for(int i=0;i<list.Count;i++)
                            {
                                for(int j=i;j<list.Count;j++)
                                {
                                    if (list[j]<list[i])
                                    {
                                        int temp = list[i];
                                        list[i] = list[j];
                                        list[j] = temp;
                                    }
                                }
                            }
                            Console.WriteLine("Arranged ascendingly successfully! \nList:");
                            for (int i = 0; i < list.Count; i++)
                            {
                                Console.WriteLine(list[i]);
                            }
                            break;
                        }
                    case '^':
                        {
                            Console.WriteLine("Sorting descendingly..."); // Sorting Descendingly {Bonus}
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
                            Console.WriteLine("Arranged descendingly successfully! \nList:");
                            for (int i = 0; i < list.Count; i++)
                            {
                                Console.WriteLine(list[i]);
                            }
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