#region Copyright statement
// --------------------------------------------------------------
// Copyright (C) 1999-2016 Exclaimer Ltd. All Rights Reserved.
// No part of this source file may be copied and/or distributed 
// without the express permission of a director of Exclaimer Ltd
// ---------------------------------------------------------------
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

using DeveloperTestInterfaces;

namespace DeveloperTest
{
     public sealed class DeveloperTestImplementationAsync : IDeveloperTestAsync
    {
        #region Member constants and variables used for the solution of both questions

        private const int zeroDelayPeriod = 0;
        private const int defaultDelayInterval = 10000;

        private static readonly int defaultDelayPeriod = zeroDelayPeriod;
        private static readonly int delayInterval = defaultDelayInterval;
        private static readonly int questionOneTimeout = 500;
        private static readonly int questionTwoTimeout = 70000;

        // A boolean flag that activates the 10 second output interval mechanism only when necessary, i.e. only for question 2.
        // This mechanism by default is de-activated for question 1 and activated for question 2. To fully de-activate this
        // mechanism for both question 1 and 2 just set the value of this variable, where is used in the code, back to false.
        private static bool wasCallInitiatedInQuestionTwo = false;

        // A dictionary collection that holds words as strings and the frequency of their appearence as integers.
        // This is declared here in order to allow combining multiple outputs easily (needed for question 2b and 2c).
        private static readonly IDictionary<string, int> wordDictionary = new Dictionary<string, int>();

        #endregion

        #region Solution for Question 1

