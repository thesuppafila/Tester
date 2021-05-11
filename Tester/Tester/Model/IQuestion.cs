namespace Tester.Model
{
    public interface IQuestion
    {
        object Clone();
               
        bool IsValid();
    }
}
