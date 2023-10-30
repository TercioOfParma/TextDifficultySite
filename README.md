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
Category:
    - Count words by occurence and by percentage
        - Occurence goes by jumps described by Kazakov
        - Percentage goes by 1% from 87% up

Overall Score:
	Score = (Overall Difficulty of All Words / Word Count) * Sentence Length
Have considered doing something more interesting with Euclidean Distance, which could indicate a magnitude of difference from an "Average Text", but this seems useless. However, this could be a very useful feature for judging relative difficulties between texts (Say, comparing the Book of Romans to 
the Gospel of Matthew)

# Stretch Goals
- Euclidean Distance esque algorithm to indicate relative difference and difficulties of texts with Vectors (In fact, I think this may be superior 
to simply having a )
- Using Euclidean Distance to cluster texts in a corpus 