        /// <summary>
        /// This is the answer to question 1. 
        /// 
        /// ASSUMPTIONS:
        /// ============
        /// 
        /// 1. The text read by the reader only contains words of the English language, so in this
        /// case there is no provision for words of other languages written in different alphabets,
        /// or for numerals, etc. Please also see the assumptions listed in class CharExtensions.cs
        /// for more details.
        /// 
        /// 2. The second assumption, and in accordance to assumption 1 above, is that when it comes
        /// to deciding if a character is part of a word or not, we assume that we are always dealing
        /// with characters of the English alphabet, both lower case and capitals, and with any special
        /// symbols that can be accepted as part of an English word, i.e. hyphen. In this case, there
        /// is no provision for characters of different alphabets, numerals, etc.
        /// 
        /// 3. Another assumption is that it is preferable to deal with the input character stream
        /// dynamically, on the fly, instead of reading the whole stream and storing it in a local
        /// variable prior to processing it. This, for example, could help in situations of extremely
        /// long streams (such as when reading a character stream from a large file) that would require
        /// the use of extensive amounts of in-memory storage prior to processing.
        /// 
        /// LOGIC:
        /// ======
        /// 
        /// An instance of this class (DeveloperTestImplementationAsync) is created elsewhere in the code
        /// (for example in a unit test such as StandardTestAsync.TestQuestionOneAsync()). This method
        /// can also be called asynchronously as part of the solution of question 2. When it is called
        /// two objects are passed into it as dependencies using the method dependency injection pattern,
        /// the first one is a reader object, for example an instance of the SimpleCharacterReader or the
        /// SlowCharacterReader class, they both implement the ICharacterReader interface, and the second
        /// one is an output object, which is an instance of the Question1TestOutput class that implements
        /// the IOutputResult interface.
        /// 
        /// The purpose of this method is to use these two objects in order to read a character stream,
        /// dynamically process the character stream in order to separate it into English words, then order
        /// those words by frequency and then alphabetically, and finally create an appropriate output in
        /// the required output format that will be tested by the relevant unit test mentioned above for its
        /// correctness (i.e. the words that it contains, the frequency of appearence of each word in the character
        /// stream, and whether these words have been ordered by frequency and alphabetically as required).
        /// 
        /// Reading the character stream: In order to successfully read the character stream this method uses
        /// a simple do-while loop that in each iteration reads a new character, then process the character
        /// in order to make a decision if it should be accepted as part of an English word or not, and
        /// consequently adds the character in the next word or rejects it.
        /// 
        /// EndOfStreamException: The algorithm then deals with the EndOfStreamException, thrown by the reader
        /// when the end of the character stream has been reached, by making sure in the finally sub-block of the
        /// try-catch-finally block that the very last word read is captured correctly and stored in the dictionary in the
        /// same way as with all the previous words.
        /// 
        /// Finally, this method makes sure that the dictionary of words and word frequences is sorted
        /// according to the requirements, i.e. first by word frequency and then alphabetically, and then creates
        /// the desired output.
        /// 
        /// Parts of this algorithm, such as processing a character, forming a word, sorting the dictionary, and
        /// creating the required output, have been implemented as separate methods in order to modularise and
        /// declutter the main algorithm. The purpose here is to improve its readibity and at the same time to
        /// demonstrate how to create methods for reoccurring tasks and functions, for improved reusability,
        /// and for making it more clear where and how the state of certain objects is changed (instead of changing the
        /// state of these objects all over the place). I have chosen to pass parameters back and forth to these
        /// methods (even if in some cases they are dealing with the member variables that are already available
        /// to them) in order to show that we could easily move them out of this class altogether, maybe into some
        /// sort of helper class where they could be re-used by other classes of a hypothetical bigger program.
        /// For simplicity I have left these methods in this class.
        /// </summary>
        /// <param name="reader">An object of a class that implements the ICharacterReader interface, which provides
        /// a method for reading the next character of a character stream.</param>
        /// <param name="output">An object of a class that implements the IOutputResult interface, which allows to
        /// format an output according to specified requirements.</param>
        public Task RunQuestionOne(ICharacterReader reader, IOutputResult output)
        {
            // This whole task can run asynchronously. This is useful when we need to run several readers
            // in parallel and we do not want to wait for the synchronous completion of each reader before
            // starting the next one.
            return Task.Run(async () =>
            {
                // A string variable that helps us form the next word from the input character stream.
                string nextWord = string.Empty;

                using (reader)
                {
                    try
                    {
                        // This is a conditional method (the condition is located within the method
                        // implementation). It is used only by the solution for question 2 in order
                        // to generate an output every 10 seconds or any other specified interval.
                        CreateIntervalOutputs(wordDictionary, output, delayInterval);

                        // This is the main loop that reads a stream of characters, one by one,
                        // processes the characters according to the assumptions made above, forms
                        // English words and stores these words to a dictionary collection,
                        // keeping also track of how often each of these words appear in the input
                        // stream (word frequency).
                        do
                        {

                            ProcessNextChar(reader.GetNextChar(), ref nextWord, wordDictionary);

                        } while (true);
                    }
                    catch (EndOfStreamException e)
                    {
                        // Normally an error message, like the one below, would be logged in a log file or
                        // log database, by being passed to an appropriate method of a dedicated logger object.
                        // As this is out of the scope of this exercise, for now I am just imitating
                        // logging the error message by just displaying the error message to the console.
                        Console.WriteLine($"Error reading stream: {e.GetType().Name}.");
                    }
                    finally
                    {
                        // Here we make sure that we do not miss out the very last word of the input stream
                        // because of the EndOfStreamException thrown by the GetNextChar() method of the reader.
                        if (nextWord != string.Empty)
                        {
                            AddStringToDictionary(wordDictionary, nextWord);
                        }
                    }

                    // Sort the dictionary by word frequency and then alphabetically and then
                    // create the required output. The delay period is part of the mechanism that
                    // allows to create an output on specified intervals. The default value is
                    // zero, meaning an immediate creation of the output.
                    CreateOutputAsync(SortDictionary(wordDictionary), output, defaultDelayPeriod);

                    // Allow some time for the completion of this task before exiting.
                    await DelayTimerAsync(questionOneTimeout);
                }
            });
        }

