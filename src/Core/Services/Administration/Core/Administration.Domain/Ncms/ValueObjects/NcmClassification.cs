namespace Administration.Domain.Ncms.ValueObjects
{
    public record NcmClassification : ValueObject
    {
        public string? Group { get; init; }
        public string? Subgroup { get; init; }
        public string? Section { get; init; }
        public string? SectionDetail { get; init; }

        public NcmClassification(string? group, string? subgroup, string? section, string? sectionDetail)
        {
            Group = Normalize(group);
            Subgroup = Normalize(subgroup);
            Section = Normalize(section);
            SectionDetail = Normalize(sectionDetail);
        }

        private static string? Normalize(string? value) => string.IsNullOrWhiteSpace(value) ? null : value;
    }
}
