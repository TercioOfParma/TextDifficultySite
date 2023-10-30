

public class ZipContainer 
{
    
    public List<ZipContainerFile> Files {get; set;}
    public ZipContainer(List<ZipContainerFile> files)
    {
        Files = files;
    }
}