        /// <summary>
        /// This method processes a character and decides if it could be part of an English word or not.
        /// In order to come to that decision it utilises the extension methods defined in CharExtension.cs,
        /// such as IsLetter() and IsAcceptedSymbol().
        /// 
        /// If a character can be accepted then it adds it to the next word. It continuous forming the next
        /// word until it reaches the first character that is not acceptable, e.g. a white space, a comma,
        /// a new line character, etc. At this point it resets the variable that holds the next word
        /// and starts again.
        /// 
        /// Special provision exists for a double hyphen. The solution provided here for this special case
        /// is not particularly elegant, but I realised that 'daisy-chain' is a single word, meaning that I
        /// have to accept a single hyphen as part of a word, very late in the development process
        /// of this solution, so this is more of a quick fix to an existing algorithm.
        /// </summary>
        /// <param name="nextChar">Character to be processed.</param>
        /// <param name="nextWord">The word to be formed next processing the incoming stream of characters.</param>
        /// <param name="wordDictionary">The dictionay collection that holds words and their frequencies of
        /// appearence in the character stream.</param>
        private void ProcessNextChar(char nextChar, ref string nextWord, IDictionary<string, int> wordDictionary)
        {
            // If the next character is an accepted character then add it to the next word.
            if (nextChar.IsLetter() || nextChar.IsAcceptedSymbol())
            {
                nextWord += nextChar.ToString().ToLower();
            }
            // Else, add the word or words (in the case of a double hyphen)
            // and reset the string that holds the next word.
            else
            {
                if (nextWord != string.Empty)
                {
                    if (!nextWord.Contains("--"))
                    {
                        AddStringToDictionary(wordDictionary, nextWord);
                    }
                    else
                    {
                        nextWord = nextWord.Replace("--", "-");
                        string[] wordArray = nextWord.Split('-');
                        for (int index = 0; index < wordArray.Length; index++)
                        {
                            AddStringToDictionary(wordDictionary, wordArray[index]);
                        }
                    }

                    nextWord = string.Empty;
                }
            }
        }

        /// <summary>
        /// Mechanism for creating an output in given intervals (e.g. 10 seconds) as per requirements.
        /// </summary>
        /// <param name="dictionary">The collection that contains the data for the output.</param>
        /// <param name="output">The output where the data will be added.</param>
        /// <param name="delayInterval">The interval that specifies how often to create an output.</param>
        private void CreateIntervalOutputs(IDictionary<string, int> dictionary, IOutputResult output, int delayInterval = defaultDelayInterval)
        {
            if (wasCallInitiatedInQuestionTwo)
            {
                int delayPeriod = delayInterval;

                for (int i = 0; i < questionTwoTimeout / delayInterval; i++)
                {
                    CreateOutputAsync(SortDictionary(wordDictionary), output, delayPeriod);

                    delayPeriod += delayInterval;
                }
            }
        }

        /// <summary>
        /// This method adds a string to a dictionary that has string keys and integer values.
        /// 
        /// The idea here is that the first time we add a string as a key into the dictionary we set
        /// the corresponding value for that key to one. Everytime that the same string key comes back
        /// for addition to the dictionary, instead of adding it again (that is not possible anyway
        /// because of the uniqueness of the keys) we increase its corresponding value by one. 
        /// 
        /// In relation to the problem in hand this seems to be a good representation of the collection
        /// of words that we get from the input stream of characters, together with their frequencies.
        /// Everytime we come across a new word we add it to the dictionary. On the other hand if the
        /// word has appeared before, meaning it has already been added to the dictionary, we just
        /// increase its value by one, i.e. we use the value to represent the frequency of the word
        /// in the input stream.
        /// </summary>
        /// <param name="dictionary">A dictionary collection with string keys and integer values.</param>
        /// <param name="str">A given string to be added to the dictionary.</param>
        private void AddStringToDictionary(IDictionary<string, int> dictionary, string str)
        {
            if (dictionary != null)
            {
                if (!dictionary.TryGetValue(str, out int value))
                {
                    dictionary.Add(str, 1);
                }
                else
                {
                    dictionary[str]++;
                }
            }
        }

        /// <summary>
        /// Sorts a dictionay first by its integer values in descending order and then by its string keys
        /// alphabetically.
        /// </summary>
        /// <param name="dictionary">Dicrionary to be sorted.</param>
        /// <returns>An ordered and enumerable collection of key/value pairs, in this case string/int pairs.</returns>
        private IOrderedEnumerable<KeyValuePair<string, int>> SortDictionary(IDictionary<string, int> dictionary)
        {
            return dictionary.OrderByDescending(p => p.Value)
                            .ThenBy(p => p.Key);
        }

