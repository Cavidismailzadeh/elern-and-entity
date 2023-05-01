namespace ClassPractic.Services.Interfaces
{
    public interface ICourse
    {
        Task<List<Course>> GetAll();
    }
}
