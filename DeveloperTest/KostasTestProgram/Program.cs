using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using KostasGlobalProperties;
using KostasTestProgram.TestPrograms;

namespace KostasTestProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            GlobalProperties.VerboseMode = true;
            bool extraVerboseMode = false;

            char key;

            DisplayMenu();

            do
            {
                key = Console.ReadKey(true).KeyChar;

                if (key == '1')
                {
                    // TEST QUESTION ONE WITH SINGLE CHARACTER READER
                    QuestionOneTestProgram.TestQuestionOneWithSingleCharacterReader(extraVerboseMode);
                    Thread.Sleep(2000);
                }
                else if (key == '2')
                {
                    // TEST QUESTION ONE WITH SLOW CHARACTER READER
                    QuestionOneTestProgram.TestQuestionOneWithSlowCharacterReader(extraVerboseMode);
                    Thread.Sleep(70000);
                }
                else if (key == '3')
                {
                    // TEST QUESTION TWO WITH SINGLE SLOW CHARACTER READER
                    QuestionTwoTestProgram.TestQuestionTwoWithSingleSlowCharacterReader(extraVerboseMode);
                    Thread.Sleep(80000);
                }
                else if (key == '4')
                {
                    // TEST QUESTION TWO WITH MULTIPLE SLOW CHARACTER READERS (running in parallel)
                    QuestionTwoTestProgram.TestQuestionTwoWithMultipleSlowCharacterReaders(extraVerboseMode);
                    Thread.Sleep(80000);
                }
                else if (key == 'q' || key == 'Q')
                {
                    break;
                }
                else
                {
                    DisplayUsageMessage();
                }

                WaitForTestResults();

                DisplayMenu();

            } while (key != 'q' && key != 'Q');
        }

        private static void DisplayUsageMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Please use keys 1, 2, 3, 4, and q only");
            Console.WriteLine();
        }

        private static void DisplayMenu()
        {
            Console.Clear();

            Console.WriteLine();
            Console.WriteLine("Please enter your choise:\n");
            Console.WriteLine("1: Test Question One with Simple Character Reader");
            Console.WriteLine("2: Test Question One with Slow Character Reader");
            Console.WriteLine("3: Test Question Two with Single Slow Character Reader");
            Console.WriteLine("4: Test Question One with Multiple Slow Character Readers");
            Console.WriteLine();
            Console.WriteLine("q: Exit this program");
            Console.WriteLine();
        }

        private static void WaitForTestResults()
        {
            Console.WriteLine("\nPress a key to go back to menu...\n");
            Console.ReadKey();
        }
    }
}