        /// <summary>
        /// An async Task that allows to await the completion of a delay timer.
        /// </summary>
        /// <param name="delayPeriod">The delay period necessary for the completion of the timer.</param>
        /// <returns>When the timer is completed the task returns.</returns>
        private async Task DelayTimerAsync(int delayPeriod)
        {
            await DelayTimer(delayPeriod);
        }

        /// <summary>
        /// A task that implements a simple waiting mechanism for a specified period of time.
        /// </summary>
        /// <param name="delayPeriod">The dealy period that the delay will occur for.</param>
        /// <returns>When the required delay period has expired the task returns.</returns>
        private Task DelayTimer(int delayPeriod)
        {
            return Task.Run(() =>
            {
                Thread.Sleep(delayPeriod);
            });
        }

        /// <summary>
        /// A method that allows to await the creation of an output.
        /// </summary>
        /// <param name="pairs">An ordered and enumerable collection of key/value pairs, in this case string/int pairs,
        /// containing the data that will be used for the creation of the output.</param>
        /// <param name="output">An object of a class that implements the IOutputResult interface.</param>
        /// <param name="delayPeriod">The dealy period needed before the creation of the output.</param>
        private async void CreateOutputAsync(IOrderedEnumerable<KeyValuePair<string, int>> pairs, IOutputResult output, int delayPeriod)
        {
            await DelayTimer(delayPeriod);
            await CreateOutput(pairs, output);
        }

        /// <summary>
        /// A task that allows the creation of an output in the required format.
        /// </summary>
        /// <param name="pairs">An ordered and enumerable collection of key/value pairs, in this case string/int pairs,
        /// containing the data that will be used for the creation of the output.</param>
        /// <param name="output">An object of a class that implements the IOutputResult interface.</param>
        /// <returns>The task that creates the output.</returns>
        private Task CreateOutput(IOrderedEnumerable<KeyValuePair<string, int>> pairs, IOutputResult output)
        {
            return Task.Run(() =>
            {
                foreach (var pair in pairs)
                {
                    output.AddResult($"{pair.Key} - {pair.Value.ToString()}");
                }
            });
        }

        #endregion

        #region Solution for Question 2

        /// <summary>
        /// The solution to question 2 uses the solution to question 1: The idea here is that we can
        /// pass more than one reader objects to this task and just one output object. The readers can then
        /// run in parallel, meaning that they can read streams of characters, process them, form words and
        /// create outputs, all in parallel. This happens by utiliasing the solution to question 1 and
        /// calling the necessary methods in an asynchronous way.
        /// 
        /// Additionally a mechanism has been added to the solution of question 1 for delaying the creation
        /// of outputs for specified periods of times, i.e. creating outputs in specific intervals, as
        /// per the requirements for the solution to question 2.
        /// </summary>
        /// <param name="readers">An array of objects of a class that implements the ICharacterReader interface,
        /// which provides a method for reading the next character of a character stream.</param>
        /// <param name="output">An object of a class that implements the IOutputResult interface, which allows to
        /// format an output according to specific requirements.</param>
        /// <returns>A task that calls the method RunQuestionOne one or more times asynchronously.</returns>
        public Task RunQuestionTwo(ICharacterReader[] readers, IOutputResult output)
        {
            // Reset the following variable to false in order to de-activate the mechanism that produces
            // an output in specified intervals, e.g. 10 seconds.
            wasCallInitiatedInQuestionTwo = true;

            return Task.Run(async () =>
            {
                foreach (var reader in readers)
                {
                    Task task = RunQuestionOneAsync(reader, output);

                    await DelayTimerAsync(questionTwoTimeout);
                }
            });
        }

        /// <summary>
        /// Task that allows us to await the completion of the solution to question 1 without
        /// blocking the execution of the rest of the program.
        /// </summary>
        /// <param name="reader">An object of a class that implements the ICharacterReader interface, which provides
        /// a method for reading the next character of a character stream.</param>
        /// <param name="output">An object of a class that implements the IOutputResult interface, which allows to
        /// format an output according to specific requirements.</param>
        /// <returns>When the solution to Question 1 is complete the task returns.</returns>
        private async Task RunQuestionOneAsync(ICharacterReader reader, IOutputResult output)
        {
            await RunQuestionOne(reader, output);
        }

        #endregion
    }
}