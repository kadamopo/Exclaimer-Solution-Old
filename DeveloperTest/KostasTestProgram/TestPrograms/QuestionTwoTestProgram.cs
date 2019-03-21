using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DeveloperTest;
using DeveloperTestInterfaces;
using DeveloperTestSupport;
using DeveloperTestFramework;

namespace KostasTestProgram.TestPrograms
{
    public static class QuestionTwoTestProgram
    {
        #region Test Question Two with Single Slow Character Reader

        public static void TestQuestionTwoWithSingleSlowCharacterReader(bool verbose)
        {
            if (verbose)
            {
                Console.WriteLine("TestQuestionTwoWithSingleSlowCharacterReader: started");
                AwaitQuestionTwoWithSingleSlowCharacterReaderAsync(verbose);
                Console.WriteLine("TestQuestionTwoWithSingleSlowCharacterReader:\n\tLine after calling AwaitQuestionOneWithSimpleCharacterReaderAsync()");
                Console.WriteLine("TestQuestionTwoWithSingleSlowCharacterReader: ended");
            }
            else
            {
                AwaitQuestionTwoWithSingleSlowCharacterReaderAsync(verbose);
            }
        }

        private async static void AwaitQuestionTwoWithSingleSlowCharacterReaderAsync(bool verbose)
        {
            if (verbose)
            {
                Console.WriteLine("AwaitQuestionTwoWithSingleSlowCharacterReaderAsync: started");
                await TestQuestionTwoWithSingleSlowCharacterReaderAsync(verbose);
                Console.WriteLine("AwaitQuestionTwoWithSingleSlowCharacterReaderAsync: ended");
            }
            else
            {
                await TestQuestionTwoWithSingleSlowCharacterReaderAsync(verbose);
            }
        }

        private static async Task TestQuestionTwoWithSingleSlowCharacterReaderAsync(bool verbose)
        {
            DeveloperTestImplementationAsync test = new DeveloperTestImplementationAsync();

            var output = new Question2TestOutput();
            using (var slowReader = new SlowCharacterReader())
            {
                if (verbose)
                {
                    Console.WriteLine("TestQuestionTwoWithSingleSlowCharacterReaderAsync: started");
                    await test.RunQuestionTwo(new ICharacterReader[] { slowReader }, output);
                    Console.WriteLine("TestQuestionTwoWithSingleSlowCharacterReaderAsync: ended");
                }
                else
                {
                    await test.RunQuestionTwo(new ICharacterReader[] { slowReader }, output);
                }
            }
        }

        #endregion

        #region Test Question Two with Multiple Slow Character Readers (running in parallel)

        public static void TestQuestionTwoWithMultipleSlowCharacterReaders(bool verbose)
        {
            if (verbose)
            {
                Console.WriteLine("TestQuestionTwoWithMultipleSlowCharacterReaders: started");
                AwaitQuestionTwoWithMultipleSlowCharacterReadersAsync(verbose);
                Console.WriteLine("TestQuestionTwoWithMultipleSlowCharacterReaders:\n\tLine after calling AwaitQuestionOneWithSimpleCharacterReaderAsync()");
                Console.WriteLine("TestQuestionTwoWithMultipleSlowCharacterReaders: ended");
            }
            else
            {
                AwaitQuestionTwoWithMultipleSlowCharacterReadersAsync(verbose);
            }
        }

        private async static void AwaitQuestionTwoWithMultipleSlowCharacterReadersAsync(bool verbose)
        {
            if (verbose)
            {
                Console.WriteLine("AwaitQuestionTwoWithMultipleSlowCharacterReadersAsync: started");
                await TestQuestionTwoWithMultipleSlowCharacterReadersAsync(verbose);
                Console.WriteLine("AwaitQuestionTwoWithMultipleSlowCharacterReadersAsync: ended");
            }
            else
            {
                await TestQuestionTwoWithMultipleSlowCharacterReadersAsync(verbose);
            }
        }

        private static async Task TestQuestionTwoWithMultipleSlowCharacterReadersAsync(bool verbose)
        {
            DeveloperTestImplementationAsync test = new DeveloperTestImplementationAsync();

            var output = new Question2TestOutput();
            using (var slowReader1 = new SlowCharacterReader())
            using (var slowReader2 = new SlowCharacterReader())
            using (var slowReader3 = new SlowCharacterReader())
            {
                if (verbose)
                {
                    Console.WriteLine("TestQuestionTwoWithMultipleSlowCharacterReadersAsync: started");
                    await test.RunQuestionTwo(new ICharacterReader[] { slowReader1, slowReader2, slowReader3 }, output);
                    Console.WriteLine("TestQuestionTwoWithMultipleSlowCharacterReadersAsync: ended");
                }
                else
                {
                    await test.RunQuestionTwo(new ICharacterReader[] { slowReader1, slowReader2, slowReader3 }, output);
                }
            }
        }

        #endregion
    }

}
