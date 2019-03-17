A. SOLUTION

1. The solution to both questions of this assessment is in the top-level folder:

    DeveloperTest

2. Specifically, I have implemented the solution for the DeveloperTestImplementationAsync
    class.

3. My code can be found in the following two classes:

    DeveloperTest.CharExtensions
    DeveloperTest.DeveloperTestImplementationAsync
    
4. I have included my general assumptions and code comments in the above two classes.

B. DOCUMENTS

1. This repository also contains a few supporting documents in the top-level folder:

    Documents

2. The first supporting document is a screen-shot that shows that the unit tests
    in the StandardTestAsync class have been completed successfully. It also shows that in
    order to complete the second test, TestQuestionTwoMultipleAsync, I had to increase
    the test timeout value from 120000 to 220000, else the test would timeout before
    completion. This indicates that my solution can improve, please read GENERAL NOTES
    below for more details.

3. During the implementation of my solution I created a simple test program (not included here)
    to imitate the three unit tests in StandardTestAsync class. In this program I added 'markers'
    in order to be able to follow the control flow between the various asynchronous tasks and also
    for dubugging purposes.

    I have included the outputs of a couple of runs of my test program that imitate the following
    two unit tests:

        TestQuestionTwoSingleAsync
        TestQuestionTwoMultipleAsync

    For the first program run I utilised a single slow reader and for the second program
    run I utilised multiple (three in total) slow readers running in parallel.
    
    Those two documents clearly demonstrate producing outputs of the required format in 10 second
    intervals. I never experienced any timeouts when running my test program, but my
    delay mechanism was a simple Thread.Sleep(requiredTime) at the end of the main method.

C. GENERAL NOTES

1. When running my own program I could complete all the tasks within the 12 second timeout.
    This is not the case for when testing the solution using the provided unit tests. As I
    mentioned above in this case I had to increase the timout value from 120000 to 220000 for
    test: TestQuestionTwoMultipleAsync
    
2. The provided tests always succeeded when ran one by one. When tried to run the tests as
    a batch job, running the lot in an automated manner one after the other, sometimes they failed.
    
3. Finally the Nuget package needed for running these tests was NUnit3TestAdapter.
