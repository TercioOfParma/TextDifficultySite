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

# Difficulty Test Algorithm 
Category:
    - Count words by occurence and by percentage
        - Occurence goes by jumps described by Kazakov
        - Percentage goes by 1% from 87% up
    

Overall Score:
	Score = (Overall Difficulty of All Words / Word Count) * Sentence Length