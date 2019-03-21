A. KNOWN ISSUES
===============

1. This is a complete solution for both questions, i.e. question 1 and 2. This solution
    passes successfully all three unit tests in class StandardTestAsync, namely:
    
    StandardTestAsync.TestQuestionOneAsync()
    StandardTestAsync.TestQuestionTwoMultipleAsync()
    StandardTestAsync.TestQuestionTwoSingleAsync()
    
    The only issue is that when the three unit test are executed in Visual Studio Test
    Explorer as a batch job, i.e. in an automated way one after the other, sometimes
    the second and the third test fail. The issues seems to be related to the disposal
    of the simple character reader object of the first test, i.e. probably it is not
    disposed on time after the end of the test. Sorry I didn't have the time to investigate
    this further.
    
    When the tests are executed manually in Test Explorer, i.e. one at a time, they
    always pass successfully.
    
    In order to show how my solution works I have created a test program in project
    
    KostasTestProgram
    
    Please execute the program (Ctlr + F5) in order to see how my solution reads characters
    from the readers, creates outputs (all at once for question 1 and every 10 seconds for
    question 2), runs tasks in parallel as required, etc.
    
2. I have resolved a previous issue that I had with one of the tests timing out. This is
    not an issue anymore.
    
--------------------------------------------------------------------------------------------------------

A. SOLUTION
===========

1. The solution to both questions of this assessment is in the top-level folder:

    DeveloperTest

2. Specifically, I have implemented the solution for the DeveloperTestImplementationAsync
    class.

3. My code can be found in the following two classes:

    DeveloperTest.CharExtensions
    DeveloperTest.DeveloperTestImplementationAsync
    
4. I have included my general assumptions and code comments in the above two classes.

5. For testing purposes (for better visualisation of the results and for debugging purposes)
    I have also included a test program inside the project:
    
    KostasTestProgram
    
    Please run this program in order to see the results of my solutions to questions 1 and 2.
    
6. It took me quite a bit more than two hours to complete, document and test this solution.

--------------------------------------------------------------------------------------------------------

B. DOCUMENTS
============

1. This repository also contains a few supporting documents in the top-level folder:

    Documents
    
2. This README_FIRST.txt file is the fitst supporting document. As you can see it provides an
    overview of any known issues, information about the solution and the supporting documents,
    and some other general notes.

3. The second supporting document is a screen-shot that shows that all three unit tests
    in the StandardTestAsync class have been completed successfully.
    
4. The rest of the supporting documents are a small sample of outputs produced by my test program
    for the following scenarios:
    
    a. Output for Question 1 with Single Simple Character Reader - imitates test:
    
        StandardTestAsync.TestQuestionOneAsync()
        
    b. Output for Question 1 with Single Slow Character Reader. It doesn't imitate
        any of the provided tests.
    
    c. Output for Question 2 with Single Slow Character Reader - imitates test:
    
        StandardTestAsync.TestQuestionTwoSingleAsync()
        
    d. Output for Question 2 with Multiple (3 in total) Slow Character Readers running
        in parallel - imitates test:
        
        StandardTestAsync.TestQuestionTwoMultipleAsync()
        
--------------------------------------------------------------------------------------------------------

C. GENERAL NOTES
================
    
3. For the utilisation of the provided unit tests I had to install the following Nuget package:

    NUnit3TestAdapter
