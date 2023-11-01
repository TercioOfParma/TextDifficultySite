# Initial Zip to CSV steps

1) Write out data structures for holding the following information: X
    - Logging Information for a word in the frequency dictionary X
    - A container for the frequency dictionary for the Zip File X
    - A container for the word information within a file (I think I'll just use FrequencyDictionary) X
    - A Container for a Zip Request's information X

2) Produce a web page from which a Zip file is uploaded X **Change this to a Web Controller that handles multiple files** X
3) Have an application layer function handle translation of the files into strings X
4) Generate Frequency Dictionaries for Each fileX
5) Concat these together to form an overall Frequency Dictionary X  
6) Calculate Relative Difficulty Scores for Each file and output this to an Excel Sheet 

# Handling ZIP files or an alternative upload form for a corpus of texts?
Zips are kind of unsafe, and I want to avoid having to save any files to the server itself, so how does one group these files together?

I could simply use the multiple file upload component from Radzen, but that defaults to using a Web API endpoint, so that would require a bit of reworking inside of the website (I think that I can have a Web API endpoint inside of a Blazor Website, but it might take some mucking around with the right cookies being sent).

Alternatively, we could just go the Zip -> Unzip on Server -> Delete files route. I think this would be a lot more of a pain than simply having a Multiple Radzen upload, since with the RadzenUpload I could simply throw all of the files at the endpoint at once. 

I think that the former is much easier 

# Does the Word Information Model need to contain a rank, or should it be generated on the fly?

In previous iterations of this algorithm, I've stored the rank as separate. This cuts down on immediate computation costs significantly, as this can be accessed to determine immediately the difficulty of a word. However, this would need some sort of chronjob mechanism to update at intervals to keep it accurate if the website is being constantly updated with new text files to compose the corpus. This could be a bit of a pain. 

If I keep it separate, however, this chronjob aspect could be avoided, and calculations could be done either on the fly with each request to the server (This sounds absolutely terrible due to each additional request putting lots more strain on the server) or an alternative algorithm could be designed to not utilise rank but some other metric (This seems to be contrary to the idea of having a frequency dictionary in the first place).

A distance algorithm could be used only factoring in the difference between current frequencies of the words. This would remove the need to calculate the Rank at all, as all ranking would be relative. However, because of Zipf's Law, this would mean that the distances would intensely spike in the beginning as words like "and" and "the" would end up being compared to less frequent words like "Company", with much more drastic consequences than between "Company" and less common words such as "Gazophylactum", simply due to the overwhelmingly outsized use of the words "And" and "The". 

# Investigating an alternative algorithm taking account of the outsized influence of very high frequency words

This could be solved with a logarithmic function, that is, reducing the influence of the larger numbers by subjecting them to powers of 10. For instance, a word that has a frequency difference of 10,000 would have a score of 3, whereas a difference of 100 would be 2, and so on. So when the difference between "the" and "company" then becomes the difference between 5 and 3, for the sake of argument, it becomes far more manageable and also keeps the extremely high frequency words from having a huge influence. 

# Whether this is wrongheaded?

For determining comprehensible input, the text must have 95% to 98% of the same words. This doesn't seem necessary in that case. 

BUT, this is could be a very useful way of determining what texts are worth reading for the sake of accruing vocabulary. If somebody wants to become a more effective communicator in a given language, it makes sense to me that you would want to get the higher frequency vocabulary first based on where you are. This is where some sort of distance algorithm makes sense, since if a person has the first 1000 words, they'd want the second 1000 up to two thousand and then from there the third so on and so forth, for maximum gain in communication and understanding capability. 


However, the really serious problem is that relative distances would never be constant if the frequency of the words is always changing. Therefore, I'll keep Rank inside of the FrequencyWord Class

# Determining Relative Difficulty of a Text

Looking through an older version of this algorithm I wrote earlier this year, I've found that the difficulty is founded simply on the rank of the word in a frequency dictionary, as mentioned earlier. I don't think this makes much sense in retrospect, because then this would mean that "and" and "or" would have significantly different difficulties.

I'm tempted to use some sort of generated score based on how many words of unprocessed text that it would take to learn said word, and therefore scaffold, perhaps something like:

Difficulty of Word = Log(x)(Average Words Taken To Encounter a Word 12 times (Paul Nation's "Magic" Number))

x being the base. 

I bet there's some sort of logarithmic relationship between the word counts presented by [Kazakov](https://www.academia.edu/49862484/How_much_input_do_we_need_to_learn_a_language) 

There isn't.

Perhaps it's better to draw lines between certain numbers of words consumed and difficulty?

Beginner 1: 250k 1
Beginner 2: 500k 2
Intermediate 1: 1M (B1 for Kazakhov) 3 
Intermediate 2: 2M 4
Advanced 1: 4M (Upper end of where JustinLearnsLatin made French read fluently) 5
Advanced 2: 8M 

This seems to be kinda similar to Kazakhov's in some way. This is based on hearsay and I wish that the Polyglots would get together and get more data on this so that I could make a better division. 

Difficulty Score of Word = log(2)(Score / 125,000) Note from later: It't can't be score, it has to be the number of words to read to 
get twelve repeats of a word 

So this could be log(2)(32) = 5 

Lets say score is 64000000, so that means that it will be log(2)(512) = 9

2^5 

Threshold = 2^9 * 125,000 = 

Score = (Repeats / Dictionary Size)  * 12

Therefore the Final Score should be :

Threshold = 2^Score * 125,0000

# Is this a classification problem?

The overwhelming majority of any text will be easier words. So what even is difficulty?

The difficulty score should be related to the level required for 95% coverage, for realistic reading, and 98% for extensive reading. 

What we are answering is, how many words should I have read of indiscriminate text to be able to comprehend this?

This should be solved by the following

1) Grab the words and the number of words from a text X
2) Map the difficulty score calculated above from the presaved text corpus X
3) Rank the words in order of the difficulty score from the text corpus X
4) Take the word at the 95% and 98% line X
5) Reverse log these numbers to get the number of words to be read X