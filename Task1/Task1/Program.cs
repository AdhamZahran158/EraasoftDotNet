int smallCarpetPrice = 25;
int largeCarpetPrice = 35;
double tax = 0.06;
int numOfSmallCarpets = 0;
int numOfLargeCarpets = 0;

System.Console.WriteLine("Islam's Carpet Cleaning Service\r\n    Charges:\r\n        $25 per small\r\n        $35 per large\r\n    Sales tax rate is 6%\r\n");
System.Console.Write("Number of small carpets: ");
numOfSmallCarpets = System.Convert.ToInt32(System.Console.ReadLine());
System.Console.Write("Number of large carpets: ");
numOfLargeCarpets = System.Convert.ToInt32(System.Console.ReadLine());
int costWithoutTax = numOfLargeCarpets * largeCarpetPrice + numOfSmallCarpets * smallCarpetPrice;
System.Console.WriteLine($"Price per small room: $25\r\nPrice per large room: $35\r\nCost : ${costWithoutTax}\r\nTax: ${costWithoutTax*tax}");
System.Console.WriteLine($"===============================\r\nTotal estimate: ${costWithoutTax + costWithoutTax*tax} \r\nThis estimate is valid for 30 days\r\n");
