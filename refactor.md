These are notes for refactoring 

# Website

- Handle errors better on all sheets. I don't really have error checking
- Look at making the text file loading a bit smaller 
- Remove the ProcessTextFiles Service and move it all into the application layer 
- Look into seeing if there are better ways of downloading files than those suggested with blazor 
- Move the download code that does exist into a command if possible
- Provide an indication in the browser that the process is completed

# Application
- Make Loading Content Take in Lists rather than Individual Files

# Infrastructure


# Domain
- Figure out why the difficulty scoring is so slow 
- Remove all of the Converts
- Ideally, figure out how to store BigInteger in a Database

# HTMXFrontend

- Remove the ProcessTextFiles service and move into Application so it uses the same code as Blazor
- Move each service that has both post and get into separate controller files