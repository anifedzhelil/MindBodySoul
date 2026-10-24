namespace MindBodySoul.Models.Enum
{
    public enum ArticleStatusFilter
    {
        // Default value, returned when the client sends no status
        All = 0,
        // IsPublished == false and IsArchived == false
        Draft = 1,
        // IsPublished == true and IsArchived == false
        Published = 2,
        // IsArchived == true
        Archived = 3
    }
}
