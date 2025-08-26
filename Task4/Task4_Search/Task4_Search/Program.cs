namespace Task4_Search
{
    internal class Program
    {
        static void ExceptionTest (string sample)
        {
            if (!(sample.Contains('a') || sample.Contains('o') || sample.Contains('u') || sample.Contains('i') || sample.Contains('e')))
            {
                throw new Exception();
            }
        }
        static void Main(string[] args)
        {
            // ===================================== 1 ========================================

            //List<int> list = new List<int>();

            //while (true)
            //{
            //        Console.Write("Enter A number --> ");
            //        int num = Convert.ToInt32(Console.ReadLine());
            //    try
            //    {
            //        if (list.Contains(num))
            //            throw new Exception();
            //        list.Add(num);
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("Rebeated Number Please Try Another");
            //    }
            //}



            // ===================================== 2 =========================================

            try
            {
                ExceptionTest(Console.ReadLine().ToLower());
            }
            catch (Exception ex)
            {
                Console.WriteLine("your word does not contain vowel letters");
            }
        }
    }
}
