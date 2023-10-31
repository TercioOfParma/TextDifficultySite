# TextDifficultySite


# To Do

# Upload Texts
- Setup SQLite to track words and their relative ranks by language
- Create means of breaking down text and counting their words, and then saving this in the database
- Create means of taking in Leipzig Corpus Information and translating it into the same database 
- That TSV format that was for the Septuagint that was found 

# Test Their Difficulty
- Take in text from a textbox and test
- Take in a text from a text file and test
- .docx and .pdf? 
- .epub?
- Upload a Zip file with a bunch of .txt files in them, and then evaluate the difficulties of each text file relative to the others
    - Generate word frequency table
    - Count the words within each file and generate an aggregate score
    - Calculate according to the algorithm 

# Difficulty Test Algorithm
Scoring currently works as follows:
    - Difficulty of a Word:
        Difficulty = log2( ( Overall Words Logged by Dictionary Corpus / Occurences of Word in Corpus) * 12 ) / 125,000)
        - 12 is the number of times a word needs to be read to be memorised generally, 125,000 is a number used to crunch numbers down for smaller difficulty scores within a preplanned scale below
    - To regenerate the threshold, you do the following:
        Threshold = 2^Score * 125,000

# Stretch Goals
- Euclidean Distance esque algorithm to indicate relative difference and difficulties of texts with Vectors (In fact, I think this may be superior 
to simply having a )
- Using Euclidean Distance to cluster texts in a corpus 

# Scales of Difficulty
This scale indicates how much undifferentiated text a reader would need to read in order to encounter the given word 12 times and their corresponding scoring:

Beginner 1: 250k 1
Beginner 2: 500k 2
Intermediate 1: 1M (B1 for Kazakhov) 3 
Intermediate 2: 2M 4
Advanced 1: 4M (Upper end of where JustinLearnsLatin made French read fluently) 5
Advanced 2: 8M 