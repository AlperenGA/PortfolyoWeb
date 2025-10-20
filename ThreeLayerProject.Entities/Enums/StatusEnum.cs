namespace ThreeLayerProject.Entities.Enums
{
    /// <summary>
    /// Entity durumlarını temsil eder.
    /// </summary>
    public enum StatusEnum
    {
        Inactive = 0,   // Pasif
        Active = 1,     // Aktif
        Pending = 2,    // Beklemede
        Deleted = 3     // Silinmiş (soft delete için)
    }
}
