# Initial Zip to CSV steps

1) Write out data structures for holding the following information:
    - Logging Information for a word in the frequency dictionary
    - A container for the frequency dictionary for the Zip File
    - A container for the word information within a file 

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
