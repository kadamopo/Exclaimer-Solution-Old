using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using DeveloperTest;
using DeveloperTestInterfaces;
using DeveloperTestSupport;
using DeveloperTestFramework;

namespace KostasTestProgram.TestPrograms
{
    public static class QuestionOneTestProgram
    {
        #region Question One with Single Character Reader

        public static void TestQuestionOneWithSingleCharacterReader(bool verbose)
        {
            if (verbose)
            {
                Console.WriteLine("TestQuestionOneWithSingleCharacterReader: started");
                AwaitQuestionOneWithSimpleCharacterReaderAsync(verbose);
                Console.WriteLine("TestQuestionOneWithSingleCharacterReader:\n\tLine after calling AwaitQuestionOneWithSimpleCharacterReaderAsync()");
                Console.WriteLine("TestQuestionOneWithSingleCharacterReader: ended");
            }
            else
            {
                AwaitQuestionOneWithSimpleCharacterReaderAsync(verbose);
            }
        }

        private async static void AwaitQuestionOneWithSimpleCharacterReaderAsync(bool verbose)
        {
            if (verbose)
            {
                Console.WriteLine("AwaitQuestionOneWithSimpleCharacterReaderAsync: started");
                await TestQuestionOneWithSimpleCharacterReaderAsync(verbose);
                Console.WriteLine("AwaitQuestionOneWithSimpleCharacterReaderAsync: ended");
            }
            else
            {
                await TestQuestionOneWithSimpleCharacterReaderAsync(verbose);
            }
        }

        private static async Task TestQuestionOneWithSimpleCharacterReaderAsync(bool verbose)
        {
            DeveloperTestImplementationAsync test = new DeveloperTestImplementationAsync();

            var output = new Question1TestOutput();
            using (var simpleReader = new SimpleCharacterReader())
            {
                if (verbose)
                {
                    Console.WriteLine("TestQuestionOneWithSimpleCharacterReaderAsync: started");
                    await test.RunQuestionOne(simpleReader, output);
                    Console.WriteLine("TestQuestionOneWithSimpleCharacterReaderAsync: ended");
                }
                else
                {
                    await test.RunQuestionOne(simpleReader, output);
                }
            }
        }

        #endregion

        #region Question One with Slow Character Reader

        public static void TestQuestionOneWithSlowCharacterReader(bool verbose)
        {
            if (verbose)
            {
                Console.WriteLine("TestQuestionOneWithSlowCharacterReader: started");
                AwaitQuestionOneWithSlowCharacterReaderAsync(verbose);
                Console.WriteLine("TestQuestionOneWithSlowCharacterReader:\n\tLine after calling AwaitQuestionOneWithSlowCharacterReaderAsync()");
                Console.WriteLine("TestQuestionOneWithSlowCharacterReader: ended");
            }
            else
            {
                AwaitQuestionOneWithSlowCharacterReaderAsync(verbose);
            }
        }

        private async static void AwaitQuestionOneWithSlowCharacterReaderAsync(bool verbose)
        {
            if (verbose)
            {
                Console.WriteLine("AwaitQuestionOneWithSlowCharacterReaderAsync: started");
                await TestQuestionOneWithSlowCharacterReaderAsync(verbose);
                Console.WriteLine("AwaitQuestionOneWithSlowCharacterReaderAsync: ended");
            }
            else
            {
                await TestQuestionOneWithSlowCharacterReaderAsync(verbose);
            }
        }

        private static async Task TestQuestionOneWithSlowCharacterReaderAsync(bool verbose)
        {
            DeveloperTestImplementationAsync test = new DeveloperTestImplementationAsync();

            var output = new Question1TestOutput();
            using (var slowReader = new SlowCharacterReader())
            {
                if (verbose)
                {
                    Console.WriteLine("TestQuestionOneWithSlowCharacterReaderAsync: started");
                    await test.RunQuestionOne(slowReader, output);
                    Console.WriteLine("TestQuestionOneWithSlowCharacterReaderAsync: ended");
                }
                else
                {
                    await test.RunQuestionOne(slowReader, output);
                }
            }
        }

        #endregion
    }

}
