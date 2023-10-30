

public class TextContainer 
{
    
    public List<TextContainerFile> Files {get; set;}
    public TextContainer(List<TextContainerFile> files)
    {
        Files = files;
    }
}