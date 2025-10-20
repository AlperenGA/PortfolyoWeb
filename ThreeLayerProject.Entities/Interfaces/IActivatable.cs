namespace ThreeLayerProject.Entities.Interfaces
{
    public interface IActivatable
    {
        bool IsActive { get; set; }
        void Activate();
        void Deactivate();
    }
}
